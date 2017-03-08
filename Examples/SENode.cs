using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace SEViz.Common.Model
{
    public class SENode
    {


        public int Id
        {
            get; set;
        }


        public SENode ( int id )
        {
            Id = id;
        }


        public override string ToString ()
        {
            return Id.ToString();
        }

        public static SENode Factory ( string id )
        {
            return new SENode(int.Parse(id));
        }

    }

}
