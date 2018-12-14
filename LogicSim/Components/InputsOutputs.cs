using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicSim.Input;

namespace LogicSim.Components {
    public abstract class IO {

        public virtual bool Data { get; set; }

        public Vector2 pos = new Vector2();

        protected Image image;

        public virtual void Render() {
            if(Mouse.Pos.DistanceTo(pos) < 10) {
                OnMouseHover();
                if (Mouse.Ldown) {
                    OnClick();
                }
            }
            Draw.Fill(20);
            if (Data) {
                Draw.Fill(245, 255, 180);
            }
            Vector2 dim = Vector2.One * 20;
            Draw.StrokeWidth(1);
            Draw.Stroke(200);
            //Draw.Ellipse(pos - (dim * .5f), dim);
            Draw.Image(image, pos - (dim * .5f), dim);
        }
        protected virtual void OnMouseHover() { }
        protected virtual void OnClick() { }
    }



    public class InputPort : IO {

        public OutputPort InnData;

        public InputPort() {
            image = Properties.Resources.Input;
        }

        public override bool Data { 
            get {
                if(InnData != null) {
                    Data = InnData.Data;
                    return InnData.Data;
                } else {
                    Data = false;
                    return false;
                }
            }
            set => base.Data = value;
        }

        public override void Render() {
            if (InnData != null) {
                if (Data) {
                    Draw.Stroke(245, 255, 180);
                } else {
                    Draw.Stroke(150);
                }
                Draw.StrokeWidth(6);
                float dist = Math.Abs(pos.x - InnData.pos.x) / 1.5f;
                Draw.Bezier(pos, pos - (Vector2.UnitX * dist), 
                            InnData.pos, InnData.pos + (Vector2.UnitX * dist));
            }
            base.Render();       
        }

        protected override void OnClick() {
            InnData = ScreenSurface.CurrentWorktable.SelectedOutPort;                          
            ScreenSurface.CurrentWorktable.SelectedOutPort = null;    
        }
    }




    public class OutputPort : IO {

        public OutputPort() {
            image = Properties.Resources.Output;
        }

        public override void Render() {         
            base.Render();
        }

        protected override void OnClick() {
            ScreenSurface.CurrentWorktable.SelectedOutPort = this;
        }
    }
}
