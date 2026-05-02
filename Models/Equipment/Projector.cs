using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace APBD_TASK2.Models.Equipment
{
    public class Projector : Equipment
    {
        public double DisplaySize { get; }
        public int ContrastRatio { get; }

        public Projector(string name, double displaySize, int contrastRatio) : base(name)
        {
            {
                DisplaySize = displaySize;
                ContrastRatio = contrastRatio;
            }
        }
        public override string GetTypeName() => "Projector";

        public override string GetSpecificDetails() =>
            $"Display size {DisplaySize} | ContrastRatio {ContrastRatio}:1";
    }
}
