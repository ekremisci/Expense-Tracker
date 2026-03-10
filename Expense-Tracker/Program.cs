using ExpenseTracker.Models;
using ExpenseTracker.Data;
using ExpenseTracker.Services;
using ExpenseTracker.Utilities;

var fileHandler = new JsonFileHandler("expenses.json");
var manager = new ExpenseManager(fileHandler);

if (args.Length == 0) {
    fileHandler.ShowHelp();
    return;
}

string command = args[0].ToLower();
var arguments = ArgumentParser.Parse(args);

try 
{
    switch (command)
    {
        case "add":
            manager.Add(arguments["--description"], decimal.Parse(arguments["--amount"]));
            break;
        case "delete":
            manager.Delete(arguments["--id"]);
            break;
        case "update":
            manager.Update(arguments["--id"], arguments["--description"], arguments["--amount"]);
            break;
        case "summary":
           int? month = arguments.ContainsKey("--month") ? int.Parse(arguments["--month"]) : null;
            decimal total = manager.Summary(month);
            
            if (month.HasValue)
                Console.WriteLine($"Total expenses for month {month}: ${total}");
            else
                Console.WriteLine($"Total expenses (All time): ${total}");
            break;
        case "list":
            var expenses = manager.List();
    
            if (expenses.Count == 0)
            {
                Console.WriteLine("No expenses found.");
            }
            else
            {
                Console.WriteLine($"{"ID",-4} {"Date",-12} {"Description",-20} {"Amount",-10}");
                Console.WriteLine(new string('-', 50));

                foreach (var e in expenses)
                {
                    Console.WriteLine($"{e.Id,-4} {e.Date,-12:yyyy-MM-dd} {e.Description,-20} €{e.Amount,-10}");
                }
            }
            break;
        default:
            Console.WriteLine("Unknown command.");
            break;
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}