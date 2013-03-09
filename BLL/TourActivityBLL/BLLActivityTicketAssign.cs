﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;
using System.Data;

namespace BLL
{
    public class BLLActivityTicketAssign : BLLBase<ActivityTicketAssign>
    {
        DALActivityTicketAssign dALActivityTicketAssign;
        public DAL.DALActivityTicketAssign DALActivityTicketAssign
        {
            get
            {
                if (dALActivityTicketAssign == null)
                {
                    dALActivityTicketAssign = new DALActivityTicketAssign();
                }
                return dALActivityTicketAssign;
            }
            set
            {
                dALActivityTicketAssign = value;
            }
        }

        public ActivityTicketAssign GetOneByQuery(string activityCode, string partnerCode, string ticketCode, DateTime date)
        {
            date = date.Date;
            return DALActivityTicketAssign.GetOneByQuery(activityCode, partnerCode, ticketCode, date);
        }
        /// <summary>
        /// 所有的getlist 的查询都重载此方法.
        /// </summary>
        /// <param name="activityCode"></param>
        /// <param name="partnerCode"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public IList<ActivityTicketAssign> GetList(string activityCode, string partnerCode, DateTime date)
        {
            date = date.Date;
            IList<ActivityTicketAssign> assignList = DALActivityTicketAssign.GetList(activityCode, partnerCode, date);
            return assignList;
        }

        public DataTable GetDTbyLvTicket(Guid actId)
        {
            return DALActivityTicketAssign.GetDTbyLvTicket(actId);
        }
        public DataTable GetDtBycheckTicket(Guid actId)
        {
            return DALActivityTicketAssign.GetDtBycheckTicket(actId);
        }
    }
}
