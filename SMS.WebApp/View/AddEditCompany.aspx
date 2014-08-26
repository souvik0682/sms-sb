<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditCompany.aspx.cs" Inherits="VPR.WebApp.View.AddEditCompany" MasterPageFile="~/Site.Master" Title=":: SMS :: Add / Edit Company" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">    
    <script src="../Scripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <div id="headercaption">ADD / EDIT COMPANY</div>
    <center>
        <fieldset style="width:450px;">
            <legend>Add / Edit Company</legend>
            <table border="0" cellpadding="3" cellspacing="3" width="100%">
                <tr>
                    <td style="width:150px;">Company Type:<span class="errormessage1">*</span></td>
                    <td>
                        <asp:DropDownList ID="ddlDebCre" runat="server">
<%--                        <asp:TextBox ID="TextBox1" runat="server" CssClass="textboxuppercase" MaxLength="200" Width="250"></asp:TextBox><br />--%>
                            <asp:ListItem Text="Debtor" Value="D" />
                            <asp:ListItem Text="Creditor" Value="C" />
                        </asp:DropDownList>
                 <%--       <br />--%>
                       <%-- <span id="Span4" runat="server" class="errormessage" style="display:none;"></span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="errormessage" ControlToValidate="txtCompName" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td style="width:150px;">Company Name:<span class="errormessage1">*</span></td>
                    <td>
                        <asp:TextBox ID="txtCompName" runat="server" CssClass="textboxuppercase" MaxLength="100" Width="250"></asp:TextBox><br />
                        <span id="spnCompName" runat="server" class="errormessage" style="display:none;"></span>
                        <asp:RequiredFieldValidator ID="rfvCompName" runat="server" CssClass="errormessage" ControlToValidate="txtCompName" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Company Address 1:<span class="errormessage1">*</span></td>
                    <td>
                        <asp:TextBox ID="txtAddress1" runat="server" CssClass="textboxuppercase" MaxLength="200" Width="250"></asp:TextBox><br />
                        <span id="spnAddress1" runat="server" class="errormessage" style="display:none;"></span>
                        <asp:RequiredFieldValidator ID="rfvAddress1" runat="server" CssClass="errormessage" ControlToValidate="txtAddress1" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Company Address 2:</td>
                    <td>
                        <asp:TextBox ID="txtAddress2" runat="server" CssClass="textboxuppercase" MaxLength="200" Width="250"></asp:TextBox><br />
                        <span id="spnAddress2" runat="server" class="errormessage" style="display:none;"></span>
                        <%--<asp:RequiredFieldValidator ID="rfvLName" runat="server" CssClass="errormessage" ControlToValidate="txtLName" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td>Email Id:<span class="errormessage1">*</span></td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" MaxLength="200" Width="250"></asp:TextBox><br />
                        <span id="spnEmail" runat="server" class="errormessage" style="display:none;"></span>
                        <%--<asp:RequiredFieldValidator ID="rfvEmail" runat="server" CssClass="errormessage" ControlToValidate="txtEmail" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" CssClass="errormessage" ValidationGroup="Save" Display="Dynamic"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>Country Name:<span class="errormessage1">*</span></td>
                    <td>
                        <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" Width="250" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"></asp:DropDownList><br />
                        <span id="spnCountry" runat="server" class="errormessage" style="display:none;"></span>
                        <%--<asp:RequiredFieldValidator ID="rfvRole" runat="server" CssClass="errormessage" ControlToValidate="ddlRole" InitialValue="0" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td>State:<span class="errormessage1">*</span></td>
                    <td>
                        <asp:DropDownList ID="ddlState" Width="250" runat="server"></asp:DropDownList><br />
                        <asp:TextBox ID="txtState" runat="server" CssClass="textboxuppercase" MaxLength="50" Width="250"></asp:TextBox><br />
                        <span id="spnState" runat="server" class="errormessage" style="display:none;"></span>
                        <asp:RequiredFieldValidator ID="rfvState" runat="server" CssClass="errormessage" ControlToValidate="txtState" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>City:</td>
                    <td>
                        <asp:TextBox ID="txtCity" runat="server" CssClass="textboxuppercase" MaxLength="50" Width="250"></asp:TextBox><br />
                        <span id="spnCity" runat="server" class="errormessage" style="display:none;"></span>
                        <%--<asp:RequiredFieldValidator ID="rfvLName" runat="server" CssClass="errormessage" ControlToValidate="txtLName" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td>PIN</td>
                    <td>
                        <asp:TextBox ID="txtPIN" runat="server" CssClass="textboxuppercase" MaxLength="10" Width="250"></asp:TextBox><br />
                        <span id="spnPIN" runat="server" class="errormessage" style="display:none;"></span>
                        <%--<asp:RequiredFieldValidator ID="rfvLName" runat="server" CssClass="errormessage" ControlToValidate="txtLName" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td>Company Phone</td>
                    <td>
                        <asp:TextBox ID="txtCompPhone" runat="server" CssClass="textboxuppercase" MaxLength="200" Width="250"></asp:TextBox><br />
                        <span id="spnCompPhone" runat="server" class="errormessage" style="display:none;"></span>
                        <%--<asp:RequiredFieldValidator ID="rfvLName" runat="server" CssClass="errormessage" ControlToValidate="txtLName" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td>Contact Person:</td>
                    <td>
                        <asp:TextBox ID="txtContactPerson" runat="server" CssClass="textboxuppercase" MaxLength="100" Width="250"></asp:TextBox><br />
                        <span id="spnContactPerson" runat="server" class="errormessage" style="display:none;"></span>
                        <%--<asp:RequiredFieldValidator ID="rfvLName" runat="server" CssClass="errormessage" ControlToValidate="txtLName" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td>Registered Mobile No:</td>
                    <td>
                        <asp:TextBox ID="txtRegMobile" runat="server" CssClass="textboxuppercase" MaxLength="12" Width="250"></asp:TextBox><br />
                        <span id="spnRegMobile" runat="server" class="errormessage" style="display:none;"></span>
                        <%--<asp:RequiredFieldValidator ID="rfvLName" runat="server" CssClass="errormessage" ControlToValidate="txtLName" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>

               <%-- <tr>
                    <td>Is Active?:</td>
                    <td><asp:CheckBox ID="chkActive" runat="server" /></td>
                </tr>--%>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click" />&nbsp;&nbsp;<asp:Button 
                            ID="btnBack" runat="server" CssClass="button" Text="Back" 
                            onclick="btnBack_Click"/>
                    </td>
                </tr>
            </table>
        </fieldset>
    </center>
</asp:Content>