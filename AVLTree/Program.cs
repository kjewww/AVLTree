using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVLTree
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AVL tree = new AVL();
            tree.Insert(10);
            tree.Insert(20);
            tree.Insert(30);
            tree.Insert(40);
            tree.Insert(50);
            tree.Insert(25);

            tree.InOrder(); // Output harus: 10 20 25 30 40 50
        }
    }
}



