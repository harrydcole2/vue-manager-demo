<template>
  <div class="home">
    <div class="home-content">
      <h1 class="text-center mb-4">Client Management System</h1>
      <div class="text-center">
        <b-button
          v-if="!isAuthenticated"
          to="/login"
          variant="primary"
          size="lg"
        >
          Login to Get Started
        </b-button>
        <b-button v-else to="/clients" variant="primary" size="lg">
          Go to Clients
        </b-button>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  name: "Home",
  computed: {
    ...mapGetters({
      isAuthenticated: "auth/isAuthenticated",
    }),
  },
};
</script>
