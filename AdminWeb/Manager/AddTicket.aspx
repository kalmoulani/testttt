﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Manager/manager.master" AutoEventWireup="true" CodeFile="AddTicket.aspx.cs" Inherits="Manager_ScenicManage_AddTicket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphmain" Runat="Server">

<div>现有门票</div>
<asp:Repeater runat="server" ID="rptTickets">
<HeaderTemplate><table><tr><td>名称</td><td>原价</td><td>在线支付价</td><td>景区现付价</td></tr></HeaderTemplate>
<FooterTemplate></table></FooterTemplate>
<ItemTemplate>
<tr>
<td></td><td></td><td></td><td></td>
</tr>
</ItemTemplate>
</asp:Repeater>
<div>
  <div>增加门票</div>
  <table>
  <tr>
    <td>门票名称</td><td><asp:TextBox runat="server" ID="tbxName"></asp:TextBox></td>

  <td>原价</td><td><asp:TextBox runat="server" ID="tbxOriginal"></asp:TextBox></td>
  <td>在线支付价</td><td><asp:TextBox runat="server" ID="tbxPayOnline"></asp:TextBox></td>
  <td>景区现付价</td><td><asp:TextBox runat="server" ID="tbxPayOffline"></asp:TextBox></td>
  </tr>
  </table>
</div>
</asp:Content>

