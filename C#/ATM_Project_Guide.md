# 🏧 ATM Simulation System (Console CLI)
> **Comprehensive Project Blueprint & Architecture Guide**  
> *Target: Build an enterprise-grade, secure, and modular ATM Console Application in C# practicing OOP, State Machines, Algorithms (Cash Dispenser), File I/O, and LINQ.*

---

## 🎯 1. Project Overview & Learning Objectives

ATM Simulation ek classic project hai jo real-world banking workflows aur terminal hardware behavior ko mimic karta hai. Is project ko banakar aap seekhenge:
* **Object-Oriented Design:** Real-world entities (Bank, Account, Card, Cash Dispenser, Transaction) ko clean classes aur interfaces me design karna.
* **State Machine Pattern:** ATM ke distinct states (Idle/Card Inserted/Authenticated/Dispensing/Admin) ko systematically manage karna.
* **Algorithmic Thinking:** Cash dispenser me optimal notes count calculate karna (e.g. ₹500, ₹200, ₹100 notes ka optimal distribution).
* **Security Practices:** Password/PIN masking, 3-attempt account freeze, PIN hashing concepts.
* **Transactional Integrity:** Ek account se dusre account me transfer karte waqt atomic updates ensure karna.
* **LINQ for Banking:** Mini-statements (last 5 transactions), daily limit calculation (last 24 hours me total withdraw check karna).

---

## 🏗️ 2. Recommended Project & Folder Structure

```text
AtmSimulation/
│
├── Models/                        # Domain Entities & Value Objects
│   ├── Enums.cs                   # AccountType, TransactionType, AtmState
│   ├── Account.cs                 # Bank Account details
│   ├── Card.cs                    # Card details & PIN hash
│   ├── TransactionRecord.cs       # Ledger entry for each action
│   └── CashCassette.cs            # Denominations count in ATM
│
├── Services/                      # Business & Hardware Logic
│   ├── IAtmService.cs             # ATM operations contract
│   ├── AtmService.cs              # Core operations (Withdraw, Deposit, Transfer)
│   ├── CashDispenserService.cs    # Denomination calculation algorithm
│   └── AuthService.cs             # PIN verification & Card lockout logic
│
├── Data/                          # File Persistence Layer
│   ├── IBankRepository.cs         # Repository contract
│   └── JsonBankRepository.cs      # Read/write accounts & ATM cash via System.Text.Json
│
├── Security/                      # Safety utilities
│   └── PasswordHelper.cs          # Console PIN masking (*), hashing logic
│
├── UI/                            # Presentation Layer
│   ├── ReceiptPrinter.cs          # Formatted ASCII receipt generation
│   ├── ConsoleUiHelper.cs         # Screen clearing, colors, table rendering
│   └── AtmStateMachine.cs         # Main event loop & menu navigation
│
├── Program.cs                     # Bootstrapper & entry point
├── accounts.json                  # Persistent bank accounts data
└── atm_cash.json                  # Physical cash inventory inside ATM
```

---

## 🔄 3. ATM Flow & State Progression

Ek real ATM system state-driven hota hai. Har state par user ko sirf valid actions allow hote hain:

```text
[State 1: IDLE / WELCOME]
       │
       ▼  User inserts Card Number (or selects test card)
[State 2: PIN_ENTRY]
       │
       ├── (3 Incorrect Attempts) ──► [ACCOUNT FROZEN] ──► Eject Card / Exit
       │
       ▼  (PIN Valid)
[State 3: AUTHENTICATED_MENU]
       │
       ├──► 1. Check Balance
       ├──► 2. Fast Cash (₹500, ₹1000, ₹2000, ₹5000)
       ├──► 3. Custom Withdrawal ──► Check Daily Limit ──► Check ATM Cash ──► [DISPENSE]
       ├──► 4. Cash Deposit ──► Update Account & ATM Cassette
       ├──► 5. Fund Transfer (Account to Account)
       ├──► 6. Mini Statement (Last 5 transactions)
       ├──► 7. Change PIN
       └──► 8. Exit / Eject Card
       │
       ▼ (Special Admin PIN)
[State 4: ADMIN_PANEL]
       ├──► View ATM Total Cash & Denominations Breakdown
       ├──► Refill ATM Cash Cassettes
       └──► Unlock Frozen Accounts
```

---

## 📋 4. Domain Models Blueprint (What to Define)

### A. Enums (`Models/Enums.cs`)
1. **`AccountType`**: `Savings`, `Current`
2. **`TransactionType`**: `Withdrawal`, `Deposit`, `TransferIn`, `TransferOut`, `PinChange`, `BalanceInquiry`
3. **`AtmState`**: `Welcome`, `PinPrompt`, `CustomerMenu`, `AdminMenu`, `CardLocked`

---

### B. Account Entity (`Models/Account.cs`)
* **`AccountNumber`**: `string` (e.g. `"1001234567"`)
* **`AccountHolderName`**: `string`
* **`Type`**: `AccountType`
* **`Balance`**: `decimal`
* **`Card`**: `Card` (Linked card details)
* **`Transactions`**: `List<TransactionRecord>`
* **`DailyWithdrawalLimit`**: `decimal` (e.g. ₹25,000 per day)

