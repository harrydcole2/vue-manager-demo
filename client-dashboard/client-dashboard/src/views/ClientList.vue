<template>
  <div>
    <h1>Your Clients</h1>
    <div v-for="client in clients" :key="client.id">
      <router-link :to="'/clients/' + client.id">{{ client.name }}</router-link>
    </div>
  </div>
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
  },
};
</script>
