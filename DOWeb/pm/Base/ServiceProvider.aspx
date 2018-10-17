<%@ Page Language="C#" AutoEventWireup="true" Inherits="project.Presentation.Base.ServiceProvider,project"  %>
<!DOCTYPE>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="head1" runat="server">
    <title>服务商资料</title>
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
    <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 基础资料 <span class="c-gray en">&gt;</span> 服务商资料 <a class="btn btn-success radius r mr-20" style="line-height:1.6em;margin-top:2px" href="javascript:location.replace(location.href);" title="刷新" ><i class="Hui-iconfont">&#xe68f;</i></a></nav>
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
	    <div class="cl pd-10  bk-gray mt-2"> 
		    服务商名称&nbsp;<input type="text" class="input-text size-MINI" style="width:150px" placeholder="服务商名称" id="SPNameS" name="">&nbsp;
		    <button type="submit" class="btn btn-success radius" onclick="select()"><i class="Hui-iconfont">&#xe665;</i> 检索</button>
	    </div>
	    <div class="mt-5" id="outerlist">
	    <%=list %>
	    </div>
    </div>
    <div id="edit" class="pt-5 pr-20 pb-5 pl-20" style="display:none;">
      <div class="form form-horizontal bk-gray mt-15 pb-10" id="editlist">
        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>服务商编号：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text required" placeholder="服务商编号" id="SPNo" data-valid="isNonEmpty||between:1-16" data-error="服务商编号不能为空||服务商编号长度为1-16位" />
          </div>
            <div class="col-3"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>服务商名称：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text required" placeholder="服务商名称" id="SPName" data-valid="isNonEmpty||between:2-80" data-error="服务商名称不能为空||服务商名称长度为2-80位"/>
          </div>
          <div class="col-3"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2"><span class="c-red">*</span>服务商简称：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text required" placeholder="服务商简称" id="SPShortName" data-valid="isNonEmpty||between:1-50" data-error="服务商简称不能为空||服务商简称长度为0-30位"/>
          </div>
          <div class="col-3"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2">主营服务：</label>
          <div class="formControls col-4">
            <%=ServiceTypeStr %>
            <%--<input type="text" class="input-text required" placeholder="主营服务" id="MService" data-valid="between:0-30" data-error="主营服务为0-30位"/>--%>
          </div>
          <div class="col-3"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2">营业执照编号：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text required" placeholder="营业执照编号" id="SPLicenseNo" data-valid="between:0-30" data-error="营业执照编号为0-30位"/>
          </div>
          <div class="col-3"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2">联系人：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text required" placeholder="联系人" id="SPContact" data-valid="between:0-30" data-error="联系人为0-30位"/>
          </div>
          <div class="col-3"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2">手机：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text required" placeholder="手机" id="SPContactMobile" data-valid="between:0-30" data-error="手机为0-30位"/>
          </div>
          <div class="col-3"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2">固定电话：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text required" placeholder="固定电话" id="SPTel" data-valid="between:0-30" data-error="固定电话为0-30位"/>
          </div>
          <div class="col-3"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2">邮箱：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text required" placeholder="邮箱" id="SPEMail" data-valid="between:0-30" data-error="邮箱为0-30位"/>
          </div>
          <div class="col-3"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2">地址：</label>
          <div class="formControls col-5">
            <textarea cols="" rows="" class="textarea required" placeholder="地址" id="SPAddr" data-valid="between:0-300" data-error="地址长度为0-300位"></textarea>
          </div>
          <div class="col-2"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2">开户行：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text required" placeholder="开户行" id="SPBank" data-valid="between:0-30" data-error="开户行为0-30位"/>
          </div>
          <div class="col-3"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2">开户账户：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text required" placeholder="开户账户" id="SPBankAccount" data-valid="between:0-30" data-error="开户账户为0-30位"/>
          </div>
          <div class="col-3"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2">收款人：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text required" placeholder="收款人" id="SPBankTitle" data-valid="between:0-30" data-error="收款人为0-30位"/>
          </div>
          <div class="col-3"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2">U8账套：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text required" placeholder="U8账套" id="U8Account" data-valid="between:1-30" data-error="U8账套为1-30位"/>
          </div>
          <div class="col-3"></div>
        </div>
