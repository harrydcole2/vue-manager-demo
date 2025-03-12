<template>
  <b-navbar toggleable="lg" type="dark">
    <b-navbar-brand to="/">
      <b-icon-book class="mx-2"></b-icon-book>
      Client Manager
    </b-navbar-brand>

    <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

    <b-collapse id="nav-collapse" is-nav>
      <b-navbar-nav>
        <b-nav-item to="/" exact>Home</b-nav-item>
        <b-nav-item to="/help">Help</b-nav-item>
        <b-nav-item v-if="isAuthenticated" to="/clients">Clients</b-nav-item>
      </b-navbar-nav>

      <b-navbar-nav class="ml-auto">
        <b-nav-item v-if="!isAuthenticated" to="/login">Login</b-nav-item>
        <b-nav-item-dropdown v-else right>
          <template #button-content>
            {{ email }}
          </template>
          <b-dropdown-item @click="logout">Logout</b-dropdown-item>
        </b-nav-item-dropdown>
      </b-navbar-nav>
    </b-collapse>
  </b-navbar>
</template>

<script>
import { mapGetters, mapActions } from "vuex";

export default {
  name: "NavBar",
  computed: {
    ...mapGetters({
      isAuthenticated: "auth/isAuthenticated",
      email: "auth/email",
    }),
  },
  methods: {
    ...mapActions({
      logout: "auth/logout",
    }),
  },
};
</script>
