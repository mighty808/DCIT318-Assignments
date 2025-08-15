using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Program
{
    static void Main()
    {
        var app = new InventoryApp("inventory.json");
        app.SeedSampleData();
        app.SaveData();
        var newApp = new InventoryApp("inventory.json");
        newApp.LoadData();
        newApp.PrintAllItems();
    }
}


public interface IInventoryEntity
{
    int Id { get; }
}

public record InventoryItem(int Id, string Name, int Quantity, DateTime DateAdded) : IInventoryEntity;

public class InventoryLogger<T> where T : IInventoryEntity
{
    private List<T> _log = new List<T>();
    private string _filePath;
    public InventoryLogger(string filePath) { _filePath = filePath; }
    public void Add(T item) { _log.Add(item); }
    public List<T> GetAll() { return _log; }
    public void SaveToFile()
    {
        using (StreamWriter sw = new StreamWriter(_filePath))
        {
            string json = JsonSerializer.Serialize(_log);
            sw.Write(json);
        }
    }
    public void LoadFromFile()
    {
        if (!File.Exists(_filePath)) return;
        using (StreamReader sr = new StreamReader(_filePath))
        {
            string json = sr.ReadToEnd();
            _log = JsonSerializer.Deserialize<List<T>>(json);
        }
    }
}

public class InventoryApp
{
    private InventoryLogger<InventoryItem> _logger;
    public InventoryApp(string filePath) { _logger = new InventoryLogger<InventoryItem>(filePath); }
    public void SeedSampleData()
    {
        _logger.Add(new InventoryItem(1, "Laptop", 5, DateTime.Now));
        _logger.Add(new InventoryItem(2, "Mouse", 20, DateTime.Now));
        _logger.Add(new InventoryItem(3, "Keyboard", 10, DateTime.Now));
    }
    public void SaveData() { _logger.SaveToFile(); }
    public void LoadData() { _logger.LoadFromFile(); }
    public void PrintAllItems()
    {
        foreach (var item in _logger.GetAll())
        {
            Console.WriteLine($"ID: {item.Id}, Name: {item.Name}, Quantity: {item.Quantity}, Date Added: {item.DateAdded}");
        }
    }
}

