import * as requests from '@/network/request'
import store from '@/store/index.js'

export default {
  getModules() {
    return requests.framworkRequest({
      url: '/Home.asmx/GetModules',
      params: {
        userId: store.state.loginUserId,
        address: store.state.address
      }
    })
  }
}
