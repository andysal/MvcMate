using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MvcMate.Samples.Models.Helpers
{
    public class UploadForInNestedViewModel
    {
        public ChildViewModel Child { get; set; }

        public class ChildViewModel
        {
            [Required]
            public HttpPostedFileBase TheRequiredFile { get; set; }
        }
    }
}
