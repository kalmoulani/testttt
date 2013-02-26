﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BLL;
using Model;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using TourControls;
using System.Configuration;

public partial class Scenic_Default : basepage
{
    BLLScenic bllscenic = new BLLScenic();
    BLLMembership bllMember = new BLLMembership();
    BLLTicketPrice bllticketprice = new BLLTicketPrice();
    BLLOrder bllorder = new BLLOrder();
    BLLScenicImg bllscenicimg = new BLLScenicImg();
    BLLTopic blltopic = new BLLTopic();
    BLLTicket bllticket = new BLLTicket();
    public int TicketId = 0;
    public string scpoint = "";
    public string scbindname = "";
    public string bindimglist = "";
    public int imgcount;
    public string scaddress = "";
    public string booknote = "";
    public string sclevel = "";
    public string transguid = "";
    public string scdesc = "";
    public string scshortdesc = "";
    public string scmapname = "";
    public int scid;
    Scenic s;
    protected void Page_Load(object sender, EventArgs e)
    {

        string paramSname = Request["sname"];

        if (!string.IsNullOrEmpty(paramSname))
        {
            s = new BLLScenic().GetScenicBySeoName(paramSname);
            if (s == null)
            {
                ErrHandler.Redirect(ErrType.UnknownError, "从seoname获取景区失败");
            }
            bind(s);
            if (IsPackageScenic(s))
            {
                bindpackage(s);
            }

        }
        else
        {
            ErrHandler.Redirect(ErrType.ParamIllegal, "传入参数是空");
        }
        var ticket = s.Tickets.FirstOrDefault(x => x.IsMain == true);
        decimal onlineprice = ticket == null ? 0 : ticket.GetPrice(PriceType.PayOnline);
        SetSeoTitle(s.Name, onlineprice);
    }
    /*
     景点内容页的title公式：***门票预订_***门票价格/多少钱 – 中国旅游在线     
     */
    const string scenicTitleFormat = "{0}门票预订_{0}门票价格/{1}元 – 中国旅游在线";
    private void SetSeoTitle(string scenicName, decimal price)
    {
        this.Title = string.Format(scenicTitleFormat, scenicName, price.ToString("0"));
        this.MetaDescription = string.Empty;
        this.MetaKeywords = string.Empty;
    }
    private void bind(Scenic scenic)
    {

        maintitlett.InnerHtml = scenic.Name;

        hfposition.Value = scenic.Position;
        scbindname = scenic.Name;
        hfscname.Value = scenic.Name;
        scid = scenic.Id;
        Area parentarea = new BLLArea().GetAreaByCode(scenic.Area.Code.Substring(0, 4) + "00");
        Area childarea = scenic.Area;
        areaname.HRef = "/Tickets/" + parentarea.SeoName;
        areaname.InnerHtml = parentarea.Name.Substring(3, parentarea.Name.Length - 3) + "&nbsp;>";
        if (parentarea.Id != childarea.Id)
        {
            county.Visible = true;
            string childname = childarea.Name.Substring(3);
            if (childname.Length >= 6)
                childname = childname.Substring(3);
            county.InnerHtml = childname + "&nbsp;>";
            county.HRef = "/Tickets/" + parentarea.SeoName + "_" + childarea.SeoName;
        }
        else
        {
            county.Visible = false;
        }
        //导航链接 隐藏套票的所属单位
        string owerName = scenic.Name;
        IList<Ticket> tickets = s.Tickets;
        foreach (Ticket t in tickets)
        {
            if (t.As<Ticket>() is TicketUnion)
            {
                owerName = t.DisplayNameOfOwner;
                break;
            }

        }
        scenicname.HRef = "/Tickets/" + parentarea.SeoName + "_" + scenic.Area.SeoName + "/" + scenic.SeoName + ".html";
        scenicname.InnerHtml = owerName;
        scaddress = scenic.Address;
        booknote = scenic.BookNote;
        sclevel = scenic.Level;
        scdesc = scenic.ScenicDetail;
        transguid = scenic.Trafficintro;
        if (!string.IsNullOrEmpty(scenic.Desec))
        {
            if (scenic.Desec.Length > 30)
                scshortdesc = scenic.Desec.Substring(0, 30) + "...";
            else
                scshortdesc = scenic.Desec + "...";
        }
        IList<ScenicImg> listsi = bllscenicimg.GetSiByType(scenic, 1);
        if (listsi.Count > 0)
            ImgMainScenic.Src = "/ScenicImg/mainimg/" + listsi[0].Name;
        //判断是否是联票，如果是的话则使用新的样式'
        if (scenic.Tickets != null && scenic.Tickets.Count > 0)
        {
            var t = scenic.Tickets[0].As<Ticket>();
            if (t is TicketUnion)
            {
                rptBookNote.DataSource = ((TicketUnion)t).TicketList;
                rptBookNote.DataBind();
                rptscInfo.DataSource = ((TicketUnion)t).TicketList;
                rptscInfo.DataBind();
                rptJt.DataSource = ((TicketUnion)t).TicketList;
                rptJt.DataBind();
            }
            else
            {
                List<Ticket> listTicket = new List<Ticket>();
                listTicket.Add(t);
                rptBookNote.DataSource = listTicket;
                rptBookNote.DataBind();
                rptscInfo.DataSource = listTicket;
                rptscInfo.DataBind();
                rptJt.DataSource = listTicket;
                rptJt.DataBind();
            }
        }
        else
        {
            Ticket t = new TicketNormal();
            t.Scenic = scenic;
            List<Ticket> listTicket = new List<Ticket>();
            listTicket.Add(t);
            rptBookNote.DataSource = listTicket;
            rptBookNote.DataBind();
            rptscInfo.DataSource = listTicket;
            rptscInfo.DataBind();
            rptJt.DataSource = listTicket;
            rptJt.DataBind();
        }




        IList<Scenic> list = bllscenic.GetScenic();
        Dictionary<Scenic, double> places = new Dictionary<Scenic, double>();
        List<double> listdistance = new List<double>();
        if (!string.IsNullOrEmpty(scenic.Position))
        {
            //bindimg(list, scenic);
            //foreach (ScenicImg item in scdiction.Keys)
            //{
            bindimglist += scenic.Position + ":";
            // }
        }


        //绑定主题
        rpttopic.DataSource = blltopic.GetStByscid(scenic.Id);
        rpttopic.DataBind();

        //绑定普通票
        IList<Ticket> listticket = bllticket.GetTp(scenic.Id);

        //衢州新春门票，的主门票productCode
        if (listticket.Count > 0)
        {
            maintitlett.InnerHtml = listticket[0].DisplayNameOfOwner;
            IList<Ticket> listTicket = listticket.Where(x => x.IsMain).Where(x => x.TourActivity != null).ToList();
            if (listTicket.Count() > 0)
            {
                hfProductCode.Value = listTicket[0].ProductCode;
                //判断门票是否已经过期
                TourActivity act = listTicket[0].TourActivity;
                if (DateTime.Now.Date >= act.BeginDate && DateTime.Now.Date <= act.EndDate)
                {


                    var ticketAsign = listTicket[0].TourActivity
                         .GetActivityAssignForPartnerTicketDate(SiteConfig.PartnerCodeOfTourOL, listTicket[0].ProductCode, DateTime.Now.Date);

                    hfSyCount.Value = (ticketAsign.AssignedAmount - ticketAsign.SoldAmount).ToString();
                }
            }
            else
            {
                qzTicketCount.Visible = false;
            }
        }
        else
        {
            qzTicketCount.Visible = false;
        }



        rpttp.DataSource = listticket.Where(x => x.Enabled).ToList();
        rpttp.DataBind();
        //编辑
        EditRole();
        //sc_dp.scname = scenic.Name;
        //sc_dp.BaseData = booknote;



        //plate2.scname = scenic.Name;
        //plate2.BaseData = scenic.ScenicDetail;
        //sc_jtzn.scname = scenic.Name;
        //sc_jtzn.BaseData = scenic.Trafficintro;
    }
    List<ScenicImg> sclist = new List<ScenicImg>();    //绑定周边景区
    Dictionary<ScenicImg, double> scdiction = new Dictionary<ScenicImg, double>();
    const double PI = 3.1415926535;
    double CaculateDistance(double lat1, double lng1, double lat2, double lng2)
    {
        double EARTH_RADIUS = 6378.137;   // 地球半径
        double radLat1 = lat1 * PI / 180;     // 转化为弧度值
        double radLat2 = lat2 * PI / 180;
        double a = radLat1 - radLat2;
        double b = (lng1 - lng2) * PI / 180;
        double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
        s = s * EARTH_RADIUS;
        return s;
    }


