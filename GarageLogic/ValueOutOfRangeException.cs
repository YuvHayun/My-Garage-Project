using System;

namespace GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        internal float m_MinValue;
        internal float m_MaxValue;

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue): base(String.Format("The value is out of range, the value should be between {0} to {1}", i_MinValue, i_MaxValue))
        {
            this.m_MinValue = i_MinValue;
            this.m_MaxValue = i_MaxValue;
        }

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue, String i_Message) : base(i_Message)
        {
            this.m_MinValue = i_MinValue;
            this.m_MaxValue = i_MaxValue;
        }

        internal float MinValue
        {
            get
            {
                return m_MinValue;
            }

            set
            {
                m_MinValue = value;
            }
        }

        internal float MaxValue
        {
            get
            {
                return m_MaxValue;
            }

            set
            {
                m_MaxValue = value;
            }
        }
    }
}
