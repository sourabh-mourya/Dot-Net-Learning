import { NavLink } from "react-router";

function Navbar() {
  return (
    <nav>
      <h2>Employee Management</h2>

      <div>
        <NavLink to="/">Home</NavLink>

        <NavLink to="/users">Users</NavLink>

        <NavLink to="/users/add">Add User</NavLink>
      </div>
    </nav>
  );
}

export default Navbar;