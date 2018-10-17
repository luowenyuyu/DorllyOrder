<%@ Page Language="C#" AutoEventWireup="true" Inherits="project.Presentation.Op.Contract_Bak,project"  %>
<!DOCTYPE>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="head1" runat="server">
    <title>合同管理</title>    
    <!--[if lt IE 9]>
    <script type="text/javascript" src="../../jscript/html5.js"></script>
    <script type="text/javascript" src="../../jscript/respond.min.js"></script>
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
    <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 业务管理 <span class="c-gray en">&gt;</span> 合同管理 <a class="btn btn-success radius r mr-20" style="line-height:1.6em;margin-top:2px" href="javascript:location.replace(location.href);" title="刷新" ><i class="Hui-iconfont">&#xe68f;</i></a></nav>
    <div id="list" class="pt-5 pr-20 pb-5 pl-20">
	    <div class="cl pd-3 bg-1 bk-gray mt-2"> 
            <span class="l">
                <a href="javascript:;" onclick="insert()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe600;</i> 添加</a>
                <a href="javascript:;" onclick="update()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe60c;</i> 修改</a> 
                <a href="javascript:;" onclick="del()" class="btn btn-danger radius"><i class="Hui-iconfont">&#xe6e2;</i> 删除</a> 
                <a href="javascript:;" onclick="view()" class="btn btn-secondary radius"><i class="Hui-iconfont">&#xe627;</i> 查看</a>&nbsp;&nbsp;&nbsp;
                <a href="javascript:;" onclick="approve()" class="btn btn-primary radius"><i class="Hui-iconfont">&#xe615;</i> 审核</a>
                <a href="javascript:;" onclick="invalid()" class="btn btn-warning radius"><i class="Hui-iconfont">&#xe60b;</i> 作废</a> 
                <input type="hidden" id="selectKey" />
            </span> 
	    </div>
	    <div class="cl pd-10  bk-gray mt-2"> 
		    合同编号&nbsp;<input type="text" class="input-text size-MINI" placeholder="合同编号" id="ContractNoS" style="width:110px">            
            手工合同编号&nbsp;<input type="text" class="input-text size-MINI" placeholder="手工合同编号" id="ContractNoManualS" style="width:110px">
            合同类型&nbsp;<%=ContractTypeStrS %>
            服务商&nbsp;<%=ContractSPNoStrS %>
            客户&nbsp;<input type="text" class="input-text size-MINI" placeholder="" id="ContractCustNoS" style="width:110px">
            退租状态&nbsp;<select class="input-text size-MINI" style="width:110px" id="OffLeaseStatusS"><option value="" selected="selected">全部</option><option value="1">未退租</option><option value="2">已申请</option><option value="3">已办理</option><option value="4">已结算</option></select>
            <br />
            合同签订日期 从&nbsp;<input type="text" class="input-text size-MINI" id="MinContractSignedDate" style="width:110px">
            至&nbsp;<input type="text" class="input-text size-MINI" id="MaxContractSignedDate" style="width:110px">            
            合同到期日期 从&nbsp;<input type="text" class="input-text size-MINI" id="MinContractEndDate" style="width:110px">
            至&nbsp;<input type="text" class="input-text size-MINI" id="MaxContractEndDate" style="width:110px">
            实际退租日期 从&nbsp;<input type="text" class="input-text size-MINI" id="MinOffLeaseActulDate" style="width:110px">
            至&nbsp;<input type="text" class="input-text size-MINI" id="MaxOffLeaseActulDate" style="width:110px">
		    <button type="submit" class="btn btn-success radius" onclick="select()"><i class="Hui-iconfont">&#xe665;</i> 检索</button>
	    </div>
	    <div class="mt-5" id="outerlist">
	    <%=list %>
	    </div>
    </div>
    <div id="edit" class="editdiv" style="display:none;">        
        <div class="itab">
  	        <ul> 
                <li><a href="javascript:void(0)" onclick="show(1)" id="itemtab1" class="selected">基本信息</a></li> 
                <li><a href="javascript:void(0)" onclick="show(2)" id="itemtab2">房屋信息</a></li>
                <li><a href="javascript:void(0)" onclick="show(3)" id="itemtab3">工位信息</a></li>
                <li><a href="javascript:void(0)" onclick="show(4)" id="itemtab4">广告位信息</a></li>
                <li><a href="javascript:void(0)" onclick="show(7)" id="itemtab7">管理费</a></li>
                <li><a href="javascript:void(0)" onclick="show(8)" id="itemtab8">空调费</a></li>
                <li><a href="javascript:void(0)" onclick="show(5)" id="itemtab5">水表信息</a></li>
                <li><a href="javascript:void(0)" onclick="show(6)" id="itemtab6">电表信息</a></li>
                <%--<li><a href="javascript:void(0)" onclick="show(7)" id="itemtab7">变更记录</a></li>--%>
  	        </ul>
        </div>

        <div id="topeditdiv">
            <table class="tabedit">
                <tr>
                    <td class="tdl">合同类型</td>
                    <td class="tdr"><%=ContractTypeStr %></td>
                    <td class="tdl">客户</td>
                    <td class="tdr">
                        <input type="text" class="input-text size-MINI" id="ContractCustName" onblur="checkcust()" />
                        <input type="hidden" id="ContractCustNo" />
                        <img id="ContractCustImg" alt="" src="../../images/view_detail.png" class="view_detail" />
                    </td>
                    <td class="tdl">合同主体</td>
                    <td class="tdr"><%=ContractSPNoStr %></td>
                    <td class="tdl"></td>
                    <td class="tdr"></td>
                </tr>
                <tr>
                    <td class="tdl">合同编号</td>
                    <td class="tdr"><input type="text" id="ContractNo" disabled="disabled" class="input-text size-MINI" /></td>
                    <td class="tdl">手工合同编号</td>
                    <td class="tdr"><input type="text" id="ContractNoManual" class="input-text size-MINI" /></td>
                    <td class="tdl">经办人</td>
                    <td class="tdr"><input type="text" id="ContractHandler" disabled="disabled" class="input-text size-MINI" /></td>
                    <td class="tdl">合同状态</td>
                    <td class="tdr"><input type="text" id="ContractStatus" disabled="disabled" class="input-text size-MINI" /></td>
                </tr>
                <tr>
                    <td class="tdl">合同签订日期</td>
                    <td class="tdr"><input type="text" id="ContractSignedDate" class="input-text size-MINI" /></td>
                    <td class="tdl">生效日期</td>
                    <td class="tdr"><input type="text" id="ContractStartDate" class="input-text size-MINI" /></td>
                    <td class="tdl">到期日期</td>
                    <td class="tdr"><input type="text" id="ContractEndDate" class="input-text size-MINI" /></td>
                    <td class="tdl">客户入场日期</td>
                    <td class="tdr"><input type="text" id="EntryDate"class="input-text size-MINI" /></td>
                </tr>
                <tr>
                    <td class="tdl">租金起收日期</td>
                    <td class="tdr"><input type="text" id="FeeStartDate" class="input-text size-MINI" /></td>
                    <td class="tdl">减免开始日期</td>
                    <td class="tdr"><input type="text" id="ReduceStartDate" class="input-text size-MINI" /></td>
                    <td class="tdl">减免截止日期</td>
                    <td class="tdr"><input type="text" id="ReduceEndDate" class="input-text size-MINI" /></td>
                    <td class="tdl">滞纳金占比</td>
                    <td class="tdr"><input type="text" id="ContractLatefeeRate"class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                </tr>
                <tr>
                    <td class="tdl">房屋押金</td>
                    <td class="tdr"><input type="text" id="RMRentalDeposit" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl">房屋水电押金</td>
                    <td class="tdr"><input type="text" id="RMUtilityDeposit" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl"></td>
                    <td class="tdr"></td>
                    <td class="tdl"></td>
                    <td class="tdr"></td>
                </tr>
                <tr>
                    <td class="tdl">管理费起收日期</td>
                    <td class="tdr"><input type="text" id="PropertyFeeStartDate" class="input-text size-MINI" /></td>
                    <td class="tdl">管理费减免开始日期</td>
                    <td class="tdr"><input type="text" id="PropertyFeeReduceStartDate" class="input-text size-MINI" /></td>
                    <td class="tdl">管理费减免结束日期</td>
                    <td class="tdr"><input type="text" id="PropertyFeeReduceEndDate" class="input-text size-MINI" /></td>
                    <td class="tdl"></td>
                    <td class="tdr"></td>
                </tr>
                <tr>
                    <td class="tdl">水费单价</td>
                    <td class="tdr"><input type="text" id="WaterUnitPrice" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl">电费单价</td>
                    <td class="tdr"><input type="text" id="ElecticityUintPrice" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl">空调费</td>
                    <td class="tdr"><input type="text" id="AirconUnitPrice" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl"></td>
                    <td class="tdr"></td>
                </tr>
                <tr>
                    <td class="tdl">公摊水费</td>
                    <td class="tdr"><input type="text" id="SharedWaterFee" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl">公摊电费</td>
                    <td class="tdr"><input type="text" id="SharedElectricyFee" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl"></td>
                    <td class="tdr"></td>
                    <td class="tdl"></td>
                    <td class="tdr"></td>
                </tr>
                <tr>
                    <td class="tdl">工位押金</td>
                    <td class="tdr"><input type="text" id="WPRentalDeposit" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl">工位电费押金</td>
                    <td class="tdr"><input type="text" id="WPUtilityDeposit" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl"></td>
                    <td class="tdr"></td>
                    <td class="tdl"></td>
                    <td class="tdr"></td>
                </tr>
                <tr>
                    <td class="tdl">工位数量</td>
                    <td class="tdr"><input type="text" id="WPQTY" class="input-text size-MINI" onchange="validInt(this.id)" /></td>
                    <td class="tdl">工位用电额度</td>
                    <td class="tdr"><input type="text" id="WPElectricyLimit" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl">超额用电单价</td>
                    <td class="tdr"><input type="text" id="WPOverElectricyPrice" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl"></td>
                    <td class="tdr"></td>
                </tr>
                <tr>
                    <td class="tdl">广告位数量</td>
                    <td class="tdr"><input type="text" id="BBQTY" class="input-text size-MINI" disabled="disabled" onchange="validInt(this.id)" /></td>
                    <td class="tdl">广告位合同总金额</td>
                    <td class="tdr"><input type="text" id="BBAmount" class="input-text size-MINI" disabled="disabled" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl"></td>
                    <td class="tdr"></td>
                    <td class="tdl"></td>
                    <td class="tdr"></td>
                </tr>
                <tr>
                    <td class="tdl">递增开始时间1</td>
                    <td class="tdr"><input type="text" id="IncreaseStartDate1" class="input-text size-MINI" /></td>
                    <td class="tdl">递增率1</td>
                    <td class="tdr"><input type="text" id="IncreaseRate1" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl">递增开始时间2</td>
                    <td class="tdr"><input type="text" id="IncreaseStartDate2" class="input-text size-MINI" /></td>
                    <td class="tdl">递增率2</td>
                    <td class="tdr"><input type="text" id="IncreaseRate2" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                </tr>
                <tr>
                    <td class="tdl">递增开始时间3</td>
                    <td class="tdr"><input type="text" id="IncreaseStartDate3" class="input-text size-MINI" /></td>
                    <td class="tdl">递增率3</td>
                    <td class="tdr"><input type="text" id="IncreaseRate3" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl">递增开始时间4</td>
                    <td class="tdr"><input type="text" id="IncreaseStartDate4" class="input-text size-MINI" /></td>
                    <td class="tdl">递增率4</td>
                    <td class="tdr"><input type="text" id="IncreaseRate4" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                </tr>
                <tr>
                    <td class="tdl">备注</td>
                    <td class="tdr" colspan="7"><textarea cols="" rows="3" class="textarea required" id="Remark"></textarea></td>
                </tr>
                <tr>
                    <td class="tdl">附件</td>
                    <td class="tdr"><button type="button" class="btn btn-primary radius" id="uploadFiles" onclick="uploadFiles()" style="margin-left:20px;">上传附件</button></td>
                    <td colspan="6"><div id="ContractAttachmentFiles"></div><input type="hidden" id="ContractAttachment" /></td>
                </tr>
            </table>
        </div>

        <div id="bodyeditdiv1">        
            <table class="tabedit">
                <tr>
                    <td class="tdl2">房屋编号</td>
                    <td class="tdr2">
                        <input type="text" class="input-text size-MINI" id="RMID" disabled="disabled" /> <%--onblur="checkRM('RMID2','RM')"--%>
                        <button type="button" class="btn btn-primary radius" id="chooseRMID1">选择</button>
                    </td>
                    <td class="tdl2">所属费用项目</td>
                    <td class="tdr2"><%=SRVNo1Str %></td>
                </tr>
                <tr>
                    <td class="tdl2">出租面积</td>
                    <td class="tdr2"><input type="text" id="RMArea" class="input-text size-MINI" onchange="validDecimal(this.id)" /> ㎡</td>
                    <td class="tdl2">出租单价</td>
                    <td class="tdr2"><input type="text" id="RentalUnitPrice" class="input-text size-MINI" onchange="validDecimal(this.id)" /> 元/㎡/月</td>
                </tr>
                <tr>
                    <td class="tdl2">房屋位置</td>
                    <td colspan="3"><input type="text" id="RMLoc" class="input-text size-MINI" /></td>
                </tr>
                <tr>
                    <td class="tdl2">备注</td>
                    <td colspan="3"><input type="text" id="Remark1" class="input-text size-MINI" /></td>
                </tr>
                <tr>
                    <td colspan="6">
                        <input class="btn btn-primary radius" type="button" id="itemsave1" onclick="itemsave1()" value="保存" />&nbsp;
	                    <input class="btn btn-primary radius" type="button" id="itemclear1" onclick="itemclear1()" value="新增" />&nbsp;
                    </td>
                </tr>
            </table>
            <div style="width:100%;height:5px;"></div>
            <div id="itemlist1"style="width:1058px; height:320px; overflow:auto; margin:0px; padding:0px;"></div>
        </div>

        <div id="bodyeditdiv2">  
            <table class="tabedit">
                <tr>
                    <td class="tdl1">房屋编号</td>
                    <td class="tdr1">
                        <input type="text" class="input-text size-MINI" id="RMID2" disabled="disabled" style="width:150px;" />
                        <button type="button" class="btn btn-primary radius" id="chooseRMID2">选择</button>
                    </td>
                    <td class="tdl1">工位号</td>
                    <td class="tdr1">
                        <input type="text" id="WPNo" class="input-text size-MINI" disabled="disabled" style="width:150px;" />
                        <button type="button" class="btn btn-primary radius" id="chooseWPNo">选择</button>
                    </td>
                    <td class="tdl1">所属费用项目</td>
                    <td class="tdr1"><%=SRVNo2Str %></td>
                </tr>
                <tr>
                    <td class="tdl1">工位数</td>
                    <td class="tdr1"><input type="text" id="WPQTY2" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl1">单价</td>
                    <td class="tdr1"><input type="text" id="WPRentalUnitPrice" class="input-text size-MINI" onchange="validDecimal(this.id)" /></td>
                    <td class="tdl1"></td>
                    <td class="tdr1"></td>
                </tr>
                <tr>
                    <td class="tdl1">工位位置</td>
                    <td colspan="5"><input type="text" id="RMLoc2" class="input-text size-MINI" /></td>
                </tr>
                <tr>
                    <td class="tdl1">备注</td>
                    <td colspan="5"><input type="text" id="Remark2" class="input-text size-MINI"  /></td>
                </tr>
                <tr>
                    <td colspan="6">
                        <input class="btn btn-primary radius" type="button" id="itemsave2" onclick="itemsave2()" value="保存" />&nbsp;
	                    <input class="btn btn-primary radius" type="button" id="itemclear2" onclick="itemclear2()" value="新增" />&nbsp;
                    </td>
                </tr>
            </table>
            <div style="width:100%;height:5px;"></div>
            <div id="itemlist2"style="width:1058px; height:320px; overflow:auto; margin:0px; padding:0px;"></div>
        </div>

        <div id="bodyeditdiv3">        
            <table class="tabedit">
                <tr>
                    <td class="tdl1">广告位编号</td>
                    <td class="tdr1">
                        <input type="text" class="input-text size-MINI" id="BBNo" disabled="disabled" style="width:150px;" />
                        <button type="button" class="btn btn-primary radius" id="chooseBBNo">选择</button>
                    </td>
                    <td class="tdl1">广告位名称</td>
                    <td class="tdr1"><input type="text" id="BBName" disabled="disabled" class="input-text size-MINI" /></td>
                    <td class="tdl1">所属服务项目</td>
                    <td class="tdr1"><%=SRVNo3Str %></td>
                </tr>
                <tr>
                    <td class="tdl1">开始投放日期</td>
                    <td class="tdr1"><input type="text" id="BBStartDate" class="input-text size-MINI" /></td>
                    <td class="tdl1">截止投放日期</td>
                    <td class="tdr1"><input type="text" id="BBEndDate" class="input-text size-MINI" /></td>
                    <td class="tdl1">尺寸大小</td>
                    <td class="tdr1"><input type="text" id="BBSize" class="input-text size-MINI" /></td>
                </tr>
                <tr>
                    <td class="tdl1">租用月数</td>
                    <td class="tdr1"><input type="text" id="BBRentalMonths" class="input-text size-MINI" /></td>
                    <td class="tdl1">单价(元/月)</td>
                    <td class="tdr1"><input type="text" id="RentalUnitPrice3" class="input-text size-MINI" /></td>
                    <td class="tdl1">金额</td>
                    <td class="tdr1"><input type="text" id="RentalAmount" class="input-text size-MINI" /></td>
                </tr>
                <tr>
                    <td class="tdl1">地址</td>
                    <td colspan="5"><input type="text" id="BBAddr" class="input-text size-MINI" /></td>
                </tr>
                <tr>
                    <td class="tdl1">备注</td>
                    <td colspan="5"><input type="text" id="Remark3" class="input-text size-MINI" /></td>
                </tr>
                <tr>
                    <td colspan="6">
                        <input class="btn btn-primary radius" type="button" id="itemsave3" onclick="itemsave3()" value="保存" />&nbsp;
	                    <input class="btn btn-primary radius" type="button" id="itemclear3" onclick="itemclear3()" value="新增" />&nbsp;
                    </td>
                </tr>
            </table>
            <div style="width:100%;height:5px;"></div>
            <div id="itemlist3"style="width:1058px; height:320px; overflow:auto; margin:0px; padding:0px;"></div>
        </div>

        <div id="bodyeditdiv4">        
            <table class="tabedit">
                <tr>
                    <td class="tdl1">房屋编号</td>
                    <td class="tdr1">
                        <input type="text" class="input-text size-MINI" id="RMID4" disabled="disabled" style="width:150px;" />
                        <button type="button" class="btn btn-primary radius" id="chooseRMID4">选择</button>
                    </td>
                    <td class="tdl1">表计编号</td>
                    <td class="tdr1">
                        <input type="text" class="input-text size-MINI" id="WMMeterNo" disabled="disabled" style="width:150px;" />
                        <button type="button" class="btn btn-primary radius" id="chooseWMMeterNo">选择</button>
                    </td>
                    <td class="tdl1">所属费用项目</td>
                    <td class="tdr1"><%=SRVNo4Str %></td>
                </tr>
                <tr>
                    <td class="tdl1">水表起始数</td>
                    <td class="tdr1"><input type="text" id="WMStartReadout" class="input-text size-MINI" /></td>
                    <td class="tdl1">倍率</td>
                    <td class="tdr1"><input type="text" id="WMMeterRate" class="input-text size-MINI" /></td>
                    <td class="tdl1"></td>
                    <td class="tdr1"></td>
                </tr>
                <tr>
                    <td class="tdl1">备注</td>
                    <td colspan="5"><input type="text" id="Remark4" class="input-text size-MINI" /></td>
                </tr>
                <tr>
                    <td colspan="6">
                        <input class="btn btn-primary radius" type="button" id="itemsave4" onclick="itemsave4()" value="保存" />&nbsp;
	                    <input class="btn btn-primary radius" type="button" id="itemclear4" onclick="itemclear4()" value="新增" />&nbsp;
                    </td>
                </tr>
            </table>
            <div style="width:100%;height:5px;"></div>
            <div id="itemlist4"style="width:1058px; height:320px; overflow:auto; margin:0px; padding:0px;"></div>
        </div>

        <div id="bodyeditdiv5">        
            <table class="tabedit">
                <tr>
                    <td class="tdl1">房屋编号</td>
                    <td class="tdr1">
                        <input type="text" class="input-text size-MINI" id="RMID5" disabled="disabled" style="width:150px;" />
                        <button type="button" class="btn btn-primary radius" id="chooseRMID5">选择</button>
                    </td>
                    <td class="tdl1">表计编号</td>
                    <td class="tdr1">
                        <input type="text" class="input-text size-MINI" id="AMMeterNo" disabled="disabled" style="width:150px;" />
                        <button type="button" class="btn btn-primary radius" id="chooseAMMeterNo">选择</button>
                    </td>
                    <td class="tdl1">所属费用项目</td>
                    <td class="tdr1"><%=SRVNo5Str %></td>
                </tr>
                <tr>
                    <td class="tdl1">电表起始数</td>
                    <td class="tdr1"><input type="text" id="AMStartReadout" class="input-text size-MINI" /></td>
                    <td class="tdl1">倍率</td>
                    <td class="tdr1"><input type="text" id="AMMeterRate" class="input-text size-MINI" /></td>
                    <td class="tdl1"></td>
                    <td class="tdr1"></td>
                </tr>
                <tr>
                    <td class="tdl1">备注</td>
                    <td colspan="5"><input type="text" id="Remark5" class="input-text size-MINI" /></td>
                </tr>
                <tr>
                    <td colspan="6">
                        <input class="btn btn-primary radius" type="button" id="itemsave5" onclick="itemsave5()" value="保存" />&nbsp;
	                    <input class="btn btn-primary radius" type="button" id="itemclear5" onclick="itemclear5()" value="新增" />&nbsp;
                    </td>
                </tr>
            </table>
            <div style="width:100%;height:5px;"></div>
            <div id="itemlist5"style="width:1058px; height:320px; overflow:auto; margin:0px; padding:0px;"></div>
        </div>

        <div id="bodyeditdiv6">        
            <table class="tabedit">
                <tr>
                    <td class="tdl1">房屋编号</td>
                    <td class="tdr1">
                        <input type="text" class="input-text size-MINI" id="RMID6" disabled="disabled" style="width:150px;" />
                        <button type="button" class="btn btn-primary radius" id="chooseRMID6">选择</button>
                    </td>
                    <td class="tdl">出租面积</td>
                    <td class="tdr"><input type="text" id="RMArea1" class="input-text size-MINI" /></td>
                    <td class="tdl">单价(元/㎡/月)</td>
                    <td class="tdr"><input type="text" id="UnitPrice1" class="input-text size-MINI" /></td>
                </tr>
                <tr>
                    <td class="tdl1">备注</td>
                    <td colspan="5"><input type="text" id="Remark6" class="input-text size-MINI" /></td>
                </tr>
                <tr>
                    <td colspan="6">
                        <input class="btn btn-primary radius" type="button" id="itemsave6" onclick="itemsave6()" value="保存" />&nbsp;
	                    <input class="btn btn-primary radius" type="button" id="itemclear6" onclick="itemclear6()" value="新增" />&nbsp;
                    </td>
                </tr>
            </table>
            <div style="width:100%;height:5px;"></div>
            <div id="itemlist6"style="width:1058px; height:320px; overflow:auto; margin:0px; padding:0px;"></div>
        </div>

        <div id="bodyeditdiv7">        
            <table class="tabedit">
                <tr>
                    <td class="tdl1">房屋编号</td>
                    <td class="tdr1">
                        <input type="text" class="input-text size-MINI" id="RMID7" disabled="disabled" style="width:150px;" />
                        <button type="button" class="btn btn-primary radius" id="chooseRMID7">选择</button>
                    </td>
                    <td class="tdl">出租面积</td>
                    <td class="tdr"><input type="text" id="RMArea2" class="input-text size-MINI" /></td>
                    <td class="tdl">单价(元/㎡/月)</td>
                    <td class="tdr"><input type="text" id="UnitPrice2" class="input-text size-MINI" /></td>
                </tr>
                <tr>
                    <td class="tdl1">备注</td>
                    <td colspan="5"><input type="text" id="Remark7" class="input-text size-MINI" /></td>
                </tr>
                <tr>
                    <td colspan="6">
                        <input class="btn btn-primary radius" type="button" id="itemsave7" onclick="itemsave7()" value="保存" />&nbsp;
	                    <input class="btn btn-primary radius" type="button" id="itemclear7" onclick="itemclear7()" value="新增" />&nbsp;
                    </td>
                </tr>
            </table>
            <div style="width:100%;height:5px;"></div>
            <div id="itemlist7"style="width:1058px; height:320px; overflow:auto; margin:0px; padding:0px;"></div>
        </div>
        
        
        <div style="margin-top:10px;">
            <input class="btn btn-primary radius" type="button" id="submit" onclick="submit()" value="保存退出" />
            <input class="btn btn-primary radius" type="button" id="submit1" onclick="submit1()" value="保存继续" />
	        <input class="btn btn-default radius" type="button" id="cancel" onclick="cancel()" value="&nbsp;&nbsp;取&nbsp;&nbsp;消&nbsp;&nbsp;" />
        </div>
        <br />
        <br />
    </div>    
    <script type="text/javascript" src="../../jscript/jquery-1.10.2.js"></script> 
    <script type="text/javascript" src="../../jscript/script_ajax.js"></script>
    <script type="text/javascript" src="../../jscript/script_common.js"></script>
    <script type="text/javascript" src="../../jscript/JsInputDate.js"></script>
    <script type="text/javascript" src="../../jscript/json2.js"></script>
    <script type="text/javascript" src="../../jscript/H-ui.js"></script> 
    <script type="text/javascript" src="../../jscript/H-ui.admin.js"></script> 
    <script type="text/javascript" src="../../lib/layer/layer.js"></script> 
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
            if (vjson.type == "delete") {
                if (vjson.flag == "1") {
                    $("#outerlist").html(vjson.liststr);
                    $("#selectKey").val("");
                    reflist();
                }
                else if (vjson.flag == "3") {
                    layer.alert("当前订单已审核，不能删除！");
                }
                else {
                    layer.alert("数据操作出错！");
                }
                return;
            }
            if (vjson.type == "insert") {
                if (vjson.flag == "1") {
                    $("#itemlist1").html(vjson.itemlist1);
                    $("#itemlist2").html(vjson.itemlist2);
                    $("#itemlist3").html(vjson.itemlist3);
                    $("#itemlist4").html(vjson.itemlist4);
                    $("#itemlist5").html(vjson.itemlist5);
                    $("#itemlist6").html(vjson.itemlist6);
                    $("#itemlist7").html(vjson.itemlist7);
                    itemclear1();
                    itemclear2();
                    itemclear3();
                    itemclear4();
                    itemclear5();
                    itemclear6();
                    itemclear7();
                    show(1);
                    id = "";
                    type = "insert";
                    copyid = "";

                    $("#ContractNo").val("");
                    $("#ContractType").val("");
                    $("#ContractSPNo").val("");
                    $("#ContractSPName").val("");
                    $("#ContractCustNo").val("");
                    $("#ContractCustName").val("");
                    $("#ContractNoManual").val("");
                    $("#ContractHandler").val("<%=userName %>");
                    $("#ContractSignedDate").val("<%=date %>");
                    $("#ContractStartDate").val("<%=date %>");
                    $("#ContractEndDate").val("");
                    $("#EntryDate").val("");
                    $("#FeeStartDate").val("<%=date %>");
                    $("#ReduceStartDate").val("");
                    $("#ReduceEndDate").val("");
                    $("#ContractLatefeeRate").val("");
                    $("#RMRentalDeposit").val("");
                    $("#RMUtilityDeposit").val("");
                    $("#PropertyFeeStartDate").val("<%=date %>");
                    $("#PropertyFeeReduceStartDate").val("");
                    $("#PropertyFeeReduceEndDate").val("");
                    $("#WaterUnitPrice").val("");
                    $("#ElecticityUintPrice").val("");
                    $("#AirconUnitPrice").val("");
                    $("#SharedWaterFee").val("");
                    $("#SharedElectricyFee").val("");
                    $("#WPRentalDeposit").val("");
                    $("#WPUtilityDeposit").val("");
                    $("#WPQTY").val("");
                    $("#WPElectricyLimit").val("");
                    $("#WPOverElectricyPrice").val("");
                    $("#BBQTY").val("");
                    $("#BBAmount").val("");
                    $("#IncreaseStartDate1").val("");
                    $("#IncreaseRate1").val("");
                    $("#IncreaseStartDate2").val("");
                    $("#IncreaseRate2").val("");
                    $("#IncreaseStartDate3").val("");
                    $("#IncreaseRate3").val("");
                    $("#IncreaseStartDate4").val("");
                    $("#IncreaseRate4").val("");
                    $("#ContractStatus").val("制单");
                    $("#Remark").val("");

                    $("#ContractAttachment").val("");
                    $("#ContractAttachmentFiles").html("");

                    $("#uploadFiles").css("display", "");

                    //$("#ContractNo").attr("disabled", false);
                    $("#list").css("display", "none");
                    $("#edit").css("display", "");

                    $("#itemsave1").css("display", "");
                    $("#itemclear1").css("display", "");
                    $("#itemsave2").css("display", "");
                    $("#itemclear2").css("display", "");
                    $("#itemsave3").css("display", "");
                    $("#itemclear3").css("display", "");
                    $("#itemsave4").css("display", "");
                    $("#itemclear4").css("display", "");
                    $("#itemsave5").css("display", "");
                    $("#itemclear5").css("display", "");
                    $("#itemsave6").css("display", "");
                    $("#itemclear6").css("display", "");
                    $("#itemsave7").css("display", "");
                    $("#itemclear7").css("display", "");

                    $("#submit").css("display", "");
                    $("#submit1").css("display", "");
                }
                else {
                    layer.alert("数据操作出错！");
                }
                return;
            }
            if (vjson.type == "submit") {
                if (vjson.flag == "1") {
                    if (vjson.submittp == "submit") {
                        $("#outerlist").html(vjson.liststr);
                        $("#selectKey").val("");
                        $("#list").css("display", "");
                        $("#edit").css("display", "none");
                        reflist();
                    }
                    else {
                        id = vjson.RowPointer;
                        type = "update";
                        layer.msg("保存成功！", { icon: 3, time: 1000 });

                        if (copyid != "") {
                            $("#itemlist1").html(vjson.itemlist1);
                            $("#itemlist2").html(vjson.itemlist2);
                            $("#itemlist3").html(vjson.itemlist3);
                            $("#itemlist4").html(vjson.itemlist4);
                            $("#itemlist5").html(vjson.itemlist5);
                            $("#itemlist6").html(vjson.itemlist6);
                            $("#itemlist7").html(vjson.itemlist7);
                        }
                        show(2);
                        copyid = "";
                    }
                }
                else if (vjson.flag == "3") {
                    layer.msg("此合同编号已存在！", { icon: 3, time: 1000 });
                }
                else {
                    layer.alert("数据操作出错！");
                }
                return;
            }
            if (vjson.type == "approve") {
                if (vjson.flag == "1") {
                    if (vjson.status == "1") {
                        $("#" + vjson.id + " td").eq(8).html("<label style=\"color:red;\">制单</label>");
                        layer.alert("取消审核成功！");
                    }
                    else {
                        $("#" + vjson.id + " td").eq(8).html("<label style=\"color:blue;\">已审核</label>");
                        layer.alert("审核成功！");
                    }
                }
                else if (vjson.flag == "3") {
                    layer.alert("当前合同状态不允许审核！");
                }
                else if (vjson.flag == "4") {
                    layer.alert("当前合同已生成订单，不能取消审核！");
                }
                else {
                    layer.alert("数据操作出错！");
                }
                return;
            }
            if (vjson.type == "invalid") {
                if (vjson.flag == "1") {
                    if (vjson.status == "1") {
                        $("#" + vjson.id + " td").eq(8).html("<label style=\"color:red;\">制单</label>");
                        layer.alert("取消作废成功！");
                    }
                    else {
                        $("#" + vjson.id + " td").eq(8).html("<label style=\"color:gray;\">已作废</label>");
                        layer.alert("作废成功！");
                    }
                }
                else if (vjson.flag == "3") {
                    layer.alert("当前合同状态不允许作废！");
                }
                else {
                    layer.alert("数据操作出错！");
                }
                return;
            }
            if (vjson.type == "update") {
                if (vjson.flag == "1") {
                    $("#ContractNo").val(vjson.ContractNo);
                    $("#ContractType").val(vjson.ContractType);
                    $("#ContractSPNo").val(vjson.ContractSPNo);
                    $("#ContractCustNo").val(vjson.ContractCustNo);
                    $("#ContractCustName").val(vjson.ContractCustName);
                    $("#ContractNoManual").val(vjson.ContractNoManual);
                    $("#ContractHandler").val(vjson.ContractHandler);
                    $("#ContractSignedDate").val(vjson.ContractSignedDate);
                    $("#ContractStartDate").val(vjson.ContractStartDate);
                    $("#ContractEndDate").val(vjson.ContractEndDate);
                    $("#EntryDate").val(vjson.EntryDate);
                    $("#FeeStartDate").val(vjson.FeeStartDate);
                    $("#ReduceStartDate").val(vjson.ReduceStartDate);
                    $("#ReduceEndDate").val(vjson.ReduceEndDate);
                    $("#ContractLatefeeRate").val(vjson.ContractLatefeeRate);
                    $("#RMRentalDeposit").val(vjson.RMRentalDeposit);
                    $("#RMUtilityDeposit").val(vjson.RMUtilityDeposit);
                    $("#PropertyFeeStartDate").val(vjson.PropertyFeeStartDate);
                    $("#PropertyFeeReduceStartDate").val(vjson.PropertyFeeReduceStartDate);
                    $("#PropertyFeeReduceEndDate").val(vjson.PropertyFeeReduceEndDate);
                    $("#WaterUnitPrice").val(vjson.WaterUnitPrice);
                    $("#ElecticityUintPrice").val(vjson.ElecticityUintPrice);
                    $("#AirconUnitPrice").val(vjson.AirconUnitPrice);
                    $("#SharedWaterFee").val(vjson.SharedWaterFee);
                    $("#SharedElectricyFee").val(vjson.SharedElectricyFee);
                    $("#WPRentalDeposit").val(vjson.WPRentalDeposit);
                    $("#WPUtilityDeposit").val(vjson.WPUtilityDeposit);
                    $("#WPQTY").val(vjson.WPQTY);
                    $("#WPElectricyLimit").val(vjson.WPElectricyLimit);
                    $("#WPOverElectricyPrice").val(vjson.WPOverElectricyPrice);
                    $("#BBQTY").val(vjson.BBQTY);
                    $("#BBAmount").val(vjson.BBAmount);
                    $("#IncreaseStartDate1").val(vjson.IncreaseStartDate1);
                    $("#IncreaseRate1").val(vjson.IncreaseRate1);
                    $("#IncreaseStartDate2").val(vjson.IncreaseStartDate2);
                    $("#IncreaseRate2").val(vjson.IncreaseRate2);
                    $("#IncreaseStartDate3").val(vjson.IncreaseStartDate3);
                    $("#IncreaseRate3").val(vjson.IncreaseRate3);
                    $("#IncreaseStartDate4").val(vjson.IncreaseStartDate4);
                    $("#IncreaseRate4").val(vjson.IncreaseRate4);
                    $("#ContractStatus").val(vjson.ContractStatus);
                    $("#Remark").val(vjson.Remark);

                    $("#ContractAttachment").val(vjson.ContractAttachment);
                    $("#ContractAttachmentFiles").html(vjson.files);

                    $("#uploadFiles").css("display", "");

                    copyid = "";
                    show(1);
                    itemclear1();
                    itemclear2();
                    itemclear3();
                    itemclear4();
                    itemclear5();
                    itemclear6();
                    itemclear7();
                    $("#itemlist1").html(vjson.itemlist1);
                    $("#itemlist2").html(vjson.itemlist2);
                    $("#itemlist3").html(vjson.itemlist3);
                    $("#itemlist4").html(vjson.itemlist4);
                    $("#itemlist5").html(vjson.itemlist5);
                    $("#itemlist6").html(vjson.itemlist6);
                    $("#itemlist7").html(vjson.itemlist7);

                    //$("#ContractNo").attr("disabled", true);
                    $("#list").css("display", "none");
                    $("#edit").css("display", "");

                    $("#itemsave1").css("display", "");
                    $("#itemclear1").css("display", "");
                    $("#itemsave2").css("display", "");
                    $("#itemclear2").css("display", "");
                    $("#itemsave3").css("display", "");
                    $("#itemclear3").css("display", "");
                    $("#itemsave4").css("display", "");
                    $("#itemclear4").css("display", "");
                    $("#itemsave5").css("display", "");
                    $("#itemclear5").css("display", "");
                    $("#itemsave6").css("display", "");
                    $("#itemclear6").css("display", "");
                    $("#itemsave7").css("display", "");
                    $("#itemclear7").css("display", "");
                    $("#submit").css("display", "");
                    $("#submit1").css("display", "");
                }
                else if (vjson.flag == "3") {
                    layer.alert("当前订单已审核，不能修改！");
                }
                else {
                    layer.alert("数据操作出错！");
                }
                return;
            }
            if (vjson.type == "view") {
                if (vjson.flag == "1") {
                    $("#ContractNo").val(vjson.ContractNo);
                    $("#ContractType").val(vjson.ContractType);
                    $("#ContractSPNo").val(vjson.ContractSPNo);
                    $("#ContractCustNo").val(vjson.ContractCustNo);
                    $("#ContractCustName").val(vjson.ContractCustName);
                    $("#ContractNoManual").val(vjson.ContractNoManual);
                    $("#ContractHandler").val(vjson.ContractHandler);
                    $("#ContractSignedDate").val(vjson.ContractSignedDate);
                    $("#ContractStartDate").val(vjson.ContractStartDate);
                    $("#ContractEndDate").val(vjson.ContractEndDate);
                    $("#EntryDate").val(vjson.EntryDate);
                    $("#FeeStartDate").val(vjson.FeeStartDate);
                    $("#ReduceStartDate").val(vjson.ReduceStartDate);
                    $("#ReduceEndDate").val(vjson.ReduceEndDate);
                    $("#ContractLatefeeRate").val(vjson.ContractLatefeeRate);
                    $("#RMRentalDeposit").val(vjson.RMRentalDeposit);
                    $("#RMUtilityDeposit").val(vjson.RMUtilityDeposit);
                    $("#PropertyFeeStartDate").val(vjson.PropertyFeeStartDate);
                    $("#PropertyFeeReduceStartDate").val(vjson.PropertyFeeReduceStartDate);
                    $("#PropertyFeeReduceEndDate").val(vjson.PropertyFeeReduceEndDate);
                    $("#WaterUnitPrice").val(vjson.WaterUnitPrice);
                    $("#ElecticityUintPrice").val(vjson.ElecticityUintPrice);
                    $("#AirconUnitPrice").val(vjson.AirconUnitPrice);
                    $("#SharedWaterFee").val(vjson.SharedWaterFee);
                    $("#SharedElectricyFee").val(vjson.SharedElectricyFee);
                    $("#WPRentalDeposit").val(vjson.WPRentalDeposit);
                    $("#WPUtilityDeposit").val(vjson.WPUtilityDeposit);
                    $("#WPQTY").val(vjson.WPQTY);
                    $("#WPElectricyLimit").val(vjson.WPElectricyLimit);
                    $("#WPOverElectricyPrice").val(vjson.WPOverElectricyPrice);
                    $("#BBQTY").val(vjson.BBQTY);
                    $("#BBAmount").val(vjson.BBAmount);
                    $("#IncreaseStartDate1").val(vjson.IncreaseStartDate1);
                    $("#IncreaseRate1").val(vjson.IncreaseRate1);
                    $("#IncreaseStartDate2").val(vjson.IncreaseStartDate2);
                    $("#IncreaseRate2").val(vjson.IncreaseRate2);
                    $("#IncreaseStartDate3").val(vjson.IncreaseStartDate3);
                    $("#IncreaseRate3").val(vjson.IncreaseRate3);
                    $("#IncreaseStartDate4").val(vjson.IncreaseStartDate4);
                    $("#IncreaseRate4").val(vjson.IncreaseRate4);
                    $("#ContractStatus").val(vjson.ContractStatus);
                    $("#Remark").val(vjson.Remark);

                    $("#ContractAttachment").val(vjson.ContractAttachment);
                    $("#ContractAttachmentFiles").html(vjson.files);

                    $("#uploadFiles").css("display", "none");

                    copyid = "";
                    show(1);
                    itemclear1();
                    itemclear2();
                    itemclear3();
                    itemclear4();
                    itemclear5();
                    itemclear6();
                    itemclear7();
                    $("#itemlist1").html(vjson.itemlist1);
                    $("#itemlist2").html(vjson.itemlist2);
                    $("#itemlist3").html(vjson.itemlist3);
                    $("#itemlist4").html(vjson.itemlist4);
                    $("#itemlist5").html(vjson.itemlist5);
                    $("#itemlist6").html(vjson.itemlist6);
                    $("#itemlist7").html(vjson.itemlist7);

                    //$("#ContractNo").attr("disabled", true);
                    $("#list").css("display", "none");
                    $("#edit").css("display", "");

                    $("#itemsave1").css("display", "none");
                    $("#itemclear1").css("display", "none");
                    $("#itemsave2").css("display", "none");
                    $("#itemclear2").css("display", "none");
                    $("#itemsave3").css("display", "none");
                    $("#itemclear3").css("display", "none");
                    $("#itemsave4").css("display", "none");
                    $("#itemclear4").css("display", "none");
                    $("#itemsave5").css("display", "none");
                    $("#itemclear5").css("display", "none");
                    $("#itemsave6").css("display", "none");
                    $("#itemclear6").css("display", "none");
                    $("#itemsave7").css("display", "none");
                    $("#itemclear7").css("display", "none");
                    $("#submit").css("display", "none");
                    $("#submit1").css("display", "none");
                }
                else {
                    layer.alert("数据操作出错！");
                }
                return;
            }
            if (vjson.type == "checkcust") {
                if (vjson.flag == "1") {
                    $("#ContractCustNo").val(vjson.Code);
                    $("#ContractCustName").val(vjson.Name);
                }
                else if (vjson.flag == "3") {
                    layer.alert("未找到记录，确认是否输入正确！");
                }
                else {
                    layer.alert("提取数据出错！");
                }
                return;
            }
            if (vjson.type == "check") {
                if (vjson.flag == "1") {
                    $("#" + vjson.id).val(vjson.Code);
                    //$("#" + vjson.id + "Name").val(vjson.Name);
                }
                else if (vjson.flag == "3") {
                    layer.alert("未找到记录，确认是否输入正确！");
                }
                else {
                    layer.alert("提取数据出错！");
                }
                return;
            }

            if (vjson.type == "itemsave1") {
                if (vjson.flag == "1") {
                    itemclear1();
                    $("#RMID").focus();
                    $("#itemlist1").html(vjson.liststr);
                }
                else {
                    layer.alert("提交数据出错！");
                }
                return;
            }
            if (vjson.type == "itemupdate1") {
                if (vjson.flag == "1") {
                    $("#RMID").val(vjson.RMID);
                    $("#SRVNo1").val(vjson.SRVNo);
                    $("#RMArea").val(vjson.RMArea);
                    $("#RentalUnitPrice").val(vjson.RentalUnitPrice);
                    $("#RMLoc").val(vjson.RMLoc);
                    $("#Remark1").val(vjson.Remark);

                    itemid1 = vjson.ItemId;
                    itemtp1 = "update";
                }
                else {
                    layer.alert("修改数据出错！");
                }
                return;
            }
            if (vjson.type == "itemdel1") {
                if (vjson.flag == "1") {
                    $("#itemlist1").html(vjson.liststr);
                    itemclear1();
                }
                else {
                    layer.alert("删除数据出错！");
                }
                return;
            }
            if (vjson.type == "getrmid") {
                if (vjson.flag == "1") {
                    $("#RMArea").val(vjson.RMBuildSize);
                    $("#RMLoc").val(vjson.RMAddr);
                }
                else {
                    layer.alert("获取数据出错！");
                }
                return;
            }


            if (vjson.type == "itemsave2") {
                if (vjson.flag == "1") {
                    itemclear2();
                    $("#RMID2").focus();
                    $("#itemlist2").html(vjson.liststr);
                }
                else {
                    layer.alert("提交数据出错！");
                }
                return;
            }
            if (vjson.type == "itemupdate2") {
                if (vjson.flag == "1") {
                    $("#RMID2").val(vjson.RMID);
                    $("#WPNo").val(vjson.WPNo);
                    $("#SRVNo2").val(vjson.SRVNo);
                    $("#WPQTY2").val(vjson.WPQTY);
                    $("#WPRentalUnitPrice").val(vjson.WPRentalUnitPrice);
                    $("#RMLoc2").val(vjson.RMLoc);
                    $("#Remark2").val(vjson.Remark);

                    itemid2 = vjson.ItemId;
                    itemtp2 = "update";
                }
                else {
                    layer.alert("修改数据出错！");
                }
                return;
            }
            if (vjson.type == "itemdel2") {
                if (vjson.flag == "1") {
                    $("#itemlist2").html(vjson.liststr);
                    itemclear2();
                }
                else {
                    layer.alert("删除数据出错！");
                }
                return;
            }
            if (vjson.type == "getwpno") {
                if (vjson.flag == "1") {
                    $("#WPQTY2").val(vjson.WPSeat);
                    $("#WPRentalUnitPrice").val(vjson.WPSeatPrice);
                    $("#RMLoc2").val(vjson.WPAddr);
                }
                else {
                    layer.alert("获取数据出错！");
                }
                return;
            }


            if (vjson.type == "itemsave3") {
                if (vjson.flag == "1") {
                    itemclear3();
                    $("#BBNo").focus();
                    $("#itemlist3").html(vjson.liststr);

                    $("#BBQTY").val(vjson.BBQTY);
                    $("#BBAmount").val(vjson.BBAmount);
                }
                else {
                    layer.alert("提交数据出错！");
                }
                return;
            }
            if (vjson.type == "itemupdate3") {
                if (vjson.flag == "1") {
                    $("#SRVNo3").val(vjson.SRVNo);
                    $("#BBNo").val(vjson.BBNo);
                    $("#BBName").val(vjson.BBName);
                    $("#BBSize").val(vjson.BBSize);
                    $("#BBAddr").val(vjson.BBAddr);
                    $("#BBStartDate").val(vjson.BBStartDate);
                    $("#BBEndDate").val(vjson.BBEndDate);
                    $("#BBRentalMonths").val(vjson.BBRentalMonths);
                    $("#RentalUnitPrice3").val(vjson.RentalUnitPrice);
                    $("#RentalAmount").val(vjson.RentalAmount);
                    $("#Remark3").val(vjson.Remark);

                    itemid3 = vjson.ItemId;
                    itemtp3 = "update";
                }
                else {
                    layer.alert("修改数据出错！");
                }
                return;
            }
            if (vjson.type == "itemdel3") {
                if (vjson.flag == "1") {
                    $("#itemlist3").html(vjson.liststr);
                    $("#BBQTY").val(vjson.BBQTY);
                    $("#BBAmount").val(vjson.BBAmount);
                    itemclear3();
                }
                else {
                    layer.alert("删除数据出错！");
                }
                return;
            }
            if (vjson.type == "getbbno") {
                if (vjson.flag == "1") {
                    $("#BBSize").val(vjson.BBSize);
                    $("#BBAddr").val(vjson.BBAddr);
                    $("#RentalUnitPrice3").val(vjson.BBINPrice);
                }
                else {
                    layer.alert("获取数据出错！");
                }
                return;
            }


            if (vjson.type == "itemsave4") {
                if (vjson.flag == "1") {
                    itemclear4();
                    $("#RMID4").focus();
                    $("#itemlist4").html(vjson.liststr);
                }
                else {
                    layer.alert("提交数据出错！");
                }
                return;
            }
            if (vjson.type == "itemupdate4") {
                if (vjson.flag == "1") {
                    $("#RMID4").val(vjson.RMID);
                    $("#SRVNo4").val(vjson.SRVNo);
                    $("#WMMeterNo").val(vjson.WMMeterNo);
                    $("#WMStartReadout").val(vjson.WMStartReadout);
                    $("#WMMeterRate").val(vjson.WMMeterRate);
                    $("#Remark4").val(vjson.Remark);

                    itemid4 = vjson.ItemId;
                    itemtp4 = "update";
                }
                else {
                    layer.alert("修改数据出错！");
                }
                return;
            }
            if (vjson.type == "itemdel4") {
                if (vjson.flag == "1") {
                    $("#itemlist4").html(vjson.liststr);
                    itemclear4();
                }
                else {
                    layer.alert("删除数据出错！");
                }
                return;
            }
            if (vjson.type == "getwmmeterno") {
                if (vjson.flag == "1") {
                    $("#WMStartReadout").val(vjson.MeterReadout);
                    $("#WMMeterRate").val(vjson.MeterRate);
                }
                else {
                    layer.alert("获取数据出错！");
                }
                return;
            }


            if (vjson.type == "itemsave5") {
                if (vjson.flag == "1") {
                    itemclear5();
                    $("#RMID5").focus();
                    $("#itemlist5").html(vjson.liststr);
                }
                else {
                    layer.alert("提交数据出错！");
                }
                return;
            }
            if (vjson.type == "itemupdate5") {
                if (vjson.flag == "1") {
                    $("#RMID5").val(vjson.RMID);
                    $("#SRVNo5").val(vjson.SRVNo);
                    $("#AMMeterNo").val(vjson.AMMeterNo);
                    $("#AMStartReadout").val(vjson.AMStartReadout);
                    $("#AMMeterRate").val(vjson.AMMeterRate);
                    $("#Remark5").val(vjson.Remark);

                    itemid5 = vjson.ItemId;
                    itemtp5 = "update";
                }
                else {
                    layer.alert("修改数据出错！");
                }
                return;
            }
            if (vjson.type == "itemdel5") {
                if (vjson.flag == "1") {
                    $("#itemlist5").html(vjson.liststr);
                    itemclear5();
                }
                else {
                    layer.alert("删除数据出错！");
                }
                return;
            }
            if (vjson.type == "getammeterno") {
                if (vjson.flag == "1") {
                    $("#AMStartReadout").val(vjson.MeterReadout);
                    $("#AMMeterRate").val(vjson.MeterRate);
                }
                else {
                    layer.alert("获取数据出错！");
                }
                return;
            }


            if (vjson.type == "itemsave6") {
                if (vjson.flag == "1") {
                    itemclear6();
                    $("#RMID6").focus();
                    $("#itemlist6").html(vjson.liststr);
                }
                else {
                    layer.alert("提交数据出错！");
                }
                return;
            }
            if (vjson.type == "itemupdate6") {
                if (vjson.flag == "1") {
                    $("#RMID6").val(vjson.RMID6);
                    $("#RMArea1").val(vjson.RMArea);
                    $("#UnitPrice1").val(vjson.UnitPrice);
                    $("#Remark6").val(vjson.Remark);

                    itemid6 = vjson.ItemId;
                    itemtp6 = "update";
                }
                else {
                    layer.alert("修改数据出错！");
                }
                return;
            }
            if (vjson.type == "itemdel6") {
                if (vjson.flag == "1") {
                    $("#itemlist6").html(vjson.liststr);
                    itemclear6();
                }
                else {
                    layer.alert("删除数据出错！");
                }
                return;
            }
            if (vjson.type == "getpropertyfee") {
                if (vjson.flag == "1") {
                    $("#RMArea1").val(vjson.RMBuildSize);
                    $("#UnitPrice1").val(vjson.PropertyFee);
                }
                else {
                    layer.alert("获取数据出错！");
                }
                return;
            }


            if (vjson.type == "itemsave7") {
                if (vjson.flag == "1") {
                    itemclear7();
                    $("#RMID7").focus();
                    $("#itemlist7").html(vjson.liststr);
                }
                else {
                    layer.alert("提交数据出错！");
                }
                return;
            }
            if (vjson.type == "itemupdate7") {
                if (vjson.flag == "1") {
                    $("#RMID7").val(vjson.RMID6);
                    $("#RMArea2").val(vjson.RMArea);
                    $("#UnitPrice2").val(vjson.UnitPrice);
                    $("#Remark7").val(vjson.Remark);

                    itemid7 = vjson.ItemId;
                    itemtp7 = "update";
                }
                else {
                    layer.alert("修改数据出错！");
                }
                return;
            }
            if (vjson.type == "itemdel7") {
                if (vjson.flag == "1") {
                    $("#itemlist7").html(vjson.liststr);
                    itemclear7();
                }
                else {
                    layer.alert("删除数据出错！");
                }
                return;
            }
            if (vjson.type == "getairconditionfee") {
                if (vjson.flag == "1") {
                    $("#RMArea2").val(vjson.RMBuildSize);
                    $("#UnitPrice2").val(vjson.AirConditionFee);
                }
                else {
                    layer.alert("获取数据出错！");
                }
                return;
            }
        }

        function insert() {
            var submitData = new Object();
            submitData.Type = "insert";
            transmitData(datatostr(submitData));
            return;
        }
        function update() {
            if ($("#selectKey").val() == "") {
                layer.msg("请先选择一条记录", { icon: 3, time: 1000 });
                return;
            }
            id = $("#selectKey").val();
            type = "update";
            var submitData = new Object();
            submitData.Type = "update";
            submitData.id = id;

            transmitData(datatostr(submitData));
            return;
        }
        function edit(rid) {
            id = rid;
            type = "update";
            var submitData = new Object();
            submitData.Type = "edit";
            submitData.id = id;

            transmitData(datatostr(submitData));
            return;
        }
        function view() {
            if ($("#selectKey").val() == "") {
                layer.msg("请先选择一条记录", { icon: 3, time: 1000 });
                return;
            }
            id = $("#selectKey").val();
            var submitData = new Object();
            submitData.Type = "view";
            submitData.id = id;

            transmitData(datatostr(submitData));
            return;
        }
        function submit() {
            //if ($("#ContractNo").val() == "") {
            //    layer.msg("请填写合同编号！", { icon: 7, time: 1000 });
            //    $("#ContractNo").focus();
            //    return;
            //}
            if ($("#ContractType").val() == "") {
                layer.msg("请选择合同类型！", { icon: 7, time: 1000 });
                $("#ContractType").focus();
                return;
            }
            if ($("#ContractSPNo").val() == "") {
                layer.msg("请选择合同主体！", { icon: 7, time: 1000 });
                $("#ContractSPNo").focus();
                return;
            }
            if ($("#ContractCustNo").val() == "") {
                layer.msg("请选择客户！", { icon: 7, time: 1000 });
                $("#ContractCustNo").focus();
                return;
            }
            if ($("#ContractSignedDate").val() == "") {
                layer.msg("请选择合同签订日期！", { icon: 7, time: 1000 });
                $("#ContractSignedDate").focus();
                return;
            }
            if ($("#ContractStartDate").val() == "") {
                layer.msg("请选择合同生效日期！", { icon: 7, time: 1000 });
                $("#ContractStartDate").focus();
                return;
            }
            if ($("#ContractEndDate").val() == "") {
                layer.msg("请选择合同到期日期！", { icon: 7, time: 1000 });
                $("#ContractEndDate").focus();
                return;
            }
            var submitData = new Object();
            submitData.Type = "submit";
            submitData.submittp = "submit";
            submitData.id = id;

            submitData.ContractNo = $("#ContractNo").val();
            submitData.ContractType = $("#ContractType").val();
            submitData.ContractSPNo = $("#ContractSPNo").val();
            submitData.ContractCustNo = $("#ContractCustNo").val();
            submitData.ContractNoManual = $("#ContractNoManual").val();
            submitData.ContractHandler = $("#ContractHandler").val();
            submitData.ContractSignedDate = $("#ContractSignedDate").val();
            submitData.ContractStartDate = $("#ContractStartDate").val();
            submitData.ContractEndDate = $("#ContractEndDate").val();
            submitData.EntryDate = $("#EntryDate").val();
            submitData.FeeStartDate = $("#FeeStartDate").val();
            submitData.ReduceStartDate = $("#ReduceStartDate").val();
            submitData.ReduceEndDate = $("#ReduceEndDate").val();
            submitData.ContractLatefeeRate = $("#ContractLatefeeRate").val();
            submitData.RMRentalDeposit = $("#RMRentalDeposit").val();
            submitData.RMUtilityDeposit = $("#RMUtilityDeposit").val();
            submitData.PropertyFeeStartDate = $("#PropertyFeeStartDate").val();
            submitData.PropertyFeeReduceStartDate = $("#PropertyFeeReduceStartDate").val();
            submitData.PropertyFeeReduceEndDate = $("#PropertyFeeReduceEndDate").val();
            submitData.WaterUnitPrice = $("#WaterUnitPrice").val();
            submitData.ElecticityUintPrice = $("#ElecticityUintPrice").val();
            submitData.AirconUnitPrice = $("#AirconUnitPrice").val();
            submitData.SharedWaterFee = $("#SharedWaterFee").val();
            submitData.SharedElectricyFee = $("#SharedElectricyFee").val();
            submitData.WPRentalDeposit = $("#WPRentalDeposit").val();
            submitData.WPUtilityDeposit = $("#WPUtilityDeposit").val();
            submitData.WPQTY = $("#WPQTY").val();
            submitData.WPElectricyLimit = $("#WPElectricyLimit").val();
            submitData.WPOverElectricyPrice = $("#WPOverElectricyPrice").val();
            submitData.BBQTY = $("#BBQTY").val();
            submitData.BBAmount = $("#BBAmount").val();
            submitData.IncreaseStartDate1 = $("#IncreaseStartDate1").val();
            submitData.IncreaseRate1 = $("#IncreaseRate1").val();
            submitData.IncreaseStartDate2 = $("#IncreaseStartDate2").val();
            submitData.IncreaseRate2 = $("#IncreaseRate2").val();
            submitData.IncreaseStartDate3 = $("#IncreaseStartDate3").val();
            submitData.IncreaseRate3 = $("#IncreaseRate3").val();
            submitData.IncreaseStartDate4 = $("#IncreaseStartDate4").val();
            submitData.IncreaseRate4 = $("#IncreaseRate4").val();
            submitData.Remark = $("#Remark").val();
            submitData.ContractAttachment = $("#ContractAttachment").val();
            
            submitData.tp = type;
            submitData.copyid = copyid;
            submitData.ContractNoS = $("#ContractNoS").val();
            submitData.ContractNoManualS = $("#ContractNoManualS").val();
            submitData.ContractTypeS = $("#ContractTypeS").val();
            submitData.ContractSPNoS = $("#ContractSPNoS").val();
            submitData.ContractCustNoS = $("#ContractCustNoS").val();
            submitData.MinContractSignedDate = $("#MinContractSignedDate").val();
            submitData.MaxContractSignedDate = $("#MaxContractSignedDate").val();
            submitData.MinContractEndDate = $("#MinContractEndDate").val();
            submitData.MaxContractEndDate = $("#MaxContractEndDate").val();
            submitData.OffLeaseStatusS = $("#OffLeaseStatusS").val();
            submitData.MinOffLeaseActulDate = $("#MinOffLeaseActulDate").val();
            submitData.MaxOffLeaseActulDate = $("#MaxOffLeaseActulDate").val();
            submitData.page = page;
            transmitData(datatostr(submitData));
            return;
        }
        function submit1() {
            if ($("#ContractNo").val() == "") {
                layer.msg("请填写合同编号！", { icon: 7, time: 1000 });
                $("#ContractNo").focus();
                return;
            }
            if ($("#ContractType").val() == "") {
                layer.msg("请选择合同类型！", { icon: 7, time: 1000 });
                $("#ContractType").focus();
                return;
            }
            if ($("#ContractSPNo").val() == "") {
                layer.msg("请选择合同主体！", { icon: 7, time: 1000 });
                $("#ContractSPNo").focus();
                return;
            }
            if ($("#ContractCustNo").val() == "") {
                layer.msg("请选择客户！", { icon: 7, time: 1000 });
                $("#ContractCustNo").focus();
                return;
            }
            if ($("#ContractSignedDate").val() == "") {
                layer.msg("请选择合同签订日期！", { icon: 7, time: 1000 });
                $("#ContractSignedDate").focus();
                return;
            }
            if ($("#ContractStartDate").val() == "") {
                layer.msg("请选择合同生效日期！", { icon: 7, time: 1000 });
                $("#ContractStartDate").focus();
                return;
            }
            if ($("#ContractEndDate").val() == "") {
                layer.msg("请选择合同到期日期！", { icon: 7, time: 1000 });
                $("#ContractEndDate").focus();
                return;
            }
            var submitData = new Object();
            submitData.Type = "submit";
            submitData.submittp = "submit1";
            submitData.id = id;

            submitData.ContractNo = $("#ContractNo").val();
            submitData.ContractType = $("#ContractType").val();
            submitData.ContractSPNo = $("#ContractSPNo").val();
            submitData.ContractCustNo = $("#ContractCustNo").val();
            submitData.ContractNoManual = $("#ContractNoManual").val();
            submitData.ContractHandler = $("#ContractHandler").val();
            submitData.ContractSignedDate = $("#ContractSignedDate").val();
            submitData.ContractStartDate = $("#ContractStartDate").val();
            submitData.ContractEndDate = $("#ContractEndDate").val();
            submitData.EntryDate = $("#EntryDate").val();
            submitData.FeeStartDate = $("#FeeStartDate").val();
            submitData.ReduceStartDate = $("#ReduceStartDate").val();
            submitData.ReduceEndDate = $("#ReduceEndDate").val();
            submitData.ContractLatefeeRate = $("#ContractLatefeeRate").val();
            submitData.RMRentalDeposit = $("#RMRentalDeposit").val();
            submitData.RMUtilityDeposit = $("#RMUtilityDeposit").val();
            submitData.PropertyFeeStartDate = $("#PropertyFeeStartDate").val();
            submitData.PropertyFeeReduceStartDate = $("#PropertyFeeReduceStartDate").val();
            submitData.PropertyFeeReduceEndDate = $("#PropertyFeeReduceEndDate").val();
            submitData.WaterUnitPrice = $("#WaterUnitPrice").val();
            submitData.ElecticityUintPrice = $("#ElecticityUintPrice").val();
            submitData.AirconUnitPrice = $("#AirconUnitPrice").val();
            submitData.SharedWaterFee = $("#SharedWaterFee").val();
            submitData.SharedElectricyFee = $("#SharedElectricyFee").val();
            submitData.WPRentalDeposit = $("#WPRentalDeposit").val();
            submitData.WPUtilityDeposit = $("#WPUtilityDeposit").val();
            submitData.WPQTY = $("#WPQTY").val();
            submitData.WPElectricyLimit = $("#WPElectricyLimit").val();
            submitData.WPOverElectricyPrice = $("#WPOverElectricyPrice").val();
            submitData.BBQTY = $("#BBQTY").val();
            submitData.BBAmount = $("#BBAmount").val();
            submitData.IncreaseStartDate1 = $("#IncreaseStartDate1").val();
            submitData.IncreaseRate1 = $("#IncreaseRate1").val();
            submitData.IncreaseStartDate2 = $("#IncreaseStartDate2").val();
            submitData.IncreaseRate2 = $("#IncreaseRate2").val();
            submitData.IncreaseStartDate3 = $("#IncreaseStartDate3").val();
            submitData.IncreaseRate3 = $("#IncreaseRate3").val();
            submitData.IncreaseStartDate4 = $("#IncreaseStartDate4").val();
            submitData.IncreaseRate4 = $("#IncreaseRate4").val();
            submitData.Remark = $("#Remark").val();
            submitData.ContractAttachment = $("#ContractAttachment").val();

            submitData.tp = type;
            submitData.copyid = copyid;
            submitData.ContractNoS = $("#ContractNoS").val();
            submitData.ContractNoManualS = $("#ContractNoManualS").val();
            submitData.ContractTypeS = $("#ContractTypeS").val();
            submitData.ContractSPNoS = $("#ContractSPNoS").val();
            submitData.ContractCustNoS = $("#ContractCustNoS").val();
            submitData.MinContractSignedDate = $("#MinContractSignedDate").val();
            submitData.MaxContractSignedDate = $("#MaxContractSignedDate").val();
            submitData.MinContractEndDate = $("#MinContractEndDate").val();
            submitData.MaxContractEndDate = $("#MaxContractEndDate").val();
            submitData.OffLeaseStatusS = $("#OffLeaseStatusS").val();
            submitData.MinOffLeaseActulDate = $("#MinOffLeaseActulDate").val();
            submitData.MaxOffLeaseActulDate = $("#MaxOffLeaseActulDate").val();
            submitData.page = page;
            transmitData(datatostr(submitData));
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

                submitData.ContractNoS = $("#ContractNoS").val();
                submitData.ContractNoManualS = $("#ContractNoManualS").val();
                submitData.ContractTypeS = $("#ContractTypeS").val();
                submitData.ContractSPNoS = $("#ContractSPNoS").val();
                submitData.ContractCustNoS = $("#ContractCustNoS").val();
                submitData.MinContractSignedDate = $("#MinContractSignedDate").val();
                submitData.MaxContractSignedDate = $("#MaxContractSignedDate").val();
                submitData.MinContractEndDate = $("#MinContractEndDate").val();
                submitData.MaxContractEndDate = $("#MaxContractEndDate").val();
                submitData.OffLeaseStatusS = $("#OffLeaseStatusS").val();
                submitData.MinOffLeaseActulDate = $("#MinOffLeaseActulDate").val();
                submitData.MaxOffLeaseActulDate = $("#MaxOffLeaseActulDate").val();
                submitData.page = page;
                transmitData(datatostr(submitData));
                layer.close(index);
            });
            return;
        }
        function approve() {
            if ($("#selectKey").val() == "") {
                layer.msg("请先选择一条记录", { icon: 3, time: 1000 });
                return;
            }
            layer.confirm('确认要审核吗？', function (index) {
                var submitData = new Object();
                submitData.Type = "approve";
                submitData.id = $("#selectKey").val();
                transmitData(datatostr(submitData));
                layer.close(index);
            });
            return;
        }
        function invalid() {
            if ($("#selectKey").val() == "") {
                layer.msg("请先选择一条记录", { icon: 3, time: 1000 });
                return;
            }
            layer.confirm('确认要作废吗？', function (index) {
                var submitData = new Object();
                submitData.Type = "invalid";
                submitData.id = $("#selectKey").val();
                transmitData(datatostr(submitData));
                layer.close(index);
            });
            return;
        }
        function select() {
            var submitData = new Object();
            submitData.Type = "select";
            submitData.ContractNoS = $("#ContractNoS").val();
            submitData.ContractNoManualS = $("#ContractNoManualS").val();
            submitData.ContractTypeS = $("#ContractTypeS").val();
            submitData.ContractSPNoS = $("#ContractSPNoS").val();
            submitData.ContractCustNoS = $("#ContractCustNoS").val();
            submitData.MinContractSignedDate = $("#MinContractSignedDate").val();
            submitData.MaxContractSignedDate = $("#MaxContractSignedDate").val();
            submitData.MinContractEndDate = $("#MinContractEndDate").val();
            submitData.MaxContractEndDate = $("#MaxContractEndDate").val();
            submitData.OffLeaseStatusS = $("#OffLeaseStatusS").val();
            submitData.MinOffLeaseActulDate = $("#MinOffLeaseActulDate").val();
            submitData.MaxOffLeaseActulDate = $("#MaxOffLeaseActulDate").val();
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

        function uploadFiles() {
            var temp = "../Base/Upload.html?action=upload&id=uploadFiles";
            layer_show("文件上传", temp, 400, 200);
        }

        function showPic(name, id) {
            $("#ContractAttachmentFiles").append("<span style=\"margin-left:10px;\"><a href=\"..\\..\\upload\\" + name + "\">" + name + "</a>&nbsp;" +
                "<button id=\"" + RandomName() + "\" onclick=\"deletefile(this.id,'" + name + "')\">删除</button></span>");
            $("#ContractAttachment").val($("#ContractAttachment").val() + "<file>" + name + "</file>");
            layer.closeAll();
        }

        function deletefile(controlid,filename) {
            layer.confirm('确定要删除此附件吗？', function (index) {
                $("#" + controlid).parent().remove();
                $("#ContractAttachment").val($("#ContractAttachment").val().replace("<file>" + filename + "</file>",""));
                layer.close(index);
            });
        }


        function itemsave1() {
            if ($("#RMID").val() == "") {
                layer.msg("请选择房间编号！", { icon: 7, time: 1500 });
                $("#RMID").focus();
                return;
            }
            if ($("#SRVNo1").val() == "") {
                layer.msg("请选择所属费用项目！", { icon: 7, time: 1500 });
                $("#SRVNo1").focus();
                return;
            }
            if ($("#RMArea").val() == "") {
                layer.msg("请填写出租面积！", { icon: 7, time: 1500 });
                $("#RMArea").focus();
                return;
            }
            if ($("#RentalUnitPrice").val() == "") {
                layer.msg("请填写出租单价！", { icon: 7, time: 1500 });
                $("#RentalUnitPrice").focus();
                return;
            }
            var submitData = new Object();
            submitData.Type = "itemsave1";
            submitData.id = id;
            submitData.itemid = itemid1;
            submitData.itemtp = itemtp1;
            submitData.RMID = $("#RMID").val();
            submitData.SRVNo = $("#SRVNo1").val();
            submitData.RMArea = $("#RMArea").val();
            submitData.RentalUnitPrice = $("#RentalUnitPrice").val();
            submitData.RMLoc = $("#RMLoc").val();
            submitData.Remark = $("#Remark1").val();

            transmitData(datatostr(submitData));
            return;
        }
        function itemupdate1(itemid) {
            var submitData = new Object();
            submitData.Type = "itemupdate1";
            submitData.itemid = itemid;
            transmitData(datatostr(submitData));
            return;
        }
        function itemdel1(itemid) {
            layer.confirm('确认要删除吗？', function (index) {
                var submitData = new Object();
                submitData.Type = "itemdel1";
                submitData.id = id;
                submitData.itemid = itemid;
                transmitData(datatostr(submitData));
                layer.close(index);
            });
            return;
        }
        function itemclear1() {
            itemtp1 = "insert";
            itemid1 = "";

            $("#RMID").val("");
            $("#SRVNo1").val("");
            $("#RMArea").val("");
            $("#RentalUnitPrice").val("");
            $("#RMLoc").val("");
            $("#Remark1").val("");
        }


        function itemsave2() {
            if ($("#RMID2").val() == "") {
                layer.msg("请选择房间编号！", { icon: 7, time: 1500 });
                $("#RMID2").focus();
                return;
            }
            if ($("#WPNo").val() == "") {
                layer.msg("请选择工位编号！", { icon: 7, time: 1500 });
                $("#WPNo").focus();
                return;
            }
            if ($("#SRVNo2").val() == "") {
                layer.msg("请选择所属费用项目！", { icon: 7, time: 1500 });
                $("#SRVNo2").focus();
                return;
            }
            if ($("#WPQTY2").val() == "") {
                layer.msg("请填写工位数量！", { icon: 7, time: 1500 });
                $("#WPQTY2").focus();
                return;
            }
            if ($("#WPRentalUnitPrice").val() == "") {
                layer.msg("请填写工位单价！", { icon: 7, time: 1500 });
                $("#WPRentalUnitPrice").focus();
                return;
            }
            var submitData = new Object();
            submitData.Type = "itemsave2";
            submitData.id = id;
            submitData.itemid = itemid2;
            submitData.itemtp = itemtp2;
            submitData.RMID = $("#RMID2").val();
            submitData.WPNo = $("#WPNo").val();
            submitData.SRVNo = $("#SRVNo2").val();
            submitData.WPQTY = $("#WPQTY2").val();
            submitData.WPRentalUnitPrice = $("#WPRentalUnitPrice").val();
            submitData.RMLoc = $("#RMLoc2").val();
            submitData.Remark = $("#Remark2").val();

            transmitData(datatostr(submitData));
            return;
        }
        function itemupdate2(itemid) {
            var submitData = new Object();
            submitData.Type = "itemupdate2";
            submitData.itemid = itemid;
            transmitData(datatostr(submitData));
            return;
        }
        function itemdel2(itemid) {
            layer.confirm('确认要删除吗？', function (index) {
                var submitData = new Object();
                submitData.Type = "itemdel2";
                submitData.id = id;
                submitData.itemid = itemid;
                transmitData(datatostr(submitData));
                layer.close(index);
            });
            return;
        }
        function itemclear2() {
            itemtp2 = "insert";
            itemid2 = "";

            $("#RMID2").val("");
            $("#WPNo").val("");
            $("#SRVNo2").val("");
            $("#WPQTY2").val("");
            $("#WPRentalUnitPrice").val("");
            $("#RMLoc2").val("");
            $("#Remark2").val("");
        }


        function itemsave3() {
            if ($("#BBNo").val() == "") {
                layer.msg("请选择广告位编号！", { icon: 7, time: 1500 });
                $("#BBNo").focus();
                return;
            }
            if ($("#SRVNo3").val() == "") {
                layer.msg("请选择所属费用项目！", { icon: 7, time: 1500 });
                $("#SRVNo3").focus();
                return;
            }
            if ($("#BBRentalMonths").val() == "") {
                layer.msg("请填写租用月数！", { icon: 7, time: 1500 });
                $("#BBRentalMonths").focus();
                return;
            }
            if ($("#RentalUnitPrice3").val() == "") {
                layer.msg("请填写单价！", { icon: 7, time: 1500 });
                $("#RentalUnitPrice3").focus();
                return;
            }
            var submitData = new Object();
            submitData.Type = "itemsave3";
            submitData.id = id;
            submitData.itemid = itemid3;
            submitData.itemtp = itemtp3;
            submitData.SRVNo = $("#SRVNo3").val();
            submitData.BBNo = $("#BBNo").val();
            submitData.BBName = $("#BBName").val();
            submitData.BBSize = $("#BBSize").val();
            submitData.BBAddr = $("#BBAddr").val();
            submitData.BBStartDate = $("#BBStartDate").val();
            submitData.BBEndDate = $("#BBEndDate").val();
            submitData.BBRentalMonths = $("#BBRentalMonths").val();
            submitData.RentalUnitPrice = $("#RentalUnitPrice3").val();
            submitData.RentalAmount = $("#RentalAmount").val();
            submitData.Remark = $("#Remark3").val();
            transmitData(datatostr(submitData));
            return;
        }
        function itemupdate3(itemid) {
            var submitData = new Object();
            submitData.Type = "itemupdate3";
            submitData.itemid = itemid;
            transmitData(datatostr(submitData));
            return;
        }
        function itemdel3(itemid) {
            layer.confirm('确认要删除吗？', function (index) {
                var submitData = new Object();
                submitData.Type = "itemdel3";
                submitData.id = id;
                submitData.itemid = itemid;
                transmitData(datatostr(submitData));
                layer.close(index);
            });
            return;
        }
        function itemclear3() {
            itemtp3 = "insert";
            itemid3 = "";

            $("#SRVNo3").val("");
            $("#BBNo").val("");
            $("#BBName").val("");
            $("#BBSize").val("");
            $("#BBAddr").val("");
            $("#BBStartDate").val("");
            $("#BBEndDate").val("");
            $("#BBRentalMonths").val("");
            $("#RentalUnitPrice3").val("");
            $("#RentalAmount").val("");
            $("#Remark3").val("");
        }


        function itemsave4() {
            if ($("#RMID4").val() == "") {
                layer.msg("请选择房屋编号！", { icon: 7, time: 1500 });
                $("#RMID4").focus();
                return;
            }
            if ($("#SRVNo4").val() == "") {
                layer.msg("请选择所属费用项目！", { icon: 7, time: 1500 });
                $("#SRVNo4").focus();
                return;
            }
            if ($("#WMMeterNo").val() == "") {
                layer.msg("请选择水表编号！", { icon: 7, time: 1500 });
                $("#WMMeterNo").focus();
                return;
            }
            if ($("#WMMeterRate").val() == "") {
                layer.msg("请填写倍率！", { icon: 7, time: 1500 });
                $("#WMMeterRate").focus();
                return;
            }
            var submitData = new Object();
            submitData.Type = "itemsave4";
            submitData.id = id;
            submitData.itemid = itemid4;
            submitData.itemtp = itemtp4;
            submitData.RMID = $("#RMID4").val();
            submitData.SRVNo = $("#SRVNo4").val();
            submitData.WMMeterNo = $("#WMMeterNo").val();
            submitData.WMStartReadout = $("#WMStartReadout").val();
            submitData.WMMeterRate = $("#WMMeterRate").val();
            submitData.Remark = $("#Remark4").val();
            transmitData(datatostr(submitData));
            return;
        }
        function itemupdate4(itemid) {
            var submitData = new Object();
            submitData.Type = "itemupdate4";
            submitData.itemid = itemid;
            transmitData(datatostr(submitData));
            return;
        }
        function itemdel4(itemid) {
            layer.confirm('确认要删除吗？', function (index) {
                var submitData = new Object();
                submitData.Type = "itemdel4";
                submitData.id = id;
                submitData.itemid = itemid;
                transmitData(datatostr(submitData));
                layer.close(index);
            });
            return;
        }
        function itemclear4() {
            itemtp4 = "insert";
            itemid4 = "";

            $("#RMID4").val("");
            $("#SRVNo4").val("");
            $("#WMMeterNo").val("");
            $("#WMStartReadout").val("");
            $("#WMMeterRate").val("");
            $("#Remark4").val("");
        }


        function itemsave5() {
            if ($("#RMID5").val() == "") {
                layer.msg("请选择房屋编号！", { icon: 7, time: 1500 });
                $("#RMID5").focus();
                return;
            }
            if ($("#SRVNo5").val() == "") {
                layer.msg("请选择所属费用项目！", { icon: 7, time: 1500 });
                $("#SRVNo5").focus();
                return;
            }
            if ($("#AMMeterNo").val() == "") {
                layer.msg("请选择水表编号！", { icon: 7, time: 1500 });
                $("#AMMeterNo").focus();
                return;
            }
            if ($("#AMMeterRate").val() == "") {
                layer.msg("请填写倍率！", { icon: 7, time: 1500 });
                $("#AMMeterRate").focus();
                return;
            }
            var submitData = new Object();
            submitData.Type = "itemsave5";
            submitData.id = id;
            submitData.itemid = itemid5;
            submitData.itemtp = itemtp5;
            submitData.RMID = $("#RMID5").val();
            submitData.SRVNo = $("#SRVNo5").val();
            submitData.AMMeterNo = $("#AMMeterNo").val();
            submitData.AMStartReadout = $("#AMStartReadout").val();
            submitData.AMMeterRate = $("#AMMeterRate").val();
            submitData.Remark = $("#Remark5").val();
            transmitData(datatostr(submitData));
            return;
        }
        function itemupdate5(itemid) {
            var submitData = new Object();
            submitData.Type = "itemupdate5";
            submitData.itemid = itemid;
            transmitData(datatostr(submitData));
            return;
        }
        function itemdel5(itemid) {
            layer.confirm('确认要删除吗？', function (index) {
                var submitData = new Object();
                submitData.Type = "itemdel5";
                submitData.id = id;
                submitData.itemid = itemid;
                transmitData(datatostr(submitData));
                layer.close(index);
            });
            return;
        }
        function itemclear5() {
            itemtp5 = "insert";
            itemid5 = "";

            $("#RMID5").val("");
            $("#SRVNo5").val("");
            $("#AMMeterNo").val("");
            $("#AMStartReadout").val("");
            $("#AMMeterRate").val("");
            $("#Remark5").val("");
        }


        function itemsave6() {
            if ($("#RMID6").val() == "") {
                layer.msg("请选择房屋编号！", { icon: 7, time: 1500 });
                $("#RMID6").focus();
                return;
            }
            if ($("#RMArea1").val() == "") {
                layer.msg("请填写出租面积！", { icon: 7, time: 1500 });
                $("#RMArea1").focus();
                return;
            }
            if ($("#UnitPrice1").val() == "") {
                layer.msg("请填写单价！", { icon: 7, time: 1500 });
                $("#UnitPrice1").focus();
                return;
            }
            var submitData = new Object();
            submitData.Type = "itemsave6";
            submitData.id = id;
            submitData.itemid = itemid6;
            submitData.itemtp = itemtp6;
            submitData.RMID = $("#RMID6").val();
            submitData.RMArea = $("#RMArea1").val();
            submitData.UnitPrice = $("#UnitPrice1").val();
            submitData.Remark = $("#Remark6").val();
            transmitData(datatostr(submitData));
            return;
        }
        function itemupdate6(itemid) {
            var submitData = new Object();
            submitData.Type = "itemupdate6";
            submitData.itemid = itemid;
            transmitData(datatostr(submitData));
            return;
        }
        function itemdel6(itemid) {
            layer.confirm('确认要删除吗？', function (index) {
                var submitData = new Object();
                submitData.Type = "itemdel6";
                submitData.id = id;
                submitData.itemid = itemid;
                transmitData(datatostr(submitData));
                layer.close(index);
            });
            return;
        }
        function itemclear6() {
            itemtp6 = "insert";
            itemid6 = "";
        }


        function itemsave7() {
            if ($("#RMID7").val() == "") {
                layer.msg("请选择房屋编号！", { icon: 7, time: 1500 });
                $("#RMID7").focus();
                return;
            }
            if ($("#RMArea2").val() == "") {
                layer.msg("请填写出租面积！", { icon: 7, time: 1500 });
                $("#RMArea2").focus();
                return;
            }
            if ($("#UnitPrice2").val() == "") {
                layer.msg("请填写单价！", { icon: 7, time: 1500 });
                $("#UnitPrice2").focus();
                return;
            }
            var submitData = new Object();
            submitData.Type = "itemsave7";
            submitData.id = id;
            submitData.itemid = itemid7;
            submitData.itemtp = itemtp7;
            submitData.RMID = $("#RMID7").val();
            submitData.RMArea = $("#RMArea2").val();
            submitData.UnitPrice = $("#UnitPrice2").val();
            submitData.Remark = $("#Remark7").val();
            transmitData(datatostr(submitData));
            return;
        }
        function itemupdate7(itemid) {
            var submitData = new Object();
            submitData.Type = "itemupdate7";
            submitData.itemid = itemid;
            transmitData(datatostr(submitData));
            return;
        }
        function itemdel7(itemid) {
            layer.confirm('确认要删除吗？', function (index) {
                var submitData = new Object();
                submitData.Type = "itemdel7";
                submitData.id = id;
                submitData.itemid = itemid;
                transmitData(datatostr(submitData));
                layer.close(index);
            });
            return;
        }
        function itemclear7() {
            itemtp6 = "insert";
            itemid6 = "";
        }

        var page = 1;
        function jump(pageindex) {
            page = pageindex;
            var submitData = new Object();
            submitData.Type = "select";
            submitData.ContractNoS = $("#ContractNoS").val();
            submitData.ContractNoManualS = $("#ContractNoManualS").val();
            submitData.ContractTypeS = $("#ContractTypeS").val();
            submitData.ContractSPNoS = $("#ContractSPNoS").val();
            submitData.ContractCustNoS = $("#ContractCustNoS").val();
            submitData.MinContractSignedDate = $("#MinContractSignedDate").val();
            submitData.MaxContractSignedDate = $("#MaxContractSignedDate").val();
            submitData.MinContractEndDate = $("#MinContractEndDate").val();
            submitData.MaxContractEndDate = $("#MaxContractEndDate").val();
            submitData.OffLeaseStatusS = $("#OffLeaseStatusS").val();
            submitData.MinOffLeaseActulDate = $("#MinOffLeaseActulDate").val();
            submitData.MaxOffLeaseActulDate = $("#MaxOffLeaseActulDate").val();
            submitData.page = page;
            transmitData(datatostr(submitData));
            return;
        }

        function show(page) {
            if (page == 1) {
                $('#itemtab2').removeClass("selected");
                $('#itemtab3').removeClass("selected");
                $('#itemtab4').removeClass("selected");
                $('#itemtab5').removeClass("selected");
                $('#itemtab6').removeClass("selected");
                $('#itemtab7').removeClass("selected");
                $('#itemtab8').removeClass("selected");
                $("#bodyeditdiv1").hide();
                $("#bodyeditdiv2").hide();
                $("#bodyeditdiv3").hide();
                $("#bodyeditdiv4").hide();
                $("#bodyeditdiv5").hide();
                $("#bodyeditdiv6").hide();
                $("#bodyeditdiv7").hide();

                $('#itemtab1').addClass("selected");
                $("#topeditdiv").show();
            }
            else {
                if (id == "") {
                    layer.msg("请先保存表头信息！", { icon: 3, time: 1000 });
                    return;
                }

                $('#itemtab1').removeClass("selected");
                $('#itemtab2').removeClass("selected");
                $('#itemtab3').removeClass("selected");
                $('#itemtab4').removeClass("selected");
                $('#itemtab5').removeClass("selected");
                $('#itemtab6').removeClass("selected");
                $('#itemtab7').removeClass("selected");
                $('#itemtab8').removeClass("selected");
                $("#topeditdiv").hide();
                $("#bodyeditdiv1").hide();
                $("#bodyeditdiv2").hide();
                $("#bodyeditdiv3").hide();
                $("#bodyeditdiv4").hide();
                $("#bodyeditdiv5").hide();
                $("#bodyeditdiv6").hide();
                $("#bodyeditdiv7").hide();

                if (page == "2") {
                    $('#itemtab2').addClass("selected");
                    $("#bodyeditdiv1").show();
                }
                else if (page == "3") {
                    $('#itemtab3').addClass("selected");
                    $("#bodyeditdiv2").show();
                }
                else if (page == "4") {
                    $('#itemtab4').addClass("selected");
                    $("#bodyeditdiv3").show();
                }
                else if (page == "5") {
                    $('#itemtab5').addClass("selected");
                    $("#bodyeditdiv4").show();
                }
                else if (page == "6") {
                    $('#itemtab6').addClass("selected");
                    $("#bodyeditdiv5").show();
                }
                else if (page == "7") {
                    $('#itemtab7').addClass("selected");
                    $("#bodyeditdiv6").show();
                }
                else if (page == "8") {
                    $('#itemtab8').addClass("selected");
                    $("#bodyeditdiv7").show();
                }
            }
        }
        
        $("#ContractCustImg").click(function () {
            ChooseBasic("ContractCustNo", "Cust");
        });
        $("#chooseRMID1").click(function () {
            var temp = "../Base/ChooseRMID.aspx?id=RMID";
            layer_show("选择页面", temp, 800, 630);
        });
        $("#chooseRMID2").click(function () {
            var temp = "../Base/ChooseRMID.aspx?id=RMID2";
            layer_show("选择页面", temp, 800, 630);
        });
        $("#chooseWPNo").click(function () {
            if ($("#RMID2").val() == "") {
                layer.msg("请先选择房间编号", { icon: 3, time: 1000 });
                return;
            }
            var temp = "../Base/ChooseWPNo.aspx?id=WPNo&RMID=" + $("#RMID2").val();
            layer_show("选择页面", temp, 800, 600);
        });
        $("#chooseBBNo").click(function () {
            var temp = "../Base/ChooseBasic.aspx?type=Billboard&id=BBNo";
            layer_show("选择页面", temp, 900, 600);
        });
        $("#chooseRMID4").click(function () {
            var temp = "../Base/ChooseRMID.aspx?id=RMID4";
            layer_show("选择页面", temp, 800, 630);
        });
        $("#chooseWMMeterNo").click(function () {
            if ($("#RMID4").val() == "") {
                layer.msg("请先选择房间编号", { icon: 3, time: 1000 });
                return;
            }
            var temp = "../Base/ChooseMeter.aspx?id=WMMeterNo&MeterType=wm&RMID="+$("#RMID4").val();
            layer_show("选择页面", temp, 800, 600);
        });
        $("#chooseRMID5").click(function () {
            var temp = "../Base/ChooseRMID.aspx?id=RMID5";
            layer_show("选择页面", temp, 800, 630);
        });
        $("#chooseAMMeterNo").click(function () {
            if ($("#RMID5").val() == "") {
                layer.msg("请先选择房间编号", { icon: 3, time: 1000 });
                return;
            }
            var temp = "../Base/ChooseMeter.aspx?id=AMMeterNo&MeterType=am&RMID=" + $("#RMID5").val();
            layer_show("选择页面", temp, 800, 600);
        });
        $("#chooseRMID6").click(function () {
            var temp = "../Base/ChooseRMID.aspx?id=RMID6";
            layer_show("选择页面", temp, 800, 630);
        });
        $("#chooseRMID7").click(function () {
            var temp = "../Base/ChooseRMID.aspx?id=RMID7";
            layer_show("选择页面", temp, 800, 630);
        });
        function choose(id, labels, values) {
            if (labels != "" && labels != undefined && labels != "undefined") {
                if (id == "ContractCustNo") {
                    $("#ContractCustName").val(values);
                    $("#ContractCustNo").val(labels);
                }
                else if (id == "RMID2" || id == "RMID4" || id == "RMID5") {
                    $("#" + id).val(labels);
                }
                else if (id == "RMID6") {
                    $("#" + id).val(labels);

                    var submitData = new Object();
                    submitData.Type = "getpropertyfee";
                    submitData.rmid = $("#" + id).val();
                    transmitData(datatostr(submitData));
                }
                else if (id == "RMID7") {
                    $("#" + id).val(labels);

                    var submitData = new Object();
                    submitData.Type = "getairconditionfee";
                    submitData.rmid = $("#" + id).val();
                    transmitData(datatostr(submitData));
                }
                else if (id == "RMID") {
                    $("#" + id).val(labels);

                    var submitData = new Object();
                    submitData.Type = "getrmid";
                    submitData.rmid = $("#" + id).val();
                    transmitData(datatostr(submitData));
                }
                else if (id == "WPNo") {
                    $("#" + id).val(labels);

                    var submitData = new Object();
                    submitData.Type = "getwpno";
                    submitData.wpid = $("#" + id).val();
                    transmitData(datatostr(submitData));
                }
                else if (id == "WMMeterNo") {
                    $("#" + id).val(labels);

                    var submitData = new Object();
                    submitData.Type = "getwmmeterno";
                    submitData.meterid = $("#" + id).val();
                    transmitData(datatostr(submitData));
                }
                else if (id == "AMMeterNo") {
                    $("#" + id).val(labels);

                    var submitData = new Object();
                    submitData.Type = "getammeterno";
                    submitData.meterid = $("#" + id).val();
                    transmitData(datatostr(submitData));
                }
                else if (id == "BBNo") {
                    $("#" + id).val(labels);
                    $("#BBName").val(values);

                    var submitData = new Object();
                    submitData.Type = "getbbno";
                    submitData.bbid = $("#" + id).val();
                    transmitData(datatostr(submitData));
                }
            }
        }

        function checkRM(id, type) {
            if ($("#" + id).val() == "") {
                return;
            }
            var submitData = new Object();
            submitData.Type = "check";
            submitData.val = $("#" + id).val();
            submitData.tp = type;
            submitData.id = id;
            transmitData(datatostr(submitData));
            return;
        }
        function checkcust() {
            if ($("#ContractCustName").val() == "") {
                $("#ContractCustNo").val("");
                return;
            }
            var submitData = new Object();
            submitData.Type = "checkcust";
            submitData.val = $("#ContractCustName").val();
            submitData.tp = "Cust";
            transmitData(datatostr(submitData));
            return;
        }
        

        jQuery(function () {
            var ContractSignedDate = new JsInputDate("ContractSignedDate");
            ContractSignedDate.setDisabled(false);
            ContractSignedDate.setWidth("130px");

            var ContractStartDate = new JsInputDate("ContractStartDate");
            ContractStartDate.setDisabled(false);
            ContractStartDate.setWidth("130px");

            var ContractEndDate = new JsInputDate("ContractEndDate");
            ContractEndDate.setDisabled(false);
            ContractEndDate.setWidth("130px");

            var EntryDate = new JsInputDate("EntryDate");
            EntryDate.setDisabled(false);
            EntryDate.setWidth("130px");

            var FeeStartDate = new JsInputDate("FeeStartDate");
            FeeStartDate.setDisabled(false);
            FeeStartDate.setWidth("130px");

            var ReduceStartDate = new JsInputDate("ReduceStartDate");
            ReduceStartDate.setDisabled(false);
            ReduceStartDate.setWidth("130px");

            var ReduceEndDate = new JsInputDate("ReduceEndDate");
            ReduceEndDate.setDisabled(false);
            ReduceEndDate.setWidth("130px");

            var PropertyFeeStartDate = new JsInputDate("PropertyFeeStartDate");
            PropertyFeeStartDate.setDisabled(false);
            PropertyFeeStartDate.setWidth("130px");

            var PropertyFeeReduceStartDate = new JsInputDate("PropertyFeeReduceStartDate");
            PropertyFeeReduceStartDate.setDisabled(false);
            PropertyFeeReduceStartDate.setWidth("130px");

            var PropertyFeeReduceEndDate = new JsInputDate("PropertyFeeReduceEndDate");
            PropertyFeeReduceEndDate.setDisabled(false);
            PropertyFeeReduceEndDate.setWidth("130px");

            var IncreaseStartDate1 = new JsInputDate("IncreaseStartDate1");
            IncreaseStartDate1.setDisabled(false);
            IncreaseStartDate1.setWidth("130px");

            var IncreaseStartDate2 = new JsInputDate("IncreaseStartDate2");
            IncreaseStartDate2.setDisabled(false);
            IncreaseStartDate2.setWidth("130px");

            var IncreaseStartDate3 = new JsInputDate("IncreaseStartDate3");
            IncreaseStartDate3.setDisabled(false);
            IncreaseStartDate3.setWidth("130px");

            var IncreaseStartDate4 = new JsInputDate("IncreaseStartDate4");
            IncreaseStartDate4.setDisabled(false);
            IncreaseStartDate4.setWidth("130px");


            var MinContractSignedDate = new JsInputDate("MinContractSignedDate");
            MinContractSignedDate.setDisabled(false);
            MinContractSignedDate.setWidth("100px");

            var MaxContractSignedDate = new JsInputDate("MaxContractSignedDate");
            MaxContractSignedDate.setDisabled(false);
            MaxContractSignedDate.setWidth("100px");            

            var MinContractEndDate = new JsInputDate("MinContractEndDate");
            MinContractEndDate.setDisabled(false);
            MinContractEndDate.setWidth("100px");

            var MaxContractEndDate = new JsInputDate("MaxContractEndDate");
            MaxContractEndDate.setDisabled(false);
            MaxContractEndDate.setWidth("100px");            

            var MinOffLeaseActulDate = new JsInputDate("MinOffLeaseActulDate");
            MinOffLeaseActulDate.setDisabled(false);
            MinOffLeaseActulDate.setWidth("100px");

            var MaxOffLeaseActulDate = new JsInputDate("MaxOffLeaseActulDate");
            MaxOffLeaseActulDate.setDisabled(false);
            MaxOffLeaseActulDate.setWidth("100px");


            var BBStartDate = new JsInputDate("BBStartDate");
            BBStartDate.setDisabled(false);
            BBStartDate.setWidth("130px");

            var BBEndDate = new JsInputDate("BBEndDate");
            BBEndDate.setDisabled(false);
            BBEndDate.setWidth("130px");
        });

        var id = "";
        var tp = "";
        var itemid1 = "";
        var itemtp1 = "";
        var itemid2 = "";
        var itemtp2 = "";
        var itemid3 = "";
        var itemtp3 = "";
        var itemid4 = "";
        var itemtp4 = "";
        var itemid5 = "";
        var itemtp5 = "";
        var itemid6 = "";
        var itemtp6 = "";
        var itemid7 = "";
        var itemtp7 = "";
        var copyid = "";
        var trid = "";
        reflist();
</script>
</body>
</html>