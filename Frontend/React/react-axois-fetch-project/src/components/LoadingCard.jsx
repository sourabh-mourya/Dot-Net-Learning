import "./cards.css";

function LoadingCard() {
  return (
    <div className="user-card skeleton-card">
      <div className="avatar skeleton" />
      <div className="skeleton line w-60" />
      <div className="skeleton line w-80" />
      <div className="skeleton line w-30" />
    </div>
  );
}

export default LoadingCard;
