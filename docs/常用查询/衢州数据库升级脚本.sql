/*
a ��Ʊ�����޸�
----------------------------------------------------------------�����Լ����
 1 ������λ ��������λ���� һ�� activitylist
 -- 1) ����һ��� �õ�@id

-- 2) �����к����̲��뵽activitypartner��
*/
declare @activityid varchar(100)
select @activityid='159477B2-04C7-48AD-BB02-A15A011CF16A'
insert into ActivityTicketAssign 
select 
NEWID() as id
,pta.AsignedAmount as assignedamount
,ta.Date as dateassign
,ta.SoldAmount
,ap.Id as Partner_Id
,ta.Ticket_id,@activityid
 from QZTicketAsign ta
 ,QZPartnerTicketAsign pta
 ,ActivityPartner ap,
 QZSpringPartner sp
where pta.QZTicketAsign_id=ta.Id
 and ap.PartnerCode=sp.FriendlyId
  and pta.Partner_id=sp.Id

select * from QZSpringPartner 
select * from QZPartnerTicketAsign
select * from QZTicketAsign
select * from ActivityTicketAssign
select * from ActivityPartner

--------------------------------------------------------------------����:�޸�onlycontrolamount

/*
2 ��Ʊ��ʽ�޸�

*/
 insert into TicketNormal (Ticket_id,MipangId)
 select Id,MipangId from Ticket
 alter table ticket drop column mipangid
update Ticket set Enabled=1
 go
 
 /*3
 ��ticketassign����� ticketcode����һ��
 */
 
 update TicketAssign   
 set TicketCode=t.ProductCode
 --select t.ProductCode,ta.*
 from TicketAssign ta, OrderDetail detail,TicketPrice tp,Ticket t
 where ta.OrderDetail_id=detail.Id and detail.TicketPrice_id=tp.Id
 and tp.Ticket_id=t.Id


