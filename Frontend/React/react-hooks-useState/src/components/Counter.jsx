import React, { useState } from "react";

export default function Counter() {
  const [count, setcount] = useState(0);
  return (
    <div className="counter-container">
      <p id="para">you have clicked {count} times</p>
      <button
        id="btn"
        onClick={() => {
          setcount(count + 1);
        }}//jasie button pr click krunga wo ye function ko excute kr de ur uske ander likha hi 
        // setCoubt(count+1 ) kr do ... mtlb count ko set kr do..
      >
        Click Me
      </button>
    </div>
  );
}
