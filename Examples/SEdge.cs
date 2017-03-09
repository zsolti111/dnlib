using QuickGraph;
using System;
using System.ComponentModel;


namespace dnlib.Examples
{
    public class SEdge : IEdge<SENode>
    {

        public int Id
        {
            get; set;
        }

        public SENode Source
        {
            get
            {
                throw new NotImplementedException();
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

        public static SEdge Factory ( SENode source, SENode target, string id )
        {
            return new SEdge(Int32.Parse(id), source, target);
        }

    }

}