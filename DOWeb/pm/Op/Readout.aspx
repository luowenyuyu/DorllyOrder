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
    <style>
        /*房间编号选择按钮样式：背景为搜索图标*/
        #chooseMeterNo {
            background-image: url(../../images/search.ico);
            background-repeat: no-repeat;
            background-position: center;
            position: absolute;
            top: 2px;
            right: 11px;
            height: 27px;
            width: 30px;
            border: none;
            border-left: solid 1px;
        }
        /*列表图片放大或缩小样式：放大后单个图片容器DIV单独设置宽高，缩小后容器DIV统一使用本样式*/
        .readoutImg {
            width: 85px;
            height: 50px;
            cursor: pointer;
        }

            .readoutImg img {
                width: 100%;
                height: 100%;
            }

        /*添加或编辑DIV容器样式设置：针对抄表图片而新更改样式*/
        #edit {
            /*容器设置*/
            min-width: 800px;
            padding: 15px;
            margin: 10px;
            margin-left: 20px;
        }


            /*输入框区域全局设置*/
            #edit .edit-data > .row {
                margin-bottom: 12px;
            }
            /*图片区域全局设置*/
            #edit .edit-img div {
                width: 100%;
                height: 100%;
                position: relative;
            }

            #edit .edit-img .showdiv {
            }

                #edit .edit-img .showdiv img {
                    width: 100%;
                    height: 100%;
                    /*cursor: pointer;*/
                }

                #edit .edit-img .showdiv .delbtn {
                    position: absolute;
                    right: 1px;
                    top: 1px;
                    /*padding: 0px 2px;*/
                    color: red;
                    background-color: #d6e1e3;
                    cursor: pointer;
                    width: 20px;
                    height: 23px;
                    text-align: center;
                    font-size: 15px;
                    font-weight: bold;
                }

            #edit .edit-img .adddiv {
                border: dashed 1px black;
            }

                #edit .edit-img .adddiv .addlabel {
                    color: #969393de;
                    font-size: 100px;
                    cursor: pointer;
                    position: absolute;
                    top: 50%;
                    left: 50%;
                    margin: -90px 0 0 -50px;
                }
            /*提交按钮区域全局设置*/
            #edit .edit-submit {
                padding-top: 20px;
            }

                #edit .edit-submit > div {
                    text-align: center;
                }

                    #edit .edit-submit > div input {
                        height: 40px;
                        border-radius: 10px;
                        font-size: 17px;
                        letter-spacing: 3px;
                        margin-left: 10px;
                    }

        @media screen and (max-width:960px) {
            #edit .edit-data {
                width: 100%;
                clear: right;
            }

            #edit .edit-img {
                width: 100%;
                height: 500px;
            }
            /*文本框较为居中显示*/
            #edit .edit-data > .row > .form-label {
                width: 25%;
            }
        }

        @media screen and (min-width:960px) {
            #edit .edit-data {
                width: 34%;
                float: left;
            }

            #edit .edit-img {
                width: 66%;
                height: 400px;
                float: left;
            }
        }
        /*
         *   查询条件区域调整
         */
        .condition {
            padding: 6px;
        }

            .condition .row {
                margin: 0px;
            }

                .condition .row:not(:first-child) {
                    margin-top: 8px;
                }
            /*下拉框调整*/
            .condition .select-input {
                width: 9%;
            }
            /*标题调整*/
            .condition .form-label {
                padding-right: 1px;
                width: 7%;
                height: 27px;
                margin: 0px;
                line-height: 27px;
            }
            /*文本框调整*/
            .condition .formControls {
                padding-right: 5px;
            }
        /*树形菜单样式微调*/
        .ztreeDiv {
            width: 200px;
            left: 15px;
            top: 40px;
            bottom: 0;
            border-right: 1px solid #e5e5e5;
            background-color: #fcfcfe;
            overflow: auto;
        }
    </style>
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

    <div class="pos-a ztreeDiv">
        <ul id="ztree" class="ztree"></ul>
        <div class="ztree-loading" style="text-align:center;margin-top:300px;">
            <img src="../../images/loading.gif" style="width:19px;"/>&nbsp;&nbsp;加载中&nbsp;.&nbsp;.&nbsp;.
        </div>
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
            <div class="cl pd-6  bk-gray mt-2 form form-horizontal condition">
                <div class="row">
                    <div class="form-label col-1">楼栋：</div>
                    <div class="formControls col-1 select-input">
                        <select class="input-text required size-MINI" id="Loc3">
                            <option value="" selected>全部</option>
                            <%=loc3List %>
                        </select>
                    </div>
                    <div class="form-label col-1">楼层：</div>
                    <div class="formControls col-1 select-input">
                        <select class="input-text required size-MINI" id="Loc4">
                            <option value="" selected>全部</option>
                        </select>
                        <div class="loading-select" style="display: none">
                            <img src="../../images/loading.gif" />
                        </div>
                    </div>
                    <div class="form-label col-1">房间编号：</div>
                    <div class="formControls col-2">
                        <input type="text" class="input-text size-MINI" placeholder="房间编号" id="RMIDS" />
                    </div>
                    <div class="form-label col-1">表记编号：</div>
                    <div class="formControls col-2">
                        <input type="text" class="input-text size-MINI" placeholder="表记编号" id="MeterNoS" />
                    </div>
                </div>
                <div class="row">
                    <div class="form-label col-1">抄表类型：</div>
                    <div class="formControls col-1 select-input">
                        <select class="input-text required size-MINI" id="ReadoutTypeS">
                            <option value="" selected>全部</option>
                            <option value="1">正常抄表</option>
                            <option value="2">临时抄表</option>
                            <option value="0">租前抄表</option>
                        </select>
                    </div>
                    <div class="form-label col-1">表记类型：</div>
                    <div class="formControls col-1 select-input">
                        <select class="input-text required size-MINI" id="MeterTypeS">
                            <option value="" selected>全部</option>
                            <option value="wm">水表</option>
                            <option value="am">电表</option>
                        </select>
                    </div>
                    <div class="form-label col-1">抄表状态：</div>
                    <div class="formControls col-1 select-input">
                        <select class="input-text size-MINI" id="AuditStatusS">
                            <option value="" selected>全部</option>
                            <option value="0">待审核</option>
                            <option value="1">审核通过</option>
                            <option value="-1">审核不通过</option>
                        </select>
                    </div>
                    <div class="form-label col-1">抄表日期：</div>
                    <div class="formControls col-3">
                        <input type="text" class="input-text size-MINI" id="MinRODate" style="width: 45%" placeholder="开始日期" />
                        ~
                        <input type="text" class="input-text size-MINI" id="MaxRODate" style="width: 45%" placeholder="结束日期" />
                    </div>
                    <div class="formControls col-1">
                        <button type="submit" class="btn btn-success radius" onclick="select()"><i class="Hui-iconfont">&#xe665;</i> 查询</button>
                    </div>
                </div>

                <%--                房间编号&nbsp;<input type="text" class="input-text size-MINI" style="width: 150px" placeholder="房间编号" id="RMIDS" />&nbsp;
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
		    <button type="submit" class="btn btn-success radius" onclick="select()"><i class="Hui-iconfont">&#xe665;</i> 查询</button>--%>
            </div>
            <div class="mt-5" id="outerlist">
                <%=list %>
            </div>
        </div>
        <div id="edit" class="form form-horizontal bk-gray pt-10 pb-10 pl-20 pr-20" style="display: none;">
            <div class="edit-data">
                <div class="row cl">
                    <label class="form-label col-4"><span class="c-red">*</span>表记编号：</label>
                    <div class="formControls col-8">
                        <input type="text" class="input-text required"
                            placeholder="表记编号" id="MeterNo" data-valid="isNonEmpty" data-error="请选择原表记编号" />
                        <button type="button" class="btn" id="chooseMeterNo"></button>
                        <input type="hidden" id="MeterDigit" />
                    </div>
                </div>
                <div class="row cl">
                    <label class="form-label col-4"><span class="c-red">*</span>房间编号：</label>
                    <div class="formControls col-8">
                        <input type="text" class="input-text required" placeholder="房间编号" id="RMID" disabled="disabled" data-valid="between:0-30" data-error="" />
                    </div>
                </div>
                <div class="row cl">
                    <label class="form-label col-4"><span class="c-red">*</span>上期读数：</label>
                    <div class="formControls col-8">
                        <input type="text" class="input-text required" placeholder="上期读数" disabled="disabled" id="LastReadout" data-valid="onlyNum" data-error="未读取上期读数" />
                    </div>
                </div>
                <div class="row cl">
                    <label class="form-label col-4"><span class="c-red">*</span>本期读数：</label>
                    <div class="formControls col-8">
                        <input type="text" class="input-text required" placeholder="本期读数" id="Readout" data-valid="onlyNum" data-error="需是数字且不能小于0" />
                    </div>
                </div>
                <div class="row cl">
                    <label class="form-label col-4"><span class="c-red">*</span>关联表记读数：</label>
                    <div class="formControls col-8">
                        <input type="text" class="input-text required" placeholder="关联表记读数" id="JoinReadings" data-valid="onlyNum" data-error="需是数字且不能小于0" />
                    </div>
                </div>
                <div class="row cl" style="display: none;">
                    <label class="form-label col-4"><span class="c-red">*</span>倍率：</label>
                    <div class="formControls col-8">
                        <input type="text" class="input-text required" placeholder="倍率" id="MeteRate" data-valid="onlyNum" data-error="需是数字" />
                    </div>
                </div>
                <div class="row cl">
                    <label class="form-label col-4"><span class="c-red">*</span>行度：</label>
                    <div class="formControls col-8">
                        <input type="text" class="input-text required" placeholder="行度" disabled="disabled" id="Readings" data-valid="onlyNum" data-error="需是数字且不能小于0" />
                    </div>
                </div>
                <div class="row cl">
                    <label class="form-label col-4"><span class="c-red">*</span>抄表类型：</label>
                    <div class="formControls col-8">
                        <select class="input-text required" id="ReadoutType" data-valid="isNonEmpty" data-error="请选择抄表类型">
                            <option value="">请选择抄表类型</option>
                            <option value="1" selected>正常抄表</option>
                            <option value="2">临时抄表</option>
                            <option value="0">租前抄表</option>
                        </select>
                    </div>
                </div>
                <div class="row cl">
                    <label class="form-label col-4"><span class="c-red">*</span>抄表人：</label>
                    <div class="formControls col-8">
                        <%=ROOperatorStr %>
                        <%--<input type="text" class="input-text required" placeholder="抄表人" id="ROOperator" data-valid="isNonEmpty||between:1-30" data-error="请填写抄表人||抄表人长度为1-30位"/>--%>
                    </div>
                </div>
                <div class="row cl">
                    <label class="form-label col-4"><span class="c-red">*</span>抄表日期：</label>
                    <div class="formControls col-8">
                        <input type="text" class="input-text required" placeholder="抄表日期" id="RODate" data-valid="isNonEmpty" data-error="请选择抄表日期" />
                    </div>
                </div>
                <div style="display: none;">
                    <label class="form-label col-4"><span class="c-red"></span>抄表图片：</label>
                    <div class="formControls col-8">
                        <input type="hidden" id="Img" value="" />
                    </div>
                </div>
            </div>
            <div class="edit-img">
                <form action="ReadouImg.ashx" method="post" id="imgForm" enctype="multipart/form-data">
                    <div class="showdiv bk-gray" style="display: none;">
                        <img src="#" onclick="$('#addbtn').trigger('click');" />
                        <i class="Hui-iconfont Hui-iconfont-close delbtn" onclick="delImg()"></i>
                    </div>
                    <div class="adddiv">
                        <input id="addbtn" type="file" name="imgfile" accept="image/*" style="display: none;" onchange="addImg(this)" />
                        <label class="Hui-iconfont Hui-iconfont-add addlabel" for="addbtn"></label>
                    </div>
                </form>

            </div>
            <div class="row edit-submit">
                <div>
                    <input class="btn btn-primary radius" type="button" onclick="submit()" value="提交" />
                    <input class="btn btn-primary radius" type="button" onclick="submit1()" value="提交继续录入" />
                    <input class="btn btn-default radius" type="button" onclick="cancel()" value="取消" />
                </div>
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
    <%--<iframe id="openfile" width="0" height="0" marginwidth="0" frameborder="0" src="about:blank" content-disposition="attachment"></iframe>--%>
    <iframe id="openfile" width="0" height="0" src="about:blank"></iframe>
    <div id="imglist">
    </div>
    <script type="text/javascript" src="../../jscript/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="../../jscript/script_common.js"></script>
    <script type="text/javascript" src="../../jscript/json2.js"></script>
    <script type="text/javascript" src="../../jscript/H-ui.js"></script>
    <script type="text/javascript" src="../../jscript/H-ui.admin.js"></script>
    <script type="text/javascript" src="../../jscript/jquery-form.js"></script>
    <script type="text/javascript" src="../../lib/layer/layer.js"></script>
    <script type="text/javascript" src="../../lib/validate/jquery.validate.js"></script>
    <script type="text/javascript" src="../../jscript/JsInputDate.js"></script>
    <%--<script type="text/javascript" src="../../jscript/e-smart-zoom-jquery.min.js"></script>--%>
    <script type="text/javascript" src="../../lib/zTree/js/jquery.ztree.all-3.5.min.js"></script>
    <%--<script type="text/javascript" src="../../lib/zTree/js/jquery.ztree.exhide-3.5.js"></script>--%>
    <script type="text/javascript">
        jQuery(function () {
            var RODate = new JsInputDate("RODate");
            var MinRODate = new JsInputDate("MinRODate");
            var MaxRODate = new JsInputDate("MaxRODate");
            //列表图片点击放大
            $(".readoutImg").click(function () {
                showImg($(this).attr("id"));
            });
            //楼栋变化获取楼层信息
            $("#Loc3").change(function () {
                expandNode($(this).val());
                var submitData = new Object();
                submitData.Type = "getLoc4";
                submitData.Loc3No = $(this).val();
                transmitData(datatostr(submitData));
            });
            //楼层变化
            $("#Loc4").change(function () {
                expandNode($(this).val());
            });
            //获取树形菜单数据
            getTree();
        });

        //-----------   图片数据，单独用一个表单装载  ----------
        var imgFormData = new FormData;
        imgFormData.append("img", "");
        imgFormData.append("id", "");
        imgFormData.append("meterNo", "");

        //---------------------     全局变量    ------------------
        var page = 1;   //页码
        var meterNo = "";
        var zNodes = new Array();
        reflist();

        //--------------------------    查询界面        -------------------------------
        //列表图片放大显示
        function showImg(id) {
            layer.open({
                type: 1,
                content: $("#" + id),
                area: [$(window).width() * 0.7 + 'px', $(window).height() * 0.9 + 'px'],
                title: false,
                closeBtn: 0,
                shadeClose: true,
                skin: 'layui-layer-nobg',
                success: function () {
                    $("#" + id).css({ "width": "100%", "height": "100%" });
                    $("#" + id).unbind("click").bind("click", function () { layer.closeAll(); });
                },
                end: function () {
                    $("#" + id).css({ "width": "", "height": "" });
                    $("#" + id).show();
                    $("#" + id).unbind("click").bind("click", function () {
                        showImg($(this).attr("id"));
                    });
                }
            });
        }

        //树形菜单显示或隐藏
        function zTreeShowOrHide(isShow) {
            if (isShow) {
                //显示
                $("#ztree").parent().show();
                $("#edit").parent().css("margin-left", "200px");
            } else {
                //隐藏
                $("#ztree").parent().hide();
                $("#edit").parent().css("margin-left", "");
            }
        }
        //编辑区域显示或隐藏
        function editDivShowOrHide(isShow) {
            if (isShow) {
                $("#edit").show();
                $("#list").hide();
            } else {
                $("#edit").hide();
                $("#list").show();
            }
        }
        //编辑区域图片层显示或隐藏
        function imgDivShowOrHide(isShow) {
            if (isShow) {
                $(".showdiv").show();
                $(".adddiv").hide();
            } else {
                $(".showdiv").hide();
                $(".adddiv").show();
            }
        }

        //添加按钮点击
        function insert() {
            $('#edit').validate('reset');
            type = "insert";
            resetEditDiv();
            zTreeShowOrHide(false);
            editDivShowOrHide(true);
            return;
        }
        //更新按钮点击
        function update() {
            if ($("#selectKey").val() == "") {
                layer.msg("请先选择要修改的信息", { icon: 3, time: 1000 });
                return;
            }
            $('#edit').validate('reset');
            id = $("#selectKey").val();
            type = "update";
            var submitData = new Object();
            submitData.Type = "update";
            submitData.id = id;
            transmitData(datatostr(submitData));
            return;
        }
        //删除按钮点击
        function del() {
            if ($("#selectKey").val() == "") {
                layer.msg("请先选择一条记录", { icon: 3, time: 1000 });
                return;
            }
            layer.confirm('确认要删除吗？', function (index) {
                var submitData = new Object();
                submitData.Type = "delete";
                submitData.id = $("#selectKey").val();
                submitData.page = page;
                getCondition(submitData)
                transmitData(datatostr(submitData));
                layer.close(index);
            });
            return;
        }
        //审核按钮点击
        function audit() {
            if ($("#selectKey").val() == "") {
                layer.msg("请先选择一条记录", { icon: 3, time: 1000 });
                return;
            }
            layer.confirm('确认要审核通过吗？', function (index) {
                var submitData = new Object();
                submitData.Type = "audit";
                submitData.id = $("#selectKey").val();
                submitData.page = page;
                getCondition(submitData);
                transmitData(datatostr(submitData));
                layer.close(index);
            });
            return;
        }
        //取消审核按钮点击
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
        //审核未通过提交按钮
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
        //审核未通过取消按钮
        function auditcancel() {
            layer.closeAll();
        }
        //查询按钮提交
        function select() {
            var submitData = new Object();
            submitData.Type = "select";
            submitData.page = 1;
            getCondition(submitData);
            transmitData(datatostr(submitData));
            return;
        }
        //导出execel按钮点击
        function excel() {
            var submitData = new Object();
            submitData.Type = "excel";
            getCondition(submitData);
            transmitData(datatostr(submitData));
            return;
        }
        /*
         * 跳页按钮点击
         */

        function jump(pageindex) {
            page = pageindex;
            var submitData = new Object();
            submitData.Type = "jump";
            submitData.page = page;
            getCondition(submitData);
            transmitData(datatostr(submitData));
            return;
        }
        //查询区域参数收集
        function getCondition(obj) {
            obj.Loc3 = $("#Loc3").val();
            obj.Loc4 = $("#Loc4").val();
            obj.RMIDS = $("#RMIDS").val();
            obj.MeterNoS = $("#MeterNoS").val();
            obj.ReadoutTypeS = $("#ReadoutTypeS").val();
            obj.MeterTypeS = $("#MeterTypeS").val();
            obj.AuditStatusS = $("#AuditStatusS").val();
            obj.MinRODate = $("#MinRODate").val();
            obj.MaxRODate = $("#MaxRODate").val();
        }
        //---------------------------   添加或编辑 -------------------------------------------

        //添加或编辑，提交按钮点击
        function submit() {
            if ($('#edit').validate('submitValidate')) {
                //先上传图片
                if (uploadReadoutImg()) {
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
                    submitData.page = page;
                    getCondition(submitData);
                    transmitData(datatostr(submitData));
                } else {
                    layer.msg("图片上传错误！");
                }
            }
            return;
        }
        //添加或编辑，提交并继续录入按钮点击
        function submit1() {
            if ($('#edit').validate('submitValidate')) {
                if (uploadReadoutImg()) {
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
                    submitData.page = page;
                    getCondition(submitData);
                    transmitData(datatostr(submitData));
                } else {
                    layer.msg("图片上传错误！");
                }
            }
            return;
        }
        //添加或编辑，取消按钮点击
        function cancel() {
            resetEditDiv();
            editDivShowOrHide(false);
            zTreeShowOrHide(true);
            return;
        }
        //添加或修改中的表记编号文本框，失去焦点操作
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
        //添加或编辑区域重置
        function resetEditDiv() {
            //主键重置
            id = "";
            //文本框重置
            $("#chooseMeterNo").unbind('click').bind("click", chooseMeterNo);
            $("#RMID").val("");
            $("#MeterNo").val("");
            $("#ReadoutType").val("1");
            $("#LastReadout").val("");
            $("#Readout").val("");
            $("#JoinReadings").val("");
            $("#Readings").val("");
            $("#MeteRate").val("");
            $("#ROOperator").val("");
            $("#RODate").val("<%=date %>");
            $("#Img").val("");
            //图片表单数据重置
            imgFormData.set("img", "");
            imgFormData.set("id", "");
            imgFormData.set("meterNo", "");
            $(".showdiv img").attr("src", "");
            imgDivShowOrHide(false);

        }
        //添加或编辑，表读数数据变化计算
        $("#Readout").change(function () {
            calureadout();
        });
        //添加或编辑，表关联读数数据变化计算
        $("#JoinReadings").change(function () {
            calureadout();
        });
        //度数计算
        function calureadout() {
            if ($("#Readout").val() != "" && $("#LastReadout").val() != "") {
                if ($("#JoinReadings").val() == "") $("#JoinReadings").val("0");

                if (parseFloat($("#Readout").val()) >= parseFloat($("#LastReadout").val()))
                    $("#Readings").val(parseFloat($("#Readout").val()) - parseFloat($("#LastReadout").val()) - parseFloat($("#JoinReadings").val()))
                else {
                    if (parseInt($("#MeterDigit").val()) == 4)
                        $("#Readings").val(parseFloat($("#Readout").val()) + (9999 - parseFloat($("#LastReadout").val()) + 1) - parseFloat($("#JoinReadings").val()));
                    else if (parseInt($("#MeterDigit").val()) == 5)
                        $("#Readings").val(parseFloat($("#Readout").val()) + (99999 - parseFloat($("#LastReadout").val()) + 1) - parseFloat($("#JoinReadings").val()));
                    else if (parseInt($("#MeterDigit").val()) == 6)
                        $("#Readings").val(parseFloat($("#Readout").val()) + (999999 - parseFloat($("#LastReadout").val()) + 1) - parseFloat($("#JoinReadings").val()));
                    else if (parseInt($("#MeterDigit").val()) == 7)
                        $("#Readings").val(parseFloat($("#Readout").val()) + (9999999 - parseFloat($("#LastReadout").val()) + 1) - parseFloat($("#JoinReadings").val()));
                    else
                        $("#Readings").val(parseFloat($("#Readout").val()) + (99999999 - parseFloat($("#LastReadout").val()) + 1) - parseFloat($("#JoinReadings").val()));
                }
            }
            else {
                $("#Readings").val("")
            }
        }
        //添加或编辑，，重置
        $('#edit').validate({
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
        //添加或编辑，选择房间界面弹出
        function chooseRMID() {
            var temp = "../Base/ChooseRMID.aspx?id=RMID";
            layer_show("选择页面", temp, 800, 630);
        }
        //添加或编辑，选择表记资料界面弹出
        function chooseMeterNo() {
            var temp = "../Base/ChooseMeter.aspx?id=MeterNo&RMID=";// + $("#RMID").val();
            layer_show("选择页面", temp, 800, 630);
        }


        //-----------------------------    添加或编辑，图片处理  -----------------------------

        //选择图片
        function addImg(obj) {
            var ff = $("#addbtn").val();
            if (ff == null || ff == "") {
                if ($(".showdiv img").attr("src").length <= 0) {
                    layer.msg("请选择文件");
                }
                return;
            } else if (!/.(gif|jpg|jpeg|png|gif|jpg|png)$/.test(ff)) {
                layer.msg("图片类型必须是.gif,jpeg,jpg,png中的一种");
                return;
            }
            imgFormData.set("img", obj.files[0]);
            var objUrl = getObjectURL(obj.files[0]); //获取图片的路径，该路径不是图片在本地的路径
            if (objUrl) {
                $(".showdiv img").attr("src", objUrl); //将图片路径存入src中，显示出图片
                imgDivShowOrHide(true);
            }
        }
        //删除图片
        function delImg() {
            imgDivShowOrHide(false);
            imgFormData.set("img", "");
            $(".showdiv img").attr("src", ""); //将图片路径存入src中，显示出图片
            $("#Img").val("");
            document.getElementById("imgForm").reset();
        }
        //上传图片
        function uploadReadoutImg() {
            var success = false;
            if (imgFormData.get("img").name != "undefined" && imgFormData.get("img") != "") {
                imgFormData.set("id", id);
                imgFormData.set("meterNo", $("#MeterNo").val())
                $.ajax({
                    url: "ReadoutImg.ashx",
                    type: "POST",
                    data: imgFormData,
                    dataType: "json",
                    async: false,
                    contentType: false,
                    processData: false,
                    success: function (result) {
                        if (result.flag == 1) {
                            $("#Img").val(result.info);
                            success = true;
                        } else {
                            console.log(result.info);
                        }
                    },
                    error: function (err) {
                        console.log(err.statusText);
                    }
                });
            } else success = true;
            return success;
        }
        //获取本地图片URL
        function getObjectURL(file) {
            var url = null;
            if (window.createObjectURL != undefined) { // basic
                url = window.createObjectURL(file);
            } else if (window.URL != undefined) { // mozilla(firefox)
                url = window.URL.createObjectURL(file);
            } else if (window.webkitURL != undefined) { // webkit or chrome
                url = window.webkitURL.createObjectURL(file);
            }
            return url;
        }

        //--------------------------    树形菜单    ----------------------------

        //function choose(id, labels, values) {
        //    if (labels != "" && labels != undefined && labels != "undefined") {
        //        if (id == "MeterNo") {
        //            var submitData = new Object();
        //            submitData.Type = "getMeterInfo";
        //            submitData.MeterNo = labels;
        //            transmitData(datatostr(submitData));
        //        }
        //        else {
        //            $("#" + id).val(labels);
        //        }
        //    }
        //}

        function getTree() {
            var submitData = new Object();
            submitData.Type = "gettreelist";
            getCondition(submitData);
            transmitData(datatostr(submitData));
        }


        //树形菜单选项配置
        var setting = {
            view: {
                dblClickExpand: false, //双击节点时，是否自动展开父节点的标识
                showLine: true, //是否显示节点之间的连线
                //fontCss:{'color':'black','font-size':'22px'}//字体样式函数
                //selectedMulti: false //设置是否允许同时选中多个节点
            },
            data: {
                simpleData: { //简单数据模式
                    enable: true,
                    idKey: "id",
                    pIdKey: "pid",
                    rootPId: null
                }
            },
            data: {
                key: {
                    name: "name"
                }
            },
            callback: {
                onClick: zTreeOnClick
                // beforeExpand:zTreeBeforeExpand, // 用于捕获父节点展开之前的事件回调函数，并且根据返回值确定是否允许展开操作
            }
        }
        //树形菜单节点点击
        function zTreeOnClick(event, treeId, treeNode) {
            if (treeNode.type == "building") {
                //楼栋节点
                if (treeNode.id != $("#Loc3").val()) {
                    $("#Loc3").val(treeNode.id).trigger("change");//选中并触发
                }
            } else if (treeNode.type == "floor") {
                //楼层节点
                var selectId = $("#Loc4 option[value='" + treeNode.id + "']").val();
                if (!selectId) {
                    $("#Loc4").next().show();
                    $("#Loc3").val(treeNode.getParentNode().id).trigger("change");
                    var count = 0;
                    var interval = setInterval(function () {
                        selectId = $("#Loc4 option[value='" + treeNode.id + "']").val();
                        if (!!selectId || count > 10) {
                            if (!!selectId) {
                                $("#Loc4").val(treeNode.id);
                            }
                            $("#Loc4").next().hide();
                            clearInterval(interval);
                        } else {
                            count++;
                        }
                    }, 200);
                } else {
                    $("#Loc4").val(treeNode.id);
                }
            } else if (treeNode.type == "room") {
                $("#RMIDS").val(treeNode.id);
            } else if (treeNode.type == "meter") {
                $("#MeterNoS").val(treeNode.id);
            }
        }
        //树形菜单展开
        function expandNode(id) {
            var zTreeObj = $.fn.zTree.getZTreeObj("ztree");
            var selectObj = zTreeObj.getSelectedNodes()[0];
            //console.log(selectObj);
            if (selectObj && selectObj.id == id) {
                return;
            }
            zTreeObj.expandAll(false);
            setTimeout(function () {
                //解决展开速度大于收缩速度，导致展开失效
                var node = zTreeObj.getNodesByParam("id", id, null)[0];
                zTreeObj.selectNode(node);
                zTreeObj.expandNode(node, true);
            }, 300)

        }
        //---------------------------------------------  异步返回数据操作    ------------------------------------------

        //异步提交操作返回数据操作
        function BandResuleData(temp) {
            var vjson = JSON.parse(temp);
            //查询
            if (vjson.type == "select") {
                if (vjson.flag == "1") {
                    $("#outerlist").html(vjson.liststr);
                    $(".readoutImg").click(function () {
                        showImg($(this).attr("id"));
                    });
                    $("#selectKey").val("");
                    page = 1;
                    reflist();
                }
            }
            //跳页
            if (vjson.type == "jump") {
                if (vjson.flag == "1") {
                    $("#outerlist").html(vjson.liststr);
                    $(".readoutImg").click(function () {
                        showImg($(this).attr("id"));
                    });
                    $("#selectKey").val("");
                    reflist();
                }
            }
            //删除
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
            //提交
            if (vjson.type == "submit") {
                if (vjson.flag == "1") {
                    $("#selectKey").val("");
                    $("#outerlist").html(vjson.liststr);
                    $("#MeterNoS").val(vjson.MeterNo);
                    $(".readoutImg").click(function () {
                        showImg($(this).attr("id"));
                    });
                    reflist();
                    resetEditDiv();
                    editDivShowOrHide(false);
                    zTreeShowOrHide(true);
                }
                //else {
                //    layer.msg(vjson.info);
                //}
                layer.msg(vjson.msg);
                //else if (vjson.flag == "3") {
                //    showMsg("MeterNo", "表记编号不存在", "1");
                //}
                return;
            }
            //提交继续录入
            if (vjson.type == "submit1") {
                if (vjson.flag == "1") {
                    $("#selectKey").val("");
                    $("#outerlist").html(vjson.liststr);
                    $(".readoutImg").click(function () {
                        showImg($(this).attr("id"));
                    });
                    reflist();
                    resetEditDiv();
                    insert();

                }
                layer.msg(vjson.msg);
                return;
            }
            //修改
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
                    $("#Img").val(vjson.Img);
                    $("#chooseRMID").unbind('click');
                    $("#chooseMeterNo").unbind('click');
                    if (vjson.Img != "") {
                        $(".showdiv img").attr("src", "../../upload/meter/" + vjson.Img);
                        imgDivShowOrHide(true);
                    }
                    zTreeShowOrHide(false);
                    editDivShowOrHide(true);

                }
                else if (vjson.flag == "3") {
                    layer.alert("当前状态不允许修改！");
                }
                else {
                    layer.alert("数据操作出错！");
                }
                return;
            }
            //审核
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
            //取消审核
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
            //导出
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
            //树形菜单：作废
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
            //表记信息
            if (vjson.type == "getMeterInfo") {
                if (vjson.flag == "1") {
                    $("#MeterNo").val(vjson.MeterNo);
                    $("#RMID").val(vjson.MeterRMID);
                    $("#LastReadout").val(vjson.readout);
                    $("#MeteRate").val(vjson.rate);
                    $("#MeterDigit").val(vjson.digit);
                    $("#JoinReadings").val("0");

                    if ($("#Readout").val() != "" && $("#LastReadout").val() != "") {
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
            //楼层信息
            if (vjson.type == "getLoc4") {
                $("#Loc4").empty();
                $("#Loc4").append(vjson.data);
            }
            //树形菜单
            if (vjson.type == "gettree") {
                if (vjson.flag == 0) {
                    var zTreeObj = $.fn.zTree.init($("#ztree"), setting, JSON.parse(vjson.data));
                }
                $(".ztree-loading").remove();
            }
        }

    </script>
</body>
</html>
