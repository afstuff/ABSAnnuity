﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_ANNTY_POLY_PERSNAL.aspx.vb" Inherits="PRG_ANNTY_POLY_PERSNAL" %>
<%@ Register Src="UC_BANT.ascx" TagName="UC_BANT" TagPrefix="uc1" %>

<%@ Register Src="UC_FOOT.ascx" TagName="UC_FOOT" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Annuity Module</title>
    <link rel="Stylesheet" href="SS_ILIFE.css" type="text/css" />
    <script language="javascript" type="text/javascript" src="Script/ScriptJS.js">
    </script>
    <script language="javascript" type="text/javascript" src="Script/SJS_02.js">
    </script>
    <script src="../jquery.min.js" type="text/javascript"></script>
    <script src="../jquery.simplemodal.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">


        //            function confirm(message, callback) {
        //                var modalWindow = document.getElementById("confirm");
        //                console.log(modalWindow);
        //                $(modalWindow).modal({
        //                    closeHTML: "<a href='#' title='Close' class='modal-close'>x</a>",
        //                    position: ["20%", ],
        //                    overlayId: 'confirm-overlay',
        //                    containerId: 'confirm-container',
        //                    onShow: function(dialog) {
        //                        var modal = this;

        //                        $('.message', dialog.data[0]).append(message);

        //                        $('.yes', dialog.data[0]).click(function() {
        //                            modal.close(); $.modal.close();
        //                            callback(); // HERE IS THE CALLBACK
        //                            return true;
        //                        });
        //                    }
        //                });
        //            }
        //            function doSomething() {
        //$('#txtRecStatusTemp').attr('value', 'OLD');
        //do nothing

        //            }

        // calling jquery functions once document is ready
        $(document).ready(function () {
        });



function cmdPfa_Browse_onclick() {

}

    </script>
    
     <style>
        .RecordOriginator
        {
            margin-left: 100px;
        }
    </style>

</head>

