using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASessionOnSession.Models
{
    public class AllTheSessionDataViewModel
    {
        public List<MyPuppyAndPopsicleDream> Dreams { get; set; }
        public MyPuppyAndPopsicleDream SingleObjectInSession { get; set; }
        public string UsernameInSession { get; set; }

        public bool DreamsExist
        {
            get
            {
                if (Dreams == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
