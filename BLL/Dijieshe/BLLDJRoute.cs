﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Model;
namespace BLL
{
    public class BLLDJRoute
    {

        public DALDJ_Route Idjroute = new DAL.DALDJ_Route();
        BLLDJEnterprise bllEnt = new BLLDJEnterprise();
        /// <summary>
        /// 根据预报时间得到当前的团队
        /// </summary>
        /// <param name="time">预报时间</param>
        /// <param name="teid">企业id</param>
        /// <returns>团队列表</returns>
        public IList<Model.DJ_Route> GetTgByTime(DateTime time, int teid)
        {
            List<Model.DJ_TourGroup> ListAllTg = Idjroute.GetNotendGroup().ToList();
            List<Model.DJ_Route> ListRoute = new List<Model.DJ_Route>();
            foreach (Model.DJ_TourGroup tg in ListAllTg)
            {
                foreach (Model.DJ_Route route in tg.Routes)
                {
                    if (DateTime.Parse(tg.BeginDate.AddDays(route.DayNo - 1).ToShortDateString()) <= DateTime.Parse(time.ToShortDateString()) && DateTime.Parse(tg.BeginDate.AddDays(route.DayNo - 1).ToShortDateString()) >= DateTime.Parse(DateTime.Now.ToShortDateString()) && DateTime.Parse(tg.EndDate.ToShortDateString()) >= DateTime.Parse(DateTime.Now.ToShortDateString()) && route.Enterprise.Id == teid)
                    {
                        if (new DAL.DALDJ_GroupConsumRecord().GetGroupConsumRecordByRouteId(route.Id) == null)
                            ListRoute.Add(route);
                    }
                }
            }
            return ListRoute;
        }

        public Model.DJ_Route GetById(Guid routeId)
        {
            return Idjroute.GetOne(routeId);
        }

        public IList<Model.DJ_Route> GetRouteByDayNoandGroupid(int dayno, Guid groupid, int entid)
        {
            return Idjroute.GetRouteByDayNoandGroupid(dayno, groupid, entid);
        }
        public void Save(Model.DJ_Route route)
        {
            Idjroute.Save(route);
        }
        public void SaveOrUpdate(Model.DJ_Route route)
        {
            Idjroute.SaveOrUpdate(route);
        }

        public void Delete(Model.DJ_Route route)
        {
            Idjroute.Delete(route);
        }
        public IList<Model.DJ_Route> GetRouteByAllCondition(string groupname, string EntName, string BeginTime, string EndTime, int enterid)
        {
            IList<Model.DJ_Route> ListRoute = Idjroute.GetRouteByAllCondition(groupname, EntName, BeginTime, EndTime, enterid);
            List<Model.DJ_Route> ListWroute = new List<Model.DJ_Route>();
            if (ListRoute.Count > 1)
            {
                ListWroute.Add(ListRoute[0]);
                for (int i = 1; i < ListRoute.Count; i++)
                {
                    //过滤掉行程中重复的
                    if (ListRoute[i].DJ_TourGroup.Id == ListRoute[i - 1].DJ_TourGroup.Id && (ListRoute[i - 1].DayNo - ListRoute[i].DayNo) <= 1)
                    {
                        continue;
                    }
                    else
                    {
                        ListWroute.Add(ListRoute[i]);

                    }
                }

            }
            else
                ListWroute.AddRange(ListRoute);
            //去掉验证过的
            List<DJ_Route> ListWroute2 = new List<DJ_Route>();
            foreach (DJ_Route item in ListWroute)
            {
                if (new BLLDJConsumRecord().GetGroupConsumRecordByRouteId(item.Id) == null)
                {
                    ListWroute2.Add(item);
                }
            }
            return ListWroute2;
        }

