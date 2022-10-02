import React, { Component } from 'react';
import Tweets from './Tweets';


export default class Home extends Component {




  render() {

    const {tweets} = this.props;
    console.log(tweets);

    const time = Date.now();


    const timeHour = Math.floor((time / (1000*60*60)) % 24); // Saat

    const timeMin = Math.floor ((time / (1000*60)) % 60); // Dakika

    var today = new Date(),
    todayTime = today.getHours() + ':' + today.getMinutes() + ':' + today.setSeconds();


    return (
      <div className='col bkgr'>
        <div className="post-tweet">
            <div className="tweet-home">
              <span className="fw-bolder">Home</span>
            </div>
            <div className="tweet-body">
              <div>
                <img src="https://i.redd.it/oq8e1utnl0651.jpg" alt="profile" className="rounded-circle float-start" width="50px" height="50px"/>
              </div>
              <div>
                <input type="text" placeholder="What's Happening?"/>
                <h1>{todayTime}</h1>
                <hr/>
                <div>
                  <span><i className="fa-solid fa-image"></i></span>
                  <span><i className="fa-solid fa-gift"></i></span>
                  <span><i className="fa-solid fa-chart-simple"></i></span>
                  <span><i className="fa-solid fa-face-smile"></i></span>
                  <span><i className="fa-solid fa-calendar-days"></i></span>
                  <span><i className="fa-solid fa-location-dot"></i></span>
                </div>
                <a href="#" className="btn float-end fw-bolder tweet-button">Tweet</a>
              </div>
            </div>
        </div>


        <Tweets 
            tweets = {tweets}
        />


      </div>
    )
  }
}
