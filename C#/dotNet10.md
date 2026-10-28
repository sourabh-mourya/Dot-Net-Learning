# C# Multithreading — Complete Notes (Beginner to Production Level)

## 1. Multithreading Kya Hai?

Multithreading ek programming technique hai jisme ek process ke andar multiple threads use kiye ja sakte hain, taaki different tasks ko concurrently execute kiya ja sake.

Example: Ek web application mein:

- Thread/task A: Database se data fetch karna.
- Thread/task B: File read karna.
- Thread/task C: Background processing karna.

In operations ko is tarah manage kiya ja sakta hai ki ek operation ke wait karne ke dauran doosra operation progress kar sake.

### Process, Thread aur Application

**Process:** Jab koi application execute hoti hai, operating system uske liye ek process create karta hai. Process ke paas apna virtual address space aur operating-system resources hote hain.

**Thread:** Thread execution ka ek path hai. Ek process mein ek ya multiple threads ho sakte hain. Threads process ke resources, jaise memory, share karte hain, lekin har thread ka apna execution state aur stack hota hai.

**Main Thread:** Console application mein normally `Main()` method main thread par execute hota hai.

```text
Application
    |
    v
Process
    |
    +---- Main Thread
    |
    +---- Worker Thread
    |
    +---- Worker Thread
```

### Kya har application single-threaded hoti hai?

Nahi. Console application mein tumhara managed code usually main thread se start hota hai, lekin .NET runtime aur operating system additional threads use kar sakte hain.

Jab hum kehte hain ki application single-threaded hai, generally iska matlab hota hai ki application ka relevant work ek hi thread par execute ho raha hai.

### Kya ek process mein multiple threads simultaneously execute ho sakte hain?

Haan.

- Single CPU core par threads generally time-sharing ke through interleave hote hain.
- Multiple CPU cores par multiple threads actual mein parallel execute bhi ho sakte hain.

**Concurrency:** Multiple tasks ki progress overlapping hoti hai.

**Parallelism:** Multiple tasks same time par different CPU cores par execute ho sakte hain.

Ye dono related hain, lekin same concept nahi hain.

---

## 2. Main Thread in C#

### Example 1: Current Thread

```csharp
using System;
using System.Threading;

namespace ThreadDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World");

            Thread t = Thread.CurrentThread;

            t.Name = "My Main Thread";

            Console.WriteLine("Thread Name: " + t.Name);
            Console.WriteLine("Thread ID: " + t.ManagedThreadId);
            Console.WriteLine("Is Background: " + t.IsBackground);
        }
    }
}
```

### Explanation

`using System.Threading;`

Is namespace mein `Thread`, `Monitor`, `Mutex`, `SemaphoreSlim` aur related threading types milte hain.

`Thread.CurrentThread`

Ye currently executing thread ka `Thread` object return karta hai.

`Thread.Name`

Thread ka optional name set kar sakte hain. Debugging mein useful hai.

`Thread.ManagedThreadId`

Current managed thread ki identifier value deta hai. Ye debugging aur diagnostics mein useful hai; ise permanent identity ya business identifier mat samajhna.

`Thread.Sleep(5000)`

Current thread ko approximately 5 seconds ke liye suspend karta hai. Is duration mein current thread doosra managed work execute nahi karta.

**Important:** `Thread.Sleep()` I/O ka wait karne ka recommended approach nahi hai. Asynchronous code mein generally `await Task.Delay()` use karo.

---

## 3. Sequential Execution vs Multithreading

### Sequential execution

```csharp
using System;

class Program
{
    static void Test1()
    {
        Console.WriteLine("Test1 started");
        Console.WriteLine("Test1 completed");
    }

    static void Test2()
    {
        Console.WriteLine("Test2 started");
        Console.WriteLine("Test2 completed");
    }

    static void Main()
    {
        Test1();
        Test2();
    }
}
```

Execution order:

```text
Test1 started
Test1 completed
Test2 started
Test2 completed
```

`Test1()` return hone ke baad hi `Test2()` start hota hai.

Ye sequential execution hai.

### Multithreading using Thread

```csharp
using System;
using System.Threading;

class Program
{
    static void Test1()
    {
        Console.WriteLine("Test1 started");
        Thread.Sleep(2000);
        Console.WriteLine("Test1 completed");
    }

    static void Test2()
    {
        Console.WriteLine("Test2 started");
        Thread.Sleep(1000);
        Console.WriteLine("Test2 completed");
    }

    static void Main()
    {
        Thread t1 = new Thread(Test1);
        Thread t2 = new Thread(Test2);

        t1.Start();
        t2.Start();

        t1.Join();
        t2.Join();

        Console.WriteLine("Main completed");
    }
}
```

