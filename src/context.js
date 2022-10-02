import React, { Component } from 'react'

const TwitterContext = React.createContext();


const reducer = (state,action) => {
  switch(action.type){
    case "DELETE_TWEET":
      return{
        ...state,
        tweets: state.tweets.filter(tweet=>action.payload !== tweet.id)
      }
  }
}



export class TwitterProvider extends Component {

    state = {
        tweets:[
            {
                id: 1,
                username: "Test1",
                desc: "test 1 2 3 deneme test deneme işte bu denemedir dikkatinize",
                image: "https://i.redd.it/oq8e1utnl0651.jpg",
                comments: 2,
                retweet: 15,
                like: 100,
                share: 5,
                time: 9
            },
            {
                id: 2,
                username: "Test2",
                desc: "test 1 2 3 deneme test deneme işte bu denemedir dikkatinize",
                image: "https://i.redd.it/oq8e1utnl0651.jpg",
                comments: 22,
                retweet: 150,
                like: 20,
                share: 15,
                time: 95
            },
        ],
        dispatch:action => {
          this.setState(state => reducer(state,action))
        }
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