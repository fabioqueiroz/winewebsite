using System;
using System.Collections.Generic;
using System.Text;
using Wine.Commons.Exceptions;

namespace Wine.Commons.Helper
{
    public class InformationProvider
    {
        [Flags]
        public enum Score
        {
            Bad = 1,
            Average = 2,
            Good = 3,
            VeryGood = 4,
            Excellent = 5
        }

        private static int QualityIndex(int score)
        {
            var result = score;

            if (score < 1 || score > 5)
            {
                throw new ItemNotFoundExceptions("Value out of range");
            }

            else if (score == 1)
            {
                result = (int)Score.Bad;
            }

            else if (score == 2)
            {
                result = (int)Score.Average;
            }

            else if (score == 3)
            {
                result = (int)Score.Good;
            }

            else if (score == 4)
            {
                result = (int)Score.VeryGood;
            }

            else
            {
                result = (int)Score.Excellent;
            }

            return result;
        }
        private static double CalculateUnits(double abv, int volume)
        {
            double units = (abv * volume) / 1000;
            return Math.Round(units, 2);
        }

        private static double CaloriesChecker(double abv, int volume)
        {
            double indexfactor;

            if (abv < 9.0 || abv > 23.0)
            {
                throw new ItemNotFoundExceptions("Value out of range");
            }
            else if (abv >= 9.0 && abv < 11.0)
            {
                indexfactor = 0.55;
            }

            else if (abv >= 12.00)
            {
                indexfactor = 0.71;
            }

            else if (abv >= 13.0)
            {
                indexfactor = 0.92;
            }
            else
            {
                indexfactor = 1.3;
            }

            return volume * indexfactor;
        }

        public Func<double, int, int, double> WineInfo = (abv, volume, score) =>
        {
            var units = CalculateUnits(abv, volume);
            var kcal = CaloriesChecker(abv, volume);
            var quality = QualityIndex(score);

            Console.WriteLine($"Units: {units} , Calories: {kcal}, Quality: {quality}");

            return units;
        };

    }
}
