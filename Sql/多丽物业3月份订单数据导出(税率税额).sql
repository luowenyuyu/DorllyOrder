--导出费用税率和税额，主体：订单，条件：3月份，多丽物业，已审核
select
b.OrderNo as '订单编号',
CONVERT(nvarchar(7),b.OrderTime,121) as '月份',
c.CustName as '客户名称',
REPLACE(d.SRVName,'（订单）','') as '费用名称',
a.ODUnit as '单位',
a.ODQTY as '数量',
a.ODUnitPrice as '单价',
a.ODARAmount as '金额',
a.ODTaxRate as '税率',
a.ODTaxAmount as '税额'
from Op_OrderDetail a 
left join Op_OrderHeader b on a.RefRP=b.RowPointer
left join Mstr_Customer c on b.CustNo=c.CustNo
left join Mstr_Service d on a.ODSRVNo=d.SRVNo
where CONVERT(nvarchar(7),b.OrderTime,121)='2019-03'
and b.OrderStatus>1
and a.ODContractSPNo='FWC-004'
order by b.OrderNo
--select * from Op_OrderDetail