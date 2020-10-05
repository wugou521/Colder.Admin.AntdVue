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
        <a-form-model-item label="UserId" prop="UserId">
          <a-input v-model="entity.UserId" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="小节编号" prop="SchedulesId">
          <a-input v-model="entity.SchedulesId" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="一级课程编号" prop="SchedulesOneId">
          <a-input v-model="entity.SchedulesOneId" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="总题目数量" prop="FractionCount">
          <a-input v-model="entity.FractionCount" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="回答题目数量" prop="FractionAnswerCount">
          <a-input v-model="entity.FractionAnswerCount" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="正确率" prop="RightRate">
          <a-input v-model="entity.RightRate" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="正确数量" prop="RightFraction">
          <a-input v-model="entity.RightFraction" autocomplete="off" />
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
        this.$http.post('/Primary/AnswerRecords/GetTheData', { id: id }).then(resJson => {
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
        this.$http.post('/Primary/AnswerRecords/SaveData', this.entity).then(resJson => {
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
