import API from '../api/api.js'

const login = async (loginValues) => {
    const response = await API.post('Auth/Login', loginValues)

    console.log(response.data.token)
}

export default{
    login,

}