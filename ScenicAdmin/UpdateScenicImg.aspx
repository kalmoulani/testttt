﻿<%@ Page Title="" Language="C#" MasterPageFile="~/sm.master" AutoEventWireup="true"
    CodeFile="UpdateScenicImg.aspx.cs" Inherits="ScenicManager_UpdateScenicImg" %>

<%@ MasterType VirtualPath="~/sm.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="smHeader" runat="Server">
    <link href="/theme/default/css/smdefault.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.uploadify.min.js" type="text/javascript"></script>
    <link href="/Styles/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/swfobject.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id$='FileUpload1']").uploadify({
                'uploader': 'uploadify.swf',
                'script': 'Upload.ashx?mark=upload',
                'cancelImg': '/Scripts/uploadify-cancel.png',
                'folder': 'Upload',
                'multi': false,
                'auto': true,
                'fileExt': '*.jpg;*.gif;*.png',
                'fileDesc': 'Image Files (.JPG, .GIF, .PNG)',
                'buttonText': '选择文件',
                'wmode ': 'transparent ',
                'width': '76',
                'height': '31',
                'buttonImg': '/theme/default/image/btnupload.png',
                'onComplete': function (event, queueId, fileObj, response, data) {
                    var x = "Upload/" + response + "";
                    $("[id$='uploadimg']").attr("src", x);
                    $("[id$='hfimgurl']").val(response);
                }
            });
        });
        ///删除上传的图片。
        function deleteImg() {
            $.ajax({
                cache: true,
                url: "/Upload.ashx?mark=delete",
                type: "POST",
                dataType: "json"
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphmain" runat="Server">
    <asp:HiddenField ID="hfimgurl" runat="server" />
    <p class="fuctitle">
        <a id="listname" runat="server" href="/ScenicPictureShow.aspx">景区图片管理</a>>&nbsp;<span
            id="pictype" runat="server">编辑图片</span></p>
    <hr />
    <div id="picshow">
        <div class="scpicmain">
            <span>编辑图片</span>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 100px">
                    </td>
                    <td style="width: 550px">
                        <img id="uploadimg" style="width: 500px; height: 350px;" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        上传
                    </td>
                    <td>
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        图片类型
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlpictype" runat="server">
                            <asp:ListItem Value="1">主图</asp:ListItem>
                            <asp:ListItem Value="2">辅图</asp:ListItem>
                            <asp:ListItem Value="3">备图</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        图片名称
                    </td>
                    <td>
                        <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        图片描述
                    </td>
                    <td>
                        <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" Width="500px" Height="60px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnok" runat="server" CssClass="btnsaveimg enable" OnClick="btnok_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
