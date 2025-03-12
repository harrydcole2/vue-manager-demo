// import axios from "axios";

const state = {
  //   token: localStorage.getItem("token") || null,
  //   user: JSON.parse(localStorage.getItem("user")) || null,
};

const getters = {
  //   isLoggedIn: (state) => !!state.token,
  //   user: (state) => state.user,
};

const actions = {
  //   async login({ commit }, credentials) {
  //     try {
  //       // This would be replaced with your actual API endpoint
  //       // const response = await axios.post('/api/user/login', credentials)
  //       console.log("Login credentials:", credentials, axios.defaults.baseURL);
  //       const mockResponse = {
  //         data: {
  //           token: "mock-token",
  //           user: {
  //             id: 1,
  //             username: "testuser",
  //           },
  //         },
  //       };
  //       const { token, user } = mockResponse.data;
  //       localStorage.setItem("token", token);
  //       localStorage.setItem("user", JSON.stringify(user));
  //       commit("SET_AUTH", { token, user });
  //       return true;
  //     } catch (error) {
  //       console.error("Login error:", error);
  //       return false;
  //     }
  //   },
  //   logout({ commit }) {
  //     localStorage.removeItem("token");
  //     localStorage.removeItem("user");
  //     commit("CLEAR_AUTH");
  //   },
};

const mutations = {
  //   SET_AUTH(state, { token, user }) {
  //     state.token = token;
  //     state.user = user;
  //   },
  //   CLEAR_AUTH(state) {
  //     state.token = null;
  //     state.user = null;
  //   },
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations,
};
