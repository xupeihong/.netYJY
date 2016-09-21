using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_SUPSugestion
    {
        private string _sid;
        [DataFieldAttribute("Sid", "varchar")]
        public string Sid
        {
            get { return _sid; }
            set { _sid = value; }
        }
        private string _SState;
        [DataFieldAttribute("SState", "varchar")]
        public string SState
        {
            get { return _SState; }
            set { _SState = value; }
        }
        private string _Sperson;
        [DataFieldAttribute("Sperson", "varchar")]
        public string Sperson
        {
            get { return _Sperson; }
            set { _Sperson = value; }
        }
        private string _SCreate;
        [DataFieldAttribute("SCreate", "datetime")]
        public string SCreate
        {
            get { return _SCreate; }
            set { _SCreate = value; }
        }
        private string _SContent;
        [DataFieldAttribute("SContent", "varchar")]
        public string SContent
        {
            get { return _SContent; }
            set { _SContent = value; }
        }
    }
}
