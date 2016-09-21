var i = 0;
var j = 0;
var h = 0;
var strSelect;
var strSelect2;
var strSelect3;
$(document).ready(function () {
    $("#hole").height($(window).height());
    //loadUser();
    loadAppType();
    $.ajax({
        url: "GetAppType",
        type: "post",
        data: {},
        async: false,
        dataType: "Json",
        success: function (data) {
            if (data.success == "true") {
                var strSel = data.Strid;
                var strVal = data.Strname;
                var sel = strSel.split(',');
                var val = strVal.split(',');
                $.ajax({
                    url: "GetHaveContent",
                    type: "post",
                    data: { data1: $("#Content").val() },
                    dataType: "Json",
                    success: function (data) {
                        if (data.success == "true") {
                            var content = data.Msg;
                            var arr = content.split('@');
                            for (var k = 0 ; k < arr.length; k++)
                            {
                                var brr = arr[k].split('/');
                                j++;
                                var testTable = document.getElementById("task");
                                var newTr = testTable.insertRow();
                                newTr.id = "row" + j;

                                var newTd0 = newTr.insertCell();
                                newTd0.className = "textright";
                                newTd0.style.width = "17%";
                                newTd0.innerHTML = j + "<input type='hidden' value='" + (j - 1) + "'/>";

                                var newTd1 = newTr.insertCell();
                                newTd1.className = "textright";
                                newTd1.style.width = "34%";
                                newTd1.innerHTML = "<input type='text' id='Text" + j + "' value='"+brr[1]+"' readonly='true' onkeypress='return !(event.keyCode==8)' onkeydown='return !(event.keyCode==8)' style='width:80%'/> <a onclick='openChoseUser(" + j + ")' style='color:blue;cursor:pointer;'>选择人员</a>";

                                var newTd2 = newTr.insertCell();
                                newTd2.className = "textright";
                                newTd2.style.width = "17%";
                                newTd2.innerHTML = "<select id='App" + j + "' onchange = 'loadtd(" + j + ")' style='width:160px;'></select>";

                                var select2 = document.getElementById("App" + j);
                                for (var h = 0; h < sel.length; h++) {
                                    var selOption2 = document.createElement("option");
                                    selOption2.value = sel[h];
                                    selOption2.innerHTML = val[h];
                                    select2.appendChild(selOption2);
                                }
                                select2.value = brr[2];

                                var newTd3 = newTr.insertCell();
                                if (brr[3] == "") {
                                    newTd3.className = "textright";
                                    newTd3.style.width = "17%";
                                    newTd3.innerHTML = "<input type='text' id='Tum" + j + "' style='display:none;'/>";
                                }
                                else {
                                    newTd3.className = "textright";
                                    newTd3.style.width = "17%";
                                    newTd3.innerHTML = "<input type='text' id='Tum" + j + "' value='"+brr[3]+"'/>";
                                }

                                var newTd4 = newTr.insertCell();
                                newTd4.className = "textright";
                                newTd4.style.width = "17%";
                                newTd4.innerHTML = "<a onclick='deleteTr(" + newTr.id + ")' style='color:blue;cursor:pointer;'>删除</a>";

                                var newTd5 = newTr.insertCell();
                                newTd5.className = "textright";
                                newTd5.style.display = "none";
                                newTd5.innerHTML = "<input type = 'hidden' id='Hid" + j + "' value = '"+brr[4]+"'/> ";

                                var newTd6 = newTr.insertCell();
                                newTd6.className = "textright";
                                newTd6.style.display = "none";
                                newTd6.innerHTML = "<input type = 'hidden' id='HidW" + j + "' value = '" + brr[5] + "'/> ";
                            }
                        }
                        else {
                            return;
                        }
                    }
                });
            }
            else {
                //document.getElementById("Pip").options.length = 0;
                return;
            }
        }
    });

    $.ajax({
        url: "GetLevel",
        type: "post",
        data: {},
        dataType: "Json",
        success: function (data) {
            if (data.success == "true") {
                var strSel = data.Strid;
                var strVal = data.Strname;
                var sel = strSel.split(',');
                var val = strVal.split(',');
                strSelect = "<select id='level' style='width:160px;'>";
                for (var i = 0; i < sel.length; i++) {
                    strSelect += "<option value=" + sel[i] + ">" + val[i] + "</option>";
                }
                strSelect += "</select>";
            }
            else {
                //document.getElementById("Pip").options.length = 0;
                return;
            }
        }
    });

    $("#addContent").click(function () {
        var tab = document.getElementById("task");
        var tds = tab.getElementsByTagName("tr");
        i = tds.length;
        i++;
        //loadUser();
        loadAppType();
        var testTable = document.getElementById("task");
        var newTr = testTable.insertRow();
        newTr.id = "row" + i;

        var newTd0 = newTr.insertCell();
        newTd0.className = "textright";
        newTd0.style.width = "17%";
        newTd0.innerHTML = i + "<input type='hidden' value='" + (i - 1) + "'/>";

        var newTd1 = newTr.insertCell();
        newTd1.className = "textright";
        newTd1.style.width = "34%";
        newTd1.innerHTML = "<input type='text' id='Text" + i + "' readonly='true' onkeypress='return !(event.keyCode==8)' onkeydown='return !(event.keyCode==8)' style='width:80%'/> <a onclick='openChoseUser(" + i + ")' style='color:blue;cursor:pointer;'>选择人员</a>";

        var newTd2 = newTr.insertCell();
        newTd2.className = "textright";
        newTd2.style.width = "17%";
        newTd2.innerHTML = strSelect3;

        var newTd3 = newTr.insertCell();
        newTd3.className = "textright";
        newTd3.style.width = "17%";
        newTd3.innerHTML = "<input type='text' id='Tum" + i + "' style='display:none;'/>";

        var newTd4 = newTr.insertCell();
        newTd4.className = "textright";
        newTd4.style.width = "17%";
        newTd4.innerHTML = "<a onclick='deleteTr(" + newTr.id + ")' style='color:blue;cursor:pointer;'>删除</a>";

        var newTd5 = newTr.insertCell();
        newTd5.className = "textright";
        newTd5.style.display = "none";
        newTd5.innerHTML = "<input type = 'hidden' id='Hid" + i + "'/> ";

        var newTd6 = newTr.insertCell();
        newTd6.className = "textright";
        newTd6.style.display = "none";
        newTd6.innerHTML = "<input type = 'hidden' id='HidW" + i + "'/> ";
    })

    $("#ProjectformInfo").submit(function () {
        var options = {
            url: "InsertExamine",
            data: {},
            type: 'POST',
            async: false,
            success: function (data) {
                if (data.success == "true") {
                    alert(data.Msg);
                    window.parent.frames["iframeRight"].reload();
                }
                else {
                    alert(data.Msg);
                }
            }
        };
        $("#ProjectformInfo").ajaxSubmit(options);
        return false;
    })

    $("#charge").click(function () {
        var tab = document.getElementById("task");
        var tds = tab.getElementsByTagName("tr");
        var leng = document.getElementById("task").rows.length;
        if (leng == 0) {
            alert("还没有添加工作内容，请点击‘添加'，进行操作");
            return;
        }
        var level;
        var user;
        var apptype;
        var tmun;
        var allcontent;
        for (var i = 0; i < tds.length; i++) {
            var td0 = tds[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].value;

            var td1 = tds[i].getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
            if (td1 == "") {
                alert("人员不能为空，请点击选择人员进行添加"); return;
            }
            var td1H = tds[i].getElementsByTagName("td")[5].getElementsByTagName("input")[0].value;
            var arr = td1H.split(',');
            var td1Hw = tds[i].getElementsByTagName("td")[6].getElementsByTagName("input")[0].value;
            var brr = td1Hw.split(',');

            var td2 = tds[i].getElementsByTagName("td")[2].getElementsByTagName("select")[0].value;
            if (td2 == "") {
                alert("请选择审批方式"); return;
            }
            if (td2 == "3" && tds[i].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value == "") {
                alert("部分审批，请填写人数");
                return;
            }

            var td3 = tds[i].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value;
            for (var j = 0 ; j < arr.length ; j++)
            {
                if(td3 == "")
                    allcontent += arr[j] + "/" + brr[j] + "/" + td0 + "/" + td2 + "@";
                else
                    allcontent += arr[j] + "/" + brr[j] + "/" + td0 + "/" + td2 + "-" + td3 + "@";
            }
        }
        allcontent = allcontent.substr(0, allcontent.length - 1);
        allcontent = allcontent.replace("undefined", "");
        //alert(allcontent);
        var a = confirm("确定保存审批设置信息吗")
        if (a == false)
            return;
        else {
            $("#allcontent").val(allcontent);
            $("#ProjectformInfo").submit();
        }
    })



    $("#ss").click(function () {
        var html = document.getElementById("task").innerHTML;
        var tab = document.getElementById("task");
        var tds = tab.getElementsByTagName("tr");
        var level;
        var user;
        var apptype;
        var tmun;
        var allcontent;
        for (var i = 0; i < tds.length; i++) {
            var id = tds[i].id;
            var td0 = tds[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].value;
            alert(id + "/" + td0);
            var td1 = tds[i].getElementsByTagName("td")[1].getElementsByTagName("input")[0].value; //名字
            var td2 = tds[i].getElementsByTagName("td")[2].getElementsByTagName("select")[0].value;//select
            var td3 = tds[i].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value;//shu
            var td1H = tds[i].getElementsByTagName("td")[5].getElementsByTagName("input")[0].value;//名字id
            var td1Hw = tds[i].getElementsByTagName("td")[6].getElementsByTagName("input")[0].value;//职位
        }
    })

})

