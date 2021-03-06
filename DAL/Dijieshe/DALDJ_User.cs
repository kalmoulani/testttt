﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace DAL
{
    public class DALDJ_User:DalBase<Model.DJ_User_TourEnterprise>
    {
        #region User_TourEnterprise
        /// <summary>
        /// 根据enterprise 获取 user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.DJ_User_TourEnterprise GetUser_TEbyId(int id)
        {
            string sql = "select u from DJ_User_TourEnterprise u where u.Enterprise.Id='" + id + "'";
            IQuery query = session.CreateQuery(sql);
            return query.FutureValue<Model.DJ_User_TourEnterprise>().Value;
        }
        public Model.DJ_User_TourEnterprise GetUser_TEbyName(string name)
        {
           return GetOneByQuery("select u from DJ_User_TourEnterprise u where u.Name='" + name + "'");
        }

        public IList<Model.DJ_User_TourEnterprise> GetUser_TEbyId(int id,int permis)
        {
            string sql = "select u from DJ_User_TourEnterprise u where u.Enterprise.Id='" + id + "' and PermissionType=" + permis + "";
            IQuery query = session.CreateQuery(sql);
            return query.Future<Model.DJ_User_TourEnterprise>().ToList < Model.DJ_User_TourEnterprise>();
        }
        #endregion


        public Model.DJ_User_TourEnterprise GetByMemberId(Guid id)
        {
            string sql = "select u from DJ_User_TourEnterprise u where u.Id='" + id + "'";
            IQuery query = session.CreateQuery(sql);
            return query.FutureValue<Model.DJ_User_TourEnterprise>().Value;
        }

        public IList<Model.DJ_User_Gov> GetGov_UserBygovIdAndPermis(Guid govid, int permis)
        {
            string sql = "select u from DJ_User_Gov u where u.GovDpt.Id='" + govid + "'";
            if (permis == 0)
            {
                sql += " u.PermissionType=" + permis + "";
            }
            IQuery query = session.CreateQuery(sql);
            return query.Future<Model.DJ_User_Gov>().ToList<Model.DJ_User_Gov>();
        }



        public IList<Model.DJ_User_Gov> GetAllGov_User()
        {
            string sql = "select u from DJ_User_Gov u";
            IQuery query = session.CreateQuery(sql);
            return query.Future<Model.DJ_User_Gov>().ToList<Model.DJ_User_Gov>();
        }


        public void DeleteGov_User(Model.TourMembership m)
        {
            using (var x=session.Transaction)
            {
                x.Begin();
                session.Delete(m);
                x.Commit();
            }
        }


        public Model.DJ_User_Gov GetGov_UserById(Guid id)
        {
            string sql = "select u from DJ_User_Gov u where u.Id='" + id + "'";
            IQuery query = session.CreateQuery(sql);
            return query.FutureValue<Model.DJ_User_Gov>().Value;
        }

        public Model.DJ_User_Gov GetGov_UserByName(string seoname)
        {
            string sql = "select u from DJ_User_Gov u where u.Name='" + seoname + "'";
            IQuery query = session.CreateQuery(sql);
            return query.FutureValue<Model.DJ_User_Gov>().Value;
        }



        public void SaveOrUpdate(Model.TourMembership m)
        {
            using (var x=session.Transaction)
            {
                x.Begin();
                session.SaveOrUpdate(m);
                x.Commit();
            }
        }


        public IList<Model.DJ_User_TourEnterprise> GetAllEnt_User()
        {
            string sql = "select u from DJ_User_TourEnterprise u ";
            IQuery query = session.CreateQuery(sql);
            return query.Future<Model.DJ_User_TourEnterprise>().ToList<Model.DJ_User_TourEnterprise>();
        }
    }
}