<body onload="<%= FirstMsg %>">
    <form id="Form1" name="Form1" runat="server">

        <!-- start banner -->
        <div id="div_banner" align="center">

            <uc1:UC_BANT ID="UC_BANT1" runat="server" />

        </div>

        <!-- start header -->
        <div id="div_header" align="center">
            <table id="tbl_header" align="center">
                <tr>
                    <td align="left" valign="top" class="myMenu_Title_02">
                        <table border="0" width="100%">

                            <tr>
                                <td align="left" colspan="2" valign="top" style="color: Red; font-weight: bold;"><%=STRMENU_TITLE%></td>
                                <td align="right" colspan="1" valign="top" style="display: none;">&nbsp;&nbsp;Status:&nbsp;<asp:TextBox ID="txtAction" Visible="true" ForeColor="Gray" runat="server" EnableViewState="False" Width="50px"></asp:TextBox>
                                </td>
                                <td align="right" colspan="1" valign="top">&nbsp;&nbsp;Find Insured Name:&nbsp;
                                <input type="text" id="txtSearch" name="txtSearch" value="Search..." runat="server"
                                    onfocus="if (this.value == 'Search...') {this.value = '';}"
                                    onblur="if (this.value == '') {this.value = 'Search...';}" />
                                    &nbsp;<asp:Button ID="cmdSearch" Text="Search" runat="server" />
                                    &nbsp;<asp:DropDownList ID="cboSearch" AutoPostBack="true" Width="150px" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4" valign="top">
                                    <hr />
                                </td>
                            </tr>

                            <tr>
                                <td align="center" colspan="4" valign="top">&nbsp;&nbsp;<a href="#" onclick="javascript:JSDO_RETURN('PRG_ANNTY_PROP_POLICY.aspx?menu=AN_QUOTE')">Go to Menu</a>

                                    &nbsp;&nbsp;<asp:Button ID="cmdPrev" CssClass="cmd_butt" Enabled="false" Text="«..Previous" runat="server" />
                                    &nbsp;&nbsp;<asp:Button ID="cmdNew_ASP" CssClass="cmd_butt" runat="server" Text="New Data" OnClientClick="JSNew_ASP();"></asp:Button>
                                    &nbsp;&nbsp;<asp:Button ID="cmdSave_ASP" CssClass="cmd_butt" runat="server" Text="Save Data"></asp:Button>
                                    &nbsp;&nbsp;<asp:Button ID="cmdDelete_ASP" CssClass="cmd_butt" Enabled="false" runat="server" Text="Delete Data" OnClientClick="JSDelete_ASP();"></asp:Button>
                                    &nbsp;&nbsp;<asp:Button ID="cmdPrint_ASP" CssClass="cmd_butt" Enabled="False" runat="server" Text="Print"></asp:Button>
                                    &nbsp;&nbsp;<asp:Button ID="cmdNext" CssClass="cmd_butt" Enabled="false" Text="Next..»" runat="server" />
                                    &nbsp;&nbsp;
                                </td>
                            </tr>

                        </table>
                    </td>
                </tr>
            </table>
        </div>

        <!-- start content -->
        <div id="div_content" align="center">
            <table class="tbl_cont" align="center">
                <tr>
                    <td nowrap class="myheader">Personal Information</td>
                </tr>
                <tr>
                    <td align="center" valign="top" class="td_menu">
                        <table align="center" border="0" class="tbl_menu_new">
                            <tr>
                                <td align="left" colspan="4" valign="top">
                                    <asp:Label ID="lblMsg" ForeColor="Red" Font-Size="Small" runat="server"></asp:Label>
                                    <asp:Label ID="lblOriginator" runat="server" Font-Size="Small" ForeColor="Red" CssClass="RecordOriginator"></asp:Label>
                                </td>
                            </tr>

                            <tr style="display: none;">
                                <td align="left" colspan="4" valign="top" class="myMenu_Title">Product/Marketer Info.</td>
                            </tr>

                            <tr>
                                <td align="left" valign="top">
                                    <asp:CheckBox ID="chkFileNum" AutoPostBack="true" Text="" runat="server" />
                                    &nbsp;<asp:Label ID="lblFileNum" Text="File No:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="1">
                                    <asp:TextBox ID="txtFileNum" Enabled="false" Width="150px" runat="server"></asp:TextBox>
                                    &nbsp;<asp:Button ID="cmdFileNum" Enabled="false" Text="Get Record" runat="server" />
                                    &nbsp;<asp:TextBox ID="txtRecNo" Visible="false" Enabled="false" MaxLength="18" Width="40" runat="server"></asp:TextBox></td>
                                <td nowrap align="left" valign="top">
                                    <asp:Label ID="lblPolNum" Text="Policy Number:" Enabled="false" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="1">
                                    <asp:TextBox ID="txtPolNum" Width="200px"
                                        Enabled="false" runat="server"></asp:TextBox>
                                    &nbsp;<asp:Button ID="cmdGetPol" Enabled="false" Text="Go" runat="server" />

                                </td>
                            </tr>

                            <tr>
                                <td align="left" valign="top">
                                    <asp:Label ID="lblTrans_Date" Text="Proposal Date:"
                                        runat="server"></asp:Label></td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtTrans_Date" MaxLength="10" runat="server"></asp:TextBox>
                                    <asp:ImageButton ID="butCal" Visible="false" runat="server"
                                        OnClientClick="OpenModal_Cal('../Calendar1.aspx?popup=YES',this.form.name,'txtTrans_Date','txtTrans_Date')"
                                        ImageUrl="~/Images/Calendar.bmp" Height="17" />
                                    &nbsp;<asp:Label ID="lblTrans_Date_Format" Text="dd/mm/yyyy" runat="server"></asp:Label>
                                </td>
                                <td align="left" valign="top">
                                    <asp:Label ID="lblQuote_Num" Text="Proposal No:" runat="server"></asp:Label></td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtQuote_Num" Width="200px" Enabled="true" runat="server"></asp:TextBox>
                                    <asp:CheckBox ID="chkPremiaRec" AutoPostBack="true" Text="Premia Record?" runat="server" />
                                </td>
                            </tr>

                            <tr>
                                <td align="left" valign="top">
                                    <asp:Label ID="lblUWYear" Text="Underwriting Year:" runat="server"></asp:Label></td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtUWYear" Enabled="true" MaxLength="4" Width="80" runat="server"></asp:TextBox>
                                </td>
                                <td nowrap align="left" valign="top">
                                    <asp:Label ID="lblBusSource" Text="Business Source:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="1">
                                    <asp:DropDownList ID="cboBusSource" Width="150px" runat="server">
                                    </asp:DropDownList>
                                    &nbsp;<asp:TextBox ID="txtBusSource" Visible="false" Enabled="false" MaxLength="10" Width="40px" runat="server"></asp:TextBox>&nbsp;<asp:TextBox ID="txtBusSourceName" Visible="false" Enabled="false" MaxLength="10" Width="40px" runat="server"></asp:TextBox></td>
                            </tr>


                            <tr>
                                <td align="left" valign="top">
                                    <asp:Label ID="lblProductClass" Text="Product Category:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="1">
                                    <asp:DropDownList ID="cboProductClass" AutoPostBack="true" CssClass="selProduct" runat="server" OnTextChanged="DoProc_ProductClass_Change"></asp:DropDownList>
                                    &nbsp;<asp:TextBox ID="txtProductClass" Visible="false" Enabled="false" MaxLength="10" Width="20" runat="server"></asp:TextBox></td>
                                <td align="left" valign="top">
                                    <asp:Label ID="lblProduct_Num" Text="Product Code:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="1">
                                    <asp:DropDownList ID="cboProduct" AutoPostBack="true" CssClass="selProduct" runat="server" OnTextChanged="DoProc_Product_Change"></asp:DropDownList>
                                    &nbsp;<asp:TextBox ID="txtProduct_Num" Visible="false" Enabled="false" MaxLength="10" Width="20px" runat="server"></asp:TextBox>&nbsp;<asp:TextBox ID="txtProduct_Name" Visible="false" Enabled="false" Width="20px" runat="server"></asp:TextBox></td>
                            </tr>

                            <tr>
                                <td align="left" valign="top">
                                    <asp:Label ID="lblCover_Num" Text="Cover Code:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="1">
                                    <asp:DropDownList ID="cboCover_Name" AutoPostBack="false" CssClass="selProduct" runat="server"></asp:DropDownList>
                                    &nbsp;<asp:TextBox ID="txtCover_Num" Visible="false" Enabled="true" MaxLength="10" Width="20" runat="server"></asp:TextBox>&nbsp;<asp:TextBox ID="txtCover_Name" Visible="false" Enabled="false" Width="20px" runat="server"></asp:TextBox></td>
                                <td align="left" valign="top">
                                    <asp:Label ID="lblPlan_Num" Text="Plan Code:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="1">
                                    <asp:DropDownList ID="cboPlan_Name" AutoPostBack="false" CssClass="selProduct" runat="server"></asp:DropDownList>
                                    &nbsp;<asp:TextBox ID="txtPlan_Num" Visible="false" Enabled="true" MaxLength="10" Width="20" runat="server"></asp:TextBox>&nbsp;<asp:TextBox ID="txtPlan_Name" Visible="false" Enabled="false" Width="20" runat="server"></asp:TextBox></td>
                            </tr>

                            <tr class="tr_frame_02">
                                <td nowrap align="left" valign="top">
                                    <asp:Label ID="lblBroker_Search" Text="Search for Broker:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="3">
                                    <asp:TextBox ID="txtBroker_Search" runat="server"></asp:TextBox>
                                    &nbsp;<asp:Button ID="cmdBroker_Search" Text="Search..." runat="server" OnClick="DoProc_Broker_Search" />
                                    &nbsp;<asp:DropDownList ID="cboBroker_Search" AutoPostBack="True" Width="400px"
                                        runat="server" OnTextChanged="DoProc_Broker_Change">
                                    </asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
                                <td align="left" valign="top">
                                    <asp:Label ID="lblBrokerNum" Text="Broker Code:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="3">
                                    <asp:TextBox ID="txtBrokerNum" AutoPostBack="true" MaxLength="10" runat="server" OnTextChanged="DoProc_Validate_Broker"></asp:TextBox>
                                    &nbsp;<asp:Label ID="lblBrokerName" Text="Full Name:" runat="server"></asp:Label>
                                    &nbsp;<asp:TextBox ID="txtBrokerName" Enabled="False" runat="server" Width="250px"></asp:TextBox>
                                    &nbsp;<input type="button" id="cmdBroker_Setup" name="cmdBroker_Setup" value="Setup" onclick="javascript: jsDoPopNew_Full('PRG_ANNTY_BRK_DTL.aspx?optid=001&optd=Brokers_Agents_Details&popup=YES')" />
                                    &nbsp;<input type="button" id="cmdBroker_Browse" name="cmdBroker_Browse" visible="false" value="Browse..." onclick="javascript: Sel_Func_Open('BRK', 'WebForm3.aspx?popup=YES', 'Form1', 'txtBrokerNum', 'txtBrokerName')" />
                                </td>
                            </tr>

                            <tr class="tr_frame_02">
                                <td nowrap align="left" valign="top">
                                    <asp:Label ID="lblAgcy_Search" Text="Search for Marketer:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="3">
                                    <asp:TextBox ID="txtAgcy_Search" runat="server"></asp:TextBox>
                                    &nbsp;<asp:Button ID="cmdAgcy_Search" Text="Search..." runat="server" OnClick="DoProc_Agcy_Search" />
                                    &nbsp;<asp:DropDownList ID="cboAgcy_Search" AutoPostBack="True" Width="400px"
                                        runat="server" OnTextChanged="DoProc_Agcy_Change">
                                    </asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
                                <td align="left" valign="top">
                                    <asp:Label ID="lblAgcyNum" Text="Marketer Code:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="3">
                                    <asp:TextBox ID="txtAgcyNum" AutoPostBack="true" MaxLength="10" runat="server" OnTextChanged="DoProc_Validate_Agency"></asp:TextBox>
                                    &nbsp;<asp:Label ID="lblAgcyName" Text="Full Name:" runat="server"></asp:Label>
                                    &nbsp;<asp:TextBox ID="txtAgcyName" Enabled="False" runat="server" Width="250px"></asp:TextBox>
                                    &nbsp;<input type="button" id="cmdAgcy_Setup" name="cmdAgcy_Setup" value="Setup" onclick="javascript: jsDoPopNew_Full('PRG_ANNTY_MKT_CD.aspx?optid=001&optd=Marketers_Agency&popup=YES')" />
                                    &nbsp;<input type="button" id="cmdAgcy_Browse" name="cmdAgcy_Browse" value="Browse..." onclick="javascript: Sel_Func_Open('MKT', 'WebForm3.aspx?popup=YES', 'Form1', 'txtAgcyNum', 'txtAgcyName')" />
                                </td>
                            </tr>

                            <tr style="display: none;">
                                <td align="left" colspan="4" valign="top" class="myMenu_Title">Assured/Customer Info</td>
                            </tr>
                            <tr class="tr_frame_02">
                                <td align="left" valign="top">
                                    <asp:Label ID="lblAssured_Search" Text="Search for Assured:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="3">
                                    <asp:TextBox ID="txtAssured_Search" runat="server"></asp:TextBox>
                                    &nbsp;<asp:Button ID="cmdAssured_Search" Text="Search..." runat="server" OnClick="DoProc_Assured_Search" />
                                    &nbsp;<asp:DropDownList ID="cboAssured_Search" AutoPostBack="True" Width="400px"
                                        runat="server" OnTextChanged="DoProc_Assured_Change">
                                    </asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
                                <td align="left" valign="top">
                                    <asp:Label ID="lblAssuredNum" Text="Assured Code:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="3">
                                    <asp:TextBox ID="txtAssured_Num" AutoPostBack="true" MaxLength="10" runat="server" OnTextChanged="DoProc_Validate_Assured"></asp:TextBox>
                                    &nbsp;<asp:Label ID="lblAssuredName" Text="Full Name:" runat="server"></asp:Label>
                                    &nbsp;<asp:TextBox ID="txtAssured_Name" Enabled="False" runat="server" Width="250px"></asp:TextBox>
                                    &nbsp;<input type="button" id="cmdAssured_Setup" name="cmdAssured_Setup" value="Setup" onclick="javascript: jsDoPopNew_Full('PRG_ANNTY_CUST_DTL.aspx?optid=001&optd=Customer_Details&popup=YES')" />
                                    &nbsp;<input type="button" id="cmdAssured_Browse" name="cmdAssured_Browse" value="Browse..." onclick="javascript: Sel_Func_Open('INS', 'WebForm3.aspx?popup=YES', 'Form1', 'txtAssured_Num', 'txtAssured_Name')" />
                                </td>
                            </tr>

                            <tr>
                                <td align="left" valign="top">
                                    <asp:Label ID="lblNationality" Text="Nationality:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="3">
                                    <asp:DropDownList ID="cboNationality" Width="220px" runat="server" OnTextChanged="DoProc_Nationality_Change"></asp:DropDownList>
                                    &nbsp;<asp:TextBox ID="txtNationality" Visible="false" MaxLength="4" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                                    &nbsp;<asp:TextBox ID="txtNationalityName" Visible="false" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                                    &nbsp;<input type="button" id="cmdNationality_Setup" name="cmdNationality_Setup" value="Setup" onclick="javascript: jsDoPopNew_Full('PRG_ANNTY_CODES.aspx?optid=001&optd=Nationality&popup=YES')" />
                                    &nbsp;&nbsp;<asp:Button ID="cmdNationality_Refresh" Text="Refresh" runat="server" OnClick="DoProc_Nationality_Refresh" />
                                </td>
                            </tr>

                            <tr>
                                <td nowrap align="left" valign="top">
                                    <asp:Label ID="lblOccupationClass" Text="Occupation Class:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="3">
                                    <asp:DropDownList ID="cboOccupationClass" Width="220px" runat="server" OnTextChanged="DoProc_OccupationClass_Change"></asp:DropDownList>
                                    &nbsp;<asp:TextBox ID="txtOccupationClass" Visible="false" MaxLength="4" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                                    &nbsp;<asp:TextBox ID="txtOccupationClassName" Visible="false" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                                    &nbsp;<input type="button" id="cmdOccupationClass_Setup" name="cmdOccupationClass_Setup" value="Setup" onclick="javascript: jsDoPopNew_Full('PRG_ANNTY_CODES.aspx?optid=007&optd=Occupation_Class&popup=YES')" />
                                    &nbsp;&nbsp;<asp:Button ID="cmdOccupationClass_Refresh" Text="Refresh" runat="server" OnClick="DoProc_OccupationClass_Refresh" />
                                </td>
                            </tr>

                            <tr>
                                <td nowrap align="left" valign="top">
                                    <asp:Label ID="lblOccupation" Text="Assured Occupation:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="3">
                                    <asp:DropDownList ID="cboOccupation" Width="220px" runat="server" OnTextChanged="DoProc_Occupation_Change"></asp:DropDownList>
                                    &nbsp;<asp:TextBox ID="txtOccupationNum" Visible="false" MaxLength="4" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                                    &nbsp;<asp:TextBox ID="txtOccupationName" Visible="false" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                                    &nbsp;<input type="button" id="cmdOccupation_Setup" name="cmdOccupation_Setup" value="Setup" onclick="javascript: jsDoPopNew_Full('PRG_ANNTY_CODES.aspx?optid=008&optd=Occupation&popup=YES')" />
                                    &nbsp;&nbsp;<asp:Button ID="cmdOccupation_Refresh" Text="Refresh" runat="server" OnClick="DoProc_Occupation_Refresh" />
                                </td>
                            </tr>

                            <tr>
                                <td align="left" valign="top">
                                    <asp:Label ID="lblGender" Text="Gender:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="1">
                                    <asp:DropDownList ID="cboGender" Width="150px" runat="server"></asp:DropDownList>
                                    &nbsp;<asp:TextBox ID="txtGender" Visible="false" Enabled="false" MaxLength="4" Width="40px" runat="server"></asp:TextBox>
                                    &nbsp;<asp:TextBox ID="txtGenderName" Visible="false" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                                </td>
                                <td align="left" valign="top" colspan="1">
                                    <asp:Label ID="lblMaritalStatus" Text="Marital Status:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="1">
                                    <asp:DropDownList ID="cboMaritalStatus" Width="150px" runat="server"></asp:DropDownList>
                                    &nbsp;<asp:TextBox ID="txtMaritalStatus" Visible="false" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                                    &nbsp;<asp:TextBox ID="txtMaritalStatusName" Visible="false" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td align="left" valign="top">
                                    <asp:Label ID="lblDOB" Text="Date of Birth:" runat="server"></asp:Label></td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtDOB" MaxLength="10" runat="server"></asp:TextBox>
                                    &nbsp;<asp:Label ID="lblDOB_Format" Text="dd/mm/yyyy" runat="server"></asp:Label></td>
                                <td align="left" valign="top">
                                    <asp:Label ID="lblDOB_ANB" Enabled="False" Text="Age:" runat="server"></asp:Label></td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtDOB_ANB" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td align="left" valign="top">
                                    <asp:Label ID="lblDOB_Place" Text="Place of Birth:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="1">
                                    <asp:TextBox ID="txtDOB_Place" Visible="true" Enabled="true" MaxLength="25" Width="200px" runat="server"></asp:TextBox>
                                </td>
                                <td align="left" valign="top">
                                    <asp:Label ID="lblDOB_Proof" Text="Proof of Age:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="1">
                                    <asp:DropDownList ID="cboDOB_Proof" Width="150px" runat="server">
                                        <asp:ListItem Selected="True" Value="*">(Select item)</asp:ListItem>
                                        <asp:ListItem Value="D">Drivers Licence</asp:ListItem>
                                        <asp:ListItem Value="P">Passport</asp:ListItem>
                                        <asp:ListItem Value="N">National ID Card</asp:ListItem>
                                        <asp:ListItem Value="V">Voters Card</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;<asp:TextBox ID="txtDOB_Proof" Visible="false" Enabled="false" MaxLength="10" Width="40px" runat="server"></asp:TextBox>
                                    &nbsp;<asp:TextBox ID="txtDOB_ProofName" Visible="false" Enabled="false" MaxLength="10" Width="40px" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>

                                <td align="left" valign="top">
                                    <asp:Label ID="lblReligion" Text="Religion/Belief:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="1">
                                    <asp:DropDownList ID="cboReligion" Width="150px" runat="server"></asp:DropDownList>
                                    &nbsp;<asp:TextBox ID="txtReligion" Visible="false" Enabled="false" MaxLength="4" Width="40px" runat="server"></asp:TextBox>
                                    &nbsp;<asp:TextBox ID="txtReligionName" Visible="false" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                                </td>
                                <td align="left" valign="top" style="display: none;">
                                    <asp:Label ID="lblRelation" Text="Relation:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="1" style="display: none;">
                                    <asp:DropDownList ID="cboRelation" Width="150px" runat="server"></asp:DropDownList>
                                    &nbsp;<asp:TextBox ID="txtRelation" Visible="false" Enabled="false" MaxLength="4" Width="40px" runat="server"></asp:TextBox>
                                    &nbsp;<asp:TextBox ID="txtRelationName" Visible="false" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td align="left" colspan="4" valign="top" class="myMenu_Title">Business Source Info</td>
                            </tr>
                            <tr>
                                <td align="left" valign="top">
                                    <asp:Label ID="lblBraNum" Text="Branch:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="3">
                                    <asp:DropDownList ID="cboBranch" Width="220px" runat="server" OnTextChanged="DoProc_Branch_Change"></asp:DropDownList>
                                    &nbsp;<asp:TextBox ID="txtBraNum" Visible="false" Enabled="true" MaxLength="4" Width="40px" runat="server"></asp:TextBox>
                                    &nbsp;<asp:TextBox ID="txtBraName" Visible="false" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                                    &nbsp;<input type="button" id="cmdBranch_Setup" name="cmdBranch_Setup" value="Setup" onclick="javascript: jsDoPopNew_Full('PRG_ANNTY_CODES.aspx?optid=003&optd=Branch&popup=YES')" />
                                    &nbsp;&nbsp;<asp:Button ID="cmdBranch_Refresh" Text="Refresh" runat="server" OnClick="DoProc_Branch_Refresh" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top">
                                    <asp:Label ID="lblDeptNum" Text="Department:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="3">
                                    <asp:DropDownList ID="cboDepartment" Width="220px" runat="server" OnTextChanged="DoProc_Dept_Change"></asp:DropDownList>
                                    &nbsp;<asp:TextBox ID="txtDeptNum" Visible="false" Enabled="true" MaxLength="4" runat="server" Width="40px"></asp:TextBox>
                                    &nbsp;<asp:TextBox ID="txtDeptName" Visible="false" Enabled="false" runat="server" Width="40px"></asp:TextBox>
                                    &nbsp;<input type="button" id="cmdDept_Setup" name="cmdDept_Setup" value="Setup" onclick="javascript: jsDoPopNew_Full('PRG_ANNTY_CODES.aspx?optid=005&optd=Department&popup=YES')" />
                                    &nbsp;&nbsp;<asp:Button ID="cmdDept_Refresh" Text="Refresh" runat="server" OnClick="DoProc_Dept_Refresh" />
                                </td>
                            </tr>

                            <tr>
                                <td align="left" valign="top">
                                    <asp:Label ID="lblProStatus" Enabled="false" Text="Proposal Status:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="3">
                                    <asp:DropDownList ID="cboProStatus" Width="150px" Enabled="false" runat="server">
                                        <asp:ListItem Selected="True" Value="*">(Select item)</asp:ListItem>
                                        <asp:ListItem Value="P">Proposal</asp:ListItem>
                                        <asp:ListItem Value="A">Active Policy</asp:ListItem>
                                        <asp:ListItem Value="W">Waiver</asp:ListItem>
                                        <asp:ListItem Value="C">Canceled</asp:ListItem>
                                        <asp:ListItem Value="L">Lapse Policy</asp:ListItem>
                                        <asp:ListItem Value="R">Reactivated policy</asp:ListItem>
                                        <asp:ListItem Value="U">Paid Up</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;<asp:TextBox ID="txtProStatus" Visible="false" Enabled="false" MaxLength="10" Width="40px" runat="server"></asp:TextBox>
                                    &nbsp;<asp:TextBox ID="txtProStatusName" Visible="false" Enabled="false" MaxLength="10" Width="40px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4" valign="top" class="myMenu_Title">Retirement/Retiree Info</td>

                            </tr>

                            <tr>
                                <td align="left" valign="top">
                                    <asp:Label ID="Label1" Text="Last Employer:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="0">
                                    <asp:TextBox ID="txtLastEmployer" Visible="true" Enabled="true" Width="250px" 
                                        runat="server" TextMode="MultiLine"></asp:TextBox>
                                </td>
                                <td align="left" valign="top">
                                    <asp:Label ID="Label3" Text="Last Emp. Addr:" runat="server"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtLastEmpAddr" Visible="true" Enabled="true" Width="300px" 
                                        runat="server" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                             <tr class="tr_frame_02">
                                <td nowrap align="left" valign="top">
                                    <asp:Label ID="Label13" Text="Search for PFA:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="3">
                                    <asp:TextBox ID="txtPfa_Search" runat="server"></asp:TextBox>
                                    &nbsp;<asp:Button ID="cmdPfa_Search" Text="Search..." runat="server" />
                                    &nbsp;<asp:DropDownList ID="cbo_PfaName" AutoPostBack="True" Width="400px"
                                        runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
                                <td align="left" valign="top">
                                    <asp:Label ID="Label14" Text="PFA CODE:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="3">
                                    <asp:TextBox ID="txtPfaNum" AutoPostBack="True" MaxLength="10" runat="server" 
                                        OnTextChanged="DoProc_Validate_Pfa"></asp:TextBox>
                                    &nbsp;<asp:Label ID="Label15" Text="PFA Name:" runat="server"></asp:Label>
                                    &nbsp;<asp:TextBox ID="txtPfaName" Enabled="False" runat="server" Width="250px"></asp:TextBox>
                                    &nbsp;<input type="button" id="cmdPfa_Setup" name="cmdPfa_Setup" value="Setup" onclick="javascript: jsDoPopNew_Full('PRG_ANN_PFA_DTL.aspx?optid=001&optd=pfa&popup=YES');" />
                                    &nbsp;<%--onclick="return cmdPfa_Browse_onclick()"--%>
                                    <input id="cmdPfa_Browse" name="cmdPfa_Browse" 
                                        onclick="javascript: Sel_Func_Open('PFA', 'Browse_Pfa.aspx?popup=YES', 'Form1', 'txtPfaNum', 'txtPfaName');"
                                        type="button" value="Browse..." />
                                    <%--<asp:Button ID="cmdPfa_Browse" runat="server" Text="Button" />--%>
                                </td>
                            </tr>
                            <tr style="">
                                <td align="left" valign="top">
                                    <asp:Label ID="Label4" Text="PFA PIN Number:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="0">
                                    <asp:TextBox ID="txtPIN" Visible="true" Enabled="true" Width="300px" runat="server"></asp:TextBox>
                                </td>
                                <td align="left" valign="top">
                                    <asp:Label ID="Label6" Text="Last Occupation:" runat="server"></asp:Label></td>
                                <td align="left">
                                    <asp:TextBox ID="txtLastOccupation" Visible="true" Enabled="true" Width="250px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top">
                                    <asp:Label ID="Label8" Text="Bank A/C Name:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="0">
                                    <asp:TextBox ID="txtAccountName" Visible="true" Enabled="true" Width="250px" runat="server"></asp:TextBox>
                                </td>
                                <td align="left" valign="top">
                                    <asp:Label ID="Label7" Text="Bank Name:" runat="server"></asp:Label></td>
                                <td align="left">
                                    <asp:TextBox ID="txtBankName" Visible="true" Enabled="true" Width="300px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top">
                                    <asp:Label ID="Label10" Text="Bank Ssort Code:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="0">
                                    <asp:TextBox ID="txtBankSortCode" Visible="true" Enabled="true" Width="250px" runat="server"></asp:TextBox>
                                </td>
                                <td align="left" valign="top">
                                    <asp:Label ID="Label9" Text="Account No:" runat="server"></asp:Label></td>
                                <td align="left">
                                    <asp:TextBox ID="txtAccountNo" Visible="true" Enabled="true" Width="300px" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td align="left" valign="top">
                                    <asp:Label ID="lblRetirementDate" Text="Retirement Date:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="0">
                                    <asp:TextBox ID="txtRetirementDate" Visible="true" Enabled="true"
                                        Width="171px" runat="server"></asp:TextBox>
                                    <asp:Label ID="lblDOB_Format0" Text="dd/mm/yyyy" runat="server"></asp:Label>
                                </td>
                                <td align="left" valign="top" rowspan="2">
                                    <asp:Label ID="Label11" Text="Bank Address:" runat="server"></asp:Label></td>
                                <td rowspan="2" valign="top" align="left">
                                    <asp:TextBox ID="txtBankAddress" Visible="true" Enabled="true" Width="300px" 
                                        runat="server" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top">
                                    &nbsp;</td>
                                <td align="left" valign="top" colspan="0">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4" valign="top" class="myMenu_Title">Other Info</td>

                            </tr>

                            <tr>
                                <td align="left" valign="top">
                                    <asp:Label ID="Label2" Text="Other Reference No:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="1">
                                    <asp:TextBox ID="txtOtherRefNo" Visible="true" Enabled="true" Width="200px" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td align="left" colspan="4" valign="top">&nbsp;</td>
                            </tr>

                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div id='confirm'>
            <div class='header'><span>Confirm</span></div>
            <div class='message'></div>
            <div class='buttons'>
                <div class='no simplemodal-close'>No</div>
                <div class='yes'>Yes</div>
            </div>
        </div>
        <div id="div_footer" align="center">

            <table id="tbl_footer" align="center">
                <tr>
                    <td valign="top">
                        <table align="center" border="0" class="footer" style="background-color: Black;">
                            <tr>
                                <td colspan="4">
                                    <uc2:UC_FOOT ID="UC_FOOT1" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>

    </form>
</body>
</html>
