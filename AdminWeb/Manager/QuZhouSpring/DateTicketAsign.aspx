﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Manager/manager.master" AutoEventWireup="true" CodeFile="DateTicketAsign.aspx.cs" Inherits="Manager_QuZhouSpring_DateTicketAsign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphmain" Runat="Server">
    <div class="detail_titlebg">
      <h2>媒体和台州的数量已经在1月29日设置好,其他日期只需要将它们的数量设置为1</h2>  <asp:Literal Text="" ID="laDate" runat="server" />
    </div>
    <div class="detaillist">
        <div class="detailtitle">
            分配列表
        </div>
        <asp:Repeater runat="server" ID="rptAsignList" 
            onitemdatabound="rptAsignList_ItemDataBound" 
            onitemcommand="rptAsignList_ItemCommand">
            <HeaderTemplate>
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            门票名称
                        </td>
                        <td>
                            总票数
                        </td>
                        <td>
                            分配情况(合作商名称--分配票数--已售票数)
                        </td>
                        <td>
                            已售总数
                        </td>
                        <td>
                            操作
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("Ticket.Scenic.Name")%> <%# Eval("Ticket.Name")%>
                    <asp:HiddenField runat="server" ID="hfTaId" Value='<%# Eval("Id") %>' />
                    </td>
                    <td><%# Eval("Amount")%></td>
                    <td>
                        <asp:Repeater runat="server" ID="rptPartnerList" OnItemDataBound="rptPartnerList_ItemDataBound">
                            <HeaderTemplate>
                                <table border="0" cellpadding="0" cellspacing="0">
                            </HeaderTemplate>
                            <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%# Eval("Partner.Name")%>
                                            <asp:HiddenField ID="lblticketid" Value='<%# Eval("QZTicketAsign.Ticket.Id") %>' runat="server" />
                                            <asp:HiddenField runat="server" ID="hfPartnerId" Value='<%# Eval("Partner.Id") %>' />
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="tbxAsignAmount"  Text='<%# Eval("AsignedAmount") %>'/>
                                        </td>
                                        <td>
                                            <%# Eval("SoldAmount")%>
                                        </td>
                                    </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                    <tr>
                                        <td>
                                            媒体(不计算在售票总数内)
                                            <asp:HiddenField runat="server" ID="HiddenField1" Value='<%# Eval("Partner.Id") %>' />
                                        </td>
                                        <td></td>
                                        <td>
                                        <%--<asp:HiddenField runat="server" ID="hfProductId" Value='<%# Eval("QZTicketAsign.ProductCode") %>' />--%>
                                            <asp:Label ID="lblMedia" Text="0" runat="server" />
                                            <%--<%# Eval("SoldAmount")%>--%>
                                        </td>
                                    </tr>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </td>
                    <td>
                        <%# Eval("SoldAmount")%>
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="保存" CommandName="save" CommandArgument='<%# Eval("Id") %>' />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>

