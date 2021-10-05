using System;
using System.Collections.Generic;
using System.Drawing;

namespace func_rocket
{
    public class LevelsTask
    {
        static readonly Rocket rocket = new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI);
        static readonly Vector target = new Vector(700, 500);
        static readonly Physics standardPhysics = new Physics();

        public static Level NewLevel(string name, Gravity gravity)
        {
            return new Level(name, rocket, target, gravity, standardPhysics);
        }

        public static IEnumerable<Level> CreateLevels()
        {
            yield return new Level("Zero", rocket, new Vector(600, 200),
                (size, v) => Vector.Zero, standardPhysics);

            yield return NewLevel("Heavy", (size, v) => HeavyLevel());
            yield return NewLevel("Up", (size, v) => UpLevel(size, v));
            yield return NewLevel("WhiteHole", (size, v) => WhiteHole(v));
            yield return NewLevel("BlackHole", (size, v) => BlackHole(v));
            yield return NewLevel("BlackAndWhite", (size, v) => BlackAndWhite(v));
        }

        public static Vector HeavyLevel()
        {
            return new Vector(0, 0.9);
        }

        public static Vector UpLevel(Size s, Vector v)
        {
            return new Vector(0, -300 / (300 + s.Height - v.Y));
        }

        public static Vector WhiteHole(Vector v)
        {
            var d = (v - new Vector(700, 500)).Length;
            var mod = 140 * d / (1 + d * d);
            var angle = Math.Atan2(v.X - 700, v.Y - 500);
            return new Vector(mod * Math.Sin(angle), mod * Math.Cos(angle));
        }

        public static Vector BlackHole(Vector v)
        {
            var anomaly = (new Vector(200, 500) + new Vector(700, 500)) / 2;
            var d = (v - anomaly).Length;
            var mod = 300 * d / (1 + d * d);
            var angle = Math.Atan2(anomaly.X - v.X, anomaly.Y - v.Y);
            return new Vector(mod * Math.Sin(angle), mod * Math.Cos(angle));
        }

        public static Vector BlackAndWhite(Vector v)
        {
            return (BlackHole(v) + WhiteHole(v)) / 2;
        }
    }
}