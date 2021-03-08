using System;
using System.Collections.Generic;

namespace DZ4_BynaryTree
{
    public class UsingTree : ITree
    {
        public static Node rootNode;

        public void PrintTree()
        {
            Console.Clear();
            Console.WriteLine("Построение двоичнго дерева.");
            Console.WriteLine();

            ConsolePrintTree();

            PrintUserControls(" ");
            //PrintTree((Console.WindowWidth / 3), 15, rootNode); //отрисовка рекурсией
        }

        private void PrintUserControls(string error)
        {
            Console.WriteLine(); 
            Console.WriteLine();            
            Console.WriteLine("Введите +'значение' для добавления узла, например +33");
            Console.WriteLine("Введите -'значение' для удаления узла, например -11");
            Console.WriteLine("Введите ?'значение' для проверки наличия узла со значением, например ?11");
            Console.WriteLine("Введите == для ребалансировки дерева, например ==");
            
            if (error[0].Equals('+'))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                error = error.Substring(1);
            }
            else
                Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(error);
            Console.ResetColor();

            Console.WriteLine("                                                                            ");
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            string input = Console.ReadLine();

            if (input.Length == 0)
            {
                ReprintUserControls("Не удалось распознать команду.");
            }

            if (input[0].Equals('+'))
            {
                if (Int32.TryParse(input.Substring(1), out int addValue))
                {
                    AddItem(addValue);

                    PrintTree();
                }
                else
                {
                    ReprintUserControls("Число не удалось распознать");
                }
            }
            else if (input[0].Equals('-'))
            {
                if (Int32.TryParse(input.Substring(1), out int remValue))
                {
                    RemoveItem(remValue);

                    PrintTree();
                }
                else
                {
                    ReprintUserControls("Число не удалось распознать");
                }
            }
            else if (input[0].Equals('?'))
            {
                if (Int32.TryParse(input.Substring(1), out int serchValue))
                {
                    Node exist = GetNodeByValue(serchValue);

                    if(exist == null)
                    {
                        ReprintUserControls($"Узел {serchValue} не найден.          ");
                    }
                    else
                    {
                        ReprintUserControls($"+Узел {serchValue} найден.            ");
                    }
                }
                else
                {
                    ReprintUserControls("Число не удалось распознать");
                }
            }
            else if (input[0].Equals('='))
            {
                if (input.Equals("=="))
                {
                    int[] nodesBelowRoot = GetAllNodesBelow(rootNode);
                    int[] nodesAll = new int[nodesBelowRoot.Length + 1];

                    Array.Copy(nodesBelowRoot, nodesAll, nodesBelowRoot.Length);
                    nodesAll[nodesAll.Length - 1] = rootNode.Value;

                    rootNode = null;
                    AddNodesRange(nodesAll);

                    PrintTree();
                }
                else
                {
                    ReprintUserControls("Не удалось распознать команду.");
                }
            }
            else
            {
                ReprintUserControls("Не удалось распознать команду.");
            }
        }

        private void ReprintUserControls(string error)
        {
            Console.SetCursorPosition(0, Console.CursorTop - 8);
            PrintUserControls(error);
        }

