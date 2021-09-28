using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerLib
{
    public class TraceResult
    {
        private int id;
        private double executionTime;

        public int getId()
        {
            return id;
        }
        public void setId(int id)
        {
            this.id = id;
        }
    }
}
