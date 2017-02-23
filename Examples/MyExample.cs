using dnlib.DotNet;
using dnlib.DotNet.Emit;
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
        public static void Run ()
        {

            /* Saját dll-t készítettem, a filename a path és egy ModuleDefMD típusú objektumba ezt betöltöm*/

            //string filename = "C:\\Users\\Zsolti\\Desktop\\önlab\\MyClassLibrary\\MyClassLibrary\\MyClassLibrary\\bin\\Debug\\MyClassLibrary.dll";

            /* Algorithms vizsgálata */
            string filename = @"C:\Users\Zsolti\Desktop\C-Sharp-Algorithms-master\C-Sharp-Algorithms-master\Algorithms\bin\Debug\Algorithms.dll";
            ModuleDefMD mod = ModuleDefMD.Load(filename);


            int totalNumTypes = 0;

            /* Kiírjuk a betöltött assembly nevét */
            Console.WriteLine(mod.Assembly.FullName);
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine();
            }
            Thread.Sleep(1000);


            if (mod.GetManagedEntryPoint() == null)
                Console.WriteLine(" Managed entry point is NULL! ");


            // mod.Types only returns non-nested types.
            // mod.GetTypes() returns all types, including nested types.
            foreach (TypeDef type in mod.GetTypes())
            {
                totalNumTypes++;

                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
                }


                ////// TYPE //////  
                Console.WriteLine();
                Console.WriteLine("Type: {0}", type.FullName);
                if (type.BaseType != null)
                    Console.WriteLine("  Base type: {0}", type.BaseType.FullName);

                Console.WriteLine("  Methods: {0}", type.Methods.Count);
                Console.WriteLine("  Fields: {0}", type.Fields.Count);
                Console.WriteLine("  Properties: {0}", type.Properties.Count);
                Console.WriteLine("  Events: {0}", type.Events.Count);
                Console.WriteLine("  Nested types: {0}", type.NestedTypes.Count);
                Console.WriteLine("  Visibility: {0}", type.Visibility);
                Console.WriteLine("  TypeOrMethodDefTag: {0}", type.TypeOrMethodDefTag);


                if (type.Interfaces.Count > 0)
                {
                    Console.WriteLine("  Interfaces:");
                    foreach (InterfaceImpl iface in type.Interfaces)
                        Console.WriteLine("    {0}", iface.Interface.FullName);
                }

                Console.WriteLine();
                Console.WriteLine(" METHODS IN A TYPE / CLASS ");
                Console.WriteLine("-------------------------------------");

                //// Type-on belül methodok ////

                foreach (var method in type.Methods)
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

                        // Az adott blokk forrás blokkja
                        foreach (var source in block.Sources)
                        {
                            Console.WriteLine("Source: " + source.Id);
                        }

                        // Az adott blokk cél blokkja
                        foreach (var target in block.Targets)
                        {
                            Console.WriteLine("Target: " + target.Id);
                        }

                        //Console.WriteLine("Footer: {0}", block.Footer.ToString());
                        //Console.WriteLine("Header: {0}", block.Header.ToString());

                        Console.WriteLine();

                    }

                    Console.WriteLine("==============================");

                }


            }

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            }
            Console.WriteLine();
            Console.WriteLine("Total number of types: {0}", totalNumTypes);
        }
    }
}
