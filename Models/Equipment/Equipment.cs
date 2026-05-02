using System;
using System.Collections.Generic;
using System.Text;

namespace APBD_TASK2.Models.Equipment
{
    public abstract class Equipment
    {
        public int Id { get; }
        public string Name { get; }
        public bool IsAvailable { get; private set; }
        public string UnavailableReason { get; private set; } = string.Empty;

        private static int _nextId = 1;

        protected Equipment(String name)
        {
            Id = _nextId++;
            Name = name;
            IsAvailable = true;
        }
        public void MarkUnavailable(string reason)
        {
            IsAvailable = false;
            UnavailableReason = reason;
        }

        public void MarkAvailable()
        {
            IsAvailable = true;
            UnavailableReason = string.Empty;
        }

        public abstract string GetTypeName();
        public abstract string GetSpecificDetails();

        public override string ToString()
        {
            var status = IsAvailable ? "Available" : $"Unavailable because of ({UnavailableReason})";
            return $"[{Id}] {GetTypeName()}: {Name} | {status}";
        }
    }
}
