<template>
  <div class="container">
    <h1>Login</h1>
    <input v-model="username" placeholder="Username" />
    <input v-model="password" type="password" placeholder="Password" />
    <button @click="handleLogin">Login</button>
  </div>
</template>

<script>
import { mapActions } from "vuex";

export default {
  data() {
    return {
      username: "",
      password: "",
    };
  },
  methods: {
    ...mapActions("auth", ["login"]),
    async handleLogin() {
      await this.login({ username: this.username, password: this.password });
      this.$router.push("/clients");
    },
  },
};
</script>

<style>
.container {
  text-align: center;
  margin-top: 50px;
}
</style>
