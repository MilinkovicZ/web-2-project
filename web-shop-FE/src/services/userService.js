import API from '../api/api.js'

const register = async (registerValues) => {
    await API.post('Auth/Register', registerValues);
}

const getProfile = async () => {
    try {
      const token = localStorage.getItem('token');
      const config = {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      };    
      
      const response = await API.get('User/GetProfile', config);
      return response.data
    } catch (error) {
      console.log(error);
    }
  };

const editProfile = async (editProfileValues) => {
    const token = localStorage.getItem('token');
    const config = {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    };    
  
    const response = await API.put('User/EditProfile', editProfileValues, config);
}

export default{
    register,
    getProfile,
    editProfile
}