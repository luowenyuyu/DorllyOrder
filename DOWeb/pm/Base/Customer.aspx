<%@ Page Language="C#" AutoEventWireup="true" Inherits="project.Presentation.Base.Customer,project"  %>
<!DOCTYPE>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="head1" runat="server">
    <title>客户资料</title>
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
    <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 基础资料 <span class="c-gray en">&gt;</span> 客户资料 <a class="btn btn-success radius r mr-20" style="line-height:1.6em;margin-top:2px" href="javascript:location.replace(location.href);" title="刷新" ><i class="Hui-iconfont">&#xe68f;</i></a></nav>
    <div id="list" class="pt-5 pr-20 pb-5 pl-20">
	    <div class="cl pd-5 bg-1 bk-gray mt-2"> 
            <span class="l">
                <%--<a href="javascript:;" onclick="insert()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe600;</i> 添加</a>
                <a href="javascript:;" onclick="update()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe60c;</i> 修改</a> 
                <a href="javascript:;" onclick="del()" class="btn btn-danger radius"><i class="Hui-iconfont">&#xe6e2;</i> 删除</a>--%>
                <%=Buttons %>
                <input type="hidden" id="selectKey" />
            </span> 
	    </div>

	    <div class="cl pd-10  bk-gray mt-2"> 
		    客户编号&nbsp;<input type="text" class="input-text size-MINI" style="width:150px" placeholder="客户编号" id="CustNoS" />&nbsp;
            客户名称&nbsp;<input type="text" class="input-text size-MINI" style="width:150px" placeholder="客户名称" id="CustNameS" />&nbsp;
            客户类别&nbsp;<select class="input-text size-MINI" style="width:120px" id="CustTypeS">
                <option value="" selected>全部</option>
                <option value="企业">企业</option>
                <option value="个人">个人</option>
                </select>&nbsp;
            状态&nbsp;<select class="input-text size-MINI" style="width:120px" id="CustStatusS">
                <option value="" selected>全部</option>
                <option value="1">在租</option>
                <option value="2">退租</option>
                <option value="3">未租</option>
                </select>&nbsp;
		    <button type="submit" class="btn btn-success radius" onclick="select()"><i class="Hui-iconfont">&#xe665;</i> 查询</button>
	    </div>     
	    <div class="mt-5" id="outerlist">
	        <%=list %>
        </div>
    </div>
    <div id="edit" class="pt-5 pr-20 pb-5 pl-20" style="display:none;">
      <div class="form form-horizontal bk-gray mt-15 pb-10" id="editlist">
        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>客户编号：</label>
          <div class="formControls col-2">
            <input type="text" class="input-text required" placeholder="客户编号" id="CustNo" data-valid="isNonEmpty||between:1-16" data-error="客户编号不能为空||客户编号长度为1-16位" />
          </div>
          <label class="form-label col-3"><span class="c-red">*</span>客户名称：</label>
          <div class="formControls col-2">
            <input type="text" class="input-text required" placeholder="客户名称" id="CustName" data-valid="isNonEmpty||between:2-50" data-error="客户名称不能为空||客户名称长度为2-50位"/>
          </div>
          <div class="col-1"></div>
        </div>
    
        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>客户简称：</label>
          <div class="formControls col-2">
            <input type="text" class="input-text required" placeholder="客户简称" id="CustShortName" data-valid="isNonEmpty||between:1-30" data-error="客户简称不能为空||客户简称长度为1-30位"/>
          </div>       
             
          <label class="form-label col-3"><span class="c-red">*</span>客户类别：</label>
          <div class="formControls col-2">
            <select class="input-text required" id="CustType" data-valid="isNonEmpty" data-error="客户类别不能为空" >
                <option value="企业">企业</option>
                <option value="个人">个人</option>
            </select>
          </div>
          <div class="col-1"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2">单位法人：</label>
          <div class="formControls col-2">
            <input type="text" class="input-text required" placeholder="单位法人" id="Representative" data-valid="between:0-30" data-error="单位法人编号为0-30位"/>
          </div>
          
          <label class="form-label col-3">企业经营范围：</label>
          <div class="formControls col-2">
            <input type="text" class="input-text required"  placeholder="企业经营范围" id="BusinessScope" data-valid="between:0-30" data-error="企业经营范围为0-50位"/>
          </div>
          <div class="col-1"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2">营业执照编号：</label>
          <div class="formControls col-2">
            <input type="text" class="input-text required" placeholder="营业执照编号" id="CustLicenseNo" data-valid="between:0-30" data-error="营业执照编号为0-30位"/>
          </div>
      
          <label class="form-label col-3">法人身份证号：</label>
          <div class="formControls col-2">
            <input type="text" class="input-text required" placeholder="法人身份证号" id="RepIDCard" data-valid="between:0-30" data-error="法人身份证号为0-30位"/>
          </div>
          <div class="col-1"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2">客户联系人：</label>
          <div class="formControls col-2">
            <input type="text" class="input-text required" placeholder="客户联系人" id="CustContact" data-valid="between:0-30" data-error="客户联系人为0-30位"/>
          </div>
        
          <label class="form-label col-3">固定电话：</label>
          <div class="formControls col-2">
           <input type="text" class="input-text required" placeholder="固定电话" id="CustTel" data-valid="between:0-30" data-error="固定电话为0-30位"/>
          </div>
          <div class="col-1"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2">联系人手机号码：</label>
          <div class="formControls col-2">
           <input type="text" class="input-text required" placeholder="联系人手机号码" id="CustContactMobile" data-valid="isMobile" data-error="联系人手机号码不正确"/>
          </div>
       
          <label class="form-label col-3">电子邮箱：</label>
          <div class="formControls col-2">
            <input type="text" class="input-text required" placeholder="电子邮箱" id="CustEmail" data-valid="isEmail" data-error="邮箱格式不正确"/>
          </div>
          <div class="col-1"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2">收款人：</label>
          <div class="formControls col-2">
            <input type="text" class="input-text required" placeholder="收款人" id="CustBankTitle" data-valid="between:0-30" data-error="收款人为0-30位"/>
          </div>
        
          <label class="form-label col-3">收款账号：</label>
          <div class="formControls col-2">
            <input type="text" class="input-text required" placeholder="收款账号" id="CustBankAccount" data-valid="between:0-50" data-error="收款账号为0-50位"/>
          </div>
          <div class="col-1"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2">开户行：</label>
          <div class="formControls col-2">
            <input type="text" class="input-text required" placeholder="开户行" id="CustBank" data-valid="between:0-50" data-error="开户行为0-50位"/>
          </div>
        
          <label class="form-label col-3">是否外部客户：</label>
          <div class="formControls col-2">
            <select class="input-text required" id="IsExternal" data-valid="between:0-10" data-error="请选择是否外部客户">
                <option value="0">否</option>
                <option value="1">是</option>
            </select>
          </div>
          <div class="col-1"></div>
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
                    showMsg("CustNo", "此客户编号已存在", "1");
                }
                else {
                    layer.msg("数据操作出错！");
                }
                return;
            }
            if (vjson.type == "update") {
                if (vjson.flag == "1") {
                    $("#CustNo").val(vjson.CustNo);
                    $("#CustName").val(vjson.CustName);
                    $("#CustShortName").val(vjson.CustShortName);
                    $("#CustType").val(vjson.CustType);
                    $("#Representative").val(vjson.Representative);
                    $("#BusinessScope").val(vjson.BusinessScope);
                    $("#CustLicenseNo").val(vjson.CustLicenseNo);
                    $("#RepIDCard").val(vjson.RepIDCard);
                    $("#CustContact").val(vjson.CustContact);
                    $("#CustTel").val(vjson.CustTel);
                    $("#CustContactMobile").val(vjson.CustContactMobile);
                    $("#CustEmail").val(vjson.CustEmail);
                    $("#CustBankTitle").val(vjson.CustBankTitle);
                    $("#CustBankAccount").val(vjson.CustBankAccount);
                    $("#CustBank").val(vjson.CustBank);
                    $("#IsExternal").val(vjson.IsExternal);

                    $("#CustNo").attr("disabled", true);
                    $("#list").css("display", "none");
                    $("#edit").css("display", "");
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
            $("#CustNo").attr("disabled", false);
            $("#CustNo").val("");
            $("#CustName").val("");
            $("#CustShortName").val("");
            $("#CustType").val("");
            $("#Representative").val("");
            $("#BusinessScope").val("");
            $("#CustLicenseNo").val("");
            $("#RepIDCard").val("");
            $("#CustContact").val("");
            $("#CustTel").val("");
            $("#CustContactMobile").val("");
            $("#CustEmail").val("");
            $("#CustBankTitle").val("");
            $("#CustBankAccount").val("");
            $("#CustBank").val("");
            $("#IsExternal").val("0");

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
                submitData.CustNo = $("#CustNo").val();
                submitData.CustName = $("#CustName").val();
                submitData.CustShortName = $("#CustShortName").val();
                submitData.CustType = $("#CustType").val();
                submitData.Representative = $("#Representative").val();
                submitData.BusinessScope = $("#BusinessScope").val();
                submitData.CustLicenseNo = $("#CustLicenseNo").val();
                submitData.RepIDCard = $("#RepIDCard").val();
                submitData.CustContact = $("#CustContact").val();
                submitData.CustTel = $("#CustTel").val();
                submitData.CustContactMobile = $("#CustContactMobile").val();
                submitData.CustEmail = $("#CustEmail").val();
                submitData.CustBankTitle = $("#CustBankTitle").val();
                submitData.CustBankAccount = $("#CustBankAccount").val();
                submitData.CustBank = $("#CustBank").val();
                submitData.IsExternal = $("#IsExternal").val();

                submitData.tp = type;
                submitData.CustNoS = $("#CustNoS").val();
                submitData.CustNameS = $("#CustNameS").val();
                submitData.CustTypeS = $("#CustTypeS").val();
                submitData.CustStatusS = $("#CustStatusS").val();
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
                submitData.CustNoS = $("#CustNoS").val();
                submitData.CustNameS = $("#CustNameS").val();
                submitData.CustTypeS = $("#CustTypeS").val();
                submitData.CustStatusS = $("#CustStatusS").val();
                submitData.page = page;
                transmitData(datatostr(submitData));
                layer.close(index);
            });
            return;
        }
        function select() {
            var submitData = new Object();
            submitData.Type = "select";
            submitData.CustNoS = $("#CustNoS").val();
            submitData.CustNameS = $("#CustNameS").val();
            submitData.CustTypeS = $("#CustTypeS").val();
            submitData.CustStatusS = $("#CustStatusS").val();
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
            submitData.CustNoS = $("#CustNoS").val();
            submitData.CustNameS = $("#CustNameS").val();
            submitData.CustTypeS = $("#CustTypeS").val();
            submitData.CustStatusS = $("#CustStatusS").val();
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