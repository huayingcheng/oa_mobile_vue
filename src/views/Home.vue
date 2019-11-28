<template>
  <div>
    <div class="userInfo-wrap"></div>
    <van-grid :column-num="4">
      <van-grid-item
        v-for="m in modules"
        :text="m.Text"
        :key="m.Key"
        :info="m.Info"
        @click="toUrl(m)"
      >
        <template v-slot:icon>
          <div class="item-div" :style="'background-color:' + m.Bg">
            <img
              :src="
                'http://' +
                  $store.state.serverIp +
                  '/' +
                  $store.state.contextPath +
                  '/' +
                  m.ImgSrc
              "
            />
          </div>
        </template>
        <template v-slot:text>
          <div class="item-div-text">{{ m.Text }}</div>
        </template>
      </van-grid-item>
    </van-grid>
  </div>
</template>

<script>
import Vue from "vue";
import { Grid, GridItem } from "vant";
import "vant/lib/grid/style";
import "vant/lib/grid-item/style";
import homeRequst from "@/network/home/home";
import { routesList } from "@/router/routes";

Vue.use(Grid).use(GridItem);

export default {
  components: { Grid, GridItem },
  props: {},
  data() {
    return {
      modules: []
    };
  },
  created() {
    this.getModules();
  },
  methods: {
    getModules() {
      if (this.$store.state.config.length > 0) {
        for (let m of this.$store.state.config) {
          if (m.IsShow) {
            if (m.IsShow) this.modules.push(m);
          }
        }
      } else {
        homeRequst
          .getModules()
          .then(res => {
            for (let m of res.data.Data) {
              if (m.IsShow) this.modules.push(m);
            }
          })
          .catch(err => {
            console.log(err);
          });
      }
    },
    toUrl(module) {
      this.$router.push({
        path: routesList[module.RouteName].path,
        query: module.Query
      });
    }
  }
};
</script>
<style scoped>
@import "~@/assets/css/home/home.css";
</style>
