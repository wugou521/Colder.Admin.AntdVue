<template>
  <a-card :bordered="false">
    <div class="table-operator">
      <a-button type="primary" icon="plus" @click="hanldleAdd()">新建</a-button>
      <a-button
        type="primary"
        icon="minus"
        @click="handleDelete(selectedRowKeys)"
        :disabled="!hasSelected()"
        :loading="loading"
        >删除</a-button
      >
      <a-button type="primary" icon="redo" @click="getDataList()">刷新</a-button>
      <a-button type="primary" icon="download" @click="DownLoadTemplate()">下载模板</a-button>
      <a-form-item label="试题上传" :labelCol="labelCol" :wrapperCol="wrapperCol">
        <c-upload-file v-model="filePath" :maxCount="1"></c-upload-file>
        <a-button type="danger" icon="to-top" @click="UploadFractions()">上传</a-button>
      </a-form-item>
    </div>

    <div class="table-page-search-wrapper">
      <a-form layout="inline">
        <a-row :gutter="48">
          <a-col :md="4" :sm="24">
            <a-form-item>
              <a-input v-model="queryParam.keyword" placeholder="关键字" />
            </a-form-item>
          </a-col>
          <a-col :md="6" :sm="24">
            <a-button
              type="primary"
              @click="
                () => {
                  this.pagination.current = 1
                  this.getDataList()
                }
              "
              >查询</a-button
            >
            <a-button style="margin-left: 8px" @click="() => (queryParam = {})">重置</a-button>
          </a-col>
        </a-row>
      </a-form>
    </div>

    <a-table
      ref="table"
      :columns="columns"
      :rowKey="(row) => row.Id"
      :dataSource="data"
      :pagination="pagination"
      :loading="loading"
      @change="handleTableChange"
      :rowSelection="{ selectedRowKeys: selectedRowKeys, onChange: onSelectChange }"
      :bordered="true"
      size="small"
    >
      <span slot="action" slot-scope="text, record">
        <template>
          <a @click="handleEdit(record.Id)">编辑</a>
          <a-divider type="vertical" />
          <a @click="handleDelete([record.Id])">删除</a>
        </template>
      </span>
    </a-table>

    <edit-form ref="editForm" :parentObj="this"></edit-form>
  </a-card>
</template>

<script>
import EditForm from './EditForm'
import CUploadFile from '@/components/CUploadFile/CUploadFile'
import defaultSettings from '@/config/defaultSettings'
const columns = [
  { title: '章节名称', dataIndex: 'SchedulesName', width: '30%' },
  { title: '父级标题', dataIndex: 'ParentName', width: '10%' },
  { title: '题目标题', dataIndex: 'Title', width: '10%' },
  // { title: '题目描述', dataIndex: 'Description', width: '10%' },
  { title: '题目答案', dataIndex: 'AnswerList', width: '10%' },
  { title: '题目类型', dataIndex: 'TypeName', width: '10%' },
  { title: '操作', dataIndex: 'action', scopedSlots: { customRender: 'action' } },
]

export default {
  components: {
    EditForm,
    CUploadFile,
  },
  mounted() {
    this.getDataList()
  },
  data() {
    return {
      data: [],
      pagination: {
        current: 1,
        pageSize: 10,
        showTotal: (total, range) => `总数:${total} 当前:${range[0]}-${range[1]}`,
      },
      filters: {},
      sorter: { field: 'Id', order: 'asc' },
      loading: false,
      columns,
      queryParam: {},
      selectedRowKeys: [],
      filePath: '',
    }
  },
  methods: {
    handleTableChange(pagination, filters, sorter) {
      this.pagination = { ...pagination }
      this.filters = { ...filters }
      this.sorter = { ...sorter }
      this.getDataList()
    },
    DownLoadTemplate() {
      window.open(defaultSettings.localRootUrl + '/Upload/templates/fraction_upload_template.xlsx', '下载模板')
    },
    UploadFractions() {
      if (!this.filePath || this.filePath.length == 0) {
        this.$message.error('请上传文件')
      } else {
        this.$http.post('/Primary/Fractions/UploadFractions', { filepath: this.filePath }).then((resJson) => {
          this.loading = false
          console.log(resJson)
          alert(resJson)
        })
      }
    },
    getDataList() {
      this.selectedRowKeys = []

      this.loading = true
      this.$http
        .post('/Primary/Fractions/GetDataList', {
          PageIndex: this.pagination.current,
          PageRows: this.pagination.pageSize,
          SortField: this.sorter.field || 'Id',
          SortType: this.sorter.order,
          Search: this.queryParam,
          ...this.filters,
        })
        .then((resJson) => {
          this.loading = false
          this.data = resJson.Data
          const pagination = { ...this.pagination }
          pagination.total = resJson.Total
          this.pagination = pagination
        })
    },
    onSelectChange(selectedRowKeys) {
      this.selectedRowKeys = selectedRowKeys
    },
    hasSelected() {
      return this.selectedRowKeys.length > 0
    },
    hanldleAdd() {
      this.$refs.editForm.openForm()
    },
    handleEdit(id) {
      this.$refs.editForm.openForm(id)
    },
    handleDelete(ids) {
      var thisObj = this
      this.$confirm({
        title: '确认删除吗?',
        onOk() {
          return new Promise((resolve, reject) => {
            thisObj.$http.post('/Primary/Fractions/DeleteData', ids).then((resJson) => {
              resolve()

              if (resJson.Success) {
                thisObj.$message.success('操作成功!')

                thisObj.getDataList()
              } else {
                thisObj.$message.error(resJson.Msg)
              }
            })
          })
        },
      })
    },
  },
}
</script>