function openChoseUser(num)
{
    var tab = document.getElementById("task");
    var tds = tab.getElementsByTagName("tr");
    var td1H = "";
    for (var i = 0; i < tds.length; i++) {
         td1H += tds[i].getElementsByTagName("td")[5].getElementsByTagName("input")[0].value + ",";
    }
    td1H = td1H.substr(0, td1H.length - 1);
    ShowIframe1("选择人员", "ChooseUser?id=" + num + "&userIDs=" + td1H, 550, 450, '')
}
function loadtd(num)
{
    var selectVal = $("#App" + num).val();
    if (selectVal == "3") {
        $("#Tum" + num).css("display", "");
        $("#Tum" + num).val("");
    }
    else {
        $("#Tum" + num).css("display", "none");
        $("#Tum" + num).val("");
    }
}

function loadAppType()
{
    $.ajax({
        url: "GetAppType",
        type: "post",
        data: {},
        async: false,
        dataType: "Json",
        success: function (data) {
            if (data.success == "true") {
                var strSel = data.Strid;
                var strVal = data.Strname;
                var sel = strSel.split(',');
                var val = strVal.split(',');
                strSelect3 = "<select id='App" + i + "' onchange = 'loadtd("+i+")' style='width:160px;'>";
                for (var j = 0; j < sel.length; j++) {
                    strSelect3 += "<option value=" + sel[j] + ">" + val[j] + "</option>";
                }
                strSelect3 += "</select>";
            }
            else {
                //document.getElementById("Pip").options.length = 0;
                return;
            }
        }
    });
}

