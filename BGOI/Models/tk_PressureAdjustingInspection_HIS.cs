using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
   public class tk_PressureAdjustingInspection_HIS
    {
         public string strTYID = "";
        [DataFieldAttribute("TYID", "varchar")]
        //调压记录编号
        public string TYID
        {
            get { return strTYID; }
            set { strTYID = value; }
        }
        public string strUserAdd = "";
        [DataFieldAttribute("UserAdd", "varchar")]
        //用户地址
        public string UserAdd
        {
            get { return strUserAdd; }
            set { strUserAdd = value; }
        }
        public string strUsers = "";
        [DataFieldAttribute("Users", "varchar")]
        //联系人
        public string Users
        {
            get { return strUsers; }
            set { strUsers = value; }
        }
        public string strTel = "";
        [DataFieldAttribute("Tel", "varchar")]
        //电话
        public string Tel
        {
            get { return strTel; }
            set { strTel = value; }
        }
        public string strKeyStorageUnitJia = "";
        [DataFieldAttribute("KeyStorageUnitJia", "varchar")]
        //钥匙保存单位    甲方 
        public string KeyStorageUnitJia
        {
            get { return strKeyStorageUnitJia; }
            set { strKeyStorageUnitJia = value; }
        }
        public string strUses = "";
        [DataFieldAttribute("Uses", "varchar")]
        //用途
        public string Uses
        {
            get { return strUses; }
            set { strUses = value; }
        }
        public string strBoiler = "";
        [DataFieldAttribute("Boiler", "varchar")]
        //锅炉
        public string Boiler
        {
            get { return strBoiler; }
            set { strBoiler = value; }
        }
        public string strKungFu = "";
        [DataFieldAttribute("KungFu", "varchar")]
        //公福
        public string KungFu
        {
            get { return strKungFu; }
            set { strKungFu = value; }
        }
        public string strCivil = "";
        [DataFieldAttribute("Civil", "varchar")]
        //民用
        public string Civil
        {
            get { return strCivil; }
            set { strCivil = value; }
        }
        public string strOther = "";
        [DataFieldAttribute("Other", "varchar")]
        //其他
        public string Other
        {
            get { return strOther; }
            set { strOther = value; }
        }
        public string strUsePressureShang = "";
        [DataFieldAttribute("UsePressureShang", "varchar")]
        //使用压力    上台       Kpa
        public string UsePressureShang
        {
            get { return strUsePressureShang; }
            set { strUsePressureShang = value; }
        }
        public DateTime strOperationTime;
        [DataFieldAttribute("OperationTime", "datetime")]
        //运维时间
        public DateTime OperationTime
        {
            get { return strOperationTime; }
            set { strOperationTime = value; }
        }
        public string strInspectionPersonnel = "";
        [DataFieldAttribute("InspectionPersonnel", "varchar")]
        //巡检人员
        public string InspectionPersonnel
        {
            get { return strInspectionPersonnel; }
            set { strInspectionPersonnel = value; }
        }
        public string strUserSignature = "";
        [DataFieldAttribute("UserSignature", "varchar")]
        //用户签字
        public string UserSignature
        {
            get { return strUserSignature; }
            set { strUserSignature = value; }
        }
        public string strRemarks = "";
        [DataFieldAttribute("Remarks", "varchar")]
        //备注
        public string Remarks
        {
            get { return strRemarks; }
            set { strRemarks = value; }
        }
        public string strKeyStorageUnitYi = "";
        [DataFieldAttribute("KeyStorageUnitYi", "varchar")]
        //钥匙保存单位     乙方
        public string KeyStorageUnitYi
        {
            get { return strKeyStorageUnitYi; }
            set { strKeyStorageUnitYi = value; }
        }
        public string strUsePressureXia = "";
        [DataFieldAttribute("UsePressureXia", "varchar")]
        //使用压力      下台     Kpa
        public string UsePressureXia
        {
            get { return strUsePressureXia; }
            set { strUsePressureXia = value; }
        }
        public DateTime strCreateTime;
        [DataFieldAttribute("CreateTime", "datetime")]
        //创建时间
        public DateTime CreateTime
        {
            get { return strCreateTime; }
            set { strCreateTime = value; }
        }
        public string strCreateUser = "";
        [DataFieldAttribute("CreateUser", "varchar")]
        //登记人
        public string CreateUser
        {
            get { return strCreateUser; }
            set { strCreateUser = value; }
        }
        public string strValidate = "";
        [DataFieldAttribute("Validate", "varchar")]
        public string Validate
        {
            get { return strValidate; }
            set { strValidate = value; }
        }
        public string strUserName = "";
        [DataFieldAttribute("UserName", "varchar")]
        public string UserName
        {
            get { return strUserName; }
            set { strUserName = value; }
        }
        private string strNCreateUser;
        [DataFieldAttribute("NCreateUser", "varchar")]
        public string NCreateUser
        {
            get { return strNCreateUser; }
            set { strNCreateUser = value; }
        }
        private DateTime? strNCreateTime;
        [DataFieldAttribute("NCreateTime", "DateTime")]
        public DateTime? NCreateTime
        {
            get { return strNCreateTime; }
            set { strNCreateTime = value; }
        }
        //public DateTime strAfternoonTime;
        //[DataFieldAttribute("AfternoonTime", "datetime")]
        ////运维时间下午
        //public DateTime AfternoonTime
        //{
        //    get { return strAfternoonTime; }
        //    set { strAfternoonTime = value; }
        //}
    }
}