        private void ConsolePrintTree()
        {
            if (rootNode != null)
            {
                int startX = Console.WindowWidth / 2 - 5;

                var bufer = new Queue<NodeInfo>();

                var root = new NodeInfo()
                {
                    Node = rootNode,
                    Depth = 1,
                    XPosition = startX
                };
                bufer.Enqueue(root);

                while (bufer.Count != 0)
                {
                    var element = bufer.Dequeue();

                    Console.SetCursorPosition(element.XPosition, element.Depth);
                    Console.Write("[" + element.Node.Value + "]");

                    var depth = element.Depth + 1;

                    int xPosition = startX / 2;
                    for (int i = 0; i < element.Depth - 1; i++)
                    {
                        xPosition = xPosition / 2;
                    }
                    if (element.Node.RightChild != null)
                    {
                        var right = new NodeInfo()
                        {
                            Node = element.Node.RightChild,
                            Depth = depth,
                            XPosition = element.XPosition + xPosition
                        };
                        bufer.Enqueue(right);

                        if (xPosition - 2 - element.Node.Value.ToString().Length > 0)
                            Console.Write(new string('_', xPosition - 2 - element.Node.Value.ToString().Length));
                    }

                    if (element.Node.LeftChild != null)
                    {
                        var left = new NodeInfo()
                        {
                            Node = element.Node.LeftChild,
                            Depth = depth,
                            XPosition = element.XPosition - xPosition
                        };
                        bufer.Enqueue(left);

                        if (xPosition - 2 - element.Node.Value.ToString().Length > 0)
                        {
                            Console.SetCursorPosition(element.XPosition - xPosition + 2 + element.Node.LeftChild.Value.ToString().Length,
                                element.Depth);
                            Console.Write(new string('_', xPosition - 2 - element.Node.LeftChild.Value.ToString().Length));
                        }
                    }
                }
            }    
        }

        //отрисовка рекурсией
        //public void PrintTree(int x, int y, Node root, int delta = 0)
        //{
        //    if (root != null)
        //    {
        //        if (delta == 0) delta = x / 2;
        //        Console.SetCursorPosition(x, y);
        //        Console.Write("(" + root.Value + ")");
        //        PrintTree(x - delta, y + 2, root.LeftChild, delta / 2);
        //        PrintTree(x + delta, y + 2, root.RightChild, delta / 2);
        //    }
        //}

        public void AddItem(int value)
        {
            Node newNode = new Node(value);
            Node currentNode = rootNode;

            bool nodeAdded = false;
            while (!nodeAdded)
            {
                if (rootNode == null)
                {
                    rootNode = newNode;
                    nodeAdded = true;
                }
                else
                {
                    if (value == currentNode.Value) // налево 
                    {
                        //ничего не делаем, выходим из while 
                        nodeAdded = true;
                    }
                    else if (value < currentNode.Value) // налево 
                    {
                        if (currentNode.LeftChild == null)
                        {
                            // добавляем 
                            currentNode.LeftChild = newNode;
                            nodeAdded = true;
                        }
                        else
                        {
                            currentNode = currentNode.LeftChild;
                        }
                    }
                    else // направо
                    {
                        if (currentNode.RightChild == null)
                        {
                            // добавляем 
                            currentNode.RightChild = newNode;
                            nodeAdded = true;
                        }
                        else
                        {
                            currentNode = currentNode.RightChild;
                        }
                    }
                }
            }
        }

        public void AddNodesRange(params int[] nodeValues)
        {
            // nodeValues отсортировать для баланса
            nodeValues = SortArrayToBalance(nodeValues);

            for (int i = 0; i < nodeValues.Length; i++)
            {
                AddItem(nodeValues[i]);
            }
        }

        private int[] SortArrayToBalance(int[] nodeValues)
        {
            Array.Sort(nodeValues);

            List<int> list = new List<int>(nodeValues);
            List<int> listTarget = new List<int>();

            //Array.Clear(nodeValues, 0, nodeValues.Length); // необязательно, только для дебага

            GetMiddleValue(list, listTarget);

             nodeValues = listTarget.ToArray();
            return nodeValues;
        }

        private void GetMiddleValue(List<int> listSource, List<int> listTarget)
        {
            List<int> leftList = new List<int>();
            List<int> rightList = new List<int>();

            for (int i = 0; i < listSource.Count / 2; i++)
            {
                leftList.Add(listSource[i]);
            }

            for (int i = listSource.Count / 2 + 1; i < listSource.Count ; i++)
            {
                rightList.Add(listSource[i]);
            }

            listTarget.Add(listSource[listSource.Count / 2]);
            listSource.RemoveAt(listSource.Count / 2);

            if(leftList.Count > 0)
            GetMiddleValue(leftList, listTarget);

            if (rightList.Count > 0)
                GetMiddleValue(rightList, listTarget);
        }

