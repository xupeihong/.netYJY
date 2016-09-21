using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class CardRT
    {
        private string CardID;
        private string DetectID;
        private string PipeStand;
        private string Rttype;
        private string TZType;
        private string TZPly;
        private string StandSize;
        private string TaskName;
        private string PieceName;
        private string ProductNum;
        private string PressType;
        private string HotState;
        private string PieceState;
        private string MainTxture;
        private string TxtureNum;
        private string PokoType;
        private string HjType;
        private string YiqiModel;
        private string FocusSize;
        private string XZJModel;
        private string Pb;
        private string BPB;
        private string FilmType;
        private string FilmSpec;
        private string FilmPF;
        private string WashType;
        private string WashEquipModel;
        private string ExecuteStand;
        private string TechlLevelRT;
        private string HegeLevel;
        private string DeceteBL;
        private string Hflength;
        private string DetectTime;
        private string DecetePart;
        private string BlackRange;
        private string SiNum;
        private string Kv;
        private string Ci;
        private string mA;
        private string CiTime;
        private string Cimin;
        private string FocusLength;
        private string OneTimelength;
        private string Delength;
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

        [DataFieldAttribute("PipeStand", "varchar")]
        public string StrPipeStand
        {
            get { return PipeStand; }
            set { PipeStand = value; }
        }

        [DataFieldAttribute("Rttype", "varchar")]
        public string StrRttype
        {
            get { return Rttype; }
            set { Rttype = value; }
        }

        [DataFieldAttribute("TZType", "varchar")]
        public string StrTZType
        {
            get { return TZType; }
            set { TZType = value; }
        }

        [DataFieldAttribute("TZPly", "varchar")]
        public string StrTZPly
        {
            get { return TZPly; }
            set { TZPly = value; }
        }

        [DataFieldAttribute("StandSize", "varchar")]
        public string StrStandSize
        {
            get { return StandSize; }
            set { StandSize = value; }
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

        [DataFieldAttribute("PressType", "varchar")]
        public string StrPressType
        {
            get { return PressType; }
            set { PressType = value; }
        }

        [DataFieldAttribute("HotState", "varchar")]
        public string StrHotState
        {
            get { return HotState; }
            set { HotState = value; }
        }

        [DataFieldAttribute("PieceState", "varchar")]
        public string StrPieceState
        {
            get { return PieceState; }
            set { PieceState = value; }
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

        [DataFieldAttribute("PokoType", "varchar")]
        public string StrPokoType
        {
            get { return PokoType; }
            set { PokoType = value; }
        }

        [DataFieldAttribute("HjType", "varchar")]
        public string StrHjType
        {
            get { return HjType; }
            set { HjType = value; }
        }

        [DataFieldAttribute("YiqiModel", "varchar")]
        public string StrYiqiModel
        {
            get { return YiqiModel; }
            set { YiqiModel = value; }
        }

        [DataFieldAttribute("FocusSize", "varchar")]
        public string StrFocusSize
        {
            get { return FocusSize; }
            set { FocusSize = value; }
        }

        [DataFieldAttribute("XZJModel", "varchar")]
        public string StrXZJModel
        {
            get { return XZJModel; }
            set { XZJModel = value; }
        }

        [DataFieldAttribute("Pb", "varchar")]
        public string StrPb
        {
            get { return Pb; }
            set { Pb = value; }
        }

        [DataFieldAttribute("BPB", "varchar")]
        public string StrBPB
        {
            get { return BPB; }
            set { BPB = value; }
        }

        [DataFieldAttribute("FilmType", "varchar")]
        public string StrFilmType
        {
            get { return FilmType; }
            set { FilmType = value; }
        }

        [DataFieldAttribute("FilmSpec", "varchar")]
        public string StrFilmSpec
        {
            get { return FilmSpec; }
            set { FilmSpec = value; }
        }

        [DataFieldAttribute("FilmPF", "varchar")]
        public string StrFilmPF
        {
            get { return FilmPF; }
            set { FilmPF = value; }
        }

        [DataFieldAttribute("WashType", "varchar")]
        public string StrWashType
        {
            get { return WashType; }
            set { WashType = value; }
        }

        [DataFieldAttribute("WashEquipModel", "varchar")]
        public string StrWashEquipModel
        {
            get { return WashEquipModel; }
            set { WashEquipModel = value; }
        }

        [DataFieldAttribute("ExecuteStand", "varchar")]
        public string StrExecuteStand
        {
            get { return ExecuteStand; }
            set { ExecuteStand = value; }
        }

        [DataFieldAttribute("TechlLevelRT", "varchar")]
        public string StrTechlLevelRT
        {
            get { return TechlLevelRT; }
            set { TechlLevelRT = value; }
        }

        [DataFieldAttribute("HegeLevel", "varchar")]
        public string StrHegeLevel
        {
            get { return HegeLevel; }
            set { HegeLevel = value; }
        }

        [DataFieldAttribute("DeceteBL", "varchar")]
        public string StrDeceteBL
        {
            get { return DeceteBL; }
            set { DeceteBL = value; }
        }

        [DataFieldAttribute("Hflength", "varchar")]
        public string StrHflength
        {
            get { return Hflength; }
            set { Hflength = value; }
        }

        [DataFieldAttribute("DetectTime", "varchar")]
        public string StrDetectTime
        {
            get { return DetectTime; }
            set { DetectTime = value; }
        }

        [DataFieldAttribute("DecetePart", "varchar")]
        public string StrDecetePart
        {
            get { return DecetePart; }
            set { DecetePart = value; }
        }

        [DataFieldAttribute("BlackRange", "varchar")]
        public string StrBlackRange
        {
            get { return BlackRange; }
            set { BlackRange = value; }
        }

        [DataFieldAttribute("SiNum", "varchar")]
        public string StrSiNum
        {
            get { return SiNum; }
            set { SiNum = value; }
        }

        [DataFieldAttribute("Kv", "varchar")]
        public string StrKv
        {
            get { return Kv; }
            set { Kv = value; }
        }

        [DataFieldAttribute("Ci", "varchar")]
        public string StrCi
        {
            get { return Ci; }
            set { Ci = value; }
        }

        [DataFieldAttribute("mA", "varchar")]
        public string StrmA
        {
            get { return mA; }
            set { mA = value; }
        }

        [DataFieldAttribute("CiTime", "varchar")]
        public string StrCiTime
        {
            get { return CiTime; }
            set { CiTime = value; }
        }

        [DataFieldAttribute("Cimin", "varchar")]
        public string StrCimin
        {
            get { return Cimin; }
            set { Cimin = value; }
        }

        [DataFieldAttribute("FocusLength", "varchar")]
        public string StrFocusLength
        {
            get { return FocusLength; }
            set { FocusLength = value; }
        }

        [DataFieldAttribute("OneTimelength", "varchar")]
        public string StrOneTimelength
        {
            get { return OneTimelength; }
            set { OneTimelength = value; }
        }

        [DataFieldAttribute("Delength", "varchar")]
        public string StrDelength
        {
            get { return Delength; }
            set { Delength = value; }
        }

        [DataFieldAttribute("TechlAsk", "varchar")]
        public string StTechlAsk
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
