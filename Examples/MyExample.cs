﻿using dnlib.DotNet;
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

        // Key: ID
        // Value : [1] Source-k [2] Target-ek
        public static Dictionary<int, Dictionary<List<int>, List<int>>> blockList = new Dictionary<int, Dictionary<List<int>, List<int>>>();
        public static List<SENode> blocks = new List<SENode>();
        public static BidirectionalGraph<SENode, SEdge> graph = new BidirectionalGraph<SENode, SEdge>();



        public static void Run ()
        {

            /* Saját dll-t készítettem, a filename a path és egy ModuleDefMD típusú objektumba ezt betöltöm*/

            string filename = @"C:\Users\Zsolti\Desktop\önlab\MClassLibrary2\MyClassLibrary2\MyClassLibrary2\bin\Debug\MyClassLibrary2.dll";

            /* Algorithms vizsgálata */
            //string filename = @"C:\Users\Zsolti\Desktop\C-Sharp-Algorithms-master\C-Sharp-Algorithms-master\Algorithms\bin\Debug\Algorithms.dll";
            /* DataStructures vizsgálata */
            //string filename = @"C:\Users\Zsolti\Desktop\C-Sharp-Algorithms-master\C-Sharp-Algorithms-master\DataStructures\bin\Debug\DataStrcutres.dll";

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
                        //Console.WriteLine("Method Parameters: {0}", method.Parameters);

                        // A függvényből megcsináljuk a CFG-t
                        ControlFlowGraph graph = ControlFlowGraph.Construct(method.Body);

                        Console.WriteLine("==============================");
                        // Blokkok száma a CFG-ben
                        Console.WriteLine("Count (Numbers of blocks in CFG)) : {0}", graph.Count);
                        Console.WriteLine("Instructions:");

                        foreach (var block in graph.GetAllBlocks())
                        {
                            // Az adott instrukció ID-ja
                            Console.WriteLine("instr: {0}", block.Id);
                            var sourceList = new List<int>();
                            var sourceListNodes = new List<SENode>();

                            var targetList = new List<int>();
                            var targetListNodes = new List<SENode>();

                            if (block.Sources.Count == 0)
                            {
                                if (block.Targets.Count == 0)
                                {

                                }
                            }

                            // Az adott blokk forrás blokkja

                            foreach (var source in block.Sources)
                            {

                                Console.WriteLine("Source: " + source.Id);
                                var tempSENodeSource = new SENode(source.Id);
                                sourceList.Add(source.Id);
                                sourceListNodes.Add(tempSENodeSource);
                            }

                            // Az adott blokk cél blokkja
                            foreach (var target in block.Targets)
                            {
                                Console.WriteLine("Target: " + target.Id);
                                var tempSENodeTarget = new SENode(target.Id);
                                targetList.Add(target.Id);
                                targetListNodes.Add(tempSENodeTarget);
                            }

                            //Console.WriteLine("Footer: {0}", block.Footer.ToString());
                            //Console.WriteLine("Header: {0}", block.Header.ToString());

                            Console.WriteLine();

                            var tempDictionary = new Dictionary<List<int>, List<int>>();
                            tempDictionary.Add(sourceList, targetList);
                            blockList.Add(block.Id, tempDictionary);

                            var tempSENode = new SENode(block.Id, sourceListNodes, targetListNodes);
                            blocks.Add(tempSENode);

                        }



                        Console.WriteLine("==============================");
                    }

                }


            }


            foreach (var item in blocks)
            {

                Console.WriteLine("ID: " + item.Id);


                Console.WriteLine("SOURCES: ");
                foreach (var item2 in item.Source)
                {
                    Console.WriteLine(item2.Id);
                }

                Console.WriteLine("TARGETS: ");
                foreach (var item2 in item.Target)
                {
                    Console.WriteLine(item2.Id);
                }
                Console.WriteLine();
            }

            //////foreach (var blockListItem in blockList)
            //////{
            //////    Console.WriteLine("ID:" + blockListItem.Key);

            //////    foreach (var dictionaryItem in blockListItem.Value)
            //////    {
            //////        Console.WriteLine("Source: ");
            //////        foreach (var source in dictionaryItem.Key)
            //////        {
            //////            Console.WriteLine(source);
            //////        }

            //////        Console.WriteLine("Target: ");
            //////        foreach (var target in dictionaryItem.Value)
            //////        {
            //////            Console.WriteLine(target);
            //////        }
            //////    }

            //////    Console.WriteLine();
            //////}

        }
    }
}
