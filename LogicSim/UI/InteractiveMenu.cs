using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicSim.Input;

namespace LogicSim.UI {
    class InteractiveMenu : UIElement {

        public InteractiveMenu(Vector2 p) {
            pos = p;
        }

        public override void Start() {
            dim = new Vector2(200, 200);
        }

        public override void Update() {
            Draw.StrokeWidth(3);
            Draw.Fill(18, 9, 33);
            Draw.Rect(pos, dim);
            if(!Mouse.Pos.Inside(pos, pos + dim) && (Mouse.Rbutton || Mouse.Lbutton)) {
                canvas.Remove(this);
            }
        }
    }
}
