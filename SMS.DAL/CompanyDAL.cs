﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VPR.Common;
using VPR.DAL.DbManager;
using VPR.Entity;

namespace VPR.DAL
{
    public sealed class CompanyDAL
    {
        public static List<ICompany> GetCompanyList(bool isActiveOnly, SearchCriteria searchCriteria)
        {
            string strExecution = "[dbo].[uspGetCompany]";
            List<ICompany> lstUser = new List<ICompany>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBooleanParam("@IsActiveOnly", isActiveOnly);
                oDq.AddIntegerParam("@CompID", searchCriteria.IntegerOption1);
                oDq.AddVarcharParam("@SchCompName", 10, searchCriteria.Company);
                oDq.AddVarcharParam("@SchCountry", 30, searchCriteria.Country);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    ICompany user = new CompanyEntity(reader);
                    lstUser.Add(user);
                }

                reader.Close();
            }

            return lstUser;
        }

        public static int SaveCompany(ICompany Comp, int modifiedBy)
        {
            string strExecution = "[admin].[prcAddEditCompany]";
            int result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@pk_CompanyId", Comp.Id);
                oDq.AddVarcharParam("@CompName", 100, Comp.CompName);
                oDq.AddVarcharParam("@CompAddress1", 200, Comp.CompAddress.Address);
                oDq.AddVarcharParam("@CompAddress2", 200, Comp.CompAddress.Address2);
                oDq.AddVarcharParam("@City", 50, Comp.CompAddress.City);
                oDq.AddVarcharParam("@PIN", 10, Comp.CompAddress.Pin);
                oDq.AddVarcharParam("@CompPhone", 200, Comp.CompPhone);
                oDq.AddVarcharParam("@EmailId", 200, Comp.EmailID);
                oDq.AddVarcharParam("@RegMobile", 12, Comp.RegMobile);
                oDq.AddIntegerParam("@userID", modifiedBy);
                oDq.AddVarcharParam("@ProductInterest", 100, Comp.ProductInterest);
                oDq.AddIntegerParam("@fk_CountryID", Comp.fk_CountryID);
                oDq.AddIntegerParam("@fk_StateID", Comp.fk_StateID);
                oDq.AddVarcharParam("@StateName", 50, Comp.StateName);
                oDq.AddVarcharParam("@CompType", 1, Comp.CompType);
                oDq.AddVarcharParam("@ContactPerson", 100, Comp.ContactPerson);

                //oDq.AddIntegerParam("@Result", result, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                result = 1;
                //result = Convert.ToInt32(oDq.GetParaValue("@Result"));
            }

            return result;
        }

        public static List<ICompany> GetCountry()
        {
            string strExecution = "[admin].[usp_GetCountry]";
            List<ICompany> lstCountry = new List<ICompany>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    ICompany role = new CompanyEntity(reader);
                    lstCountry.Add(role);
                }

                reader.Close();
            }

            return lstCountry;
        }

        public static List<ICompany> GetState()
        {
            string strExecution = "[admin].[usp_GetState]";
            List<ICompany> lstState = new List<ICompany>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    ICompany role = new CompanyEntity(reader);
                    lstState.Add(role);
                }

                reader.Close();
            }

            return lstState;
        }

        public static DataSet GetCompanyById(int CompId)
        {
            string strExecution = "uspGetCompany";
            DataSet reader = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@CompId", CompId);
                oDq.AddVarcharParam("@SortExpression", 10, null);
                oDq.AddVarcharParam("@SortDirection", 10, null);
                oDq.AddBooleanParam("@IsActiveOnly", true);

                reader = oDq.GetTables();


            }

            return reader;
        }

        public static ICompany GetCompany(int CompId, bool isActiveOnly, SearchCriteria searchCriteria)
        {
            string strExecution = "[dbo].[uspGetCompany]";
            ICompany loc = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@CompId", CompId);
                oDq.AddBooleanParam("@IsActiveOnly", isActiveOnly);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    loc = new CompanyEntity(reader);
                }

                reader.Close();
            }

            return loc;
        }

    }
}
