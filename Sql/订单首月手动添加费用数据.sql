select top 4 * from Op_ContractRMRentList where SRVNo in('DL-DF-1','DL-SF-1','DL-GTDF','DL-GTSF');
select  * from Op_ContractRMRentList where Creator like '%罗%'
select  count(*) from Op_ContractRMRentList
--delete from Op_ContractRMRentList where Creator = '罗文瑜'
select b.RowPointer,b.ContractNo,b.ContractCreateDate,b.FeeStartDate,b.ContractCustNo,c.CustName,
a.RMID,a.UnitPrice,a.SRVNo
from Op_ContractPropertyFee a 
left join Op_Contract b on a.RefRP=b.RowPointer
left join Mstr_Customer c on b.ContractCustNo=c.CustNo
where
b.ContractStatus='2' and 
b.FeeStartDate>='2019-03-01' and 
b.FeeStartDate<'2019-03-02' and
b.ContractSPNo='FWC-004' and 
a.SRVNo in('DL-DF-1','DL-SF-1','DL-GTDF','DL-GTSF') and 
b.ContractCreateDate<'2019-03-26 09:00:00'
order by b.FeeStartDate;

--第一阶段，合同录入时间小于3月26号上午9点，合同收费时间为3月1号
insert into Op_ContractRMRentList(RowPointer,RefRP,RefNo,RMID,SRVNo,FeeStartDate,FeeEndDate,FeeQty,FeeUnitPrice,FeeAmount,FeeStatus,Creator,CreateDate,IsRefund)
select NEWID(),b.RowPointer,a.RowPointer,a.RMID,a.SRVNo,'2019-02-01','2019-02-28',0,a.UnitPrice,0,0,'罗文瑜',GETDATE(),0
from Op_ContractPropertyFee a 
left join Op_Contract b on a.RefRP=b.RowPointer
where
b.ContractStatus='2' and 
b.FeeStartDate>='2019-03-01' and 
b.FeeStartDate<'2019-03-02' and
b.ContractSPNo='FWC-004' and 
a.SRVNo in('DL-DF-1','DL-SF-1','DL-GTDF','DL-GTSF') and 
b.ContractCreateDate<'2019-03-26 09:00:00.000';

--第一批导出给财务语句
select  b.ContractNo as '合同编号',c.CustName as '客户名称'
from Op_ContractPropertyFee a 
left join Op_Contract b on a.RefRP=b.RowPointer
left join Mstr_Customer c on b.ContractCustNo=c.CustNo
where
b.ContractStatus='2' and 
b.FeeStartDate>='2019-03-01' and 
b.FeeStartDate<'2019-03-02' and
b.ContractSPNo='FWC-004' and 
a.SRVNo in('DL-DF-1','DL-SF-1','DL-GTDF','DL-GTSF') and 
b.ContractCreateDate<'2019-03-26 09:00:00.000'
group by b.ContractNo,c.CustName order by b.ContractNo;
--####################################################        第二批数据处理      ######################################################################
--第二批查询
select b.RowPointer,b.ContractNo,b.ContractCreateDate,b.FeeStartDate,b.ContractCustNo,c.CustName,
a.RMID,a.UnitPrice,a.SRVNo
from Op_ContractPropertyFee a 
left join Op_Contract b on a.RefRP=b.RowPointer
left join Mstr_Customer c on b.ContractCustNo=c.CustNo
where
b.ContractStatus='2' and 
b.FeeStartDate>='2019-03-01' and 
b.FeeStartDate<'2019-03-02' and
b.ContractSPNo='FWC-004' and 
a.SRVNo in('DL-DF-1','DL-SF-1','DL-GTDF','DL-GTSF') and 
b.ContractAuditDate<='2019-03-27 09:00:00' and
b.RowPointer not in (select RefRP from Op_ContractRMRentList where Creator='罗文瑜' group by RefRP)
order by b.FeeStartDate;
--第二批插入
insert into Op_ContractRMRentList(RowPointer,RefRP,RefNo,RMID,SRVNo,FeeStartDate,FeeEndDate,FeeQty,FeeUnitPrice,FeeAmount,FeeStatus,Creator,CreateDate,IsRefund)
select NEWID(),b.RowPointer,a.RowPointer,a.RMID,a.SRVNo,'2019-02-01','2019-02-28',0,a.UnitPrice,0,0,'罗文瑜',GETDATE(),0
from Op_ContractPropertyFee a 
left join Op_Contract b on a.RefRP=b.RowPointer
where
b.ContractStatus='2' and 
b.FeeStartDate>='2019-03-01' and 
b.FeeStartDate<'2019-03-02' and
b.ContractSPNo='FWC-004' and 
a.SRVNo in('DL-DF-1','DL-SF-1','DL-GTDF','DL-GTSF') and 
b.ContractAuditDate<='2019-03-27 09:00:00' and
b.RowPointer not in (select RefRP from Op_ContractRMRentList where Creator='罗文瑜' group by RefRP)
--第二批导出给财务语句
select  b.ContractNo as '合同编号',c.CustName as '客户名称'
from Op_ContractPropertyFee a 
left join Op_Contract b on a.RefRP=b.RowPointer
left join Mstr_Customer c on b.ContractCustNo=c.CustNo
where
b.ContractStatus='2' and 
b.FeeStartDate>='2019-03-01' and 
b.FeeStartDate<'2019-03-02' and
b.ContractSPNo='FWC-004' and 
a.SRVNo in('DL-DF-1','DL-SF-1','DL-GTDF','DL-GTSF') and 
b.ContractAuditDate<='2019-03-27 09:00:00' and
b.RowPointer not in (select RefRP from Op_ContractRMRentList where Creator='罗文瑜' group by RefRP)
group by b.ContractNo,c.CustName order by b.ContractNo;

