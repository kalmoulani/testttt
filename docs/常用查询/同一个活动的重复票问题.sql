/*
δ���
��ȡһ��idcard��ĳ�λ�л�ȡ����Ʊ��
*/
select COUNT(ta.IdCard) as c,ta.IdCard 
,act.ActivityCode 
from TicketAssign ta,OrderDetail detail
,TicketPrice tp,Ticket t,TourActivity act
where ta.OrderDetail_id=detail.Id and detail.TicketPrice_id=tp.Id and tp.Ticket_id=t.Id
and t.TourActivity_id=act.Id group by act.ActivityCode,ta.IdCard
order by c desc


