var isConfirm = false;
var MeterID = "";
var ModelType = "";

$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#contentUT").width($("#pageContent").width() - 30);
    $("#content").width($("#pageContent").width() - 30);

    // 加载仪表检查项
    LoadCheckDetail();
    LoadCheckDetailUT();

    GetMeterName();

    // 其他
    $('#QTdiv').click(function () {
        this.className = "btnTw";
        $('#UTdiv').attr("class", "btnTh");

        $("#UTList").css("display", "none");
        $("#QTList").css("display", "");
        $("#content").width($("#pageContent").width() - 30);

    })

    // 超声波 
    $('#UTdiv').click(function () {
        this.className = "btnTw";
        $('#QTdiv').attr("class", "btnTh");

        $("#QTList").css("display", "none");
        $("#UTList").css("display", "");
        $("#contentUT").width($("#pageContent").width() - 30);

        GetMeterNameUT();
    })

    // 确定新增登记卡
    $("#QD").click(function () {
        isConfirm = confirm("确定要新建登记卡吗")
        if (isConfirm == false) {
            return false;
        }
        else {
            var tab = document.getElementById("CheckItems");
            var tds = tab.getElementsByTagName("tr");
            var strTitle = "";
            var strChecked = "";
            var title = "";
            var checks;

            for (var i = 0 ; i < tds.length; i++) {
                // 文字列
                title = tds[i].getElementsByTagName("td")[0].innerHTML;
                strTitle += title + ",";
                title = tds[i].getElementsByTagName("td")[2].innerHTML;
                strTitle += title + ",";

                // checkbox列 
                var check1 = "";
                checks = tds[i].getElementsByTagName("td")[1].getElementsByTagName("INPUT");
                for (var j = 0; j < checks.length; j++) {
                    var obj = checks[j];
                    if (obj.type == 'checkbox') {
                        if ($('input[id=' + obj.id + ']').prop("checked"))
                            check1 += obj.value + ",";
                    }
                }
                if (check1 != "")
                    strChecked += check1.substring(0, check1.length - 1);
                strChecked += "?";

                var check2 = "";
                checks = tds[i].getElementsByTagName("td")[3].getElementsByTagName("INPUT");
                for (var k = 0; k < checks.length; k++) {
                    var obj0 = checks[k];
                    if (obj0.type == 'checkbox') {
                        if ($('input[id=' + obj0.id + ']').prop("checked"))
                            check2 += obj0.value + ",";
                    }
                }
                if (check2 != "")
                    strChecked += check2.substring(0, check2.length - 1);
                strChecked += "?";
            }

            strTitle = strTitle.substring(0, strTitle.length - 1);
            strChecked = strChecked.substring(0, strChecked.length - 1);
            $("#HTitle").val(strTitle);
            $("#HChecked").val(strChecked);

            //
            var tabUT = document.getElementById("CheckItemsUT");
            var tdsUT = tabUT.getElementsByTagName("tr");
            var strTitleUT = "";
            var strCheckedUT = "";
            var titleUT = "";
            var checksUT;

            for (var i = 0 ; i < tdsUT.length; i++) {
                // 文字列
                titleUT = tdsUT[i].getElementsByTagName("td")[0].innerHTML;
                strTitleUT += titleUT + ",";
                titleUT = tdsUT[i].getElementsByTagName("td")[2].innerHTML;
                strTitleUT += titleUT + ",";

                // checkbox列 
                var checkUT1 = "";
                checksUT = tdsUT[i].getElementsByTagName("td")[1].getElementsByTagName("INPUT");
                for (var j = 0; j < checksUT.length; j++) {
                    var objUT = checksUT[j];
                    if (objUT.type == 'checkbox') {
                        if ($('input[id=' + objUT.id + ']').prop("checked"))
                            checkUT1 += objUT.value + ",";
                    }
                }
                if (checkUT1 != "")
                    strCheckedUT += checkUT1.substring(0, checkUT1.length - 1);
                strCheckedUT += "?";

                var checkUT2 = "";
                checksUT = tdsUT[i].getElementsByTagName("td")[3].getElementsByTagName("INPUT");
                for (var k = 0; k < checksUT.length; k++) {
                    var obj0 = checksUT[k];
                    if (obj0.type == 'checkbox') {
                        if ($('input[id=' + obj0.id + ']').prop("checked"))
                            checkUT2 += obj0.value + ",";
                    }
                }
                if (checkUT2 != "")
                    strCheckedUT += checkUT2.substring(0, checkUT2.length - 1);
                strCheckedUT += "?";
            }

            strTitleUT = strTitleUT.substring(0, strTitleUT.length - 1);
            strCheckedUT = strCheckedUT.substring(0, strCheckedUT.length - 1);
            $("#HTitleUT").val(strTitleUT);
            $("#HCheckedUT").val(strCheckedUT);

            $("#StrIsRepair").val($('input:radio[name="IsRepair"]:checked').val());
            $("#StrIsOut").val($('input:radio[name="IsOut"]:checked').val());
            $("#StrIsOutUT").val($('input:radio[name="IsOutUT"]:checked').val());
            //
            $("#StrFirstCheckUT").val($('input:radio[name="FirstCheckUT"]:checked').val());
            $("#StrSecondCheckUT").val($('input:radio[name="SecondCheckUT"]:checked').val());
            $("#StrThirdCheckUT").val($('input:radio[name="ThirdCheckUT"]:checked').val());

            submitInfo();
        }

    })

    // 流量范围
    $("#SelFlowRange").click(function () {
        selid1('GetFlowRange', 'FlowRange', 'divSelFlowRange', 'ulSelFlowRange', 'LoadFlowRange');
    })

    // 承压等级
    $("#SelPressure").click(function () {
        selid1('GetPressure', 'Pressure', 'divSelPressure', 'ulSelPressure', 'LoadPressure');
    })

    // 流量范围
    $("#SelFlowRangeUT").click(function () {
        selid2('GetFlowRangeUT', 'FlowRangeUT', 'divSelFlowRangeUT', 'ulSelFlowRangeUT', 'LoadFlowRangeUT');
    })

    // 承压等级
    $("#SelPressureUT").click(function () {
        selid2('GetPressureUT', 'PressureUT', 'divSelPressureUT', 'ulSelPressureUT', 'LoadPressureUT');
    })

})

