using System;
using System.Text;

namespace KKP
{
    class TwoTrains
    {
        public decimal Train1Speed { get; private set; }       // MPH
        public decimal Train2Speed { get; private set; }       // MPH
        public decimal DistanceBetween { get; private set; }   // Miles
        public decimal MinutesToMeet { get; private set; }
        public decimal Train1DistanceTraveled { get; private set; }
        public decimal Train2DistanceTraveled { get; private set; }
        public decimal RelativeSpeed { get; private set; }
        public TimeSpan TimeSpanToMeet { get; private set; }

        public void CalculateTimeAndDistance(decimal train1Speed, decimal train2Speed, decimal distance)
        {
            if (train1Speed < 0)
            {
                throw new ArgumentOutOfRangeException("train1Speed", "Speed for train 1 must be positive.");
            }

            if (train2Speed < 0)
            {
                throw new ArgumentOutOfRangeException("train2Speed", "Speed for train 2 must be positive.");
            }

            if (distance < 0)
            {
                throw new ArgumentOutOfRangeException("distance", "The distance between the train must be positive.");
            }

            Train1Speed = train1Speed;
            Train2Speed = train2Speed;
            DistanceBetween = distance;
            RelativeSpeed = Train1Speed + Train2Speed;
            MinutesToMeet = decimal.Round(((DistanceBetween / RelativeSpeed) * 60), 2);
            TimeSpanToMeet = TimeSpan.FromMinutes(Convert.ToDouble(MinutesToMeet));
            Train1DistanceTraveled = decimal.Round(Train1Speed * (MinutesToMeet / 60), 2);
            Train2DistanceTraveled = decimal.Round(Train2Speed * (MinutesToMeet / 60), 2);
        }

        public void PrintResults()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine();
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(string.Format("Train 1 Speed: {0} MPH.", Train1Speed));
            stringBuilder.AppendLine(string.Format("Train 2 Speed: {0} MPH.", Train2Speed));
            stringBuilder.AppendLine(string.Format("Distance between trains: {0} Miles.", DistanceBetween));
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(string.Format("The trains will meet after {0}.", FormatTimeSpanString()));
            stringBuilder.AppendLine(string.Format("Train 1 traveled {0} miles.", Train1DistanceTraveled));
            stringBuilder.AppendLine(string.Format("Train 2 traveled {0} miles.", Train2DistanceTraveled));

            Console.WriteLine(stringBuilder.ToString());
        }

        private string FormatTimeSpanString()
        {
            var stringBuilder = new StringBuilder();

            if (TimeSpanToMeet.Hours > 1)
            {
                stringBuilder.Append(string.Format("{0} hours ", TimeSpanToMeet.Hours));
            }
            else if (TimeSpanToMeet.Hours == 1)
            {
                stringBuilder.Append(string.Format("{0} hour ", TimeSpanToMeet.Hours));
            }

            if (TimeSpanToMeet.Minutes > 1)
            {
                stringBuilder.Append(string.Format("{0} minutes ", TimeSpanToMeet.Minutes));
            }
            else if (TimeSpanToMeet.Minutes == 1)
            {
                stringBuilder.Append(string.Format("{0} minute ", TimeSpanToMeet.Minutes));
            }

            if (TimeSpanToMeet.Seconds > 1)
            {
                stringBuilder.Append(string.Format("{0} seconds ", TimeSpanToMeet.Seconds));
            }
            else if (TimeSpanToMeet.Minutes == 1)
            {
                stringBuilder.Append(string.Format("{0} seconds ", TimeSpanToMeet.Seconds));
            }

            return stringBuilder.ToString().Trim();
        }
    }
}
