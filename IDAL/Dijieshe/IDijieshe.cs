﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDAL
{
    public interface IDJEnterprise
    {
        #region djs
        Guid AddDJS(Model.DJ_TourEnterprise djs);

        void DeleteDJS();

        void UpdateDJS();

        IList<Model.DJ_TourEnterprise> GetDJS8All();

        IList<Model.DJ_TourEnterprise> GetDJS8Muti(int areaid, string type, string id, string namelike);

        #endregion

        #region group

        Guid AddGroup(Model.DJ_TourGroup tg);

        void UpdateGroup(Model.DJ_TourGroup tg);

        Model.DJ_TourGroup GetGroup8name(string name);

        Model.DJ_TourGroup GetGroup8gid(string groupid);

        IList<Model.DJ_TourGroup> GetGroup8all();

        #endregion

        #region groupmem

        void UpdateGuide(Model.DJ_Group_Guide gg);

        void UpdateDriver(Model.DJ_Group_Driver gd);

        IList<Model.DJ_Group_Base> GetGroupmem8epid(string id);

        IList<Model.DJ_Group_Guide> GetGuide8id(string id);

        IList<Model.DJ_Group_Driver> GetDriver8id(string id);
        #endregion
    }
}