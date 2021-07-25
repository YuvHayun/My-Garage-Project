using System;
using System.Collections.Generic;

namespace ConsoleUI
{
    public class GarageUI
    {
        internal static GarageLogic.GarageManager s_GarageManager;

        public static void GarageInitalizer()
        {
            System.Console.WriteLine(@"Welcome to our garage!
Here you will be able to add new fuel and electric vehicles, get a list of all the cars in the garage, 
change the status of a vehicle in the garage, inflate tires, fuel or charge a vehicle and view full details of a vehicle.
To continue please press enter");
            System.Console.ReadLine();
            s_GarageManager = new GarageLogic.GarageManager();
            ShowGarageOptionsMenu();
        }

        public static void ShowGarageOptionsMenu()
        {
            int integerUserInput;

            Console.Clear();
            integerUserInput = requestIntegerInput(@"*********************************
Please choose what do you wish to do:
Enter 1 to add a new vehicle to the garage.
Enter 2 to view a list of the vehicles in the garage.
Enter 3 to change the status of a vehicle in the garage.
Enter 4 to inflate tires of a vehicle in the garage.
Enter 5 to fuel a regular vehicle.
Enter 6 to charge an electric vehicle.
Enter 7 to view full detalis of a vehicle by its license number
Enter 8 to close the garage.
*********************************");
            taskHandler((eMenuOptions)integerUserInput);
        }

        private static void taskHandler(eMenuOptions i_UserChoice)
        {
            switch (i_UserChoice)
            {
                case eMenuOptions.AddNewVehicle:
                    {
                        addNewVehicle();
                        break;
                    }
                case eMenuOptions.ViewVehiclesList:
                    {
                        viewVehiclesList();
                        break;
                    }
                case eMenuOptions.ChangeVehicleStatus:
                    {
                        changeVehicleStatus();
                        break;
                    }
                case eMenuOptions.InflateTires:
                    {
                        inflateTires();
                        break;
                    }
                case eMenuOptions.FuelRegularVehicle:
                    {
                        fuelRegularVehicle();
                        break;
                    }
                case eMenuOptions.ChargeElectricVehicle:
                    {
                        chargeElectricVehicle();
                        break;
                    }
                case eMenuOptions.ViewVehicleInfo:
                    {
                        viewVehicleInfo();
                        break;
                    }
                case eMenuOptions.CloseProgram:
                    {
                        System.Environment.Exit(0);
                        break;
                    }
                default:
                    {
                        System.Console.WriteLine("Please make sure you enter a number between 1 to 8");
                        endTaskHandler();
                        break;
                    }
            }

            ShowGarageOptionsMenu();
        }

        private static void addNewVehicle()
        {
            String licenseNumber = getLicenseNumberFromUser();     

            if (GarageLogic.GarageManager.s_GarageDataBase.ContainsKey(licenseNumber))
            {
                System.Console.WriteLine("The vehicle with the given license number is already in the garage and its status will be changed to InRepair");
                s_GarageManager.ChangeVehicleStatus(licenseNumber, GarageLogic.eVehicleStatus.InRepair);
            }
            else
            {
                newVehicleRelatedInformationHandler(licenseNumber);
            }

            endTaskHandler();
        }

        private static void newVehicleRelatedInformationHandler(String i_LicenseNumber)
        {
            GarageLogic.VehicleInitalizer.eVehicleTypes vehicleType;

            try 
            {
                fillVehicleGeneralQuestions(GarageLogic.GarageManager.s_GeneralNewVehicleQuestions, i_LicenseNumber);
                vehicleType = getVehicleTypeFromUser();
                GarageLogic.VehicleInitalizer.CreateNewVehicle(vehicleType, GarageLogic.GarageManager.s_GeneralNewVehicleQuestions, i_LicenseNumber);
                fillTiresQuestions(s_GarageManager.GetTiresQuestions(i_LicenseNumber), i_LicenseNumber);
                fillVehicleParticularQuestions(s_GarageManager.GetVehicleParticularQuestions(i_LicenseNumber), i_LicenseNumber);
                System.Console.WriteLine("The vehicle was added successfully");
            }            
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                endTaskHandler();
                newVehicleRelatedInformationHandler(i_LicenseNumber);
            }
        }

        private static void fillVehicleGeneralQuestions(List<GarageLogic.VehicleQNA> i_VehicleQuestionsList, String i_LicenseNumber)
        {
            foreach (GarageLogic.VehicleQNA question in i_VehicleQuestionsList)
            {
                question.Answer = requestStringInput(question.Question);
            }
        }

        private static void fillVehicleParticularQuestions(List<GarageLogic.VehicleQNA> i_VehicleQuestionsList, String i_LicenseNumber)
        {
            foreach(GarageLogic.VehicleQNA question in i_VehicleQuestionsList)
            {
                question.Answer = requestStringInput(question.Question);
            }

            try
            {
                s_GarageManager.ParseRequiredQuestionsToFields(i_LicenseNumber);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                endTaskHandler();
                fillVehicleParticularQuestions(i_VehicleQuestionsList, i_LicenseNumber);
            }
        }

        private static void fillTiresQuestions(List<GarageLogic.VehicleQNA> i_TiresQuestionsList, String i_LicenseNumber)
        {
            foreach (GarageLogic.VehicleQNA question in i_TiresQuestionsList)
            {
                question.Answer = requestStringInput(question.Question);
            }

            try
            {
                s_GarageManager.ParseTiresQuestions(i_LicenseNumber);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                endTaskHandler();
                fillTiresQuestions(i_TiresQuestionsList, i_LicenseNumber);
            }
        }

        private static void viewVehiclesList()
        {
            String userInput;
            List<String> carLicensesList = new List<string>();

            try {
                while (!string.Equals(userInput = requestStringInput("If you wish to filter the data please enter 'Y', otherwise enter'N'"), "N") && !string.Equals(userInput, "Y"))
                {
                    System.Console.WriteLine("Please make sure you enter 'Y' or 'N'");
                }

                if (string.Equals(userInput, "N"))
                {
                    s_GarageManager.GetLicenseNumbersList(carLicensesList);
                }
                else
                {
                    s_GarageManager.GetLicenseNumbersListByStatus(getCarStatusFromUser(), carLicensesList);
                }

                printVehicleList(carLicensesList);            
                endTaskHandler();
            }
            catch(FormatException e)
            {
                System.Console.WriteLine(e.Message);
                endTaskHandler();
                viewVehiclesList();
            }            
        }

        private static void printVehicleList(List<String> i_CarLicensesList)
        {
            if (i_CarLicensesList.Count == 0)
            {
                System.Console.WriteLine("There are no cars in the garage with the given status");
            }
            else
            {
                Console.Clear();
                System.Console.WriteLine("This is the vehicle list you requested:");
                foreach (String licenseNumber in i_CarLicensesList)
                {
                    System.Console.WriteLine(licenseNumber);
                }
            }
        }

        private static void changeVehicleStatus()
        {
            try
            {
                s_GarageManager.ChangeVehicleStatus(getLicenseNumberFromUser(), getCarStatusFromUser());
                System.Console.WriteLine("Done!");
                endTaskHandler();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                endTaskHandler();
                if (e is ArgumentException)
                {
                    ShowGarageOptionsMenu();
                }
                else
                {
                    changeVehicleStatus();
                }
            }
        }

        private static void inflateTires()
        {
            try
            {
                s_GarageManager.PumpTiresToMax(getLicenseNumberFromUser());
                System.Console.WriteLine("Done!");
                endTaskHandler();
            }
            catch (ArgumentException e)
            {
                System.Console.WriteLine(e.Message);
                endTaskHandler();
                ShowGarageOptionsMenu();
            }
        }

        private static void fuelRegularVehicle()
        {
            String licenseNumber = getLicenseNumberFromUser();
            float fuelToAdd = requestFloatInput("Please enter the amount of fuel you wish to fuel");
            int integerFuelTypeInput = requestIntegerInput(@"Please enter the fuel type
Enter 1 for Soler,
Enter 2 for Octan96,
Enter 3 for Octan95,
Enter 4 for Octan98");

            try
            {
                s_GarageManager.FuelVehicle(licenseNumber, fuelToAdd, (GarageLogic.eFuelTypes)integerFuelTypeInput);
                System.Console.WriteLine("Done!");
                endTaskHandler();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                endTaskHandler();
                if(e is ArgumentException)
                {
                    ShowGarageOptionsMenu();
                }
                else
                {
                    fuelRegularVehicle();
                }
            }
        }

        private static void chargeElectricVehicle()
        {
            String licenseNumber = getLicenseNumberFromUser();
            int minutesToAdd = requestIntegerInput("Please enter the number of minutes you wish to charge");

            try
            {
                s_GarageManager.ChargeVehicle(licenseNumber, (float)minutesToAdd / 60);
                System.Console.WriteLine("Done!");
                endTaskHandler();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                endTaskHandler();
                if (e is ArgumentException)
                {
                    ShowGarageOptionsMenu();
                }
                else
                {
                    chargeElectricVehicle();
                }
            }
        }

        private static void viewVehicleInfo()
        {
            try
            {
                System.Console.WriteLine(s_GarageManager.ShowCarDataByLicenseNumber(getLicenseNumberFromUser()));
                endTaskHandler();
            }
            catch (ArgumentException e)
            {
                System.Console.WriteLine(e.Message);
                endTaskHandler();
                ShowGarageOptionsMenu();
            }
        }

        private static String getLicenseNumberFromUser()
        {
            String userInput;

            while (!s_GarageManager.ValidateLicenseNumberFormat(userInput = requestStringInput("Please enter the vehicle's license number, the license number must contain only digits and characters")))
            {
                System.Console.WriteLine("Please make sure you enter only digits and characters");
            }
            
            return userInput;
        }
        
        private static int requestIntegerInput(String i_Message)
        {
            String stringUserInput;
            int integerUserInput = 0;

            System.Console.WriteLine(i_Message);
            while (!int.TryParse(stringUserInput = System.Console.ReadLine(), out integerUserInput))
            {
                System.Console.WriteLine("Please make sure the input is integer and that you type in something");
                System.Console.WriteLine(i_Message);
            }

            return integerUserInput;
        }

        private static float requestFloatInput(String i_Message)
        {
            String stringUserInput;
            float floatUserInput = 0;

            System.Console.WriteLine(i_Message);
            while (!float.TryParse(stringUserInput = System.Console.ReadLine(), out floatUserInput)) {
                System.Console.WriteLine("Please make sure the input is float and that you type in something");
                System.Console.WriteLine(i_Message);
            }

            return floatUserInput;
        }

        private static String requestStringInput(String i_Message)
        {
            String userInput = "";
            System.Console.WriteLine(i_Message);

            while((userInput = System.Console.ReadLine()).Length == 0)
            {
                System.Console.WriteLine("Please make sure you type in something");
                System.Console.WriteLine(i_Message);
            }

            return userInput;
        }

        private static GarageLogic.eVehicleStatus getCarStatusFromUser()
        {
            int integerUserInput = requestIntegerInput(@"Enter 1 to filter by InRepair,
Enter 2 to filter by Fixed,
Enter 3 to filter by Payed");           

            return (GarageLogic.eVehicleStatus)integerUserInput;
        }

        private static GarageLogic.VehicleInitalizer.eVehicleTypes getVehicleTypeFromUser()
        {
            int integerUserInput = requestIntegerInput(@"Please choose the type of vehicle you want to put in the garage:
Enter 1 to add a new regular motorcycle
Enter 2 to add a new electric motorcycle
Enter 3 to add a new regular car
Enter 4 to add a new electric car
Enter 5 to add a new truck");

            return (GarageLogic.VehicleInitalizer.eVehicleTypes)integerUserInput;            
        }

        private static void endTaskHandler()
        {
            System.Console.WriteLine("Please press enter when you are ready to continue");
            System.Console.ReadLine();
        }

        private enum eMenuOptions
        {
            AddNewVehicle = 1,
            ViewVehiclesList,
            ChangeVehicleStatus,
            InflateTires,
            FuelRegularVehicle,
            ChargeElectricVehicle,
            ViewVehicleInfo,
            CloseProgram
        }
    }
}