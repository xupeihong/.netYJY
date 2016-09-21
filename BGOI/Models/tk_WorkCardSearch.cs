using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TECOCITY_BGOI
{
     public class tk_WorkCardSearch
     {
         private string m_strRepairID;
         private string m_strSRepairDate;
         private string m_strERepairDate;
         private string m_strRepairUser;

         [StringLength(20, ErrorMessage = "长度不能超过20个字符")]
         [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
         public string RepairID
         {
             get { return m_strRepairID; }
             set { m_strRepairID = value; }
         }

         [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
         public string SRepairDate
         {
             get { return m_strSRepairDate; }
             set { m_strSRepairDate = value; }
         }

         [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
         public string ERepairDate
         {
             get { return m_strERepairDate; }
             set { m_strERepairDate = value; }
         }

         [ValInjection(ErrorMessageResourceName = "Injection", ErrorMessageResourceType = typeof(Resources.ErrorMsg))]
         public string RepairUser
         {
             get { return m_strRepairUser; }
             set { m_strRepairUser = value; }
         }
         
     }
 }
