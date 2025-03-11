<template>
  <nav class="appbar">
    <router-link to="/" class="logo">📖</router-link>
    <router-link to="/help">Help</router-link>
    <router-link v-if="!user" to="/login">Login</router-link>
    <template v-if="user">
      <router-link to="/clients">Clients</router-link>
      <button @click="logout">Logout</button>
    </template>
  </nav>
</template>

<script>
import { mapState, mapActions } from "vuex";

export default {
  computed: {
    ...mapState("auth", ["user"]),
  },
  methods: {
    ...mapActions("auth", ["logout"]),
  },
};
</script>

<style>
.appbar {
  background-color: #9ec1a3;
  padding: 10px;
  display: flex;
  justify-content: space-between;
  align-items: center;
}
.appbar a {
  color: black;
  text-decoration: none;
  margin: 0 10px;
}
</style>
