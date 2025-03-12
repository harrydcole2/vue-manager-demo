<template>
  <div class="home">
    <b-card class="home-card">
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
    </b-card>
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

<style scoped>
.home {
  min-height: calc(100vh - 120px);
  background-image: url("../assets/repeatable_book.png");
  background-repeat: repeat;
  display: flex;
  align-items: center;
  justify-content: center;
}

.home-card {
  max-width: 700px;
  padding: 40px 20px;
  width: 100%;
}
</style>
