using System;

namespace GarageLogic
{
    internal class Tire
    {
        internal String m_Manufacturer;
        internal float m_CurrentAirPressure;
        internal readonly int r_MaxAirPressure;
        const int k_MinAirPressure = 0;

        internal Tire(int i_MaxAirPressure)
        {
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public String Manufacturer
        {
            get
            {
                return m_Manufacturer;
            }

            set
            {
                m_Manufacturer = value;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }

            set
            {
                if ((value >= 0) && (value <= MaxAirPressure))
                {
                    m_CurrentAirPressure = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(k_MinAirPressure, MaxAirPressure, String.Format("The air pressure value is out of range, Please make sure you enter an air pressure value between {0} to {1}", k_MinAirPressure, MaxAirPressure));
                }
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return r_MaxAirPressure;
            }
        }

        internal String GetInfo(int i_TiresCounter)
        {
            return String.Format("Tire No.({0}) current air pressure: {1}, manufactured by: {2}.", i_TiresCounter, CurrentAirPressure, Manufacturer);
        }

        public void PumpTheTire(float i_AirPressureToAdd)
        {
            if (i_AirPressureToAdd + CurrentAirPressure <= MaxAirPressure)
            {
                CurrentAirPressure += i_AirPressureToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(k_MinAirPressure, MaxAirPressure - CurrentAirPressure);
            }
        }
    }
}
    


