﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Manager/manager.master" AutoEventWireup="true"
    CodeFile="UnionTicketEdit.aspx.cs" Inherits="Manager_ScenicManage_TicketManage_UnionTicketEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphmain" runat="Server">
    <!--已绑定景区-->
   
        <h2>
            当前门票:
        </h2>
        
        <%=CurrentTicket.Name %>
 <div>
      
       <asp:Button runat="server" ID="btnSavePrice" Text="保存价格" OnClick="btnSavePrice_Click" />
        </div>
        <h3>
            该门票已经绑定的门票</h3>
   
    <div>
        <asp:Repeater runat="server" ID="rptTickets" OnItemCommand="rptScenics_ItemCommand">
            <ItemTemplate>
                <li><span>
                    <%#Eval("Name") %></span><asp:Button runat="server" ID="btnDelete" CommandArgument='<%#Eval("id") %>'
                        CommandName="deletescenic" Text="删除" /></li>
            </ItemTemplate>
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <FooterTemplate>
                </ul></FooterTemplate>
        </asp:Repeater>
    </div>
    <div>
        <h3>
            为联票添加关联景区</h3>
        景区名称关键字:<asp:TextBox runat="server" ID="tbxKeyword"></asp:TextBox>
       <asp:CheckBox runat="server" ID="chxAutoAdd" Text="如果没有则自动创建一张门票,加入此套票" /> 
       
       <asp:Button runat="server" ID="btnSearch" Text="搜索/添加" OnClick="btnSearch_Click" />
        <asp:Repeater runat="server" ID="rptSearchScenics" OnItemCommand="rptSearchScenics_ItemCommand">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <FooterTemplate>
                </ul></FooterTemplate>
            <ItemTemplate>
                <li><span>
                    <%#Eval("Name") %></span><asp:Button runat="server" ID="btnAdd" CommandArgument='<%#Eval("id") %>'
                        CommandName="addscenic" Text="添加普通门票" />
                        <asp:Button runat="server" ID="Button1" CommandArgument='<%#Eval("id") %>'
                        CommandName="addscenic" Text="添加套票" />
                        </li>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