--delete from Op_ContractRMRentList where Creator='罗文瑜' and CreateDate>='2019-03-27 09:00:00'
--####################################################        第三批数据处理      ######################################################################

--第三批插入
insert into Op_ContractRMRentList(RowPointer,RefRP,RefNo,RMID,SRVNo,FeeStartDate,FeeEndDate,FeeQty,FeeUnitPrice,FeeAmount,FeeStatus,Creator,CreateDate,IsRefund)
select NEWID(),b.RowPointer,a.RowPointer,a.RMID,a.SRVNo,'2019-02-01','2019-02-28',0,a.UnitPrice,0,0,'罗文瑜',GETDATE(),0
from Op_ContractPropertyFee a 
left join Op_Contract b on a.RefRP=b.RowPointer
where
b.ContractStatus='2' and 
b.FeeStartDate>='2019-03-01' and 
b.FeeStartDate<'2019-03-02' and
b.ContractSPNo='FWC-004' and 
a.SRVNo in('DL-DF-1','DL-SF-1','DL-GTDF','DL-GTSF') and 
b.ContractAuditDate<='2019-03-27 11:00:00' and
b.RowPointer not in (select RefRP from Op_ContractRMRentList where Creator='罗文瑜' group by RefRP)
--第三批查询
select b.RowPointer,b.ContractNo,b.ContractCreateDate,b.FeeStartDate,b.ContractCustNo,c.CustName,
a.RMID,a.UnitPrice,a.SRVNo
from Op_ContractPropertyFee a 
left join Op_Contract b on a.RefRP=b.RowPointer
left join Mstr_Customer c on b.ContractCustNo=c.CustNo
where
b.ContractStatus='2' and 
b.FeeStartDate>='2019-03-01' and 
b.FeeStartDate<'2019-03-02' and
b.ContractSPNo='FWC-004' and 
a.SRVNo in('DL-DF-1','DL-SF-1','DL-GTDF','DL-GTSF') and 
b.ContractAuditDate<='2019-03-27 11:00:00' and
b.RowPointer not in (select RefRP from Op_ContractRMRentList where Creator='罗文瑜' group by RefRP)
order by b.FeeStartDate;
--第三批导出给财务语句
select  b.ContractNo as '合同编号',c.CustName as '客户名称'
from Op_ContractPropertyFee a 
left join Op_Contract b on a.RefRP=b.RowPointer
left join Mstr_Customer c on b.ContractCustNo=c.CustNo
where
b.ContractStatus='2' and 
b.FeeStartDate>='2019-03-01' and 
b.FeeStartDate<'2019-03-02' and
b.ContractSPNo='FWC-004' and 
a.SRVNo in('DL-DF-1','DL-SF-1','DL-GTDF','DL-GTSF') and 
b.ContractAuditDate<='2019-03-27 11:00:00' and
b.RowPointer not in (select RefRP from Op_ContractRMRentList where Creator='罗文瑜' group by RefRP)
group by b.ContractNo,c.CustName order by b.ContractNo;
--####################################################        第四批数据处理      ######################################################################
--第四批插入
insert into Op_ContractRMRentList(RowPointer,RefRP,RefNo,RMID,SRVNo,FeeStartDate,FeeEndDate,FeeQty,FeeUnitPrice,FeeAmount,FeeStatus,Creator,CreateDate,IsRefund)
select NEWID(),b.RowPointer,a.RowPointer,a.RMID,a.SRVNo,'2019-03-01','2019-03-31',0,a.UnitPrice,0,0,'罗文瑜',GETDATE(),0
from Op_ContractPropertyFee a 
left join Op_Contract b on a.RefRP=b.RowPointer
where
b.FeeStartDate>='2019-04-01' and 
b.FeeStartDate<'2019-04-02' and
b.ContractNo='20190600004' and
b.ContractStatus='2' and 
b.ContractSPNo='FWC-004' and 
a.SRVNo in('DL-DF-1','DL-SF-1','DL-GTDF','DL-GTSF') and 
b.RowPointer not in (select RefRP from Op_ContractRMRentList where Creator='罗文瑜' group by RefRP)
--第四批查询
select b.RowPointer,b.ContractNo,b.ContractCreateDate,b.FeeStartDate,b.ContractCustNo,c.CustName,
a.RMID,a.UnitPrice,a.SRVNo
from Op_ContractPropertyFee a 
left join Op_Contract b on a.RefRP=b.RowPointer
left join Mstr_Customer c on b.ContractCustNo=c.CustNo
where
b.FeeStartDate>='2019-04-01' and 
b.FeeStartDate<'2019-04-02' and
b.ContractNo='20190600004' and
b.ContractStatus='2' and 
b.ContractSPNo='FWC-004' and 
a.SRVNo in('DL-DF-1','DL-SF-1','DL-GTDF','DL-GTSF') and 
b.RowPointer not in (select RefRP from Op_ContractRMRentList where Creator='罗文瑜' group by RefRP)
order by b.FeeStartDate;
--第四批导出给财务语句
select  b.ContractNo as '合同编号',c.CustName as '客户名称'
from Op_ContractPropertyFee a 
left join Op_Contract b on a.RefRP=b.RowPointer
left join Mstr_Customer c on b.ContractCustNo=c.CustNo
where
b.FeeStartDate>='2019-04-01' and 
b.FeeStartDate<'2019-04-02' and
b.ContractNo='20190600004' and
b.ContractStatus='2' and 
b.ContractSPNo='FWC-004' and 
a.SRVNo in('DL-DF-1','DL-SF-1','DL-GTDF','DL-GTSF') and 
b.RowPointer not in (select RefRP from Op_ContractRMRentList where Creator='罗文瑜' group by RefRP)
group by b.ContractNo,c.CustName order by b.ContractNo;

select  b.ContractNo as '合同编号',b.FeeStartDate as '收费开始时间', c.CustName as '客户名称'
from Op_ContractPropertyFee a 
left join Op_Contract b on a.RefRP=b.RowPointer
left join Mstr_Customer c on b.ContractCustNo=c.CustNo
where
b.ContractStatus='2' and 
b.FeeStartDate>='2019-04-01' and 
b.FeeStartDate<'2019-04-02' and
b.ContractSPNo='FWC-004' and 
a.SRVNo in('DL-DF-1','DL-SF-1','DL-GTDF','DL-GTSF') and 
b.ContractNo='20190600004' and
b.RowPointer not in (select RefRP from Op_ContractRMRentList where Creator='罗文瑜' group by RefRP)
group by b.ContractNo,c.CustName,b.FeeStartDate order by b.FeeStartDate;
