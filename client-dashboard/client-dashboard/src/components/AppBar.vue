<template>
  <b-navbar
    toggleable="lg"
    type="dark"
    :variant="isAuthenticated ? 'custom-dark' : 'custom-light'"
  >
    <b-navbar-brand>
      <b-icon-book class="mr-2"></b-icon-book> Client Dashboard
    </b-navbar-brand>

    <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

    <b-collapse id="nav-collapse" is-nav>
      <b-navbar-nav>
        <b-nav-item to="/">Home</b-nav-item>
        <b-nav-item to="/help">Help</b-nav-item>
        <b-nav-item v-if="isAuthenticated" to="/clients">Clients</b-nav-item>
      </b-navbar-nav>

      <b-navbar-nav class="ml-auto">
        <b-button v-if="!isAuthenticated" variant="outline-light" @click="login"
          >Login</b-button
        >
        <b-button v-else variant="danger" @click="logout">Logout</b-button>
      </b-navbar-nav>
    </b-collapse>
  </b-navbar>
</template>

<script>
import { mapState, mapActions } from "vuex";
import { BIconBook } from "bootstrap-vue";

export default {
  components: {
    BIconBook,
  },
  computed: {
    ...mapState("auth", ["isAuthenticated"]),
  },
  methods: {
    ...mapActions("auth", ["login", "logout"]),
  },
};
</script>

<style>
.custom-light {
  background-color: #c8d5b9 !important;
}

.custom-dark {
  background-color: #8fc0a9 !important;
}
</style>
