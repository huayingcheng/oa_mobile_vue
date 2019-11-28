import Vue from 'vue'
import VueRouter from 'vue-router'
import store from '@/store'
import homeRequst from "@/network/home/home";

import {
  routesList
} from './routes'

Vue.use(VueRouter)

const routes = [{
  path: '/',
  name: 'home',
  component: () => import('@/views/Home')
}]

const router = new VueRouter({
  routes,
  mode: 'history'
})



export default router
