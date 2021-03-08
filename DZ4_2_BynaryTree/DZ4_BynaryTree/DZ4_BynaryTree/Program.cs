using System;

namespace DZ4_BynaryTree
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            * Alerxander Fakhrudinov = Александр Фахрудинов
            * asbuka@gmail.com
            * 
            * 2. Реализуйте двоичное дерево и метод вывода его в консоль
            * Реализуйте класс двоичного дерева поиска с операциями вставки, удаления, поиска. 
            * Дерево должно быть сбалансированным (это требование не обязательно). 
            * Также напишите метод вывода в консоль дерева, чтобы увидеть, насколько корректно работает ваша реализация. 
            */
            
            Console.SetWindowSize(120,30);

            bool allpassed = true;
            
            ///проверка AddItem(value);
            Tests[] testSuitAddNode = new Tests[2];
            testSuitAddNode[0] = new Tests()//проверка добавления первой ноды
            {
                setValues = new int[] { 99 },
                expectedValues = new int?[] { 99 }
            };
            testSuitAddNode[1] = new Tests()//проверка добавления нод после первой
            {
                setValues = new int[] { 1, 2, 3 },
                expectedValues = new int?[] { 1, 2, 3 },
            };
            foreach (Tests testCaseAddNode in testSuitAddNode)
            {
                //сначала добавляем ноды 
                PrepareTestEnvironment(testCaseAddNode.setValues);

                //затем собираем все значения актуальных нод и сравниваем с контрольным
                CompareTestResult("AddItem(value)", testCaseAddNode.expectedValues, ref allpassed);
            }

            /// RemoveNode by value
            Tests[] testSuitRemoveNode = new Tests[4];
            testSuitRemoveNode[0] = new Tests()// remove first
            {
                setValues = new int[] { 1, 2, 3 },
                expectedValues = new int?[] { 2, 3 },
                nodeTarget = 1
            };
            testSuitRemoveNode[1] = new Tests()//remove middle
            {
                setValues = new int[] { 1, 2, 3 },
                expectedValues = new int?[] { 1, 3 },
                nodeTarget = 2
            };
            testSuitRemoveNode[2] = new Tests()//remove last
            {
                setValues = new int[] { 1, 2, 3 },
                expectedValues = new int?[] { 1, 2 },
                nodeTarget = 3
            };
            testSuitRemoveNode[3] = new Tests()//remove non exist
            {
                setValues = new int[] { 1, 2, 3 },
                expectedValues = new int?[] { 1, 2, 3 },
                nodeTarget = 10
            };
            foreach (Tests testCaseRemoveNode in testSuitRemoveNode)
            {
                //сначала добавляем ноды 
                PrepareTestEnvironment(testCaseRemoveNode.setValues);

                int target = (int)testCaseRemoveNode.nodeTarget;

                UsingTree nodeToRemove = new UsingTree();
                nodeToRemove.RemoveItem(target);

                //затем собираем все значения актуальных нод и сравниваем с контрольным
                CompareTestResult($"RemoveItem({target})", testCaseRemoveNode.expectedValues, ref allpassed);
            }


            ///Node = GetNodeByValue(int searchValue)
            Tests[] testSuitFindNode = new Tests[4];
            testSuitFindNode[0] = new Tests()// FindNode first
            {
                setValues = new int[] { 1, 2, 3 },
                expectedValues = new int?[] { 1 },
                nodeTarget = 1
            };
            testSuitFindNode[1] = new Tests()//FindNode middle
            {
                setValues = new int[] { 1, 2, 3 },
                expectedValues = new int?[] { 1 },
                nodeTarget = 1
            };
            testSuitFindNode[2] = new Tests()//FindNode last
            {
                setValues = new int[] { 1, 2, 3 },
                expectedValues = new int?[] { 3 },
                nodeTarget = 3
            };
            testSuitFindNode[3] = new Tests()//FindNode non exist
            {
                setValues = new int[] { 1, 2, 3 },
                expectedValues = new int?[] { null },
                nodeTarget = 10
            };
            foreach (Tests testCaseFindNode in testSuitFindNode)
            {
                //сначала добавляем ноды 
                PrepareTestEnvironment(testCaseFindNode.setValues);

                int target = (int)testCaseFindNode.nodeTarget;

                UsingTree node = new UsingTree();
                Node sNode = node.GetNodeByValue(target);

                CheckFindTestresult(sNode, $"FindNode({target})", testCaseFindNode.expectedValues[0], ref allpassed);
            }


            if (allpassed)
            {
                Console.WriteLine("All Tests passed, OK.");


                UsingTree setNode = new UsingTree();
                //setNode.AddItem(777);
                setNode.AddNodesRange(1, 2, 34, 4, 5, 62, 7, 81, 9, 10, 11, 122, 122, 14, 159, 16);

                //Node rNode = setNode.GetRoot();
                //Console.WriteLine(">>" + rNode.Value);

                //Node sNode = setNode.GetNodeByValue(42);
                //Console.WriteLine("??" + sNode.Value);

                //setNode.RemoveItem(5);

                setNode.PrintTree();
            }


            Console.ReadLine();
        }

        private static void CheckFindTestresult(Node result, string testName, int? expected, ref bool allpassed)
        {

            if (result != null)
            {
                if (result.Value != expected)
                {
                    allpassed = false;
                    Console.WriteLine($"Ошибка! Для метода {testName} ожидаемый результат {expected} != фактическому {result.Value}");
                }
            }
            else
            {
                if (expected != null)
                {
                    string actVal = result?.Value.ToString() ?? "null";
                    Console.WriteLine($"Ошибка! Для метода {testName} ожидаемый результат {expected} != фактическому {actVal}");
                    allpassed = false;
                }
            }

            //затем зачищаем за собой - удаляем все ноды.
            DeleteAlNodes();
        }

        private static void CompareTestResult(string testName, int?[] expectedValues, ref bool allpassed)
        {
            UsingTree compare = new UsingTree();
            int[] actual = compare.GetAllNodes();

            if (expectedValues.Length != actual.Length)
            {
                Console.WriteLine($"Ошибка! Для метода {testName} количество ожидаемых и фактических результатов не равно.");
                Console.WriteLine("Ожидаемые: ");
                foreach (int ex in expectedValues) 
                {
                    Console.Write(ex + ", ");                
                }

                Console.Write ("  Фактические : ");
                foreach (int ex in actual)
                {
                    Console.Write(ex + ", ");
                }

                allpassed = false;
            }
            else
            {
                for (int i = 0; i < expectedValues.Length; i++)
                {
                    if (expectedValues[i] != actual[i])
                    {
                        Console.WriteLine($"Ошибка! Для метода {testName} " +
                            $"фактическое значение {actual[i]} " +
                            $"не равно для ожидаемого {expectedValues[i]}");
                        allpassed = false;
                    }
                }
            }

            //затем зачищаем за собой - удаляем все ноды.
            DeleteAlNodes();
        }

        private static void PrepareTestEnvironment(int[] setValues)
        {
            UsingTree prepare = new UsingTree();

            foreach (int value in setValues)
            {
                prepare.AddItem(value);
            }
        }

        private static void DeleteAlNodes()
        {
            UsingTree delete = new UsingTree();

            int[] del = delete.GetAllNodes();

            foreach (int itemToDel in del)
            {
                delete.RemoveItem(itemToDel);
            }
        }
    }
}
