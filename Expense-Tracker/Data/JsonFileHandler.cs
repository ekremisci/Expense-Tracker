using System.Text.Json;
using ExpenseTracker.Models;

namespace ExpenseTracker.Data;

public class JsonFileHandler(string fileName)
{
    public List<Expense> Load()
    {
        if (!File.Exists(fileName)) return new List<Expense>();
        return JsonSerializer.Deserialize<List<Expense>>(File.ReadAllText(fileName)) ?? new List<Expense>();
    }

    public void Save(List<Expense> expenses)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(fileName, JsonSerializer.Serialize(expenses, options));
    }

    public void ShowHelp() => Console.WriteLine("Usage: expense-tracker <command> [options]");
}