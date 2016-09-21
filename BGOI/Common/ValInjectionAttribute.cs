using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.WebPages;

namespace TECOCITY_BGOI
{
    public class ValInjectionAttribute : ValidationAttribute, IClientValidatable
    {
        public ValInjectionAttribute()
        {
            this.RexValue = GValidator.GetInjectionRegexString();
        }

        public override bool IsValid(object value)
        {
            if (null == value)
            {
                return true;
            }
            return !GValidator.HasInjectionData(value.ToString());// this.Values.Any(item => value.ToString() == item);
        }

        public override string FormatErrorMessage(string name)
        {
            //return string.Format("{0}包含网络安全风险语句", name);
            return base.FormatErrorMessage("信息验证不通过");
        }

        public string RexValue { get;private set; }

        public IEnumerable<ModelClientValidationRule> 
               GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var validationRule = new ModelClientValidationRule
            {
            ErrorMessage=FormatErrorMessage(metadata.DisplayName),
            ValidationType = "injection",
            };
            validationRule.ValidationParameters.Add("rexvalue", RexValue);

            yield return validationRule;
        }
    }
}
