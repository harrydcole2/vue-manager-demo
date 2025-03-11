import axios from "axios";

export default {
  namespaced: true,
  state: {
    user: null,
  },
  mutations: {
    setUser(state, user) {
      state.user = user;
    },
    logout(state) {
      state.user = null;
    },
  },
  actions: {
    async login({ commit }, credentials) {
      try {
        const response = await axios.post("/api/login", credentials);
        commit("setUser", response.data.user);
      } catch (error) {
        console.error("Login failed", error);
      }
    },
    logout({ commit }) {
      commit("logout");
    },
  },
};
