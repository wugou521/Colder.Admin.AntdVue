<template>
  <div class="main">
    <div class="form">
      <a-spin :spinning="loading">
        <a-form id="formLogin" class="user-layout-login" ref="formLogin" :form="form" @submit="handleSubmit">
          <a-tabs
            :activeKey="customActiveKey"
            :tabBarStyle="{ textAlign: 'center', borderBottom: 'unset', tabBarStyle: '#21CBC9' }"
            @change="handleTabClick"
          >
            <a-tab-pane key="tab1" tab="账号密码登录">
              <a-form-item>
                <a-input
                  size="large"
                  type="text"
                  placeholder="请输入用户名"
                  v-decorator="['userName', { rules: [{ required: true, message: '请输入用户名' }] }]"
                >
                  <a-icon slot="prefix" type="user" :style="{ color: '#21CBC9' }" />
                </a-input>
              </a-form-item>

              <a-form-item>
                <a-input
                  size="large"
                  type="password"
                  autocomplete="false"
                  placeholder="请输入密码"
                  v-decorator="['password', { rules: [{ required: true, message: '请输入密码' }] }]"
                >
                  <a-icon slot="prefix" type="lock" :style="{ color: '#21CBC9' }" />
                </a-input>
              </a-form-item>
              <a-form-item>
                <a-checkbox v-decorator="['savePwd', { valuePropName: 'checked' }]">记住密码</a-checkbox>
              </a-form-item>
            </a-tab-pane>
            <!-- <a-tab-pane key="tab2" tab="手机号登录">
          <a-form-item>
            <a-input
              size="large"
              type="text"
              placeholder="手机号"
              v-decorator="[
                'mobile',
                {
                  rules: [{ required: true, pattern: /^1[34578]\d{9}$/, message: '请输入正确的手机号' }],
                  validateTrigger: 'change'
                }
              ]"
            >
              <a-icon slot="prefix" type="mobile" :style="{ color: 'rgba(0,0,0,.25)' }" />
            </a-input>
          </a-form-item>

          <a-row :gutter="16">
            <a-col class="gutter-row" :span="16">
              <a-form-item>
                <a-input
                  size="large"
                  type="text"
                  placeholder="验证码"
                  v-decorator="[
                    'captcha',
                    { rules: [{ required: true, message: '请输入验证码' }], validateTrigger: 'blur' }
                  ]"
                >
                  <a-icon slot="prefix" type="mail" :style="{ color: 'rgba(0,0,0,.25)' }" />
                </a-input>
              </a-form-item>
            </a-col>
            <a-col class="gutter-row" :span="8">
              <a-button
                class="getCaptcha"
                tabindex="-1"
                :disabled="state.smsSendBtn"
                @click.stop.prevent="getCaptcha"
                v-text="(!state.smsSendBtn && '获取验证码') || state.time + ' s'"
              ></a-button>
            </a-col>
          </a-row>
        </a-tab-pane> -->
          </a-tabs>

          <a-form-item style="margin-top: 24px">
            <a-button size="large" style="background: #21cbc9; color: #fff" htmlType="submit" class="login-button"
              >确定</a-button
            >
          </a-form-item>
        </a-form>
      </a-spin>
    </div>
  </div>
</template>

<script>
import TokenCache from '@/utils/cache/TokenCache'

export default {
  data() {
    return {
      loading: false,
      customActiveKey: 'tab1',
      form: this.$form.createForm(this),
    }
  },
  mounted() {
    var userName = localStorage.getItem('userName')
    var password = localStorage.getItem('password')
    if (userName && password) {
      this.form.setFieldsValue({ userName, password, savePwd: true })
    }
  },
  methods: {
    handleTabClick(key) {
      this.customActiveKey = key
      // this.form.resetFields()
    },
    handleSubmit(e) {
      e.preventDefault()

      this.form.validateFields((errors, values) => {
        //校验成功
        if (!errors) {
          var values = this.form.getFieldsValue()
          this.loading = true
          this.$http.post('/Base_Manage/Home/SubmitLogin', values).then((resJson) => {
            this.loading = false

            if (resJson.Success) {
              TokenCache.setToken(resJson.Data)
              //保存密码
              if (values['savePwd']) {
                localStorage.setItem('userName', values['userName'])
                localStorage.setItem('password', values['password'])
              } else {
                localStorage.removeItem('userName')
                localStorage.removeItem('password')
              }
              this.$router.push({ path: '/' })
            } else {
              this.$message.error(resJson.Msg)
            }
          })
        }
      })
    },
  },
}
</script>

<style lang="less" scoped>
.main {
  background: url(~@/assets/login/loginbanner.png) no-repeat;
  width: 100%;
  height: 800px;
  margin-top: 100px;
  .form {
    float: right;
    margin-top: 80px;
    width: 370px;
    height: 420px;
    position: relative;
    border: 1px solid rgb(202, 202, 202);
    padding: 20px;
  }
}
.user-layout-login {
  // width: 350px;
  // height: 400px;
  // position: absolute;
  // margin-top: 200px;
  // right: 50px;
  // border: 1px solid #cacaca;
  label {
    font-size: 14px;
  }

  .getCaptcha {
    display: block;
    width: 100%;
    height: 40px;
  }

  .forge-password {
    font-size: 14px;
  }

  button.login-button {
    padding: 0 15px;
    font-size: 16px;
    height: 40px;
    width: 100%;
  }

  .user-login-other {
    text-align: left;
    margin-top: 24px;
    line-height: 22px;

    .item-icon {
      font-size: 24px;
      color: rgba(0, 0, 0, 0.2);
      margin-left: 16px;
      vertical-align: middle;
      cursor: pointer;
      transition: color 0.3s;

      &:hover {
        color: #1890ff;
      }
    }

    .register {
      float: right;
    }
  }
}
</style>
