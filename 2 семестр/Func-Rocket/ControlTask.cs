using System;

namespace func_rocket
{
    public class ControlTask
    {
        public static Turn ControlRocket(Rocket rocket, Vector target)
        {
            var angle = 0.0;
            var vector = target - rocket.Location;
            var v1 = vector.Angle - rocket.Direction;
            var v2 = vector.Angle - rocket.Velocity.Angle;

            if (Math.Abs(v1) < 0.5 || Math.Abs(v2) < 0.5) angle = v1 + v2;
            else angle = v1;

            if (angle > 0) return Turn.Right;
            else return Turn.Left;
        }
    }
}