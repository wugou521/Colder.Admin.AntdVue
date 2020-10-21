const getters = {
    token: state => state.user.token,
    color: state => state.app.color,
    userInfo: state => state.user.info
}

export default getters
