<%@ Page Language="C#" AutoEventWireup="true" Inherits="project.Presentation.Base.Billboard,project"  %>
<!DOCTYPE>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="head1" runat="server">
    <title>广告位资料</title>
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
   <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 基础资料 <span class="c-gray en">&gt;</span>广告位资料 <a class="btn btn-success radius r mr-20" style="line-height:1.6em;margin-top:2px" href="javascript:location.replace(location.href);" title="刷新" ><i class="Hui-iconfont">&#xe68f;</i></a></nav>
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
		    广告位类型&nbsp;<%=BBTypeStrS %>&nbsp;
		    广告位编号&nbsp;<input type="text" class="input-text size-MINI ml-5 mr-10" style="width:120px" placeholder="广告位编号" id="BBNoS" />&nbsp;
		    广告位名称&nbsp;<input type="text" class="input-text size-MINI ml-5 mr-10" style="width:120px" placeholder="广告位名称" id="BBNameS" />&nbsp;
            服务商&nbsp;<%=BBSPNoStrS %>&nbsp;
            <br />
		    所属园区&nbsp;<%=BBLOCNoStrS %>&nbsp;
		    位置&nbsp;<input type="text" class="input-text size-MINI ml-5 mr-10" style="width:120px" placeholder="位置" id="BBAddrS" />&nbsp;
		    状态&nbsp;<select class="input-text size-MINI ml-5 mr-10" style="width:100px" id="BBStatusS">
                <option value="" selected>全部</option>
                <option value="free">空闲</option>
                <option value="use">占用</option>
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
          <label class="form-label col-2"><span class="c-red">*</span>广告位编号：</label>
          <div class="formControls col-2">
            <input type="text" class="input-text required" placeholder="广告位编号" id="BBNo" data-valid="isNonEmpty||between:1-30" data-error="广告位编号不能为空||广告位编号长度为1-30位" />
          </div>
          <label class="form-label col-3"><span class="c-red">*</span>广告位名称：</label>
          <div class="formControls col-2">
            <input type="text" class="input-text required" placeholder="广告位名称" id="BBName" data-valid="isNonEmpty||between:2-50" data-error="广告位名称不能为空||广告位名称长度为2-50位"/>
          </div>
          <div class="col-3"></div>
        </div>
        <div class="row cl">         
          <label class="form-label col-2"><span class="c-red">*</span>广告位类型：</label>
          <div class="formControls col-2">
            <%=BBTypeStr %>
          </div>
          <label class="form-label col-3"><span class="c-red">*</span>所在园区：</label>
          <div class="formControls col-2">
            <%=BBLOCNoStr %>
          </div>
          <div class="col-3"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>服务商：</label>
          <div class="formControls col-2">
            <%=BBSPNoStr %>
          </div>       
          <label class="form-label col-3">规格：</label>
          <div class="formControls col-2">
            <input type="text" class="input-text required" placeholder="规格" id="BBSize" data-valid="between:0-30" data-error="规格长度为0-30位"/>
          </div>
          <div class="col-1"></div>
        </div>
        <div class="row cl">        
          <label class="form-label col-2"><span class="c-red">*</span>对内价格/天：</label>
          <div class="formControls col-2">
            <input type="text" class="input-text required" placeholder="对内价格/天" id="BBINPriceDay" data-valid="isNonEmpty||onlyNum" data-error="对内价格/天不能为空||须为数字"/>
          </div>
          <label class="form-label col-3"><span class="c-red">*</span>对外价格/天：</label>
          <div class="formControls col-2">
            <input type="text" class="input-text required" placeholder="对外价格/天" id="BBOUTPriceDay" data-valid="isNonEmpty||onlyNum" data-error="对外价格/天不能为空||须为数字"/>
          </div>
          <div class="col-1"></div>
        </div>
        <div class="row cl">        
          <label class="form-label col-2"><span class="c-red">*</span>对内价格/月：</label>
          <div class="formControls col-2">
            <input type="text" class="input-text required" placeholder="对内价格/月" id="BBINPriceMonth" data-valid="isNonEmpty||onlyNum" data-error="对内价格/月不能为空||须为数字"/>
          </div>
          <label class="form-label col-3"><span class="c-red">*</span>对外价格/月：</label>
          <div class="formControls col-2">
            <input type="text" class="input-text required" placeholder="对外价格/月" id="BBOUTPriceMonth" data-valid="isNonEmpty||onlyNum" data-error="对外价格/月不能为空||须为数字"/>
          </div>
          <div class="col-1"></div>
        </div>
        <div class="row cl">        
          <label class="form-label col-2"><span class="c-red">*</span>对内价格/季度：</label>
          <div class="formControls col-2">
            <input type="text" class="input-text required" placeholder="对内价格/季度" id="BBINPriceQuarter" data-valid="isNonEmpty||onlyNum" data-error="对内价格/季度不能为空||须为数字"/>
          </div>
          <label class="form-label col-3"><span class="c-red">*</span>对外价格/季度：</label>
          <div class="formControls col-2">
            <input type="text" class="input-text required" placeholder="对外价格/季度" id="BBOUTPriceQuarter" data-valid="isNonEmpty||onlyNum" data-error="对外价格/季度不能为空||须为数字"/>
          </div>
          <div class="col-1"></div>
        </div>
        <div class="row cl">        
          <label class="form-label col-2"><span class="c-red">*</span>对内价格/年：</label>
          <div class="formControls col-2">
            <input type="text" class="input-text required" placeholder="对内价格/年" id="BBINPriceYear" data-valid="isNonEmpty||onlyNum" data-error="对内价格/年不能为空||须为数字"/>
          </div>
          <label class="form-label col-3"><span class="c-red">*</span>对外价格/年：</label>
          <div class="formControls col-2">
            <input type="text" class="input-text required" placeholder="对外价格/年" id="BBOUTPriceYear" data-valid="isNonEmpty||onlyNum" data-error="对外价格/年不能为空||须为数字"/>
          </div>
          <div class="col-1"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>押金：</label>
          <div class="formControls col-2">
            <input type="text" class="input-text required" placeholder="押金" id="BBDeposit" data-valid="isNonEmpty||onlyNum" data-error="押金不能为空||须为数字"/>
          </div>        
          <div class="col-1"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2">位置：</label>
          <div class="formControls col-7">
            <textarea cols="" rows="3" class="textarea required" placeholder="位置" id="BBAddr" data-valid="between:0-300" data-error="位置长度为0-300位"></textarea>
          </div>
          <div class="col-1"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2">图片：</label>
          <div class="formControls col-7">
            <img id="BBImageImg" src="" width="120" height="120" alt="" title="" />
            <input type="hidden" id="BBImage" /><br /><br />
            <input type="button" class="btn btn-primary radius" value="上传图像" id="uploadPic" onclick="showUpload('BBImage')" />
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
                    showMsg("BBNo", "此广告位编号已存在", "1");
                }
                else {
                    layer.msg("数据操作出错！");
                }
                return;
            }
            if (vjson.type == "update") {
                if (vjson.flag == "1") {
                    $("#BBNo").val(vjson.BBNo);
                    $("#BBName").val(vjson.BBName);
                    $("#BBLOCNo").val(vjson.BBLOCNo);
                    $("#BBSPNo").val(vjson.BBSPNo);
                    $("#BBAddr").val(vjson.BBAddr);
                    $("#BBSize").val(vjson.BBSize);
                    $("#BBType").val(vjson.BBType);
                    $("#BBINPriceDay").val(vjson.BBINPriceDay);
                    $("#BBOUTPriceDay").val(vjson.BBOUTPriceDay);
                    $("#BBINPriceMonth").val(vjson.BBINPriceMonth);
                    $("#BBOUTPriceMonth").val(vjson.BBOUTPriceMonth);
                    $("#BBINPriceQuarter").val(vjson.BBINPriceQuarter);
                    $("#BBOUTPriceQuarter").val(vjson.BBOUTPriceQuarter);
                    $("#BBINPriceYear").val(vjson.BBINPriceYear);
                    $("#BBOUTPriceYear").val(vjson.BBOUTPriceYear);
                    $("#BBDeposit").val(vjson.BBDeposit);
                    $("#BBImage").val(vjson.BBImage);
                    $("#BBImageImg").attr("src","../../upload/"+vjson.BBImage);

                    $("#BBNo").attr("disabled", true);
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
        }

        function insert() {
            $('#editlist').validate('reset');
            id = "";
            type = "insert";
            $("#BBNo").attr("disabled", false);
            $("#BBNo").val("");
            $("#BBName").val("");
            $("#BBLOCNo").val("");
            $("#BBSPNo").val("");
            $("#BBAddr").val("");
            $("#BBSize").val("");
            $("#BBType").val("");
            $("#BBINPriceDay").val("");
            $("#BBOUTPriceDay").val("");
            $("#BBINPriceMonth").val("");
            $("#BBOUTPriceMonth").val("");
            $("#BBINPriceQuarter").val("");
            $("#BBOUTPriceQuarter").val("");
            $("#BBINPriceYear").val("");
            $("#BBOUTPriceYear").val("");
            $("#BBDeposit").val("");
            $("#BBImage").val("");
            $("#BBImageImg").attr("src", "");

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
                submitData.BBNo = $("#BBNo").val();
                submitData.BBName = $("#BBName").val();
                submitData.BBLOCNo = $("#BBLOCNo").val();
                submitData.BBSPNo = $("#BBSPNo").val();
                submitData.BBAddr = $("#BBAddr").val();
                submitData.BBSize = $("#BBSize").val();
                submitData.BBType = $("#BBType").val();
                submitData.BBINPriceDay = $("#BBINPriceDay").val();
                submitData.BBOUTPriceDay = $("#BBOUTPriceDay").val();
                submitData.BBINPriceMonth = $("#BBINPriceMonth").val();
                submitData.BBOUTPriceMonth = $("#BBOUTPriceMonth").val();
                submitData.BBINPriceQuarter = $("#BBINPriceQuarter").val();
                submitData.BBOUTPriceQuarter = $("#BBOUTPriceQuarter").val();
                submitData.BBINPriceYear = $("#BBINPriceYear").val();
                submitData.BBOUTPriceYear = $("#BBOUTPriceYear").val();
                submitData.BBDeposit = $("#BBDeposit").val();
                submitData.BBImage = $("#BBImage").val();

                submitData.tp = type;
                submitData.BBNoS = $("#BBNoS").val();
                submitData.BBNameS = $("#BBNameS").val();
                submitData.BBAddrS = $("#BBAddrS").val();
                submitData.BBStatusS = $("#BBStatusS").val();
                submitData.BBTypeS = $("#BBTypeS").val();
                submitData.BBSPNoS = $("#BBSPNoS").val();
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

                submitData.BBNoS = $("#BBNoS").val();
                submitData.BBNameS = $("#BBNameS").val();
                submitData.BBAddrS = $("#BBAddrS").val();
                submitData.BBStatusS = $("#BBStatusS").val();
                submitData.BBTypeS = $("#BBTypeS").val();
                submitData.BBSPNoS = $("#BBSPNoS").val();
                submitData.page = page;
                transmitData(datatostr(submitData));
                layer.close(index);
            });
            return;
        }
        function select() {
            var submitData = new Object();
            submitData.Type = "select";
            submitData.BBNoS = $("#BBNoS").val();
            submitData.BBNameS = $("#BBNameS").val();
            submitData.BBAddrS = $("#BBAddrS").val();
            submitData.BBStatusS = $("#BBStatusS").val();
            submitData.BBTypeS = $("#BBTypeS").val();
            submitData.BBSPNoS = $("#BBSPNoS").val();
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

                submitData.BBNoS = $("#BBNoS").val();
                submitData.BBNameS = $("#BBNameS").val();
                submitData.BBAddrS = $("#BBAddrS").val();
                submitData.BBStatusS = $("#BBStatusS").val();
                submitData.BBTypeS = $("#BBTypeS").val();
                submitData.BBSPNoS = $("#BBSPNoS").val();
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
            submitData.BBNoS = $("#BBNoS").val();
            submitData.BBNameS = $("#BBNameS").val();
            submitData.BBAddrS = $("#BBAddrS").val();
            submitData.BBStatusS = $("#BBStatusS").val();
            submitData.BBTypeS = $("#BBTypeS").val();
            submitData.BBSPNoS = $("#BBSPNoS").val();
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
        },
        tiptype = "1");


        function showUpload(id) {
            var temp = "Upload.html?action=uploadpic&id=" + id;
            layer_show("文件上传", temp, 400, 200);
        }

        function showPic(name, id) {
            $("#" + id + "Img").attr("src", "../../upload/" + name);
            $("#" + id).val(name);
            layer.closeAll();
        }

        //jQuery(function () {
        //    $("a[rel=timage]").fancybox({
        //        'transitionIn': 'none',
        //        'transitionOut': 'none',
        //        'titlePosition': 'over',
        //        'showRotateButton': true,
        //        'showCloseButton': true,
        //        'titleFormat': function (title, currentArray, currentIndex, currentOpts) {
        //            return '<span id="fancybox-title-over">' + (currentIndex + 1) + ' / ' + currentArray.length + (title.length ? ' &nbsp; ' + title : '') + '</span>';
        //        }
        //    });
        //});
        
        var trid = "";
        reflist();
</script>
</body>
</html>