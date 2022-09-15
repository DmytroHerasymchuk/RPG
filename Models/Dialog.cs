using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Dialog
    {
        public Dictionary<string, string> DialogCatalog = new Dictionary<string, string>();

        public Dialog(Dictionary<string, string> dialogCatalog)
        {
            DialogCatalog = dialogCatalog;
        }
    }
}