### Step-by-step execution

1. `t1` aur `t2` thread objects create hote hain.
2. `t1.Start()` thread ko start karne ke liye schedule karta hai.
3. `t2.Start()` doosre thread ko start karne ke liye schedule karta hai.
4. Dono methods concurrently execute ho sakte hain.
5. `Join()` main thread ko tab tak wait karwata hai jab tak specified thread complete na ho.
6. Dono threads complete hone ke baad `Main completed` print hota hai.

Output ka exact order guaranteed nahi hai. `Test1` aur `Test2` ki start/completion messages different order mein aa sakti hain.

### Important correction

Ye syntax incorrect hai:

```csharp
// Incorrect
Thread t1 = new Thread(test1());
```

Correct syntax:

```csharp
Thread t1 = new Thread(Test1);
t1.Start();
```

`Test1` method group pass hota hai, aur `Start()` thread execution begin karta hai.

`Test1()` likhne par method immediately call ho jayega aur uska return value pass karne ki koshish hogi.

---

## 4. Thread Scheduling aur Time-Sharing

Maan lo 3 threads hain:

```text
Thread A ----\
Thread B -----+----> CPU Scheduler ----> CPU Core
Thread C ----/
```

Operating system decide karta hai ki kis thread ko CPU time kab milega.

Single core par scheduler threads ko time slices de sakta hai. Isliye ek thread wait kar raha ho ya descheduled ho, to doosra thread execute ho sakta hai.

Lekin kuch important differences hain:

- Thread ko exact fixed time guarantee nahi hota.
- Scheduler application ke business logic ke according execute order guarantee nahi karta.
- Thread `Sleep()` ya I/O wait ke karan CPU use nahi kar raha ho sakta hai.
- Multiple CPU cores par threads simultaneously execute ho sakte hain.

**Multithreading ka matlab ye nahi hai ki har thread ko equal time milega ya slow thread ko automatically skip kar diya jayega.**

# Part 2 — Thread vs ThreadPool vs Task

## 5. Thread Class

`Thread` ek operating-system thread ko represent karta hai.

Jab tum `new Thread(...)` karte ho, tab ek dedicated thread create karne ke liye object prepare hota hai. Actual thread execution `Start()` par begin hota hai.

### Example

```csharp
using System;
using System.Threading;

class Program
{
    static void DoWork()
    {
        Console.WriteLine(
            $"Working on thread {Thread.CurrentThread.ManagedThreadId}");

        Thread.Sleep(2000);

        Console.WriteLine("Work completed");
    }

    static void Main()
    {
        Thread worker = new Thread(DoWork);

        worker.Start();

        worker.Join();

        Console.WriteLine("Main completed");
    }
}
```

### Thread kab use karein?

Dedicated thread tab useful ho sakta hai jab:

- Tumhe thread ki lifecycle par direct control chahiye.
- Kisi specific legacy API ko dedicated thread chahiye.
- Long-running blocking operation ko thread-pool workers se alag rakhna ho.
- Thread-specific configuration ya foreground/background behavior required ho.

Har request ya har method ke liye `new Thread()` create karna recommended nahi hai.

Thread create aur manage karne mein memory aur scheduling overhead hota hai.

---

## 6. ThreadPool

`ThreadPool` reusable worker threads ka pool hai. .NET in threads ko different work items execute karne ke liye reuse kar sakta hai.

```text
Work Item A ----\
Work Item B -----+----> ThreadPool ----> Reusable Worker Threads
Work Item C ----/
```

Har operation ke liye naya operating-system thread create karna zaroori nahi hota.

### Example

```csharp
using System;
using System.Threading;

class Program
{
    static void DoWork(object? state)
    {
        Console.WriteLine(
            $"Working on thread {Thread.CurrentThread.ManagedThreadId}");

        Console.WriteLine($"State: {state}");
    }

    static void Main()
    {
        ThreadPool.QueueUserWorkItem(DoWork, "Background Task");

        Console.WriteLine("Main completed");

        Thread.Sleep(500);
    }
}
```

Ye basic demonstration hai. `Thread.Sleep(500)` work complete hone ki reliable guarantee nahi deta. Production code mein completion track karne ke liye proper signaling ya Task-based APIs use karni chahiye.

### ThreadPool kab useful hai?

