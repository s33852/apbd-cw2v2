using APBD_TASK2.Database;
using APBD_TASK2.Models.Users;
using EquipmentBase = APBD_TASK2.Models.Equipment.Equipment;

namespace APBD_TASK2.Models
{
    public class Rental
    {
        public int Id { get; }
        public User User { get; }
        public EquipmentBase Equipment { get; }
        public DateTime RentedAt { get; }
        public DateTime DueDate { get; }
        public DateTime? ReturnedAt { get; private set; }
        public decimal? PenaltyFee { get; private set; }

        public bool IsActive => ReturnedAt == null;
        public bool IsOverdue => IsActive && DateTime.Now > DueDate;

        private static int _nextId = 1;

        public Rental(User user, EquipmentBase equipment, DateTime rentedAt, int rentalDays)
        {
            Id = _nextId++;
            User = user;
            Equipment = equipment;
            RentedAt = rentedAt;
            DueDate = rentedAt.AddDays(rentalDays);
        }

        public void Complete(DateTime returnedAt, decimal penaltyFee)
        {
            ReturnedAt = returnedAt;
            PenaltyFee = penaltyFee;
        }

        public int DaysOverdue()
        {
            var reference = ReturnedAt ?? DateTime.Now;
            return reference > DueDate ? (int)(reference - DueDate).TotalDays : 0;
        }

        public override string ToString()
        {
            var status = IsActive
                ? (IsOverdue ? $"OVERDUE by {DaysOverdue()} day(s)" : "Active")
                : $"Returned {ReturnedAt:yyyy-MM-dd}" + (PenaltyFee > 0 ? $" | Penalty: {PenaltyFee:C}" : " | No penalty");
            return $"[{Id}] {User.FullName} → {Equipment.Name} | Due: {DueDate:yyyy-MM-dd} | {status}";
        }
    }
}
