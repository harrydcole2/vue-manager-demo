<template>
  <b-navbar toggleable="lg" type="dark">
    <div class="container-fluid d-flex justify-content-between">
      <!-- Brand on the left -->
      <b-navbar-brand to="/">
        <b-icon-book class="mx-2"></b-icon-book>
        Client Manager
      </b-navbar-brand>

      <!-- Navigation items on the right with proper spacing -->
      <div>
        <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

        <b-collapse id="nav-collapse" is-nav>
          <b-navbar-nav class="align-items-center">
            <b-nav-item
              to="/"
              exact
              :class="{ 'font-weight-bold': isActive('/') }"
              >Home</b-nav-item
            >
            <b-nav-item
              to="/help"
              :class="{ 'font-weight-bold': isActive('/help') }"
              >Help</b-nav-item
            >

            <!-- Client tabs instead of dropdown -->
            <b-nav-item
              v-if="isAuthenticated"
              to="/clients/active"
              :class="{ 'font-weight-bold': isActive('/clients/active') }"
            >
              Active Clients
            </b-nav-item>
            <b-nav-item
              v-if="isAuthenticated"
              to="/clients/archived"
              :class="{ 'font-weight-bold': isActive('/clients/archived') }"
            >
              Archived Clients
            </b-nav-item>

            <!-- Login item for unauthenticated users -->
            <b-nav-item
              v-if="!isAuthenticated"
              to="/login"
              :class="{ 'font-weight-bold': isActive('/login') }"
            >
              Login
            </b-nav-item>

            <!-- Logout button for authenticated users -->
            <b-nav-item v-if="isAuthenticated" class="nav-button-item">
              <b-button size="sm" variant="outline-light" @click="logout">
                Logout
              </b-button>
            </b-nav-item>
          </b-navbar-nav>
        </b-collapse>
      </div>
    </div>
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
    isActive(route) {
      return this.$route.path === route;
    },
  },
};
</script>

<style scoped>
.navbar-nav {
  height: 47px;
}

.nav-button-item {
  display: flex;
  align-items: center;
  height: 100%;
  padding-top: 0;
  padding-bottom: 0;
}
</style>
