<template>
  <b-container class="d-flex justify-content-center align-items-center vh-100">
    <b-card
      class="text-center shadow-lg"
      bg-variant="light"
      text-variant="dark"
      style="max-width: 400px"
    >
      <b-card-title>Welcome to Client Dashboard</b-card-title>
      <b-card-text v-if="!isAuthenticated"
        >Please log in to continue.</b-card-text
      >
      <b-button v-if="!isAuthenticated" variant="success" @click="login"
        >Login</b-button
      >
      <b-alert v-else variant="info" show>You are already logged in.</b-alert>
    </b-card>
  </b-container>
</template>

<script>
import { mapState, mapActions } from "vuex";

export default {
  computed: {
    ...mapState("auth", ["isAuthenticated"]),
  },
  methods: {
    ...mapActions("auth", ["login"]),
  },
};
</script>

<style>
.vh-100 {
  height: 100vh;
  background-color: #faf3dd;
}
</style>
