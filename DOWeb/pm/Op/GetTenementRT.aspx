<%@ Page Language="C#" AutoEventWireup="true" Inherits="project.Presentation.Op.GetTenementRT,project" %>

<!DOCTYPE>
<html xmlns="http://www.w3.org/1999/xhtml">
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
    <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 报表管理 <span class="c-gray en">&gt;</span> 出租情况统计表 <a class="btn btn-success radius r mr-20" style="line-height: 1.6em; margin-top: 2px" href="javascript:location.replace(location.href);" title="刷新"><i class="Hui-iconfont">&#xe68f;</i></a></nav>
    <div id="list" class="pt-5 pr-20 pb-5 pl-20">
        <div class="cl pd-10  bk-gray mt-2">
            <span>客户名称：</span>&nbsp;<input type="text" class="input-text size-MINI" placeholder="" id="CustName" style="width: 110px" />
            <span>服务商  ：</span>&nbsp;<%=SPSelect %>
            <span>合同类型：</span>&nbsp;<%=CTSelect %>
            <span>合同状态：</span>&nbsp;<%=CSSelect %>
            <span>退租状态：</span>&nbsp;<%=OSSelect %>
            <!--SignStartDate SignEndDate ExpireStartDate ExpireEndDate RealStartDate  RealEndData  ServiceProvider ContractTypeNo ContractStatus OffLeaseStatus -->
            <br />
            <span>签订日期：</span> 从&nbsp;<input type="text" class="input-text size-MINI" id="SignStartDate" style="width: 110px" />
          &nbsp;&nbsp;  至&nbsp;&nbsp;<input type="text" class="input-text size-MINI" id="SignEndDate" style="width: 110px" />

            <span>到期日期：</span> 从&nbsp;<input type="text" class="input-text size-MINI" id="ExpireStartDate" style="width: 110px" />
           &nbsp;&nbsp;  至&nbsp;&nbsp;<input type="text" class="input-text size-MINI" id="ExpireEndDate" style="width: 110px" />

            <span>实际退租日期：</span> 从&nbsp;<input type="text" class="input-text size-MINI" id="RealStartDate" style="width: 110px" />
            &nbsp;&nbsp;  至&nbsp;&nbsp;<input type="text" class="input-text size-MINI" id="RealEndDate" style="width: 110px" />

            <button type="submit" class="btn btn-success radius" onclick="select()"><i class="Hui-iconfont">&#xe665;</i> 检索</button>&nbsp;&nbsp;
            <%=Buttons %>
        </div>
        <div class="mt-5" id="outerlist">
            <%=list %>
        </div>
    </div>
    <iframe id="openfile" width="0" height="0" marginwidth="0" frameborder="0" src="about:blank" content-disposition="attachment"></iframe>
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
            if (vjson.type == "getdate") {
                var SignStartDate = new JsInputDate("SignStartDate");
                var SignEndDate = new JsInputDate("SignEndDate");
                if (vjson.flag == "1") {
                    SignStartDate.setValue(vjson.start);
                    SignEndDate.setValue(vjson.end);
                }
            }
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
            submitData.SignStartDate = $("#SignStartDate").val();
            submitData.SignEndDate = $("#SignEndDate").val();
            submitData.ExpireStartDate = $("#ExpireStartDate").val();
            submitData.ExpireEndDate = $("#ExpireEndDate").val();
            submitData.RealStartDate = $("#RealStartDate").val();
            submitData.RealEndData = $("#RealEndData").val();
            submitData.CustName = $("#CustName").val();
            submitData.SPNo = $("#SPNo option:selected").val();
            submitData.ContractStatus = $("#ContractStatus  option:selected").val();
            submitData.OffLeaseStatus = $("#OffLeaseStatus  option:selected").val();
            submitData.ContractTypeNo = $("#ContractTypeNo  option:selected").val();
            transmitData(datatostr(submitData));
            return;
        }

        function select() {
            var submitData = new Object();
            submitData.Type = "select";
            submitData.SignStartDate = $("#SignStartDate").val();
            submitData.SignEndDate = $("#SignEndDate").val();
            submitData.ExpireStartDate = $("#ExpireStartDate").val();
            submitData.ExpireEndDate = $("#ExpireEndDate").val();
            submitData.RealStartDate = $("#RealStartDate").val();
            submitData.RealEndData = $("#RealEndData").val();
            submitData.CustName = $("#CustName").val();
            submitData.SPNo = $("#SPNo option:selected").val();
            submitData.ContractStatus = $("#ContractStatus  option:selected").val();
            submitData.OffLeaseStatus = $("#OffLeaseStatus  option:selected").val();
            submitData.ContractTypeNo = $("#ContractTypeNo  option:selected").val();
            transmitData(datatostr(submitData));
            return;
        }

        jQuery(function () {           
            var ExpireStartDate = new JsInputDate("ExpireStartDate");
            var ExpireEndDate = new JsInputDate("ExpireEndDate");
            var RealStartDate = new JsInputDate("RealStartDate");
            var RealEndDate = new JsInputDate("RealEndDate");          
            ExpireStartDate.setValue("");
            ExpireEndDate.setValue("");
            RealStartDate.setValue("");
            RealEndDate.setValue("");
            var djson = "Type:getdate";
            transmitData(djson);
        });
    </script>
</body>
</html>
