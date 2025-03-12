<template>
  <b-container fluid class="p-4">
    <h1 class="mb-4 text-center">Clients</h1>
    <b-row>
      <b-col v-for="client in clients" :key="client.id" md="4" lg="3">
        <b-card :title="client.name" class="mb-3 shadow-sm">
          <b-card-text>{{ client.email }}</b-card-text>
          <b-button variant="primary" @click="viewClient(client.id)"
            >View</b-button
          >
        </b-card>
      </b-col>
    </b-row>
  </b-container>
</template>

<script>
import { mapState, mapActions } from "vuex";

export default {
  computed: {
    ...mapState("clients", ["clients"]),
  },
  created() {
    this.fetchClients();
  },
  methods: {
    ...mapActions("clients", ["fetchClients"]),
    viewClient(id) {
      this.$router.push(`/client/${id}`);
    },
  },
};
</script>

<style>
h1 {
  color: #8fc0a9;
}
</style>
