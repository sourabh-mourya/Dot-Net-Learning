import { useState, useEffect } from "react";

function App() {
  // useEffect(()=>{
  //   console.log("Component Renderedd...")
  // })

  //   useEffect(()=>{
  //   console.log("Component Dependency mounted...")
  // },[])
  const [count, setCount] = useState(0);
  //jinti baar bhi count change hoga unti baar ye useEffect chlega...

  useEffect(() => {
    console.log("Count Updated:", count);
  }, [count]);

  return (
    <>
      <h1>Count: {count}</h1>

      <button onClick={() => setCount(count + 1)}>
        Increase
      </button>
    </>
  );
}

export default App;