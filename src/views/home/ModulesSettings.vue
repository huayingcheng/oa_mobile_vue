<template>
  <div class="table">
    <el-container>
      <!-- 头部工具栏 -->
      <el-header>
        <el-select
          v-model="selectValue"
          placeholder="请选择配置"
          @change="selectChange"
        >
          <el-option
            v-for="item in configs"
            :key="item.Name"
            :value="item.Name"
          >
          </el-option>
        </el-select>
        <!-- 添加配置窗口 -->
        <el-dialog
          title="提示"
          :visible.sync="showAddConfigWindow"
          :lock-scroll="false"
        >
          <div>
            配置名称:<el-input
              size="medium "
              placeholder="请输入内容"
              v-model="newConfigName"
            ></el-input>
          </div>
          <span slot="footer" class="dialog-footer">
            <el-button @click="showAddConfigWindow = false">取 消</el-button>
            <el-button type="primary" @click="addConfig">确 定</el-button>
          </span>
        </el-dialog>
        <el-button @click="openAddConfigWindow">添加配置</el-button>
        <!-- 导入配置上传 -->
        <el-dialog :visible.sync="showUploadWindow" :lock-scroll="false">
          <el-upload
            class="upload-demo"
            ref="upload"
            :action="
              'http://' +
                $store.state.serverIp +
                '/' +
                $store.state.contextPath +
                '/Extensions/ZhejiangGovernmentDingTalkServer/Config/Webservices/Home/Modules.asmx/UploadConfig'
            "
            :auto-upload="false"
            :on-success="uploadSuccess"
            :on-error="uploadError"
          >
            <el-button slot="trigger" size="small" type="primary"
              >选取文件</el-button
            >
            <el-button
              style="margin-left: 10px;"
              size="small"
              type="success"
              @click="submitUpload"
              >上传到服务器</el-button
            >
          </el-upload>
        </el-dialog>
        <el-button @click="showUploadWindow = true" v-show="false"
          >导入配置</el-button
        >
        <!-- 添加模块窗口 -->
        <el-dialog
          title="提示"
          :visible.sync="showAddModuleWindow"
          :lock-scroll="false"
        >
          <div>
            模块名称:<el-input
              size="medium "
              placeholder="请输入内容"
              v-model="newModuleName"
            ></el-input>
          </div>
          <span slot="footer" class="dialog-footer">
            <el-button @click="showAddModuleWindow = false">取 消</el-button>
            <el-button type="primary" @click="addModule">确 定</el-button>
          </span>
        </el-dialog>
        <el-button @click="openAddModuleWindow" v-show="false"
          >添加模块</el-button
        >
        <el-button @click="deleteModule" v-show="false">删除模块</el-button>
        <el-button @click="saveConfig">保存</el-button>
      </el-header>

      <el-container>
        <!-- tree -->
        <el-aside id="el-aside">
          <el-menu
            :default-openeds="['1']"
            :router="false"
            @select="selectModule"
            :default-active="selectedModule.Text"
          >
            <el-submenu index="1">
              <template slot="title">
                <i class="el-icon-setting"></i>
                模块名称
              </template>
              <el-menu-item
                v-for="item in selectedConfig"
                :key="item.Text"
                :index="item.Text"
              >
                {{ item.Text }}
              </el-menu-item>
            </el-submenu>
          </el-menu>
        </el-aside>
        <!-- 模块配置信息 -->
        <el-main id="el-main">
          <el-row :gutter="20">
            <el-col :span="8"><div class="div">模块名称</div></el-col>
            <el-col :span="16"
              ><div class="div">
                <el-input
                  size="medium "
                  placeholder="请输入内容"
                  v-model="selectedModule.Text"
                ></el-input></div
            ></el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="8"><div class="div">背景色</div></el-col>
            <el-col :span="16"
              ><div class="div">
                <el-color-picker
                  v-model="selectedModule.Bg"
                  :show-alpha="true"
                  color-format="rgb"
                ></el-color-picker></div
            ></el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="8"><div class="div">图标</div></el-col>
            <el-col :span="12"
              ><div
                class="div"
                :style="
                  'width: 120px; height: 120px;border-radius: 20px;background-color:' +
                    selectedModule.Bg
                "
              >
                <el-image
                  style="width: 100px; height: 100px"
                  :src="
                    'http://' +
                      $store.state.serverIp +
                      '/' +
                      $store.state.contextPath +
                      '/' +
                      selectedModule.ImgSrc
                  "
                ></el-image></div
            ></el-col>
            <el-col :span="4"
              ><div class="div">
                <el-button @click="pageSelectIcon = true">选择图标</el-button>
              </div>
              <!-- 图标选择的弹出界面 -->
              <el-dialog
                title="提示"
                :visible.sync="pageSelectIcon"
                :lock-scroll="false"
                @open="initPageSelectIcons"
              >
                <div class="divPageSelectIcon">
                  <div
                    class="divPageSelectIconItem"
                    v-for="item in icons"
                    :key="item"
                    :style="
                      'background-color:' +
                        (item == currentSelectedIcon
                          ? selectedModule.Bg
                          : 'black')
                    "
                    @click="iconItemClick(item)"
                  >
                    <img
                      :src="
                        'http://' +
                          $store.state.serverIp +
                          '/' +
                          $store.state.contextPath +
                          '/' +
                          item
                      "
                    />
                  </div>
                </div>
                <span slot="footer" class="dialog-footer">
                  <el-button @click="pageSelectIcon = false">取 消</el-button>
                  <el-button type="primary" @click="pageSelectIcon = false"
                    >确 定</el-button
                  >
                </span>
              </el-dialog>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="8"><div class="div">是否有右上角info</div></el-col>
            <el-col :span="16"
              ><div class="div">
                <el-switch
                  v-model="selectedModule.IfInfo"
                  active-color="#13ce66"
                  inactive-color="#ff4949"
                >
                </el-switch></div
            ></el-col>
          </el-row>
          <el-row :gutter="20" v-show="selectedModule.IfInfo">
            <el-col :span="8"><div class="div">右上角info计算方法</div></el-col>
            <el-col :span="16"
              ><div class="div">
                <el-select
                  v-model="selectedModule.MethodName"
                  placeholder="请选择"
                >
                  <el-option
                    v-for="item in methods"
                    :key="item.MethodName"
                    :value="item.MethodName"
                    :label="item.Description"
                  >
                  </el-option>
                </el-select></div
            ></el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="8"><div class="div">是否显示</div></el-col>
            <el-col :span="16"
              ><div class="div">
                <el-switch
                  v-model="selectedModule.IsShow"
                  active-color="#13ce66"
                  inactive-color="#ff4949"
                >
                </el-switch></div
            ></el-col>
          </el-row>
        </el-main>
      </el-container>
    </el-container>
  </div>
