# Design Patterns in C# --- Introduction aur Singleton Pattern

## 1. Design Pattern kya hota hai?

**Design Pattern ek reusable, proven approach hai jo software
development mein baar-baar aane wali common design problems ko solve
karne mein help karta hai.**

Design patterns ready-made code nahi hote. Ye ek general blueprint dete
hain jise hum apni requirement ke according implement karte hain.

Design patterns code ko maintainable, flexible aur extensible banane
mein help kar sakte hain. Ye unnecessary coupling kam kar sakte hain aur
team ko common design ideas communicate karne mein help karte hain.
Lekin har problem mein pattern use karna zaroori nahi; unnecessary
patterns code ko over-engineered bana sakte hain.

## 2. Design Patterns ki 3 categories

Classic Gang of Four (GoF) book mein 23 design patterns hain.

### A. Creational Design Patterns

Object creation ki process par focus karte hain: object kab, kis tarah
aur kis logic se create hoga.

1.  Singleton
2.  Factory Method
3.  Abstract Factory
4.  Prototype
5.  Builder

> Correct name **Abstract Factory** hai, "Abstract Method" nahi.

### B. Structural Design Patterns

Classes aur objects ko combine/organize karne par focus karte hain.
Composition, interfaces aur relationships important hote hain.

1.  Adapter
2.  Bridge
3.  Composite
4.  Decorator
5.  Facade
6.  Flyweight
7.  Proxy

### C. Behavioral Design Patterns

Objects ke beech communication, responsibilities aur behavior ko
organize karte hain.

1.  Chain of Responsibility
2.  Command
3.  Interpreter
4.  Iterator
5.  Mediator
6.  Memento
7.  Observer
8.  State
9.  Strategy
10. Template Method
11. Visitor

  Category     Main question
  ------------ ----------------------------------------------
  Creational   Object kaise create hoga?
  Structural   Objects/classes ko kaise combine karenge?
  Behavioral   Objects kaise communicate ya behave karenge?

------------------------------------------------------------------------

# 3. Singleton Design Pattern

## 3.1 Singleton kya hota hai?

**Singleton Pattern ensure karta hai ki class ka ek hi shared instance
ho aur us instance tak ek common access point mile.**

Iske do main goals hain:

1.  Class ka ek hi instance maintain karna.
2.  Us instance ko access karne ke liye common point provide karna.

Singleton ka scope implementation par depend karta hai. Ek static
singleton automatically multiple processes ya multiple servers ke beech
shared nahi hota.

## 3.2 Singleton ke bina kya hota hai?

``` csharp
public class AppSettings
{
    public string AppName { get; set; } = "MyApp";
}

var settings1 = new AppSettings();
var settings2 = new AppSettings();

Console.WriteLine(ReferenceEquals(settings1, settings2)); // False
```

Dono variables alag objects ko refer karte hain.

**Important:** Multiple objects banne ka matlab ye nahi ki memory zaroor
full ho jayegi. Har object memory use karta hai, lekin impact object ke
size, number aur lifetime par depend karta hai. Singleton ka main
purpose sirf memory bachana nahi, balki single shared instance enforce
karna hai.

## 3.3 Singleton ke main parts

-   **Private constructor:** doosri classes `new AppSettings()` se
    object create nahi kar sakti.
-   **Static instance:** class apna shared instance maintain karti hai.
-   **Public static property/method:** doosri classes shared instance
    access karti hain.
-   **Sealed class:** class ko inherit karne se rokta hai. Singleton
    implementations mein commonly use hota hai.

## 3.4 Simple thread-safe Singleton --- `static readonly`

``` csharp
using System;

namespace SingletonDemo
{
    public sealed class AppSettings
    {
        // Class ka shared instance.
        private static readonly AppSettings _instance = new AppSettings();

        // Bahar se new AppSettings() karna allowed nahi.
        private AppSettings()
        {
            Console.WriteLine("AppSettings instance created.");
        }

        // Shared instance access karne ka common point.
        public static AppSettings Instance => _instance;

        public void ShowMessage()
        {
            Console.WriteLine("Using the shared AppSettings instance.");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            AppSettings first = AppSettings.Instance;
            AppSettings second = AppSettings.Instance;

            first.ShowMessage();

            Console.WriteLine(
                ReferenceEquals(first, second)
            ); // True
        }
    }
}
```

### Code kaise kaam karta hai?

