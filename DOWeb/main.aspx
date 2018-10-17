<%@ Page Language="C#" AutoEventWireup="true" Inherits="project.Presentation.main,project" %>
<!DOCTYPE HTML>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>我的桌面</title>
    <!--[if lt IE 9]>
    <script type="text/javascript" src="lib/html5.js"></script>
    <script type="text/javascript" src="lib/respond.min.js"></script>
    <![endif]-->
    <link href="css/H-ui.min.css" rel="stylesheet" type="text/css" />
    <link href="css/H-ui.login.css" rel="stylesheet" type="text/css" />
    <link href="css/main.css" rel="stylesheet" type="text/css" />
    <link href="lib/iconfont/iconfont.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function transmitData(submitData) {
            var data = submitData;
                <%=ClientScript.GetCallbackEventReference(this, "data", "BandResuleData", null) %>
            }
    </script>
</head>
<body>
   <form id="form1" runat="server"></form>
   <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 我的桌面 <a class="btn btn-success radius r mr-20" style="line-height:1.6em;margin-top:2px" href="javascript:location.replace(location.href);" title="刷新" ><i class="Hui-iconfont">&#xe68f;</i></a></nav>
   <div class="page-container cl" style="margin-left:20px;">
    <div class="welcome_content">
        <div class="pd-10 bk-gray radius box-shadow" style="margin-top:5px;">
            <div id="advanced_search">
                <%=RMLOCNo1Str %>
                <%=RMLOCNo2Str %>
                <input type="text" id="RMID" placeholder=" 房屋编码" class="input-text size-MINI" style="width:120px;vertical-align:middle" />
                <input type="text" id="CustName" placeholder=" 客户名称" class="input-text size-MINI" style="width:120px;vertical-align:middle" />
		        <select id="RMStatus" class="input-text size-MINI" style="width:120px;vertical-align:middle">
                    <option value="" selected="selected">全部</option>
                    <option value="free">空闲</option>
                    <option value="use">已租</option>
                    <option value="reserve">预留</option>
                </select>
                <button class="btn btn-success" type="button" onclick="select()"><i class="Hui-iconfont">&#xe665;</i> 搜索</button>
            </div>

        </div>
        <div class="mt-15  bk-gray f-l" style="width: 100%"><!--查询结果-->
            <div class="pd-10">
                栋数：
                <div class="btn-group dil va-m" id="building">
                    <%=RMLOCNo3Str %>
                </div>
                <div class="line mt-5"></div>
                <div id="rmlist">
                    <%=rmlist %>
                </div>
            </div>
        </div>
    </div>

    <div class="welcome_sidebar">
        <div class="remind">
            <h4>事务提醒</h4>
            <%=trans %> 
            <%--<dl style="clear:both;"></dl>--%>           
        </div>

       <div class="remind">
            <h4>园区概况</h4>
            <%=packprofile %>
            <%--<dl style="clear:both;"></dl>--%>
        </div>

        <div class="clear">
            <h4>新租提醒</h4>
            <%--<a class="btn-more">more</a>--%>
            <%=newRentRemind %> 
        </div>

        <div>
            <h4>预约退租提醒</h4>
            <%--<a class="btn-more">more</a>--%>
            <%=reservationRemind %> 
        </div>

        <div>
            <h4>合同到期提醒</h4>
            <%--<a class="btn-more">more</a>--%>
            <%=expireRemind %> 
        </div>
        <div>
            <h4>退租提醒</h4>
            <%--<a class="btn-more">more</a>--%>
            <%=endRentRemind %> 
        </div>
        
    </div>
</div>
    
    <script type="text/javascript" src="jscript/jquery-1.10.2.js"></script> 
    <script type="text/javascript" src="jscript/script_common.js"></script>
    <script type="text/javascript" src="jscript/json2.js"></script>
    <script type="text/javascript" src="jscript/H-ui.js"></script>
    <script type="text/javascript">
        $(function () {

        })
        function BandResuleData(temp) {
            var vjson = JSON.parse(temp);
            if (vjson.type == "select") {
                if (vjson.flag == "1") {
                    $("#building").html(vjson.building);
                    $("#rmlist").html(vjson.rmlist);
                }
            }
            if (vjson.type == "getvalue") {
                if (vjson.flag == "1") {
                    $("#" + vjson.child).empty();
                    for (var i = 0; i <= vjson.row - 1; i++) {
                        if (i == 0)
                            var option = "<option value='" + vjson.subtype.split(";")[i].split(":")[0].toString() + "' selected='selected'>" + vjson.subtype.split(";")[i].split(":")[1].toString() + "</option>";
                        else
                            var option = "<option value='" + vjson.subtype.split(";")[i].split(":")[0].toString() + "'>" + vjson.subtype.split(";")[i].split(":")[1].toString() + "</option>";
                        $("#" + vjson.child).append(option);
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
            submitData.RMID = $("#RMID").val();
            submitData.CustName = $("#CustName").val();
            submitData.RMStatus = $("#RMStatus").val();

            RMLOCNo1 = $("#RMLOCNo1").val();
            RMLOCNo2 = $("#RMLOCNo2").val();
            RMID = $("#RMID").val();
            CustName = $("#CustName").val();
            RMStatus = $("#RMStatus").val();
            transmitData(datatostr(submitData));
            return;
        }

        function getRM(loc) {
            var submitData = new Object();
            submitData.Type = "getRM";
            submitData.RMLOCNo1 = RMLOCNo1;
            submitData.RMLOCNo2 = RMLOCNo2;
            submitData.RMLOCNo3 = loc;
            submitData.RMID = RMID;
            submitData.CustName = CustName;
            submitData.RMStatus = RMStatus;
            transmitData(datatostr(submitData));
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

        var RMLOCNo1 = "<%=locno %>";
        var RMLOCNo2 = "<%=locno1 %>";
        var RMID = "";
        var CustName = "";
        var RMStatus = "";
    </script>
</body>
</html>