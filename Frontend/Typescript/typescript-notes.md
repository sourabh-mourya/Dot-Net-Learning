
---

## 1. TypeScript kya hai aur kyun?

**Definition:** TypeScript = JavaScript + **Types**. Ye Microsoft ne banaya hai. Browser/Node TypeScript direct nahi chalate — pehle ye **compile hoke JavaScript** banta hai.

```
 .ts file  --(tsc compiler)-->  .js file  --(Node / Browser)-->  Output
```

### JavaScript me problem

JS me variable/function ka type fix nahi hota:

```js
// JavaScript
let age = 20;
age = "twenty";   // JS ko koi problem nahi, error nahi dega
```

TypeScript me jab bhi kuch define karte ho, to **type bhi batana hota hai** ki wo kis type ka data accept karega.

```ts
let age: number = 20;
age = "twenty";   // ❌ ERROR: Type 'string' is not assignable to type 'number'
```

### Why TypeScript?

**1. Bugs development me hi pakde jaate hain (run karne se pehle)**
JS me chhote-mote bugs tab tak pata nahi chalte jab tak code **run** na karo. TS me **editor me hi red line** aa jaati hai, yani bug code likhte time hi pata chal jaata hai.

**2. Development speed badhti hai**
- Autocomplete (IntelliSense) achha milta hai — `user.` likhte hi saari properties dikh jaati hain.
- Refactoring safe hoti hai (naam badlo to saari jagah error dikhega).
- Dusre ka code padhna easy — type dekhke pata chal jaata hai function kya leta hai kya deta hai.

**3. Backend me bahut useful** (tumhare stack me — Express, API response, DB models — sabme types kaam aate hain).

### Important point

> **Jitna bhi JavaScript code hai wo sab valid TypeScript hai.**
> TS = JS ka **superset**. Matlab JS file ka naam `.ts` kar do, chalega (bas types add karte jao).

> ⚠️ Types sirf **compile time** pe hote hain. Compile ke baad JS me types **gayab** ho jaate hain. Runtime pe TS koi check nahi karta.

---

## 2. Setup & Run karna [EXTRA]

```bash
# TypeScript install (global)
npm install -g typescript

# version check
tsc -v

# ek file compile karna
tsc index.ts          # index.js ban jaayegi
node index.js         # run

# direct run karne ke liye (dev time)
npm install -g ts-node
ts-node index.ts

# project me tsconfig.json banane ke liye
tsc --init
```

### tsconfig.json (important options)

```json
{
  "compilerOptions": {
    "target": "ES2020",        // kis JS version me compile karna hai
    "module": "commonjs",      // Node ke liye commonjs
    "outDir": "./dist",        // compiled .js yahan aayegi
    "rootDir": "./src",        // .ts files yahan hongi
    "strict": true,            // ⭐ saari strict checks ON (hamesha ON rakho)
    "esModuleInterop": true
  }
}
```

> `"strict": true` ON rakhna best practice hai. Isme `noImplicitAny`, `strictNullChecks` jaise checks aa jaate hain.

---

## 3. Basic (in-built) types

```ts
let age: number = 25;            // number (integer + decimal dono)
let name: string = "Sourabh";    // string
let isActive: boolean = true;    // boolean
let marks: number[] = [90, 80];  // number ka array
let names: Array<string> = ["a", "b"];  // same cheez, dusra syntax: Array<type>
```

| Type | Matlab | Example |
|------|--------|---------|
| `number` | int/float sab | `10`, `3.14` |
| `string` | text | `"hello"` |
| `boolean` | true/false | `true` |
| `T[]` / `Array<T>` | T type ka array | `number[]` |

> Note: TS me `int`, `float` jaisa alag type **nahi** hota. Sab `number` hai.
> `int result = ...` TS me **galat** hai (ye Java/C++ ka syntax hai).

---

## 4. Type Annotation vs Type Inference

**Annotation** = tum khud type likhte ho.

