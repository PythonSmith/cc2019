using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using static System.Math;
using static HelloBla.CcHelpers;

namespace HelloBla
{
    class Program
    {
        public static double FULLCIRCLE => 2 * Math.PI;

        static void Main(string[] args)
        {
            //DoLevel2();
            DoLevel3();

            //Console.WriteLine(Level2Neu(9.53d, 8.12d, 0.00d));
        }

        private static void DoLevel3()
        {/*
            1.09 4 9.86 10 9.86 10 9.86 10 9.86 10
1.00 3 1.00 15.00 1.00 0.00 1.00 -15.00
1.00 1 5.00 23.00
0.50 2 10.00 0.00 500.00 3.00
4.20 1 -100.00 -12.00
            */
            var rawInput = @"1.09 4 9.86 10 9.86 10 9.86 10 9.86 10
1.00 3 6.00 23.00 10.00 -23.00 23.50 23.00
2.70 3 5.00 10.00 5.00 -10.00 20.00 0.00
9.53 10 -1.00 1.00 -2.00 2.00 3.00 -3.00 4.00 4.00 5.00 -5.00 6.00 6.00 7.00 7.00 -8.00 8.00 9.00 9.00 10.00 -10.00";

            foreach (var raw in rawInput.Split('\n'))
            {
                Lv3Input input = Lv3Input.Parse(raw);
                Console.WriteLine("Doing..");
                var rover = new Rover(input.WheelBase, new Vector2(0, 0));
                foreach (var move in input.Moves)
                {
                    rover.MoveAdvanced(move);
                }
            }
        }

        private static void DoLevel2()
        {
            var rawInput = @"1.00 1.00 30.00
2.13 4.30 23.00
1.75 3.14 -23.00
2.70 45.00 -34.00
4.20 -5.30 20.00
9.53 8.12 0.00"; // -> 0.00 8.12 0.00

            var input = ParseCCLv2(rawInput.Split('\n'), 3, a => (WheelBase: DoubleParse(a[0]), Distance: DoubleParse(a[1]), SteeringAngle: DoubleParse(a[2])));
            Console.WriteLine("Level 2");
            foreach (var i in input)
            {
                WriteLineCCLv2(i);
                WriteLineCCLv2(Level2Neu(i.WheelBase, i.Distance, Grd2Rad(i.SteeringAngle)), true);
            }
        }

        public static double Grd2Rad(double v) => v * (2d * PI) / 360d;
        public static double Rad2Grd(double v) => v / (2d * PI) * 360d;

        static double Turnradius(double wheelBase, double steeringAngle) => wheelBase / Sin(steeringAngle);

        public static (float x, float y, float newSteeringAngle) Level2Neu(double wheelBase, double distance, double steeringAngle)
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

            var y_2 = 2 * turnRadios * Sin(alpha / 2);

            return ((float)(s * Cos(beta)), (float)(s * Sin(beta)), (float)newDirection);
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
