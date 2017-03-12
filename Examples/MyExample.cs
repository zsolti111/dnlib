using dnlib.DotNet;
using dnlib.DotNet.Emit;
using QuickGraph;
using QuickGraph.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace dnlib.Examples
{
    public class MyExample
    {


        // Lista melyben a cúcsokat tároljuk
        public static List<CFGNode> nodes = new List<CFGNode>();

        // Lista melyben az éleket tároljuk
        public static List<CFGEdge> edges = new List<CFGEdge>();
        // gráf
        public static BidirectionalGraph<CFGNode, CFGEdge> graph = new BidirectionalGraph<CFGNode, CFGEdge>();

        public static int edgeId = 0;



        public static void Serialize ( BidirectionalGraph<CFGNode, CFGEdge> graph, string path )
        {

            var ser = new GraphMLSerializer<CFGNode, CFGEdge, BidirectionalGraph<CFGNode, CFGEdge>>();
            using (var writer = XmlWriter.Create(path + "temp.graphml", new XmlWriterSettings { Indent = true, WriteEndDocumentOnClose = false }))
            {
                ser.Serialize(writer, graph, v => v.Id.ToString(), e => e.Id.ToString());
            }
        }
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

                        // Először feltöltjük a csúcsokat
                        foreach (var block in graph.GetAllBlocks())
                        {
                            nodes.Add(new CFGNode(block.Id));
                        }

                        // Kiíratás + élek feltöltése
                        foreach (var block in graph.GetAllBlocks())
                        {
                            // Az adott instrukció ID-ja
                            Console.WriteLine("instr: {0}", block.Id);



                            // Az adott blokk forrás blokkja

                            foreach (var source in block.Sources)
                            {
                                Console.WriteLine("Source: " + source.Id);
                            }

                            // Az adott blokk cél blokkja
                            // itt hozunk létre élt 
                            foreach (var target in block.Targets)
                            {
                                Console.WriteLine("Target: " + target.Id);
                                var tempEdge = new CFGEdge(edgeId, nodes.Where(x => x.Id == block.Id).FirstOrDefault(), nodes.Where(x => x.Id == target.Id).FirstOrDefault());
                                edgeId++;
                                edges.Add(tempEdge);
                            }

                            Console.WriteLine();

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




            // A gráfhoz hozzáadjuk a csúcsokat
            graph.AddVertexRange(nodes);
            graph.AddEdgeRange(edges);
            var temppath = @"C:\Users\Zsolti\Desktop\önlab";
            Serialize(graph, temppath);





            // a gráfhoz hozzáadjuk az éleket



            Console.WriteLine("EDGES: " + graph.EdgeCount);
            Console.WriteLine("NODES: " + graph.VertexCount);





        }
    }
}
