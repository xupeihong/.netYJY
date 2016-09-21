
var isConfirm = false;
var i = 0;
var j = 0;

$(document).ready(function () {
    $("#pageContent").height($(window).height());

    // [维修意见相同]
    var IsIsRepairY = $("#strIsRepairY").val();
    if (IsIsRepairY == 0) {
        $("input[name=IsRepairY]:eq(0)").attr("checked", 'checked');
    }
    else {
        $("input[name=IsRepairY]:eq(1)").attr("checked", 'checked');
    }
    var IsYChangeBak = $("#strYChangeBak").val();
    if (IsYChangeBak == 0) {
        $("input[name=IsYChangeBak]:eq(0)").attr("checked", 'checked');
    }
    else {
        $("input[name=IsYChangeBak]:eq(1)").attr("checked", 'checked');
    }
    var IsYUnChangeBak = $("#strYUnChangeBak").val();
    if (IsYUnChangeBak == 0) {
        $("input[name=IsYUnChangeBak]:eq(0)").attr("checked", 'checked');
    }
    else {
        $("input[name=IsYUnChangeBak]:eq(1)").attr("checked", 'checked');
    }

    // [维修意见不相同]
    var IsRepairN = $("#strIsRepairN").val();
    if (IsRepairN == 0) {
        $("input[name=IsRepairN]:eq(0)").attr("checked", 'checked');
    }
    else {
        $("input[name=IsRepairN]:eq(1)").attr("checked", 'checked');
    }
    var IsNChangeBak = $("#strNChangeBak").val();
    if (IsNChangeBak == 0) {
        $("input[name=IsNChangeBak]:eq(0)").attr("checked", 'checked');
    }
    else {
        $("input[name=IsNChangeBak]:eq(1)").attr("checked", 'checked');
    }
    var IsNUnChangeBak = $("#strNUnChangeBak").val();
    if (IsNUnChangeBak == 0) {
        $("input[name=IsNUnChangeBak]:eq(0)").attr("checked", 'checked');
    }
    else {
        $("input[name=IsNUnChangeBak]:eq(1)").attr("checked", 'checked');
    }



    // 加载出厂测试项
    LoadCheckDetail();

    // 为checkbox 赋值 
    GetChecked();

    // 加载部件更换记录
    $.ajax({
        url: "getChangeBakList2",
        type: "post",
        data: { strRID: $("#strRID").val() },
        dataType: "Json",
        success: function (data) {
            if (data.success == "false") {
                return;
            }
            else {
                var strBakName = data.BakName;
                var strBakType = data.BakType;
                var strBakNum = data.BakNum;
                var strComments = data.Comments;
                //
                var BakName = strBakName.split(',');
                var BakType = strBakType.split(',');
                var BakNum = strBakNum.split(',');
                var Comments = strComments.split('@');

                for (var k = 0 ; k < BakName.length; k++) {
                    j++;
                    var testTable = document.getElementById("taskList");
                    var newTr = testTable.insertRow();
                    newTr.id = "row" + j;

                    //部件名称 
                    var newTd1 = newTr.insertCell();
                    newTd1.className = "textright";
                    newTd1.style.width = "80px";
                    newTd1.innerHTML = "<input type='text' style='width:80px;' value='" + BakName[k] + "'/>";

                    //规格型号
                    var newTd2 = newTr.insertCell();
                    newTd2.className = "textright";
                    newTd2.style.width = "80px";
                    newTd2.innerHTML = "<input type='text' value='" + BakType[k] + "' style='width:80px;'/>";

                    //数量
                    var newTd3 = newTr.insertCell();
                    newTd3.className = "textright";
                    newTd3.style.width = "80px";
                    newTd3.innerHTML = "<input type='text' value='" + BakNum[k] + "' style='width:80px;'/>";

                    //备注
                    var newTd4 = newTr.insertCell();
                    newTd4.className = "textright";
                    newTd4.style.width = "200px";
                    newTd4.innerHTML = "<input type='text' value='" + Comments[k] + "' style='width:200px;'/>";

                    var newTd5 = newTr.insertCell();
                    newTd5.className = "textright";
                    newTd5.style.width = "80px";
                    newTd5.innerHTML = "<a onclick='deleteTr(" + newTr.id + ")' style='color:blue;cursor:hand; width:80px;'> 删 除 </a>";
                }
            }
        }
    });

    // 添加部件更换记录 
    $("#addContent").click(function () {
        var testTable = document.getElementById("taskList");
        var rowLength = testTable.rows.length;
        i = rowLength;
        var newTr = testTable.insertRow();
        newTr.id = "row" + i;

        //部件名称
        var newTd1 = newTr.insertCell();
        newTd1.className = "textright";
        newTd1.style.width = "80px";
        newTd1.innerHTML = "<input type='text' style='width:80px;'/>";

        //规格型号 
        var newTd2 = newTr.insertCell();
        newTd2.className = "textright";
        newTd2.style.width = "80px";
        newTd2.innerHTML = "<input type='text' style='width:80px;'/>";

        //数量
        var newTd3 = newTr.insertCell();
        newTd3.className = "textright";
        newTd3.style.width = "80px";
        newTd3.innerHTML = "<input type='text' style='width:80px;'/>";

        //备注
        var newTd4 = newTr.insertCell();
        newTd4.className = "textright";
        newTd4.style.width = "200px";
        newTd4.innerHTML = "<input type='text' style='width:200px;'/>";

        var newTd5 = newTr.insertCell();
        newTd5.className = "textright";
        newTd5.style.width = "80px";
        newTd5.innerHTML = "<a onclick='deleteTr(" + newTr.id + ")' style='color:blue;cursor:hand; width:80px;'> 删 除 </a>";

    });

    // 确定修改
    $("#QRXG").click(function () {
        isConfirm = confirm("确定要修改随工单吗")
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
                if (i == 0 || i == 2) {
                    title = tds[i].getElementsByTagName("td")[1].innerHTML;
                    strTitle += title + ",";
                    title = tds[i].getElementsByTagName("td")[3].innerHTML;
                    strTitle += title + ",";
                    title = tds[i].getElementsByTagName("td")[5].innerHTML;
                    strTitle += title + ",";

                    // checkbox列 
                    var check1 = "";
                    checks = tds[i].getElementsByTagName("td")[2].getElementsByTagName("INPUT");
                    if (checks.length == 0)
                        strChecked += tds[i].getElementsByTagName("td")[2].innerText;
                    else {
                        for (var j = 0; j < checks.length; j++) {
                            var obj = checks[j];
                            if (obj.type == 'checkbox') {
                                if ($('input[id=' + obj.id + ']').prop("checked"))
                                    check1 += obj.value + ",";
                            }
                        }
                        if (check1 != "")
                            strChecked += check1.substring(0, check1.length - 1);
                    }
                    strChecked += "?";

                    var check2 = "";
                    checks = tds[i].getElementsByTagName("td")[4].getElementsByTagName("INPUT");
                    if (checks.length == 0)
                        strChecked += tds[i].getElementsByTagName("td")[2].innerText;
                    else {
                        for (var k = 0; k < checks.length; k++) {
                            var obj0 = checks[k];
                            if (obj0.type == 'checkbox') {
                                if ($('input[id=' + obj0.id + ']').prop("checked"))
                                    check2 += obj0.value + ",";
                            }
                        }
                        if (check2 != "")
                            strChecked += check2.substring(0, check2.length - 1);
                    }
                    strChecked += "?";

                    var check3 = "";
                    checks = tds[i].getElementsByTagName("td")[6].getElementsByTagName("INPUT");
                    if (checks.length == 0)
                        strChecked += tds[i].getElementsByTagName("td")[2].innerText;
                    else {
                        for (var k = 0; k < checks.length; k++) {
                            var obj0 = checks[k];
                            if (obj0.type == 'checkbox') {
                                if ($('input[id=' + obj0.id + ']').prop("checked"))
                                    check3 += obj0.value + ",";
                            }
                        }
                        if (check3 != "")
                            strChecked += check3.substring(0, check3.length - 1);
                    }
                    strChecked += "?";
                }
                else {
                    title = tds[i].getElementsByTagName("td")[0].innerHTML;
                    strTitle += title + ",";
                    title = tds[i].getElementsByTagName("td")[2].innerHTML;
                    strTitle += title + ",";
                    title = tds[i].getElementsByTagName("td")[4].innerHTML;
                    strTitle += title + ",";

                    // checkbox列 
                    var check1 = "";
                    checks = tds[i].getElementsByTagName("td")[1].getElementsByTagName("INPUT");
                    if (checks.length == 0)
                        strChecked += tds[i].getElementsByTagName("td")[2].innerText;
                    else {
                        for (var j = 0; j < checks.length; j++) {
                            var obj = checks[j];
                            if (obj.type == 'checkbox') {
                                if ($('input[id=' + obj.id + ']').prop("checked"))
                                    check1 += obj.value + ",";
                            }
                        }
                        if (check1 != "")
                            strChecked += check1.substring(0, check1.length - 1);
                    }
                    strChecked += "?";

                    var check2 = "";
                    checks = tds[i].getElementsByTagName("td")[3].getElementsByTagName("INPUT");
                    if (checks.length == 0)
                        strChecked += tds[i].getElementsByTagName("td")[2].innerText;
                    else {
                        for (var k = 0; k < checks.length; k++) {
                            var obj0 = checks[k];
                            if (obj0.type == 'checkbox') {
                                if ($('input[id=' + obj0.id + ']').prop("checked"))
                                    check2 += obj0.value + ",";
                            }
                        }
                        if (check2 != "")
                            strChecked += check2.substring(0, check2.length - 1);
                    }
                    strChecked += "?";

                    var check3 = "";
                    checks = tds[i].getElementsByTagName("td")[5].getElementsByTagName("INPUT");
                    if (checks.length == 0)
                        strChecked += tds[i].getElementsByTagName("td")[2].innerText;
                    else {
                        for (var k = 0; k < checks.length; k++) {
                            var obj0 = checks[k];
                            if (obj0.type == 'checkbox') {
                                if ($('input[id=' + obj0.id + ']').prop("checked"))
                                    check3 += obj0.value + ",";
                            }
                        }
                        if (check3 != "")
                            strChecked += check3.substring(0, check3.length - 1);
                    }
                    strChecked += "?";
                }
            }

            strTitle = strTitle.substring(0, strTitle.length - 1);
            strChecked = strChecked.substring(0, strChecked.length - 1);
            $("#HTitle").val(strTitle);
            $("#HChecked").val(strChecked);

            $("#StrIsIsRepairY").val($('input:radio[name="IsRepairY"]:checked').val());
            $("#StrIsYChangeBak").val($('input:radio[name="IsYChangeBak"]:checked').val());
            $("#StrIsYUnChangeBak").val($('input:radio[name="IsYUnChangeBak"]:checked').val());
            $("#StrIsIsRepairN").val($('input:radio[name="IsRepairN"]:checked').val());
            $("#StrIsNChangeBak").val($('input:radio[name="IsNChangeBak"]:checked').val());
            $("#StrIsNUnChangeBak").val($('input:radio[name="IsNUnChangeBak"]:checked').val());

            GetChangeBakList();
            submitInfo();
        }
    });

})

