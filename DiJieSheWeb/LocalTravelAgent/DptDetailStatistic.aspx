﻿<%@ Page Title="" Language="C#" MasterPageFile="~/LocalTravelAgent/LTA.master" AutoEventWireup="true" CodeFile="DptDetailStatistic.aspx.cs" Inherits="LocalTravelAgent_DptDetailStatistic" %>
<%@ MasterType VirtualPath="~/LocalTravelAgent/LTA.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <div class="detail_titlebg">
        旅游管理部门详细信息
    </div>
    <div class="searchdiv">
        旅游企业的名称:<span runat="server" id="ETName" style=" font-size:14px;font-weight:bold"></span>
    </div>
    <div class="detaillist">
        <div class="detailtitle">
            详细列表
        </div>
        <asp:Repeater ID="rptETDetail" runat="server" 
            onitemdatabound="rptETDetail_ItemDataBound">
            <HeaderTemplate>
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            日期
                        </td>
                        <td>
                            游玩人数
                        </td>
                        <td>
                            住宿人天数
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <asp:Repeater ID="rptETMonthDetail" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%# Eval("ConsumeTime","{0:yyyy-MM-dd}")%>
                            </td>
                            <td>
                                <asp:Literal ID="laVisitedCount" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal ID="laLiveCount" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr>
                    <td>
                        <%# Eval("MonthIndex")%>月份小计
                    </td>
                    <td>
                        <asp:Literal ID="laMonthVTotal" runat="server"></asp:Literal>
                    </td>
                    <td>
                        <asp:Literal ID="laMonthLTotal" runat="server"></asp:Literal>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                <tr>
                    <td>
                        总计
                    </td>
                    <td>
                        <asp:Literal ID="laYearVTotal" runat="server"></asp:Literal>
                    </td>
                    <td>
                        <asp:Literal ID="laYearLTotal" runat="server"></asp:Literal>
                    </td>
                </tr>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
