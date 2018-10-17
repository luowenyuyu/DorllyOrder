<%@ Page Language="C#" AutoEventWireup="true" Inherits="project.Presentation.Platform.UserOrderTypeRight,project"  %>
<!DOCTYPE>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="head2" runat="server">
    <title>用户订单权限设置</title>
    <!--[if lt IE 9]>
    <script type="text/javascript" src="../jscript/html5.js"></script>
    <script type="text/javascript" src="../jscript/respond.min.js"></script>
    <![endif]-->
    <link href="../../css/H-ui.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/H-ui.admin.css" rel="stylesheet" type="text/css" />
    <link href="../../lib/iconfont/iconfont.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function transmitData(submitData) {
            var data = submitData;
            <%=ClientScript.GetCallbackEventReference(this, "data", "BandResuleData", null) %>
        }
    </script>
</head>
<body>
    <form id="form2" runat="server"></form>
    <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 系统管理 <span class="c-gray en">&gt;</span> 用户订单权限设置 <a class="btn btn-success radius r mr-20" style="line-height:1.6em;margin-top:2px" href="javascript:location.replace(location.href);" title="刷新" ><i class="Hui-iconfont">&#xe68f;</i></a></nav>
    <div id="list" class="pt-5 pr-20 pb-5 pl-20">
	    <div class="cl pd-3 bg-1 bk-gray mt-2"> 
            用户类别：<%=TypeStr %>&nbsp;
		    <button type="submit" class="btn btn-primary radius" onclick="submit()"><i class="Hui-iconfont">&#xe60c;</i> 保存</button>
	    </div>
	    <div class="mt-5" id="outerlist">
	    <%=list %>
	    </div>
    </div>
    
    <script type="text/javascript" src="../../jscript/jquery-1.10.2.js"></script> 
    <script type="text/javascript" src="../../jscript/script_ajax.js"></script>
    <script type="text/javascript" src="../../jscript/script_common.js"></script>
    <script type="text/javascript" src="../../jscript/json2.js"></script>
    <script type="text/javascript" src="../../jscript/H-ui.js"></script> 
    <script type="text/javascript" src="../../jscript/H-ui.admin.js"></script> 
    <script type="text/javascript" src="../../lib/layer/layer.js"></script> 
    <script type="text/javascript" src="../../lib/validate/jquery.validate.js"></script>
    <script type="text/javascript">
        function BandResuleData(temp) {
            var vjson = JSON.parse(temp);
            if (vjson.type == "select") {
                if (vjson.flag == "1") {
                    $("#outerlist").html(vjson.liststr);
                    return;
                }
            }
            if (vjson.type == "submit") {
                layer.closeAll('loading');
                if (vjson.flag == "1") {
                    layer.alert("保存成功！");
                }
                else if (vjson.errinfo == "保存出现错误！") {
                    layer.alert(vjson.errinfo);
                }
                else {
                    layer.alert("第" + vjson.errinfo + "行数据保存出错！");
                }
                return;
            }
        }

        function submit() {
            var items = "";

            $("input[name='chkmenu']").each(function () {
                var menu = $(this);
                if (menu.prop("checked")) {
                    items = items + menu[0].id + "@";
                }
            });

            layer.load(1);

            var submitData = new Object();
            submitData.Type = "submit";
            submitData.UserType = $("#UserType").val();
            submitData.ID = items;
            transmitData(datatostr(submitData));
            return;
        }

        jQuery(function () {
            $("#UserType").change(function () {
                var submitData = new Object();
                submitData.Type = "select";
                submitData.UserType = $("#UserType").val();
                transmitData(datatostr(submitData));
                return;
            });            
        });
</script>
</body>
</html>