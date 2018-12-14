using LogicSim.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicSim.UI;
using LogicSim.Input;



namespace LogicSim {
    public class WorkTable : ScreenSurface{

        public Circuit BaseCircuit;
        public Circuit CurrentCircuit;

        public Vector2 MouseDisplacement;
        public Component SelectedComp;
        public OutputPort SelectedOutPort;

        public PropMenu propMenu;
        public ToolBoxMenu toolBox;

        private Keyboard.Key escapeKey;

        public WorkTable() {
            BaseCircuit = new Circuit();
            Init();
        }

        public WorkTable(Circuit c) {
            BaseCircuit = c;
            Init();
        }


        private void Init() {
            CurrentCircuit = BaseCircuit;
            toolBox = new ToolBoxMenu();
            UI.Add(new SelectionPanel(toolBox, false));
            propMenu = new PropMenu();
            SelectionPanel s = new SelectionPanel(propMenu, true);
            s.pos.x = width;
            UI.Add(s);
            escapeKey = Keyboard.key("esc");

            
        }

        public void AddCompToCurrent(Component c) {
            CurrentCircuit.AddComponent(c);
            SelectedComp = c;
            
        }

        public override void Render() {
            //Draw.Background(68, 53, 84);
            Draw.Background(0);

            BaseCircuit.ProcessOutput();
            CurrentCircuit.RenderChildren();
            if (SelectedComp != null && SelectedOutPort == null) {               
                SelectedComp.pos = Mouse.Pos - MouseDisplacement;
                Draw.NoFill();
                Draw.Stroke(255);
                Draw.StrokeWidth(2);
                Draw.Rect(SelectedComp.pos - (Vector2.One * 10), SelectedComp.dim + (Vector2.One * 20));                
            } else if(SelectedOutPort != null) {
                Draw.Stroke(255);
                Draw.StrokeWidth(6);
                Draw.Line(Mouse.Pos, SelectedOutPort.pos);
            }
            if (Mouse.Lbutton == false) {
                SelectedComp = null;
                MouseDisplacement.Set(0, 0);
            }
            if (Keyboard.key("del").Value) {
                SelectedOutPort = null;
                if (SelectedComp != null) {
                    SelectedComp.ClearIO();
                    CurrentCircuit.RemoveComponent(SelectedComp);
                    SelectedComp = null;
                }
            }
            if (toolBox.selectionPanel.Open) {
                SelectedOutPort = null;
            }

            //if (Mouse.Rdown) {
            //    UI.Add(new InteractiveMenu(Mouse.Pos - (Vector2.One * 100)));
            //}
            
            if (escapeKey.Value) {
                ScreenSurface.CurrentSurface = new MainMenu();
            }

            
        }
    }
}
