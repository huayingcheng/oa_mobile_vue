//该文件需要同步到配置后台
export const routesList = {
  "收发文": {
    path: '/doc',
    name: 'doc',
    component: () => import("@/views/Doc/Doc")
  }
}
