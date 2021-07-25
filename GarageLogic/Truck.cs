using System;

namespace GarageLogic
{
    public class Truck : Vehicle
    {
        static readonly int sr_NumOfTiers = 16;
        static readonly int sr_MaxTireAirPressure = 26;
        static readonly eFuelTypes sr_FuelType = eFuelTypes.Soler;
        static readonly int sr_MaxFuelCapacity = 120; 
        internal Boolean m_CarringHazardousMaterials;
        internal int m_MaxCarringWeight;
        internal const int k_MinCarringWeight = 0;

        internal Truck(String i_Model, String i_LicenseNumber) : base(i_Model, i_LicenseNumber, sr_NumOfTiers, sr_MaxTireAirPressure)
        {
            m_EnergyManager = new Fuel(MaxFuelCapacity, FuelType);
        }

        internal override String GetInfo()
        {
            return base.GetInfo() + String.Format(@"
Carring hazardous materials: {0}
Max carrige capacity: {1}", CarringHazardousMaterials, CarringWeight);
        }

        internal override void AddParticularNewVehicleQuestionsToList()
        {
            m_ParticularNewVehicleQuestions.Add(new VehicleQNA(this.m_EnergyManager.GetEnergyQuestion()));
            m_ParticularNewVehicleQuestions.Add(new VehicleQNA("Eneter Max Carrige Capacity"));
            m_ParticularNewVehicleQuestions.Add(new VehicleQNA("Is it Carring Hazadous Materials?"));
        }

        internal override void ParseParticularNewVehicleQuestionsToFields()
        {
            const int k_EnergyQuestionIndex = 0, k_MaxCarrigeQuestionIndex = 1, k_CarringHazardousMaterialsQuestionIndex = 2;
            int maxCarringWeight;
            float currentEnergy;
            Boolean isCarringHazardousMaterials;

            if (!float.TryParse(m_ParticularNewVehicleQuestions[k_EnergyQuestionIndex].Answer, out currentEnergy) || 
            !int.TryParse(m_ParticularNewVehicleQuestions[k_MaxCarrigeQuestionIndex].Answer, out maxCarringWeight) ||
            !bool.TryParse(m_ParticularNewVehicleQuestions[k_CarringHazardousMaterialsQuestionIndex].Answer, out isCarringHazardousMaterials))
            {
                throw new FormatException();
            }
            else
            {
                this.m_EnergyManager.CurrentEnergy = currentEnergy;
                CarringWeight = maxCarringWeight;
                CarringHazardousMaterials = isCarringHazardousMaterials;
            }
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

        internal Boolean CarringHazardousMaterials
        {
            get
            {
                return m_CarringHazardousMaterials;
            }

            set
            {
                m_CarringHazardousMaterials = value;
            }
        }

        internal int CarringWeight
        {
            get
            {
                return m_MaxCarringWeight;
            }

            set
            {
                if(value >= k_MinCarringWeight)
                {
                    m_MaxCarringWeight = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(k_MinCarringWeight, int.MaxValue, "Max carring weight value is out of range, Please make sure you enter a non-negative max carring weight");
                }
            }
        }
    }
}


