<template>
  <a-modal
    :title="title"
    width="40%"
    :visible="visible"
    :confirmLoading="loading"
    @ok="handleSubmit"
    @cancel="()=>{this.visible=false}"
  >
    <a-spin :spinning="loading">
      <a-form-model ref="form" :model="entity" :rules="rules" v-bind="layout">
        <a-form-model-item label="用户编号" prop="UserId">
          <a-input v-model="entity.UserId" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="小节编号" prop="ScheduleId">
          <a-input v-model="entity.ScheduleId" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="一级目录编号" prop="ScheduleOneId">
          <a-input v-model="entity.ScheduleOneId" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="题目编号" prop="FractionId">
          <a-input v-model="entity.FractionId" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="记录类型" prop="Type">
          <a-input v-model="entity.Type" autocomplete="off" />
        </a-form-model-item>
      </a-form-model>
    </a-spin>
  </a-modal>
</template>

<script>
export default {
  props: {
    parentObj: Object
  },
  data() {
    return {
      layout: {
        labelCol: { span: 5 },
        wrapperCol: { span: 18 }
      },
      visible: false,
      loading: false,
      entity: {},
      rules: {},
      title: ''
    }
  },
  methods: {
    init() {
      this.visible = true
      this.entity = {}
      this.$nextTick(() => {
        this.$refs['form'].clearValidate()
      })
    },
    openForm(id, title) {
      this.init()

      if (id) {
        this.loading = true
        this.$http.post('/Primary/Records/GetTheData', { id: id }).then(resJson => {
          this.loading = false

          this.entity = resJson.Data
        })
      }
    },
    handleSubmit() {
      this.$refs['form'].validate(valid => {
        if (!valid) {
          return
        }
        this.loading = true
        this.$http.post('/Primary/Records/SaveData', this.entity).then(resJson => {
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
    }
  }
}
</script>
