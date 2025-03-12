<template>
  <b-container class="d-flex justify-content-center align-items-center vh-100">
    <b-card
      class="shadow-lg text-center p-4"
      bg-variant="light"
      text-variant="dark"
      style="max-width: 400px"
    >
      <b-card-title>Login</b-card-title>
      <b-alert v-if="error" variant="danger" show>{{ error }}</b-alert>
      <b-form @submit.prevent="handleLogin">
        <b-form-group label="Username">
          <b-form-input v-model="username" required></b-form-input>
        </b-form-group>

        <b-form-group label="Password">
          <b-form-input
            type="password"
            v-model="password"
            required
          ></b-form-input>
        </b-form-group>

        <b-button type="submit" variant="success" class="w-100">Login</b-button>
      </b-form>
    </b-card>
  </b-container>
</template>

<script>
import { mapActions } from "vuex";

export default {
  data() {
    return {
      username: "",
      password: "",
      error: null,
    };
  },
  methods: {
    ...mapActions("auth", ["login"]),
    async handleLogin() {
      try {
        await this.login({ username: this.username, password: this.password });
        this.$router.push("/clients");
      } catch (err) {
        this.error = "Invalid credentials. Please try again.";
      }
    },
  },
};
</script>

<style>
.vh-100 {
  height: 100vh;
  background-color: #faf3dd;
}
</style>
