using System;
using System.Collections.Generic;

namespace GarageLogic
{
    public class GarageManager
    {
        public static Dictionary<string, VehicleInfo> s_GarageDataBase;
        public static List<VehicleQNA> s_GeneralNewVehicleQuestions;

        public GarageManager()
        {
            s_GarageDataBase = new Dictionary<string, VehicleInfo>();
            s_GeneralNewVehicleQuestions = new List<VehicleQNA>();
            addGeneralNewVehicleQuestionsToList();
        }

        private void addGeneralNewVehicleQuestionsToList()
        {
            s_GeneralNewVehicleQuestions.Add(new VehicleQNA("Please enter the owner name"));
            s_GeneralNewVehicleQuestions.Add(new VehicleQNA("Please enter the owner phone number (must be a 10 digits number)"));
            s_GeneralNewVehicleQuestions.Add(new VehicleQNA("Please enter the vehicle's model"));
        }

        public static void AddNewVehicleToTheGarage(Vehicle i_VehicleToAdd, List<VehicleQNA> i_VehicleGeneralQuestions)
        {
            const int k_OwnerNameQuestionIndex = 0, k_OwnerPhoneNumberQuestionIndex = 1;
            VehicleInfo vehicleInfo;

            if (!validatePhoneNumberFormat(i_VehicleGeneralQuestions[k_OwnerPhoneNumberQuestionIndex].Answer))
            {
                throw new ArgumentException("The phone number format should contain excactly 10 digits!");
            }

            vehicleInfo = new VehicleInfo(i_VehicleGeneralQuestions[k_OwnerNameQuestionIndex].Answer, i_VehicleGeneralQuestions[k_OwnerPhoneNumberQuestionIndex].Answer, i_VehicleToAdd);
            s_GarageDataBase[i_VehicleToAdd.LicenseNumber] = vehicleInfo;
        }

        public void ParseTiresQuestions(String i_LicenseNumber)
        {
            s_GarageDataBase[i_LicenseNumber].m_Vehicle.ParseTiresQuestions();
        }

        public void ParseRequiredQuestionsToFields(String i_LicenseNumber)
        {
            s_GarageDataBase[i_LicenseNumber].m_Vehicle.ParseParticularNewVehicleQuestionsToFields();
        }

        public List<VehicleQNA> GetVehicleParticularQuestions(String i_LicenseNumber)
        {
            s_GarageDataBase[i_LicenseNumber].m_Vehicle.AddParticularNewVehicleQuestionsToList();

            return s_GarageDataBase[i_LicenseNumber].m_Vehicle.m_ParticularNewVehicleQuestions;
        }

        public List<VehicleQNA> GetTiresQuestions(String i_LicenseNumber)
        {
            return s_GarageDataBase[i_LicenseNumber].m_Vehicle.m_TiresQuestions;
        }

        public void GetLicenseNumbersList(List<String> i_carLicensesList)
        {
            foreach (String key in s_GarageDataBase.Keys)
            {
                i_carLicensesList.Add(key);                
            }
        }

        public void GetLicenseNumbersListByStatus(eVehicleStatus i_VehicleStatusFilter, List<String> i_carLicensesList)
        {
            if(!Enum.IsDefined(typeof(eVehicleStatus), i_VehicleStatusFilter))
            {
                throw new FormatException("Invalid input of vehicle status type format") ;
            }
            else
            {
                foreach (String key in s_GarageDataBase.Keys)
                {
                    if (i_VehicleStatusFilter == s_GarageDataBase[key].m_VehicleStatus)
                    {
                        i_carLicensesList.Add(key);
                    }
                }
            }
        }

        public void ChangeVehicleStatus(String i_LicenseNumber, eVehicleStatus i_NewVehicleStatus)
        {
            if (s_GarageDataBase.ContainsKey(i_LicenseNumber))
            {
                s_GarageDataBase[i_LicenseNumber].VehicleStatus = i_NewVehicleStatus;
            }
            else
            {
                throw new ArgumentException("There is no car with the given license number in the garage.");
            }
        }

        public void PumpTiresToMax(String i_LicenseNumber)
        {
            if (s_GarageDataBase.ContainsKey(i_LicenseNumber))
            {
                s_GarageDataBase[i_LicenseNumber].m_Vehicle.PumpAirPressureToMax();
            }
            else
            {
                throw new ArgumentException("There is no car with the given license number in the garage.");
            }
        }

        public void ChargeVehicle(String i_LicenseNumber, float i_MinutesToCharge) 
        {
            if (s_GarageDataBase.ContainsKey(i_LicenseNumber))
            {
                s_GarageDataBase[i_LicenseNumber].m_Vehicle.ChargeVehicle(i_MinutesToCharge);
            }
            else
            {
                throw new ArgumentException("There is no car with the given license number in the garage.");
            }
        }

        public void FuelVehicle(String i_LicenseNumber, float i_FuelAmountToAdd, eFuelTypes i_FuelType)
        {
            if (s_GarageDataBase.ContainsKey(i_LicenseNumber))
            {
                s_GarageDataBase[i_LicenseNumber].m_Vehicle.FuelVehicle(i_FuelAmountToAdd, i_FuelType);
            }
            else
            {
                throw new ArgumentException("There is no car with the given license number in the garage.");
            }
        }

        public String ShowCarDataByLicenseNumber(String i_LicenseNumber) 
        {
            if (s_GarageDataBase.ContainsKey(i_LicenseNumber))
            {
                return s_GarageDataBase[i_LicenseNumber].m_Vehicle.GetInfo();
            }
            else
            {
                throw new ArgumentException("There is no car with the given license number in the garage.");
            }
        }

        public bool ValidateLicenseNumberFormat(String i_UserInput)
        {
            Boolean isValidLicenseNumber = true;

            foreach (char charcter in i_UserInput)
            {
                if (!Char.IsLetterOrDigit(charcter))
                {
                    isValidLicenseNumber = false;
                    break;
                }
            }

            return isValidLicenseNumber;
        }

        private static bool validatePhoneNumberFormat(String i_UserInput)
        {
            Boolean isValidPhoneNumber = i_UserInput.Length == 10;

            foreach (char charcter in i_UserInput)
            {
                if (!Char.IsDigit(charcter))
                {
                    isValidPhoneNumber = false;
                    break;
                }
            }

            return isValidPhoneNumber;
        }
    }
}