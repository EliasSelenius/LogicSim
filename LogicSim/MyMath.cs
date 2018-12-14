using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicSim {
    public static class MyMath {

        public static float ClampMin(float v, float min) {
            return (v < min) ? min : v;
        }

        public static float Clamp(float v, float min, float max) {
            return (v < min) ? min : (v > max) ? max : v;
        }

        public static float Lerp(float a, float b, float t) {
            float p = a - b;
            p = Math.Abs(p) * t;
            return a + p;
        }
    }
}
