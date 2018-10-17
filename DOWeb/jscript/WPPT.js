
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
                layer.alert("当前订单已审核，不能删除！");
            }
            else {
                layer.alert("数据操作出错！");
            }
            return;
        }
        if (vjson.type == "insert") {
            if (vjson.flag == "1") {
                $("#itemlist4").html(vjson.itemlist4);
                itemclear4();
                show(1);
                id = "";
                type = "insert";
                copyid = "";

                $("#ContractNo").val("");
                $("#ContractType").val("04");
                $("#ContractSPNo").val("<%=PTSPNo %>");
                $("#ContractSPName").val("");
                $("#ContractCustNo").val("");
                $("#ContractCustName").val("");
                $("#ContractNoManual").val("");
                $("#ContractHandler").val("<%=userName %>");
                $("#ContractSignedDate").val("<%=date %>");
                $("#ContractStartDate").val("<%=date %>");
                $("#ContractEndDate").val("");
                $("#EntryDate").val("<%=date %>");
                $("#FeeStartDate").val("<%=date %>");
                $("#ReduceStartDate1").val("");
                $("#ReduceEndDate1").val("");
                $("#ReduceStartDate2").val("");
                $("#ReduceEndDate2").val("");
                $("#ReduceStartDate3").val("");
                $("#ReduceEndDate3").val("");
                $("#ReduceStartDate4").val("");
                $("#ReduceEndDate4").val("");
                $("#ContractLatefeeRate").val("");
                $("#RMRentalDeposit").val("");
                $("#RMUtilityDeposit").val("");
                $("#PropertyFeeStartDate").val("<%=date %>");
                $("#PropertyFeeReduceStartDate").val("");
                $("#PropertyFeeReduceEndDate").val("");
                $("#WaterUnitPrice").val(vjson.WaterFee);
                $("#ElecticityUintPrice").val(vjson.ElectricFee);
                $("#AirconUnitPrice").val(vjson.AirConditionFee);
                $("#PropertyUnitPrice").val(vjson.PropertyFee);
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

                $("#itemsave4").css("display", "");
                $("#itemclear4").css("display", "");

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
                    $("#outerlist").html(vjson.liststr);
                    $("#selectKey").val("");
                    reflist();

                    $("#ContractNo").val(vjson.ContractNo);
                    layer.msg("保存成功！", { icon: 3, time: 1000 });

                    if (copyid != "") {
                        $("#itemlist4").html(vjson.itemlist4);
                    }
                    show(5);
                    copyid = "";
                }
            }
            else {
                layer.alert("数据操作出错！");
            }
            return;
        }
        if (vjson.type == "approve") {
            if (vjson.flag == "1") {
                if (vjson.status == "1") {
                    $("#" + vjson.id + " td").eq(7).html("<label style=\"color:red;\">制单</label>");
                    layer.alert("取消审核成功！");
                }
                else {
                    $("#" + vjson.id + " td").eq(7).html("<label style=\"color:blue;\">已审核</label>");
                    layer.alert("审核成功！");
                }
            }
            else if (vjson.flag == "3") {
                layer.alert("当前合同状态不允许审核！");
            }
            else if (vjson.flag == "4") {
                layer.alert("当前合同已生成订单，不能取消审核！");
            }
            else if (vjson.flag == "5") {
                layer.alert(vjson.InfoBar);
            }
            else {
                layer.alert("数据操作出错！");
            }
            return;
        }
        if (vjson.type == "invalid") {
            if (vjson.flag == "1") {
                if (vjson.status == "1") {
                    $("#" + vjson.id + " td").eq(7).html("<label style=\"color:red;\">制单</label>");
                    layer.alert("取消作废成功！");
                }
                else {
                    $("#" + vjson.id + " td").eq(7).html("<label style=\"color:gray;\">已作废</label>");
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
                $("#ReduceStartDate1").val(vjson.ReduceStartDate1);
                $("#ReduceEndDate1").val(vjson.ReduceEndDate1);
                $("#ReduceStartDate2").val(vjson.ReduceStartDate2);
                $("#ReduceEndDate2").val(vjson.ReduceEndDate2);
                $("#ReduceStartDate3").val(vjson.ReduceStartDate3);
                $("#ReduceEndDate3").val(vjson.ReduceEndDate3);
                $("#ReduceStartDate4").val(vjson.ReduceStartDat4);
                $("#ReduceEndDate4").val(vjson.ReduceEndDate4);
                $("#ContractLatefeeRate").val(vjson.ContractLatefeeRate);
                $("#RMRentalDeposit").val("");
                $("#RMUtilityDeposit").val("");
                $("#PropertyFeeStartDate").val("<%=date %>");
                $("#PropertyFeeReduceStartDate").val(vjson.PropertyFeeReduceStartDate);
                $("#PropertyFeeReduceEndDate").val(vjson.PropertyFeeReduceEndDate);
                $("#WaterUnitPrice").val(vjson.WaterUnitPrice);
                $("#ElecticityUintPrice").val(vjson.ElecticityUintPrice);
                $("#AirconUnitPrice").val(vjson.AirconUnitPrice);
                $("#PropertyUnitPrice").val(vjson.PropertyUnitPrice);
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
                itemclear4();
                $("#itemlist4").html(vjson.itemlist4);

                //$("#ContractNo").attr("disabled", true);
                $("#list").css("display", "none");
                $("#edit").css("display", "");

                $("#itemsave4").css("display", "");
                $("#itemclear4").css("display", "");
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
        if (vjson.type == "clone") {
            if (vjson.flag == "1") {
                $("#ContractNo").val("");
                $("#ContractType").val(vjson.ContractType);
                $("#ContractSPNo").val(vjson.ContractSPNo);
                $("#ContractCustNo").val(vjson.ContractCustNo);
                $("#ContractCustName").val(vjson.ContractCustName);
                $("#ContractNoManual").val("");
                $("#ContractHandler").val("<%=userName %>");
                $("#ContractSignedDate").val("<%=date %>");
                $("#ContractStartDate").val("<%=date %>");
                $("#ContractEndDate").val("");
                $("#EntryDate").val("");
                $("#FeeStartDate").val("<%=date %>");
                $("#ReduceStartDate1").val("");
                $("#ReduceEndDate1").val("");
                $("#ReduceStartDate2").val("");
                $("#ReduceEndDate2").val("");
                $("#ReduceStartDate3").val("");
                $("#ReduceEndDate3").val("");
                $("#ReduceStartDate4").val("");
                $("#ReduceEndDate4").val("");
                $("#ContractLatefeeRate").val(vjson.ContractLatefeeRate);
                $("#RMRentalDeposit").val(vjson.RMRentalDeposit);
                $("#RMUtilityDeposit").val(vjson.RMUtilityDeposit);
                $("#PropertyFeeStartDate").val("<%=date %>");
                $("#PropertyFeeReduceStartDate").val("");
                $("#PropertyFeeReduceEndDate").val("");
                $("#WaterUnitPrice").val(vjson.WaterUnitPrice);
                $("#ElecticityUintPrice").val(vjson.ElecticityUintPrice);
                $("#AirconUnitPrice").val(vjson.AirconUnitPrice);
                $("#PropertyUnitPrice").val(vjson.PropertyUnitPrice);
                $("#SharedWaterFee").val(vjson.SharedWaterFee);
                $("#SharedElectricyFee").val(vjson.SharedElectricyFee);
                $("#WPRentalDeposit").val(vjson.WPRentalDeposit);
                $("#WPUtilityDeposit").val(vjson.WPUtilityDeposit);
                $("#WPQTY").val(vjson.WPQTY);
                $("#WPElectricyLimit").val(vjson.WPElectricyLimit);
                $("#WPOverElectricyPrice").val(vjson.WPOverElectricyPrice);
                $("#BBQTY").val(vjson.BBQTY);
                $("#BBAmount").val(vjson.BBAmount);
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

                copyid = vjson.cloneid;
                id = "";
                show(1);
                itemclear4();
                $("#itemlist4").html(vjson.itemlist4);

                //$("#ContractNo").attr("disabled", true);
                $("#list").css("display", "none");
                $("#edit").css("display", "");

                $("#itemsave4").css("display", "");
                $("#itemclear4").css("display", "");
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
                $("#ReduceStartDate1").val(vjson.ReduceStartDate1);
                $("#ReduceEndDate1").val(vjson.ReduceEndDate1);
                $("#ReduceStartDate2").val(vjson.ReduceStartDate2);
                $("#ReduceEndDate2").val(vjson.ReduceEndDate2);
                $("#ReduceStartDate3").val(vjson.ReduceStartDate3);
                $("#ReduceEndDate3").val(vjson.ReduceEndDate3);
                $("#ReduceStartDate4").val(vjson.ReduceStartDate4);
                $("#ReduceEndDate4").val(vjson.ReduceEndDate4);
                $("#ContractLatefeeRate").val(vjson.ContractLatefeeRate);
                $("#RMRentalDeposit").val(vjson.RMRentalDeposit);
                $("#RMUtilityDeposit").val(vjson.RMUtilityDeposit);
                $("#PropertyFeeStartDate").val(vjson.PropertyFeeStartDate);
                $("#PropertyFeeReduceStartDate").val(vjson.PropertyFeeReduceStartDate);
                $("#PropertyFeeReduceEndDate").val(vjson.PropertyFeeReduceEndDate);
                $("#WaterUnitPrice").val(vjson.WaterUnitPrice);
                $("#ElecticityUintPrice").val(vjson.ElecticityUintPrice);
                $("#AirconUnitPrice").val(vjson.AirconUnitPrice);
                $("#PropertyUnitPrice").val(vjson.PropertyUnitPrice);
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
                itemclear4();
                $("#itemlist4").html(vjson.itemlist4);

                //$("#ContractNo").attr("disabled", true);
                $("#list").css("display", "none");
                $("#edit").css("display", "");

                $("#itemsave4").css("display", "none");
                $("#itemclear4").css("display", "none");
                $("#submit").css("display", "none");
                $("#submit1").css("display", "none");
            }
            else {
                layer.alert("数据操作出错！");
            }
            return;
        }
        if (vjson.type == "viewfee") {
            if (vjson.flag == "1") {
                layer.open({
                    type: 1,
                    area: ["900px", "600px"],
                    fix: true,
                    maxmin: true,
                    scrollbar: false,
                    shade: 0.5,
                    title: "费用清单",
                    content: vjson.feelist
                });
            }
            else {
                layer.alert("提取数据出错！");
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

        if (vjson.type == "itemsave4") {
            if (vjson.flag == "1") {
                itemclear4();
                $("#itemlist4").html(vjson.liststr);
            }
            else {
                layer.alert("提交数据出错！");
            }
            return;
        }
        if (vjson.type == "itemupdate4") {
            if (vjson.flag == "1") {
                $("#RMID3").val(vjson.RMID);
                $("#SRVNo4").val(vjson.SRVNo);
                $("#RMArea1").val(vjson.RMArea);
                $("#UnitPrice1").val(vjson.UnitPrice);
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
        if (vjson.type == "getrmid4") {
            if (vjson.flag == "1") {
                $("#RMArea1").val(vjson.RMBuildSize);
            }
            else {
                layer.alert("删除数据出错！");
            }
            return;
        }
        if (vjson.type == "getprice4") {
            if (vjson.flag == "1") {
                $("#UnitPrice1").val(vjson.UnitPrice);
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
function viewfee() {
    if ($("#selectKey").val() == "") {
        layer.msg("请先选择一条记录", { icon: 3, time: 1000 });
        return;
    }
    id = $("#selectKey").val();
    var submitData = new Object();
    submitData.Type = "viewfee";
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
    submitData.ReduceStartDate1 = $("#ReduceStartDate1").val();
    submitData.ReduceEndDate1 = $("#ReduceEndDate1").val();
    submitData.ReduceStartDate2 = $("#ReduceStartDate2").val();
    submitData.ReduceEndDate2 = $("#ReduceEndDate2").val();
    submitData.ReduceStartDate3 = $("#ReduceStartDate3").val();
    submitData.ReduceEndDate3 = $("#ReduceEndDate3").val();
    submitData.ReduceStartDate4 = $("#ReduceStartDate4").val();
    submitData.ReduceEndDate4 = $("#ReduceEndDate4").val();
    submitData.ContractLatefeeRate = $("#ContractLatefeeRate").val();
    submitData.RMRentalDeposit = $("#RMRentalDeposit").val();
    submitData.RMUtilityDeposit = $("#RMUtilityDeposit").val();
    submitData.PropertyFeeStartDate = $("#PropertyFeeStartDate").val();
    submitData.PropertyFeeReduceStartDate = $("#PropertyFeeReduceStartDate").val();
    submitData.PropertyFeeReduceEndDate = $("#PropertyFeeReduceEndDate").val();
    submitData.WaterUnitPrice = $("#WaterUnitPrice").val();
    submitData.ElecticityUintPrice = $("#ElecticityUintPrice").val();
    submitData.AirconUnitPrice = $("#AirconUnitPrice").val();
    submitData.PropertyUnitPrice = $("#PropertyUnitPrice").val();
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
    submitData.ContractTypeS = "04";
    submitData.ContractSPNoS = $("#ContractSPNoS").val();
    submitData.ContractCustNoS = $("#ContractCustNoS").val();
    submitData.MinContractSignedDate = $("#MinContractSignedDate").val();
    submitData.MaxContractSignedDate = $("#MaxContractSignedDate").val();
    submitData.MinContractEndDate = $("#MinContractEndDate").val();
    submitData.MaxContractEndDate = $("#MaxContractEndDate").val();
    submitData.ContractStatusS = $("#ContractStatusS").val();
    submitData.OffLeaseStatusS = $("#OffLeaseStatusS").val();
    submitData.MinOffLeaseActulDate = $("#MinOffLeaseActulDate").val();
    submitData.MaxOffLeaseActulDate = $("#MaxOffLeaseActulDate").val();
    submitData.page = page;
    transmitData(datatostr(submitData));
    return;
}
function submit1() {
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
    submitData.ReduceStartDate1 = $("#ReduceStartDate1").val();
    submitData.ReduceEndDate1 = $("#ReduceEndDate1").val();
    submitData.ReduceStartDate2 = $("#ReduceStartDate2").val();
    submitData.ReduceEndDate2 = $("#ReduceEndDate2").val();
    submitData.ReduceStartDate3 = $("#ReduceStartDate3").val();
    submitData.ReduceEndDate3 = $("#ReduceEndDate3").val();
    submitData.ReduceStartDate4 = $("#ReduceStartDate4").val();
    submitData.ReduceEndDate4 = $("#ReduceEndDate4").val();
    submitData.ContractLatefeeRate = $("#ContractLatefeeRate").val();
    submitData.RMRentalDeposit = $("#RMRentalDeposit").val();
    submitData.RMUtilityDeposit = $("#RMUtilityDeposit").val();
    submitData.PropertyFeeStartDate = $("#PropertyFeeStartDate").val();
    submitData.PropertyFeeReduceStartDate = $("#PropertyFeeReduceStartDate").val();
    submitData.PropertyFeeReduceEndDate = $("#PropertyFeeReduceEndDate").val();
    submitData.WaterUnitPrice = $("#WaterUnitPrice").val();
    submitData.ElecticityUintPrice = $("#ElecticityUintPrice").val();
    submitData.AirconUnitPrice = $("#AirconUnitPrice").val();
    submitData.PropertyUnitPrice = $("#PropertyUnitPrice").val();
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
    submitData.ContractTypeS = "04";
    submitData.ContractSPNoS = $("#ContractSPNoS").val();
    submitData.ContractCustNoS = $("#ContractCustNoS").val();
    submitData.MinContractSignedDate = $("#MinContractSignedDate").val();
    submitData.MaxContractSignedDate = $("#MaxContractSignedDate").val();
    submitData.MinContractEndDate = $("#MinContractEndDate").val();
    submitData.MaxContractEndDate = $("#MaxContractEndDate").val();
    submitData.ContractStatusS = $("#ContractStatusS").val();
    submitData.OffLeaseStatusS = $("#OffLeaseStatusS").val();
    submitData.MinOffLeaseActulDate = $("#MinOffLeaseActulDate").val();
    submitData.MaxOffLeaseActulDate = $("#MaxOffLeaseActulDate").val();
    submitData.page = page;
    transmitData(datatostr(submitData));
    return;
}
function clone() {
    if ($("#selectKey").val() == "") {
        layer.msg("请先选择一条记录", { icon: 3, time: 1000 });
        return;
    }
    id = $("#selectKey").val();
    type = "insert";
    var submitData = new Object();
    submitData.Type = "clone";
    submitData.id = id;

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
        submitData.ContractTypeS = "04";
        submitData.ContractSPNoS = $("#ContractSPNoS").val();
        submitData.ContractCustNoS = $("#ContractCustNoS").val();
        submitData.MinContractSignedDate = $("#MinContractSignedDate").val();
        submitData.MaxContractSignedDate = $("#MaxContractSignedDate").val();
        submitData.MinContractEndDate = $("#MinContractEndDate").val();
        submitData.MaxContractEndDate = $("#MaxContractEndDate").val();
        submitData.ContractStatusS = $("#ContractStatusS").val();
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
    submitData.ContractTypeS = "04";
    submitData.ContractSPNoS = $("#ContractSPNoS").val();
    submitData.ContractCustNoS = $("#ContractCustNoS").val();
    submitData.MinContractSignedDate = $("#MinContractSignedDate").val();
    submitData.MaxContractSignedDate = $("#MaxContractSignedDate").val();
    submitData.MinContractEndDate = $("#MinContractEndDate").val();
    submitData.MaxContractEndDate = $("#MaxContractEndDate").val();
    submitData.ContractStatusS = $("#ContractStatusS").val();
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

function deletefile(controlid, filename) {
    layer.confirm('确定要删除此附件吗？', function (index) {
        $("#" + controlid).parent().remove();
        $("#ContractAttachment").val($("#ContractAttachment").val().replace("<file>" + filename + "</file>", ""));
        layer.close(index);
    });
}


function itemsave4() {
    if ($("#RMID3").val() == "") {
        layer.msg("请选择房间编号！", { icon: 7, time: 1500 });
        $("#RMID3").focus();
        return;
    }
    if ($("#WPNo").val() == "") {
        layer.msg("请选择工位编号！", { icon: 7, time: 1500 });
        $("#WPNo").focus();
        return;
    }
    if ($("#SRVNo4").val() == "") {
        layer.msg("请选择所属费用项目！", { icon: 7, time: 1500 });
        $("#SRVNo4").focus();
        return;
    }    
    if ($("#UnitPrice1").val() == "") {
        layer.msg("请填写单价！", { icon: 7, time: 1500 });
        $("#UnitPrice1").focus();
        return;
    }
    var submitData = new Object();
    submitData.Type = "itemsave4";
    submitData.id = id;
    submitData.itemid = itemid4;
    submitData.itemtp = itemtp4;
    submitData.RMID = $("#RMID3").val();
    submitData.SRVNo = $("#SRVNo4").val();
    submitData.WPNo = $("#WPNo").val();
    submitData.UnitPrice = $("#UnitPrice1").val();
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

    $("#SRVNo4").val("<%=PTRentSRVNo %>");
    $("#RMID3").val("");
    $("#WPNo").val("");
    $("#RMArea1").val("");
    $("#UnitPrice1").val("<%=PTRentFee %>");
    $("#Remark4").val("");
}

var page = 1;
function jump(pageindex) {
    page = pageindex;
    var submitData = new Object();
    submitData.Type = "jump";
    submitData.ContractNoS = $("#ContractNoS").val();
    submitData.ContractNoManualS = $("#ContractNoManualS").val();
    submitData.ContractTypeS = "05";
    submitData.ContractSPNoS = $("#ContractSPNoS").val();
    submitData.ContractCustNoS = $("#ContractCustNoS").val();
    submitData.MinContractSignedDate = $("#MinContractSignedDate").val();
    submitData.MaxContractSignedDate = $("#MaxContractSignedDate").val();
    submitData.MinContractEndDate = $("#MinContractEndDate").val();
    submitData.MaxContractEndDate = $("#MaxContractEndDate").val();
    submitData.ContractStatusS = $("#ContractStatusS").val();
    submitData.OffLeaseStatusS = $("#OffLeaseStatusS").val();
    submitData.MinOffLeaseActulDate = $("#MinOffLeaseActulDate").val();
    submitData.MaxOffLeaseActulDate = $("#MaxOffLeaseActulDate").val();
    submitData.page = page;
    transmitData(datatostr(submitData));
    return;
}

function show(page) {
    if (page == 1) {
        $('#itemtab5').removeClass("selected");
        $("#bodyeditdiv4").hide();

        $('#itemtab1').addClass("selected");
        $("#topeditdiv").show();
    }
    else {
        if (id == "") {
            layer.msg("请先保存表头信息！", { icon: 3, time: 1000 });
            return;
        }

        $('#itemtab1').removeClass("selected");
        $('#itemtab5').removeClass("selected");
        $("#topeditdiv").hide();
        $("#bodyeditdiv4").hide();

        if (page == "5") {
            $('#itemtab5').addClass("selected");
            $("#bodyeditdiv4").show();
        }
    }
}

$("#ContractCustImg").click(function () {
    ChooseBasic("ContractCustNo", "Cust");
});
$("#chooseRMID3").click(function () {
    var temp = "../Base/ChooseRMID.aspx?id=RMID3";
    layer_show("选择页面", temp, 800, 630);
});
$("#chooseWPNo").click(function () {
    if ($("#RMID2").val() == "") {
        layer.msg("请先选择房间编号", { icon: 3, time: 1000 });
        return;
    }
    var temp = "../Base/ChooseWPNo.aspx?id=WPNo&RMID=" + $("#RMID3").val();
    layer_show("选择页面", temp, 800, 600);
});
function choose(id, labels, values) {
    if (labels != "" && labels != undefined && labels != "undefined") {
        if (id == "ContractCustNo") {
            $("#ContractCustName").val(values);
            $("#ContractCustNo").val(labels);
        }
        else if (id == "RMID3") {
            $("#" + id).val(labels);

            var submitData = new Object();
            submitData.Type = "getrmid4";
            submitData.rmid = $("#" + id).val();
            transmitData(datatostr(submitData));
        }
    }
}
$("#SRVNo4").change(function () {
    var submitData = new Object();
    submitData.Type = "getprice4";
    submitData.SRVNo = $("#SRVNo4").val();
    transmitData(datatostr(submitData));
});

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

function datechange() {
    $("#ContractStartDate").val($("#ContractSignedDate").val());
    $("#EntryDate").val($("#ContractSignedDate").val());
    $("#FeeStartDate").val($("#ContractSignedDate").val());
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
    //var ReduceStartDate = new JsInputDate("ReduceStartDate");
    //ReduceStartDate.setDisabled(false);
    //ReduceStartDate.setWidth("130px");
    //var ReduceEndDate = new JsInputDate("ReduceEndDate");
    //ReduceEndDate.setDisabled(false);
    //ReduceEndDate.setWidth("130px");

    //var PropertyFeeStartDate = new JsInputDate("PropertyFeeStartDate");
    //PropertyFeeStartDate.setDisabled(false);
    //PropertyFeeStartDate.setWidth("130px");
    //var PropertyFeeReduceStartDate = new JsInputDate("PropertyFeeReduceStartDate");
    //PropertyFeeReduceStartDate.setDisabled(false);
    //PropertyFeeReduceStartDate.setWidth("130px");
    //var PropertyFeeReduceEndDate = new JsInputDate("PropertyFeeReduceEndDate");
    //PropertyFeeReduceEndDate.setDisabled(false);
    //PropertyFeeReduceEndDate.setWidth("130px");

    //var IncreaseStartDate1 = new JsInputDate("IncreaseStartDate1");
    //IncreaseStartDate1.setDisabled(false);
    //IncreaseStartDate1.setWidth("130px");
    //var IncreaseStartDate2 = new JsInputDate("IncreaseStartDate2");
    //IncreaseStartDate2.setDisabled(false);
    //IncreaseStartDate2.setWidth("130px");
    //var IncreaseStartDate3 = new JsInputDate("IncreaseStartDate3");
    //IncreaseStartDate3.setDisabled(false);
    //IncreaseStartDate3.setWidth("130px");
    //var IncreaseStartDate4 = new JsInputDate("IncreaseStartDate4");
    //IncreaseStartDate4.setDisabled(false);
    //IncreaseStartDate4.setWidth("130px");


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
});

var id = "";
var tp = "";
var itemid4 = "";
var itemtp4 = "";
var copyid = "";
var trid = "";
reflist();
