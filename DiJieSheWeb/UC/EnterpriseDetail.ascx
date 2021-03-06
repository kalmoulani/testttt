﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EnterpriseDetail.ascx.cs"
    Inherits="UC_EnterpriseDetail" %>
<div class="detail_titlebg">
    企业信息
</div>
<div class="detaillist">
    <div class="detailtitle">
        企业信息
    </div>
    <table>
        <tr>
            <td>
                名称:
            </td>
            <td>
                <%=Ent.Name %>
            </td>
        </tr>
        <tr>
            <td>
                已认证:
            </td>
            <td>
                <%--<%=Ent.IsVeryfied?"是":"否" %>--%>
            </td>
        </tr>
        <tr>
            <td>
                电话:
            </td>
            <td>
                <%=Ent.Phone %>
            </td>
        </tr>
        <tr>
            <td>
                类型:
            </td>
            <td>
                <%=Ent.Type %>
            </td>
        </tr>
        <tr>
            <td>
                类型:
            </td>
            <td>
                <%=Ent.Address %>
            </td>
        </tr>
        <tr>
            <td>
                区域:
            </td>
            <td>
                <%=Ent.Area.Name %>
            </td>
        </tr>
        <tr>
            <td>
                星级:
            </td>
            <td>
                <%=Ent.Level %>
            </td>
        </tr>
        <tr>
            <td>
                负责人姓名:
            </td>
            <td>
                <%=Ent.ChargePersonName %>
            </td>
        </tr>
        <tr>
            <td>
                负责人电话:
            </td>
            <td>
                <%=Ent.ChargePersonPhone %>
            </td>
        </tr>
        <tr>
            <td>
                电子邮箱:
            </td>
            <td>
                <%=Ent.Email %>
            </td>
        </tr>
        <tr>
            <td>
                营业执照:
            </td>
            <td>
                <%=Ent.Buslicense %>
            </td>
        </tr>
    </table>
</div>
