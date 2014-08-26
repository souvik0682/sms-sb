using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VPR.BLL;
using VPR.Common;
using VPR.Entity;
using VPR.Utilities;
using VPR.Utilities.ResourceManager;

namespace VPR.WebApp.View
{

    public partial class AddEditCompany : System.Web.UI.Page
    {
        #region Private Member Variables

        private int _userId = 0;
        private int _uId = 0;
        private bool _canAdd = false;
        private bool _canEdit = false;
        private bool _canDelete = false;
        private bool _canView = false;

        #endregion

        #region Protected Event Handlers
        protected void Page_Load(object sender, EventArgs e)
        {
            RetriveParameters();
            CheckUserAccess();
            SetAttributes();

            if (!IsPostBack)
            {
                PopulateCountry();
                PopulateState();
                LoadData();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveCompany();
            if (_uId != -1)
                Response.Redirect("~/View/ManageCompany.aspx");
            else
                InitializeData();
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCountry.Text == "98")
            {
                ddlState.Visible = true;
                ddlState.Enabled = true;
                ddlState.SelectedIndex = 0;
                txtState.Visible = false;
            }
            else
            {
                ddlState.Visible = false;
                txtState.Visible = true;
                ddlState.Enabled = false;
            }
        }

        #endregion

        #region Private Methods

        private void RetriveParameters()
        {
            _userId = UserBLL.GetLoggedInUserId();

            //Get user permission.
            UserBLL.GetUserPermission(out _canAdd, out _canEdit, out _canDelete, out _canView);

            if (!ReferenceEquals(Request.QueryString["id"], null))
            {
                Int32.TryParse(GeneralFunctions.DecryptQueryString(Request.QueryString["id"].ToString()), out _uId);
            }
        }

        private void SetAttributes()
        {
            spnCompName.Style["display"] = "none";
            spnAddress1.Style["display"] = "none";
            spnAddress2.Style["display"] = "none";
            spnEmail.Style["display"] = "none";
            spnCity.Style["display"] = "none";
            spnState.Style["display"] = "none";
            spnRegMobile.Style["display"] = "none";
            spnCompPhone.Style["display"] = "none";
            spnContactPerson.Style["display"] = "none";

            if (!IsPostBack)
            {
                if (_uId == -1) //Add mode
                {
                    if (!_canAdd) btnSave.Visible = false;
                }
                else
                {
                    if (!_canEdit) btnSave.Visible = false;
                }

                ddlState.Enabled = false;
                ddlState.Visible = true;
                txtState.Visible = false;

                btnBack.OnClientClick = "javascript:return RedirectAfterCancelClick('ManageCompany.aspx','" + ResourceManager.GetStringWithoutName("ERR00017") + "')";
                revEmail.ValidationExpression = Constants.EMAIL_REGX_EXP;
                revEmail.ErrorMessage = ResourceManager.GetStringWithoutName("ERR00026");

                spnCompName.InnerText = ResourceManager.GetStringWithoutName("ERR00048");
                spnAddress1.InnerText = ResourceManager.GetStringWithoutName("ERR00049");
                spnAddress2.InnerText = ResourceManager.GetStringWithoutName("ERR00050");
                spnEmail.InnerText = ResourceManager.GetStringWithoutName("ERR00051");
                spnCity.InnerText = ResourceManager.GetStringWithoutName("ERR00052");
                spnState.InnerText = ResourceManager.GetStringWithoutName("ERR00037");
                spnRegMobile.InnerText = ResourceManager.GetStringWithoutName("ERR00052");
                spnCompPhone.InnerText = ResourceManager.GetStringWithoutName("ERR00052");
                spnContactPerson.InnerText = ResourceManager.GetStringWithoutName("ERR00052");
            }

        }

