using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class CardPT
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
        private string DeceteWD;
        private string Pttype;
        private string WashJtype;
        private string FilmType;
        private string TestPiece;
        private string Quick;
        private string DeceteMethod;
        private string PtMethod;
        private string DelMethod;
        private string HotWD;
        private string HotTime;
        private string FilmtMethod;
        private string PtTime;
        private string FilmTime;
        private string WatchMethod;
        private string FaceAsk;
        private string DeceteStand;
        private string DeceteBL;
        private string HegeLevel;
        private string DetectJask;
        private string SureStand;
        private string PersonSafe;
        private string EquipSafe;
        private string Faskstr;
        private string Fdesc;
        private string Waskstr;
        private string Wdesc;
        private string PTaskstr;
        private string PTdesc;
        private string Delaskstr;
        private string Deldesc;
        private string Filaskstr;
        private string Fildesc;
        private string Watchaskstr;
        private string Watchdesc;
        private string Reaskstr;
        private string Redesc;
        private string Backaskstr;
        private string Backdesc;
        private string Waitaskstr;
        private string Waitdesc;
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

        [DataFieldAttribute("DeceteWD", "varchar")]
        public string StrDeceteWD
        {
            get { return DeceteWD; }
            set { DeceteWD = value; }
        }

        [DataFieldAttribute("Pttype", "varchar")]
        public string StrPttype
        {
            get { return Pttype; }
            set { Pttype = value; }
        }

        [DataFieldAttribute("WashJtype", "varchar")]
        public string StrWashJtype
        {
            get { return WashJtype; }
            set { WashJtype = value; }
        }

        [DataFieldAttribute("FilmType", "varchar")]
        public string StrFilmType
        {
            get { return FilmType; }
            set { FilmType = value; }
        }

        [DataFieldAttribute("TestPiece", "varchar")]
        public string StrTestPiece
        {
            get { return TestPiece; }
            set { TestPiece = value; }
        }

        [DataFieldAttribute("Quick", "varchar")]
        public string StrQuick
        {
            get { return Quick; }
            set { Quick = value; }
        }

        [DataFieldAttribute("DeceteMethod", "varchar")]
        public string StrDeceteMethod
        {
            get { return DeceteMethod; }
            set { DeceteMethod = value; }
        }

        [DataFieldAttribute("PtMethod", "varchar")]
        public string StrPtMethod
        {
            get { return PtMethod; }
            set { PtMethod = value; }
        }

        [DataFieldAttribute("DelMethod", "varchar")]
        public string StrDelMethod
        {
            get { return DelMethod; }
            set { DelMethod = value; }
        }

        [DataFieldAttribute("HotWD", "varchar")]
        public string StrHotWD
        {
            get { return HotWD; }
            set { HotWD = value; }
        }

        [DataFieldAttribute("HotTime", "varchar")]
        public string StrHotTime
        {
            get { return HotTime; }
            set { HotTime = value; }
        }

        [DataFieldAttribute("FilmtMethod", "varchar")]
        public string StrFilmtMethod
        {
            get { return FilmtMethod; }
            set { FilmtMethod = value; }
        }

        [DataFieldAttribute("PtTime", "varchar")]
        public string StrPtTime
        {
            get { return PtTime; }
            set { PtTime = value; }
        }

        [DataFieldAttribute("FilmTime", "varchar")]
        public string StrFilmTime
        {
            get { return FilmTime; }
            set { FilmTime = value; }
        }

        [DataFieldAttribute("WatchMethod", "varchar")]
        public string StrWatchMethod
        {
            get { return WatchMethod; }
            set { WatchMethod = value; }
        }

        [DataFieldAttribute("FaceAsk", "varchar")]
        public string StrFaceAsk
        {
            get { return FaceAsk; }
            set { FaceAsk = value; }
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

        [DataFieldAttribute("DetectJask", "varchar")]
        public string StrDetectJask
        {
            get { return DetectJask; }
            set { DetectJask = value; }
        }

        [DataFieldAttribute("SureStand", "varchar")]
        public string StrSureStand
        {
            get { return SureStand; }
            set { SureStand = value; }
        }

        [DataFieldAttribute("PersonSafe", "nvarchar")]
        public string StrPersonSafe
        {
            get { return PersonSafe; }
            set { PersonSafe = value; }
        }

        [DataFieldAttribute("EquipSafe", "nvarchar")]
        public string StrEquipSafe
        {
            get { return EquipSafe; }
            set { EquipSafe = value; }
        }

        [DataFieldAttribute("Faskstr", "nvarchar")]
        public string StrFaskstr
        {
            get { return Faskstr; }
            set { Faskstr = value; }
        }

        [DataFieldAttribute("Fdesc", "nvarchar")]
        public string StrFdesc
        {
            get { return Fdesc; }
            set { Fdesc = value; }
        }

        [DataFieldAttribute("Waskstr", "nvarchar")]
        public string StrWaskstr
        {
            get { return Waskstr; }
            set { Waskstr = value; }
        }

        [DataFieldAttribute("Wdesc", "nvarchar")]
        public string StrWdesc
        {
            get { return Wdesc; }
            set { Wdesc = value; }
        }

        [DataFieldAttribute("PTaskstr", "nvarchar")]
        public string StrPTaskstr
        {
            get { return PTaskstr; }
            set { PTaskstr = value; }
        }

        [DataFieldAttribute("PTdesc", "nvarchar")]
        public string StrPTdesc
        {
            get { return PTdesc; }
            set { PTdesc = value; }
        }

        [DataFieldAttribute("Delaskstr", "nvarchar")]
        public string StrDelaskstr
        {
            get { return Delaskstr; }
            set { Delaskstr = value; }
        }

        [DataFieldAttribute("Deldesc", "nvarchar")]
        public string StrDeldesc
        {
            get { return Deldesc; }
            set { Deldesc = value; }
        }

        [DataFieldAttribute("Filaskstr", "nvarchar")]
        public string StrFilaskstr
        {
            get { return Filaskstr; }
            set { Filaskstr = value; }
        }

        [DataFieldAttribute("Fildesc", "nvarchar")]
        public string StrFildesc
        {
            get { return Fildesc; }
            set { Fildesc = value; }
        }

        [DataFieldAttribute("Watchaskstr", "nvarchar")]
        public string StrWatchaskstr
        {
            get { return Watchaskstr; }
            set { Watchaskstr = value; }
        }

        [DataFieldAttribute("Watchdesc", "nvarchar")]
        public string StrWatchdesc
        {
            get { return Watchdesc; }
            set { Watchdesc = value; }
        }

        [DataFieldAttribute("Reaskstr", "nvarchar")]
        public string StrReaskstr
        {
            get { return Reaskstr; }
            set { Reaskstr = value; }
        }

        [DataFieldAttribute("Redesc", "nvarchar")]
        public string StrRedesc
        {
            get { return Redesc; }
            set { Redesc = value; }
        }

        [DataFieldAttribute("Backaskstr", "nvarchar")]
        public string StrBackaskstr
        {
            get { return Backaskstr; }
            set { Backaskstr = value; }
        }

        [DataFieldAttribute("Backdesc", "nvarchar")]
        public string StrBackdesc
        {
            get { return Backdesc; }
            set { Backdesc = value; }
        }

        [DataFieldAttribute("Waitaskstr", "nvarchar")]
        public string StrWaitaskstr
        {
            get { return Waitaskstr; }
            set { Waitaskstr = value; }
        }

        [DataFieldAttribute("Waitdesc", "nvarchar")]
        public string StrWaitdesc
        {
            get { return Waitdesc; }
            set { Waitdesc = value; }
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
