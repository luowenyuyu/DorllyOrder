<%@ Page Language="C#" AutoEventWireup="true" Inherits="project.Presentation.Op.Order,project" %>

<!DOCTYPE>
<html xmlns="http://www.w3.org/1999/xhtml">
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
    <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;&gt;</span> 订单管理 <a class="btn btn-success radius r mr-20" style="line-height: 1.6em; margin-top: 2px" href="javascript:location.replace(location.href);" title="刷新"><i class="Hui-iconfont">&#xe68f;</i></a></nav>
    <div id="list" class="pt-5 pr-20 pb-5 pl-20">
        <div class="cl pd-3 bg-1 bk-gray mt-2">
            <span class="l">
                <%--<a href="javascript:;" onclick="insert()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe600;</i> 添加</a>
                <a href="javascript:;" onclick="update()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe60c;</i> 修改</a> 
                <a href="javascript:;" onclick="del()" class="btn btn-danger radius"><i class="Hui-iconfont">&#xe6e2;</i> 删除</a> 
                <a href="javascript:;" onclick="view()" class="btn btn-secondary radius"><i class="Hui-iconfont">&#xe627;</i> 查看</a>&nbsp;&nbsp;&nbsp;
                <a href="javascript:;" onclick="auditpass()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe615;</i> 审核通过</a>
                <a href="javascript:;" onclick="raudit()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe615;</i> 反审核</a>--%>
                <%--<a href="javascript:;" onclick="auditfailed()" class="btn btn-warning radius"><i class="Hui-iconfont">&#xe60b;</i> 审核不通过</a>--%>
                <%=Buttons %>
                <input type="hidden" id="selectKey" />
            </span>
        </div>
        <div class="cl pd-10  bk-gray mt-2">
            订单编号&nbsp;<input type="text" class="input-text size-MINI" placeholder="订单编号" id="OrderNoS" style="width: 110px" />
            订单类型&nbsp;<%=OrderTypeStrS %>
            客户&nbsp;<input type="text" class="input-text size-MINI" placeholder="" id="CustNoS" style="width: 110px" />
            状态&nbsp;<select class="input-text size-MINI" id="OrderStatusS" style="width: 110px">
                <option value="">全部</option>
                <option value="0">制单</option>
                <option value="1">审核通过</option>
                <option value="2">部分收款</option>
                <option value="3">完成收款</option>
                <option value="-1">审核不通过</option>
            </select>
            主体&nbsp;<%=serviceProvider %>
            <br />
            所属年月&nbsp;<input type="text" class="input-text size-MINI" id="OrderTimeS" style="width: 110px" readonly="readonly" />
            应收日期 从&nbsp;<input type="text" class="input-text size-MINI" id="MinOrderCreateDate" style="width: 110px" />
            至&nbsp;<input type="text" class="input-text size-MINI" id="MaxOrderCreateDate" style="width: 110px" />
            排序字段&nbsp;<select class="input-text size-MINI" id="OrderField" style="width: 110px">
                <option value="OrderNo DESC" selected="selected">订单编号</option>
                <option value="ResourceNo ASC">房间编号</option>
            </select>
            每页行数&nbsp;<select class="input-text size-MINI" id="PageCount" style="width: 90px">
                <option value="15" selected="selected">15</option>
                <option value="30">30</option>
                <option value="60">60</option>
                <option value="90">90</option>
                <option value="120">120</option>
                <option value="150">150</option>
                <option value="200">200</option>
                <option value="ALL">全部</option>
            </select>
            <button type="submit" class="btn btn-success radius" onclick="select()"><i class="Hui-iconfont">&#xe665;</i> 检索</button>
        </div>
        <div class="mt-5" id="outerlist">
            <%=list %>
        </div>
    </div>
    <div id="edit" class="editdiv" style="display: none;">
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
                    <td class="tdr1">
                        <input type="text" id="OrderNo" disabled="disabled" class="input-text size-MINI" /></td>
                    <td class="tdl1">订单类型</td>
                    <td class="tdr1"><%=OrderTypeStr %></td>
                    <td class="tdl1">订单状态</td>
                    <td class="tdr1">
                        <input type="text" id="OrderStatus" class="input-text size-MINI" disabled="disabled" /></td>
                </tr>
                <tr>
                    <td class="tdl1">客户</td>
                    <td class="tdr1">
                        <input type="text" class="input-text size-MINI" id="CustName" onblur="checkcust()" />
                        <input type="hidden" id="CustNo" />
                        <img id="CustImg" alt="" src="../../images/view_detail.png" class="view_detail" />
                    </td>
                    <td class="tdl1">所属月份</td>
                    <td class="tdr1">
                        <input type="text" id="OrderTime" class="input-text size-MINI" readonly="readonly" /></td>
                    <td class="tdl1"></td>
                    <td class="tdr1"></td>
                </tr>
                <tr>
                    <td class="tdl1">应收日期</td>
                    <td class="tdr1">
                        <input type="text" id="ARDate" class="input-text size-MINI" /></td>
                    <td class="tdl1">当月天数</td>
                    <td class="tdr1">
                        <input type="text" id="DaysofMonth" class="input-text size-MINI" disabled="disabled" /></td>
                    <td class="tdl1"></td>
                    <td class="tdr1"></td>
                </tr>
                <tr>
                    <td class="tdl">备注</td>
                    <td class="tdr" colspan="7">
                        <textarea cols="" rows="3" class="textarea required" id="Remark"></textarea></td>
                </tr>
                <tr>
                    <td class="tdl1">制单人</td>
                    <td class="tdr1">
                        <input type="text" id="OrderCreator" class="input-text size-MINI" disabled="disabled" /></td>
                    <td class="tdl1">制单日期</td>
                    <td class="tdr1">
                        <input type="text" id="OrderCreateDate" class="input-text size-MINI" disabled="disabled" /></td>
                    <td class="tdl1"></td>
                    <td class="tdr1"></td>
                </tr>
                <tr>
                    <td class="tdl1">审核人</td>
                    <td class="tdr1">
                        <input type="text" id="OrderAuditor" class="input-text size-MINI" disabled="disabled" /></td>
                    <td class="tdl1">审核日期</td>
                    <td class="tdr1">
                        <input type="text" id="OrderAuditDate" class="input-text size-MINI" disabled="disabled" /></td>
                    <td class="tdl1">不通过原因</td>
                    <td class="tdr1">
                        <input type="text" id="OrderAuditReason" class="input-text size-MINI" disabled="disabled" /></td>
                </tr>
                <tr>
                    <td class="tdl1">反审核人</td>
                    <td class="tdr1">
                        <input type="text" id="OrderRAuditor" class="input-text size-MINI" disabled="disabled" /></td>
                    <td class="tdl1">反审核日期</td>
                    <td class="tdr1">
                        <input type="text" id="OrderRAuditDate" class="input-text size-MINI" disabled="disabled" /></td>
                    <td class="tdl1">反审核原因</td>
                    <td class="tdr1">
                        <input type="text" id="OrderRAuditReason" class="input-text size-MINI" disabled="disabled" /></td>
                </tr>
                <tr>
                    <td class="tdl1">应收总金额</td>
                    <td class="tdr1">
                        <input type="text" id="ARAmount" class="input-text size-MINI" disabled="disabled" /></td>
                    <td class="tdl1">减免总金额</td>
                    <td class="tdr1">
                        <input type="text" id="ReduceAmount" class="input-text size-MINI" disabled="disabled" /></td>
                    <td class="tdl1">实收总金额</td>
                    <td class="tdr1">
                        <input type="text" id="PaidinAmount" class="input-text size-MINI" disabled="disabled" /></td>
                </tr>
            </table>
        </div>
        <div id="bodyeditdiv1">
            <table class="tabedit">
                <tr>
                    <td class="tdl1">所属服务大类</td>
                    <td class="tdr1"><%=SRVTypeNo1Str %></td>
                    <td class="tdl1">所属服务小类</td>
                    <td class="tdr1"><%=SRVTypeNo2Str %></td>
                    <td class="tdl1"></td>
                    <td class="tdr1"></td>
                </tr>
                <tr>
                    <td class="tdl1">订单服务项</td>
                    <td class="tdr1">
                        <select class="input-text size-MINI" id="ODSRVNo"></select>
                    </td>
                    <td class="tdl1">服务项说明</td>
                    <td class="tdr1">
                        <input type="text" id="ODSRVRemark" class="input-text size-MINI" /></td>
                    <td class="tdl1">订单主体</td>
                    <td class="tdr1"><%=ODContractSPNoStr %></td>
                </tr>
                <tr>
                    <td class="tdl1">订单编号</td>
                    <td class="tdr1">
                        <input type="text" id="ODContractNo" class="input-text size-MINI" /></td>
                    <td class="tdl1">手工订单编号</td>
                    <td class="tdr1">
                        <input type="text" id="ODContractNoManual" class="input-text size-MINI" /></td>
                    <td class="tdl1"></td>
                    <td class="tdr1">
                        <select class="input-text size-MINI" id="ODSRVCalType" style="display: none;">
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
                    <td class="tdr1">
                        <input type="text" id="ResourceNo" class="input-text size-MINI" />
                        <img id="ChooseImg" alt="" src="../../images/view_detail.png" class="view_detail" /></td>
                    <td class="tdl1">资源名称</td>
                    <td class="tdr1">
                        <input type="text" id="ResourceName" class="input-text size-MINI" /></td>
                    <td class="tdl1">单位</td>
                    <td class="tdr1">
                        <input type="text" id="ODUnit" class="input-text size-MINI" /></td>
                </tr>
                <tr>
                    <td class="tdl1">计费开始日期</td>
                    <td class="tdr1">
                        <input type="text" id="ODFeeStartDate" class="input-text size-MINI" onchange="ODFeeStartDateChange()" /></td>
                    <td class="tdl1">计费结束日期</td>
                    <td class="tdr1">
                        <input type="text" id="ODFeeEndDate" class="input-text size-MINI" onchange="ODFeeStartDateChange()" /></td>
                    <td class="tdl1">计费天数</td>
                    <td class="tdr1">
                        <input type="text" id="BillingDays" class="input-text size-MINI" disabled="disabled" /></td>
                </tr>
                <tr>
                    <td class="tdl1">数量</td>
                    <td class="tdr1">
                        <input type="text" id="ODQTY" class="input-text size-MINI" onchange="CaluAmount(this.id)" /></td>
                    <td class="tdl1">单价</td>
                    <td class="tdr1">
                        <input type="text" id="ODUnitPrice" class="input-text size-MINI" onchange="CaluAmount(this.id)" /></td>
                    <td class="tdl1">应收金额</td>
                    <td class="tdr1">
                        <input type="text" id="ODARAmount" class="input-text size-MINI" onchange="CaluTaxAmount(this.id)" /></td>
                </tr>
                <tr>
                    <td class="tdl1">税率</td>
                    <td class="tdr1">
                        <input type="text" id="ODTaxRate" class="input-text size-MINI" onchange="CaluTaxAmount(this.id)" /></td>
                    <td class="tdl1">税额</td>
                    <td class="tdr1">
                        <input type="text" id="ODTaxAmount" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl1">
                        <input type="hidden" id="SRVRoundType" class="input-text size-MINI" /></td>
                    <td class="tdr1"></td>
                </tr>
                <tr>
                    <td colspan="6">
                        <input class="btn btn-primary radius" type="button" id="itemsave1" onclick="itemsave1()" value="保存" />&nbsp;
	                    <input class="btn btn-primary radius" type="button" id="itemclear1" onclick="itemclear1()" value="清空" />&nbsp;
                    </td>
                </tr>
            </table>
            <div style="width: 100%; height: 5px;"></div>
            <div id="itemlist1" style="width: 1058px; height: 320px; overflow: auto; margin: 0px; padding: 0px;"></div>
        </div>

        <div style="margin-top: 10px;">
            <input class="btn btn-primary radius" type="button" id="submit" onclick="submit()" value="保存退出" />
            <input class="btn btn-primary radius" type="button" id="submit1" onclick="submit1()" value="保存继续" />
            <input class="btn btn-default radius" type="button" id="cancel" onclick="cancel()" value="&nbsp;&nbsp;取&nbsp;&nbsp;消&nbsp;&nbsp;" />
        </div>
        <br />
        <br />
    </div>

    <div id="rauditdiv" style="display: none;">
        <table style="width: 400px; margin-top: 20px;">
            <tr>
                <td style="width: 70px; text-align: center; padding: 8px; height: 40px;">原因</td>
                <td style="width: 300px;">
                    <textarea cols="" rows="3" class="textarea required" placeholder="反审核原因" id="OrderRAuditReason1"></textarea>
                </td>
                <td style="width: 30px;"></td>
            </tr>
        </table>
        <div style="margin: 10px 30px;">
            <input class="btn btn-primary radius" type="button" onclick="rauditsubmit()" value="&nbsp;确&nbsp;定&nbsp;" />&nbsp;&nbsp;
            <input class="btn btn-primary radius" type="button" onclick="rauditcancel()" value="&nbsp;关&nbsp;闭&nbsp;" />
        </div>
    </div>

    <div id="auditfaileddiv" style="display: none;">
        <table style="width: 400px; margin-top: 20px;">
            <tr>
                <td style="width: 70px; text-align: center; padding: 8px; height: 40px;">原因</td>
                <td style="width: 300px;">
                    <textarea cols="" rows="3" class="textarea required" placeholder="审核不通过原因" id="OrderAuditReason1"></textarea>
                </td>
                <td style="width: 30px;"></td>
            </tr>
        </table>
        <div style="margin: 10px 30px;">
            <input class="btn btn-primary radius" type="button" onclick="auditfailedsubmit()" value="&nbsp;确&nbsp;定&nbsp;" />&nbsp;&nbsp;
            <input class="btn btn-primary radius" type="button" onclick="auditfailedcancel()" value="&nbsp;关&nbsp;闭&nbsp;" />
        </div>
    </div>
    <div id="resourceDiv" style="display: none; text-align: center">
        <input class="btn btn-primary radius" type="button" onclick="getResourceData(1)" value="选择房间" style="display: block; margin: 20px; margin-left: 30%" />
        <input class="btn btn-primary radius" type="button" onclick="getResourceData(2)" value="选择工位" style="display: block; margin: 20px; margin-left: 30%" />
        <input class="btn btn-primary radius" type="button" onclick="getResourceData(3)" value="选择广告位" style="display: block; margin: 20px; margin-left: 30%" />
    </div>

    <div id="feediv" style="display: none;">
        <table style="width: 600px; margin-top: 10px;">
            <tr>
                <td style="width: 80px; text-align: center; padding: 8px;">主体</td>
                <td style="width: 520px;" colspan="3">
                    <input type="text" id="ODSPName" class="input-text size-MINI" style="width: 490px;" disabled="disabled" /></td>
            </tr>
            <tr>
                <td style="width: 80px; text-align: center; padding: 8px;">客户名称</td>
                <td style="width: 520px;" colspan="3">
                    <input type="text" id="ODCustName" class="input-text size-MINI" style="width: 490px;" disabled="disabled" /></td>
            </tr>
            <tr>
                <td style="width: 80px; text-align: center; padding: 8px;">原应收金额</td>
                <td style="width: 215px;">
                    <input type="text" id="RODARAmount" class="input-text size-MINI" style="width: 180px;" disabled="disabled" /></td>
                <td style="width: 80px; text-align: center; padding: 8px;">应收总金额</td>
                <td style="width: 220px;">
                    <input type="text" id="RODUnAmount" class="input-text size-MINI" style="width: 180px;" disabled="disabled" /></td>
            </tr>
            <tr>
                <td style="width: 80px; text-align: center; padding: 8px;">减免金额</td>
                <td style="width: 215px;">
                    <input type="text" id="RReduceAmount" class="input-text size-MINI" style="width: 180px;" disabled="disabled" /></td>
                <td style="width: 80px; text-align: center; padding: 15px;">减免日期</td>
                <td style="width: 220px;">
                    <input type="text" id="ReduceDate" class="input-text size-MINI" style="width: 180px;" /></td>
            </tr>
            <tr>
                <td style="width: 80px; text-align: center; padding: 15px;">备注</td>
                <td style="width: 520px;" colspan="3">
                    <textarea cols="" rows="3" style="width: 490px" class="textarea required" placeholder="备注" id="RODRemark"></textarea></td>
            </tr>
            <tr>
                <td style="width: 600px;" colspan="4">
                    <div id="paylist"></div>
                </td>
            </tr>
        </table>
        <div style="margin: 15px 100px;">
            <input class="btn btn-primary radius" type="button" onclick="reduce()" value="&nbsp;确&nbsp;定&nbsp;" />&nbsp;&nbsp;
            <input class="btn btn-primary radius" type="button" onclick="feecancel()" value="&nbsp;关&nbsp;闭&nbsp;" />
        </div>
    </div>

    <iframe id="openfile" width="0" height="0" marginwidth="0" frameborder="0" src="about:blank" content-disposition="attachment"></iframe>
    <script type="text/javascript" src="../../jscript/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="../../jscript/script_ajax.js"></script>
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
                    $("#checkall").click(function () {
                        var flag = $(this).prop("checked");
                        $("[name=chk]:checkbox").each(function () {
                            $(this).prop("checked", flag);
                        })
                    });
                    $("#selectKey").val("");
                    ordertype = $("OrderTypeS").val();
                    page = 1;
                    reflist();
                }
            }
            if (vjson.type == "jump") {
                if (vjson.flag == "1") {
                    $("#outerlist").html(vjson.liststr);
                    $("#checkall").click(function () {
                        var flag = $(this).prop("checked");
                        $("[name=chk]:checkbox").each(function () {
                            $(this).prop("checked", flag);
                        })
                    });
                    $("#selectKey").val("");
                    ordertype = $("OrderTypeS").val();
                    reflist();
                }
            }
            if (vjson.type == "delete") {
                if (vjson.flag == "1") {
                    $("#outerlist").html(vjson.liststr);
                    $("#checkall").click(function () {
                        var flag = $(this).prop("checked");
                        $("[name=chk]:checkbox").each(function () {
                            $(this).prop("checked", flag);
                        })
                    });
                    $("#selectKey").val("");
                    ordertype = $("OrderTypeS").val();
                    reflist();
                }
                else if (vjson.flag == "3") {
                    layer.alert("当前订单已审核，不能删除！");
                }
                else if (vjson.flag == "4") {
                    layer.alert(vjson.InfoBar);
                }
                else {
                    layer.alert("数据操作出错！");
                }
                return;
            }
            if (vjson.type == "insert") {
                if (vjson.flag == "1") {
                    $("#itemlist1").html(vjson.itemlist1);
                    itemclear1();
                    show(1);
                    id = "";
                    type = "insert";
                    copyid = "";

                    $("#OrderNo").val("");
                    $("#OrderType").val("");
                    $("#CustNo").val("");
                    $("#CustName").val("");
                    $("#OrderTime").val("");
                    $("#ARDate").val("");
                    $("#DaysofMonth").val("");
                    $("#Remark").val("");
                    $("#OrderCreator").val("<%=userName %>");
                    $("#OrderCreateDate").val("<%=date %>");
                    $("#OrderAuditor").val("");
                    $("#OrderAuditDate").val("");
                    $("#OrderAuditReason").val("");
                    $("#OrderRAuditor").val("");
                    $("#OrderRAuditDate").val("");
                    $("#OrderRAuditReason").val("");
                    $("#ARAmount").val("");
                    $("#ReduceAmount").val("");
                    $("#PaidinAmount").val("");
                    $("#OrderStatus").val("待审核");

                    $("#list").css("display", "none");
                    $("#edit").css("display", "");

                    $("#itemsave1").css("display", "");
                    $("#itemclear1").css("display", "");
                    $("#submit").css("display", "");
                    $("#submit1").css("display", "");

                    $('#OrderTime').monthpicker({
                        years: [2016, 2017, 2018, 2019, 2020, 2021, 2022, 2023, 2024, 2025],
                        topOffset: 6,
                        year: 2018
                    });
                    $("#ChooseImg").show();
                }
                else {
                    layer.alert("数据操作出错！");
                }
                return;
            }
            if (vjson.type == "submit") {
                layer.closeAll("loading");
                if (vjson.flag == "1") {
                    if (vjson.submittp == "submit") {
                        $("#outerlist").html(vjson.liststr);
                        $("#checkall").click(function () {
                            var flag = $(this).prop("checked");
                            $("[name=chk]:checkbox").each(function () {
                                $(this).prop("checked", flag);
                            })
                        });
                        $("#selectKey").val("");
                        $("#list").css("display", "");
                        $("#edit").css("display", "none");
                        reflist();
                    }
                    else {
                        id = vjson.RowPointer;
                        type = "update";
                        $("#outerlist").html(vjson.liststr);
                        $("#checkall").click(function () {
                            var flag = $(this).prop("checked");
                            $("[name=chk]:checkbox").each(function () {
                                $(this).prop("checked", flag);
                            })
                        });
                        $("#selectKey").val("");
                        reflist();
                        $("#OrderNo").val(vjson.OrderNo);

                        layer.msg("保存成功！", { icon: 3, time: 1000 });

                        if (copyid != "") {
                            $("#itemlist1").html(vjson.itemlist1);
                        }
                        show(2);
                        copyid = "";
                    }
                }
                else {
                    layer.alert("数据操作出错！");
                }
                return;
            }
            if (vjson.type == "auditpass") {
                layer.closeAll("loading");
                if (vjson.flag == "1") {
                    layer.closeAll("loading");
                    $("#" + vjson.id + " td").eq(4).html("<label style=\"color:blue;\">审核通过</label>");
                    layer.alert("审核成功！");
                }
                else if (vjson.flag == "3") {
                    layer.alert("当前订单状态不允许审核！");
                }
                else if (vjson.flag == "4") {
                    layer.alert(vjson.info);
                }
                else if (vjson.flag == "6") {
                    layer.alert("当前订单没有明细，不能审核！");
                }
                else {
                    layer.alert("数据操作出错！");
                }
                return;
            }
            if (vjson.type == "raudit") {
                if (vjson.flag == "1") {
                    $("#" + vjson.id + " td").eq(4).html("<label style=\"color:black;\">未审核</label>");
                    layer.alert("反审核成功！");
                }
                else if (vjson.flag == "3") {
                    layer.alert("当前订单状态不允许反审核！");
                }
                else {
                    layer.alert("数据操作出错！");
                }
                return;
            }
            if (vjson.type == "auditfailed") {
                if (vjson.flag == "1") {
                    $("#" + vjson.id + " td").eq(4).html("<label style=\"color:red;\">审核不通过</label>");
                    layer.alert("审核不通过成功！");
                }
                else if (vjson.flag == "3") {
                    layer.alert("当前订单状态不允许审核！");
                }
                else {
                    layer.alert("数据操作出错！");
                }
                return;
            }
            if (vjson.type == "update") {
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
                    $("#OrderAuditor").val(vjson.OrderAuditor);
                    $("#OrderAuditDate").val(vjson.OrderAuditDate);
                    $("#OrderAuditReason").val(vjson.OrderAuditReason);
                    $("#OrderRAuditor").val(vjson.OrderRAuditor);
                    $("#OrderRAuditDate").val(vjson.OrderRAuditDate);
                    $("#OrderRAuditReason").val(vjson.OrderRAuditReason);
                    $("#ARAmount").val(vjson.ARAmount);
                    $("#ReduceAmount").val(vjson.ReduceAmount);
                    $("#PaidinAmount").val(vjson.PaidinAmount);
                    $("#OrderStatus").val(vjson.OrderStatus);
                    $("#Remark").val(vjson.Remark);
                    $("#ChooseImg").show();
                    copyid = "";
                    show(1);
                    itemclear1();
                    $("#itemlist1").html(vjson.itemlist1);

                    $("#list").css("display", "none");
                    $("#edit").css("display", "");

                    $("#itemsave1").css("display", "");
                    $("#itemclear1").css("display", "");
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
                    $("#OrderNo").val(vjson.OrderNo);
                    $("#OrderType").val(vjson.OrderType);
                    $("#CustNo").val(vjson.CustNo);
                    $("#CustName").val(vjson.CustName);
                    $("#OrderTime").val(vjson.OrderTime);
                    $("#ARDate").val(vjson.ARDate);
                    $("#DaysofMonth").val(vjson.DaysofMonth);
                    $("#OrderCreator").val(vjson.OrderCreator);
                    $("#OrderCreateDate").val(vjson.OrderCreateDate);
                    $("#OrderAuditor").val(vjson.OrderAuditor);
                    $("#OrderAuditDate").val(vjson.OrderAuditDate);
                    $("#OrderAuditReason").val(vjson.OrderAuditReason);
                    $("#OrderRAuditor").val(vjson.OrderRAuditor);
                    $("#OrderRAuditDate").val(vjson.OrderRAuditDate);
                    $("#OrderRAuditReason").val(vjson.OrderRAuditReason);
                    $("#ARAmount").val(vjson.ARAmount);
                    $("#ReduceAmount").val(vjson.ReduceAmount);
                    $("#PaidinAmount").val(vjson.PaidinAmount);
                    $("#OrderStatus").val(vjson.OrderStatus);
                    $("#Remark").val(vjson.Remark);

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
            if (vjson.type == "print") {
                if (vjson.flag == "1") {
                    document.getElementById("openfile").src = "../../pdf/缴费通知单" + vjson.path;
                }
                else if (vjson.flag == "4") {
                    layer.alert("勾选订单有明细记录为空，不能打印！");
                }
                else {
                    layer.alert(vjson.ex);
                }
                stat = 0;
                return;
            }
            if (vjson.type == "unpayprint") {
                if (vjson.flag == "1") {
                    document.getElementById("openfile").src = "../../pdf/未缴费通知单" + vjson.path;
                }
                else if (vjson.flag == "4") {
                    layer.alert("勾选订单有明细记录为空，不能打印！");
                }
                else {
                    layer.alert(vjson.ex);
                }
                stat = 0;
                return;
            }
            if (vjson.type == "excelrentorder") {
                if (vjson.flag == "1") {
                    document.getElementById("openfile").src = "../../downfile/" + vjson.path;
                }
                else {
                    layer.alert(vjson.ex);
                }
                stat = 0;
                return;
            }
            if (vjson.type == "excelproporder") {
                if (vjson.flag == "1") {
                    document.getElementById("openfile").src = "../../downfile/" + vjson.path;
                }
                else {
                    layer.alert(vjson.ex);
                }
                stat = 0;
                return;
            }
            if (vjson.type == "getfee") {
                if (vjson.flag == "1") {
                    $("#ODCustName").val(vjson.ODCustName);
                    $("#ODSPName").val(vjson.ODSPName);
                    $("#ReduceDate").val("<%=date %>");
                    $("#RODRemark").val("");
                    $("#RODARAmount").val(vjson.ODARAmount);
                    $("#RReduceAmount").val(vjson.ReduceAmount);
                    $("#RODUnAmount").val(vjson.UnReduceAmount);
                    $("#paylist").html(vjson.paylist);
                    layer.open({
                        type: 1,
                        area: ["600px", "600px"],
                        fix: true,
                        maxmin: true,
                        scrollbar: false,
                        shade: 0.5,
                        title: "费用减免",
                        content: $("#feediv")
                    });
                }
                else if (vjson.flag == "5") {
                    layer.alert("订单已审核，不能减免！");
                }
                else if (vjson.flag == "6") {
                    layer.alert("当前订单没有明细！");
                }
                return;
            }
            if (vjson.type == "reduce") {
                layer.closeAll("loading");
                if (vjson.flag == "1") {
                    layer.closeAll();
                    $("#outerlist").html(vjson.liststr);
                    $("#selectKey").val("");
                    reflist();
                    layer.alert("减免成功！");
                }
                else if (vjson.flag == "4") {
                    layer.alert(vjson.info);
                }
                else {
                    layer.alert("提取数据出错！");
                }
                return;
            }
            if (vjson.type == "checkcust") {
                if (vjson.flag == "1") {
                    $("#CustNo").val(vjson.Code);
                    $("#CustName").val(vjson.Name);
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

            if (vjson.type == "itemsave1") {
                if (vjson.flag == "1") {
                    itemclear1();
                    $("#RMID").focus();
                    $("#itemlist1").html(vjson.liststr);

                    $("#ARAmount").val(vjson.ARAmount);
                }
                else {
                    layer.alert("提交数据出错！");
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
                    $("#ODTaxRate").val(vjson.ODTaxRate);
                    $("#ODTaxAmount").val(vjson.ODTaxAmount);

                    $("#ODSRVTypeNo2").empty();
                    for (var i = 0; i <= vjson.row - 1; i++) {
                        var option = "<option value='" + vjson.subtype.split(";")[i].toString().split(":")[0].toString() + "'>" + vjson.subtype.split(";")[i].toString().split(":")[1].toString() + "</option>";
                        $("#ODSRVTypeNo2").append(option);
                    }

                    $("#ODSRVNo").empty();
                    for (var i = 0; i <= vjson.row1 - 1; i++) {
                        var option = "<option value='" + vjson.subtype1.split(";")[i].toString().split(":")[0].toString() + "'>" + vjson.subtype1.split(";")[i].toString().split(":")[1].toString() + "</option>";
                        $("#ODSRVNo").append(option);
                    }


                    $("#ODSRVTypeNo1").val(vjson.ODSRVTypeNo1);
                    $("#ODSRVTypeNo2").val(vjson.ODSRVTypeNo2);
                    $("#ODSRVNo").val(vjson.ODSRVNo);

                    itemid1 = vjson.ItemId;
                    itemtp1 = "update";
                }
                else {
                    layer.alert("修改数据出错！");
                }
                return;
            }
            if (vjson.type == "itemdel1") {
                if (vjson.flag == "1") {
                    $("#itemlist1").html(vjson.liststr);
                    itemclear1();

                    $("#ARAmount").val(vjson.ARAmount);
                }
                else if (vjson.flag == "4") {
                    layer.alert(vjson.InfoBar);
                }
                else {
                    layer.alert("删除数据出错！");
                }
                return;
            }
            if (vjson.type == "ODSRVNoChange") {
                if (vjson.flag == "1") {
                    $("#ODSRVCalType").val(vjson.SRVCalType);
                    $("#ODContractSPNo").val(vjson.SRVSPNo);
                    $("#ODTaxRate").val(vjson.SRVTaxRate);
                    $("#SRVRoundType").val(vjson.SRVRoundType);
                    $("#ODARAmount").val(vjson.amount);
                    $("#ODTaxAmount").val(vjson.taxAmount);
                }
                else {
                    layer.alert("获取数据出错！");
                }
                return;
            }
            if (vjson.type == "CaluAmount") {
                if (vjson.flag == "1") {
                    $("#ODARAmount").val(vjson.amount);
                    $("#ODTaxAmount").val(vjson.taxAmount);
                }
                else {
                    layer.alert("获取数据出错！");
                }
                return;
            }
            if (vjson.type == "CaluTaxAmount") {
                if (vjson.flag == "1") {
                    $("#ODTaxAmount").val(vjson.amount);
                }
                else {
                    layer.alert("获取数据出错！");
                }
                return;
            }

            if (vjson.type == "gettype") {
                if (vjson.flag == "1") {
                    $("#ODSRVTypeNo2").empty();
                    for (var i = 0; i <= vjson.row - 1; i++) {
                        var option = "<option value='" + vjson.subtype.split(";")[i].toString().split(":")[0].toString() + "'>" + vjson.subtype.split(";")[i].toString().split(":")[1].toString() + "</option>";
                        $("#ODSRVTypeNo2").append(option);
                    }

                    $("#ODSRVNo").empty();
                    for (var i = 0; i <= vjson.row1 - 1; i++) {
                        var option = "<option value='" + vjson.subtype1.split(";")[i].toString().split(":")[0].toString() + "'>" + vjson.subtype1.split(";")[i].toString().split(":")[1].toString() + "</option>";
                        $("#ODSRVNo").append(option);
                    }
                    $("#ODSRVNo").val("");
                }
                return;
            }

            if (vjson.type == "getsubtype") {
                if (vjson.flag == "1") {
                    $("#ODSRVNo").empty();
                    for (var i = 0; i <= vjson.row - 1; i++) {
                        var option = "<option value='" + vjson.subtype.split(";")[i].toString().split(":")[0].toString() + "'>" + vjson.subtype.split(";")[i].toString().split(":")[1].toString() + "</option>";
                        $("#ODSRVNo").append(option);
                    }
                    $("#ODSRVNo").val("");
                }
                return;
            }
        }

        var ordertype = "";
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
        function submit() {
            if ($("#OrderType").val() == "") {
                layer.msg("请选择订单类型！", { icon: 7, time: 1000 });
                $("#OrderType").focus();
                return;
            }
            if ($("#CustNo").val() == "") {
                layer.msg("请选择客户！", { icon: 7, time: 1000 });
                $("#CustNo").focus();
                return;
            }
            if ($("#CustNo").val() == "") {
                layer.msg("请选择客户！", { icon: 7, time: 1000 });
                $("#CustNo").focus();
                return;
            }
            if ($("#OrderTime").val() == "") {
                layer.msg("请选择所属年月！", { icon: 7, time: 1000 });
                $("#OrderTime").focus();
                return;
            }
            layer.load(2);
            var submitData = new Object();
            submitData.Type = "submit";
            submitData.submittp = "submit";
            submitData.id = id;
            submitData.OrderNo = $("#OrderNo").val();
            submitData.OrderType = $("#OrderType").val();
            submitData.CustNo = $("#CustNo").val();
            submitData.OrderTime = $("#OrderTime").val();
            submitData.ARDate = $("#ARDate").val();
            submitData.Remark = $("#Remark").val();
            submitData.DaysofMonth = $("#DaysofMonth").val();

            submitData.tp = type;
            submitData.copyid = copyid;
            submitData.OrderNoS = $("#OrderNoS").val();
            submitData.OrderTypeS = $("#OrderTypeS").val();
            submitData.CustNoS = $("#CustNoS").val();
            submitData.OrderStatusS = $("#OrderStatusS").val();
            submitData.OrderTimeS = $("#OrderTimeS").val();
            submitData.MinOrderCreateDate = $("#MinOrderCreateDate").val();
            submitData.MaxOrderCreateDate = $("#MaxOrderCreateDate").val();
            submitData.OrderField = $("#OrderField").val();
            submitData.PageCount = $("#PageCount").val();
            submitData.page = page;
            transmitData(datatostr(submitData));
            return;
        }
        function submit1() {
            if ($("#OrderType").val() == "") {
                layer.msg("请选择订单类型！", { icon: 7, time: 1000 });
                $("#OrderType").focus();
                return;
            }
            if ($("#CustNo").val() == "") {
                layer.msg("请选择客户！", { icon: 7, time: 1000 });
                $("#CustNo").focus();
                return;
            }
            if ($("#CustNo").val() == "") {
                layer.msg("请选择客户！", { icon: 7, time: 1000 });
                $("#CustNo").focus();
                return;
            }
            if ($("#OrderTime").val() == "") {
                layer.msg("请选择所属年月！", { icon: 7, time: 1000 });
                $("#OrderTime").focus();
                return;
            }
            var submitData = new Object();
            submitData.Type = "submit";
            submitData.submittp = "submit1";
            submitData.id = id;
            submitData.OrderNo = $("#OrderNo").val();
            submitData.OrderType = $("#OrderType").val();
            submitData.CustNo = $("#CustNo").val();
            submitData.OrderTime = $("#OrderTime").val();
            submitData.ARDate = $("#ARDate").val();
            submitData.Remark = $("#Remark").val();
            submitData.DaysofMonth = $("#DaysofMonth").val();

            submitData.tp = type;
            submitData.copyid = copyid;
            submitData.OrderNoS = $("#OrderNoS").val();
            submitData.OrderTypeS = $("#OrderTypeS").val();
            submitData.CustNoS = $("#CustNoS").val();
            submitData.OrderStatusS = $("#OrderStatusS").val();
            submitData.OrderTimeS = $("#OrderTimeS").val();
            submitData.MinOrderCreateDate = $("#MinOrderCreateDate").val();
            submitData.MaxOrderCreateDate = $("#MaxOrderCreateDate").val();
            submitData.OrderField = $("#OrderField").val();
            submitData.PageCount = $("#PageCount").val();
            submitData.page = page;
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

                submitData.OrderNoS = $("#OrderNoS").val();
                submitData.OrderTypeS = $("#OrderTypeS").val();
                submitData.CustNoS = $("#CustNoS").val();
                submitData.OrderStatusS = $("#OrderStatusS").val();
                submitData.OrderTimeS = $("#OrderTimeS").val();
                submitData.MinOrderCreateDate = $("#MinOrderCreateDate").val();
                submitData.MaxOrderCreateDate = $("#MaxOrderCreateDate").val();
                submitData.OrderField = $("#OrderField").val();
                submitData.PageCount = $("#PageCount").val();
                submitData.page = page;
                transmitData(datatostr(submitData));
                layer.close(index);
            });
            return;
        }
        function auditpass() {
            if ($("#selectKey").val() == "") {
                layer.msg("请先选择一条记录", { icon: 3, time: 1000 });
                return;
            }
            layer.confirm('确认要审核吗？', function (index) {
                layer.load(2);
                var submitData = new Object();
                submitData.Type = "auditpass";
                submitData.id = $("#selectKey").val();
                transmitData(datatostr(submitData));
                layer.close(index);
            });
            return;
        }

        function raudit() {
            if ($("#selectKey").val() == "") {
                layer.msg("请先选择一条记录", { icon: 3, time: 1000 });
                return;
            }

            $("#OrderRAuditReason1").val("");
            layer.open({
                type: 1,
                area: ["400px", "240px"],
                fix: true,
                maxmin: true,
                scrollbar: false,
                shade: 0.5,
                title: "反审核原因填写",
                content: $("#rauditdiv")
            });
            return;
        }
        function rauditsubmit() {
            var submitData = new Object();
            submitData.Type = "raudit";
            submitData.id = $("#selectKey").val();
            submitData.OrderRAuditReason = $("#OrderRAuditReason1").val();
            transmitData(datatostr(submitData));
            layer.closeAll();
            return;
        }
        function rauditcancel() {
            layer.closeAll();
        }

        function auditfailed() {
            if ($("#selectKey").val() == "") {
                layer.msg("请先选择一条记录", { icon: 3, time: 1000 });
                return;
            }

            $("#OrderAuditReason1").val("");
            layer.open({
                type: 1,
                area: ["400px", "240px"],
                fix: true,
                maxmin: true,
                scrollbar: false,
                shade: 0.5,
                title: "审核不通过原因填写",
                content: $("#auditfaileddiv")
            });
            return;
        }
        function auditfailedsubmit() {
            var submitData = new Object();
            submitData.Type = "auditfailed";
            submitData.id = $("#selectKey").val();
            submitData.OrderAuditReason = $("#OrderAuditReason1").val();
            transmitData(datatostr(submitData));
            layer.closeAll();
            return;
        }
        function auditfailedcancel() {
            layer.closeAll();
        }
        function getfee() {
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
        function reduce() {
            if ($("#ReduceDate").val() == "") {
                layer.msg("请选择减免日期！", { icon: 7, time: 1000 });
                $("#ReduceDate").focus();
                return;
            }
            var ids = "";
            var checklist = jQuery("#reducetbody input:checkbox");
            for (var i = 0; i < checklist.length; i++) {
                var id = checklist[i].value;
                ids += id + ":" + $("#amt" + id).val() + ";";
            }

            layer.load(2);
            var submitData = new Object();
            submitData.Type = "reduce";
            submitData.id = $("#selectKey").val();

            submitData.ReduceDate = $("#ReduceDate").val();
            submitData.ids = ids;

            submitData.OrderNoS = $("#OrderNoS").val();
            submitData.OrderTypeS = $("#OrderTypeS").val();
            submitData.CustNoS = $("#CustNoS").val();
            submitData.OrderStatusS = $("#OrderStatusS").val();
            submitData.OrderTimeS = $("#OrderTimeS").val();
            submitData.MinOrderCreateDate = $("#MinOrderCreateDate").val();
            submitData.MaxOrderCreateDate = $("#MaxOrderCreateDate").val();
            submitData.OrderField = $("#OrderField").val();
            submitData.PageCount = $("#PageCount").val();
            submitData.page = page;
            transmitData(datatostr(submitData));
            return;
        }
        function feecancel() {
            layer.closeAll();
        }
        function CaluReduceAmt(detailid) {
            var unamt = parseFloat($("#aramt" + detailid).text()) - parseFloat($("#amt" + detailid).val());
            if (unamt < 0) unamt = 0;
            $("#unamt" + detailid).html(unamt.toFixed(2));

            var amount = 0;
            var checklist = jQuery("#reducetbody input:checkbox");
            for (var i = 0; i < checklist.length; i++) {
                var id = checklist[i].value;
                validDecimal2("amt" + id);
                if ($("#amt" + id).val() != "") {
                    amount = amount + parseFloat($("#amt" + id).val());
                }
            }
            var TotUnReduceAmount = parseFloat($("#TotODARAmount").text()) - amount;
            if (TotUnReduceAmount < 0) TotUnReduceAmount = 0;

            $("#TotReduceAmt").html(amount.toFixed(2));
            $("#TotUnReduceAmount").html(TotUnReduceAmount.toFixed(2));

            $("#RReduceAmount").val(amount.toFixed(2));
            $("#RODUnAmount").val(TotUnReduceAmount.toFixed(2));
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

        var stat = 0;
        function print() {
            if (ordertype == "") {
                layer.msg("请先按照订单类型查询！", { icon: 7, time: 1500 });
                return;
            }
            var contractID = "";
            var checklist = jQuery("#tbody input:checkbox:checked");
            if (checklist.length < 1) {
                layer.alert("请选择一条数据！");
                return;
            }
            var custno = "";
            for (var i = 0; i < checklist.length; i++) {
                contractID += checklist[i].value + ";";
            }

            if (stat == 1) return;

            var submitData = new Object();
            submitData.Type = "print";
            submitData.ids = contractID;
            transmitData(datatostr(submitData));
            stat = 1;
            return;
        }
        function unpayprint() {
            if (ordertype == "") {
                layer.msg("请先按照订单类型查询！", { icon: 7, time: 1500 });
                return;
            }
            var contractID = "";
            var checklist = jQuery("#tbody input:checkbox:checked");
            if (checklist.length < 1) {
                layer.alert("请选择一条数据！");
                return;
            }
            var custno = "";
            for (var i = 0; i < checklist.length; i++) {
                contractID += checklist[i].value + ";";
            }

            if (stat == 1) return;

            var submitData = new Object();
            submitData.Type = "unpayprint";
            submitData.ids = contractID;
            transmitData(datatostr(submitData));
            stat = 1;
            return;
        }
        function excelrentorder() {
            var submitData = new Object();
            submitData.Type = "excelrentorder";

            submitData.OrderNoS = $("#OrderNoS").val();
            submitData.OrderTypeS = $("#OrderTypeS").val();
            submitData.CustNoS = $("#CustNoS").val();
            submitData.OrderStatusS = $("#OrderStatusS").val();
            submitData.OrderTimeS = $("#OrderTimeS").val();
            submitData.MinOrderCreateDate = $("#MinOrderCreateDate").val();
            submitData.MaxOrderCreateDate = $("#MaxOrderCreateDate").val();
            submitData.OrderField = $("#OrderField").val();
            submitData.PageCount = $("#PageCount").val();
            submitData.ServiceProvider = $("#serviceProvider").val();
            transmitData(datatostr(submitData));
            return;
        }
        function excelproporder() {
            var submitData = new Object();
            submitData.Type = "excelproporder";

            submitData.OrderNoS = $("#OrderNoS").val();
            submitData.OrderTypeS = $("#OrderTypeS").val();
            submitData.CustNoS = $("#CustNoS").val();
            submitData.OrderStatusS = $("#OrderStatusS").val();
            submitData.OrderTimeS = $("#OrderTimeS").val();
            submitData.MinOrderCreateDate = $("#MinOrderCreateDate").val();
            submitData.MaxOrderCreateDate = $("#MaxOrderCreateDate").val();
            submitData.OrderField = $("#OrderField").val();
            submitData.PageCount = $("#PageCount").val();
            submitData.serviceProvider = $("#serviceProvider").val();
            transmitData(datatostr(submitData));
            return;
        }

        $("#checkall").click(function () {
            var flag = $(this).prop("checked");
            $("[name=chk]:checkbox").each(function () {
                $(this).prop("checked", flag);
            })
        });

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
            submitData.OrderField = $("#OrderField").val();
            submitData.PageCount = $("#PageCount").val();
            submitData.serviceProvider = $("#serviceProvider").val();
            submitData.page = 1;
            transmitData(datatostr(submitData));
            return;
        }
        function cancel() {
            id = "";
            $("#ChooseImg").hide();
            $("#list").css("display", "");
            $("#edit").css("display", "none");
            return;
        }

        function itemsave1() {
            if ($("#ODSRVNo").val() == "") {
                layer.msg("请选择订单服务项！", { icon: 7, time: 1500 });
                $("#ODSRVNo").focus();
                return;
            }
            if ($("#ODContractSPNo").val() == "") {
                layer.msg("请选择服务主体！", { icon: 7, time: 1500 });
                $("#ODContractSPNo").focus();
                return;
            }
            if ($("#ODQTY").val() == "") {
                layer.msg("请填写数量！", { icon: 7, time: 1500 });
                $("#ODQTY").focus();
                return;
            }
            if ($("#ODUnitPrice").val() == "") {
                layer.msg("请填写单价！", { icon: 7, time: 1500 });
                $("#ODUnitPrice").focus();
                return;
            }
            if ($("#ODARAmount").val() == "") {
                layer.msg("请填写应收金额！", { icon: 7, time: 1500 });
                $("#ODARAmount").focus();
                return;
            }
            //if ($("#ODSRVCalType").val() == "") {
            //    layer.msg("请选择计费方式！", { icon: 7, time: 1500 });
            //    $("#ODSRVCalType").focus();
            //    return;
            //}
            //if ($("#ODSRVCalType").val() == "3") {
            //    if ($("#ODReduceStartDate").val() == "" || $("#ODReduceEndDate").val() == "") {
            //        layer.msg("请选择计费开始结束日期！", { icon: 7, time: 1500 });
            //        return;
            //    }
            //}
            var submitData = new Object();
            submitData.Type = "itemsave1";
            submitData.id = id;
            submitData.itemid = itemid1;
            submitData.itemtp = itemtp1;
            submitData.ODSRVTypeNo1 = $("#ODSRVTypeNo1").val();
            submitData.ODSRVTypeNo2 = $("#ODSRVTypeNo2").val();
            submitData.ODSRVNo = $("#ODSRVNo").val();
            submitData.ODSRVRemark = $("#ODSRVRemark").val();
            submitData.ODContractSPNo = $("#ODContractSPNo").val();
            submitData.ODContractNo = $("#ODContractNo").val();
            submitData.ODContractNoManual = $("#ODContractNoManual").val();
            submitData.ResourceNo = $("#ResourceNo").val();
            submitData.ResourceName = $("#ResourceName").val();
            submitData.ODSRVCalType = $("#ODSRVCalType").val();
            submitData.ODFeeStartDate = $("#ODFeeStartDate").val();
            submitData.ODFeeEndDate = $("#ODFeeEndDate").val();
            submitData.BillingDays = $("#BillingDays").val();
            submitData.ODQTY = $("#ODQTY").val();
            submitData.ODUnit = $("#ODUnit").val();
            submitData.ODUnitPrice = $("#ODUnitPrice").val();
            submitData.ODARAmount = $("#ODARAmount").val();
            submitData.ODTaxRate = $("#ODTaxRate").val();
            submitData.ODTaxAmount = $("#ODTaxAmount").val();

            transmitData(datatostr(submitData));
            return;
        }
        function itemupdate1(itemid) {
            var submitData = new Object();
            submitData.Type = "itemupdate1";
            submitData.itemid = itemid;
            transmitData(datatostr(submitData));
            return;
        }
        function itemdel1(itemid) {
            layer.confirm('确认要删除吗？', function (index) {
                var submitData = new Object();
                submitData.Type = "itemdel1";
                submitData.id = id;
                submitData.itemid = itemid;
                transmitData(datatostr(submitData));
                layer.close(index);
            });
            return;
        }
        function itemclear1() {
            itemtp1 = "insert";
            itemid1 = "";

            $("#ODSRVTypeNo1").val("");
            $("#ODSRVTypeNo2").val("");
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
            $("#ODTaxRate").val("");
            $("#ODTaxAmount").val("");
        }

        function getResourceData(type) {
            if (type == 1) {
                var temp = "../Base/ChooseRMID.aspx?id=ResourceNo";
                layer_show("选择房间页面", temp, 800, 630);
            } else if (type == 2) {
                $("#ResourceNo").val("");
                var temp = "../Base/ChooseRMID.aspx?id=ResourceNo";
                layer.open({
                    type: 2,
                    title: '选择房间页面',
                    content: temp,
                    area: ['800px', '630px'],
                    fix: false,
                    shade: 0.3,
                    maxmin: true,
                    end: function () {
                        temp = "../Base/ChooseWPNo.aspx?id=ResourceNo&RMID=" + $("#ResourceNo").val();
                        $("#ResourceNo").val("");
                        layer_show("选择工位页面", temp, 800, 630);
                    }
                });
            } else {
                var temp = "../Base/ChooseBasic.aspx?type=Billboard&id=ResourceNo";
                layer_show("选择广告位页面", temp, 900, 600);
            }
            layer.close(resourceLayer);
        }

        jQuery("#ODSRVTypeNo1").change(function () {
            var submitData = new Object();
            submitData.Type = "gettype";
            submitData.SRVTypeNo1 = $("#ODSRVTypeNo1").val();
            transmitData(datatostr(submitData));
            return;
        });
        jQuery("#ODSRVTypeNo2").change(function () {
            var submitData = new Object();
            submitData.Type = "getsubtype";
            submitData.SRVTypeNo2 = $("#ODSRVTypeNo2").val();
            transmitData(datatostr(submitData));
            return;
        });

        var page = 1;
        function jump(pageindex) {
            page = pageindex;
            var submitData = new Object();
            submitData.Type = "jump";
            submitData.OrderNoS = $("#OrderNoS").val();
            submitData.OrderTypeS = $("#OrderTypeS").val();
            submitData.CustNoS = $("#CustNoS").val();
            submitData.OrderStatusS = $("#OrderStatusS").val();
            submitData.OrderTimeS = $("#OrderTimeS").val();
            submitData.MinOrderCreateDate = $("#MinOrderCreateDate").val();
            submitData.MaxOrderCreateDate = $("#MaxOrderCreateDate").val();
            submitData.OrderField = $("#OrderField").val();
            submitData.PageCount = $("#PageCount").val();
            submitData.serviceProvider = $("#serviceProvider").val();
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

        $("#CustImg").click(function () {
            ChooseBasic("CustNo", "Cust");
        });
        function choose(id, labels, values) {
            if (labels != "" && labels != undefined && labels != "undefined") {
                if (id == "CustNo") {
                    $("#CustName").val(values);
                    $("#CustNo").val(labels);
                }
                else if (id == "ResourceNo") {
                    $("#" + id).val(labels);
                }
            }
        }
        function checkcust() {
            if ($("#CustName").val() == "") {
                $("#CustNo").val("");
                return;
            }
            var submitData = new Object();
            submitData.Type = "checkcust";
            submitData.val = $("#CustName").val();
            submitData.tp = "Cust";
            transmitData(datatostr(submitData));
            return;
        }
        function OrderTimeChange() {
            var date = new Date($("#OrderTime").val() + "-01");
            date.setMonth(date.getMonth() + 1);
            date.setDate(date.getDate() - 1);
            $("#DaysofMonth").val(date.getDate());
        }
        function ODFeeStartDateChange() {
            if ($("#ODFeeEndDate").val() != "" && $("#ODFeeStartDate").val() != "") {
                var date1 = new Date($("#ODFeeStartDate").val());
                var date2 = new Date($("#ODFeeEndDate").val());
                $("#BillingDays").val((date2 - date1) / (24 * 60 * 60 * 1000) + 1);
                if ($("#ODSRVCalType").val() == "3")
                    $("#ODQTY").val($("#BillingDays").val());
            }
        }
        $("#ODSRVNo").change(function () {
            var submitData = new Object();
            submitData.Type = "ODSRVNoChange";
            submitData.ODSRVNo = $("#ODSRVNo").val();
            submitData.ODQTY = $("#ODQTY").val();
            submitData.ODUnitPrice = $("#ODUnitPrice").val();
            submitData.ODTaxRate = $("#ODTaxRate").val();
            transmitData(datatostr(submitData));
            return;
        });

        function CaluAmount(id) {
            validDecimal("ODQTY");
            validDecimal("ODUnitPrice");
            if ($("#ODQTY").val() != "" && $("#ODUnitPrice").val() != "" && $("#ODSRVNo").val() != "") {
                if (parseFloat($("#ODQTY").val()) > 0 && parseFloat($("#ODUnitPrice").val()) > 0) {
                    var type = $("#SRVRoundType").val();
                    var amount= parseFloat($("#ODQTY").val()) * parseFloat($("#ODUnitPrice").val());
                    if (type.toUpperCase == "FLOOR") {
                        $("#ODARAmount").val(Math.floor(amount)).change();
                    } else if (type.toUpperCase == "CEILING") {
                        $("#ODARAmount").val(Math.ceil(amount)).change();
                    } else {
                        $("#ODARAmount").val(Math.round(amount)).change();
                    }
                    return;
                }
            }
            $("#ODARAmount").val("");
            $("#ODTaxAmount").val("");
            return;
            //田工代码
            validDecimal(id);
            if ($("#ODQTY").val() != "" && $("#ODUnitPrice").val() != "" && $("#ODSRVNo").val() != "") {
                if (parseFloat($("#ODQTY").val()) > 0 && parseFloat($("#ODUnitPrice").val()) > 0) {
                    var submitData = new Object();
                    submitData.Type = "CaluAmount";
                    submitData.ODSRVNo = $("#ODSRVNo").val();
                    submitData.ODQTY = $("#ODQTY").val();
                    submitData.ODUnitPrice = $("#ODUnitPrice").val();
                    submitData.ODTaxRate = $("#ODTaxRate").val();
                    transmitData(datatostr(submitData));
                    return;
                }
            }
            $("#ODARAmount").val("");
            $("#ODTaxAmount").val("");
            return;
        }

        function CaluTaxAmount(id) {
            validDecimal("ODARAmount");
            validDecimal("ODTaxRate");
            if ($("#ODARAmount").val() != "" && $("#ODTaxRate").val() != "" && $("#ODSRVNo").val() != "") {
                if (parseFloat($("#ODARAmount").val()) > 0 && parseFloat($("#ODTaxRate").val()) > 0) {
                    var type = $("#SRVRoundType").val();
                    var taxamount = parseFloat($("#ODARAmount").val()) - parseFloat($("#ODARAmount").val()) / (1 + parseFloat($("#ODTaxRate").val()));
                    if (type.toUpperCase == "FLOOR") {
                        $("#ODTaxAmount").val(Math.floor(taxamount));
                    } else if (type.toUpperCase == "CEILING") {
                        $("#ODTaxAmount").val(Math.ceil(taxamount));
                    } else {
                        $("#ODTaxAmount").val(Math.round(taxamount));
                    }
                    return;
                }
            }
            $("#ODTaxAmount").val("")
            return;
            //田工代码
            validDecimal(id);
            if ($("#ODARAmount").val() != "" && $("#ODTaxRate").val() != "" && $("#ODSRVNo").val() != "") {
                if (parseFloat($("#ODARAmount").val()) > 0 && parseFloat($("#ODTaxRate").val()) > 0) {
                    var submitData = new Object();
                    submitData.Type = "CaluTaxAmount";
                    submitData.ODSRVNo = $("#ODSRVNo").val();
                    submitData.ODARAmount = $("#ODARAmount").val();
                    submitData.ODTaxRate = $("#ODTaxRate").val();
                    transmitData(datatostr(submitData));
                    return;
                }
            }
            $("#ODTaxAmount").val("")
            return;
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
            var ReduceDate = new JsInputDate("ReduceDate");

            var MinOrderCreateDate = new JsInputDate("MinOrderCreateDate");
            MinOrderCreateDate.setWidth("100px");
            MinOrderCreateDate.setValue(getfirstday());
            var MaxOrderCreateDate = new JsInputDate("MaxOrderCreateDate");
            MaxOrderCreateDate.setWidth("100px");
            MaxOrderCreateDate.setValue(gettoday());

            var ODFeeStartDate = new JsInputDate("ODFeeStartDate");
            var ODFeeEndDate = new JsInputDate("ODFeeEndDate");
        });

        var id = "";
        var tp = "";
        var itemid1 = "";
        var itemtp1 = "";
        var copyid = "";
        var trid = "";
        var resourceLayer;
        $("#ChooseImg").hide();
        $("#ChooseImg").click(function () {
            resourceLayer = layer.open({
                type: 1,
                //area:['140','210'],
                area: ['250', '210'],
                title: '选择资源',
                content: $("#resourceDiv")
            });
        });
        reflist();
    </script>
</body>
</html>
