using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class tk_StockRemain
    {
        public string strProductID = "";
        [DataFieldAttribute("ProductID", "varchar")]
        //货品唯一编号
        public string ProductID
        {
            get { return strProductID; }
            set { strProductID = value; }
        }
        public int strFinishCount;
        [DataFieldAttribute("FinishCount", "int")]
        //库存数量
        public int FinishCount
        {
            get { return strFinishCount; }
            set { strFinishCount = value; }
        }
        public int strOnlineCount;
        [DataFieldAttribute("OnlineCount", "int")]
        //在线生产数量
        public int OnlineCount
        {
            get { return strOnlineCount; }
            set { strOnlineCount = value; }
        }
       
        public string strHouseID = "";
        [DataFieldAttribute("HouseID", "varchar")]
        //所属仓库
        public string HouseID
        {
            get { return strHouseID; }
            set { strHouseID = value; }
        }
        public string strUsableStock = "";
        [DataFieldAttribute("UsableStock", "varchar")]
        //可用库存
        public string UsableStock
        {
            get { return strUsableStock; }
            set { strUsableStock = value; }
        }
        public string strCosting = "";
        [DataFieldAttribute("Costing", "varchar")]
        //成本
        public string Costing
        {
            get { return strCosting; }
            set { strCosting = value; }
        }
        public string strLocation = "";
        [DataFieldAttribute("Location", "varchar")]
        //存放位置
        public string Location
        {
            get { return strLocation; }
            set { strLocation = value; }
        }
        
        public int strProtoCount;
        [DataFieldAttribute("ProtoCount", "int")]
        //样机数量
        public int ProtoCount
        {
            get { return strProtoCount; }
            set { strProtoCount = value; }
        }
        public int strDefectCount;
        [DataFieldAttribute("DefectCount", "int")]
        //残次机数量
        public int DefectCount
        {
            get { return strDefectCount; }
            set { strDefectCount = value; }
        }
        public int strCompleteCount;
        [DataFieldAttribute("CompleteCount", "int")]
        //成品
        public int CompleteCount
        {
            get { return strCompleteCount; }
            set { strCompleteCount = value; }
        }
        public int strHalfCount;
        [DataFieldAttribute("HalfCount", "int")]
        //半成品数量
        public int HalfCount
        {
            get { return strHalfCount; }
            set { strHalfCount = value; }
        }
    }
}
