﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Model;
using DAL;
/// <summary>
///sigmagrid xhr请求对象
/// </summary>
namespace BLL
{ 
public class SigmaGridRequestObject : DalBase
{

    /*
     _gt_json:
     * {"fieldsName":["no","tourertype","realname","phone","idcardno","othercardno","memberhid"],
     * "recordType":"array",
     * "parameters":{},
     * "action":"save",
     * "insertedRecords":[],
     * "updatedRecords":[["1","成人游客","11111","13282151877","520822198010103916",""]],"deletedRecords":[]}
     */
    public string[] fieldsName { get; set; }
    public string recordType { get; set; }
    public ParametersObject parameters { get; set; }
    public string action { get; set; }
    public IList<RecordObject> insertedRecords { get; set; }
    public IList<RecordObject> updatedRecords { get; set; }
    public IList<RecordObject> deletedRecords { get; set; }


    public string Act()
    {
        string returnValue = string.Empty;
        Guid groupId;
        string paramGroupId = parameters.groupid;
        if (!Guid.TryParse(paramGroupId, out groupId))
        {
            BLL.ErrHandler.Redirect(BLL.ErrType.ParamIllegal);
        }
       DJ_TourGroup group = new BLL.BLLDJTourGroup().GetTourGroupById(groupId);
        if (action == "save")
        {
            //delete/update/insert
            IList<Model.DJ_TourGroupMember> memberList = new List<Model.DJ_TourGroupMember>();

            foreach (RecordObject ro in insertedRecords)
            {

                Model.DJ_TourGroupMember member = ConvertToMember(ro);

            session.Save(member); session.Flush();
            }
            foreach (RecordObject ro in updatedRecords)
            {

                Model.DJ_TourGroupMember member = ConvertToMember(ro);

                session.Update(member); session.Flush();
            } foreach (RecordObject ro in deletedRecords)
            {

                Model.DJ_TourGroupMember member = ConvertToMember(ro);

                session.Delete(member); session.Flush();
            }
           
        }

        if (action == "load")
        {
           // return bll BuildLoadResponse(group.Members);
        }
        return returnValue;
    }

    
    public Model.DJ_TourGroupMember ConvertToMember(RecordObject ro)
    {
        
        Model.DJ_TourGroupMember member = new DJ_TourGroupMember();
        member.IdCardNo = ro.idcardno;
        member.PhoneNum = ro.phone;
        member.RealName = ro.realname;
        member.TouristType = ro.tourertype;
        string memberId = ro.memberhid;
        if (!string.IsNullOrEmpty(memberId))
        {
            member.Id = Guid.Parse(memberId);
        }
        //  member.
        return member;



    }
    /*
        
		{ name: 'no' },
		{ name: 'tourertype' },
		{ name: 'realname' },
		{ name: 'phone' },
		{ name: 'idcardno' },
		{ name: 'othercardno' }  
     * { name: 'memberid' },
		
     */
  
}

 public class ParametersObject
{
     public string groupid { get; set; }
}
 public class RecordObject
 {
     /*["no","tourertype","realname","phone","idcardno","othercardno","memberhid"],*/
     public string no { get; set; }
     public string tourertype { get; set; }
     public string realname { get; set; }
     public string phone { get; set; }
     public string idcardno { get; set; }
     public string othercardno { get; set; }
     public string memberhid { get; set; }
 }

/// <summary>
/// sigmagrid load方法返回的对象
/// 
/// </summary>
public class SigmaGridLoadObject
{
    /*{"data":
        *  [
        *      {"no":52,"name":"abc40","age":25,"gender":"br","english":88,"math":76}
        *      ,{"no":53,"name":"abc33","age":25,"gender":"br","english":98,"math":99}
        *      ,{"no":54,"name":"abc34","age":24,"gender":"us","english":23,"math":77}
        *      ,{"no":55,"name":"abc35","age":23,"gender":"fr","english":67,"math":55}
        *   ],
        *   "pageInfo":
        *      {"pageSize":10,"pageNum":3,"totalRowNum":25,"totalPageNum":3,"startRowNum":21,"endRowNum":25},
        *   "exception":null
        * }*/
    // public 
}

}