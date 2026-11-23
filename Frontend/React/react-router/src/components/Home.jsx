import { useNavigate } from "react-router";

function Home() {
  const navigate = useNavigate();

  const goToAbout = () => {
    navigate("/about");
  };

  return (
    <>
      <h1>Home Page</h1>
      <button onClick={() => navigate("/about")}>Go to About</button>
      <button onClick={() => navigate("/contact")}>Contact Page</button>{" "}
    </>
  );
}

export default Home;
