import axios from 'axios';
import React, { Component } from 'react'

const TwitterContext = React.createContext();


const reducer = (state,action) => {
  switch(action.type){
    case "DELETE_TWEET":
      return{
        ...state,
        tweets: state.tweets.filter(tweet=>action.payload !== tweet.id)
      }
    case "ADD_TWEET":
      return{
        ...state,
        tweets: [...state.tweets,action.payload]
      }
  }
}



export class TwitterProvider extends Component {

    state = {
        tweets:[],
        dispatch:action => {
          this.setState(state => reducer(state,action))
        }
      }

      componentDidMount = async () => { 
        const tweets =  await axios.get("http://localhost:3001/tweets");
        this.setState({
          tweets: tweets.data
        })
       }


  render() {
    return (
      <TwitterContext.Provider value={this.state}>
        {this.props.children}
      </TwitterContext.Provider>
    )
  }
}

const TwitterConsumer = TwitterContext.Consumer;


export default TwitterConsumer;