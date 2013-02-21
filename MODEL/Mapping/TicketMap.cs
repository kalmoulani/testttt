﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Model.Mapping
{
    public class TicketMap : ClassMap<Ticket>
    {
        public TicketMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.ProductCode);
            Map(x => x.Enabled);
            HasMany<TicketPrice>(x => x.TicketPrice).Cascade.All();
            References<DJ_TourEnterprise>(x => x.Scenic);
            Map(x => x.IsMain);
            Map(x => x.Lock);
            Map(x => x.BeginDate);
            Map(x => x.EndDate);
            References<TicketUnion>(x => x.TicketUnion);
            //References<TicketsType>(x => x.TicketsType);
            // References<TicketPrice>(x => x.TicketPrice);
            Map(x => x.Remark);
            References<TourActivity>(x => x.TourActivity);
        }
    }
}
