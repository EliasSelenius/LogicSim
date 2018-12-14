using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicSim.Input;

namespace LogicSim.UI {
    public class UIButton : UIElement {

        public string Name;
        public UIButtonAction Action;
        public bool Click { get; private set; }
        public Bitmap Image;

        public UIButton(string n, UIButtonAction act, Vector2 p, Vector2 d) {
            pos = p;
            dim = d;
            Name = n;
            Action = act;
        }

        public UIButton(string n, Vector2 p, Vector2 d) {
            pos = p;
            dim = d;
            Name = n;            
        }


        public static int Pressed = 0;

        public override void Update() {
            Draw.Stroke(160);
            Draw.StrokeWidth(3);
            Draw.Fill(255);            
            if (Mouse.Pos.Inside(pos, pos + dim)) {
                Draw.Fill(120);
                if (Mouse.Ldown) {
                    Pressed++;
                    Click = true;                    
                    if (Action != null) {
                        Action.Action();
                    }
                }
                
            }
            if (Mouse.Lreleased) {
                Click = false;
            }
            if (Click) {
                Draw.Fill(100);
            }
            
            Draw.Rect(pos, dim);
            if(Image != null) {
                Draw.Image(Image, pos, dim);
            }            
            Draw.Fill(0);
            Draw.Text(Name, pos);
        }
        public override void Start() {
        }
    }

    public abstract class UIButtonAction {
        public abstract void Action();
    }
}
