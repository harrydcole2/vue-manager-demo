<template>
  <div id="app">
    <AppBar />
    <router-view />
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
  background-color: #cfe0c3;
  color: black;
  font-family: Arial, sans-serif;
}
</style>
