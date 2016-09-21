
$(document).ready(function () {
    $("#pageContent").height($(window).height());

    // 加载仪表检查项
    LoadCheckDetail();

    // 为checkbox 赋值 
    GetChecked();

    // 获取操作历史记录
    GetoperateLog();

    //
    if ($("#HideType").val() == "物流取表") {
        $("#Logistic").show();
        $("#UserInfo").hide();
    }
    else if ($("#HideType").val() == "取表人") {
        $("#UserInfo").show();
        $("#Logistic").hide();
    }

})

//
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

                                    td = $("<td id='" + n['CheckItem'] + "' class='textLeftL'>" + n['CheckItem'] + "</td>");
                                    td.appendTo(tr);
                                    if (arryValues1.length > 1) {
                                        var id1 = n['CheckItem'] + '-' + arryValues1[0] + '-1';
                                        var id2 = n['CheckItem'] + '-' + arryValues1[1] + '-1';
                                        var id3 = n['CheckItem'] + '-' + arryValues2[0] + '-2';
                                        var id4 = n['CheckItem'] + '-' + arryValues2[1] + '-2';

                                        td = $("<td style='text-align:center;' class='textRightL'>"
                                            + arryValues1[0] + "<input type='checkbox' name='ck' id='" + id1 + "' value='" + arryValues1[0] + "' onclick=\"selChange('" + id1 + "')\"/> &nbsp;&nbsp;"
                                            + arryValues1[1] + "<input type='checkbox' name='ck' id='" + id2 + "' value='" + arryValues1[1] + "' onclick=\"selChange('" + id2 + "')\"/></br></br>"
                                            + arryValues2[0] + "<input type='checkbox' name='ck' id='" + id3 + "' value='" + arryValues2[0] + "' onclick=\"selChange('" + id3 + "')\"/>&nbsp;&nbsp;"
                                            + arryValues2[1] + "<input type='checkbox' name='ck' id='" + id4 + "' value='" + arryValues2[1] + "' onclick=\"selChange('" + id4 + "')\"/></td>");
                                    }
                                    else {
                                        var id1 = n['CheckItem'] + '-' + arryValues1[0] + '-1';
                                        var id2 = n['CheckItem'] + '-' + arryValues2[0] + '-2';
                                        var id3 = n['CheckItem'] + '-' + arryValues2[1] + '-2';

                                        td = $("<td style='text-align:center;' class='textRightL'>"
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

                                    td = $("<td id='" + n['CheckItem'] + "' class='textLeftL'>" + n['CheckItem'] + "</td>");
                                    td.appendTo(tr);
                                    td = $("<td style='text-align:center;' class='textRightL'>"
                                        + arryValues[0] + "<input type='checkbox' name='ck' id='" + id1 + "' value='" + arryValues[0] + "' onclick=\"selChange('" + id1 + "')\"/>&nbsp;&nbsp;"
                                        + arryValues[1] + "<input type='checkbox' name='ck' id='" + id2 + "' value='" + arryValues[1] + "' onclick=\"selChange('" + id2 + "')\"/></td>");

                                    td.appendTo(tr);
                                }
                            }
                        }
                        tr.appendTo(tab);
                    });

                    td = $("<td class='textLeftL'></td>");
                    td.appendTo(tr);
                    td = $("<td class='textRightL'></td>");
                    td.appendTo(tr);
                    tr.appendTo(tab);
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

// 为checkbox 赋值 
function GetChecked() {
    // 先获取
    $.ajax({
        url: "GetCheckeds",
        type: "post",
        data: { RID: $("#RID").val() },
        dataType: "json",
        async: false, //是否异步
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
                        // 两种类别的选项 
                        if (ids.length > 1) {
                            var id1 = CheckItem[k] + "-" + ids[0] + "-1";
                            var id2 = CheckItem[k] + "-" + ids[1] + "-2";
                            if (checkID == id1 || checkID == id2)
                                $('input[id=' + checkID + ']').prop("checked", true);

                        }
                        else {
                            var id3 = CheckItem[k] + "-" + ids[0] + "-1";
                            if (checkID == id3)
                                $('input[id=' + checkID + ']').prop("checked", true);

                        }
                    }
                }
            }
        }
    });

}

//
function GetoperateLog() {
    $.ajax({
        url: "GetoperateLog",
        type: "post",
        data: { RID: $("#RID").val() },
        dataType: "json",
        async: false, //是否异步
        success: function (data) {
            if (data.success == "false") {
                return;
            }
            else {
                FillData(data);
            }
        }
    });
}

//
function FillData(data) {
    if ($("#operateLog_tableLayout").length != 0) {
        $("#operateLog_tableLayout").before($("#operateLog"));
        $("#operateLog_tableLayout").empty();
    }
    if (data.OperateList != "") {
        var tab = $("#OperateList");
        tab.html('');
        var datas = eval("(" + data.OperateList + ")");
        $.each(datas.OperateList, function (i, n) {
            var tr = $('<tr></tr>');
            var td = '';
            for (var key in n) {
                var keyValue = n[key];
                if (key = "LogContent")
                    td = $('<td style="width:200px;">' + keyValue + '</td>');
                else
                    td = $('<td style="width:100px;">' + keyValue + '</td>');
                td.appendTo(tr);
            }
            tr.appendTo(tab);
        });

        $("#OperateList").width = $("#pageContent").width - 20;
        $("#OperateList").height = 200;

    }

}
