﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace DAL
{
    public class DALDJEnterprise : DalBase<Model.DJ_TourEnterprise>
    {
        #region DJS

       
        public void DeleteDJS()
        {
            throw new NotImplementedException();
        }

        public void UpdateDJS(Model.DJ_TourEnterprise obj)
        {
            using (var x=session.Transaction)
            {
                x.Begin();
                session.Update(obj);
                x.Commit();
            }
            
        }

        public IList<Model.DJ_TourEnterprise> GetDJS8All()
        {
            string sql = "select D from DJ_TourEnterprise D";
            IQuery query = session.CreateQuery(sql);
            return query.Future<Model.DJ_TourEnterprise>().ToList<Model.DJ_TourEnterprise>();
        }

        /// <summary>
        /// 多条件查询企业
        /// </summary>
        /// <param name="areaid"></param>
        /// <param name="type">景点 = 1,饭店 = 2,宾馆 = 4,购物点 = 8,旅行社 = 16</param>
        /// <param name="id"></param>
        /// <param name="namelike"></param>
        /// <returns></returns>
        public IList<Model.DJ_TourEnterprise> GetDJS8Muti(int areaid, string type, string id, string namelike)
        {
            string sql = "select D from DJ_TourEnterprise D where ";
            if (areaid != 0)
            {
                sql += " D.Area.Id=" + areaid + " and";
            }
            if (!string.IsNullOrEmpty(type))
            {
                sql += " D.Type=" + (int)(Model.EnterpriseType)Enum.Parse(typeof(Model.EnterpriseType), type) + " and";
            }
            if (!string.IsNullOrEmpty(id))
            {
                sql += " D.Id='" + id + "' and";
            }
            if (!string.IsNullOrEmpty(namelike))
            {
                sql += " D.Name = '" + namelike + "' and";
            }
            sql = sql.Substring(0, sql.Length - 3);
            IQuery query = session.CreateQuery(sql);
            return query.Future<Model.DJ_TourEnterprise>().ToList<Model.DJ_TourEnterprise>();
        }

        /// <summary>
        /// 多个区域内的旅游企业
        /// </summary>
        ///      <param name="areaIds">辖区对应的areaid, 用逗号隔开</param>
        /// <param name="where"></param>
        /// <returns></returns>
        public IList<Model.DJ_TourEnterprise> GetDJSInAreas(string areaids)
        {
            string where = " and  D.Area.Id in ( " + areaids + ")";

            return GetDJS8Multi(where);

        }

        public IList<Model.DJ_TourEnterprise> GetEnterpriseWithoutScenic(string areaIds)
        {
            return GetEnterpriseList(areaIds, false,null, null);

        }
        public IList<Model.DJ_TourEnterprise> GetEnterpriseList(string areaIds,bool includeScenic,bool? includeDJS, bool? isRecomm)
        {
            string where = string.Empty;
            if (!string.IsNullOrEmpty(areaIds))
            {
                where += " and D.Area.Id in (" + areaIds + ")";
            }
            if (!includeScenic)
            {
                where += " and D.Type<>" + (int)Model.EnterpriseType.景点;
            }
            if (includeDJS != null)
            {
                where += " and D.Type<>" + (int)Model.EnterpriseType.旅行社;
            }
            if (isRecomm!=null)
            {
                where += " and D.IsVeryfied='"+isRecomm+"'";
            }
            return GetDJS8Multi(where);

        }

        public IList<Model.DJ_TourEnterprise> GetDJS8Multi(string where)
        {
            string sql = "select D from DJ_TourEnterprise D where D.Type<>0 ";
            sql = sql + where;
            IQuery query = session.CreateQuery(sql);
            return query.List<Model.DJ_TourEnterprise>();
        }
        DALArea dalArea = new DALArea();
      /// <summary>
      /// 
      /// </summary>
      /// <param name="name">名称</param>
      /// <param name="isNameLike">是否根据名称模糊查询</param>
      /// <param name="areacode"></param>
      /// <param name="type"></param>
      /// <param name="rewardType"></param>
      /// <param name="pageIndex"></param>
      /// <param name="pageSize"></param>
      /// <param name="totalRecord"></param>
      /// <returns></returns>
        public IList<Model.DJ_TourEnterprise> GetListPaged(string name,bool isNameLike, string areacode, Model.EnterpriseType? type, Model.RewardType? rewardType,
            int pageIndex,int pageSize,out int totalRecord
            )
        {
            string sql = "select D from DJ_TourEnterprise D where 1=1 ";

            Model.Area govArea = dalArea.GetAreaByCode(areacode);
            string areaIds = dalArea.GetSubAreaIds(areacode);

            if (!string.IsNullOrEmpty(areaIds))
            {

                sql += " and  D.Area.Id in (" + areaIds + ")";
            }

            if (!string.IsNullOrEmpty(name))
            {
                if (isNameLike)
                {
                    sql += " and D.Name like '%" + name + "%'";
                }
                else
                {
                    sql += " and D.Name = '" + name + "'";
                }
            }

            if (type.HasValue&&type!=0)
            {
                string typeInts = string.Empty;
                if ((type.Value & Model.EnterpriseType.宾馆) == Model.EnterpriseType.宾馆)
                {
                    typeInts += (int)Model.EnterpriseType.宾馆 + ",";
                }
                if ((type.Value & Model.EnterpriseType.饭店) == Model.EnterpriseType.饭店)
                {
                    typeInts += (int)Model.EnterpriseType.饭店 + ",";
                }
                if ((type.Value & Model.EnterpriseType.购物点) == Model.EnterpriseType.购物点)
                {
                    typeInts += (int)Model.EnterpriseType.购物点 + ",";
                }
                if ((type.Value & Model.EnterpriseType.景点) == Model.EnterpriseType.景点)
                {
                    typeInts += (int)Model.EnterpriseType.景点 + ",";
                }
                if ((type.Value & Model.EnterpriseType.旅行社) == Model.EnterpriseType.旅行社)
                {
                    typeInts += (int)Model.EnterpriseType.旅行社 + ",";
                }
                typeInts = typeInts.TrimEnd(',');
                sql += " and  D.Type in (" + typeInts + ")";
            }


            if (rewardType.HasValue&&rewardType!=0)
            {
                string rewardPropertyName = string.Empty;
                switch (govArea.Level)
                {
                    case Model.AreaLevel.市: rewardPropertyName = "CityVeryfyState"; break;
                    case Model.AreaLevel.区县: rewardPropertyName = "CountryVeryfyState"; break;
                    case Model.AreaLevel.省: rewardPropertyName = "ProvinceVeryfyState"; break;
                }

                string rewardTypeInts = string.Empty;
                if ((rewardType.Value & Model.RewardType.从未纳入) == Model.RewardType.从未纳入)
                {
                    rewardTypeInts += (int)Model.RewardType.从未纳入 + ",";
                }
                if ((rewardType.Value & Model.RewardType.纳入后移除) == Model.RewardType.纳入后移除)
                {
                    rewardTypeInts += (int)Model.RewardType.纳入后移除 + ",";
                }
                if ((rewardType.Value & Model.RewardType.已纳入) == Model.RewardType.已纳入)
                {
                    rewardTypeInts += (int)Model.RewardType.已纳入 + ",";
                }
                rewardTypeInts = rewardTypeInts.TrimEnd(',');
                if (!string.IsNullOrEmpty(rewardTypeInts))
                {
                    sql += " and D." + rewardPropertyName + " in (" + rewardTypeInts + ")";
                }
            }
            return GetList(sql, pageIndex, pageSize, out totalRecord);
        }
        public IList<Model.DJ_TourEnterprise> GetList(string areacode, Model.EnterpriseType? type, Model.RewardType? rewardType)
        {
            return GetList(string.Empty,false, areacode, type, rewardType);
        }

        public IList<Model.DJ_TourEnterprise> GetListByNameLike(string nameLike)
        {
            var entlist = session.QueryOver<Model.DJ_TourEnterprise>().Where(x => x.Name.Contains(nameLike)).List();
            var unionTicketList=session.QueryOver<Model.TicketUnion>().Where(x=>x.TicketList.Where(y=>y.Scenic.Name.Contains(nameLike)).Count()>0).List();
            foreach (Model.TicketUnion ticket  in unionTicketList)
            {
                entlist.Add(ticket.Scenic);
            }
            return entlist;
            
        }

        //精确名称查找
        public IList<Model.DJ_TourEnterprise> GetList(string name,  string areacode, Model.EnterpriseType? type, Model.RewardType? rewardType

              )
        {
            return GetList(name, false, areacode, type, rewardType);
        }
        public IList<Model.DJ_TourEnterprise> GetList(string name,bool isNameLike,string areacode, Model.EnterpriseType? type, Model.RewardType? rewardType

              )
        {
            int totalRecords;
            return GetListPaged(name, isNameLike, areacode, type, rewardType, 1, 99999, out totalRecords);
        }

        #endregion

        #region group

        public string AddGroup(Model.DJ_TourGroup tg,out string id)
        {
            id = string.Empty;
            //todo: 团队编号已经删除.
            Model.DJ_TourGroup tg_old = GetGroup8no(string.Empty);
            if (null != tg_old)//已经有该编号的团队
            {
                //如果还没开始就删除团队
                if (tg_old.BeginDate <= DateTime.Now)
                {
                    return "保存失败，与该编号相同的团队已开始行程！";
                }
                //开始了的团队无法删除.
                else
                {
                    using (var t1 = session.BeginTransaction())
                    {
                        session.Delete(tg_old);
                        t1.Commit();
                    }
                }
            }
            using (var t2= session.BeginTransaction())
            {
                foreach (var item in tg.Members)
                {
                    session.Save(item);
                }
                //foreach (var item in tg.Workers)
                //{
                //    session.Save(item);
                //}
                foreach (var item in tg.Vehicles)
                {
                    session.Save(item);
                }
                foreach (var item in tg.Routes)
                {
                    session.Save(item);
                }
                session.Save(tg);
                id = tg.Id.ToString();
                t2.Commit();
            }
            return "保存成功";
        }

        public void UpdateGroup(Model.DJ_TourGroup tg)
        {
            using (var t = session.BeginTransaction())
            {
                foreach (var item in tg.Members)
                {
                    session.Save(item);
                }
                session.Update(tg);
                t.Commit();
            }
        }

        public Model.DJ_TourGroup GetGroup8no(string no)
        {
            string sql = "select G from DJ_TourGroup G where G.No='" + no + "'";
            IQuery query = session.CreateQuery(sql);
            return query.FutureValue<Model.DJ_TourGroup>().Value;
        }

        public Model.DJ_TourGroup GetGroup8name(string name)
        {
            string sql = "select G from DJ_TourGroup G where G.Name='" + name + "'";
            IQuery query = session.CreateQuery(sql);
            return query.FutureValue<Model.DJ_TourGroup>().Value;
        }

        public Model.DJ_TourGroup GetGroup8gid(string groupid)
        {
            string sql = "select G from DJ_TourGroup G where G.Id='" + groupid + "'";
            IQuery query = session.CreateQuery(sql);
            return query.FutureValue<Model.DJ_TourGroup>().Value;
        }

        public IList<Model.DJ_TourGroup> GetGroup8all()
        {
            string sql = "select G from DJ_TourGroup G";
            IQuery query = session.CreateQuery(sql);
            return query.Future<Model.DJ_TourGroup>().ToList<Model.DJ_TourGroup>();
        }
        #endregion

        #region groupmem

        public void UpdateGuide(Model.DJ_Group_Worker gg)
        {
            using (var t = session.BeginTransaction())
            {
                session.Update(gg);
                t.Commit();
            }
        }

        public void UpdateDriver(Model.DJ_Group_Worker gd)
        {
            using (var t = session.BeginTransaction())
            {
                session.Update(gd);
                t.Commit();
            }
        }

        public IList<Model.DJ_Group_Worker> GetGroupmem8epid(string id)
        {
            string sql = "select G from DJ_Group_Base G where TourEnterprise.Id='" + id + "'";
            IQuery query = session.CreateQuery(sql);
            return query.Future<Model.DJ_Group_Worker>().ToList<Model.DJ_Group_Worker>();
        }


        public IList<Model.DJ_Group_Worker> GetGuide8id(string id)
        {
            string sql = "select G from DJ_Group_Worker G where Id='" + id + "'";
            IQuery query = session.CreateQuery(sql);
            return query.Future<Model.DJ_Group_Worker>().ToList();
        }

        public IList<Model.DJ_Group_Worker> GetDriver8id(string id)
        {
            string sql = "select G from DJ_Group_Worker G where Id='" + id + "'";
            IQuery query = session.CreateQuery(sql);
            return query.Future<Model.DJ_Group_Worker>().ToList();
        }

        #endregion

    }
}
