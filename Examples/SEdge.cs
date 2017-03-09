using QuickGraph;
using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace dnlib.Examples
{
    public class SEdge : IEdge<SENode>
    {

        public int Id
        {
            get; set;
        }

        public List<SENode> Sources
        {
            get; set;
        }

        public List<SENode> Targets
        {
            get; set;
        }

        public SENode Source
        {
            get
            {

                return null;
            }
        }

        public SENode Target
        {
            get
            {
                throw new NotImplementedException();

            }
        }

        public SEdge ( int id, SENode source, SENode target )
        {
            Id = id;
        }

        public SEdge ( int id, List<SENode> sources, List<SENode> targets )
        {
            Sources = sources;
            Targets = targets;
        }


    }

}