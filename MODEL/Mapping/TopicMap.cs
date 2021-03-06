﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Model.Mapping
{
    public class TopicMap : ClassMap<Topic>
    {
        public TopicMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.seoname);
        }
    }
    public class ScenicTopicMap : ClassMap<ScenicTopic>
    {
        public ScenicTopicMap()
        {
            Id(x => x.Id);
            References(x => x.Scenic);
            References(x => x.Topic);
        }
    }

}
