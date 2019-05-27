<%@ Page Language="C#" AutoEventWireup="true" Inherits="project.Presentation.Base.Meter,project" %>

<!DOCTYPE>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head1" runat="server">
    <title>表计资料</title>
    <!--[if lt IE 9]>
    <script type="text/javascript" src="../jscript/html5.js"></script>
    <script type="text/javascript" src="../jscript/respond.min.js"></script>
    <![endif]-->
    <link href="../../css/H-ui.min.css" rel="stylesheet" type="text/css" />
    <link href="../../css/H-ui.admin.css" rel="stylesheet" type="text/css" />
    <link href="../../lib/iconfont/iconfont.css" rel="stylesheet" type="text/css" />
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <style>
        .printck {
            display: none;
        }

            .printck input {
                height: 15px;
                width: 15px;
            }

        #view label {
            font-size: 18px;
        }

            #view label:first-child {
                display: inline-block;
                width: 100px;
                font-weight: bold;
                text-align: right;
            }

            #view label:last-child {
                display: inline-block;
                width: 300px;
                text-align: left;
            }

        #view div {
            float: left;
            margin-right: 10px;
            margin-top: 15px;
            border-right: 2px solid #e5dddd;
        }

            #view div:first-child {
                padding-left: 40px;
            }

                #view div:first-child label:last-child {
                    width: 260px;
                }

            #view div:last-child {
                text-align: center;
                clear: both;
                width: 100%;
                margin-top: 20px;
                border: none;
            }

                #view div:last-child button {
                    width: 90px;
                    height: 50px;
                    border-radius: 5px;
                    font-size: 20px;
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
    <nav class="breadcrumb">
        <i class="Hui-iconfont">&#xe67f;</i> 首页 
        <span class="c-gray en">&gt;</span> 表计管理 
        <span class="c-gray en">&gt;</span>表计资料 
       <a class="btn btn-success radius r mr-20" style="line-height: 1.6em; margin-top: 2px" href="javascript:location.replace(location.href);" title="刷新">
           <i class="Hui-iconfont">&#xe68f;</i>
       </a>
    </nav>
    <div id="list" class="pt-5 pr-20 pb-5 pl-20">
        <div class="cl pd-5 bg-1 bk-gray mt-2">
            <div class="normalbtn"><%=Buttons %></div>
            <div class="printbtn" style="display: none;">
                <a href="javascript:;" onclick="printchoice()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe615;</i> 打印(选择)</a>
                <a href="javascript:;" onclick="printall()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe615;</i> 打印(全部)</a>
                <a href="javascript:;" onclick="printcancel()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe615;</i> 取消打印</a>
            </div>
            <span class="l">
                <%--<a href="javascript:;" onclick="insert()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe600;</i> 添加</a>
                <a href="javascript:;" onclick="update()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe60c;</i> 修改</a> 
                <a href="javascript:;" onclick="del()" class="btn btn-danger radius"><i class="Hui-iconfont">&#xe6e2;</i> 删除</a>
                <a href="javascript:;" onclick="valid()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe615;</i> 启用/停用</a>--%>
                <input type="hidden" id="selectKey" />
            </span>
        </div>
        <div class="cl pd-10  bk-gray mt-2">
            园区&nbsp;<%=MeterLOCNo1StrS %>&nbsp;
            建设期&nbsp;<select class="input-text required size-MINI" id="MeterLOCNo2S" style="width: 120px"><option value="">全部</option>
            </select>&nbsp;
            楼栋&nbsp;<select class="input-text required size-MINI" id="MeterLOCNo3S" style="width: 120px"><option value="">全部</option>
            </select>&nbsp;
            楼层&nbsp;<select class="input-text required size-MINI" id="MeterLOCNo4S" style="width: 120px"><option value="">全部</option>
            </select>&nbsp;
		    状态&nbsp;<select class="input-text size-MINI ml-5 mr-10" style="width: 100px" id="MeterStatusS">
                <option value="" selected>全部</option>
                <option value="open">正常</option>
                <option value="close">停用</option>
            </select>
            <br />
            表计编号&nbsp;<input type="text" class="input-text size-MINI ml-5 mr-10" style="width: 120px" placeholder="表计编号" id="MeterNoS" />&nbsp;
		    表计类别&nbsp;<select class="input-text size-MINI ml-5 mr-10" style="width: 100px" id="MeterTypeS">
                <option value="" selected>全部</option>
                <option value="wm">水表</option>
                <option value="am">电表</option>
            </select>
            使用类别&nbsp;<select class="input-text size-MINI ml-5 mr-10" style="width: 100px" id="MeterUsageTypeS">
                <option value="" selected>全部</option>
                <option value="0">公共</option>
                <option value="1">家用</option>
                <option value="2">商用</option>
                <option value="3">其他</option>

            </select>
            大小类别&nbsp;<select class="input-text size-MINI ml-5 mr-10" style="width: 100px" id="MeterSizeS">
                <option value="" selected>全部</option>
                <option value="1">大表</option>
                <option value="2">小表</option>
            </select>
            房间号&nbsp;<input type="text" class="input-text size-MINI ml-5 mr-10" style="width: 120px" placeholder="位置" id="MeterRMIDS" />&nbsp;
		    <button type="button" class="btn btn-success radius" onclick="select()"><i class="Hui-iconfont">&#xe665;</i> 查询</button>
        </div>
        <div class="mt-5" id="outerlist">
            <%=list %>
        </div>
    </div>
    <div id="edit" class="pt-5 pr-20 pb-5 pl-20 " style="display: none;">
        <div class="form form-horizontal bk-gray mt-15 pb-10" id="editlist">
            <div class="row cl">
                <label class="form-label col-2"><span class="c-red">*</span>表计编号：</label>
                <div class="formControls col-4">
                    <input type="text" class="input-text w-200 required" placeholder="表计编号" id="MeterNo" data-valid="isNonEmpty||between:1-30" data-error="表计编号不能为空||表计编号长度为1-30位" />
                </div>
                <div class="col-3"></div>
            </div>

            <div class="row cl">
                <label class="form-label col-2"><span class="c-red">*</span>表计名称：</label>
                <div class="formControls col-4">
                    <input type="text" class="input-text w-200 required" placeholder="表计名称" id="MeterName" data-valid="isNonEmpty||between:2-50" data-error="表计名称不能为空||表计名称长度为2-50位" />
                </div>
                <div class="col-3"></div>
            </div>

            <div class="row cl">
                <label class="form-label col-2"><span class="c-red">*</span>表计类别：</label>
                <div class="formControls col-4">
                    <select class="input-text w-200 required" id="MeterType" data-valid="isNonEmpty" data-error="表计类别不能为空">
                        <option value="">请选择表计类别</option>
                        <option value="wm">水表</option>
                        <option value="am">电表</option>
                    </select>
                </div>
                <div class="col-3"></div>
            </div>

            <div class="row cl">
                <label class="form-label col-2"><span class="c-red">*</span>表计倍率：</label>
                <div class="formControls col-4">
                    <input type="text" class="input-text w-200 required" placeholder="表计倍率" id="MeterRate" data-valid="isNonEmpty||onlyNum" data-error="表计倍率不能为空||须为数字" />
                </div>
                <div class="col-3"></div>
            </div>

            <div class="row cl">
                <label class="form-label col-2"><span class="c-red">*</span>表计位数：</label>
                <div class="formControls col-4">
                    <select class="input-text w-200 required" id="MeterDigit" data-valid="isNonEmpty" data-error="请选择表计位数">
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
                    <select class="input-text required" id="MeterLOCNo2" data-valid="isNonEmpty" data-error="建设期不能为空"></select>
                </div>
                <div class="col-3"></div>
            </div>

            <div class="row cl">
                <label class="form-label col-2"><span class="c-red">*</span>楼栋：</label>
                <div class="formControls col-4">
                    <select class="input-text required" id="MeterLOCNo3" data-valid="isNonEmpty" data-error="楼栋不能为空"></select>
                </div>
                <div class="col-3"></div>
            </div>

            <div class="row cl">
                <label class="form-label col-2"><span class="c-red">*</span>楼层：</label>
                <div class="formControls col-4">
                    <select class="input-text required" id="MeterLOCNo4" data-valid="isNonEmpty" data-error="楼层不能为空"></select>
                </div>
                <div class="col-3"></div>
            </div>

            <div class="row cl">
                <label class="form-label col-2">房间编号：</label>
                <div class="formControls col-4">
                    <input type="text" class="input-text required" placeholder="房间编号" id="MeterRMID" style="width: 240px;" disabled="disabled" data-valid="between:0-30" data-error="" />
                    <button type="button" class="btn btn-primary radius" id="chooseRMID">选择</button>
                </div>
                <div class="col-3"></div>
            </div>

            <div class="row cl">
                <label class="form-label col-2">位置：</label>
                <div class="formControls col-4">
                    <input type="text" class="input-text w-200 required" placeholder="位置" id="Addr" data-valid="between:0-200" data-error="位置长度为0-200位" />
                </div>
                <div class="col-3"></div>
            </div>

            <div class="row cl">
                <label class="form-label col-2"><span class="c-red">*</span>大小类别：</label>
                <div class="formControls col-4">
                    <select class="input-text w-200 required" id="MeterSize" data-valid="isNonEmpty" data-error="大小类别不能为空">
                        <option value="">请选择大小类别</option>
                        <option value="1">大表</option>
                        <option value="2">小表</option>
                    </select>
                </div>
                <div class="col-3"></div>
            </div>

            <div class="row cl" style="display: none;">
                <label class="form-label col-2">关联表计编号：</label>
                <div class="formControls col-4">
                    <input type="text" class="input-text w-200 required" placeholder="关联表计编号" id="MeterRelatedMeterNo" data-valid="between:0-200" data-error="房间编号长度为0-200位" />
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
    <div id="view" class="pt-20 pr-20 pb-5 pl-20 form-horizontal" style="display: none;">
    </div>
    <div id="change" class="form form-horizontal" style="display: none;">
        <div class="row">
            <label class="form-label col-2"><span class="c-red">*</span>[旧表]园区：</label>
            <div class="formControls col-3">
                <input type="text" class="input-text  loc1" disabled="disabled" />
            </div>
            <label class="form-label col-2"><span class="c-red">*</span>[新表]园区：</label>
            <div class="formControls col-3">
                <input type="text" class="input-text  loc1" disabled="disabled" />
            </div>
        </div>
        <div class="row">
            <label class="form-label col-2"><span class="c-red">*</span>[旧表]建设期：</label>
            <div class="formControls col-3">
                <input type="text" class="input-text  loc2" disabled="disabled" />
            </div>
            <label class="form-label col-2"><span class="c-red">*</span>[新表]建设期：</label>
            <div class="formControls col-3">
                <input type="text" class="input-text  loc2" disabled="disabled" />
            </div>
        </div>
        <div class="row">
            <label class="form-label col-2"><span class="c-red">*</span>[旧表]楼栋：</label>
            <div class="formControls col-3">
                <input type="text" class="input-text  loc3" disabled="disabled" />
            </div>
            <label class="form-label col-2"><span class="c-red">*</span>[新表]楼栋：</label>
            <div class="formControls col-3">
                <input type="text" class="input-text  loc3" disabled="disabled" />
            </div>
        </div>
        <div class="row">
            <label class="form-label col-2"><span class="c-red">*</span>[旧表]楼层：</label>
            <div class="formControls col-3">
                <input type="text" class="input-text  loc4" disabled="disabled" />
            </div>
            <label class="form-label col-2"><span class="c-red">*</span>[新表]楼层：</label>
            <div class="formControls col-3">
                <input type="text" class="input-text  loc4" disabled="disabled" />
            </div>
        </div>
        <div class="row">
            <label class="form-label col-2"><span class="c-red">*</span>[旧表]房间：</label>
            <div class="formControls col-3">
                <input type="text" class="input-text  loc5" disabled="disabled" />
            </div>
            <label class="form-label col-2"><span class="c-red">*</span>[新表]房间：</label>
            <div class="formControls col-3">
                <input type="text" class="input-text  loc5" disabled="disabled" />
            </div>
        </div>
        <%--  --%>
        <div class="row">
            <label class="form-label col-2"><span class="c-red">*</span>[旧表]表计编号：</label>
            <div class="formControls col-3">
                <input type="text" class="input-text meterNo" disabled="disabled" />
            </div>
            <label class="form-label col-2"><span class="c-red">*</span>[新表]表计编号：</label>
            <div class="formControls col-3">
                <input type="text" class="input-text required meterNo" data-valid="isNonEmpty||between:2-50" data-error="表计编号不能为空||长度2-50" />
            </div>
            <div class="col-2"></div>
        </div>
        <div class="row">
            <label class="form-label col-2"><span class="c-red">*</span>[旧表]表计名称：</label>
            <div class="formControls col-3">
                <input type="text" class="input-text meterName" disabled="disabled" />
            </div>
            <label class="form-label col-2"><span class="c-red">*</span>[新表]表计名称：</label>
            <div class="formControls col-3">
                <input type="text" class="input-text required meterName" data-valid="isNonEmpty||between:2-50" data-error="表计名称不能为空||长度2-50" />
            </div>
            <div class="col-2"></div>
        </div>
        <div class="row">
            <label class="form-label col-2"><span class="c-red">*</span>[旧表]上期读数：</label>
            <div class="formControls col-3">
                <input type="text" class="input-text lastRead" disabled="disabled" />
            </div>
            <label class="form-label col-2"><span class="c-red">*</span>[新表]起始读数：</label>
            <div class="formControls col-3">
                <input type="text" class="input-text required startRead" data-valid="isNonEmpty||onlyNum" data-error="起始读数不能为空||必须为数字" />
            </div>
            <div class="col-2"></div>
        </div>
        <div class="row">
            <label class="form-label col-2"><span class="c-red">*</span>[旧表]换表止度：</label>
            <div class="formControls col-3">
                <input type="text" class="input-text required endRead" data-valid="isNonEmpty||onlyNum" data-error="换表止度不能为空||必须为数字" onchange="calcReadout()"/>
            </div>
            <div class="col-2"></div>
        </div>
        <div class="row">
            <label class="form-label col-2"><span class="c-red">*</span>[旧表]行度：</label>
            <div class="formControls col-3">
                <input type="text" class="input-text required reading" disabled="disabled" data-valid="isNonEmpty||onlyNum" data-error="行度不能为空||必须为数字" />
            </div>
            <div class="col-2"></div>
        </div>
        <div class="row">
            <label class="form-label col-2"><span class="c-red">*</span>[旧表]倍率：</label>
            <div class="formControls col-3">
                <input type="text" class="input-text rate" disabled="disabled" />
            </div>
            <label class="form-label col-2"><span class="c-red">*</span>[新表]倍率：</label>
            <div class="formControls col-3">
                <input type="text" class="input-text required rate" data-valid="isNonEmpty||onlyNum" data-error="倍率不能为空||必须为数字" />
            </div>
            <div class="col-2"></div>
        </div>
        <div class="row">
            <label class="form-label col-2"><span class="c-red">*</span>[旧表]位数：</label>
            <div class="formControls col-3">
                <select class="input-text digit" style="background-color: #EEEEEE;" disabled="disabled">
                    <option value="4">4</option>
                    <option value="5">5</option>
                    <option value="6">6</option>
                    <option value="7">7</option>
                    <option value="8">8</option>
                </select>
            </div>
            <label class="form-label col-2"><span class="c-red">*</span>[新表]位数：</label>
            <div class="formControls col-3">
                <select class="input-text required digit" data-valid="isNonEmpty" data-error="位数不能为空">
                    <option value="4">4</option>
                    <option value="5">5</option>
                    <option value="6">6</option>
                    <option value="7">7</option>
                    <option value="8">8</option>
                </select>
            </div>
            <div class="col-2"></div>
        </div>
        <%--  --%>
        <div class="row">
            <label class="form-label col-2"><span class="c-red">*</span>[旧表]表计类别：</label>
            <div class="formControls col-3">
                <select class="input-text meterType" disabled="disabled" style="background-color: #EEEEEE;">
                    <option value="">请选择</option>
                    <option value="wm">水表</option>
                    <option value="am">电表</option>
                </select>
            </div>
            <label class="form-label col-2"><span class="c-red">*</span>[新表]表计类别：</label>
            <div class="formControls col-3">
                <select class="input-text required meterType" data-valid="isNonEmpty" data-error="表计类别不能为空">
                    <option value="">请选择</option>
                    <option value="wm">水表</option>
                    <option value="am">电表</option>
                </select>
            </div>
            <div class="col-2"></div>
        </div>
        <div class="row">
            <label class="form-label col-2"><span class="c-red">*</span>[旧表]大小类别：</label>
            <div class="formControls col-3">
                <select class="input-text sizeType" disabled="disabled" style="background-color: #EEEEEE;">
                    <option value="">请选择</option>
                    <option value="1">大表</option>
                    <option value="2">小表</option>
                </select>
            </div>
            <label class="form-label col-2"><span class="c-red">*</span>[新表]大小类别：</label>
            <div class="formControls col-3">
                <select class="input-text required sizeType" data-valid="isNonEmpty" data-error="大小类别不能为空">
                    <option value="">请选择</option>
                    <option value="1">大表</option>
                    <option value="2">小表</option>
                </select>
            </div>
            <div class="col-2"></div>
        </div>
        <div class="row">
            <label class="form-label col-2"><span class="c-red">*</span>[旧表]性质类别：</label>
            <div class="formControls col-3">
                <select class="input-text natureType" style="background-color: #EEEEEE;" disabled="disabled">
                    <option value="">请选择</option>
                    <option value="1">居民用电</option>
                    <option value="2">工业用电</option>
                    <option value="3">商业用电</option>
                    <option value="4">其它用电</option>
                </select>
            </div>
            <label class="form-label col-2"><span class="c-red">*</span>[新表]性质类别：</label>
            <div class="formControls col-3">
                <select class="input-text required natureType" data-valid="isNonEmpty" data-error="性质类别不能为空">
                    <option value="">请选择</option>
                    <option value="1">居民用电</option>
                    <option value="2">工业用电</option>
                    <option value="3">商业用电</option>
                    <option value="4">其它用电</option>
                </select>
            </div>
            <div class="col-2"></div>
        </div>
        <div class="row">
            <label class="form-label col-2"><span class="c-red">*</span>[旧表]使用类别：</label>
            <div class="formControls col-3">
                <select class="input-text useType" style="background-color: #EEEEEE;" disabled="disabled">
                    <option value="">请选择</option>
                    <option value="0">公共</option>
                    <option value="1">家用</option>
                    <option value="2">商用</option>
                    <option value="3">其他</option>
                </select>
            </div>
            <label class="form-label col-2"><span class="c-red">*</span>[新表]使用类别：</label>
            <div class="formControls col-3">
                <select class="input-text required useType" data-valid="isNonEmpty" data-error="使用类别不能为空">
                    <option value="">请选择</option>
                    <option value="0">公共</option>
                    <option value="1">家用</option>
                    <option value="2">商用</option>
                    <option value="3">其他</option>
                </select>
            </div>
            <div class="col-2"></div>
        </div>
        <div class="row">
            <label class="form-label col-2"><span class="c-red">*</span>[旧表]表计位置：</label>
            <div class="formControls col-3">
                <input type="text" class="input-text addr" disabled="disabled" />
            </div>
            <label class="form-label col-2"><span class="c-red">*</span>[新表]表计位置：</label>
            <div class="formControls col-3">
                <input type="text" class="input-text addr" />
            </div>
        </div>
        <div class="row" style="text-align: center; margin: 20px 0px;">
            <button class="btn btn-primary" style="margin-right: 20px; width: 80px; height: 40px; border-radius: 5px; font-size: 18px;" onclick="changesubmit()">提&nbsp;交</button>
            <button class="btn btn-default" style="width: 80px; height: 40px; border-radius: 5px; font-size: 18px;" onclick="javascript:$('#change').hide();$('#list').show();">取&nbsp;消</button>
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
                    if (printFlag == 1) {
                        $(".printck").show();
                    } else {
                        $(".printck").hide();
                    }
                    page = 1;
                    reflist();
                }
            }
            if (vjson.type == "jump") {
                if (vjson.flag == "1") {
                    $("#outerlist").html(vjson.liststr);
                    $("#selectKey").val("");
                    if (printFlag == 1) {
                        $(".printck").show();
                    } else {
                        $(".printck").hide();
                    }
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
                    showMsg("MeterNo", "此表计编号已存在", "1");
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
            if (vjson.type == "label") {
                if (vjson.flag == "0") {
                    layer.alert("打印失败！");
                } else {
                    layer.alert("成功发送打印！请查看打印机打印结果！");
                }
            }
            if (vjson.type == "view") {
                if (vjson.flag == "1") {
                    var meter = JSON.parse(vjson.data)[0];
                    var html = "";
                    html += "<div>";
                    html += "<p><label>电表编号：</label><label>" + meter.No + "</label></p>";
                    html += "<p><label>电表名称：</label><label>" + meter.Name + "</label></p>";
                    html += "<p><label>电表倍数：</label><label>" + meter.Rate + "</label></p>";
                    html += "<p><label>电表位数：</label><label>" + meter.Digit + "</label></p>";
                    html += "<p style='visibility:hidden;'><label>电表位数：</label><label>" + meter.Digit + "</label></p>";
                    html += "</div>";

                    html += "<div>";
                    html += "<p><label>园区：</label><label>" + meter.Loc1 + "</label></p>";
                    html += "<p><label>建设期：</label><label>" + meter.Loc2 + "</label></p>";
                    html += "<p><label>楼栋：</label><label>" + meter.Loc3 + "</label></p>";
                    html += "<p><label>楼层：</label><label>" + meter.Loc4 + "</label></p>";
                    if (meter.Room) {
                        html += "<p><label>房间：</label><label>" + meter.Room + "</label></p>";
                    } else {
                        html += "<p style='visibility:hidden;'><label>房间：</label><label>" + meter.Room + "</label></p>";
                    }
                    html += "</div>";

                    html += "<div>";
                    html += "<p><label>表计类别：</label><label>" + meter.MeterType + "</label></p>";
                    html += "<p><label>大小类别：</label><label>" + meter.SizeType + "</label></p>";
                    html += "<p><label>使用类别：</label><label>" + meter.NatureType + "</label></p>";
                    html += "<p><label>性质类别：</label><label>" + meter.UseType + "</label></p>";
                    html += "<p><label>电表位置：</label><label>" + meter.Addr + "</label></p>";
                    html += "</div>";
                    html += "<div>";
                    html += "<button class='btn btn-success' onclick='javascript:$(\"#view\").hide(); $(\"#list\").show();'>确定</button>";
                    html += "</div>";
                    $('#list').hide();
                    $("#view").empty().append(html).show();
                } else {
                    layer.alert("获取数据失败！");
                }
            }
            if (vjson.type == "change") {
                if (vjson.flag == "1") {
                    var meter = JSON.parse(vjson.data);
                    $("#change .loc1").val(meter.MeterLOCNo1Name);
                    $("#change .loc2").val(meter.MeterLOCNo2Name);
                    $("#change .loc3").val(meter.MeterLOCNo3Name);
                    $("#change .loc4").val(meter.MeterLOCNo4Name);
                    if (meter.MeterRMID) {
                        $("#change .loc5").val(meter.MeterRMID);
                        $("#change .loc5").parent().parent().show();
                    } else {
                        $("#change .loc5").parent().parent().hide();
                    }
                    $("#change .meterNo").eq(0).val(meter.MeterNo);
                    $("#change .meterName").eq(0).val(meter.MeterName);
                    $("#change .lastRead").val(meter.MeterReadout);
                    $("#change .rate").val(meter.MeterRate);
                    $("#change .digit").val(meter.MeterDigit);
                    $("#change .meterType").val(meter.MeterType);
                    $("#change .sizeType").val(meter.MeterSize);
                    $("#change .natureType").val(meter.MeterNatureType);
                    $("#change .useType").val(meter.MeterUsageType);
                    $("#change .addr").val(meter.Addr);
                    $("#change").show();
                    $("#list").hide();
                } else if (vjson.flag == "2") {
                    layer.msg(vjson.info);
                } else {
                    layer.msg("数据异常！");
                    console.log(vjson.info);
                }
            }
            if (vjson.type == "changesubmit") {
                if (vjson.flag == "1") {
                    $("#change").hide();
                    $("#list").show();
                    layer.msg("换表成功！");
                } else {
                    layer.alert("换表失败！");
                }
            }
        }
        function view() {
            if ($("#selectKey").val() == "") {
                layer.msg("请选择要查看的数据", { icon: 3, time: 1000 });
                return;
            }
            id = $("#selectKey").val();
            type = "view";
            var submitData = new Object();
            submitData.Type = "view";
            submitData.id = id;
            transmitData(datatostr(submitData));
            return;
        }
        function change() {
            if ($("#selectKey").val() == "") {
                layer.msg("请选择要更换的表计", { icon: 3, time: 1000 });
                return;
            }
            $('#change').validate('reset');
            $('#change').validate('init');
            $('#change input,#change select').val('');
            id = $("#selectKey").val();
            type = "change";
            var submitData = new Object();
            submitData.Type = "change";
            submitData.id = id;
            transmitData(datatostr(submitData));
            return;
        }

        function changesubmit() {
            if ($('#change').validate('submitValidate')) {
                var submitData = new Object();
                submitData.Type = "changesubmit";
                submitData.meterNo = id;
                submitData.endReadout = $("#change .endRead").eq(0).val();
                submitData.reading = $("#change .reading").eq(0).val();
                submitData.newMeterNo = $("#change .meterNo").eq(1).val();
                submitData.newMeterName = $("#change .meterName").eq(1).val();
                submitData.newMeterReadout = $("#change .startRead").eq(0).val();
                submitData.newMeterDigit = $("#change .digit").eq(1).val();
                submitData.newMeterRate = $("#change .rate").eq(1).val();
                submitData.newMeterType = $("#change .meterType").eq(1).val();
                submitData.newMeterSizeType = $("#change .sizeType").eq(1).val();
                submitData.newMeterNatureType = $("#change .natureType").eq(1).val();
                submitData.newMeterUseType = $("#change .useType").eq(1).val();
                submitData.newMeterAddr = $("#change .addr").eq(1).val();
                //submitData.readout = $("").val();
                transmitData(datatostr(submitData));
            }
            return;
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
                layer.msg("请先选择一条计录", { icon: 3, time: 1000 });
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

        var printFlag = 0;
        function print() {
            $(".normalbtn").hide();
            $(".printbtn").show();
            $(".printck").show();
            printFlag = 1;
        }
        function printchoice() {
            var printIndex = 0;
            var printStr = "";
            $("input[name='meterno']").each(function () {
                if ($(this).is(":checked")) {
                    printIndex++;
                    printStr += "'" + $(this).val() + "',";
                }
            });

            //layer.alert(printStr);
            if (printIndex <= 0) {
                layer.msg("请勾选要打印的数据！");
                return;
            }
            printStr = printStr.substr(0, printStr.lastIndexOf(","));
            var submitData = new Object();
            submitData.Type = "label";
            submitData.Meter = printStr;
            transmitData(datatostr(submitData));
        }
        function printall() {
            var submitData = new Object();
            submitData.Type = "label";
            submitData.Meter = "";
            transmitData(datatostr(submitData));
        }
        function printcancel() {
            $(".normalbtn").show();
            $(".printbtn").hide();
            $(".printck").hide();
            $("input[name='meterno']").prop("checked", false);
            printFlag = 0;
        }
        function getall() {
            if ($("#checkall").is(":checked")) {
                $("input[name='meterno']").prop("checked", true);
            } else {
                $("input[name='meterno']").prop("checked", false);
            }
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

        //度数计算
        function calcReadout() {
            if ($("#change .endRead").eq(0).val() != "" && $("#change .lastRead").eq(0).val() != "") {
                var lastRead = parseFloat($("#change .lastRead").eq(0).val());
                var endRead = parseFloat($("#change .endRead").eq(0).val());
                var digit = parseInt($("#change .digit").eq(0).val());
                var reading = $("#change .reading").eq(0);
                if (endRead >= lastRead)
                    reading.val(endRead - lastRead)
                else {
                    if (digit == 4)
                        reading.val(endRead + (9999 - lastRead + 1));
                    else if (digit == 5)
                        reading.val(endRead + (99999 - lastRead + 1));
                    else if (digit == 6)
                        reading.val(endRead + (999999 - lastRead + 1));
                    else if (digit == 7)
                        reading.val(endRead + (9999999 - lastRead + 1));
                    else if (digit == 8)
                        reading.val(endRead + (99999999 - lastRead + 1));
                    else {
                        reading.val("");
                    }
                }
            }
            else {
                reading.val("")
            }
        }
        var trid = "";
        reflist();
    </script>
</body>
</html>
