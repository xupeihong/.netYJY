var j = 0;

$(document).ready(function () {
    $("#pageContent").height($(window).height());


    // 确定
    $("#QRXG").click(function () {
        var a = confirm("确定要提交修改吗")
        if (a == false)
            return;
        else {

            submit();
        }
    });
    // 添加部件更换记录 
    $("#addContent").click(function () {
        j++;
        var testTable = document.getElementById("taskList");
        var newTr = testTable.insertRow();
        newTr.id = "row" + j;

        //部件名称
        var newTd1 = newTr.insertCell();
        newTd1.className = "textright";
        newTd1.style.width = "200px";
        newTd1.innerHTML = "<input type='text' style='width:200px;'/>";

        //规格型号 
        var newTd2 = newTr.insertCell();
        newTd2.className = "textright";
        newTd2.style.width = "80px";
        newTd2.innerHTML = "<input type='text' style='width:80px;'/>";

        //单位
        var newTd3 = newTr.insertCell();
        newTd3.className = "textright";
        newTd3.style.width = "80px";
        newTd3.innerHTML = "<input type='text' style='width:80px;'/>";

        //数量
        var newTd4 = newTr.insertCell();
        newTd4.className = "textright";
        newTd4.style.width = "80px";
        newTd4.innerHTML = "<input type='text' style='width:80px;'/>";

        var newTd5 = newTr.insertCell();
        newTd5.className = "textright";
        newTd5.style.width = "80px";
        newTd5.innerHTML = "<a onclick='deleteTr(" + newTr.id + ")' style='color:blue;cursor:hand; width:80px;'> 删 除 </a>";

    });
    // 加载部件更换记录
    $.ajax({
        url: "RepairChange",
        type: "post",
        data: { CleanID: $("#StrCleanID").val() },
        dataType: "Json",
        success: function (data) {
            if (data.success == "false") {
                
                return;
            }
            else {
                
                var strBakName = data.BakName;
                var strBakType = data.BakType;
                var strBakNum = data.BakNum;
                var strMeasure = data.Measure;
                //
                var BakName = strBakName.split(',');
                var BakType = strBakType.split(',');
                var BakNum = strBakNum.split(',');
                var Measure = strMeasure.split(',');

                for (var k = 0 ; k < BakName.length; k++) {
                    j++;
                    var testTable = document.getElementById("taskList");
                    var newTr = testTable.insertRow();
                    newTr.id = "row" + j;

                    //部件名称 
                    var newTd1 = newTr.insertCell();
                    newTd1.className = "textright";
                    newTd1.style.width = "200px";
                    newTd1.innerHTML = "<input type='text' style='width:200px;' value='" + BakName[k] + "'/>";

                    //规格型号
                    var newTd2 = newTr.insertCell();
                    newTd2.className = "textright";
                    newTd2.style.width = "80px";
                    newTd2.innerHTML = "<input type='text' value='" + BakType[k] + "' style='width:80px;'/>";

                    //单位
                    var newTd3 = newTr.insertCell();
                    newTd3.className = "textright";
                    newTd3.style.width = "80px";
                    newTd3.innerHTML = "<input type='text' value='" + Measure[k] + "' style='width:80px;'/>";

                    //数量
                    var newTd4 = newTr.insertCell();
                    newTd4.className = "textright";
                    newTd4.style.width = "80px";
                    newTd4.innerHTML = "<input type='text' value='" + BakNum[k] + "' style='width:80px;'/>";

                    var newTd5 = newTr.insertCell();
                    newTd5.className = "textright";
                    newTd5.style.width = "80px";
                    newTd5.innerHTML = "<a onclick='deleteTr(" + newTr.id + ")' style='color:blue;cursor:hand; width:80px;'> 删 除 </a>";
                }
            }
        }
    });

})
// 界面提交
function submit() {

    var options = {
        url: "UpdateCleanRepairSure",
        data: {  },
        type: 'POST',
        async: false,
        success: function (data) {
            if (data.success == "true") {
                alert(data.Msg);
                window.parent.frames["iframeRight"].reload();
                setTimeout('parent.ClosePop()', 100);
            }
            else {
                //alert(data.Msg);
            }
        }
    };
    $("#ProjectformInfo").ajaxSubmit(options);
    return false;
}

function returnConfirm() { return false; }


function deleteTr(id) {
    var strid = id.id;
    $("tr[id=" + strid + "]").remove();
}
