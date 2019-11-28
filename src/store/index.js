import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    serverIp: "localhost",
    contextPath: "oa",
    address: "衢江",
    loginUserId: 31,

    config: [],
    ifAddedConfig: false
  },
  mutations: {
    saveConfig(state, payload) {
      state.config.push(...payload.config)
      state.ifAddedConfig = true
    }
  },
  actions: {},
  modules: {}
})
