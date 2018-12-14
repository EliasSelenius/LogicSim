using LogicSim.Components;
using LogicSim.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicSim.UI {
    public class SelectionPanel : UIElement {

        public SelectionPanel(Content c, bool l) {
            c.selectionPanel = this;
            content = c;
            OpenToLeft = l;
        }

        public readonly bool OpenToLeft;
        public bool Open;
        public override void Start() {
            idealWidth = canvas.width / 5;
            content.Start();
        }

        Content content;

        private float size;
        public int idealWidth;
        public int width;

        public override void Update() {
            Draw.StrokeWidth(3);
            Draw.Stroke(200);
            Draw.Fill(18, 9, 33);
            width = (int)MyMath.Lerp(0, idealWidth, size);

            if (OpenToLeft) {
                Draw.Rect((int)pos.x - width, 0, width, canvas.height);
            } else {
                Draw.Rect((int)pos.x, 0, width, canvas.height);
            }

            content.Update();

            if (Open) {
                if (size < 1) {
                    size = MyMath.Clamp(size + 0.3f, 0, 1);
                }
                content.OnOpen();
            } else {
                if (size > 0) {
                    size -= 0.3f;
                }
                content.OnClose();
            }
        }

        public abstract class Content {
            public SelectionPanel selectionPanel;
            public abstract void OnOpen();
            public abstract void OnClose();
            public abstract void Update();
            public abstract void Start();
        }
    }

    public class ToolBoxMenu : SelectionPanel.Content {

        private Component[] Components = {
            new AndGate(), new OrGate(), new XorGate(), new NotGate(),
            new Circuit(), new Battery(), new Clock()
        };

        public override void OnClose() {
            Draw.StrokeWidth(2);
            Draw.Ellipse((int)selectionPanel.pos.x - 16, 100, 32, selectionPanel.canvas.height - 200);
            if (selectionPanel.OpenToLeft) {
                if (Mouse.Pos.x > selectionPanel.canvas.width - 16) {
                    selectionPanel.Open = true;
                }
            } else {
                if (Mouse.Pos.x < 16) {
                    selectionPanel.Open = true;
                }
            }
        }

        public override void OnOpen() {
            for (int i = 0; i < Components.Length; i++) {

                int collum = i / 3;
                Vector2 p = new Vector2(24 + (i * (selectionPanel.width / 3)) - selectionPanel.width * collum, 24 + collum * (selectionPanel.width / 3));
                Vector2 d = new Vector2(selectionPanel.width / 3 - 48);
                if (Mouse.Pos.Inside(p, p + d)) {
                    d += Vector2.One * 10;
                    if (Mouse.Ldown) {
                        ScreenSurface.CurrentWorktable.AddCompToCurrent(Components[i].New());
                    }
                }
                Draw.Image(Components[i].Image, p, d);
            }

            if (selectionPanel.OpenToLeft) {
                if (Mouse.Pos.x < selectionPanel.canvas.width - selectionPanel.idealWidth) {
                    selectionPanel.Open = false;
                }
            } else {
                if (Mouse.Pos.x > selectionPanel.idealWidth) {
                    selectionPanel.Open = false;
                }
            }
        }

        public override void Start() {

        }

        public override void Update() {

        }
    }

    public class PropMenu : SelectionPanel.Content {
        public override void OnClose() {

        }

        public override void Start() {
            ui = new Canvas(selectionPanel.width, selectionPanel.canvas.height);
        }

        Component component;
        public Canvas ui { private set; get; }


        public override void OnOpen() {
            if (Mouse.Pos.x < selectionPanel.pos.x - selectionPanel.width && Mouse.Ldown) {
                selectionPanel.Open = false;
            }

            ui.Update();



            Vector2 pos = new Vector2(selectionPanel.pos.x - selectionPanel.width, 0);
            Image img = component.Image;
            Draw.Image(img, pos);
            Draw.Fill(255);
            Draw.Text(component.ToString().Remove(0, 20), (int)pos.x + (int)(img.Width * 1.5f), (int)pos.y + img.Height / 3);
        }

        public override void Update() {
            //Draw.Fill(255);
            //Draw.Text(ui.Size.ToString(), 300, 300);
        }

        public void Open(Component c) {
            ui.KillAll();
            selectionPanel.Open = true;
            component = c;

            int yLevel = 64;

            if (component is LogicGate) {
                addInverterBox();
                addIOadjuster(component.Inputs, "Inputs");
                
            } else if (component is Battery) {
                addInverterBox();
            } else if (component is Clock) {

            } else if (component is Circuit) {
                addIOadjuster(component.Inputs, "Inputs");
                addIOadjuster(component.Outputs, "Outputs");
            } else if (component is NotGate) {

            }
            Vector2 relativePos() {
                Vector2 p = selectionPanel.pos;
                p.x -= selectionPanel.idealWidth;
                return p;
            }
            void addInverterBox() {
                UICheckBox chk = new UICheckBox(new InvertComp(component));
                chk.pos = relativePos();
                chk.pos.x += 10;
                chk.pos.y = yLevel + 16;
                chk.dim = Vector2.One * 48;
                yLevel += 64;
                ui.Add(new UIText("Inverted:", chk.pos - (Vector2.UnitY * 16)));
                ui.Add(chk);
            }
            void addIOadjuster<T>(List<T> ios, string txt) where T : IO {
                Vector2 p = relativePos() + (Vector2.UnitY * (yLevel + 16)) + (Vector2.UnitX * 10);
                Vector2 d = Vector2.One * 48;
                UIButton l, r;
                l = new UIButton("<<<", p, d);
                r = new UIButton(">>>", 
                    p + (Vector2.UnitX * (selectionPanel.idealWidth - 68)),
                    d);

                yLevel += 64;

               

                ui.Add(new UIText(txt, p - (Vector2.UnitY * 16)));
                ui.Add(l);
                ui.Add(r);
            }

        }


        

        public class InvertComp : UICheckBox.Content {
            Component comp;
            public InvertComp(Component c) {
                comp = c;

            }

            public override void Start() {
                ChekBox.State = comp.Inverted;
            }

            public override void Activated() {
                comp.Inverted = true;
            }

            public override void DeActivated() {
                comp.Inverted = false;
            }
        }
    }
}