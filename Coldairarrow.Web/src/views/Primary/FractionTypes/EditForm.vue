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
        <a-form-model-item label="试题类型" prop="FractionType">
          <a-input v-model="entity.FractionType" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="试题分数" prop="FractionCount">
          <a-input v-model="entity.FractionCount" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="是否隐藏" prop="IsHidden">
          <a-radio-group v-model="entity.IsHidden">
            <a-radio :value="0">否</a-radio>
            <a-radio :value="1">是</a-radio>
          </a-radio-group>
        </a-form-model-item>
        <a-form-model-item label="错题扣分" prop="WrongCount">
          <a-input v-model="entity.WrongCount" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="少选得分" prop="LessCount">
          <a-input v-model="entity.LessCount" autocomplete="off" />
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
    },
    openForm(id, title) {
      this.init()

      if (id) {
        this.loading = true
        this.$http.post('/Primary/FractionTypes/GetTheData', { id: id }).then((resJson) => {
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
        this.$http.post('/Primary/FractionTypes/SaveData', this.entity).then((resJson) => {
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
