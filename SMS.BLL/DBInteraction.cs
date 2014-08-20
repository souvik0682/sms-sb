using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VPR.DAL.DbManager;
using VPR.Entity;

namespace VPR.BLL
{
    public class DBInteraction
    {
        #region General
        public bool IsUnique(string tableName, string colName, string val)
        {

            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery("Select count(*) from " + tableName + " where " + colName + " = '" + val + "'", true);
            bool returnval = false;
            try
            {
                returnval = Convert.ToInt32(dquery.GetScalar()) > 0 ? false : true;
            }
            catch (Exception)
            {


            }

            return returnval;

        }


        public DataSet PopulateDDLDS(string tableName, string textField, string valuefield)
        {

            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery("Select [" + textField + "] ListItemValue, [" + valuefield + "] ListItemText from dbo." + tableName+" order by ListItemText", true);

            return dquery.GetTables();

        }
        public DataSet PopulateDDLDS(string tableName, string textField, string valuefield, bool isDSR)
        {

            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery("Select [" + textField + "] ListItemValue, [" + valuefield + "] ListItemText from " + tableName + " order by ListItemText", true);

            return dquery.GetTables();

        }

        public DataSet PopulateDDLDS(string tableName, string textField, string valuefield, string WhereClause)
        {
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery("Select [" + textField + "] ListItemValue, [" + valuefield + "] ListItemText from dbo." + tableName + " " + WhereClause, true);
            return dquery.GetTables();
        }

        public DataSet PopulateDDLDS(string tableName, string textField, string valuefield, string WhereClause, bool isDSR)
        {

            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery("Select [" + textField + "] ListItemValue, [" + valuefield + "] ListItemText from " + tableName + " " + WhereClause + ") order by ListItemText", true);

            return dquery.GetTables();

        }

        public int GetId(string ItemName, string ItemValue)
        {
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery("prcGetId");
            dquery.AddVarcharParam("@ItemName", 15, ItemName);
            dquery.AddVarcharParam("@ItemValue", 50, ItemValue);
            return Convert.ToInt32(dquery.GetScalar());
        }

        public decimal GetExchnageRate(DateTime dt)
        {
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery("prcGetExchnageRate");
            dquery.AddDateTimeParam("@Exdate", dt);
            return Convert.ToDecimal(dquery.GetScalar());
        }

        public DataSet GetShipLine_Tax(int userid)
        {
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery("prcGetShipLine_Tax");
            dquery.AddIntegerParam("@userid", userid);
            return dquery.GetTables();
        }

        public DataSet GetPCSLogin(int Locationid)
        {
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery("prcGetPCSLogin");
            dquery.AddIntegerParam("@Locationid", Locationid);
            return dquery.GetTables();
        }

        public void DeleteGeneral(string formName, int pk_tableID)
        {
            string ProcName = "prcDeleteGeneral";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            dquery.AddIntegerParam("@pk_tableID", pk_tableID);
            dquery.AddVarcharParam("@formName", 20, formName);
            dquery.RunActionQuery();

        }

        public int InvoiceDateCheck(DateTime dt)
        {
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery("prcInvoiceDateCheck");
            dquery.AddDateTimeParam("@StaxDate", dt);
            return Convert.ToInt32(dquery.GetScalar());
        }
        #endregion

        #region country

        public DataSet GetCountry(params object[] sqlParam)
        {
            string ProcName = "admin.prcGetCountry";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);

            dquery.AddIntegerParam("@pk_countryId", Convert.ToInt32(sqlParam[0]));
            dquery.AddVarcharParam("@CountryName", 200, Convert.ToString(sqlParam[1]));
            dquery.AddVarcharParam("@CountryAbbr", 2, Convert.ToString(sqlParam[2]));

            return dquery.GetTables();
        }

        public void DeleteCountry(int countryId)
        {
            string ProcName = "admin.prcDeleteCountry";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            dquery.AddIntegerParam("@pk_countryId", countryId);
            dquery.RunActionQuery();

        }

        public int AddEditCountry(int userID, int pk_CountryID, string CountryName, string CountryAbbr,string GMT,string ISD,string Sector, bool isEdit,
                                string mailhost, string userName, string password, string SMTPPort, string POP3Port, string SenderName, string SendMail, string Replyto)
        {
            string ProcName = "admin.prcAddEditCountry";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            dquery.AddIntegerParam("@userID", userID);
            dquery.AddIntegerParam("@pk_CountryId", pk_CountryID);
            dquery.AddVarcharParam("@CountryName", 200, CountryName);
            dquery.AddVarcharParam("@CountryAbbr", 5, CountryAbbr);
            dquery.AddVarcharParam("@GMT", 10, GMT);
            dquery.AddVarcharParam("@ISD", 10, ISD);
            dquery.AddVarcharParam("@Sector", 50, Sector);
            dquery.AddBooleanParam("@isEdit", isEdit);
            dquery.AddVarcharParam("@mailhost", 50, mailhost);
            dquery.AddVarcharParam("@username", 50, userName);
            dquery.AddVarcharParam("@password", 50, password);
            dquery.AddVarcharParam("@SMTPPort", 50, SMTPPort);
            dquery.AddVarcharParam("@POP3Port", 50, POP3Port);
            dquery.AddVarcharParam("@SenderName", 50, SenderName);
            dquery.AddVarcharParam("@SendMail", 50, SendMail);
            dquery.AddVarcharParam("@ReplyTo", 50, Replyto);
            return dquery.RunActionQuery();

        }

        #endregion
    }
}
