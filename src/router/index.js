import Vue from 'vue'
import VueRouter from 'vue-router'

Vue.use(VueRouter)
const originalPush = VueRouter.prototype.push
VueRouter.prototype.push = function push(location) {
  return originalPush.call(this, location).catch(err => err)
}

const routes = [{
  path: '',
  redirect: '/home'
}, {
  path: '/home',
  name: 'main',
  component: () => import('@/views/Main'),
  children: [{
    path: '',
    redirect: 'init'
  }, {
    path: 'init',
    name: 'init',
    component: () => import('@/views/home/Init')
  }, {
    path: 'modulessettings',
    name: 'modulessettings',
    component: () => import('@/views/home/ModulesSettings')
  }]
}]

const router = new VueRouter({
  routes,
  mode: 'history'
})

export default router
