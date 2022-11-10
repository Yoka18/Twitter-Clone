import React, { Component } from 'react'
import Main from './Home/Home';
import SideBar from './Home/Sidebar';
import RightBar from './Home/Rightbar';


export default class Homes extends Component {
  render() {
    return (
        <div className="row align-items-start">
            <SideBar/>
            <Main />
            <RightBar/>
        </div>
    )
  }
}
