<%@ Page Language="C#" AutoEventWireup="true" Inherits="project.Presentation.Base.ChooseRMID,project"  %>
<!DOCTYPE>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="head1" runat="server">
    <title>选择房间资料</title>
    <!--[if lt IE 9]>
    <script type="text/javascript" src="../../jscript/html5.js"></script>
    <script type="text/javascript" src="../../jscript/respond.min.js"></script>
    <![endif]-->
    <link href="../../css/H-ui.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/H-ui.admin.css" rel="stylesheet" type="text/css" />
    <link href="../../lib/iconfont/iconfont.css" rel="stylesheet" type="text/css" />
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function transmitData(submitData) {
            var data = submitData;
            <%=ClientScript.GetCallbackEventReference(this, "data", "BandResuleData", null) %>
        }
    </script>
</head>
<body>
    <form id="form1" runat="server"></form>
    <div>
    <div id="list" class="pt-5 pr-20 pb-5 pl-20">  
	    <div class="cl pd-3 bg-1 bk-gray mt-2"> 
            园区&nbsp;<%=RMLOCNo1Str %>&nbsp;
            建设期&nbsp;<select class="input-text required size-MINI" id="RMLOCNo2" style="width:120px"><option value="">全部</option></select>&nbsp;
            楼栋&nbsp;<select class="input-text required size-MINI" id="RMLOCNo3" style="width:120px"><option value="">全部</option></select>&nbsp;
            楼层&nbsp;<select class="input-text required size-MINI" id="RMLOCNo4" style="width:120px"><option value="">全部</option></select>&nbsp;
            <br />
            房间号&nbsp;<input type="text" class="input-text required size-MINI" id="RMID" style="width:120px" />&nbsp;
            当前客户&nbsp;<input type="text" class="input-text required size-MINI" id="CustName" style="width:120px" />&nbsp;
		    <button type="submit" class="btn btn-success radius" onclick="select()"><i class="Hui-iconfont">&#xe665;</i> 检索</button>
	    </div>
	    <div class="mt-5" id="outerlist">
	    <%=list %>
	    </div>
    </div>
    </div>
    
    <script type="text/javascript" src="../../jscript/jquery-1.10.2.js"></script> 
    <script type="text/javascript" src="../../jscript/script_ajax.js"></script>
    <script type="text/javascript" src="../../jscript/script_common.js"></script>
    <script type="text/javascript" src="../../jscript/json2.js"></script>
    <script type="text/javascript" src="../../jscript/H-ui.js"></script> 
    <script type="text/javascript" src="../../jscript/H-ui.admin.js"></script> 
    <script type="text/javascript">

       function transmitData(submitData) {
            var temp = submitData;
          <%=ClientScript.GetCallbackEventReference(this, "temp", "BandResuleData", null) %>
        }

        function BandResuleData(temp) {
            var vjson = JSON.parse(temp);
            if (vjson.type == "select") {
                if (vjson.flag == "1") {
                    $("#outerlist").html(vjson.liststr);
                    return;
                }
            }
            if (vjson.type == "getvalue") {
                if (vjson.flag == "1") {
                    $("#" + vjson.child).empty();
                    $("#" + vjson.child).append("<option value=''>请选择</option>");

                    for (var i = 0; i <= vjson.row - 1; i++) {
                        var option = "<option value='" + vjson.subtype.split(";")[i].split(":")[0].toString() + "'>" + vjson.subtype.split(";")[i].split(":")[1].toString() + "</option>";
                        $("#" + vjson.child).append(option);
                    }

                    if (vjson.child == "RMLOCNo2") {
                        $("#RMLOCNo3").empty();
                        $("#RMLOCNo3").append("<option value=''>请选择</option>");
                        $("#RMLOCNo4").empty();
                        $("#RMLOCNo4").append("<option value=''>请选择</option>");
                    }
                    else if (vjson.child == "RMLOCNo3") {
                        $("#RMLOCNo4").empty();
                        $("#RMLOCNo4").append("<option value=''>请选择</option>");
                    }
                }
                return;
            }
        }

        function select() {
            var submitData = new Object();
            submitData.Type = "select";
            submitData.RMLOCNo1 = $("#RMLOCNo1").val();
            submitData.RMLOCNo2 = $("#RMLOCNo2").val();
            submitData.RMLOCNo3 = $("#RMLOCNo3").val();
            submitData.RMLOCNo4 = $("#RMLOCNo4").val();
            submitData.RMID = $("#RMID").val();
            submitData.CustName = $("#CustName").val();
            submitData.page = 1;
            transmitData(datatostr(submitData));
            return;
        }

        function cancel() {
            var index = parent.layer.getFrameIndex(window.name);
            parent.layer.close(index);
            return;
        }

        var page = 1;
        function jump(pageindex) {
            page = pageindex;
            var submitData = new Object();
            submitData.Type = "select";
            submitData.RMLOCNo1 = $("#RMLOCNo1").val();
            submitData.RMLOCNo2 = $("#RMLOCNo2").val();
            submitData.RMLOCNo3 = $("#RMLOCNo3").val();
            submitData.RMLOCNo4 = $("#RMLOCNo4").val();
            submitData.RMID = $("#RMID").val();
            submitData.CustName = $("#CustName").val();
            submitData.page = page;
            transmitData(datatostr(submitData));
            return;
        }

        function submit(id,guid) {
            parent.choose("<%=id %>", id, $("#it" + guid).val());
            var index = parent.layer.getFrameIndex(window.name);
            parent.layer.close(index);
            return;
        }


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
    </script>
</body>
</html>