1.  `private AppSettings()` external code ko constructor call karne se
    rokta hai.
2.  `static readonly` field ek shared instance store karti hai.
3.  `Instance` property us instance ko return karti hai.
4.  `ReferenceEquals(first, second)` `True` deta hai kyunki dono same
    object ko refer karte hain.
5.  .NET static field initialization ko thread-safe banata hai. Is
    implementation mein manually `lock` lagane ki zaroorat nahi.

## 3.5 `lock` ke saath lazy Singleton

Lazy initialization ka matlab hai object tab create karna jab pehli baar
zaroorat pade. Unsynchronized lazy Singleton mein multiple threads same
time par object create kar sakte hain. `lock` is initialization ko
synchronize kar sakta hai.

``` csharp
using System;

namespace SingletonDemo
{
    public sealed class AppSettings
    {
        private static AppSettings? _instance;
        private static readonly object _lock = new object();

        private AppSettings()
        {
            Console.WriteLine("AppSettings instance created.");
        }

        public static AppSettings Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new AppSettings();
                    }

                    return _instance;
                }
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            AppSettings first = AppSettings.Instance;
            AppSettings second = AppSettings.Instance;

            Console.WriteLine(
                ReferenceEquals(first, second)
            ); // True
        }
    }
}
```

### `lock` kyun use kiya?

Maan lo do threads same time par `Instance` access karte hain. Lock ke
bina dono `_instance == null` check pass karke alag objects create kar
sakte hain.

`lock (_lock)` ek time par sirf ek thread ko critical section mein enter
karne deta hai. Pehla thread instance create karta hai; doosra lock
milne par existing instance return karta hai.

Is simple example mein har property access par lock acquire hota hai. Ye
samajhne ke liye straightforward approach hai, lekin har situation mein
most performant approach nahi.

## 3.6 Lazy initialization ka convenient option: `Lazy<T>`

Modern C#/.NET mein lazy, thread-safe initialization ke liye `Lazy<T>`
ek clean option hai.

``` csharp
using System;

public sealed class AppSettings
{
    private static readonly Lazy<AppSettings> _lazyInstance =
        new Lazy<AppSettings>(() => new AppSettings());

    private AppSettings()
    {
        Console.WriteLine("AppSettings instance created.");
    }

    public static AppSettings Instance => _lazyInstance.Value;
}
```

Default `Lazy<T>` initialization mode thread-safe hota hai. Instance
pehli baar `Instance` property access hone par create hota hai.

## 3.7 Singleton kahan use kar sakte hain?

Jab genuinely ek shared instance ki requirement ho, jaise: - Immutable
application-wide configuration access. - Aisi shared service jiska
single-instance lifetime intentional ho. - In-process cache coordinator,
agar concurrency aur lifecycle carefully designed ho.

ASP.NET Core mein manually Singleton likhne ke bajay aksar Dependency
Injection (DI) container lifetime manage kar sakta hai:

``` csharp
builder.Services.AddSingleton<IAppSettings, AppSettingsService>();
```

DI container configured service-provider scope mein service ka ek
instance reuse karta hai. Exact lifetime hosting aur DI setup par depend
karti hai.

## 3.8 Kya database connection ko Singleton banana chahiye?

**Aam taur par raw database connection ko application-wide Singleton
banana sahi approach nahi hai.**

-   ADO.NET providers connection pooling support karte hain.
-   Application zaroorat par connection open karti hai aur kaam ke baad
    dispose karti hai.
-   Shared connection par concurrent operations aur transaction handling
    problems create kar sakte hain.
-   Connection pooling aur appropriate connection lifetime use karna
    usually better hota hai.

Logger/configuration service aur raw database connection alag resources
hain; dono ka lifetime same assume nahi karna chahiye.

## 3.9 Advantages aur drawbacks

### Advantages

-   Shared instance consistently reuse hota hai.
-   Accidental duplicate instances prevent hote hain.
-   Common access point milta hai.
-   Correct implementation mein initialization thread-safe ho sakti hai.

### Drawbacks

-   Global mutable state create ho sakti hai.
-   Unit testing aur test isolation difficult ho sakte hain.
-   Dependencies hidden ho sakti hain.
-   Shared object's mutable state thread-safe na ho to concurrency
    issues aa sakte hain.
-   Unnecessary Singleton tight coupling create kar sakta hai.


# Factpry Design pattern 