using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class MandateInfo
    {
        //预约号
        [DataFieldAttribute("YYCode", "varchar")]
        public string YYCode { get; set; }
        //委托编号
        [DataFieldAttribute("MCode", "varchar")]
        public string MCode { get; set; }
        //客户名称
        [DataFieldAttribute("ClienName", "nvarchar")]
        public string ClienName { get; set; }
        //电话
        [DataFieldAttribute("ClienTel", "varchar")]
        public string ClienTel { get; set; }
        //客户地址
        [DataFieldAttribute("ClienAddress", "nvarchar")]
        public string ClienAddress { get; set; }
        //邮政编码
        [DataFieldAttribute("PostalCode", "varchar")]
        public string PostalCode { get; set; }
        //工程名称
        [DataFieldAttribute("ProName", "nvarchar")]
        public string ProName { get; set; }
        //来源方式
        [DataFieldAttribute("SourceWay", "nvarchar")]
        public string SourceWay { get; set; }
        /// <summary>
        /// 送样人
        /// </summary>
        [DataFieldAttribute("SamplePeople", "nvarchar")]
        public string SamplePeople { get; set; }
        //送样时间
        [DataFieldAttribute("SampleTime", "datetime")]
        public string SampleTime { get; set; }
        //生产厂家
        [DataFieldAttribute("Manufacturer", "nvarchar")]
        public string Manufacturer { get; set; }
        //随机文件
        [DataFieldAttribute("Document", "nvarchar")]
        public string Document { get; set; }
        //随机文件保密要求
        [DataFieldAttribute("Secrecy", "nvarchar")]
        public string Secrecy { get; set; }
        //检测依据
        [DataFieldAttribute("TestingBasis", "nvarchar")]
        public string TestingBasis { get; set; }
        //检测项目
        [DataFieldAttribute("TestingItems", "nvarchar")]
        public string TestingItems { get; set; }
        //要求完成时间
        [DataFieldAttribute("DemandFinishDate", "datetime")]
        public string DemandFinishDate { get; set; }
        //报告领取方式
        [DataFieldAttribute("PickupMethod", "nvarchar")]
        public string PickupMethod { get; set; }

        //邮寄地址
        [DataFieldAttribute("MailingAddress", "nvarchar")]
        public string MailingAddress { get; set; }
        //备注
        [DataFieldAttribute("Remark", "nvarchar")]
        public string Remark { get; set; }
        //样品处置方式
        [DataFieldAttribute("SampleDisposition", "nvarchar")]
        public string SampleDisposition { get; set; }
        //委托人
        [DataFieldAttribute("MandatePeople", "nvarchar")]
        public string MandatePeople { get; set; }
        //受理时间
        [DataFieldAttribute("AcceptTime", "datetime")]
        public string AcceptTime { get; set; }
        //受理人
        [DataFieldAttribute("AcceptPeople", "nvarchar")]
        public string AcceptPeople { get; set; }
        //费用
        [DataFieldAttribute("Charge", "decimal")]
        public double Charge { get; set; }
        //样品领取状态
        [DataFieldAttribute("SReceiveState", "int")]
        public int SReceiveState { get; set; }
        //状态
        [DataFieldAttribute("State", "int")]
        public int State { get; set; }
        //创建时间
        [DataFieldAttribute("CreateTime", "datetime")]
        public string CreateTime { get; set; }
        //创建人
        [DataFieldAttribute("CreateUser", "nvarchar")]
        public string CreateUser { get; set; }
        [DataFieldAttribute("Validate", "datetime")]
        public string Validate { get; set; }

        [DataFieldAttribute("RepealReason", "nvarchar")]
        public string RepealReason { get; set; }

        [DataFieldAttribute("TestType", "nvarchar")]
        public string TestType { get; set; }

        [DataFieldAttribute("Provinces", "nvarchar")]
        public string Provinces { get; set; }
    }
}
