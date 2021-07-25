using System;

namespace GarageLogic
{
    public class VehicleQNA
    {
        private String m_Question;
        private String m_Answer;

        internal VehicleQNA(String i_Question)
        {
            this.m_Question = i_Question;
        }

        public String Question
        {
            get
            {
                return m_Question;
            }

            set
            {
                m_Question = value;
            }
        }

        public String Answer
        {
            get
            {
                return m_Answer;
            }

            set
            {
                m_Answer = value;
            }
        }
    }
}