        /// <summary>
        /// 根据list 保存route.
        /// </summary>
        /// <param name="group"></param>
        /// <param name="dayNo"></param>
        /// <param name="entNames"></param>
        /// <param name="errMsg"></param>
        public void SaveFromNameList(Model.DJ_TourGroup group, int dayNo, Dictionary<EnterpriseType, IList<string>> entNames, out string errMsg)
        {
            //todo: group移除route,保存后,route的 外键会变成null...
            //解决方法:http://stackoverflow.com/questions/302720/how-to-delete-child-object-in-nhibernate
            IList<DJ_Route> dayRoutes = group.Routes.Where(x => x.DayNo == dayNo).ToList();
            foreach (DJ_Route r in dayRoutes)
            {

                group.Routes.Remove(r);
            }
            IList<DJ_Route> routes = CreateRouteFromNameList(dayNo, entNames, out errMsg).Cast<DJ_Route>().ToList();

            foreach (DJ_Route route in routes)
            {
                group.Routes.Add(route);

            }
        }

        public void CreateRouteFromMultiLineString(DJ_TourGroup group, string multiLineString, out string errMsg)
        {
            errMsg = string.Empty;
            group.Routes.Clear();
            IList<DJ_Route> allRoutes = new List<DJ_Route>();
            string[] arrSingleLine = multiLineString.Split(Environment.NewLine.ToCharArray()).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            Dictionary<EnterpriseType, IList<String>> entDicts = new Dictionary<EnterpriseType, IList<string>>();

            for (int i = 1; i <= arrSingleLine.Length; i++)
            {
                int dayNo = i;
                string currentLine = arrSingleLine[i - 1];
                string[] diffEnts = currentLine.Split(new char[] { ';', '；' });
                if (diffEnts.Length > 2 || diffEnts.Length == 0)
                {
                    errMsg = "格式错误:请用分号将景点和宾馆隔开." + currentLine;
                    return;
                }
                List<string> entNames = diffEnts[0].Split(new char[] { '\\', '＼', '、' }).Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
                entDicts.Add(EnterpriseType.景点, entNames);
                List<string> entHotels = new List<string>();
                if (diffEnts.Length == 2)
                {
                    entHotels = diffEnts[1].Split(new char[] { '\\', '＼', '、' }).Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
                    entDicts.Add(EnterpriseType.宾馆, entNames);
                }

                IList<DJ_Route> dayRoutes = CreateRouteFromNameList(dayNo, entDicts, out errMsg).Cast<DJ_Route>().ToList();
                allRoutes = allRoutes.Concat(dayRoutes).ToList();
            }
            foreach (DJ_Route r in allRoutes)
            {
                group.Routes.Add(r);
            }
            // group.Routes = allRoutes;
            // return allRoutes;


        }

        public string GenerateFormatingStringForRoutes(IList<DJ_Route> allroutes)
        {
            string sb = string.Empty;
            foreach (var routes in allroutes.OrderBy(x => x.DayNo).GroupBy(x => x.DayNo))
            {
                foreach (var item in routes)
                {
                    sb += item.Enterprise.Name + "\\";

                }
                sb = sb.TrimEnd(new char[] { ',', '，' });
                sb += Environment.NewLine;
            }
            return sb;
        }

        public IList<DJ_ProductRoute> CreateRouteFromNameList(int dayNo, Dictionary<EnterpriseType, IList<string>> entDicts, out string errMsg)
        {
            IList<DJ_ProductRoute> routes = new List<DJ_ProductRoute>();
            errMsg = string.Empty;

            foreach (EnterpriseType type in entDicts.Keys)
            {
               IList<string> entNames = entDicts[type];

                foreach (string entName in entNames)
                {
                    if (string.IsNullOrEmpty(entName)) continue;
                    DJ_TourEnterprise ent = bllEnt.GetEntByName(entName);
                    if (ent == null)
                    {
                        ///创建新企业,只有名称
                        // errMsg = "企业名称有误:" + entName;
                        ent = new DJ_TourEnterprise();
                        ent.Name = entName;
                        ent.Type = type;

                        // continue;
                    }
                    DJ_Route newRoute = new DJ_Route();
                    newRoute.DayNo = dayNo;

                    newRoute.Enterprise = ent;
                    newRoute.RD_EnterpriseName = entName;
                    routes.Add(newRoute);
                }
            }
            return routes;
        }

        /// <summary>
        /// 一个route有多少个行政单位在奖励?
        /// </summary>
        /// <param name="routes"></param>
        /// <returns></returns>

    }
}
