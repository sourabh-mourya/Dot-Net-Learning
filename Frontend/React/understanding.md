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

## UseEffects

useEffects ye react hook jisse me ek side effect generate kr skta hu ...

useEffect() ek Hook hai, jiska use hum component ke render hone ke baad kuch specific tasks perform karne ke liye karte hain.

Jab component render ho jaye aur hume uske baad koi operation perform karna ho, tab hum useEffect() use karte hain.

# useEffect ka basic syntax...

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

Yahan useEffect() component render hone ke baad execute hota hai.

# useEffect ke 3 sbse important cases...

1. without Dependency Array.

useEffect(() => {
console.log("Component Rendered");
});

- First render par execute hoga.
- Har subsequent re-render par bhi execute hoga.

2. Empty dependency array []
   useEffect(() => {
   console.log("Component Mounted");
   }, []);

- Sirf initial mount par execute hota hai.
- Normal re-renders par dobara execute nahi hota.

3. With Dependency Array
   const [count, setCount] = useState(0);

useEffect(() => {
console.log("Count Updated:", count);
}, [count]);

- Initial render par execute hoga.
- Jab bhi count change hoga, effect dobara execute hoga.
- Kisi unrelated state ke change hone par execute nahi hoga.

## useRef Hooks... Explain

first case : jaha apne ko useRef ko used krna hi ...

yesa variable jaha pr apne ko value presists rkhi across the re-render

value presists ka mtlb apne ko varialbe bandye hi and wo baar baar re0redner whi se value start kr rha hi se apne ne usko intailize kra tha ye hi presists

agar varialbe ko apne persists rkhna hi to apne useRef ka used krenge...

let x = useRef()

to ye ek object ko return krta hi...

iss x ke ander .current ke ander apne data ko insert kr rhe hote hi ur access kr rhe hote hi ...

useRef se apne yesa varialbe bana dete ki value re-render ho ya na ho but usko value persisits jaura kregi....

basically useRef ko isliye banaya gya tha ki apne DOM me se element ko directly access kr ske...

1. DOM elements ko directly access karna.
2. Aisi values store karna jinhe update karne par component re-render na ho.

Yahan:

- useRef(0) ek ref object create karta hai.
- .current us object ki property hai.
- Is property mein hum value store ya update kar sakte hain.

2. useRef vs useState – Difference samjho

Ye sabse important comparison hai.

Suppose hume ek counter banana hai.

Using useState:
const [count, setCount] = useState(0);

const increase = () => {
setCount(count + 1);
};

Jab setCount() call hoga:

- State update hogi.
- React component re-render hoga.
- UI updated count show karega.
  Using useRef:
  const countRef = useRef(0);

const increase = () => {
countRef.current++;
console.log(countRef.current);
};

Jab countRef.current++ hoga:

- Ref ki value update hogi.
- Component re-render nahi hoga.
- UI automatically update nahi hoga.

## useMemo ....

React mein useMemo() ek performance optimization Hook hai. Iska use hum kisi expensive calculation ke result ko cache (memorize) karne ke liye karte hain, taaki unnecessary re-renders ke time same calculation baar-baar execute na ho.

Simple language mein:

Suppose tumhare component mein ek calculation hai jo execute hone mein zyada time leti hai. Component kisi unrelated reason se re-render hota hai, toh normally calculation dobara execute hogi.

1. useMemo ka syntax

   import { useMemo } from "react";

const memoizedValue = useMemo(() => {
return expensiveCalculation();
}, [dependencies]);

Ismein 3 important things hain:

1. Callback function: Jisme calculation perform hoti hai.
2. Return value: Calculation ka result.
3. Dependency array: Jiske change hone par calculation dobara execute hoti hai.
4. Without useMemo – Problem samjho
   Suppose hum ek counter bana rahe hain aur saath mein ek expensive calculation bhi perform kar rahe hain.

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

      <button onClick={() => setCount(count + 1)}>
        Increase Count
      </button>

      <h2>Result: {result}</h2>
    </>

);
}

