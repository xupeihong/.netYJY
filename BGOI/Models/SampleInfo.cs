using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECOCITY_BGOI
{
    public class SampleInfo
    {
        [DataFieldAttribute("YYCode", "varchar")]
        public string YYCode { get; set; }
        [DataFieldAttribute("SampleName", "nvarchar")]
        public string SampleName { get; set; }
        [DataFieldAttribute("Specification", "varchar")]
        public string Specification { get; set; }
        [DataFieldAttribute("Number", "int")]
        public int Number { get; set; }

        [DataFieldAttribute("Brand", "varchar")]
        public string Brand { get; set; }
        [DataFieldAttribute("SampleCode", "varchar")]
        public string SampleCode { get; set; }

        [DataFieldAttribute("TestingBasis", "nvarchar")]
        public string TestingBasis { get; set; }
        [DataFieldAttribute("TestingItem", "nvarchar")]
        public string TestingItem { get; set; }

        [DataFieldAttribute("SampleState", "nvarchar")]
        public string SampleState { get; set; }

        [DataFieldAttribute("serialNumber", "int")]
        public int serialNumber { get; set; }
    }
}