        public Node GetNodeByValue(int value)
        {
            Node currentNode = rootNode;

            bool searching = true;
            while (searching)
            {
                if (currentNode.Value == value)
                {
                    return currentNode;
                }
                else if (value > currentNode.Value) // направо
                {
                    if (currentNode.RightChild == null)// выходим из цикла, ничего не найдено
                    {
                        searching = false;
                    }
                    else
                    {
                        currentNode = currentNode.RightChild;
                    }
                }
                else // налево
                {
                    if (currentNode.LeftChild == null)// выходим из цикла, ничего не найдено
                    {
                        searching = false;
                    }
                    else
                    {
                        currentNode = currentNode.LeftChild;
                    }
                }
            }

            // ничего не нашли
            return null;
        }

        public Node GetRoot()
        {
            return rootNode;
        }

        public void RemoveItem(int value)
        {
            Node deleteNode = GetNodeByValue(value);

            if (deleteNode != null)
            {
                int[] nodeValues = GetAllNodesBelow(deleteNode);//найти все потомство

                if (!deleteNode.Equals(rootNode))
                {
                    //найти предка и удалить ссылку на deleteNode
                    Node ancector = FindAncestor(deleteNode);

                    if (ancector != null)//проверка удаления root
                    {
                        if (value > ancector.Value)
                            ancector.RightChild = null;
                        else
                            ancector.LeftChild = null;
                    }
                }

                if (deleteNode.Equals(rootNode))
                {
                    rootNode = null;
                }

                //добавить ноды 
                if (nodeValues.Length > 0)
                {
                    nodeValues = SortArrayToBalance(nodeValues);
                    AddNodesRange(nodeValues);
                }
            }
        }

        private Node FindAncestor(Node deleteNode)
        {           
            if (deleteNode.Equals(rootNode)) // нет предка у рута
            {
                return null;
            }
            
            Node currentNode = rootNode;
            Node ancestorNode = null;

            bool searching = true;
            while (searching)
            {
                if (deleteNode.Equals(currentNode))
                {
                    return ancestorNode;
                }
                else
                {
                    if (deleteNode.Value < currentNode.Value) // налево 
                    {
                        if (currentNode.LeftChild == null)
                        {
                            return null;
                        }
                        else
                        {
                            ancestorNode = currentNode;
                            currentNode = currentNode.LeftChild;
                        }
                    }
                    else // направо
                    {
                        if (currentNode.RightChild == null)
                        {
                            return null;
                        }
                        else
                        {
                            ancestorNode = currentNode;
                            currentNode = currentNode.RightChild;
                        }
                    }
                }
            }

            return null;
        }

        public int[] GetAllNodes()
        {
            Node root = rootNode;

            int[] nodesBelow = GetAllNodesBelow(root);

            int[] nodeValues = new int[nodesBelow.Length + 1];

            nodeValues[0] = rootNode.Value;
            for (int i = 0; i < nodesBelow.Length; i++)
            {
                nodeValues[i+1] = nodesBelow[i];
            }

            Array.Sort(nodeValues);

            return nodeValues;
        }

        private int[] GetAllNodesBelow(Node node)
        {
            List<int> list = new List<int>();
            var bufer = new Queue<Node>();

            bufer.Enqueue(node);

            while (bufer.Count != 0)
            {
                Node element = bufer.Dequeue();
                if(element.Value != node.Value)// не добавляем первую ноду
                    list.Add(element.Value);

                if (element.LeftChild != null)
                {
                    bufer.Enqueue(element.LeftChild);
                }
                if (element.RightChild != null)
                {
                    bufer.Enqueue(element.RightChild);
                }
            }

            int[] nodeValues = list.ToArray();
            return nodeValues;
        }
    }
}
