import Vue from "vue";
import axios from "axios";

export default {
  namespaced: true,
  state: {
    clients: [],
  },
  mutations: {
    setClients(state, clients) {
      state.clients = clients;
    },
    updateClient(state, updatedClient) {
      const index = state.clients.findIndex((c) => c.id === updatedClient.id);
      if (index !== -1) {
        Vue.set(state.clients, index, updatedClient);
      }
    },
  },
  actions: {
    async fetchClients({ commit }) {
      try {
        const response = await axios.get("/api/clients");
        commit("setClients", response.data);
      } catch (error) {
        console.error("Error fetching clients", error);
      }
    },
    async updateClient({ commit }, client) {
      try {
        const response = await axios.put(`/api/clients/${client.id}`, client);
        commit("updateClient", response.data);
      } catch (error) {
        console.error("Error updating client", error);
      }
    },
  },
};
