using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAppliancesStore.ViewModels
{
    public class RoleModificationViewModel
    {
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public string[] ToAdd { get; set; }
        public string[] ToDelete { get; set; }
    }
}
