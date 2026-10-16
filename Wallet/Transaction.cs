namespace Wallet
{
    public class Transaction
    {
        public Guid Id { get; }
        public decimal Amount { get; }
        public string Category { get; }
        public DateTime Date { get; }
        public TransactionType Type { get; }


        public Transaction(decimal amount, string category, DateTime date, TransactionType type)
        {
            if (amount <= 0)
            {
                throw new ArgumentException($"Сумма должна быть больше нуля, получено: {amount}");
            }

            if (string.IsNullOrWhiteSpace(category))
            {
                throw new ArgumentException("Категория пуста!");
            }

            Id = Guid.NewGuid();
            Amount = amount;
            Category = category;
            Date = date;
            Type = type;
        }
    }
}
