using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace dnlib.Examples
{
    public class SENode
    {


        public int Id
        {
            get; set;
        }

        public List<SENode> Source
        {
            get; set;
        }

        public List<SENode> Target
        {
            get; set;
        }

        public SENode ( int id )
        {
            Id = id;
        }

        public SENode ( int id, List<SENode> sourceOrTargets, string sourceOrTarget )
        {
            Id = id;

            if (sourceOrTarget == "source")
            {
                Source = new List<SENode>();
                Source = sourceOrTargets;
            }

            if (sourceOrTarget == "target")
            {
                Target = new List<SENode>();
                Target = sourceOrTargets;
            }


        }

        public SENode ( int id, List<SENode> sources, List<SENode> targets )
        {
            Id = id;
            Source = new List<SENode>();
            Source = sources;

            Target = new List<SENode>();
            Target = targets;
        }


        public override string ToString ()
        {
            return Id.ToString();
        }


    }

}
