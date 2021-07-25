using System;

namespace GarageLogic
{
    public abstract class EnergyManager
    {
        internal readonly float r_MaxEnergy;
        internal float m_CurrentEnergy;
        internal const int k_MinEnergyValueToAdd = 0;

        internal EnergyManager(float i_MaxEnergy)
        {
            this.r_MaxEnergy = i_MaxEnergy;
        }

        internal float MaxEnergy
        {
            get
            {
                return r_MaxEnergy;
            }
        }

        internal abstract String GetInfo();

        internal abstract String GetEnergyQuestion();

        internal float CurrentEnergy
        {
            get
            {
                return m_CurrentEnergy;
            }

            set
            {
                if((value >= k_MinEnergyValueToAdd) && (value <= MaxEnergy))
                {
                    m_CurrentEnergy = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(k_MinEnergyValueToAdd, MaxEnergy, String.Format("Energy value out of range, the value should be between {0} to {1}", k_MinEnergyValueToAdd, MaxEnergy));
                }
            }            
        }

        internal float GetEnergyPrecentage()
        {
            return (CurrentEnergy / MaxEnergy) * 100;
        }
    }
}



