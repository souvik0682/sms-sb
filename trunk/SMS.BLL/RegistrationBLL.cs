using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPR.Common;
using VPR.DAL;
using VPR.Utilities;
using VPR.Utilities.ResourceManager;
using VPR.Entity;
using System.Data;

namespace VPR.BLL
{
    public sealed class RegistrationBLL
    {
        public void SaveUser(UserEntity user, int modifiedBy)
        {
            RegistrationDAL.SaveUser(user, Constants.DEFAULT_COMPANY_ID, modifiedBy);            
        }

        public string SaveCompanyandUser(CompanyEntity company, UserEntity user)
        {
            int result = 0;
            string errMessage = string.Empty;
            result = RegistrationDAL.SaveCompanyandUser(company, user);

            switch (result)
            {
                case 1:
                    errMessage = ResourceManager.GetStringWithoutName("ERR00060");
                    break;
                default:
                    break;
            }

            return errMessage;        
        }

        public DataTable checkUserExist(string emailid)
        {
           return RegistrationDAL.checkUserExist(emailid);
        }

    }
}
