import { useState } from "react";
import heroImg from "./assets/hero.png";
import reactLogo from "./assets/react.svg";
import viteLogo from "./assets/vite.svg";
import "./App.css";
import { useEffect, useRef } from "react";

function App() {
  const [count, setCount] = useState(0);

  let val = useRef(0); //initialize vlaue zero de di hi  and x ayah pr ek object hi ...
  //ab iske inske ander data ko insert krna hi to wo apne .current field ke ander krenge...

  function handleIncrement() {
    val.current = val.current + 1;
    console.log("Value of val :", val.current);
    setCount(count + 1);
  }

  useEffect(() => {
    console.log("Main firse render ho gya hu ");
  });

  let btnRef = useRef();
  function changeColor() {
    btnRef.current.style.backgroundColor = "red";
  }
  return (
    <div>
      <h1> Hello jee....</h1>
      <button ref={btnRef} onClick={handleIncrement}>
        Increment
      </button>

      <button onClick={changeColor}>Change the color of 1st button</button>

      <div>Count:{count}</div>
    </div>
  );
}

export default App;
