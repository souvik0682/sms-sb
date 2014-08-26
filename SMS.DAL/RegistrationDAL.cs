using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPR.DAL.DbManager;
using VPR.Common;
using VPR.Entity;
using System.Data;

namespace VPR.DAL
{
   public static class RegistrationDAL
    {
        public static void SaveUser(UserEntity user, int companyId, int modifiedBy)
        {
            string strExecution = "[admin].[uspSaveUser]";  
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@UserId", 1);
                oDq.AddVarcharParam("@UserName", 10, user.Name);
                oDq.AddVarcharParam("@Pwd", 50, user.Password);
                oDq.AddVarcharParam("@FirstName", 30, user.FirstName);
                oDq.AddVarcharParam("@LastName", 30, user.LastName);
                oDq.AddIntegerParam("@RoleId", user.UserRole.Id);
                oDq.AddIntegerParam("@CompId", user.UserCompany.Id);
                oDq.AddVarcharParam("@EmailId", 50, user.EmailId);
                oDq.AddBooleanParam("@IsActive", true);
                oDq.AddIntegerParam("@ModifiedBy", 1);
                oDq.AddIntegerParam("@Result", 0);
                oDq.AddVarcharParam("@MobileNo", 50, user.MobileNo);
                oDq.RunActionQuery();
            }
        }

        public static int SaveCompanyandUser(CompanyEntity company, UserEntity user)
        {
            string strExecution = "[dbo].[procsaveCompandUser]";
            int result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@userID", 0);
                oDq.AddIntegerParam("@pk_CompanyId", 0);
                oDq.AddVarcharParam("@CompName", 100,company.Name);
                oDq.AddVarcharParam("@CompAddress1", 200, company.CompAddress.Address);
                oDq.AddVarcharParam("@CompAddress2", 200, company.CompAddress.Address2);
                oDq.AddVarcharParam("@City", 50, company.City);
                oDq.AddVarcharParam("@PIN", 10, company.PIN);
                oDq.AddVarcharParam("@CompPhone", 200, company.CompPhone);
                oDq.AddBooleanParam("@isedit", false);
                oDq.AddIntegerParam("@fk_CountryID", Convert.ToInt32(company.Country));

                oDq.AddIntegerParam("@fk_StateID",company.fk_StateID);

                oDq.AddVarcharParam("@RegMobile", 12, company.RegMobile);
                oDq.AddVarcharParam("@ProductInterest", 100, company.ProductInterest);
                oDq.AddVarcharParam("@statename",100, company.StateName);

                oDq.AddVarcharParam("@EmailID", 200, company.EmailID);
                oDq.AddVarcharParam("@ContactPerson", 100, company.ContactPerson);
                oDq.AddVarcharParam("@UserName", 10, user.Name);
                oDq.AddVarcharParam("@Pwd", 50, user.Password);
                oDq.AddVarcharParam("@FirstName", 30, user.FirstName);
                oDq.AddVarcharParam("@LastName", 30, user.LastName);
                oDq.AddIntegerParam("@RoleId", user.UserRole.Id);
                oDq.AddIntegerParam("@CompId", user.UserCompany.Id);
                oDq.AddBooleanParam("@IsActive", true);
                oDq.AddIntegerParam("@ModifiedBy", 1);
                oDq.AddIntegerParam("@Result", result, QueryParameterDirection.Output);               
                oDq.AddVarcharParam("@MobileNo", 12, user.MobileNo);
                oDq.RunActionQuery();
                result = Convert.ToInt32(oDq.GetParaValue("@Result"));
            }
            return result;
        }

       //check user exist
       public static DataTable checkUserExist(string emailid)
       {
           string strExecution = "[dbo].[procCheckUserExist]";
           DataTable dt = new DataTable();

           using (DbQuery oDq = new DbQuery(strExecution))
           {
               oDq.AddVarcharParam("@emailid",50, emailid);
               dt = oDq.GetTable();
           }
           return dt;
       }
    }
}
