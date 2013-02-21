﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    ///门票的基类, 统一 单一门票 和 套票
    public abstract class Ticket
    {
        

        public virtual int Id { get; set; }
      
        public virtual string DisplayNameOfOwner
        {
            get {
                if (As<Ticket>() is TicketUnion)
                {
                    return Name;
                }
                else
                {
                    return Scenic.Name;
                }
            }
           
        }
        /// <summary>
        /// 该门票的拥有者
        /// </summary>
        public virtual DJ_TourEnterprise Scenic { get; set; }
        public virtual string Name { get; set; }
        public virtual string ProductCode { get; set; }
       
        public virtual bool IsMain { get; set; }
        public virtual bool Lock { get; set; }
        public virtual DateTime BeginDate { get; set; }
        public virtual decimal OrderNumber { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        public virtual DateTime EndDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
        /// <summary>
        /// 是否启用(页面显示,后台判断)
        /// </summary>
        public virtual bool Enabled { get; set; }
        //价格列表(每种价格的每种列表)
        public virtual IList<TicketPrice> TicketPrice { get; set; }
        //参与的活动
        public virtual TourActivity TourActivity { get; set; }
        //属于那张套票
        public virtual TicketUnion TicketUnion { get; set; }
        public abstract bool IsBelongTo(Scenic s);
        public abstract decimal GetPrice(PriceType priceType);
        /// <summary>
        /// 获取这个票价
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual TicketPrice GetTicketPrice(PriceType type)
        {
            var tp = TicketPrice.Where<TicketPrice>(x => x.PriceType == type).FirstOrDefault();
            if (tp == null) return null;
            else return tp;
        }
        public virtual T As<T>() where T : Ticket
        {
            return this as T;
        }
    }
}
