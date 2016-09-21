using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TECOCITY_BGOI
{
   public  class ExchangeGoods_DetailInfo
    {
        private string m_TID;
       [DataField("TID", "varchar")]
        public string TID
        {
            get { return m_TID; }
            set { m_TID = value; }
        }
        private string m_TDID;
       [DataField ("TDID","varchar")]
        public string TDID
        {
            get { return m_TDID; }
            set { m_TDID = value; }
        }
        private string m_DID;
       [DataField ("DID","varchar")]
        public string DID
        {
            get { return m_DID; }
            set { m_DID = value; }
        }
       private string m_ProductID;
          [DataField("ProductID", "varchar")]
       public string ProductID
       {
           get { return m_ProductID; }
           set { m_ProductID = value; }
       }
          private string m_SpecsModels;
          [DataField("SpecsModels", "varchar")]
          public string SpecsModels
          {
              get { return m_SpecsModels; }
              set { m_SpecsModels = value; }
          }
        private string m_PackWreck;
       [DataField("PackWreck", "varchar")]
        public string PackWreck
        {
            get { return m_PackWreck; }
            set { m_PackWreck = value; }
        }
        private string m_FeatureWreck;
       [DataField("FeatureWreck", "varchar")]
        public string FeatureWreck
        {
            get { return m_FeatureWreck; }
            set { m_FeatureWreck = value; }
        }
        private string m_Componments;
       [DataField("Componments", "varchar")]
        public string Componments
        {
            get { return m_Componments; }
            set { m_Componments = value; }
        }
        private string m_Quality;
       [DataField("Quality", "varchar")]
        public string Quality
        {
            get { return m_Quality; }
            set { m_Quality = value; }
        }
        private string m_Remark;
       [DataField("Remark", "varchar")]
        public string Remark
        {
            get { return m_Remark; }
            set { m_Remark = value; }
        }
        private string m_CreateTime;
       [DataField("CreateTime", "date")]
        public string CreateTime
        {
            get { return m_CreateTime; }
            set { m_CreateTime = value; }
        }
        private string m_CreateUser;
       [DataField("CreateUser", "varchar")]
        public string CreateUser
        {
            get { return m_CreateUser; }
            set { m_CreateUser = value; }
        }
        private string m_Validate;
       [DataField ("Validate","varchar")]
        public string Validate
        {
            get { return m_Validate; }
            set { m_Validate = value; }
        }
    }
}
