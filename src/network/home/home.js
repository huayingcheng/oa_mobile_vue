import * as requests from '@/network/request'
import Axios from 'axios'

export default {
  getConfigs() {
    return requests.framworkRequest({
      url: '/Home/Modules.asmx/GetConfigs'
    })
  },

  getIcons() {
    return requests.framworkRequest({
      url: '/Home/Modules.asmx/GetIcons'
    })
  },

  getMethods() {
    return requests.framworkRequest({
      url: '/Home/Modules.asmx/GetInfoMethods'
    })
  },

  saveConfig(configName, configContent) {
    return requests.framworkRequest({
      url: '/Home/Modules.asmx/SaveConfig',
      data: {
        configName,
        configContent: JSON.stringify(configContent)
      },
      method: 'post'
    })
  },

  addNewConfig(configName) {
    return requests.framworkRequest({
      url: '/Home/Modules.asmx/AddNewConfig',
      params: {
        configName
      }
    })
  }
}
