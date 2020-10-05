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
        <a-form-model-item label="班级" prop="ClassesId">
          <a-select v-model="entity.ClassesId" allowClear>
            <a-select-option v-for="item in ClassesList" :key="item.Id">{{ item.ClassName }}</a-select-option>
          </a-select>
        </a-form-model-item>
        <a-form-model-item label="父级" prop="ParentId">
          <a-tree-select
            v-model="entity.ParentId"
            allowClear
            :treeData="ScheduleList"
            placeholder="请选择父级"
            treeDefaultExpandAll
          ></a-tree-select>
        </a-form-model-item>
        <a-form-model-item label="名称" prop="Name">
          <a-input v-model="entity.Name" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="标题" prop="Title">
          <a-input v-model="entity.Title" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="图标" prop="IconUrl">
          <a-input v-model="entity.IconUrl" autocomplete="off" />
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
      ScheduleList: [],
      ClassesList: [],
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
      this.$http.post('/Primary/Schedules/GetDataList', {}).then((resJson) => {
        this.loading = false
        this.ScheduleList = resJson.Data
      })
      this.$http.post('/Primary/Classes/GetDataList', {}).then((resJson) => {
        this.loading = false
        this.ClassesList = resJson.Data
      })
    },
    onClassChange(){

    },
    openForm(id, title) {
      this.init()

      if (id) {
        this.loading = true
        this.$http.post('/Primary/Schedules/GetTheData', { id: id }).then((resJson) => {
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
        this.$http.post('/Primary/Schedules/SaveData', this.entity).then((resJson) => {
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
