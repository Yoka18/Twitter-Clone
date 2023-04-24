import React, { Component } from 'react';
import Tweets from './Tweets';
import TwitterConsumer from '../../context';
import axios from 'axios';



export default class Home extends Component {

  state={
    id:"",
    username : "",
    desc : "",
    image : "",
    comments:"",
    retweet:"",
    like:"",
    share:"",
    time:""
  }



  changeDesc = (e)=> {
    this.setState({
      desc : e.target.value
    })
  }

  changeImage = (e) =>{
    this.setState({
      image : e.target.value
    })
  }


  addTweet = async (dispatch,e) => {
    e.preventDefault();
    const {desc,image} = this.state;
    var today = new Date(),
    todayTime = today.getHours() + ':' + today.getMinutes();
    const newTweet={
       username:"Test",
       desc:desc,
       image:image,
       time:todayTime,
       like:0,
       share:0,
       comments:0,
       retweet:0
    }
    const respoTweet = await axios.post("http://localhost:3001/tweets",newTweet);
    dispatch({type:"ADD_TWEET",payload:respoTweet.data});
    console.log("Tweet");
  }


  render() {

    const {tweets,username,desc,image,comments,retweet,like,share,time} = this.props;

    console.log();
    

    return  <TwitterConsumer>
      {
        value => {
          const {dispatch} = value;
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
                      <form onSubmit={this.addTweet.bind(this,dispatch)}>
                      <input type="text" placeholder="What's Happening?" value={desc} onChange={this.changeDesc}/>
                      <hr/>
                      <div>
                        <input type="text" id="a" value={image} onChange={this.changeImage} />
                        <span><label htmlFor="a"><i className="fa-solid fa-image"></i></label></span>
                        <span><i className="fa-solid fa-gift"></i></span>
                        <span><i className="fa-solid fa-chart-simple"></i></span>
                        <span><i className="fa-solid fa-face-smile"></i></span>
                        <span><i className="fa-solid fa-calendar-days"></i></span>
                        <span><i className="fa-solid fa-location-dot"></i></span>
                      </div>
                      <button type='submit' className='btn float-end fw-bolder tweet-button'>Tweet</button>
                      </form>
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
    </TwitterConsumer>
  }
}
