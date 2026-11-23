import React from "react";
import "./UserCard.css";



export default function UserCard(props) {
  return (
    <div className="user-container">
      <p id="user-name">{props.name}</p>
      <img
        id="user-img"
        src="https://scontent-bom2-1.xx.fbcdn.net/v/t39.30808-6/461318528_1057199622436993_2429427774568686653_n.jpg?stp=dst-jpg_tt6&cstp=mx1080x1080&ctp=s1080x1080&_nc_cat=107&_nc_map=urlgen_bucketless&ccb=1-7&_nc_sid=a5f93a&_nc_ohc=exsx0RiqhXQQ7kNvwH13vdW&_nc_oc=AdqDW0cYuercYDO9zlu_ZtM3R_9I7VCKlJSBQilOvXAZWqOxivLVFJaj6LecgqSaYEA&_nc_zt=23&_nc_ht=scontent-bom2-1.xx&_nc_gid=oeNPP_2GJGn6CE80uWbAOg&_nc_ss=7b289&oh=00_AQTlG4TLDsrSOiIyTrE00hjF72pW2FHHu6bq45ihbNwf-Q&oe=6B060356"
        alt="love"
      ></img>
      <p id="user-desc">{props.desc}</p>
    </div>
  );
}
