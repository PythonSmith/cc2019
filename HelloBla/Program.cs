using System;
using static System.Math;

namespace HelloBla
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Turnradius(1, 30d / 360 * 2 * Math.PI));
            Console.WriteLine(Turnradius(1, 13.76d / 360 * 2 * Math.PI));
            Console.WriteLine(Turnradius(1, 2.34d / 360 * 2 * Math.PI));
            Console.WriteLine(Turnradius(1, 90d / 360 * 2 * Math.PI));
            Console.WriteLine(Turnradius(2.45, 90d / 360 * 2 * Math.PI));
            Console.WriteLine(Turnradius(1, 30d / 360 * 2 * Math.PI));
            Console.WriteLine(Turnradius(1, 30d / 360 * 2 * Math.PI));
            Console.WriteLine(Turnradius(1, 30d / 360 * 2 * Math.PI));
            Console.WriteLine("Level 2");
            Console.WriteLine(Level2Neu(1d, 1d, Grd2Rad(30)));
        }

        private static double Grd2Rad(double v) => v * (2d * Math.PI) / 360d;
        private static double Rad2Grd(double v) => v / (2d * Math.PI) * 360d;

        static double Turnradius(double wheelBase, double steeringAngle) => wheelBase / Math.Sin(steeringAngle);

        static (double x, double y, double newSteeringAngle) Level2Neu(double wheelBase, double distance, double steeringAngle)
        {

            var turnRadios = Turnradius(wheelBase, steeringAngle);
            var alpha = distance / turnRadios;

            var s = 2d * turnRadios * Sin(alpha / 2d);
            var beta = (Math.PI - alpha) / 2d;

            return (Math.Round(s * Math.Cos(beta), 2), Math.Round(s * Math.Sin(beta), 2), Rad2Grd(beta - steeringAngle));
        }

        static (double x, double y, double newSteeringAngle) Level2(double wheelBase, double distance, double steeringAngle)
        {

            var turnRadios = Turnradius(wheelBase, steeringAngle);



            var s = turnRadios * Math.Sin(distance / turnRadios);
            var alpha = Math.Sinh(turnRadios / s);


            var h = turnRadios * (1 - Math.Cos(alpha / 2));

            var juliaalpha = Math.Cosh(s * s) / (2 * s * turnRadios);

            return (s * Math.Cos(juliaalpha), s * Math.Sin(juliaalpha), Rad2Grd(steeringAngle));
        }
    }
}