export default App;

Problem kya hai?

Yahan humne do states banayi hain:

- count – Counter increase karne ke liye.
- number – Calculation ke liye.
  Ab jab tum Increase Count button click karoge:

1. count update hoga.
2. Component re-render hoga.
3. expensiveCalculation(number) dobara execute hoga.
   Lekin dhyan do, number change hi nahi hua!

Phir bhi calculation dobara execute ho rahi hai. Ye unnecessary computation hai.

3. With useMemo – Solution
   Ab isi code mein useMemo() implement karte hain.

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

      <button onClick={() => setCount(count + 1)}>
        Increase Count
      </button>

      <h2>Result: {result}</h2>
    </>

);
}

export default App;

Ab kya hoga?

- Initial render: Calculation execute hogi.
- count change: Calculation execute nahi hogi.
- number change: Calculation dobara execute hogi.

## Event handling ......

apne yaha pr apne bahut saare event handlers milte hi.....

jb bhi apne ye event handleers lagte hi to apne ussme apne ko ek function ko fena padta hi.............

agar apne iss type se event handler likh dete hi to

<button onClick={alert("Button click me hua hi")}></button>

to kr dete agar buttone lcikc bhi nhi krege to bhi alreat nhi aayega wo page render pr hi aa jygea ..

isko hmm immediation invokation bolte hi...

to hme isko arrouw function ke through likhna chiye tb jakar wo chlega...

<button onClick={()=>alert("Button click me hua hi")}></button>

kbhi bhi apne ko direct immediate invokation nhi krni hi ....

## Conditional Rendering in react

1. if-else
2. tenerary operator
3. Logical operator
4. Early return se..

import { useState } from "react";
import heroImg from "./assets/hero.png";
import reactLogo from "./assets/react.svg";
import viteLogo from "./assets/vite.svg";
import "./App.css";
import LogoutBtn from "./components/LogoutBtn";
import LoginBtn from "./components/LoginBtn"
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

<diV>{isLoggedIn ? <LogoutBtn /> : <LoginBtn />}</diV>
</>
);
}

export default App;

# List Rederning

React mein hum generally JavaScript ke map() method ka use karke lists render karte hain.

Suppose tumhare paas 5 employees ka data hai aur tumhe sabhi employees ke names UI par show karne hain.

Kya tum 5 baar manually <h2> likhoge?

Nahi! Hum map() use karenge.

2. React mein map() ka use kaise karte hain?
   Ab maan lo tumhare paas students ki list hai.

const students = ["Sourabh", "Rahul", "Aman", "Rohit"];

without map :-

  <h2>{students[0]}</h2>
      <h2>{students[1]}</h2>
      <h2>{students[2]}</h2>
      <h2>{students[3]}</h2>

with map :-

{students.map((student) => {
return <h2>{student}</h2>;})}

## Forms Controlled components

iska basically ye mtlb hi ki jb apne form bana rhe hoge to uski jo bhi value change hona ye sb apni state ke saath hi manage hona chiye

means jo bhi chiz krenge wo sb state ke saath hi manage hoge

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

# React-routing ...

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

1. React Router ki need kyun hoti hai?

Agar tumhe multiple pages display karne hain, toh manually conditions manage karni pad sakti hain

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

React Router is problem ko solve karta hai.

Iski help se hum URL ke according components render kar sakte hain.

npm install react-router

saar component bana lege Home page About page Contact page ye sb ke ...

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

ab browserRouter Config krna padega main.jsx file ke ander

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
</StrictMode>
);


# Fetch & Axois

fetch and axois basically backend se data fetch krne ke liye used krte hi ...


fetch me apne data aa jane ke baad .json krna padta hi 

fetch me apne ko method bhi bhejni padti hi.....




axois me apne direct bhej dete hi 

axoisInstance.post
axoisInstance.get 

yese krke apne bhej dete hi ...

