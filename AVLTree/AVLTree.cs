using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AVLTree
{
    public class Node
    {
        public int Key;
        public Node Left;
        public Node Right;
        public int Height;

        public Node(int key)
        {
            Key = key;
            Left = null; // Awalnya tidak memiliki anak kiri
            Right = null; // Awalnya tidak memiliki anak kanan
            Height = 1; // Awalnya dianggap tinggi 1 (karena daun)
        }

    }

    public class AVL
    {
        private Node root;

        private int Height(Node node)
        {
            if (root == null)
            {
                return 0; // Jika node kosong, tinggi adalah 0
            }
            return node.Height; // Mengembalikan tinggi node
        }

        private int BalanceFactor(Node node)
        {
            if (node == null)
            {
                return 0; // Jika node kosong, faktor keseimbangan adalah 0
            }
            return Height(node.Left) - Height(node.Right); // Menghitung faktor keseimbangan
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

        public void Insert(int key)
        {
            root = Insert(root, key); // Memanggil metode Insert untuk menambahkan kunci
        }

        private Node BSTInsert(Node node, int key)
        {
            if (node == null)
            {
                return new Node(key); // Jika node kosong, buat node baru
            }
            if (key < node.Key)
            {
                node.Left = BSTInsert(node.Left, key); // Masukkan ke subtree kiri
            }
            else if (key > node.Key)
            {
                node.Right = BSTInsert(node.Right, key); // Masukkan ke subtree kanan
            }
            else
            {
                return node; // Kunci sudah ada, tidak perlu memasukkan lagi
            }
            return node; // Kembalikan node yang tidak berubah
        }

        private Node Balancing(Node node, int key)
        {
            // Get BalanceFactor
            int balance = BalanceFactor(node);

            // LL
            if (balance > 1 && key < node.Left.Key)
            {
                return RightRotate(node); // Lakukan rotasi kanan
            }

            // RR
            if (balance < -1 && key > node.Right.Key)
            {
                return LeftRotate(node); // Lakukan rotasi kiri
            }

            // LR
            if (balance > 1 && key > node.Left.Key)
            {
                node.Left = LeftRotate(node.Left); // Lakukan rotasi kiri pada anak kiri
                return RightRotate(node); // Lakukan rotasi kanan pada node ini
            }

            // RL
            if (balance < -1 && key < node.Right.Key)
            {
                node.Right = RightRotate(node.Right); // Lakukan rotasi kanan pada anak kanan
                return LeftRotate(node); // Lakukan rotasi kiri pada node ini
            }

            return node; // Kembalikan node yang tidak berubah
        }

        private Node Insert(Node node, int key)
        {
            // 1. Lakukan BST insert
            BSTInsert(node, key);

            // 2. Perbarui tinggi dari ancestor node
            node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;

            // 3. Balancing
            return Balancing(node, key);
        }

        public void InOrder(Node root)
        {
            if (root != null)
            {
                InOrder(root.Left);
                Console.Write(root.Key + " ");
                InOrder(root.Right);
            }
        }
    }
}