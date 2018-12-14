using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogicSim.UI;

namespace LogicSim {
    public class MainMenu : ScreenSurface {
        public MainMenu() {
            UI.Add(new UIButton("New", new NewWorkTable(), new Vector2(width / 2 - 150, height / 2), new Vector2(300, 50)));
            UI.Add(new UIButton("Load", null, new Vector2(width / 2 - 150, height / 2 + 55), new Vector2(300, 50)));
            UI.Add(new UIButton("Exit", new ExitApp(), new Vector2(width / 2 - 150, height / 2 + 110), new Vector2(300, 50)));
        }

        Bitmap title = Properties.Resources.Title;

        float f;
        public override void Render() {
            f += .1f;
            Draw.Background(0);            
            Draw.Image(title, (width / 2) - (title.Width / 2), height / 8);
            //Draw.Text(Mouse.Pos.x + " " + Mouse.Pos.y, 100, 100);
            Draw.Fill(255);
            Draw.Text("Made by Elias Sebastian Haneborg", 0, height - 30);
        }

        private class NewWorkTable : UIButtonAction {
            public override void Action() {
                ScreenSurface.CurrentSurface = new WorkTable();
            }
        }

        private class ExitApp : UIButtonAction {
            public override void Action() {
                Application.Exit();
            }
        }
    }
}
