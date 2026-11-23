import { useState } from "react";
import heroImg from "./assets/hero.png";
import reactLogo from "./assets/react.svg";
import viteLogo from "./assets/vite.svg";
import "./App.css";

function App() {
  const [count, setCount] = useState(0);


    function expensiveTask(num){
      console.log("Inside Expensive Task..")
      //ye empty loop bana diya jiske karan ye bahut time lega to wo to expensive ho gya...
      for(let i=0;i<1000000000;i++){}
      return num*2;
    }


   let doubleValue = expensiveTask(4);


  return (
    <>
      <div>
        <h1> Hello world....</h1>
        <br></br>

        <button  onClick={()=>setCount(count+1)}>

          Count:{count}
        </button>
      </div>
      <div>
        Double:{doubleValue}
      </div>
    </>
  );
}

export default App;