</template>

<script>
import request from "@/network/home/home.js";
import { notify } from "@/common/utils.js";
export default {
  data() {
    return {
      //页面初始化数据
      configs: [],
      icons: [],
      methods: [],
      pageSelectIcon: false,
      showAddModuleWindow: false,
      newModuleName: "",
      showAddConfigWindow: false,
      newConfigName: "",
      showUploadWindow: false,

      //过程数据
      selectValue: "",
      selectedConfig: [],
      selectedModule: { Text: "", Bg: "", ImgSrc: "", IfInfo: false }
    };
  },
  computed: {
    currentSelectedIcon() {
      return this.selectedModule.ImgSrc;
    }
  },
  created() {
    this.getConfigs();
    this.getMethods();
  },
  methods: {
    //获取所有的配置信息
    getConfigs() {
      request.getConfigs().then(res => {
        this.configs.push(...res.data);
      });
    },
    getMethods() {
      request.getMethods().then(res => {
        this.methods.push(...res.data);
      });
    },
    //select发生变化
    selectChange(selectedValue) {
      for (let config of this.configs) {
        if (config.Name === selectedValue) {
          this.selectedConfig.splice(0, this.selectedConfig.length);
          this.selectedConfig.push(...config.Content);
        }
      }
    },
    //选择模块
    selectModule(index, indexPath) {
      for (let m of this.selectedConfig) {
        if (m.Text === index) {
          this.selectedModule = m;
        }
      }
    },
    //初始化选择iconsPage
    initPageSelectIcons() {
      request.getIcons().then(res => {
        this.icons.splice(0, this.icons.length);
        this.icons.push(...res.data);
      });
    },
    //iconsPage页面上选择图标
    iconItemClick(item) {
      this.selectedModule.ImgSrc = item;
    },
    //保存配置
    saveConfig() {
      if (this.selectValue === "") {
        notify(this, "保存配置", "请先选择一个配置");
        return;
      }

      request
        .saveConfig(this.selectValue, this.selectedConfig)
        .then(res => {
          const result = JSON.parse(res.data.replace(/{"d":null}/g, ""));
          if (result.ErrorCount === 0) {
            notify(this, "保存结果", result.Data);
          }
        })
        .catch(err => {
          console.log(err);
        });
    },
    //打开添加模块窗口
    openAddModuleWindow() {
      if (this.selectValue === "") {
        notify(this, "添加模块", "请先选择某个配置");
      } else {
        this.showAddModuleWindow = true;
      }
    },
    //添加一个模块
    addModule() {
      if (this.newModuleName === "") {
        notify(this, "添加模块", "请先给模块取名");
      } else {
        let ifSameName = false;
        for (let m of this.selectedConfig) {
          if (m.Text === this.newModuleName) {
            ifSameName = true;
          }
        }
        if (ifSameName) {
          notify(this, "添加模块", "不允许出现同名模块");
        } else {
          this.selectedConfig.push({
            Text: this.newModuleName,
            ImgSrc: "",
            Bg: "",
            RoutePath: "",
            RouteComponent: "",
            IfInfo: false,
            MethodName: ""
          });
          this.showAddModuleWindow = false;
          this.selectModule(this.newModuleName, "");
          this.newModuleName = "";
        }
      }
    },
    //删除模块
    deleteModule() {
      if (this.selectedModule.Text === "") {
        notify(this, "删除模块", "请先选择一个模块");
      } else {
        for (let i = 0; i < this.selectedConfig.length; i++) {
          if (this.selectedConfig[i].Text === this.selectedModule.Text) {
            this.selectedConfig.splice(i, 1);
            this.selectedModule.Text = "";
          }
        }
      }
    },
    //打开添加配置窗口
    openAddConfigWindow() {
      this.showAddConfigWindow = true;
    },
    //添加一个配置
    addConfig() {
      if (this.newConfigName === "") {
        notify(this, "添加配置", "配置名称不能为空");
        return;
      }
      if (this.newConfigName === "base") {
        notify(this, "添加配置", "配置名称不能为base");
        return;
      }
      for (let c of this.configs) {
        if (c.Name === this.newConfigName) {
          notify(this, "添加配置", "已有同名配置存在");
          return;
        }
      }

      request.addNewConfig(this.newConfigName).then(res => {
        this.$router.go(0);
      });
    },
    //上传配置到服务器
    submitUpload() {
      this.$refs.upload.submit();
    },
    uploadSuccess(res, file) {
      this.$router.go(0);
    },
    uploadError(res, file) {
      console.log(res);
    }
  }
};
</script>

<style scoped>
@import "~@/assets/css/home/ModulesSettigs.css";
</style>