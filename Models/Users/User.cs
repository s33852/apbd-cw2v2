namespace APBD_TASK2.Models.Users
{
    internal abstract class User
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string FullName => $"{FirstName} {LastName}";

        private static int _nextId = 1;

        protected User(string firstName, string lastName)
        {
            Id = _nextId++;
            FirstName = firstName;
            LastName = lastName;
        }

        public abstract int MaxActiveRentals { get; }
        public abstract string GetUserType();

        public override string ToString() =>
            $"[{Id}] {GetUserType()}: {FullName} (max rentals: {MaxActiveRentals})";
    }

    internal class Student : User
    {
        public string StudentId { get; }

        public Student(string firstName, string lastName, string studentId)
            : base(firstName, lastName)
        {
            StudentId = studentId;
        }

        public override int MaxActiveRentals => 2;
        public override string GetUserType() => "Student";

        public override string ToString() =>
            $"[{Id}] Student: {FullName} | Index: {StudentId} | Max rentals: {MaxActiveRentals}";
    }

    internal class Employee : User
    {
        public string Department { get; }

        public Employee(string firstName, string lastName, string department)
            : base(firstName, lastName)
        {
            Department = department;
        }

        public override int MaxActiveRentals => 5;
        public override string GetUserType() => "Employee";

        public override string ToString() =>
            $"[{Id}] Employee: {FullName} | Dept: {Department} | Max rentals: {MaxActiveRentals}";
    }
}
