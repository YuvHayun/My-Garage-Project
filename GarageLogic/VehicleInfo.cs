using System;

namespace GarageLogic
{
    public class VehicleInfo
    {
        internal String m_OwnerName;
        internal String m_OwnerPhoneNumber;
        internal eVehicleStatus m_VehicleStatus;
        internal Vehicle m_Vehicle;

        internal VehicleInfo(String i_OwnerName, String i_OwnerPhoneNumber, Vehicle io_Vehicle)
        {
            this.m_OwnerName = i_OwnerName;
            this.m_OwnerPhoneNumber = i_OwnerPhoneNumber;
            this.m_VehicleStatus = eVehicleStatus.InRepair;
            this.m_Vehicle = io_Vehicle;
        }

        internal String GetInfo()
        {
            return String.Format(@"Owner name: {0}
Owner phone number: {1}
Vehicle status: {2}", m_OwnerName, m_OwnerPhoneNumber, m_VehicleStatus);
        }

        internal eVehicleStatus VehicleStatus
        {
            get
            {
                return m_VehicleStatus;
            }
            set
            {
                if (Enum.IsDefined(typeof(eVehicleStatus), (eVehicleStatus)value))
                {
                    m_VehicleStatus = value;
                }
                else
                {
                    throw new FormatException("Invalid input of vehicle status type format");
                }
            }
        }
    }
}


