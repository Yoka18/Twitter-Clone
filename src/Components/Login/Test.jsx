// import React, { Component } from 'react'
// import TwitterConsumer from '../../context';




// export default class Test extends Component {




//   render() {


    

//     return (
//         <TwitterConsumer>
//         {
//             value => {
//                 const {users,dispatch} = value;
//                 return (
//                     <div className="row align-items-start z-index-0">
//         <div className="col-md-7 login-background position-relative">
//           <img className="position-absolute top-50 start-50 translate-middle" height="290" width="360" src="/images/white twitter.png"/>
//         </div>
//         <div className="col px-4">
//             <div className="logo">
//                 <img className="twitter-logo" src="/images/twitter logo.png"/>
//             </div>
//             <div className="text-white login-screen-2 fw-bolder">
//                 <p>See what's happening in the world right now</p>
//             </div>
//             <div className="text-white login-screen-3 fw-bolder">
//                 <p>Join Twitter today.</p>
                
//             </div>
//             <div className="login-screen-4">
//                 <button className="btn btn-primary btn-lg btn-block fw-bolder"><i className="fa-brands fa-google"></i>Sign up with google</button>
//                 <button className="btn btn-primary btn-lg btn-block fw-bolder"><i className="fa-brands fa-apple"></i>Sign up with apple</button>
//                 <p className="hr-line">or</p>
//                 <button className="btn btn-primary btn-lg btn-block fw-bolder text-white" onClick={()=> setRegister(!register)}>Sign up with phone number or email</button>
//                 <p className="log-terms-policy">By signing up, you agree to the Terms of Service and Privacy Policy, including Cookie Use.</p>
//                 <br/>
//                 <br/>
//                 <h5 className="text-white">Already have an account?</h5>
//                 <br/>
//                 <a className="btn btn-outline-info login-button fw-bold" id="login-button" onClick={()=> setVisible(!visible)} >Login</a>
//             </div>
//         </div>

//         <div className="modal" id="modal" style={visible ? {display:'block'}:null}  tabIndex="-1">
//             <div className="modal-dialog modal-dialog-centered">
//               <div className="modal-content">
//                 <div className="modal-header">
//                     <i onClick={()=> setVisible(!visible)} className="fa-solid fa-xmark close"></i>
//                 </div>
//                 <div className="modal-body">
//                     <div className="login-modal">
//                         <h1>Log in to Twitter</h1>
//                         <br/>
//                         <form onSubmit={onLogin.bind(this,users)}>
//                             <div className="mb-3">
//                                 <input type="text" value={email} onChange={e=> setEmail(e.target.value)} className="pass-username" placeholder="Phone, email or username "/>
//                             </div>
//                             <div className="mb-3">
//                                 <input type="password" value={password} onChange={e=> setPass(e.target.value)} className="pass-username" placeholder="Password"/>
//                             </div>
//                             <button type="submit" className="btn btn-primary modal-login-button">Login</button>
//                             <br />
//                             <br />
//                             {
//                                 LogCheck ? <p>Please try again</p> : null
//                             }
//                         </form>
//                     </div>
//                 </div>
//                 <br/>
//               </div>
//             </div>
//     </div>


//     <div className="modal" id="modal" style={register ? {display:'block'}:null}  tabIndex="-1">
//             <div className="modal-dialog modal-dialog-centered">
//               <div className="modal-content">
//                 <div className="modal-header">
//                     <i onClick={()=> setRegister(!register)} className="fa-solid fa-xmark close"></i>
//                 </div>
//                 <div className="modal-body">
//                     <div className="login-modal">
//                         <h1>Create account</h1>
//                         <br/>
//                         <form onSubmit={onRegister.bind(this)}>
//                             <div className="mb-3">
//                                 <input type="text" value={RegName} onChange={e=> setRegName(e.target.value)} className="pass-username" placeholder="Username"/>
//                             </div>
//                             <div className="mb-3">
//                                 <input type="text" value={RegEmail} onChange={e=> setRegEmail(e.target.value)} className="pass-username" placeholder="Email"/>
//                             </div>
//                             <div>
//                                 <input type="password" value={RegPass} onChange={ e=> setRegPass(e.target.value)} className="pass-username" placeholder='Password' />
//                             </div>
//                             <div className='mb-3'>
//                                 <input type="date" name="" id="" className="pass-username" />
//                             </div>
//                             <button type="submit" className="btn btn-primary modal-login-button">Register</button>
//                             {
//                                 RegCheck ? <p>Please try again</p> : null
//                             }
//                         </form>
//                     </div>
//                 </div>
//                 <br/>
//               </div>
//             </div>
//     </div>

//           <footer class="log-footer">
//             <ul>
//               <li><a href="#">About</a></li>
//               <li><a href="#">Help Center</a></li>
//               <li><a href="#">Term Of Service</a></li>
//               <li><a href="#">Privacy Policy</a></li>
//               <li><a href="#">Cookie Policy</a></li>
//               <li><a href="#">Imprint</a></li>
//               <li><a href="#">Accessibility</a></li>
//               <li><a href="#">Advertising Information</a></li>
//               <li><a href="#">Blog</a></li>
//               <li><a href="#">Situation</a></li>
//               <li><a href="#">Career</a></li>
//               <li><a href="#">Brand Resources</a></li>
//               <li><a href="#">Advertisement</a></li>
//               <li><a href="#">Marketing</a></li>
//               <li><a href="#">Twitter For Businesses</a></li>
//               <li><a href="#">Developers</a></li>
//               <li><a href="#">Concordance</a></li>
//               <li><a href="#">Settings</a></li>
//               <li><a href="#">2022 Twitter, Inc</a></li>
//             </ul>
//           </footer>
//     </div>
//                 )
//             }

//         }
//     </TwitterConsumer>
//     )
//   }
// }
