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
            string filename = "C:\\Users\\Zsolti\\Desktop\\önlab\\MyClassLibrary\\MyClassLibrary\\MyClassLibrary\\bin\\Debug\\MyClassLibrary.dll";
            ModuleDefMD mod = ModuleDefMD.Load(filename);


            int totalNumTypes = 0;


            if (mod.GetManagedEntryPoint() == null)
                Console.WriteLine(" Managed entry point is NULL! ");

            var moduleRefs = mod.GetModuleRefs();

            if (moduleRefs == null)
                Console.WriteLine(" Module Refs is NULL!");

            else
            {
                foreach (var item in moduleRefs)
                {
                    Console.WriteLine(item.Module);

                }
            }

            Thread.Sleep(1000);
            Console.WriteLine(" Let's start check the types ");

            // mod.Types only returns non-nested types.
            // mod.GetTypes() returns all types, including nested types.
            foreach (TypeDef type in mod.GetTypes())
            {

                Thread.Sleep(1000);

                ////// TYPE //////

                totalNumTypes++;
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
                Thread.Sleep(1000);

                Console.WriteLine("Methods inside a Type");
                Console.WriteLine("-------------------------------------");

                //// Type-on belül methodok ////

                foreach (var method in type.Methods)
                {
                    Console.WriteLine();
                    Console.WriteLine("Method Name: {0}", method.Name);
                    //Console.WriteLine("Method Parameters: {0}", method.Parameters);

                    ControlFlowGraph graph = ControlFlowGraph.Construct(method.Body);

                    Console.WriteLine("HERE IS MY GRAPH: ");
                    Console.WriteLine("==============================");
                    Console.WriteLine("Count (Numbers of blocks in CFG)) : {0}", graph.Count);
                    Console.WriteLine("Instructions:");
                    foreach (var block in graph.GetAllBlocks())
                    {
                        Console.WriteLine("instr: {0}", block.ToString());
                    }
                    Console.WriteLine("==============================");
                    Console.WriteLine();
                    Console.WriteLine();

                }



                Console.WriteLine("-------------------------------------");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();


            }
            Console.WriteLine();
            Console.WriteLine("Total number of types: {0}", totalNumTypes);
        }
    }
}
