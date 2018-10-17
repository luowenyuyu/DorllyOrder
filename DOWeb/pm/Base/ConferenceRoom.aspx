<%@ Page Language="C#" AutoEventWireup="true" Inherits="project.Presentation.Base.ConferenceRoom,project"  %>
<!DOCTYPE>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="head1" runat="server">
    <title>会议室资料</title>
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
   <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 基础资料 <span class="c-gray en">&gt;</span>会议室资料 <a class="btn btn-success radius r mr-20" style="line-height:1.6em;margin-top:2px" href="javascript:location.replace(location.href);" title="刷新" ><i class="Hui-iconfont">&#xe68f;</i></a></nav>
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
		    会议室编号&nbsp;<input type="text" class="input-text size-MINI ml-5 mr-10" style="width:150px" placeholder="会议室编号" id="CRNoS" />&nbsp;
		    会议室名称&nbsp;<input type="text" class="input-text size-MINI ml-5 mr-10" style="width:150px" placeholder="会议室名称" id="CRNameS" />&nbsp;
		    位置&nbsp;<input type="text" class="input-text size-MINI ml-5 mr-10" style="width:150px" placeholder="位置" id="CRAddrS" />&nbsp;
		    状态&nbsp;<select class="input-text size-MINI ml-5 mr-10" style="width:150px" id="CRStatusS">
                <option value="" selected>全部</option>
                <option value="free">空闲</option>
                <option value="use">占用</option>
                <option value="reserve">预定</option>
		      </select>
		    <button type="submit" class="btn btn-success radius" onclick="select()"><i class="Hui-iconfont">&#xe665;</i> 查询</button>
	    </div>
	    <div class="mt-5" id="outerlist">
	        <%=list %>
	    </div>
    </div>
    <div id="edit" class="pt-5 pr-20 pb-5 pl-20 " style="display:none;">
      <div class="form form-horizontal bk-gray mt-15 pb-10" id="editlist">
        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>会议室编号：</label>
          <div class="formControls col-2">
            <input type="text" class="input-text required" placeholder="会议室编号" id="CRNo" data-valid="isNonEmpty||between:1-30" data-error="会议室编号不能为空||会议室编号长度为1-30位" />
          </div>
          <div class="col-3"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>会议室名称：</label>
          <div class="formControls col-2">
            <input type="text" class="input-text required" placeholder="会议室名称" id="CRName" data-valid="isNonEmpty||between:2-50" data-error="会议室名称不能为空||会议室名称长度为2-50位"/>
          </div>
         
          <label class="form-label col-3"><span class="c-red">*</span>容纳人数：</label>
          <div class="formControls col-2">
            <input type="text" class="input-text required" placeholder="容纳人数" id="CRCapacity" data-valid="isNonEmpty" data-error="容纳人数不能为空"/>
          </div>
          <div class="col-3"></div>
        </div>
          <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>对内价格/小时：</label>
          <div class="formControls col-2">
            <input type="text" class="input-text required" placeholder="对内价格/小时" id="CRINPriceHour" data-valid="isNonEmpty||onlyNum" data-error="对内价格/小时不能为空||须为数字"/>
          </div>
       
          <label class="form-label col-3"><span class="c-red">*</span>对外价格/小时：</label>
          <div class="formControls col-2">
            <input type="text" class="input-text required" placeholder="对外价格/小时" id="CROUTPriceHour" data-valid="isNonEmpty||onlyNum" data-error="对外价格/小时不能为空||须为数字"/>
          </div>
          <div class="col-1"></div>
        </div>
        <div class="row cl">        
          <label class="form-label col-2"><span class="c-red">*</span>对内价格/半天：</label>
          <div class="formControls col-2">
            <input type="text" class="input-text required" placeholder="对内价格/半天" id="CRINPriceHalfDay" data-valid="isNonEmpty||onlyNum" data-error="对内价格/半天不能为空||须为数字"/>
          </div>

          <label class="form-label col-3"><span class="c-red">*</span>对外价格/半天：</label>
          <div class="formControls col-2">
            <input type="text" class="input-text required" placeholder="对外价格/半天" id="CROUTPriceHalfDay" data-valid="isNonEmpty||onlyNum" data-error="对外价格/半天不能为空||须为数字"/>
          </div>
          <div class="col-1"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>对内价格/全天：</label>
          <div class="formControls col-2">
            <input type="text" class="input-text required" placeholder="对内价格/全天" id="CRINPriceDay" data-valid="isNonEmpty||onlyNum" data-error="对内价格/全天不能为空||须为数字"/>
          </div>
        
          <label class="form-label col-3"><span class="c-red">*</span>对外价格/全天：</label>
          <div class="formControls col-2">
            <input type="text" class="input-text required" placeholder="对外价格/全天" id="CROUTPriceDay" data-valid="isNonEmpty||onlyNum" data-error="对外价格/全天不能为空||须为数字"/>
          </div>
          <div class="col-3"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>押金：</label>
          <div class="formControls col-2">
            <input type="text" class="input-text required" placeholder="押金" id="CRDeposit" data-valid="isNonEmpty||onlyNum" data-error="押金不能为空||须为数字"/>
          </div>        
          <div class="col-1"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2">位置：</label>
          <div class="formControls col-7">
            <textarea cols="" rows="3" class="textarea required" placeholder="位置" id="CRAddr" data-valid="between:0-300" data-error="位置长度为0-300位"></textarea>
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
        
    <div id="reservediv" style="display:none;">
        <table style="width:400px; margin-top:10px;">
            <tr>
                <td style="width:100px; text-align:center; padding:8px; height:40px;">会议室编号</td>
                <td style="width:300px;">
                    <input type="text" id="ReserveCRID" disabled="disabled" class="input-text size-MINI" style="width:200px;" />
                </td>
            </tr>
            <tr>
                <td style="width:100px; text-align:center; padding:8px; height:40px;">预留客户</td>
                <td style="width:300px;">
                    <input type="text" id="ReserveCustName" onblur="check('ReserveCust','Cust')" class="input-text size-MINI" style="width:200px;" />
                    <input style="display:none;" type="text" id="ReserveCust" />
                    <img id="ReserveCustImg" alt="" src="../../images/view_detail.png" class="view_detail" />
                </td>
            </tr>
            <tr>
                <td style="width:100px; text-align:center; padding:8px; height:40px;">预留开始日期</td>
                <td style="width:300px;"><input type="text" id="BegReserveDate" class="input-text size-MINI" style="width:200px;"/></td>
            </tr>
            <tr>
                <td style="width:100px; text-align:center; padding:8px; height:40px;">预留到期日期</td>
                <td style="width:300px;"><input type="text" id="EndReserveDate" class="input-text size-MINI" style="width:200px;"/></td>
            </tr>
        </table>
        <div style="margin:10px 30px;">
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
                    showMsg("CRNo", "此会议室编号已存在", "1");
                }
                else {
                    layer.msg("数据操作出错！");
                }
                return;
            }
            if (vjson.type == "update") {
                if (vjson.flag == "1") {
                    $("#CRNo").val(vjson.CRNo);
                    $("#CRName").val(vjson.CRName);
                    $("#CRCapacity").val(vjson.CRCapacity);
                    $("#CRINPriceHour").val(vjson.CRINPriceHour);
                    $("#CRINPriceHalfDay").val(vjson.CRINPriceHalfDay);
                    $("#CRINPriceDay").val(vjson.CRINPriceDay);
                    $("#CROUTPriceHour").val(vjson.CROUTPriceHour);
                    $("#CROUTPriceHalfDay").val(vjson.CROUTPriceHalfDay);
                    $("#CROUTPriceDay").val(vjson.CROUTPriceDay);
                    $("#CRDeposit").val(vjson.CRDeposit);
                    $("#CRAddr").val(vjson.CRAddr);

                    $("#CRNo").attr("disabled", true);
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
            $("#CRNo").attr("disabled", false);
            $("#CRNo").val("");
            $("#CRName").val("");
            $("#CRCapacity").val("");
            $("#CRINPriceHour").val("");
            $("#CRINPriceHalfDay").val("");
            $("#CRINPriceDay").val("");
            $("#CROUTPriceHour").val("");
            $("#CROUTPriceHalfDay").val("");
            $("#CROUTPriceDay").val("");
            $("#CRDeposit").val("");
            $("#CRAddr").val("");

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
                submitData.CRNo = $("#CRNo").val();
                submitData.CRName = $("#CRName").val();
                submitData.CRCapacity = $("#CRCapacity").val();
                submitData.CRINPriceHour = $("#CRINPriceHour").val();
                submitData.CRINPriceHalfDay = $("#CRINPriceHalfDay").val();
                submitData.CRINPriceDay = $("#CRINPriceDay").val();
                submitData.CROUTPriceHour = $("#CROUTPriceHour").val();
                submitData.CROUTPriceHalfDay = $("#CROUTPriceHalfDay").val();
                submitData.CROUTPriceDay = $("#CROUTPriceDay").val();
                submitData.CRDeposit = $("#CRDeposit").val();
                submitData.CRAddr = $("#CRAddr").val();

                submitData.tp = type;
                submitData.CRNoS = $("#CRNoS").val();
                submitData.CRNameS = $("#CRNameS").val();
                submitData.CRAddrS = $("#CRAddrS").val();
                submitData.CRStatusS = $("#CRStatusS").val();
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

                submitData.CRNoS = $("#CRNoS").val();
                submitData.CRNameS = $("#CRNameS").val();
                submitData.CRAddrS = $("#CRAddrS").val();
                submitData.CRStatusS = $("#CRStatusS").val();
                submitData.page = page;
                transmitData(datatostr(submitData));
                layer.close(index);
            });
            return;
        }
        function select() {
            var submitData = new Object();
            submitData.Type = "select";
            submitData.CRNoS = $("#CRNoS").val();
            submitData.CRNameS = $("#CRNameS").val();
            submitData.CRAddrS = $("#CRAddrS").val();
            submitData.CRStatusS = $("#CRStatusS").val();
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

                submitData.CRNoS = $("#CRNoS").val();
                submitData.CRNameS = $("#CRNameS").val();
                submitData.CRAddrS = $("#CRAddrS").val();
                submitData.CRStatusS = $("#CRStatusS").val();
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
            submitData.CRNoS = $("#CRNoS").val();
            submitData.CRNameS = $("#CRNameS").val();
            submitData.CRAddrS = $("#CRAddrS").val();
            submitData.CRStatusS = $("#CRStatusS").val();
            submitData.page = page;
            transmitData(datatostr(submitData));
            return;
        }

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
            $("#ReserveCRID").val(id);
            $("#ReserveCust").val("");
            $("#ReserveCustName").val("");
            $("#BegReserveDate").val("");
            $("#EndReserveDate").val("");
            layer.open({
                type: 1,
                area: ["400px", "280px"],
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

                submitData.CRNoS = $("#CRNoS").val();
                submitData.CRNameS = $("#CRNameS").val();
                submitData.CRAddrS = $("#CRAddrS").val();
                submitData.CRStatusS = $("#CRStatusS").val();
                submitData.page = page;
                transmitData(datatostr(submitData));
                layer.closeAll();
            });
            return;
        }

        function reservesubmit() {
            if ($("#ReserveCRID").val() == "") {
                layer.msg("未识别当前会议室编号，请刷新再试", { icon: 3, time: 1000 });
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
            submitData.id = $("#ReserveCRID").val();
            submitData.ReserveCust = $("#ReserveCust").val();
            submitData.BegReserveDate = $("#BegReserveDate").val();
            submitData.EndReserveDate = $("#EndReserveDate").val();

            submitData.CRNoS = $("#CRNoS").val();
            submitData.CRNameS = $("#CRNameS").val();
            submitData.CRAddrS = $("#CRAddrS").val();
            submitData.CRStatusS = $("#CRStatusS").val();
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
        },
        tiptype = "1");

        jQuery(function () {
            var BegReserveDate = new JsInputDate("BegReserveDate");
            var EndReserveDate = new JsInputDate("EndReserveDate");
        });

        var trid = "";
        reflist();
</script>
</body>
</html>