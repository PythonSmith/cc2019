using System;
using static System.Math;

namespace HelloBla
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Turnradius(1, 30d / 360 * 2 * PI));
            Console.WriteLine(Turnradius(1, 13.76d / 360 * 2 * PI));
            Console.WriteLine(Turnradius(1, 2.34d / 360 * 2 * PI));
            Console.WriteLine(Turnradius(1, 90d / 360 * 2 * PI));
            Console.WriteLine(Turnradius(2.45, 90d / 360 * 2 * PI));
            Console.WriteLine(Turnradius(1, 30d / 360 * 2 * PI));
            Console.WriteLine(Turnradius(1, 30d / 360 * 2 * PI));
            Console.WriteLine(Turnradius(1, 30d / 360 * 2 * PI));
            Console.WriteLine("Level 2");
            Console.WriteLine(Level2Neu(1d, 1d, Grd2Rad(30)));
        }

        private static double Grd2Rad(double v) => v * (2d * PI) / 360d;
        private static double Rad2Grd(double v) => v / (2d * PI) * 360d;

        static double Turnradius(double wheelBase, double steeringAngle) => wheelBase / Sin(steeringAngle);

        static (double x, double y, double newSteeringAngle) Level2Neu(double wheelBase, double distance, double steeringAngle)
        {

            double turnRadios = Turnradius(wheelBase, steeringAngle);
            double alpha = distance / turnRadios;

            double s = 2d * turnRadios * Sin(alpha / 2d);
            double beta = (PI - alpha) / 2d;

            return (Round(s * Cos(beta), 2), Round(s * Sin(beta), 2), Rad2Grd(beta - steeringAngle));
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
