using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VPR.Common;
using VPR.DAL.DbManager;
using VPR.Entity;
using VPR.Utilities;
namespace VPR.DAL
{
    public sealed class CommonDAL
    {
        private CommonDAL()
        {
        }

        #region Common

        public static DataTable GetVoyageList(string prefixText)
        {
            string strExecution = "[common].[uspGetVoyageList]";
            DataTable myDataTable;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@InitialChar", 100, prefixText);
                myDataTable = oDq.GetTable();
            }

            return myDataTable;
        }

        public static DataTable GetVesselList(string prefixText)
        {
            string strExecution = "[common].[uspGetVesselList]";
            DataTable myDataTable;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@InitialChar", 100, prefixText);
                myDataTable = oDq.GetTable();
            }

            return myDataTable;
        }

        /// <summary>
        /// Saves the error log.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="message">The message.</param>
        /// <param name="stackTrace">The stack trace.</param>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>02/12/2012</createddate>
        public static void SaveErrorLog(int userId, string message, string stackTrace)
        {
            string strExecution = "[admin].[uspSaveError]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@UserId", userId);
                oDq.AddVarcharParam("@ErrorMessage", 255, message);
                oDq.AddVarcharParam("@StackTrace", -1, stackTrace);
                oDq.RunActionQuery();
            }
        }


        /// <summary>
        /// Common method to populate all the dropdownlist throughout the application
        /// </summary>
        /// <param name="Number">Unique Number</param>
        /// <returns>DataTable</returns>
        /// <createdby>Rajen Saha</createdby>
        /// <createddate>01/12/2012</createddate>
        public static DataTable PopulateDropdown(int Number, int? Filter1, int? Filter2)
        {
            string strExecution = "[common].[spPopulateDropDownList]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@Number", Number);
                oDq.AddIntegerParam("@Filter", Filter1.Value);
                oDq.AddIntegerParam("@Type", Filter2.Value);


                return oDq.GetTable();
            }
        }

        #endregion

        #region Location

        public static void DeleteLocation(int locId, int modifiedBy)
        {
            string strExecution = "[dbo].[uspDeleteLocation]";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@LocId", locId);
                oDq.AddIntegerParam("@ModifiedBy", modifiedBy);
                oDq.RunActionQuery();
            }
        }

        public static int SaveCompany(ICompany loc, int modifiedBy)
        {
            string strExecution = "[dbo].[uspSaveCompany]";
            int result = 0;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@CompId", loc.Id);
                oDq.AddVarcharParam("@CompName", 50, loc.CompName);
                oDq.AddVarcharParam("@CompAddress", 200, loc.CompAddress.Address);
                oDq.AddVarcharParam("@CompAddress2", 200, loc.CompAddress.Address2);
                oDq.AddVarcharParam("@LocCity", 20, loc.CompAddress.City);
                oDq.AddVarcharParam("@LocPin", 10, loc.CompAddress.Pin);
                oDq.AddVarcharParam("@LocAbbr", 3, loc.ContactPerson);
                oDq.AddVarcharParam("@LocPhone", 30, loc.CompPhone);
                //oDq.AddIntegerParam("@ManagerId", loc.ManagerId);
                oDq.AddBooleanParam("@IsActive", loc.IsActive);
                oDq.AddIntegerParam("@ModifiedBy", modifiedBy);
                oDq.AddIntegerParam("@Result", result, QueryParameterDirection.Output);
                oDq.RunActionQuery();
                result = Convert.ToInt32(oDq.GetParaValue("@Result"));

            }

            return result;
        }
        //New Function Added By Souvik - 11-06-2013
        public static List<ICompany> GetLocation_New(char isActiveOnly, int UserId, SearchCriteria searchCriteria)
        {
            string strExecution = "[dbo].[uspGetLocation_New]"; //Create a new SP with this Name (Previous one was : uspGetLocation)
            List<ICompany> lstLoc = new List<ICompany>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddCharParam("@IsActiveOnly", 1, isActiveOnly);
                oDq.AddIntegerParam("@UserId", UserId);
                oDq.AddVarcharParam("@SchAbbr", 3, searchCriteria.LocAbbr);
                oDq.AddVarcharParam("@SchLocName", 50, searchCriteria.LocName);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    ICompany loc = new CompanyEntity(reader);
                    lstLoc.Add(loc);
                }

                reader.Close();
            }

            return lstLoc;
        }

        public static List<ICompany> GetLocation(char isActiveOnly, SearchCriteria searchCriteria)
        {
            string strExecution = "[dbo].[uspGetLocation]";
            List<ICompany> lstLoc = new List<ICompany>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddCharParam("@IsActiveOnly", 1, isActiveOnly);
                oDq.AddVarcharParam("@SchAbbr", 3, searchCriteria.LocAbbr);
                oDq.AddVarcharParam("@SchLocName", 50, searchCriteria.LocName);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    ICompany loc = new CompanyEntity(reader);
                    lstLoc.Add(loc);
                }

                reader.Close();
            }

            return lstLoc;
        }

        public static List<ICompany> GetCompany(bool isActiveOnly, SearchCriteria searchCriteria)
        {
            string strExecution = "[dbo].[uspGetCompany]"; //Create a new SP with this Name (Previous one was : uspGetLocation)
            List<ICompany> lstLoc = new List<ICompany>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddBooleanParam("@IsActiveOnly", isActiveOnly);

                oDq.AddVarcharParam("@SchCountry", 3, searchCriteria.Country);
                oDq.AddVarcharParam("@SchCompName", 50, searchCriteria.Company);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    ICompany loc = new CompanyEntity(reader);
                    lstLoc.Add(loc);
                }

                reader.Close();
            }

            return lstLoc;
        }

        public static ICompany GetCompany(int CompId, char isActiveOnly, SearchCriteria searchCriteria)
        {
            string strExecution = "[dbo].[uspGetCompany]";
            ICompany loc = null;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@LocId", CompId);
                oDq.AddCharParam("@IsActiveOnly", 1, isActiveOnly);
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

        
        public static List<ICompany> GetCompanyByUser(int userId)
        {
            string strExecution = "[dbo].[uspGetCompanyByUser]";
            List<ICompany> lstLoc = new List<ICompany>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@UserId", userId);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    ICompany loc = new CompanyEntity(reader);
                    lstLoc.Add(loc);
                }

                reader.Close();
            }

            return lstLoc;
        }

        #endregion

        

        #region Group Company

        public static List<IGroupCompany> GetGroupCompany(char isActiveOnly, SearchCriteria searchCriteria)
        {
            string strExecution = "[common].[uspGetGroupCompany]";
            List<IGroupCompany> lstGroupCompany = new List<IGroupCompany>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddCharParam("@IsActiveOnly", 1, isActiveOnly);
                oDq.AddVarcharParam("@SchGroupName", 50, searchCriteria.GroupName);
                oDq.AddVarcharParam("@SortExpression", 50, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 4, searchCriteria.SortDirection);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IGroupCompany groupCompany = new GroupCompanyEntity(reader);
                    lstGroupCompany.Add(groupCompany);
                }

                reader.Close();
            }

            return lstGroupCompany;
        }

        #endregion

        #region User

        public static List<IUser> GetSalesExecutiveNew(int userId)
        {
            string strExecution = "[common].[uspGetSalesExecutiveNew]";
            List<IUser> lstUser = new List<IUser>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@UserId", userId);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    IUser user = new UserEntity(reader);
                    lstUser.Add(user);
                }

                reader.Close();
            }

            return lstUser;
        }

        #endregion

        #region Container Type

        public static IList<IContainerType> GetContainerType()
        {
            string strExecution = "[common].[uspGetContainerType]";
            List<IContainerType> lstContainerType = new List<IContainerType>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    lstContainerType.Add(new ContainerType(reader));
                }

                reader.Close();
            }

            return lstContainerType;
        }

        #endregion


        public static void SavePrintCount(string blNo)
        {
            string strExecution = "prcSaveInvoicePrintCount";
            using (DbQuery oDq = new DbQuery(strExecution))
            {

                oDq.AddVarcharParam("@BlNo", 60, blNo);
                oDq.RunActionQuery();
            }

        }

 
        #region Report

        public static DataTable GenerateExcel(string Location, string Vessel, string PortOfDischarge, string Line, string Voyage, string VIANo)
        {
            string strExecution = "GenerateExcel";
            DataTable dt = new DataTable();

            using (DbQuery oDq = new DbQuery(strExecution))
            {

                //oDq.AddVarcharParam("@filename", 1000, filename);
                oDq.AddVarcharParam("@Location", 60, Location);
                oDq.AddVarcharParam("@Vessel", 60, Vessel);
                oDq.AddVarcharParam("@PortOfDischarge", 60, PortOfDischarge);
                oDq.AddVarcharParam("@Line", 60, Line);
                oDq.AddVarcharParam("@Voyage", 60, Voyage);
                oDq.AddVarcharParam("@VIANo", 200, VIANo);
                dt = oDq.GetTable();
                //oDq.AddIntegerParam("@return", 0, QueryParameterDirection.Output);
                //oDq.RunActionQuery();
                //var result = Convert.ToInt32(oDq.GetParaValue("@return"));
                //if (result == 1) return true;
            }
            return dt;
            //return false;
        }

        public static bool GenerateTxt(string filename, int Location, int Vessel, int PortOfDischarge, int Line, int Voyage, int VIANo)
        {
            string strExecution = "[trn].[GenAdvanceContList]";


            using (DbQuery oDq = new DbQuery(strExecution))
            {

                oDq.AddNVarcharParam("@filename", 1000, filename);
                oDq.AddIntegerParam("@Loc", Location);
                oDq.AddIntegerParam("@Vsl", Vessel);
                oDq.AddIntegerParam("@Pod", PortOfDischarge);
                oDq.AddIntegerParam("@Line", Line);
                oDq.AddIntegerParam("@Vog", Voyage);
                oDq.AddIntegerParam("@VIA", VIANo);
                oDq.AddBooleanParam("@Result", false,QueryParameterDirection.Output);
                oDq.RunActionQuery();
                return Convert.ToBoolean(oDq.GetParaValue("@Result"));
            }
            
        }


        public static string GetTerminalType(int VoyageID, int VesselID, int PortOfDischarge)
        {
            //
            string strExecution = "[trn].[GetTerminalType]";
            string TerminalType = string.Empty;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@VoyageID",VoyageID);
                oDq.AddIntegerParam("@VesselID", VesselID);
                oDq.AddIntegerParam("@PortOfDischarge", PortOfDischarge);
                TerminalType = Convert.ToString(oDq.GetScalar());
            }
            return TerminalType;
        }

        public static DataTable GetLine(string Location)
        {
            string strExecution = "rptUspGetLineByLoc";
            DataTable myDataTable;
            if (Location == "All")
                Location = "0";
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@Location", Location.ToInt());
                myDataTable = oDq.GetTable();
            }

            return myDataTable;
        }

        public static DataTable GetExpLine(string Location)
        {
            string strExecution = "[exp].[rptUspGetLineByLoc]";
            DataTable myDataTable;
            if (Location == "All")
                Location = "0";
            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@Location", Location.ToInt());
                myDataTable = oDq.GetTable();
            }

            return myDataTable;
        }

        public static DataTable GetLineForHire(string Location)
        {
            string strExecution = "rptUspGetLineByLocForHireContainer";
            DataTable myDataTable;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@Location", Location.ToInt());
                myDataTable = oDq.GetTable();
            }

            return myDataTable;
        }
        public static DataTable GetVessels(string Line)
        {
            string strExecution = "rptUspGetVesselByNVOCCID";
            DataTable myDataTable;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@line", Line.ToInt());
                myDataTable = oDq.GetTable();
            }

            return myDataTable;
        }

        public static DataTable GetVoyages(string Vessel, string Line)
        {
            string strExecution = "rptUspGetVoyageByVesselID";
            DataTable myDataTable;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@Vessel", Vessel.ToInt()); oDq.AddIntegerParam("@line", Line.ToInt());
                myDataTable = oDq.GetTable();
            }

            return myDataTable;
        }

        public static DataTable GetExpVoyages(string Vessel, string Line)
        {
            string strExecution = "[exp].[spGetVoyageByVesselID]";
            DataTable myDataTable;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@Vessel", Vessel.ToInt()); 
                //oDq.AddIntegerParam("@line", Line.ToInt());
                myDataTable = oDq.GetTable();
            }

            return myDataTable;
        }


        public static DataTable GetVoyages(string Vessel)
        {
            string strExecution = "[dbo].[rptUspGetVoyageByVessel]";
            DataTable myDataTable;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@Vessel", Vessel.ToInt()); 
                myDataTable = oDq.GetTable();
            }

            return myDataTable;
        }

        public static DataTable GetBLNo(string line, string Vessel, string Voyage)
        {
            string strExecution = "rptUspGetLineBLNo";
            DataTable myDataTable;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@line", line.ToInt());
                oDq.AddIntegerParam("@Vessel", Vessel.ToInt());
                oDq.AddIntegerParam("@voyage", Voyage.ToInt());
                myDataTable = oDq.GetTable();
            }

            return myDataTable;
        }
        public static DataTable GetInvoiceByBLNo(string BLNo)
        {
            string strExecution = "rptPrcGetInvoice";
            DataTable myDataTable;

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@LineBLNo", 60, BLNo);
                myDataTable = oDq.GetTable();
            }

            return myDataTable;
        }
        #endregion

        #region Currency
        public static DataTable GetAllCurrency()
        {
            string strExecution = "[exp].[prcGetAllCurrency]";
            DataTable myDataTable;

            using (DbQuery oDq = new DbQuery(strExecution))
            {              
                myDataTable = oDq.GetTable();
            }

            return myDataTable;
        }
        #endregion

        public static DataSet GetCompanyDetails(Int32 companyId)
        {
            string ProcName = "[dbo].[PrcRptCompanyDetails]";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            dquery.AddIntegerParam("@CompanyId", companyId);
            return dquery.GetTables();
        }

        public static DataTable GetAllCountry()
        {
            string ProcName = "[dbo].[usp_GetAllCountry]";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            return dquery.GetTable();
        }

        public static DataTable GetAllGroup()
        {
            string ProcName = "[dbo].[usp_GetAllGroup]";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            return dquery.GetTable();
        }

        public static DataTable GetAllSubGroup()
        {
            string ProcName = "[dbo].[usp_GetAllSubGroup]";
            DAL.DbManager.DbQuery dquery = new DAL.DbManager.DbQuery(ProcName);
            return dquery.GetTable();
        }

        public static BerthEntity GetBerth(int BerthId)
        {
            string strExecution = "usp_GetBerth";
            BerthEntity o = new BerthEntity();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@BerthId", BerthId);
                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    o = new BerthEntity(reader);
                }
                reader.Close();
            }
            return o;
        }

     

        public static int SaveBerth(BerthEntity o)
        {
            int berthId = 0;
            string strExecution = "usp_SaveBerth";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                if (o.BerthId > 0)
                    oDq.AddIntegerParam("@BerthId", o.BerthId);
                
                oDq.AddIntegerParam("@PortId", o.PortId);
                oDq.AddVarcharParam("@BerthName", 50, o.BerthName);

                berthId = Convert.ToInt32(oDq.GetScalar());
                return berthId;
            }
        }

        public static List<BerthEntity> GetBerths(SearchCriteria searchCriteria)
        {
            string strExecution = "uspGetBerthList";
            List<BerthEntity> lstEg = new List<BerthEntity>();

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddVarcharParam("@BerthName", 500, searchCriteria.VesselName);
                oDq.AddVarcharParam("@Port", 200, searchCriteria.Port);

                oDq.AddVarcharParam("@SortExpression", 100, searchCriteria.SortExpression);
                oDq.AddVarcharParam("@SortDirection", 100, searchCriteria.SortDirection);

                DataTableReader reader = oDq.GetTableReader();

                while (reader.Read())
                {
                    BerthEntity eg = new BerthEntity(reader);
                    lstEg.Add(eg);
                }
                reader.Close();
            }
            return lstEg;
        }

        public static void DeleteBerth(int BerthId)
        {
            string strExecution = "usp_DeleteBerth";

            using (DbQuery oDq = new DbQuery(strExecution))
            {
                oDq.AddIntegerParam("@BerthId", BerthId);
                oDq.RunActionQuery();
            }
        }
    }
}
