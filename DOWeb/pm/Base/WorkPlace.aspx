<%@ Page Language="C#" AutoEventWireup="true" Inherits="project.Presentation.Base.WorkPlace,project" %>

<!DOCTYPE>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head1" runat="server">
    <title>工位资料</title>
    <!--[if lt IE 9]>
    <script type="text/javascript" src="../jscript/html5.js"></script>
    <script type="text/javascript" src="../jscript/respond.min.js"></script>
    <![endif]-->
    <link href="../../css/H-ui.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/H-ui.admin.css" rel="stylesheet" type="text/css" />
    <link href="../../lib/iconfont/iconfont.css" rel="stylesheet" type="text/css" />
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/JsInput.css" rel="stylesheet" />
    <script type="text/javascript">
        function transmitData(submitData) {
            var data = submitData;
            <%=ClientScript.GetCallbackEventReference(this, "data", "BandResuleData", null) %>
        }
    </script>
</head>
<body>
    <form id="form1" runat="server"></form>
    <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 基础资料 <span class="c-gray en">&gt;</span>工位资料 <a class="btn btn-success radius r mr-20" style="line-height: 1.6em; margin-top: 2px" href="javascript:location.replace(location.href);" title="刷新"><i class="Hui-iconfont">&#xe68f;</i></a></nav>
    <div id="list" class="pt-5 pr-20 pb-5 pl-20">
        <div class="cl pd-5 bg-1 bk-gray mt-2">
            <span class="l">
                <%--<a href="javascript:;" onclick="insert()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe600;</i> 添加</a>
                <a href="javascript:;" onclick="update()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe60c;</i> 修改</a> 
                <a href="javascript:;" onclick="del()" class="btn btn-danger radius"><i class="Hui-iconfont">&#xe6e2;</i> 删除</a>
                <a href="javascript:;" onclick="valid()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe615;</i> 启用(禁用)</a>--%>
                <%=Buttons %>
                <input type="hidden" id="selectKey" />
            </span>
        </div>

        <div class="cl pd-10  bk-gray mt-2">
            园区&nbsp;<%=WPLOCNo1StrS %>&nbsp;
            建设期&nbsp;<select class="input-text required size-MINI" id="WPLOCNo2S" style="width: 120px"><option value="">全部</option>
            </select>&nbsp;
            楼栋&nbsp;<select class="input-text required size-MINI" id="WPLOCNo3S" style="width: 120px"><option value="">全部</option>
            </select>&nbsp;
            楼层&nbsp;<select class="input-text required size-MINI" id="WPLOCNo4S" style="width: 120px"><option value="">全部</option>
            </select>
            <br />
            房间编号&nbsp;<input type="text" class="input-text size-MINI" style="width: 150px" placeholder="房间编号" id="WPRMIDS" />&nbsp;
		    工位编号&nbsp;<input type="text" class="input-text size-MINI" style="width: 150px" placeholder="工位编号" id="WPNoS" />&nbsp;
		    工位类型&nbsp;<%=WPTypeStrS %>&nbsp;
            状态&nbsp;<select class="input-text size-MINI" id="WPStatusS" style="width: 150px">
                <option value="" selected>全部</option>
                <option value="free">空闲</option>
                <option value="use">占用</option>
                <option value="reserve">预留</option>
            </select>&nbsp;
		    <button type="submit" class="btn btn-success radius" onclick="select()"><i class="Hui-iconfont">&#xe665;</i> 查询</button>
        </div>
        <div class="mt-5" id="outerlist">
            <%=list %>
        </div>
    </div>
    <div id="edit" class="pt-5 pr-20 pb-5 pl-20 " style="display: none;">
        <div class="form form-horizontal bk-gray mt-15 pb-10" id="editlist">
            <div class="row cl">
                <label class="form-label col-2"><span class="c-red">*</span>工位编号：</label>
                <div class="formControls col-2">
                    <input type="text" class="input-text required" placeholder="工位编号" disabled="disabled" id="WPNo" data-valid="isNonEmpty||between:1-30" data-error="工位号不能为空||工位编号长度为1-30位" />
                </div>
                <div class="col-1"></div>
            </div>
            <div class="row cl">
                <label class="form-label col-2"><span class="c-red">*</span>工位类型：</label>
                <div class="formControls col-2">
                    <%=WPTypeStr %>
                </div>

                <label class="form-label col-2"><span class="c-red">*</span>房间编号：</label>
                <div class="formControls col-3">
                    <input type="text" class="input-text required" placeholder="房间编号" id="WPRMID" style="width: 200px;" disabled="disabled" data-valid="isNonEmpty" data-error="房间编号不能为空" />
                    <button type="button" class="btn btn-primary radius" id="chooseRMID">选择</button>
                </div>
                <div class="col-3"></div>
            </div>
            <div class="row cl">
                <label class="form-label col-2"><span class="c-red">*</span>工位人数：</label>
                <div class="formControls col-2">
                    <input type="text" class="input-text required" placeholder="工位人数" id="WPSeat" data-valid="isNonEmpty||onlyInt" data-error="须是整数||须是整数" />
                </div>

                <label class="form-label col-2"><span class="c-red">*</span>每工位单价：</label>
                <div class="formControls col-3">
                    <input type="text" class="input-text required" placeholder="每工位单价" id="WPSeatPrice" data-valid="isNonEmpty||onlyNum" data-error="须是数字||须是数字" />
                </div>
                <div class="col-3"></div>
            </div>
            <div class="row cl">
                <label class="form-label col-2"><span class="c-red">*</span>园区：</label>
                <div class="formControls col-2">
                    <%=WPLOCNo1Str %>
                </div>

                <label class="form-label col-2"><span class="c-red">*</span>建设期：</label>
                <div class="formControls col-3">
                    <select class="input-text required" id="WPLOCNo2" data-valid="isNonEmpty" data-error="建设期不能为空"></select>
                </div>
                <div class="col-1"></div>
            </div>
            <div class="row cl">
                <label class="form-label col-2"><span class="c-red">*</span>楼栋：</label>
                <div class="formControls col-2">
                    <select class="input-text required" id="WPLOCNo3" data-valid="isNonEmpty" data-error="楼栋不能为空"></select>
                </div>

                <label class="form-label col-2"><span class="c-red">*</span>楼层：</label>
                <div class="formControls col-3">
                    <select class="input-text required" id="WPLOCNo4" data-valid="isNonEmpty" data-error="楼层不能为空"></select>
                </div>
                <div class="col-1"></div>
            </div>
            <div class="row cl">
                <label class="form-label col-2"><span class="c-red">*</span>所属项目：</label>
                <div class="formControls col-2">
                    <input type="text" class="input-text required" placeholder="所属项目" id="WPProject" data-valid="isNonEmpty||between:1-50" data-error="所属项目不能为空||房间编号长度为1-50位" />
                </div>
                <div class="col-1"></div>
                <label class="form-label col-2"><span class="c-red">*</span>是否纳入统计：</label>
                <div class="formControls col-2">
                    <select class="input-text required" id="IsStatistics" data-valid="isNonEmpty" data-error="纳入统计不能为空">
                        <option value="true" selected>是</option>
                        <option value="false">否</option>
                    </select>
                </div>
            </div>
            <div class="row cl">
                <label class="form-label col-2">位置：</label>
                <div class="formControls col-7">
                    <textarea cols="" rows="3" class="textarea required" placeholder="位置" id="WPAddr" data-valid="between:0-300" data-error="位置长度为0-300位"></textarea>
                </div>
                <div class="col-1"></div>
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
    <script type="text/javascript" src="../../jscript/script_ajax.js"></script>
    <script type="text/javascript" src="../../jscript/json2.js"></script>
    <script type="text/javascript" src="../../jscript/H-ui.js"></script>
    <script type="text/javascript" src="../../jscript/H-ui.admin.js"></script>
    <script type="text/javascript" src="../../lib/layer/layer.js"></script>
    <script type="text/javascript" src="../../lib/validate/jquery.validate.js"></script>
    <script src="../../jscript/JsInputDate.js"></script>
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
                    console.log(vjson.sync);
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
                    console.log(vjson.sync);
                }
                else if (vjson.flag == "3") {
                    showMsg("WorkerNo", "此服务类型编号已存在", "1");
                }
                else {
                    layer.msg("数据操作出错！");
                }
                return;
            }
            if (vjson.type == "update") {
                if (vjson.flag == "1") {
                    $("#WPRMID").val(vjson.WPRMID);
                    $("#WPNo").val(vjson.WPNo);
                    $("#WPType").val(vjson.WPType);
                    $("#WPLOCNo1").val(vjson.WPLOCNo1);
                    $("#WPSeat").val(vjson.WPSeat);
                    $("#WPSeatPrice").val(vjson.WPSeatPrice);
                    $("#WPProject").val(vjson.WPProject);
                    $("#WPAddr").val(vjson.WPAddr);
                    $("#IsStatistics").val(vjson.IsStatistics);

                    $("#WPLOCNo2").empty();
                    $("#WPLOCNo2").append("<option value=''>请选择</option>");
                    for (var i = 0; i <= vjson.row - 1; i++) {
                        var option = "<option value='" + vjson.subtype.split(";")[i].split(":")[0].toString() + "'>" + vjson.subtype.split(";")[i].split(":")[1].toString() + "</option>";
                        $("#WPLOCNo2").append(option);
                    }

                    $("#WPLOCNo3").empty();
                    $("#WPLOCNo3").append("<option value=''>请选择</option>");
                    for (var i = 0; i <= vjson.row1 - 1; i++) {
                        var option = "<option value='" + vjson.subtype1.split(";")[i].split(":")[0].toString() + "'>" + vjson.subtype1.split(";")[i].split(":")[1].toString() + "</option>";
                        $("#WPLOCNo3").append(option);
                    }

                    $("#WPLOCNo4").empty();
                    $("#WPLOCNo4").append("<option value=''>请选择</option>");
                    for (var i = 0; i <= vjson.row2 - 1; i++) {
                        var option = "<option value='" + vjson.subtype2.split(";")[i].split(":")[0].toString() + "'>" + vjson.subtype2.split(";")[i].split(":")[1].toString() + "</option>";
                        $("#WPLOCNo4").append(option);
                    }

                    $("#WPLOCNo2").val(vjson.WPLOCNo2);
                    $("#WPLOCNo3").val(vjson.WPLOCNo3);
                    $("#WPLOCNo4").val(vjson.WPLOCNo4);

                    //$("#WPRMID").attr("disabled", true);
                    $("#WPNo").attr("disabled", true);
                    $("#WPLOCNo1").attr("disabled", true);
                    $("#WPLOCNo2").attr("disabled", true);
                    $("#WPLOCNo3").attr("disabled", true);
                    $("#WPLOCNo4").attr("disabled", true);
                    $("#WPLOCNo1").addClass("disabled");
                    $("#WPLOCNo2").addClass("disabled");
                    $("#WPLOCNo3").addClass("disabled");
                    $("#WPLOCNo4").addClass("disabled");
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
                    layer.alert(vjson.stat + "成功！");
                    //$("#" + vjson.id).find(".td-status").html(vjson.stat);
                    $("#outerlist").html(vjson.liststr);
                    $("#selectKey").val("");
                    reflist();
                    console.log(vjson.sync);
                }
                else {
                    layer.alert("停止(启用)出现异常！");
                }
                return;
            }
            if (vjson.type == "getvalue") {
                if (vjson.flag == "1") {
                    $("#" + vjson.child).empty();

                    if (vjson.child == "WPLOCNo2S" || vjson.child == "WPLOCNo3S" || vjson.child == "WPLOCNo4S")
                        $("#" + vjson.child).append("<option value=''>全部</option>");
                    else
                        $("#" + vjson.child).append("<option value=''>请选择</option>");

                    for (var i = 0; i <= vjson.row - 1; i++) {
                        var option = "<option value='" + vjson.subtype.split(";")[i].split(":")[0].toString() + "'>" + vjson.subtype.split(";")[i].split(":")[1].toString() + "</option>";
                        $("#" + vjson.child).append(option);
                    }

                    if (vjson.child == "WPLOCNo2") {
                        $("#WPLOCNo3").empty();
                        $("#WPLOCNo3").append("<option value=''>请选择</option>");
                        $("#WPLOCNo4").empty();
                        $("#WPLOCNo4").append("<option value=''>请选择</option>");
                    }
                    else if (vjson.child == "WPLOCNo3") {
                        $("#WPLOCNo4").empty();
                        $("#WPLOCNo4").append("<option value=''>请选择</option>");
                    }
                    else if (vjson.child == "WPLOCNo2S") {
                        $("#WPLOCNo3S").empty();
                        $("#WPLOCNo3S").append("<option value=''>全部</option>");
                        $("#WPLOCNo4S").empty();
                        $("#WPLOCNo4S").append("<option value=''>全部</option>");
                    }
                    else if (vjson.child == "WPLOCNo3S") {
                        $("#WPLOCNo4S").empty();
                        $("#WPLOCNo4S").append("<option value=''>全部</option>");
                    }
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
                    layer.alert("数据操作出错！");
                }
                return;
            }
        }

        function insert() {
            $('#editlist').validate('reset');
            id = "";
            type = "insert";
            $("#WPRMID").val("");
            $("#WPNo").val("");
            $("#WPType").val("");
            $("#WPLOCNo1").val("");
            $("#WPSeat").val("");
            $("#WPSeatPrice").val("");
            $("#WPProject").val("");
            $("#WPAddr").val("");

            $("#WPLOCNo2").empty();
            $("#WPLOCNo2").append("<option value=''>请选择</option>");
            $("#WPLOCNo3").empty();
            $("#WPLOCNo3").append("<option value=''>请选择</option>");
            $("#WPLOCNo4").empty();
            $("#WPLOCNo4").append("<option value=''>请选择</option>");

            //$("#WPRMID").attr("disabled", false);
            $("#WPNo").attr("disabled", false);
            $("#WPLOCNo1").attr("disabled", false);
            $("#WPLOCNo2").attr("disabled", false);
            $("#WPLOCNo3").attr("disabled", false);
            $("#WPLOCNo4").attr("disabled", false);
            $("#WPLOCNo1").removeClass("disabled");
            $("#WPLOCNo2").removeClass("disabled");
            $("#WPLOCNo3").removeClass("disabled");
            $("#WPLOCNo4").removeClass("disabled");

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
                submitData.WPRMID = $("#WPRMID").val();
                submitData.WPNo = $("#WPNo").val();
                submitData.WPType = $("#WPType").val();
                submitData.WPLOCNo1 = $("#WPLOCNo1").val();
                submitData.WPLOCNo2 = $("#WPLOCNo2").val();
                submitData.WPLOCNo3 = $("#WPLOCNo3").val();
                submitData.WPLOCNo4 = $("#WPLOCNo4").val();
                submitData.WPSeat = $("#WPSeat").val();
                submitData.WPSeatPrice = $("#WPSeatPrice").val();
                submitData.WPProject = $("#WPProject").val();
                submitData.WPAddr = $("#WPAddr").val();
                submitData.IsStatistics = $("#IsStatistics").val();

                submitData.tp = type;
                submitData.WPNoS = $("#WPNoS").val();
                submitData.WPTypeS = $("#WPTypeS").val();
                submitData.WPRMIDS = $("#WPRMIDS").val();
                submitData.WPStatusS = $("#WPStatusS").val();
                submitData.WPLOCNo1S = $("#WPLOCNo1S").val();
                submitData.WPLOCNo2S = $("#WPLOCNo2S").val();
                submitData.WPLOCNo3S = $("#WPLOCNo3S").val();
                submitData.WPLOCNo4S = $("#WPLOCNo4S").val();
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

                submitData.WPNoS = $("#WPNoS").val();
                submitData.WPTypeS = $("#WPTypeS").val();
                submitData.WPRMIDS = $("#WPRMIDS").val();
                submitData.WPStatusS = $("#WPStatusS").val();
                submitData.WPLOCNo1S = $("#WPLOCNo1S").val();
                submitData.WPLOCNo2S = $("#WPLOCNo2S").val();
                submitData.WPLOCNo3S = $("#WPLOCNo3S").val();
                submitData.WPLOCNo4S = $("#WPLOCNo4S").val();
                submitData.page = page;
                transmitData(datatostr(submitData));
                layer.close(index);
            });
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

                submitData.WPNoS = $("#WPNoS").val();
                submitData.WPTypeS = $("#WPTypeS").val();
                submitData.WPRMIDS = $("#WPRMIDS").val();
                submitData.WPStatusS = $("#WPStatusS").val();
                submitData.WPLOCNo1S = $("#WPLOCNo1S").val();
                submitData.WPLOCNo2S = $("#WPLOCNo2S").val();
                submitData.WPLOCNo3S = $("#WPLOCNo3S").val();
                submitData.WPLOCNo4S = $("#WPLOCNo4S").val();
                submitData.page = page;
                transmitData(datatostr(submitData));
            });
            return;
        }
        function select() {
            var submitData = new Object();
            submitData.Type = "select";

            submitData.WPNoS = $("#WPNoS").val();
            submitData.WPTypeS = $("#WPTypeS").val();
            submitData.WPRMIDS = $("#WPRMIDS").val();
            submitData.WPStatusS = $("#WPStatusS").val();
            submitData.WPLOCNo1S = $("#WPLOCNo1S").val();
            submitData.WPLOCNo2S = $("#WPLOCNo2S").val();
            submitData.WPLOCNo3S = $("#WPLOCNo3S").val();
            submitData.WPLOCNo4S = $("#WPLOCNo4S").val();
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

            submitData.WPNoS = $("#WPNoS").val();
            submitData.WPTypeS = $("#WPTypeS").val();
            submitData.WPRMIDS = $("#WPRMIDS").val();
            submitData.WPStatusS = $("#WPStatusS").val();
            submitData.WPLOCNo1S = $("#WPLOCNo1S").val();
            submitData.WPLOCNo2S = $("#WPLOCNo2S").val();
            submitData.WPLOCNo3S = $("#WPLOCNo3S").val();
            submitData.WPLOCNo4S = $("#WPLOCNo4S").val();
            submitData.page = page;
            transmitData(datatostr(submitData));
            return;
        }

        jQuery("#WPLOCNo1").change(function () {
            var submitData = new Object();
            submitData.Type = "getvalue";
            submitData.parent = $("#WPLOCNo1").val();
            submitData.child = "WPLOCNo2";
            transmitData(datatostr(submitData));
            return;
        });
        jQuery("#WPLOCNo1S").change(function () {
            var submitData = new Object();
            submitData.Type = "getvalue";
            submitData.parent = $("#WPLOCNo1S").val();
            submitData.child = "WPLOCNo2S";
            transmitData(datatostr(submitData));
            return;
        });

        jQuery("#WPLOCNo2").change(function () {
            var submitData = new Object();
            submitData.Type = "getvalue";
            submitData.parent = $("#WPLOCNo2").val();
            submitData.child = "WPLOCNo3";
            transmitData(datatostr(submitData));
            return;
        });
        jQuery("#WPLOCNo2S").change(function () {
            var submitData = new Object();
            submitData.Type = "getvalue";
            submitData.parent = $("#WPLOCNo2S").val();
            submitData.child = "WPLOCNo3S";
            transmitData(datatostr(submitData));
            return;
        });

        jQuery("#WPLOCNo3").change(function () {
            var submitData = new Object();
            submitData.Type = "getvalue";
            submitData.parent = $("#WPLOCNo3").val();
            submitData.child = "WPLOCNo4";
            transmitData(datatostr(submitData));
            return;
        });
        jQuery("#WPLOCNo3S").change(function () {
            var submitData = new Object();
            submitData.Type = "getvalue";
            submitData.parent = $("#WPLOCNo3S").val();
            submitData.child = "WPLOCNo4S";
            transmitData(datatostr(submitData));
            return;
        });

        $("#chooseRMID").click(function () {
            var temp = "../Base/ChooseRMID.aspx?id=WPRMID";
            layer_show("选择页面", temp, 800, 630);
        });
        function choose(id, labels, values) {
            if (labels != "" && labels != undefined && labels != "undefined") {
                if (id == "WPRMID") {
                    $("#" + id).val(labels);
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
