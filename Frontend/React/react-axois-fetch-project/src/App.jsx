import { Routes, Route } from "react-router";

import Navbar from "./components/Navbar";

import Home from "./pages/Home";
import Users from "./pages/Users";
import UserDetails from "./pages/UserDetails";
import AddUser from "./pages/AddUser";
import EditUser from "./pages/EditUser";
import NotFound from "./pages/NotFound";

function App() {
  return (
    <>
      <Navbar />

      <Routes>
        <Route path="/" element={<Home />} />

        <Route path="/users" element={<Users />} />

        <Route path="/users/add" element={<AddUser />} />

        <Route path="/users/:id" element={<UserDetails />} />

        <Route path="/users/edit/:id" element={<EditUser />} />

        <Route path="*" element={<NotFound />} />
      </Routes>
    </>
  );
}

export default App;