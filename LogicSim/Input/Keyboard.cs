using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicSim.Input {
    public static class Keyboard {

        static Keyboard() {
            keys = new List<Key>{
                new Key("e"),
                new Key("del"),
                new Key("esc")
            };
        }

        public static List<Key> keys;

        public static void Reset() {
            foreach(Key k in keys) {               
                k.Released = false;
            }
        }

        public static Key key(string n) {
            for (int i = 0; i < keys.Count; i++) {
                if (keys[i].Name == n) {
                    return keys[i];
                }
            }
            return null;
        }

        public class Key {            
            public bool Released;
            public bool Value;
            public string Name;
            public Key(string n) {
                Name = n;
            }
        }
    }

 
}
