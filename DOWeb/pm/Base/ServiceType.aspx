﻿<%@ Page Language="C#" AutoEventWireup="true" Inherits="project.Presentation.Base.ServiceType,project"  %>
<!DOCTYPE>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="head1" runat="server">
    <title>服务类型</title>
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
    <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 基础资料 <span class="c-gray en">&gt;</span> 服务类型 <a class="btn btn-success radius r mr-20" style="line-height:1.6em;margin-top:2px" href="javascript:location.replace(location.href);" title="刷新" ><i class="Hui-iconfont">&#xe68f;</i></a></nav>
    <div id="list" class="pt-5 pr-20 pb-5 pl-20">
	    <div class="cl pd-3 bg-1 bk-gray mt-2"> 
            <span class="l">
                <%--<a href="javascript:;" onclick="insert()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe600;</i> 添加</a>
                <a href="javascript:;" onclick="update()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe60c;</i> 修改</a> 
                <a href="javascript:;" onclick="del()" class="btn btn-danger radius"><i class="Hui-iconfont">&#xe6e2;</i> 删除</a>
                <a href="javascript:;" onclick="valid()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe615;</i> 启用/停用</a>--%>
                <%=Buttons %>
                <input type="hidden" id="selectKey" />
            </span> 
	    </div>
	    <div class="mt-5" id="outerlist">
	    <%=list %>
	    </div>
    </div>
    <div id="edit" class="pt-5 pr-20 pb-5 pl-20" style="display:none;">
      <div class="form form-horizontal bk-gray mt-15 pb-10" id="editlist">
        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>服务类型编号：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text required" placeholder="服务类型编号" id="SRVTypeNo" data-valid="isNonEmpty||between:1-16" data-error="服务类型编号不能为空||服务类型编号长度为1-16位" />
          </div>
            <div class="col-3"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>服务类型名称：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text required" placeholder="服务类型名称" id="SRVTypeName" data-valid="isNonEmpty||between:2-50" data-error="服务类型名称不能为空||服务类型名称长度为2-50位"/>
          </div>
          <div class="col-3"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>父项类型：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text required" placeholder="服务类型名称" id="ParentTypeNoName" onblur="check('ParentTypeNo','SRVType')" style="width:240px;"  data-valid="between:0-50" data-error="父项类型长度为0-50位"/>
            <input type="hidden" id="ParentTypeNo" />
            <button type="button" class="btn btn-primary radius" id="chooseParentType">选择</button>
          </div>
          <div class="col-3"></div>
        </div>
        <div class="row cl" style="display:none;">
          <label class="form-label col-2">服务商：</label>
          <div class="formControls col-4">
            <%=SRVSPNoStr %>
          </div>
          <div class="col-3"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2">描述：</label>
          <div class="formControls col-5">
            <textarea cols="" rows="" class="textarea required" placeholder="地址" id="Remark" data-valid="between:0-300" data-error="地址长度为0-300位"></textarea>
          </div>
          <div class="col-2"></div>
        </div>
        <div class="row cl">
          <div class="col-9 col-offset-3">
            <input class="btn btn-primary radius" type="button" onclick="submit()" value="&nbsp;&nbsp;提&nbsp;&nbsp;交&nbsp;&nbsp;" />
			<input class="btn btn-default radius" type="button" onclick="cancel()" value="&nbsp;&nbsp;取&nbsp;&nbsp;消&nbsp;&nbsp;" />
          </div>
        </div>
      </div>
    </div>
    <script type="text/javascript" src="../../jscript/jquery-1.10.2.js"></script> 
    <script type="text/javascript" src="../../jscript/script_common.js"></script>
    <script type="text/javascript" src="../../jscript/script_ajax.js"></script>
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
                    reflist();
                }
            }
            if (vjson.type == "insert") {
                if (vjson.flag == "1") {
                    $("#SRVTypeNo").attr("disabled", false);
                    $("#SRVTypeNo").val(vjson.SRVTypeNo);
                    $("#SRVTypeName").val("");
                    $("#ParentTypeNo").val(vjson.SRVTypeNo);
                    $("#ParentTypeNoName").val(vjson.SRVTypeName);
                    $("#SRVSPNo").val("");
                    $("#Remark").val("");

                    $("#SRVTypeNo").focus();
                    $("#list").css("display", "none");
                    $("#edit").css("display", "");
                }
                else {
                    layer.alert("数据操作出错！");
                }
                return;
            }
            if (vjson.type == "delete") {
                if (vjson.flag == "1") {
                    $("#outerlist").html(vjson.liststr);
                    $("#selectKey").val("");
                    reflist();
                }
                else if (vjson.flag == "3") {
                    layer.alert("当前服务类型已有子项，不能删除！");
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
                    showMsg("SRVTypeNo", "此服务类型编号在系统已存在", "1");
                }
                else {
                    layer.msg("数据操作出错！");
                }
                return;
            }
            if (vjson.type == "update") {
                if (vjson.flag == "1") {
                    $("#SRVTypeNo").val(vjson.SRVTypeNo);
                    $("#SRVTypeName").val(vjson.SRVTypeName);
                    $("#ParentTypeNo").val(vjson.ParentTypeNo);
                    $("#ParentTypeNoName").val(vjson.ParentTypeNoName);
                    $("#SRVSPNo").val(vjson.SRVSPNo);
                    $("#Remark").val(vjson.Remark);

                    $("#SRVTypeNo").attr("disabled", true);
                    $("#list").css("display", "none");
                    $("#edit").css("display", "");
                }
                else {
                    layer.alert("数据操作出错！");
                }
                return;
            }
            if (vjson.type == "check") {
                if (vjson.flag == "1") {
                    $("#" + vjson.id).val(vjson.Code);
                    $("#" + vjson.id + "Name").val(vjson.Name);
                }
                else if (vjson.flag == "3") {
                    layer.alert("未找到记录，确认是否输入正确！");
                }
                else {
                    layer.alert("提取数据出错！");
                }
                return;
            }
            if (vjson.type == "valid") {
                if (vjson.flag == "1") {
                    //$("#" + vjson.id).find(".td-status").html(vjson.stat);
                    layer.alert(vjson.stat + "成功！");
                    $("#outerlist").html(vjson.liststr);
                    $("#selectKey").val("");
                    reflist();
                }
                else {
                    layer.alert("停止(启用)出现异常！");
                }
                return;
            }
        }

        function insert() {
            $('#editlist').validate('reset');
            id = "";
            type = "insert";

            if ($("#selectKey").val() == "") {
                $("#SRVTypeNo").attr("disabled", false);
                $("#SRVTypeNo").val("");
                $("#SRVTypeName").val("");
                $("#ParentTypeNo").val("");
                $("#ParentTypeNoName").val("");
                $("#SRVSPNo").val("");
                $("#Remark").val("");

                $("#list").css("display", "none");
                $("#edit").css("display", "");
            }
            else {
                var submitData = new Object();
                submitData.Type = "insert";
                submitData.id = $("#selectKey").val();

                transmitData(datatostr(submitData));
            }
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
                submitData.SRVTypeNo = $("#SRVTypeNo").val();
                submitData.SRVTypeName = $("#SRVTypeName").val();
                submitData.ParentTypeNo = $("#ParentTypeNo").val();
                submitData.SRVSPNo = $("#SRVSPNo").val();
                submitData.Remark = $("#Remark").val();

                submitData.tp = type;
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
                transmitData(datatostr(submitData));
                layer.close(index);
            });
            return;
        }
        function select() {
            var submitData = new Object();
            submitData.Type = "select";
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

        $("#chooseParentType").click(function () {
            ChooseBasic("ParentTypeNo", "SRVType");
        });
        function choose(id, labels, values) {
            if (labels != "" && labels != undefined && labels != "undefined") {
                if (id == "ParentTypeNo") {
                    $("#ParentTypeNoName").val(values);
                    $("#ParentTypeNo").val(labels);
                }
            }
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