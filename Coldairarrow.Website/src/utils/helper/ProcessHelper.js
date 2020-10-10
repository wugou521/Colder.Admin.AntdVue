const productKey = 'production'
const ProcessHelper = {
    isProduction() {
        debugger;
        return process.env.NODE_ENV == productKey
    },
    isPreview() {
        debugger;
        return process.env.VUE_APP_PREVIEW === 'true'
    }
}
export default ProcessHelper