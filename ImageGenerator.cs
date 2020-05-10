using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadOefening
{
    class ImageGenerator
    {
        private static readonly object LockObj = new object();
        Graphics g;
        Bitmap bm;
        int SizeMultiplier = 4;
        int treeSize = 6;
        public ImageGenerator(int x, int y)
        {
            bm = new Bitmap(x * SizeMultiplier, y * SizeMultiplier);
            g = Graphics.FromImage(bm);
        }

        public void drawTree(int x, int y)
        {
            Pen pen = new Pen(Color.Red);
            g.DrawEllipse(pen, (x* SizeMultiplier) - (treeSize/2), (y*SizeMultiplier) - (treeSize/2), treeSize, treeSize);
        }

        public void drawStartingPoint(Color color, int x, int y)
        {
            g.FillEllipse(new SolidBrush(color), (x * SizeMultiplier) - (treeSize / 2), (y * SizeMultiplier) - (treeSize / 2), treeSize, treeSize);
        }

        public async Task drawJump(Color color,int x1, int y1, int x2, int y2)
        {
            lock (LockObj)
            {
                Pen pen = new Pen(color);
                g.DrawLine(pen, x1 * SizeMultiplier, y1 * SizeMultiplier, x2 * SizeMultiplier, y2 * SizeMultiplier);
            }
        }

        internal void Save(int forestID)
        {
            bm.Save($"forest{forestID}.jpg", ImageFormat.Jpeg);
        }
    }
}
