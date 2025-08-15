using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        var manager = new WareHouseManager();
        manager.SeedData();

        Console.WriteLine("\n--- Fresh Produce Inventory ---");
        manager.PrintAllItems(manager.GetGroceriesRepo());

        Console.WriteLine("\n--- Electronic Stock List ---");
        manager.PrintAllItems(manager.GetElectronicsRepo());

        Console.WriteLine("\n--- Demonstrating Exception Handling ---");

        try
        {
            manager.GetElectronicsRepo().AddItem(new ElectronicItem(1, "Ipad Pro", 5, "Apple Store", 12));
        }
        catch (DuplicateItemException ex)
        {
            Console.WriteLine($"[Error] {ex.Message}");
        }

        try
        {
            manager.GetGroceriesRepo().RemoveItem(99);
        }
        catch (ItemNotFoundException ex)
        {
            Console.WriteLine($"[Error] {ex.Message}");
        }

        try
        {
            manager.GetElectronicsRepo().UpdateQuantity(2, -5);
        }
        catch (InvalidQuantityException ex)
        {
            Console.WriteLine($"[Error] {ex.Message}");
        }
    }
}

public interface IInventoryItem
{
    int Id { get; }
    string Name { get; }
    int Quantity { get; set; }
}

public class ElectronicItem : IInventoryItem
{
    public int Id { get; }
    public string Name { get; }
    public int Quantity { get; set; }
    public string Brand { get; }
    public int WarrantyMonths { get; }

    public ElectronicItem(int id, string name, int quantity, string brand, int warrantyMonths)
    {
        Id = id;
        Name = name;
        Quantity = quantity;
        Brand = brand;
        WarrantyMonths = warrantyMonths;
    }
}

public class GroceryItem : IInventoryItem
{
    public int Id { get; }
    public string Name { get; }
    public int Quantity { get; set; }
    public DateTime ExpiryDate { get; }

    public GroceryItem(int id, string name, int quantity, DateTime expiryDate)
    {
        Id = id;
        Name = name;
        Quantity = quantity;
        ExpiryDate = expiryDate;
    }
}

public class DuplicateItemException : Exception
{
    public DuplicateItemException(string message) : base(message) { }
}

public class ItemNotFoundException : Exception
{
    public ItemNotFoundException(string message) : base(message) { }
}

public class InvalidQuantityException : Exception
{
    public InvalidQuantityException(string message) : base(message) { }
}

public class InventoryRepository<T> where T : IInventoryItem
{
    private Dictionary<int, T> _items = new Dictionary<int, T>();

    public void AddItem(T item)
    {
        if (_items.ContainsKey(item.Id))
            throw new DuplicateItemException($"Item with ID {item.Id} already exists.");
        _items[item.Id] = item;
    }

    public T GetItemById(int id)
    {
        if (!_items.ContainsKey(id))
            throw new ItemNotFoundException($"Item with ID {id} not found.");
        return _items[id];
    }

    public void RemoveItem(int id)
    {
        if (!_items.Remove(id))
            throw new ItemNotFoundException($"Item with ID {id} not found.");
    }

    public List<T> GetAllItems()
    {
        return _items.Values.ToList();
    }

    public void UpdateQuantity(int id, int newQuantity)
    {
        if (newQuantity < 0)
            throw new InvalidQuantityException("Quantity cannot be negative.");
        var item = GetItemById(id);
        item.Quantity = newQuantity;
    }
}

public class WareHouseManager
{
    private InventoryRepository<ElectronicItem> _electronics = new InventoryRepository<ElectronicItem>();
    private InventoryRepository<GroceryItem> _groceries = new InventoryRepository<GroceryItem>();

    public void SeedData()
    {
        _electronics.AddItem(new ElectronicItem(1, "Ultrabook Pro", 10, "Telephoneca", 24));
        _electronics.AddItem(new ElectronicItem(2, "Smart Handset", 20, "Franko Phone", 12));

        _groceries.AddItem(new GroceryItem(1, "Apples", 50, DateTime.Now.AddDays(8)));
        _groceries.AddItem(new GroceryItem(2, "Cooking Oil", 30, DateTime.Now.AddDays(4)));
    }

    public void PrintAllItems<T>(InventoryRepository<T> repo) where T : IInventoryItem
    {
        foreach (var item in repo.GetAllItems())
        {
            Console.Write($"{item.Id} - {item.Name} - Qty: {item.Quantity}");
            if (item is ElectronicItem e)
                Console.WriteLine($" - Brand: {e.Brand}, Warranty: {e.WarrantyMonths} months");
            else if (item is GroceryItem g)
                Console.WriteLine($" - Expiry: {g.ExpiryDate:yyyy-MM-dd}");
            else
                Console.WriteLine();
        }
    }

    public void IncreaseStock<T>(InventoryRepository<T> repo, int id, int quantity) where T : IInventoryItem
    {
        try
        {
            var item = repo.GetItemById(id);
            repo.UpdateQuantity(id, item.Quantity + quantity);
            Console.WriteLine($"Stock increased for {item.Name}. New Qty: {item.Quantity}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error increasing stock: {ex.Message}");
        }
    }

    public void RemoveItemById<T>(InventoryRepository<T> repo, int id) where T : IInventoryItem
    {
        try
        {
            repo.RemoveItem(id);
            Console.WriteLine($"Item with ID {id} removed successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error removing item: {ex.Message}");
        }
    }

    public InventoryRepository<ElectronicItem> GetElectronicsRepo() => _electronics;
    public InventoryRepository<GroceryItem> GetGroceriesRepo() => _groceries;
}
