import { useEffect, useState } from "react";
import { Link } from "react-router";
import api from "../api/axiosInstance";
import UserCard from "../components/UserCard";
import LoadingCard from "../components/LoadingCard";
import "../components/cards.css";

function Users() {
  const [users, setUsers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
   
  const getUsers = async () => {
    try {
      const response = await api.get("/users");
      setUsers(response.data.users || []);
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    getUsers();
  }, []);

  const deleteUser = async (id) => {
    const confirmDelete = window.confirm(
      "Are you sure you want to delete this user?"
    );

    if (!confirmDelete) return;

    try {
      await api.delete(`/users/${id}`);

      setUsers((previousUsers) =>
        previousUsers.filter((user) => user.id !== id)
      );

      alert("User deleted successfully!");
    } catch (err) {
      alert(err.message);
    }
  };

  if (error) return <h2>Error: {error}</h2>;

  return (
    <div className="container">
      <h1>All Users</h1>

      <Link to="/users/add" className="btn">
        Add New User
      </Link>

      <div className="cards-grid">
        {loading
          ? Array.from({ length: 12 }).map((_, i) => <LoadingCard key={i} />)
          : users.map((user) => (
              <UserCard key={user.id} user={user} onDelete={deleteUser} />
            ))}
      </div>
    </div>
  );
}

export default Users;