- Short-running background work
- CPU work ko reusable worker threads par schedule karna
- Task-based operations ko execute karna

Modern .NET mein `Task.Run()` common approach hai. Ye generally ThreadPool par work schedule karta hai.

ThreadPool unlimited threads create nahi karta. Excessive blocking ya work submission latency badha sakte hain.

---

## 7. Task

`Task` ek asynchronous operation ko represent karta hai. Ye zaroori nahi ki har Task ke liye dedicated thread ho.

Task ko samajhne ka sabse important point:

**Thread execution resource hai; Task operation ka representation hai.**

Task:

- Complete ho sakta hai.
- Fail ho sakta hai.
- Cancel ho sakta hai.
- Result return kar sakta hai (`Task<T>`).
- Asynchronous completion ka wait allow karta hai (`await`).

### Example

```csharp
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Task task = Task.Run(() =>
        {
            Console.WriteLine("Background work started");

            // Simulate CPU/blocking work for demonstration
            ThreadingDelay();

            Console.WriteLine("Background work completed");
        });

        Console.WriteLine("Main method continues");

        await task;

        Console.WriteLine("Task completed");
    }

    static void ThreadingDelay()
    {
        System.Threading.Thread.Sleep(1000);
    }
}
```

### Task<T> Example

```csharp
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        int result = await CalculateAsync();

        Console.WriteLine(result);
    }

    static async Task<int> CalculateAsync()
    {
        await Task.Delay(1000);

        return 100;
    }
}
```

`Task<int>` ek asynchronous operation represent karta hai jo eventually integer result provide karega.

### Thread vs ThreadPool vs Task

| Feature           | Thread                             | ThreadPool                         | Task                                 |
| ----------------- | ---------------------------------- | ---------------------------------- | ------------------------------------ |
| Main purpose      | Dedicated thread execution         | Reusable worker threads            | Operation/completion represent karna |
| Lifecycle control | Direct control comparatively zyada | Pool manages workers               | Task represents operation state      |
| Thread required?  | Yes, when started                  | Worker thread for queued work      | Not necessarily                      |
| Result support    | Direct built-in result nahi        | Direct built-in result nahi        | `Task<T>`                            |
| Async I/O         | Usually not ideal                  | Blocking worker waste ho sakta hai | Natural fit with `await`             |
| Common use        | Dedicated execution                | Reusable background workers        | Modern async APIs                    |

### Production recommendation

- I/O-bound work: `async/await`
- CPU-bound work: zarurat par `Task.Run()` ya parallel processing
- Dedicated thread: sirf jab genuine requirement ho
- Background services: ASP.NET Core mein `BackgroundService` jaise lifecycle-managed patterns consider karo

---

# Part 3 — async/await Internals

## 8. async/await Kya Hai?

`async` aur `await` C# ke language features hain jo asynchronous code ko readable banate hain.

`async` method ko asynchronous pattern ke according compile karne deta hai. `await` kisi awaitable operation ki completion ka wait karta hai bina normally current thread ko block kiye.

### Example

```csharp
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Start");

        await GetDataAsync();

        Console.WriteLine("End");
    }

    static async Task GetDataAsync()
    {
        Console.WriteLine("Fetching data...");

        await Task.Delay(2000);

        Console.WriteLine("Data received");
    }
}
```

Output:

```text
Start
Fetching data...
[Approximately 2 seconds later]
Data received
End
```

`Task.Delay(2000)` time delay represent karta hai. Ye demonstration hai; actual network ya database request mein `HttpClient` ya asynchronous database APIs use hongi.

### await par exactly kya hota hai?

1. Async method start hota hai.
2. `await` ke paas operation check hota hai.
3. Agar operation already complete hai, method synchronously continue kar sakta hai.
4. Agar operation incomplete hai, method apni continuation register karke caller ko control return kar sakta hai.
5. Operation complete hone par continuation resume hoti hai.
6. Method remaining code execute karta hai aur eventually Task complete hota hai.

**`await` ka matlab automatically new thread create karna nahi hai.**

---

## 9. async/await State Machine

Compiler async method ko state machine mein transform karta hai.

State machine execution ka progress maintain karti hai, jaise:

- Method kis point tak execute hua.
- Kaunsa asynchronous operation await ho raha hai.
- Resume hone par kahan se continue karna hai.
- Result ya exception ko kaise propagate karna hai.

Conceptual flow:

