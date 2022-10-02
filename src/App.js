import React, { Component } from 'react';
import './App.css';
import Home from "./Components/Home";
import Trends from "./Components/Trends";
import Sidebar from './Components/Sidebar';



class App extends Component {

  render() {
    return (
      <div className="row align-items-start">
      <Sidebar/>
      <Home/>
      <Trends/>
      
    </div>
    )
  }
}

export default App;