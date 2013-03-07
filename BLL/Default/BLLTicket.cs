﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
using DAL;
using Model;
using System.Web;
using Newtonsoft;
namespace BLL
{
    public class BLLTicket:BLLBase<Ticket>
    {
        DALTicket iticket;
        BLLScenic bllScenic = new BLLScenic();

        public DALTicket Iticket
        {
            get
            {
                if (iticket == null)
                {
                    iticket = new DALTicket();
                }
                return iticket;
            }
            set { iticket = value; }
        }
        /*未找到对该方法的引用.
        /// <summary>
        /// 如果不存在该景区门票 则自动创建
        /// </summary>
        public Model.Ticket EnsureTicket(int scid)
        {
            IList<Model.Ticket> tickets = Iticket.GetTicketByscId(scid);
            Model.Ticket ticket = null;
            if (tickets.Count == 0)
            {
                ticket = new TicketNormal();
                ticket.Scenic = bllScenic.GetScenicById(scid);
                SaveOrUpdateTicket(ticket);
            }
            else if (tickets.Count == 1)
            {
                ticket = tickets[0];
            }
            return ticket;
        }*/
        /// <summary>
        /// 首页展示的是 门票 而不是景区.
        /// </summary>
        /// <param name="areaid"></param>
        /// <returns></returns>
        public IList<Model.Ticket> GetTicketByAreaId(int areaid)
        {
            return Iticket.GetTicketByAreaId(areaid);
        }
        public IList<Model.Scenic> GetTicketByAreaIdAndLevel(Area area, int level, string topic, int pageIndex, int pageSize, out int totalRecord)
        {
            return Iticket.GetTicketByAreaIdAndLevel(area, level, topic, pageIndex - 1, pageSize, out totalRecord);
        }

        public IList<Model.TicketNormal> GetMainTicketByscId(int scid)
        {
            //EnsureTicket(scid);  删除，与下文重复

            return Iticket.GetMainTicketByscId(scid);
            //注释: 7行   SST.aug.8
            //IList<Model.Ticket> tickets = Iticket.GetTicketByscId(scid);
            //if (tickets.Count == 0)
            //{
            //    Model.Ticket newTicket = new TicketNormal();
            //    newTicket.Scenic = bllScenic.GetScenicById(scid);
            //    SaveOrUpdateTicket(newTicket);
            //    tickets.Add(newTicket);
            //}


            //else if (tickets.Count > 1)
            //{
            //    throw new Exception("一个景区限定为一张门票");
            //}
            //return tickets;
        }

        public IList<Model.TicketNormal> GetTicketByscId(int scid)
        {
            return Iticket.GetTicketByscId(scid);
        }

        public Ticket GetTicket(int ticketId)
        {
            return Iticket.Get(ticketId);
        }

        public Ticket GetTicketByScenicSeoName(string scenicSeoName)
        {
            return Iticket.GetByScenicSeo(scenicSeoName);
        }

        BLLTicketPrice bllTp = new BLLTicketPrice();
        public void SaveOrUpdateTicket(string ticketname, string yuan, string xf, string zx, string ticketid, string scid,string begindate,string enddate)
        {
            Model.Ticket ticket;
            if (!string.IsNullOrEmpty(ticketid))
            {
                ticket = GetTicket(int.Parse(ticketid));
                ticket.TicketPrice.Where(x => x.PriceType == PriceType.Normal).First().Price = decimal.Parse(yuan);
                ticket.TicketPrice.Where(x => x.PriceType == PriceType.PreOrder).First().Price = decimal.Parse(xf);
                ticket.TicketPrice.Where(x => x.PriceType == PriceType.PayOnline).First().Price = decimal.Parse(zx);
                ticket.Name = ticketname;
                ticket.Lock = true;
                ticket.BeginDate = DateTime.Parse(begindate);
                ticket.EndDate = DateTime.Parse(enddate);
            }
            else
            {
                ticket = new TicketNormal();
                ticket.Name = ticketname;
                ticket.Scenic = bllScenic.GetScenicById(int.Parse(scid));
                ticket.Lock = true;
                ticket.TicketPrice = new List<TicketPrice>() { 
                        new TicketPrice(){PriceType=PriceType.Normal,Price=decimal.Parse(yuan),Ticket=ticket},
                        new TicketPrice(){PriceType=PriceType.PreOrder,Price=decimal.Parse(xf),Ticket=ticket},
                        new TicketPrice(){PriceType=PriceType.PayOnline,Price=decimal.Parse(zx),Ticket=ticket}
                    };
                ticket.BeginDate = DateTime.Parse(begindate);
                ticket.EndDate = DateTime.Parse(enddate);
            }
            SaveOrUpdateTicket(ticket);
        }
        public void SaveOrUpdateTicket(Model.Ticket ticket)
        {
            //foreach (TicketPrice tp in ticket.TicketPrice)
            //{
            //    bllTp.SaveOrUpdateTicketPrice(tp);
            //}
            Iticket.SaveOrUpdateTicket(ticket);
        }
        public void SaveOrUpdateTicket(IList<Model.Ticket> tickets)
        {
            foreach (var item in tickets)
            {
                foreach (TicketPrice tp in item.TicketPrice)
                {
                    bllTp.SaveOrUpdateTicketPrice(tp);
                }
            }
        }

        public IList<Scenic> Search(string paramKey, int pageIndex, int pageSize, out int totalRecord)
        {
            return Iticket.Search(paramKey, pageIndex - 1, pageSize, out totalRecord);
        }
        /// <summary>
        /// 购物车内的门票
        /// </summary>
        /// <returns></returns>
        public IList<Ticket> GetTicketsFromCart()
        {
            List<Ticket> Tickets = new List<Ticket>();
            foreach (CartItem item in GetCartFromCookies())
            {
                OrderDetail od = new OrderDetail();
                Ticket ti = GetTicket(item.TicketId);
                Tickets.Add(ti);
            }

            return Tickets;
        }