```ts
let num1: number = 1;
let num2: number = 1;
let ans: number = num1 + num2;   // ans bhi number
console.log(ans);                // 2
```

**Inference** = TS khud samajh leta hai value dekhke.

```ts
let city = "Indore";   // TS ne infer kiya: string
city = 10;             // ❌ ERROR — city string hai
```

> **Best practice:** Jahan TS khud infer kar le wahan type likhne ki zarurat nahi. **Function parameters** me type **hamesha** likho.

---

## 5. Functions me types

Function me 2 jagah type dete hain: **parameters** ka aur **return value** ka.

```ts
//            param type            return type
function getTotal(numbers: Array<number>): number {
    return numbers.reduce((acc, item) => {
        return acc + item;     // acc aur item dono number
    }, 0);                     // 0 = initial value
}

console.log(getTotal([1, 2, 3, 4, 5]));   // 15
```

**Line by line:**
- `numbers: Array<number>` → ye function sirf number ka array lega.
- `: number` (paranthesis ke baad) → ye function **number return** karega.
- `reduce` → array ko ek single value me convert karta hai (yahan sum).
- `acc` = accumulator (ab tak ka total), `item` = current element.

```ts
getTotal(["a", "b"]);   // ❌ ERROR — string array nahi chalega
```

**Arrow function:**

```ts
const add = (a: number, b: number): number => a + b;
```

**Optional / default parameter:**

```ts
function greet(name: string, greeting?: string): string {
    return `${greeting ?? "Hello"} ${name}`;
}
greet("Sourabh");            // Hello Sourabh
greet("Sourabh", "Hi");      // Hi Sourabh

function power(base: number, exp: number = 2) { return base ** exp; }  // default value
```

**Return type `void`** (kuch return nahi karta):

```ts
function logMsg(msg: string): void {
    console.log(msg);
}
```

---

## 6. Objects aur Custom Types (`type`)

Plain object (bina type ke) — TS inference kar leta hai:

```ts
const user = {
    firstName: 'john',
    lastName: 'Doe',
    role: 1
}
console.log(user);
```

In-built types ke alawa hum **apne custom types** bhi bana sakte hain. Ye type ek **pura object** ka structure ho sakta hai:

```ts
type User = {
    name: string,
    age: number
}
```

Ye basically apna ek **custom type** hai. Jaise `number`/`string` use karte the, waise hi ab `User` ko directly use kar sakte hain:

```ts
const user1: User = {
    name: "hello",
    age: 12
}
console.log(user1);
```

```ts
const bad: User = { name: "hello" };           // ❌ age missing
const bad2: User = { name: "a", age: "12" };   // ❌ age string nahi chalega
const bad3: User = { name: "a", age: 1, x: 5 };// ❌ extra property x allowed nahi
```

**Function me custom type use karna:**

```ts
function login(userData: User): User {   // input bhi User, output bhi User
    return userData;
}

console.log(login({
    name: 'John Doe',
    age: 23
}))
```

> Yahan hum function ko bata rahe hain: "tum `User` type ka object loge aur `User` hi return karoge."

### Primitive ka alias (naya naam dena)

```ts
type SanitizedString = string;   // string ko naya naam
type EvenNumber = number;        // number ko naya naam
```

> ⚠️ Ye sirf **naam (alias)** hai. `EvenNumber` me TS khud check **nahi** karta ki number even hai. Wo logic tumhe khud likhna padega. (Tumhare notes me `type EvenNumber = string` likha tha — wo galat hai, `number` hona chahiye.)

Ye cheez **interface me nahi ho sakti** (primitive ko interface se extend nahi kar sakte):

```ts
interface SanitizedString extends string { }   // ❌ ERROR
```

Kyunki `interface` sirf **object shapes** ke liye hai. `string` ek primitive type hai, object shape nahi. Primitive/union ke liye **`type`** use karna padega.

---

## 7. Optional (`?`) aur `readonly`

