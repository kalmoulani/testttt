/*
 
 1 ������վ����Ա
  
 
 */
/****************** 1 ������վ����Ա******************/
 if not exists(select * from userrole where membername='admin' and rolename='siteadmin')
insert into userrole values(newid(), 'admin','SiteAdmin' )





/****************** 2 ����һ����վ����Ա �û���admin����123456****************/ 
insert into TourMembership([Id]
      ,[Name]
      ,RegistDate
      ,lastLogin
      ,[Password]
      ,[Openid]
      ,[Opentype]) 
      values(NEWID(),'admin',getdate(),getdate(),'E10ADC3949BA59ABBE56E057F20F883E',null,null)
select Id from TourMembership where Name='admin' and Password='E10ADC3949BA59ABBE56E057F20F883E'

