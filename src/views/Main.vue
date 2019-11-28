<template>
  <el-container style="height: 100%; border: 1px solid #eee">
    <el-header>
      <span>浙政钉服务端配置管理</span>
    </el-header>
    <el-container>
      <el-aside width="200px" style="background-color: rgb(238, 241, 246)">
        <el-menu :default-openeds="['1']" :router="true">
          <el-submenu index="1">
            <template slot="title">
              <i class="el-icon-setting"></i>
              主页面配置
            </template>
            <el-menu-item index="/home/init">
              初始化设置
            </el-menu-item>
            <el-menu-item index="/home/modulessettings">
              模块配置
            </el-menu-item>
          </el-submenu>
        </el-menu>
      </el-aside>
      <el-main>
        <router-view></router-view>
      </el-main>
    </el-container>
  </el-container>
</template>


<script>
export default {
  data() {
    return {};
  },
  methods: {}
};
</script>

<style>
@import "~@/assets/css/main/main.css";

.el-header {
  background-color: #b3c0d1;
  color: #333;
  line-height: 60px;
}

.el-aside {
  color: #333;
}
</style>