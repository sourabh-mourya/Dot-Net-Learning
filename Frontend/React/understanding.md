# React Notes

## Table of Contents

1. [Components aur Props](#components-aur-props-nextjs-se-pehle)
2. [Hooks aur useState](#hooks-aur-usestate-in-depth)
3. [useEffect](#useeffect)
4. [useRef](#useref-hook)
5. [useMemo](#usememo)
6. [Event Handling](#event-handling)
7. [Conditional Rendering](#conditional-rendering-in-react)
8. [List Rendering](#list-rendering)
9. [Forms: Controlled Components](#forms-controlled-components)
10. [React Routing](#react-routing)
11. [Fetch & Axios](#fetch--axios)

---

# Components aur Props (Next.js se pehle)

## 1. Component kya hota hai?

Component ek **JavaScript function** hai jo **JSX return karta hai** (JSX = HTML jaisa dikhne wala code). Ye UI ka ek reusable tukda hota hai.

Jaise Lego blocks: ek baar block banao (Navbar, Card, Button, Footer), phir jitni baar chaho use karo. Poora page bhi in components se milkar bana hota hai.

```jsx
function Navbar() {
  return (
    <nav>
      <a href="/">Home</a>
      <a href="/about">About</a>
    </nav>
  );
}
```

Ab `<Navbar />` likhte hi ye kahin bhi use ho jayega.

**Rules:**

- Component ka naam **Capital letter** se shuru hona chahiye (`UserCard`, `userCard` nahi). Lowercase hoga to React usko normal HTML tag samjhega.
- Function ko **kuch return karna zaroori** hai (JSX ya `null`).
- Return mein **ek hi parent element** hona chahiye (div ya koi bhi container). Extra div nahi chahiye to Fragment use karo: `<> ... </>`

---

## 2. JSX kya hai?

JSX = **JavaScript XML**. Isme HTML jaisa code JavaScript ke andar likhte hain, aur `{ }` ke andar koi bhi JS expression chala sakte hain.

```jsx
const name = "Sourabh";
return (
  <h1>
    Hello {name}, 2 + 2 = {2 + 2}
  </h1>
);
```

**HTML se differences:**

| HTML          | JSX                                    |
| ------------- | -------------------------------------- |
| `class="box"` | `className="box"`                      |
| `<img>`       | `<img />` (har tag close hona chahiye) |
| `onclick`     | `onClick`                              |
| `for`         | `htmlFor`                              |

---

## 3. Basic UserCard component

```jsx
import React from "react";
import "./UserCard.css";

export default function UserCard() {
  return (
    <div className="user-container">
      <p id="user-name">Love Babbar</p>
      <img id="user-img" src="image-url-here" alt="love" />
      <p id="user-desc">Description of Love Babbar......</p>
    </div>
  );
}
```

- Poora code **ek single container** (div) ke andar hota hai.
- `<img ... ></img>` bhi chalta hai, lekin standard tareeka self-closing hai: `<img ... />`

---

## 4. Props kya hote hain?

**Problem:** `UserCard` banaya, lekin har card mein alag data chahiye (alag naam, photo, description). Data andar hardcode kiya to har card same dikhega.

**Solution: props** (properties). Props se parent component, child component ko data bhejta hai. Ye function ke arguments jaisa hi hai.

**Bhejna (parent mein):**

```jsx
<UserCard name="User Card - 1" />
<UserCard name="User Card - 2" />
<UserCard name="User Card - 3" />
```

**Receive karna (child mein):**

```jsx
export default function UserCard(props) {
  return (
    <div className="user-container">
      <p id="user-name">{props.name}</p>
    </div>
  );
}
```

`props` ek **object** hai jisme wo saara data aa jata hai jo bheja tha:

```js
{
  name: "User Card - 1";
}
```

---

## 5. Destructuring (practically sabse zyada use hota hai)

Har baar `props.name` likhna lamba hai, isliye direct destructure karte hain:

```jsx
function UserCard({ name, desc, img }) {
  return (
    <div className="user-container">
      <p>{name}</p>
      <img src={img} alt={name} />
      <p>{desc}</p>
    </div>
  );
}
```

---

## 6. Props mein sirf string nahi, kuch bhi bhej sakte ho

String ke alawa baaki sab `{ }` ke andar jata hai:

```jsx
<UserCard
  name="Sourabh" // string
  age={21} // number
  isActive={true} // boolean
  skills={["Node", "React"]} // array
  address={{ city: "Indore" }} // object
  onClick={handleClick} // function
/>
```

---

## 7. Important rule: Props read-only hote hain

- Child component props ko **change nahi kar sakta**.
- Data hamesha **parent se child ki taraf** (one-way) flow hota hai.
- Data badalna ho to wo **state** (`useState`) ka kaam hai, jo alag topic hai.

---

## 8. Real use: map se multiple cards

Asli projects mein cards manually nahi likhte, data array se generate karte hain:

```jsx
const users = [
  { id: 1, name: "Love Babbar", desc: "DSA Teacher" },
  { id: 2, name: "Striver", desc: "Coding Mentor" },
];

export default function App() {
  return (
    <div>
      {users.map((user) => (
        <UserCard key={user.id} name={user.name} desc={user.desc} />
      ))}
    </div>
  );
}
```

`key` unique hona chahiye. React isse list items ko track karta hai.

---

## 9. Bonus: `children` prop

Component ke opening aur closing tag ke beech jo likhte ho, wo `children` ke roop mein milta hai:

```jsx
function Card({ children }) {
  return <div className="card">{children}</div>;
}

<Card>
  <h2>Title</h2>
  <p>Kuch bhi yahan</p>
</Card>;
```

Next.js mein **layouts** isi concept par chalte hain.

---

## 10. Next.js se connection

- Next.js mein **har page bhi ek component hi hai** (`page.js` ka default export).
- `layout.js` ek component hai jo `children` leta hai.
- App Router mein components by default **Server Components** hote hain. `useState`, `onClick` jaisi browser wali cheezein chahiye to file ke top par `"use client"` likhna padta hai.
- Props ka concept bilkul same hai, koi change nahi.

---

# Hooks aur useState (in depth)

## 1. Hook kya hota hai?

**Hook** ek special React function hai jiska naam `use` se shuru hota hai. Isse function component ke andar React ki features (state, lifecycle, context, etc.) "hook" kar sakte ho.

Pehle ye features sirf class components mein milti thi. **React 16.8** ke baad hooks aaye, taaki simple function component mein bhi state aur lifecycle use kar sako.

> **Correction:** Tumhare notes mein likha hai "pehle class component pe hooks chalta tha". Ye ulta hai. **Hooks sirf function components mein chalte hain, class components mein nahi.** Class mein `this.state` aur `this.setState` hota tha, hooks ne usko replace kiya.

**Common hooks:**

| Hook                      | Kaam                                                  |
| ------------------------- | ----------------------------------------------------- |
| `useState`                | Component ka data (state) rakhna                      |
| `useEffect`               | Side effects: API call, timer, event listener         |
| `useRef`                  | DOM element ya value ko re-render ke bina store karna |
| `useContext`              | Global data share karna (props drilling avoid)        |
| `useMemo` / `useCallback` | Performance optimization                              |
| `useReducer`              | Complex state logic                                   |

---

## 2. Rules of Hooks (yaad rakhna)

1. Hooks sirf **function component** ya **custom hook** ke andar call karo.
2. Hooks hamesha **top level** par call karo. `if`, `for`, nested function ke andar nahi.
3. Hook ka naam `use` se shuru hona chahiye (custom hook ke liye bhi).

```jsx
// GALAT
function App() {
  if (true) {
    const [x, setX] = useState(0); // condition ke andar
  }
}

// SAHI
function App() {
  const [x, setX] = useState(0);
}
```

---

## 3. State kya hoti hai?

State = component ki **memory**. Aisa data jo **time ke saath badal sakta hai**, aur jab badle to **UI automatically update** ho.

Normal variable kyun kaam nahi karta?

```jsx
function Counter() {
  let count = 0;

  return (
    <button
      onClick={() => {
        count = count + 1;
        console.log(count);
      }}
    >
      {count}
    </button>
  );
}
```

Console mein count badhega, lekin **screen par 0 hi dikhega**, kyunki:

1. Normal variable change hone par React ko pata nahi chalta, re-render nahi hota.
2. Re-render hone par function dobara chalta hai aur `let count = 0` phir se reset ho jata hai.

`useState` dono problem solve karta hai: value **yaad rakhta hai** aur change hone par **re-render trigger karta hai**.

---

## 4. useState ka syntax

```jsx
import { useState } from "react";

const [count, setCount] = useState(0);
```

| Part          | Matlab                               |
| ------------- | ------------------------------------ |
| `count`       | Current value (state variable)       |
| `setCount`    | Value badalne wala function (setter) |
| `useState(0)` | Initial value `0`                    |

Ye **array destructuring** hai. `useState` ek array return karta hai: `[value, setterFunction]`. Naam tum kuch bhi rakh sakte ho, convention hai `[something, setSomething]`.

> **Note:** Tumne notes mein `setcount` likha tha aur `setCount` bhi. JavaScript **case-sensitive** hai, dono alag hain. Hamesha camelCase: `setCount`.

**Example: poora Counter**

```jsx
import { useState } from "react";

export default function Counter() {
  const [count, setCount] = useState(0);

  return (
    <div>
      <h1>Count: {count}</h1>
      <button onClick={() => setCount(count + 1)}>+</button>
      <button onClick={() => setCount(count - 1)}>-</button>
      <button onClick={() => setCount(13)}>Set 13</button>
      <button onClick={() => setCount(0)}>Reset</button>
    </div>
  );
}
```

`setCount(13)` chalate hi: React `count` ko 13 karta hai, component **dobara render** hota hai, aur UI mein 13 dikhta hai.

---

## 5. State kaise kaam karti hai (behind the scenes)

1. Component pehli baar render hota hai, `count = 0`.
2. User button dabata hai, `setCount(1)` call hota hai.
3. React nayi value store karta hai aur component function ko **dobara chalata hai**.
4. Is baar `useState(0)` initial value ignore karta hai aur stored value `1` deta hai.
5. UI nayi value ke saath update ho jata hai.

**Do important baatein:**

### a) State update asynchronous / batched hota hai

```jsx
setCount(count + 1);
console.log(count); // purani value dikhegi, nayi nahi
```

`setCount` ke turant baad `count` abhi purani value hi hota hai. Nayi value agle render mein milti hai.

### b) Previous value par depend ho to functional update use karo

```jsx
// Problem: teeno baar same purani value se calculate hota hai
setCount(count + 1);
setCount(count + 1);
setCount(count + 1); // result: sirf +1

// Solution: previous state par kaam karo
setCount((prev) => prev + 1);
setCount((prev) => prev + 1);
setCount((prev) => prev + 1); // result: +3
```

**Rule:** Nayi value purani value par depend karti hai to `setX(prev => ...)` use karo.

---

## 6. Different types ki state

### String (input field)

```jsx
const [name, setName] = useState("");

<input value={name} onChange={(e) => setName(e.target.value)} />
<p>Hello {name}</p>
```

Isko **controlled input** kehte hain: input ki value React state se control hoti hai.

### Boolean (toggle)

```jsx
const [isOpen, setIsOpen] = useState(false);

<button onClick={() => setIsOpen(!isOpen)}>Toggle</button>;
{
  isOpen && <p>Menu khula hai</p>;
}
```

### Object

```jsx
const [user, setUser] = useState({ name: "Sourabh", age: 21 });

// GALAT: direct mutate
user.age = 22;

// SAHI: naya object banao (spread operator)
setUser({ ...user, age: 22 });
// ya
setUser((prev) => ({ ...prev, age: 22 }));
```

### Array

```jsx
const [items, setItems] = useState([]);

// Add
setItems([...items, newItem]);

// Remove (id se)
setItems(items.filter((item) => item.id !== id));

// Update (id se)
setItems(
  items.map((item) => (item.id === id ? { ...item, done: true } : item)),
);
```

> **Golden rule: State ko kabhi directly mutate mat karo** (`push`, `splice`, `obj.key = ...`). Hamesha **nayi copy** banao aur setter ko do. Warna React ko change detect nahi hota aur UI update nahi hoti.

---

## 7. Lazy initial state (optional, performance)

Initial value calculate karna heavy ho to function pass karo, wo sirf pehli baar chalega:

```jsx
const [data, setData] = useState(() => {
  const saved = localStorage.getItem("data");
  return saved ? JSON.parse(saved) : [];
});
```

---

## 8. State vs Props

|                   | Props            | State                        |
| ----------------- | ---------------- | ---------------------------- |
| Kahan se aata hai | Parent se        | Component ke andar           |
| Badal sakte ho?   | Nahi (read-only) | Haan (setter se)             |
| Kiske liye        | Data bhejna      | Data yaad rakhna aur badalna |

Parent ki state ko child ko **props** ke roop mein de sakte ho:

```jsx
function Parent() {
  const [count, setCount] = useState(0);
  return <Child count={count} onIncrease={() => setCount(count + 1)} />;
}

function Child({ count, onIncrease }) {
  return <button onClick={onIncrease}>Count: {count}</button>;
}
```

Isko **lifting state up** kehte hain.

---

## 9. Database / API se data kaise laate hain? (Real-world)

Ab main part. Database se data frontend mein seedha nahi aata. Flow ye hota hai:

```
React (frontend)  --HTTP request-->  Backend (Node/Express)  -->  Database (MongoDB/MySQL)
React (frontend)  <--JSON response--  Backend (Node/Express)  <--  Database
```

Frontend **backend API** ko call karta hai, API database se data nikal kar JSON bhejti hai, aur hum wo JSON **state mein store** karte hain taaki UI mein dikh sake.

Iske liye **`useState` + `useEffect`** dono chahiye:

- `useEffect`: component load hote hi API call karne ke liye
- `useState`: aaya hua data (aur loading/error) store karne ke liye

### 9.1 `useEffect` ka basic idea

```jsx
useEffect(() => {
  // ye code component render hone ke BAAD chalega
}, [dependencies]);
```

| Dependency array  | Kab chalega                                |
| ----------------- | ------------------------------------------ |
| `[]` (khali)      | Sirf **ek baar**, component mount hone par |
| `[id]`            | Mount par + jab bhi `id` badle             |
| (kuch nahi likha) | Har render ke baad (usually avoid karo)    |

### 9.2 GET: data fetch karke dikhana

Backend (Express) maan lo ye route de raha hai: `GET /api/users` jo MongoDB se users ka array bhejta hai.

```jsx
import { useState, useEffect } from "react";

export default function UserList() {
  const [users, setUsers] = useState([]); // data
  const [loading, setLoading] = useState(true); // loading state
  const [error, setError] = useState(null); // error state

  useEffect(() => {
    const fetchUsers = async () => {
      try {
        const res = await fetch("http://localhost:5000/api/users");

        if (!res.ok) {
          throw new Error("Server error: " + res.status);
        }

        const data = await res.json();
        setUsers(data); // DB ka data state mein
      } catch (err) {
        setError(err.message);
      } finally {
        setLoading(false); // chahe success ho ya fail
      }
    };

    fetchUsers();
  }, []); // [] = sirf pehli baar

  if (loading) return <p>Loading...</p>;
  if (error) return <p>Error: {error}</p>;

  return (
    <div>
      {users.map((user) => (
        <UserCard key={user._id} name={user.name} desc={user.email} />
      ))}
    </div>
  );
}
```

**Teen state kyun?** Real API call mein 3 situations hoti hain, aur UI ko teeno handle karni chahiye:

| State            | Matlab                | UI mein kya dikhaye    |
| ---------------- | --------------------- | ---------------------- |
| `loading = true` | Data abhi aa raha hai | Spinner / "Loading..." |
| `error != null`  | Request fail hui      | Error message          |
| `data`           | Data aa gaya          | Actual list            |

> **Dhyan do:** `useEffect` ka callback directly `async` nahi bana sakte. Isliye andar alag `async function` banao aur usko call karo.

> MongoDB mein id ka field `_id` hota hai, MySQL mein usually `id`. Apne backend ke hisaab se `key` set karo.

### 9.3 Axios ke saath (aur clean)

```bash
npm install axios
```

```jsx
import axios from "axios";

useEffect(() => {
  const fetchUsers = async () => {
    try {
      const res = await axios.get("http://localhost:5000/api/users");
      setUsers(res.data); // axios mein JSON already res.data mein hota hai
    } catch (err) {
      setError(err.response?.data?.message || err.message);
    } finally {
      setLoading(false);
    }
  };
  fetchUsers();
}, []);
```

Axios mein `res.json()` ki zaroorat nahi, aur non-2xx status par apne aap error throw karta hai.

### 9.4 Cleanup: unmount hone par request cancel karna

Component hat jaye aur request abhi chal rahi ho to state set karne se warning/bug aa sakta hai. `AbortController` se cancel karo:

```jsx
useEffect(() => {
  const controller = new AbortController();

  const fetchUsers = async () => {
    try {
      const res = await fetch("/api/users", { signal: controller.signal });
      const data = await res.json();
      setUsers(data);
    } catch (err) {
      if (err.name !== "AbortError") setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  fetchUsers();

  return () => controller.abort(); // cleanup
}, []);
```

### 9.5 Dependency ke saath fetch (e.g. id ya search se)

```jsx
function UserProfile({ userId }) {
  const [user, setUser] = useState(null);

  useEffect(() => {
    fetch(`/api/users/${userId}`)
      .then((res) => res.json())
      .then((data) => setUser(data));
  }, [userId]); // userId badla to dobara fetch

  if (!user) return <p>Loading...</p>;
  return <h2>{user.name}</h2>;
}
```

### 9.6 POST: form se data database mein bhejna

```jsx
function AddUser() {
  const [form, setForm] = useState({ name: "", email: "" });
  const [submitting, setSubmitting] = useState(false);

  // ek hi handler se saare inputs
  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault(); // page reload roko
    setSubmitting(true);
    try {
      const res = await fetch("http://localhost:5000/api/users", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(form),
      });
      if (!res.ok) throw new Error("Failed to add user");
      setForm({ name: "", email: "" }); // form reset
      alert("User added!");
    } catch (err) {
      alert(err.message);
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <input
        name="name"
        value={form.name}
        onChange={handleChange}
        placeholder="Name"
      />
      <input
        name="email"
        value={form.email}
        onChange={handleChange}
        placeholder="Email"
      />
      <button type="submit" disabled={submitting}>
        {submitting ? "Adding..." : "Add User"}
      </button>
    </form>
  );
}
```

`[e.target.name]: e.target.value` ko **computed property name** kehte hain. Input ke `name` attribute ke hisaab se sahi key update hoti hai.

### 9.7 DELETE aur UPDATE: UI aur DB dono sync rakhna

Delete ke baad poora list dobara fetch karna zaroori nahi. Backend success de to **state se bhi hata do**:

```jsx
const handleDelete = async (id) => {
  try {
    const res = await fetch(`/api/users/${id}`, { method: "DELETE" });
    if (!res.ok) throw new Error("Delete failed");
    setUsers((prev) => prev.filter((u) => u._id !== id)); // UI se hatao
  } catch (err) {
    setError(err.message);
  }
};

const handleUpdate = async (id, updatedData) => {
  const res = await fetch(`/api/users/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(updatedData),
  });
  const updatedUser = await res.json();
  setUsers((prev) => prev.map((u) => (u._id === id ? updatedUser : u)));
};
```

**Pattern:** Backend pehle confirm kare, phir state update karo. Isse UI aur database hamesha match karte hain.

### 9.8 JWT token ke saath protected API (tumhare stack jaisa)

```jsx
const token = localStorage.getItem("token");

const res = await fetch("http://localhost:5000/api/users", {
  headers: {
    Authorization: `Bearer ${token}`,
  },
});
```

Axios mein har request par token lagane ke liye **interceptor** ya `axios.create({ baseURL, headers })` use karte hain.

### 9.9 Custom hook: `useFetch` (reuse karne ke liye)

Har component mein loading/error/data wala same code likhna repeat hota hai. Isko ek **custom hook** mein daal do:

```jsx
// hooks/useFetch.js
import { useState, useEffect } from "react";

export default function useFetch(url) {
  const [data, setData] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const controller = new AbortController();

    const getData = async () => {
      try {
        setLoading(true);
        const res = await fetch(url, { signal: controller.signal });
        if (!res.ok) throw new Error("Error: " + res.status);
        setData(await res.json());
        setError(null);
      } catch (err) {
        if (err.name !== "AbortError") setError(err.message);
      } finally {
        setLoading(false);
      }
    };

    getData();
    return () => controller.abort();
  }, [url]);

  return { data, loading, error };
}
```

Use:

```jsx
function UserList() {
  const { data: users, loading, error } = useFetch("/api/users");

  if (loading) return <p>Loading...</p>;
  if (error) return <p>{error}</p>;

  return users.map((u) => <UserCard key={u._id} name={u.name} />);
}
```

---

## 10. Common galtiyan

1. **State directly mutate karna**: `users.push(x)` ki jagah `setUsers([...users, x])`.
2. **`useEffect` mein dependency array bhoolna**: infinite loop ban jata hai (fetch, setState, re-render, fetch, ...). Hamesha `[]` ya sahi dependencies do.
3. **`useState` ke baad turant nayi value expect karna**: nayi value agle render mein milti hai.
4. **Loading/error handle na karna**: `users.map` chalane se pehle `users` array hona chahiye, warna `Cannot read properties of null`. Isliye initial state `[]` rakho.
5. **Hook condition/loop ke andar call karna**.
6. **Objects/arrays ke liye spread bhoolna**, aur poora object overwrite kar dena.
7. **CORS error**: backend Express mein `cors` middleware lagao (`app.use(cors())`), warna browser request block karega.

---

## 11. Next.js mein kya alag hai?

- `useState`, `useEffect` jaise hooks sirf **Client Components** mein chalte hain. File ke top par `"use client"` likhna padta hai.
- Next.js (App Router) mein **Server Components** mein tum seedha `async` component bana kar data fetch kar sakte ho, bina `useState`/`useEffect` ke:

```jsx
// app/users/page.js (Server Component, "use client" nahi)
export default async function UsersPage() {
  const res = await fetch("http://localhost:5000/api/users", {
    cache: "no-store",
  });
  const users = await res.json();

  return (
    <div>
      {users.map((u) => (
        <p key={u._id}>{u.name}</p>
      ))}
    </div>
  );
}
```

- Interactive cheezein (buttons, forms, counters, search box) ke liye **Client Component** + `useState`.
- Pattern: **data fetching server par, interactivity client par.**

Isliye pehle React mein `useState` + `useEffect` achhe se samajhna zaroori hai.

---

# useEffect

`useEffect` ek React Hook hai jisse hum **side effects** handle karte hain.

**Side effect kya hota hai?** Aisa kaam jo component ke render ke bahar ki duniya se interact karta hai:

- API call (data fetch karna)
- Timer (`setTimeout`, `setInterval`)
- Event listener lagana (`window.addEventListener`)
- `document.title` badalna
- `localStorage` read/write karna

> **Rule:** Render ke andar ka code "pure" hona chahiye (sirf JSX calculate kare). Side effects render ke andar nahi, `useEffect` ke andar likhte hain. `useEffect` ka callback component render hone ke **baad** (screen update hone ke baad) chalta hai.

## useEffect ka basic syntax

```jsx
import { useEffect } from "react";

function App() {
  useEffect(() => {
    console.log("Component Rendered");
  });

  return (
    <>
      <h1>Hello React</h1>
    </>
  );
}

export default App;
```

Yahan `useEffect()` component render hone ke baad execute hota hai.

## useEffect ke 3 sbse important cases

### 1. Without Dependency Array

```jsx
useEffect(() => {
  console.log("Component Rendered");
});
```

- First render par execute hoga.
- Har subsequent re-render par bhi execute hoga.
- Usually isse avoid karte hain, kyunki ye zyada chalta hai aur setState ke saath infinite loop bana sakta hai.

### 2. Empty dependency array `[]`

```jsx
useEffect(() => {
  console.log("Component Mounted");
}, []);
```

- Sirf initial mount par execute hota hai.
- Normal re-renders par dobara execute nahi hota.
- API call (page load par data lana) ke liye sabse common.

### 3. With Dependency Array

```jsx
const [count, setCount] = useState(0);

useEffect(() => {
  console.log("Count Updated:", count);
}, [count]);
```

- Initial render par execute hoga.
- Jab bhi count change hoga, effect dobara execute hoga.
- Kisi unrelated state ke change hone par execute nahi hoga.

**Dependency array mein kya daalna hai?** Effect ke andar jo bhi state, props ya variable use ho raha hai, wo dependency array mein hona chahiye. Isse effect hamesha latest value ke saath chalta hai.

| Dependency array | Kab chalega                       |
| ---------------- | --------------------------------- |
| nahi likha       | Har render ke baad                |
| `[]`             | Sirf mount par (ek baar)          |
| `[a, b]`         | Mount par + jab `a` ya `b` badle  |

## Cleanup function

Effect kuch "chalu" kar raha ho (timer, listener, subscription), to use band karna padta hai. Iske liye effect se ek **function return** karte hain:

```jsx
useEffect(() => {
  const id = setInterval(() => {
    console.log("tick");
  }, 1000);

  return () => {
    clearInterval(id); // cleanup
  };
}, []);
```

Cleanup function tab chalta hai:

1. Jab component **unmount** (screen se hat) ho.
2. Jab effect **dobara chalne se pehle** (dependency badli ho).

Cleanup na likhne par memory leak aur duplicate listeners/timers ban sakte hain.

> **Note (StrictMode):** Development mein `<StrictMode>` ke wajah se effect **do baar** chalta hai (mount, unmount, mount). Ye bug nahi hai, React cleanup sahi likhe hone ki jaanch kar raha hai. Production build mein effect sirf ek baar chalta hai.

## Common galti: infinite loop

```jsx
// GALAT: setState effect ke andar, dependency array nahi
useEffect(() => {
  setCount(count + 1); // render, effect, setState, render, effect, ...
});

// SAHI: dependency sahi do
useEffect(() => {
  setCount(10);
}, []);
```

---

# useRef Hook

`useRef` ek aisa hook hai jo ek **object** return karta hai, jiske andar `.current` property hoti hai:

```jsx
const x = useRef(0);
// x = { current: 0 }
```

Is `.current` mein hum data store aur update karte hain.

**useRef ke 2 main use cases:**

1. **DOM elements ko directly access karna** (jaise input par focus karna).
2. **Aisi value store karna jo re-renders ke beech yaad rahe, lekin badalne par component re-render na kare.**

## Value "persist" hone ka matlab

Normal variable (`let`) ke saath problem: component ek function hai, aur har re-render par function **dobara chalta hai**, isliye variable wapas apni initial value se start hota hai.

```jsx
function App() {
  let count = 0; // har render par dobara 0 se shuru
}
```

Agar value ko re-renders ke beech **yaad rakhna** hai (persist karna hai), to `useRef` use karte hain. React same ref object ko har render mein wapas deta hai, isliye `.current` ki value bani rehti hai. Aur `.current` badalne par re-render **trigger nahi hota**.

## Use case 1: DOM access

```jsx
import { useRef } from "react";

function App() {
  const inputRef = useRef(null);

  const focusInput = () => {
    inputRef.current.focus(); // input element par seedha focus
  };

  return (
    <>
      <input ref={inputRef} type="text" />
      <button onClick={focusInput}>Focus Input</button>
    </>
  );
}
```

`ref={inputRef}` lagane par React us DOM element ko `inputRef.current` mein daal deta hai.

## Use case 2: Re-render ke bina value store karna

Timer ID, previous value, render count jaisi cheezein jo UI mein dikhani nahi hain, bas yaad rakhni hain:

```jsx
const timerRef = useRef(null);

const start = () => {
  timerRef.current = setInterval(() => console.log("running"), 1000);
};

const stop = () => {
  clearInterval(timerRef.current);
};
```

## 2. useRef vs useState: Difference samjho

Ye sabse important comparison hai.

Suppose hume ek counter banana hai.

**Using useState:**

```jsx
const [count, setCount] = useState(0);

const increase = () => {
  setCount(count + 1);
};
```

Jab `setCount()` call hoga:

- State update hogi.
- React component re-render hoga.
- UI updated count show karega.

**Using useRef:**

```jsx
const countRef = useRef(0);

const increase = () => {
  countRef.current++;
  console.log(countRef.current);
};
```

Jab `countRef.current++` hoga:

- Ref ki value update hogi.
- Component re-render nahi hoga.
- UI automatically update nahi hoga (console mein nayi value dikhegi, screen par purani hi rahegi jab tak koi aur cheez re-render na kare).

| | Normal variable | `useState` | `useRef` |
| --- | --- | --- | --- |
| Re-render par value yaad rehti hai? | Nahi (reset) | Haan | Haan |
| Badalne par re-render hota hai? | Nahi | Haan | Nahi |
| Kab use karein | Temporary calculation | UI mein dikhne wala data | DOM access, timer id, UI se unrelated value |

> **Rule:** Jo value screen par dikhani hai, wo `useState` mein rakho. Jo sirf yaad rakhni hai, wo `useRef` mein. Render ke dauraan `ref.current` ko likho/badlo mat, wo event handler ya `useEffect` ke andar karo.

---

# useMemo

React mein `useMemo()` ek performance optimization Hook hai. Iska use hum kisi expensive calculation ke result ko cache (memorize) karne ke liye karte hain, taaki unnecessary re-renders ke time same calculation baar-baar execute na ho.

Simple language mein:

Suppose tumhare component mein ek calculation hai jo execute hone mein zyada time leti hai. Component kisi unrelated reason se re-render hota hai, toh normally calculation dobara execute hogi.

## 1. useMemo ka syntax

```jsx
import { useMemo } from "react";

const memoizedValue = useMemo(() => {
  return expensiveCalculation();
}, [dependencies]);
```

Ismein 3 important things hain:

1. Callback function: Jisme calculation perform hoti hai.
2. Return value: Calculation ka result.
3. Dependency array: Jiske change hone par calculation dobara execute hoti hai.

## 2. Without useMemo: Problem samjho

Suppose hum ek counter bana rahe hain aur saath mein ek expensive calculation bhi perform kar rahe hain.

```jsx
import { useState } from "react";

function App() {
  const [count, setCount] = useState(0);
  const [number, setNumber] = useState(5);

  function expensiveCalculation(num) {
    console.log("Calculation Running...");

    let result = 0;

    for (let i = 0; i < 100000000; i++) {
      result += num;
    }

    return result;
  }

  const result = expensiveCalculation(number);

  return (
    <>
      <h1>Count: {count}</h1>

      <button onClick={() => setCount(count + 1)}>Increase Count</button>

      <h2>Result: {result}</h2>
    </>
  );
}

export default App;
```

**Problem kya hai?**

Yahan humne do states banayi hain:

- `count`: Counter increase karne ke liye.
- `number`: Calculation ke liye.

Ab jab tum Increase Count button click karoge:

1. count update hoga.
2. Component re-render hoga.
3. `expensiveCalculation(number)` dobara execute hoga.

Lekin dhyan do, number change hi nahi hua!

Phir bhi calculation dobara execute ho rahi hai. Ye unnecessary computation hai.

## 3. With useMemo: Solution

Ab isi code mein `useMemo()` implement karte hain.

```jsx
import { useState, useMemo } from "react";

function App() {
  const [count, setCount] = useState(0);
  const [number, setNumber] = useState(5);

  function expensiveCalculation(num) {
    console.log("Calculation Running...");

    let result = 0;

    for (let i = 0; i < 100000000; i++) {
      result += num;
    }

    return result;
  }

  const result = useMemo(() => {
    return expensiveCalculation(number);
  }, [number]);

  return (
    <>
      <h1>Count: {count}</h1>

      <button onClick={() => setCount(count + 1)}>Increase Count</button>

      <h2>Result: {result}</h2>
    </>
  );
}

export default App;
```

Ab kya hoga?

- Initial render: Calculation execute hogi.
- count change: Calculation execute nahi hogi.
- number change: Calculation dobara execute hogi.

## 4. Dhyan rakhne wali baatein

- `useMemo` ek **value** return karta hai (calculation ka result) aur render ke **dauraan** chalta hai. `useEffect` side effect ke liye hai aur render ke **baad** chalta hai. Dono alag kaam ke hain.
- `useMemo` sirf **optimization** hai. Har jagah mat lagao, chhoti calculation par ulta overhead badh sakta hai. Pehle problem (lag/slow) dikhe, tab use karo.
- Function ko memoize karna ho to `useCallback` use hota hai (`useCallback(fn, deps)` ek tarah se `useMemo(() => fn, deps)` jaisa hi hai).
- Child component ka unnecessary re-render rokna ho to `React.memo` ke saath `useMemo`/`useCallback` combine karte hain.

---

# Event Handling

React mein bhi HTML jaise bahut saare event handlers milte hain: `onClick`, `onChange`, `onSubmit`, `onKeyDown`, `onMouseEnter`, etc. React mein inke naam **camelCase** mein hote hain (`onclick` nahi, `onClick`).

Event handler mein hume ek **function** dena padta hai, function ka **call** nahi.

## Galti: Immediate Invocation

```jsx
// GALAT
<button onClick={alert("Button click me hua hi")}>Click</button>
```

Yahan `alert(...)` ko hum ne **call** kar diya. JSX `{ }` ke andar jo expression hota hai wo render ke time hi evaluate ho jata hai, isliye button click karne se pehle hi page render hote hi alert aa jayega. Isko **immediate invocation** kehte hain.

## Sahi tareeka

Arrow function ke through likhte hain, taaki function click hone par chale:

```jsx
// SAHI: arrow function
<button onClick={() => alert("Button click me hua hi")}>Click</button>
```

Ya alag function bana kar uska **reference** do (bina `()` ke):

```jsx
function handleClick() {
  alert("Button click me hua hi");
}

<button onClick={handleClick}>Click</button>; // SAHI (handleClick, handleClick() nahi)
```

> **Rule:** `onClick={handleClick}` (reference) ya `onClick={() => handleClick()}` (arrow function). Kabhi bhi `onClick={handleClick()}` nahi likhna, ye direct call ho jata hai.

## Argument bhejna ho to

```jsx
function handleDelete(id) {
  console.log("Delete:", id);
}

<button onClick={() => handleDelete(5)}>Delete</button>;
```

## Event object

Handler ko React automatically **event object** deta hai:

```jsx
function handleChange(e) {
  console.log(e.target.value); // input ki value
}

<input onChange={handleChange} />;
```

`e.preventDefault()` se default behavior roka jata hai (jaise form submit par page reload).

---

# Conditional Rendering in react

Condition ke hisaab se alag UI dikhana. 4 tareeke hain:

1. if-else
2. ternary operator
3. Logical operator (`&&`)
4. Early return

## 1. if-else

```jsx
if (isLoggedIn) {
  return <LogoutBtn />;
} else {
  return <LoginBtn />;
}
```

JSX ke andar seedha `if` nahi likh sakte, isliye return se pehle likhte hain.

## 2. Ternary operator (JSX ke andar sabse common)

```jsx
<div>{isLoggedIn ? <LogoutBtn /> : <LoginBtn />}</div>
```

## 3. Logical operator `&&` (jab else wala case nahi chahiye)

```jsx
{
  isLoggedIn && <p>Welcome back!</p>;
}
```

Condition `true` hai to element dikhega, `false` hai to kuch nahi.

> **Dhyan do:** `&&` ke left mein number mat rakho. `{count && <p>Items</p>}` mein agar `count = 0` hai to screen par `0` dikh jayega. Aise case mein `{count > 0 && <p>Items</p>}` likho.

## 4. Early return

```jsx
function Profile({ user }) {
  if (!user) return <p>Loading...</p>; // condition pehle hi handle karke bahar

  return <h2>{user.name}</h2>;
}
```

Loading/error jaise cases mein ye clean rehta hai.

## Poora example

```jsx
import { useState } from "react";
import heroImg from "./assets/hero.png";
import reactLogo from "./assets/react.svg";
import viteLogo from "./assets/vite.svg";
import "./App.css";
import LogoutBtn from "./components/LogoutBtn";
import LoginBtn from "./components/LoginBtn";

function App() {
  const [isLoggedIn, setLoggedIn] = useState(true);

  //if lese case - login btn show or logout btn show..

  // if(isLoggedIn){

  // return <LogoutBtn/>

  // }
  // else
  // {
  // return <LoginBtn/>
  // }

  return (
    <>
      <div>{isLoggedIn ? <LogoutBtn /> : <LoginBtn />}</div>
    </>
  );
}

export default App;
```

---

# List Rendering

React mein hum generally JavaScript ke `map()` method ka use karke lists render karte hain.

Suppose tumhare paas 5 employees ka data hai aur tumhe sabhi employees ke names UI par show karne hain.

Kya tum 5 baar manually `<h2>` likhoge?

Nahi! Hum `map()` use karenge.

## 2. React mein map() ka use kaise karte hain?

Ab maan lo tumhare paas students ki list hai.

```jsx
const students = ["Sourabh", "Rahul", "Aman", "Rohit"];
```

**without map :-**

```jsx
<h2>{students[0]}</h2>
<h2>{students[1]}</h2>
<h2>{students[2]}</h2>
<h2>{students[3]}</h2>
```

**with map :-**

```jsx
{students.map((student) => {
  return <h2 key={student}>{student}</h2>;
})}
```

`map()` array ke har item par function chalata hai aur har baar jo return hota hai (yahan JSX), un sab ka **naya array** banata hai. React us array ko screen par render kar deta hai.

## `key` prop (zaroori)

List ke har item ko ek **unique `key`** deni padti hai, taaki React ko pata chale kaun sa item add, remove ya change hua.

```jsx
const employees = [
  { id: 1, name: "Rahul" },
  { id: 2, name: "Aman" },
];

{employees.map((emp) => (
  <h2 key={emp.id}>{emp.name}</h2>
))}
```

- `key` **unique** aur **stable** honi chahiye (database ki `id` best hai).
- Index (`key={index}`) tabhi use karo jab list kabhi reorder/add/delete na hoti ho, warna bugs aa sakte hain.
- `key` `map` ke andar ke **sabse bahar wale element** par lagti hai.

## filter + map

```jsx
{employees
  .filter((emp) => emp.id > 1)
  .map((emp) => (
    <h2 key={emp.id}>{emp.name}</h2>
  ))}
```

---

# Forms: Controlled Components

iska basically ye mtlb hi ki jb apne form bana rhe hoge to uski jo bhi value change hona ye sb apni state ke saath hi manage hona chiye

means jo bhi chiz krenge wo sb state ke saath hi manage hoge

Input ki value ka "single source of truth" React state hoti hai: `value={name}` state se aati hai, aur `onChange` mein state update hoti hai. Isliye input hamesha state ke saath sync rehta hai.

```jsx
import { useState } from "react";

function App() {
  const [name, setName] = useState("");

  function handleChange(event) {
    setName(event.target.value);
  }

  return (
    <>
      <h1>React Controlled Component</h1>

      <input
        type="text"
        placeholder="Enter your name"
        value={name}
        onChange={handleChange}
      />

      <h2>Your Name: {name}</h2>
    </>
  );
}

export default App;
```

> **Dhyan do:** Agar `value` laga diya lekin `onChange` nahi diya, to input type hi nahi hoga (read-only ho jata hai), kyunki value React state se control ho rahi hai.

## Form submit

```jsx
function handleSubmit(e) {
  e.preventDefault(); // page reload roko
  console.log("Submitted:", name);
}

<form onSubmit={handleSubmit}>
  <input value={name} onChange={handleChange} />
  <button type="submit">Submit</button>
</form>;
```

## Multiple inputs ek hi state se

```jsx
const [form, setForm] = useState({ name: "", email: "" });

const handleChange = (e) => {
  setForm({ ...form, [e.target.name]: e.target.value });
};

<input name="name" value={form.name} onChange={handleChange} />
<input name="email" value={form.email} onChange={handleChange} />
```

Input ka `name` attribute aur state ki key same honi chahiye.

## Checkbox ke liye

Checkbox mein `value` nahi, `checked` use hota hai aur `e.target.checked` padhte hain:

```jsx
const [agree, setAgree] = useState(false);

<input
  type="checkbox"
  checked={agree}
  onChange={(e) => setAgree(e.target.checked)}
/>;
```

## Controlled vs Uncontrolled

| | Controlled | Uncontrolled |
| --- | --- | --- |
| Value kaun rakhta hai | React state | DOM khud |
| Value kaise padhte hain | State variable se | `useRef` se (`ref.current.value`) |
| Kab use karein | Validation, live preview, zyadatar forms | Simple/file input |

---

# React Routing

React mein React Router ek library hai jiska use hum application mein multiple pages/routes ke beech navigation karne ke liye karte hain, without completely reloading the browser page.

Suppose tum ek application bana rahe ho jisme multiple pages hain:

- Home Page
- About Page
- Contact Page
- Login Page
- Dashboard Page
- Employee Page

Ab jab user Home se About page par click kare, toh hume About page display karna hai.

Iske liye React Router use karte hain.

> React actually **Single Page Application (SPA)** hota hai: browser mein sirf ek `index.html` load hoti hai. React Router URL badalta hai aur URL ke hisaab se alag component dikhata hai, naya page server se nahi aata.

## 1. React Router ki need kyun hoti hai?

Agar tumhe multiple pages display karne hain, toh manually conditions manage karni pad sakti hain

```jsx
function App() {
  const [page, setPage] = useState("home");

  return (
    <>
      {page === "home" && <Home />}
      {page === "about" && <About />}
      {page === "contact" && <Contact />}
    </>
  );
}
```

Is tareeke mein URL nahi badalta, browser ka back button kaam nahi karta, aur kisi page ka link share nahi kar sakte.

React Router is problem ko solve karta hai.

Iski help se hum URL ke according components render kar sakte hain.

```bash
npm install react-router
```

Saare component bana lo: Home page, About page, Contact page ye sb ke ...

```jsx
import { Routes, Route } from "react-router";

import Home from "./pages/Home";
import About from "./pages/About";
import Contact from "./pages/Contact";

function App() {
  return (
    <Routes>
      <Route path="/" element={<Home />} />

      <Route path="/about" element={<About />} />

      <Route path="/contact" element={<Contact />} />
    </Routes>
  );
}

export default App;
```

Ab `BrowserRouter` config karna padega `main.jsx` file ke andar

```jsx
import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import { BrowserRouter } from "react-router";

import App from "./App.jsx";
import "./index.css";

createRoot(document.getElementById("root")).render(
  <StrictMode>
    <BrowserRouter>
      <App />
    </BrowserRouter>
  </StrictMode>,
);
```

## 2. Navigation: `Link` aur `NavLink`

Pages ke beech jaane ke liye `<a>` ki jagah `Link` use karo. `<a href>` poora page reload kar deta hai, `Link` nahi karta.

```jsx
import { Link, NavLink } from "react-router";

<nav>
  <Link to="/">Home</Link>
  <Link to="/about">About</Link>
  <NavLink to="/contact">Contact</NavLink>
</nav>;
```

`NavLink` current page ke link par automatically `active` class laga deta hai (navbar highlight ke liye).

## 3. Code se navigate karna: `useNavigate`

Login ke baad dashboard par bhejna jaise cases mein:

```jsx
import { useNavigate } from "react-router";

function Login() {
  const navigate = useNavigate();

  const handleLogin = () => {
    // login logic...
    navigate("/dashboard");
  };

  return <button onClick={handleLogin}>Login</button>;
}
```

## 4. Dynamic route: `useParams`

```jsx
<Route path="/employee/:id" element={<Employee />} />
```

```jsx
import { useParams } from "react-router";

function Employee() {
  const { id } = useParams(); // URL /employee/5 -> id = "5"
  return <h2>Employee ID: {id}</h2>;
}
```

`useParams` hamesha **string** return karta hai.

## 5. 404 page

```jsx
<Route path="*" element={<NotFound />} />
```

`*` un sab URLs ko pakadta hai jo upar ke kisi route se match nahi hue.

## 6. Nested routes aur `Outlet` (layout ke liye)

```jsx
import { Outlet } from "react-router";

function DashboardLayout() {
  return (
    <>
      <Sidebar />
      <Outlet /> {/* yahan child route render hoga */}
    </>
  );
}

<Route path="/dashboard" element={<DashboardLayout />}>
  <Route index element={<DashboardHome />} />
  <Route path="profile" element={<Profile />} />
</Route>;
```

---

# Fetch & Axios

`fetch` aur `axios` basically backend se data fetch karne (API call karne) ke liye use hote hain.

- `fetch` browser ka **built-in** hai, kuch install nahi karna padta.
- `axios` ek **external library** hai (`npm install axios`).

## Fetch ki khaas baatein

- `fetch` ka response aane ke baad data nikalne ke liye `.json()` karna padta hai (ye bhi ek promise hai, isliye `await`).
- Default method **GET** hota hai. GET ke liye method bhejna zaroori nahi, lekin **POST, PUT, DELETE** mein `method` batani padti hai, saath mein `headers` aur `body: JSON.stringify(...)` bhi.
- **Dhyan do:** `fetch` 404/500 jaise error status par **error throw nahi karta**, wo sirf network fail hone par throw karta hai. Isliye `res.ok` khud check karna padta hai.

```jsx
// GET
const res = await fetch("http://localhost:5000/api/users");
if (!res.ok) throw new Error("Server error: " + res.status);
const data = await res.json();

// POST
const res2 = await fetch("http://localhost:5000/api/users", {
  method: "POST",
  headers: { "Content-Type": "application/json" },
  body: JSON.stringify({ name: "Sourabh" }),
});
const created = await res2.json();
```

## Axios ki khaas baatein

- Method seedha function ka naam hota hai: `axios.get`, `axios.post`, `axios.put`, `axios.delete`.
- `.json()` nahi karna padta, data seedha `res.data` mein milta hai.
- Object ko khud `JSON.stringify` nahi karna, axios automatically kar deta hai.
- Non-2xx status (404, 500) par **automatically error throw** karta hai.

```jsx
import axios from "axios";

// GET
const res = await axios.get("http://localhost:5000/api/users");
console.log(res.data);

// POST
const res2 = await axios.post("http://localhost:5000/api/users", {
  name: "Sourabh",
});
```

## Axios instance

Har baar poora URL aur headers likhne ki jagah ek **instance** bana lete hain:

```jsx
// api/axiosInstance.js
import axios from "axios";

const axiosInstance = axios.create({
  baseURL: "http://localhost:5000/api",
});

export default axiosInstance;
```

```jsx
axiosInstance.get("/users");
axiosInstance.post("/users", { name: "Sourabh" });
```

yese krke apne bhej dete hi, URL mein sirf baaki path likhna padta hai.

## Interceptor (har request par JWT token lagana)

```jsx
axiosInstance.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});
```

## Fetch vs Axios

| | `fetch` | `axios` |
| --- | --- | --- |
| Install | Built-in | `npm install axios` |
| JSON nikalna | `await res.json()` | `res.data` |
| Body bhejna | `JSON.stringify` + headers | Object seedha |
| HTTP error (404/500) | Throw nahi karta, `res.ok` check karo | Automatically throw |
| Base URL / interceptor | Manual | `axios.create`, interceptors |