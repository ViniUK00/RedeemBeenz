using System;
using System.Collections.Generic;
using RedeemBeenz.Component;
using RedeemBeenz.Interfaces;
using RedeemBeenz.Models;

public class Program
{
    private static Guid _employeeId;
    private static IRewardsAPI _beenz = null!;
    private static RedemptionStore _redemptionStore = null!;
    private static Catalogue _catalogue = null!;
    private static Guid _lastRedemptionId = Guid.Empty;

    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Setup "infrastructure"
        var accounts = new AccountStore();
        _catalogue = new Catalogue();
        _redemptionStore = new RedemptionStore();
        _beenz = new BeenzRewards(accounts, _catalogue, _redemptionStore);

        _employeeId = Guid.NewGuid();
        accounts.Credit(_employeeId, 400, "Initial allocation");

        // Fake login
        _beenz.Login("user@hk.net", "password");

        RunMainLoop();
    }

    private static void RunMainLoop()
    {
        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            WriteHeader("hk.net – Beenz Rewards");
            Console.WriteLine($"Employee: {_employeeId}");
            WriteBalance();

            Console.WriteLine();
            WriteMenuOption("1", "View rewards and redeem");
            WriteMenuOption("2", "View redemption history");
            WriteMenuOption("3", "View last redemption status");
            WriteMenuOption("4", "Exit");

            Console.Write("\nChoose an option: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    RedeemFlow();
                    break;
                case "2":
                    ShowHistory();
                    break;
                case "3":
                    ShowLastRedemptionStatus();
                    break;
                case "4":
                    exit = true;
                    break;
                default:
                    WriteWarning("Invalid choice. Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }

        Console.Clear();
        WriteHeader("Goodbye 👋");
    }

    private static void WriteHeader(string text)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(new string('═', text.Length + 4));
        Console.WriteLine($"  {text}");
        Console.WriteLine(new string('═', text.Length + 4));
        Console.ResetColor();
    }

    private static void WriteMenuOption(string key, string text)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(key.PadRight(3));
        Console.ResetColor();
        Console.WriteLine(text);
    }

    private static void WriteSuccess(string text)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    private static void WriteWarning(string text)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    private static void WriteBalance()
    {
        int balance = _beenz.GetBalance(_employeeId);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Current beenz: ");
        Console.ResetColor();
        Console.WriteLine($"{balance} pts");
    }

    private static void RedeemFlow()
    {
        Console.Clear();
        WriteHeader("Redeem Beenz");

        var rewards = _beenz.ListRewards();
        if (rewards.Count == 0)
        {
            WriteWarning("No rewards available.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Available rewards:\n");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("#  Name                               Cost   Stock");
        Console.ResetColor();
        Console.WriteLine(new string('-', 55));

        for (int i = 0; i < rewards.Count; i++)
        {
            var r = rewards[i];
            Console.WriteLine(
                $"{(i + 1).ToString().PadRight(3)}{r.Name.PadRight(35)} {r.PointsCost.ToString().PadLeft(4)}   {r.InStock.ToString().PadLeft(3)}");
        }

        Console.WriteLine();
        int index = ReadInt($"Select a reward (1-{rewards.Count}, or 0 to cancel): ", 0, rewards.Count);
        if (index == 0) return;

        var selected = rewards[index - 1];

        Console.WriteLine($"\nYou selected: {selected.Name} ({selected.PointsCost} pts)");
        int qty = ReadInt("Enter quantity: ", 1, selected.InStock);

        Console.WriteLine("\nChoose delivery method:");
        WriteMenuOption("1", "Postal");
        WriteMenuOption("2", "Courier");
        WriteMenuOption("3", "E-Voucher");
        int dm = ReadInt("Your choice: ", 1, 3);

        DeliveryMethod method = (DeliveryMethod)dm;
        string address = "N/A";

        if (method == DeliveryMethod.Postal || method == DeliveryMethod.Courier)
        {
            Console.Write("Enter delivery address: ");
            address = Console.ReadLine() ?? "";
        }

        var delivery = new DeliveryOption { Method = method, Address = address };

        int totalCost = selected.PointsCost * qty;
        Console.WriteLine($"\nYou are about to redeem {qty} x {selected.Name} for {totalCost} points.");
        Console.Write("Confirm? (y/n): ");
        var confirm = Console.ReadLine();
        if (!string.Equals(confirm, "y", StringComparison.OrdinalIgnoreCase))
        {
            WriteWarning("Redemption cancelled by user.");
            Console.ReadKey();
            return;
        }

        bool available = _beenz.CheckAvailability(selected.RewardId, qty);
        if (!available)
        {
            WriteWarning("Sorry, not enough stock for that reward.");
            Console.ReadKey();
            return;
        }

        try
        {
            var redemption = _beenz.Redeem(_employeeId, selected.RewardId, qty, delivery);
            _lastRedemptionId = redemption.RedemptionId;

            Console.WriteLine();
            WriteSuccess($"✅ Redemption successful! ID: {_lastRedemptionId}");
            WriteBalance();
        }
        catch (InvalidOperationException ex)
        {
            WriteWarning("❌ Redemption failed due to business rule violation.");
            Console.WriteLine(ex.Message);
        }

        WriteBalance();
        Console.WriteLine("\nPress any key to return to menu...");
        Console.ReadKey();
    }

    private static void ShowHistory()
    {
        Console.Clear();
        WriteHeader("Redemption History");

        var history = _redemptionStore.GetByEmployee(_employeeId);
        if (!history.Any())
        {
            WriteWarning("No redemptions yet.");
            Console.ReadKey();
            return;
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Date/Time              Reward                          Qty   Cost   Status");
        Console.ResetColor();
        Console.WriteLine(new string('-', 80));

        foreach (var r in history)
        {
            string line =
                $"{r.CreatedAt:yyyy-MM-dd HH:mm}  " +
                $"{r.RewardName.PadRight(30)}  " +
                $"{r.Quantity.ToString().PadLeft(3)}   " +
                $"{r.TotalCost.ToString().PadLeft(4)}   " +
                $"{r.Status}";
            Console.WriteLine(line);
        }

        Console.WriteLine("\nPress any key to return to menu...");
        Console.ReadKey();
    }

    private static void ShowLastRedemptionStatus()
    {
        Console.Clear();
        WriteHeader("Last Redemption Status");

        if (_lastRedemptionId == Guid.Empty)
        {
            WriteWarning("No redemption has been made yet.");
            Console.ReadKey();
            return;
        }

        var status = _beenz.GetRedemptionStatus(_lastRedemptionId);
        Console.WriteLine($"Last redemption ID: {_lastRedemptionId}");
        Console.WriteLine($"Status: {status}");

        Console.WriteLine("\nPress any key to return to menu...");
        Console.ReadKey();
    }

    private static int ReadInt(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            if (int.TryParse(input, out int value) && value >= min && value <= max)
                return value;

            WriteWarning($"Please enter a number between {min} and {max}.");
        }
    }
}