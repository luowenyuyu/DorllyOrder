<%@ Page Language="C#" AutoEventWireup="true" Inherits="project.Presentation.Base.Service,project"  %>
<!DOCTYPE>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="head1" runat="server">
    <title>费用资料</title>
    <!--[if lt IE 9]>
    <script type="text/javascript" src="../jscript/html5.js"></script>
    <script type="text/javascript" src="../jscript/respond.min.js"></script>
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
    <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 基础资料 <span class="c-gray en">&gt;</span> 费用资料 <a class="btn btn-success radius r mr-20" style="line-height:1.6em;margin-top:2px" href="javascript:location.replace(location.href);" title="刷新" ><i class="Hui-iconfont">&#xe68f;</i></a></nav>
    <div id="list" class="pt-5 pr-20 pb-5 pl-20">
	    <div class="cl pd-5 bg-1 bk-gray mt-2"> 
            <span class="l">
                <%--<a href="javascript:;" onclick="insert()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe600;</i> 添加</a>
                <a href="javascript:;" onclick="update()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe60c;</i> 修改</a> 
                <a href="javascript:;" onclick="del()" class="btn btn-danger radius"><i class="Hui-iconfont">&#xe6e2;</i> 删除</a>
                <a href="javascript:;" onclick="valid()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe615;</i> 启用/停用</a>--%>
                <%=Buttons %>
                <input type="hidden" id="selectKey" />
            </span> 
	    </div>
        
	    <div class="cl pd-10  bk-gray mt-2"> 
            收费项目编号<input type="text" class="input-text size-MINI" style="width:120px" placeholder="收费项目编号" id="SRVNoS" />&nbsp;
            收费项目名称<input type="text" class="input-text size-MINI" style="width:120px" placeholder="收费项目名称" id="SRVNameS" />&nbsp;
            所属服务大类&nbsp;<%=SRVTypeNo1StrS %>&nbsp;
            所属服务小类&nbsp;<%=SRVTypeNo2StrS %>&nbsp;
            <br />
		    收费科目&nbsp;<%=CANoStrS %>&nbsp;
            服务商&nbsp;<%=SRVSPNoStrS %>&nbsp;
            收费方式&nbsp;<select class="input-text size-MINI" style="width:120px" id="SRVCalTypeS">
                    <option value="">全部</option>
                    <option value="1">按出租面积</option>
                    <option value="2">按使用量</option>
                    <option value="3">按天数</option>
                    <option value="4">按次数</option>
                    <option value="5">滞纳</option>
                </select>
		    <button type="button" class="btn btn-success radius" onclick="select()"><i class="Hui-iconfont">&#xe665;</i> 查询</button>
	    </div>
	    <div class="mt-5" id="outerlist">
	    <%=list %>
        </div>
    </div>
    <div id="edit" class="pt-5 pr-20 pb-5 pl-20" style="display:none;">
      <div class="form form-horizontal bk-gray mt-15 pb-10" id="editlist">
        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>收费项目编号：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text required" placeholder="收费项目编号" id="SRVNo" data-valid="isNonEmpty||between:1-16" data-error="收费项目编号不能为空||收费项目编号长度为1-30位" />
          </div>
            <div class="col-3"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>收费项目名称：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text required" placeholder="收费项目名称" id="SRVName" data-valid="isNonEmpty||between:2-80" data-error="收费项目名称不能为空||收费项目名称长度为2-50位"/>
          </div>
          <div class="col-3"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>所属服务大类：</label>
          <div class="formControls col-4">
            <%=SRVTypeNo1Str %>
          </div>
          <div class="col-3"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>所属服务小类：</label>
          <div class="formControls col-4">
            <%=SRVTypeNo2Str %>
          </div>
          <div class="col-3"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>所属服务商：</label>
          <div class="formControls col-4">
            <%=SRVSPNoStr %>
          </div>
          <div class="col-3"></div>
        </div>
        <div class="row cl" style="display:none;">
          <label class="form-label col-2"><span class="c-red">*</span>收费方式：</label>
          <div class="formControls col-4">
            <select class="input-text required" id="SRVCalType" style="width:240px;" data-valid="between:0-30" data-error="">
                <option value=""></option>
                <option value="1">按出租面积</option>
                <option value="2">按使用量</option>
                <option value="3">按天数</option>
                <option value="4">按次数</option>
                <option value="5">滞纳</option>
            </select>
          </div>
          <div class="col-3"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>财务收费科目：</label>
          <div class="formControls col-4">
            <%=CANoStr %>
          </div>
          <div class="col-3"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>取整方式：</label>
          <div class="formControls col-4">
            <select class="input-text required" id="SRVRoundType" style="width:240px;" data-valid="isNonEmpty" data-error="请选择取整方式">
                <option value=""></option>
                <option value="round">四舍五入</option>
                <option value="ceiling">向上取位</option>
                <option value="floor">向下取位</option>
            </select>
          </div>
          <div class="col-3"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>精准位数：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text required" placeholder="精准位数" id="SRVDecimalPoint" onchange="validInt(this.id)" data-valid="isNonEmpty" data-error="请填写精准位数"/>
          </div>
          <div class="col-3"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>倍率：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text required" placeholder="倍率" id="SRVRate" onchange="validDecimal(this.id)" data-valid="isNonEmpty" data-error="请填写倍率"/>
          </div>
          <div class="col-3"></div>
        </div>
        <div class="row cl" style="display:none;">
          <label class="form-label col-2"><span class="c-red">*</span>税率：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text required" placeholder="税率" id="SRVTaxRate" onchange="validDecimal(this.id)" data-valid="isNonEmpty" data-error="请填写税率"/>
          </div>
          <div class="col-3"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2">备注：</label>
          <div class="formControls col-4">
            <textarea cols="" rows="" class="textarea required" placeholder="备注" id="SRVRemark" data-valid="between:0-300" data-error="备注长度为0-300位"></textarea>
          </div>
          <div class="col-2"></div>
        </div>
        
        <div class="row cl">
          <div class="col-9 col-offset-4">
            <input class="btn btn-primary radius" type="button" onclick="submit()" value="&nbsp;&nbsp;提&nbsp;&nbsp;交&nbsp;&nbsp;" />
			<input class="btn btn-default radius" type="button" onclick="cancel()" value="&nbsp;&nbsp;取&nbsp;&nbsp;消&nbsp;&nbsp;" />
          </div>
        </div>
      </div>
    </div>
    
    <script type="text/javascript" src="../../jscript/jquery-1.10.2.js"></script> 
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
                    showMsg("SRVNo", "此费用项目编号已存在", "1");
                }
                else {
                    layer.msg("数据操作出错！");
                }
                return;
            }
            if (vjson.type == "update") {
                if (vjson.flag == "1") {
                    $("#SRVNo").val(vjson.SRVNo);
                    $("#SRVName").val(vjson.SRVName);
                    $("#SRVTypeNo1").val(vjson.SRVTypeNo1);
                    $("#SRVSPNo").val(vjson.SRVSPNo);
                    $("#SRVCalType").val(vjson.SRVCalType);
                    $("#SRVRoundType").val(vjson.SRVRoundType);
                    $("#SRVDecimalPoint").val(vjson.SRVDecimalPoint);
                    $("#SRVRate").val(vjson.SRVRate);
                    $("#SRVTaxRate").val(vjson.SRVTaxRate);
                    $("#SRVRemark").val(vjson.SRVRemark);

                    $("#SRVTypeNo2").empty();
                    $("#SRVTypeNo2").append("<option value=''>请选择所属服务小类</option>");
                    for (var i = 0; i <= vjson.row - 1; i++) {
                        var option = "<option value='" + vjson.subtype.split(";")[i].split(":")[0].toString() + "'>" + vjson.subtype.split(";")[i].split(":")[1].toString() + "</option>";
                        $("#SRVTypeNo2").append(option);
                    }
                    $("#SRVTypeNo2").val(vjson.SRVTypeNo2);

                    $("#CANo").empty();
                    $("#CANo").append("<option value=''></option>");
                    for (var i = 0; i <= vjson.row1 - 1; i++) {
                        var option = "<option value='" + vjson.subtype1.split(";")[i].split(":")[0].toString() + "'>" + vjson.subtype1.split(";")[i].split(":")[1].toString() + "</option>";
                        $("#CANo").append(option);
                    }
                    $("#CANo").val(vjson.CANo);

                    $("#SRVNo").attr("disabled", true);
                    $("#list").css("display", "none");
                    $("#edit").css("display", "");
                }
                else {
                    layer.alert("数据操作出错！");
                }
                return;
            }
            if (vjson.type == "valid") {
                if (vjson.flag == "1") {
                    $("#" + vjson.id).find(".td-status").html(vjson.stat);
                    layer.alert(vjson.stat + "成功！");
                }
                else {
                    layer.alert("停止(启用)出现异常！");
                }
                return;
            }

            if (vjson.type == "getsubtype") {
                if (vjson.flag == "1") {
                    $("#SRVTypeNo2").empty();
                    $("#SRVTypeNo2").append("<option value=''></option>");
                    for (var i = 0; i <= vjson.row - 1; i++) {
                        var option = "<option value='" + vjson.subtype.split(";")[i].toString().split(":")[0].toString() + "'>" + vjson.subtype.split(";")[i].toString().split(":")[1].toString() + "</option>";
                        $("#SRVTypeNo2").append(option);
                    }
                }
                return;
            }

            if (vjson.type == "getsubtypes") {
                if (vjson.flag == "1") {
                    $("#SRVTypeNo2S").empty();
                    $("#SRVTypeNo2S").append("<option value=''>全部</option>");
                    for (var i = 0; i <= vjson.row - 1; i++) {
                        var option = "<option value='" + vjson.subtype.split(";")[i].toString().split(":")[0].toString() + "'>" + vjson.subtype.split(";")[i].toString().split(":")[1].toString() + "</option>";
                        $("#SRVTypeNo2S").append(option);
                    }
                }
                return;
            }

            if (vjson.type == "getcano") {
                if (vjson.flag == "1") {
                    $("#CANo").empty();
                    $("#CANo").append("<option value=''></option>");
                    for (var i = 0; i <= vjson.row - 1; i++) {
                        var option = "<option value='" + vjson.subtype.split(";")[i].toString().split(":")[0].toString() + "'>" + vjson.subtype.split(";")[i].toString().split(":")[1].toString() + "</option>";
                        $("#CANo").append(option);
                    }
                }
                return;
            }
        }

        function insert() {
            $('#editlist').validate('reset');
            id = "";
            type = "insert";
            $("#SRVNo").attr("disabled", false);
            $("#SRVNo").val("");
            $("#SRVName").val("");
            $("#SRVTypeNo1").val("");
            $("#SRVTypeNo2").val("");
            $("#SRVSPNo").val("");
            $("#CANo").val("");
            $("#SRVCalType").val("");
            $("#SRVRoundType").val("");
            $("#SRVDecimalPoint").val("");
            $("#SRVRate").val("");
            $("#SRVTaxRate").val("0");
            $("#SRVRemark").val("");

            $("#SRVTypeNo2").empty();
            $("#CANo").empty();
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
                submitData.SRVNo = $("#SRVNo").val();
                submitData.SRVName = $("#SRVName").val();
                submitData.SRVTypeNo1 = $("#SRVTypeNo1").val();
                submitData.SRVTypeNo2 = $("#SRVTypeNo2").val();
                submitData.SRVSPNo = $("#SRVSPNo").val();
                submitData.CANo = $("#CANo").val();
                submitData.SRVCalType = $("#SRVCalType").val();
                submitData.SRVRoundType = $("#SRVRoundType").val();
                submitData.SRVDecimalPoint = $("#SRVDecimalPoint").val();
                submitData.SRVRate = $("#SRVRate").val();
                submitData.SRVTaxRate = $("#SRVTaxRate").val();
                submitData.SRVRemark = $("#SRVRemark").val();

                submitData.tp = type;
                submitData.SRVNoS = $("#SRVNoS").val();
                submitData.SRVNameS = $("#SRVNameS").val();
                submitData.SRVTypeNo1S = $("#SRVTypeNo1S").val();
                submitData.SRVTypeNo2S = $("#SRVTypeNo2S").val();
                submitData.SRVSPNoS = $("#SRVSPNoS").val();
                submitData.CANoS = $("#CANoS").val();
                submitData.SRVCalTypeS = $("#SRVCalTypeS").val();
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

                submitData.SRVNoS = $("#SRVNoS").val();
                submitData.SRVNameS = $("#SRVNameS").val();
                submitData.SRVTypeNo1S = $("#SRVTypeNo1S").val();
                submitData.SRVTypeNo2S = $("#SRVTypeNo2S").val();
                submitData.SRVSPNoS = $("#SRVSPNoS").val();
                submitData.CANoS = $("#CANoS").val();
                submitData.SRVCalTypeS = $("#SRVCalTypeS").val();
                submitData.page = page;
                transmitData(datatostr(submitData));
                layer.close(index);
            });
            return;
        }
        function select() {
            var submitData = new Object();
            submitData.Type = "select";
            submitData.SRVNoS = $("#SRVNoS").val();
            submitData.SRVNameS = $("#SRVNameS").val();
            submitData.SRVTypeNo1S = $("#SRVTypeNo1S").val();
            submitData.SRVTypeNo2S = $("#SRVTypeNo2S").val();
            submitData.SRVSPNoS = $("#SRVSPNoS").val();
            submitData.CANoS = $("#CANoS").val();
            submitData.SRVCalTypeS = $("#SRVCalTypeS").val();
            submitData.page = 1;
            transmitData(datatostr(submitData));
            return;
        }
        function valid() {
            if ($("#selectKey").val() == "") {
                layer.msg("请先选择要停止(启用)的信息！");
                return;
            }

            layer.confirm('你确定停止(启用)吗？', function (index) {
                var submitData = new Object();
                submitData.Type = "valid";
                submitData.id = $("#selectKey").val();

                transmitData(datatostr(submitData));
            });
            return;
        }
        function cancel() {
            id = "";
            $("#list").css("display", "");
            $("#edit").css("display", "none");
            return;
        }

        jQuery("#SRVTypeNo1").change(function () {
            var submitData = new Object();
            submitData.Type = "getsubtype";
            submitData.SRVTypeNo1 = $("#SRVTypeNo1").val();
            transmitData(datatostr(submitData));
            return;
        });
        jQuery("#SRVTypeNo1S").change(function () {
            var submitData = new Object();
            submitData.Type = "getsubtypes";
            submitData.SRVTypeNo1 = $("#SRVTypeNo1S").val();
            transmitData(datatostr(submitData));
            return;
        });
        jQuery("#SRVSPNo").change(function () {
            var submitData = new Object();
            submitData.Type = "getcano";
            submitData.SRVSPNo = $("#SRVSPNo").val();
            transmitData(datatostr(submitData));
            return;
        });

        var page = 1;
        function jump(pageindex) {
            page = pageindex;
            var submitData = new Object();
            submitData.Type = "jump";
            submitData.SRVNoS = $("#SRVNoS").val();
            submitData.SRVNameS = $("#SRVNameS").val();
            submitData.SRVTypeNo1S = $("#SRVTypeNo1S").val();
            submitData.SRVTypeNo2S = $("#SRVTypeNo2S").val();
            submitData.SRVSPNoS = $("#SRVSPNoS").val();
            submitData.CANoS = $("#CANoS").val();
            submitData.SRVCalTypeS = $("#SRVCalTypeS").val();
            submitData.page = page;
            transmitData(datatostr(submitData));
            return;
        }

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

        var trid = "";
        reflist();
</script>
</body>
</html>