### Optional property `?`

Agar property ke aage `?` laga diya, to wo property **ho bhi sakti hai, nahi bhi** (matlab `undefined` bhi ho sakti hai).

```ts
type Profile = {
    name: string;
    age?: number;      // optional
}

const p1: Profile = { name: "A" };            // ✅ age nahi diya, chalega
const p2: Profile = { name: "A", age: 20 };   // ✅
```

Use karte time dhyan rakho (kyunki `undefined` ho sakta hai):

```ts
console.log(p1.age.toFixed());     // ❌ age undefined ho sakta hai
console.log(p1.age?.toFixed());    // ✅ optional chaining
```

### `readonly` [EXTRA]

Ek baar value de di, phir change nahi kar sakte.

```ts
type Config = {
    readonly id: number;
    name: string;
}
const c: Config = { id: 1, name: "app" };
c.name = "new";   // ✅
c.id = 5;         // ❌ ERROR: read-only
```

---

## 8. Interface

`interface` bhi **type alias jaise hi hote hain** — object ka shape define karte hain.

```ts
interface Transaction {
    payerAccountNumber: number;
    payeeAccountNumber: number;
}

interface BankAccount {
    accountNumber: number;
    accountHolder: string;
    balance: number;
    isActive: boolean;
    transactions: Transaction[]    // Transaction type ka array (ek interface dusre me use hua)
}
```

Ab objects banate hain:

```ts
const transaction1: Transaction = {
    payerAccountNumber: 9091,
    payeeAccountNumber: 1232
}
const transaction2: Transaction = { payerAccountNumber: 9091, payeeAccountNumber: 1232 }
const transaction3: Transaction = { payerAccountNumber: 9091, payeeAccountNumber: 1232 }

const bankAccount: BankAccount = {
    accountNumber: 123,
    accountHolder: 'hello world',
    balance: 4832,
    isActive: true,
    transactions: [transaction1, transaction2, transaction3]
}
```

**Samajhne wali baat:** `BankAccount` ke andar `transactions: Transaction[]` hai — matlab interfaces ko **compose** (jodna) kar sakte hain, bilkul real DB schema jaise (MongoDB me embedding jaisa).

**Interface me method bhi likh sakte hain:**

```ts
interface IAuth {
    username: string;
    password: string;
    login(username: string, password: string): void;   // method ka signature
}
```

---

## 9. `type` vs `interface` — difference

| Point | `type` | `interface` |
|-------|--------|-------------|
| Object shape | ✅ | ✅ |
| Primitive alias (`type A = string`) | ✅ | ❌ |
| Union (`number \| string`) | ✅ | ❌ |
| Tuple | ✅ | ⚠️ (possible but awkward) |
| Inheritance | `&` (intersection) | `extends` |
| Declaration merging (same naam 2 baar likhna = merge) | ❌ | ✅ |
| Classes `implements` kar sakti hain | ✅ (object type ho to) | ✅ |

**Declaration merging (sirf interface me):**

```ts
interface Car { brand: string }
interface Car { year: number }
// Dono merge: Car = { brand: string; year: number }
```

```ts
type Bike = { brand: string }
type Bike = { year: number }   // ❌ ERROR: Duplicate identifier
```

> **Thumb rule:** Object ya class ka shape → `interface`. Union / primitive alias / complex type → `type`. (Dono chalte hain, team ka convention follow karo.)

---

## 10. Interface `extends` (inheritance)

Tumhare code me `Book`, `EBook`, `AudioBook` me **same fields repeat** ho rahe the (name, price). Isko `extends` se avoid karte hain — **DRY principle**.

```ts
// ❌ Repetition wala tarika (tumhara code)
interface EBook {
    name: string;
    price: number;       // repeat
    fileSize: number;
    format: string;
}

// ✅ Sahi tarika — extends
interface Book {
    name: string;
    price: number;
}

interface EBook extends Book {     // Book ki saari properties + apni
    fileSize: number;
    format: string;
}

interface AudioBook extends EBook {   // EBook (aur Book) ki saari + duration
    duration: number;
}
```

