using apbd_cw2.Database.Equipment;
using APBD_TASK2.Models.Users;

namespace APBD_TASK2.Models.Services
{
    internal class RentalService
    {
        private const decimal PenaltyPerDay = 10.00m;
        private const int DefaultRentalDays = 7;

        // Shortcut to the singleton data
        private List<Equipment> Equipment => Singleton.Instance.Equipment;
        private List<User> Users => Singleton.Instance.Users;
        private List<Rental> Rentals => Singleton.Instance.Rentals;

        public (bool Success, string Message, Rental? Rental) RentEquipment(
            User user, Equipment equipment, int days, DateTime? from = null)
        {
            if (!equipment.IsAvailable)
                return (false, $"'{equipment.Name}' is not available: {equipment.UnavailableReason}", null);

            var activeCount = Rentals.Count(r => r.User.Id == user.Id && r.IsActive);
            if (activeCount >= user.MaxActiveRentals)
                return (false, $"{user.GetUserType()} '{user.FullName}' already has {activeCount}/{user.MaxActiveRentals} active rentals.", null);

            var rental = new Rental(user, equipment, from ?? DateTime.Now, days);
            equipment.MarkUnavailable("Currently rented");
            Rentals.Add(rental);

            return (true, "Rental created successfully.", rental);
        }

        public (bool Success, string Message, Rental? Rental) ReturnEquipment(int rentalId, DateTime? returnedAt = null)
        {
            var rental = Rentals.FirstOrDefault(r => r.Id == rentalId);
            if (rental == null)
                return (false, $"Rental with ID {rentalId} not found.", null);

            if (!rental.IsActive)
                return (false, $"Rental [{rentalId}] has already been returned.", null);

            var returnTime = returnedAt ?? DateTime.Now;
            int daysOverdue = rental.DaysOverdue();
            decimal penalty = daysOverdue > 0 ? daysOverdue * PenaltyPerDay : 0;

            rental.Complete(returnTime, penalty);
            rental.Equipment.MarkAvailable();

            return (true, "Equipment returned.", rental);
        }

        public List<Rental> GetActiveRentalsForUser(int userId) =>
            Rentals.Where(r => r.User.Id == userId && r.IsActive).ToList();

        public List<Rental> GetOverdueRentals() =>
            Rentals.Where(r => r.IsOverdue).ToList();
    }
}
