using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadOefening
{
    class Tree
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        private static int HighestID = 0;
        public int ID;

        public Tree(int x, int y)
        {
            ID = HighestID;
            HighestID++;
            this.X = x;
            this.Y = y;
        }

        public double GetDistanceTo(Tree tree)
        {
            double d = Math.Sqrt(Math.Pow(this.X - tree.X, 2) + Math.Pow(this.Y - tree.Y, 2));
            d = Math.Abs(d);
            return d;
        }

        public override bool Equals(object obj)
        {
            return obj is Tree tree &&
                   ID == tree.ID;
        }

        public override int GetHashCode()
        {
            return 1213502048 + ID.GetHashCode();
        }
    }
}
