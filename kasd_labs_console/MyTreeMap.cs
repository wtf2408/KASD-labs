﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace kasd_labs_console
{

    public class MyTreeMap<TKey, TValue> 
    {

        private IComparer<TKey> comparator;
        private Node root;
        private int size;
        public int Size { get; set; }

        private enum Color
        {
            Red,
            Black
        }

        private class Node
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public Color Color { get; set; }

            public Node(TKey key, TValue value, Color color = Color.Red, Node left = null, Node right = null)
            {
                this.Key = key;
                this.Value = value;
                this.Color = color;
                this.Left = left;
                this.Right = right;
            }
        }

        public MyTreeMap()
        {
            this.comparator = Comparer<TKey>.Default;
            this.root = null;
        }

        public MyTreeMap(IComparer<TKey> comparer)
        {
            this.comparator = comparer;
            this.root = null;
        }

        // Print method remains the same
        public void Print()
        {
            var dictionary = new Dictionary<int, List<Node>>();
            dictionary.Add(0, new List<Node> { root });
            FillDictionary(root, dictionary, 1);

            foreach (var item in dictionary.OrderBy(d => d.Key))
            {
                foreach (var item1 in item.Value)
                {
                    Console.Write($"{item1.Key}  ");
                }
                Console.WriteLine();
            }
        }

        private void FillDictionary(Node node, Dictionary<int, List<Node>> dictionary, int num)
        {
            List<Node> list = new List<Node>();

            if (node.Left != null)
            {
                list.Add(node.Left);
                FillDictionary(node.Left, dictionary, num + 1);
            }
            if (node.Right != null)
            {
                list.Add(node.Right);
                FillDictionary(node.Right, dictionary, num + 1);
            }
            if (list.Count > 0)
            {
                dictionary.Add(num, list);
            }
        }

        public void Clear()
        {
            root = null;
            size = 0;
        }

        public void Put(TKey key, TValue value)
        {
            root = PutNode(root, key, value);
            root.Color = Color.Black;  // корень всегда черный
            size++;
        }


        private Node PutNode(Node node, TKey key, TValue value)
        {
            if (node == null)
                return new Node(key, value, Color.Red); 

            int cmp = comparator.Compare(key, node.Key);

            if (cmp < 0)
                node.Left = PutNode(node.Left, key, value);
            else if (cmp > 0)
                node.Right = PutNode(node.Right, key, value);
            else
                node.Value = value;

            if (IsRed(node.Right) && !IsRed(node.Left))
                node = RotateLeft(node);

            if (IsRed(node.Left) && IsRed(node.Left.Left))
                node = RotateRight(node);

            if (IsRed(node.Left) && IsRed(node.Right))
                FlipColors(node);

            return node;
        }

        private Node RotateLeft(Node node)
        {
            Node x = node.Right;
            node.Right = x.Left;
            x.Left = node;
            x.Color = node.Color;
            node.Color = Color.Red;
            return x;
        }

        private Node RotateRight(Node node)
        {
            Node x = node.Left;
            node.Left = x.Right;
            x.Right = node;
            x.Color = node.Color;
            node.Color = Color.Red;
            return x;
        }

        private void FlipColors(Node node)
        {
            node.Color = Color.Red;
            node.Left.Color = Color.Black;
            node.Right.Color = Color.Black;
        }

        private bool IsRed(Node node)
        {
            if (node == null) return false;
            return node.Color == Color.Red;
        }

        public TValue Get(TKey key)
        {
            var node = GetNode(root, key);
            return node != null ? node.Value : default;
        }

        private Node GetNode(Node node, TKey key)
        {
            if (node == null) return null;

            int cmp = comparator.Compare(key, node.Key);
            if (cmp < 0) return GetNode(node.Left, key);
            if (cmp > 0) return GetNode(node.Right, key);
            return node;
        }

        public bool ContainsKey(object key)
        {
            if (key is TKey tKey) return ContainsKey(root, tKey);
            else throw new ArgumentException("Invalid type of key");
        }

        private bool ContainsKey(Node node, TKey key)
        {
            if (node == null) return false;

            int cmp = comparator.Compare(key, node.Key);
            if (cmp < 0) return ContainsKey(node.Left, key);
            if (cmp > 0) return ContainsKey(node.Right, key);
            return true;
        }

        public bool Remove(object key)
        {
            if (key is TKey tKey)
            {
                int initialSize = size;
                root = RemoveNode(root, tKey);
                if (root != null) root.Color = Color.Black;  // Ensure the root is black
                return size < initialSize;
            }
            else throw new ArgumentException("Invalid type of key");
        }

        private Node RemoveNode(Node node, TKey key)
        {
            if (node == null) return null;

            if (comparator.Compare(key, node.Key) < 0)
            {
                if (!IsRed(node.Left) && !IsRed(node.Left.Left))
                    node = MoveRedLeft(node);

                node.Left = RemoveNode(node.Left, key);
            }
            else
            {
                if (IsRed(node.Left))
                    node = RotateRight(node);

                if (comparator.Compare(key, node.Key) == 0 && node.Right == null)
                    return null;

                if (!IsRed(node.Right) && !IsRed(node.Right.Left))
                    node = MoveRedRight(node);

                if (comparator.Compare(key, node.Key) == 0)
                {
                    Node min = FindMin(node.Right);
                    node.Key = min.Key;
                    node.Value = min.Value;
                    node.Right = RemoveMin(node.Right);
                }
                else
                    node.Right = RemoveNode(node.Right, key);
            }
            return FixUp(node);
        }

        private Node RemoveMin(Node node)
        {
            if (node.Left == null) return null;
            if (!IsRed(node.Left) && !IsRed(node.Left.Left))
                node = MoveRedLeft(node);
            node.Left = RemoveMin(node.Left);
            return FixUp(node);
        }

        private Node MoveRedLeft(Node node)
        {
            FlipColors(node);
            if (IsRed(node.Right.Left))
            {
                node.Right = RotateRight(node.Right);
                node = RotateLeft(node);
                FlipColors(node);
            }
            return node;
        }

        private Node MoveRedRight(Node node)
        {
            FlipColors(node);
            if (IsRed(node.Left.Left))
            {
                node = RotateRight(node);
                FlipColors(node);
            }
            return node;
        }

        private Node FixUp(Node node)
        {
            if (IsRed(node.Right)) node = RotateLeft(node);
            if (IsRed(node.Left) && IsRed(node.Left.Left)) node = RotateRight(node);
            if (IsRed(node.Left) && IsRed(node.Right)) FlipColors(node);
            return node;
        }

        public bool ContainsValue(object value)
        {
            if (value is TValue tValue) return ContainsValue(root, tValue);
            else throw new ArgumentException("Invalid value type.");
        }


        private bool ContainsValue(Node node, TValue value)
        {
            if (node != null)
            {
                bool equals = object.Equals(value, node.Value);
                if (equals) return true;
                return ContainsValue(node.Left, value) || ContainsValue(node.Right, value);
            }
            return false;
        }

        public bool IsEmpty()
        {
            if (size == 0) return true;
            return false;
        }

        public HashSet<TKey> KeySet()
        {
            HashSet<TKey> entries = new HashSet<TKey>();
            CollectEntrieKeys(root, ref entries);
            return entries;
        }

        private void CollectEntrieKeys(Node node, ref HashSet<TKey> entries)
        {
            if (node == null) return;
            entries.Add(node.Key);
            CollectEntrieKeys(node.Left, ref entries);
            CollectEntrieKeys(node.Right, ref entries);
        }

        public HashSet<KeyValuePair<TKey, TValue>> EntrySet()
        {
            HashSet<KeyValuePair<TKey, TValue>> entries = new HashSet<KeyValuePair<TKey, TValue>>();
            CollectEntries(root, ref entries);
            return entries;
        }

        private void CollectEntries(Node node, ref HashSet<KeyValuePair<TKey, TValue>> entries)
        {
            if (node == null) return;
            entries.Add(new KeyValuePair<TKey, TValue>(node.Key, node.Value));
            CollectEntries(node.Left, ref entries);
            CollectEntries(node.Right, ref entries);
        }

        private Node FindMin(Node node)
        {
            while (node.Left != null) node = node.Left;
            return node;
        }

        private Node FindMax(Node node)
        {
            while (node.Right != null)
                node = node.Right;
            return node;
        }

        public TKey LastKey()
        {
            if (root == null)
                throw new InvalidOperationException("The map is empty.");

            return FindMax(root).Key;
        }

        public TKey FirstKey()
        {
            if (root == null)
                throw new InvalidOperationException("The map is empty.");
            return FindMin(root).Key;
        }

        public MyTreeMap<TKey, TValue> HeadMap(TKey end)
        {
            MyTreeMap<TKey, TValue> headMap = new MyTreeMap<TKey, TValue>(comparator);
            AddToHeadMap(root, end, ref headMap);
            return headMap;
        }

        private void AddToHeadMap(Node node, TKey end, ref MyTreeMap<TKey, TValue> headMap)
        {
            if (node == null) return;

            // Если ключ текущего узла меньше end, добавляем его в headMap
            int cmp = comparator.Compare(node.Key, end);
            if (cmp < 0)
            {
                headMap.Put(node.Key, node.Value);
                // Рекурсивно добавляем узлы из левого и правого поддерева
                AddToHeadMap(node.Left, end, ref headMap);
                AddToHeadMap(node.Right, end, ref headMap);
            }
            else if (cmp == 0)
            {
                // Если ключ равен end, прекращаем рекурсию, так как нужно включать только элементы с ключами меньше end
                return;
            }
            else
            {
                // Если текущий ключ больше end, то идем только в левое поддерево
                AddToHeadMap(node.Left, end, ref headMap);
            }
        }

        public MyTreeMap<TKey, TValue> SubMap(TKey start, TKey end)
        {
            MyTreeMap<TKey, TValue> subMap = new MyTreeMap<TKey, TValue>(comparator);
            AddToSubMap(root, start, end, subMap);
            return subMap;
        }

        private void AddToSubMap(Node node, TKey start, TKey end, MyTreeMap<TKey, TValue> subMap)
        {
            if (node == null)
                return;

            // Сравниваем ключ с границами start и end
            int cmpStart = comparator.Compare(node.Key, start);
            int cmpEnd = comparator.Compare(node.Key, end);

            // Если ключ больше или равен start и меньше end, добавляем его в subMap
            if (cmpStart >= 0 && cmpEnd < 0)
            {
                subMap.Put(node.Key, node.Value);
            }

            if (cmpStart > 0) // Если ключ больше start, идем в левое поддерево (на уменьшен)
                AddToSubMap(node.Left, start, end, subMap);

            if (cmpEnd < 0) // Если ключ меньше end, идем в правое поддерево (на увеличен)
                AddToSubMap(node.Right, start, end, subMap);
        }

        public MyTreeMap<TKey, TValue> TailMap(TKey start)
        {
            MyTreeMap<TKey, TValue> tailMapList = new MyTreeMap<TKey, TValue>();
            AddToTailMap(root, start, tailMapList);
            return tailMapList;
        }

        private void AddToTailMap(Node node, TKey start, MyTreeMap<TKey, TValue> tailMapList)
        {
            if (node == null) return;

            int cmp = comparator.Compare(node.Key, start);

            // Если ключ больше start, добавляем его в tailMapList
            if (cmp > 0)
            {
                tailMapList.Put(node.Key, node.Value);
                AddToTailMap(node.Left, start, tailMapList);
                AddToTailMap(node.Right, start, tailMapList);
            }
            else if (cmp == 0)
            {
                // Если ключ равен start, то продолжаем обход только в правом поддереве,
                AddToTailMap(node.Right, start, tailMapList);
            }
            else
            {
                // Если ключ меньше start, идем только в правое поддерево
                AddToTailMap(node.Right, start, tailMapList);
            }
        }

        private KeyValuePair<TKey, TValue>? FindEntry(TKey key, Func<int, bool> condition)
        {
            return FindEntryHelper(root, key, null, condition);
        }
        private KeyValuePair<TKey, TValue>? FindEntryHelper(Node node, TKey key, KeyValuePair<TKey, TValue>? bestEntry, Func<int, bool> condition)
        {
            if (node == null) return bestEntry;

            int cmp = comparator.Compare(node.Key, key);

            if (condition(cmp))
            {
                bestEntry = new KeyValuePair<TKey, TValue>(node.Key, node.Value);
                return FindEntryHelper(node.Right, key, bestEntry, condition);
            }
            else
            {
                return FindEntryHelper(node.Left, key, bestEntry, condition);
            }
        }

        public KeyValuePair<TKey, TValue>? LowerEntry(TKey key)
        {
            return FindEntry(key, cmp => cmp < 0);
        }

        public KeyValuePair<TKey, TValue>? FloorEntry(TKey key)
        {
            return FindEntry(key, cmp => cmp <= 0);
        }

        public KeyValuePair<TKey, TValue>? HigherEntry(TKey key)
        {
            return FindEntry(key, cmp => cmp > 0);
        }

        public KeyValuePair<TKey, TValue>? CeilingEntry(TKey key)
        {
            return FindEntry(key, cmp => cmp >= 0);
        }

        public TKey LowerKey(TKey key)
        {
            var entry = LowerEntry(key);
            return entry.HasValue ? entry.Value.Key : default;
        }

        public TKey FloorKey(TKey key)
        {
            var entry = FloorEntry(key);
            return entry.HasValue ? entry.Value.Key : default;
        }

        public TKey HigherKey(TKey key)
        {
            var entry = HigherEntry(key);
            return entry.HasValue ? entry.Value.Key : default ;
        }

        public TKey CeilingKey(TKey key)
        {
            var entry = CeilingEntry(key);
            return entry.HasValue ? entry.Value.Key : default;
        }

        public KeyValuePair<TKey, TValue>? PollFirstEntry()
        {
            if (root == null) return null;

            KeyValuePair<TKey, TValue>? firstEntry = FirstEntry();
            if (firstEntry == null) return null;
            root = RemoveNode(root, firstEntry.Value.Key);
            return firstEntry;
        }

        public KeyValuePair<TKey, TValue>? PollLastEntry()
        {
            if (root == null) return null;

            KeyValuePair<TKey, TValue>? lastEntry = LastEntry();
            if (lastEntry == null) return null;
            root = RemoveNode(root, lastEntry.Value.Key);
            return lastEntry;
        }

        public KeyValuePair<TKey, TValue>? FirstEntry()
        {
            if (root == null) return null;
            Node current = root;
            while (current.Left != null)
                current = current.Left;
            return new KeyValuePair<TKey, TValue>(current.Key, current.Value);
        }

        public KeyValuePair<TKey, TValue>? LastEntry()
        {
            if (root == null) return null;
            Node current = root;
            while (current.Right != null)
                current = current.Right;
            return new KeyValuePair<TKey, TValue>(current.Key, current.Value);
        }
    }
}