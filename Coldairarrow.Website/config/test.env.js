'use strict'
const merge = require('webpack-merge')
const devEnv = require('./dev.env')

module.exports = merge(devEnv, {
  NODE_ENV: '"testing"',
  VUE_APP_ProjectName: '1000ti',
  VUE_APP_DesktopPath: '/Home/Index',
  VUE_APP_PublishRootUrl: 'http://120.27.244.70:5000',
  VUE_APP_LocalRootUrl: 'http://localhost:5000',
  VUE_APP_ApiTimeout: 10000,
  VUE_APP_DevPort: 5002
})