// 界面提交
function submitInfo() {
    var options = {
        url: "UpdateWorkCard",
        data: {},
        type: 'POST',
        async: false,
        success: function (data) {
            if (data.success == "true") {
                window.parent.frames["iframeRight"].reload();
                alert(data.Msg);
                setTimeout('parent.ClosePop()',10);
            }
            else {
                //alert(data.Msg);
            }
        }
    };
    $("#ProjectformInfo").ajaxSubmit(options);
    return false;
}

// 获取更换部件列表 
function GetChangeBakList() {
    var tab = document.getElementById("taskList");
    var tds = tab.getElementsByTagName("tr");
    var leng = document.getElementById("taskList").rows.length;
    if (leng == 0) {
        $("#RID").val(RID);
        $("#BakName").val("");
        $("#BakType").val("");
        $("#BakNum").val("");
        $("#Comments").val("");
        return;
    }
    else {
        var RID;// 维修内部编号 
        var BakName;// 部件名称 
        var BakType;// 规格型号
        var BakNum;// 数量
        var Comments;// 备注

        for (var i = 1 ; i < tds.length; i++) {
            RID += $("#strRID").val() + ",";

            // 部件名称
            var td1 = tds[i].getElementsByTagName("td")[0].getElementsByTagName("INPUT")[0].value;
            BakName += td1 + ",";

            // 规格型号
            var td2 = tds[i].getElementsByTagName("td")[1].getElementsByTagName("INPUT")[0].value;
            BakType += td2 + ",";

            // 数量
            var td3 = tds[i].getElementsByTagName("td")[2].getElementsByTagName("INPUT")[0].value;
            BakNum += td3 + ",";

            // 备注
            var td4 = tds[i].getElementsByTagName("td")[3].getElementsByTagName("INPUT")[0].value;
            Comments += td4 + "@";

        }

        RID = RID.substr(0, RID.length - 1);
        RID = RID.substr(9);
        BakName = BakName.substr(0, BakName.length - 1);
        BakName = BakName.substr(9);
        BakType = BakType.substr(0, BakType.length - 1);
        BakType = BakType.substr(9);
        BakNum = BakNum.substr(0, BakNum.length - 1);
        BakNum = BakNum.substr(9);
        Comments = Comments.substr(0, Comments.length - 1);
        Comments = Comments.substr(9);

        $("#RID").val(RID);
        $("#BakName").val(BakName);
        $("#BakType").val(BakType);
        $("#BakNum").val(BakNum);
        $("#Comments").val(Comments);

    }
}

