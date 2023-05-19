import React, { useContext } from 'react'
import Login from './components/Login/Login.jsx'
import Register from './components/Register/Register.jsx'
import { Route, Routes, Link, useNavigate } from 'react-router-dom'
import Dashboard from './components/Dashboard/Dashboard.jsx'
import AuthContext from './store/authContext.jsx'

function App() {  
  const navigator = useNavigate();
  const context = useContext(AuthContext);
  const token = context.token;
  const role = context.type;

  return (
    <React.Fragment>
      <Routes>
        <Route path='/' element={!role ? <Login/> : navigator("/dashboard")}/>
        <Route path='/register' element={!role ? <Register/> : navigator("/dashboard")}/>
        <Route path="/dashboard" element={role ? <Dashboard userType={role} /> : <h1>You don't have access to this page, please <Link to="/">login</Link> first!</h1>}/>
      </Routes>
    </React.Fragment>
  )
}

export default App