```ts
const book: Book = {
    name: 'Atomic Habits',
    price: 10222
}

const ebook: EBook = {
    name: 'Atomic Habits',
    price: 10222,
    fileSize: 300,
    format: 'pdf'
}

const audio: AudioBook = {
    name: 'Atomic Habits',
    price: 500,
    fileSize: 900,
    format: 'mp3',
    duration: 340
}
```

**Multiple extend:**

```ts
interface A { a: number }
interface B { b: number }
interface C extends A, B { c: number }   // a, b, c teeno
```

**`type` me same kaam — Intersection `&`:**

```ts
type BookT = { name: string; price: number };
type EBookT = BookT & { fileSize: number; format: string };
```

---

## 11. Union types aur Narrowing

### Union (`|`) — "ya ye ya wo"

Agar chahte ho `ID` **number bhi** accept kare aur **string bhi**, to **union** dena padega:

```ts
type ID = number | string;     // ID ya to number hogi ya string

const userId: ID = 123;        // ✅ number
const userId1: ID = '123';     // ✅ string
// const x: ID = true;         // ❌ boolean nahi chalega

console.log("User Id - number", userId)
console.log("User Id - string", userId1)
```

### Problem: union me kaunsi method chalegi?

```ts
function bad(id: ID) {
    id.toUpperCase();   // ❌ ERROR — id number bhi ho sakta hai, number pe toUpperCase nahi hota
}
```

TS ko pata nahi ki andar string hai ya number, isliye wo **sirf wahi cheez allow** karta hai jo **dono me common** ho.

### Narrowing — type ko check karke chhota (specific) karna

```ts
function printId(id: ID) {
    // typeof se check karte hain
    if (typeof id === 'string') {
        // Is block ke andar TS ko pata hai id = string
        console.log(id.toUpperCase());   // ✅
    }
    else {
        // Yahan TS ko pata hai id = number
        console.log(id);
    }
}

printId('1');
printId(5);
```

> Isi ko **Narrowing** kehte hain: `typeof` check ke baad TS type ko **narrow** karke ek specific type bana deta hai.

### Common methods wala union

```ts
// string aur array — dono me .slice() hota hai, isliye allowed
function getFirstThree(x: string | number[]) {
    return x.slice(0, 3);
}

console.log("string slice:", getFirstThree('hello'));             // "hel"
console.log("array slice:", getFirstThree([1, 2, 3, 4, 5, 6]));   // [1, 2, 3]
```

> Yahan narrowing ki zarurat nahi padi kyunki `slice` dono (string aur array) me common hai.

### Narrowing ke aur tareeke [EXTRA]

```ts
// 1. typeof        -> primitive ke liye
if (typeof x === "number") { }

// 2. instanceof    -> class ke liye
if (err instanceof Error) { console.log(err.message) }

// 3. in operator   -> property check
type Dog = { bark(): void };
type Cat = { meow(): void };
function speak(pet: Dog | Cat) {
    if ("bark" in pet) pet.bark();
    else pet.meow();
}

// 4. Truthiness
function show(s?: string) {
    if (s) console.log(s.toUpperCase());  // s yahan string hai (undefined nahi)
}

// 5. Discriminated union (bahut important, API response me)
type Success = { status: "success"; data: string };
type Failure = { status: "error"; message: string };
type Result = Success | Failure;

function handle(r: Result) {
    if (r.status === "success") console.log(r.data);     // Success
    else console.log(r.message);                          // Failure
}
```

---

## 12. Special types [EXTRA]

### `any` — type safety band (avoid karo!)

```ts
let a: any = 10;
a = "hello";
a = true;
a.foo.bar();   // ✅ compile me koi error nahi, par runtime pe crash!
```
> `any` = "TS, tum check mat karo". Ye TypeScript ka **fayda hi khatam** kar deta hai. Jitna ho sake avoid karo.