// 流量范围 承压等级
function selid1(actionid, selid, divid, ulid, jsfun) {
    $.ajax({
        url: actionid,
        type: "post",
        data: { Model: $("#strModel").val() },
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.success == "true") {
                if (data != "") {
                    var unit = data.strDetail.split('?');
                    $("#" + divid).show();
                    $("#" + ulid + " li").remove();
                    for (var i = 0; i < unit.length; i++) {
                        $("#" + ulid).append("<li style='cursor:pointer;margin-left:2px; width:150px;height:20px;list-style-type:none; color:black;'><span onclick='" + jsfun + "(this);' style='margin-left:8px; width:150px; height:20px;display:block;'>" + unit[i] + "</span>");
                    }
                }
            }
            else {
                $("#" + divid).hide();
            }
        }
    });
}

// 流量范围ut 承压等级ut 
function selid2(actionid, selid, divid, ulid, jsfun) {
    $.ajax({
        url: actionid,
        type: "post",
        data: { Model: $("#strModelUT").val() },
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.success == "true") {
                if (data != "") {
                    var unit = data.strDetail.split('?');
                    $("#" + divid).show();
                    $("#" + ulid + " li").remove();
                    for (var i = 0; i < unit.length; i++) {
                        $("#" + ulid).append("<li style='cursor:pointer;margin-left:2px; width:150px;height:20px;list-style-type:none; color:black;'><span onclick='" + jsfun + "(this);' style='margin-left:8px; width:150px; height:20px;display:block;'>" + unit[i] + "</span>");
                    }
                }
            }
            else {
                $("#" + divid).hide();
            }
        }
    });
}

// 流量范围 
function LoadFlowRange(liInfo) {
    $('#SelFlowRange').val($(liInfo).text());
    $('#divSelFlowRange').hide();
}

// 承压等级 
function LoadPressure(liInfo) {
    $('#SelPressure').val($(liInfo).text());
    $('#divSelPressure').hide();
}

