using dnlib.DotNet;
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
            // mod.Types only returns non-nested types.
            // mod.GetTypes() returns all types, including nested types.
            foreach (TypeDef type in mod.GetTypes())
            {

                Thread.Sleep(2000);

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

                if (type.Interfaces.Count > 0)
                {
                    Console.WriteLine("  Interfaces:");
                    foreach (InterfaceImpl iface in type.Interfaces)
                        Console.WriteLine("    {0}", iface.Interface.FullName);
                }
            }
            Console.WriteLine();
            Console.WriteLine("Total number of types: {0}", totalNumTypes);
        }
    }
}
