using ExpenseTracker.Models;
using ExpenseTracker.Data;

namespace ExpenseTracker.Services;

public class ExpenseManager(JsonFileHandler fileHandler)
{
    public void Add(string desc, decimal amount)
    {
        var expenses = fileHandler.Load();
        int newId = expenses.Count > 0 ? expenses.Max(e => e.Id) + 1 : 1;
        expenses.Add(new Expense { Id = newId, Description = desc, Amount = amount, Date = DateTime.Now });
        fileHandler.Save(expenses);
        Console.WriteLine($"Expense added successfully (ID: {newId})");
    }

    public void Delete(string? idStr)
    {
        if (!int.TryParse(idStr, out int id)) throw new Exception("Valid ID is required.");
        var expenses = List();
        var expense = expenses.FirstOrDefault(e => e.Id == id) ?? throw new Exception("Expense not found.");
        
        expenses.Remove(expense);
        fileHandler.Save(expenses);
        Console.WriteLine("Expense deleted successfully");
    }

    public void Update(string? idStr, string? desc, string? amountStr)
    {
        if (!int.TryParse(idStr, out int id)) throw new Exception("Valid ID is required.");
        var expenses = fileHandler.Load();
        var expense = expenses.FirstOrDefault(e => e.Id == id) ?? throw new Exception("Expense not found.");

        if (!string.IsNullOrEmpty(desc)) expense.Description = desc;
        if (decimal.TryParse(amountStr, out decimal amount)) expense.Amount = amount;

        fileHandler.Save(expenses);
        Console.WriteLine("Expense updated successfully");
    }

    public List<Expense> List() => fileHandler.Load();

    public decimal Summary(int? month = null)
    {
        var expenses = fileHandler.Load();
        var query = expenses.AsQueryable();
        if (month.HasValue)
            query = query.Where(e => e.Date.Month == month.Value && e.Date.Year == DateTime.Now.Year);
        
        return query.Sum(e => e.Amount);
    }
    
}