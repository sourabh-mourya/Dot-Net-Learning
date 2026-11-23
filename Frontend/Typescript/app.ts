
let num1: number = 1;

let num2: number = 1;

let ans: number = num1 + num2;

console.log(ans);


function getTotal(numbers: Array<number>) {
    return numbers.reduce((acc, item) => {
        return acc + item;
    }, 0);
}


console.log(getTotal([1, 2, 3, 4, 5]));



const user = {
    firstName: 'john',
    lastName: 'Doe',
    role: 1
}


console.log(user);


type User = {
    name: string,
    age: number
}


const user1: User = {
    name: "hello",
    age: 12
}

console.log(user1);



function login(userData: User): User {

    return userData;
}


console.log(login({
    name: 'John Doe',
    age: 23
}))




//interface
interface Transaction {
    payerAccountNumber: number;
    payeeAccountNumber: number;
}

interface BankAccount {
    accountNumber: number;
    accountHolder: string;
    balance: number;
    isActive: boolean;
    transactions: Transaction[]
}

const transaction1: Transaction = {

    payerAccountNumber: 9091,
    payeeAccountNumber: 1232
}

const transaction2: Transaction = {

    payerAccountNumber: 9091,
    payeeAccountNumber: 1232
}
const transaction3: Transaction = {

    payerAccountNumber: 9091,
    payeeAccountNumber: 1232
}

const bankAccount: BankAccount = {
    accountNumber: 123,
    accountHolder: 'hello world',
    balance: 4832,
    isActive: true,
    transactions: [transaction1, transaction2, transaction3]


}


//extend
interface Book {
    name: string;
    price: number;
}


interface EBook {
    name: string;
    price: number;
    fileSize: number;
    format: string;
}

interface AudioBook {
    name: string,
    price: number,
    fileSize: number,
    format: string,
    duration: number
}

const book1: EBook = {
    name: 'Atomic habitats',
    price: 10222,
    fileSize: 300,
    format: 'pdf'
}

const book: Book = {
    name: 'Atomic habitats',
    price: 10222
}



//unions

type ID = number | string;

const userId: ID = 123;
const userId1: ID = '123';


console.log("User Id - number", userId)
console.log("User Id - string", userId1)



function printId(id: ID) {
    //agar id apne string bhej rhe hi to simple usko upper case me convert kr dunga...

    // 
    if (typeof id === 'string') {
        console.log(id.toUpperCase());
    }
    else {
        console.log(id);
    }
}


printId('1');


function getFirstThree(x: string | number[]) {
    return x.slice(0, 3);
}

console.log("We passing the string hello after slicing we get the data :- ", getFirstThree('hello'))

console.log("we passing the number arrary [1,2,3,4,5,6] then slicing we get the data ", getFirstThree([1, 2, 3, 4, 5, 6]));



console.log("-------------Generics--------------------")


function getData<T>(value: T): T {
    return value;
}

let numberResult = getData<number>(100);

let stringResult = getData<string>("Sourabh");

let booleanResult = getData<boolean>(true);


console.log(numberResult);


console.log("Generic with array")


function getFirstElement<T>(items: T[]): T {
    return items[0];
}


let number = getFirstElement([10, 20, 30])

let name1 = getFirstElement(['sourabh', 'rahul'])


console.log("This number :- ", number)
console.log("This name is :- ", name);


//genericw with interface
interface ApiResponse<T> {
    success: boolean;
    data: T;
}

//Ab different response types define kar sakte hain:


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

//Promise<IPost[]> ye ek promsie return kre kyuki async  ek promise return krta hi 
// ur ek IPost ka array retrun krege usske ander apen ne defined krdiya hi ...
async function fetchPostData(path: string): Promise<IPost[]> {
    const response = await fetch(`https://example.com/${path}`)
    return response.json();
}


async function fetchUserData(path: string): Promise<IUser[]> {
    const response = await fetch(`https://example.com/${path}`)
    return response.json();
}

(async () => {
    const posts = await fetchPostData('posts');
})


/// ye shi but apne yaha bahut saara repetition wala kaam kr rhe hi ...
async function fetchData<T>(path: string): Promise<T> {
    const response = await fetch(`https://example.com/${path}`)
    return response.json();
}


//ab apne iska type funcition calling ke time pr batayege...
(async () => {
    const user = await fetchData<IUser[]>('users');

})

//structural typing or duck typing agar do object ka same type hi to 
//agar do object  ka type same hi to typescript ke according unke object ka type bhi same rehta hi ...          
interface ICreadential {

    username: string;
    password: string;
    isAdmin?: boolean
}


function login1(credentials: ICreadential): boolean {
    console.log(credentials);
    return true;
}

const user3 = {
    username: 'codergyan',
    password: 'secret',
    isAdmin: true
}

login1(user3);


interface IAuth {
    username: string;
    password: string;
    login(username: string, password: string): void;
}


const auth: IAuth = {
    username: 'codersgyan',
    password: 'secert',
    login(username, password) {
        username = 'hello',
            password = '123'
    }
}