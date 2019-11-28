import axios from "axios";
import store from '@/store/index.js'
const framworkRequest = axios.create({
  baseURL: "http://" + store.state.serverIp + "/" + store.state.contextPath + "/Extensions/ZhejiangGovernmentDingTalkServer/Webservices"
})
// framworkRequest.interceptors.request.use(config => {
//   console.log('我是request拦截器的config');
//   return config;
// }, err => {
//   console.log(err);
// })

// framworkRequest.interceptors.response.use(res => {
//   console.log('我是response拦截器的res');
//   return res.data;
// }, err => {
//   console.log(err);
// })



export {
  framworkRequest
}
