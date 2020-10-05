<template>
  <a-modal
    :title="title"
    width="40%"
    :visible="visible"
    :confirmLoading="loading"
    @ok="handleSubmit"
    @cancel="
      () => {
        this.visible = false
      }
    "
  >
    <a-spin :spinning="loading">
      <a-form-model ref="form" :model="entity" :rules="rules" v-bind="layout">
        <a-form-model-item label="用户名称" prop="UserId">
          <a-select v-model="entity.UserId" allowClear>
            <a-select-option v-for="item in UserList" :key="item.Id">{{ item.UserName }}</a-select-option>
          </a-select>
        </a-form-model-item>
        <a-form-model-item label="课程名称" prop="ClassesId">
          <a-select v-model="entity.ClassesId" allowClear>
            <a-select-option v-for="item in ClassesList" :key="item.Id">{{ item.ClassName }}</a-select-option>
          </a-select>
        </a-form-model-item>
        <a-form-model-item label="过期时间" prop="ExpireTime">
          <a-date-picker v-model="entity.ExpireTime" format="YYYY-MM-DD" />
        </a-form-model-item>
      </a-form-model>
    </a-spin>
  </a-modal>
</template>

<script>
export default {
  props: {
    parentObj: Object,
  },
  data() {
    return {
      layout: {
        labelCol: { span: 5 },
        wrapperCol: { span: 18 },
      },
      UserList: [],
      ClassesList: [],
      visible: false,
      loading: false,
      entity: {},
      rules: {},
      title: '',
    }
  },
  methods: {
    init() {
      this.visible = true
      this.entity = {}
      this.$nextTick(() => {
        this.$refs['form'].clearValidate()
      })
      this.$http.post('/Primary/User/GetDataList', {}).then((resJson) => {
        if (resJson.Success) {
          this.UserList = resJson.Data
        }
      })
      this.$http.post('/Primary/Classes/GetDataList', {}).then((resJson) => {
        if (resJson.Success) {
          this.ClassesList = resJson.Data
        }
      })
    },
    openForm(id, title) {
      this.init()

      if (id) {
        this.loading = true
        this.$http.post('/Primary/BuyRecord/GetTheData', { id: id }).then((resJson) => {
          this.loading = false

          this.entity = resJson.Data
        })
      }
    },
    handleSubmit() {
      this.$refs['form'].validate((valid) => {
        if (!valid) {
          return
        }
        this.loading = true
        this.$http.post('/Primary/BuyRecord/SaveData', this.entity).then((resJson) => {
          this.loading = false

          if (resJson.Success) {
            this.$message.success('操作成功!')
            this.visible = false

            this.parentObj.getDataList()
          } else {
            this.$message.error(resJson.Msg)
          }
        })
      })
    },
  },
}
</script>
