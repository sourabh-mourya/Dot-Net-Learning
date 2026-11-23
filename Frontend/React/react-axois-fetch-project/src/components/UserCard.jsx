import { Link } from "react-router";
import "./cards.css";

function UserCard({ user, onDelete }) {
  return (
    <div className="user-card">
      <img
        className="avatar-img"
        src={user.image}
        alt={`${user.firstName} ${user.lastName}`}
      />

      <h3 className="user-name">
        {user.firstName} {user.lastName}
      </h3>
      <p className="user-email">{user.email}</p>

      <div className="user-meta">
        <span className="user-age">Age: {user.age}</span>
        <span className="user-role">{user.role}</span>
      </div>

      <div className="actions">
        <Link to={`/users/${user.id}`} className="action-btn view">
          View
        </Link>
        <Link to={`/users/edit/${user.id}`} className="action-btn edit">
          Edit
        </Link>
        <button
          className="action-btn delete"
          onClick={() => onDelete(user.id)}
        >
          Delete
        </button>
      </div>
    </div>
  );
}

export default UserCard;