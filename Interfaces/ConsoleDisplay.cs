using APBD_TASK2.Database;
using APBD_TASK2.Models;
using APBD_TASK2.Models.Equipment;
using APBD_TASK2.Models.Users;

namespace APBD_TASK2.Interfaces
{
    internal static class ConsoleDisplay
    {
        public static void Header(string title)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"── {title} ──");
            Console.ResetColor();
        }

        public static void Success(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{msg}");
            Console.ResetColor();
        }

        public static void Error(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{msg}");
            Console.ResetColor();
        }

        public static void Info(string msg) =>
            Console.WriteLine($"    {msg}");

        public static void PrintEquipmentList(IEnumerable<Equipment> list, string label)
        {
            Header(label);
            var items = list.ToList();
            if (items.Count == 0) { Info("(none)"); return; }
            foreach (var e in items)
            {
                Console.WriteLine($"    {e}");
                Console.WriteLine($"      {e.GetSpecificDetails()}");
            }
        }

        public static void PrintUserList(IEnumerable<User> list, string label)
        {
            Header(label);
            var items = list.ToList();
            if (items.Count == 0) { Info("(none)"); return; }
            foreach (var u in items)
                Console.WriteLine($"    {u}");
        }

        public static void PrintRentalList(IEnumerable<Rental> list, string label)
        {
            Header(label);
            var items = list.ToList();
            if (items.Count == 0) { Info("(none)"); return; }
            foreach (var r in items)
            {
                Console.ForegroundColor = r.IsOverdue ? ConsoleColor.Red : ConsoleColor.White;
                Console.WriteLine($"    {r}");
                Console.ResetColor();
            }
        }

        public static void PrintRentalResult(Rental rental, bool isNew)
        {
            if (isNew)
            {
                Success($"Rental [{rental.Id}] created");
                Info($"  {rental.User.FullName} rented '{rental.Equipment.Name}'");
                Info($"  Due: {rental.DueDate:yyyy-MM-dd}");
            }
            else
            {
                Success($"Rental [{rental.Id}] returned");
                Info($"  '{rental.Equipment.Name}' returned by {rental.User.FullName}");
                if (rental.PenaltyFee > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Late by {rental.DaysOverdue()} day(s). Penalty: {rental.PenaltyFee:C}");
                    Console.ResetColor();
                }
                else
                {
                    Info("  Returned on time. No penalty.");
                }
            }
        }

        public static void PrintSummary()
        {
            var db = Singleton.Instance;
            var completed = db.Rentals.Where(r => !r.IsActive).ToList();

            Header("SUMMARY REPORT");
            Console.WriteLine($"    Equipment  : {db.Equipment.Count} total, {db.Equipment.Count(e => e.IsAvailable)} available");
            Console.WriteLine($"    Users      : {db.Users.Count} total");
            Console.WriteLine($"    Rentals    : {db.Rentals.Count} total, {db.Rentals.Count(r => r.IsActive)} active, {db.Rentals.Count(r => r.IsOverdue)} overdue");
            Console.WriteLine($"    Penalties  : {completed.Sum(r => r.PenaltyFee ?? 0):C} collected");
        }
    }
}
