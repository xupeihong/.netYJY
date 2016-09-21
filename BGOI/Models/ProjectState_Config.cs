using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class ProjectState_Config
    {
        private string strStateId;
        [DataField("ID", "nvarchar")]
        public string StateId
        {
            get { return strStateId; }
            set { strStateId = value; }
        }
        private string strStateDesc;
        [DataField("Text", "nvarchar")]
        public string StateDesc
        {
            get { return strStateDesc; }
            set { strStateDesc = value; }
        }
        private string strStateType;
        [DataField("Type", "nvarchar")]
        public string StateType
        {
            get { return strStateType; }
            set { strStateType = value; }
        }
    }
}
