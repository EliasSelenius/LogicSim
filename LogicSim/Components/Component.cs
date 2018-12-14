using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using LogicSim.Input;


/*components to add:
    * Indexer maybe?
    * randomizer.
     */

namespace LogicSim.Components {
    public abstract class Component {

        public List<OutputPort> Outputs = new List<OutputPort>();
        public List<InputPort> Inputs = new List<InputPort>();
        

        /// <summary>
        /// gets or sets the first Output element on this component.
        /// Don't use unles you are sure there are at least one output on the component.
        /// </summary>
        protected bool Output {
            get {
                return Outputs[0].Data;
            }
            set {
                Outputs[0].Data = value;
            }
        }
        /// <summary>
        /// gets the first Input element on this component.
        /// Don't use unles you are sure there are at least one Input on the component.
        /// </summary>
        protected bool Input {
            get {
                return Inputs[0].Data;
            }
        }

        public Component() {
            setScale();
        }

        public abstract Component New();

        public Vector2 pos;
        public Vector2 dim;

        public Bitmap Image;

        public bool Inverted;

        public void Render() {
            Draw.Fill(20);
            Draw.Stroke(200);
            Draw.StrokeWidth(4);
            Draw.Rect(pos, dim);
            Vector2 imageSize = Vector2.One * 40;
            Draw.Image(Image, pos + (dim / 2) - (imageSize / 2), imageSize);
            
            float distOut = dim.y / (Outputs.Count + 1);
            float distInp = dim.y / (Inputs.Count + 1);
            float maxOut = Outputs.Count * 32;
            float maxinp = Inputs.Count * 32;
            for (int i = 1; i <= Outputs.Count; i++) {
                OutputPort outp = Outputs[i - 1];                
                outp.pos.Set(pos.x + dim.x,((dim.y / 2) - (maxOut / 2)) + (pos.y + (i * 32)) - 16);
                outp.Render();
            }
            for (int i = 1; i <= Inputs.Count; i++) {
                InputPort inp = Inputs[i - 1];
                inp.pos.Set(pos.x, ((dim.y / 2) - (maxinp / 2)) + (pos.y + (i * 32)) - 16);
                inp.Render();
            }
            if (Mouse.Pos.Inside(pos, pos + dim) && ScreenSurface.CurrentWorktable.SelectedComp == null) {
                if (Mouse.Lbutton) {
                    ScreenSurface.CurrentWorktable.SelectedComp = this;
                    ScreenSurface.CurrentWorktable.MouseDisplacement = Mouse.Pos - pos;
                } else if (Mouse.Rdown) {
                    ScreenSurface.CurrentWorktable.propMenu.Open(this);
                }                
            }
        }

        protected void setScale() {
            float max = Math.Max(Inputs.Count, Outputs.Count);
            dim.Set(64, MyMath.ClampMin(max * 32, 64));
            
        }

        public void ProcessOutput() {
            _processOutput();
            if (Inverted) {
                for (int i = 0; i < Outputs.Count; i++) {
                    Outputs[i].Data = !Outputs[i].Data;
                }
            }
        }

        protected abstract void _processOutput();

        public void ClearIO() {
            Inputs.Clear();
            Outputs.Clear();
        }
        
    }

    public abstract class LogicGate : Component {
        public LogicGate() {
            Outputs.Add(new OutputPort());
            Inputs.Add(new InputPort());
            Inputs.Add(new InputPort());
            setScale();

            
        }
    }


    public class AndGate : LogicGate {

        public AndGate() {        
            Image = Properties.Resources.And;
        }

        public override Component New() {
            return new AndGate();
        }

        protected override void _processOutput() {
            for (int i = 0; i < Inputs.Count; i++) {
                if(Inputs[i].Data == false) {
                    Output = false;
                    return;
                }
            }
            Output = true;
        }
    }

    public class OrGate : LogicGate {
        public OrGate() {
            Image = Properties.Resources.Or;
        }

        public override Component New() {
            return new OrGate();
        }

        protected override void _processOutput() {
            for (int i = 0; i < Inputs.Count; i++) {
                if (Inputs[i].Data) {
                    Output = true;
                    return;
                }
            }
            Output = false;
        }
    }

    public class XorGate : LogicGate {
        public XorGate() {
            Image = Properties.Resources.Xor;
        }

        public override Component New() {
            return new XorGate();
        }

        protected override void _processOutput() {
            Output = false;
            for (int i = 0; i < Inputs.Count; i++) {
                if(Inputs[i].Data) {
                    if (Output) {
                        Output = false;
                        return;
                    } else {
                        Output = true;
                    }
                }
            }
        }
    }

    public class Battery : Component {
        public Battery() {
            Outputs.Add(new OutputPort());
            Image = Properties.Resources.Battery;
        }

        public override Component New() {
            return new Battery();
        }

        protected override void _processOutput() {
            Output = true;
        }
    }

    public class Clock : Component {
        public Clock() {
            Outputs.Add(new OutputPort());
            Image = Properties.Resources.Clock;
        }
        public override Component New() {
            return new Clock();
        }

        int interval = 10;
        private int i;

        protected override void _processOutput() {
            i++;
            if (i > interval) {
                i = 0;
                Output = !Output;
            }
        }
    }

    public class NotGate : Component {
        public NotGate() {
            Image = Properties.Resources.Not;
            Outputs.Add(new OutputPort());
            Inputs.Add(new InputPort());
        }

        public override Component New() {
            return new NotGate();
        }

        protected override void _processOutput() {
            Output = !Input;
        }
    }
}
