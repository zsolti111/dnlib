using dnlib.DotNet;
using dnlib.DotNet.Emit;
using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dnlib.Examples
{
    public class MyExample
    {
        // Letároluk az adott Node ID-jához tartozó Source, és Targeteket
        public static Dictionary<int, Dictionary<List<SENode>, List<SENode>>> nodeContainer = new Dictionary<int, Dictionary<List<SENode>, List<SENode>>>();

        // Lista melyben a cúcsokat tároljuk
        public static List<SENode> blocks = new List<SENode>();
        // gráf
        public static BidirectionalGraph<SENode, SEdge> graph = new BidirectionalGraph<SENode, SEdge>();

        public static int edgeId = 0;



        public static void Run ()
        {


            string filename = @"C:\Users\Zsolti\Desktop\önlab\MClassLibrary2\MyClassLibrary2\MyClassLibrary2\bin\Debug\MyClassLibrary2.dll";


            ModuleDefMD mod = ModuleDefMD.Load(filename);



            foreach (TypeDef type in mod.GetTypes())
            {

                //// Type-on belül methodok ////

                foreach (var method in type.Methods)
                {

                    // -----------------------------------
                    // A Foo-ra szedünk ki értékeket csak
                    // -----------------------------------

                    if (method.Name == "Foo")
                    {
                        Console.WriteLine();
                        Console.WriteLine("Method Name: {0}", method.Name);

                        ControlFlowGraph graph = ControlFlowGraph.Construct(method.Body);

                        Console.WriteLine("==============================");

                        //-------------------------------------
                        //-------------------------------------
                        //-------------------------------------
                        //-------------------------------------

                        foreach (var block in graph.GetAllBlocks())
                        {
                            // Az adott instrukció ID-ja
                            Console.WriteLine("instr: {0}", block.Id);
                            blocks.Add(new SENode(block.Id));

                            var sourceListNodes = new List<SENode>();
                            var targetListNodes = new List<SENode>();


                            // Az adott blokk forrás blokkja

                            foreach (var source in block.Sources)
                            {

                                Console.WriteLine("Source: " + source.Id);
                                var tempSENodeSource = new SENode(source.Id);
                                sourceListNodes.Add(tempSENodeSource);
                            }

                            // Az adott blokk cél blokkja
                            foreach (var target in block.Targets)
                            {
                                Console.WriteLine("Target: " + target.Id);
                                var tempSENodeTarget = new SENode(target.Id);
                                targetListNodes.Add(tempSENodeTarget);
                            }


                            Console.WriteLine();

                            var tempDictionary = new Dictionary<List<SENode>, List<SENode>>();
                            tempDictionary.Add(sourceListNodes, targetListNodes);
                            nodeContainer.Add(block.Id, tempDictionary);





                        }

                        //-------------------------------------
                        //-------------------------------------
                        //-------------------------------------
                        //-------------------------------------




                        Console.WriteLine("==============================");
                    }

                }


            }




            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine();
            }
            //----------------------------------
            //----------------------------------
            //    MEGNÉZZÜK JÓ-E A CONTAINER
            //----------------------------------
            //----------------------------------
            Console.WriteLine("NODE CONTAINER");
            foreach (var node in nodeContainer)
            {
                Console.WriteLine("ID: ");
                Console.WriteLine(node.Key);

                foreach (var node2 in node.Value)
                {
                    Console.WriteLine("SOURCE:");
                    foreach (var item in node2.Key)
                    {
                        Console.WriteLine(item.Id);
                    }


                    Console.WriteLine("TARGET:");
                    foreach (var item in node2.Value)
                    {
                        Console.WriteLine(item.Id);
                    }
                }
                Console.WriteLine();
            }

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine();
            }

            // A gráfhoz hozzáadjuk a csúcsokat
            foreach (var item in blocks)
            {
                graph.AddVertex(item);
            }




            // a gráfhoz hozzáadjuk az éleket



            foreach (var node in nodeContainer)
            {
                foreach (var block in blocks)
                {
                    if (node.Key == block.Id)
                    {
                        foreach (var node2 in node.Value)
                        {
                            // TARGETS
                            foreach (var item in node2.Value)
                            {
                                graph.AddEdge(new SEdge(edgeId, block, item));
                                edgeId++;
                            }
                        }
                    }
                }
            }

            Console.WriteLine("EDGES: " + graph.EdgeCount);
            Console.WriteLine("NODES: " + graph.VertexCount);
            Console.WriteLine("EDGEEES:" + edgeId);




        }
    }
}
