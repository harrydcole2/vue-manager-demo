<!-- src/views/Home.vue -->
<template>
  <div class="home">
    <div class="home-content">
      <h1 class="text-center mb-4">Client Management System</h1>
      <div class="text-center">
        <b-button v-if="!isLoggedIn" to="/login" variant="primary" size="lg">
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
      isLoggedIn: "auth/isLoggedIn",
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

.home-content {
  background-color: rgba(250, 243, 221, 0.9);
  padding: 2rem;
  border-radius: 10px;
  box-shadow: 0 0 20px rgba(0, 0, 0, 0.2);
  max-width: 600px;
  width: 100%;
}

h1 {
  color: var(--primary-color);
}
</style>
