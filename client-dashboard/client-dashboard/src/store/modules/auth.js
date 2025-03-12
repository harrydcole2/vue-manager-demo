import axios from "axios";

const state = {
  userId: localStorage.getItem("userId") || null,
  email: localStorage.getItem("email") || null,
  loading: false,
  error: null,
};

const getters = {
  isAuthenticated: (state) => !!state.userId,
  userId: (state) => state.userId,
  email: (state) => state.email,
  loading: (state) => state.loading,
  error: (state) => state.error,
};

const actions = {
  async login({ commit }, credentials) {
    try {
      commit("SET_LOADING", true);
      commit("SET_ERROR", null);

      const response = await axios.post("/users/login", {
        email: credentials.email,
        password: credentials.password,
      });

      const userData = response.data;

      commit("SET_USER", {
        userId: userData.userId,
        email: userData.email,
      });

      localStorage.setItem("userId", userData.userId);
      localStorage.setItem("email", userData.email);

      return true;
    } catch (error) {
      commit("SET_ERROR", error.response?.data || error.message);
      console.error("Login error:", error);
      return false;
    } finally {
      commit("SET_LOADING", false);
    }
  },

  logout({ commit }) {
    localStorage.removeItem("userId");
    localStorage.removeItem("email");
    commit("CLEAR_USER");
  },
};

const mutations = {
  SET_LOADING(state, loading) {
    state.loading = loading;
  },
  SET_ERROR(state, error) {
    state.error = error;
  },
  SET_USER(state, { userId, email }) {
    state.userId = userId;
    state.email = email;
  },
  CLEAR_USER(state) {
    state.userId = null;
    state.email = null;
    state.error = null;
  },
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations,
};