---

### C. Card Entity (`Models/Card.cs`)
* **`CardNumber`**: `string` (16-digit formatted or unique ID)
* **`PinHash`**: `string` (Plain text PIN store mat karein, hashed format)
* **`FailedAttempts`**: `int` (Max 3 allowed)
* **`IsLocked`**: `bool`
* **`ExpiryDate`**: `DateTime`

---

### D. Cash Cassette / ATM Inventory (`Models/CashCassette.cs`)
ATM ke paas finite physical cash hota hai:
* **`Denomination`**: `int` (₹2000, ₹500, ₹200, ₹100)
* **`NotesAvailable`**: `int` (Kitne notes bache hain)
* *Calculated Total*: `Denomination * NotesAvailable`

---

### E. Transaction Record (`Models/TransactionRecord.cs`)
* **`TransactionId`**: `Guid` ya `long`
* **`Timestamp`**: `DateTime`
* **`Type`**: `TransactionType`
* **`Amount`**: `decimal`
* **`BalanceAfter`**: `decimal`
* **`TargetAccountNumber`**: `string?` (Fund transfer ke case me receiver)
* **`Remarks`**: `string`

---

## 💡 5. Core Algorithms & Logic to Implement

### 🧠 Algorithm 1: Smart Cash Dispenser (Greedy Note Dispensation)
Jab user custom amount daale (e.g., ₹3,700), to ATM ko optimal note distribution calculate karna hota hai:
1. **Condition 1:** Amount ₹100 ka multiple hona chahiye (`amount % 100 == 0`).
2. **Condition 2 (Greedy Check):**
   * Sabse badi available denomination se shuru karein (e.g., ₹2000 ➡️ ₹500 ➡️ ₹200 ➡️ ₹100).
   * Check karein ki ATM ke paas us denomination ke kitne notes physically maujood hain.
   * Required count aur available count ka minimum use karein.
   * Remaining amount next choti denomination par pass karein.
3. **Condition 3:** Agar pura amount exact notes me distribute nahi ho sakta, to transaction cancel karein aur message dein: *"Unable to dispense requested amount with available denominations."*

---

### 🛡️ Algorithm 2: Console PIN Masking (`Security/PasswordHelper.cs`)
User jab PIN type kare, to console par actual digits show na hon, balki asterisk (`*`) ya hidden cursor show ho:
* `Console.ReadKey(intercept: true)` use karein.
* Har character press hone par buffer me append karein aur console par `*` print karein.
* Backspace press hone par console se last char delete karein aur buffer se remove karein.
* Enter press hone par finalized PIN string return karein.

---

### 💸 Algorithm 3: Atomic Fund Transfer
Account A se Account B me transfer karte waqt consistency zaroori hai:
1. Target account number exist karta hai ya nahi?
2. Source account me required amount + minimum balance maintain ho raha hai ya nahi?
3. Source account debit karein (`TransactionType.TransferOut`).
4. Target account credit karein (`TransactionType.TransferIn`).
5. Dono accounts ko persistent file me atomically save karein.

---

## 💾 6. JSON Storage Schemas

### `accounts.json`
```json
[
  {
    "AccountNumber": "AC9876543210",
    "AccountHolderName": "Rahul Sharma",
    "Type": "Savings",
    "Balance": 45000.00,
    "DailyWithdrawalLimit": 25000.00,
    "Card": {
      "CardNumber": "4532-1234-5678-9010",
      "PinHash": "E2FC714C4727EE9395F324CD2E7F331F",
      "FailedAttempts": 0,
      "IsLocked": false,
      "ExpiryDate": "2028-12-31T00:00:00"
    },
    "Transactions": [
      {
        "TransactionId": "f47ac10b-58cc-4372-a567-0e02b2c3d479",
        "Timestamp": "2026-10-20T14:32:00",
        "Type": "Withdrawal",
        "Amount": 2000.00,
        "BalanceAfter": 45000.00,
        "Remarks": "ATM Cash Withdrawal"
      }
    ]
  }
]
```

### `atm_cash.json`
```json
{
  "Cassettes": [
    { "Denomination": 2000, "NotesAvailable": 25 },
    { "Denomination": 500,  "NotesAvailable": 120 },
    { "Denomination": 200,  "NotesAvailable": 200 },
    { "Denomination": 100,  "NotesAvailable": 350 }
  ]
}
```

---

## 🚀 7. Step-by-Step Implementation Roadmap

### Phase 1: Models & Data Repositories
* `Models/` folder banakar `Account`, `Card`, `TransactionRecord`, aur `CashCassette` classes create karein.
* `JsonBankRepository` banayein jo starting data ke sath initial JSON files auto-create kare (agar files pehle se exist na karein to 2-3 sample accounts pre-populate karein).

---

