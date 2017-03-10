using QuickGraph;
using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace dnlib.Examples
{
    public class SEdge : Edge<SENode>
    {

        public int Id
        {
            get; set;
        }


        public SEdge ( int id, SENode source, SENode target ) : base(source, target)
        {
            Id = id;

        }

    }

}