using System;
using System.Text;
using System.Collections.Generic;

namespace GarageLogic
{
    public abstract class Vehicle
    {
        protected String m_Model;
        protected String m_LicenseNumber;
        internal Tire[] m_Tires;
        internal EnergyManager m_EnergyManager;
        public List<VehicleQNA> m_ParticularNewVehicleQuestions;
        public List<VehicleQNA> m_TiresQuestions;

        internal Vehicle(String i_Model, String i_LicenseNumber, int i_NumberOfTires, int i_MaxAirPressure)
        {
            this.m_Model = i_Model;
            this.m_LicenseNumber = i_LicenseNumber;
            this.m_Tires = new Tire[i_NumberOfTires];
            this.m_ParticularNewVehicleQuestions = new List<VehicleQNA>();
            this.m_TiresQuestions = new List<VehicleQNA>();
            InitTires(i_MaxAirPressure);
            AddTiresQuestionsToList(i_MaxAirPressure);
        }

        internal virtual String GetInfo()
        {
            return String.Format(@"Full information regarding the vehicle by the license number {0}
Model: {1}
{2}
Number of tires: {3}
Tires inforamtion:
{4}{5}", m_LicenseNumber, m_Model, GarageManager.s_GarageDataBase[m_LicenseNumber].GetInfo(), m_Tires.Length, GetTiresInfo(), m_EnergyManager.GetInfo());
        }

        internal String GetTiresInfo()
        {
            StringBuilder tiresInforamtion = new StringBuilder();
            int counter = 1;

            foreach (Tire tire in m_Tires)
            {
                tiresInforamtion.AppendFormat("{0}\n", tire.GetInfo(counter));
                counter++;
            }

            return tiresInforamtion.ToString();
        }

        internal abstract void ParseParticularNewVehicleQuestionsToFields();

        internal abstract void AddParticularNewVehicleQuestionsToList();

        internal void InitTires(int i_MaxAirPressure)
        {
            for(int i = 0; i < m_Tires.Length; i++)
            {
                m_Tires[i] = new Tire(i_MaxAirPressure);
            }
        }

        private void AddTiresQuestionsToList(int i_MaxAirPressure)
        {
            for (int i = 0; i < m_Tires.Length; i++)
            {
                m_TiresQuestions.Add(new VehicleQNA(string.Format("Please enter the Manufacturer name of tire No.({0})", (i + 1))));
                m_TiresQuestions.Add(new VehicleQNA(string.Format("Please enter the current air pressure of tire No.({0}), make sure the given air pressure is equal or less than {1}", (i + 1), i_MaxAirPressure)));
            }
        }

        internal void ParseTiresQuestions()
        {
            float currentAirPressure;

            for (int i = 0; i < m_Tires.Length; i++)
            {   
                if(!float.TryParse(m_TiresQuestions[(i * 2) + 1].Answer, out currentAirPressure))
                {
                    throw new FormatException("Please make sure you enter a valid float value for the tires air pressure");
                }
                m_Tires[i].CurrentAirPressure = currentAirPressure;
                m_Tires[i].Manufacturer = m_TiresQuestions[(i * 2)].Answer;
            }
        }

        internal String Model
        {
            get
            {
                return m_Model;
            }
        }

        internal String LicenseNumber
        {
            get
            {
                return m_LicenseNumber;
            }
        }

        internal void PumpAirPressureToMax()
        {
            foreach (Tire tire in this.m_Tires)
            {
                tire.PumpTheTire((float) tire.MaxAirPressure - tire.CurrentAirPressure);
            }
        }

        internal void ChargeVehicle(float i_TimeToAdd)
        {
            if (this.m_EnergyManager is Electric electric)
            {
                electric.ReCharge(i_TimeToAdd);
            }
            else
            {
                throw new ArgumentException("You can't charge a gas vehicle.");
            }
        }

        internal void FuelVehicle(float i_FuelToAdd, eFuelTypes i_FuelType)
        {
            if (this.m_EnergyManager is Fuel fuel)
            {
                fuel.ReFuel(i_FuelToAdd, i_FuelType);
            }
            else
            {
                throw new ArgumentException("You can't fuel an electric vehicle.");
            }
        }

        public float EnergyStatus
        {
            get
            {
                return m_EnergyManager.GetEnergyPrecentage();
            }
        }
    }
}
