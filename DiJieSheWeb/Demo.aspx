﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Demo.master"  CodeFile="Demo.aspx.cs" Inherits="Admin_Demo" %>


<asp:Content runat="server" ContentPlaceHolderID="main">
<script>
    $(function () {
        $(".pageMid_left").hide();
        $(".userinfo").hide();
    });

   
</script>
<style>
input{cursor:pointer;}
.detail_titlebg
{
    display:block;
    background:url("/theme/default/image/Detail_Titlebg.gif");
    height:33px;
    padding-left:10px;
    color:#5E716B;
    line-height:33px;
    font-weight:bold;
    font-size:13px;
}input[type='submit']
{
 width:250px;
}
</style>
    <div class="detail_titlebg">
地接社管理系统DEMO
    </div>
   <div class="detaillist">
    <fieldset>
        <legend>旅游管理部门</legend>
        <ul>
            <li>
                <asp:Button runat="server" ID="btnDptAdminLogin" Text="临海市风景旅游管理局 管理员登录" OnClick="btnDptAdminLogin_Click"  /></li></ul>
    </fieldset>
    <fieldset>
        <legend>旅行社</legend>
        <ul>
         <li>
            <asp:Button runat="server" Text="旅行社管理员登录界面" OnClick="btnDjsLoginUI_Click" ID="Button2" /></li>
           
            <li>
            <asp:Button runat="server" Text="临海市交通旅行社 管理员直接登录" OnClick="btnDjsLogin_Click" ID="btnDjsLogin" /></li>
            <li>
              <asp:Button runat="server" Text="台州海创旅游有限公司 管理员直接登录" OnClick="btnDjsLogin_Click2" ID="Button1" /></li>
         
           
           
        </ul>
    </fieldset>
      <fieldset>
        <legend>宾馆</legend>
        <ul>
            <li>
                <asp:Button runat="server" Text="临海市交通宾馆 管理员登录" OnClick="btnEntLogin_Click" ID="btnEntLogin" /></li>
        </ul>
    </fieldset>
    <fieldset>
        <legend>景区</legend>
        <ul>
            <li>
                <asp:Button runat="server" Text="情人谷 管理员登录" OnClick="btnScenicLogin_Click" ID="Button3" /></li>
        </ul>
    </fieldset>
     <fieldset>
        <legend>网站管理员</legend>
        <ul>
            <li>
                <asp:Button runat="server" ID="btnAdminLogin" Text="管理员登录" OnClick="btnAdminLogin_Click" /></li>
                  <li>
            <asp:Button runat="server" Text="测试数据复位(删除再重新生成)" OnClick="btnReport_Click" ID="btnReport" />
            </li>   <li>
            <asp:Button runat="server" Text="删除测试数据" OnClick="btnDelete_Click" ID="btnDelete" />
            </li>
                </ul>
    </fieldset>
    </div></asp:Content>