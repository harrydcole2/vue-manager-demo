<template>
  <div id="app">
    <NavBar />
    <b-container fluid class="mt-4">
      <router-view></router-view>
    </b-container>
  </div>
</template>

<script>
import NavBar from "@/components/NavBar.vue";

export default {
  name: "App",
  components: {
    NavBar,
  },
};
</script>

<style>
@import "./assets/styles.css";
</style>
