<%@ Page Language="C#" AutoEventWireup="true" Inherits="project.Presentation.Base.Room,project" %>

<!DOCTYPE>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head1" runat="server">
    <title>房间资料</title>
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
    <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 基础资料 <span class="c-gray en">&gt;</span>房间资料 <a class="btn btn-success radius r mr-20" style="line-height: 1.6em; margin-top: 2px" href="javascript:location.replace(location.href);" title="刷新"><i class="Hui-iconfont">&#xe68f;</i></a></nav>
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
            园区&nbsp;<%=RMLOCNo1StrS %>&nbsp;
            建设期&nbsp;<select class="input-text required size-MINI" id="RMLOCNo2S" style="width: 120px"><option value="">全部</option>
            </select>&nbsp;
            楼栋&nbsp;<select class="input-text required size-MINI" id="RMLOCNo3S" style="width: 120px"><option value="">全部</option>
            </select>&nbsp;
            楼层&nbsp;<select class="input-text required size-MINI" id="RMLOCNo4S" style="width: 120px"><option value="">全部</option>
            </select>
            <br />
            房间编号&nbsp;<input type="text" class="input-text size-MINI" style="width: 150px" placeholder="房间编号" id="RMIDS" />&nbsp;
		    客户&nbsp;            
            <input type="text" class="input-text size-MINI" placeholder="名称" id="CustNoSName" onblur="check('CustNoS','Cust')" style="width: 150px;" />
            <input type="hidden" id="CustNoS" />
            <img id="CustNoSImg" alt="" src="../../images/view_detail.png" class="view_detail" />
            状态&nbsp;<select class="input-text size-MINI" id="RMStatusS" style="width: 150px">
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
                <label class="form-label col-2"><span class="c-red">*</span>房间编号：</label>
                <div class="formControls col-2">
                    <input type="text" class="input-text required" placeholder="房间编号" disabled="disabled" id="RMID" data-valid="isNonEmpty||between:1-30" data-error="房间号不能为空||房间编号长度为1-30位" />
                </div>
                <div class="col-1"></div>
            </div>
            <div class="row cl">
                <label class="form-label col-2"><span class="c-red">*</span>房间号：</label>
                <div class="formControls col-2">
                    <input type="text" class="input-text required" placeholder="房间号" id="RMNo" data-valid="isNonEmpty||between:1-16" data-error="房间号不能为空||房间号长度为1-16位" />
                </div>

                <label class="form-label col-3"><span class="c-red">*</span>园区：</label>
                <div class="formControls col-2">
                    <%=RMLOCNo1Str %>
                </div>
                <div class="col-1"></div>
            </div>
            <div class="row cl">
                <label class="form-label col-2"><span class="c-red">*</span>建设期：</label>
                <div class="formControls col-2">
                    <select class="input-text required" id="RMLOCNo2" data-valid="isNonEmpty" data-error="建设期不能为空"></select>
                </div>

                <label class="form-label col-3"><span class="c-red">*</span>楼栋：</label>
                <div class="formControls col-2">
                    <select class="input-text required" id="RMLOCNo3" data-valid="isNonEmpty" data-error="楼栋不能为空"></select>
                </div>
                <div class="col-1"></div>
            </div>
            <div class="row cl">
                <label class="form-label col-2"><span class="c-red">*</span>楼层：</label>
                <div class="formControls col-2">
                    <select class="input-text required" id="RMLOCNo4" data-valid="isNonEmpty" data-error="楼层不能为空"></select>
                </div>

                <label class="form-label col-3"><span class="c-red">*</span>出租类型：</label>
                <div class="formControls col-2">
                    <select class="input-text required" id="RMRentType" data-valid="isNonEmpty" data-error="出租类型不能为空">
                        <option value="1">办公楼</option>
                        <option value="2">住宅</option>
                        <option value="3">仓库</option>
                        <option value="4">商铺</option>
                        <option value="5">厂房</option>
                    </select>
                </div>
                <div class="col-1"></div>
            </div>
            <div class="row cl">
                <label class="form-label col-2"><span class="c-red">*</span>出租面积：</label>
                <div class="formControls col-2">
                    <input type="text" class="input-text required" placeholder="出租面积" id="RMRentSize" onchange="validDecimal(this.id)" data-valid="isNonEmpty" data-error="出租面积不能为空" />
                </div>

                <label class="form-label col-3">建筑面积：</label>
                <div class="formControls col-2">
                    <input type="text" class="input-text required" placeholder="建筑面积" id="RMBuildSize" onchange="validDecimal(this.id)" data-valid="between:0-30" data-error="" />
                </div>
                <div class="col-1"></div>
            </div>
            <div class="row cl">
                <label class="form-label col-2"><span class="c-red">*</span>是否纳入统计：</label>
                <div class="formControls col-2">
                    <select class="input-text required" id="IsStatistics" data-valid="isNonEmpty" data-error="纳入统计不能为空">
                        <option value="true" selected>是</option>
                        <option value="false">否</option>
                    </select>
                </div>
                <div class="col-1"></div>
            </div>
            <div class="row cl">
                <label class="form-label col-2">位置：</label>
                <div class="formControls col-7">
                    <textarea cols="" rows="3" class="textarea required" placeholder="位置" id="RMAddr" data-valid="between:0-300" data-error="位置长度为0-300位"></textarea>
                </div>
                <div class="col-1"></div>
            </div>
            <div class="row cl">
                <label class="form-label col-2">备注：</label>
                <div class="formControls col-7">
                    <textarea cols="" rows="3" class="textarea required" placeholder="备注" id="RMRemark" data-valid="between:0-500" data-error="备注长度为0-500位"></textarea>
                </div>
                <div class="col-1"></div>
            </div>

            <div class="row cl" style="display: none;">
                <label class="form-label col-3"><span class="c-red">*</span>是否有空调：</label>
                <div class="formControls col-2">
                    <select class="input-text required" id="HaveAirCondition" data-valid="isNonEmpty" data-error="请选择">
                        <option value="false">无</option>
                        <option value="true">有</option>
                    </select>
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

    <div id="reservediv" style="display: none;">
        <table style="width: 400px; margin-top: 10px;">
            <tr>
                <td style="width: 100px; text-align: center; padding: 8px; height: 40px;">房间号</td>
                <td style="width: 300px;">
                    <input type="text" id="ReserveRMID" disabled="disabled" class="input-text size-MINI" style="width: 200px;" />
                </td>
            </tr>
            <tr>
                <td style="width: 100px; text-align: center; padding: 8px; height: 40px;">预留客户</td>
                <td style="width: 300px;">
                    <input type="text" id="ReserveCustName" onblur="check('ReserveCust','Cust')" class="input-text size-MINI" style="width: 200px;" />
                    <input style="display: none;" type="text" id="ReserveCust" />
                    <img id="ReserveCustImg" alt="" src="../../images/view_detail.png" class="view_detail" />
                </td>
            </tr>
            <tr>
                <td style="width: 100px; text-align: center; padding: 8px; height: 40px;">预留到期日期</td>
                <td style="width: 300px;">
                    <input type="text" id="ReserveDate" class="input-text size-MINI" style="width: 200px;" /></td>
            </tr>
        </table>
        <div style="margin: 10px 30px;">
            <input class="btn btn-primary radius" type="button" onclick="reservesubmit()" value="&nbsp;确&nbsp;定&nbsp;" />&nbsp;&nbsp;
            <input class="btn btn-primary radius" type="button" onclick="reservecancel()" value="&nbsp;关&nbsp;闭&nbsp;" />
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
                    showMsg("RMID", "此房间编号已存在", "1");
                }
                else {
                    layer.msg("数据操作出错！");
                }
                return;
            }
            if (vjson.type == "update") {
                if (vjson.flag == "1") {
                    $("#RMID").val(vjson.RMID);
                    $("#RMNo").val(vjson.RMNo);
                    $("#RMLOCNo1").val(vjson.RMLOCNo1);
                    $("#RMRentType").val(vjson.RMRentType);
                    $("#RMBuildSize").val(vjson.RMBuildSize);
                    $("#RMRentSize").val(vjson.RMRentSize);
                    $("#RMAddr").val(vjson.RMAddr);
                    $("#RMRemark").val(vjson.RMRemark);
                    $("#HaveAirCondition").val(vjson.HaveAirCondition);
                    $("#IsStatistics").val(vjson.IsStatistics);
                    $("#RMLOCNo2").empty();
                    $("#RMLOCNo2").append("<option value=''>请选择</option>");
                    for (var i = 0; i <= vjson.row - 1; i++) {
                        var option = "<option value='" + vjson.subtype.split(";")[i].split(":")[0].toString() + "'>" + vjson.subtype.split(";")[i].split(":")[1].toString() + "</option>";
                        $("#RMLOCNo2").append(option);
                    }

                    $("#RMLOCNo3").empty();
                    $("#RMLOCNo3").append("<option value=''>请选择</option>");
                    for (var i = 0; i <= vjson.row1 - 1; i++) {
                        var option = "<option value='" + vjson.subtype1.split(";")[i].split(":")[0].toString() + "'>" + vjson.subtype1.split(";")[i].split(":")[1].toString() + "</option>";
                        $("#RMLOCNo3").append(option);
                    }

                    $("#RMLOCNo4").empty();
                    $("#RMLOCNo4").append("<option value=''>请选择</option>");
                    for (var i = 0; i <= vjson.row2 - 1; i++) {
                        var option = "<option value='" + vjson.subtype2.split(";")[i].split(":")[0].toString() + "'>" + vjson.subtype2.split(";")[i].split(":")[1].toString() + "</option>";
                        $("#RMLOCNo4").append(option);
                    }

                    $("#RMLOCNo2").val(vjson.RMLOCNo2);
                    $("#RMLOCNo3").val(vjson.RMLOCNo3);
                    $("#RMLOCNo4").val(vjson.RMLOCNo4);

                    $("#RMNo").attr("disabled", true);
                    $("#RMLOCNo1").attr("disabled", true);
                    $("#RMLOCNo2").attr("disabled", true);
                    $("#RMLOCNo3").attr("disabled", true);
                    $("#RMLOCNo4").attr("disabled", true);
                    $("#RMLOCNo1").addClass("disabled");
                    $("#RMLOCNo2").addClass("disabled");
                    $("#RMLOCNo3").addClass("disabled");
                    $("#RMLOCNo4").addClass("disabled");

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

                    if (vjson.child == "RMLOCNo2S" || vjson.child == "RMLOCNo3S" || vjson.child == "RMLOCNo4S")
                        $("#" + vjson.child).append("<option value=''>全部</option>");
                    else
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
                    else if (vjson.child == "RMLOCNo2S") {
                        $("#RMLOCNo3S").empty();
                        $("#RMLOCNo3S").append("<option value=''>全部</option>");
                        $("#RMLOCNo4S").empty();
                        $("#RMLOCNo4S").append("<option value=''>全部</option>");
                    }
                    else if (vjson.child == "RMLOCNo3S") {
                        $("#RMLOCNo4S").empty();
                        $("#RMLOCNo4S").append("<option value=''>全部</option>");
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
            if (vjson.type == "reserve") {
                if (vjson.flag == "1") {
                    $("#outerlist").html(vjson.liststr);
                    $("#selectKey").val("");
                    reflist();
                }
                else if (vjson.flag == "3") {
                    layer.alert("当前状态不能预留，请刷新再试！");
                }
                else {
                    layer.alert("数据操作出错！");
                }
                return;
            }
            if (vjson.type == "unreserve") {
                if (vjson.flag == "1") {
                    $("#outerlist").html(vjson.liststr);
                    $("#selectKey").val("");
                    reflist();
                }
                else if (vjson.flag == "3") {
                    layer.alert("当前状态不能取消预留，请刷新再试！");
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
            $("#RMID").val("");
            $("#RMNo").val("");
            $("#RMLOCNo1").val("");
            $("#RMLOCNo2").val("");
            $("#RMLOCNo3").val("");
            $("#RMLOCNo4").val("");
            $("#RMRentType").val("");
            $("#RMBuildSize").val("");
            $("#RMRentSize").val("");
            $("#RMAddr").val("");
            $("#RMRemark").val("");
            $("#HaveAirCondition").val("false");

            $("#RMLOCNo2").empty();
            $("#RMLOCNo2").append("<option value=''>请选择</option>");
            $("#RMLOCNo3").empty();
            $("#RMLOCNo3").append("<option value=''>请选择</option>");
            $("#RMLOCNo4").empty();
            $("#RMLOCNo4").append("<option value=''>请选择</option>");

            $("#RMNo").attr("disabled", false);
            $("#RMLOCNo1").attr("disabled", false);
            $("#RMLOCNo2").attr("disabled", false);
            $("#RMLOCNo3").attr("disabled", false);
            $("#RMLOCNo4").attr("disabled", false);
            $("#RMLOCNo1").removeClass("disabled");
            $("#RMLOCNo2").removeClass("disabled");
            $("#RMLOCNo3").removeClass("disabled");
            $("#RMLOCNo4").removeClass("disabled");

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
                submitData.RMNo = $("#RMNo").val();
                submitData.RMLOCNo1 = $("#RMLOCNo1").val();
                submitData.RMLOCNo2 = $("#RMLOCNo2").val();
                submitData.RMLOCNo3 = $("#RMLOCNo3").val();
                submitData.RMLOCNo4 = $("#RMLOCNo4").val();
                submitData.RMRentType = $("#RMRentType").val();
                submitData.IsStatistics = $("#IsStatistics").val();
                submitData.RMBuildSize = $("#RMBuildSize").val();
                submitData.RMRentSize = $("#RMRentSize").val();
                submitData.RMAddr = $("#RMAddr").val();
                submitData.RMRemark = $("#RMRemark").val();
                submitData.HaveAirCondition = $("#HaveAirCondition").val();

                submitData.tp = type;
                submitData.RMIDS = $("#RMIDS").val();
                submitData.RMLOCNo1S = $("#RMLOCNo1S").val();
                submitData.RMLOCNo2S = $("#RMLOCNo2S").val();
                submitData.RMLOCNo3S = $("#RMLOCNo3S").val();
                submitData.RMLOCNo4S = $("#RMLOCNo4S").val();
                submitData.CustNoS = $("#CustNoS").val();
                submitData.RMStatusS = $("#RMStatusS").val();
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
                submitData.RMLOCNo1S = $("#RMLOCNo1S").val();
                submitData.RMLOCNo2S = $("#RMLOCNo2S").val();
                submitData.RMLOCNo3S = $("#RMLOCNo3S").val();
                submitData.RMLOCNo4S = $("#RMLOCNo4S").val();
                submitData.CustNoS = $("#CustNoS").val();
                submitData.RMStatusS = $("#RMStatusS").val();
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

                submitData.RMIDS = $("#RMIDS").val();
                submitData.RMLOCNo1S = $("#RMLOCNo1S").val();
                submitData.RMLOCNo2S = $("#RMLOCNo2S").val();
                submitData.RMLOCNo3S = $("#RMLOCNo3S").val();
                submitData.RMLOCNo4S = $("#RMLOCNo4S").val();
                submitData.CustNoS = $("#CustNoS").val();
                submitData.RMStatusS = $("#RMStatusS").val();
                submitData.page = page;
                transmitData(datatostr(submitData));
            });
            return;
        }
        function select() {
            var submitData = new Object();
            submitData.Type = "select";
            submitData.RMIDS = $("#RMIDS").val();
            submitData.RMLOCNo1S = $("#RMLOCNo1S").val();
            submitData.RMLOCNo2S = $("#RMLOCNo2S").val();
            submitData.RMLOCNo3S = $("#RMLOCNo3S").val();
            submitData.RMLOCNo4S = $("#RMLOCNo4S").val();
            submitData.CustNoS = $("#CustNoS").val();
            submitData.RMStatusS = $("#RMStatusS").val();
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
            submitData.RMLOCNo1S = $("#RMLOCNo1S").val();
            submitData.RMLOCNo2S = $("#RMLOCNo2S").val();
            submitData.RMLOCNo3S = $("#RMLOCNo3S").val();
            submitData.RMLOCNo4S = $("#RMLOCNo4S").val();
            submitData.CustNoS = $("#CustNoS").val();
            submitData.RMStatusS = $("#RMStatusS").val();
            submitData.page = page;
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
        jQuery("#RMLOCNo1S").change(function () {
            var submitData = new Object();
            submitData.Type = "getvalue";
            submitData.parent = $("#RMLOCNo1S").val();
            submitData.child = "RMLOCNo2S";
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
        jQuery("#RMLOCNo2S").change(function () {
            var submitData = new Object();
            submitData.Type = "getvalue";
            submitData.parent = $("#RMLOCNo2S").val();
            submitData.child = "RMLOCNo3S";
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
        jQuery("#RMLOCNo3S").change(function () {
            var submitData = new Object();
            submitData.Type = "getvalue";
            submitData.parent = $("#RMLOCNo3S").val();
            submitData.child = "RMLOCNo4S";
            transmitData(datatostr(submitData));
            return;
        });

        jQuery("#RMLOCNo4").change(function () {
            GenRMID();
        });

        jQuery("#RMNo").change(function () {
            GenRMID();
        });

        function GenRMID() {
            if ($("#RMLOCNo4").val() != null && $("#RMLOCNo4").val() != "")
                $("#RMID").val($("#RMLOCNo4").val() + "-" + $("#RMNo").val());
        }

        $("#CustNoSImg").click(function () {
            ChooseBasic("CustNoS", "Cust");
        });

        $("#ReserveCustImg").click(function () {
            ChooseBasic("ReserveCust", "Cust");
        });
        function choose(id, labels, values) {
            if (labels != "" && labels != undefined && labels != "undefined") {
                $("#" + id + "Name").val(values);
                $("#" + id).val(labels);
            }
        }

        function reserve(id) {
            $("#ReserveRMID").val(id);
            $("#ReserveCust").val("");
            $("#ReserveCustName").val("");
            $("#ReserveDate").val("");
            layer.open({
                type: 1,
                area: ["400px", "240px"],
                fix: true,
                maxmin: true,
                scrollbar: false,
                shade: 0.5,
                title: "预留信息填写",
                content: $("#reservediv")
            });
            return;
        }
        function unreserve(id) {
            layer.confirm('你确定取消预留吗？', function (index) {
                var submitData = new Object();
                submitData.Type = "unreserve";
                submitData.id = id;

                submitData.RMIDS = $("#RMIDS").val();
                submitData.RMLOCNo1S = $("#RMLOCNo1S").val();
                submitData.RMLOCNo2S = $("#RMLOCNo2S").val();
                submitData.RMLOCNo3S = $("#RMLOCNo3S").val();
                submitData.RMLOCNo4S = $("#RMLOCNo4S").val();
                submitData.CustNoS = $("#CustNoS").val();
                submitData.RMStatusS = $("#RMStatusS").val();
                submitData.page = page;
                transmitData(datatostr(submitData));
                layer.closeAll();
            });
            return;
        }

        function reservesubmit() {
            if ($("#ReserveRMID").val() == "") {
                layer.msg("未识别当前房间号，请刷新再试", { icon: 3, time: 1000 });
                return;
            }
            if ($("#ReserveCust").val() == "") {
                layer.msg("请选择预留客户", { icon: 3, time: 1000 });
                return;
            }
            if ($("#ReserveDate").val() == "") {
                layer.msg("请填写预留到期日期", { icon: 3, time: 1000 });
                return;
            }

            var submitData = new Object();
            submitData.Type = "reserve";
            submitData.id = $("#ReserveRMID").val();
            submitData.ReserveCust = $("#ReserveCust").val();
            submitData.ReserveDate = $("#ReserveDate").val();

            submitData.RMIDS = $("#RMIDS").val();
            submitData.RMLOCNo1S = $("#RMLOCNo1S").val();
            submitData.RMLOCNo2S = $("#RMLOCNo2S").val();
            submitData.RMLOCNo3S = $("#RMLOCNo3S").val();
            submitData.RMLOCNo4S = $("#RMLOCNo4S").val();
            submitData.CustNoS = $("#CustNoS").val();
            submitData.RMStatusS = $("#RMStatusS").val();
            submitData.page = page;
            transmitData(datatostr(submitData));
            layer.closeAll();
            return;
        }

        function reservecancel() {
            layer.closeAll();
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

        jQuery(function () {
            var ReserveDate = new JsInputDate("ReserveDate");
        });

        var trid = "";
        reflist();
    </script>
</body>
</html>
