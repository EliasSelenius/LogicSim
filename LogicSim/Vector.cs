using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicSim {
    public struct Vector2 {
        public float x, y;

        // constructors:
        public Vector2(float X, float Y) {
            x = X;
            y = Y;
        }
        public Vector2(float XY) {
            x = y = XY;
        }


        public static Vector2 Zero { get { return new Vector2(0); } }
        public static Vector2 One { get { return new Vector2(1); } }
        public static Vector2 UnitX { get { return new Vector2(1, 0); } }
        public static Vector2 UnitY { get { return new Vector2(0, 1); } }


        public void Set(Vector2 v) {
            x = v.x;
            y = v.y;
        }
        public void Set(float X, float Y) {
            x = X;
            y = Y;
        }

        public bool Inside(Vector2 a, Vector2 b) {
            return (x > a.x && x < b.x && y > a.y && y < b.y);
        }
        public float DistanceTo(Vector2 a) {
            return (float)Math.Sqrt(Math.Pow(x - a.x, 2) + Math.Pow(y - a.y, 2));
        }


        // operators:
        public static Vector2 operator +(Vector2 v1, Vector2 v2) {
            return new Vector2(v1.x + v2.x, v1.y + v2.y);
        }
        public static Vector2 operator -(Vector2 v1, Vector2 v2) {
            return new Vector2(v1.x - v2.x, v1.y - v2.y);
        }
        public static Vector2 operator *(Vector2 v1, Vector2 v2) {
            return new Vector2(v1.x * v2.x, v1.y * v2.y);
        }
        public static Vector2 operator *(Vector2 v1, float f) {
            return new Vector2(v1.x * f, v1.y * f);
        }
        public static Vector2 operator /(Vector2 v1, float f) {
            return new Vector2(v1.x / f, v1.y / f);
        }
    }
}
