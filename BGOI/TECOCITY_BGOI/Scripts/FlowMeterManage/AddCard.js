var isConfirm = false;
$(document).ready(function () {
    $("#pageContent").height($(window).height());

    // 加载仪表检查项
    LoadCheckDetail();

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

            $("#StrIsRepair").val($('input:radio[name="IsRepair"]:checked').val());
            submitInfo();
        }

    })

})

function submitInfo() {
    var options = {
        url: "AddNewCard",
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


