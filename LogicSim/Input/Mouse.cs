using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicSim.Input {
    public static class Mouse {

        private static Vector2 _pos;

        public static bool Rbutton, Lbutton;
        public static bool Ldown, Rdown;
        public static bool Lreleased, Rreleased;

        //public static bool Ldown {
        //    set { _ldown = value; }
        //    get {
        //        bool tmp = _ldown;
        //        _ldown = false;
        //        return tmp;
        //    }
        //}
        //public static bool Rdown {
        //    set { _rdown = value; }
        //    get {
        //        bool tmp = _rdown;
        //        _rdown = false;
        //        return tmp;
        //    }
        //}

        public static Vector2 Pos {
            set {
                LastPos = _pos;
                _pos = value;
            }
            get {
                return _pos;
            }
        }
        public static Vector2 LastPos { get; private set; }

        public static void Reset() {
            Lreleased = Rreleased = Rdown = Ldown = false;         
        }


    }
}
