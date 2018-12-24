<%@ Page Language="C#" AutoEventWireup="true" Inherits="project.Presentation.Op.ContractRefund_WPPT,project"  %>
<!DOCTYPE>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="head1" runat="server">
    <title>工位租赁合同退租</title>    
    <!--[if lt IE 9]>
    <script type="text/javascript" src="../../jscript/html5.js"></script>
    <script type="text/javascript" src="../../jscript/respond.min.js"></script>
    <![endif]-->
    <link href="../../css/H-ui.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/H-ui.admin.css" rel="stylesheet" type="text/css" />
    <link href="../../lib/iconfont/iconfont.css" rel="stylesheet" type="text/css" />
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/JsInput.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function transmitData(submitData) {
            var data = submitData;
            <%=ClientScript.GetCallbackEventReference(this, "data", "BandResuleData", null) %>
        }
    </script>
</head>
<body>
    <form id="form1" runat="server"></form>
    <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 退租管理 <span class="c-gray en">&gt;</span> 工位服务合同退租 <a class="btn btn-success radius r mr-20" style="line-height:1.6em;margin-top:2px" href="javascript:location.replace(location.href);" title="刷新" ><i class="Hui-iconfont">&#xe68f;</i></a></nav>
    <div id="list" class="pt-5 pr-20 pb-5 pl-20">
	    <div class="cl pd-3 bg-1 bk-gray mt-2"> 
            <span class="l">
                <%--<a href="javascript:;" onclick="view()" class="btn btn-secondary radius"><i class="Hui-iconfont">&#xe627;</i> 查看</a>
                <a href="javascript:;" onclick="viewfee()" class="btn btn-secondary radius"><i class="Hui-iconfont">&#xe627;</i> 查看租金明细</a>&nbsp;&nbsp;&nbsp;
                <a href="javascript:;" onclick="applyrefund()" class="btn btn-warning radius"><i class="Hui-iconfont">&#xe6e0;</i> 预约退租</a>
                <a href="javascript:;" onclick="refund()" class="btn btn-danger radius"><i class="Hui-iconfont">&#xe631;</i> 确认退租</a>--%>
                <%=Buttons %>
                <input type="hidden" id="selectKey" />
            </span> 
	    </div>
	    <div class="cl pd-10  bk-gray mt-2"> 
		    合同编号&nbsp;<input type="text" class="input-text size-MINI" placeholder="合同编号" id="ContractNoS" style="width:110px">            
            手工合同编号&nbsp;<input type="text" class="input-text size-MINI" placeholder="手工合同编号" id="ContractNoManualS" style="width:110px">
            <%--合同类型&nbsp;<%=ContractTypeStrS %>--%>
            服务商&nbsp;<%=ContractSPNoStrS %>
            客户&nbsp;<input type="text" class="input-text size-MINI" placeholder="" id="ContractCustNoS" style="width:110px">
            退租状态&nbsp;<select class="input-text size-MINI" style="width:110px" id="OffLeaseStatusS"><option value="" selected="selected">全部</option><option value="1">未退租</option><option value="2">已申请</option><option value="3">已办理</option><option value="4">已结算</option></select>
            <br />
            合同签订日期 从&nbsp;<input type="text" class="input-text size-MINI" id="MinContractSignedDate" style="width:110px">
            至&nbsp;<input type="text" class="input-text size-MINI" id="MaxContractSignedDate" style="width:110px">            
            合同到期日期 从&nbsp;<input type="text" class="input-text size-MINI" id="MinContractEndDate" style="width:110px">
            至&nbsp;<input type="text" class="input-text size-MINI" id="MaxContractEndDate" style="width:110px">
            实际退租日期 从&nbsp;<input type="text" class="input-text size-MINI" id="MinOffLeaseActulDate" style="width:110px">
            至&nbsp;<input type="text" class="input-text size-MINI" id="MaxOffLeaseActulDate" style="width:110px">
		    <button type="submit" class="btn btn-success radius" onclick="select()"><i class="Hui-iconfont">&#xe665;</i> 检索</button>
	    </div>
	    <div class="mt-5" id="outerlist">
	    <%=list %>
	    </div>
    </div>
    <div id="edit" class="editdiv" style="display:none;">        
        <div class="itab">
  	        <ul> 
                <li><a href="javascript:void(0)" onclick="show(1)" id="itemtab1" class="selected">基本信息</a></li> 
                <li><a href="javascript:void(0)" onclick="show(3)" id="itemtab3">工位信息</a></li>
  	        </ul>
        </div>

        <div id="topeditdiv">
            <table class="tabedit">
                <tr>
                    <td class="tdl">合同类型</td>
                    <td class="tdr"><%=ContractTypeStr %></td>
                    <td class="tdl">客户</td>
                    <td class="tdr">
                        <input type="text" class="input-text size-MINI" id="ContractCustName" />
                        <input type="hidden" id="ContractCustNo" />
                        <img id="ContractCustImg" alt="" src="../../images/view_detail.png" class="view_detail" />
                    </td>
                    <td class="tdl">合同主体</td>
                    <td class="tdr"><%=ContractSPNoStr %></td>
                    <td class="tdl"></td>
                    <td class="tdr"></td>
                </tr>
                <tr>
                    <td class="tdl">合同编号</td>
                    <td class="tdr"><input type="text" id="ContractNo" disabled="disabled" class="input-text size-MINI" /></td>
                    <td class="tdl">手工合同编号</td>
                    <td class="tdr"><input type="text" id="ContractNoManual" class="input-text size-MINI" /></td>
                    <td class="tdl">经办人</td>
                    <td class="tdr"><input type="text" id="ContractHandler" disabled="disabled" class="input-text size-MINI" /></td>
                    <td class="tdl">合同状态</td>
                    <td class="tdr"><input type="text" id="ContractStatus" disabled="disabled" class="input-text size-MINI" /></td>
                </tr>
                <tr>
                    <td class="tdl">合同签订日期</td>
                    <td class="tdr"><input type="text" id="ContractSignedDate" class="input-text size-MINI" /></td>
                    <td class="tdl">生效日期</td>
                    <td class="tdr"><input type="text" id="ContractStartDate" class="input-text size-MINI" /></td>
                    <td class="tdl">到期日期</td>
                    <td class="tdr"><input type="text" id="ContractEndDate" class="input-text size-MINI" /></td>
                    <td class="tdl">客户入场日期</td>
                    <td class="tdr"><input type="text" id="EntryDate"class="input-text size-MINI" /></td>
                </tr>
                <tr>
                    <td class="tdl">租金起收日期</td>
                    <td class="tdr"><input type="text" id="FeeStartDate" class="input-text size-MINI" /></td>
                    <td class="tdl">减免开始日期</td>
                    <td class="tdr"><input type="text" id="ReduceStartDate" class="input-text size-MINI" /></td>
                    <td class="tdl">减免截止日期</td>
                    <td class="tdr"><input type="text" id="ReduceEndDate" class="input-text size-MINI" /></td>
                    <td class="tdl">滞纳金占比</td>
                    <td class="tdr"><input type="text" id="ContractLatefeeRate"class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                </tr>
                <tr style="display:none;">
                    <td class="tdl">房屋押金</td>
                    <td class="tdr"><input type="text" id="RMRentalDeposit" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl">房屋水电押金</td>
                    <td class="tdr"><input type="text" id="RMUtilityDeposit" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl">广告位数量</td>
                    <td class="tdr"><input type="text" id="BBQTY" class="input-text size-MINI" disabled="disabled" onchange="validInt(this.id)" /></td>
                    <td class="tdl">广告位合同总金额</td>
                    <td class="tdr"><input type="text" id="BBAmount" class="input-text size-MINI" disabled="disabled" onchange="validDecimal(this.id)" /></td>
                </tr>
                <tr style="display:none;">
                    <td class="tdl">管理费起收日期</td>
                    <td class="tdr"><input type="text" id="PropertyFeeStartDate" class="input-text size-MINI" /></td>
                    <td class="tdl">管理费减免开始日期</td>
                    <td class="tdr"><input type="text" id="PropertyFeeReduceStartDate" class="input-text size-MINI" /></td>
                    <td class="tdl">管理费减免结束日期</td>
                    <td class="tdr"><input type="text" id="PropertyFeeReduceEndDate" class="input-text size-MINI" /></td>
                    <td class="tdl"></td>
                    <td class="tdr"></td>
                </tr>
                <tr style="display:none;">
                    <td class="tdl">水费单价</td>
                    <td class="tdr"><input type="text" id="WaterUnitPrice" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl">电费单价</td>
                    <td class="tdr"><input type="text" id="ElecticityUintPrice" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl">空调费单价</td>
                    <td class="tdr"><input type="text" id="AirconUnitPrice" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl">管理费单价</td>
                    <td class="tdr"><input type="text" id="PropertyUnitPrice" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                </tr>
                <tr style="display:none;">
                    <td class="tdl">公摊水费</td>
                    <td class="tdr"><input type="text" id="SharedWaterFee" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl">公摊电费</td>
                    <td class="tdr"><input type="text" id="SharedElectricyFee" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl"></td>
                    <td class="tdr"></td>
                    <td class="tdl">工位电费押金</td>
                    <td class="tdr"><input type="text" id="WPUtilityDeposit" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                </tr>
                <tr>
                    <td class="tdl">工位数量</td>
                    <td class="tdr"><input type="text" id="WPQTY" class="input-text size-MINI" onchange="validInt(this.id)" /></td>
                    <td class="tdl">工位用电额度</td>
                    <td class="tdr"><input type="text" id="WPElectricyLimit" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl">超额用电单价</td>
                    <td class="tdr"><input type="text" id="WPOverElectricyPrice" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl">工位押金</td>
                    <td class="tdr"><input type="text" id="WPRentalDeposit" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                </tr>
                <tr>
                    <td class="tdl">递增开始时间1</td>
                    <td class="tdr"><input type="text" id="IncreaseStartDate1" class="input-text size-MINI" /></td>
                    <td class="tdl">递增率1</td>
                    <td class="tdr"><input type="text" id="IncreaseRate1" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl">递增开始时间2</td>
                    <td class="tdr"><input type="text" id="IncreaseStartDate2" class="input-text size-MINI" /></td>
                    <td class="tdl">递增率2</td>
                    <td class="tdr"><input type="text" id="IncreaseRate2" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                </tr>
                <tr>
                    <td class="tdl">递增开始时间3</td>
                    <td class="tdr"><input type="text" id="IncreaseStartDate3" class="input-text size-MINI" /></td>
                    <td class="tdl">递增率3</td>
                    <td class="tdr"><input type="text" id="IncreaseRate3" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl">递增开始时间4</td>
                    <td class="tdr"><input type="text" id="IncreaseStartDate4" class="input-text size-MINI" /></td>
                    <td class="tdl">递增率4</td>
                    <td class="tdr"><input type="text" id="IncreaseRate4" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                </tr>
                <tr>
                    <td class="tdl">备注</td>
                    <td class="tdr" colspan="7"><textarea cols="" rows="3" class="textarea required" id="Remark"></textarea></td>
                </tr>
                <tr>
                    <td class="tdl">附件</td>
                    <td class="tdr"><button type="button" class="btn btn-primary radius" id="uploadFiles" style="margin-left:20px;">上传附件</button></td>
                    <td colspan="6"><div id="ContractAttachmentFiles"></div><input type="hidden" id="ContractAttachment" /></td>
                </tr>
            </table>
        </div>

        <div id="bodyeditdiv2">  
            <table class="tabedit">
                <tr>
                    <td class="tdl1">房屋编号</td>
                    <td class="tdr1">
                        <input type="text" class="input-text size-MINI" id="RMID2" disabled="disabled" style="width:150px;" />
                    </td>
                    <td class="tdl1">工位号</td>
                    <td class="tdr1">
                        <input type="text" id="WPNo" class="input-text size-MINI" disabled="disabled" style="width:150px;" />
                    </td>
                    <td class="tdl1">所属费用项目</td>
                    <td class="tdr1"><%=SRVNo2Str %></td>
                </tr>
                <tr>
                    <td class="tdl1">工位数</td>
                    <td class="tdr1"><input type="text" id="WPQTY2" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl1">单价</td>
                    <td class="tdr1"><input type="text" id="WPRentalUnitPrice" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl1"></td>
                    <td class="tdr1"></td>
                </tr>
                <tr>
                    <td class="tdl1">工位位置</td>
                    <td colspan="5"><input type="text" id="RMLoc2" class="input-text size-MINI" /></td>
                </tr>
                <tr>
                    <td class="tdl1">备注</td>
                    <td colspan="5"><input type="text" id="Remark2" class="input-text size-MINI"  /></td>
                </tr>
                <tr>
                    <td colspan="6">
                        <input class="btn btn-primary radius" type="button" id="itemsave2" onclick="itemsave2()" value="保存" />&nbsp;
	                    <input class="btn btn-primary radius" type="button" id="itemclear2" onclick="itemclear2()" value="清空" />&nbsp;
                    </td>
                </tr>
            </table>
            <div style="width:100%;height:5px;"></div>
            <div id="itemlist2"style="width:1058px; height:320px; overflow:auto; margin:0px; padding:0px;"></div>
        </div>    
        
        <div style="margin-top:10px;">
            <input class="btn btn-primary radius" type="button" id="submit" onclick="submit()" value="保存退出" />
            <input class="btn btn-primary radius" type="button" id="submit1" onclick="submit1()" value="保存继续" />
	        <input class="btn btn-default radius" type="button" id="cancel" onclick="cancel()" value="&nbsp;&nbsp;取&nbsp;&nbsp;消&nbsp;&nbsp;" />
        </div>
        <br />
        <br />
    </div>        

    <div id="applyrefunddiv" style="display:none;">
        <table style="width:460px; margin-top:10px;">
            <tr>
                <td style="width:100px; text-align:center; padding:15px;">申请退租日期</td>
                <td style="width:360px;"><input type="text" id="OffLeaseApplyDate" class="input-text size-MINI" style="width:200px;"  disabled="disabled"/></td>
            </tr>
            <tr>
                <td style="width:100px; text-align:center; padding:15px;">预约退租日期</td>
                <td style="width:360px;"><input type="text" id="OffLeaseScheduleDate" class="input-text size-MINI" style="width:200px;" /></td>
            </tr>
            <tr>
                <td style="width:100px; text-align:center; padding:15px;">退租原因</td>
                <td style="width:360px;"><textarea cols="" rows="3" class="textarea required" id="OffLeaseReason"></textarea></td>
            </tr>
        </table>
        <div style="margin:15px 100px;">
            <input class="btn btn-primary radius" type="button" onclick="applyrefundsubmit()" value="&nbsp;确&nbsp;定&nbsp;" />&nbsp;&nbsp;
            <input class="btn btn-primary radius" type="button" onclick="applyrefundcancel()" value="&nbsp;关&nbsp;闭&nbsp;" />
        </div>
    </div> 
    <div id="refunddatediv" style="display: none;">
        <table style="width: 320px; margin-top: 30px; margin-bottom: 20px;">
            <tr>
                <td style="width: 100px; text-align: center; padding: 10px;">实际退租日期</td>
                <td style="width: 250px;">
                    <input type="text" id="ActuallyLeaveDate" class="input-text size-MINI" /></td>
            </tr>
        </table>
        <div style="margin: 15px 100px;">
            <input class="btn btn-primary radius" type="button" onclick="refunddatesubmit()" value="&nbsp;确定&nbsp;" />&nbsp;&nbsp;
            <input class="btn btn-primary radius" type="button" onclick="refunddatecancel()" value="&nbsp;取消&nbsp;" />
        </div>
    </div>
    <script type="text/javascript" src="../../jscript/jquery-1.10.2.js"></script> 
    <script type="text/javascript" src="../../jscript/script_ajax.js"></script>
    <script type="text/javascript" src="../../jscript/script_common.js"></script>
    <script type="text/javascript" src="../../jscript/JsInputDate.js"></script>
    <script type="text/javascript" src="../../jscript/json2.js"></script>
    <script type="text/javascript" src="../../jscript/H-ui.js"></script> 
    <script type="text/javascript" src="../../jscript/H-ui.admin.js"></script> 
    <script type="text/javascript" src="../../lib/layer/layer.js"></script> 
    <script type="text/javascript">
        function BandResuleData(temp) {
            var vjson = JSON.parse(temp);
            if (vjson.type == "select") {
                if (vjson.flag == "1") {
                    $("#outerlist").html(vjson.liststr);
                    $("#selectKey").val("");
                    page = 1;
                    reflist();
                }
            }
            if (vjson.type == "jump") {
                if (vjson.flag == "1") {
                    $("#outerlist").html(vjson.liststr);
                    $("#selectKey").val("");
                    reflist();
                }
            }
            if (vjson.type == "cancelrefund") {
                if (vjson.flag == "1") {
                    $("#outerlist").html(vjson.liststr);
                    $("#selectKey").val("");
                    reflist();
                }
                else if (vjson.flag == "3") {
                    layer.alert("当前状态未预约，不能取消预约！");
                }
                else {
                    layer.alert("提取数据出错！");
                }
                return;
            }
            if (vjson.type == "applyrefundsubmit") {
                if (vjson.flag == "1") {
                    $("#outerlist").html(vjson.liststr);
                    $("#selectKey").val("");
                    reflist();

                    layer.closeAll();
                }
                else if (vjson.flag == "1") {
                    layer.alert("当前状态不允许预约退租！");
                }
                else {
                    layer.alert("提取数据出错！");
                }
                return;
            }
            if (vjson.type == "refundsubmit") {
                if (vjson.flag == "1") {
                    $("#outerlist").html(vjson.liststr);
                    $("#selectKey").val("");
                    reflist();

                    layer.closeAll();
                    layer.alert("退租成功！");
                    console.log(vjson.sync);
                }
                else if (vjson.flag == "2") {
                    layer.alert("退租时间有误！");
                }
                else if (vjson.flag == "3") {
                    layer.alert("当前状态不允许退租！");
                }
                else if (vjson.flag == "4") {
                    layer.alert(vjson.InfoBar);
                }
                else {
                    layer.alert("提取数据出错！");
                }
                return;
            }
            if (vjson.type == "view") {
                if (vjson.flag == "1") {
                    $("#ContractNo").val(vjson.ContractNo);
                    $("#ContractType").val(vjson.ContractType);
                    $("#ContractSPNo").val(vjson.ContractSPNo);
                    $("#ContractCustNo").val(vjson.ContractCustNo);
                    $("#ContractCustName").val(vjson.ContractCustName);
                    $("#ContractNoManual").val(vjson.ContractNoManual);
                    $("#ContractHandler").val(vjson.ContractHandler);
                    $("#ContractSignedDate").val(vjson.ContractSignedDate);
                    $("#ContractStartDate").val(vjson.ContractStartDate);
                    $("#ContractEndDate").val(vjson.ContractEndDate);
                    $("#EntryDate").val(vjson.EntryDate);
                    $("#FeeStartDate").val(vjson.FeeStartDate);
                    $("#ReduceStartDate").val(vjson.ReduceStartDate);
                    $("#ReduceEndDate").val(vjson.ReduceEndDate);
                    $("#ContractLatefeeRate").val(vjson.ContractLatefeeRate);
                    $("#RMRentalDeposit").val(vjson.RMRentalDeposit);
                    $("#RMUtilityDeposit").val(vjson.RMUtilityDeposit);
                    $("#PropertyFeeStartDate").val(vjson.PropertyFeeStartDate);
                    $("#PropertyFeeReduceStartDate").val(vjson.PropertyFeeReduceStartDate);
                    $("#PropertyFeeReduceEndDate").val(vjson.PropertyFeeReduceEndDate);
                    $("#WaterUnitPrice").val(vjson.WaterUnitPrice);
                    $("#ElecticityUintPrice").val(vjson.ElecticityUintPrice);
                    $("#AirconUnitPrice").val(vjson.AirconUnitPrice);
                    $("#PropertyUnitPrice").val(vjson.PropertyUnitPrice);
                    $("#SharedWaterFee").val(vjson.SharedWaterFee);
                    $("#SharedElectricyFee").val(vjson.SharedElectricyFee);
                    $("#WPRentalDeposit").val(vjson.WPRentalDeposit);
                    $("#WPUtilityDeposit").val(vjson.WPUtilityDeposit);
                    $("#WPQTY").val(vjson.WPQTY);
                    $("#WPElectricyLimit").val(vjson.WPElectricyLimit);
                    $("#WPOverElectricyPrice").val(vjson.WPOverElectricyPrice);
                    $("#BBQTY").val(vjson.BBQTY);
                    $("#BBAmount").val(vjson.BBAmount);
                    $("#IncreaseStartDate1").val(vjson.IncreaseStartDate1);
                    $("#IncreaseRate1").val(vjson.IncreaseRate1);
                    $("#IncreaseStartDate2").val(vjson.IncreaseStartDate2);
                    $("#IncreaseRate2").val(vjson.IncreaseRate2);
                    $("#IncreaseStartDate3").val(vjson.IncreaseStartDate3);
                    $("#IncreaseRate3").val(vjson.IncreaseRate3);
                    $("#IncreaseStartDate4").val(vjson.IncreaseStartDate4);
                    $("#IncreaseRate4").val(vjson.IncreaseRate4);
                    $("#ContractStatus").val(vjson.ContractStatus);
                    $("#Remark").val(vjson.Remark);

                    $("#ContractAttachment").val(vjson.ContractAttachment);
                    $("#ContractAttachmentFiles").html(vjson.files);

                    $("#uploadFiles").css("display", "none");

                    copyid = "";
                    show(1);
                    itemclear2();
                    $("#itemlist2").html(vjson.itemlist2);

                    //$("#ContractNo").attr("disabled", true);
                    $("#list").css("display", "none");
                    $("#edit").css("display", "");

                    $("#itemsave2").css("display", "none");
                    $("#itemclear2").css("display", "none");
                    $("#submit").css("display", "none");
                    $("#submit1").css("display", "none");
                }
                else {
                    layer.alert("数据操作出错！");
                }
                return;
            }
            if (vjson.type == "viewfee") {
                if (vjson.flag == "1") {
                    layer.open({
                        type: 1,
                        area: ["900px", "600px"],
                        fix: true,
                        maxmin: true,
                        scrollbar: false,
                        shade: 0.5,
                        title: "费用清单",
                        content: vjson.feelist
                    });
                }
                else {
                    layer.alert("提取数据出错！");
                }
                return;
            }
            if (vjson.type == "check") {
                if (vjson.flag == "1") {
                    $("#" + vjson.id).val(vjson.Code);
                    //$("#" + vjson.id + "Name").val(vjson.Name);
                }
                else if (vjson.flag == "3") {
                    layer.alert("未找到记录，确认是否输入正确！");
                }
                else {
                    layer.alert("提取数据出错！");
                }
                return;
            }


            if (vjson.type == "itemupdate2") {
                if (vjson.flag == "1") {
                    $("#RMID2").val(vjson.RMID);
                    $("#WPNo").val(vjson.WPNo);
                    $("#SRVNo2").val(vjson.SRVNo);
                    $("#WPQTY2").val(vjson.WPQTY);
                    $("#WPRentalUnitPrice").val(vjson.WPRentalUnitPrice);
                    $("#RMLoc2").val(vjson.RMLoc);
                    $("#Remark2").val(vjson.Remark);

                    itemid2 = vjson.ItemId;
                    itemtp2 = "update";
                }
                else {
                    layer.alert("修改数据出错！");
                }
                return;
            }
        }


        function applyrefund() {
            if ($("#selectKey").val() == "") {
                layer.msg("请先选择一条记录", { icon: 3, time: 1000 });
                return;
            }
            $("#OffLeaseApplyDate").val("<%=date %>");
            $("#OffLeaseScheduleDate").val("");
            $("#OffLeaseReason").val("");
            layer.open({
                type: 1,
                area: ["500px", "300px"],
                fix: true,
                maxmin: true,
                scrollbar: false,
                shade: 0.5,
                title: "预约退租",
                content: $("#applyrefunddiv")
            });
            return;
        }
        function cancelrefund() {
            if ($("#selectKey").val() == "") {
                layer.msg("请先选择一条记录", { icon: 3, time: 1000 });
                return;
            }
            layer.confirm('确认要取消预约申请吗？', function (index) {
                var submitData = new Object();
                submitData.Type = "cancelrefund";
                submitData.id = $("#selectKey").val();

                submitData.ContractNoS = $("#ContractNoS").val();
                submitData.ContractNoManualS = $("#ContractNoManualS").val();
                submitData.ContractTypeS = "12";
                submitData.ContractSPNoS = $("#ContractSPNoS").val();
                submitData.ContractCustNoS = $("#ContractCustNoS").val();
                submitData.MinContractSignedDate = $("#MinContractSignedDate").val();
                submitData.MaxContractSignedDate = $("#MaxContractSignedDate").val();
                submitData.MinContractEndDate = $("#MinContractEndDate").val();
                submitData.MaxContractEndDate = $("#MaxContractEndDate").val();
                submitData.OffLeaseStatusS = $("#OffLeaseStatusS").val();
                submitData.MinOffLeaseActulDate = $("#MinOffLeaseActulDate").val();
                submitData.MaxOffLeaseActulDate = $("#MaxOffLeaseActulDate").val();
                submitData.page = page;
                transmitData(datatostr(submitData));
                layer.close(index);
            });
            return;
        }
        function applyrefundsubmit() {
            if ($("#OffLeaseScheduleDate").val() == "") {
                layer.msg("请选择预约退租日期！", { icon: 7, time: 1000 });
                $("#OffLeaseScheduleDate").focus();
                return;
            }
            var submitData = new Object();
            submitData.Type = "applyrefundsubmit";
            submitData.id = $("#selectKey").val();
            submitData.OffLeaseScheduleDate = $("#OffLeaseScheduleDate").val();
            submitData.OffLeaseReason = $("#OffLeaseReason").val();

            submitData.ContractNoS = $("#ContractNoS").val();
            submitData.ContractNoManualS = $("#ContractNoManualS").val();
            submitData.ContractTypeS = "12";
            submitData.ContractSPNoS = $("#ContractSPNoS").val();
            submitData.ContractCustNoS = $("#ContractCustNoS").val();
            submitData.MinContractSignedDate = $("#MinContractSignedDate").val();
            submitData.MaxContractSignedDate = $("#MaxContractSignedDate").val();
            submitData.MinContractEndDate = $("#MinContractEndDate").val();
            submitData.MaxContractEndDate = $("#MaxContractEndDate").val();
            submitData.OffLeaseStatusS = $("#OffLeaseStatusS").val();
            submitData.MinOffLeaseActulDate = $("#MinOffLeaseActulDate").val();
            submitData.MaxOffLeaseActulDate = $("#MaxOffLeaseActulDate").val();
            submitData.page = page;
            transmitData(datatostr(submitData));
            return;
        }
        function applyrefundcancel() {
            layer.closeAll();
        }
        function refund() {
            if ($("#selectKey").val() == "") {
                layer.msg("请先选择一条记录", { icon: 3, time: 1000 });
                return;
            }
            $("#ActuallyLeaveDate").val("");
            layer.open({
                type: 1,
                area: ["400px", "200px"],
                fix: true,
                maxmin: true,
                scrollbar: false,
                shade: 0.5,
                title: "选择退租日期",
                content: $("#refunddatediv")
            });
        }
        function refunddatesubmit() {
            if ($("#ActuallyLeaveDate").val() == "") {
                layer.msg("请选择退租日期", 7, 1000);
                $("#ActuallyLeaveDate").focus();
                return;
            }
            var submitData = new Object();
            submitData.Type = "refundsubmit";
            submitData.id = $("#selectKey").val();
            submitData.RefundDate = $("#ActuallyLeaveDate").val();
            //检索条件数据
            submitData.ContractNoS = $("#ContractNoS").val();
            submitData.ContractNoManualS = $("#ContractNoManualS").val();
            submitData.ContractTypeS = "12";
            submitData.ContractSPNoS = $("#ContractSPNoS").val();
            submitData.ContractCustNoS = $("#ContractCustNoS").val();
            submitData.MinContractSignedDate = $("#MinContractSignedDate").val();
            submitData.MaxContractSignedDate = $("#MaxContractSignedDate").val();
            submitData.MinContractEndDate = $("#MinContractEndDate").val();
            submitData.MaxContractEndDate = $("#MaxContractEndDate").val();
            submitData.OffLeaseStatusS = $("#OffLeaseStatusS").val();
            submitData.MinOffLeaseActulDate = $("#MinOffLeaseActulDate").val();
            submitData.MaxOffLeaseActulDate = $("#MaxOffLeaseActulDate").val();
            submitData.page = page;
            transmitData(datatostr(submitData));
            return;
        }
        function refunddatecancel() {
            layer.closeAll();
        }
        function view() {
            if ($("#selectKey").val() == "") {
                layer.msg("请先选择一条记录", { icon: 3, time: 1000 });
                return;
            }
            id = $("#selectKey").val();
            var submitData = new Object();
            submitData.Type = "view";
            submitData.id = id;

            transmitData(datatostr(submitData));
            return;
        }
        function viewfee() {
            if ($("#selectKey").val() == "") {
                layer.msg("请先选择一条记录", { icon: 3, time: 1000 });
                return;
            }
            id = $("#selectKey").val();
            var submitData = new Object();
            submitData.Type = "viewfee";
            submitData.id = id;

            transmitData(datatostr(submitData));
            return;
        }
        function select() {
            var submitData = new Object();
            submitData.Type = "select";
            submitData.ContractNoS = $("#ContractNoS").val();
            submitData.ContractNoManualS = $("#ContractNoManualS").val();
            submitData.ContractTypeS = "12";
            submitData.ContractSPNoS = $("#ContractSPNoS").val();
            submitData.ContractCustNoS = $("#ContractCustNoS").val();
            submitData.MinContractSignedDate = $("#MinContractSignedDate").val();
            submitData.MaxContractSignedDate = $("#MaxContractSignedDate").val();
            submitData.MinContractEndDate = $("#MinContractEndDate").val();
            submitData.MaxContractEndDate = $("#MaxContractEndDate").val();
            submitData.OffLeaseStatusS = $("#OffLeaseStatusS").val();
            submitData.MinOffLeaseActulDate = $("#MinOffLeaseActulDate").val();
            submitData.MaxOffLeaseActulDate = $("#MaxOffLeaseActulDate").val();
            submitData.page = 1;
            transmitData(datatostr(submitData));
            return;
        }
        function cancel() {
            id = "";
            $("#list").css("display", "");
            $("#edit").css("display", "none");
            return;
        }


        function itemupdate2(itemid) {
            var submitData = new Object();
            submitData.Type = "itemupdate2";
            submitData.itemid = itemid;
            transmitData(datatostr(submitData));
            return;
        }
        function itemclear2() {
            itemtp2 = "insert";
            itemid2 = "";

            $("#RMID2").val("");
            $("#WPNo").val("");
            $("#SRVNo2").val("");
            $("#WPQTY2").val("");
            $("#WPRentalUnitPrice").val("");
            $("#RMLoc2").val("");
            $("#Remark2").val("");
        }

        var page = 1;
        function jump(pageindex) {
            page = pageindex;
            var submitData = new Object();
            submitData.Type = "jump";
            submitData.ContractNoS = $("#ContractNoS").val();
            submitData.ContractNoManualS = $("#ContractNoManualS").val();
            submitData.ContractTypeS = "12";
            submitData.ContractSPNoS = $("#ContractSPNoS").val();
            submitData.ContractCustNoS = $("#ContractCustNoS").val();
            submitData.MinContractSignedDate = $("#MinContractSignedDate").val();
            submitData.MaxContractSignedDate = $("#MaxContractSignedDate").val();
            submitData.MinContractEndDate = $("#MinContractEndDate").val();
            submitData.MaxContractEndDate = $("#MaxContractEndDate").val();
            submitData.OffLeaseStatusS = $("#OffLeaseStatusS").val();
            submitData.MinOffLeaseActulDate = $("#MinOffLeaseActulDate").val();
            submitData.MaxOffLeaseActulDate = $("#MaxOffLeaseActulDate").val();
            submitData.page = page;
            transmitData(datatostr(submitData));
            return;
        }

        function show(page) {
            if (page == 1) {
                $('#itemtab3').removeClass("selected");
                $("#bodyeditdiv2").hide();

                $('#itemtab1').addClass("selected");
                $("#topeditdiv").show();
            }
            else {
                if (id == "") {
                    layer.msg("请先保存表头信息！", { icon: 3, time: 1000 });
                    return;
                }

                $('#itemtab1').removeClass("selected");
                $('#itemtab3').removeClass("selected");
                $("#topeditdiv").hide();
                $("#bodyeditdiv2").hide();

                if (page == "3") {
                    $('#itemtab3').addClass("selected");
                    $("#bodyeditdiv2").show();
                }
            }
        }
        

        jQuery(function () {
            var ContractSignedDate = new JsInputDate("ContractSignedDate");
            ContractSignedDate.setDisabled(false);
            ContractSignedDate.setWidth("130px");

            var ContractStartDate = new JsInputDate("ContractStartDate");
            ContractStartDate.setDisabled(false);
            ContractStartDate.setWidth("130px");

            var ContractEndDate = new JsInputDate("ContractEndDate");
            ContractEndDate.setDisabled(false);
            ContractEndDate.setWidth("130px");

            var EntryDate = new JsInputDate("EntryDate");
            EntryDate.setDisabled(false);
            EntryDate.setWidth("130px");

            var FeeStartDate = new JsInputDate("FeeStartDate");
            FeeStartDate.setDisabled(false);
            FeeStartDate.setWidth("130px");
            var ReduceStartDate = new JsInputDate("ReduceStartDate");
            ReduceStartDate.setDisabled(false);
            ReduceStartDate.setWidth("130px");
            var ReduceEndDate = new JsInputDate("ReduceEndDate");
            ReduceEndDate.setDisabled(false);
            ReduceEndDate.setWidth("130px");

            var IncreaseStartDate1 = new JsInputDate("IncreaseStartDate1");
            IncreaseStartDate1.setDisabled(false);
            IncreaseStartDate1.setWidth("130px");

            var IncreaseStartDate2 = new JsInputDate("IncreaseStartDate2");
            IncreaseStartDate2.setDisabled(false);
            IncreaseStartDate2.setWidth("130px");

            var IncreaseStartDate3 = new JsInputDate("IncreaseStartDate3");
            IncreaseStartDate3.setDisabled(false);
            IncreaseStartDate3.setWidth("130px");

            var IncreaseStartDate4 = new JsInputDate("IncreaseStartDate4");
            IncreaseStartDate4.setDisabled(false);
            IncreaseStartDate4.setWidth("130px");


            var MinContractSignedDate = new JsInputDate("MinContractSignedDate");
            MinContractSignedDate.setDisabled(false);
            MinContractSignedDate.setWidth("100px");

            var MaxContractSignedDate = new JsInputDate("MaxContractSignedDate");
            MaxContractSignedDate.setDisabled(false);
            MaxContractSignedDate.setWidth("100px");

            var MinContractEndDate = new JsInputDate("MinContractEndDate");
            MinContractEndDate.setDisabled(false);
            MinContractEndDate.setWidth("100px");

            var MaxContractEndDate = new JsInputDate("MaxContractEndDate");
            MaxContractEndDate.setDisabled(false);
            MaxContractEndDate.setWidth("100px");

            var MinOffLeaseActulDate = new JsInputDate("MinOffLeaseActulDate");
            MinOffLeaseActulDate.setDisabled(false);
            MinOffLeaseActulDate.setWidth("100px");

            var MaxOffLeaseActulDate = new JsInputDate("MaxOffLeaseActulDate");
            MaxOffLeaseActulDate.setDisabled(false);
            MaxOffLeaseActulDate.setWidth("100px");

            var OffLeaseScheduleDate = new JsInputDate("OffLeaseScheduleDate");
            OffLeaseScheduleDate.setDisabled(false);
            OffLeaseScheduleDate.setWidth("120px");

            var ActuallyLeaveDate = new JsInputDate("ActuallyLeaveDate");
            ActuallyLeaveDate.setDisabled(false);
            ActuallyLeaveDate.setWidth("150px");
        });

        var id = "";
        var tp = "";
        var itemid2 = "";
        var itemtp2 = "";
        var copyid = "";
        var trid = "";
        reflist();
</script>
</body>
</html>