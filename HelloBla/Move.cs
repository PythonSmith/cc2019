namespace HelloBla
{
    internal class Move
    {
        private double _v1;
        private double _v2;

        public Move(double distance, double steeringAngle)
        {
            Distance = distance;
            Angle = steeringAngle;
        }

        public double Distance { get => _v1; set => _v1 = value; }
        public double Angle { get => _v2; set => _v2 = value; }
    }
}