<template>
  <div class="table">
    <el-row :gutter="20" v-show="selectedModule.IfInfo">
      <el-col :span="8"><div class="div">是否使用轮播图</div></el-col>
      <el-col :span="16"></el-col>
    </el-row>
  </div>
</template>

<script>
export default {
  components: {},
  props: {},
  data() {
    return {};
  },
  computed: {},
  watch: {},
  created() {},
  mounted() {},
  methods: {}
};
</script>

<style scoped>
@import "~@/assets/css/home/Init.css";
</style>