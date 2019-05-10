--抄表记录
select * from Op_Readout where RMID='FTYQ-1-02-004-1/418' and ROCreateDate>'2019-02-24';
--合同引用记录
select * from Op_ContractRMRentList_Readout where 
RefReadoutRP in (select RowPointer from Op_Readout where RMID='FTYQ-1-02-004-1/418' and ROCreateDate>'2019-02-24');
--租金清单引用记录
select * from Op_ContractRMRentList where RowPointer in
(
select RefRentRP from Op_ContractRMRentList_Readout where 
RefReadoutRP in (select RowPointer from Op_Readout where RMID='FTYQ-1-02-004-1/418' and ROCreateDate>'2019-02-24')
);
--订单详情引用记录
select * from Op_OrderDetail where RefNo in
(
select RowPointer from Op_ContractRMRentList where RowPointer in
(
select RefRentRP from Op_ContractRMRentList_Readout where 
RefReadoutRP in (select RowPointer from Op_Readout where RMID='FTYQ-1-02-004-1/418' and ROCreateDate>'2019-02-24')
));
--订单引用记录
select * from Op_OrderHeader where RowPointer in
(
select * from Op_OrderDetail where RefNo in
(
select RowPointer from Op_ContractRMRentList where RowPointer in
(
select RefRentRP from Op_ContractRMRentList_Readout where 
RefReadoutRP in (select RowPointer from Op_Readout where RMID='FTYQ-1-02-004-1/418' and ROCreateDate>'2019-02-24')
)));