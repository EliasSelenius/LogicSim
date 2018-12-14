using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicSim.UI {
    public class Canvas {

        private List<UIElement> Elements = new List<UIElement>();
        public readonly int width, height;

        public Canvas(int w, int h) {
            width = w;
            height = h;

        }

        public int Size { get { return Elements.Count; } }

        public void Update() {
            for (int i = 0; i < Elements.Count; i++) {
                if (Elements[i].Active) {
                    Elements[i].Update();
                }                
            }
        }

        public void KillAll() {
            Elements.Clear();
        }

        public UIElement Add(UIElement elm) {
            elm.canvas = this;
            Elements.Add(elm);
            elm.Start();
            return elm;
        }
        public void Remove(UIElement elm) {
            Elements.Remove(elm);
        }
    }

    public abstract class UIElement {
        public Canvas canvas;
        public Vector2 pos, dim;
        public bool Active = true;
        public abstract void Update();
        public abstract void Start();
    }
}
