<template>
  <div id="app">
    <AppBar />
    <b-container fluid>
      <router-view></router-view>
    </b-container>
  </div>
</template>

<script>
import AppBar from "./components/AppBar.vue";

export default {
  components: {
    AppBar,
  },
};
</script>

<style>
body {
  background-color: #faf3dd;
  color: black;
  font-family: Arial, sans-serif;
}
</style>
