using System;
using System.Collections.Generic;
using System.Text;

namespace APBD_TASK2.Models.Equipment
{
    public class Camera : Equipment
    {
        public int MegaPixels { get; set; }
        public bool HasStabilization { get; set; }
        public Camera(string name, int megaPixels, bool hasStabilization)
        : base(name)
        {
            MegaPixels = megaPixels;
            HasStabilization = hasStabilization;
        }

        public override string GetTypeName() => "Camera";

        public override string GetSpecificDetails() =>
       $"MP: {MegaPixels} | Stabilization: {(HasStabilization ? "Yes" : "No")}";

    }
}
