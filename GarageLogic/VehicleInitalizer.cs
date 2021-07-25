using System;
using System.Collections.Generic;

namespace GarageLogic
{
    public class VehicleInitalizer
    {
        const int k_VehicleModelQuestionIndex = 2;

        public static void CreateNewVehicle(eVehicleTypes i_VehicleType, List<VehicleQNA> i_VehicleGeneralQuestions, String i_LicenseNumber)
        {
            Vehicle newVehicle = null;

            switch (i_VehicleType)
            {
                case eVehicleTypes.RegularMotorcycle:
                    {
                        newVehicle = new RegularMotorcycle(i_VehicleGeneralQuestions[k_VehicleModelQuestionIndex].Answer, i_LicenseNumber);
                        break;
                    }
                case eVehicleTypes.ElectricMotorcycle:
                    {
                        newVehicle = new ElectricMotorcycle(i_VehicleGeneralQuestions[k_VehicleModelQuestionIndex].Answer, i_LicenseNumber);
                        break;
                    }
                case eVehicleTypes.RegularCar:
                    {
                        newVehicle = new RegularCar(i_VehicleGeneralQuestions[k_VehicleModelQuestionIndex].Answer, i_LicenseNumber);
                        break;
                    }
                case eVehicleTypes.ElectricCar:
                    {
                        newVehicle = new ElectricCar(i_VehicleGeneralQuestions[k_VehicleModelQuestionIndex].Answer, i_LicenseNumber);
                        break;
                    }
                case eVehicleTypes.Truck:
                    {
                        newVehicle = new Truck(i_VehicleGeneralQuestions[k_VehicleModelQuestionIndex].Answer, i_LicenseNumber);
                        break;
                    }
                default:
                    {
                        throw new FormatException("Invalid input of vehicle type format");
                    }
            }

            GarageManager.AddNewVehicleToTheGarage(newVehicle, i_VehicleGeneralQuestions);
        }

        public enum eVehicleTypes
        {
            RegularMotorcycle = 1,
            ElectricMotorcycle,
            RegularCar,
            ElectricCar,
            Truck
        }
    }
}