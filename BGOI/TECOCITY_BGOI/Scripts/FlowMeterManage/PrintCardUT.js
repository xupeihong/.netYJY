
$(document).ready(function () {
    //
    var FirstCheck = $("#StrFirstCheckUT").val();
    if (FirstCheck == 0) {
        $("input[name=FirstCheckUT]:eq(0)").attr("checked", 'checked');
    }
    else {
        $("input[name=FirstCheckUT]:eq(1)").attr("checked", 'checked');
    }
    var SecondCheck = $("#StrSecondCheckUT").val();
    if (SecondCheck == 0) {
        $("input[name=SecondCheckUT]:eq(0)").attr("checked", 'checked');
    }
    else {
        $("input[name=SecondCheckUT]:eq(1)").attr("checked", 'checked');
    }
    var ThirdCheck = $("#StrThirdCheckUT").val();
    if (ThirdCheck == 0) {
        $("input[name=ThirdCheckUT]:eq(0)").attr("checked", 'checked');
    }
    else {
        $("input[name=ThirdCheckUT]:eq(1)").attr("checked", 'checked');
    }

    // 加载仪表检查项
    LoadCheckDetailUT();
    // 为checkbox 赋值 
    GetChecked();

    //
    if ($("#HideType").val() == "物流取表") {
        $("#LogisticUT").show();
        $("#UserInfoUT").hide();
    }
    else if ($("#HideType").val() == "取表人") {
        $("#UserInfoUT").show();
        $("#LogisticUT").hide();
    }

    // 打印 
    $("#btnPrint").click(function () {
        document.getElementById("btnPrint").className = "Noprint";

        pagesetup("0.3", "0", "0.3", "0.3");
        window.print();
        document.getElementById("btnPrint").className = "btn";

    });

})

