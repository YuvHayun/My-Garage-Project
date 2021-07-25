﻿using System;

namespace GarageLogic
{
    public class ElectricMotorcycle : Motorcycle
    {
        static readonly float sr_MaxEnergyDuration = 1.8f;

        internal ElectricMotorcycle(String i_Model, String i_LicenseNumber) : base(i_Model, i_LicenseNumber)
        {
            this.m_EnergyManager = new Electric(MaxEnergyDuration);
        }
        
        public float MaxEnergyDuration
        {
            get
            {
                return sr_MaxEnergyDuration;
            }
        } 
    }
}



