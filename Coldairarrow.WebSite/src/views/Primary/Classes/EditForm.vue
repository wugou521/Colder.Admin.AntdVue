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
        <a-form-model-item label="所属学院" prop="CollegeId">
          <a-select v-model="entity.CollegeId" allowClear>
            <a-select-option v-for="item in CollegeList" :key="item.Id">{{ item.CollegeName }}</a-select-option>
          </a-select>
        </a-form-model-item>
        <a-form-model-item label="班级名称" prop="ClassName">
          <a-input v-model="entity.ClassName" autocomplete="off" />
        </a-form-model-item>
        <a-form-item label="班级图片路径" :labelCol="labelCol" :wrapperCol="wrapperCol">
          <!--v-model为图片连接地址(可传单个或数组),maxCount为最大上传数:默认为1-->
          <c-upload-img v-model="entity.IconUrl" :maxCount="1"></c-upload-img>
        </a-form-item>
        <!-- <a-form-model-item label="班级图片路径" prop="IconUrl">
          <a-input v-model="entity.IconUrl" autocomplete="off" />
        </a-form-model-item> -->
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
      CollegeList: [],
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
      this.$http.post('/Primary/College/GetDataList', {}).then((resJson) => {
        if (resJson.Success) {
          this.CollegeList = resJson.Data
        }
      })
    },
    openForm(id, title) {
      this.init()

      if (id) {
        this.loading = true
        this.$http.post('/Primary/Classes/GetTheData', { id: id }).then((resJson) => {
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
        this.$http.post('/Primary/Classes/SaveData', this.entity).then((resJson) => {
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