function returnConfirm() {
    return false;
}

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
                            td = $("<td rowspan='2' colspan='2' style='text-align:center; width:100px;'>高频检测</td>");
                            td.appendTo(tr);
                        }
                        else if (i % 3 == 0) {
                            tr = $('<tr></tr>')
                            if (i / 3 == 2) {
                                td = $("<td rowspan='3' colspan='2' style='text-align:center; width:100px;'>低频检测</td>");
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
                                    td = $("<td id='" + n['CheckItem'] + "' class='textLeftCard'>" + n['CheckItem'] + "</td>");
                                    td.appendTo(tr);

                                    td = $("<td style='text-align:center;' class='textRightCard'>"
                                            + arryContent[0] + "<input type='checkbox' name='ck' id='" + id1
                                            + "' value='" + arryContent[0] + "' onclick=\"selChange('" + id1 + "')\"/>&nbsp;&nbsp;\n"
                                            + arryContent[1] + "<input type='checkbox' name='ck' id='" + id2
                                            + "' value='" + arryContent[1] + "' onclick=\"selChange('" + id2 + "')\"/></td>");

                                    td.appendTo(tr);
                                }
                                else {
                                    var id3 = n['CheckItem'] + '-1';
                                    td = $("<td id='" + n['CheckItem'] + "' class='textLeftCard'>" + n['CheckItem'] + "</td>");
                                    td.appendTo(tr);

                                    td = $("<td style='text-align:center;'class='textRightCard'>" + n[key] + "</td>");
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

// 删除添加的部件更换记录
function deleteTr(obj) {
    var one = confirm("确认删除该行吗？");
    if (one == false)
        return;
    else {
        if (obj)
            obj.parentNode.removeChild(obj);
    }
}