### `unknown` — `any` ka safe version

```ts
let u: unknown = "hello";
// u.toUpperCase();          // ❌ pehle check karna padega
if (typeof u === "string") {
    u.toUpperCase();          // ✅ narrowing ke baad
}
```
> Jab type pata na ho (jaise API/JSON/`catch` ka error) to `any` ki jagah `unknown` use karo.

### `void` — function kuch return nahi karta
```ts
function log(): void { console.log("hi"); }
```

### `never` — function kabhi return hi nahi karta
```ts
function fail(msg: string): never {
    throw new Error(msg);   // hamesha error throw, kabhi return nahi
}
```

### `null` aur `undefined`
```ts
let a: string | null = null;
let b: string | undefined = undefined;
```
> `strict: true` me `string` me `null`/`undefined` **allowed nahi** — alag se union me likhna padta hai.

---

## 13. Literal types, Tuple, Enum [EXTRA]

### Literal types — exact value hi allowed

```ts
type Role = "admin" | "user" | "guest";   // sirf ye 3 values

let r: Role = "admin";   // ✅
r = "manager";           // ❌ ERROR
```
> Backend me roles/status/HTTP method ke liye bahut use hota hai (tumhara **RBAC** concept).

### Tuple — fixed length + fixed position types ka array

```ts
let person: [string, number] = ["Sourabh", 22];
// person = [22, "Sourabh"];   // ❌ order galat
```

### Enum — named constants

```ts
enum Status {
    Pending,     // 0
    Approved,    // 1
    Rejected     // 2
}
let s: Status = Status.Approved;

// String enum (zyada readable)
enum Role2 {
    Admin = "ADMIN",
    User = "USER"
}
```

---

## 14. Generics

### Generics kya hain?

Generics hume **code reusability** dete hain. Isme hum **type fix nahi karte**, balki **`T`** (placeholder) de dete hain. Type **calling ke time** decide hota hai ya TS **automatic detect** kar leta hai.

> Analogy: Generic = ek **box** jiske upar label `T` hai. Jo cheez daalo, box usi type ka ho jaata hai.

### 1. Generics ke bina (`any` use karke) — problem

```ts
function getData(value: any): any {
    return value;
}

let result = getData(100);     // result bhi any ho gaya
```

Yahan `any` use kiya, jiski wajah se TS **type safety lose** kar deta hai. Matlab koi bhi data bhej sakte ho aur return me bhi pata nahi kya type aaya (`result.foo()` karoge to TS chup rahega).

### 2. Generics ke saath

```ts
function getData<T>(value: T): T {
    return value;
}

let numberResult  = getData<number>(100);
let stringResult  = getData<string>("Sourabh");
let booleanResult = getData<boolean>(true);
```

**Line by line:**
- `<T>` → ek **generic type parameter** hai (placeholder).
- `value: T` → input ka type **T** hoga.
- `: T` (return) → output ka type **input ke same** hoga.
- `getData<number>(100)` → yahan `T = number`.

### 3. TS automatic infer bhi kar leta hai

```ts
let num  = getData(100);        // T = number infer
let name = getData("Sourabh");  // T = string infer
```
- `num` ka type **number**, `name` ka type **string** automatically.

### 4. Generics with Array

```ts
function getFirstElement<T>(items: T[]): T {
    return items[0];
}

let number = getFirstElement([10, 20, 30]);            // T = number
let name1  = getFirstElement(['sourabh', 'rahul']);    // T = string

console.log("This number :- ", number);
console.log("This name is :- ", name1);
```
> `T[]` = T type ka array, aur return bhi wahi `T`.

### 5. Generics with Interface (Backend me sabse useful!)

```ts
interface ApiResponse<T> {
    success: boolean;
    data: T;           // data ka type bahar se aayega
}

interface User1 {
    id: number;
    name: string;
}

const response: ApiResponse<User1> = {
    success: true,
    data: {
        id: 1,
        name: "Sourabh"
    }
};
```

