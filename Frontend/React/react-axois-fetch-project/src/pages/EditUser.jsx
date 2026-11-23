import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router";
import api from "../api/axiosInstance";

function EditUser() {
  const { id } = useParams();
  const navigate = useNavigate();

  const [formData, setFormData] = useState({
    firstName: "",
    lastName: "",
    age: "",
    email: "",
  });

  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const getUser = async () => {
      try {
        const response = await api.get(`/users/${id}`);

        const user = response.data;

        setFormData({
          firstName: user.firstName,
          lastName: user.lastName,
          age: user.age,
          email: user.email,
        });
      } catch (error) {
        alert(error.message);
      } finally {
        setLoading(false);
      }
    };

    getUser();
  }, [id]);

  const handleChange = (e) => {
    const { name, value } = e.target;

    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await api.put(`/users/${id}`, {
        ...formData,
        age: Number(formData.age),
      });

      console.log("Updated User:", response.data);

      alert("User updated successfully!");

      navigate("/users");
    } catch (error) {
      alert(error.message);
    }
  };

  if (loading) return <h2>Loading User...</h2>;

  return (
    <div className="container">
      <h1>Edit User</h1>

      <form onSubmit={handleSubmit} className="user-form">
        <input
          type="text"
          name="firstName"
          value={formData.firstName}
          onChange={handleChange}
          placeholder="First Name"
          required
        />

        <input
          type="text"
          name="lastName"
          value={formData.lastName}
          onChange={handleChange}
          placeholder="Last Name"
          required
        />

        <input
          type="number"
          name="age"
          value={formData.age}
          onChange={handleChange}
          placeholder="Age"
          required
        />

        <input
          type="email"
          name="email"
          value={formData.email}
          onChange={handleChange}
          placeholder="Email"
          required
        />

        <button type="submit">Update User</button>
      </form>
    </div>
  );
}

export default EditUser;