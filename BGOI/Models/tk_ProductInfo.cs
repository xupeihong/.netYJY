using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_ProductInfo
    {
        public string strPID = "";
        [DataFieldAttribute("PID", "varchar")]
        //货品唯一编号
        public string PID
        {
            get { return strPID; }
            set { strPID = value; }
        }
        public string strProTypeID = "";
        [DataFieldAttribute("ProTypeID", "varchar")]
        //货品库类型(货品库类型表)
        public string ProTypeID
        {
            get { return strProTypeID; }
            set { strProTypeID = value; }
        }
        public string strProName = "";
        [DataFieldAttribute("ProName", "varchar")]
        //货品名称
        public string ProName
        {
            get { return strProName; }
            set { strProName = value; }
        }
        public string strMaterialNum = "";
        [DataFieldAttribute("MaterialNum", "varchar")]
        //物料号
        public string MaterialNum
        {
            get { return strMaterialNum; }
            set { strMaterialNum = value; }
        }
        public string strSpec = "";
        [DataFieldAttribute("Spec", "varchar")]
        //规格型号
        public string Spec
        {
            get { return strSpec; }
            set { strSpec = value; }
        }
        public decimal strUnitPrice ;
        [DataFieldAttribute("UnitPrice", "decimal")]
        //单价（含税），参考价格
        public decimal UnitPrice
        {
            get { return strUnitPrice; }
            set { strUnitPrice = value; }
        }

        public string strUnits = "";
        [DataFieldAttribute("Units", "varchar")]
        //单位，个/台/把
        public string Units
        {
            get { return strUnits; }
            set { strUnits = value; }
        }
        public decimal strPrice2;
        [DataFieldAttribute("Price2", "decimal")]
        //不含税价格
        public decimal Price2
        {
            get { return strPrice2; }
            set { strPrice2 = value; }
        }
        public string strManufacturer = "";
        [DataFieldAttribute("Manufacturer", "varchar")]
        //厂家，生产厂商，供应商
        public string Manufacturer
        {
            get { return strManufacturer; }
            set { strManufacturer = value; }
        }

        public string strRemark = "";
        [DataFieldAttribute("Remark", "varchar")]
        //备注
        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }
        public string strDetail = "";
        [DataFieldAttribute("Detail", "varchar")]
        //详细说明
        public string Detail
        {
            get { return strDetail; }
            set { strDetail = value; }
        }
        public string strPtype = "";
        [DataFieldAttribute("Ptype", "varchar")]
        //物品类型，如劳保，办公，零配件等
        public string Ptype
        {
            get { return strPtype; }
            set { strPtype = value; }
        }

        private string _ffilename;
        [DataFieldAttribute("FFilename", "nvarchar")]
        public string FFilename
        {
            get { return _ffilename; }
            set { _ffilename = value; }
        }
        private string _filetype;
        [DataFieldAttribute("FileType", "varchar")]
        public string FileType
        {
            get { return _filetype; }
            set { _filetype = value; }
        }
        private string _fileinfo;
        [DataFieldAttribute("Fileinfo", "varbinary")]
        public string Fileinfo
        {
            get { return _fileinfo; }
            set { _fileinfo = value; }
        }
    }
}
