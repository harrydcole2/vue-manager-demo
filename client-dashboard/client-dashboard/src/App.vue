<template>
  <div id="app">
    <NavBar />
    <b-container fluid>
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
