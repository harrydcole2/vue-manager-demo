<template>
  <div class="client-dashboard">
    <h1 class="mt-4">Client Dashboard</h1>

    <b-container fluid>
      <b-row class="mb-4">
        <b-col>
          <b-button variant="primary" @click="showCreateModal = true">
            <b-icon-plus></b-icon-plus> Create New Client
          </b-button>
        </b-col>
      </b-row>

      <b-alert
        v-if="error"
        show
        variant="danger"
        dismissible
        @dismissed="clearError"
      >
        {{ error }}
      </b-alert>

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
              <div v-if="client.description" class="mb-2">
                {{ client.description }}
              </div>
              <template #footer>
                <small class="text-muted"> </small>
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
    </b-container>

    <b-modal
      v-model="showCreateModal"
      title="Create New Client"
      header-bg-variant="primary"
      header-text-variant="light"
      @hidden="resetForm"
      @ok.prevent="createNewClient"
      hide-header-close
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
          id="client-description-group"
          label="Description (optional):"
          label-for="client-description"
        >
          <b-form-textarea
            id="client-description"
            v-model="newClient.description"
            placeholder="Enter description"
            rows="3"
          ></b-form-textarea>
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
        description: "",
        phone: "",
      },
    };
  },
  computed: {
    ...mapGetters({
      allClients: "clients/allClients",
      loading: "clients/loading",
      error: "clients/error",
    }),
  },
  methods: {
    ...mapActions({
      fetchClients: "clients/fetchClients",
      createClient: "clients/createClient",
    }),
    clearError() {
      this.$store.commit("clients/SET_ERROR", null);
    },
    goToClientDetail(id) {
      this.$router.push({ name: "ClientManagement", params: { id } });
    },
    resetForm() {
      this.newClient = {
        name: "",
        description: "",
        phone: "",
      };
    },
    async createNewClient() {
      if (!this.newClient.name) return;

      const clientData = {
        name: this.newClient.name,
        description: this.newClient.description,
        phones: this.newClient.phone ? [{ number: this.newClient.phone }] : [],
      };

      const success = await this.createClient(clientData);
      if (success) {
        this.showCreateModal = false;
      }
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
