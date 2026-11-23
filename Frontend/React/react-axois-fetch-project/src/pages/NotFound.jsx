import { Link } from "react-router";

function NotFound() {
  return (
    <div className="container">
      <h1>404 - Page Not Found</h1>

      <Link to="/">Go to Home</Link>
    </div>
  );
}

export default NotFound;