<%@ Page Language="C#" AutoEventWireup="true" Inherits="project.Presentation.Op.Contract_WP,project" %>

<!DOCTYPE>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head1" runat="server">
    <title>工位租赁合同</title>
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
    <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 合同管理 <span class="c-gray en">&gt;</span> 工位租赁合同 <a class="btn btn-success radius r mr-20" style="line-height: 1.6em; margin-top: 2px" href="javascript:location.replace(location.href);" title="刷新"><i class="Hui-iconfont">&#xe68f;</i></a></nav>
    <div id="list" class="pt-5 pr-20 pb-5 pl-20">
        <div class="cl pd-3 bg-1 bk-gray mt-2">
            <span class="l">
                <%--<a href="javascript:;" onclick="insert()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe600;</i> 添加</a>
                <a href="javascript:;" onclick="update()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe60c;</i> 修改</a> 
                <a href="javascript:;" onclick="del()" class="btn btn-danger radius"><i class="Hui-iconfont">&#xe6e2;</i> 删除</a> 
                <a href="javascript:;" onclick="clone()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe604;</i> 复制</a>
                <a href="javascript:;" onclick="view()" class="btn btn-secondary radius"><i class="Hui-iconfont">&#xe627;</i> 查看</a>
                <a href="javascript:;" onclick="viewfee()" class="btn btn-secondary radius"><i class="Hui-iconfont">&#xe627;</i> 查看租金明细</a>&nbsp;&nbsp;&nbsp;
                <a href="javascript:;" onclick="approve()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe615;</i> 审核</a>
                <a href="javascript:;" onclick="invalid()" class="btn btn-warning radius"><i class="Hui-iconfont">&#xe60b;</i> 作废</a>--%>
                <%=Buttons %>
                <input type="hidden" id="selectKey" />
            </span>
        </div>
        <%-- 检索条件 --%>
        <div class="cl pd-10  bk-gray mt-2">
            合同编号&nbsp;<input type="text" class="input-text size-MINI" placeholder="合同编号" id="ContractNoS" style="width: 110px">
            手工合同编号&nbsp;<input type="text" class="input-text size-MINI" placeholder="手工合同编号" id="ContractNoManualS" style="width: 110px">
            <%--合同类型&nbsp;<%=ContractTypeStrS %>--%>
            服务商&nbsp;<%=ContractSPNoStrS %>
            客户&nbsp;<input type="text" class="input-text size-MINI" placeholder="" id="ContractCustNoS" style="width: 110px">
            合同状态&nbsp;<select class="input-text size-MINI" style="width: 110px" id="ContractStatusS"><option value="" selected="selected">全部</option>
                <option value="1">制单</option>
                <option value="2">已审核</option>
                <option value="3">已退租</option>
                <option value="4">已作废</option>
            </select>
            退租状态&nbsp;<select class="input-text size-MINI" style="width: 110px" id="OffLeaseStatusS"><option value="" selected="selected">全部</option>
                <option value="1">未退租</option>
                <option value="2">已申请</option>
                <option value="3">已办理</option>
                <option value="4">已结算</option>
            </select>
            <br />
            合同签订日期 从&nbsp;<input type="text" class="input-text size-MINI" id="MinContractSignedDate" style="width: 110px">
            至&nbsp;<input type="text" class="input-text size-MINI" id="MaxContractSignedDate" style="width: 110px">
            合同到期日期 从&nbsp;<input type="text" class="input-text size-MINI" id="MinContractEndDate" style="width: 110px">
            至&nbsp;<input type="text" class="input-text size-MINI" id="MaxContractEndDate" style="width: 110px">
            实际退租日期 从&nbsp;<input type="text" class="input-text size-MINI" id="MinOffLeaseActulDate" style="width: 110px">
            至&nbsp;<input type="text" class="input-text size-MINI" id="MaxOffLeaseActulDate" style="width: 110px">
            <button type="submit" class="btn btn-success radius" onclick="select()"><i class="Hui-iconfont">&#xe665;</i> 检索</button>
        </div>

        <%-- 数据表格 --%>
        <div class="mt-5" id="outerlist">
            <%=list %>
        </div>
    </div>
    <%-- 编辑操作 --%>
    <div id="edit" class="editdiv" style="display: none;">
        <%-- 编辑头部 --%>
        <div class="itab">
            <ul>
                <li><a href="javascript:void(0)" onclick="show(1)" id="itemtab1" class="selected">基本信息</a></li>
                <%--<li><a href="javascript:void(0)" onclick="show(2)" id="itemtab2">房屋信息</a></li>--%>
                <li><a href="javascript:void(0)" onclick="show(3)" id="itemtab3">工位信息</a></li>
                <%--<li><a href="javascript:void(0)" onclick="show(4)" id="itemtab4">广告位信息</a></li>--%>
            </ul>
        </div>
        <%-- 基本信息--%>
        <div id="topeditdiv">
            <table class="tabedit">
                <tr>
                    <td class="tdl">合同类型</td>
                    <td class="tdr"><%=ContractTypeStr %></td>
                    <td class="tdl">合同主体</td>
                    <td class="tdr"><%=ContractSPNoStr %></td>
                    <td class="tdl">客户</td>
                    <td class="tdr" colspan="3">
                        <input type="text" class="input-text size-MINI" id="ContractCustName" onblur="checkcust()" style="width:400px" />
                        <input type="hidden" id="ContractCustNo" />
                        <img id="ContractCustImg" alt="" src="../../images/view_detail.png" class="view_detail" style="padding-left:5px" />
                    </td>
