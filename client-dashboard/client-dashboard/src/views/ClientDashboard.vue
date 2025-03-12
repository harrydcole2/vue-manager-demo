<!-- src/views/ClientDashboard.vue -->
<template>
  <div class="client-dashboard">
    <h1>Client Dashboard</h1>

    <b-row class="mb-4">
      <b-col>
        <b-button variant="primary" @click="showCreateModal = true">
          <b-icon-plus></b-icon-plus> Create New Client
        </b-button>
      </b-col>
    </b-row>

    <b-overlay :show="loading" rounded="sm">
      <b-row>
        <b-col
          v-for="client in allClients"
          :key="client.id"
          lg="4"
          md="6"
          class="mb-4"
        >
          <b-card
            :title="client.name"
            @click="goToClientDetail(client.id)"
            class="client-card"
          >
            <template #footer>
              <small class="text-muted">
                <b-icon-telephone class="mr-1"></b-icon-telephone>
                {{
                  client.phones && client.phones.length
                    ? client.phones.length
                    : "No"
                }}
                phone number{{
                  client.phones && client.phones.length !== 1 ? "s" : ""
                }}
              </small>
            </template>
          </b-card>
        </b-col>

        <b-col v-if="allClients.length === 0 && !loading" cols="12">
          <b-alert show variant="info"
            >No clients found. Create a new client to get started.</b-alert
          >
        </b-col>
      </b-row>
    </b-overlay>

    <!-- Create New Client Modal -->
    <b-modal
      v-model="showCreateModal"
      title="Create New Client"
      header-bg-variant="primary"
      header-text-variant="light"
      @hidden="resetForm"
      @ok.prevent="createNewClient"
    >
      <b-form>
        <b-form-group
          id="client-name-group"
          label="Client Name:"
          label-for="client-name"
        >
          <b-form-input
            id="client-name"
            v-model="newClient.name"
            type="text"
            placeholder="Enter client name"
            required
          ></b-form-input>
        </b-form-group>

        <b-form-group
          id="client-phone-group"
          label="Phone Number:"
          label-for="client-phone"
        >
          <b-form-input
            id="client-phone"
            v-model="newClient.phone"
            type="text"
            placeholder="Enter phone number"
          ></b-form-input>
        </b-form-group>
      </b-form>

      <template #modal-footer="{ ok, cancel }">
        <b-button variant="secondary" @click="cancel()"> Cancel </b-button>
        <b-button variant="primary" @click="ok()" :disabled="!newClient.name">
          Create
        </b-button>
      </template>
    </b-modal>
  </div>
</template>

<script>
import { mapActions, mapGetters } from "vuex";

export default {
  name: "ClientDashboard",
  data() {
    return {
      showCreateModal: false,
      newClient: {
        name: "",
        phone: "",
      },
    };
  },
  computed: {
    ...mapGetters({
      allClients: "clients/allClients",
      loading: "clients/loading",
    }),
  },
  methods: {
    ...mapActions({
      fetchClients: "clients/fetchClients",
      createClient: "clients/createClient",
    }),
    goToClientDetail(id) {
      this.$router.push({ name: "ClientManagement", params: { id } });
    },
    resetForm() {
      this.newClient = {
        name: "",
        phone: "",
      };
    },
    async createNewClient() {
      if (!this.newClient.name) return;

      const clientData = {
        name: this.newClient.name,
        phones: this.newClient.phone ? [{ number: this.newClient.phone }] : [],
      };

      await this.createClient(clientData);
      this.showCreateModal = false;
    },
  },
  created() {
    this.fetchClients();
  },
};
</script>

<style scoped>
.client-card {
  cursor: pointer;
  transition: transform 0.2s, box-shadow 0.2s;
}

.client-card:hover {
  transform: translateY(-5px);
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
}

h1 {
  color: var(--primary-color);
  margin-bottom: 1.5rem;
}
</style>
