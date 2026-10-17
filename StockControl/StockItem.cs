namespace StockControl;

/// <summary>Описывает товар и поддерживает его текущий складской остаток.</summary>
public sealed class StockItem
{
    public string Name { get; }
    public int Quantity { get; private set; }

    public StockItem(string name, int initialQuantity = 0)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Название товара не может быть пустым.", nameof(name));
        if (initialQuantity < 0)
            throw new ArgumentOutOfRangeException(nameof(initialQuantity), "Начальный остаток не может быть отрицательным.");

        Name = name.Trim();
        Quantity = initialQuantity;
    }

    public void Add(int amount)
    {
        EnsurePositive(amount);
        Quantity = checked(Quantity + amount);
    }

    public void Remove(int amount)
    {
        EnsurePositive(amount);
        if (amount > Quantity)
            throw new InvalidOperationException("Нельзя списать больше товара, чем есть на складе.");

        Quantity -= amount;
    }

    private static void EnsurePositive(int amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Количество должно быть больше нуля.");
    }
}

