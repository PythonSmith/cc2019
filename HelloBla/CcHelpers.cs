using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Text;

namespace HelloBla
{
    class CcHelpers
    {
        public static string AsCCDegree(double x)
        {
            while (x < 0) x += 2 * Math.PI;
            while (x >= 2* Math.PI) x -= 2 * Math.PI;

            return AsCCDouble(Program.Rad2Grd(x));
        }
        public static string AsCCDouble(double x) => string.Format(CultureInfo.InvariantCulture, "{0:0.00}", x);

        internal static int IntParse(string v) => int.Parse(v);

        public static void WriteLineCCLv2((double WheelBase, double Distance, double SteeringAngle) i, bool output = false)
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

        internal static string AsString(Vector2 position) => $"{AsCCDouble(position.X)} {AsCCDouble(position.Y)}";

        public static double DoubleParse(string x) => double.Parse(x, CultureInfo.InvariantCulture);

        public static T[] ParseCCLv2<T>(string[] rawInput, int requiredLength, Func<string[], T> factory)
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
    }
}