`ApiResponse<T>` ek **reusable interface** hai. `T` ki jagah `User1` diya, to `data` ka type `User1` ho gaya. Dusri jagah:

```ts
const r2: ApiResponse<string[]> = { success: true, data: ["a", "b"] };
const r3: ApiResponse<number>   = { success: true, data: 10 };
```

> Express me har API ka response same format me hota hai (`success`, `data`) — bas `data` alag hota hai. Generic se ek hi interface sab jagah.

### 6. Generic Constraints (`extends`) [EXTRA]

`T` ko limit karna — "T kuch bhi nahi, kam se kam ye shape hona chahiye."

```ts
function printLength<T extends { length: number }>(item: T): number {
    return item.length;
}

printLength("hello");     // ✅ string me length hai
printLength([1, 2, 3]);   // ✅ array me length hai
printLength(10);          // ❌ number me length nahi
```

### 7. Multiple generics [EXTRA]

```ts
function makePair<K, V>(key: K, value: V): [K, V] {
    return [key, value];
}
const p = makePair("age", 22);   // [string, number]
```

---

## 15. Async / Promise ke saath types

`async` function hamesha **Promise return** karta hai. Isliye return type `Promise<...>` me likhte hain.

```ts
interface IPost {
    title: string,
    id: number,
    description: string
}

interface IUser {
    id: number,
    name: string,
    age: number
}

// Promise<IPost[]> = ye promise resolve hoke IPost ka ARRAY dega
async function fetchPostData(path: string): Promise<IPost[]> {
    const response = await fetch(`https://example.com/${path}`)
    return response.json();
}

async function fetchUserData(path: string): Promise<IUser[]> {
    const response = await fetch(`https://example.com/${path}`)
    return response.json();
}
```

**Problem:** Dono functions me **same code repeat** ho raha hai, sirf return type alag hai. 👉 **Generic use karo:**

```ts
async function fetchData<T>(path: string): Promise<T> {
    const response = await fetch(`https://example.com/${path}`)
    return response.json();
}

// Type function call karte time batate hain
(async () => {
    const users = await fetchData<IUser[]>('users');   // users: IUser[]
    const posts = await fetchData<IPost[]>('posts');   // posts: IPost[]
})();      // <-- yahan () lagana zaruri hai, tabhi function run hoga (IIFE)
```

> ⚠️ `response.json()` ka type actually `any` hota hai — TS sirf **trust** karta hai ki tum sahi type bata rahe ho. Runtime pe validate nahi karta (uske liye Zod / Joi use hota hai).

---

## 16. Structural typing (Duck typing)

> "Agar kuch **duck jaise chalta hai aur duck jaise bolta hai**, to wo duck hai."

TS me **naam (class/interface ka naam) matter nahi karta, shape (structure) matter karta hai.** Agar do objects ka structure same hai, to TS unko same type maanta hai.

```ts
interface ICredential {
    username: string;
    password: string;
    isAdmin?: boolean      // optional
}

function login1(credentials: ICredential): boolean {
    console.log(credentials);
    return true;
}

// Ye object kisi bhi interface se declare NAHI hua
const user3 = {
    username: 'codergyan',
    password: 'secret',
    isAdmin: true
}

login1(user3);   // ✅ chal gaya! Kyunki shape ICredential se match karta hai
```

`user3` ko `ICredential` nahi bataya, phir bhi chala — kyunki usme `username`, `password`, `isAdmin` teeno hain jo `ICredential` maangta hai.

### Method wala interface

```ts
interface IAuth {
    username: string;
    password: string;
    login(username: string, password: string): void;
}

