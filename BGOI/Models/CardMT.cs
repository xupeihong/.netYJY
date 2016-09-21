using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class CardMT
    {
        private string CardID;
        private string DetectID;
        private string TaskName;
        private string PieceName;
        private string TxtNum;
        private string PieceSpec;
        private string HjType;
        private string PokoType;
        private string DetectTime;
        private string PieceFace;
        private string DecetePart;
        private string DeceteEquip;
        private string EquipNumber;
        private string CiType;
        private string CiConsert;
        private string TestPiece;
        private string BlackModel;
        private string DeceteStand;
        private string DeceteBL;
        private string HegeLevel;
        private string DeceteMethod;
        private string CiTakeType;
        private string CiMethod;
        private string PowerType;
        private string SunFace;
        private string NoBad;
        private string Picture;
        private string Waskstr;
        private string Equipaskstr;
        private string CIaskstr;
        private string CITimeaskstr;
        private string TestTimeaskstr;
        private string Concertaskstr;
        private string Makeaskstr;
        private string Watchaskstr;
        private string Setingaskstr;
        private string Fwatchaskstr;
        private string Reaskstr;
        private string Passaskstr;
        private string Rtypeaskstr;
        private string Rcontaskstr;
        private string DelCI;
        private string BackHand;
        private string Report;
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

        [DataFieldAttribute("TxtNum", "varchar")]
        public string StrTxtNum
        {
            get { return TxtNum; }
            set { TxtNum = value; }
        }

        [DataFieldAttribute("PieceSpec", "varchar")]
        public string StrPieceSpec
        {
            get { return PieceSpec; }
            set { PieceSpec = value; }
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

        [DataFieldAttribute("DetectTime", "varchar")]
        public string StrDetectTime
        {
            get { return DetectTime; }
            set { DetectTime = value; }
        }

        [DataFieldAttribute("PieceFace", "varchar")]
        public string StrPieceFace
        {
            get { return PieceFace; }
            set { PieceFace = value; }
        }

        [DataFieldAttribute("DecetePart", "varchar")]
        public string StrDecetePart
        {
            get { return DecetePart; }
            set { DecetePart = value; }
        }

        [DataFieldAttribute("DeceteEquip", "varchar")]
        public string StrDeceteEquip
        {
            get { return DeceteEquip; }
            set { DeceteEquip = value; }
        }

        [DataFieldAttribute("EquipNumber", "varchar")]
        public string StrEquipNumber
        {
            get { return EquipNumber; }
            set { EquipNumber = value; }
        }

        [DataFieldAttribute("CiType", "varchar")]
        public string StrCiType
        {
            get { return CiType; }
            set { CiType = value; }
        }

        [DataFieldAttribute("CiConsert", "varchar")]
        public string StrCiConsert
        {
            get { return CiConsert; }
            set { CiConsert = value; }
        }

        [DataFieldAttribute("TestPiece", "varchar")]
        public string StrTestPiece
        {
            get { return TestPiece; }
            set { TestPiece = value; }
        }

        [DataFieldAttribute("BlackModel", "varchar")]
        public string StrBlackModel
        {
            get { return BlackModel; }
            set { BlackModel = value; }
        }

        [DataFieldAttribute("DeceteStand", "varchar")]
        public string StrDeceteStand
        {
            get { return DeceteStand; }
            set { DeceteStand = value; }
        }

        [DataFieldAttribute("DeceteBL", "varchar")]
        public string StrDeceteBL
        {
            get { return DeceteBL; }
            set { DeceteBL = value; }
        }

        [DataFieldAttribute("HegeLevel", "varchar")]
        public string StrHegeLevel
        {
            get { return HegeLevel; }
            set { HegeLevel = value; }
        }

        [DataFieldAttribute("DeceteMethod", "varchar")]
        public string StrDeceteMethod
        {
            get { return DeceteMethod; }
            set { DeceteMethod = value; }
        }

        [DataFieldAttribute("CiTakeType", "varchar")]
        public string StrCiTakeType
        {
            get { return CiTakeType; }
            set { CiTakeType = value; }
        }

        [DataFieldAttribute("CiMethod", "varchar")]
        public string StrCiMethod
        {
            get { return CiMethod; }
            set { CiMethod = value; }
        }

        [DataFieldAttribute("PowerType", "varchar")]
        public string StrPowerType
        {
            get { return PowerType; }
            set { PowerType = value; }
        }

        [DataFieldAttribute("SunFace", "varchar")]
        public string StrSunFace
        {
            get { return SunFace; }
            set { SunFace = value; }
        }

        [DataFieldAttribute("NoBad", "varchar")]
        public string StrNoBad
        {
            get { return NoBad; }
            set { NoBad = value; }
        }

        [DataFieldAttribute("Picture", "varchar")]
        public string StrPicture
        {
            get { return Picture; }
            set { Picture = value; }
        }

        [DataFieldAttribute("Waskstr", "nvarchar")]
        public string StrWaskstr
        {
            get { return Waskstr; }
            set { Waskstr = value; }
        }

        [DataFieldAttribute("Equipaskstr", "nvarchar")]
        public string StrEquipaskstr
        {
            get { return Equipaskstr; }
            set { Equipaskstr = value; }
        }

        [DataFieldAttribute("CIaskstr", "nvarchar")]
        public string StrCIaskstr
        {
            get { return CIaskstr; }
            set { CIaskstr = value; }
        }

        [DataFieldAttribute("CITimeaskstr", "nvarchar")]
        public string StrCITimeaskstr
        {
            get { return CITimeaskstr; }
            set { CITimeaskstr = value; }
        }

        [DataFieldAttribute("TestTimeaskstr", "nvarchar")]
        public string StrTestTimeaskstr
        {
            get { return TestTimeaskstr; }
            set { TestTimeaskstr = value; }
        }

        [DataFieldAttribute("Concertaskstr", "nvarchar")]
        public string StrConcertaskstr
        {
            get { return Concertaskstr; }
            set { Concertaskstr = value; }
        }

        [DataFieldAttribute("Makeaskstr", "nvarchar")]
        public string StrMakeaskstr
        {
            get { return Makeaskstr; }
            set { Makeaskstr = value; }
        }

        [DataFieldAttribute("Watchaskstr", "nvarchar")]
        public string StrWatchaskstrr
        {
            get { return Watchaskstr; }
            set { Watchaskstr = value; }
        }

        [DataFieldAttribute("Setingaskstr", "nvarchar")]
        public string StrSetingaskstr
        {
            get { return Setingaskstr; }
            set { Setingaskstr = value; }
        }

        [DataFieldAttribute("Fwatchaskstr", "nvarchar")]
        public string StrFwatchaskstr
        {
            get { return Fwatchaskstr; }
            set { Fwatchaskstr = value; }
        }

        [DataFieldAttribute("Reaskstr", "nvarchar")]
        public string StrReaskstr
        {
            get { return Reaskstr; }
            set { Reaskstr = value; }
        }

        [DataFieldAttribute("Passaskstr", "nvarchar")]
        public string StrPassaskstr
        {
            get { return Passaskstr; }
            set { Passaskstr = value; }
        }

        [DataFieldAttribute("Rtypeaskstr", "nvarchar")]
        public string StrRtypeaskstr
        {
            get { return Rtypeaskstr; }
            set { Rtypeaskstr = value; }
        }

        [DataFieldAttribute("Rcontaskstr", "nvarchar")]
        public string StrRcontaskstr
        {
            get { return Rcontaskstr; }
            set { Rcontaskstr = value; }
        }

        [DataFieldAttribute("DelCI", "nvarchar")]
        public string StrDelCI
        {
            get { return DelCI; }
            set { DelCI = value; }
        }

        [DataFieldAttribute("BackHand", "nvarchar")]
        public string StrBackHand
        {
            get { return BackHand; }
            set { BackHand = value; }
        }

        [DataFieldAttribute("Report", "nvarchar")]
        public string StrReport
        {
            get { return Report; }
            set { Report = value; }
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