// 流量范围 ut
function LoadFlowRangeUT(liInfo) {
    $('#SelFlowRangeUT').val($(liInfo).text());
    $('#divSelFlowRangeUT').hide();
}

// 承压等级 ut
function LoadPressureUT(liInfo) {
    $('#SelPressureUT').val($(liInfo).text());
    $('#divSelPressureUT').hide();
}


// 根据类型获取仪表名称
function GetMeterNameUT() {
    $.ajax({
        url: "GetMeterName",
        type: "post",
        data: { ModelType: $("#strModelTypeUT").val() },
        dataType: "json",
        async: false, //是否异步
        success: function (data) {
            if (data.success == "false") {
                if (data.Msg != "")
                    return;
            }
            else {
                var json = data.MeterName;
                if (json != "")
                    $("#strMeterNameUT").val(json);
                else
                    $("#strMeterNameUT").val("");
            }
        }
    })
}

// 根据类型获取仪表名称
function GetMeterName() {
    $.ajax({
        url: "GetMeterName",
        type: "post",
        data: { ModelType: $("#strModelType").val() },
        dataType: "json",
        async: false, //是否异步
        success: function (data) {
            if (data.success == "false") {
                if (data.Msg != "")
                    return;
            }
            else {
                var json = data.MeterName;
                if (json != "")
                    $("#strMeterName").val(json);
                else
                    $("#strMeterName").val("");
            }
        }
    })
}

// 取表方式onchange
function ChangeTypes() {
    // 物流取表
    if ($("#strGetTypeModel").val() == "GetType1") {
        $("#Logistic").show();
        $("#UserInfo").hide();
    }
    else if ($("#strGetTypeModel").val() == "GetType2") {
        $("#UserInfo").show();
        $("#Logistic").hide();
    }
}

// 取表方式onchangeUT
function ChangeTypesUT() {
    // 物流取表
    if ($("#strGetTypeModelUT").val() == "GetType1") {
        $("#LogisticUT").show();
        $("#UserInfoUT").hide();
    }
    else if ($("#strGetTypeModelUT").val() == "GetType2") {
        $("#UserInfoUT").show();
        $("#LogisticUT").hide();
    }
}

// 
function ShowInfo(num) {
    if (num == 0) {
        $("#OutTitle").show();
        $("#OutUnit").show();
    }
    else {
        $("#OutTitle").hide();
        $("#OutUnit").hide();
    }
}
//
function ShowInfoUT(num) {
    if (num == 0) {
        $("#OutTitleUT").show();
        $("#OutUnitUT").show();
    }
    else {
        $("#OutTitleUT").hide();
        $("#OutUnitUT").hide();
    }
}

function submitInfo() {
    var options = {
        url: "AddNewCard2",
        data: {},
        type: 'POST',
        async: false,
        success: function (data) {
            if (data.success == "true") {
                window.parent.frames["iframeRight"].reload();
                alert(data.Msg);
                setTimeout('parent.ClosePop()', 10);
            }
            else {
                //alert(data.Msg);
            }
        }
    };
    $("#ProjectformInfo").ajaxSubmit(options);
    return false;
}

function returnConfirm() {
    return false;
}

