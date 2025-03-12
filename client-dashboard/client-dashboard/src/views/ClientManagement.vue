<template>
  <div class="client-management mt-4">
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
          <h2 class="mb-0">
            {{ isArchived ? "View Archived Client:" : "Edit Client:" }}
            {{ currentClient.client.name }}
          </h2>
        </template>

        <b-form @submit.prevent="saveClient">
          <b-form-group
            id="client-name-group"
            label="Client Name:"
            label-for="client-name"
            class="mb-3"
          >
            <template v-if="isArchived">
              <p>{{ editedClient.name }}</p>
            </template>
            <template v-else>
              <b-form-input
                id="client-name"
                v-model="editedClient.name"
                type="text"
                placeholder="Enter client name"
                required
                :disabled="isArchived"
              ></b-form-input>
            </template>
          </b-form-group>

          <b-form-group
            id="client-description-group"
            label="Description (optional):"
            label-for="client-description"
            class="mb-4"
          >
            <template v-if="isArchived">
              <p>{{ editedClient.description }}</p>
            </template>
            <template v-else>
              <b-form-textarea
                id="client-description"
                v-model="editedClient.description"
                placeholder="Enter description"
                rows="3"
                :disabled="isArchived"
              ></b-form-textarea>
            </template>
          </b-form-group>

          <h4 class="mt-4 mb-3">Phone Numbers</h4>

          <b-card
            v-for="(phone, index) in editedClient.phones"
            :key="index"
            class="mb-3 phone-card"
          >
            <b-row align-v="center">
              <b-col>
                <div class="d-flex align-items-center">
                  <template v-if="isArchived">
                    <p>{{ phone.number }}</p>
                  </template>
                  <template v-else>
                    <b-icon-telephone
                      class="mr-2 text-primary"
                    ></b-icon-telephone>
                    <b-form-input
                      v-model="phone.number"
                      placeholder="Phone number"
                      required
                      :disabled="isArchived"
                    ></b-form-input>
                  </template>
                </div>
              </b-col>
              <b-col cols="auto">
                <template v-if="!isArchived">
                  <b-button
                    variant="danger"
                    @click="removePhone(index)"
                    size="sm"
                  >
                    <b-icon-trash></b-icon-trash>
                  </b-button>
                </template>
              </b-col>
            </b-row>
          </b-card>

          <template v-if="!isArchived">
            <b-button variant="outline-primary" @click="addPhone" class="mb-4">
              <b-icon-plus></b-icon-plus> Add Phone Number
            </b-button>
          </template>

          <div class="d-flex mt-4 action-buttons gap-3">
            <b-button variant="secondary" @click="goBack" class="mr-3">
              Cancel
            </b-button>
            <template v-if="!isArchived">
              <b-button variant="success" type="submit" class="mr-3">
                Save Changes
              </b-button>
              <b-button variant="danger" @click="showArchiveConfirm = true">
                Archive Client
              </b-button>
            </template>
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
      hide-header-close
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
import { BIconTelephone, BIconPlus, BIconTrash } from "bootstrap-vue";

export default {
  name: "ClientManagement",
  components: {
    BIconTelephone,
    BIconPlus,
    BIconTrash,
  },
  props: {
    id: {
      type: Number,
      required: true,
    },
  },
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
    isArchived() {
      return this.currentClient?.client?.isArchived || false;
    },
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
      if (this.currentClient && this.currentClient.client) {
        const client = this.currentClient.client;
        const phoneNumbers = this.currentClient.phoneNumbers || [];

        const phones = phoneNumbers.map((phoneObj) => ({
          number: phoneObj.phone,
        }));

        this.editedClient = {
          id: client.id,
          name: client.name,
          description: client.description || "",
          phones: phones.length > 0 ? phones : [],
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
      if (this.currentClient && this.currentClient.client) {
        this.editedClient.id = this.currentClient.client.id;
      }

      this.editedClient.phones = this.editedClient.phones.filter(
        (phone) => phone.number.trim() !== ""
      );

      const success = await this.updateClient(this.editedClient);
      if (success) {
        this.goBack();
      }
    },
    async archiveCurrentClient() {
      if (this.currentClient && this.currentClient.client) {
        const success = await this.archiveClient(this.currentClient.client.id);
        if (success) {
          this.goBack();
        }
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
  padding: 12px;
}

.action-buttons button {
  min-width: 120px;
}
</style>
