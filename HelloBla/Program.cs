using System;
using System.Collections.Generic;
using System.Globalization;
using static System.Math;
using static HelloBla.CcHelpers;

namespace HelloBla
{
    class Program
    {
        public static double FULLCIRCLE => 2 * Math.PI;

        static void Main(string[] args)
        {
            DoLevel2();


            //Console.WriteLine(Level2Neu(9.53d, 8.12d, 0.00d));
        }

        private static void DoLevel2()
        {
            var rawInput = @"1.00 1.00 30.00
2.13 4.30 23.00
1.75 3.14 -23.00
2.70 45.00 -34.00
4.20 -5.30 20.00
9.53 8.12 0.00"; // -> 0.00 8.12 0.00

            var input = ParseCC(rawInput.Split('\n'), 3, a => (WheelBase: DoubleParse(a[0]), Distance: DoubleParse(a[1]), SteeringAngle: DoubleParse(a[2])));
            Console.WriteLine("Level 2");
            foreach (var i in input)
            {
                WriteLineCC(i);
                WriteLineCC(Level2Neu(i.WheelBase, i.Distance, Grd2Rad(i.SteeringAngle)), true);
            }
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
                s = distance;
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