const auth: IAuth = {
    username: 'codersgyan',
    password: 'secret',
    login(username, password) {     // yahan param ka type likhna zaruri nahi (interface se infer)
        username = 'hello';
        password = '123';
    }
}
```

### Excess property check (dhyan rakho)

```ts
login1(user3);                                   // ✅ variable bheja — extra property ho to chalega
login1({ username: "a", password: "b", x: 1 });  // ❌ seedha object literal me extra property -> error
```

---

## 17. Tumhare code ki galtiyan (Corrections)

Notes/code me ye chhoti galtiyan thi — dhyan do, interview/real project me problem karengi:

| # | Tumhara code | Problem | Sahi |
|---|--------------|---------|------|
| 1 | `int result = getData(100);` | `int` TS me nahi hota | `let result = getData(100);` |
| 2 | `type EvenNumber = string;` | Even number ke liye `string` galat | `type EvenNumber = number;` (check logic khud likho) |
| 3 | `interface SanitizedString extends string {}` | Interface primitive ko extend nahi kar sakta | `type SanitizedString = string;` |
| 4 | `id:nubmber` | Spelling | `id: number` |
| 5 | `const response = ApiResponse<User>={...}` | Syntax galat | `const response: ApiResponse<User> = {...}` |
| 6 | `console.log("This name is :- ", name);` | `name1` print karna tha. `name` ek global (window.name) ban jaata hai, alag value | `console.log(..., name1);` |
| 7 | `(async () => {...})` (bina `()` ke) | Function define hua par **call nahi hua** | `(async () => {...})();` |
| 8 | `Book`, `EBook`, `AudioBook` me repeat fields | Code duplication | `extends` use karo (Section 10) |
| 9 | `interface ICreadential`, `success` me 3 c (`succcess`) | Spelling | `ICredential`, `success` |
| 10 | `const user = {...}` aur phir `const user = await ...` | Variable shadowing se confusion | Alag naam do |

---

## 18. Utility Types, Type Assertion, Classes [EXTRA]

### Utility Types — pehle se bane hue helpful types

```ts
interface Employee {
    id: number;
    name: string;
    salary: number;
    email: string;
}

// Partial<T>  -> saari properties optional
type UpdateEmp = Partial<Employee>;
// Use: PATCH API me (sirf kuch fields update)

// Required<T> -> saari properties compulsory

// Readonly<T> -> saari properties readonly

// Pick<T, Keys> -> sirf chune hue fields
type EmpPreview = Pick<Employee, "id" | "name">;      // { id, name }

// Omit<T, Keys> -> kuch fields hata do
type EmpPublic = Omit<Employee, "salary">;            // salary ke bina
// Use: password hata ke user bhejna

// Record<K, V> -> key-value object
type RoleMap = Record<string, number>;                // { [key: string]: number }
const roles: Record<"admin" | "user", string[]> = {
    admin: ["read", "write"],
    user: ["read"]
};
```

### Type Assertion (`as`) — TS ko bolna "mujhe pata hai ye kya hai"

```ts
const input = document.getElementById("name") as HTMLInputElement;
const data = JSON.parse('{"a":1}') as { a: number };
```
> Ye sirf compile-time hint hai. Galat assertion pe runtime crash ho sakta hai. Zyada use mat karo.

### Non-null assertion `!`
```ts
const el = document.getElementById("x")!;   // "ye null nahi hoga" (risky)
```

### Classes with access modifiers

```ts
class Employee {
    // constructor me hi property declare + assign (shorthand)
    constructor(
        public name: string,         // kahin se bhi access
        private salary: number,      // sirf class ke andar
        protected id: number,        // class + child class
        readonly company: string = "Medicaps"
    ) {}

    getSalary(): number {
        return this.salary;
    }
}

const e = new Employee("Sourabh", 50000, 1);
console.log(e.name);        // ✅
// console.log(e.salary);   // ❌ private hai
```

**Class + interface (`implements`):**

```ts
interface Payable {
    pay(amount: number): void;
}

