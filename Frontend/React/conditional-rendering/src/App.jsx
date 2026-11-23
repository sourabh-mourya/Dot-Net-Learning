import { useState } from "react";
import heroImg from "./assets/hero.png";
import reactLogo from "./assets/react.svg";
import viteLogo from "./assets/vite.svg";
import "./App.css";
import LogoutBtn from "./components/LogoutBtn";
import LoginBtn from "./components/LoginBtn"
function App() {
  const [isLoggedIn, setLoggedIn] = useState(true);


  //if lese case  -   login btn show or logout btn  show..

  // if(isLoggedIn){

  //     return <LogoutBtn/>

  // }
  // else
  // {
  //     return <LoginBtn/>
  // }
  return (
    <>
      <diV>{isLoggedIn ? <LogoutBtn /> : <LoginBtn />}</diV>
    </>
  );
}

export default App;
