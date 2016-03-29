using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitClassLibrary
{
    public class MainClass
    {
        public int method1()
        {
            return 100;
        }

        public bool method2(int parm1, int parm2)
        {
            return parm1 >= parm2 ? true : false;
        }

        public void method3()
        {
            throw new ApplicationException();
        }
    }
}
