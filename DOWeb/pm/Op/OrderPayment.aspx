<%@ Page Language="C#" AutoEventWireup="true" Inherits="project.Presentation.Op.OrderPayment,project"  %>
<!DOCTYPE>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="head1" runat="server">
    <title>订单管理</title>    
    <!--[if lt IE 9]>
    <script type="text/javascript" src="../../jscript/html5.js"></script>
    <script type="text/javascript" src="../../jscript/respond.min.js"></script>
    <![endif]-->
    <link href="../../css/H-ui.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/H-ui.admin.css" rel="stylesheet" type="text/css" />
    <link href="../../lib/iconfont/iconfont.css" rel="stylesheet" type="text/css" />
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/JsInput.css" rel="stylesheet" type="text/css" />
    <link href="../../css/jquery.monthpicker.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function transmitData(submitData) {
            var data = submitData;
            <%=ClientScript.GetCallbackEventReference(this, "data", "BandResuleData", null) %>
        }
    </script>
</head>
<body>
    <form id="form1" runat="server"></form>
    <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 订单管理 <span class="c-gray en">&gt;</span> 订单收款<a class="btn btn-success radius r mr-20" style="line-height:1.6em;margin-top:2px" href="javascript:location.replace(location.href);" title="刷新" ><i class="Hui-iconfont">&#xe68f;</i></a></nav>
    <div id="list" class="pt-5 pr-20 pb-5 pl-20">
	    <div class="cl pd-3 bg-1 bk-gray mt-2"> 
            <span class="l">
                <%--<a href="javascript:;" onclick="view()" class="btn btn-secondary radius"><i class="Hui-iconfont">&#xe627;</i> 查看订单详情</a>&nbsp;&nbsp;&nbsp;
                <a href="javascript:;" onclick="viewfee()" class="btn btn-secondary radius"><i class="Hui-iconfont">&#xe627;</i> 查看收款记录</a>&nbsp;&nbsp;&nbsp;
                <a href="javascript:;" onclick="fee()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe615;</i> 收款</a>--%>
                <%=Buttons %>
                <input type="hidden" id="selectKey" />
            </span> 
	    </div>
	    <div class="cl pd-10  bk-gray mt-2"> 
		    订单编号&nbsp;<input type="text" class="input-text size-MINI" placeholder="订单编号" id="OrderNoS" style="width:110px" />
            订单类型&nbsp;<%=OrderTypeStrS %>
            客户&nbsp;<input type="text" class="input-text size-MINI" placeholder="" id="CustNoS" style="width:110px" />
            状态&nbsp;<select class="input-text size-MINI" id="OrderStatusS" style="width:110px">
                        <option value="ALL">全部</option>
                        <option value="1">审核通过</option>
                        <option value="2">部分收款</option>
                        <option value="3">完成收款</option>
                    </select>
            主体&nbsp;<%=serviceProvider %>
            <br />
            所属年月&nbsp;<input type="text" class="input-text size-MINI" id="OrderTimeS" style="width:110px" readonly="readonly" /> 
            制单日期 从&nbsp;<input type="text" class="input-text size-MINI" id="MinOrderCreateDate" style="width:110px" />
            至&nbsp;<input type="text" class="input-text size-MINI" id="MaxOrderCreateDate" style="width:110px" />
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
                <li><a href="javascript:void(0)" onclick="show(2)" id="itemtab2">费用明细</a></li>
  	        </ul>
        </div>

        <div id="topeditdiv">
            <table class="tabedit">
                <tr>
                    <td class="tdl1">订单编号</td>
                    <td class="tdr1"><input type="text" id="OrderNo" disabled="disabled" class="input-text size-MINI" /></td>
                    <td class="tdl1">订单类型</td>
                    <td class="tdr1"><%=OrderTypeStr %></td>
                    <td class="tdl1">订单状态</td>
                    <td class="tdr1"><input type="text" id="OrderStatus" class="input-text size-MINI" disabled="disabled" /></td>
                </tr>
                <tr>
                    <td class="tdl1">客户</td>
                    <td class="tdr1">
                        <input type="text" class="input-text size-MINI" id="CustName" onblur="checkcust()" />
                        <input type="hidden" id="CustNo" />
                        <img id="CustImg" alt="" src="../../images/view_detail.png" class="view_detail" />
                    </td>
                    <td class="tdl1">所属月份</td>
                    <td class="tdr1"><input type="text" id="OrderTime" class="input-text size-MINI" readonly="readonly" /></td>
                    <td class="tdl1"></td>
                    <td class="tdr1"></td>
                </tr>
                <tr>
                    <td class="tdl1">应收日期</td>
                    <td class="tdr1"><input type="text" id="ARDate" class="input-text size-MINI" /></td>
                    <td class="tdl1">当月天数</td>
                    <td class="tdr1"><input type="text" id="DaysofMonth" class="input-text size-MINI" disabled="disabled" /></td>
                    <td class="tdl1"></td>
                    <td class="tdr1"></td>
                </tr>
                <tr>
                    <td class="tdl">备注</td>
                    <td class="tdr" colspan="7"><textarea cols="" rows="3" class="textarea required" id="Remark"></textarea></td>
                </tr>
                <tr>
                    <td class="tdl1">制单人</td>
                    <td class="tdr1"><input type="text" id="OrderCreator" class="input-text size-MINI" disabled="disabled" /></td>
                    <td class="tdl1">制单日期</td>
                    <td class="tdr1"><input type="text" id="OrderCreateDate" class="input-text size-MINI" disabled="disabled" /></td>
                    <td class="tdl1"></td>
                    <td class="tdr1"></td>
                </tr>
                <tr>
                    <td class="tdl1">审核人</td>
                    <td class="tdr1"><input type="text" id="OrderLastReviser" class="input-text size-MINI" disabled="disabled" /></td>
                    <td class="tdl1">审核日期</td>
                    <td class="tdr1"><input type="text" id="OrderLastRevisedDate" class="input-text size-MINI" disabled="disabled" /></td>
                    <td class="tdl1">不通过原因</td>
                    <td class="tdr1"><input type="text" id="OrderAuditReason" class="input-text size-MINI" disabled="disabled" /></td>
                </tr>
                <tr>
                    <td class="tdl1">反审核人</td>
                    <td class="tdr1"><input type="text" id="OrderRAuditor" class="input-text size-MINI" disabled="disabled" /></td>
                    <td class="tdl1">反审核日期</td>
                    <td class="tdr1"><input type="text" id="OrderRAuditDate" class="input-text size-MINI" disabled="disabled" /></td>
                    <td class="tdl1">反审核原因</td>
                    <td class="tdr1"><input type="text" id="OrderRAuditReason" class="input-text size-MINI" disabled="disabled" /></td>
                </tr>
                <tr>
                    <td class="tdl1">应收总金额</td>
                    <td class="tdr1"><input type="text" id="ARAmount" class="input-text size-MINI" disabled="disabled" /></td>
                    <td class="tdl1">减免总金额</td>
                    <td class="tdr1"><input type="text" id="ReduceAmount" class="input-text size-MINI" disabled="disabled" /></td>
                    <td class="tdl1">实收总金额</td>
                    <td class="tdr1"><input type="text" id="PaidinAmount" class="input-text size-MINI" disabled="disabled" /></td>
                </tr>
            </table>
        </div>
        <div id="bodyeditdiv1">        
            <table class="tabedit">
                <tr>
                    <td class="tdl1">订单服务项</td>
                    <td class="tdr1"><%=ODSRVNoStr %></td>
                    <td class="tdl1">服务项说明</td>
                    <td class="tdr1"><input type="text" id="ODSRVRemark" class="input-text size-MINI" /></td>
                    <td class="tdl1">订单主体</td>
                    <td class="tdr1"><%=ODContractSPNoStr %></td>
                </tr>
                <tr>
                    <td class="tdl1">订单编号</td>
                    <td class="tdr1"><input type="text" id="ODContractNo" class="input-text size-MINI" /></td>
                    <td class="tdl1">手工订单编号</td>
                    <td class="tdr1"><input type="text" id="ODContractNoManual" class="input-text size-MINI" /></td>
                    <td class="tdl1">收费方式</td>
                    <td class="tdr1">
                        <select class="input-text size-MINI" id="ODSRVCalType">
                            <option value="1">按出租面积</option>
                            <option value="2">按使用量</option>
                            <option value="3">按天数</option>
                            <option value="4">按次数</option>
                            <option value="5">滞纳</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td class="tdl1">资源编号</td>
                    <td class="tdr1"><input type="text" id="ResourceNo" class="input-text size-MINI" /></td>
                    <td class="tdl1">资源名称</td>
                    <td class="tdr1"><input type="text" id="ResourceName" class="input-text size-MINI" /></td>
                    <td class="tdl1">单位</td>
                    <td class="tdr1"><input type="text" id="ODUnit" class="input-text size-MINI" /></td>
                </tr>
                <tr>
                    <td class="tdl1">计费开始日期</td>
                    <td class="tdr1"><input type="text" id="ODFeeStartDate" class="input-text size-MINI" onchange="ODFeeStartDateChange()" /></td>
                    <td class="tdl1">计费结束日期</td>
                    <td class="tdr1"><input type="text" id="ODFeeEndDate" class="input-text size-MINI" onchange="ODFeeStartDateChange()" /></td>
                    <td class="tdl1">计费天数</td>
                    <td class="tdr1"><input type="text" id="BillingDays" class="input-text size-MINI" disabled="disabled" /></td>
                </tr>
                <tr>
                    <td class="tdl1">数量</td>
                    <td class="tdr1"><input type="text" id="ODQTY" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl1">单价</td>
                    <td class="tdr1"><input type="text" id="ODUnitPrice" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl1">应收金额</td>
                    <td class="tdr1"><input type="text" id="ODARAmount" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                </tr>
                <tr>
                    <td colspan="6">
                        <input class="btn btn-primary radius" type="button" id="itemsave1" onclick="itemsave1()" value="保存" />&nbsp;
	                    <input class="btn btn-primary radius" type="button" id="itemclear1" onclick="itemclear1()" value="清空" />&nbsp;
                    </td>
                </tr>
            </table>
            <div style="width:100%;height:5px;"></div>
            <div id="itemlist1"style="width:1058px; height:320px; overflow:auto; margin:0px; padding:0px;"></div>
        </div>
        
        <div style="margin-top:10px;">
            <input class="btn btn-primary radius" type="button" id="submit" onclick="submit()" value="保存退出" />
            <input class="btn btn-primary radius" type="button" id="submit1" onclick="submit1()" value="保存继续" />
	        <input class="btn btn-default radius" type="button" id="cancel" onclick="cancel()" value="&nbsp;&nbsp;取&nbsp;&nbsp;消&nbsp;&nbsp;" />
        </div>
        <br />
        <br />
    </div> 

    <div id="feediv" style="display:none;">
        <table style="width:750px; margin-top:10px;">
            <tr>
                <td style="width:80px; text-align:center; padding:8px;">主体</td>
                <td style="width:670px;" colspan="3"><input type="text" id="ODSPName" class="input-text size-MINI" style="width:490px;" disabled="disabled" /></td>
            </tr>
            <tr>
                <td style="width:80px; text-align:center; padding:8px;">客户名称</td>
                <td style="width:670px;" colspan="3"><input type="text" id="ODCustName" class="input-text size-MINI" style="width:490px;" disabled="disabled" /></td>
            </tr>
            <tr>
                <td style="width:80px; text-align:center; padding:8px;">应收金额</td>
                <td style="width:670px;" colspan="3"><input type="text" id="ODUnAmount" class="input-text size-MINI" style="width:490px;" disabled="disabled" /></td>
            </tr>
            <tr>
                <td style="width:80px; text-align:center; padding:8px;">收款方式</td>
                <td style="width:120px;">
                    <select class="input-text size-MINI" id="ODPaidType">
                        <option value="现金">现金</option>
                        <option value="银行">银行</option>
                        <option value=""></option>
                    </select>
                </td>
                <td style="width:40px; text-align:center; padding:8px;">银行</td>
                <td style="width:200px;"><%=BankStr %></td>
            </tr>
            <tr>
                <td style="width:80px; text-align:center; padding:15px;">收款日期</td>
                <td style="width:670px;" colspan="3"><input type="text" id="ODPaidDate" class="input-text size-MINI" style="width:200px;" /></td>
            </tr>
           <%--<tr>
                <td style="width:80px; text-align:center; padding:8px;">收款金额</td>
                <td style="width:360px;"><input type="text" id="ODPaidAmount" class="input-text size-MINI" style="width:200px;" onchange="validDecimal2(this.id)" /></td>
            </tr>--%>
            <tr>
                <td style="width:80px; text-align:center; padding:15px;">备注</td>
                <td style="width:670px;" colspan="3"><textarea cols="" rows="3" style ="width:490px" class="textarea required" placeholder="备注" id="ODFeeReceiveRemark"></textarea></td>
            </tr>
            <tr>
                <td style="width:750px;" colspan="4"><div id="paylist"></div></td>
            </tr>
        </table>
        <div style="margin:15px 100px;">
            <input class="btn btn-primary radius" type="button" onclick="feesubmit()" value="&nbsp;确&nbsp;定&nbsp;" />&nbsp;&nbsp;
            <input class="btn btn-primary radius" type="button" onclick="feecancel()" value="&nbsp;关&nbsp;闭&nbsp;" />
        </div>
    </div>

    <script type="text/javascript" src="../../jscript/jquery-1.10.2.js"></script> 
    <script type="text/javascript" src="../../jscript/script_common.js"></script>
    <script type="text/javascript" src="../../jscript/JsInputDate.js"></script>
    <script type="text/javascript" src="../../jscript/jquery.monthpicker.js"></script>
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
            if (vjson.type == "view") {
                if (vjson.flag == "1") {
                    $("#OrderNo").val(vjson.OrderNo);
                    $("#OrderType").val(vjson.OrderType);
                    $("#CustNo").val(vjson.CustNo);
                    $("#CustName").val(vjson.CustName);
                    $("#OrderTime").val(vjson.OrderTime);
                    $("#ARDate").val(vjson.ARDate);
                    $("#DaysofMonth").val(vjson.DaysofMonth);
                    $("#OrderCreator").val(vjson.OrderCreator);
                    $("#OrderCreateDate").val(vjson.OrderCreateDate);
                    $("#OrderAuditReason").val(vjson.OrderAuditReason);
                    $("#OrderRAuditor").val(vjson.OrderRAuditor);
                    $("#OrderRAuditDate").val(vjson.OrderRAuditDate);
                    $("#OrderRAuditReason").val(vjson.OrderRAuditReason);
                    $("#ARAmount").val(vjson.ARAmount);
                    $("#ReduceAmount").val(vjson.ReduceAmount);
                    $("#PaidinAmount").val(vjson.PaidinAmount);
                    $("#OrderStatus").val(vjson.OrderStatus);

                    copyid = "";
                    show(1);
                    itemclear1();
                    $("#itemlist1").html(vjson.itemlist1);

                    $("#list").css("display", "none");
                    $("#edit").css("display", "");

                    $("#itemsave1").css("display", "none");
                    $("#itemclear1").css("display", "none");
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
                        title: "收款记录",
                        content: vjson.feelist
                    });
                }
                else {
                    layer.alert("提取数据出错！");
                }
                return;
            }
            if (vjson.type == "getfee") {
                if (vjson.flag == "1") {
                    $("#ODCustName").val(vjson.ODCustName);
                    $("#ODSPName").val(vjson.ODSPName);
                    $("#ODUnAmount").val(vjson.ODUnAmount);
                    //$("#ODPaidAmount").val("");
                    $("#ODPaidDate").val("<%=date %>");
                    $("#ODFeeReceiveRemark").val("");
                    $("#ODPaidType").val("");
                    $("#ODPaidBank").val("");
                    $("#paylist").html(vjson.paylist);
                    layer.open({
                        type: 1,
                        area: ["750px", "600px"],
                        fix: true,
                        maxmin: true,
                        scrollbar: false,
                        shade: 0.5,
                        title: "收款",
                        content: $("#feediv")
                    });
                }
                else if (vjson.flag == "6") {
                    layer.alert("当前订单没有明细，不能审核！");
                }
                return;
            }
            if (vjson.type == "feesubmit") {
                layer.closeAll("loading");
                if (vjson.flag == "1") {
                    layer.closeAll();
                    $("#outerlist").html(vjson.liststr);
                    $("#selectKey").val("");
                    reflist();
                    layer.alert("收款成功！");
                }
                else if (vjson.flag == "4") {
                    layer.alert(vjson.info);
                }
                else {
                    layer.alert("提取数据出错！");
                }
                return;
            }

            if (vjson.type == "itemupdate1") {
                if (vjson.flag == "1") {
                    $("#ODSRVNo").val(vjson.ODSRVNo);
                    $("#ODSRVRemark").val(vjson.ODSRVRemark);
                    $("#ODContractSPNo").val(vjson.ODContractSPNo);
                    $("#RentalUnitPrice").val(vjson.RentalUnitPrice);
                    $("#ODContractNo").val(vjson.ODContractNo);
                    $("#ODContractNoManual").val(vjson.ODContractNoManual);
                    $("#ResourceNo").val(vjson.ResourceNo);
                    $("#ResourceName").val(vjson.ResourceName);
                    $("#ODSRVCalType").val(vjson.ODSRVCalType);
                    $("#ODFeeStartDate").val(vjson.ODFeeStartDate);
                    $("#ODFeeEndDate").val(vjson.ODFeeEndDate);
                    $("#BillingDays").val(vjson.BillingDays);
                    $("#ODQTY").val(vjson.ODQTY);
                    $("#ODUnit").val(vjson.ODUnit);
                    $("#ODUnitPrice").val(vjson.ODUnitPrice);
                    $("#ODARAmount").val(vjson.ODARAmount);

                    itemid1 = vjson.ItemId;
                    itemtp1 = "update";
                }
                else {
                    layer.alert("修改数据出错！");
                }
                return;
            }

            if (vjson.type == "feedelete") {
                if (vjson.flag == "1") {
                    $("#outerlist").html(vjson.liststr);
                    //$("#selectKey").val("");
                    reflist();
                    $("#" + $("#selectKey").val()).addClass('active');

                    layer.closeAll();
                    layer.open({
                        type: 1,
                        area: ["900px", "600px"],
                        fix: true,
                        maxmin: true,
                        scrollbar: false,
                        shade: 0.5,
                        title: "收款记录",
                        content: vjson.feelist
                    });
                }
                else {
                    layer.alert("修改数据出错！");
                }
                return;
            }
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
            submitData.OrderNoS = $("#OrderNoS").val();
            submitData.OrderTypeS = $("#OrderTypeS").val();
            submitData.CustNoS = $("#CustNoS").val();
            submitData.OrderStatusS = $("#OrderStatusS").val();
            submitData.OrderTimeS = $("#OrderTimeS").val();
            submitData.MinOrderCreateDate = $("#MinOrderCreateDate").val();
            submitData.MaxOrderCreateDate = $("#MaxOrderCreateDate").val();
            submitData.SPNo = $("#SPNo").val();
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
        function fee() {
            if ($("#selectKey").val() == "") {
                layer.msg("请先选择一条记录", { icon: 3, time: 1000 });
                return;
            }
            var submitData = new Object();
            submitData.Type = "getfee";
            submitData.id = $("#selectKey").val();
            transmitData(datatostr(submitData));
            return;
        }
        function feesubmit() {
            if ($("#ODPaidType").val() == "" || $("#ODPaidType").val() == null) {
                layer.msg("请选择收款方式！", { icon: 7, time: 1000 });
                $("#ODPaidType").focus();
                return;
            }
            if ($("#ODPaidType").val() == "银行" && $("#ODPaidBank").val() == "") {
                layer.msg("请选择银行！", { icon: 7, time: 1000 });
                $("#ODPaidBank").focus();
                return;
            }
            if ($("#ODPaidDate").val() == "") {
                layer.msg("请选择收款日期！", { icon: 7, time: 1000 });
                $("#ODPaidDate").focus();
                return;
            }
            //if ($("#ODPaidAmount").val() == "") {
            //    layer.msg("请输入收款金额！", { icon: 7, time: 1000 });
            //    $("#ODPaidAmount").focus();
            //    return;
            //}

            var amount = 0;
            var ids = "";
            var checklist = jQuery("#tbody input:checkbox");
            for (var i = 0; i < checklist.length; i++) {
                var id = checklist[i].value;

                if ($("#amt" + id).val() != "") {
                    if (parseFloat($("#amt" + id).val()) > 0) {
                        ids += id + ":" + $("#amt" + id).val() + ";";
                        amount = amount + parseFloat($("#amt" + id).val());
                    }
                }
            }
            if (amount <= 0) {
                layer.msg("请填写收款金额！", { icon: 7, time: 1000 });
                return;
            }

            layer.load(2);
            var submitData = new Object();
            submitData.Type = "feesubmit";
            submitData.id = $("#selectKey").val();
            //submitData.ODPaidAmount = $("#ODPaidAmount").val();
            submitData.ODPaidDate = $("#ODPaidDate").val();
            submitData.ODFeeReceiveRemark = $("#ODFeeReceiveRemark").val();
            submitData.ODPaidType = $("#ODPaidType").val();
            submitData.ODPaidBank = $("#ODPaidBank").val();

            submitData.ids = ids;

            submitData.OrderNoS = $("#OrderNoS").val();
            submitData.OrderTypeS = $("#OrderTypeS").val();
            submitData.CustNoS = $("#CustNoS").val();
            submitData.OrderStatusS = $("#OrderStatusS").val();
            submitData.OrderTimeS = $("#OrderTimeS").val();
            submitData.MinOrderCreateDate = $("#MinOrderCreateDate").val();
            submitData.MaxOrderCreateDate = $("#MaxOrderCreateDate").val();
            submitData.SPNo = $("#SPNo").val();
            submitData.page = page;
            transmitData(datatostr(submitData));
            return;
        }
        function feecancel() {
            layer.closeAll();
        }

        function feedelete(id) {
            layer.confirm('确认要删除吗？', function (index) {
                var submitData = new Object();
                submitData.Type = "feedelete";
                submitData.id = $("#selectKey").val();
                submitData.itemid = id;

                submitData.OrderNoS = $("#OrderNoS").val();
                submitData.OrderTypeS = $("#OrderTypeS").val();
                submitData.CustNoS = $("#CustNoS").val();
                submitData.OrderStatusS = $("#OrderStatusS").val();
                submitData.OrderTimeS = $("#OrderTimeS").val();
                submitData.MinOrderCreateDate = $("#MinOrderCreateDate").val();
                submitData.MaxOrderCreateDate = $("#MaxOrderCreateDate").val();
                submitData.SPNo = $("#SPNo").val();
                submitData.page = page;
                transmitData(datatostr(submitData));
                layer.close(index);
            });
            return;
        }

        $("#ODPaidType").change(function () {
            if ($("#ODPaidType").val() == "现金") {
                $("#ODPaidBank").val("");
                $("#ODPaidBank").prop("disabled", true);
            }
            else {
                $("#ODPaidBank").prop("disabled", false);
            }
            return;
        });

        function caluamount() {
            
            var amount = 0;
            var checklist = jQuery("#tbody input:checkbox");
            for (var i = 0; i < checklist.length; i++) {
                var id = checklist[i].value;
                validDecimal2("amt" + id);
                if ($("#amt" + id).val() != "") {
                    amount = amount + parseFloat($("#amt" + id).val());
                }
            }
            $("#PayAmt").html(amount.toFixed(2));
        }

        function itemupdate1(itemid) {
            var submitData = new Object();
            submitData.Type = "itemupdate1";
            submitData.itemid = itemid;
            transmitData(datatostr(submitData));
            return;
        }
        function itemclear1() {
            itemtp1 = "insert";
            itemid1 = "";

            $("#ODSRVNo").val("");
            $("#ODSRVRemark").val("");
            $("#ODContractSPNo").val("");
            $("#ODContractNo").val("");
            $("#ODContractNoManual").val("");
            $("#ResourceNo").val("");
            $("#ResourceName").val("");
            $("#ODSRVCalType").val("");
            $("#ODFeeStartDate").val("");
            $("#ODFeeEndDate").val("");
            $("#BillingDays").val("");
            $("#ODQTY").val("");
            $("#ODUnit").val("");
            $("#ODUnitPrice").val("");
            $("#ODARAmount").val("");
        }

        var page = 1;
        function jump(pageindex) {
            page = pageindex;
            var submitData = new Object();
            submitData.Type = "jump";
            submitData.OrderNoS = $("#OrderNoS").val();
            submitData.OrderTypeS = $("#OrderTypeS").val();
            submitData.CustNoS = $("#CustNoS").val();
            submitData.OrderStatusS = $("#OrderStatusS").val();
            submitData.SPNo = $("#SPNo").val();
            submitData.OrderTimeS = $("#OrderTimeS").val();
            submitData.MinOrderCreateDate = $("#MinOrderCreateDate").val();
            submitData.MaxOrderCreateDate = $("#MaxOrderCreateDate").val();
            submitData.page = page;
            transmitData(datatostr(submitData));
            return;
        }

        function show(page) {
            if (page == 1) {
                $('#itemtab2').removeClass("selected");
                $('#itemtab1').addClass("selected");
                $("#bodyeditdiv1").hide();
                $("#topeditdiv").show();
            }
            else {
                if (id == "") {
                    layer.msg("请先保存表头信息！", { icon: 3, time: 1000 });
                    return;
                }

                $('#itemtab1').removeClass("selected");
                $('#itemtab2').addClass("selected");
                $("#topeditdiv").hide();
                $("#bodyeditdiv1").show();
            }
        }

        function validDecimal2(itemid) {
            var val;
            try {
                val = parseFloat($("#" + itemid).val());
            } catch (r) { }

            if (isNaN(val)) {
                $("#" + itemid).val("");
                return;
            }
            if (val < 0) val = -val;
            if (val.toString().indexOf(".") > 0)
                $("#" + itemid).val(val.toFixed(2));
            else
                $("#" + itemid).val(val);
        }

        jQuery(function () {
            var now = new Date();
            var year = now.getFullYear();
            $('#OrderTimeS').monthpicker({
                years: [2016, 2017, 2018, 2019, 2020, 2021, 2022, 2023, 2024, 2025],
                topOffset: 6,
                year: year
            });

            var ARDate = new JsInputDate("ARDate");

            var MinOrderCreateDate = new JsInputDate("MinOrderCreateDate");
            MinOrderCreateDate.setWidth("100px");
            var MaxOrderCreateDate = new JsInputDate("MaxOrderCreateDate");
            MaxOrderCreateDate.setWidth("100px");

            var ODFeeStartDate = new JsInputDate("ODFeeStartDate");
            var ODFeeEndDate = new JsInputDate("ODFeeEndDate");
            var ODPaidDate = new JsInputDate("ODPaidDate");
        });

        var id = "";
        var tp = "";
        var itemid1 = "";
        var itemtp1 = "";
        var copyid = "";
        var trid = "";
        reflist();
</script>
</body>
</html>