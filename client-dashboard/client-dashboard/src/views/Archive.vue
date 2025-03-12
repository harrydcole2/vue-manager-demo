<template>
  <div class="client-dashboard">
    <h1 class="mt-4">Archived Clients</h1>

    <b-container fluid>
      <b-row class="mb-4">
        <b-col>
          <router-link to="/clients">
            <b-button variant="primary">
              <b-icon-arrow-left></b-icon-arrow-left> Back to Active Clients
            </b-button>
          </router-link>
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
                <small class="text-muted">Archived</small>
              </template>
            </b-card>
          </b-col>

          <b-col v-if="allClients.length === 0 && !loading" cols="12">
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
  name: "Archive",
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
    }),
    clearError() {
      this.$store.commit("clients/SET_ERROR", null);
    },
    goToClientDetail(id) {
      this.$router.push({ name: "ClientManagement", params: { id } });
    },
  },
  created() {
    this.fetchClients(true);
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
