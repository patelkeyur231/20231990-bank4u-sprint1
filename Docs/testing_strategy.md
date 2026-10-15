# Bank4U — Sprint 2 Testing Strategy

This document maps out the automated testing plan for Sprint 2's business logic
(Deposit, Withdraw, Interest, Fees) and the custom exception handling added to
the Account hierarchy. Every row below corresponds to a real `[TestMethod]` in
the `BankSprint1.Tests` project, so it can be checked directly against Visual
Studio's Test Explorer.

Two paths are tested for every account type:

- **Happy Path** — the transaction completes with no exception raised.
- **Failure Path** — the transaction breaks a business rule (insufficient
  funds, or an exceeded overdraft limit) and `Account.Withdraw` throws a
  `BankingException` subclass with an account-specific message.

A third, narrower case — withdrawing **$0** — is deliberately *not* treated as
a `BankingException`. Zero/negative amounts are an input-validation problem,
not a funds/overdraft business rule, so they are still handled the Sprint 1
way: a failed status message, no exception. This distinction is called out
explicitly in the table below.

## Test Case Table

| Test Case ID | Path | Test Method | Input Data | Expected Result | Actual Result |
|---|---|---|---|---|---|
| TC-01 | Happy | `EverydayAccountTests.Withdraw_WithinBalance_Succeeds` | Balance $100, withdraw $40 | Balance becomes $60; status contains "Successful" | Pass |
| TC-02 | Happy (edge case) | `EverydayAccountTests.Withdraw_ExactBalance_SucceedsAndLeavesZero` | Balance $75, withdraw $75 | Balance becomes $0; status contains "Successful" | Pass |
| TC-03 | Happy (edge case) | `OmniAccountTests.Withdraw_ExactlyAtOverdraftLimit_Succeeds` | Balance $100, overdraft limit $200, withdraw $300 | Balance becomes -$200; status contains "Successful" | Pass |
| TC-04 | Happy | `OmniAccountTests.CalculateInterest_BalanceAboveThreshold_OnlyAppliesToExcess` | Balance $1,500, rate 2% | Interest = $10.00 (2% of the $500 above the $1,000 threshold) | Pass |
| TC-05 | Happy | `CustomerControllerTests.CreateCustomer_AddsNewCustomer` | New customer number "C-1" | Customer created; `GetCustomerCount()` = 1 | Pass |
| TC-06 | Failure (input validation, no exception) | `EverydayAccountTests.Withdraw_ZeroAmount_FailsWithoutThrowing` | Balance $100, withdraw $0 | Balance unchanged; status contains "Amount must be greater than zero"; **no exception thrown** | Pass |
| TC-07 | Failure | `EverydayAccountTests.Withdraw_MoreThanBalance_ThrowsInsufficientFundsException` | Balance $50, withdraw $50.01 | `InsufficientFundsException` thrown; message names "Everyday Account"; balance unchanged (no fee) | Pass |
| TC-08 | Failure | `InvestmentAccountTests.Withdraw_MoreThanBalance_ThrowsInsufficientFundsException_AndChargesFee` | Balance $100, fee $25, withdraw $150 | `InsufficientFundsException` thrown; balance becomes $75 (fee charged, no overdraft) | Pass |
| TC-09 | Failure (required edge case) | `OmniAccountTests.Withdraw_OverdraftExceededByOneCent_ThrowsOverdraftLimitExceededException` | Balance $100, overdraft limit $200, fee $35, withdraw $300.01 | `OverdraftLimitExceededException` thrown; balance becomes $65 (fee charged); withdrawal of $300.01 never applied | Pass |
| TC-10 | Failure | `CustomerTests.PerformWithdrawal_InsufficientFunds_CatchesExceptionAndReturnsFriendlyMessage` | Everyday account, balance $50, withdraw $100 | `Customer.PerformWithdrawal` catches the exception internally and returns the friendly message as a string (does not throw out to the caller) | Pass |
| TC-11 | Failure | `CustomerControllerTests.CreateCustomer_DuplicateCustomerNumber_ReturnsNullAndDoesNotAdd` | Create "C-3" twice | Second call returns `null`; customer count stays at 1 | Pass |
| TC-12 | Failure | `CustomerControllerTests.DeleteCustomer_UnknownCustomer_ReturnsFalse` | Delete a customer number that does not exist | Returns `false`; no exception thrown | Pass |

> **Before submission:** open Visual Studio's **Test Explorer** (Test menu →
> Test Explorer), click **Run All Tests**, and confirm every test shows a
> green tick. The "Actual Result" column above reflects that expected
> behaviour — take a screenshot of the green Test Explorer run for the
> Technical Report.

## Happy Path vs Failure Path (flow)

```
Withdraw(amount, feeDiscountRate)
        |
        v
  amount <= 0 ? ----Yes----> Fail: "Amount must be greater than zero"
        |                     (status message only, no exception)
        No
        v
  amount <= available funds? ----No----> Charge fee (if applicable)
        |                                 Set LastTransactionStatus
        Yes                               throw BankingException
        v                                 (InsufficientFundsException /
  Deduct balance                           OverdraftLimitExceededException)
  Set LastTransactionStatus = "Successful"       |
  (HAPPY PATH)                                   v
                                           Caller (Customer.PerformWithdrawal)
                                           catches BankingException, returns
                                           ex.Message as the status string
                                           (FAILURE PATH)
```