<%--                    <td class="tdl"></td>
                    <td class="tdr"></td>--%>
                </tr>
                <tr>
                    <td class="tdl">合同编号</td>
                    <td class="tdr">
                        <input type="text" id="ContractNo" disabled="disabled" class="input-text size-MINI" /></td>
                    <td class="tdl">手工合同编号</td>
                    <td class="tdr">
                        <input type="text" id="ContractNoManual" class="input-text size-MINI" /></td>
                    <td class="tdl">经办人</td>
                    <td class="tdr">
                        <input type="text" id="ContractHandler" disabled="disabled" class="input-text size-MINI" /></td>
                    <td class="tdl">合同状态</td>
                    <td class="tdr">
                        <input type="text" id="ContractStatus" disabled="disabled" class="input-text size-MINI" /></td>
                </tr>
                <tr>
                    <td class="tdl">合同签订日期</td>
                    <td class="tdr">
                        <input type="text" id="ContractSignedDate" class="input-text size-MINI" onchange="datechange()" /></td>
                    <td class="tdl">租金起收日期</td>
                    <td class="tdr">
                        <input type="text" id="FeeStartDate" class="input-text size-MINI" /></td>
                    <td class="tdl">到期日期</td>
                    <td class="tdr">
                        <input type="text" id="ContractEndDate" class="input-text size-MINI" /></td>
                    <td class="tdl">客户入场日期</td>
                    <td class="tdr">
                        <input type="text" id="EntryDate" class="input-text size-MINI" /></td>
                </tr>
                <tr>
                    <td class="tdl">生效日期</td>
                    <td class="tdr">
                        <input type="text" id="ContractStartDate" class="input-text size-MINI" /></td>
                    <td class="tdl">滞纳金占比</td>
                    <td class="tdr">
                        <input type="text" id="ContractLatefeeRate" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl">工位押金</td>
                    <td class="tdr">
                        <input type="text" id="WPRentalDeposit" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    
                    <td class="tdl"></td>
                    <td class="tdr"></td>
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
                
                
                <tr style="display:none;">
                    <td class="tdl">工位数量</td>
                    <td class="tdr">
                        <input type="text" id="WPQTY" class="input-text size-MINI" onchange="validInt(this.id)" /></td>
                    <td class="tdl">工位用电额度</td>
                    <td class="tdr">
                        <input type="text" id="WPElectricyLimit" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl">超额用电单价</td>
                    <td class="tdr">
                        <input type="text" id="WPOverElectricyPrice" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <%--<td class="tdl">工位押金</td>
                    <td class="tdr">
                        <input type="text" id="WPRentalDeposit" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>--%>
                </tr>
                <tr>
                    <td class="tdl">减免开始日期1</td>
                    <td class="tdr">
                        <input type="text" id="ReduceStartDate1" class="input-text size-MINI" /></td>
                    <td class="tdl">减免截止日期1</td>
                    <td class="tdr">
                        <input type="text" id="ReduceEndDate1" class="input-text size-MINI" /></td>
                    <td class="tdl">减免开始日期2</td>
                    <td class="tdr">
                        <input type="text" id="ReduceStartDate2" class="input-text size-MINI" /></td>
                    <td class="tdl">减免截止日期2</td>
                    <td class="tdr">
                        <input type="text" id="ReduceEndDate2" class="input-text size-MINI" /></td>
                </tr>
                <tr>
                    <td class="tdl">减免开始日期3</td>
                    <td class="tdr">
                        <input type="text" id="ReduceStartDate3" class="input-text size-MINI" /></td>
                    <td class="tdl">减免截止日期3</td>
                    <td class="tdr">
                        <input type="text" id="ReduceEndDate3" class="input-text size-MINI" /></td>
                    <td class="tdl">减免开始日期4</td>
                    <td class="tdr">
                        <input type="text" id="ReduceStartDate4" class="input-text size-MINI" /></td>
                    <td class="tdl">减免截止日期4</td>
                    <td class="tdr">
                        <input type="text" id="ReduceEndDate4" class="input-text size-MINI" /></td>
                </tr>
                <tr>
                    <td class="tdl">是否按照固定金额</td>
                    <td class="tdr">
                        <input type="checkbox" id="IsFixedAmt" class="input-text size-MINI" style="width: 30px;" title="请勾选"/></td>
                    <td class="tdl">固定金额</td>
                    <td class="tdr">
                        <input type="text" id="Amount" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl">递增方式</td>
                    <td class="tdr">
                        <select id="IncreaseType" class="input-text size-MINI">
                            <option value="1">按递增率递增</option>
                            <option value="2">按固定金额递增</option>
                        </select>
                    </td>
                     <td class="tdl"></td>
                    <td class="tdr"></td>
                </tr>
                <tr>
                    <td class="tdl">递增开始时间1</td>
                    <td class="tdr">
                        <input type="text" id="IncreaseStartDate1" class="input-text size-MINI" /></td>
                    <td class="tdl">递增1</td>
                    <td class="tdr">
                        <input type="text" id="IncreaseRate1" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl">递增开始时间2</td>
                    <td class="tdr">
                        <input type="text" id="IncreaseStartDate2" class="input-text size-MINI" /></td>
                    <td class="tdl">递增2</td>
                    <td class="tdr">
                        <input type="text" id="IncreaseRate2" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                </tr>
                <tr>
                    <td class="tdl">递增开始时间3</td>
                    <td class="tdr">
                        <input type="text" id="IncreaseStartDate3" class="input-text size-MINI" /></td>
                    <td class="tdl">递增3</td>
                    <td class="tdr">
                        <input type="text" id="IncreaseRate3" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl">递增开始时间4</td>
                    <td class="tdr">
                        <input type="text" id="IncreaseStartDate4" class="input-text size-MINI" /></td>
                    <td class="tdl">递增4</td>
                    <td class="tdr">
                        <input type="text" id="IncreaseRate4" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                </tr>
                <tr>
                    <td class="tdl">备注</td>
                    <td class="tdr" colspan="7">
                        <textarea cols="" rows="3" class="textarea required" id="Remark"></textarea></td>
                </tr>
                <tr>
                    <td class="tdl">附件</td>
                    <td class="tdr">
                        <button type="button" class="btn btn-primary radius" id="uploadFiles" onclick="uploadFiles()" style="margin-left: 20px;">上传附件</button></td>
                    <td colspan="6">
                        <div id="ContractAttachmentFiles"></div>
                        <input type="hidden" id="ContractAttachment" /></td>
                </tr>
            </table>
        </div>
        <%-- 工位信息 --%>
        <div id="bodyeditdiv2">
            <table class="tabedit">
                <tr>
                    <td class="tdl1">房屋编号</td>
                    <td class="tdr1">
                        <input type="text" class="input-text size-MINI" id="RMID2" disabled="disabled" style="width: 70%;" />
                        <button type="button" class="btn btn-primary radius" id="chooseRMID2" >选择</button>
                    </td>
                    <td class="tdl1">工位编号</td>
                    <td class="tdr1">
                        <input type="text" id="WPNo" class="input-text size-MINI" disabled="disabled" style="width: 70%" />
                        <button type="button" class="btn btn-primary radius" id="chooseWPNo">选择</button>
                    </td>
                    <td class="tdl1">费用项目</td>
                    <td class="tdr1"><%=SRVNo2Str %></td>
                </tr>
                <tr>
                    <td class="tdl1">工位数量</td>
                    <td class="tdr1">
                        <input type="text" id="WPQTY2" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl1">工位单价</td>
                    <td class="tdr1">
                        <input type="text" id="WPRentalUnitPrice" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl1"></td>
                    <td class="tdr1"></td>
                </tr>
                <tr>
                    <td class="tdl1">工位位置</td>
                    <td colspan="5">
                        <input type="text" id="RMLoc2" class="input-text size-MINI" /></td>
                </tr>
                <tr>
                    <td class="tdl1">备注</td>
                    <td colspan="5">
                        <input type="text" id="Remark2" class="input-text size-MINI" /></td>
                </tr>
                <tr>
                    <td colspan="6">
                        <input class="btn btn-primary radius" type="button" id="itemsave2" onclick="itemsave2()" value="保存" />&nbsp;
	                    <input class="btn btn-primary radius" type="button" id="itemclear2" onclick="itemclear2()" value="清空" />&nbsp;
                    </td>
                </tr>
            </table>
            <div style="width: 100%; height: 5px;"></div>
            <div id="itemlist2" style="width: 1058px; height: 320px; overflow: auto; margin: 0px; padding: 0px;"></div>
        </div>

        <div style="margin-top: 10px;">
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

            if (vjson.type == "delete") {
                if (vjson.flag == "1") {
                    $("#outerlist").html(vjson.liststr);
                    $("#selectKey").val("");
                    reflist();
                }
                else if (vjson.flag == "3") {
                    layer.alert("当前订单已审核，不能删除！");
                } else {
                    layer.alert("数据操作出错！");
                }
                return;
            }

            if (vjson.type == "insert") {
                if (vjson.flag == "1") {
                    $("#itemlist2").html(vjson.itemlist2);
                    itemclear2();
                    show(1);
                    id = "";
                    type = "insert";
                    copyid = "";
                    $("#ContractNo").val("");
                    $("#ContractType").val("02");
                    $("#ContractSPNo").val("<%=WPSPNo %>");
                    $("#ContractSPName").val("");
                    $("#ContractCustNo").val("");
                    $("#ContractCustName").val("");
                    $("#ContractNoManual").val("");
                    $("#ContractHandler").val("<%=userName %>");
                    $("#ContractSignedDate").val("<%=date %>");
                    $("#ContractStartDate").val("<%=date %>");
                    $("#ContractEndDate").val("");
                    $("#EntryDate").val("<%=date %>");
                    $("#FeeStartDate").val("<%=date %>");
                    $("#ReduceStartDate1").val("");
                    $("#ReduceEndDate1").val("");
                    $("#ReduceStartDate2").val("");
                    $("#ReduceEndDate2").val("");
                    $("#ReduceStartDate3").val("");
                    $("#ReduceEndDate3").val("");
                    $("#ReduceStartDate4").val("");
                    $("#ReduceEndDate4").val("");
                    $("#ContractLatefeeRate").val("");
                    $("#RMRentalDeposit").val("");
                    $("#RMUtilityDeposit").val("");
                    $("#PropertyFeeStartDate").val("<%=date %>");
                    $("#PropertyFeeReduceStartDate").val("");
                    $("#PropertyFeeReduceEndDate").val("");
                    $("#WaterUnitPrice").val("");
                    $("#ElecticityUintPrice").val("");
                    $("#AirconUnitPrice").val("");
                    $("#PropertyUnitPrice").val("");
                    $("#SharedWaterFee").val("");
                    $("#SharedElectricyFee").val("");
                    $("#WPRentalDeposit").val("");
                    $("#WPUtilityDeposit").val("");
                    $("#WPQTY").val("");
                    $("#WPElectricyLimit").val("");
                    $("#WPOverElectricyPrice").val(vjson.OverElectricFee);
                    $("#BBQTY").val("");
                    $("#BBAmount").val("");
                    $("#IncreaseType").val("1");
                    $("#IncreaseStartDate1").val("");
                    $("#IncreaseRate1").val("");
                    $("#IncreaseStartDate2").val("");
                    $("#IncreaseRate2").val("");
                    $("#IncreaseStartDate3").val("");
                    $("#IncreaseRate3").val("");
                    $("#IncreaseStartDate4").val("");
                    $("#IncreaseRate4").val("");
                    $("#ContractStatus").val("制单");
                    $("#Remark").val("");
                    $("#ContractAttachment").val("");
                    $("#ContractAttachmentFiles").html("");
                    $("#uploadFiles").css("display", "");
                    $("#list").css("display", "none");
                    $("#edit").css("display", "");
                    $("#itemsave2").css("display", "");
                    $("#itemclear2").css("display", "");
                    $("#submit").css("display", "");
                    $("#submit1").css("display", "");
                }
                else {
                    layer.alert("数据操作出错！");
                }
                return;
            }

            if (vjson.type == "submit") {
                if (vjson.flag == "1") {
                    if (vjson.submittp == "submit") {
                        $("#outerlist").html(vjson.liststr);
                        $("#selectKey").val("");
                        $("#list").css("display", "");
                        $("#edit").css("display", "none");
                        reflist();
                    }
                    else {
                        id = vjson.RowPointer;
                        type = "update";
                        $("#outerlist").html(vjson.liststr);
                        $("#selectKey").val("");
                        reflist();

                        $("#ContractNo").val(vjson.ContractNo);
                        layer.msg("保存成功！", { icon: 3, time: 1000 });

                        if (copyid != "") {
                            $("#itemlist2").html(vjson.itemlist2);
                        }
                        show(3);
                        copyid = "";
                    }
                }
                else {
                    layer.alert("数据操作出错！");
                }
                return;
            }

            if (vjson.type == "approve") {
                if (vjson.flag == "1") {
                    if (vjson.status == "1") {
                        $("#" + vjson.id + " td").eq(7).html("<label style=\"color:red;\">制单</label>");
                        layer.alert("取消审核成功！");
                    }
                    else {
                        $("#" + vjson.id + " td").eq(7).html("<label style=\"color:blue;\">已审核</label>");
                        layer.alert("审核成功！");
                    }
                }
                else if (vjson.flag == "3") {
                    layer.alert("当前合同状态不允许审核！");
                }
                else if (vjson.flag == "4") {
                    layer.alert("当前合同已生成订单，不能取消审核！");
                }
                else if (vjson.flag == "5") {
                    layer.alert(vjson.InfoBar);
                }
                else {
                    layer.alert("数据操作出错！");
                }
                return;
            }

            if (vjson.type == "invalid") {
                if (vjson.flag == "1") {
                    if (vjson.status == "1") {
                        $("#" + vjson.id + " td").eq(7).html("<label style=\"color:red;\">制单</label>");
                        layer.alert("取消作废成功！");
                    }
                    else {
                        $("#" + vjson.id + " td").eq(7).html("<label style=\"color:gray;\">已作废</label>");
                        layer.alert("作废成功！");
                    }
                }
                else if (vjson.flag == "3") {
                    layer.alert("当前合同状态不允许作废！");
                }
                else {
                    layer.alert("数据操作出错！");
                }
                return;
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
                    $("#ReduceStartDate4").val(vjson.ReduceStartDate4);
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
                    $("#IncreaseType").val(vjson.IncreaseType);
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

                    $("#uploadFiles").css("display", "");

                    copyid = "";
                    show(1);
                    itemclear2();
                    $("#itemlist2").html(vjson.itemlist2);

                    //$("#ContractNo").attr("disabled", true);
                    $("#list").css("display", "none");
                    $("#edit").css("display", "");

                    $("#itemsave2").css("display", "");
                    $("#itemclear2").css("display", "");
                    $("#submit").css("display", "");
                    $("#submit1").css("display", "");
                }
                else if (vjson.flag == "3") {
                    layer.alert("当前订单已审核，不能修改！");
                }
                else {
                    layer.alert("数据操作出错！");
                }
                return;
            }

            if (vjson.type == "clone") {
                if (vjson.flag == "1") {
                    $("#ContractNo").val("");
                    $("#ContractType").val(vjson.ContractType);
                    $("#ContractSPNo").val(vjson.ContractSPNo);
                    $("#ContractCustNo").val(vjson.ContractCustNo);
                    $("#ContractCustName").val(vjson.ContractCustName);
                    $("#ContractNoManual").val("");
                    $("#ContractHandler").val("<%=userName %>");
                    $("#ContractSignedDate").val("<%=date %>");
                    $("#ContractStartDate").val("<%=date %>");
                    $("#ContractEndDate").val("");
                    $("#EntryDate").val("");
                    $("#FeeStartDate").val("<%=date %>");
                    $("#ReduceStartDate1").val("");
                    $("#ReduceEndDate1").val("");
                    $("#ReduceStartDate2").val("");
                    $("#ReduceEndDate2").val("");
                    $("#ReduceStartDate3").val("");
                    $("#ReduceEndDate3").val("");
                    $("#ReduceStartDate4").val("");
                    $("#ReduceEndDate4").val("");
                    $("#ContractLatefeeRate").val(vjson.ContractLatefeeRate);
                    $("#RMRentalDeposit").val(vjson.RMRentalDeposit);
                    $("#RMUtilityDeposit").val(vjson.RMUtilityDeposit);
                    $("#PropertyFeeStartDate").val("<%=date %>");
                    $("#PropertyFeeReduceStartDate").val("");
                    $("#PropertyFeeReduceEndDate").val("");
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
                    $("#IncreaseType").val("");
                    $("#IncreaseStartDate1").val("");
                    $("#IncreaseRate1").val("");
                    $("#IncreaseStartDate2").val("");
                    $("#IncreaseRate2").val("");
                    $("#IncreaseStartDate3").val("");
                    $("#IncreaseRate3").val("");
                    $("#IncreaseStartDate4").val("");
                    $("#IncreaseRate4").val("");
                    $("#ContractStatus").val("制单");
                    $("#Remark").val("");

                    $("#ContractAttachment").val("");
                    $("#ContractAttachmentFiles").html("");

                    $("#uploadFiles").css("display", "");

                    copyid = vjson.cloneid;
                    id = "";
                    show(1);
                    itemclear2();
                    $("#itemlist2").html(vjson.itemlist2);

                    //$("#ContractNo").attr("disabled", true);
                    $("#list").css("display", "none");
                    $("#edit").css("display", "");

                    $("#itemsave2").css("display", "");
                    $("#itemclear2").css("display", "");
                    $("#submit").css("display", "");
                    $("#submit1").css("display", "");
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
                    $("#IncreaseType").val(vjson.IncreaseType);
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

            if (vjson.type == "checkcust") {
                if (vjson.flag == "1") {
                    $("#ContractCustNo").val(vjson.Code);
                    $("#ContractCustName").val(vjson.Name);
                }
                else if (vjson.flag == "3") {
                    layer.alert("未找到记录，确认是否输入正确！");
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

            if (vjson.type == "itemsave2") {
                if (vjson.flag == "1") {
                    itemclear2();
                    $("#RMID2").focus();
                    $("#itemlist2").html(vjson.liststr);
                }
                else {
                    layer.alert("提交数据出错！");
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

            if (vjson.type == "itemdel2") {
                if (vjson.flag == "1") {
                    $("#itemlist2").html(vjson.liststr);
                    itemclear2();
                }
                else {
                    layer.alert("删除数据出错！");
                }
                return;
            }

            if (vjson.type == "getwpno") {
                if (vjson.flag == "1") {
                    $("#WPQTY2").val(vjson.WPSeat);
                    $("#WPRentalUnitPrice").val(vjson.WPSeatPrice);
                    $("#RMLoc2").val(vjson.WPAddr);
                }
                else {
                    layer.alert("获取数据出错！");
                }
                return;
            }
            if (vjson.type == "getprice2") {
                if (vjson.flag == "1") {
                    $("#WPRentalUnitPrice").val(vjson.UnitPrice);
                }
                else {
                    layer.alert("获取数据出错！");
                }
                return;
            }
        }

        function insert() {
            var submitData = new Object();
            submitData.Type = "insert";
            transmitData(datatostr(submitData));
            return;
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

        function edit(rid) {
            id = rid;
            type = "update";
            var submitData = new Object();
            submitData.Type = "edit";
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

        function submit() {
            //if ($("#ContractNo").val() == "") {
            //    layer.msg("请填写合同编号！", { icon: 7, time: 1000 });
            //    $("#ContractNo").focus();
            //    return;
            //}
            //if ($("#ContractType").val() == "") {
            //    layer.msg("请选择合同类型！", { icon: 7, time: 1000 });
            //    $("#ContractType").focus();
            //    return;
            //}
            if ($("#ContractSPNo").val() == "") {
                layer.msg("请选择合同主体！", { icon: 7, time: 1000 });
                $("#ContractSPNo").focus();
                return;
            }
            if ($("#ContractCustNo").val() == "") {
                layer.msg("请选择客户！", { icon: 7, time: 1000 });
                $("#ContractCustNo").focus();
                return;
            }
            if ($("#ContractSignedDate").val() == "") {
                layer.msg("请选择合同签订日期！", { icon: 7, time: 1000 });
                $("#ContractSignedDate").focus();
                return;
            }
            if ($("#ContractStartDate").val() == "") {
                layer.msg("请选择合同生效日期！", { icon: 7, time: 1000 });
                $("#ContractStartDate").focus();
                return;
            }
            if ($("#ContractEndDate").val() == "") {
                layer.msg("请选择合同到期日期！", { icon: 7, time: 1000 });
                $("#ContractEndDate").focus();
                return;
            }
            var submitData = new Object();
            submitData.Type = "submit";
            submitData.submittp = "submit";
            submitData.id = id;

            submitData.ContractNo = $("#ContractNo").val();
            submitData.ContractType = $("#ContractType").val();
            submitData.ContractSPNo = $("#ContractSPNo").val();
            submitData.ContractCustNo = $("#ContractCustNo").val();
            submitData.ContractNoManual = $("#ContractNoManual").val();
            submitData.ContractHandler = $("#ContractHandler").val();
            submitData.ContractSignedDate = $("#ContractSignedDate").val();
            submitData.ContractStartDate = $("#ContractStartDate").val();
            submitData.ContractEndDate = $("#ContractEndDate").val();
            submitData.EntryDate = $("#EntryDate").val();
            submitData.FeeStartDate = $("#FeeStartDate").val();
            submitData.ReduceStartDate1 = $("#ReduceStartDate1").val();
            submitData.ReduceEndDate1 = $("#ReduceEndDate1").val();
            submitData.ReduceStartDate2 = $("#ReduceStartDate2").val();
            submitData.ReduceEndDate2 = $("#ReduceEndDate2").val();
            submitData.ReduceStartDate3 = $("#ReduceStartDate3").val();
            submitData.ReduceEndDate3 = $("#ReduceEndDate3").val();
            submitData.ReduceStartDate4 = $("#ReduceStartDate4").val();
            submitData.ReduceEndDate4 = $("#ReduceEndDate4").val();
            submitData.ContractLatefeeRate = $("#ContractLatefeeRate").val();
            submitData.RMRentalDeposit = $("#RMRentalDeposit").val();
            submitData.RMUtilityDeposit = $("#RMUtilityDeposit").val();
            submitData.PropertyFeeStartDate = $("#PropertyFeeStartDate").val();
            submitData.PropertyFeeReduceStartDate = $("#PropertyFeeReduceStartDate").val();
            submitData.PropertyFeeReduceEndDate = $("#PropertyFeeReduceEndDate").val();
            submitData.WaterUnitPrice = $("#WaterUnitPrice").val();
            submitData.ElecticityUintPrice = $("#ElecticityUintPrice").val();
            submitData.AirconUnitPrice = $("#AirconUnitPrice").val();
            submitData.PropertyUnitPrice = $("#PropertyUnitPrice").val();
            submitData.SharedWaterFee = $("#SharedWaterFee").val();
            submitData.SharedElectricyFee = $("#SharedElectricyFee").val();
            submitData.WPRentalDeposit = $("#WPRentalDeposit").val();
            submitData.WPUtilityDeposit = $("#WPUtilityDeposit").val();
            submitData.WPQTY = $("#WPQTY").val();
            submitData.WPElectricyLimit = $("#WPElectricyLimit").val();
            submitData.WPOverElectricyPrice = $("#WPOverElectricyPrice").val();
            submitData.BBQTY = $("#BBQTY").val();
            submitData.BBAmount = $("#BBAmount").val();
            submitData.IncreaseType = $("#IncreaseType").val();
            submitData.IncreaseStartDate1 = $("#IncreaseStartDate1").val();
            submitData.IncreaseRate1 = $("#IncreaseRate1").val();
            submitData.IncreaseStartDate2 = $("#IncreaseStartDate2").val();
            submitData.IncreaseRate2 = $("#IncreaseRate2").val();
            submitData.IncreaseStartDate3 = $("#IncreaseStartDate3").val();
            submitData.IncreaseRate3 = $("#IncreaseRate3").val();
            submitData.IncreaseStartDate4 = $("#IncreaseStartDate4").val();
            submitData.IncreaseRate4 = $("#IncreaseRate4").val();
            submitData.Remark = $("#Remark").val();
            submitData.ContractAttachment = $("#ContractAttachment").val();

            submitData.tp = type;
            submitData.copyid = copyid;
            submitData.ContractNoS = $("#ContractNoS").val();
            submitData.ContractNoManualS = $("#ContractNoManualS").val();
            submitData.ContractTypeS = "02";
            submitData.ContractSPNoS = $("#ContractSPNoS").val();
            submitData.ContractCustNoS = $("#ContractCustNoS").val();
            submitData.MinContractSignedDate = $("#MinContractSignedDate").val();
            submitData.MaxContractSignedDate = $("#MaxContractSignedDate").val();
            submitData.MinContractEndDate = $("#MinContractEndDate").val();
            submitData.MaxContractEndDate = $("#MaxContractEndDate").val();
            submitData.ContractStatusS = $("#ContractStatusS").val();
            submitData.OffLeaseStatusS = $("#OffLeaseStatusS").val();
            submitData.MinOffLeaseActulDate = $("#MinOffLeaseActulDate").val();
            submitData.MaxOffLeaseActulDate = $("#MaxOffLeaseActulDate").val();
            submitData.page = page;
            transmitData(datatostr(submitData));
            return;
        }

        function submit1() {
            if ($("#ContractType").val() == "") {
                layer.msg("请选择合同类型！", { icon: 7, time: 1000 });
                $("#ContractType").focus();
                return;
            }
            if ($("#ContractSPNo").val() == "") {
                layer.msg("请选择合同主体！", { icon: 7, time: 1000 });
                $("#ContractSPNo").focus();
                return;
            }
            if ($("#ContractCustNo").val() == "") {
                layer.msg("请选择客户！", { icon: 7, time: 1000 });
                $("#ContractCustNo").focus();
                return;
            }
            if ($("#ContractSignedDate").val() == "") {
                layer.msg("请选择合同签订日期！", { icon: 7, time: 1000 });
                $("#ContractSignedDate").focus();
                return;
            }
            if ($("#ContractStartDate").val() == "") {
                layer.msg("请选择合同生效日期！", { icon: 7, time: 1000 });
                $("#ContractStartDate").focus();
                return;
            }
            if ($("#ContractEndDate").val() == "") {
                layer.msg("请选择合同到期日期！", { icon: 7, time: 1000 });
                $("#ContractEndDate").focus();
                return;
            }
            var submitData = new Object();
            submitData.Type = "submit";
            submitData.submittp = "submit1";
            submitData.id = id;

            submitData.ContractNo = $("#ContractNo").val();
            submitData.ContractType = $("#ContractType").val();
            submitData.ContractSPNo = $("#ContractSPNo").val();
            submitData.ContractCustNo = $("#ContractCustNo").val();
            submitData.ContractNoManual = $("#ContractNoManual").val();
            submitData.ContractHandler = $("#ContractHandler").val();
            submitData.ContractSignedDate = $("#ContractSignedDate").val();
            submitData.ContractStartDate = $("#ContractStartDate").val();
            submitData.ContractEndDate = $("#ContractEndDate").val();
            submitData.EntryDate = $("#EntryDate").val();
            submitData.FeeStartDate = $("#FeeStartDate").val();
            submitData.ReduceStartDate1 = $("#ReduceStartDate1").val();
            submitData.ReduceEndDate1 = $("#ReduceEndDate1").val();
            submitData.ReduceStartDate2 = $("#ReduceStartDate2").val();
            submitData.ReduceEndDate2 = $("#ReduceEndDate2").val();
            submitData.ReduceStartDate3 = $("#ReduceStartDate3").val();
            submitData.ReduceEndDate3 = $("#ReduceEndDate3").val();
            submitData.ReduceStartDate4 = $("#ReduceStartDate4").val();
            submitData.ReduceEndDate4 = $("#ReduceEndDate4").val();
            submitData.ContractLatefeeRate = $("#ContractLatefeeRate").val();
            submitData.RMRentalDeposit = $("#RMRentalDeposit").val();
            submitData.RMUtilityDeposit = $("#RMUtilityDeposit").val();
            submitData.PropertyFeeStartDate = $("#PropertyFeeStartDate").val();
            submitData.PropertyFeeReduceStartDate = $("#PropertyFeeReduceStartDate").val();
            submitData.PropertyFeeReduceEndDate = $("#PropertyFeeReduceEndDate").val();
            submitData.WaterUnitPrice = $("#WaterUnitPrice").val();
            submitData.ElecticityUintPrice = $("#ElecticityUintPrice").val();
            submitData.AirconUnitPrice = $("#AirconUnitPrice").val();
            submitData.PropertyUnitPrice = $("#PropertyUnitPrice").val();
            submitData.SharedWaterFee = $("#SharedWaterFee").val();
            submitData.SharedElectricyFee = $("#SharedElectricyFee").val();
            submitData.WPRentalDeposit = $("#WPRentalDeposit").val();
            submitData.WPUtilityDeposit = $("#WPUtilityDeposit").val();
            submitData.WPQTY = $("#WPQTY").val();
            submitData.WPElectricyLimit = $("#WPElectricyLimit").val();
            submitData.WPOverElectricyPrice = $("#WPOverElectricyPrice").val();
            submitData.BBQTY = $("#BBQTY").val();
            submitData.BBAmount = $("#BBAmount").val();
            submitData.IncreaseType = $("#IncreaseType").val();
            submitData.IncreaseStartDate1 = $("#IncreaseStartDate1").val();
            submitData.IncreaseRate1 = $("#IncreaseRate1").val();
            submitData.IncreaseStartDate2 = $("#IncreaseStartDate2").val();
            submitData.IncreaseRate2 = $("#IncreaseRate2").val();
            submitData.IncreaseStartDate3 = $("#IncreaseStartDate3").val();
            submitData.IncreaseRate3 = $("#IncreaseRate3").val();
            submitData.IncreaseStartDate4 = $("#IncreaseStartDate4").val();
            submitData.IncreaseRate4 = $("#IncreaseRate4").val();
            submitData.Remark = $("#Remark").val();
            submitData.ContractAttachment = $("#ContractAttachment").val();

            submitData.tp = type;
            submitData.copyid = copyid;
            submitData.ContractNoS = $("#ContractNoS").val();
            submitData.ContractNoManualS = $("#ContractNoManualS").val();
            submitData.ContractTypeS = "02";
            submitData.ContractSPNoS = $("#ContractSPNoS").val();
            submitData.ContractCustNoS = $("#ContractCustNoS").val();
            submitData.MinContractSignedDate = $("#MinContractSignedDate").val();
            submitData.MaxContractSignedDate = $("#MaxContractSignedDate").val();
            submitData.MinContractEndDate = $("#MinContractEndDate").val();
            submitData.MaxContractEndDate = $("#MaxContractEndDate").val();
            submitData.ContractStatusS = $("#ContractStatusS").val();
            submitData.OffLeaseStatusS = $("#OffLeaseStatusS").val();
            submitData.MinOffLeaseActulDate = $("#MinOffLeaseActulDate").val();
            submitData.MaxOffLeaseActulDate = $("#MaxOffLeaseActulDate").val();
            submitData.page = page;
            transmitData(datatostr(submitData));
            return;
        }

        function clone() {
            if ($("#selectKey").val() == "") {
                layer.msg("请先选择一条记录", { icon: 3, time: 1000 });
                return;
            }
            id = $("#selectKey").val();
            type = "insert";
            var submitData = new Object();
            submitData.Type = "clone";
            submitData.id = id;

            transmitData(datatostr(submitData));
            return;
        }

        function del() {
            if ($("#selectKey").val() == "") {
                layer.msg("请先选择一条记录", { icon: 3, time: 1000 });
                return;
            }
            layer.confirm('确认要删除吗？', function (index) {
                var submitData = new Object();
                submitData.Type = "delete";
                submitData.id = $("#selectKey").val();

                submitData.ContractNoS = $("#ContractNoS").val();
                submitData.ContractNoManualS = $("#ContractNoManualS").val();
                submitData.ContractTypeS = "02";
                submitData.ContractSPNoS = $("#ContractSPNoS").val();
                submitData.ContractCustNoS = $("#ContractCustNoS").val();
                submitData.MinContractSignedDate = $("#MinContractSignedDate").val();
                submitData.MaxContractSignedDate = $("#MaxContractSignedDate").val();
                submitData.MinContractEndDate = $("#MinContractEndDate").val();
                submitData.MaxContractEndDate = $("#MaxContractEndDate").val();
                submitData.ContractStatusS = $("#ContractStatusS").val();
                submitData.OffLeaseStatusS = $("#OffLeaseStatusS").val();
                submitData.MinOffLeaseActulDate = $("#MinOffLeaseActulDate").val();
                submitData.MaxOffLeaseActulDate = $("#MaxOffLeaseActulDate").val();
                submitData.page = page;
                transmitData(datatostr(submitData));
                layer.close(index);
            });
            return;
        }

        function approve() {
            if ($("#selectKey").val() == "") {
                layer.msg("请先选择一条记录", { icon: 3, time: 1000 });
                return;
            }
            layer.confirm('确认要审核吗？', function (index) {
                var submitData = new Object();
                submitData.Type = "approve";
                submitData.id = $("#selectKey").val();
                transmitData(datatostr(submitData));
                layer.close(index);
            });
            return;
        }

        function invalid() {
            if ($("#selectKey").val() == "") {
                layer.msg("请先选择一条记录", { icon: 3, time: 1000 });
                return;
            }
            layer.confirm('确认要作废吗？', function (index) {
                var submitData = new Object();
                submitData.Type = "invalid";
                submitData.id = $("#selectKey").val();
                transmitData(datatostr(submitData));
                layer.close(index);
            });
            return;
        }

        function select() {
            var submitData = new Object();
            submitData.Type = "select";
            submitData.ContractNoS = $("#ContractNoS").val();
            submitData.ContractNoManualS = $("#ContractNoManualS").val();
            submitData.ContractTypeS = "02";
            submitData.ContractSPNoS = $("#ContractSPNoS").val();
            submitData.ContractCustNoS = $("#ContractCustNoS").val();
            submitData.MinContractSignedDate = $("#MinContractSignedDate").val();
            submitData.MaxContractSignedDate = $("#MaxContractSignedDate").val();
            submitData.MinContractEndDate = $("#MinContractEndDate").val();
            submitData.MaxContractEndDate = $("#MaxContractEndDate").val();
            submitData.ContractStatusS = $("#ContractStatusS").val();
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

        function uploadFiles() {
            var temp = "../Base/Upload.html?action=upload&id=uploadFiles";
            layer_show("文件上传", temp, 400, 200);
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


        function itemsave2() {
            if ($("#RMID2").val() == "") {
                layer.msg("请选择房间编号！", { icon: 7, time: 1500 });
                $("#RMID2").focus();
                return;
            }
            if ($("#WPNo").val() == "") {
                layer.msg("请选择工位编号！", { icon: 7, time: 1500 });
                $("#WPNo").focus();
                return;
            }
            if ($("#SRVNo2").val() == "") {
                layer.msg("请选择所属费用项目！", { icon: 7, time: 1500 });
                $("#SRVNo2").focus();
                return;
            }
            if ($("#WPQTY2").val() == "") {
                layer.msg("请填写工位数量！", { icon: 7, time: 1500 });
                $("#WPQTY2").focus();
                return;
            }
            if ($("#WPRentalUnitPrice").val() == "") {
                layer.msg("请填写工位单价！", { icon: 7, time: 1500 });
                $("#WPRentalUnitPrice").focus();
                return;
            }
            var submitData = new Object();
            submitData.Type = "itemsave2";
            submitData.id = id;
            submitData.itemid = itemid2;
            submitData.itemtp = itemtp2;
            submitData.RMID = $("#RMID2").val();
            submitData.WPNo = $("#WPNo").val();
            submitData.SRVNo = $("#SRVNo2").val();
            submitData.WPQTY = $("#WPQTY2").val();
            submitData.WPRentalUnitPrice = $("#WPRentalUnitPrice").val();
            submitData.RMLoc = $("#RMLoc2").val();
            submitData.Remark = $("#Remark2").val();

            if ($("#IsFixedAmt").prop("checked"))
                submitData.IsFixedAmt = "true";
            else
                submitData.IsFixedAmt = "false";
            submitData.Amount = $("#Amount").val();
            submitData.IncreaseType = $("#IncreaseType").val();
            submitData.IncreaseStartDate1 = $("#IncreaseStartDate1").val();
            submitData.IncreaseRate1 = $("#IncreaseRate1").val();
            submitData.IncreaseStartDate2 = $("#IncreaseStartDate2").val();
            submitData.IncreaseRate2 = $("#IncreaseRate2").val();
            submitData.IncreaseStartDate3 = $("#IncreaseStartDate3").val();
            submitData.IncreaseRate3 = $("#IncreaseRate3").val();
            submitData.IncreaseStartDate4 = $("#IncreaseStartDate4").val();
            submitData.IncreaseRate4 = $("#IncreaseRate4").val();

            transmitData(datatostr(submitData));
            return;
        }

        function itemupdate2(itemid) {
            var submitData = new Object();
            submitData.Type = "itemupdate2";
            submitData.itemid = itemid;
            transmitData(datatostr(submitData));
            return;
        }

        function itemdel2(itemid) {
            layer.confirm('确认要删除吗？', function (index) {
                var submitData = new Object();
                submitData.Type = "itemdel2";
                submitData.id = id;
                submitData.itemid = itemid;
                transmitData(datatostr(submitData));
                layer.close(index);
            });
            return;
        }

        function itemclear2() {
            itemtp2 = "insert";
            itemid2 = "";
            $("#RMID2").val("");
            $("#WPNo").val("");
            $("#SRVNo2").val("<%=WPRentSRVNo %>");
            $("#WPQTY2").val("");
            $("#WPRentalUnitPrice").val("<%=WPRentFee %>");
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
            submitData.ContractTypeS = "02";
            submitData.ContractSPNoS = $("#ContractSPNoS").val();
            submitData.ContractCustNoS = $("#ContractCustNoS").val();
            submitData.MinContractSignedDate = $("#MinContractSignedDate").val();
            submitData.MaxContractSignedDate = $("#MaxContractSignedDate").val();
            submitData.MinContractEndDate = $("#MinContractEndDate").val();
            submitData.MaxContractEndDate = $("#MaxContractEndDate").val();
            submitData.ContractStatusS = $("#ContractStatusS").val();
            submitData.OffLeaseStatusS = $("#OffLeaseStatusS").val();
            submitData.MinOffLeaseActulDate = $("#MinOffLeaseActulDate").val();
            submitData.MaxOffLeaseActulDate = $("#MaxOffLeaseActulDate").val();
            submitData.page = page;
            transmitData(datatostr(submitData));
            return;
        }
        $("#SRVNo2").change(function () {
            var submitData = new Object();
            submitData.Type = "getprice2";
            submitData.SRVNo = $("#SRVNo2").val();
            transmitData(datatostr(submitData));
        });

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

        $("#ContractCustImg").click(function () {
            ChooseBasic("ContractCustNo", "Cust");
        });

        $("#chooseRMID2").click(function () {
            var temp = "../Base/ChooseRMID.aspx?id=RMID2";
            layer_show("选择页面", temp, 800, 630);
        });

        $("#chooseWPNo").click(function () {
            if ($("#RMID2").val() == "") {
                layer.msg("请先选择房间编号", { icon: 3, time: 1000 });
                return;
            }
            var temp = "../Base/ChooseWPNo.aspx?id=WPNo&RMID=" + $("#RMID2").val();
            layer_show("选择页面", temp, 800, 600);
        });
        $("#Amount").bind("input propertychange change", function (event) {    
            $("#IncreaseRate1").val($("#Amount").val());
            $("#IncreaseRate2").val($("#Amount").val());
            $("#IncreaseRate3").val($("#Amount").val());
            $("#IncreaseRate4").val($("#Amount").val());
        });
        $("#IsFixedAmt").change(function () {
            if ($("#IsFixedAmt").is(":checked")) {
                $("#Amount").removeAttr("disabled");
                $("#IncreaseRate1").attr("disabled", "disabled");
                $("#IncreaseRate2").attr("disabled", "disabled");
                $("#IncreaseRate3").attr("disabled", "disabled");
                $("#IncreaseRate4").attr("disabled", "disabled");
            } else {
                $("#Amount").attr("disabled", "disabled");
                $("#IncreaseRate1").removeAttr("disabled");
                $("#IncreaseRate2").removeAttr("disabled");
                $("#IncreaseRate3").removeAttr("disabled");
                $("#IncreaseRate4").removeAttr("disabled");
            }
        });
        function choose(id, labels, values) {
            if (labels != "" && labels != undefined && labels != "undefined") {
                if (id == "ContractCustNo") {
                    $("#ContractCustName").val(values);
                    $("#ContractCustNo").val(labels);
                }
                else if (id == "RMID2") {
                    $("#" + id).val(labels);
                }
                else if (id == "WPNo") {
                    $("#" + id).val(labels);

                    var submitData = new Object();
                    submitData.Type = "getwpno";
                    submitData.wpid = $("#" + id).val();
                    transmitData(datatostr(submitData));
                }
            }
        }

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
           
                $("#Amount").attr("disabled", "disabled");
            
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

            var ReduceStartDate1 = new JsInputDate("ReduceStartDate1");
            ReduceStartDate1.setDisabled(false);
            ReduceStartDate1.setWidth("130px");
            var ReduceEndDate1 = new JsInputDate("ReduceEndDate1");
            ReduceEndDate1.setDisabled(false);
            ReduceEndDate1.setWidth("130px");

            var ReduceStartDate2 = new JsInputDate("ReduceStartDate2");
            ReduceStartDate2.setDisabled(false);
            ReduceStartDate2.setWidth("130px");
            var ReduceEndDate = new JsInputDate("ReduceEndDate2");
            ReduceEndDate2.setDisabled(false);
            ReduceEndDate2.setWidth("130px");

            var ReduceStartDate3 = new JsInputDate("ReduceStartDate3");
            ReduceStartDate3.setDisabled(false);
            ReduceStartDate3.setWidth("130px");
            var ReduceEndDate3 = new JsInputDate("ReduceEndDate3");
            ReduceEndDate3.setDisabled(false);
            ReduceEndDate3.setWidth("130px");

            var ReduceStartDate4 = new JsInputDate("ReduceStartDate4");
            ReduceStartDate4.setDisabled(false);
            ReduceStartDate4.setWidth("130px");
            var ReduceEndDate4 = new JsInputDate("ReduceEndDate4");
            ReduceEndDate4.setDisabled(false);
            ReduceEndDate4.setWidth("130px");

            //var PropertyFeeStartDate = new JsInputDate("PropertyFeeStartDate");
            //PropertyFeeStartDate.setDisabled(false);
            //PropertyFeeStartDate.setWidth("130px");
            //var PropertyFeeReduceStartDate = new JsInputDate("PropertyFeeReduceStartDate");
            //PropertyFeeReduceStartDate.setDisabled(false);
            //PropertyFeeReduceStartDate.setWidth("130px");
            //var PropertyFeeReduceEndDate = new JsInputDate("PropertyFeeReduceEndDate");
            //PropertyFeeReduceEndDate.setDisabled(false);
            //PropertyFeeReduceEndDate.setWidth("130px");

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
