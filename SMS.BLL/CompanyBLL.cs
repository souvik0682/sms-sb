using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPR.DAL;
using VPR.Common;
using VPR.Entity;
using VPR.Utilities;
using System.Web;
using VPR.Utilities.ResourceManager;
using VPR.Utilities.Cryptography;
using System.Data;

namespace VPR.BLL
{
    public class CompanyBLL
    {
        public List<ICompany> GetAllCompanyList(SearchCriteria searchCriteria)
        {
            return CompanyDAL.GetCompanyList(true, searchCriteria);
        }

        public string SaveCompany(ICompany Comp, int modifiedBy)
        {
            int result = 0;
            string errMessage = string.Empty;
            result = CompanyDAL.SaveCompany(Comp, modifiedBy);

            //switch (result)
            //{
            //    case 1:
            //        errMessage = ResourceManager.GetStringWithoutName("ERR00060");
            //        break;
            //    default:
            //        break;
            //}

            return errMessage;
        }

        public List<ICompany> GetActiveCountry()
        {
            return CompanyDAL.GetCountry();
        }

        public List<ICompany> GetActiveState()
        {
            return CompanyDAL.GetState();
        }

        public static System.Data.DataSet GetCompanyById(int compID)
        {
            return CompanyDAL.GetCompanyById(compID);
        }

        public ICompany GetCompany(int CompId)
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            searchCriteria.SortExpression = "CompName";
            searchCriteria.SortDirection = "ASC";
            return CompanyDAL.GetCompany(CompId, false, searchCriteria);
        }
    }


}
