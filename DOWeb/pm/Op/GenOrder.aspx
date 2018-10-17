<%@ Page Language="C#" AutoEventWireup="true" Inherits="project.Presentation.Op.GenOrder,project"  %>
<!DOCTYPE>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="head1" runat="server">
    <title>合同生成订单</title>    
    <!--[if lt IE 9]>
    <script type="text/javascript" src="../../jscript/html5.js"></script>
    <script type="text/javascript" src="../../jscript/respond.min.js"></script>
    <![endif]-->
    <link href="../../css/H-ui.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/H-ui.admin.css" rel="stylesheet" type="text/css" />
    <link href="../../lib/iconfont/iconfont.css" rel="stylesheet" type="text/css" />
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
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
    <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 订单管理 <span class="c-gray en">&gt;</span> 合同生成订单 <a class="btn btn-success radius r mr-20" style="line-height:1.6em;margin-top:2px" href="javascript:location.replace(location.href);" title="刷新" ><i class="Hui-iconfont">&#xe68f;</i></a></nav>
    <div id="list" class="pt-5 pr-20 pb-5 pl-20">
	    <div class="cl pd-3 bg-1 bk-gray mt-2"> 
            <span class="l">
                <a href="javascript:;" onclick="genorder()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe615;</i> 生成订单</a>
            </span> 
	    </div>
	    <div class="cl pd-10  bk-gray mt-2"> 
            园区&nbsp;<%=RMLOCNo1Str %>&nbsp;
            建设期&nbsp;<select class="input-text required size-MINI" id="RMLOCNo2" style="width:120px"><option value="">全部</option></select>&nbsp;
            楼栋&nbsp;<select class="input-text required size-MINI" id="RMLOCNo3" style="width:120px"><option value="">全部</option></select>&nbsp;
            楼层&nbsp;<select class="input-text required size-MINI" id="RMLOCNo4" style="width:120px"><option value="">全部</option></select>&nbsp;
            主体&nbsp;<%=SPNoStr %>
            <br />
            客户&nbsp;<input type="text" class="input-text size-MINI" id="CustName" style="width:120px" />&nbsp;
            月份 从&nbsp;<input type="text" id="MinMonth" class="input-text size-MINI" readonly="readonly" value="<%=GetDate().AddMonths(-1).ToString("yyyy-MM") %>" style="width:90px;" />&nbsp;
            至&nbsp;<input type="text" id="MaxMonth" class="input-text size-MINI" readonly="readonly" value="<%=GetDate().ToString("yyyy-MM") %>" style="width:90px;" />&nbsp;
            合同类型&nbsp;<%=ContractType %>&nbsp;
		    <button type="submit" class="btn btn-success radius" onclick="select()"><i class="Hui-iconfont">&#xe665;</i> 检索</button>
	    </div>
	    <div class="mt-5" id="outerlist">
	    <%=list %>
	    </div>
    </div>
    <div id="edit" class="editdiv" style="display:none;">
    </div>    
    <script type="text/javascript" src="../../jscript/jquery-1.10.2.js"></script> 
    <script type="text/javascript" src="../../jscript/script_common.js"></script>
    <script type="text/javascript" src="../../jscript/json2.js"></script>
    <script type="text/javascript" src="../../jscript/H-ui.js"></script> 
    <script type="text/javascript" src="../../jscript/H-ui.admin.js"></script> 
    <script type="text/javascript" src="../../jscript/jquery.monthpicker.js"></script>
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
            if (vjson.type == "getvalue") {
                if (vjson.flag == "1") {
                    $("#" + vjson.child).empty();
                    $("#" + vjson.child).append("<option value=''>全部</option>");

                    for (var i = 0; i <= vjson.row - 1; i++) {
                        var option = "<option value='" + vjson.subtype.split(";")[i].split(":")[0].toString() + "'>" + vjson.subtype.split(";")[i].split(":")[1].toString() + "</option>";
                        $("#" + vjson.child).append(option);
                    }

                    if (vjson.child == "RMLOCNo2") {
                        $("#RMLOCNo3").empty();
                        $("#RMLOCNo3").append("<option value=''>全部</option>");
                        $("#RMLOCNo4").empty();
                        $("#RMLOCNo4").append("<option value=''>全部</option>");
                    }
                    else if (vjson.child == "RMLOCNo3") {
                        $("#RMLOCNo4").empty();
                        $("#RMLOCNo4").append("<option value=''>全部</option>");
                    }
                }
                return;
            }
        }

        function genorder() {
            var contractID = "";
            var checklist = jQuery("#tbody input:checkbox:checked");
            if (checklist.length < 1) {
                layer.alert("请选择一条数据！");
                return;
            }
            var custno = "";
            for (var i = 0; i < checklist.length; i++) {
                var id = checklist[i].value;

                contractID += $("#ID" + id).val() + ":" + $("#Month" + id).val() + ";";
            }

            layer.load(1);

            var submitData = new Object();
            submitData.Type = "genorder";
            submitData.ContractID = contractID;

            submitData.RMLOCNo1 = $("#RMLOCNo1").val();
            submitData.RMLOCNo2 = $("#RMLOCNo2").val();
            submitData.RMLOCNo3 = $("#RMLOCNo3").val();
            submitData.RMLOCNo4 = $("#RMLOCNo4").val();
            submitData.CustName = $("#CustName").val();
            submitData.ContractType = $("#ContractType").val();
            submitData.MinMonth = $("#MinMonth").val();
            submitData.MaxMonth = $("#MaxMonth").val();
            submitData.SPNo = $("#SPNo").val();
            transmitData(datatostr(submitData));
            return;
        }
        function select() {
            var submitData = new Object();
            submitData.Type = "select";
            submitData.RMLOCNo1 = $("#RMLOCNo1").val();
            submitData.RMLOCNo2 = $("#RMLOCNo2").val();
            submitData.RMLOCNo3 = $("#RMLOCNo3").val();
            submitData.RMLOCNo4 = $("#RMLOCNo4").val();
            submitData.CustName = $("#CustName").val();
            submitData.ContractType = $("#ContractType").val();
            submitData.MinMonth = $("#MinMonth").val();
            submitData.MaxMonth = $("#MaxMonth").val();
            submitData.SPNo = $("#SPNo").val();
            transmitData(datatostr(submitData));
            return;
        }

        $("#checkall").click(function () {
            var flag = $(this).prop("checked");
            $("[name=chk]:checkbox").each(function () {
                $(this).prop("checked", flag);
            })
        });

        jQuery("#RMLOCNo1").change(function () {
            var submitData = new Object();
            submitData.Type = "getvalue";
            submitData.parent = $("#RMLOCNo1").val();
            submitData.child = "RMLOCNo2";
            transmitData(datatostr(submitData));
            return;
        });
        jQuery("#RMLOCNo2").change(function () {
            var submitData = new Object();
            submitData.Type = "getvalue";
            submitData.parent = $("#RMLOCNo2").val();
            submitData.child = "RMLOCNo3";
            transmitData(datatostr(submitData));
            return;
        });
        jQuery("#RMLOCNo3").change(function () {
            var submitData = new Object();
            submitData.Type = "getvalue";
            submitData.parent = $("#RMLOCNo3").val();
            submitData.child = "RMLOCNo4";
            transmitData(datatostr(submitData));
            return;
        });

        jQuery(function () {
            var now = new Date();
            var year = now.getFullYear();
            $('#MinMonth').monthpicker({
                years: [2016, 2017, 2018, 2019, 2020, 2021, 2022, 2023, 2024, 2025],
                topOffset: 6,
                year: year
            });
            $('#MaxMonth').monthpicker({
                years: [2016, 2017, 2018, 2019, 2020, 2021, 2022, 2023, 2024, 2025],
                topOffset: 6,
                year: year
            });
        });
</script>
</body>
</html>