import React, { Component } from 'react';
import './App.css';
import Home from './Components/Home';


import {createBrowserRouter,RouterProvider} from "react-router-dom";

import Login from './Components/Login/Login';
import Register from './Components/Login/Register';




const router = createBrowserRouter([
  {
    path: "/",
    element: <Home/>
  },
  {
    path: "login",
    element: <Login/>
  },
  {
    path: "register",
    element: <Register/>
    
  },
])


class App extends Component {

  render() { 
    return (
      <RouterProvider router={ router } />
    )
  }
}

export default App;