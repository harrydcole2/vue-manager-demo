<template>
  <b-container class="py-5">
    <b-card class="shadow-sm" header="Edit Client">
      <b-form v-if="client">
        <b-form-group label="Name">
          <b-form-input v-model="client.name"></b-form-input>
        </b-form-group>

        <b-form-group label="Email">
          <b-form-input v-model="client.email"></b-form-input>
        </b-form-group>

        <b-form-group label="Phone">
          <b-form-input v-model="client.phone"></b-form-input>
        </b-form-group>

        <b-button variant="success" @click="saveClient">Save</b-button>
      </b-form>
      <b-spinner v-else label="Loading..."></b-spinner>
    </b-card>
  </b-container>
</template>

<script>
import { mapState, mapActions } from "vuex";

export default {
  data() {
    return {
      client: null,
    };
  },
  computed: {
    ...mapState("clients", ["clients"]),
  },
  async created() {
    const clientId = this.$route.params.id;
    this.client = this.clients.find((c) => c.id === clientId);
    if (!this.client) {
      await this.fetchClients();
      this.client = this.clients.find((c) => c.id === clientId);
    }
  },
  methods: {
    ...mapActions("clients", ["fetchClients", "updateClient"]),
    async saveClient() {
      await this.updateClient(this.client);
      this.$router.push("/clients");
    },
  },
};
</script>
