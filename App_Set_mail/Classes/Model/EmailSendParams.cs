using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Set_mail.Classes.Model
{
    public class EmailSendParams
    {
        public List<string> Destinatarios { get; set; }
        public List<string> Copias { get; set; }
        public string Subject { get; set; }
        public string Nombre { get; set; }
        public string Body { get; set; }
    }
}
