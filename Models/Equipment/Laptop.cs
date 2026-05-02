using System;
using System.Collections.Generic;
using System.Text;

namespace APBD_TASK2.Models.Equipment
{

    public class Laptop : Equipment
    {
        public string Processor { get; }
        public int RamGb { get; }
        public string OperatingSystem { get; }

        public Laptop(string name, string processor, int ramGb, string operatingSystem)
        : base(name)
        {
            Processor = processor;
            RamGb = ramGb;
            OperatingSystem = operatingSystem;
        }
        public override string GetTypeName() => "Laptop";

        public override string GetSpecificDetails() =>
            $"CPU: {Processor} | RAM: {RamGb}GB | OS: {OperatingSystem}";
    }
}
