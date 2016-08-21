using System;
using System.Text;

namespace KKP
{
    internal sealed class TwoTrains
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
                throw new ArgumentOutOfRangeException(nameof(train1Speed), "Speed for train 1 must be positive.");
            }

            if (train2Speed < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(train2Speed), "Speed for train 2 must be positive.");
            }

            if (distance < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(distance), "The distance between the train must be positive.");
            }

            Train1Speed = train1Speed;
            Train2Speed = train2Speed;
            DistanceBetween = distance;
            RelativeSpeed = Train1Speed + Train2Speed;
            MinutesToMeet = decimal.Round(DistanceBetween / RelativeSpeed * 60, 2);
            TimeSpanToMeet = TimeSpan.FromMinutes(Convert.ToDouble(MinutesToMeet));
            Train1DistanceTraveled = decimal.Round(Train1Speed * (MinutesToMeet / 60), 2);
            Train2DistanceTraveled = decimal.Round(Train2Speed * (MinutesToMeet / 60), 2);
        }

        public void PrintResults()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine();
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"Train 1 Speed: {Train1Speed} MPH.");
            stringBuilder.AppendLine($"Train 2 Speed: {Train2Speed} MPH.");
            stringBuilder.AppendLine($"Distance between trains: {DistanceBetween} Miles.");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"The trains will meet after {FormatTimeSpanString()}.");
            stringBuilder.AppendLine($"Train 1 traveled {Train1DistanceTraveled} miles.");
            stringBuilder.AppendLine($"Train 2 traveled {Train2DistanceTraveled} miles.");

            Console.WriteLine(stringBuilder.ToString());
        }

        private string FormatTimeSpanString()
        {
            var stringBuilder = new StringBuilder();

            if (TimeSpanToMeet.Hours > 1)
            {
                stringBuilder.Append($"{TimeSpanToMeet.Hours} hours ");
            }
            else if (TimeSpanToMeet.Hours == 1)
            {
                stringBuilder.Append($"{TimeSpanToMeet.Hours} hour ");
            }

            if (TimeSpanToMeet.Minutes > 1)
            {
                stringBuilder.Append($"{TimeSpanToMeet.Minutes} minutes ");
            }
            else if (TimeSpanToMeet.Minutes == 1)
            {
                stringBuilder.Append($"{TimeSpanToMeet.Minutes} minute ");
            }

            if (TimeSpanToMeet.Seconds > 1)
            {
                stringBuilder.Append($"{TimeSpanToMeet.Seconds} seconds ");
            }
            else if (TimeSpanToMeet.Minutes == 1)
            {
                stringBuilder.Append($"{TimeSpanToMeet.Seconds} seconds ");
            }

            return stringBuilder.ToString().Trim();
        }
    }
}