//其他
function LoadCheckDetail() {
    $.ajax({
        url: "GetCheckItems",
        type: "post",
        data: {},
        dataType: "json",
        async: false, //是否异步
        success: function (data) {
            if (data.success == "false") {
                if (data.Msg != "")
                    return;
            }
            else {
                if (data.CheckItems != "") {
                    var tab = $("#CheckItems");
                    tab.html('');
                    var datas = eval("(" + data.CheckItems + ")");
                    var tr = $("<tr></tr>");
                    $.each(datas.CheckItems, function (i, n) {
                        if (i % 2 == 0)
                            tr = $('<tr style="height:50px;"></tr>');
                        var td = "";
                        for (var key in n) {
                            var keyValue = '';
                            if (key == 'CheckContent') {
                                var arryContent = n[key].split('/');
                                if (arryContent.length > 1) {// 有选项 
                                    var arryValues1 = arryContent[0].split(',');
                                    var arryValues2 = arryContent[1].split(',');

                                    td = $("<td class='textLeftL' id='" + n['CheckItem'] + "'>" + n['CheckItem'] + "</td>");
                                    td.appendTo(tr);
                                    if (arryValues1.length > 1) {
                                        var id1 = n['CheckItem'] + '-' + arryValues1[0] + '-1';
                                        var id2 = n['CheckItem'] + '-' + arryValues1[1] + '-1';
                                        var id3 = n['CheckItem'] + '-' + arryValues2[0] + '-2';
                                        var id4 = n['CheckItem'] + '-' + arryValues2[1] + '-2';

                                        td = $("<td class='textRightL' style='text-align:center;'>"
                                            + arryValues1[0] + "<input type='checkbox' name='ck' id='" + id1 + "' value='" + arryValues1[0] + "' onclick=\"selChange('" + id1 + "')\"/> &nbsp;&nbsp;"
                                            + arryValues1[1] + "<input type='checkbox' name='ck' id='" + id2 + "' value='" + arryValues1[1] + "' onclick=\"selChange('" + id2 + "')\"/></br></br>"
                                            + arryValues2[0] + "<input type='checkbox' name='ck' id='" + id3 + "' value='" + arryValues2[0] + "' onclick=\"selChange('" + id3 + "')\"/>&nbsp;&nbsp;"
                                            + arryValues2[1] + "<input type='checkbox' name='ck' id='" + id4 + "' value='" + arryValues2[1] + "' onclick=\"selChange('" + id4 + "')\"/></td>");
                                    }
                                    else {
                                        var id1 = n['CheckItem'] + '-' + arryValues1[0] + '-1';
                                        var id2 = n['CheckItem'] + '-' + arryValues2[0] + '-2';
                                        var id3 = n['CheckItem'] + '-' + arryValues2[1] + '-2';

                                        td = $("<td class='textRightL' style='text-align:center;'>"
                                            + arryValues1[0] + "<input type='checkbox' name='ck' id='" + id1 + "'  value='" + arryValues1[0] + "' onclick=\"selChange('" + id1 + "')\"/></br></br>"
                                            + arryValues2[0] + "<input type='checkbox' name='ck' id='" + id2 + "'  value='" + arryValues2[0] + "' onclick=\"selChange('" + id2 + "')\"/>&nbsp;&nbsp;"
                                            + arryValues2[1] + "<input type='checkbox' name='ck' id='" + id3 + "'  value='" + arryValues2[1] + "' onclick=\"selChange('" + id3 + "')\"/></td>");
                                    }

                                    td.appendTo(tr);

                                }
                                else {
                                    var arryValues = arryContent[0].split(',');
                                    var id1 = n['CheckItem'] + '-' + arryValues[0] + '-1';
                                    var id2 = n['CheckItem'] + '-' + arryValues[1] + '-1';

                                    td = $("<td class='textLeftL' id='" + n['CheckItem'] + "'>" + n['CheckItem'] + "</td>");
                                    td.appendTo(tr);
                                    td = $("<td class='textRightL' style='text-align:center;'>"
                                        + arryValues[0] + "<input type='checkbox' name='ck' id='" + id1 + "' value='" + arryValues[0] + "' onclick=\"selChange('" + id1 + "')\"/>&nbsp;&nbsp;"
                                        + arryValues[1] + "<input type='checkbox' name='ck' id='" + id2 + "' value='" + arryValues[1] + "' onclick=\"selChange('" + id2 + "')\"/></td>");

                                    td.appendTo(tr);

                                }

                            }

                        }
                        tr.appendTo(tab);
                    });

                    td = $("<td class='textRightL'></td>");
                    td.appendTo(tr);
                    td = $("<td class='textRightL'></td>");
                    td.appendTo(tr);
                    tr.appendTo(tab);
                }
            }
        }

    });
}

