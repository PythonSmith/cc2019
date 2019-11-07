using System;
using System.Numerics;

namespace HelloBla
{
    internal class Rover
    {
        public double WheelBase { get; }

        private Vector2 _position;
        private float _angle;

        public Rover(double wheelBase, Vector2 position)
        {
            WheelBase = wheelBase;
            this._position = position;
            _angle = 0;
        }

        internal void MoveAdvanced(Move move)
        {
            var (x, y, newDirection) = Program.Level2Neu(WheelBase, move.Distance, move.Angle);

            var vec = new Vector2(x, y);
            vec = TurnVector(vec, _angle);

            _position += vec;
            _angle += newDirection;

            Console.WriteLine($"{CcHelpers.AsString(_position)} {CcHelpers.AsCCDegree(_angle)}");
        }

        private Vector2 TurnVector(Vector2 vec, float angle)
        {
            var oldAngle = MathF.Atan2(vec.Y, vec.X);
            angle = oldAngle - angle;

            return new Vector2(vec.Length() * MathF.Cos(angle), vec.Length() * MathF.Sin(angle));
        }
    }
}