<%--        <div class="row cl">
          <label class="form-label col-2">U8银行科目：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text required" placeholder="U8银行科目" id="BankAccount" data-valid="between:1-30" data-error="U8银行科目为1-30位"/>
          </div>
          <div class="col-3"></div>
        </div>--%>
        <div class="row cl">
          <label class="form-label col-2">U8现金科目：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text required" placeholder="U8现金科目" id="CashAccount" data-valid="between:1-30" data-error="U8现金科目为1-30位"/>
          </div>
          <div class="col-3"></div>
        </div>
        <div class="row cl">
          <label class="form-label col-2">U8税额科目：</label>
          <div class="formControls col-4">
            <input type="text" class="input-text required" placeholder="U8税额科目" id="TaxAccount" data-valid="between:1-30" data-error="U8税额科目为1-30位"/>
          </div>
          <div class="col-3"></div>
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
    <script type="text/javascript" src="../../jscript/script_ajax.js"></script>
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
                    showMsg("WorkerNo", "此服务商编号已存在", "1");
                }
                else {
                    layer.msg("数据操作出错！");
                }
                return;
            }
            if (vjson.type == "update") {
                if (vjson.flag == "1") {
                    $("#SPNo").val(vjson.SPNo);
                    $("#SPName").val(vjson.SPName);
                    $("#SPShortName").val(vjson.SPShortName);
                    $("#MService").val(vjson.MService);
                    $("#SPLicenseNo").val(vjson.SPLicenseNo);
                    $("#SPContact").val(vjson.SPContact);
                    $("#SPContactMobile").val(vjson.SPContactMobile);
                    $("#SPTel").val(vjson.SPTel);
                    $("#SPEMail").val(vjson.SPEMail);
                    $("#SPAddr").val(vjson.SPAddr);
                    $("#SPBank").val(vjson.SPBank);
                    $("#SPBankAccount").val(vjson.SPBankAccount);
                    $("#SPBankTitle").val(vjson.SPBankTitle);
                    $("#U8Account").val(vjson.U8Account);
                    //$("#BankAccount").val(vjson.BankAccount);
                    $("#CashAccount").val(vjson.CashAccount);
                    $("#TaxAccount").val(vjson.TaxAccount);

                    $("#SPNo").attr("disabled", true);
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
        }

        function insert() {
            $('#editlist').validate('reset');
            id = "";
            type = "insert";
            $("#SPNo").attr("disabled", false);
            $("#SPNo").val("");
            $("#SPName").val("");
            $("#SPShortName").val("");
            $("#MService").val("");
            $("#SPLicenseNo").val("");
            $("#SPContact").val("");
            $("#SPContactMobile").val("");
            $("#SPTel").val("");
            $("#SPEMail").val("");
            $("#SPAddr").val("");
            $("#SPBank").val("");
            $("#SPBankAccount").val("");
            $("#SPBankTitle").val("");
            $("#U8Account").val("");
            //$("#BankAccount").val("");
            $("#CashAccount").val("");
            $("#TaxAccount").val("");

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
                submitData.SPNo = $("#SPNo").val();
                submitData.SPName = $("#SPName").val();
                submitData.SPShortName = $("#SPShortName").val();
                submitData.MService = $("#MService").val();
                submitData.SPLicenseNo = $("#SPLicenseNo").val();
                submitData.SPContact = $("#SPContact").val();
                submitData.SPContactMobile = $("#SPContactMobile").val();
                submitData.SPTel = $("#SPTel").val();
                submitData.SPEMail = $("#SPEMail").val();
                submitData.SPAddr = $("#SPAddr").val();
                submitData.SPBank = $("#SPBank").val();
                submitData.SPBankAccount = $("#SPBankAccount").val();
                submitData.SPBankTitle = $("#SPBankTitle").val();
                submitData.U8Account = $("#U8Account").val();
                //submitData.BankAccount = $("#BankAccount").val();
                submitData.CashAccount = $("#CashAccount").val();
                submitData.TaxAccount = $("#TaxAccount").val();

                submitData.tp = type;
                submitData.SPNameS = $("#SPNameS").val();
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
                submitData.SPNameS = $("#SPNameS").val();
                submitData.page = page;
                transmitData(datatostr(submitData));
                layer.close(index);
            });
            return;
        }
        function select() {
            var submitData = new Object();
            submitData.Type = "select";
            submitData.SPNameS = $("#SPNameS").val();
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
        var page = 1;
        function jump(pageindex) {
            page = pageindex;
            var submitData = new Object();
            submitData.Type = "jump";
            submitData.SPNameS = $("#SPNameS").val();
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