using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicSim.Components;
using LogicSim.Input;

namespace LogicSim.UI {
    class UIPanel : UIElement {

        UIButton button;

        private UIButton[] buttons;


        public override void Start() {
            button = new UIButton("Toolbox", new OpenMenu(this), Vector2.Zero, new Vector2(100, 32));
            canvas.Add(button);

            Component[] Components = { new AndGate(), new OrGate(), new Battery(), new XorGate(), new Circuit(), new Clock() };
            buttons = new UIButton[Components.Length];
            for (int i = 0; i < Components.Length; i++) {
                buttons[i] = new UIButton("", new SummonComponentAction(Components[i]),
                    new Vector2(i * 64, 32), new Vector2(64, 64)) {
                    Image = Components[i].Image
                };
                canvas.Add(buttons[i]);
            }
        }

        public override void Update() {

        }



        private class OpenMenu : UIButtonAction {
            UIPanel panel;
            public OpenMenu(UIPanel p) {
                panel = p;
            }
            public override void Action() {
                for (int i = 0; i < panel.buttons.Length; i++) {
                    panel.buttons[i].Active = !panel.buttons[i].Active;
                }
            }
        }

        private class SummonComponentAction : UIButtonAction {
            Component Comp;
            public SummonComponentAction(Component c) {
                Comp = c;
            }
            public override void Action() {
                Component c = Comp.New();
                ScreenSurface.CurrentWorktable.CurrentCircuit.AddComponent(c);
                ScreenSurface.CurrentWorktable.SelectedComp = c;
            }

        }
    }
}





    
