<%@ Page Title="" Language="C#" MasterPageFile="~/SiteWithoutSesion.Master" AutoEventWireup="true"
    CodeBehind="Registration.aspx.cs" Inherits="VPR.WebApp.Master.Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <link href="../Styles/Sitenew.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/stylenew.css" rel="Stylesheet" type="text/css" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
     <div id="headercaption">REGISTER USER</div>
    <center>
        <fieldset style="width:450px;">
            <legend>Register User</legend>
             
            <table border="0" cellpadding="3" cellspacing="3" width="100%">
            <tr>
            <td>First Name:</td>
            <td>
                <asp:TextBox ID="txtFirstname" runat="server" CssClass="txtMaster"></asp:TextBox></td><td nowrap="nowrap">
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtFirstname"> </asp:RequiredFieldValidator>
                </td>
               
            </tr>
             <tr>
            <td>Last Name:</td>
            <td>
                <asp:TextBox ID="txtLastName" runat="server" CssClass="txtMaster"></asp:TextBox></td><td>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtLastName"> </asp:RequiredFieldValidator></td>
            </tr>

        <tr>
            <td>
                User Name:
            </td>
            <td>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="txtMaster"></asp:TextBox></td><td>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtUsername"> </asp:RequiredFieldValidator>
            </td>

            <td colspan="2" class="hdr">
               Bank Details :</td>

        </tr>
        <tr>
            <td>
                Password:
            </td>
            <td>
                <asp:TextBox ID="txtpass" runat="server" CssClass="txtMaster" TextMode="Password"></asp:TextBox></td><td>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtpass"> </asp:RequiredFieldValidator>
            </td>
            <td nowrap="nowrap">
                Bank Name:</td>
            <td nowrap="nowrap">
                <asp:Label ID="lblBankName" runat="server" Text="UCO Bank"></asp:Label></td>
        </tr>
        <tr>
            <td>
               Confirm Password:
            </td>
            <td>
                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="txtMaster" TextMode="Password"></asp:TextBox></td><td nowrap="nowrap">
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                ErrorMessage="Passwords must match!" ControlToValidate="txtConfirmPassword" 
                ControlToCompare="txtpass" ForeColor="Red"></asp:CompareValidator>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtConfirmPassword"> </asp:RequiredFieldValidator>
            </td>
            <td>
                Address:</td>
            <td nowrap="nowrap">
                <asp:Label ID="lblAddress" runat="server" Text="Kolkata-54"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Company:
            </td>
            <td>
                <asp:TextBox ID="txtCompany" runat="server" CssClass="txtMaster"></asp:TextBox></td><td>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtCompany"> </asp:RequiredFieldValidator>
            </td>
            <td>
                IFSC Code:</td>
            <td nowrap="nowrap">
                <asp:Label ID="lblIFSCCode" runat="server" Text="1234567"></asp:Label></td>
        </tr>
        <tr>
        <td>
            Company Address:
        </td>
        <td>
            <asp:TextBox ID="txtCompanyAddress" runat="server" CssClass="txtMaster"></asp:TextBox></td><td>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtCompanyAddress"> </asp:RequiredFieldValidator>
        </td>
        <td>
           Account No.:</td>
            <td nowrap="nowrap">
            <asp:Label ID="lblAccountNo" runat="server" Text="001023456789"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Contact Person:
            </td>
            <td>
                <asp:TextBox ID="txtContactPerson" runat="server" CssClass="txtMaster"></asp:TextBox></td><td>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtContactPerson"> </asp:RequiredFieldValidator>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
          
        <tr>
            <td>
            
                Country:
            </td>
            <td>
           
              <asp:DropDownList ID="ddlCountry" CssClass="txtMaster" runat="server" 
                    AutoPostBack="True" 
                    onselectedindexchanged="ddlCountry_SelectedIndexChanged"></asp:DropDownList>                  
               
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
       
        <tr>
            <td>
                State:
            </td>
            <td>
                <asp:TextBox ID="txtState" runat="server" CssClass="txtMaster"></asp:TextBox>
                <asp:DropDownList ID="ddlState" runat="server" Visible="false" CssClass="txtMaster">
                </asp:DropDownList>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtState">
                </asp:RequiredFieldValidator>--%>
                 
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        
        <tr>
            <td>
                Mobile No:
            </td>
            <td>
                <asp:TextBox ID="txtMobileNo" runat="server" CssClass="txtMaster"></asp:TextBox></td><td>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtMobileNo"> </asp:RequiredFieldValidator>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Email ID:
            </td>
            <td>
                <asp:TextBox ID="txtEmailID" runat="server" CssClass="txtMaster"></asp:TextBox></td><td>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtEmailID"> </asp:RequiredFieldValidator>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Product Interest:
            </td>
            <td>
                <asp:DropDownList ID="ddlProductIneterest" runat="server" CssClass="txtMaster">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Enter
                Capcha:
            </td>
            <td>
                <asp:TextBox ID="txtCaptcha" runat="server" CssClass="txtMaster"></asp:TextBox></td><td>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtEmailID"> </asp:RequiredFieldValidator>
                </td>
                <td >
                                        <asp:Image ID="imgCaptcha" runat="server" />
                                    </td>
            
             <td valign="middle">
               <asp:Button ID="btnReloadCaptcha" runat="server" Text="Reload Captcha" 
                     onclick="btnReloadCaptcha_Click" CausesValidation="False"/>
                 </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                ADDRESS:
            </td>
            <td>
                <asp:TextBox ID="txtAddress" runat="server" CssClass="txtMaster"></asp:TextBox></td><td>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtEmailID"> </asp:RequiredFieldValidator>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="butBig" 
                    onclick="btnSubmit_Click" />
            </td>
      
            <td>
                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="butBig"/>
            </td>
      
            <td>
                &nbsp;</td>
      
            <td>
                &nbsp;</td>
        </tr>
      
    </table>  
 
       </fieldset>
    </center>
</asp:Content>
