using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPR.DAL;
using VPR.Common;
using VPR.Entity;
using VPR.Utilities.ResourceManager;
using VPR.Utilities;
using VPR.Utilities.Cryptography;
using System.Net.Mail;
using System.Data;
using System.Web.UI.WebControls;

namespace VPR.BLL
{
    public class CommonBLL
    {
        public static void SavePrintCount(string blNo)
        {
            CommonDAL.SavePrintCount(blNo);
        }

        #region Common

        public static DataTable GetVoyageList(string prefixText)
        {
            return CommonDAL.GetVoyageList(prefixText);
        }

        public static DataTable GetVesselList(string prefixText)
        {
            return CommonDAL.GetVesselList(prefixText);
        }

        #region Email

        public static bool SendMail(string from, string displayName, string mailTo, string cc, string subject, string body, string mailServerIP)
        {
            bool sent = true;

            try
            {
                if (mailTo != "")
                {
                    MailMessage MyMail = new MailMessage();
                    MyMail.To.Add(new MailAddress(mailTo));
                    MyMail.Priority = MailPriority.High;
                    MyMail.From = new MailAddress(from, displayName);

                    if (cc != "")
                    {
                        MailAddress ccAddr = new MailAddress(cc);
                        MyMail.CC.Add(ccAddr);
                    }

                    MyMail.Subject = subject;
                    MyMail.Body = GetMessageBody(body);
                    //MyMail.BodyEncoding = System.Text.Encoding.ASCII;
                    MyMail.IsBodyHtml = true;

                    SmtpClient client = new SmtpClient(mailServerIP);
                    client.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                    client.Send(MyMail);
                }
                else { sent = false; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sent;
        }

        public static bool SendMail(string from, string displayName, string mailTo, string cc, string subject, string body, string mailServerIP, string mailUserAccount, string mailUserPwd)
        {
            bool sent = true;

            try
            {
                if (mailTo != "")
                {
                    MailMessage MyMail = new MailMessage();
                    MyMail.To.Add(new MailAddress(mailTo));
                    MyMail.Priority = MailPriority.High;
                    MyMail.From = new MailAddress(from, displayName);

                    if (cc != "")
                    {
                        MailAddress ccAddr = new MailAddress(cc);
                        MyMail.CC.Add(ccAddr);
                    }

                    MyMail.Subject = subject;
                    MyMail.Body = GetMessageBody(body);
                    //MyMail.BodyEncoding = System.Text.Encoding.ASCII;
                    MyMail.IsBodyHtml = true;

                    SmtpClient client = new SmtpClient(mailServerIP, 25);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    System.Net.NetworkCredential credential = new System.Net.NetworkCredential(mailUserAccount, mailUserPwd);
                    client.Credentials = credential;
                    client.Send(MyMail);
                }
                else { sent = false; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sent;
        }

        public static string GetMessageBody(string strBodyContent)
        {
            try
            {
                StringBuilder sbMsgBody = new StringBuilder();
                //sbMsgBody.Append("<font face='Verdana, Arial, Helvetica, sans-serif' size='10' color='#8B4B0D'>Daily Sales Call</font>");
                //sbMsgBody.Append("<br />");
                //sbMsgBody.Append("<br /><br /><br />");
                sbMsgBody.Append("<html><body>");
                sbMsgBody.Append("<font face=verdana size=2>" + strBodyContent + "</font>");
                sbMsgBody.Append("</body></html>");

                return sbMsgBody.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        /// <summary>
        /// Handles the exception.
        /// </summary>
        /// <param name="ex">The <see cref="System.Exception"/> object.</param>
        /// <param name="logFilePath">The log file path.</param>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>02/12/2012</createddate>
        public static void HandleException(Exception ex, string logFilePath)
        {
            int userId = 0;
            string userDetail = string.Empty;
            string baseException = string.Empty;

            if (ex.GetType() != typeof(System.Threading.ThreadAbortException))
            {
                if (System.Web.HttpContext.Current.Session[Constants.SESSION_USER_INFO] != null)
                {
                    IUser user = (IUser)System.Web.HttpContext.Current.Session[Constants.SESSION_USER_INFO];

                    if (!ReferenceEquals(user, null))
                    {
                        userId = user.Id;
                        //userDetail = user.Id.ToString() + ", " + user.FirstName + " " + user.LastName;
                    }
                }

                if (ex.GetBaseException() != null)
                {
                    baseException = ex.GetBaseException().ToString();
                }
                else
                {
                    baseException = ex.StackTrace;
                }

                try
                {
                    CommonDAL.SaveErrorLog(userId, ex.Message, baseException);
                }
                catch
                {
                    //try
                    //{
                    //    string message = DateTime.UtcNow.ToShortDateString().ToString() + " "
                    //            + DateTime.UtcNow.ToLongTimeString().ToString() + " ==> " + "User Id: " + userDetail + "\r\n"
                    //            + ex.GetBaseException().ToString();

                    //    GeneralFunctions.WriteErrorLog(logFilePath + LogFileName, message);
                    //}
                    //catch
                    //{
                    //    // Consume the exception.
                    //}
                }
            }
        }

        /// <summary>
        /// Common method to populate all the dropdownlist throughout the application
        /// </summary>
        /// <param name="Number">Unique Number</param>
        /// <returns>DataTable</returns>
        /// <createdby>Rajen Saha</createdby>
        /// <createddate>01/12/2012</createddate>
        public static void PopulateDropdown(int Number, DropDownList ddl, int? Filter1, int? Filter2)
        {
            ddl.DataSource = CommonDAL.PopulateDropdown(Number, Filter1, Filter2);
            ddl.DataValueField = "Value";
            ddl.DataTextField = "Text";
            ddl.DataBind();
        }

        #endregion

        #region Location

        public void DeleteLocation(int locId, int modifiedBy)
        {
            CommonDAL.DeleteLocation(locId, modifiedBy);
        }

        public string SaveCompany(ICompany loc, int modifiedBy)
        {
            int result = 0;
            string errMessage = string.Empty;
            result = CommonDAL.SaveCompany(loc, modifiedBy);

            switch (result)
            {
                case 1:
                    errMessage = ResourceManager.GetStringWithoutName("ERR00011");
                    break;
                case 2:
                    errMessage = ResourceManager.GetStringWithoutName("ERR00012");
                    break;
                default:
                    break;
            }

            return errMessage;
        }

        private void SetDefaultSearchCriteriaForLocation(SearchCriteria searchCriteria)
        {
            searchCriteria.SortExpression = "Location";
            searchCriteria.SortDirection = "ASC";
        }

        public List<ICompany> GetAllLocation(SearchCriteria searchCriteria)
        {
            return CommonDAL.GetLocation('N', searchCriteria);
        }


        //New Function Added By Souvik - 11-06-2013
        public List<ICompany> GetActiveLocation_New(int UserId)  
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForLocation(searchCriteria);
            return CommonDAL.GetLocation_New('Y', UserId, searchCriteria);
        }

        public List<ICompany> GetActiveLocation()
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForLocation(searchCriteria);
            return CommonDAL.GetLocation('Y', searchCriteria);
            //return CommonDAL.GetCountry();
        }

        public List<ICompany> GetActiveCompany()
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForLocation(searchCriteria);
            return CommonDAL.GetCompany(true, searchCriteria);
            //return CommonDAL.GetCountry();
        }

        //public void SaveLocation(ILocation loc, int modifiedBy)
        //{
        //    CommonDAL.SaveLocation(loc, modifiedBy);
        //}

        //public void DeleteLocation(int locId, int modifiedBy)
        //{
        //    CommonDAL.DeleteLocation(locId, modifiedBy);
        //}

        public List<ICompany> GetCompanyByUser(int userId)
        {
            return CommonDAL.GetCompanyByUser(userId);
        }

        #endregion

        #region Group Company

        private void SetDefaultSearchCriteriaForGroupCompany(SearchCriteria searchCriteria)
        {
            searchCriteria.SortExpression = "GroupName";
            searchCriteria.SortDirection = "ASC";
        }

        public List<IGroupCompany> GetActiveGroupCompany()
        {
            SearchCriteria searchCriteria = new SearchCriteria();
            SetDefaultSearchCriteriaForGroupCompany(searchCriteria);
            return CommonDAL.GetGroupCompany('Y', searchCriteria);
        }

        #endregion

        #region User

        public List<IUser> GetSalesExecutiveNew(int userId)
        {
            return CommonDAL.GetSalesExecutiveNew(userId);
        }

        #endregion

        #region Report
        public static string GetTerminalType(int VoyageID, int VesselID, int PortOfDischarge)
        { return CommonDAL.GetTerminalType(VoyageID, VesselID, PortOfDischarge); }

        public DataTable GenerateExcel(string Location, string Vessel, string PortOfDischarge, string Line, string Voyage, string VIANo)
        { return CommonDAL.GenerateExcel(Location, Vessel, PortOfDischarge, Line, Voyage, VIANo); }
       
        public static bool GenerateText(string filename, int Location, int Vessel, int PortOfDischarge, int Line, int Voyage, int VIANo)
        { return CommonDAL.GenerateTxt(filename, Location, Vessel, PortOfDischarge, Line, Voyage, VIANo); }

        public static DataTable GetLineForHire(string Location)
        {
            return CommonDAL.GetLineForHire(Location);
        }
        public static DataTable GetLine(string Location)
        {

            return CommonDAL.GetLine(Location);
        }
        public static DataTable GetVessels(string Line)
        {
            return CommonDAL.GetVessels(Line);
        }

        public static DataTable GetVoyages(string Vessel, string Line)
        {
            return CommonDAL.GetVoyages(Vessel, Line);
        }

        public static DataTable GetExpVoyages(string Vessel, string Line)
        {
            return CommonDAL.GetExpVoyages(Vessel, Line);
        }

        public static DataTable GetVoyages(string Vessel)
        {
            return CommonDAL.GetVoyages(Vessel);
        }
        public static DataTable GetBLNo(string line, string Vessel, string Voyage)
        {
            return CommonDAL.GetBLNo(line, Vessel, Voyage);
        }
        public static DataTable GetInvoiceByBLNo(string BLNo)
        {
            return CommonDAL.GetInvoiceByBLNo(BLNo);
        }

        public static DataTable GetExpLine(string Location)
        {

            return CommonDAL.GetExpLine(Location);
        }

      
        #endregion

        #region Currency
        public static DataTable GetAllCurrency()
        {
            return CommonDAL.GetAllCurrency();
        }
        #endregion

        public static DataSet GetCompanyDetails(Int32 companyId)
        {
            return CommonDAL.GetCompanyDetails(companyId);
        }

        public DataTable GetAllCountry()
        {
            return CommonDAL.GetAllCountry();
        }

        public DataTable GetAllGroup()
        {
            return CommonDAL.GetAllGroup();
        }

        public BerthEntity GetBerth(int BerthId)
        {
            return CommonDAL.GetBerth(BerthId);
        }

        public void SaveBerth(BerthEntity oBerth)
        {
            int BerthId = 0;
            BerthId = CommonDAL.SaveBerth(oBerth);

        }

        public List<BerthEntity> GetBerths(SearchCriteria searchCriteria)
        {
            return CommonDAL.GetBerths(searchCriteria);
        }

        public void DeleteBerth(int BerthId)
        {
            CommonDAL.DeleteBerth(BerthId);
        }
    }
}
