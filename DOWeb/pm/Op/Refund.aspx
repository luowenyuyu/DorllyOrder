<%@ Page Language="C#" AutoEventWireup="true" Inherits="project.Presentation.Op.Refund,project"  %>
<!DOCTYPE>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="head1" runat="server">
    <title>会议室退租金</title>
    <!--[if lt IE 9]>
    <script type="text/javascript" src="../jscript/html5.js"></script>
    <script type="text/javascript" src="../jscript/respond.min.js"></script>
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
    <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 订单管理 <span class="c-gray en">&gt;</span> 会议室退租金 <a class="btn btn-success radius r mr-20" style="line-height:1.6em;margin-top:2px" href="javascript:location.replace(location.href);" title="刷新" ><i class="Hui-iconfont">&#xe68f;</i></a></nav>
    <div id="list" class="pt-5 pr-20 pb-5 pl-20">
	    <div class="cl pd-5 bg-1 bk-gray mt-2"> 
            <span class="l">
                <%=Buttons %>
                <input type="hidden" id="selectKey" />
            </span> 
	    </div>
	    <div class="cl pd-10  bk-gray mt-2"> 
		    申请日期&nbsp;<input type="text" class="input-text size-MINI" style="width:150px" id="MinApplyDate" />&nbsp;
            至&nbsp;<input type="text" class="input-text size-MINI" style="width:150px" id="MaxApplyDate" />&nbsp;
            状态&nbsp;<select class="input-text size-MINI" style="width:120px" id="State">
                <option value="null" >全部</option>
                <option value="0" selected>待退款</option>
                <option value="1">已退款</option>
                </select>&nbsp;
		    <button type="submit" class="btn btn-success radius" onclick="select()"><i class="Hui-iconfont">&#xe665;</i> 查询</button>
	    </div>     
	    <div class="mt-5" id="outerlist">
	        <%=list %>
        </div>
    </div>
    <div id="edit" class="pt-5 pr-20 pb-5 pl-20" style="display:none;">
    </div>
    
    <script type="text/javascript" src="../../jscript/jquery-1.10.2.js"></script> 
    <script type="text/javascript" src="../../jscript/script_common.js"></script>
    <script type="text/javascript" src="../../jscript/json2.js"></script>
    <script type="text/javascript" src="../../jscript/H-ui.js"></script> 
    <script type="text/javascript" src="../../jscript/H-ui.admin.js"></script> 
    <script type="text/javascript" src="../../lib/layer/layer.js"></script> 
    <script type="text/javascript" src="../../jscript/JsInputDate.js"></script>
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
            if (vjson.type == "refund") {
                if (vjson.flag == "1") {
                    layer.alert("退款成功");
                    $("#outerlist").html(vjson.liststr);
                    $("#selectKey").val("");
                    reflist();
                }
                else if (vjson.flag == "3") {
                    layer.alert(vjson.info);
                }
                else {
                    layer.alert("数据操作出错！");
                }
            }
        }

        function refund() {
            layer.confirm('确认要退款吗？', function (index) {
                var submitData = new Object();
                submitData.Type = "refund";
                submitData.id = $("#selectKey").val();

                submitData.MinApplyDate = $("#MinApplyDate").val();
                submitData.MaxApplyDate = $("#MaxApplyDate").val();
                submitData.State = $("#State").val();
                submitData.page = page;
                transmitData(datatostr(submitData));
                layer.close(index);
            });
            return;
        }
        function select() {
            var submitData = new Object();
            submitData.Type = "select";
            submitData.MinApplyDate = $("#MinApplyDate").val();
            submitData.MaxApplyDate = $("#MaxApplyDate").val();
            submitData.State = $("#State").val();
            submitData.page = 1;
            transmitData(datatostr(submitData));
            return;
        }
        var page = 1;
        function jump(pageindex) {
            page = pageindex;
            var submitData = new Object();
            submitData.Type = "jump";
            submitData.MinApplyDate = $("#MinApplyDate").val();
            submitData.MaxApplyDate = $("#MaxApplyDate").val();
            submitData.State = $("#State").val();
            submitData.page = page;
            transmitData(datatostr(submitData));
            return;
        }

        jQuery(function () {
            var MinApplyDate = new JsInputDate("MinApplyDate");
            var MaxApplyDate = new JsInputDate("MaxApplyDate");
        });

        var trid = "";
        reflist();
</script>
</body>
</html>