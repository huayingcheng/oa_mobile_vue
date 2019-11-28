import router from '@/router/index'
import store from '@/store/index'
import homeRequst from "@/network/home/home";
import {
  routesList
} from "@/router/routes"

//动态添加路由
router.beforeEach(async (to, from, next) => {
  if (store.state.ifAddedConfig) {

  } else {
    if (store.state.config.length > 0) {
      for (let m of store.state.config) {
        if (m.IsShow) {
          droutes.push(routesList[m.RouteName])
        }
      }
      router.addRoutes(droutes);

    } else {
      homeRequst
        .getModules()
        .then(res => {
          const droutes = [];
          for (let m of res.data.Data) {
            if (m.IsShow) {
              droutes.push(routesList[m.RouteName])
            }
          }
          router.addRoutes(droutes);
          store.commit({
            type: 'saveConfig',
            config: res.data.Data
          });

          next();
        })
        .catch(err => {
          console.log(err);
        });
    }
  }
  next();
})
