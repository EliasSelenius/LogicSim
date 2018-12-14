using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicSim {
    public static class Draw {
        private static Bitmap btm;
        private static Graphics graphics;
        public static Graphics screen;

        private static SolidBrush brush;
        private static Pen pen; 
        private static bool fill;

        public static int FontSize;

        public static void Init(int w, int h) {
            brush = new SolidBrush(Color.Black);
            pen = new Pen(Color.Black);
            fill = true;
            btm = new Bitmap(w, h);
            graphics = Graphics.FromImage(btm);
            FontSize = 12;
        }

        public static void Render() {
            screen.DrawImage(btm, 0, 0);
        }

        

        public static void Stroke(byte r, byte g, byte b) {
            pen.Color = Color.FromArgb(r, g, b);
        }
        public static void Stroke(byte rgb) {
            Stroke(rgb, rgb, rgb);
        }
        public static void StrokeWidth(float w) {
            pen.Width = w;
        }
        public static void Fill(byte r, byte g, byte b) {
            fill = true;
            brush.Color = Color.FromArgb(r, g, b);
        }
        public static void Fill(byte rgb) {
            Fill(rgb, rgb, rgb);
        }
        public static void NoFill() {
            fill = false;
        }

        public static void Rect(int x, int y, int w, int h) {
            if (fill) {
                graphics.FillRectangle(brush, x, y, w, h);
            }
            graphics.DrawRectangle(pen, x, y, w, h);
        }
        public static void Rect(Vector2 pos, Vector2 dim) {
            Rect((int)pos.x, (int)pos.y, (int)dim.x, (int)dim.y);
        }
        public static void Ellipse(int x, int y, int w, int h) {
            if (fill) {
                graphics.FillEllipse(brush, x, y, w, h);
            }
            graphics.DrawEllipse(pen, x, y, w, h);
        }
        public static void Ellipse(Vector2 pos, Vector2 dim) {
            Ellipse((int)pos.x, (int)pos.y, (int)dim.x, (int)dim.y);
        }
        public static void CenteredEllipse(int x, int y, int w, int h) {
            throw new NotImplementedException();
        }
        public static void Image(Image i, int x, int y) {
            graphics.DrawImage(i, x, y);
        }
        public static void Image(Image i, int x, int y, int w, int h) {
            graphics.DrawImage(i, x, y, w, h);
        }
        public static void Image(Image i, Vector2 pos) {
            Image(i, (int)pos.x, (int)pos.y);
        }
        public static void Image(Image i, Vector2 pos, Vector2 dim) {
            Image(i, (int)pos.x, (int)pos.y, (int)dim.x, (int)dim.y);
        }
        public static void Background(byte r, byte g, byte b) {
            graphics.Clear(Color.FromArgb(r, g, b));
        }
        public static void Background(byte rgb) {
            Background(rgb, rgb, rgb);
        }
        public static void Text(string t, int x, int y) {
            graphics.DrawString(t, new Font(new FontFamily("Arial"), FontSize), brush, x, y);
        }
        public static void Text(string t, Vector2 p) {
            Text(t, (int)p.x, (int)p.y);
        }
        public static void Line(int x1, int y1, int x2, int y2) {
            graphics.DrawLine(pen, x1, y1, x2, y2);
        }
        public static void Line(Vector2 pos1, Vector2 pos2) {
            Line((int)pos1.x, (int)pos1.y, (int)pos2.x, (int)pos2.y);
        }
        public static void Bezier(Vector2 pos1, Vector2 pos1W, Vector2 pos2, Vector2 pos2W) {
            graphics.DrawBezier(pen, pos1.x, pos1.y, pos1W.x, pos1W.y, pos2W.x, pos2W.y, pos2.x, pos2.y);
        }
    }
}