        private void CheckUserAccess()
        {
            if (!ReferenceEquals(Session[Constants.SESSION_USER_INFO], null))
            {
                IUser user = (IUser)Session[Constants.SESSION_USER_INFO];

                if (ReferenceEquals(user, null) || user.Id == 0)
                {
                    Response.Redirect("~/Login.aspx");
                }

                if (user.UserRole.Id != (int)UserRole.Admin)
                {
                    Response.Redirect("~/Unauthorized.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }

            if (_uId == 0)
                Response.Redirect("~/View/ManageCompany.aspx");

            if (!_canView)
            {
                Response.Redirect("~/Unauthorized.aspx");
            }
        }

        private void PopulateCountry()
        {
            CompanyBLL userBll = new CompanyBLL();
            List<ICompany> lstCountry = userBll.GetActiveCountry();
            GeneralFunctions.PopulateDropDownList(ddlCountry, lstCountry, "fk_countryID", "Country", true);
        }

        private void PopulateState()
        {
            CompanyBLL commonBll = new CompanyBLL();
            List<ICompany> lstLoc = commonBll.GetActiveState();
            GeneralFunctions.PopulateDropDownList(ddlState, lstLoc, "fk_StateID", "StateName", true);
        }

        private void LoadData()
        {
            ICompany comp = new CompanyBLL().GetCompany(_uId);
            
            if (!ReferenceEquals(comp, null))
            {
                txtCompName.Text = comp.CompName;
                txtAddress1.Text = comp.CompAddress.Address;
                txtAddress2.Text = comp.CompAddress.Address2;
                txtCity.Text = comp.CompAddress.City;
                txtPIN.Text = comp.CompAddress.Pin;
                txtCompPhone.Text = comp.CompPhone;
                txtRegMobile.Text = comp.RegMobile;
                txtContactPerson.Text = comp.ContactPerson;
                txtEmail.Text = comp.EmailID;
                ddlCountry.SelectedValue = comp.fk_CountryID.ToString();
                if (comp.fk_CountryID == 98)
                {
                    ddlState.SelectedValue = comp.fk_StateID.ToString();
                    ddlState.Visible = true;
                    txtState.Visible = false;
                }
                else
                {
                    txtState.Text = comp.StateName;
                    ddlState.Visible = false;
                    txtState.Visible = true;
                }
                
            }
        }

        private void InitializeData()
        {
            
            txtCompName.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtCity.Text = "";
            txtPIN.Text = "";
            txtCompPhone.Text = "";
            txtRegMobile.Text = "";
            txtContactPerson.Text = "";
            txtEmail.Text = "";
            ddlCountry.SelectedIndex = 0;

            ddlState.SelectedIndex = -1;
            ddlState.Visible = true;
            ddlState.Enabled = false;
            txtState.Visible = false;
        }

        private bool ValidateControls(ICompany user)
        {
            bool isValid = true;

            if (user.CompName == string.Empty)
            {
                isValid = false;
                spnCompName.Style["display"] = "";
            }

            if (user.CompAddress.Address == string.Empty)
            {
                isValid = false;
                spnAddress1.Style["display"] = "";
            }

            if (user.EmailID == string.Empty)
            {
                isValid = false;
                spnEmail.Style["display"] = "";
            }

            if (user.fk_CountryID == -1)
            {
                isValid = false;
                spnCountry.Style["display"] = "";
            }
            
            //if (user.UserLocation.Id == 0)
            //{
            //    isValid = false;
            //    spnLoc.Style["display"] = "";
            //}
            //else
            //{
            //    if(user.UserRole.LocationSpecific.Value)
            //        user.UserRole.
            //}

            return isValid;
        }

        private void SaveCompany()
        {
            CompanyBLL CompBll = new CompanyBLL();
            ICompany Comp = new CompanyEntity();
            string message = string.Empty;
            BuildUserEntity(Comp);

            if (ValidateControls(Comp))
            {
                message = CompBll.SaveCompany(Comp, _userId);

                if (message != string.Empty)
                {
                    GeneralFunctions.RegisterAlertScript(this, message);
                }
            }
        }

        private void BuildUserEntity(ICompany Comp)
        {
            Comp.Id = _uId;
            Comp.CompName = txtCompName.Text.Trim().ToUpper();
            Comp.CompAddress.Address = txtAddress1.Text.Trim().ToUpper();
            Comp.CompAddress.Address2 = txtAddress2.Text.Trim().ToUpper();
            Comp.CompAddress.Pin = txtPIN.Text.Trim().ToUpper();
            Comp.CompAddress.City = txtCity.Text.Trim().ToUpper();
            Comp.CompPhone = txtCompPhone.Text.Trim().ToUpper();
            Comp.ContactPerson = txtContactPerson.Text.Trim().ToUpper();
            Comp.EmailID = txtEmail.Text;
            Comp.RegMobile = txtRegMobile.Text.Trim().ToUpper();
            Comp.CompType = ddlDebCre.SelectedValue.ToString().ToUpper();
            Comp.fk_CountryID = ddlCountry.SelectedValue.ToInt();
            Comp.fk_StateID = ddlState.SelectedValue.ToInt();
            Comp.StateName = txtState.Text.ToUpper();
        }

        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {

        }
    }
}