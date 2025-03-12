using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientDashboardAPI.Data.Model;
using ClientDashboardAPI.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClientDashboardAPI.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository clientRepository;

        private readonly IClientRepository _clientRepository;
        private readonly IPhoneNumberRepository _phoneNumberRepository;

        public ClientController(
            IClientRepository clientRepository,
            IPhoneNumberRepository phoneNumberRepository
        )
        {
            _clientRepository = clientRepository;
            _phoneNumberRepository = phoneNumberRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClients(
            [FromQuery] int userId,
            bool archived = false
        )
        {
            var clients = await _clientRepository.GetClientsByUserId(userId, archived);
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClient(int userId, int id)
        {
            var client = await _clientRepository.GetClientById(id, userId);
            if (client == null)
            {
                return NotFound();
            }

            var phoneNumbers = await _phoneNumberRepository.GetPhoneNumbersByClientId(id);

            return Ok(new { Client = client, PhoneNumbers = phoneNumbers });
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient(int userId, [FromBody] ClientModel model)
        {
            var client = new Client
            {
                Name = model.Name,
                Description = model.Description,
                UserId = userId,
                IsArchived = false,
            };

            var clientId = await _clientRepository.CreateClient(client);

            if (model.PhoneNumbers != null && model.PhoneNumbers.Count > 0)
            {
                foreach (var phoneNumber in model.PhoneNumbers)
                {
                    await _phoneNumberRepository.AddPhoneNumber(
                        new PhoneNumber { ClientId = clientId, Phone = phoneNumber }
                    );
                }
            }

            return CreatedAtAction(nameof(GetClient), new { id = clientId }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(
            int userId,
            int id,
            [FromBody] ClientModel model
        )
        {
            var client = await _clientRepository.GetClientById(id, userId);
            if (client == null)
            {
                return NotFound();
            }

            client.Name = model.Name;
            client.Description = model.Description;

            await _clientRepository.UpdateClient(client);

            var existingPhoneNumbers = await _phoneNumberRepository.GetPhoneNumbersByClientId(id);

            foreach (var existingPhone in existingPhoneNumbers)
            {
                if (!model.PhoneNumbers.Contains(existingPhone.Phone))
                {
                    await _phoneNumberRepository.DeletePhoneNumber(existingPhone.Id);
                }
            }

            var existingPhoneValues = existingPhoneNumbers.Select(p => p.Phone).ToList();
            foreach (var phoneNumber in model.PhoneNumbers)
            {
                if (!existingPhoneValues.Contains(phoneNumber))
                {
                    await _phoneNumberRepository.AddPhoneNumber(
                        new PhoneNumber { ClientId = id, Phone = phoneNumber }
                    );
                }
            }
            return NoContent();
        }

        [HttpPut("{id}/archive")]
        public async Task<IActionResult> ArchiveClient(int userId, int id)
        {
            var client = await _clientRepository.GetClientById(id, userId);
            if (client == null)
            {
                return NotFound();
            }

            client.IsArchived = true;
            await _clientRepository.UpdateClient(client);

            return NoContent();
        }
    }

    public class ClientModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> PhoneNumbers { get; set; } = new List<string>();
    }
}