<!-- src/views/Login.vue -->
<template>
  <div class="login">
    <b-card class="login-card" title="Login">
      <b-form @submit.prevent="handleLogin">
        <b-form-group
          id="username-group"
          label="Username:"
          label-for="username"
        >
          <b-form-input
            id="username"
            v-model="form.username"
            type="text"
            placeholder="Enter username"
            required
          ></b-form-input>
        </b-form-group>

        <b-form-group
          id="password-group"
          label="Password:"
          label-for="password"
        >
          <b-form-input
            id="password"
            v-model="form.password"
            type="password"
            placeholder="Enter password"
            required
          ></b-form-input>
        </b-form-group>

        <b-alert
          variant="danger"
          :show="error !== null"
          dismissible
          @dismissed="error = null"
        >
          {{ error }}
        </b-alert>

        <b-button type="submit" variant="primary" :disabled="loading">
          <b-spinner v-if="loading" small></b-spinner>
          Login
        </b-button>
      </b-form>
    </b-card>
  </div>
</template>

<script>
import { mapActions } from "vuex";

export default {
  name: "Login",
  data() {
    return {
      form: {
        username: "",
        password: "",
      },
      loading: false,
      error: null,
    };
  },
  methods: {
    ...mapActions({
      login: "auth/login",
    }),
    async handleLogin() {
      this.loading = true;
      this.error = null;

      try {
        const success = await this.login(this.form);

        if (success) {
          this.$router.push({ name: "ClientDashboard" });
        } else {
          this.error = "Login failed. Please check your credentials.";
        }
      } catch (error) {
        this.error = "An error occurred during login. Please try again.";
        console.error("Login error:", error);
      } finally {
        this.loading = false;
      }
    },
  },
};
</script>

<style scoped>
.login {
  min-height: calc(100vh - 120px);
  background-image: url("../assets/repeatable_book.png");
  background-repeat: repeat;
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 20px;
}

.login-card {
  max-width: 500px;
  width: 100%;
  background-color: var(--background-color);
}
</style>
