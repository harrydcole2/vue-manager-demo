using System.Collections.Generic;
using System.Threading.Tasks;
using ClientDashboardAPI.Data.Model;
using ClientDashboardAPI.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClientDashboardAPI.Controllers
{
    [Route("api/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Client>>> GetAllClients()
        {
            return Ok(await clientRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            return Ok(await clientRepository.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<Client>> CreateClient([FromBody] Client client)
        {
            await clientRepository.CreateAsync(client);
            return Ok();
        }

        [HttpPut("{id}")]
        public void UpdateClient(int id, [FromBody] Client client)
        { }

        [HttpPut("{id}/archive")]
        public async Task<ActionResult> ArchiveClient(int id)
        {
            await clientRepository.ArchiveAsync(id);
            return Ok();
        }
    }
}