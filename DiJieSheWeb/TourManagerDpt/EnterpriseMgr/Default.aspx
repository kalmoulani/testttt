﻿<%@ Page Title="" Language="C#" MasterPageFile="~/TourManagerDpt/manager.master"
    AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="TourManagerDpt_EnterpriseMgr_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<link href="/Scripts/jqueryplugin/jqueryui/css/ui-lightness/jquery-ui-1.9.1.custom.min.css"
        rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="/Scripts/jqueryplugin/jqueryui/js/jquery-ui-1.9.1.custom.min.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/json2.js"></script>

    <script language="javascript" type="text/javascript">
        var entNames = JSON.parse("<%=EntNames %>");
        $(function () {
            $("#<%=tbxName.ClientID %>").autocomplete({
                source: entNames
            });
        });
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">
    <fieldset>
        <legend>增加<%=ParamEntType %></legend>
        
        企业名称:<asp:TextBox runat="server" ID="tbxName"></asp:TextBox>
      
        <asp:Button runat="server" ID="btnAdd" Text="纳入奖励范围" OnClick="btnAdd_Click" />
        <asp:Label runat="server" ID="lblMsg" CssClass="success" Visible="false">操作成功</asp:Label>
    </fieldset>
    <fieldset>
        <legend><%=ParamEntType%>列表</legend>
            <div class="searchdiv">
                纳入状态:<asp:RadioButtonList runat="server" AutoPostBack="true"  ID="cbxState"  
                    RepeatDirection="Horizontal" RepeatLayout="Flow" 
                    onselectedindexchanged="cbxState_SelectedIndexChanged">
                     <asp:ListItem Value="0" Selected="True">全部</asp:ListItem>
                    <asp:ListItem>已纳入</asp:ListItem>
                    <asp:ListItem >已移除</asp:ListItem>
                </asp:RadioButtonList>
             
            </div>
            <asp:Repeater runat="server" ID="rptEntList" 
            onitemcommand="rptEntList_ItemCommand" 
            onitemdatabound="rptEntList_ItemDataBound">
                <HeaderTemplate>
                    <table class="detaillist">
                        <tr>
                            <td>
                                名称
                            </td>
                            <td>
                                负责人
                            </td>
                            <td>
                                负责人电话
                            </td>
                            <td>
                                操作
                            </td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                        <%#Eval("Name") %>
                        </td>
                        <td><%#Eval("ChargePersonName")%>
                        </td>
                         <td><%#Eval("ChargePersonPhone")%>
                        </td>
                        <td>
                         <asp:Button runat="server" ID="btnVerifyState" CommandArgument='<%#Eval("Id") %>'  CommandName="Verify" />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table></FooterTemplate>
            </asp:Repeater>
      
    </fieldset>
</asp:Content>