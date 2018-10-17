<%@ Page Language="C#" AutoEventWireup="true" Inherits="project.Presentation.Op.ChangeMeter,project"  %>
<!DOCTYPE>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="head1" runat="server">
    <title>换表管理</title>
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
    <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 表计管理 <span class="c-gray en">&gt;</span> 换表管理 <a class="btn btn-success radius r mr-20" style="line-height:1.6em;margin-top:2px" href="javascript:location.replace(location.href);" title="刷新" ><i class="Hui-iconfont">&#xe68f;</i></a></nav>
    <div id="list" class="pt-5 pr-20 pb-5 pl-20">
	    <div class="cl pd-5 bg-1 bk-gray mt-2"> 
            <span class="l">
                <%--<a href="javascript:;" onclick="insert()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe600;</i> 添加</a>
                <a href="javascript:;" onclick="update()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe60c;</i> 修改</a> 
                <a href="javascript:;" onclick="del()" class="btn btn-danger radius"><i class="Hui-iconfont">&#xe6e2;</i> 删除</a>
                <a href="javascript:;" onclick="audit()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe615;</i> 审核</a>--%>
                <%=Buttons %>
                <input type="hidden" id="selectKey" />
            </span> 
	    </div>
	    <div class="cl pd-10  bk-gray mt-2"> 
		    房间编号&nbsp;<input type="text" class="input-text size-MINI" style="width:150px" placeholder="房间编号" id="RMIDS" />&nbsp;
            原表记编号&nbsp;<input type="text" class="input-text size-MINI" style="width:150px" placeholder="原表记编号" id="OldMeterNoS" />&nbsp;
            新表记编号&nbsp;<input type="text" class="input-text size-MINI" style="width:150px" placeholder="新表记编号" id="NewMeterNoS" />&nbsp;
            表记类型&nbsp;<select class="input-text size-MINI" style="width:150px" id="OldMeterTypeS">
                <option value="" selected>全部</option>
                <option value="wm">水表</option>
                <option value="am">电表</option>
                </select>&nbsp;
		    <button type="submit" class="btn btn-success radius" onclick="select()"><i class="Hui-iconfont">&#xe665;</i> 查询</button>
	    </div>     
	    <div class="mt-5" id="outerlist">
	        <%=list %>
        </div>
    </div>
    <div id="edit" class="pt-5 pr-20 pb-5 pl-20" style="display:none;">
      <div class="form form-horizontal bk-gray mt-15 pb-10" id="editlist">
        <div class="row cl" style="display:none">
          <label class="form-label col-2"><span class="c-red">*</span>房间编号：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text required" placeholder="房间编号" id="RMID" style="width:240px;" disabled="disabled"  data-valid="between:0-30" data-error="" />
            <button type="button" class="btn btn-primary radius" id="chooseRMID">选择</button>
          </div>
          <div class="col-1"></div>
        </div>
    
        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>原表记编号：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text required" placeholder="原表记编号" id="OldMeterNo" style="width:240px;" data-valid="isNonEmpty" data-error="请选择原表记编号" />
            <button type="button" class="btn btn-primary radius" id="chooseOldMeterNo">选择</button>
          </div>
          <div class="col-1"></div>
        </div>

        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>原表上期读数：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text required" placeholder="原表上期读数" disabled="disabled" id="OldMeterLastReadout" data-valid="onlyNum" data-error="未读取上期读数"/>
          </div>
          <div class="col-1"></div>
        </div>

        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>原表换表止度：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text required" placeholder="原表换表止度" id="OldMeterReadout" data-valid="onlyNum" data-error="未读取上期止度"/>
          </div>
          <div class="col-1"></div>
        </div>

        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>原表行度：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text required" placeholder="原表行度" disabled="disabled" id="OldMeterReadings" data-valid="onlyNum" data-error="未读取原表行度"/>
        </div>
          <div class="col-1"></div>
        </div>

        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>新表记编号：</label>
          <div class="formControls col-4">
           <input type="text" class="input-text required" placeholder="新表记编号" id="NewMeterNo" data-valid="isNonEmpty||between:1-30" data-error="新表记编号不能为空||新表记编号长度为1-30位"/>
          </div>
          <div class="col-1"></div>
        </div>

        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>新表记名称：</label>
          <div class="formControls col-4">
           <input type="text" class="input-text required" placeholder="新表记名称" id="NewMeterName" data-valid="isNonEmpty||between:1-50" data-error="新表记名称不能为空||新表记编号长度为1-50位"/>
          </div>
        </div>

        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>新表记大小类型：</label>
          <div class="formControls col-4">
            <select class="input-text required" id="NewMeterSize" data-valid="isNonEmpty" data-error="请选择新表记类型">
                <option value="">请选择</option>
                <option value="1">大表</option>
                <option value="2">小表</option>
            </select>
          </div>
        </div>

        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>新表起度：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text required" placeholder="新表起度" id="NewMeterReadout" data-valid="onlyNum" data-error="需是数字"/>
          </div>
          <div class="col-1"></div>
        </div>

        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>新表倍率：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text required" placeholder="新表倍率" id="NewMeterRate" data-valid="onlyNum" data-error="需是数字"/>
          </div>
          <div class="col-1"></div>
        </div>

        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>新表位数：</label>
          <div class="formControls col-4">
            <select class="input-text w-200 required" id="NewMeterDigit" data-valid="isNonEmpty" data-error="请选择表记位数">
                <option value="4">4</option>
                <option value="5">5</option>
                <option value="6">6</option>
                <option value="7">7</option>
                <option value="8">8</option>
            </select>
          </div>
          <div class="col-1"></div>
        </div>

        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>换表人：</label>
          <div class="formControls col-4">
            <%--<input type="text" class="input-text required" placeholder="换表人" id="CMOperator" data-valid="isNonEmpty||between:1-30" data-error="请填写换表人||换表人长度为1-30位"/>--%>
            <%=CMOperatorStr %>
          </div>
          <div class="col-1"></div>
        </div>

        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>换表日期：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text required" placeholder="换表日期" id="CMDate" data-valid="isNonEmpty" data-error="请选择换表日期"/>
          </div>
          <div class="col-1"></div>
        </div>
       
        <div class="row cl">
          <div class="col-9 col-offset-3">
            <input class="btn btn-primary radius" type="button" onclick="submit()" value="&nbsp;&nbsp;提&nbsp;&nbsp;交&nbsp;&nbsp;" />
			<input class="btn btn-default radius" type="button" onclick="cancel()" value="&nbsp;&nbsp;取&nbsp;&nbsp;消&nbsp;&nbsp;" />
          </div>
        </div>
        <br /><br /><br /><br /><br /><br /><br />
      </div>
    </div>
    
    <script type="text/javascript" src="../../jscript/jquery-1.10.2.js"></script> 
    <script type="text/javascript" src="../../jscript/script_common.js"></script>
    <script type="text/javascript" src="../../jscript/json2.js"></script>
    <script type="text/javascript" src="../../jscript/H-ui.js"></script> 
    <script type="text/javascript" src="../../jscript/H-ui.admin.js"></script> 
    <script type="text/javascript" src="../../lib/layer/layer.js"></script> 
    <script type="text/javascript" src="../../lib/validate/jquery.validate.js"></script>
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
            if (vjson.type == "delete") {
                if (vjson.flag == "1") {
                    $("#outerlist").html(vjson.liststr);
                    $("#selectKey").val("");
                    reflist();
                }
                else {
                    layer.alert("数据操作出错！");
                }
                return;
            }
            if (vjson.type == "submit") {
                if (vjson.flag == "1") {
                    $("#outerlist").html(vjson.liststr);
                    $("#selectKey").val("");
                    $("#list").css("display", "");
                    $("#edit").css("display", "none");
                    reflist();
                }
                else if (vjson.flag == "3") {
                    showMsg("NewMeterNo", "新表记编号已存在", "1");
                }
                else if (vjson.flag == "4") {
                    showMsg("OldMeterNo", "旧表记编号不存在", "1");
                }
                else {
                    layer.msg("数据操作出错！");
                }
                return;
            }
            if (vjson.type == "update") {
                if (vjson.flag == "1") {
                    $("#RMID").val(vjson.RMID);
                    $("#OldMeterNo").val(vjson.OldMeterNo);
                    $("#OldMeterLastReadout").val(vjson.OldMeterLastReadout);
                    $("#OldMeterReadout").val(vjson.OldMeterReadout);
                    $("#OldMeterReadings").val(vjson.OldMeterReadings);
                    $("#NewMeterNo").val(vjson.NewMeterNo);
                    $("#NewMeterName").val(vjson.NewMeterName);
                    $("#NewMeterSize").val(vjson.NewMeterSize);
                    $("#NewMeterReadout").val(vjson.NewMeterReadout);
                    $("#NewMeterRate").val(vjson.NewMeterRate);
                    $("#NewMeterDigit").val(vjson.NewMeterDigit);
                    $("#CMOperator").val(vjson.CMOperator);
                    $("#CMDate").val(vjson.CMDate);

                    $("#chooseRMID").unbind('click');
                    $("#chooseOldMeterNo").unbind('click');
                    $("#list").css("display", "none");
                    $("#edit").css("display", "");
                }
                else if (vjson.flag == "3") {
                    layer.alert("当前状态不允许修改！");
                }
                else {
                    layer.alert("数据操作出错！");
                }
                return;
            }
            if (vjson.type == "audit") {
                if (vjson.flag == "1") {
                    layer.msg("审核成功！", { icon: 3, time: 1000 });

                    $("#outerlist").html(vjson.liststr);
                    $("#selectKey").val("");
                    reflist();
                }
                else if (vjson.flag == "3") {
                    layer.alert(vjson.InfoMsg);
                }
                else {
                    layer.alert("数据操作出错！");
                }
                return;
            }
            if (vjson.type == "getMeterInfo") {
                if (vjson.flag == "1") {
                    $("#OldMeterNo").val(vjson.OldMeterNo);
                    $("#OldMeterLastReadout").val(vjson.readout);                    
                    if ($("#OldMeterReadout").val() != "" && $("#OldMeterLastReadout").val() != "") {
                        $("#OldMeterReadings").val(parseFloat($("#OldMeterReadout").val()) - parseFloat($("#OldMeterLastReadout").val()))
                    }
                    else {
                        $("#OldMeterReadings").val("")
                    }
                }
                else {
                    layer.alert("数据操作出错！");
                }
                return;
            }
        }

        function insert() {
            $('#editlist').validate('reset');
            id = "";
            type = "insert";
            $("#chooseRMID").unbind('click').bind("click", chooseRMID);
            $("#chooseOldMeterNo").unbind('click').bind("click", chooseOldMeterNo);

            $("#RMID").val("");
            $("#OldMeterNo").val("");
            $("#OldMeterLastReadout").val("");
            $("#OldMeterReadout").val("");
            $("#OldMeterReadings").val("");
            $("#NewMeterNo").val("");
            $("#NewMeterName").val("");
            $("#NewMeterSize").val("");
            $("#NewMeterReadout").val("");
            $("#NewMeterRate").val("");
            $("#NewMeterDigit").val("");
            $("#CMOperator").val("");
            $("#CMDate").val("<%=date %>");

            $("#list").css("display", "none");
            $("#edit").css("display", "");
            return;
        }
        function update() {
            if ($("#selectKey").val() == "") {
                layer.msg("请先选择要修改的信息", { icon: 3, time: 1000 });
                return;
            }
            $('#editlist').validate('reset');

            id = $("#selectKey").val();
            type = "update";
            var submitData = new Object();
            submitData.Type = "update";
            submitData.id = id;

            transmitData(datatostr(submitData));
            return;
        }
        function submit() {
            if ($('#editlist').validate('submitValidate')) {
                var submitData = new Object();
                submitData.Type = "submit";
                submitData.id = id;
                submitData.RMID = $("#RMID").val();
                submitData.OldMeterNo = $("#OldMeterNo").val();
                submitData.OldMeterLastReadout = $("#OldMeterLastReadout").val();
                submitData.OldMeterReadout = $("#OldMeterReadout").val();
                submitData.OldMeterReadings = $("#OldMeterReadings").val();
                submitData.NewMeterNo = $("#NewMeterNo").val();
                submitData.NewMeterName = $("#NewMeterName").val();
                submitData.NewMeterSize = $("#NewMeterSize").val();
                submitData.NewMeterReadout = $("#NewMeterReadout").val();
                submitData.NewMeterRate = $("#NewMeterRate").val();
                submitData.NewMeterDigit = $("#NewMeterDigit").val();
                submitData.CMOperator = $("#CMOperator").val();
                submitData.CMDate = $("#CMDate").val();

                submitData.tp = type;
                submitData.RMIDS = $("#RMIDS").val();
                submitData.OldMeterNoS = $("#OldMeterNoS").val();
                submitData.NewMeterNoS = $("#NewMeterNoS").val();
                submitData.OldMeterTypeS = $("#OldMeterTypeS").val();
                submitData.page = page;
                transmitData(datatostr(submitData));
            }
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
                submitData.RMIDS = $("#RMIDS").val();
                submitData.OldMeterNoS = $("#OldMeterNoS").val();
                submitData.NewMeterNoS = $("#NewMeterNoS").val();
                submitData.OldMeterTypeS = $("#OldMeterTypeS").val();
                submitData.page = page;
                transmitData(datatostr(submitData));
                layer.close(index);
            });
            return;
        }
        function audit() {
            if ($("#selectKey").val() == "") {
                layer.msg("请先选择一条记录", { icon: 3, time: 1000 });
                return;
            }
            layer.confirm('确认要审核吗？', function (index) {
                var submitData = new Object();
                submitData.Type = "audit";
                submitData.id = $("#selectKey").val();
                submitData.RMIDS = $("#RMIDS").val();
                submitData.OldMeterNoS = $("#OldMeterNoS").val();
                submitData.NewMeterNoS = $("#NewMeterNoS").val();
                submitData.OldMeterTypeS = $("#OldMeterTypeS").val();
                submitData.page = page;
                transmitData(datatostr(submitData));
                layer.close(index);
            });
            return;
        }
        function select() {
            var submitData = new Object();
            submitData.Type = "select";
            submitData.RMIDS = $("#RMIDS").val();
            submitData.OldMeterNoS = $("#OldMeterNoS").val();
            submitData.NewMeterNoS = $("#NewMeterNoS").val();
            submitData.OldMeterTypeS = $("#OldMeterTypeS").val();
            submitData.page = 1;
            transmitData(datatostr(submitData));
            return;
        }
        function cancel() {
            id = "";
            $("#list").css("display", "");
            $("#edit").css("display", "none");
            return;
        }
        var page = 1;
        function jump(pageindex) {
            page = pageindex;
            var submitData = new Object();
            submitData.Type = "jump";
            submitData.RMIDS = $("#RMIDS").val();
            submitData.OldMeterNoS = $("#OldMeterNoS").val();
            submitData.NewMeterNoS = $("#NewMeterNoS").val();
            submitData.OldMeterTypeS = $("#OldMeterTypeS").val();
            submitData.page = page;
            transmitData(datatostr(submitData));
            return;
        }

        function chooseRMID() {
            var temp = "../Base/ChooseRMID.aspx?id=RMID";
            layer_show("选择页面", temp, 800, 630);
        }
        function chooseOldMeterNo() {
            //if ($("#RMID").val() == "") {
            //    layer.msg("请先选择房间编号", { icon: 3, time: 1000 });
            //    return;
            //}
            var temp = "../Base/ChooseMeter.aspx?id=OldMeterNo&RMID=" + $("#RMID").val();
            layer_show("选择页面", temp, 840, 600);
        }
        function choose(id, labels, values) {
            if (labels != "" && labels != undefined && labels != "undefined") {
                if (id == "OldMeterNo") {
                    var submitData = new Object();
                    submitData.Type = "getMeterInfo";
                    submitData.OldMeterNo = labels;
                    transmitData(datatostr(submitData));
                }
                else {
                    $("#" + id).val(labels);
                }
            }
        }

        $("#OldMeterNo").blur(function () {
            if ($("#OldMeterNo").val() != "") {
                var submitData = new Object();
                submitData.Type = "getMeterInfo";
                submitData.OldMeterNo = $("#OldMeterNo").val();
                transmitData(datatostr(submitData));
            }
            else {
                $("#OldMeterLastReadout").val("");
                $("#OldMeterReadings").val("")
            }
            return;
        });

        $("#OldMeterReadout").change(function () {
            if ($("#OldMeterReadout").val() != "" && $("#OldMeterLastReadout").val() != "") {
                $("#OldMeterReadings").val(parseFloat($("#OldMeterReadout").val()) - parseFloat($("#OldMeterLastReadout").val()))
            }
            else {
                $("#OldMeterReadings").val("")
            }
        });

        $('#editlist').validate({
            onFocus: function () {
                this.parent().addClass('active');
                return false;
            },
            onBlur: function () {
                var $parent = this.parent();
                var _status = parseInt(this.attr('data-status'));
                $parent.removeClass('active');
                if (!_status) {
                    $parent.addClass('error');
                }
                return false;
            }
        }, tiptype = "1");


        jQuery(function () {
            var CMDate = new JsInputDate("CMDate");
        });

        var trid = "";
        reflist();
</script>
</body>
</html>