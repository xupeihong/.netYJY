using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class CardUT
    {
        private string CardID;
        private string DetectID;
        private string ProductSpec;
        private string MumTxt;
        private string TaskName;
        private string PieceName;
        private string ProductNum;
        private string EquipModel;
        private string MainTxture;
        private string TxtureNum;
        private string HjType;
        private string PokoType;
        private string PieceState;
        private string PieceFace;
        private string YiqiModel;
        private string YiqiNumber;
        private string TestPart;
        private string FashePart;
        private string TantType;
        private string TantModel;
        private string Jpsize;
        private string Kval;
        private string TantHead;
        private string OuhType;
        private string DeceteStand;
        private string TechlLevel;
        private string HegeLevel;
        private string DecetePart;
        private string DeceteLength;
        private string DeceteBL;
        private string DeceteType;
        private string DeceteFace;
        private string SaoPart;
        private string SaoType;
        private string SaoSpeed;
        private string SaoConver;
        private string OuType;
        private string TantRange;
        private string FaceBC;
        private string Scan;
        private string ScanLM;
        private string Lmass;
        private string EL;
        private string SL;
        private string RL;
        private string TechlAsk;
        private string CardDesc;
        private string CreateUser;
        private DateTime? CreateTime;
        private string Validate;

        [DataFieldAttribute("CardID", "varchar")]
        public string StrCardID
        {
            get { return CardID; }
            set { CardID = value; }
        }

        [DataFieldAttribute("DetectID", "varchar")]
        public string StrDetectID
        {
            get { return DetectID; }
            set { DetectID = value; }
        }

        [DataFieldAttribute("ProductSpec", "varchar")]
        public string StrProductSpec
        {
            get { return ProductSpec; }
            set { ProductSpec = value; }
        }

        [DataFieldAttribute("MumTxt", "varchar")]
        public string StrMumTxt
        {
            get { return MumTxt; }
            set { MumTxt = value; }
        }

        [DataFieldAttribute("TaskName", "nvarchar")]
        public string StrTaskName
        {
            get { return TaskName; }
            set { TaskName = value; }
        }

        [DataFieldAttribute("PieceName", "nvarchar")]
        public string StrPieceName
        {
            get { return PieceName; }
            set { PieceName = value; }
        }

        [DataFieldAttribute("ProductNum", "varchar")]
        public string StrProductNum
        {
            get { return ProductNum; }
            set { ProductNum = value; }
        }

        [DataFieldAttribute("EquipModel", "varchar")]
        public string StrEquipModel
        {
            get { return EquipModel; }
            set { EquipModel = value; }
        }

        [DataFieldAttribute("MainTxture", "varchar")]
        public string StrMainTxture
        {
            get { return MainTxture; }
            set { MainTxture = value; }
        }

        [DataFieldAttribute("TxtureNum", "varchar")]
        public string StrTxtureNum
        {
            get { return TxtureNum; }
            set { TxtureNum = value; }
        }

        [DataFieldAttribute("HjType", "varchar")]
        public string StrHjType
        {
            get { return HjType; }
            set { HjType = value; }
        }

        [DataFieldAttribute("PokoType", "varchar")]
        public string StrPokoType
        {
            get { return PokoType; }
            set { PokoType = value; }
        }

        [DataFieldAttribute("PieceState", "varchar")]
        public string StrPieceState
        {
            get { return PieceState; }
            set { PieceState = value; }
        }

        [DataFieldAttribute("PieceFace", "varchar")]
        public string StrPieceFace
        {
            get { return PieceFace; }
            set { PieceFace = value; }
        }

        [DataFieldAttribute("YiqiModel", "varchar")]
        public string StrYiqiModel
        {
            get { return YiqiModel; }
            set { YiqiModel = value; }
        }

        [DataFieldAttribute("YiqiNumber", "varchar")]
        public string StrYiqiNumber
        {
            get { return YiqiNumber; }
            set { YiqiNumber = value; }
        }

        [DataFieldAttribute("TestPart", "varchar")]
        public string StrTestPart
        {
            get { return TestPart; }
            set { TestPart = value; }
        }

        [DataFieldAttribute("FashePart", "varchar")]
        public string StrFashePart
        {
            get { return FashePart; }
            set { FashePart = value; }
        }

        [DataFieldAttribute("TantType", "varchar")]
        public string StrTantType
        {
            get { return TantType; }
            set { TantType = value; }
        }

        [DataFieldAttribute("TantModel", "varchar")]
        public string StrTantModel
        {
            get { return TantModel; }
            set { TantModel = value; }
        }

        [DataFieldAttribute("Jpsize", "varchar")]
        public string StrJpsize
        {
            get { return Jpsize; }
            set { Jpsize = value; }
        }

        [DataFieldAttribute("Kval", "varchar")]
        public string StrKval
        {
            get { return Kval; }
            set { Kval = value; }
        }

        [DataFieldAttribute("TantHead", "varchar")]
        public string StrTantHead
        {
            get { return TantHead; }
            set { TantHead = value; }
        }

        [DataFieldAttribute("OuhType", "varchar")]
        public string StrOuhType
        {
            get { return OuhType; }
            set { OuhType = value; }
        }

        [DataFieldAttribute("DeceteStand", "varchar")]
        public string StrDeceteStand
        {
            get { return DeceteStand; }
            set { DeceteStand = value; }
        }

        [DataFieldAttribute("TechlLevel", "varchar")]
        public string StrTechlLevel
        {
            get { return TechlLevel; }
            set { TechlLevel = value; }
        }

        [DataFieldAttribute("HegeLevel", "varchar")]
        public string StrHegeLevel
        {
            get { return HegeLevel; }
            set { HegeLevel = value; }
        }

        [DataFieldAttribute("DecetePart", "varchar")]
        public string StrDecetePart
        {
            get { return DecetePart; }
            set { DecetePart = value; }
        }

        [DataFieldAttribute("DeceteLength", "varchar")]
        public string StrDeceteLength
        {
            get { return DeceteLength; }
            set { DeceteLength = value; }
        }

        [DataFieldAttribute("DeceteBL", "varchar")]
        public string StrDeceteBL
        {
            get { return DeceteBL; }
            set { DeceteBL = value; }
        }

        [DataFieldAttribute("DeceteType", "varchar")]
        public string StrDeceteType
        {
            get { return DeceteType; }
            set { DeceteType = value; }
        }

        [DataFieldAttribute("DeceteFace", "varchar")]
        public string StrDeceteFace
        {
            get { return DeceteFace; }
            set { DeceteFace = value; }
        }

        [DataFieldAttribute("SaoPart", "varchar")]
        public string StrSaoPart
        {
            get { return SaoPart; }
            set { SaoPart = value; }
        }

        [DataFieldAttribute("SaoType", "varchar")]
        public string StrSaoType
        {
            get { return SaoType; }
            set { SaoType = value; }
        }

        [DataFieldAttribute("SaoSpeed", "varchar")]
        public string StrSaoSpeed
        {
            get { return SaoSpeed; }
            set { SaoSpeed = value; }
        }

        [DataFieldAttribute("SaoConver", "varchar")]
        public string StrSaoConver
        {
            get { return SaoConver; }
            set { SaoConver = value; }
        }

        [DataFieldAttribute("OuType", "varchar")]
        public string StrOuType
        {
            get { return OuType; }
            set { OuType = value; }
        }

        [DataFieldAttribute("TantRange", "varchar")]
        public string StrTantRange
        {
            get { return TantRange; }
            set { TantRange = value; }
        }

        [DataFieldAttribute("FaceBC", "varchar")]
        public string StrFaceBC
        {
            get { return FaceBC; }
            set { FaceBC = value; }
        }

        [DataFieldAttribute("Scan", "varchar")]
        public string StrScan
        {
            get { return Scan; }
            set { Scan = value; }
        }

        [DataFieldAttribute("ScanLM", "varchar")]
        public string StrScanLM
        {
            get { return ScanLM; }
            set { ScanLM = value; }
        }

        [DataFieldAttribute("Lmass", "varchar")]
        public string StrLmass
        {
            get { return Lmass; }
            set { Lmass = value; }
        }

        [DataFieldAttribute("EL", "varchar")]
        public string StrEL
        {
            get { return EL; }
            set { EL = value; }
        }

        [DataFieldAttribute("SL", "varchar")]
        public string StrSL
        {
            get { return SL; }
            set { SL = value; }
        }

        [DataFieldAttribute("RL", "varchar")]
        public string StrRL
        {
            get { return RL; }
            set { RL = value; }
        }

        [DataFieldAttribute("TechlAsk", "varchar")]
        public string StrTechlAsk
        {
            get { return TechlAsk; }
            set { TechlAsk = value; }
        }

        [DataFieldAttribute("CardDesc", "varchar")]
        public string StCardDesc
        {
            get { return CardDesc; }
            set { CardDesc = value; }
        }

        [DataFieldAttribute("CreateUser", "varchar")]
        public string StCreateUser
        {
            get { return CreateUser; }
            set { CreateUser = value; }
        }

        [DataFieldAttribute("CreateTime", "DateTime")]
        public DateTime? StrCreateTime
        {
            get { return CreateTime; }
            set { CreateTime = value; }
        }

        [DataFieldAttribute("Validate", "varchar")]
        public string StrValidate
        {
            get { return Validate; }
            set { Validate = value; }
        }
    }
}
