﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Model;
/// <summary>
///basepage 的摘要说明
/// </summary>
public class basepage : System.Web.UI.Page
{
    BLL.BLLMembership bllMember = new BLL.BLLMembership();
    public MembershipUser CurrentUser
    {
        get { return Membership.GetUser(); }
    }
    public TourMembership CurrentMember
    {
        get { return bllMember.GetMemberById((Guid)CurrentUser.ProviderUserKey); }
    }


    bool needLogin = true;
    protected bool NeedLogin { get { return needLogin; } set { needLogin = value; } }


    protected override void OnLoad(EventArgs e)
    {
        if (NeedLogin)
        {
            if (CurrentUser == null)
            {
                Response.Redirect("/login.aspx?returnUrl="+Request.RawUrl);
            }
        }

        base.OnLoad(e);
    }
    protected void ShowNotification(string title, string content, CommonLibrary.NotificationType nt, string redirectUrl)
    {
        CommonLibrary.Notification.Show(this, title, content,nt, redirectUrl,true,5);
    }
    protected void ShowNotification(string title,string content,string redirectUrl)
    {
        ShowNotification(title, content, CommonLibrary.NotificationType.success, redirectUrl);
    }
    protected void ShowNotification(string title, string content,CommonLibrary.NotificationType nt)
    {
        ShowNotification(title, content,nt, string.Empty);
    }
    protected void ShowNotification(string title, string content)
    {
        ShowNotification(title, content, CommonLibrary.NotificationType.success);
    }
    protected void ShowNotification(string content,CommonLibrary.NotificationType nt)
    {
        ShowNotification(string.Empty, content,nt);
    }
    protected void ShowNotification(string content)
    {
       ShowNotification(content,CommonLibrary.NotificationType.success);
    }

}