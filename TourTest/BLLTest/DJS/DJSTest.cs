﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace TourTest.BLLTest
{
    [TestFixture]
    public class DJSTest
    {
        BLL.BLLDJEnterprise blldjs = new BLL.BLLDJEnterprise();
        BLL.BLLArea bllarea = new BLL.BLLArea();

        [Test]
        public void AddDjsTest()
        {
            blldjs.AddDjs("新疆牙买提", "新疆乌鲁木齐", bllarea.GetAreaByCode("330100"), "阿凡提", "15988886666", "010-156489765","","",null);
        }
        [Test]
        public void GetDijieshe()
        {
            Model.DJ_DijiesheInfo dijieshe=(Model.DJ_DijiesheInfo) blldjs.GetDJS8id("438")[0];
            

        }
    }
}
