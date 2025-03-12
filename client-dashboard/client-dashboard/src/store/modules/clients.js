// import axios from "axios";

import axios from "axios";

const state = {
  clients: [],
  currentClient: null,
  loading: false,
  error: null,
};

const getters = {
  allClients: (state) => state.clients,
  currentClient: (state) => state.currentClient,
  loading: (state) => state.loading,
};

const actions = {
  async fetchClients({ commit, rootState }) {
    try {
      commit("SET_LOADING", true);

      // API call would be:
      // const response = await axios.get('/api/clients', {
      //   headers: { Authorization: `Bearer ${rootState.auth.token}` }
      // })
      console.log(rootState, axios.defaults.baseURL);

      const mockClients = [
        {
          id: 1,
          name: "Client A",
          archived: false,
          phones: [{ id: 1, number: "555-123-4567" }],
        },
        {
          id: 2,
          name: "Client B",
          archived: false,
          phones: [{ id: 2, number: "555-987-6543" }],
        },
      ];

      commit("SET_CLIENTS", mockClients);
    } catch (error) {
      commit("SET_ERROR", error.message);
      console.error("Error fetching clients:", error);
    } finally {
      commit("SET_LOADING", false);
    }
  },

  async fetchClient({ commit, rootState }, id) {
    try {
      commit("SET_LOADING", true);

      // API call would be:
      // const response = await axios.get(`/api/clients/${id}`, {
      //   headers: { Authorization: `Bearer ${rootState.auth.token}` }
      // })
      console.log(rootState);

      // Mock data for now
      const mockClient = {
        id: id,
        name: "Client " + id,
        archived: false,
        phones: [
          { id: 1, number: "555-123-4567" },
          { id: 2, number: "555-987-6543" },
        ],
      };

      commit("SET_CURRENT_CLIENT", mockClient);
    } catch (error) {
      commit("SET_ERROR", error.message);
      console.error(`Error fetching client ${id}:`, error);
    } finally {
      commit("SET_LOADING", false);
    }
  },

  async createClient({ commit, dispatch, rootState }, client) {
    try {
      commit("SET_LOADING", true);

      // API call would be:
      // const response = await axios.post('/api/clients', client, {
      //   headers: { Authorization: `Bearer ${rootState.auth.token}` }
      // })
      console.log(rootState, client);

      // After creating, refresh the list
      await dispatch("fetchClients");
    } catch (error) {
      commit("SET_ERROR", error.message);
      console.error("Error creating client:", error);
    } finally {
      commit("SET_LOADING", false);
    }
  },

  async updateClient({ commit, dispatch, rootState }, client) {
    try {
      commit("SET_LOADING", true);

      // API call would be:
      // const response = await axios.put(`/api/clients/${client.id}`, client, {
      //   headers: { Authorization: `Bearer ${rootState.auth.token}` }
      // })
      console.log(rootState);

      // After updating, refresh the list
      await dispatch("fetchClients");
    } catch (error) {
      commit("SET_ERROR", error.message);
      console.error(`Error updating client ${client.id}:`, error);
    } finally {
      commit("SET_LOADING", false);
    }
  },

  async archiveClient({ commit, dispatch, rootState }, id) {
    try {
      commit("SET_LOADING", true);

      // API call would be:
      // const response = await axios.put(`/api/clients/${id}/archive`, {}, {
      //   headers: { Authorization: `Bearer ${rootState.auth.token}` }
      // })
      console.log(rootState);

      // After archiving, refresh the list
      await dispatch("fetchClients");
    } catch (error) {
      commit("SET_ERROR", error.message);
      console.error(`Error archiving client ${id}:`, error);
    } finally {
      commit("SET_LOADING", false);
    }
  },

  async addPhone({ commit, dispatch, rootState }, { clientId, phone }) {
    try {
      commit("SET_LOADING", true);

      // API call would be:
      // const response = await axios.post(`/api/clients/${clientId}/phones`, phone, {
      //   headers: { Authorization: `Bearer ${rootState.auth.token}` }
      // })
      console.log(rootState, phone);

      // After adding phone, refresh the client
      await dispatch("fetchClient", clientId);
    } catch (error) {
      commit("SET_ERROR", error.message);
      console.error(`Error adding phone to client ${clientId}:`, error);
    } finally {
      commit("SET_LOADING", false);
    }
  },

  async updatePhone(
    { commit, dispatch, rootState },
    { clientId, phoneId, phone }
  ) {
    try {
      commit("SET_LOADING", true);

      // API call would be:
      // const response = await axios.put(`/api/clients/${clientId}/phones/${phoneId}`, phone, {
      //   headers: { Authorization: `Bearer ${rootState.auth.token}` }
      // })
      console.log(rootState, phone);

      // After updating phone, refresh the client
      await dispatch("fetchClient", clientId);
    } catch (error) {
      commit("SET_ERROR", error.message);
      console.error(
        `Error updating phone ${phoneId} for client ${clientId}:`,
        error
      );
    } finally {
      commit("SET_LOADING", false);
    }
  },

  async deletePhone({ commit, dispatch, rootState }, { clientId, phoneId }) {
    try {
      commit("SET_LOADING", true);

      // API call would be:
      // const response = await axios.delete(`/api/clients/${clientId}/phones/${phoneId}`, {
      //   headers: { Authorization: `Bearer ${rootState.auth.token}` }
      // })
      console.log(rootState);

      await dispatch("fetchClient", clientId);
    } catch (error) {
      commit("SET_ERROR", error.message);
      console.error(
        `Error deleting phone ${phoneId} from client ${clientId}:`,
        error
      );
    } finally {
      commit("SET_LOADING", false);
    }
  },
};

const mutations = {
  SET_LOADING(state, loading) {
    state.loading = loading;
  },
  SET_ERROR(state, error) {
    state.error = error;
  },
  SET_CLIENTS(state, clients) {
    state.clients = clients;
  },
  SET_CURRENT_CLIENT(state, client) {
    state.currentClient = client;
  },
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations,
};
