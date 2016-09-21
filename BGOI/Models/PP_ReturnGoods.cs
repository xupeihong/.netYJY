using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class PP_ReturnGoods
    {
        private string strTHID;
        [DataField("THID", "varchar")]
        public string THID
        {
            get { return strTHID; }
            set { strTHID = value; }
        }

        private string strSHID;
        [DataField("SHID", "varchar")]
        public string SHID
        {
            get { return strSHID; }
            set { strSHID = value; }
        }

        private string strOrderUnit;
        [DataField("OrderUnit", "varchar")]
        public string OrderUnit
        {
            get { return strOrderUnit; }
            set { strOrderUnit = value; }
        }


        private string strXXID;
        [DataField("XXID", "varchar")]
        public string XXID
        {
            get { return strXXID; }
            set { strXXID = value; }
        }


        private string strReturnType;
        [DataField("ReturnType", "varchar")]
        public string ReturnType
        {
            get { return strReturnType; }
            set { strReturnType = value; }
        }


        private string strReturnMode;
        [DataField("ReturnMode", "varchar")]
        public string ReturnMode
        {
            get { return strReturnMode; }
            set { strReturnMode = value; }
        }


        private string strTheProject;
        [DataField("TheProject", "varchar")]
        public string TheProject
        {
            get { return strTheProject; }
            set { strTheProject = value; }
        }


        private string strReturnAgreement;
        [DataField("ReturnAgreement", "varchar")]
        public string ReturnAgreement
        {
            get { return strReturnAgreement; }
            set { strReturnAgreement = value; }
        }


        private string strReturnHandler;
        [DataField("ReturnHandler", "varchar")]
        public string ReturnHandler
        {
            get { return strReturnHandler; }
            set { strReturnHandler = value; }
        }


        private string strReturnDescription;
        [DataField("ReturnDescription", "varchar")]
        public string ReturnDescription
        {
            get { return strReturnDescription; }
            set { strReturnDescription = value; }
        }


        private string strReturnDate;
        [DataField("ReturnDate", "varchar")]
        public string ReturnDate
        {
            get { return strReturnDate; }
            set { strReturnDate = value; }
        }


        private DateTime dtCreateTime;
        [DataFieldAttribute("CreateTime", "datetime")]
        public DateTime CreateTime
        {
            get { return dtCreateTime; }
            set { dtCreateTime = value; }
        }
        private string strCreateUser;
        [DataFieldAttribute("CreateUser", "string")]
        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }
        private string strValidate;
        [DataFieldAttribute("Validate", "string")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }
    }
}
