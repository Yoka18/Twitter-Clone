import React, { Component } from 'react'
import TwitterConsumer from '../../context'


export default class Test extends Component {
  render() {
    return (
      <TwitterConsumer>
        {
            value => {
                const {users} = value;
                
                return (
                    <div>
                      <h1>{users}</h1>
                    </div>
                )
            }

        }
    </TwitterConsumer>
    )
  }
}
