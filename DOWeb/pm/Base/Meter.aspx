<%@ Page Language="C#" AutoEventWireup="true" Inherits="project.Presentation.Base.Meter,project"  %>
<!DOCTYPE>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="head1" runat="server">
    <title>表记资料</title>
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
   <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 表计管理 <span class="c-gray en">&gt;</span>表记资料 <a class="btn btn-success radius r mr-20" style="line-height:1.6em;margin-top:2px" href="javascript:location.replace(location.href);" title="刷新" ><i class="Hui-iconfont">&#xe68f;</i></a></nav>
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
            园区&nbsp;<%=MeterLOCNo1StrS %>&nbsp;
            建设期&nbsp;<select class="input-text required size-MINI" id="MeterLOCNo2S" style="width:120px"><option value="">全部</option></select>&nbsp;
            楼栋&nbsp;<select class="input-text required size-MINI" id="MeterLOCNo3S" style="width:120px"><option value="">全部</option></select>&nbsp;
            楼层&nbsp;<select class="input-text required size-MINI" id="MeterLOCNo4S" style="width:120px"><option value="">全部</option></select>&nbsp;
		    状态&nbsp;<select class="input-text size-MINI ml-5 mr-10" style="width:100px" id="MeterStatusS">
                <option value="" selected>全部</option>
                <option value="open">正常</option>
                <option value="close">停用</option>
		      </select>
            <br />
		    表记编号&nbsp;<input type="text" class="input-text size-MINI ml-5 mr-10" style="width:120px" placeholder="表记编号" id="MeterNoS" />&nbsp;
		    表记类别&nbsp;<select class="input-text size-MINI ml-5 mr-10" style="width:100px" id="MeterTypeS">
                <option value="" selected>全部</option>
                <option value="wm">水表</option>
                <option value="am">电表</option>
		      </select>
		    使用类别&nbsp;<select class="input-text size-MINI ml-5 mr-10" style="width:100px" id="MeterUsageTypeS">
                <option value="" selected>全部</option>
                <option value="0">公共</option>
                <option value="1">家用</option>
                <option value="2">商用</option>
                <option value="3">其他</option>

		      </select>
		    大小类别&nbsp;<select class="input-text size-MINI ml-5 mr-10" style="width:100px" id="MeterSizeS">
                <option value="" selected>全部</option>
                <option value="1">大表</option>
                <option value="2">小表</option>
		      </select>
		    房间号&nbsp;<input type="text" class="input-text size-MINI ml-5 mr-10" style="width:120px" placeholder="位置" id="MeterRMIDS" />&nbsp;
		    <button type="button" class="btn btn-success radius" onclick="select()"><i class="Hui-iconfont">&#xe665;</i> 查询</button>
	    </div>
	    <div class="mt-5" id="outerlist">
	        <%=list %>
	    </div>
    </div>
    <div id="edit" class="pt-5 pr-20 pb-5 pl-20 " style="display:none;">
      <div class="form form-horizontal bk-gray mt-15 pb-10" id="editlist">
        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>表记编号：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text w-200 required" placeholder="表记编号" id="MeterNo" data-valid="isNonEmpty||between:1-30" data-error="表记编号不能为空||表记编号长度为1-30位" />
          </div>
            <div class="col-3"></div>
        </div>

        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>表记名称：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text w-200 required" placeholder="表记名称" id="MeterName" data-valid="isNonEmpty||between:2-50" data-error="表记名称不能为空||表记名称长度为2-50位"/>
          </div>
          <div class="col-3"></div>
        </div>

        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>表记类别：</label>
          <div class="formControls col-4">
            <select class="input-text w-200 required" id="MeterType" data-valid="isNonEmpty" data-error="表记类别不能为空">
                <option value="">请选择表记类别</option>
                <option value="wm">水表</option>
                <option value="am">电表</option>
            </select>
          </div>
          <div class="col-3"></div>
        </div>

        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>表记倍率：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text w-200 required" placeholder="表记倍率" id="MeterRate" data-valid="isNonEmpty||onlyNum" data-error="表记倍率不能为空||须为数字"/>
          </div>
          <div class="col-3"></div>
        </div>

        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>表记位数：</label>
          <div class="formControls col-4">
            <select class="input-text w-200 required" id="MeterDigit" data-valid="isNonEmpty" data-error="请选择表记位数">
                <option value="4">4</option>
                <option value="5">5</option>
                <option value="6">6</option>
                <option value="7">7</option>
                <option value="8">8</option>
            </select>
          </div>
          <div class="col-3"></div>
        </div>

        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>使用类别：</label>
          <div class="formControls col-4">
            <select class="input-text w-200 required" id="MeterUsageType" data-valid="isNonEmpty" data-error="使用类别不能为空">
                <option value="">请选择使用类别</option>
                <option value="0">公共</option>
                <option value="1">家用</option>
                <option value="2">商用</option>
                <option value="3">其他</option>
            </select>
          </div>
          <div class="col-3"></div>
        </div>

        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>性质类别：</label>
          <div class="formControls col-4">
            <select class="input-text w-200 required" id="MeterNatureType" data-valid="isNonEmpty" data-error="性质类别不能为空">
                <option value="">请选择性质类别</option>
                <option value="1">居民用电</option>
                <option value="2">工业用电</option>
                <option value="3">商业用电</option>
                <option value="4">其它用电</option>
            </select>
          </div>
          <div class="col-3"></div>
        </div>

        <div class="row cl">         
          <label class="form-label col-2"><span class="c-red">*</span>园区：</label>
          <div class="formControls col-4">
            <%=MeterLOCNo1Str %>
          </div>
          <div class="col-3"></div>
        </div>
             
        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>建设期：</label>
          <div class="formControls col-4">
            <select class="input-text required" id="MeterLOCNo2" data-valid="isNonEmpty" data-error="建设期不能为空" ></select>
          </div>
          <div class="col-3"></div>
        </div> 
        
        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>楼栋：</label>
          <div class="formControls col-4">
            <select class="input-text required" id="MeterLOCNo3" data-valid="isNonEmpty" data-error="楼栋不能为空" ></select>
          </div>
          <div class="col-3"></div>
        </div> 
        
        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>楼层：</label>
          <div class="formControls col-4">
            <select class="input-text required" id="MeterLOCNo4" data-valid="isNonEmpty" data-error="楼层不能为空" ></select>
          </div>
          <div class="col-3"></div>
        </div>

        <div class="row cl">
          <label class="form-label col-2">房间编号：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text required" placeholder="房间编号" id="MeterRMID" style="width:240px;" disabled="disabled"  data-valid="between:0-30" data-error="" />
            <button type="button" class="btn btn-primary radius" id="chooseRMID">选择</button>
          </div>
          <div class="col-3"></div>
        </div>      

        <div class="row cl">
          <label class="form-label col-2">位置：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text w-200 required" placeholder="位置" id="Addr"  data-valid="between:0-200" data-error="位置长度为0-200位"/>
          </div>
          <div class="col-3"></div>
        </div>        

        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>大小类别：</label>
          <div class="formControls col-4">
            <select class="input-text w-200 required" id="MeterSize"  data-valid="isNonEmpty" data-error="大小类别不能为空">
                <option value="">请选择大小类别</option>
                <option value="1">大表</option>
                <option value="2">小表</option>
            </select>
          </div>
          <div class="col-3"></div>
        </div>

        <div class="row cl" style="display:none;">
          <label class="form-label col-2">关联表记编号：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text w-200 required" placeholder="关联表记编号" id="MeterRelatedMeterNo"  data-valid="between:0-200" data-error="房间编号长度为0-200位"/>
          </div>
          <div class="col-3"></div>
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
                    showMsg("MeterNo", "此表记编号已存在", "1");
                }
                else {
                    layer.msg("数据操作出错！");
                }
                return;
            }
            if (vjson.type == "update") {
                if (vjson.flag == "1") {
                    $("#MeterNo").val(vjson.MeterNo);
                    $("#MeterName").val(vjson.MeterName);
                    $("#MeterType").val(vjson.MeterType);
                    $("#MeterRate").val(vjson.MeterRate);
                    $("#MeterDigit").val(vjson.MeterDigit);
                    $("#MeterUsageType").val(vjson.MeterUsageType);
                    $("#MeterNatureType").val(vjson.MeterNatureType);
                    $("#MeterRMID").val(vjson.MeterRMID);
                    $("#MeterSize").val(vjson.MeterSize);
                    $("#MeterRelatedMeterNo").val(vjson.MeterRelatedMeterNo);
                    $("#Addr").val(vjson.Addr);

                    $("#MeterLOCNo2").empty();
                    $("#MeterLOCNo2").append("<option value=''>请选择</option>");
                    for (var i = 0; i <= vjson.row - 1; i++) {
                        var option = "<option value='" + vjson.subtype.split(";")[i].split(":")[0].toString() + "'>" + vjson.subtype.split(";")[i].split(":")[1].toString() + "</option>";
                        $("#MeterLOCNo2").append(option);
                    }

                    $("#MeterLOCNo3").empty();
                    $("#MeterLOCNo3").append("<option value=''>请选择</option>");
                    for (var i = 0; i <= vjson.row1 - 1; i++) {
                        var option = "<option value='" + vjson.subtype1.split(";")[i].split(":")[0].toString() + "'>" + vjson.subtype1.split(";")[i].split(":")[1].toString() + "</option>";
                        $("#MeterLOCNo3").append(option);
                    }

                    $("#MeterLOCNo4").empty();
                    $("#MeterLOCNo4").append("<option value=''>请选择</option>");
                    for (var i = 0; i <= vjson.row2 - 1; i++) {
                        var option = "<option value='" + vjson.subtype2.split(";")[i].split(":")[0].toString() + "'>" + vjson.subtype2.split(";")[i].split(":")[1].toString() + "</option>";
                        $("#MeterLOCNo4").append(option);
                    }

                    $("#MeterLOCNo1").val(vjson.MeterLOCNo1);
                    $("#MeterLOCNo2").val(vjson.MeterLOCNo2);
                    $("#MeterLOCNo3").val(vjson.MeterLOCNo3);
                    $("#MeterLOCNo4").val(vjson.MeterLOCNo4);
                    
                    $("#MeterNo").attr("disabled", true);
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
                }
                else {
                    layer.alert("停止(启用)出现异常！");
                }
                return;
            }
            if (vjson.type == "getvalue") {
                if (vjson.flag == "1") {
                    $("#" + vjson.child).empty();

                    if (vjson.child == "MeterLOCNo2S" || vjson.child == "MeterLOCNo3S" || vjson.child == "MeterLOCNo4S")
                        $("#" + vjson.child).append("<option value=''>全部</option>");
                    else
                        $("#" + vjson.child).append("<option value=''>请选择</option>");

                    for (var i = 0; i <= vjson.row - 1; i++) {
                        var option = "<option value='" + vjson.subtype.split(";")[i].split(":")[0].toString() + "'>" + vjson.subtype.split(";")[i].split(":")[1].toString() + "</option>";
                        $("#" + vjson.child).append(option);
                    }

                    if (vjson.child == "MeterLOCNo2") {
                        $("#MeterLOCNo3").empty();
                        $("#MeterLOCNo3").append("<option value=''>请选择</option>");
                        $("#MeterLOCNo4").empty();
                        $("#MeterLOCNo4").append("<option value=''>请选择</option>");
                    }
                    else if (vjson.child == "MeterLOCNo3") {
                        $("#MeterLOCNo4").empty();
                        $("#MeterLOCNo4").append("<option value=''>请选择</option>");
                    }
                    else if (vjson.child == "MeterLOCNo2S") {
                        $("#MeterLOCNo3S").empty();
                        $("#MeterLOCNo3S").append("<option value=''>全部</option>");
                        $("#MeterLOCNo4S").empty();
                        $("#MeterLOCNo4S").append("<option value=''>全部</option>");
                    }
                    else if (vjson.child == "MeterLOCNo3S") {
                        $("#MeterLOCNo4S").empty();
                        $("#MeterLOCNo4S").append("<option value=''>全部</option>");
                    }
                }
                return;
            }
        }

        function insert() {
            $('#editlist').validate('reset');
            id = "";
            type = "insert";
            $("#MeterNo").attr("disabled", false);
            $("#MeterNo").val("");
            $("#MeterName").val("");
            $("#MeterType").val("");
            $("#MeterLOCNo1").val("");
            $("#MeterLOCNo2").val("");
            $("#MeterLOCNo3").val("");
            $("#MeterLOCNo4").val("");
            $("#MeterRate").val("1");
            $("#MeterDigit").val("");
            $("#MeterUsageType").val("2");
            $("#MeterNatureType").val("3");
            $("#MeterRMID").val("");
            $("#MeterSize").val("");
            $("#MeterRelatedMeterNo").val("");
            $("#Addr").val("");

            $("#MeterLOCNo2").empty();
            $("#MeterLOCNo2").append("<option value=''>请选择</option>");
            $("#MeterLOCNo3").empty();
            $("#MeterLOCNo3").append("<option value=''>请选择</option>");
            $("#MeterLOCNo4").empty();
            $("#MeterLOCNo4").append("<option value=''>请选择</option>");

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
                submitData.MeterNo = $("#MeterNo").val();
                submitData.MeterName = $("#MeterName").val();
                submitData.MeterType = $("#MeterType").val();
                submitData.MeterLOCNo1 = $("#MeterLOCNo1").val();
                submitData.MeterLOCNo2 = $("#MeterLOCNo2").val();
                submitData.MeterLOCNo3 = $("#MeterLOCNo3").val();
                submitData.MeterLOCNo4 = $("#MeterLOCNo4").val();
                submitData.MeterRate = $("#MeterRate").val();
                submitData.MeterDigit = $("#MeterDigit").val();
                submitData.MeterUsageType = $("#MeterUsageType").val();
                submitData.MeterNatureType = $("#MeterNatureType").val();
                submitData.MeterRMID = $("#MeterRMID").val();
                submitData.MeterSize = $("#MeterSize").val();
                submitData.MeterRelatedMeterNo = $("#MeterRelatedMeterNo").val();
                submitData.Addr = $("#Addr").val();
                
                submitData.tp = type;
                submitData.MeterNoS = $("#MeterNoS").val();
                submitData.MeterTypeS = $("#MeterTypeS").val();
                submitData.MeterLOCNo1S = $("#MeterLOCNo1S").val();
                submitData.MeterLOCNo2S = $("#MeterLOCNo2S").val();
                submitData.MeterLOCNo3S = $("#MeterLOCNo3S").val();
                submitData.MeterLOCNo4S = $("#MeterLOCNo4S").val();
                submitData.MeterUsageTypeS = $("#MeterUsageTypeS").val();
                submitData.MeterRMIDS = $("#MeterRMIDS").val();
                submitData.MeterSizeS = $("#MeterSizeS").val();
                submitData.MeterStatusS = $("#MeterStatusS").val();
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

                submitData.MeterNoS = $("#MeterNoS").val();
                submitData.MeterTypeS = $("#MeterTypeS").val();
                submitData.MeterLOCNo1S = $("#MeterLOCNo1S").val();
                submitData.MeterLOCNo2S = $("#MeterLOCNo2S").val();
                submitData.MeterLOCNo3S = $("#MeterLOCNo3S").val();
                submitData.MeterLOCNo4S = $("#MeterLOCNo4S").val();
                submitData.MeterUsageTypeS = $("#MeterUsageTypeS").val();
                submitData.MeterRMIDS = $("#MeterRMIDS").val();
                submitData.MeterSizeS = $("#MeterSizeS").val();
                submitData.MeterStatusS = $("#MeterStatusS").val();
                submitData.page = page;
                transmitData(datatostr(submitData));
                layer.close(index);
            });
            return;
        }
        function select() {
            var submitData = new Object();
            submitData.Type = "select";

            submitData.MeterNoS = $("#MeterNoS").val();
            submitData.MeterTypeS = $("#MeterTypeS").val();
            submitData.MeterLOCNo1S = $("#MeterLOCNo1S").val();
            submitData.MeterLOCNo2S = $("#MeterLOCNo2S").val();
            submitData.MeterLOCNo3S = $("#MeterLOCNo3S").val();
            submitData.MeterLOCNo4S = $("#MeterLOCNo4S").val();
            submitData.MeterUsageTypeS = $("#MeterUsageTypeS").val();
            submitData.MeterRMIDS = $("#MeterRMIDS").val();
            submitData.MeterSizeS = $("#MeterSizeS").val();
            submitData.MeterStatusS = $("#MeterStatusS").val();
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

                submitData.MeterNoS = $("#MeterNoS").val();
                submitData.MeterTypeS = $("#MeterTypeS").val();
                submitData.MeterLOCNo1S = $("#MeterLOCNo1S").val();
                submitData.MeterLOCNo2S = $("#MeterLOCNo2S").val();
                submitData.MeterLOCNo3S = $("#MeterLOCNo3S").val();
                submitData.MeterLOCNo4S = $("#MeterLOCNo4S").val();
                submitData.MeterUsageTypeS = $("#MeterUsageTypeS").val();
                submitData.MeterRMIDS = $("#MeterRMIDS").val();
                submitData.MeterSizeS = $("#MeterSizeS").val();
                submitData.MeterStatusS = $("#MeterStatusS").val();
                submitData.page = page;
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
        var page = 1;
        function jump(pageindex) {
            page = pageindex;
            var submitData = new Object();
            submitData.Type = "jump";

            submitData.MeterNoS = $("#MeterNoS").val();
            submitData.MeterTypeS = $("#MeterTypeS").val();
            submitData.MeterLOCNo1S = $("#MeterLOCNo1S").val();
            submitData.MeterLOCNo2S = $("#MeterLOCNo2S").val();
            submitData.MeterLOCNo3S = $("#MeterLOCNo3S").val();
            submitData.MeterLOCNo4S = $("#MeterLOCNo4S").val();
            submitData.MeterUsageTypeS = $("#MeterUsageTypeS").val();
            submitData.MeterRMIDS = $("#MeterRMIDS").val();
            submitData.MeterSizeS = $("#MeterSizeS").val();
            submitData.MeterStatusS = $("#MeterStatusS").val();
            submitData.page = page;
            transmitData(datatostr(submitData));
            return;
        }

        $("#chooseRMID").click(function () {
            var temp = "../Base/ChooseRMID.aspx?id=MeterRMID&LOCNo=" + $("#MeterLOCNo4").val();
            layer_show("选择页面", temp, 800, 630);
        });
        function choose(id, labels, values) {
            if (labels != "" && labels != undefined && labels != "undefined") {
                if (id == "MeterRMID") {
                    $("#" + id).val(labels);
                    $("#Addr").val(labels);
                }
            }
        }

        $("#MeterType").change(function () {
            changemetername();
        });
        $("#MeterRate").change(function () {
            changemetername();
        });
        $("#MeterDigit").change(function () {
            changemetername();
        });

        function changemetername() {
            var type = "";
            var digit = "";
            if ($("#MeterType").val() == "wm")
                type = "水表";
            else if ($("#MeterType").val() == "am")
                type = "电表";
            if ($("#MeterDigit").val() != null) digit = $("#MeterDigit").val();

            $("#MeterName").val(type + "-" + $("#MeterRate").val() + "倍-" + digit + "位");
        }

        jQuery("#MeterLOCNo1").change(function () {
            var submitData = new Object();
            submitData.Type = "getvalue";
            submitData.parent = $("#MeterLOCNo1").val();
            submitData.child = "MeterLOCNo2";
            transmitData(datatostr(submitData));
            return;
        });
        jQuery("#MeterLOCNo1S").change(function () {
            var submitData = new Object();
            submitData.Type = "getvalue";
            submitData.parent = $("#MeterLOCNo1S").val();
            submitData.child = "MeterLOCNo2S";
            transmitData(datatostr(submitData));
            return;
        });

        jQuery("#MeterLOCNo2").change(function () {
            var submitData = new Object();
            submitData.Type = "getvalue";
            submitData.parent = $("#MeterLOCNo2").val();
            submitData.child = "MeterLOCNo3";
            transmitData(datatostr(submitData));
            return;
        });
        jQuery("#MeterLOCNo2S").change(function () {
            var submitData = new Object();
            submitData.Type = "getvalue";
            submitData.parent = $("#MeterLOCNo2S").val();
            submitData.child = "MeterLOCNo3S";
            transmitData(datatostr(submitData));
            return;
        });

        jQuery("#MeterLOCNo3").change(function () {
            var submitData = new Object();
            submitData.Type = "getvalue";
            submitData.parent = $("#MeterLOCNo3").val();
            submitData.child = "MeterLOCNo4";
            transmitData(datatostr(submitData));
            return;
        });
        jQuery("#MeterLOCNo3S").change(function () {
            var submitData = new Object();
            submitData.Type = "getvalue";
            submitData.parent = $("#MeterLOCNo3S").val();
            submitData.child = "MeterLOCNo4S";
            transmitData(datatostr(submitData));
            return;
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
        },
        tiptype = "1");

        
        var trid = "";
        reflist();
</script>
</body>
</html>