function deleteTr(obj) {
    var one = confirm("确认删除该行吗？");
    if (one == false)
        return;
    else {
        if (obj) obj.parentNode.removeChild(obj);
        reloadTab();
    }
}

function reloadTab()
{
    var tab = document.getElementById("task");
    var tds = tab.getElementsByTagName("tr");
    var len = tds.length; 
    $.ajax({
        url: "GetAppType",
        type: "post",
        data: {},
        async: false,
        dataType: "Json",
        success: function (data) {
            if (data.success == "true") {
                var strSel = data.Strid;
                var strVal = data.Strname;
                var sel = strSel.split(',');
                var val = strVal.split(',');
                for (var i = 0; i < tds.length; i++) {
                    var td0 = tds[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].value;
                    var td1 = tds[i].getElementsByTagName("td")[1].getElementsByTagName("input")[0].value; //名字
                    var td2 = tds[i].getElementsByTagName("td")[2].getElementsByTagName("select")[0].value;//select
                    var td3 = tds[i].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value;//shu
                    var td1H = tds[i].getElementsByTagName("td")[5].getElementsByTagName("input")[0].value;//名字id
                    var td1Hw = tds[i].getElementsByTagName("td")[6].getElementsByTagName("input")[0].value;//职位
                    h = i;
                    h++;
                    tds[i].id = "row" + h;
                    tds[i].getElementsByTagName("td")[0].innerHTML = h + "<input type='hidden' value='" + (h - 1) + "'/>";
                    tds[i].getElementsByTagName("td")[1].innerHTML = "<input type='text' id='Text" + h + "' value='" + td1 + "' readonly='true' onkeypress='return !(event.keyCode==8)' onkeydown='return !(event.keyCode==8)' style='width:80%'/> <a onclick='openChoseUser(" + h + ")' style='color:blue;cursor:pointer;'>选择人员</a>";
                    tds[i].getElementsByTagName("td")[2].innerHTML ="<select id='App" + h + "' onchange = 'loadtd(" + h + ")' style='width:160px;'></select>";

                    var select2 = document.getElementById("App" + h);
                    for (var k = 0; k < sel.length; k++) {
                        var selOption2 = document.createElement("option");
                        selOption2.value = sel[k];
                        selOption2.innerHTML = val[k];
                        select2.appendChild(selOption2);
                    }
                    select2.value = td2;
                        
                if (td3 == "") {
                    tds[i].getElementsByTagName("td")[3].innerHTML = "<input type='text' id='Tum" + h + "' style='display:none;'/>";
                }
                else {
                    tds[i].getElementsByTagName("td")[3].innerHTML = "<input type='text' id='Tum" + h + "' value='"+td3+"'/>";
                }
                tds[i].getElementsByTagName("td")[4].innerHTML = "<a onclick='deleteTr(row"+h+")' style='color:blue;cursor:pointer;'>删除</a>";

                tds[i].getElementsByTagName("td")[5].innerHTML= "<input type = 'hidden' id='Hid" + h + "' value = '"+td1H+"'/> ";
                tds[i].getElementsByTagName("td")[6].innerHTML = "<input type = 'hidden' id='HidW" + h + "' value = '" + td1Hw + "'/> ";
                }
            }
            else {
                return;
            }
        }
    });
}

function chang(sel1, sel2, txt) {
    var unitId = $("#" + sel1 + "").val();
    $.ajax({
        url: "GetUser",
        type: "post",
        data: { data1: unitId },
        dataType: "Json",
        success: function (data) {
            if (data.success == "true") {
                var strSel = data.Strid;
                var strName = data.Strname;
                var sel = strSel.split(',');
                var Name = strName.split(',');
                var BandingTime = document.getElementById(sel2);
                for (var i = 0; i < sel.length; i++) {
                    var tOption = document.createElement("Option");
                    tOption.value = sel[i];
                    tOption.text = Name[i];
                    BandingTime.add(tOption);
                }
            }
            else {
                document.getElementById(sel2).options.length = 0;
                $("#" + txt + "").val("");
                return;
            }
        }
    });
}

function chang2(sel1, txt) {
    var uid = $("#" + sel1 + "").val();
    $.ajax({
        url: "GetPhone",
        type: "post",
        data: { data1: uid },
        dataType: "Json",
        success: function (data) {
            if (data.success == "true") {
                var phone = data.Strphone;
                $("#" + txt + "").val(phone);
            }
            else {
                $("#" + txt + "").val("");
                return;
            }
        }
    });
}