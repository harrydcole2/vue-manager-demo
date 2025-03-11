<template>
  <div class="container">
    <h1>Welcome to Client Dashboard</h1>
    <p v-if="!isAuthenticated">Please log in to continue.</p>
    <button v-if="!isAuthenticated" @click="login">Login</button>
    <p v-else>You're already logged in!</p>
  </div>
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
.container {
  text-align: center;
  margin-top: 50px;
}
button {
  padding: 10px 15px;
  background-color: #9ec1a3;
  border: none;
  color: black;
  font-size: 16px;
  cursor: pointer;
}
</style>