//超声波
function LoadCheckDetailUT() {
    $.ajax({
        url: "GetCheckItemsUT",
        type: "post",
        data: {},
        dataType: "json",
        async: false, //是否异步
        success: function (data) {
            if (data.success == "false") {
                if (data.Msg != "")
                    return;
            }
            else {
                if (data.CheckItems != "") {
                    var tab = $("#CheckItemsUT");
                    tab.html('');
                    var datas = eval("(" + data.CheckItems + ")");
                    var tr = $("<tr></tr>");
                    $.each(datas.CheckItems, function (i, n) {
                        if (i % 2 == 0)
                            tr = $('<tr style="height:50px;"></tr>');
                        var td = "";
                        for (var key in n) {
                            var keyValue = '';
                            if (key == 'CheckContent') {
                                var arryContent = n[key].split('/');
                                if (arryContent.length > 1) {// 有选项 
                                    var arryValues1 = arryContent[0].split(',');
                                    var arryValues2 = arryContent[1].split(',');

                                    td = $("<td class='textLeftL' id='" + n['CheckItem'] + "'>" + n['CheckItem'] + "</td>");
                                    td.appendTo(tr);
                                    if (arryValues1.length > 1) {
                                        var id1 = n['CheckItem'] + '-' + arryValues1[0] + '-1';
                                        var id2 = n['CheckItem'] + '-' + arryValues1[1] + '-1';
                                        var id3 = n['CheckItem'] + '-' + arryValues2[0] + '-2';
                                        var id4 = n['CheckItem'] + '-' + arryValues2[1] + '-2';

                                        td = $("<td class='textRightL' style='text-align:center;'>"
                                            + arryValues1[0] + "<input type='checkbox' name='ck' id='" + id1 + "' value='" + arryValues1[0] + "' onclick=\"selChange1('" + id1 + "')\"/> &nbsp;&nbsp;"
                                            + arryValues1[1] + "<input type='checkbox' name='ck' id='" + id2 + "' value='" + arryValues1[1] + "' onclick=\"selChange1('" + id2 + "')\"/></br></br>"
                                            + arryValues2[0] + "<input type='checkbox' name='ck' id='" + id3 + "' value='" + arryValues2[0] + "' onclick=\"selChange1('" + id3 + "')\"/>&nbsp;&nbsp;"
                                            + arryValues2[1] + "<input type='checkbox' name='ck' id='" + id4 + "' value='" + arryValues2[1] + "' onclick=\"selChange1('" + id4 + "')\"/></td>");
                                    }
                                    else {
                                        var id1 = n['CheckItem'] + '-' + arryValues1[0] + '-1';
                                        var id2 = n['CheckItem'] + '-' + arryValues2[0] + '-2';
                                        var id3 = n['CheckItem'] + '-' + arryValues2[1] + '-2';

                                        td = $("<td class='textRightL' style='text-align:center;'>"
                                            + arryValues1[0] + "<input type='checkbox' name='ck' id='" + id1 + "'  value='" + arryValues1[0] + "' onclick=\"selChange1('" + id1 + "')\"/></br></br>"
                                            + arryValues2[0] + "<input type='checkbox' name='ck' id='" + id2 + "'  value='" + arryValues2[0] + "' onclick=\"selChange1('" + id2 + "')\"/>&nbsp;&nbsp;"
                                            + arryValues2[1] + "<input type='checkbox' name='ck' id='" + id3 + "'  value='" + arryValues2[1] + "' onclick=\"selChange1('" + id3 + "')\"/></td>");
                                    }

                                    td.appendTo(tr);

                                }
                                else {
                                    var arryValues = arryContent[0].split(',');
                                    var id1 = n['CheckItem'] + '-' + arryValues[0] + '-1';
                                    var id2 = n['CheckItem'] + '-' + arryValues[1] + '-1';

                                    td = $("<td class='textLeftL' id='" + n['CheckItem'] + "'>" + n['CheckItem'] + "</td>");
                                    td.appendTo(tr);
                                    td = $("<td class='textRightL' style='text-align:center;'>"
                                        + arryValues[0] + "<input type='checkbox' name='ck' id='" + id1 + "' value='" + arryValues[0] + "' onclick=\"selChange1('" + id1 + "')\"/>&nbsp;&nbsp;"
                                        + arryValues[1] + "<input type='checkbox' name='ck' id='" + id2 + "' value='" + arryValues[1] + "' onclick=\"selChange1('" + id2 + "')\"/></td>");

                                    td.appendTo(tr);

                                }
                            }
                        }
                        tr.appendTo(tab);
                    });
                }
            }
        }

    });
}

