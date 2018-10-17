<%@ Page Language="C#" AutoEventWireup="true" Inherits="project.Presentation.Op.GetReceivableRT,project"  %>
<!DOCTYPE>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="head1" runat="server">
    <title>应收明细报表</title>    
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
    <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 报表管理 <span class="c-gray en">&gt;</span> 应收明细报表 <a class="btn btn-success radius r mr-20" style="line-height:1.6em;margin-top:2px" href="javascript:location.replace(location.href);" title="刷新" ><i class="Hui-iconfont">&#xe68f;</i></a></nav>
   <div id="list" class="pt-5 pr-20 pb-5 pl-20">
	    <div class="cl pd-10  bk-gray mt-2"> 
             月份&nbsp;&nbsp;
            <input type="text" class="input-text size-MINI" id="BeginMonth" value="<%=GetDate().AddMonths(-1).ToString("yyyy-MM") %>" style="width:110px" />
            &nbsp;&nbsp;到&nbsp;&nbsp;
            <input type="text" class="input-text size-MINI" id="EndMonth" value="<%=GetDate().ToString("yyyy-MM") %>" style="width:110px" />
            客户名称&nbsp;&nbsp;<input type="text" class="input-text size-MINI" id="CustName" value="" style="width:110px" />
            费用项目&nbsp;&nbsp;<%=FeeTypeSelectStr %>&nbsp;
            订单类别&nbsp;&nbsp;<%=OrderTypeSelectStr %>&nbsp;
            订单状态&nbsp;&nbsp;<%=OrderStatusSelectStr %>&nbsp;
            订单主体&nbsp;&nbsp;<%=SPSelectStr %>&nbsp;
		    <button type="submit" class="btn btn-success radius" onclick="select()"><i class="Hui-iconfont">&#xe665;</i> 检索</button>&nbsp;&nbsp;
            <%=Buttons %>
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
    <script type="text/javascript" src="../../jscript/jquery.monthpicker.js"></script>
    <script type="text/javascript" src="../../lib/layer/layer.js"></script> 
    <script type="text/javascript">
        function BandResuleData(temp) {
            var vjson = JSON.parse(temp);
            if (vjson.type == "select") {
                if (vjson.flag == "1") {
                    $("#outerlist").html(vjson.liststr);
                }
            }
            if (vjson.type == "excel") {
                if (vjson.flag == "1") {
                    document.getElementById("openfile").src = "../../downfile/" + vjson.path;
                }
                else {
                    layer.alert(vjson.ex);
                }
                stat = 0;
                return;
            }
        }

        function excel() {
            var submitData = new Object();
            submitData.Type = "excel";
            submitData.BeginMonth = $("#BeginMonth").val();
            submitData.EndMonth = $("#EndMonth").val();
            submitData.CustName = $("#CustName").val();
            submitData.FeeTypeNo = $("#FeeType option:selected").val();
            submitData.FeeTypeName = $("#FeeType option:selected").text();
            submitData.OrderType = $("#OrderType option:selected").val();
            submitData.OrderStatus = $("#OrderStatus option:selected").val();
            submitData.SP = $("#SP option:selected").val();
            transmitData(datatostr(submitData));
            return;
        }

        function select() {
            var submitData = new Object();
            submitData.Type = "select";
            submitData.BeginMonth = $("#BeginMonth").val();
            submitData.EndMonth = $("#EndMonth").val();
            submitData.CustName = $("#CustName").val();
            submitData.FeeTypeNo = $("#FeeType option:selected").val();
            submitData.OrderType = $("#OrderType option:selected").val();
            submitData.OrderStatus = $("#OrderStatus option:selected").val();
            submitData.SP = $("#SP option:selected").val();
            transmitData(datatostr(submitData));
            return;
        }

        jQuery(function () {
            var now = new Date();
            var year = now.getFullYear();
            $('#BeginMonth').monthpicker({
                years: [2016, 2017, 2018, 2019, 2020, 2021, 2022, 2023, 2024, 2025],
                topOffset: 6,
                year: year
            });
            $('#EndMonth').monthpicker({
                years: [2016, 2017, 2018, 2019, 2020, 2021, 2022, 2023, 2024, 2025],
                topOffset: 6,
                year: year
            });
        });
</script>
</body>
</html>