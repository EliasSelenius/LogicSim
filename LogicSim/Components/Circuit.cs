using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicSim.Components {
    public class Circuit : Component {

        List<Component> Components = new List<Component>();

        public Circuit() {
            Image = Properties.Resources.Circuit;
            Inputs.Add(new InputPort());
            Inputs.Add(new InputPort());
            Inputs.Add(new InputPort());
            Inputs.Add(new InputPort());
            
            Outputs.Add(new OutputPort());
            Outputs.Add(new OutputPort());
            Outputs.Add(new OutputPort());
            Outputs.Add(new OutputPort());
            Outputs.Add(new OutputPort());
            setScale();

        }

        public int TotalNumberOfComponents() {
            int n = Components.Count;
            for (int i = 0; i < Components.Count; i++) {
                if (Components[i] is Circuit c) {
                    n += c.TotalNumberOfComponents();
                }
            }
            return n;
        }

        public int NumberOfComponents() {
            return Components.Count();
        }

        public void RemoveComponent(Component comp) {
            Components.Remove(comp);
        }

        public void AddComponent(Component comp) {
            Components.Add(comp);
        }

        public override Component New() {
            return new Circuit();
        }

        protected override void _processOutput() {
            for (int i = 0; i < Components.Count; i++) {
                Components[i].ProcessOutput();
            }
        }

        public void RenderChildren() {
            for (int i = 0; i < Components.Count; i++) {
                Components[i].Render();
            }
        }
    }
}
