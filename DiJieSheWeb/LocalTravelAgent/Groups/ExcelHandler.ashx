﻿<%@ WebHandler Language="C#" Class="ExcelHandler" %>

using System;
using System.Web;
using System.Linq;

public class ExcelHandler : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        string filename=context.Request["filename"];
        
        //V.2012/10/09
        ExcelOplib.ExcelGroupOpr excel = new ExcelOplib.ExcelGroupOpr();
        string message = string.Empty;
        ExcelOplib.Entity.GroupAll ga = excel.getGroup(filename,out message);
        string djsJd = context.Request["djsid"];
        Model.DJ_DijiesheInfo djsinfo = (Model.DJ_DijiesheInfo)new BLL.BLLDJEnterprise().GetDJS8id(djsJd)[0];
            

        //v.2012/10/22
        //转化excel信息为数据库信息
        Model.DJ_TourGroup tg = new Model.DJ_TourGroup();
        //基本信息
        //tg.No = ga.GroupBasic.GroupNo;
        tg.Name = ga.GroupBasic.Name;
        tg.BeginDate = DateTime.Parse(ga.GroupBasic.Begindate);
     
        tg.DJ_DijiesheInfo = djsinfo;
        //团员信息
        var tgmlist = new System.Collections.Generic.List<Model.DJ_TourGroupMember>();
        foreach (var item in ga.GroupMemberList.Where(x=>x.Memtype=="成"))
        {
            tgmlist.Add(new Model.DJ_TourGroupMember()
            {
                RealName = item.Memname,
                IdCardNo = item.Memid,
                PhoneNum = item.Memphone,
                MemberType=Model.MemberType.成,
                DJ_TourGroup=tg,
                IsChild = false
            });
        }
        foreach (var item in ga.GroupMemberList.Where(x => x.Memtype == "儿"))
        {
            tgmlist.Add(new Model.DJ_TourGroupMember()
            {
                RealName = item.Memname,
                IdCardNo = item.Memid,
                PhoneNum = item.Memphone,
                IsChild = true,
                MemberType = Model.MemberType.儿,
                DJ_TourGroup = tg,
                Keeper = item.Cardno
            });
        }
        foreach (var item in ga.GroupMemberList.Where(x => x.Memtype == "港澳台"))
        {
            tgmlist.Add(new Model.DJ_TourGroupMember()
            {
                RealName = item.Memname,
                IdCardNo = item.Memid,
                PhoneNum = item.Memphone,
                MemberType = Model.MemberType.港澳台,
                DJ_TourGroup = tg,
                IsChild = false
            });
        }
        foreach (var item in ga.GroupMemberList.Where(x => x.Memtype == "外"))
        {
            tgmlist.Add(new Model.DJ_TourGroupMember()
            {
                RealName = item.Memname,
                IdCardNo = item.Memid,
                PhoneNum = item.Memphone,
                IsChild = true,
                Keeper = item.Cardno,
                MemberType = Model.MemberType.外,
            
                DJ_TourGroup=tg
            });
        }
     
        tg.Members = tgmlist;
        
        //行程信息
        var routes = new System.Collections.Generic.List<Model.DJ_Route>();
        BLL.BLLDJEnterprise bllDJS = new BLL.BLLDJEnterprise();
        for (int i = 0; i < ga.GroupRouteList.Count; i++)
        {
            var hotels=ga.GroupRouteList[i].Hotel.Split(new char[]{'-',',','，',' '},StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in hotels)
            {
                routes.Add(new Model.DJ_Route() { 
                    DayNo=i+1,
                    DJ_TourGroup=tg,
                    Description="住宿",
                    Enterprise=bllDJS.GetDJS8name(item)[0]
                });
            }

            var scenics = ga.GroupRouteList[i].Scenic.Split(new char[] { '-', ',', '，', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in scenics)
            {
                routes.Add(new Model.DJ_Route()
                {
                    DayNo = i + 1,
                    DJ_TourGroup = tg,
                    Description = "景点",
                    Enterprise = bllDJS.GetDJS8name(item)[0]
                });
            }
        }
        tg.Routes = routes;
        
        
        DAL.DALDJEnterprise djEnterprice = new DAL.DALDJEnterprise();
        //汇总信息
        string guidid=string.Empty;
        string result = djEnterprice.AddGroup(tg,out guidid);
        context.Response.Write(result+":"+guidid);

        #region v.2012/10/9
        //string html = string.Empty;
        //if (groupall.GroupBasic == null ||
        //    groupall.GroupMemberList == null || groupall.GroupMemberList.Count < 1 ||
        //    groupall.GroupRouteList == null || groupall.GroupRouteList.Count < 1)
        //{
        //    return;
        //}

        //html += @"{""Name"":""" +  groupall.GroupBasic.Name
        //    + @""",""Bedate"":""" + DateTime.Parse(groupall.GroupBasic.Begindate).ToShortDateString() + "-" + DateTime.Parse(groupall.GroupBasic.Enddate).ToShortDateString()
        //    + @""",""Days"":""" +  groupall.GroupBasic.Days
        //    + @""",""PeopleTotal"":""" + groupall.GroupBasic.PeopleTotal
        //    + @""",""PeopleAdult"":""" + groupall.GroupBasic.PeopleAdult
        //    + @""",""PeopleChild"":""" + groupall.GroupBasic.PeopleChild
        //    + @""",""StartPlace"":""" + groupall.GroupBasic.StartPlace
        //    + @""",""EndPlace"":""" + groupall.GroupBasic.EndPlace
        //    + @""",""Foreigners"":""" + groupall.GroupBasic.Foreigners
        //    + @""",""GroupNo"":""" + groupall.GroupBasic.GroupNo
        //    + @""",""Gangaotais"":""" + groupall.GroupBasic.Gangaotais;

        //html += @""",""Member"":""";
        //foreach (ExcelOplib.Entity.GroupMember item in groupall.GroupMemberList)
        //{
        //    html += @"<tr><td>" + item.Memtype +
        //        "</td><td>" + item.Memname +
        //        "</td><td>" + item.Memid +
        //        "</td><td>" + item.Memphone +
        //        "</td><td>" + item.Cardno +
        //        "</td></tr>";
        //}

        //html += @""",""Route"":""";
        //foreach (ExcelOplib.Entity.GroupRoute item in groupall.GroupRouteList)
        //{
        //    html += @"<tr><td>" + item.RouteDate +
        //        "</td><td>" + item.City +
        //        "</td><td>" + item.Breakfast +
        //        "</td><td>" + item.Lunch +
        //        "</td><td>" + item.Dinner +
        //        "</td><td>" + item.Hotel1
        //                                 + (item.Hotel2 == string.Empty ? "" : "-" + item.Hotel2) +
        //        "</td><td>" + item.Scenic1 
        //                                 + (item.Scenic2 == string.Empty ? "" : "-" + item.Scenic2)
        //                                 + (item.Scenic3 == string.Empty ? "" : "-" + item.Scenic3)
        //                                 + (item.Scenic4 == string.Empty ? "" : "-" + item.Scenic4)
        //                                 + (item.Scenic5 == string.Empty ? "" : "-" + item.Scenic5)
        //                                 + (item.Scenic6 == string.Empty ? "" : "-" + item.Scenic6)
        //                                 + (item.Scenic7 == string.Empty ? "" : "-" + item.Scenic7)
        //                                 + (item.Scenic8 == string.Empty ? "" : "-" + item.Scenic8)
        //                                 + (item.Scenic9 == string.Empty ? "" : "-" + item.Scenic9)
        //                                 + (item.Scenic10 == string.Empty ? "" : "-" + item.Scenic10) +
        //        "</td><td>" + item.ShoppingPoint1
        //                                 + (item.ShoppingPoint2 == string.Empty ? "" : "-" + item.ShoppingPoint2)
        //                                 + (item.ShoppingPoint3 == string.Empty ? "" : "-" + item.ShoppingPoint3)+ "</td></tr>";
        //}
        //html += @"""}";
        //context.Response.Write(html);
        #endregion
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}