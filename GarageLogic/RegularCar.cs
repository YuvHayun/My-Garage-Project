using System;

namespace GarageLogic
{
    public class RegularCar : Car
    {
        static readonly eFuelTypes sr_FuelType = eFuelTypes.Octan95;
        static readonly int sr_MaxFuelCapacity = 45;

        internal RegularCar(String i_Model, String i_LicenseNumber) : base(i_Model, i_LicenseNumber)
        {
            this.m_EnergyManager = new Fuel(MaxFuelCapacity, FuelType);
        }

        public int MaxFuelCapacity
        {
            get
            {
                return sr_MaxFuelCapacity;
            }
        }

        public eFuelTypes FuelType
        {
            get
            {
                return sr_FuelType;
            }
        }
    }
}


