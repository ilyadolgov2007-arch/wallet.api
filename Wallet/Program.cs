using Wallet;

Account account = new Account("Основной");

account.Deposit(50000, "Зарплата");
account.Withdraw(3000, "Еда");
account.Withdraw(1500, "Транспорт");
account.Withdraw(2000, "Еда");

Console.WriteLine($"Баланс: {account.Balance:N2}");
Console.WriteLine($"Количество операций: {account.Transactions.Count}");
Console.WriteLine();

foreach (var transaction in account.Transactions)
{
    Console.WriteLine($"{transaction.Date:dd.MM.yyyy HH:mm}\t{transaction.Type}\t{transaction.Category}\t{transaction.Amount:N2}");
}

Console.WriteLine();

try
{
    account.Withdraw(100000, "Машина");
}
catch (Exception ex)
{
    Console.WriteLine($"Ошибка: {ex.Message}");
}

try
{
    account.Deposit(-100, "Тест");
}
catch (Exception ex)
{
    Console.WriteLine($"Ошибка: {ex.Message}");
}

try
{
    Account emptyAccount = new Account("");
}
catch (Exception ex)
{
    Console.WriteLine($"Ошибка: {ex.Message}");
}

Console.WriteLine(account.GetTotalExpenses());
Console.WriteLine(account.GetTotalIncome());

foreach (var pair in account.GetExpensesByCategory())
{
    Console.WriteLine($"{pair.Key}: {pair.Value:N2}");
}

Transaction? largestTransaction = account.GetLargestExpense();

if (largestTransaction != null)
{
    Console.WriteLine($"{largestTransaction.Amount}, {largestTransaction.Category}");
}
else
{
    Console.WriteLine("Нет списаний");

}

Console.WriteLine();
Console.WriteLine("=== Операции за сентябрь ===");

Account testAccount = new Account("Тестовый");

testAccount.Deposit(100000, "Зарплата", new DateTime(2026, 8, 5));
testAccount.Withdraw(2000, "Еда", new DateTime(2026, 8, 20));
testAccount.Withdraw(1500, "Транспорт", new DateTime(2026, 9, 3));
testAccount.Withdraw(4000, "Еда", new DateTime(2026, 9, 18));
testAccount.Withdraw(3000, "Развлечения", new DateTime(2026, 9, 30, 15, 0, 0));
testAccount.Withdraw(2500, "Еда", new DateTime(2026, 10, 2));

DateTime periodStart = new DateTime(2026, 9, 1);
DateTime periodEnd = new DateTime(2026, 9, 30);

foreach (var t in testAccount.GetTransactionsForPeriod(periodStart, periodEnd))
{
    Console.WriteLine($"{t.Date:dd.MM.yyyy HH:mm}\t{t.Type}\t{t.Category}\t{t.Amount:N2}");
}