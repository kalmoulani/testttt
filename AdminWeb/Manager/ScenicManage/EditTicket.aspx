﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Manager/manager.master" AutoEventWireup="true"
    CodeFile="EditTicket.aspx.cs" Inherits="Manager_ScenicManage_AddTicket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphmain" runat="Server">
    <!--管理某个景区的门票信息-->
    
    <div>
    <span id="spScenicName"><a href="/Scenic/?sname=<%=Scenic.SeoName %>"><%=Scenic.Name %></a>   </span>    门票列表</div>
    <a href="EditTicket.aspx?scenicid=<%=Scenic.Id %>">添加门票</a>
    <a href="/manager/scenicmanage/">返回列表</a>
  
    <div>
     
                    将所有门票移到其他景区下:

                   输入景区SeoName,或者id <asp:TextBox runat="server" ID="tbxTargetScenic" />

                   <asp:Button runat="server" ID="btnMove" Text="将门票移至指定的景区" OnClick="btnMove_Click" />
                    <!--如果是联票,创建之后 在上面的列表中会出现 指派景区的链接-->
               
    </div>
    <asp:Repeater runat="server" ID="rptTickets" 
        onitemcommand="rptTickets_ItemCommand">
        <HeaderTemplate>
            <table>
                <tr>
                    <td>
                        名称
                    </td>
                    <td>
                        原价
                    </td>
                    <td>
                        在线支付价
                    </td>
                    <td>
                        景区现付价
                    </td>
                    <td></td>
                </tr>
        </HeaderTemplate>
        <FooterTemplate>
            </table></FooterTemplate>
        <ItemTemplate>
            <tr>
                <td><%#Eval("Name") %>
                </td>
                <td><%# ((Model.Ticket)Container.DataItem).GetPrice(Model.PriceType.Normal).ToString("0") %>
                </td>
                <td><%# ((Model.Ticket)Container.DataItem).GetPrice(Model.PriceType.PayOnline).ToString("0") %>
                </td>
                <td><%# ((Model.Ticket)Container.DataItem).GetPrice(Model.PriceType.PreOrder).ToString("0") %>
                </td>
                <td>
                <asp:Button runat="server" CommandName="delete" CommandArgument=<%#Eval("id") %> Text="删除" />
                <a href='EditTicket.aspx?scenicid=<%=Scenic.Id %>&ticketId=<%#Eval("Id") %>'>编辑</a>
                 <a href='/Manager/ScenicManage/TicketManage/UnionTicketEdit.aspx?ticketid=<%#Eval("Id")%>'>联票管理</a>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
    <div>
        <div>
            增加门票</div>
        <table>
            <tr>
                <td>
                    门票名称
                </td>
                <td>
                    <asp:TextBox runat="server" ID="tbxName"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    原价
                </td>
                <td>
                    <asp:TextBox runat="server" ID="tbxOriginal"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    在线支付价
                </td>
                <td>
                    <asp:TextBox runat="server" ID="tbxPayOnline"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    景区现付价
                </td>
                <td>
                    <asp:TextBox runat="server" ID="tbxPayOffline"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    是联票
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="cbxIsUnionTicket" />
                    <!--如果是联票,创建之后 在上面的列表中会出现 指派景区的链接-->
                </td>
            </tr>
              <tr>
                <td>
                    是主票(显示在列表页的价格)
                </td>
                <td>
                    <asp:CheckBox Checked=true runat="server" ID="cbxIsMainPrice" />
                    <!--如果是联票,创建之后 在上面的列表中会出现 指派景区的链接-->
                </td>
            </tr>
           
        </table>
        <div><asp:Button runat="server" ID="btnSave"   OnClick="btnSave_Click" Text="保存" /> </div>
    </div>
</asp:Content>
