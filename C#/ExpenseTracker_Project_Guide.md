# 📊 Personal Finance CLI: Expense & Budget Tracker
> **Project Blueprint & Implementation Roadmap**  
> *Target: Build a clean, modular Console Application in C# practicing OOP, LINQ, File I/O, and Data Modeling.*

---

## 🎯 1. Project Overview & Learning Objectives

Is project ka main maksad ek real-world problem ko solve karna hai aur C# ke core concepts ko practically implement karna hai:
* **OOP & Clean Separation:** Business logic, storage, aur user interface (UI) ko alag rakhna.
* **Collections & Generics:** `List<T>`, `IEnumerable<T>`, `Dictionary<TKey, TValue>`.
* **Deep LINQ Practice:** `Where`, `Select`, `GroupBy`, `OrderByDescending`, `Sum`, `Any`, `FirstOrDefault`.
* **File I/O & Serialization:** Data ko `System.Text.Json` ke through JSON format me persist (save/load) karna.
* **Defensive Programming:** Input validation, custom exception handling, aur edge cases cover karna.

---

## 🏗️ 2. Recommended Project & Folder Structure

Aapko saara code ek hi `Program.cs` me nahi likhna hai. Code ko modular banane ke liye is directory structure ko follow karein:

```text
ExpenseTracker/
│
├── Models/                     # Data Blueprint (Entities & Enums)
│   ├── Enums.cs                # TransactionType, Category
│   ├── Transaction.cs          # Transaction record
│   └── Budget.cs               # Category-wise budget limit
│
├── Services/                   # Core Business Logic & Calculations
│   ├── IExpenseService.cs      # Service interface (Contract)
│   ├── ExpenseService.cs       # Business logic (Add, Remove, Calculate)
│   └── ReportService.cs        # LINQ queries & analytics logic
│
├── Data/                       # Data Persistence / Storage
│   └── JsonStorage.cs          # File read/write logic using System.Text.Json
│
├── UI/                         # Console Views & Menus
│   ├── ConsoleHelper.cs        # Color printing, table formatting, input readers
│   └── MenuHandler.cs          # Main menu loop & navigation
│
├── Program.cs                  # Entry point (Wires up services and starts app)
└── expenses.json               # (Auto-generated at runtime to store data)
```

---

## 📋 3. Domain Models Blueprint (What Data to Model)

### A. Enums (`Enums.cs`)
1. **`TransactionType`**:
   * Values: `Income`, `Expense`
2. **`Category`**:
   * Values: `Food`, `Rent`, `Utilities`, `Salary`, `Shopping`, `Entertainment`, `Health`, `Investment`, `Other`

---

### B. Transaction Entity (`Transaction.cs`)
Ek transaction ka record kaisa hona chahiye:
* **`Id`**: `int` ya `Guid` (Unique identifier)
* **`Title`**: `string` (Short description, e.g., "Grocery shopping")
* **`Amount`**: `decimal` (Financial data ke liye hamesha `decimal` use karein, `double` nahi)
* **`Type`**: `TransactionType` (Income ya Expense)
* **`Category`**: `Category`
* **`Date`**: `DateTime` (Kab transaction hua)
* **`Notes`**: `string?` (Optional extra note)

---

### C. Budget Entity (`Budget.cs`)
Monthly budget limit track karne ke liye:
* **`Category`**: `Category`
* **`MonthlyLimit`**: `decimal`

---

## 💾 4. Data Storage Format (JSON Schema)

Data ko locally save karne ke liye `expenses.json` file create hogi. Uska structure kuch aisa dikhna chahiye:

```json
{
  "Budgets": [
    {
      "Category": "Food",
      "MonthlyLimit": 5000.00
    },
    {
      "Category": "Entertainment",
      "MonthlyLimit": 2000.00
    }
  ],
  "Transactions": [
    {
      "Id": 1,
      "Title": "Monthly Salary",
      "Amount": 60000.00,
      "Type": "Income",
      "Category": "Salary",
      "Date": "2026-10-01T10:00:00",
      "Notes": "October salary credited"
    },
    {
      "Id": 2,
      "Title": "Supermarket Grocery",
      "Amount": 2450.50,
      "Type": "Expense",
      "Category": "Food",
      "Date": "2026-10-05T18:30:00",
      "Notes": "Vegetables and dairy"
    }
  ]
}
```

---

## 🚀 5. Step-by-Step Implementation Roadmap

### Phase 1: Setup & Data Modeling
1. Terminal me console project banayein: `dotnet new console -n ExpenseTracker`.
2. `Models/` folder banakar `Enums.cs`, `Transaction.cs`, aur `Budget.cs` define karein.
3. Constructor aur Properties ke liye proper access modifiers (`public`, `get; init;` ya `get; set;`) decide karein.

---

