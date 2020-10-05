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
        <a-form-item label="Logo" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <!--v-model为图片连接地址(可传单个或数组),maxCount为最大上传数:默认为1-->
          <c-upload-img v-model="entity.LogoUrl" :maxCount="1"></c-upload-img>
        </a-form-item>
        <a-form-model-item label="SEO关键字" prop="SeoKeyword">
          <a-input v-model="entity.SeoKeyword" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="ICP备案号" prop="ICP">
          <a-input v-model="entity.ICP" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="公安网备" prop="ICP2">
          <a-input v-model="entity.ICP2" autocomplete="off" />
        </a-form-model-item>
        <a-form-item label="幻灯片" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <!--v-model为图片连接地址(可传单个或数组),maxCount为最大上传数:默认为1-->
          <c-upload-img v-model="entity.SlidesList" :maxCount="100"></c-upload-img>
        </a-form-item>
      </a-form-model>
    </a-spin>
  </a-modal>
</template>

<script>
import CUploadImg from '@/components/CUploadImg/CUploadImg'
export default {
  components: {
    CUploadImg,
  },
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
        this.$http.post('/Primary/SystemConfig/GetTheData', { id: id }).then((resJson) => {
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
        this.$http.post('/Primary/SystemConfig/SaveData', this.entity).then((resJson) => {
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
