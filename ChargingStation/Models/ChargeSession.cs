using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChargingStation.Models
{
    public class ChargeSession
    {
		private int _consumption;

		public int Consumption
		{
			get { return _consumption; }
			set { _consumption = value; }
		}

		private DateTime _timeStamp;

		public DateTime TimeStamp
		{
			get { return _timeStamp; }
			set { _timeStamp = value; }
		}

		private float _price;        

        public float Price
		{
			get { return _price; }
			set { _price = value; }
		}

        public ChargeSession(int consumption, DateTime timeStamp, float price)
        {
            Consumption = consumption;
            TimeStamp = timeStamp;
            Price = price;
        }

        public override string ToString()
        {
			return $"{TimeStamp} ~ {Consumption} kW * {Price:c} = {(Consumption * Price):c}";
        }
    }
}
