using System;

namespace HelloBla
{
    internal class Lv3Input
    {
        private double _wheelBase;
        private Move[] _moves;

        public Lv3Input(double wheelBase, Move[] moves)
        {
            WheelBase = wheelBase;
            Moves = moves;
        }

        public double WheelBase { get => _wheelBase; set => _wheelBase = value; }
        internal Move[] Moves { get => _moves; set => _moves = value; }

        internal static Lv3Input Parse(string raw)
        {
            var parts = raw.Split(' ');

            var wheelBase = CcHelpers.DoubleParse(parts[0]);
            var n = CcHelpers.IntParse(parts[1]);
            var moves = new Move[n];
            for (int i = 0; i < n; i++)
            {
                moves[i] = new Move(CcHelpers.DoubleParse(parts[2 + 2 * i]), Program.Grd2Rad(CcHelpers.DoubleParse(parts[2 + 2 * i + 1])));
            }

            return new Lv3Input(wheelBase, moves);
        }
    }
}