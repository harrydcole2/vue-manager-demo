<template>
  <div class="container">
    <h1>Edit Client</h1>
    <div v-if="client">
      <label>Name:</label>
      <input v-model="client.name" />

      <label>Email:</label>
      <input v-model="client.email" />

      <label>Phone:</label>
      <input v-model="client.phone" />

      <button @click="saveClient">Save</button>
    </div>
    <p v-else>Loading client details...</p>
  </div>
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
      await this.fetchClients(); // Ensure data is loaded
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

<style>
.container {
  text-align: center;
  margin-top: 20px;
}
input {
  display: block;
  margin: 10px auto;
  padding: 8px;
  border: 1px solid black;
}
button {
  padding: 8px 12px;
  background-color: #9ec1a3;
  border: none;
  color: black;
  cursor: pointer;
}
</style>
