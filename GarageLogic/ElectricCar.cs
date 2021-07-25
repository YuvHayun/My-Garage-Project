using System;

namespace GarageLogic
{
    class ElectricCar : Car
    {
        static readonly float sr_MaxEnergyDuration = 3.2f;

        internal ElectricCar(String i_Model, String i_LicenseNumber) : base(i_Model, i_LicenseNumber)
        {
            this.m_EnergyManager = new Electric(MaxEnergyDuration);
        }

        public float MaxEnergyDuration
        {
            get
            {
                return sr_MaxEnergyDuration;
            }
        }
    }
}


