using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogicSim.UI;
using LogicSim.Input;
using System.Diagnostics;

namespace LogicSim {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            Text = "LogicSim";            
        }

        Thread DrawingThread;

        private void Form1_Load(object sender, EventArgs e) { 
            Draw.screen = CreateGraphics();
            Draw.Init(Width, Height);
            ScreenSurface.width = Width;
            ScreenSurface.height = Height;

            

            DrawingThread = new Thread(Upt) {
                IsBackground = true
            };
            DrawingThread.Start();
        }

        float frame;
        Stopwatch stopwatch;

        private void Upt() {
            ScreenSurface.CurrentSurface = new MainMenu();
            stopwatch = new Stopwatch();
            stopwatch.Start();
            while (true) {
                frame++;
                Mouse.Pos = new Vector2(MousePosition.X, MousePosition.Y);
                ScreenSurface.CurrentSurface.Update();
                Mouse.Reset();
                Keyboard.Reset();


                DrawDebug();

                Draw.Render();               
            }
        }

        private void DrawDebug() {
            Draw.Fill(255);
            Draw.Text("Frame Rate: " + (frame / (float)stopwatch.Elapsed.TotalSeconds).ToString(), 0, 0);
            Draw.Text("UIElements: " + ScreenSurface.CurrentSurface.UI.Size, 0, 14);
            int n = 0;
            int tn = 0;
            int uielm = 0;
            if (ScreenSurface.CurrentSurface is WorkTable w) {
                n = w.CurrentCircuit.NumberOfComponents();
                tn = w.BaseCircuit.TotalNumberOfComponents();
                uielm = w.propMenu.ui.Size;
            }
            Draw.Text("Number of components on this circuit: " + n, 0, 28);
            Draw.Text("Total number of components on Worktable: " + tn, 0, 42);
            Draw.Text("UIElements Props Menu: " + uielm, 0, 56);
        }

        

        private void Form1_MouseDown(object sender, MouseEventArgs e) {
            switch (e.Button) {
                case MouseButtons.Left:
                    Mouse.Lbutton = true;
                    Mouse.Ldown = true;
                    break;
                case MouseButtons.Right:
                    Mouse.Rbutton = true;
                    Mouse.Rdown = true;
                    break;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e) {
            switch (e.Button) {
                case MouseButtons.Left:
                    Mouse.Lbutton = false;
                    Mouse.Lreleased = true;
                    break;
                case MouseButtons.Right:
                    Mouse.Rbutton = false;
                    Mouse.Rreleased = true;
                    break;
            }
        }

        

        private void Form1_KeyUp(object sender, KeyEventArgs e) {
            Keyboard.Key k = ProcessKeys(e.KeyCode);
            if (k != null) {
                k.Released = true;
                k.Value = false;
            }            
        }

        

        private void Form1_KeyDown(object sender, KeyEventArgs e) {
            Keyboard.Key k = ProcessKeys(e.KeyCode);
            if (k != null) {
                k.Value = true;
            }            
        }

        private Keyboard.Key ProcessKeys(Keys keyCode) {
            Keyboard.Key k;
            switch (keyCode) {
                case Keys.E:
                    k = Keyboard.key("e");
                    break;
                case Keys.Escape:
                    k = Keyboard.key("esc");
                    break;
                case Keys.Delete:
                    k = Keyboard.key("del");
                    break;
                default:
                    return null;
            }
            return k;
        }
    }
}
