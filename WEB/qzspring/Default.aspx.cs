﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : basepage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        CanEdit();
    }

    private void CanEdit()
    {
        //if (CurrentUser != null && CurrentUser.UserName == "admin")
        //{
        //    @default.CanEdit=true;
        //}
        //else
        //{
        //    @default.CanEdit = false;
        //}
    }
 
}