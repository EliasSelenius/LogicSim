using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicSim.UI;

namespace LogicSim {
    public abstract class ScreenSurface {
        public static int width;
        public static int height;

        public static ScreenSurface CurrentSurface;
        public static WorkTable CurrentWorktable {
            get {
                return (WorkTable)CurrentSurface;
            }
        }

        public Canvas UI = new Canvas(width, height);
        public abstract void Render();
        public void Update() {
            Render();
            UI.Update();
        }
    }
}