```text
Async Method Starts
        |
        v
Execute Until await
        |
        v
Is Operation Complete?
      /       \
    Yes        No
     |          |
     v          v
Continue    Save State
                |
                v
          Return Incomplete Task
                |
                v
          Operation Completes
                |
                v
          Resume State Machine
                |
                v
          Continue Execution
                |
                v
           Task Completes
```

Compiler-generated state machine ek conceptual explanation hai; exact generated implementation compiler version aur method par depend karti hai.

### Important

`await` ke baad execution wahi thread par resume ho, ye guaranteed nahi hai.

Resume hone ka behavior captured synchronization context, task scheduler aur await configuration par depend karta hai.

---

## 10. SynchronizationContext

`SynchronizationContext` ek abstraction hai jo decide karne mein help karti hai ki continuation ko kis execution context par post kiya jaye.

Example environments:

- UI application mein UI thread par continuation resume karna important ho sakta hai.
- Classic ASP.NET mein request synchronization context historically relevant tha.
- ASP.NET Core mein normally custom request `SynchronizationContext` nahi hota.

Isliye console ya ASP.NET Core application mein `await` ke baad same thread par resume hone ki guarantee nahi hoti.

### Simple example

```csharp
static async Task ExampleAsync()
{
    Console.WriteLine(
        $"Before: {Environment.CurrentManagedThreadId}");

    await Task.Delay(1000);

    Console.WriteLine(
        $"After: {Environment.CurrentManagedThreadId}");
}
```

Different thread IDs print ho sakte hain, lekin same ID aana bhi possible hai.

Thread ID difference demonstrate karta hai ki continuation different thread par execute ho sakti hai. Thread ID same hone se ye prove nahi hota ki koi thread blocking nahi hui.

---

## 11. ConfigureAwait(false)

Normally, `await` available `SynchronizationContext` ko capture karne ki koshish karta hai, aur special conditions mein current `TaskScheduler` ko bhi consider karta hai.

```csharp
await SomeOperationAsync();
```

Library code mein kabhi-kabhi ye likha jata hai:

```csharp
await SomeOperationAsync().ConfigureAwait(false);
```

Iska matlab hai ki continuation ko captured context par wapas schedule karne ki requirement nahi hai.

### Kab use karein?

- Reusable library code mein, jab original context par resume hona required na ho.
- Legacy UI ya classic ASP.NET scenarios mein unnecessary context dependency avoid karne ke liye.

### ASP.NET Core mein?

ASP.NET Core normally custom `SynchronizationContext` provide nahi karta, isliye `ConfigureAwait(false)` ka effect classic UI/classic ASP.NET ke comparison mein often less significant hota hai.

## Har line par blindly use karna zaroori nahi hai. Agar UI thread par wapas aana necessary hai, to `ConfigureAwait(false)` inappropriate ho sakta hai.

# Part 4 — Task Coordination and Cancellation

## 13. Task.WhenAll

`Task.WhenAll()` multiple tasks ki completion ka wait karta hai.

Ye tasks ko automatically parallel start nahi karta. Tasks ko pehle start ya initiate karna hota hai.

### Example

```csharp
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Task task1 = Task.Delay(1000);
        Task task2 = Task.Delay(2000);
        Task task3 = Task.Delay(1500);

        await Task.WhenAll(task1, task2, task3);

        Console.WriteLine("All tasks completed");
    }
}
```

Agar tasks concurrently progress kar rahe hain, total duration approximately longest task ke duration ke aas-paas ho sakti hai, na ki sab durations ka sum.

### Task<T> ke saath

```csharp
Task<int> t1 = GetNumberAsync(10);
Task<int> t2 = GetNumberAsync(20);

int[] results = await Task.WhenAll(t1, t2);
```

`results` mein dono tasks ke results input order mein milenge.

### Real-world use

API ko multiple independent services se data fetch karna hai, jaise user profile, orders aur notifications. Agar teeno operations independent hain, to unhe concurrently start karke `WhenAll` se await kar sakte ho.

---

## 14. Task.WhenAny

`Task.WhenAny()` multiple tasks mein se kisi ek ke complete hone par return karta hai.

```csharp
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Task first = Task.Delay(1000);
        Task second = Task.Delay(3000);

        Task completed = await Task.WhenAny(first, second);

        Console.WriteLine("One task completed");

        await completed;
    }
}
```

**Important:** `WhenAny()` return karne ka matlab ye nahi ki remaining tasks automatically cancel ho gaye.

Agar tumhe losing operations cancel karne hain, to explicit cancellation implement karni hogi.

---

## 15. CancellationToken

