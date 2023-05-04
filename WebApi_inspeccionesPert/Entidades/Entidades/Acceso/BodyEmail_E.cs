using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Acceso
{
    public class BodyEmailApplus_E
    {
        public string idApp { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public object toCC { get; set; }
        public string toCCO { get; set; }
        public List<Attachment_E> attachments { get; set; }
        public object to { get; set; }
    }

    public class Attachment_E
    {
        public string file { get; set; }
        public string name { get; set; }
        public string contentType { get; set; }
    }

}
