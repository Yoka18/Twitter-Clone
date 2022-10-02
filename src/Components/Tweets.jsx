import React, { Component } from 'react';
import Tweet from './Tweet';
import TwitterConsumer from '../context'

class Tweets extends Component {
  render() {
    return(
      <TwitterConsumer>
        {
          value => {
            const {tweets} = value;
            return (
              <div>
                {
                    tweets.map( tweet =>{
                        return(
                            <Tweet
                                key = {tweet.id}
                                id = {tweet.id}
                                username = {tweet.username}
                                desc = {tweet.desc}
                                image = {tweet.image}
                                time  = {tweet.time}
                                share  = {tweet.share}
                                like = {tweet.like}
                                retweet = {tweet.retweet}
                                comments = {tweet.comments}
                            />
                        )
                    })
                }
              </div>
            )
          }
        }
      </TwitterConsumer>
    )
  }
}
export default Tweets;