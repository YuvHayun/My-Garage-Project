using System;
namespace GarageLogic
{
    public class RegularMotorcycle : Motorcycle
    {
        static readonly eFuelTypes sr_FuelType = eFuelTypes.Octan98;
        static readonly int sr_MaxFuelCapacity = 6;

        internal RegularMotorcycle(String i_Model, String i_LicenseNumber) : base(i_Model, i_LicenseNumber)
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


