using APBD_TASK2.Database;
using APBD_TASK2.Interfaces;
using APBD_TASK2.Models.Equipment;
using APBD_TASK2.Models.Users;
using APBD_TASK2.Services;

var db = Singleton.Instance;

db.Equipment.Add(new Laptop("Dell XPS 15", "Intel i7", 16, "Windows 11"));
db.Equipment.Add(new Laptop("MacBook Pro", "Apple M2", 32, "macOS"));
db.Equipment.Add(new Projector("Epson EB-X51", 3.5, 15000));
db.Equipment.Add(new Camera("Canon EOS R50", 24, true));
db.Equipment.Add(new Camera("Sony ZV-E10", 24, false));

db.Users.Add(new Student("Anna", "Kowalska", "s12345"));
db.Users.Add(new Student("Piotr", "Nowak", "s67890"));
db.Users.Add(new Employee("Maria", "Wiśniewska", "IT"));

ConsoleDisplay.PrintEquipmentList(db.Equipment, "AVAILABLE EQUIPMENT");
ConsoleDisplay.PrintUserList(db.Users, "USERS");

var service = new RentalService();
var anna = db.Users[0];
var piotr = db.Users[1];

ConsoleDisplay.Header("RENTING EQUIPMENT");

var (ok1, msg1, rental1) = service.RentEquipment(anna, db.Equipment[0], 7);
if (ok1) ConsoleDisplay.PrintRentalResult(rental1!, true);
else ConsoleDisplay.Error(msg1);

var (ok2, msg2, rental2) = service.RentEquipment(anna, db.Equipment[2], 3);
if (ok2) ConsoleDisplay.PrintRentalResult(rental2!, true);
else ConsoleDisplay.Error(msg2);

var (ok3, msg3, _) = service.RentEquipment(anna, db.Equipment[3], 5);
if (ok3) ConsoleDisplay.Success(msg3);
else ConsoleDisplay.Error(msg3);

var (ok4, msg4, rental4) = service.RentEquipment(piotr, db.Equipment[1], 14);
if (ok4) ConsoleDisplay.PrintRentalResult(rental4!, true);
else ConsoleDisplay.Error(msg4);

ConsoleDisplay.Header("RETURNING EQUIPMENT");

var (retOk1, retMsg1, returned1) = service.ReturnEquipment(rental1!.Id, DateTime.Now);
if (retOk1) ConsoleDisplay.PrintRentalResult(returned1!, false);
else ConsoleDisplay.Error(retMsg1);

var (retOk2, retMsg2, returned2) = service.ReturnEquipment(rental2!.Id, DateTime.Now.AddDays(5));
if (retOk2) ConsoleDisplay.PrintRentalResult(returned2!, false);
else ConsoleDisplay.Error(retMsg2);

ConsoleDisplay.PrintRentalList(db.Rentals, "ALL RENTALS");
ConsoleDisplay.PrintSummary();
Console.ReadKey();