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
  error: (state) => state.error,
};

const actions = {
  async fetchClients({ commit, rootState }, archived = false) {
    try {
      commit("SET_LOADING", true);
      commit("SET_ERROR", null);

      const userId = rootState.auth.userId;

      const response = await axios.get("/clients", {
        params: {
          userId,
          archived,
        },
      });

      commit("SET_CLIENTS", response.data);
    } catch (error) {
      commit("SET_ERROR", error.response?.data || error.message);
      console.error("Error fetching clients:", error);
    } finally {
      commit("SET_LOADING", false);
    }
  },

  async fetchClient({ commit, rootState }, id) {
    try {
      commit("SET_LOADING", true);
      commit("SET_ERROR", null);

      const userId = rootState.auth.userId;

      const response = await axios.get(`/clients/${id}`, {
        params: {
          userId,
        },
      });

      commit("SET_CURRENT_CLIENT", response.data);
    } catch (error) {
      commit("SET_ERROR", error.response?.data || error.message);
      console.error(`Error fetching client ${id}:`, error);
    } finally {
      commit("SET_LOADING", false);
    }
  },

  async createClient({ commit, dispatch, rootState }, clientData) {
    try {
      commit("SET_LOADING", true);
      commit("SET_ERROR", null);

      const userId = rootState.auth.userId;

      const client = {
        name: clientData.name,
        description: clientData.description,
        phoneNumbers: clientData.phones?.map((phone) => phone.number) || [],
      };

      await axios.post("/clients", client, {
        params: {
          userId,
        },
      });

      await dispatch("fetchClients");
      return true;
    } catch (error) {
      commit("SET_ERROR", error.response?.data || error.message);
      console.error("Error creating client:", error);
      return false;
    } finally {
      commit("SET_LOADING", false);
    }
  },

  async updateClient({ commit, dispatch, rootState }, clientData) {
    try {
      commit("SET_LOADING", true);
      commit("SET_ERROR", null);

      const userId = rootState.auth.userId;

      const client = {
        name: clientData.name,
        description: clientData.description,
        phoneNumbers: clientData.phones?.map((phone) => phone.number) || [],
      };

      await axios.put(`/clients/${clientData.id}`, client, {
        params: {
          userId,
        },
      });

      await dispatch("fetchClients");
      return true;
    } catch (error) {
      commit("SET_ERROR", error.response?.data || error.message);
      console.error(`Error updating client ${clientData.id}:`, error);
      return false;
    } finally {
      commit("SET_LOADING", false);
    }
  },

  async archiveClient({ commit, dispatch, rootState }, id) {
    try {
      commit("SET_LOADING", true);
      commit("SET_ERROR", null);

      const userId = rootState.auth.userId;

      await axios.put(
        `/clients/${id}/archive`,
        {},
        {
          params: {
            userId,
          },
        }
      );

      await dispatch("fetchClients");
      return true;
    } catch (error) {
      commit("SET_ERROR", error.response?.data || error.message);
      console.error(`Error archiving client ${id}:`, error);
      return false;
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
