using System;
using System.Collections.Generic;
using System.Globalization;
using static System.Math;

namespace HelloBla
{
    class Program
    {
        public static double FULLCIRCLE => 2 * Math.PI;

        static void Main(string[] args)
        {
            var rawInput = @"1.00 1.00 30.00
2.13 4.30 23.00
1.75 3.14 -23.00
2.70 45.00 -34.00
4.20 -5.30 20.00
9.53 8.12 0.00"; // -> 0.00 8.12 0.00

            var input = ParseCC(rawInput.Split('\n'), 3, a => (WheelBase: DoubleParse(a[0]), Distance: DoubleParse(a[1]), SteeringAngle: DoubleParse(a[2])));

            /*
            Console.WriteLine(Turnradius(1, 30d / 360 * 2 * PI));
            Console.WriteLine(Turnradius(1, 13.76d / 360 * 2 * PI));
            Console.WriteLine(Turnradius(1, 2.34d / 360 * 2 * PI));
            Console.WriteLine(Turnradius(1, 90d / 360 * 2 * PI));
            Console.WriteLine(Turnradius(2.45, 90d / 360 * 2 * PI));
            Console.WriteLine(Turnradius(1, 30d / 360 * 2 * PI));
            Console.WriteLine(Turnradius(1, 30d / 360 * 2 * PI));
            Console.WriteLine(Turnradius(1, 30d / 360 * 2 * PI));
            */
            Console.WriteLine("Level 2");
            foreach (var i in input)
            {
                WriteLineCC(i);
                WriteLineCC(Level2Neu(i.WheelBase, i.Distance, Grd2Rad(i.SteeringAngle)), true);
            }
           

            //Console.WriteLine(Level2Neu(9.53d, 8.12d, 0.00d));
        }

        private static string AsCCDouble(double x) => string.Format(CultureInfo.InvariantCulture, "{0:0.00}", x);

        private static void WriteLineCC((double WheelBase, double Distance, double SteeringAngle) i, bool output=false)
        {
            var c = Console.ForegroundColor;
            if (output)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }

            Console.WriteLine($"{AsCCDouble(i.WheelBase)} {AsCCDouble(i.Distance)} {AsCCDouble(i.SteeringAngle)}");
            if (output)
            {
                Console.ForegroundColor = c;
            }

        }

        private static double DoubleParse(string x) => double.Parse(x, CultureInfo.InvariantCulture);

        private static T[] ParseCC<T>(string[] rawInput, int requiredLength, Func<string[], T> factory)
        {
            var result = new List<T>();
            for (int i = 0; i < rawInput.Length; i++)
            {
                var parts = rawInput[i].Split(' ');


                if (parts.Length != requiredLength)
                {
                    continue;
                }
                result.Add(factory(parts));
            }

            return result.ToArray();
        }

        private static double Grd2Rad(double v) => v * (2d * PI) / 360d;
        private static double Rad2Grd(double v) => v / (2d * PI) * 360d;

        static double Turnradius(double wheelBase, double steeringAngle) => wheelBase / Sin(steeringAngle);

        static (double x, double y, double newSteeringAngle) Level2Neu(double wheelBase, double distance, double steeringAngle)
        {

            double turnRadios = Turnradius(wheelBase, steeringAngle);
            double alpha = distance / turnRadios;

            double s = 2d * turnRadios * Sin(alpha / 2d);
            if (turnRadios == double.PositiveInfinity)
            {
                s = 1;
            }
            double beta = (PI - alpha) / 2d;

            var newDirection = alpha;

            while (newDirection < 0) newDirection += FULLCIRCLE;
            while (newDirection >= FULLCIRCLE) newDirection -= FULLCIRCLE;


            var y_2 = 2 * turnRadios * Sin(alpha / 2);

            return (s * Cos(beta), s * Sin(beta), Rad2Grd(newDirection));
        }

        static (double x, double y, double newSteeringAngle) Level2(double wheelBase, double distance, double steeringAngle)
        {

            double turnRadios = Turnradius(wheelBase, steeringAngle);



            double s = turnRadios * Sin(distance / turnRadios);
            double alpha = Sinh(turnRadios / s);


            double h = turnRadios * (1 - Cos(alpha / 2));

            double juliaalpha = Cosh(s * s) / (2 * s * turnRadios);

            return (s * Cos(juliaalpha), s * Sin(juliaalpha), Rad2Grd(steeringAngle));
        }
    }
}
