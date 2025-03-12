<template>
  <div class="client-management">
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
      <b-card v-if="currentClient">
        <template #header>
          <h2 class="mb-0">Edit Client: {{ currentClient.name }}</h2>
        </template>

        <b-form @submit.prevent="saveClient">
          <b-form-group
            id="client-name-group"
            label="Client Name:"
            label-for="client-name"
          >
            <b-form-input
              id="client-name"
              v-model="editedClient.name"
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
              v-model="editedClient.description"
              placeholder="Enter description"
              rows="3"
            ></b-form-textarea>
          </b-form-group>

          <h4 class="mt-4">Phone Numbers</h4>

          <b-card
            v-for="(phone, index) in editedClient.phones"
            :key="index"
            class="mb-3 phone-card"
          >
            <b-row>
              <b-col>
                <b-form-input
                  v-model="phone.number"
                  placeholder="Phone number"
                  required
                ></b-form-input>
              </b-col>
              <b-col cols="auto">
                <b-button
                  variant="danger"
                  @click="removePhone(index)"
                  size="sm"
                >
                  <b-icon-trash></b-icon-trash>
                </b-button>
              </b-col>
            </b-row>
          </b-card>

          <b-button variant="outline-primary" @click="addPhone" class="mb-4">
            <b-icon-plus></b-icon-plus> Add Phone Number
          </b-button>

          <div class="d-flex mt-4">
            <b-button variant="secondary" @click="goBack" class="mr-2">
              Cancel
            </b-button>
            <b-button variant="success" type="submit" class="mr-2">
              Save Changes
            </b-button>
            <b-button variant="danger" @click="showArchiveConfirm = true">
              Archive Client
            </b-button>
          </div>
        </b-form>
      </b-card>

      <b-alert v-else-if="!loading" show variant="danger">
        Client not found. <b-link to="/clients">Return to dashboard</b-link>
      </b-alert>
    </b-overlay>

    <b-modal
      v-model="showArchiveConfirm"
      title="Confirm Archive"
      header-bg-variant="danger"
      header-text-variant="light"
      ok-variant="danger"
      ok-title="Archive"
      @ok="archiveCurrentClient"
    >
      <p>
        Are you sure you want to archive this client? This action cannot be
        undone.
      </p>
    </b-modal>
  </div>
</template>

<script>
import { mapActions, mapGetters } from "vuex";

export default {
  name: "ClientManagement",
  data() {
    return {
      editedClient: {
        id: null,
        name: "",
        description: "",
        phones: [],
      },
      showArchiveConfirm: false,
    };
  },
  computed: {
    ...mapGetters({
      currentClient: "clients/currentClient",
      loading: "clients/loading",
      error: "clients/error",
    }),
  },
  methods: {
    ...mapActions({
      fetchClient: "clients/fetchClient",
      updateClient: "clients/updateClient",
      archiveClient: "clients/archiveClient",
    }),
    clearError() {
      this.$store.commit("clients/SET_ERROR", null);
    },
    initForm() {
      if (this.currentClient) {
        const phones = this.currentClient.phoneNumbers
          ? this.currentClient.phoneNumbers.map((number) => ({ number }))
          : [];

        this.editedClient = {
          id: this.currentClient.id,
          name: this.currentClient.name,
          description: this.currentClient.description || "",
          phones: phones,
        };
      }
    },
    addPhone() {
      this.editedClient.phones.push({ number: "" });
    },
    removePhone(index) {
      this.editedClient.phones.splice(index, 1);
    },
    async saveClient() {
      this.editedClient.phones = this.editedClient.phones.filter(
        (phone) => phone.number.trim() !== ""
      );

      const success = await this.updateClient(this.editedClient);
      if (success) {
        this.goBack();
      }
    },
    async archiveCurrentClient() {
      const success = await this.archiveClient(this.currentClient.id);
      if (success) {
        this.goBack();
      }
    },
    goBack() {
      this.$router.push({ name: "ClientDashboard" });
    },
  },
  created() {
    const clientId = this.$route.params.id;
    this.fetchClient(clientId);
  },
  watch: {
    currentClient: {
      handler: "initForm",
      immediate: true,
    },
  },
};
</script>

<style scoped>
h2,
h4 {
  color: var(--primary-color);
}

.phone-card {
  background-color: rgba(104, 176, 171, 0.05);
}
</style>
