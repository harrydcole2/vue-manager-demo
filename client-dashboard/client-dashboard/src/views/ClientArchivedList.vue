<template>
  <div class="client-archived-list">
    <h1>Archived Clients</h1>

    <b-container fluid>
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
            v-for="client in archivedClients"
            :key="client.id"
            lg="4"
            md="6"
            class="mb-4"
          >
            <b-card
              :title="client.name"
              @click="goToClientDetail(client.id)"
              class="client-card"
              border-variant="secondary"
            >
              <div v-if="client.description" class="mb-2">
                {{ client.description }}
              </div>
              <template #footer>
                <div class="d-flex justify-content-between">
                  <small class="text-muted">
                    <b-icon-telephone class="mr-1"></b-icon-telephone>
                    {{
                      client.phoneNumbers && client.phoneNumbers.length
                        ? client.phoneNumbers.length
                        : "No"
                    }}
                    phone number{{
                      client.phoneNumbers && client.phoneNumbers.length !== 1
                        ? "s"
                        : ""
                    }}
                  </small>
                  <b-button
                    size="sm"
                    variant="outline-primary"
                    @click.stop="restoreClient(client.id)"
                  >
                    <b-icon-arrow-counterclockwise></b-icon-arrow-counterclockwise>
                    Restore
                  </b-button>
                </div>
              </template>
            </b-card>
          </b-col>

          <b-col v-if="archivedClients.length === 0 && !loading" cols="12">
            <b-alert show variant="info"> No archived clients found. </b-alert>
          </b-col>
        </b-row>
      </b-overlay>
    </b-container>
  </div>
</template>

<script>
import { mapActions, mapGetters } from "vuex";

export default {
  name: "ClientArchivedList",
  data() {
    return {
      // Add local state if needed
    };
  },
  computed: {
    ...mapGetters({
      archivedClients: "clients/archivedClients",
      loading: "clients/loading",
      error: "clients/error",
    }),
  },
  methods: {
    ...mapActions({
      fetchArchivedClients: "clients/fetchArchivedClients",
      unarchiveClient: "clients/unarchiveClient",
    }),
    clearError() {
      this.$store.commit("clients/SET_ERROR", null);
    },
    goToClientDetail(id) {
      this.$router.push({ name: "ClientManagement", params: { id } });
    },
    async restoreClient(id) {
      try {
        await this.unarchiveClient(id);
        // Refresh the list after unarchiving
        this.fetchArchivedClients();
      } catch (error) {
        console.error("Error restoring client:", error);
      }
    },
  },
  created() {
    this.fetchArchivedClients();
  },
};
</script>

<style scoped>
.client-card {
  cursor: pointer;
  transition: transform 0.2s, box-shadow 0.2s;
  background-color: #f8f9fa;
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