//
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
                        if (i % 3 == 0)
                            tr = $('<tr style="height:50px;"></tr>');
                        var td = "";
                        for (var key in n) {
                            var keyValue = '';
                            if (key == 'CheckContent') {
                                var arryContent = n[key].split('/');
                                if (arryContent.length > 1) {// 有选项 
                                    var arryValues1 = arryContent[0].split(',');
                                    var arryValues2 = arryContent[1].split(',');

                                    td = $("<td id='" + n['CheckItem'] + "'>" + n['CheckItem'] + "</td>");
                                    td.appendTo(tr);
                                    if (arryValues1.length > 1) {
                                        var id1 = n['CheckItem'] + '-' + arryValues1[0] + '-1';
                                        var id2 = n['CheckItem'] + '-' + arryValues1[1] + '-1';
                                        var id3 = n['CheckItem'] + '-' + arryValues2[0] + '-2';
                                        var id4 = n['CheckItem'] + '-' + arryValues2[1] + '-2';

                                        var numC = parseInt(i);
                                        if ((numC + 1) % 3 == 0 || (numC - 1) % 3 == 0) {
                                            td = $("<td style='text-align:center;' colspan='2'>"
                                            + arryValues1[0] + "<input type='checkbox' name='ck' id='" + id1 + "' value='" + arryValues1[0] + "' onclick=\"selChange('" + id1 + "')\"/> &nbsp;&nbsp;"
                                            + arryValues1[1] + "<input type='checkbox' name='ck' id='" + id2 + "' value='" + arryValues1[1] + "' onclick=\"selChange('" + id2 + "')\"/></br></br>"
                                            + arryValues2[0] + "<input type='checkbox' name='ck' id='" + id3 + "' value='" + arryValues2[0] + "' onclick=\"selChange('" + id3 + "')\"/>&nbsp;&nbsp;"
                                            + arryValues2[1] + "<input type='checkbox' name='ck' id='" + id4 + "' value='" + arryValues2[1] + "' onclick=\"selChange('" + id4 + "')\"/></td>");
                                        }
                                        else {
                                            td = $("<td style='text-align:center;'>"
                                                + arryValues1[0] + "<input type='checkbox' name='ck' id='" + id1 + "' value='" + arryValues1[0] + "' onclick=\"selChange('" + id1 + "')\"/> &nbsp;&nbsp;"
                                                + arryValues1[1] + "<input type='checkbox' name='ck' id='" + id2 + "' value='" + arryValues1[1] + "' onclick=\"selChange('" + id2 + "')\"/></br></br>"
                                                + arryValues2[0] + "<input type='checkbox' name='ck' id='" + id3 + "' value='" + arryValues2[0] + "' onclick=\"selChange('" + id3 + "')\"/>&nbsp;&nbsp;"
                                                + arryValues2[1] + "<input type='checkbox' name='ck' id='" + id4 + "' value='" + arryValues2[1] + "' onclick=\"selChange('" + id4 + "')\"/></td>");
                                        }
                                    }
                                    else {
                                        var id1 = n['CheckItem'] + '-' + arryValues1[0] + '-1';
                                        var id2 = n['CheckItem'] + '-' + arryValues2[0] + '-2';
                                        var id3 = n['CheckItem'] + '-' + arryValues2[1] + '-2';

                                        var numC = parseInt(i);
                                        if ((numC + 1) % 3 == 0 || (numC - 1) % 3 == 0) {
                                            td = $("<td style='text-align:center;' colspan='2'>"
                                                + arryValues1[0] + "<input type='checkbox' name='ck' id='" + id1 + "'  value='" + arryValues1[0] + "' onclick=\"selChange('" + id1 + "')\"/></br></br>"
                                                + arryValues2[0] + "<input type='checkbox' name='ck' id='" + id2 + "'  value='" + arryValues2[0] + "' onclick=\"selChange('" + id2 + "')\"/>&nbsp;&nbsp;"
                                                + arryValues2[1] + "<input type='checkbox' name='ck' id='" + id3 + "'  value='" + arryValues2[1] + "' onclick=\"selChange('" + id3 + "')\"/></td>");
                                        }
                                        else {
                                            td = $("<td style='text-align:center;'>"
                                                + arryValues1[0] + "<input type='checkbox' name='ck' id='" + id1 + "'  value='" + arryValues1[0] + "' onclick=\"selChange('" + id1 + "')\"/></br></br>"
                                                + arryValues2[0] + "<input type='checkbox' name='ck' id='" + id2 + "'  value='" + arryValues2[0] + "' onclick=\"selChange('" + id2 + "')\"/>&nbsp;&nbsp;"
                                                + arryValues2[1] + "<input type='checkbox' name='ck' id='" + id3 + "'  value='" + arryValues2[1] + "' onclick=\"selChange('" + id3 + "')\"/></td>");
                                        }
                                    }
                                    td.appendTo(tr);
                                }
                                else {
                                    var arryValues = arryContent[0].split(',');
                                    var id1 = n['CheckItem'] + '-' + arryValues[0] + '-1';
                                    var id2 = n['CheckItem'] + '-' + arryValues[1] + '-1';

                                    td = $("<td id='" + n['CheckItem'] + "'>" + n['CheckItem'] + "</td>");
                                    td.appendTo(tr);

                                    var numC = parseInt(i);
                                    if ((numC + 1) % 3 == 0 || (numC - 1) % 3 == 0) {
                                        td = $("<td style='text-align:center;' colspan='2'>"
                                            + arryValues[0] + "<input type='checkbox' name='ck' id='" + id1 + "' value='" + arryValues[0] + "' onclick=\"selChange('" + id1 + "')\"/>&nbsp;&nbsp;"
                                            + arryValues[1] + "<input type='checkbox' name='ck' id='" + id2 + "' value='" + arryValues[1] + "' onclick=\"selChange('" + id2 + "')\"/></td>");
                                    }
                                    else {
                                        td = $("<td style='text-align:center;'>"
                                            + arryValues[0] + "<input type='checkbox' name='ck' id='" + id1 + "' value='" + arryValues[0] + "' onclick=\"selChange('" + id1 + "')\"/>&nbsp;&nbsp;"
                                            + arryValues[1] + "<input type='checkbox' name='ck' id='" + id2 + "' value='" + arryValues[1] + "' onclick=\"selChange('" + id2 + "')\"/></td>");
                                    }
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

// 为checkbox 赋值 
function GetChecked() {
    // 先获取
    $.ajax({
        url: "GetCheckedsUT",
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
