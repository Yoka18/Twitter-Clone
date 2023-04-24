import axios from 'axios';
import React, { Component } from 'react'
import TwitterConsumer from '../../context';

export default class Tweet extends Component {


    onDeleteTweet = async (dispatch,e) =>{
        const {id} = this.props;

        await axios.delete(`http://localhost:3001/tweets/${id}`)


        dispatch({type: "DELETE_TWEET",payload:id});
    }

    onInter = (e) =>{
        const {share} = this.props;
        this.setState({share:share+1});
        console.log(share);
    }


  render() {

    const {username,desc,image,comments,retweet,like,share,time} = this.props;

    return (
        <TwitterConsumer>
            {
                value => {
                    const {dispatch} = value;

                    return (
                        <div className='twit'>
                        <div className='card'>
                            <div className='row g-8'>
                                <br />
                                <div className='col-auto'>
                                    <img src="https://i.redd.it/oq8e1utnl0651.jpg" className="border-rad" alt="..." />
                                </div>
                                <div className='col'>
                                    <div class="card-body">
                                        <span class="card-title fw-bold"><a className="card-title" href="#">{username}</a></span><span className="card-subtitle mb-2 text-muted">@{username}</span><span className="card-subtitle mb-2 text-muted"> {time}h</span>
                                        
                                        <span className='float-end post-info dropdown'><i className="fa-solid fa-circle-info"></i>
                                            <div className='dropdown-content'>
                                                <br />
                                                <p onClick={this.onDeleteTweet.bind(this,dispatch)}><i class="fa-regular fa-trash-can"></i>Delete Tweet</p>
                                            </div>
                                        </span>
                
                                        <p className="card-text">{desc}</p>
                                        <img src={image} className="card-img-bottom rounded-4" alt=""/>
                                    </div>
                
                
                                    <div className="interection">
                                        <div className="inter d-inline p-3">
                                            <span className="icon">
                                                <i className="fa-regular fa-comment"></i>
                                            </span>
                                            <span className="cul">
                                                {comments}
                                            </span>
                                        </div>
                                        <div className="inter d-inline p-3">
                                            <span className="icon">
                                                <i className="fa-solid fa-retweet"></i>
                                            </span>
                                            <span className="cul">
                                                {retweet}
                                            </span>
                                        </div>
                                        <div className="inter d-inline p-3">
                                            <span className="icon">
                                                <i className="fa-regular fa-heart"></i>
                                            </span>
                                            <span className="cul">
                                                {like}
                                            </span>
                                        </div>
                                        <div onClick={this.onInter.bind(this,share)} className="inter d-inline p-3">
                                            <span className="icon">
                                                <i className="fa-solid fa-arrow-up-from-bracket"></i>
                                            </span>
                                            <span className="cul">
                                                {share}
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    )
                }
            }
        </TwitterConsumer>
    )



  }
}