### Phase 2: Storage Layer (`Data/JsonStorage.cs`)
* Ek generic ya dedicated storage class banayein.
* **Kaam kya hoga:**
  * Application start hone par check karein: *Kya `expenses.json` exist karti hai?*
    * Agar nahi: to empty data file create karein.
    * Agar haan: to file read karke object list deserialize karein (`JsonSerializer.Deserialize<T>`).
  * Jab bhi koi new transaction add ya delete ho, file me write/save karein (`JsonSerializer.Serialize<T>`).
* **Tip:** JSON formatting ke liye `WriteIndented = true` option set karein taaki file readable rahe.

---

### Phase 3: Core Service Layer (`Services/ExpenseService.cs`)
* In-memory list maintain karein: `List<Transaction>` aur `List<Budget>`.
* Methods jo aapko implement karne hain:
  1. `AddTransaction(Transaction item)`
  2. `DeleteTransaction(int id)`
  3. `GetAllTransactions()` -> Hamesha `IEnumerable<Transaction>` return karein taaki caller direct list ko modify na kar sake.
  4. `SetBudget(Category category, decimal limit)`
  5. `CheckBudgetAlert(Category category)` -> Check kare ki kya current month me us category ka expense limit cross kar chuka hai.

---

### Phase 4: LINQ Reporting Engine (`Services/ReportService.cs`)
*Yeh aapke `IEnumerable` aur LINQ skills ka main test hai!*  
Koi bhi `foreach` loop use kiye bina, purely LINQ methods se ye reports banayein:

1. **Total Balance:**
   * Total Income - Total Expense.
2. **Current Month Summary:**
   * Sirf current month (`DateTime.Now.Month` & `DateTime.Now.Year`) ke transactions filter karna.
3. **Category-wise Spending Breakdown:**
   * Sirf `Expense` type ke items ko `GroupBy(t => t.Category)` karein.
   * Har group ka `Sum(t => t.Amount)` nikalein.
   * Result ko highest expense se lowest me order karein (`OrderByDescending`).
4. **Top N Expenses:**
   * Top 3 ya Top 5 sabse badi expenses retrieve karna (`Take(n)`).
5. **Budget vs Actual Comparison:**
   * Har category ke budget ko uske actual expense ke sath map karein aur remaining balance ya percentage spent show karein.

---

### Phase 5: User Interface & Menu (`UI/`)
User ko console par clean menu dikhayein:

```text
========================================
   💰 EXPENSE & BUDGET TRACKER CLI
========================================
[1] Add New Transaction (Income / Expense)
[2] View All Transactions
[3] View Reports & Analytics (LINQ Powered)
[4] Set / View Category Budgets
[5] Delete a Transaction
[6] Export / Backup Data
[0] Exit
========================================
Choose an option: _
```

* **Sub-menu for Reports:**
  * [A] Current Month Overview
  * [B] Category-wise Expense Chart
  * [C] Top 5 Highest Expenses
  * [D] Custom Date Range Report

---

## 🧠 6. Self-Challenge Checklist (LINQ Mastery)

Try writing each of these purely with LINQ:

- [ ] **Filter by Date Range:**  
  `transactions.Where(t => t.Date >= startDate && t.Date <= endDate)`
- [ ] **Category-wise Aggregation:**  
  `GroupBy` + `Select` with Anonymous Type ya Record:  
  `new { Category = g.Key, TotalSpent = g.Sum(x => x.Amount) }`
- [ ] **Check Over-budget without looping:**  
  `Any()` condition se verify karna ki koi category limit surpass hui hai ya nahi.
- [ ] **Daily Average Spending:**  
  Total expense divided by number of active days.

---

## 🛡️ 7. Edge Cases & Validation Rules

Aapko in cases ko handle karna hoga apne code me:
1. **Invalid Amount:** User `0` ya negative amount dale to validation error throw karein.
2. **Future Dates:** Agar user galti se aane wali date daal de to alert karein ya confirm karein.
3. **Empty Titles:** Title null, empty ya whitespace nahi hona chahiye.
4. **File Missing / Corrupted:** Agar JSON file corrupt ho jaye to app crash na ho, balki clean error message dekar backup restore ka prompt de.
5. **Delete non-existent ID:** Agar user koi aisi ID delete karne ki koshish kare jo exist nahi karti to user-friendly message show ho.

---

## 🌟 8. Next-Level Upgrades (Once Base is Ready)

1. **Table Formatting:** Console output ko visually clean banayein (Column padding use karke: `Console.WriteLine("{0,-10} | {1,-20} | {2,10}", ...)`).
2. **CSV Export:** Report ko Excel-compatible CSV file me export karne ka option add karein.
3. **Spectre.Console (Optional):** Colorful tables, interactive menus, aur progress bars add karein.

---
*Happy Coding! Start by creating the project and modeling the classes.*