### Phase 2: Security & Authentication Engine
* `PasswordHelper` me `ReadMaskedPin()` likhein jo console par asterisks print kare.
* `AuthService` banayein:
  * `Authenticate(cardNumber, rawPin)`:
    * Check if Card exists.
    * Check `IsLocked`. Agar locked hai to direct reject karein.
    * Check PIN. Agar galat hai, to `FailedAttempts++`. Agar 3 ho jayein to `IsLocked = true` karein aur file update karein.
    * Agar sahi hai, to `FailedAttempts = 0` reset karein aur authenticated session return karein.

---

### Phase 3: Cash Dispenser Logic
* `CashDispenserService` me note distribution logic test karein:
  * Input: `decimal requestedAmount`.
  * Output: `Dictionary<int, int>` (Denomination -> Note Count, e.g. `{ [500] = 6, [200] = 3 }`).
  * Unit test cases mentally verify karein: (Kya ₹2000 ke notes khatam hone par system ₹500 aur ₹200 se dispense kar pata hai?).

---

### Phase 4: Core Banking Operations (`AtmService.cs`)
* Methods to implement:
  1. `GetBalance(account)`
  2. `WithdrawCash(account, amount)`:
     * Step a: Validate amount (Multiple of 100, > 0).
     * Step b: Validate account balance.
     * Step c: Validate Daily Withdrawal Limit using LINQ (Aaj ke total withdrawals ka sum check karein).
     * Step d: Check ATM cash availability.
     * Step e: Dispense cash, debit account, decrement ATM notes, record transaction.
  3. `DepositCash(account, denominationBreakdown)`:
     * User kitne notes kis denomination ke daal raha hai.
     * ATM cassettes increment karein aur account balance credit karein.
  4. `TransferFunds(senderAccount, receiverAccountNumber, amount)`
  5. `ChangePin(account, oldPin, newPin)`

---

### Phase 5: LINQ Analytics & Statements
* **Mini Statement (Last 5 Transactions):**  
  `account.Transactions.OrderByDescending(t => t.Timestamp).Take(5)`
* **Daily Withdrawn Sum (Last 24 Hours):**  
  `account.Transactions.Where(t => t.Type == TransactionType.Withdrawal && t.Timestamp >= DateTime.Now.AddDays(-1)).Sum(t => t.Amount)`
* **Highest Single Withdrawal:**  
  `account.Transactions.Where(t => t.Type == TransactionType.Withdrawal).Max(t => (decimal?)t.Amount) ?? 0`

---

### Phase 6: ASCII Receipt Generator (`UI/ReceiptPrinter.cs`)
Ek realistic thermal receipt format design karein jo transaction complete hone par console par render ho:

```text
***************************************************
               STATE BANK OF C# ATM               
                 TERMINAL #ATM-047                 
***************************************************
DATE: 22/10/2026                 TIME: 21:40:15
CARD NUMBER: ************9010
ACCOUNT: SAVINGS (AC******3210)
---------------------------------------------------
TRANSACTION: CASH WITHDRAWAL
STATUS: SUCCESSFUL

DISPENSED NOTES:
   ₹500 x 4 = ₹2,000.00
   ₹200 x 2 =   ₹400.00
   ₹100 x 1 =   ₹100.00
TOTAL AMOUNT:     ₹2,500.00

AVAILABLE BALANCE: ₹42,500.00
---------------------------------------------------
        THANK YOU FOR BANKING WITH US!
***************************************************
```

---

### Phase 7: Admin Panel (Privileged Mode)
Ek special Master PIN (e.g. `9999`) ya alag Card Number par Admin Mode trigger karein:
* View physical cash summary in each cassette.
* Refill notes (e.g. Add 100 notes of ₹500).
* View locked accounts list aur unhe `Unlock` karne ka option.

---

## 🛡️ 8. Critical Edge Cases to Guard Against

1. **ATM Out of Cash:** Account me balance hai par ATM machine me cash kam hai.
2. **Unsupported Denomination:** User ne ₹1,255 request kiya (₹100 ka multiple nahi hai).
3. **Daily Limit Breach:** Account me ₹1,00,000 balance hai, par daily limit ₹25,000 hai aur user ₹30,000 withdraw karne ki koshish kare.
4. **Self Transfer:** Apne hi account number par fund transfer karne se block karna.
5. **Non-Existent Receiver:** Galat account number par transfer karne par clear "Account Not Found" error.
6. **Concurrent Balance File Corruption:** Write operation complete hone se pehle application crash hone par backup safe rakhna.

---

## 🌟 9. Optional Sensory Upgrades

* **Sound Effects:** `Console.Beep(frequency, duration)` use karke:
  * Key press tick sound
  * Error buzz sound
  * Cash dispensing count sound!
* **Spectre.Console:** Blue & White themed ATM screen tables, spinner animation jab cash count ho raha ho (`"Counting cash..."`).

---
*Blueprint ready hai! Pehle `Models` aur `CashDispenserService` ka calculation logic likhein, fir UI connect karein.*
