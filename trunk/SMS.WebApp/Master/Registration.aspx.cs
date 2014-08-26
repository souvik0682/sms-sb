using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Globalization;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using VPR.BLL;
using VPR.Entity;

namespace VPR.WebApp.Master
{
    public partial class Registration : System.Web.UI.Page
    {
        CommonBLL commonbll = new CommonBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCountry();
                LoadProduct();
                FillCapctha();
            }
        }

        private void LoadCountry()
        {
           
            ddlCountry.DataSource = commonbll.GetAllCountry();             
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataValueField = "pk_countryID";
            ddlCountry.DataBind();
        }

        private void LoadState()
        {
            ddlState.DataSource = commonbll.GetAllState();
            ddlState.DataTextField = "StateName";
            ddlState.DataValueField = "pk_StateID";
            ddlState.DataBind();
        }
        private void LoadProduct()
        {
           
            ddlProductIneterest.DataSource = commonbll.GetAllProduct();
            ddlProductIneterest.DataTextField = "ProductName";
            ddlProductIneterest.DataValueField = "pk_ProductID";
            ddlProductIneterest.DataBind();
        }    
        void FillCapctha()
        {
            try
            {
                Random random = new Random();
                string combination = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                StringBuilder captcha = new StringBuilder();
                for (int i = 0; i < 6; i++)
                    captcha.Append(combination[random.Next(combination.Length)]);
                Session["captcha"] = captcha.ToString();
                imgCaptcha.ImageUrl = "GenerateCaptcha.aspx?" + DateTime.Now.Ticks.ToString();
            }
            catch
            {
                throw;
            }
        }
        #region "CountryList"
        /// <summary>
        /// Get Country list in the world.
        /// </summary>
        /// <returns>SortedList</returns>
        //private SortedList CountryList()
        //{
        //    SortedList slCountry = new SortedList();
        //    string Key = "";
        //    string Value = "";

        //    foreach (CultureInfo info in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
        //    {
        //        RegionInfo info2 = new RegionInfo(info.LCID);
        //        int i = info2.GeoId;

        //        if (!slCountry.Contains(info2.EnglishName))
        //        {
        //            Value = info2.TwoLetterISORegionName;
        //            Key = info2.EnglishName;
        //            slCountry.Add(Key, Value);
        //        }
        //    }
        //    return slCountry;
        //}
        #endregion
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string catcha = Session["captcha"].ToString();
            if (catcha != txtCaptcha.Text)
                Page.ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script>javascript:alert('Invalid Captcha Code')</script>");
            else
            {
                SaveData();
                clearFields();
            }
           
            //FillCapctha();
        }
        private void clearFields()
        {
            txtCompany.Text="";
            txtCompanyAddress.Text = "";
            txtMobileNo.Text = "";
            txtEmailID.Text = "";
            txtContactPerson.Text = "";
            txtUsername.Text = "";
            txtpass.Text = "";
            txtFirstname.Text = "";
            txtLastName.Text = "";
            txtAddress.Text = "";
            txtCaptcha.Text = "";
        }
        protected void btnReloadCaptcha_Click(object sender, EventArgs e)
        {
          FillCapctha();
        }
        public void SaveData()
        {
            RegistrationBLL registration = new RegistrationBLL();
            CompanyEntity company = new CompanyEntity();
            //chk user exist or not
            if (registration.checkUserExist(txtEmailID.Text).Rows.Count > 0)
            {
                //data already exist
                Page.ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script>javascript:alert('Data Not Saved! User already exist')</script>");
            }
            else //data doesnot exist
            {
                company.Name = txtCompany.Text;
                company.CompAddress.Address = txtCompanyAddress.Text;
                company.CompAddress.Address2 = "";
                //userid
                company.RegMobile = txtMobileNo.Text;
                company.CompPhone = txtMobileNo.Text;
                company.City = "";
                company.PIN = "";
                company.EmailID = txtEmailID.Text;
                company.ContactPerson = txtContactPerson.Text;
                company.Country = ddlCountry.SelectedValue;
                if (ddlCountry.SelectedValue == "98")
                {
                    company.fk_StateID = Convert.ToInt32(ddlState.SelectedValue);
                    company.StateName = "";
                }
                else
                {
                    company.StateName = txtState.Text;
                    company.fk_StateID = 0;
                }
                company.ProductInterest = ddlProductIneterest.SelectedValue;
              

                UserEntity userEntity = new UserEntity();
                userEntity.Name = txtUsername.Text;
                userEntity.Password = txtpass.Text;
                userEntity.FirstName = txtFirstname.Text;
                userEntity.LastName = txtLastName.Text;
                userEntity.UserRole.Id = 2;
                userEntity.EmailId = txtEmailID.Text;
                userEntity.MobileNo = txtMobileNo.Text;
                //registration.SaveUser(userEntity, 1);
                string message = registration.SaveCompanyandUser(company, userEntity);
                if (message == "")
                {
                    Page.ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script>javascript:alert('Data Saved Successfully')</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script>javascript:alert('Data Not Saved')</script>");
                }
                Response.Redirect("~/Login.aspx");
            }
         
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCountry.SelectedValue == "98")
            {
                LoadState();
                ddlState.Visible = true;
                txtState.Visible = false;
            }
            else
            {
                txtState.Visible = true;
                ddlState.Visible = false;
            }
        }
        
    }
}