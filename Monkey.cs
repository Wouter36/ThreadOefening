using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadOefening
{
    class Monkey
    {
        List<Tree> VisitedTrees = new List<Tree>();
        Tree CurrentTree{get; set; }
        Forest forest { get; set; }
        bool edgeReached = false;
        string Name = "no name";
        static Random random = new Random();
        readonly Color color;
        private static int HighestID = 0;
        public int ID;

        public Monkey(Tree tree, Forest forest, string name)
        {
            ID = HighestID;
            HighestID++;
            color = Color.FromArgb(random.Next(5,250), random.Next(5, 250), random.Next(5, 250));
            forest.ImageGenerator.drawStartingPoint(color, tree.X, tree.Y);
            CurrentTree = tree;
            this.forest = forest;
            this.Name = name;
        }

        internal async Task Jump()
        {
            while(edgeReached == false)
            {
                await TxtLog.WriteText($"forestLog{forest.ID}.txt", $"{Name} is looking for closest tree");
                Tree closestTree = forest.GetClosestTree(CurrentTree, VisitedTrees);
                await TxtLog.WriteText($"forestLog{forest.ID}.txt", $"{Name} finds closest tree at x{closestTree.X} y{closestTree.Y}");
                double distance = CurrentTree.GetDistanceTo(closestTree);
                double forestEdgeDistance = (new List<double>()
                {
                    forest.Width - CurrentTree.X,
                    forest.Height - CurrentTree.Y,
                    CurrentTree.X,
                    CurrentTree.Y
                }).Min()*2;

                if (distance < forestEdgeDistance)
                {
                    string Logstring = $"{Name} jumps from tree ({CurrentTree.ID}) X: {CurrentTree.X} Y: {CurrentTree.Y} " +
                        $"to tree ({closestTree.ID}) X: {closestTree.X} Y: {closestTree.Y}";

                    Console.WriteLine(Logstring);

                    await TxtLog.WriteText($"forestLog{forest.ID}.txt", Logstring);
                    await forest.ImageGenerator.drawJump(color,CurrentTree.X, CurrentTree.Y, closestTree.X, closestTree.Y);
                    await DbInsertion.AddDbLog(ID,forest.ID,Logstring);
                    await DbInsertion.AddMonkeyRecord(ID, forest.ID, VisitedTrees.Count(), closestTree.ID, closestTree.X, closestTree.Y);
                    await DbInsertion.AddWoodRecord(forest.ID, closestTree.ID, closestTree.X, closestTree.Y);

                    VisitedTrees.Add(CurrentTree);
                    CurrentTree = closestTree;
                }
                else
                {
                    string Logstring = "End reached by monkey " + Name;
                    Console.WriteLine(Logstring);
                    await TxtLog.WriteText($"forestLog{forest.ID}.txt", Logstring);
                    edgeReached = true;

                }
            }
        }
    }
}
