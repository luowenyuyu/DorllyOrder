<%@ Page Language="C#" AutoEventWireup="true" Inherits="project.Presentation.Op.GetUnPaymentList,project"  %>
<!DOCTYPE>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="head1" runat="server">
    <title>出租情况统计表</title>    
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
    <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 报表管理 <span class="c-gray en">&gt;</span> 出租情况统计表 <a class="btn btn-success radius r mr-20" style="line-height:1.6em;margin-top:2px" href="javascript:location.replace(location.href);" title="刷新" ><i class="Hui-iconfont">&#xe68f;</i></a></nav>
    <div id="list" class="pt-5 pr-20 pb-5 pl-20">
	    <div class="cl pd-10  bk-gray mt-2"> 
            客户&nbsp;<input type="text" class="input-text size-MINI" placeholder="" id="CustName" style="width:110px" />
            应收日期 从&nbsp;<input type="text" class="input-text size-MINI" id="MinARDate" style="width:110px" />
            至&nbsp;<input type="text" class="input-text size-MINI" id="MaxARDate" style="width:110px" />
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
            submitData.CustName = $("#CustName").val();
            submitData.MinARDate = $("#MinARDate").val();
            submitData.MaxARDate = $("#MaxARDate").val();
            transmitData(datatostr(submitData));
            return;
        }

        function select() {
            var submitData = new Object();
            submitData.Type = "select";
            submitData.CustName = $("#CustName").val();
            submitData.MinARDate = $("#MinARDate").val();
            submitData.MaxARDate = $("#MaxARDate").val();
            transmitData(datatostr(submitData));
            return;
        }

        jQuery(function () {
            var MinARDate = new JsInputDate("MinARDate");
            MinARDate.setWidth("100px");
            MinARDate.setValue(getfirstday());
            var MaxARDate = new JsInputDate("MaxARDate");
            MaxARDate.setWidth("100px");
            MaxARDate.setValue(gettoday());
        });
</script>
</body>
</html>