    public void SortInsert(List<double> list)
    {
        for (int i = 1; i < list.Count; i++)
        {
            double t = list[i];
            int j = i;
            while ((j > 0) && (list[j - 1] < t))
            {
                list[j] = list[j - 1];
                --j;
            }
            list[j] = t;
        }
    }

    protected void imgbtnPay_Click(object sender, ImageClickEventArgs e)
    {
        //  Response.Redirect("ScenicPay.aspx?scid=" + Request.QueryString["id"] + "&count=" + txtTicketCount.Value + "&type=1");

    }


    #region 网站编辑人员编辑权限（暂时设置为网站后台管理员）
    public void EditRole()
    {
        if (CurrentUser != null && Roles.IsUserInRole(CurrentUser.UserName, "SiteAdmin"))
        {
            //sc_dp.CanEdit = true;
            foreach (RepeaterItem item in rptBookNote.Items)
            {
                ContentReader sc_dp = item.FindControl("sc_dp") as ContentReader;
                sc_dp.CanEdit = true;
            }
            foreach (RepeaterItem item in rptscInfo.Items)
            {
                ContentReader plate2 = item.FindControl("plate2") as ContentReader;
                plate2.CanEdit = true;
            }
            foreach (RepeaterItem item in rptJt.Items)
            {
                ContentReader sc_jtzn = item.FindControl("sc_jtzn") as ContentReader;
                sc_jtzn.CanEdit = true;
            }

        }
    }
    #endregion

