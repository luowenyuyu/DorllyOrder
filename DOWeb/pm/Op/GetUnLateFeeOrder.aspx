<%@ Page Language="C#" AutoEventWireup="true" Inherits="project.Presentation.Op.GetUnLateFeeOrder,project"  %>
<!DOCTYPE>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="head1" runat="server">
    <title>违约金待收明细</title>    
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
    <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 订单管理 <span class="c-gray en">&gt;</span> 违约金待收明细 <a class="btn btn-success radius r mr-20" style="line-height:1.6em;margin-top:2px" href="javascript:location.replace(location.href);" title="刷新" ><i class="Hui-iconfont">&#xe68f;</i></a></nav>
    <div id="list" class="pt-5 pr-20 pb-5 pl-20">
	    <div class="cl pd-3 bg-1 bk-gray mt-2"> 
            <span class="l">
                <a href="javascript:;" onclick="genorder()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe615;</i> 生成订单</a>
            </span> 
	    </div>
	    <div class="cl pd-10  bk-gray mt-2"> 
            月份&nbsp;<input type="text" class="input-text size-MINI" id="LaterFeeMonth" value="<%=Month %>" style="width:110px" />
            结算日期&nbsp;<input type="text" class="input-text size-MINI" id="ARDate" value="<%=ARDate %>" style="width:110px" />
            截止日期&nbsp;<input type="text" class="input-text size-MINI" id="EndDate" value="<%=EndDate %>" style="width:110px" />
            订单类型&nbsp;<%=ContractType %>
		    <button type="submit" class="btn btn-success radius" onclick="select()"><i class="Hui-iconfont">&#xe665;</i> 检索</button>&nbsp;&nbsp;
	    </div>
	    <div class="mt-5" id="outerlist">
	    <%=list %>
	    </div>
    </div>
    <iframe id="openfile" width="0" height="0" marginwidth="0" frameborder="0" src="about:blank" Content-Disposition="attachment"></iframe>
    <script type="text/javascript" src="../../jscript/jquery-1.10.2.js"></script> 
    <script type="text/javascript" src="../../jscript/script_ajax.js"></script>
    <script type="text/javascript" src="../../jscript/script_common.js"></script>
    <script type="text/javascript" src="../../jscript/json2.js"></script>
    <script type="text/javascript" src="../../jscript/H-ui.js"></script> 
    <script type="text/javascript" src="../../jscript/H-ui.admin.js"></script> 
    <script type="text/javascript" src="../../jscript/JsInputDate.js"></script>
    <script type="text/javascript" src="../../jscript/jquery.monthpicker.js"></script>
    <script type="text/javascript" src="../../lib/layer/layer.js"></script> 
    <script type="text/javascript">
        function BandResuleData(temp) {
            var vjson = JSON.parse(temp);
            if (vjson.type == "select") {
                if (vjson.flag == "1") {
                    $("#outerlist").html(vjson.liststr);
                    Month = $("#LaterFeeMonth").val();
                    ARDate = $("#ARDate").val();
                    EndDate = $("#EndDate").val();
                    ContractType = $("#ContractType").val();

                    $("#checkall").click(function () {
                        var flag = $(this).prop("checked");
                        $("[name=chk]:checkbox").each(function () {
                            $(this).prop("checked", flag);
                        })
                    });
                }
            }
            if (vjson.type == "genorder") {
                layer.closeAll('loading');
                if (vjson.flag == "1") {
                    layer.alert("生成订单成功！");
                    $("#outerlist").html(vjson.liststr);

                    $("#checkall").click(function () {
                        var flag = $(this).prop("checked");
                        $("[name=chk]:checkbox").each(function () {
                            $(this).prop("checked", flag);
                        })
                    });

                }
                else {
                    layer.alert("数据操作出错！");
                }
                return;
            }
        }

        var Month = "<%=Month %>";
        var ARDate = "<%=ARDate %>";
        var EndDate = "<%=EndDate %>";
        var ContractType = "";
        function genorder() {
            var orderID = "";
            var checklist = jQuery("#tbody input:checkbox:checked");
            if (checklist.length < 1) {
                layer.alert("请选择一条数据！");
                return;
            }
            for (var i = 0; i < checklist.length; i++) {
                var id = checklist[i].value;

                orderID += id + ";";
            }
            layer.load(1);

            var submitData = new Object();
            submitData.Type = "genorder";
            submitData.OrderID = orderID;

            submitData.Month = Month;
            submitData.ARDate = ARDate;
            submitData.EndDate = EndDate;
            submitData.ContractType = ContractType;
            transmitData(datatostr(submitData));
            return;
        }
        function select() {
            var submitData = new Object();
            submitData.Type = "select";
            submitData.Month = $("#LaterFeeMonth").val();
            submitData.ARDate = $("#ARDate").val();
            submitData.EndDate = $("#EndDate").val();
            submitData.ContractType = $("#ContractType").val();
            transmitData(datatostr(submitData));
            return;
        }
        function LaterFeeMonthChange() {
            $("#ARDate").val($('#LaterFeeMonth').val() + "-05");
        }
        $("#checkall").click(function () {
            var flag = $(this).prop("checked");
            $("[name=chk]:checkbox").each(function () {
                $(this).prop("checked", flag);
            })
        });

        jQuery(function () {
            var now = new Date();
            var year = now.getFullYear();
            $('#LaterFeeMonth').monthpicker({
                years: [2016, 2017, 2018, 2019, 2020, 2021, 2022, 2023, 2024, 2025],
                topOffset: 6,
                year: year
            });

            var ARDate = new JsInputDate("ARDate");
            ARDate.setDisabled(false);
            var EndDate = new JsInputDate("EndDate");
            EndDate.setDisabled(false);
        });
</script>
</body>
</html>