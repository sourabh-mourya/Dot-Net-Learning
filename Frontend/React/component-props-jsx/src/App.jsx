import { useState } from "react";
import heroImg from "./assets/hero.png";
import reactLogo from "./assets/react.svg";
import viteLogo from "./assets/vite.svg";
import "./App.css";
import UserCard from "./components/UserCard";

function App() {
  const [count, setCount] = useState(0);

  return (
    <>
      <h1> Hello jee</h1>
      <UserCard name="User Card - 1" desc="decsription - 1" />
      <UserCard name="User Card - 2" desc="decsription - 2" />
      <UserCard name="User Card - 3" desc="decsription - 3" />
      // image ko apne iss trh sedn krege...
      {/* //      <UserCard name="User Card - 3"  desc='decsription - 3' image = /{imagekaNamm jo apne import krte wqt dege}/> */}
 
 
          









    
 
 
    </>
  );
}

export default App;
