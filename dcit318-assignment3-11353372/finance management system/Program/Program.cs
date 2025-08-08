using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Financial Working...");

        FinanceApp app = new FinanceApp();
        app.Run();
    }
}

public record Transaction (int Id, DateTime Date, decimal Amount, string Category);

interface ITransactionProcessor 
{
    void Process(Transaction transaction);
}


class MobileMoneyProcessor : ITransactionProcessor
{
    public void Process(Transaction transaction)
    {
        Console.WriteLine($"MobileMoney Transaction: {transaction.Amount} | {transaction.Category}");
    }
}

class BankTransferProcessor : ITransactionProcessor
{
    public void Process(Transaction transaction)
    {
        Console.WriteLine($"Bank Transaction: {transaction.Amount} | {transaction.Category}");
    }
}

class CryptoWalletProcessor : ITransactionProcessor
{
    public void Process(Transaction transaction)
    {
        Console.WriteLine($"Crypto Wallet: {transaction.Amount} | {transaction.Category}");
    }
}


public class Account 
{
    public string AccountNumber;
    public decimal Balance;

    public Account (string accountNumber, decimal initialBalance)
    {
        AccountNumber = accountNumber;
        Balance = initialBalance;
    }

    public virtual void ApplyTransaction(Transaction transaction)
    {
        Balance -= transaction.Amount;
    }
}

public sealed class SavingsAccount : Account
{
    public SavingsAccount(string accountNumber, decimal initialBalance)
        : base(accountNumber, initialBalance)
    {
    }

    public override void ApplyTransaction(Transaction transaction)
    {
        if (transaction.Amount > Balance)
        {
            Console.WriteLine("Insufficient funds");
        }
        else
        {
            Balance -= transaction.Amount;
            Console.WriteLine($"Transaction applied. New balance: {Balance:C}");
        }
    }
}

public class FinanceApp
{
    private List<Transaction> _transactions = new List<Transaction>();

    public void Run()
    {
        SavingsAccount account = new SavingsAccount("ACC001", 2500m);

        Transaction transactionOne = new Transaction(1, DateTime.Now, 700m, "Melcom");
        Transaction transactionTwo = new Transaction(2, DateTime.Now, 100m, "ShopRite");
        Transaction transactionThree = new Transaction(3, DateTime.Now, 170m, "KFC");

        ITransactionProcessor mobileMoneyProcessor = new MobileMoneyProcessor();
        ITransactionProcessor bankTransferProcessor = new BankTransferProcessor();
        ITransactionProcessor cryptoWalletProcessor = new CryptoWalletProcessor();

        mobileMoneyProcessor.Process(transactionOne);
        bankTransferProcessor.Process(transactionTwo);
        cryptoWalletProcessor.Process(transactionThree);

        account.ApplyTransaction(transactionOne);
        account.ApplyTransaction(transactionTwo);
        account.ApplyTransaction(transactionThree);

        _transactions.Add(transactionOne);
        _transactions.Add(transactionTwo);
        _transactions.Add(transactionThree);
    }
}
