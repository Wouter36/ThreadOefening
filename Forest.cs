using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadOefening
{
    class Forest
    {
        List<Monkey> monkeys = new List<Monkey>();
        List<Tree> trees = new List<Tree>();
        public ImageGenerator ImageGenerator;
        public int Width;
        public int Height;
        private static int HighestID = 0;
        public int ID;

        public Forest(List<String> monkeyNames, int amountOfTrees, int width, int height)
        {
            ImageGenerator = new ImageGenerator(width, height);
            ID = HighestID;
            HighestID++;
            this.Width = width;
            this.Height = height;

            trees = GenerateTrees(amountOfTrees);
            List<Monkey> monkeys = new List<Monkey>();
            Random random = new Random();
            foreach (string name in monkeyNames)
            {
                Monkey monkey = new Monkey(trees[random.Next(amountOfTrees)], this, name);
                monkeys.Add(monkey);
            }
            this.monkeys = monkeys;
        }

        private List<Tree> GenerateTrees(int amountOfTrees)
        {
            Random random = new Random();
            List<Tree> trees = new List<Tree>();
            for(int i = 0; i < amountOfTrees; i++)
            {
                int x = random.Next(Width);
                int y = random.Next(Height);
                while(CheckLocationConflict(trees, x, y))
                {
                    x = random.Next(Width);
                    y = random.Next(Height);
                }

                Tree tree = new Tree(x , y);
                ImageGenerator.drawTree(x, y);
                trees.Add(tree);
            }
            return trees;
        }

        private bool CheckLocationConflict(List<Tree> trees, int x, int y)
        {
            foreach (Tree t in trees)
            {
                if (t.X == x && t.Y == y)
                {
                    return true;
                }
            }
            return false;
        }

        public Tree GetClosestTree(Tree tree, List<Tree> VisitedTrees)
        {
            double smallestDistance = -1;
            Tree closestTree = tree;
            foreach(Tree testTree in trees)
            {
                if (VisitedTrees.Contains(testTree) == false)
                {
                    double d = tree.GetDistanceTo(testTree);
                    if (smallestDistance == -1 || d < smallestDistance)
                    {
                        smallestDistance = d;
                        closestTree = testTree;
                    }
                }
            }
            return closestTree;
        }

        public async Task Start()
        {
            List<Task> tasks = new List<Task>();
            foreach (Monkey monkey in monkeys)
            {
                tasks.Add(monkey.Jump());
            }
            Task.WaitAll(tasks.ToArray());
            
            ImageGenerator.Save(ID);
        }
    }
}
