<%@ Page Language="C#" AutoEventWireup="true" Inherits="project.Presentation.Op.ContractChangeRecord,project"  %>
<!DOCTYPE>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="head1" runat="server">
    <title>物业合同变更</title>    
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
    <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 基础资料管理 <span class="c-gray en">&gt;</span> 物业合同变更 <a class="btn btn-success radius r mr-20" style="line-height:1.6em;margin-top:2px" href="javascript:location.replace(location.href);" title="刷新" ><i class="Hui-iconfont">&#xe68f;</i></a></nav>
    <div id="list" class="pt-5 pr-20 pb-5 pl-20">
	    <div class="cl pd-3 bg-1 bk-gray mt-2"> 
            <span class="l">
                <a href="javascript:;" onclick="update()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe60c;</i> 变更</a> 
                <a href="javascript:;" onclick="view()" class="btn btn-secondary radius"><i class="Hui-iconfont">&#xe627;</i> 查看</a>
                <input type="hidden" id="selectKey" />
            </span> 
	    </div>
	    <div class="cl pd-10  bk-gray mt-2"> 
		    合同编号&nbsp;<input type="text" class="input-text size-MINI" placeholder="合同编号" id="ContractNoS" style="width:110px">            
            手工合同编号&nbsp;<input type="text" class="input-text size-MINI" placeholder="手工合同编号" id="ContractNoManualS" style="width:110px">
            服务商&nbsp;<%=ContractSPNoStrS %>
            客户&nbsp;<input type="text" class="input-text size-MINI" placeholder="" id="ContractCustNoS" style="width:110px">
            合同签订日期 从&nbsp;<input type="text" class="input-text size-MINI" id="MinContractSignedDate" style="width:110px">
            至&nbsp;<input type="text" class="input-text size-MINI" id="MaxContractSignedDate" style="width:110px">            
            合同到期日期 从&nbsp;<input type="text" class="input-text size-MINI" id="MinContractEndDate" style="width:110px">
            至&nbsp;<input type="text" class="input-text size-MINI" id="MaxContractEndDate" style="width:110px">
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
                <li><a href="javascript:void(0)" onclick="show(5)" id="itemtab5">费用项目</a></li>
  	        </ul>
        </div>

        <div id="topeditdiv">
            <table class="tabedit">
                <tr>
                    <td class="tdl">合同类型</td>
                    <td class="tdr"><%=ContractTypeStr %></td>
                    <td class="tdl">客户</td>
                    <td class="tdr">
                        <input type="text" class="input-text size-MINI" id="ContractCustName" onblur="checkcust()" />
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
                    <td class="tdr"><input type="text" id="ContractSignedDate" class="input-text size-MINI" onchange="datechange()" /></td>
                    <td class="tdl">租金起收日期</td>
                    <td class="tdr"><input type="text" id="FeeStartDate" class="input-text size-MINI" /></td>
                    <td class="tdl">到期日期</td>
                    <td class="tdr"><input type="text" id="ContractEndDate" class="input-text size-MINI" /></td>
                    <td class="tdl">客户入场日期</td>
                    <td class="tdr"><input type="text" id="EntryDate"class="input-text size-MINI" /></td>
                </tr>
                <tr style="display:none;">
                    <td class="tdl">生效日期</td>
                    <td class="tdr"><input type="text" id="ContractStartDate" class="input-text size-MINI" /></td>
                    <td class="tdl">滞纳金占比</td>
                    <td class="tdr"><input type="text" id="ContractLatefeeRate"class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl"></td>
                    <td class="tdr"></td>
                    <td class="tdl"></td>
                    <td class="tdr"></td>
                </tr>
                <tr style="display:none;">
                    <td class="tdl">减免开始日期1</td>
                    <td class="tdr"><input type="text" id="ReduceStartDate1" class="input-text size-MINI" /></td>
                    <td class="tdl">减免截止日期1</td>
                    <td class="tdr"><input type="text" id="ReduceEndDate1" class="input-text size-MINI" /></td>
                    <td class="tdl">减免开始日期2</td>
                    <td class="tdr"><input type="text" id="ReduceStartDate2" class="input-text size-MINI" /></td>
                    <td class="tdl">减免截止日期2</td>
                    <td class="tdr"><input type="text" id="ReduceEndDate2" class="input-text size-MINI" /></td>
                </tr>
                <tr style="display:none;">
                    <td class="tdl">减免开始日期3</td>
                    <td class="tdr"><input type="text" id="ReduceStartDate3" class="input-text size-MINI" /></td>
                    <td class="tdl">减免截止日期3</td>
                    <td class="tdr"><input type="text" id="ReduceEndDate3" class="input-text size-MINI" /></td>
                    <td class="tdl">减免开始日期4</td>
                    <td class="tdr"><input type="text" id="ReduceStartDate4" class="input-text size-MINI" /></td>
                    <td class="tdl">减免截止日期4</td>
                    <td class="tdr"><input type="text" id="ReduceEndDate4" class="input-text size-MINI" /></td>
                </tr>
                <tr style="display:none;">
                    <td class="tdl">房屋押金</td>
                    <td class="tdr"><input type="text" id="RMRentalDeposit" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
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
                    <td class="tdl">工位押金</td>
                    <td class="tdr"><input type="text" id="WPRentalDeposit" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl"></td>
                    <td class="tdr"></td>
                </tr>
                <tr style="display:none;">
                    <td class="tdl">房屋水电押金</td>
                    <td class="tdr"><input type="text" id="RMUtilityDeposit" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl">工位电费押金</td>
                    <td class="tdr"><input type="text" id="WPUtilityDeposit" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl"></td>
                    <td class="tdr"></td>
                    <td class="tdl"></td>
                    <td class="tdr"></td>
                </tr>
                <tr style="display:none;">
                    <td class="tdl">工位数量</td>
                    <td class="tdr"><input type="text" id="WPQTY" class="input-text size-MINI" onchange="validInt(this.id)" /></td>
                    <td class="tdl">工位用电额度</td>
                    <td class="tdr"><input type="text" id="WPElectricyLimit" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl">超额用电单价</td>
                    <td class="tdr"><input type="text" id="WPOverElectricyPrice" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl"></td>
                    <td class="tdr"></td>
                </tr>
                <tr style="display:none;">
                    <td class="tdl">递增开始时间1</td>
                    <td class="tdr"><input type="text" id="IncreaseStartDate1" class="input-text size-MINI" /></td>
                    <td class="tdl">递增率1</td>
                    <td class="tdr"><input type="text" id="IncreaseRate1" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl">递增开始时间2</td>
                    <td class="tdr"><input type="text" id="IncreaseStartDate2" class="input-text size-MINI" /></td>
                    <td class="tdl">递增率2</td>
                    <td class="tdr"><input type="text" id="IncreaseRate2" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                </tr>
                <tr style="display:none;">
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
                    <td class="tdr"><button type="button" class="btn btn-primary radius" id="uploadFiles" onclick="uploadFiles()" style="margin-left:20px;">上传附件</button></td>
                    <td colspan="6"><div id="ContractAttachmentFiles"></div><input type="hidden" id="ContractAttachment" /></td>
                </tr>
            </table>
        </div>     
        
        <div id="bodyeditdiv4">        
            <table class="tabedit">
                <tr>
                    <td class="tdl1">房屋编号</td>
                    <td class="tdr1">
                        <input type="text" class="input-text size-MINI" id="RMID3" disabled="disabled" style="width:200px;" />
                        <button type="button" class="btn btn-primary radius" id="chooseRMID3">选择</button>
                    </td>
                    <td class="tdl2">所属服务项目</td>
                    <td class="tdr2"><%=SRVNo4Str %></td>
                </tr>
                <tr>
                    <td class="tdl2">出租面积</td>
                    <td class="tdr2"><input type="text" id="RMArea1" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl2">单价(元/㎡/月)</td>
                    <td class="tdr2"><input type="text" id="UnitPrice1" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                </tr>
                <tr>
                    <td class="tdl2">备注</td>
                    <td colspan="3"><input type="text" id="Remark4" class="input-text size-MINI" /></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <input class="btn btn-primary radius" type="button" id="itemsave4" onclick="itemsave4()" value="保存" />&nbsp;
	                    <input class="btn btn-primary radius" type="button" id="itemclear4" onclick="itemclear4()" value="清空" />&nbsp;
                    </td>
                </tr>
            </table>
            <div style="width:100%;height:5px;"></div>
            <div id="itemlist4"style="width:1058px; height:320px; overflow:auto; margin:0px; padding:0px;"></div>
        </div>
        <div style="margin-top:10px;">
            <input class="btn btn-primary radius" type="button" id="submit" onclick="submit()" value="保存退出" />
            <input class="btn btn-primary radius" type="button" id="submit1" onclick="submit1()" value="保存继续" />
	        <input class="btn btn-default radius" type="button" id="cancel" onclick="cancel()" value="&nbsp;&nbsp;取&nbsp;&nbsp;消&nbsp;&nbsp;" />
        </div>
        <br />
        <br />
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
            if (vjson.type == "update") {
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
                    $("#ReduceStartDate1").val(vjson.ReduceStartDate1);
                    $("#ReduceEndDate1").val(vjson.ReduceEndDate1);
                    $("#ReduceStartDate2").val(vjson.ReduceStartDate2);
                    $("#ReduceEndDate2").val(vjson.ReduceEndDate2);
                    $("#ReduceStartDate3").val(vjson.ReduceStartDate3);
                    $("#ReduceEndDate3").val(vjson.ReduceEndDate3);
                    $("#ReduceStartDate4").val(vjson.ReduceStartDat4);
                    $("#ReduceEndDate4").val(vjson.ReduceEndDate4);
                    $("#ContractLatefeeRate").val(vjson.ContractLatefeeRate);
                    $("#RMRentalDeposit").val("");
                    $("#RMUtilityDeposit").val("");
                    $("#PropertyFeeStartDate").val("<%=date %>");
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

                    //$("#uploadFiles").css("display", "");
                    $("#uploadFiles").css("display", "none");

                    copyid = "";
                    show(1);
                    itemclear4();
                    $("#itemlist4").html(vjson.itemlist4);

                    //$("#ContractNo").attr("disabled", true);
                    $("#list").css("display", "none");
                    $("#edit").css("display", "");

                    $("#itemsave4").css("display", "");
                    $("#itemclear4").css("display", "");
                    $("#submit").css("display", "none");
                    $("#submit1").css("display", "none");
                    //$("#submit").css("display", "");
                    //$("#submit1").css("display", "");
                }
                else if (vjson.flag == "3") {
                    layer.alert("当前订单已审核，不能修改！");
                }
                else {
                    layer.alert("数据操作出错！");
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
                    $("#ReduceStartDate1").val(vjson.ReduceStartDate1);
                    $("#ReduceEndDate1").val(vjson.ReduceEndDate1);
                    $("#ReduceStartDate2").val(vjson.ReduceStartDate2);
                    $("#ReduceEndDate2").val(vjson.ReduceEndDate2);
                    $("#ReduceStartDate3").val(vjson.ReduceStartDate3);
                    $("#ReduceEndDate3").val(vjson.ReduceEndDate3);
                    $("#ReduceStartDate4").val(vjson.ReduceStartDate4);
                    $("#ReduceEndDate4").val(vjson.ReduceEndDate4);
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
                    itemclear4();
                    $("#itemlist4").html(vjson.itemlist4);

                    //$("#ContractNo").attr("disabled", true);
                    $("#list").css("display", "none");
                    $("#edit").css("display", "");

                    $("#itemsave4").css("display", "none");
                    $("#itemclear4").css("display", "none");
                    $("#submit").css("display", "none");
                    $("#submit1").css("display", "none");
                }
                else {
                    layer.alert("数据操作出错！");
                }
                return;
            }            
            if (vjson.type == "itemsave4") {
                if (vjson.flag == "1") {
                    itemclear4();
                    $("#itemlist4").html(vjson.liststr);
                }
                else {
                    layer.alert("提交数据出错！");
                }
                return;
            }
            if (vjson.type == "itemupdate4") {
                if (vjson.flag == "1") {
                    $("#RMID3").val(vjson.RMID);
                    $("#SRVNo4").val(vjson.SRVNo);
                    $("#RMArea1").val(vjson.RMArea);
                    $("#UnitPrice1").val(vjson.UnitPrice);
                    $("#Remark4").val(vjson.Remark);

                    itemid4 = vjson.ItemId;
                    itemtp4 = "update";
                }
                else {
                    layer.alert("修改数据出错！");
                }
                return;
            }
            if (vjson.type == "itemdel4") {
                if (vjson.flag == "1") {
                    $("#itemlist4").html(vjson.liststr);
                    itemclear4();
                }
                else {
                    layer.alert("删除数据出错！");
                }
                return;
            }
            if (vjson.type == "getrmid4") {
                if (vjson.flag == "1") {
                    $("#RMArea1").val(vjson.RMBuildSize);
                }
                else {
                    layer.alert("删除数据出错！");
                }
                return;
            }
            if (vjson.type == "getprice4") {
                if (vjson.flag == "1") {
                    $("#UnitPrice1").val(vjson.UnitPrice);
                }
                else {
                    layer.alert("获取数据出错！");
                }
                return;
            }
        }

        function update() {
            if ($("#selectKey").val() == "") {
                layer.msg("请先选择一条记录", { icon: 3, time: 1000 });
                return;
            }
            id = $("#selectKey").val();
            type = "update";
            var submitData = new Object();
            submitData.Type = "update";
            submitData.id = id;

            transmitData(datatostr(submitData));
            return;
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
        function select() {
            var submitData = new Object();
            submitData.Type = "select";
            submitData.ContractNoS = $("#ContractNoS").val();
            submitData.ContractNoManualS = $("#ContractNoManualS").val();
            submitData.ContractTypeS = "04";
            submitData.ContractSPNoS = $("#ContractSPNoS").val();
            submitData.ContractCustNoS = $("#ContractCustNoS").val();
            submitData.MinContractSignedDate = $("#MinContractSignedDate").val();
            submitData.MaxContractSignedDate = $("#MaxContractSignedDate").val();
            submitData.MinContractEndDate = $("#MinContractEndDate").val();
            submitData.MaxContractEndDate = $("#MaxContractEndDate").val();
            submitData.OffLeaseStatusS = "1";
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

        function showPic(name, id) {
            $("#ContractAttachmentFiles").append("<span style=\"margin-left:10px;\"><a href=\"..\\..\\upload\\" + name + "\">" + name + "</a>&nbsp;" +
                "<button id=\"" + RandomName() + "\" onclick=\"deletefile(this.id,'" + name + "')\">删除</button></span>");
            $("#ContractAttachment").val($("#ContractAttachment").val() + "<file>" + name + "</file>");
            layer.closeAll();
        }

        function deletefile(controlid, filename) {
            layer.confirm('确定要删除此附件吗？', function (index) {
                $("#" + controlid).parent().remove();
                $("#ContractAttachment").val($("#ContractAttachment").val().replace("<file>" + filename + "</file>", ""));
                layer.close(index);
            });
        }

        
        function itemsave4() {
            if ($("#RMID3").val() == "") {
                layer.msg("请选择房间编号！", { icon: 7, time: 1500 });
                $("#RMID3").focus();
                return;
            }
            if ($("#SRVNo4").val() == "") {
                layer.msg("请选择所属费用项目！", { icon: 7, time: 1500 });
                $("#SRVNo4").focus();
                return;
            }
            if ($("#RMArea1").val() == "") {
                layer.msg("请填写出租面积！", { icon: 7, time: 1500 });
                $("#RMArea1").focus();
                return;
            }
            if ($("#UnitPrice1").val() == "") {
                layer.msg("请填写单价！", { icon: 7, time: 1500 });
                $("#UnitPrice1").focus();
                return;
            }
            var submitData = new Object();
            submitData.Type = "itemsave4";
            submitData.id = id;
            submitData.itemid = itemid4;
            submitData.itemtp = itemtp4;
            submitData.RMID = $("#RMID3").val();
            submitData.SRVNo = $("#SRVNo4").val();
            submitData.RMArea = $("#RMArea1").val();
            submitData.UnitPrice = $("#UnitPrice1").val();
            submitData.Remark = $("#Remark4").val();

            transmitData(datatostr(submitData));
            return;
        }
        function itemupdate4(itemid) {
            var submitData = new Object();
            submitData.Type = "itemupdate4";
            submitData.itemid = itemid;
            transmitData(datatostr(submitData));
            return;
        }
        function itemdel4(itemid) {
            layer.confirm('确认要删除吗？', function (index) {
                var submitData = new Object();
                submitData.Type = "itemdel4";
                submitData.id = id;
                submitData.itemid = itemid;
                transmitData(datatostr(submitData));
                layer.close(index);
            });
            return;
        }
        function itemclear4() {
            itemtp4 = "insert";
            itemid4 = "";

            $("#SRVNo4").val("<%=PTRentSRVNo %>");
            $("#RMID3").val("");
            $("#RMArea1").val("");
            $("#UnitPrice1").val("<%=PTRentFee %>");
            $("#Remark4").val("");
        }

        var page = 1;
        function jump(pageindex) {
            page = pageindex;
            var submitData = new Object();
            submitData.Type = "jump";
            submitData.ContractNoS = $("#ContractNoS").val();
            submitData.ContractNoManualS = $("#ContractNoManualS").val();
            submitData.ContractTypeS = "04";
            submitData.ContractSPNoS = $("#ContractSPNoS").val();
            submitData.ContractCustNoS = $("#ContractCustNoS").val();
            submitData.MinContractSignedDate = $("#MinContractSignedDate").val();
            submitData.MaxContractSignedDate = $("#MaxContractSignedDate").val();
            submitData.MinContractEndDate = $("#MinContractEndDate").val();
            submitData.MaxContractEndDate = $("#MaxContractEndDate").val();
            submitData.OffLeaseStatusS = "1";
            submitData.page = page;
            transmitData(datatostr(submitData));
            return;
        }
        
        function show(page) {
            if (page == 1) {
                $('#itemtab5').removeClass("selected");
                $("#bodyeditdiv4").hide();

                $('#itemtab1').addClass("selected");
                $("#topeditdiv").show();
            }
            else {
                if (id == "") {
                    layer.msg("请先保存表头信息！", { icon: 3, time: 1000 });
                    return;
                }

                $('#itemtab1').removeClass("selected");
                $('#itemtab5').removeClass("selected");
                $("#topeditdiv").hide();
                $("#bodyeditdiv4").hide();

                if (page == "5") {
                    $('#itemtab5').addClass("selected");
                    $("#bodyeditdiv4").show();
                }
            }
        }

        $("#ContractCustImg").click(function () {
            ChooseBasic("ContractCustNo", "Cust");
        });
        $("#chooseRMID3").click(function () {
            var temp = "../Base/ChooseRMID.aspx?id=RMID3";
            layer_show("选择页面", temp, 800, 630);
        });
        function choose(id, labels, values) {
            if (labels != "" && labels != undefined && labels != "undefined") {
                if (id == "ContractCustNo") {
                    $("#ContractCustName").val(values);
                    $("#ContractCustNo").val(labels);
                }
                else if (id == "RMID3") {
                    $("#" + id).val(labels);

                    var submitData = new Object();
                    submitData.Type = "getrmid4";
                    submitData.rmid = $("#" + id).val();
                    transmitData(datatostr(submitData));
                }
            }
        }
        $("#SRVNo4").change(function () {
            var submitData = new Object();
            submitData.Type = "getprice4";
            submitData.SRVNo = $("#SRVNo4").val();
            transmitData(datatostr(submitData));
        });

        function checkRM(id, type) {
            if ($("#" + id).val() == "") {
                return;
            }
            var submitData = new Object();
            submitData.Type = "check";
            submitData.val = $("#" + id).val();
            submitData.tp = type;
            submitData.id = id;
            transmitData(datatostr(submitData));
            return;
        }
        function checkcust() {
            if ($("#ContractCustName").val() == "") {
                $("#ContractCustNo").val("");
                return;
            }
            var submitData = new Object();
            submitData.Type = "checkcust";
            submitData.val = $("#ContractCustName").val();
            submitData.tp = "Cust";
            transmitData(datatostr(submitData));
            return;
        }

        function datechange() {
            $("#ContractStartDate").val($("#ContractSignedDate").val());
            $("#EntryDate").val($("#ContractSignedDate").val());
            $("#FeeStartDate").val($("#ContractSignedDate").val());
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
        });

        var id = "";
        var tp = "";
        var itemid4 = "";
        var itemtp4 = "";
        var copyid = "";
        var trid = "";
        reflist();
</script>
</body>
</html>