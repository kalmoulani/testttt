﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL;
using CommonLibrary;
/// <summary>
/// 政府部门数据导入
/// </summary>
public partial class TourManagerDpt_EnterpriseList : basepageMgrDpt
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindList();
        }
    }

    protected void BindList()
    {
        rpt.DataSource = bllDJEnt.GetDJSForDpt(CurrentDpt.Area.Code);
     
        rpt.DataBind();
    }
    protected void rpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            DJ_TourEnterprise ent = e.Item.DataItem as DJ_TourEnterprise;
           
            Button btnVerify = e.Item.FindControl("btnSetVerify") as Button;
            if (ent.IsVeryfied)
            {
                btnVerify.Text = "已认证";
            }
            else{
                btnVerify.Text = "尚未认证";
            }
            if (ent is DJ_DijiesheInfo)
            {
                e.Item.Visible = false;
            }
        }
    }
    BLLDJ_User bllDjUser = new BLLDJ_User();
    BLLDJEnterprise bllDJEnt = new BLLDJEnterprise();
    BLLMembership bllMember = new BLLMembership();
    protected void rpt_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int entId = int.Parse(e.CommandArgument.ToString());

       
        if (e.CommandName.ToLower() == "setverify")
        {
            DJ_TourEnterprise ent = bllDJEnt.GetDJS8id(entId.ToString())[0];
            ent.IsVeryfied = !ent.IsVeryfied;
            bllDJEnt.Save(ent);
        }
        BindList();
            
    }
}