using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicSim.UI {
    class UIText : UIElement {

        string Text;

        public UIText(string txt, Vector2 p) {
            Text = txt;
            pos = p;
        }

        public override void Start() {
            
        }

        public override void Update() {
            
            Draw.Fill(255);
            Draw.Text(Text, pos);
        }
    }
}
