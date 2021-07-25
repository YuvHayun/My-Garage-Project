using System;

namespace GarageLogic
{
    public abstract class Car : Vehicle
    {
        static readonly int sr_NumberOfTiers = 4;
        static readonly int sr_MaxTireAirPressure = 32;
        internal eCarColors m_CarColor;
        internal eNumberOfDoors m_NumberOfDoors;

        internal Car(String i_Model, String i_LicenseNumber) : base(i_Model, i_LicenseNumber, sr_NumberOfTiers, sr_MaxTireAirPressure)
        {
        }

        internal override String GetInfo()
        {
            return base.GetInfo() + String.Format(@"
Car color: {0}
No. of doors: {1}", m_CarColor, (int)m_NumberOfDoors);
        }

        internal override void AddParticularNewVehicleQuestionsToList()
        {
            m_ParticularNewVehicleQuestions.Add(new VehicleQNA(this.m_EnergyManager.GetEnergyQuestion()));
            m_ParticularNewVehicleQuestions.Add(new VehicleQNA(@"Eneter the car color
For red enter 1
For silver enter 2
For white enter 3
For black enter 4"));
            m_ParticularNewVehicleQuestions.Add(new VehicleQNA(@"Eneter the number of doors
For 2 doors enter 2
For 3 doors enter 3
For 4 doors enter 4
For 5 doors enter 5"));
        }

        internal override void ParseParticularNewVehicleQuestionsToFields()
        {
            const int k_EnergyQuestionIndex = 0, k_CarColorQuestionIndex = 1, k_NumberOfDoorsQuestionIndex = 2;
            int carColorNumber, numberOfDoorsNumber;
            float currentEnergy;

            if(!float.TryParse(m_ParticularNewVehicleQuestions[k_EnergyQuestionIndex].Answer, out currentEnergy) ||
            !int.TryParse(m_ParticularNewVehicleQuestions[k_CarColorQuestionIndex].Answer, out carColorNumber)||
            !int.TryParse(m_ParticularNewVehicleQuestions[k_NumberOfDoorsQuestionIndex].Answer, out numberOfDoorsNumber))
            {
                throw new FormatException();
            }
            else
            {
                CarColor = (eCarColors)carColorNumber;
                NumberOfDoors = (eNumberOfDoors)numberOfDoorsNumber;
                this.m_EnergyManager.CurrentEnergy = currentEnergy;
            }
        }

        internal eCarColors CarColor
        {
            get
            {
                return m_CarColor;
            }

            set
            {
                if (Enum.IsDefined(typeof(eCarColors), (eCarColors)value))
                {
                    m_CarColor = (eCarColors)value;
                }
                else
                {
                    throw new FormatException("Invalid input of car color type format");
                }
            }
        }

        internal eNumberOfDoors NumberOfDoors
        {
            get
            {
                return m_NumberOfDoors;
            }

            set
            {
                if (Enum.IsDefined(typeof(eNumberOfDoors), (eNumberOfDoors)value))
                {
                    m_NumberOfDoors = (eNumberOfDoors)value;
                }
                else
                {
                    throw new FormatException("Invalid input of number of doors type format");
                }
            }
        }
    }
}