        public IList<CartItem> GetCartFromCookies()
        {
            IList<CartItem> CartItems = new List<CartItem>();
            HttpRequest Request = HttpContext.Current.Request;
            HttpResponse Response = HttpContext.Current.Response;
            HttpServerUtility Server = HttpContext.Current.Server;
            string cookieName = "_cart";
            HttpCookie cookie = Request.Cookies[cookieName];
            if (cookie == null)
            {
                cookie = new HttpCookie(cookieName, "[]");
                Response.Cookies.Add(cookie);
                return CartItems;
            }
            string cartJson = Server.UrlDecode(cookie.Value);
            Newtonsoft.Json.Linq.JArray arrya = Newtonsoft.Json.JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(cartJson);
            if (CartItems == null)
            {
                cookie.Value = "[]";
                Response.Cookies.Add(cookie);
                return CartItems;
            }
            CartItems = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<CartItem>>(cartJson);
            return CartItems;
        }

        public IList<Model.Ticket> GetTp(int scid)
        {
            return Iticket.GetTp(scid);
        }
        public void Delete(int ticketId)
        {
            Ticket t = GetTicket(ticketId);
            Iticket.Delete(t);
        }
        /// <summary>
        /// 将 source的门票 移至 target
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public void Move(Scenic source, Scenic target)
        {

            //删除目标景区下的所有门票先!
            IList<Ticket> tobeRemoved = new List<Ticket>();
            //foreach (Ticket st in target.Tickets)
            //{

            //    Iticket.Delete(st);
            //}
         
            //foreach (Ticket removedT in tobeRemoved)
            //{
            //    Iticket.Delete(removedT);
            //}
            if (target.Tickets.Count == 0) return;
            target.Tickets.Clear();
            foreach (Ticket t in source.Tickets)
            {
                t.Scenic = target;
                target.Tickets.Add(t);
            }
         //   target.Tickets = source.Tickets;
            bllScenic.Save(target);
          //  target.Tickets.Clear();
        }
        public void Move(int mipangId, string targetScenicSEOName,out string err)
        {
            err = string.Empty;
            Scenic source=   bllScenic.GetByMipangId(mipangId);
            Scenic target = bllScenic.GetScenicBySeoName(targetScenicSEOName);

            bool isOk = true;
            if (source == null )
            {
                err +=  "mipangid不存在";
                isOk = false;
            }
            if (target == null)
            {
                err += "seoname不存在";
                isOk = false;
            }
            if (!isOk)
            {
                return;
            }
            Move(source, target);
        }
        public void BatchMove(string formatedLines,out string errMsg)
        {
            System.Text.StringBuilder sbErr = new System.Text.StringBuilder();
            string[] rowStrings = formatedLines.Split(Environment.NewLine.ToCharArray());
            foreach (string s in rowStrings)
            {
                if (string.IsNullOrEmpty(s)) continue;
                string[] pair = s.Split(';');
                if (pair.Length != 4)
                {
                    sbErr.AppendLine("格式有误:"+s);
                    continue;
                }
                int mipangId = 0;
                if (!int.TryParse(pair[1], out mipangId))
                {
                    sbErr.AppendLine("mipangid不是数字:" + s);
                    continue;
                }
                string moveResult;
                Move(mipangId, pair[3], out moveResult);
                if (!string.IsNullOrEmpty(moveResult))
                {
                    sbErr.AppendLine(moveResult + s);
                }
              
            }
            errMsg = sbErr.ToString();
        }

        public Ticket GetByProductCode(string productCode)
        {
            return Iticket.GetByProductCode(productCode);
        }
        

        public IList<Ticket> GetListByMultitTicketCode(IList<string> ticketCodes)
        { 
          return Iticket.GetListByMultitTicketCode(ticketCodes);
        }

        /// <summary>
        /// 买普通票流程
        /// </summary>
        /// <param name="member"></param>
        /// <param name="TicketId"></param>
        /// <param name="idcardno"></param>
        /// <param name="phone"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public string BuyTicket(TourMembership member, int TicketPriceId,string realName, string idcardno, string phone, int amount)
        {
            BLLMembership bllMembership = new BLLMembership();
            string returnMsg = "T";
            //身份证号码验证
            string checkIdCardNoErrMsg;
            bool idcardnoValid = CommonLibrary.StringHelper.CheckIDCard(idcardno, out checkIdCardNoErrMsg);
            if (!idcardnoValid)
            {
                return "F|" + checkIdCardNoErrMsg;
            }
            if (member == null)
            {
                member = bllMembership.GetMember(idcardno);

                if (member == null)
                {
                    //创建用户
                    member = bllMembership.CreateUser2(realName, phone, string.Empty, idcardno, idcardno, "123456", string.Empty);
                }
            }
            //生成order
            Order order = new Order();
            order.BuyTime = DateTime.Now;
            order.IsPaid = true;
            order.TourMembership = member;
            new BLLOrder().Save(order);
            //生成orderdetail
            TicketPrice tp= new BLLTicketPrice().GetOne(TicketPriceId);
            OrderDetail od = new OrderDetail(1, tp, "");
            od.Price = tp.Price;
            od.Order = order;
            new BLLOrderDetail().Save(od);
            //生成ticketAssign
            TicketAssign ta = new TicketAssign(realName, idcardno, od, amount);
            new BLLTicketAssign().Save(ta);
            return returnMsg;
        }

    }

    public class CartItem
    {
        public int TicketId;
        public int Qty;
    }
}