    //判断是否为套票景区
    public bool IsPackageScenic(Scenic s)
    {
        BLLScenicTicket bllscenicticket = new BLLScenicTicket();
        if (s.Tickets.Count > 0 && bllscenicticket.GetScenicByTicket(s.Tickets[0].Id).Count > 0)
            return true;
        return false;
    }

    //绑定套票所需要的内容
    public void bindpackage(Scenic s)
    {
        List<Scenic> list = new BLLScenicTicket().GetScenicByTicket(s.Tickets[0].Id).ToList();
        bindimglist = "";
        for (int i = 0; i < list.Count; i++)
        {
            if (i == 0)
            {
                scpoint = list[0].Position;
                scmapname = list[0].Name;
            }
            else
                bindimglist += list[i].Position + "," + list[i].Name + ":";
        }
        // bindimglist.Substring(0, bindimglist.Length - 1);
        imgcount = list.Count - 1;
        introordertk.Visible = false;
    }
    protected void rpttp_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            Ticket t = e.Item.DataItem as Ticket;
            HtmlInputButton btnputcart = e.Item.FindControl("btnputcart") as HtmlInputButton;
            btnputcart.Attributes["onclick"] = "AddToCart(this," + t.Id + ")";
            //if (t.TourActivity == null)
            //{
            //    btnputcart.Attributes.Add("isActivity", "false");
            //}
            //else
            //{
            //    btnputcart.Attributes.Add("isActivity", "true");
            //}
        }
    }

    protected void rptBookNote_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            Ticket t = e.Item.DataItem as Ticket;
            ContentReader cr = e.Item.FindControl("sc_dp") as ContentReader;
            cr.BaseData = bllscenic.GetScenicById(t.Scenic.Id).BookNote;
        }
    }

    protected void rptscInfo_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            Ticket t = e.Item.DataItem as Ticket;
            ContentReader cr = e.Item.FindControl("plate2") as ContentReader;
            cr.BaseData = bllscenic.GetScenicById(t.Scenic.Id).ScenicDetail;
        }
    }

    protected void rptJt_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            Ticket t = e.Item.DataItem as Ticket;
            ContentReader cr = e.Item.FindControl("sc_jtzn") as ContentReader;
            cr.BaseData = bllscenic.GetScenicById(t.Scenic.Id).Trafficintro;
        }
    }
}