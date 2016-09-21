using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_SYRDetail
    {
        private string _YRID;
        [DataFieldAttribute("YRID", "varchar")]
        public string YRID
        {
            get { return _YRID; }
            set { _YRID = value; }
        }
        private string _Year;
        [DataFieldAttribute("Year", "varchar")]
        public string Year
        {
            get { return _Year; }
            set { _Year = value; }
        }
        private string _SID;
        [DataFieldAttribute("SID", "varchar")]
        public string SID
        {
            get { return _SID; }
            set { _SID = value; }
        }

        private string _Score1;
        [RegularExpression(@"^(\d|10)?$", ErrorMessage = "请输入0-10的数字")]
        [DataFieldAttribute("Score1", "varchar")]
        public string Score1
        {
            get { return _Score1; }
            set { _Score1 = value; }
        }
        private string _Score2;
        [RegularExpression(@"^([0-9]|[0-3]\d|40)?$", ErrorMessage = "请输入0-40的数字")]
        [DataFieldAttribute("Score2", "varchar")]
        public string Score2
        {
            get { return _Score2; }
            set { _Score2 = value; }
        }
        private string _Score3;
        [RegularExpression(@"^(([0-9])|([0-1][0-9])|([0-2][0-5]))?$", ErrorMessage = "请输入0-25的数字")]
        [DataFieldAttribute("Score3", "varchar")]
        public string Score3
        {
            get { return _Score3; }
            set { _Score3 = value; }
        }
        private string _Score4;
        [RegularExpression(@"^(([0-9])|([0-1][0-9])|([0-2][0-5]))?$", ErrorMessage = "请输入0-25的数字")]
        [DataFieldAttribute("Score4", "varchar")]
        public string Score4
        {
            get { return _Score4; }
            set { _Score4 = value; }
        }
        private string _Score5;
        [RegularExpression(@"^(0|[0-9][0-9]?|100)?$", ErrorMessage = "请输入0-100的数字")]
        [DataFieldAttribute("Score5", "varchar")]
        public string Score5
        {
            get { return _Score5; }
            set { _Score5 = value; }
        }
        private string _Result;
        [DataFieldAttribute("Result", "varchar")]
        public string Result
        {
            get { return _Result; }
            set { _Result = value; }
        }
        private string _ResultDesc;
        [DataFieldAttribute("ResultDesc", "varchar")]
        public string ResultDesc
        {
            get { return _ResultDesc; }
            set { _ResultDesc = value; }
        }
        private string _DeclareUnit;
        [DataFieldAttribute("DeclareUnit", "varchar")]
        public string DeclareUnit
        {
            get { return _DeclareUnit; }
            set { _DeclareUnit = value; }
        }
        private string _DeclareUser;
        [DataFieldAttribute("DeclareUser", "varchar")]
        public string DeclareUser
        {
            get { return _DeclareUser; }
            set { _DeclareUser = value; }
        }
        private string _ReviewDate;
        [DataFieldAttribute("ReviewDate", "DateTime")]

        public string ReviewDate
        {
            get { return _ReviewDate; }
            set { _ReviewDate = value; }
        }
        private string _CreateUser;
        [DataFieldAttribute("CreateUser", "varchar")]
        public string CreateUser
        {
            get { return _CreateUser; }
            set { _CreateUser = value; }
        }
        private DateTime? _CreateTime;
        [DataFieldAttribute("CreateTime", "DateTime")]
        public DateTime? CreateTime
        {
            get { return _CreateTime; }
            set { _CreateTime = value; }
        }
        private string _Validate;
        [DataFieldAttribute("Validate", "varchar")]
        public string Validate
        {
            get { return _Validate; }
            set { _Validate = value; }
        }
    }
}