`CancellationToken` cooperative cancellation mechanism hai. Isse ek operation ko signal diya ja sakta hai ki use cancel ho jana chahiye.

Token khud thread ko forcefully terminate nahi karta.

### Example

```csharp
using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        using var cts = new CancellationTokenSource();

        Task work = DoWorkAsync(cts.Token);

        await Task.Delay(2500);

        cts.Cancel();

        try
        {
            await work;
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Operation cancelled");
        }
    }

    static async Task DoWorkAsync(CancellationToken token)
    {
        for (int i = 1; i <= 10; i++)
        {
            token.ThrowIfCancellationRequested();

            Console.WriteLine($"Working: {i}");

            await Task.Delay(1000, token);
        }
    }
}
```

### Flow

1. `CancellationTokenSource` create hota hai.
2. Uska token operation ko pass kiya jata hai.
3. `Cancel()` cancellation request signal karta hai.
4. Operation token check karta hai ya token-aware API cancellation handle karti hai.
5. Cancellation par commonly `OperationCanceledException` ya `TaskCanceledException` observe hota hai.

### Production use

- HTTP request cancel hone par downstream operations cancel karna.
- User ne search query change kar di ho.
- Background job shutdown karna.
- Timeout implement karna.

ASP.NET Core mein request cancellation token ko database aur HTTP calls tak propagate karna useful hota hai.

---

## Cryptography

Cryptography techniques ka collection hai jo information ki confidentiality, integrity aur authenticity protect karne mein help karta hai.

Confidentiality :- Sirf authorized person data read kar sake.
Integrit :- Data unauthorized tarike se modify hua hai ya nahi, detect karna.
Example: HMAC se API message ki authenticity aur integrity verify karna.
Authenticity :- Verify karna ki message ya signature expected key holder se aaya hai.
Example: Digital signature verify karna.

Hashing — SHA-256 aur SHA-512

Hash function input data ko fixed-length digest mein convert karta hai. Input chhota ho ya bada, SHA-256 ka output 256 bits (32 bytes) hota hai; SHA-512 ka output 512 bits (64 bytes) hota hai.

Hashing kaise kaam karti hai?
Maan lo input hai:
Hello World

SHA-256 is input ka 32-byte digest generate karega. Agar input ka ek character bhi change kar do, output generally completely different dikhega.

using System;

using System.Security.Cryptography;
using System.Text;
class Program
{
static void Main()
{
string message = "Hello World";
Console.WriteLine("Simple plane message :- " + message);

        byte[] input = Encoding.UTF8.GetBytes(message);
        byte[] hash = SHA256.HashData(input);
        string hexHash = Convert.ToHexString(hash);

        //Agar SHA12 used krna hi to apne iss trh used krenge
        ///SHA12.HashData(input) yesa kr dege...

        Console.WriteLine($"SHA-256: {hexHash}");
        Console.WriteLine($"Hash length: {hash.Length} bytes");
    }

}

4. HMAC — Hash-Based Message Authentication Code
   HMAC ek secret key aur hash function ka use karke message authentication code generate karta hai.

Normal SHA-256 HMAC-SHA256
Secret key required nahi Secret key required
Data ka digest banata hai Message integrity aur shared-key authenticity verify karne mein help karta hai
Koi bhi input ka hash calculate kar sakta hai Sirf secret key rakhne wala valid HMAC generate kar sakta hai

key banao → message ka HMAC nikalo → receive hue MAC se compare karo (constant-time mein) → match hua to data valid hai.

using System;
using System.Security.Cryptography; // HMAC, random bytes, secure compare ke liye
using System.Text; // string <-> byte[] conversion ke liye

