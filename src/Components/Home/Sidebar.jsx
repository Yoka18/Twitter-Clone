import React from 'react'

export default function Sidebar() {
  return (
    <div className='col bkgr'>
        <div className='float-end'>
          <br/>
          <div className="list-group-item list-group-item-action">
            <a href="#" className='a-decor'>
              <span><i className="fa-solid fa-house fs-4"></i><h6 className="d-inline px-3 fs-4">Home</h6></span>
            </a>
          </div>
          <div className="list-group-item list-group-item-action">
            <a href="#" className='a-decor'>
              <span><i className="fa-solid fa-hashtag fs-4"></i><h6 className="d-inline px-3 fs-4">Explore</h6></span>
            </a>
          </div>
          <div className="list-group-item list-group-item-action">
            <a href="#" className='a-decor'>
              <span><i className="fa-solid fa-bell fs-4"></i><h6 className="d-inline px-3 fs-4">Notifications</h6></span>
            </a>
          </div>
          <div className="list-group-item list-group-item-action">
            <a href="#" className='a-decor'>
              <span><i className="fa-solid fa-envelope fs-4"></i><h6 className="d-inline px-3 fs-4">Messages</h6></span>
            </a>
          </div>
          <div className="list-group-item list-group-item-action">
            <a href="#" className='a-decor'>
              <span><i className="fa-solid fa-bookmark fs-4"></i><h6 className="d-inline px-3 fs-4">Bookmarks</h6></span>
            </a>
          </div>
          <div className="list-group-item list-group-item-action">
            <a href="#" className='a-decor'>
              <span><i className="fa-solid fa-table-list fs-4"></i><h6 className="d-inline px-3 fs-4">Lists</h6></span>
            </a>
          </div>
          <div className="list-group-item list-group-item-action">
            <a href="#" className='a-decor'>
              <span><i className="fa-solid fa-user fs-4"></i><h6 className="d-inline px-3 fs-4">Profile</h6></span>
            </a>
          </div>
          <div className="list-group-item list-group-item-action">
            <a href="#" className='a-decor'>
              <span><i className="fa-solid fa-circle-info fs-4"></i><h6 className="d-inline px-3 fs-4">More</h6></span>
            </a>
          </div>
          <br/>

          <a href="#" className="list-group-item btn-info twit-btn btn">Tweet</a>
          <br/>

        </div>
    </div>
  )
}
