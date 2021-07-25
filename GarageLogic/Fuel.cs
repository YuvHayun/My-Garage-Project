using System;

namespace GarageLogic
{
    internal class Fuel : EnergyManager
    {
        internal readonly eFuelTypes r_FuelType;

        internal Fuel(float i_MaxEnergy, eFuelTypes i_FuelType) : base(i_MaxEnergy)
        {
            this.r_FuelType = i_FuelType;
        }

        internal override String GetInfo()
        {
            return String.Format(@"Fuel Type: {0}
Current fuel condition: {1} liters out of {2} liters ({3}%)", r_FuelType, this.m_CurrentEnergy, this.r_MaxEnergy, GetEnergyPrecentage());
        }

        internal override String GetEnergyQuestion()
        {
            return String.Format("Please enter the current amount of fuel in the tank out of {0} Liters", this.r_MaxEnergy);
        }

        internal eFuelTypes FuelType
        {
            get
            {
                return r_FuelType;
            }
        }

        internal void ReFuel(float i_FuelToAdd, eFuelTypes i_FuelType)
        {
            if(i_FuelType == FuelType)
            {
                if(i_FuelToAdd + CurrentEnergy <= MaxEnergy)
                {
                    CurrentEnergy += i_FuelToAdd;
                }
                else
                {
                    throw new ValueOutOfRangeException(k_MinEnergyValueToAdd, MaxEnergy - CurrentEnergy, String.Format("Fuel value out of range, the value should be between {0} to {1}", k_MinEnergyValueToAdd, MaxEnergy - CurrentEnergy));
                }
            }
            else
            {
                throw new ArgumentException("Wrong kind of fuel!");
            }
        }
    }
}
   

