<template>
  <div class="login">
    <b-card class="login-card" title="Login">
      <b-form @submit.prevent="handleLogin">
        <b-form-group id="email-group" label="Email:" label-for="email">
          <b-form-input
            id="email"
            v-model="form.email"
            type="email"
            placeholder="Enter email"
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
          :show="authError !== null"
          dismissible
          @dismissed="clearError"
        >
          {{ authError }}
        </b-alert>

        <b-button type="submit" variant="primary" :disabled="authLoading">
          <b-spinner v-if="authLoading" small></b-spinner>
          Login
        </b-button>
      </b-form>
    </b-card>
  </div>
</template>

<script>
import { mapActions, mapGetters } from "vuex";

export default {
  name: "Login",
  data() {
    return {
      form: {
        email: "",
        password: "",
      },
    };
  },
  computed: {
    ...mapGetters({
      authLoading: "auth/loading",
      authError: "auth/error",
    }),
  },
  methods: {
    ...mapActions({
      login: "auth/login",
    }),
    clearError() {
      this.$store.commit("auth/SET_ERROR", null);
    },
    async handleLogin() {
      try {
        const success = await this.login(this.form);

        if (success) {
          this.$router.push({ name: "ClientDashboard" });
        }
      } catch (error) {
        console.error("Login error:", error);
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
}

.login-card {
  max-width: 500px;
  width: 100%;
}
</style>
