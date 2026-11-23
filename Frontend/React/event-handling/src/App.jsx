import { useState } from "react";
import heroImg from "./assets/hero.png";
import reactLogo from "./assets/react.svg";
import viteLogo from "./assets/vite.svg";
import "./App.css";

function App() {
  const [count, setCount] = useState(0);

  function handleClick() {
    alert("I am clicked....");
  }

  function handleMouseOver() {
    alert("Mere upar mouse hover kiya hi ....");
  }

  function handleInputChange(e) {
    console.log("Value till now :-  ", e.target.value);
  }

  function handleSubmit(e){
    e.preventDefault();
    //I am writing my custom behaviour down....
    alert("Form submit kr du kya...")
  }
  return (
    <>
      <form onSubmit={handleSubmit} >
        <input type="text" onChange={(e) => handleInputChange(e)}></input>

        <button>Submit</button>
      </form>

      {/* <div>
        <h1> Hello world ........</h1>
      </div>
      <p onMouseOver={handleMouseOver} style={{ border: "1px solid black" }}>
        I am a para
      </p>
      <div>
        <button onClick={handleClick}>Click me</button>
      </div> */}
    </>
  );
}

export default App;
