using System;
using System.Text;

namespace KKP
{
    class TwoTrains
    {
        public  decimal Train1Speed { get; private set; }       // MPH
        public  decimal Train2Speed { get; private set; }       // MPH
        public  decimal DistanceBetween { get; private set; }   // Miles

        private  decimal RelativeSpeed
        {
            get
            {
                return Train1Speed + Train2Speed;
            }
        }

        public  decimal TimeToMeet
        {
            get
            {
                return (DistanceBetween / RelativeSpeed) * 60;
            }
        }

        public  decimal Train1DistanceTraveled
        {
            get
            {
                return Train1Speed * (TimeToMeet / 60);
            }
        }

        public  decimal Train2DistanceTraveled
        {
            get
            {
                return Train2Speed * (TimeToMeet / 60);
            }
        }

        public  void CalculateTimeAndDistance(decimal train1Speed, decimal train2Speed, decimal distance)
        {
            Train1Speed = train1Speed;
            Train2Speed = train2Speed;
            DistanceBetween = distance;

            PrintResults();
        }

        private  void PrintResults()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine();
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(string.Format("Train 1 Speed: {0} MPH.", Train1Speed));
            stringBuilder.AppendLine(string.Format("Train 2 Speed: {0} MPH.", Train2Speed));
            stringBuilder.AppendLine(string.Format("Distance between trains: {0} Miles.", DistanceBetween));
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(string.Format("The trains will meet after {0} minutes.", TimeToMeet));
            stringBuilder.AppendLine(string.Format("Train 1 traveled {0} miles.", Train1DistanceTraveled));
            stringBuilder.AppendLine(string.Format("Train 2 traveled {0} miles.", Train2DistanceTraveled));

            Console.WriteLine(stringBuilder.ToString());
        }
    }
}
