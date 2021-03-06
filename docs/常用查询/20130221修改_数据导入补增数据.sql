
  insert into [ScenicImg] values (newid(),'',1,'lb1.jpg',29)
  insert into [ScenicImg] values (newid(),'',1,'lb2.jpg',30)
  insert into [ScenicImg] values (newid(),'',1,'lb3.jpg',31)
  insert into [ScenicImg] values (newid(),'',1,'lb4.jpg',32)
  
  update [Ticket] set scenic_id=29 where id=37
  update [Ticket] set scenic_id=24 where id=38
  update [Ticket] set scenic_id=27 where id=39
  update [Ticket] set scenic_id=28 where id=40
  update [Ticket] set scenic_id=30 where id=41
  update [Ticket] set scenic_id=23 where id=42
  update [Ticket] set scenic_id=25 where id=43
  update [Ticket] set scenic_id=31 where id=44
  update [Ticket] set scenic_id=24 where id=45
  update [Ticket] set scenic_id=22 where id=46
  update [Ticket] set scenic_id=28 where id=47
  
  --开化根雕没有图片的之后加这个
  insert into ScenicImg values ('1e02db61-256b-4671-bfc5-a01c6ebdd422','',1,'1e02db61-256b-4671-bfc5-a01c6ebdd422.jpg',2)
  insert into ScenicImg values('219168423','',1,'219168423.jpg',58)
  insert into ScenicImg values('278395848','',1,'278395848.jpg',160)
  
  --更新ticket的enable属性
  update [Ticket] set Enabled=1 where id>47
  
  --更新小图片链接
  update ScenicImg set Name='0dd64f54-c6a2-4434-a195-f51042c7775e.jpg' where Title='0dd64f54-c6a2-4434-a195-f51042c7775e'
  update ScenicImg set Name='1041893237.jpg' where Title='1041893237'
  update ScenicImg set Name='1504716053.jpg' where Title='1504716053'
  update ScenicImg set Name='1530156133.jpg' where Title='1530156133'
  update ScenicImg set Name='157910065.jpg' where Title='157910065'
  
  --确定没有金钉子
  delete FROM [tourol20130222].[dbo].[Scenic] where DJ_TourEnterprise_id=115
  delete FROM [tourol20130222].[dbo].[DJ_TourEnterprise] where Name='金钉子地质公园'