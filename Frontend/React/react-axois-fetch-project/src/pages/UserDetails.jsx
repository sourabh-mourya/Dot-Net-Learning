import { useEffect, useState } from "react";
import { useParams, Link } from "react-router";
import api from "../api/axiosInstance";

function UserDetails() {
  const { id } = useParams();

  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    const getUser = async () => {
      try {
        const response = await api.get(`/users/${id}`);

        setUser(response.data);
      } catch (error) {
        setError(error.message);
      } finally {
        setLoading(false);
      }
    };

    getUser();
  }, [id]);

  if (loading) return <h2>Loading...</h2>;

  if (error) return <h2>Error: {error}</h2>;

  if (!user) return <h2>User not found</h2>;

  return (
    <div className="container">
      <h1>User Details</h1>

      <div className="user-card">
        <img src={user.image} alt={user.firstName} />

        <h2>
          {user.firstName} {user.lastName}
        </h2>

        <p>Email: {user.email}</p>
        <p>Phone: {user.phone}</p>
        <p>Age: {user.age}</p>
        <p>Gender: {user.gender}</p>
        <p>Company: {user.company?.name}</p>

        <Link to="/users">Back to Users</Link>
      </div>
    </div>
  );
}

export default UserDetails;