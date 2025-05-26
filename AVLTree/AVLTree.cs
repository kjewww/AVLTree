using System;

namespace AVLTree
{
    public class Node
    {
        public int Data;
        public Node Left;
        public Node Right;
        public int Height;

        public Node(int data)
        {
            Data = data;
            Left = null;
            Right = null;
            Height = 1;
        }
    }

    public class AVL
    {
        private Node root;

        private int Height(Node node)
        {
            if (node == null)
            {
                return 0;
            }
            return node.Height;
        }

        private int BalanceFactor(Node node)
        {
            if (node == null)
            {
                return 0;
            }
            return Height(node.Left) - Height(node.Right);
        }

        private Node RightRotate(Node y)
        {
            Node x = y.Left; // Ambil anak kiri
            Node T2 = x.Right; // Simpan anak kanan dari x

            // Lakukan rotasi

            x.Right = y;
            y.Left = T2;

            // Perbarui tinggi

            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;
            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;

            return x; // Kembalikan node baru yang menjadi akar
        }

        private Node LeftRotate(Node x)
        {
            Node y = x.Right; // Ambil anak kanan
            Node T2 = y.Left; // Simpan anak kiri dari y

            // Lakukan rotasi

            y.Left = x;
            x.Right = T2;

            // Perbarui tinggi

            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;
            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;

            return y; // Kembalikan node baru yang menjadi akar
        }

        private Node BSTInsert(Node root, int Data)
        {
            if (root == null)
            {
                root = new Node(Data);
                return root; // Jika node kosong, buat node baru
            }
            if (Data < root.Data)
            {
                root.Left = BSTInsert(root.Left, Data); // Masukkan ke subtree kiri
            }
            else if (Data > root.Data)
            {
                root.Right = BSTInsert(root.Right, Data); // Masukkan ke subtree kanan
            }
            else
            {
                return root; // Kunci sudah ada, tidak perlu memasukkan lagi
            }

            return root; // Kembalikan node yang tidak berubah
        }

        private Node Balancing(Node node, int Data)
        {
            // Get BalanceFactor
            int balance = BalanceFactor(node);

            // LL
            if (balance > 1 && Data < node.Left.Data)
            {
                return RightRotate(node); // Lakukan rotasi kanan
            }

            // RR
            if (balance < -1 && Data > node.Right.Data)
            {
                return LeftRotate(node); // Lakukan rotasi kiri
            }

            // LR
            if (balance > 1 && Data > node.Left.Data)
            {
                node.Left = LeftRotate(node.Left); // Lakukan rotasi kiri pada anak kiri
                return RightRotate(node); // Lakukan rotasi kanan pada node ini
            }

            // RL
            if (balance < -1 && Data < node.Right.Data)
            {
                node.Right = RightRotate(node.Right); // Lakukan rotasi kanan pada anak kanan
                return LeftRotate(node); // Lakukan rotasi kiri pada node ini
            }

            return node; // Kembalikan node yang tidak berubah
        }

        private Node Insert(Node node, int Data)
        {
            node = BSTInsert(node, Data);
            node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;

            return Balancing(node, Data);
        }

        public void Insert(int Data)
        {
            root = Insert(root, Data);
        }

        public void InOrder()
        {
            InOrder(root);
        }

        public void InOrder(Node root)
        {
            if (root != null)
            {
                InOrder(root.Left);
                Console.Write(root.Data + " ");
                InOrder(root.Right);
            }
        }
    }
}