
$(document).ready(function () {
    $("#pageContent").height($(window).height());

    //意见相同时： 0 否, 1 是
    if ($("#strIsRepairY").val() == '1') {
        $("#YS").show();
        $("#YD").hide();
    }
    else {
        $("#YS").hide();
        $("#YD").show();
    }
    if ($("#strYChangeBak").val() == '1') {
        $("#YC1").show();
        $("#YC2").hide();
    }
    else {
        $("#YC1").hide();
        $("#YC2").show();
    }
    if ($("#strYUnChangeBak").val() == '1') {
        $("#YU1").show();
        $("#YU2").hide();
    }
    else {
        $("#YU1").hide();
        $("#YU2").show();
    }    

    //意见不相同时： 0 否, 1 是
    if ($("#strIsRepairN").val() == '1') {
        $("#NS").show();
        $("#ND").hide();
    }
    else {
        $("#NS").hide();
        $("#ND").show();
    }
    if ($("#strNChangeBak").val() == '1') {
        $("#NC1").show();
        $("#NC2").hide();
    }
    else {
        $("#NC1").hide();
        $("#NC2").show();
    }
    if ($("#strNUnChangeBak").val() == '1') {
        $("#NU1").show();
        $("#NU2").hide();
    }
    else {
        $("#NU1").hide();
        $("#NU2").show();
    }    

    // 加载出厂测试项
    LoadCheckDetail();

    // 为checkbox 赋值 
    GetChecked();

    getChangeBakList();

})

//
function LoadCheckDetail() {
    $.ajax({
        url: "GetOutCheck",
        type: "post",
        data: {},
        dataType: "json",
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
                    var countNum = 0;
                    $.each(datas.CheckItems, function (i, n) {
                        var td = "";
                        if (i == 0) {
                            tr = $('<tr></tr>')
                            td = $("<td rowspan='2' colspan='2' class='textLeftL'>高频检测</td>");
                            td.appendTo(tr);
                        }
                        else if (i % 3 == 0) {
                            tr = $('<tr></tr>')
                            if (i / 3 == 2) {
                                td = $("<td rowspan='3' colspan='2' class='textLeftL'>低频检测</td>");
                                td.appendTo(tr);
                            }
                        }

                        for (var key in n) {
                            var keyValue = '';
                            if (key == 'CheckContent') {
                                var arryContent = n[key].split(',');// 通过,未通过
                                if (arryContent.length > 1) {// 有选项
                                    var id1 = n['CheckItem'] + '-' + arryContent[0] + '-1';
                                    var id2 = n['CheckItem'] + '-' + arryContent[1] + '-1';
                                    td = $("<td class='textLeftL' id='" + n['CheckItem'] + "'>" + n['CheckItem'] + "</td>");
                                    td.appendTo(tr);

                                    td = $("<td class='textRightL' style='text-align:center;'>"
                                            + arryContent[0] + "<input type='checkbox' name='ck' id='" + id1
                                            + "' value='" + arryContent[0] + "' onclick=\"selChange('" + id1 + "')\"/>&nbsp;&nbsp;\n"
                                            + arryContent[1] + "<input type='checkbox' name='ck' id='" + id2
                                            + "' value='" + arryContent[1] + "' onclick=\"selChange('" + id2 + "')\"/></td>");

                                    td.appendTo(tr);
                                }
                                else {
                                    var id3 = n['CheckItem'] + '-1';
                                    td = $("<td class='textLeftL' id='" + n['CheckItem'] + "'>" + n['CheckItem'] + "</td>");
                                    td.appendTo(tr);

                                    td = $("<td class='textRightL' style='text-align:center;'>" + n[key] + "</td>");
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

// 获取更换部件列表 
function getChangeBakList() {
    var sss = $("#strRID").val();
    $.ajax({
        url: "getChangeBakList",
        type: "post",
        data: { RID: $("#strRID").val() },
        dataType: "json",
        success: function (data) {
            if (data.success == "false") {
                if (data.Msg != "") {
                    FillNone();
                    return;
                }
            }
            else {
                FillData(data);
            }
        }
    })
}

// 填充更换部件列表 
function FillData(data) {
    if ($("#ChangeBak_tableLayout").length != 0) {
        $("#ChangeBak_tableLayout").before($("#ChangeBak"));
        $("#ChangeBak_tableLayout").empty();
    }
    if (data.ChangeBakList != "") {
        var tab = $("#ChangeBak");
        tab.html('');
        var countNum = data.ChangeBakList.length + 1;
        var datas = eval("(" + data.ChangeBakList + ")");
        var tr = $('<tr><td rowspan="' + countNum + '" style="width:10px;" class="textLeftL">更换部件</td><td colspan="2" class="textLeftL">名称</td><td colspan="2" class="textLeftL">规格型号</td><td class="textLeftL">数量</td><td colspan="2" class="textLeftL">备注</td></tr>');
        tr.appendTo(tab);

        $.each(datas.ChangeBakList, function (i, n) {
            var tr = $('<tr></tr>');
            var td = '';
            for (var key in n) {
                var keyValue = n[key];
                if (key == "BakNum")
                    td = $('<td>' + keyValue + '</td>');
                else
                    td = $('<td colspan="2">' + keyValue + '</td>');
                td.appendTo(tr);
            }
            tr.appendTo(tab);
        });

        $("#ChangeBak").width = $("#pageContent").width - 20;
        $("#ChangeBak").height = 200;

    }
    else {
        FillNone();
    }

}

// 
function FillNone() {
    var tab = $("#ChangeBak");
    tab.html('');
    var tr = $('<tr><td rowspan="6" style="width:10px;">更换部件</td><td colspan="2">名称</td><td colspan="2">规格型号</td><td>数量</td><td colspan="2">备注</td></tr>');
    tr.appendTo(tab);
    for (var row = 0; row < 5; row++) {
        var tr1 = $('<tr><td colspan="2"></td><td colspan="2"></td><td></td><td colspan="2"></td></tr>');
        tr1.appendTo(tab);
    }
    $("#ChangeBak").width = $("#pageContent").width - 20;
    $("#ChangeBak").height = 200;
}

// 实现checkbox单选
function selChange(id) {
    var allId = id.split('-')[0];
    var checks = document.getElementsByTagName("input");

    if ($('input[id=' + id + ']').prop("checked")) {
        for (var i = 0; i < checks.length; i++) {
            if (checks[i].id.split('-')[0] == allId && checks[i].id != id) {
                if (checks[i].id.split('-').length > 1 && id.split('-').length > 1) {
                    if (checks[i].id.split('-')[0] == id.split('-')[0])
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

// 为checkbox 赋值 
function GetChecked() {
    // 先获取
    $.ajax({
        url: "GetOutCheckeds",
        type: "post",
        data: { RID: $("#strRID").val() },
        dataType: "json",
        success: function (data) {
            if (data.success == "false") {
                return;
            }
            else {
                var checks = document.getElementsByTagName("input");
                var strCheckItem = data.CheckItem;
                var strCheckContent = data.CheckContent;
                //
                var CheckItem = strCheckItem.split('?');
                var CheckContent = strCheckContent.split('?');
                for (var i = 0; i < checks.length; i++) {
                    var checkID = checks[i].id;
                    for (var k = 0 ; k < CheckContent.length; k++) {
                        var ids = CheckContent[k].split(',');
                        var id1 = CheckItem[k] + "-" + ids[0] + "-1";
                        if (checkID == id1)
                            document.getElementById(checkID).setAttribute("checked", "checked");

                    }
                }
            }
        }
    });

}
