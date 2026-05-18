using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingStation.Models
{
    public class LicensePlate
    {
		private List<ChargeSession> _chargeSessions = new List<ChargeSession>(); //lijst initialiseren. 

		public List<ChargeSession> ChargeSessions
        {
            get { return _chargeSessions; } //enkel 'get' want er mag in het programma geen nieuwe lijst aangemaakt worden, enkel elementen toevoegen aan de lijst. 
        }
        private Customer _customer;

		public Customer Customer
		{
			get { return _customer; }
			set { _customer = value; }
		}

		private string _plate;

		public string Plate
		{
			get { return _plate; }
			set { _plate = value; }
		}

		private int _mileage;

        public int Mileage
		{
			get { return _mileage; }
			set { _mileage = value; }
		}

        public override string ToString()
        {
			return $"{Plate} ({Mileage} km)";
        }

		public string ShowChargeSessions()
		{
            StringBuilder sb = new StringBuilder();

            foreach (ChargeSession session in _chargeSessions)
			{
				sb.AppendLine(session.ToString());
            }			

			return sb.ToString();
		}       

    }
}
