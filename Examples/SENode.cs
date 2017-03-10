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

     

        public SENode ( int id )
        {
            Id = id;
        }


        public override string ToString ()
        {
            return Id.ToString();
        }


    }

}
