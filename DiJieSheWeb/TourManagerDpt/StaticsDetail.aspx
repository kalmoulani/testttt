﻿<%@ Page Title="" Language="C#" MasterPageFile="~/TourManagerDpt/manager.master"
    AutoEventWireup="true" CodeFile="StaticsDetail.aspx.cs" Inherits="TourManagerDpt_StaticsDetail" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="/Scripts/jqueryplugin/tablesorter/style.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryplugin/jquery.tablesorter.js" type="text/javascript"></script>
    <link href="/Scripts/jqueryplugin/tablesorter/style.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryplugin/jquery.tablesorter.js" type="text/javascript"></script>
    <script src="../Scripts/jqueryplugin/OrderIndex.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#tbMain").tablesorter();
            $(".IndexTable").orderIndex();
        });
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="main" runat="Server">
    <div class="detaillist">
        <div class="detailtitle" style="height:25px;">
            已接待情况
            <asp:Button ID="btnOutput" Text="导出列表" runat="server" OnClick="btnOutput1_Click" style="float:right" CssClass="btn2"/>
        </div>
        <asp:Repeater ID="rptStaticDetail" runat="server" OnItemDataBound="rptStaticDetail_ItemDataBound">
            <HeaderTemplate>
                <table class="tablesorter IndexTable">
                </table>
                <table id="tbMain" class="tablesorter InfoTable">
                    <thead>
                        <tr>
                            <th>
                                日期
                            </th>
                            <td>
                                游览人数
                            </td>
                            <th>
                                住宿人数
                            </th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%#Eval("ConsumeDate") %>
                    </td>
                    <td>
                        <ul>
                            <asp:Repeater ID="rptScenics" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <%#Eval("Enterprice.Name")%>：<%#Eval("Peoplenum")%>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </td>
                    <td>
                        <ul>
                            <asp:Repeater ID="rptHotels" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <%#Eval("Enterprice.Name")%>：<%#Eval("Peoplenum")%>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody> </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