class Wallet implements Payable {
    pay(amount: number): void {
        console.log("Paid", amount);
    }
}
```

### `keyof` aur `typeof` (type level) [EXTRA]

```ts
type UserKeys = keyof User;      // "name" | "age"

const config = { port: 3000, host: "localhost" };
type Config = typeof config;     // { port: number; host: string }
```

### Express ke saath (backend me use)

```ts
import express, { Request, Response } from "express";

interface CreateUserBody { name: string; email: string }

app.post("/users", (req: Request<{}, {}, CreateUserBody>, res: Response) => {
    const { name, email } = req.body;   // typed
    res.json({ success: true, data: { name, email } });
});
```

---

## 19. Interview Questions

**Q1. TypeScript kya hai? JS se kaise alag?**
TS = JS ka superset jisme static typing hai. Compile hoke JS banta hai. Errors compile time pe pakde jaate hain.

**Q2. `type` vs `interface`?**
Dono object shape define karte hain. `interface` → `extends`, declaration merging. `type` → union, primitive alias, tuple, intersection (`&`).

**Q3. `any` vs `unknown`?**
`any` → koi check nahi (unsafe). `unknown` → use karne se pehle type check/narrowing karni padti hai (safe).

**Q4. Generics kya hain, kyun?**
Reusable code with type safety. `T` placeholder jo call ke time decide hota hai. Example: `ApiResponse<T>`, `fetchData<T>`.

**Q5. Narrowing kya hai?**
Union type ko `typeof`, `instanceof`, `in`, equality check se specific type me convert karna.

**Q6. Type inference kya hai?**
TS value dekhke khud type decide kar leta hai.

**Q7. Structural typing kya hai?**
TS shape se type match karta hai, naam se nahi (duck typing).

**Q8. `?` (optional) ka matlab?**
Property ya parameter optional hai, value `undefined` ho sakti hai.

**Q9. `never` kab use hota hai?**
Jab function kabhi return nahi karta (throw / infinite loop), ya exhaustive checks me.

**Q10. `Partial`, `Pick`, `Omit` kya hain?**
Built-in utility types. Partial → sab optional. Pick → chune fields. Omit → kuch fields hatao.

**Q11. TS runtime pe type check karta hai?**
Nahi. Types compile ke baad hat jaate hain. Runtime validation ke liye Zod/Joi/class-validator.

**Q12. `strict` mode kyun ON rakhte hain?**
`noImplicitAny`, `strictNullChecks` jaise checks ON ho jaate hain, jisse bugs kam hote hain.

**Q13. `enum` vs literal union?**
Literal union (`"a" | "b"`) lightweight hai, JS code generate nahi karta. Enum runtime me object banata hai.

**Q14. Interface extends vs type intersection?**
Dono inheritance jaisa kaam. `extends` me conflict pe error aata hai, `&` me conflicting property `never` ban sakti hai.

---

## 20. Quick Cheat Sheet

```ts
// Basic
let a: number = 1;
let b: string = "x";
let c: boolean = true;
let d: number[] = [1, 2];
let t: [string, number] = ["a", 1];

// Function
function f(x: number, y?: number): string { return ""; }

// Type alias
type User = { name: string; age?: number; readonly id: number };

// Interface + extends
interface A { x: number }
interface B extends A { y: number }

// Union + Literal
type ID = number | string;
type Role = "admin" | "user";

// Narrowing
if (typeof id === "string") { /* string */ }

// Generics
function g<T>(v: T): T { return v; }
interface Res<T> { success: boolean; data: T }
async function fetchData<T>(p: string): Promise<T> { return (await fetch(p)).json(); }

// Utility
Partial<User>; Pick<User, "name">; Omit<User, "id">; Record<string, number>;
```

---

### Sequence me yaad rakhne ka tarika

```
Types kyun? -> Basic types -> Functions -> type (custom) -> optional/readonly
-> interface -> extends -> union + narrowing -> any/unknown/never
-> literal/tuple/enum -> generics -> async Promise -> structural typing
-> utility types -> classes
```
