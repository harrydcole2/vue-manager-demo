<template>
  <div class="help">
    <b-card title="Help & Information">
      <h2>Client Management System</h2>
      <p>
        This application allows you to manage your clients and their contact
        information.
      </p>

      <h3>Features:</h3>
      <ul>
        <li>View all active clients</li>
        <li>Create new client records</li>
        <li>Edit existing client information</li>
        <li>Manage client phone numbers</li>
        <li>Archive clients you no longer work with</li>
      </ul>

      <h3>Need assistance?</h3>
      <p>
        Contact your system administrator or email support@example.com for help.
      </p>
    </b-card>
  </div>
</template>

<script>
export default {
  name: "Help",
};
</script>

<style scoped>
.help {
  max-width: 800px;
  margin: 0 auto;
}

h2,
h3 {
  color: var(--primary-color);
}
</style>
