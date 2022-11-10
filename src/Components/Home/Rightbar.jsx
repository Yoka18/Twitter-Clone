import React, { Component } from 'react'

export default class Trends extends Component {
  render() {
    return (
      <div className='col bkgr'>
        <br/>
          <div className="search">
            <input className="search-bar" type="text" placeholder="Search Twitter"/>
          </div>


          <br/>
          <div className="trends rounded-4">
            <h5>Trends For You</h5>
            <div className="cal-4">
              <div className="trend">
                <span className="card-subtitle mb-2 text-muted">Trending in Turkey</span>
                <div><span>Trend</span></div>
                <span className="card-subtitle mb-2 text-muted">21K Tweets</span>
              </div>
            </div>
            <br/>
          </div>
          <br/>


          <div className="follows rounded-4">
            <h5>Who to follow</h5>
            <div className="cal-4">
              <div className="follow">
                <span className="card-subtitle mb-2 text-muted">Trending in Turkey</span>
                <div><span>Trend</span></div>
                <span className="card-subtitle mb-2 text-muted">21K Tweets</span>
              </div>
            </div>
            <br/>
          </div>

      </div>
    )
  }
}
