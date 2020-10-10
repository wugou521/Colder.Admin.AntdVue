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
        <a-form-model-item label="小节" prop="SchedulesId">
          <a-tree-select
            v-model="entity.SchedulesId"
            allowClear
            :treeData="ScheduleList"
            placeholder="请选择小节"
            treeDefaultExpandAll
            @change="onShedulesChange"
          ></a-tree-select>
        </a-form-model-item>
        <a-form-model-item label="试题类型" prop="Type">
          <a-select v-model="entity.Type" allowClear @change="onFractionTypeChange">
            <a-select-option :key="1">判断题</a-select-option>
            <a-select-option :key="2">单选题</a-select-option>
            <a-select-option :key="3">多选题</a-select-option>
            <a-select-option :key="4">不定项</a-select-option>
            <a-select-option :key="5">不定项子试题</a-select-option>
          </a-select>
        </a-form-model-item>
        <block v-show="entity.Type == 5 && entity.Type != null">
          <a-form-model-item label="不定项父试题" prop="ParentId">
            <a-select v-model="entity.ParentId" allowClear>
              <a-select-option v-for="item in ParentFractions" :key="item.Id">{{ item.Title }}</a-select-option>
            </a-select>
          </a-form-model-item>
        </block>
        <a-form-model-item label="题目标题" prop="Title">
          <a-input v-model="entity.Title" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="题目描述" prop="Description">
          <wang-editor v-model="entity.Description"></wang-editor>
        </a-form-model-item>
        <block v-show="entity.Type != 1 && entity.Type != 4 && entity.Type != null">
          <a-form-model-item label="选项A" prop="A">
            <wang-editor v-model="entity.A"></wang-editor>
          </a-form-model-item>
          <a-form-model-item label="选项B" prop="B">
            <wang-editor v-model="entity.B"></wang-editor>
          </a-form-model-item>
          <a-form-model-item label="选项C" prop="C">
            <wang-editor v-model="entity.C"></wang-editor>
          </a-form-model-item>
          <a-form-model-item label="选项D" prop="D">
            <wang-editor v-model="entity.D"></wang-editor>
          </a-form-model-item>
        </block>
        <a-form-model-item label="答案" prop="Answer" v-show="entity.Type == 1 && entity.Type != null">
          <a-select v-model="entity.Answer" allowClear>
            <a-select-option key="1">正确</a-select-option>
            <a-select-option key="2">错误</a-select-option>
          </a-select>
        </a-form-model-item>
        <a-form-model-item label="答案" prop="Answer" v-show="entity.Type == 2 && entity.Type != null">
          <a-select v-model="entity.Answer" allowClear>
            <a-select-option key="1">A</a-select-option>
            <a-select-option key="2">B</a-select-option>
            <a-select-option key="3">C</a-select-option>
            <a-select-option key="4">D</a-select-option>
          </a-select>
        </a-form-model-item>

        <a-form-model-item
          label="答案"
          prop="AnswerList"
          v-show="entity.Type != 1 && entity.Type != 2 && entity.Type != 4 && entity.Type != null"
        >
          <a-select v-model="entity.AnswerList" allowClear mode="multiple">
            <a-select-option key="1">A</a-select-option>
            <a-select-option key="2">B</a-select-option>
            <a-select-option key="3">C</a-select-option>
            <a-select-option key="4">D</a-select-option>
          </a-select>
        </a-form-model-item>
        <a-form-model-item label="题目解析" prop="Analysis" v-show="entity.Type != 4 && entity.Type != null">
          <wang-editor v-model="entity.Analysis" allowClear></wang-editor>
        </a-form-model-item>
      </a-form-model>
    </a-spin>
  </a-modal>
</template>

<script>
import WangEditor from '@/components/WangEditor/WangEditor'
export default {
  components: {
    WangEditor,
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
      queryParam: {},
      ScheduleList: [],
      ParentFractions: [],
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
      this.$http.post('/Primary/Schedules/GetDataList', {}).then((resJson) => {
        this.loading = false
        this.ScheduleList = resJson.Data
      })
    },
    onFractionTypeChange(type) {
      console.log(type)
      if (type != 4) {
        this.isIndefinite = true
      } else {
        this.isIndefinite = false
      }
    },
    //小节变更事件
    onShedulesChange(id) {
      this.queryParam.schedulesId = id
      this.queryParam.fractionType = 4
      this.$http.post('/Primary/Fractions/GetDataList', { Search: this.queryParam }).then((resJson) => {
        this.loading = false
        this.ParentFractions = resJson.Data
        console.log(this.ParentFractions)
      })
    },
    openForm(id, title) {
      this.init()

      if (id) {
        this.loading = true
        this.$http.post('/Primary/Fractions/GetTheData', { id: id }).then((resJson) => {
          this.loading = false
          this.entity = resJson.Data
          console.log(this.entity)
          if (this.entity.Type == 5) {
            this.queryParam.schedulesId = this.entity.schedulesId
            this.queryParam.fractionType = 4
            this.$http.post('/Primary/Fractions/GetDataList', { Search: this.queryParam }).then((resJson) => {
              this.loading = false
              this.ParentFractions = resJson.Data
            })
          }
        })
      } else {
        this.entity.Description = ''
        this.entity.Analysis = ''
        this.entity.A = ''
        this.entity.B = ''
        this.entity.C = ''
        this.entity.D = ''
      }
    },
    handleSubmit() {
      this.$refs['form'].validate((valid) => {
        if (!valid) {
          return
        }
        this.loading = true
        this.$http.post('/Primary/Fractions/SaveData', this.entity).then((resJson) => {
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