// 实现checkbox单选
function selChange(id) {
    var allId = id.split('-')[0];
    var checks = document.getElementsByTagName("input");

    if ($('input[id=' + id + ']').prop("checked")) {
        for (var i = 0; i < checks.length; i++) {
            if (checks[i].id.split('-')[0] == allId && checks[i].id != id) {
                if ((checks[i].id.split('-').length > 2 && id.split('-').length > 2) && checks[i].id.split('-')[0] != "吊装环") {
                    if (checks[i].id.split('-')[2] == id.split('-')[2])
                        checks[i].checked = false;
                }
            }
        }
        $('input[id=' + id + ']').prop("checked", true);
    }
    else {
        for (var i = 0; i < checks.length; i++) {
            if (checks[i].id.split('-')[0] == allId && checks[i].id == id)
                checks[i].checked = false;
        }
    }

}

// 实现checkbox单选
function selChange1(id) {
    var allId = id.split('-')[0];
    var checks = document.getElementsByTagName("input");

    if ($('input[id=' + id + ']').prop("checked")) {
        for (var i = 0; i < checks.length; i++) {
            if (checks[i].id.split('-')[0] == allId && checks[i].id != id) {
                if (checks[i].id.split('-').length > 2 && id.split('-').length > 2) {
                    if (checks[i].id.split('-')[2] == id.split('-')[2])
                        checks[i].checked = false;
                }
            }
        }
        $('input[id=' + id + ']').prop("checked", true);
    }
    else {
        for (var i = 0; i < checks.length; i++) {
            if (checks[i].id.split('-')[0] == allId && checks[i].id != id)
                checks[i].checked = false;
        }
    }

}

// 检查所输入的仪表编号是否在数据库里存在 存在则加载 不存在不加载
function CheckMeterInfo() {
    if ($("#strRepairID").val() != "" && $("#strRepairIDUT").val() == "") {
        MeterID = $("#strMeterID").val();
        ModelType = $("#strModelType").val();
    }
    else {
        MeterID = $("#strMeterIDUT").val();
        ModelType = $("#strModelTypeUT").val();
    }

    $.ajax({
        url: "CheckMeterInfo",
        type: "post",
        data: { MeterID: MeterID, ModelType: ModelType },
        dataType: "json",
        loadonce: false,
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    // 非超声波
                    if ($("#strRepairID").val() != "" && $("#strRepairIDUT").val() == "") {
                        $("#strCertifID").val(json[i].CertifID);
                        $("#strMeterName").val(json[i].MeterName);
                        $("#strManufacturer").val(json[i].Manufacturer);
                        $("#strPrecision").val(json[i].Precision);
                        $("#strModel").val(json[i].Model);
                        $("#strFactoryDate").val(json[i].FactoryDate);
                        $("#strRecordNum").val(json[i].RecordNum);
                        $("#strFlowRange").val(json[i].FlowRange);
                        $("#strPressure").val(json[i].Pressure);
                        $("#strCaliber").val(json[i].Caliber);
                        $("#strPreUnit").val(json[i].PreUnit);
                        $("#strNewUnit").val(json[i].NewUnit);
                    }
                    else {
                        $("#strCertifIDUT").val(json[i].CertifID);
                        $("#strMeterNameUT").val(json[i].MeterName);
                        $("#strManufacturerUT").val(json[i].Manufacturer);
                        $("#strModelUT").val(json[i].Model);
                        $("#strCirNumUT").val(json[i].CirNum);
                        $("#strCirVersionUT").val(json[i].CirVersion);
                        $("#strFactoryDateUT").val(json[i].FactoryDate);
                        $("#strFlowRangeUT").val(json[i].FlowRange);
                        $("#strPressureUT").val(json[i].Pressure);
                        $("#strCaliberUT").val(json[i].Caliber);
                    }
                }
            }
        }
    });
}