class Program
{
static void Main()
{
// 32 bytes (256-bit) ki cryptographically secure random secret key banata hai.
// Real app mein yeh key server pe safe store hoti hai (env var / key vault),
// har baar random generate nahi hoti.
byte[] key = RandomNumberGenerator.GetBytes(32);

        // Jis data ko sign/verify karna hai, use UTF-8 bytes mein convert kar rahe hain,
        // kyunki HMAC string pe nahi, bytes pe kaam karta hai.
        byte[] message = Encoding.UTF8.GetBytes(
            "orderId=101&status=paid"
        );

        // MAC (Message Authentication Code) yahan store hoga
        byte[] mac;

        // HMAC-SHA256 object secret key ke saath banaya.
        // 'using' lagaya taaki kaam ke baad key material memory se properly dispose ho jaye.
        using (var hmac = new HMACSHA256(key))
        {
            // Message + key se 32-byte ka signature (tag) calculate hota hai.
            // Same key + same message = hamesha same MAC.
            // Message ya key badli to MAC poori tarah alag aayega.
            mac = hmac.ComputeHash(message);
        }

        // MAC ko readable hex string mein print karta hai
        Console.WriteLine(Convert.ToHexString(mac));

        // Demo ke liye hum apna hi MAC "received" maan rahe hain.
        // Real case mein yeh client/webhook request ke header se aayega.
        byte[] receivedMac = mac; // Demo only: real request se aayega

        // Fixed-time comparison: dono MAC compare karta hai, lekin time hamesha same lagta hai
        // chahe pehla byte hi mismatch ho ya aakhri. Isse timing attack se bachav hota hai.
        // Normal == ya SequenceEqual use mat karna, woh jaldi return karte hain aur leak karte hain.
        bool valid = CryptographicOperations.FixedTimeEquals(
            mac,
            receivedMac
        );

        // true = message authentic hai aur tamper nahi hua
        Console.WriteLine($"Valid: {valid}");
    }

}

5. Password hashing — PBKDF2, bcrypt aur Argon2

Plain SHA-256 password ke liye unsafe kyun hai?
SHA-256 bahut fast hai. Agar attacker database se hashes chura le, toh woh bahut saare password guesses quickly hash karke compare kar sakta hai.

5.1 PBKDF2 in C#
PBKDF2 ka full form Password-Based Key Derivation Function 2 hai. Ye password, salt, iteration count aur hash algorithm ka use karke derived key generate karta hai.

Modern .NET mein Rfc2898DeriveBytes.Pbkdf2 static method use kar sakte ho.

using System;
using System.Security.Cryptography;

class Program
{
static void Main()
{
string password = "Example-password-for-demo";

        // Har password ke liye fresh random salt
        byte[] salt = RandomNumberGenerator.GetBytes(16);

        int iterations = 600_000;
        int outputLength = 32;

        byte[] passwordHash = Rfc2898DeriveBytes.Pbkdf2(
            password: password,
            salt: salt,
            iterations: iterations,
            outputLength: outputLength,
            hashAlgorithm: HashAlgorithmName.SHA256
            );

        Console.WriteLine("Plain Password :- " + password);
        Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");
        Console.WriteLine($"Hash: {Convert.ToBase64String(passwordHash)}");


    }

}

Verify password kaise krenge ....

using System;
using System.Security.Cryptography; // PBKDF2, random salt, secure compare ke liye
using verify; // humare PasswordVerifier class ka namespace

class Program
{
static void Main()
{
string password = "Example-password-for-demo";

        // Har password ke liye fresh random 16-byte salt.
        // Salt ki wajah se same password ka hash alag-alag users ke liye alag aata hai
        // (rainbow table attack se bachav).
        byte[] salt = RandomNumberGenerator.GetBytes(16);

        // PBKDF2 ko jaanbujhkar slow banane ke liye iterations zyada rakhte hain.
        // 600,000 = PBKDF2-SHA256 ke liye OWASP ki recommended value.
        int iterations = 600_000;

        // Output hash ki length bytes mein (32 = 256-bit)
        int outputLength = 32;

        // Password + salt + iterations se hash derive hota hai.
        // Same inputs = hamesha same hash, par hash se password wapas nahi nikal sakte.
        byte[] passwordHash = Rfc2898DeriveBytes.Pbkdf2(
            password: password,
            salt: salt,
            iterations: iterations,
            hashAlgorithm: HashAlgorithmName.SHA256,
            outputLength: outputLength
        );

        // Demo ke liye print kar rahe hain. Real app mein plain password kabhi log/print mat karna.
        Console.WriteLine("Plain Password :- " + password);
        Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");   // DB mein salt bhi store hota hai
        Console.WriteLine($"Hash: {Convert.ToBase64String(passwordHash)}");

        // Login jaisa step: entered password ko stored salt/hash/iterations se verify karo
        bool ans = PasswordVerifier.Verify(
            password,
            salt,
            passwordHash,
            iterations
        );

        Console.WriteLine("Ans :- " + ans); // true = password sahi hai
    }

}

# C# Channels in .NET — Complete Notes

## 1. What is a Channel?

`Channel<T>` is an asynchronous producer-consumer mechanism available in .NET through the `System.Threading.Channels` namespace.

It allows different parts of an application to exchange data safely without manually managing a shared queue using locks.

A channel temporarily stores items until a consumer reads them.

### Main components

