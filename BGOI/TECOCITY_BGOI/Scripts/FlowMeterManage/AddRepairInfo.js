var isConfirm = false;

var j = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    var Request = GetRequest();
    var RID = Request["RID"];
    $("#StrRID").val(RID);
    $("#QD").click(function () {
        isConfirm = confirm("确定要建新项目吗")
        if (isConfirm == false) {
            return false;
        }
        else {
            submitInfo();
           
        }
    })
    LoadChangeBakList();


    // 添加部件更换记录 
    $("#addContent").click(function () {
        j++;
        var testTable = document.getElementById("taskList");
        var newTr = testTable.insertRow();
        newTr.id = "row" + j;

        //部件名称
        var newTd1 = newTr.insertCell();
        newTd1.className = "textright";
        newTd1.style.width = "150px";
    
        newTd1.innerHTML = "<input  id='ProName" + j + "' onclick='SearchProduct(\"" + j + "\")' type='text' style='width:150px;' readonly='readonly'/>";
        //规格型号 
        var newTd2 = newTr.insertCell();
        newTd2.className = "textright";
        newTd2.style.width = "150px";
        newTd2.innerHTML = "<input id='Spec" + j + "' type='text' style='width:150px;' readonly='readonly' />";

        //单位
        var newTd3 = newTr.insertCell();
        newTd3.className = "textright";
        newTd3.style.width = "80px";
        newTd3.innerHTML = "<input id='Units" + j + "' type='text' style='width:80px;' readonly='readonly' />";

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
})




function Voluation(t) {
    $("#listdiv").css("display", "none");
    var j = $("#id").val();
    $("#ProName" + j).val($(t)[0].childNodes['0'].innerHTML);
   $("#Spec" + j).val($(t)[0].childNodes['1'].innerHTML);
   $("#Units" + j).val($(t)[0].childNodes['2'].innerHTML);

}
function CL() {
    $("#listdiv").css("display", "none");
}

function SearchProduct(t) {
    
    $("#id").val(t);
    $("#listdiv").css("display", "block")
    $.ajax({
        url: "GetComponent",
        type: "post",
        data: {},
        dataType: "Json",
        success: function (obj) {
            if (obj.success == "false") {

                return;
            }
            else {

                var data = eval("(" + obj.data + ")");
                var str = ""
                for (var k = 1 ; k <= data.rows.length; k++) {

                    if (k == 1) {
                        str += "<tr style='line-height:30px'>"
                    }
                    str += "<td style='width:100px' onclick='Voluation(this)'><lable id='ProName" + k + "' >" + data.rows[k - 1]["ProName"] + "</lable>"
                    str += "<lable id='Spec" + k + "' style='display:none' >" + data.rows[k - 1]["Spec"] + "</lable>"
                    str += "<lable id='Units" + k + "' style='display:none'>" + data.rows[k - 1]["Units"] + "</lable></td>";

                    if (k % 3== 0 && k != 0) {
                        if (k == data.rows.length)
                            str += "</tr>";
                        else
                            str += "</tr><tr style='line-height:30px'>";
                    }

                }

                document.getElementById('list').innerHTML = str;
            }
        }
    });
}

function SearchProductByName() {


    $.ajax({
        url: "GetComponent",
        type: "post",
        data: { name: $("#keyname").val() },
        dataType: "Json",
        success: function (obj) {
            document.getElementById('list').innerHTML = "";
            if (obj.success == "false") {
                return;
            }
            else {

                var data = eval("(" + obj.data + ")");
                var str = ""
                for (var k = 1 ; k <= data.rows.length; k++) {
                    if (k == 1) {
                        str += "<tr style='line-height:30px'>"
                    }
                    str += "<td style='width:150px' onclick='Voluation(this)'><lable id='ProName" + k + "' >" + data.rows[k - 1]["ProName"] + "</lable>"
                    str += "<lable id='Spec" + k + "' style='display:none' >" + data.rows[k - 1]["Spec"] + "</lable>"
                    str += "<lable id='Units" + k + "' style='display:none'>" + data.rows[k - 1]["Units"] + "</lable></td>";
                    if (k % 3 == 0 && k != 0) {
                        if (k == data.rows.length)
                            str += "</tr>";
                        else
                            str += "</tr><tr style='line-height:30px'>";
                    }
                }
                document.getElementById('list').innerHTML = str;
            }
        }
    });
}





// 获取更换部件列表 
function GetChangeBakList() {

    var tab = document.getElementById("taskList");
    var tds = tab.getElementsByTagName("tr");
    var leng = document.getElementById("taskList").rows.length;
    if (leng == 2) {

        $("#BakName").val("");
        $("#BakType").val("");
        $("#Measure").val("");
        $("#BakNum").val("");
        return;

    }
    else {

        var BakName;// 部件名称 
        var BakType;// 规格型号
        var Measure;//单位
        var BakNum;// 数量


        for (var i = 2 ; i < tds.length; i++) {


            // 部件名称
            var td1 = tds[i].getElementsByTagName("td")[0].getElementsByTagName("INPUT")[0].value;
            BakName += td1 + ",";

            // 规格型号
            var td2 = tds[i].getElementsByTagName("td")[1].getElementsByTagName("INPUT")[0].value;
            BakType += td2 + ",";

            // 单位
            var td3 = tds[i].getElementsByTagName("td")[2].getElementsByTagName("INPUT")[0].value;
            Measure += td3 + ",";

            // 数量
            var td4 = tds[i].getElementsByTagName("td")[3].getElementsByTagName("INPUT")[0].value;
            BakNum += td4 + ",";

        }


        BakName = BakName.substr(9);

        BakType = BakType.substr(9);

        Measure = Measure.substr(9);

        BakNum = BakNum.substr(9);




        $("#BakName").val(BakName);
        $("#BakType").val(BakType);
        $("#Measure").val(Measure);
        $("#BakNum").val(BakNum);


    }
}

function deleteTr(id) {
    var strid = id.id;
    $("tr[id=" + strid + "]").remove();
}
function returnConfirm() {
    return false;
}
function submitInfo() {
    GetChangeBakList();
    var options = {
        url: "InsertRepairInfo",
        data: { Name: $("#BakName").val(), Type: $("#BakType").val(), M: $("#Measure").val(), Num: $("#BakNum").val() },
        type: 'POST',
        async: false,
        success: function (data) {
            if (data.success == "true") {
                window.parent.frames["iframeRight"].reload();
                alert(data.Msg);
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

function GetRequest() {

    var url = location.search; //获取url中"?"符后的字串

    var theRequest = new Object();

    if (url.indexOf("?") != -1) {

        var str = url.substr(1);

        strs = str.split("&");

        for (var i = 0; i < strs.length; i++) {

            theRequest[strs[i].split("=")[0]] = unescape(strs[i].split("=")[1]);

        }

    }

    return theRequest;

}


function LoadChangeBakList() {
    // 加载部件更换记录
    $.ajax({
        url: "RepairDevice",
        type: "post",
        data: { RID: $("#StrRID").val() },
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
                    newTd1.style.width = "150px";
                    newTd1.innerHTML = "<input type='text' style='width:150px;' value='" + BakName[k] + "'/>";

                    //规格型号
                    var newTd2 = newTr.insertCell();
                    newTd2.className = "textright";
                    newTd2.style.width = "150px";
                    newTd2.innerHTML = "<input type='text' value='" + BakType[k] + "' style='width:150px;'/>";

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
}