using System;
namespace GarageLogic
{
    internal class Electric : EnergyManager
    {
        internal Electric(float i_MaxEnergy) : base(i_MaxEnergy)
        {
        }

        internal override String GetInfo()
        {
            return String.Format(@"Current battery condition: {0} hours out of {1} hours ({2}%)", CurrentEnergy, MaxEnergy, GetEnergyPrecentage());
        }

        internal override String GetEnergyQuestion()
        {
            return String.Format("Please enter the current amount of time (in hours) left for the battery out of {0} minutes", (MaxEnergy * 60));
        }

        internal void ReCharge(float i_TimeToAdd)
        {
            if ((i_TimeToAdd >= 0) && (i_TimeToAdd + CurrentEnergy <= MaxEnergy))
            {
                CurrentEnergy += i_TimeToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(k_MinEnergyValueToAdd, MaxEnergy - CurrentEnergy, String.Format("Energy value out of range, the value should be between {0} to {1}", k_MinEnergyValueToAdd,(60 * (MaxEnergy - CurrentEnergy))));
            }            
        }
    }
}