- **Producer:** Creates data and writes it to the channel.
- **Channel:** Transfers and temporarily buffers data.
- **Consumer:** Reads data from the channel and processes it.

### Example: Bakery

Imagine a bakery:

- Chef prepares bread — Producer.
- Bread counter temporarily holds bread — Channel.
- Customers collect bread — Consumers.

The same pattern can be used in backend applications:

`API Endpoint → Channel → Background Worker → Processing Service`

Examples include notifications, file processing, logging, and background jobs.

## 2. Why use Channels?

### Thread-safe communication

Channels provide synchronization for their supported concurrent read/write operations. Multiple producers and consumers can communicate through a channel without manually locking a shared collection.

### Asynchronous processing

Methods such as `WriteAsync()` and `ReadAsync()` support asynchronous waiting.

When a producer has to wait for capacity, or a consumer has to wait for an item, the operation can suspend asynchronously instead of blocking a thread while waiting.

### Backpressure

A bounded channel can slow down producers when consumers cannot process items quickly enough.

This helps control the number of buffered items and reduces uncontrolled memory growth.

### Separation of responsibilities

The producer does not need to know how the consumer processes the item. Similarly, the consumer does not need to know how the item was created.

This makes application components easier to maintain and test.

## 3. Import the namespace

```csharp
using System.Threading.Channels;
```

The namespace contains the channel types, including:

- `Channel<T>`
- `ChannelReader<T>`
- `ChannelWriter<T>`
- `BoundedChannelOptions`
- `UnboundedChannelOptions`

## 4. Create a Channel

There are two common ways to create a channel.

### A. Unbounded Channel

```csharp
var channel = Channel.CreateUnbounded<string>();
```

An unbounded channel has no configured maximum buffer capacity.

Advantages:

- Simple to configure.
- Writes normally do not wait for buffer capacity.

Disadvantages:

- If producers continuously outpace consumers, buffered items can consume excessive memory.
- It does not provide capacity-based backpressure.

Use it only when the expected volume and memory usage are understood.

### B. Bounded Channel

```csharp
var channel = Channel.CreateBounded<string>(100);
```

This creates a channel with a maximum buffered capacity of 100 items.

A more configurable example:

```csharp
var channel = Channel.CreateBounded<string>(
    new BoundedChannelOptions(100)
    {
        FullMode = BoundedChannelFullMode.Wait,
        SingleWriter = false,
        SingleReader = false
    });
```

The capacity of 100 is only an example. Choose the capacity according to workload, item size, processing speed, and available memory.

### BoundedChannelFullMode options

| Mode         | Behaviour                                                           |
| ------------ | ------------------------------------------------------------------- |
| `Wait`       | Wait until buffer capacity becomes available.                       |
| `DropOldest` | Drop the oldest buffered item when a new item needs to be admitted. |
| `DropNewest` | Drop the newest item already buffered to make room.                 |
| `DropWrite`  | Drop the incoming item when the channel is full.                    |

For important work such as payments, orders, and audit events, do not casually use a dropping mode. Dropping an item means it may never be processed.

## 5. ChannelWriter\<T>

A producer uses `ChannelWriter<T>` to send items to the channel.

Important methods:

- `WriteAsync(item)` — asynchronously writes an item; it can wait for capacity.
- `TryWrite(item)` — attempts an immediate write without waiting.
- `Complete()` — signals that no more items will be written.
- `TryComplete()` — attempts to complete the writer and reports whether it succeeded.

Example:

```csharp
await writer.WriteAsync("Bread-1");
```

With a bounded channel in `Wait` mode, this operation waits asynchronously if the channel is full.

`TryWrite()` is useful when the producer cannot wait, but the application must handle a failed write rather than silently ignoring it.

## 6. ChannelReader\<T>

A consumer uses `ChannelReader<T>` to read items.

Important methods:

- `ReadAsync()` — reads one item, waiting if necessary.
- `TryRead()` — attempts to read an item immediately.
- `WaitToReadAsync()` — waits until an item may be available.
- `ReadAllAsync()` — asynchronously iterates through the channel's available items until it completes.

Example:

```csharp
await foreach (var item in reader.ReadAllAsync())
{
    Console.WriteLine(item);
}
```

The loop finishes when the writer has completed and all buffered items have been consumed.

If the channel completes with an error, readers can observe that failure. Consumers should not assume every channel completes successfully.

## 7. Producer-Consumer workflow

