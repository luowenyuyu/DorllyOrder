<%@ Page Language="C#" AutoEventWireup="true" Inherits="project.Presentation.Op.Readout,project" %>

<!DOCTYPE>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head1" runat="server">
    <title>抄表管理</title>
    <!--[if lt IE 9]>
    <script type="text/javascript" src="../jscript/html5.js"></script>
    <script type="text/javascript" src="../jscript/respond.min.js"></script>
    <![endif]-->
    <link href="../../css/H-ui.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/H-ui.admin.css" rel="stylesheet" type="text/css" />
    <link href="../../lib/iconfont/iconfont.css" rel="stylesheet" type="text/css" />
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/JsInput.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../../lib/zTree/css/zTreeStyle/zTreeStyle.css" type="text/css">
    <script type="text/javascript">
        function transmitData(submitData) {
            var data = submitData;
            <%=ClientScript.GetCallbackEventReference(this, "data", "BandResuleData", null) %>
        }
    </script>
</head>
<body>
    <form id="form1" runat="server"></form>
    <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 表计管理 <span class="c-gray en">&gt;</span> 抄表管理 <a class="btn btn-success radius r mr-20" style="line-height: 1.6em; margin-top: 2px" href="javascript:location.replace(location.href);" title="刷新"><i class="Hui-iconfont">&#xe68f;</i></a></nav>

    <div class="pos-a" style="width: 200px; left: 15px; top: 40px; bottom: 0; height: 100%; border-right: 1px solid #e5e5e5; background-color: #f5f5f5;">
        <ul id="ztree" class="ztree"></ul>
    </div>

    <div style="margin-left: 200px;">
        <div id="list" class="pt-5 pr-20 pb-5 pl-20">
            <div class="cl pd-5 bg-1 bk-gray mt-2">
                <span class="l">
                    <%--<a href="javascript:;" onclick="insert()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe600;</i> 添加</a>
                <a href="javascript:;" onclick="update()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe60c;</i> 修改</a> 
                <a href="javascript:;" onclick="del()" class="btn btn-danger radius"><i class="Hui-iconfont">&#xe6e2;</i> 删除</a>
                <a href="javascript:;" onclick="audit()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe615;</i> 审核通过</a>--%>
                    <%--<a href="javascript:;" onclick="unaudit()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe615;</i> 不通过</a>--%>
                    <%=Buttons %>
                    <input type="hidden" id="selectKey" />
                </span>
            </div>
            <div class="cl pd-10  bk-gray mt-2">
                房间编号&nbsp;<input type="text" class="input-text size-MINI" style="width: 150px" placeholder="房间编号" id="RMIDS" />&nbsp;
		    表记编号&nbsp;<input type="text" class="input-text size-MINI" style="width: 150px" placeholder="表记编号" id="MeterNoS" />&nbsp;
		    抄表日期&nbsp;从&nbsp;<input type="text" class="input-text size-MINI" style="width: 150px" id="MinRODate" />&nbsp;
		    &nbsp;至&nbsp;<input type="text" class="input-text size-MINI" style="width: 150px" id="MaxRODate" />&nbsp;
            <br />
                抄表类型&nbsp;<select class="input-text size-MINI" style="width: 120px" id="ReadoutTypeS">
                    <option value="" selected>全部</option>
                    <option value="1">正常抄表</option>
                    <option value="2">临时抄表</option>
                    <option value="0">租前抄表</option>
                </select>&nbsp;
            表记类型&nbsp;<select class="input-text size-MINI" style="width: 120px" id="MeterTypeS">
                <option value="" selected>全部</option>
                <option value="wm">水表</option>
                <option value="am">电表</option>
            </select>&nbsp;
            状态&nbsp;<select class="input-text size-MINI" style="width: 120px" id="AuditStatusS">
                <option value="" selected>全部</option>
                <option value="0">待审核</option>
                <option value="1">审核通过</option>
                <option value="-1">审核不通过</option>
            </select>&nbsp;
		    <button type="submit" class="btn btn-success radius" onclick="select()"><i class="Hui-iconfont">&#xe665;</i> 查询</button>
            </div>
            <div class="mt-5" id="outerlist">
                <%=list %>
            </div>
        </div>
        <div id="edit" class="pt-5 pr-20 pb-5 pl-20" style="display: none;">
            <div class="form form-horizontal bk-gray mt-15 pb-10" id="editlist">
                <div class="row cl">
                    <label class="form-label col-2"><span class="c-red">*</span>表记编号：</label>
                    <div class="formControls col-4">
                        <input type="text" class="input-text required" placeholder="表记编号" id="MeterNo" style="width: 240px;" data-valid="isNonEmpty" data-error="请选择原表记编号" />
                        <button type="button" class="btn btn-primary radius" id="chooseMeterNo">选择</button>
                        <input type="hidden" id="MeterDigit" />
                    </div>
                    <div class="col-1"></div>
                </div>

                <div class="row cl">
                    <label class="form-label col-2"><span class="c-red">*</span>房间编号：</label>
                    <div class="formControls col-4">
                        <input type="text" class="input-text required" placeholder="房间编号" id="RMID" disabled="disabled" data-valid="between:0-30" data-error="" />
                    </div>
                    <div class="col-1"></div>
                </div>

                <div class="row cl">
                    <label class="form-label col-2"><span class="c-red">*</span>上期读数：</label>
                    <div class="formControls col-4">
                        <input type="text" class="input-text required" placeholder="上期读数" disabled="disabled" id="LastReadout" data-valid="onlyNum" data-error="未读取上期读数" />
                    </div>
                    <div class="col-1"></div>
                </div>

                <div class="row cl">
                    <label class="form-label col-2"><span class="c-red">*</span>本期读数：</label>
                    <div class="formControls col-4">
                        <input type="text" class="input-text required" placeholder="本期读数" id="Readout" data-valid="onlyNum" data-error="需是数字且不能小于0" />
                    </div>
                    <div class="col-1"></div>
                </div>

                <div class="row cl">
                    <label class="form-label col-2"><span class="c-red">*</span>关联表记读数：</label>
                    <div class="formControls col-4">
                        <input type="text" class="input-text required" placeholder="关联表记读数" id="JoinReadings" data-valid="onlyNum" data-error="需是数字且不能小于0" />
                    </div>
                    <div class="col-1"></div>
                </div>

                <div class="row cl" style="display: none;">
                    <label class="form-label col-2"><span class="c-red">*</span>倍率：</label>
                    <div class="formControls col-4">
                        <input type="text" class="input-text required" placeholder="倍率" id="MeteRate" data-valid="onlyNum" data-error="需是数字" />
                    </div>
                    <div class="col-1"></div>
                </div>

                <div class="row cl">
                    <label class="form-label col-2"><span class="c-red">*</span>行度：</label>
                    <div class="formControls col-4">
                        <input type="text" class="input-text required" placeholder="行度" disabled="disabled" id="Readings" data-valid="onlyNum" data-error="需是数字且不能小于0" />
                    </div>
                    <div class="col-1"></div>
                </div>

                <div class="row cl">
                    <label class="form-label col-2"><span class="c-red">*</span>抄表类型：</label>
                    <div class="formControls col-4">
                        <select class="input-text required" id="ReadoutType" data-valid="isNonEmpty" data-error="请选择抄表类型">
                            <option value="">请选择抄表类型</option>
                            <option value="1">正常抄表</option>
                            <option value="2">临时抄表</option>
                            <option value="0">租前抄表</option>
                        </select>
                    </div>
                    <div class="col-1"></div>
                </div>

                <div class="row cl">
                    <label class="form-label col-2"><span class="c-red">*</span>抄表人：</label>
                    <div class="formControls col-4">
                        <%=ROOperatorStr %>
                        <%--<input type="text" class="input-text required" placeholder="抄表人" id="ROOperator" data-valid="isNonEmpty||between:1-30" data-error="请填写抄表人||抄表人长度为1-30位"/>--%>
                    </div>
                    <div class="col-1"></div>
                </div>

                <div class="row cl">
                    <label class="form-label col-2"><span class="c-red">*</span>抄表日期：</label>
                    <div class="formControls col-4">
                        <input type="text" class="input-text required" placeholder="抄表日期" id="RODate" data-valid="isNonEmpty" data-error="请选择抄表日期" />
                    </div>
                    <div class="col-1"></div>
                </div>
                <div class="row cl">
                    <label class="form-label col-2"><span class="c-red">*</span>抄表图片：</label>
                    <div class="formControls col-4">
                        <form action="ReadouImg.ashx" method="post" id="upload" enctype="multipart/form-data">
                            <div id="showimg" style="height: 120px; width: 120px; float: left; position: relative; margin-right: 15px; display: none;">
                                <img src="#" id="showbtn" style="height: 100%; width: 100%;" />
                                <i class="Hui-iconfont Hui-iconfont-close" style="position: absolute; right: 1px; top: 1px; padding: 0px 2px; color: red; background-color: #d6e1e3; cursor: pointer;" onclick="delimg()"></i>
                            </div>
                            <div id="addimg" style="height: 120px; width: 120px; float: left; border: 1px dashed #171515; position: relative;">
                                <input id="addbtn" type="file" name="imgfile" style="height: 70px; width: 70px; position: absolute; left: 25px; top: 25px; overflow: hidden; opacity: 0; cursor: pointer;" onchange="addimg()" />
                                <i class="Hui-iconfont Hui-iconfont-add" style="display: block; text-align: center; line-height: 120px; font-size: 32px; color: #5c5555;"></i>
                            </div>
                            <div style="display: none">
                                <input type="hidden" id="Img" name="Img" value="" />
                            </div>
                        </form>
                    </div>
                    <div class="col-1"></div>
                </div>
                <div class="row cl">
                    <div class="col-9 col-offset-3">
                        <input class="btn btn-primary radius" type="button" onclick="submit()" value="&nbsp;&nbsp;提&nbsp;&nbsp;交&nbsp;&nbsp;" />
                        <input class="btn btn-primary radius" type="button" onclick="submit1()" value="&nbsp;&nbsp;提交继续录入&nbsp;&nbsp;" />
                        <input class="btn btn-default radius" type="button" onclick="cancel()" value="&nbsp;&nbsp;取&nbsp;&nbsp;消&nbsp;&nbsp;" />
                    </div>
                </div>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
            </div>
        </div>
    </div>

    <div id="auditdiv" style="display: none;">
        <table style="width: 380px; margin-top: 10px;">
            <tr>
                <td style="width: 80px; text-align: center; padding: 8px; height: 130px;">不通过原因</td>
                <td style="width: 300px;">
                    <textarea cols="" rows="3" class="textarea required" placeholder="不通过原因" id="AuditReason"></textarea>
                </td>
            </tr>
        </table>
        <div style="margin: 10px 30px;">
            <input class="btn btn-primary radius" type="button" onclick="auditsubmit()" value="&nbsp;确&nbsp;定&nbsp;" />&nbsp;&nbsp;
            <input class="btn btn-primary radius" type="button" onclick="auditcancel()" value="&nbsp;关&nbsp;闭&nbsp;" />
        </div>
    </div>
    <iframe id="openfile" width="0" height="0" marginwidth="0" frameborder="0" src="about:blank" content-disposition="attachment"></iframe>
    <script type="text/javascript" src="../../jscript/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="../../jscript/script_common.js"></script>
    <script type="text/javascript" src="../../jscript/json2.js"></script>
    <script type="text/javascript" src="../../jscript/H-ui.js"></script>
    <script type="text/javascript" src="../../jscript/H-ui.admin.js"></script>
    <script type="text/javascript" src="../../jscript/jquery-form.js"></script>
    <script type="text/javascript" src="../../lib/layer/layer.js"></script>
    <script type="text/javascript" src="../../lib/validate/jquery.validate.js"></script>
    <script type="text/javascript" src="../../jscript/JsInputDate.js"></script>

    <script type="text/javascript" src="../../lib/zTree/js/jquery.ztree.all-3.5.min.js"></script>
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
                else if (vjson.flag == "3") {
                    layer.alert("当前记录已审核，不允许删除！");
                }
                else {
                    layer.alert("数据操作出错！");
                }
                return;
            }
            if (vjson.type == "submit") {
                if (vjson.flag == "1") {
                    $("#selectKey").val("");
                    $("#outerlist").html(vjson.liststr);
                    $("#MeterNoS").val(vjson.MeterNo);
                    $("#list").css("display", "");
                    $("#edit").css("display", "none");
                    reflist();
                }
                else if (vjson.flag == "3") {
                    showMsg("MeterNo", "表记编号不存在", "1");
                }
                else {
                    layer.msg("数据操作出错！");
                }
                return;
            }
            if (vjson.type == "submit1") {
                if (vjson.flag == "1") {
                    $("#selectKey").val("");
                    $("#outerlist").html(vjson.liststr);
                    $("#MeterNoS").val(vjson.MeterNo);
                    reflist();
                    insert();
                }
                else if (vjson.flag == "3") {
                    showMsg("MeterNo", "表记编号不存在", "1");
                }
                else {
                    layer.msg("数据操作出错！");
                }
                return;
            }
            if (vjson.type == "update") {
                if (vjson.flag == "1") {
                    $("#RMID").val(vjson.RMID);
                    $("#MeterNo").val(vjson.MeterNo);
                    $("#ReadoutType").val(vjson.ReadoutType);
                    $("#LastReadout").val(vjson.LastReadout);
                    $("#Readout").val(vjson.Readout);
                    $("#JoinReadings").val(vjson.JoinReadings);
                    $("#Readings").val(vjson.Readings);
                    $("#MeteRate").val(vjson.MeteRate);
                    $("#ROOperator").val(vjson.ROOperator);
                    $("#RODate").val(vjson.RODate);
                    $("#MeterDigit").val(vjson.MeterDigit);
                    $("#showimg img").attr("src", vjson.Img);
                    $("#chooseRMID").unbind('click');
                    $("#chooseMeterNo").unbind('click');
                    $("#list").css("display", "none");
                    $("#edit").css("display", "");
                    if (vjson.Img != "") {
                        $("#showimg img").attr("src", "../../upload/meter/" + vjson.Img);
                        $("#showimg").show();
                    }
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
            if (vjson.type == "unaudit") {
                if (vjson.flag == "1") {
                    layer.msg("取消审核成功！", { icon: 3, time: 1000 });

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
            if (vjson.type == "gettreelist") {
                if (vjson.flag == "1") {

                    var Loc3 = "";
                    var Loc4 = "";
                    var roomnode = new Array();
                    var floornode = new Array();
                    var treelist = jQuery.parseJSON(vjson.list);
                    for (var i = 1; i <= vjson.count; i++) {
                        var line = jQuery.parseJSON(treelist[i]);

                        var meternode = new Array();
                        var nodelist = jQuery.parseJSON(line.mlist);
                        for (var j = 1; j <= line.mcount; j++) {
                            var line1 = jQuery.parseJSON(nodelist[j]);
                            var node = new Object();
                            node.id = line1.MeterNo;
                            node.name = line1.MeterNo;
                            node.open = false;
                            meternode.push(node);
                        }

                        if (line.Loc3 + Loc4 != line.Loc3 + line.Loc4) {
                            if (Loc4 != "") {
                                node = new Object();
                                node.name = Loc4;
                                node.id = Loc4;
                                node.open = false;
                                node.children = roomnode;
                                floornode.push(node);
                            }
                            roomnode = new Array();
                            Loc4 = line.Loc4;
                        }
                        else if (i == vjson.count) {
                            node = new Object();
                            node.name = Loc4;
                            node.id = Loc4;
                            node.open = false;
                            node.children = roomnode;
                            floornode.push(node);
                            roomnode = new Array();
                        }

                        if (Loc3 != line.Loc3) {
                            if (Loc3 != "") {
                                node = new Object();
                                node.name = Loc3;
                                node.id = Loc3;
                                node.open = false;
                                node.children = floornode;
                                zNodes.push(node);
                            }
                            floornode = new Array();
                            Loc3 = line.Loc3;
                        }

                        node = new Object();
                        node.name = line.RMID;
                        node.id = line.RMID;
                        node.open = false;
                        node.children = meternode;
                        roomnode.push(node);
                    }

                    if (Loc3 != "") {
                        node = new Object();
                        node.name = Loc3;
                        node.id = Loc3;
                        node.open = false;
                        node.children = floornode;
                        zNodes.push(node);
                    }

                    var t = $("#ztree");
                    t = $.fn.zTree.init(t, setting, zNodes);
                    var zTree = $.fn.zTree.getZTreeObj("ztree");
                    zTree.selectNode(zTree.getNodeByParam("id", ""));
                }
                else {
                    layer.alert("获取数据出错！");
                }
                return;
            }
            if (vjson.type == "getMeterInfo") {
                if (vjson.flag == "1") {
                    $("#MeterNo").val(vjson.MeterNo);
                    $("#RMID").val(vjson.MeterRMID);
                    $("#LastReadout").val(vjson.readout);
                    $("#MeteRate").val(vjson.rate);
                    $("#MeterDigit").val(vjson.digit);
                    $("#JoinReadings").val("0");

                    if ($("#Readout").val() != "" && $("#LastReadout").val() != "") {
                        //$("#Readings").val(parseFloat($("#Readout").val()) - parseFloat($("#LastReadout").val()));
                        calureadout();
                    }
                    else {
                        $("#Readings").val("")
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
            $("#chooseMeterNo").unbind('click').bind("click", chooseMeterNo);

            $("#RMID").val("");
            $("#MeterNo").val(meterNo);
            $("#ReadoutType").val("1");
            $("#LastReadout").val("");
            $("#Readout").val("");
            $("#JoinReadings").val("");
            $("#Readings").val("");
            $("#MeteRate").val("");
            $("#ROOperator").val("");
            $("#RODate").val("<%=date %>");

            $("#list").css("display", "none");
            $("#edit").css("display", "");
            $("#Img").val("");
            $("#showimg").hide();

            if ($("#MeterNo").val() != "") {
                var submitData = new Object();
                submitData.Type = "getMeterInfo";
                submitData.MeterNo = $("#MeterNo").val();
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
                submitData.RMID = $("#RMID").val();
                submitData.MeterNo = $("#MeterNo").val();
                submitData.ReadoutType = $("#ReadoutType").val();
                submitData.LastReadout = $("#LastReadout").val();
                submitData.Readout = $("#Readout").val();
                submitData.JoinReadings = $("#JoinReadings").val();
                submitData.Readings = $("#Readings").val();
                submitData.MeteRate = $("#MeteRate").val();
                submitData.ROOperator = $("#ROOperator").val();
                submitData.RODate = $("#RODate").val();
                submitData.Img = $("#Img").val();

                submitData.tp = type;
                submitData.RMIDS = $("#RMIDS").val();
                submitData.MeterNoS = $("#MeterNoS").val();
                submitData.ReadoutTypeS = $("#ReadoutTypeS").val();
                submitData.AuditStatusS = $("#AuditStatusS").val();
                submitData.MeterTypeS = $("#MeterTypeS").val();
                submitData.MinRODate = $("#MinRODate").val();
                submitData.MaxRODate = $("#MaxRODate").val();
                submitData.page = page;
                transmitData(datatostr(submitData));
            }
            return;
        }
        function submit1() {
            if ($('#editlist').validate('submitValidate')) {
                var submitData = new Object();
                submitData.Type = "submit1";
                submitData.id = id;
                submitData.RMID = $("#RMID").val();
                submitData.MeterNo = $("#MeterNo").val();
                submitData.ReadoutType = $("#ReadoutType").val();
                submitData.LastReadout = $("#LastReadout").val();
                submitData.Readout = $("#Readout").val();
                submitData.JoinReadings = $("#JoinReadings").val();
                submitData.Readings = $("#Readings").val();
                submitData.MeteRate = $("#MeteRate").val();
                submitData.ROOperator = $("#ROOperator").val();
                submitData.RODate = $("#RODate").val();
                submitData.Img = $("#Img").val();

                submitData.tp = type;
                submitData.RMIDS = $("#RMIDS").val();
                submitData.MeterNoS = $("#MeterNoS").val();
                submitData.ReadoutTypeS = $("#ReadoutTypeS").val();
                submitData.AuditStatusS = $("#AuditStatusS").val();
                submitData.MeterTypeS = $("#MeterTypeS").val();
                submitData.MinRODate = $("#MinRODate").val();
                submitData.MaxRODate = $("#MaxRODate").val();
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
                submitData.MeterNoS = $("#MeterNoS").val();
                submitData.ReadoutTypeS = $("#ReadoutTypeS").val();
                submitData.AuditStatusS = $("#AuditStatusS").val();
                submitData.MeterTypeS = $("#MeterTypeS").val();
                submitData.MinRODate = $("#MinRODate").val();
                submitData.MaxRODate = $("#MaxRODate").val();
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
            layer.confirm('确认要审核通过吗？', function (index) {
                var submitData = new Object();
                submitData.Type = "audit";
                submitData.id = $("#selectKey").val();

                submitData.RMIDS = $("#RMIDS").val();
                submitData.MeterNoS = $("#MeterNoS").val();
                submitData.ReadoutTypeS = $("#ReadoutTypeS").val();
                submitData.AuditStatusS = $("#AuditStatusS").val();
                submitData.MeterTypeS = $("#MeterTypeS").val();
                submitData.MinRODate = $("#MinRODate").val();
                submitData.MaxRODate = $("#MaxRODate").val();
                submitData.page = page;
                transmitData(datatostr(submitData));
                layer.close(index);
            });
            return;
        }
        function unaudit() {
            if ($("#selectKey").val() == "") {
                layer.msg("请先选择一条记录", { icon: 3, time: 1000 });
                return;
            }
            layer.confirm('确认要取消审核吗？', function (index) {
                var submitData = new Object();
                submitData.Type = "unaudit";
                submitData.id = $("#selectKey").val();

                submitData.RMIDS = $("#RMIDS").val();
                submitData.MeterNoS = $("#MeterNoS").val();
                submitData.ReadoutTypeS = $("#ReadoutTypeS").val();
                submitData.AuditStatusS = $("#AuditStatusS").val();
                submitData.MeterTypeS = $("#MeterTypeS").val();
                submitData.MinRODate = $("#MinRODate").val();
                submitData.MaxRODate = $("#MaxRODate").val();
                submitData.page = page;
                transmitData(datatostr(submitData));
                layer.close(index);
            });
            //layer.confirm('确认要审核不通过吗？', function (index) {
            //    $("#AuditReason").val("");
            //    layer.open({
            //        type: 1,
            //        area: ["400px", "240px"],
            //        fix: true,
            //        maxmin: true,
            //        scrollbar: false,
            //        shade: 0.5,
            //        title: "不通过原因填写",
            //        content: $("#auditdiv")
            //    });
            //    layer.close(index);
            //});
            return;
        }
        function auditsubmit() {
            if ($("#AuditReason").val() == "") {
                layer.msg("请填写不通过的原因", { icon: 3, time: 1000 });
                return;
            }
            var submitData = new Object();
            submitData.Type = "unaudit";
            submitData.id = $("#selectKey").val();
            submitData.AuditReason = $("#AuditReason").val();

            submitData.RMIDS = $("#RMIDS").val();
            submitData.MeterNoS = $("#MeterNoS").val();
            submitData.ReadoutTypeS = $("#ReadoutTypeS").val();
            submitData.AuditStatusS = $("#AuditStatusS").val();
            submitData.MeterTypeS = $("#MeterTypeS").val();
            submitData.MinRODate = $("#MinRODate").val();
            submitData.MaxRODate = $("#MaxRODate").val();
            submitData.page = page;
            transmitData(datatostr(submitData));
            layer.closeAll();
            return;
        }
        function auditcancel() {
            layer.closeAll();
        }

        function select() {
            var submitData = new Object();
            submitData.Type = "select";

            submitData.RMIDS = $("#RMIDS").val();
            submitData.MeterNoS = $("#MeterNoS").val();
            submitData.ReadoutTypeS = $("#ReadoutTypeS").val();
            submitData.AuditStatusS = $("#AuditStatusS").val();
            submitData.MeterTypeS = $("#MeterTypeS").val();
            submitData.MinRODate = $("#MinRODate").val();
            submitData.MaxRODate = $("#MaxRODate").val();
            submitData.page = 1;
            transmitData(datatostr(submitData));
            return;
        }
        function excel() {
            var submitData = new Object();
            submitData.Type = "excel";

            submitData.RMIDS = $("#RMIDS").val();
            submitData.MeterNoS = $("#MeterNoS").val();
            submitData.ReadoutTypeS = $("#ReadoutTypeS").val();
            submitData.AuditStatusS = $("#AuditStatusS").val();
            submitData.MeterTypeS = $("#MeterTypeS").val();
            submitData.MinRODate = $("#MinRODate").val();
            submitData.MaxRODate = $("#MaxRODate").val();
            transmitData(datatostr(submitData));
            return;
        }
        function cancel() {
            if (id == "" && $("#Img").val().length > 0) {
                $("#upload").ajaxSubmit({
                    url: "ReadoutImg.ashx?flag=2",
                    type: "post",
                    dataType: "json",
                    contentType: "application/x-www-form-urlencoded;charset=utf-8",
                    success: function (data) {
                        if (data.flag > 1) {
                            console.log(data.info);
                            console.log(data.exinfo);
                        }
                    },
                    error: function () {
                        console.log("数据加载有误！");
                    }
                });
            }
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
            submitData.MeterNoS = $("#MeterNoS").val();
            submitData.ReadoutTypeS = $("#ReadoutTypeS").val();
            submitData.AuditStatusS = $("#AuditStatusS").val();
            submitData.MeterTypeS = $("#MeterTypeS").val();
            submitData.MinRODate = $("#MinRODate").val();
            submitData.MaxRODate = $("#MaxRODate").val();
            submitData.page = page;
            transmitData(datatostr(submitData));
            return;
        }

        function chooseRMID() {
            var temp = "../Base/ChooseRMID.aspx?id=RMID";
            layer_show("选择页面", temp, 800, 630);
        }
        function chooseMeterNo() {
            //if ($("#RMID").val() == "") {
            //    layer.msg("请先选择房间编号", { icon: 3, time: 1000 });
            //    return;
            //}
            var temp = "../Base/ChooseMeter.aspx?id=MeterNo&RMID=";// + $("#RMID").val();
            layer_show("选择页面", temp, 800, 630);
        }
        function choose(id, labels, values) {
            if (labels != "" && labels != undefined && labels != "undefined") {
                if (id == "MeterNo") {
                    var submitData = new Object();
                    submitData.Type = "getMeterInfo";
                    submitData.MeterNo = labels;
                    transmitData(datatostr(submitData));
                }
                else {
                    $("#" + id).val(labels);
                }
            }
        }

        $("#MeterNo").blur(function () {
            if ($("#MeterNo").val() != "") {
                var submitData = new Object();
                submitData.Type = "getMeterInfo";
                submitData.MeterNo = $("#MeterNo").val();
                transmitData(datatostr(submitData));
            }
            else {
                $("#MeterNo").val("");
                $("#RMID").val("");
                $("#LastReadout").val("");
                $("#MeteRate").val("");
                $("#MeterDigit").val("");
                $("#Readings").val("")
            }
            return;
        });

        $("#Readout").change(function () {
            calureadout();
        });
        $("#JoinReadings").change(function () {
            calureadout();
        });

        function calureadout() {
            if ($("#Readout").val() != "" && $("#LastReadout").val() != "") {
                if ($("#JoinReadings").val() == "") $("#JoinReadings").val("0");

                if (parseFloat($("#Readout").val()) >= parseFloat($("#LastReadout").val()))
                    $("#Readings").val(parseFloat($("#Readout").val()) - parseFloat($("#LastReadout").val()) - parseFloat($("#JoinReadings").val()))
                else {
                    if (parseInt($("#MeterDigit").val()) == 4)
                        $("#Readings").val(parseFloat($("#Readout").val()) + (9999 - parseFloat($("#LastReadout").val())) - parseFloat($("#JoinReadings").val()));
                    else if (parseInt($("#MeterDigit").val()) == 5)
                        $("#Readings").val(parseFloat($("#Readout").val()) + (99999 - parseFloat($("#LastReadout").val())) - parseFloat($("#JoinReadings").val()));
                    else if (parseInt($("#MeterDigit").val()) == 6)
                        $("#Readings").val(parseFloat($("#Readout").val()) + (999999 - parseFloat($("#LastReadout").val())) - parseFloat($("#JoinReadings").val()));
                    else if (parseInt($("#MeterDigit").val()) == 7)
                        $("#Readings").val(parseFloat($("#Readout").val()) + (9999999 - parseFloat($("#LastReadout").val())) - parseFloat($("#JoinReadings").val()));
                    else
                        $("#Readings").val(parseFloat($("#Readout").val()) + (99999999 - parseFloat($("#LastReadout").val())) - parseFloat($("#JoinReadings").val()));
                }
            }
            else {
                $("#Readings").val("")
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


        jQuery(function () {
            var RODate = new JsInputDate("RODate");
            var MinRODate = new JsInputDate("MinRODate");
            var MaxRODate = new JsInputDate("MaxRODate");

            var submitData = new Object();
            submitData.Type = "gettreelist";
            transmitData(datatostr(submitData));
        });


        var setting = {
            view: {
                dblClickExpand: false,
                showLine: false,
                selectedMulti: false
            },
            data: {
                simpleData: {
                    enable: true,
                    idKey: "id",
                    pIdKey: "pId",
                    rootPId: ""
                }
            },
            callback: {
                beforeClick: function (treeId, treeNode) {
                    var zTree = $.fn.zTree.getZTreeObj("tree");
                    if (treeNode.isParent) {
                        zTree.expandNode(treeNode);
                        return false;
                    } else {
                        meterNo = treeNode.id;
                        return true;
                    }
                }
            }
        };
        var meterNo = "";
        var zNodes = new Array();


        var trid = "";
        reflist();

        //抄表图片处理

        function addimg() {
            if ($("#MeterNo").val().length <= 0) {
                layer.msg("请选择表计！");
                return;
            }
            $("#upload").ajaxSubmit({
                url: "ReadoutImg.ashx?flag=1&meterNo=" + $("#MeterNo").val() + "&id=" + id,
                type: "post",
                dataType: "json",
                contentType: "application/x-www-form-urlencoded;charset=utf-8",
                success: function (data) {
                    if (data.flag == 1) {
                        $("#Img").val(data.img);
                        $("#showimg img").attr("src", "../../upload/meter/" + data.img);
                        $("#showimg").show();
                    }
                    layer.alert(data.info);
                    console.log(data.exinfo);
                },
                error: function () {
                    layer.msg("数据加载有误！");
                }
            });
        }
        function delimg() {
            $("#upload").ajaxSubmit({
                url: "ReadoutImg.ashx?flag=2&id=" + id,
                type: "post",
                dataType: "json",
                contentType: "application/x-www-form-urlencoded;charset=utf-8",
                success: function (data) {
                    if (data.flag == 1) {
                        $("#Img").val("");
                        $("#showimg img").attr("src", "");
                        $("#showimg").hide();
                    }
                    layer.alert(data.info);
                    console.log(data.exinfo);
                },
                error: function () {
                    layer.msg("数据加载有误！");
                }
            });
        }
    </script>
</body>
</html>
