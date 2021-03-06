﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LocalTravelAgent_GuideList : basepageDJS
{
    BLL.BLLDJGroup_Worker bllgroupworker = new BLL.BLLDJGroup_Worker();
    BLL.BLLWorker bllworker = new BLL.BLLWorker();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            BindList();
            tbxBelong.Text = CurrentDJS.Name;
        }
    }

    private void BindList()
    {
        var driver_source = bllworker.GetWorkers8Multi(null, null, null, null, null, Model.DJ_GroupWorkerType.导游, CurrentDJS.Id.ToString());
        rptGuides.DataSource = driver_source;
        rptGuides.DataBind();
    }

    //清除输入痕迹
    protected void ClearTxt()
    {
        txtName_add.Text = string.Empty;
        txtPhone_add.Text = string.Empty;
        txtId_add.Text = string.Empty;
        txtGuideid_add.Text = string.Empty;
        tbxBelong.Text = string.Empty;
    }

    //检查完整性
    protected bool CheckComplete()
    {
        if (string.IsNullOrEmpty(txtName_add.Text))
        {
            ShowNotification("姓名未填写!");
            return false;
        }
        if (string.IsNullOrEmpty(txtId_add.Text))
        {
            ShowNotification("身份证号码未填写!");
            return false;
        }

        if (hfIdcardError.Value != "验证通过")
        {
            ShowNotification(hfIdcardError.Value);
            return false;
        }
        if (string.IsNullOrEmpty(txtGuideid_add.Text))
        {
            ShowNotification("导游证号未填写!");
            return false;
        }
        return true;
    }

    //查找
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindList();
    }

    //快速添加
    protected void btnQuickadd_Click(object sender, EventArgs e)
    {
        if (!CheckComplete()) return;
        string message = string.Empty;
        bllgroupworker.SaveData(txtName_add.Text, txtPhone_add.Text, txtId_add.Text,
            txtGuideid_add.Text,tbxBelong.Text, Model.DJ_GroupWorkerType.导游, CurrentDJS, out message);
        if (!string.IsNullOrEmpty(message))
        {
            ShowNotification(message);
        }
        else
        {
            BindList();
            ClearTxt();
            ShowNotification("保存成功");
        }
    }

}