1. Create a channel.
2. Get its writer and reader.
3. Start one or more producer tasks.
4. Start one or more consumer tasks.
5. Producers write items.
6. Consumers read and process items.
7. The producer completes the writer when no more items will be written.
8. Consumers finish after the channel is completed and drained.

A critical rule: if a producer finishes normally, it must signal completion. Otherwise, consumers may continue waiting for more items.

## 8. Multiple consumers

Multiple consumers can read from the same channel.

For example, two workers can process notifications from a shared channel. Each item is normally delivered to one reader, not broadcast to every reader.

This is useful for distributing work, but it does not guarantee that processing completes in the same order in which items were written. Concurrent consumers can finish their work in a different order.

If every subscriber must receive every event, use a separate channel per subscriber or an appropriate pub/sub design.

## 9. Cancellation and error handling

Long-running producers and consumers should generally support `CancellationToken`.

Cancellation allows an application to request that work stop, for example during application shutdown.

Important considerations:

- Pass the token to asynchronous operations that support it.
- Complete the writer when production finishes.
- Handle expected cancellation separately from unexpected errors.
- Ensure producer and consumer failures are logged and observed.
- Decide what happens to buffered items during shutdown.

Cancellation does not automatically undo work that has already been processed.

## 10. Channels in ASP.NET Core production applications

A common architecture is:

`HTTP Endpoint → Bounded Channel → BackgroundService → Business Service`

Example use cases:

- Send notifications outside the HTTP request path.
- Resize uploaded images.
- Process imported files.
- Buffer application events for asynchronous processing.

A `BackgroundService` can continuously read items using `ReadAllAsync(stoppingToken)`.

For reliable processing, also consider retries, idempotency, monitoring, graceful shutdown, and what happens when the application stops.

### Important limitation

A standard channel is an in-memory mechanism, not a durable message broker.

If the application process crashes or restarts, buffered items can be lost. Returning HTTP `202 Accepted` after placing an item in an in-memory channel means the item was accepted for in-process processing; it does not guarantee durable storage or successful delivery.

For work that must survive process failures, consider a durable queue or message broker such as RabbitMQ, Kafka, or a Redis-backed job queue, depending on the requirements.

## 11. Channel vs BlockingCollection\<T>

| Feature       | Channel\<T>                    | BlockingCollection\<T>                  |
| ------------- | ------------------------------ | --------------------------------------- |
| Primary model | Asynchronous producer-consumer | Blocking producer-consumer              |
| Waiting       | Async-friendly                 | Can block threads                       |
| Async APIs    | Yes                            | Not designed around async waiting       |
| Common use    | Modern .NET async pipelines    | Synchronous producer-consumer workflows |

Use Channels when the surrounding application is asynchronous. Use blocking collections when a synchronous blocking model is appropriate.

## 12. Channel vs a durable queue

| Feature                  | Channel\<T>                   | Durable job queue                                  |
| ------------------------ | ----------------------------- | -------------------------------------------------- |
| Storage                  | In process memory             | Usually external storage                           |
| Survives process restart | No guarantee                  | Depends on configuration and durability guarantees |
| Distributed workers      | Not directly across processes | Often supported                                    |
| Best suited for          | In-process communication      | Cross-process or durable background work           |

## 13. Production checklist

Before using Channels in a production application:

- Prefer bounded capacity when buffering could grow without limit.
- Choose the full-buffer policy deliberately.
- Handle unsuccessful `TryWrite()` calls.
- Complete the writer when production ends.
- Use cancellation tokens for cooperative shutdown.
- Handle and log processing failures.
- Add retry and idempotency logic where appropriate.
- Monitor queue depth, processing latency, failures, and dropped items.
- Do not treat an in-memory channel as durable storage.
- Avoid sharing mutable item objects between components unless their concurrency behaviour is safe.

## 14. Quick revision

**What is a Channel?**
An asynchronous mechanism for transferring items between producers and consumers.

**What is a producer?**
The component that writes items to the channel.

**What is a consumer?**
The component that reads and processes items.

**Bounded vs unbounded?**
Bounded channels have a configured maximum buffer capacity; unbounded channels do not.

**What is backpressure?**
A mechanism that prevents producers from overwhelming consumers, such as waiting when a bounded channel is full.

**Does every item reach every consumer?**
No. Consumers reading the same channel compete to receive available items.

**Does a Channel survive application restart?**
No. It is an in-memory mechanism.

**Is Channel a replacement for Kafka or a durable job queue?**
No. It solves in-process communication, while durable brokers and job queues address additional requirements such as cross-process delivery and persistence.
