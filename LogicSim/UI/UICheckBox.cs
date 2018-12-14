using LogicSim.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LogicSim.UI {
    public class UICheckBox : UIElement {

        public bool State;

        private Image image;
        private Content content;

        public UICheckBox(Content c) {
            content = c;
            content.ChekBox = this;
            pos = Vector2.Zero;
            dim = Vector2.One * 100;
        }

        public override void Start() {
            image = Properties.Resources.Check;
            content.Start();
        }

        public override void Update() {
            Draw.Stroke(160);
            Draw.StrokeWidth(1);
            Draw.Fill(255);
            if (Mouse.Pos.Inside(pos, pos + dim)) {
                Draw.Fill(120);
                if (Mouse.Ldown) {
                    if (content != null) {
                        if (State) {
                            content.DeActivated();
                        } else {
                            content.Activated();
                        }
                    }                   
                    State = !State;
                }
            }
            Draw.Ellipse(pos, dim);
            if (State) {
                Draw.Image(image, pos, dim);
                if (content != null) {
                    content.OnCheked();
                }                
            } else {
                if (content != null) {
                    content.OnUnCheked();
                }
            }
        }

        public abstract class Content {
            public UICheckBox ChekBox;
            public virtual void OnCheked() { }
            public virtual void OnUnCheked() { }
            public virtual void Activated() { }
            public virtual void DeActivated() { }
            public virtual void Start() { }
        }
    }
}
