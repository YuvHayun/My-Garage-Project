using System;

namespace GarageLogic
{
    public abstract class Motorcycle : Vehicle
    {
        static readonly int sr_NumOfTiers = 2;
        static readonly int sr_MaxTireAirPressure = 30;
        internal eLicenseType m_LicenseType;
        internal int m_EngineVolume;
        const int k_MinEngineVolume = 1;

        internal Motorcycle(String i_Model, String i_LicenseNumber) : base(i_Model, i_LicenseNumber, sr_NumOfTiers, sr_MaxTireAirPressure)
        {
        }

        internal override String GetInfo()
        {
            return base.GetInfo() + String.Format(@"
License type: {0}
Engine volume: {1}cc", m_LicenseType, m_EngineVolume);
        }

        internal override void AddParticularNewVehicleQuestionsToList()
        {
            m_ParticularNewVehicleQuestions.Add(new VehicleQNA(this.m_EnergyManager.GetEnergyQuestion()));
            m_ParticularNewVehicleQuestions.Add(new VehicleQNA(@"Eneter the license type
For A enter 1
For B1 enter 2
For AA enter 3
For BB enter 4"));
            m_ParticularNewVehicleQuestions.Add(new VehicleQNA("Eneter the Engine Volume"));
        }

        internal override void ParseParticularNewVehicleQuestionsToFields()
        {
            const int k_EnergyQuestionIndex = 0, k_LicenseTypeIndex = 1, k_EngineVolumeIndex = 2;
            int licenseTypeNumber, engineVolume;
            float currentEnergy;

            if (!float.TryParse(m_ParticularNewVehicleQuestions[k_EnergyQuestionIndex].Answer, out currentEnergy) ||
            !int.TryParse(m_ParticularNewVehicleQuestions[k_LicenseTypeIndex].Answer, out licenseTypeNumber) ||
            !int.TryParse(m_ParticularNewVehicleQuestions[k_EngineVolumeIndex].Answer, out engineVolume))
            {
                throw new FormatException();
            }
            else
            {
                LicenseType = (eLicenseType)licenseTypeNumber;
                this.m_EnergyManager.CurrentEnergy = currentEnergy;
                EngineVolume = engineVolume;
            }
        }

        internal eLicenseType LicenseType
        {
            get
            {
                return m_LicenseType;
            }

            set
            {
                if (Enum.IsDefined(typeof(eLicenseType), (eLicenseType)value))
                {
                    m_LicenseType = (eLicenseType)value;
                }
                else
                {
                    throw new FormatException("Invalid input of license type format");
                }
            }
        }

        internal int EngineVolume
        {
            get
            {
                return m_EngineVolume;
            }

            set
            {
                if (value >= k_MinEngineVolume)
                {
                    m_EngineVolume = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(k_MinEngineVolume, int.MaxValue, "Please make sure you enter a positive integer engine volume");
                }
            }
        }
    }
}
