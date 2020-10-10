/**
 */

export default {
    production: process.env.NODE_ENV === 'production' && process.env.VUE_APP_PREVIEW !== 'true',
    // vue-ls options
    storageOptions: {
      namespace: 'pro__',
      name: 'ls',
      storage: 'local'
    },
    projectName: process.env.VUE_APP_ProjectName,//项目名
    desktopPath: process.env.VUE_APP_DesktopPath,//首页路径
    publishRootUrl: process.env.VUE_APP_PublishRootUrl,//发布后接口根地址
    localRootUrl: process.env.VUE_APP_LocalRootUrl,//本地调试接口根地址
    apiTimeout: parseInt(process.env.VUE_APP_ApiTimeout),//接口超时时间ms
    devPort: parseInt(process.env.VUE_APP_DevPort) //本地开发启动端口
  }