var j = 0;
var l = 0;
var b = true;
$(document).ready(function () {
    $("#pageContent").height($(window).height());

    var Request = GetRequest();
    var RID = Request["RID"];
    $("#StrRID").val(RID);
    LoadQuotationList();
    LoadChangeBakList();
    // 确定
    $("#QD").click(function () {
        GetChangeBakList();
        GetQuotationList();
        

        if (b) {
            var a = confirm("确定要提交修改吗")
            if (a == false)
                return;
            else {

                submitInfo();
            }
        } else {
            return
        }

    });


})

// 界面提交
function submitInfo() {

    var options = {
        url: "UpdateQuotationSure",
        data: {
            rid: $("#StrRID").val(), Type: $("#BakType").val(), p: $("#BakUnitPrice").val(),
            strDeviceName: $("#DeviceName").val(), strDeviceType: $("#DeviceType").val(), strNum: $("#Num").val(),
            strUnitPrice: $("#UnitPrice").val(), strTotalPrice: $("#TotalPrice").val(), strComments: $("#Comments").val(),
            strMeasure: $("#Measure").val()
        },
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


function returnConfirm() {
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


//获取报价单列表
function GetQuotationList() {

    var tab = document.getElementById("taskList");
    var tds = tab.getElementsByTagName("tr");
    var leng = document.getElementById("taskList").rows.length;


    var BakType = "";// 类型
    var BakUnitPrice = "";//单价
    var BakConcesioPrice = "";// 优惠价


    for (var i = 2 ; i < tds.length; i++) {

        // 类型
        var td1 = tds[i].getElementsByTagName("td")[0].innerText;
        BakType += td1 + ",";

        // 单价
        var td2 = tds[i].getElementsByTagName("td")[1].getElementsByTagName("INPUT")[0].value;
        if (td2 == "") {
            var id = tds[i].getElementsByTagName("td")[1].getElementsByTagName("INPUT")[0];
            a(id);
        }
        BakUnitPrice += td2 + ",";


    }
    var wx = 0;
    var r = $("#TotalPrice").val().split(',');
    for (var i = 0; i < r.length - 1; i++) {
        wx += parseFloat(r[i]);
    }
    BakType += "维修,";
    BakUnitPrice += wx + ",";

    $("#BakType").val(BakType);
    $("#BakUnitPrice").val(BakUnitPrice);
    $("#BakConcesioPrice").val(BakConcesioPrice);


}

// 获取更换部件列表 
function GetChangeBakList() {

    var tab = document.getElementById("taskList2");
    var tds = tab.getElementsByTagName("tr");
    var leng = document.getElementById("taskList2").rows.length;



    var DeviceName = "";
    var DeviceType = "";
    var Measure = "";
    var Num = "";
    var UnitPrice = "";
    var TotalPrice = "";
    var Comments = "";
    for (var i = 2 ; i < tds.length; i++) {


        // 名称
        var td1 = tds[i].getElementsByTagName("td")[0].innerText;
        DeviceName += td1 + ",";

        // 型号
        var td2 = tds[i].getElementsByTagName("td")[1].innerText;
        DeviceType += td2 + ",";

        // 数量
        var td3 = tds[i].getElementsByTagName("td")[2].innerText;
        Num += td3 + ",";


        // 单价
        var td4 = tds[i].getElementsByTagName("td")[3].getElementsByTagName("INPUT")[0].value;
        if (td4 == "") {
            var id = tds[i].getElementsByTagName("td")[3].getElementsByTagName("INPUT")[0];
            a(id);
        }
        UnitPrice += td4 + ",";

        // 总价
        var td5 = tds[i].getElementsByTagName("td")[4].getElementsByTagName("INPUT")[0].value;

        TotalPrice += td5 + ",";

        // 备注
        var td6 = tds[i].getElementsByTagName("td")[5].getElementsByTagName("INPUT")[0].value;

        Comments += td6 + "@";
        // 单位
        var td7 = tds[i].getElementsByTagName("td")[6].getElementsByTagName("INPUT")[0].value;

        Measure += td7 + ",";
    }

    $("#DeviceName").val(DeviceName);
    $("#DeviceType").val(DeviceType);
    $("#Measure").val(Measure);
    $("#Num").val(Num);
    $("#Num").val(Num);
    $("#UnitPrice").val(UnitPrice);
    $("#TotalPrice").val(TotalPrice);
    $("#Comments").val(Comments);
}
//加载报价单列表
function LoadQuotationList() {
    var Request = GetRequest();
    var RID = Request["RID"];
    $.ajax({
        url: "LoadQuotationList",
        type: "post",
        data: { RID: RID, curpage: "1", rownum: 15 },
        dataType: "Json",
        success: function (obj) {
            var data = eval("(" + obj + ")");
            if (data.success == "false") {

                return;
            }
            else {



                for (var k = 0 ; k < data.rows.length; k++) {
                    if (data.rows[k].Type != "维修") {
                        l++;
                        var testTable = document.getElementById("taskList");
                        var newTr = testTable.insertRow();
                        newTr.id = "row" + l;

                        //类型 
                        var newTd1 = newTr.insertCell();
                        newTd1.className = "textright";
                        newTd1.style.width = "200px";
                        newTd1.innerHTML = data.rows[k].Type;

                        //原价
                        var newTd2 = newTr.insertCell();
                        newTd2.className = "textright";
                        newTd2.style.width = "80px";

                        var s = "<input type='text' id='tUnitPrice" + k + "'  value=" + data.rows[k].UnitPrice + "  oninput='a(this)' style='width:80px;'/><span style='color:red'>*</span><br />";
                        s += "<label id='tUnitPrice" + k + "L' style='color:red'></label>";
                        newTd2.innerHTML = s

                    }

                }
            }
        }
    });
}


//加载更换零件列表
function LoadChangeBakList() {
    var Request = GetRequest();
    var RID = Request["RID"];
    $.ajax({
        url: "ServicingAccessory",
        type: "post",
        data: { RID: RID },
        dataType: "Json",
        success: function (data) {
            if (data.success == "false") {

                return;
            }
            else {

                var strBakName = data.BakName;
                var strBakType = data.BakType;
                var strBakNum = data.BakNum;
                var strBakMeasure = data.BakMeasure;
                var strUnitPrice = data.UnitPrice;

                var Measure = strBakMeasure.split(',');
                var BakName = strBakName.split(',');
                var BakType = strBakType.split(',');
                var BakNum = strBakNum.split(',');
                var UnitPrice = strUnitPrice.split(',');
                var TotalPrice = data.TotalPrice.split(',');
                var Comments = data.Comments.split('@');

                for (var k = 0 ; k < BakName.length; k++) {
                    j++;
                    var testTable = document.getElementById("taskList2");
                    var newTr = testTable.insertRow();
                    newTr.id = "row" + j;

                    //部件名称 
                    var newTd1 = newTr.insertCell();
                    newTd1.className = "textright";
                    newTd1.style.width = "200px";
                    newTd1.innerHTML = BakName[k];

                    //规格型号
                    var newTd2 = newTr.insertCell();
                    newTd2.className = "textright";
                    newTd2.style.width = "80px";
                    newTd2.innerHTML = BakType[k];

                    //数量
                    var newTd3 = newTr.insertCell();
                    newTd3.className = "textright";
                    newTd3.style.width = "80px";
                    newTd3.innerHTML = "<lable id='Num" + k + "' >" + BakNum[k] + "</lable>";

                    //单价
                    var newTd4 = newTr.insertCell();
                    newTd4.className = "textright";
                    newTd4.style.width = "80px";

                    var d = "<input type='text' id='UnitPrice" + k + "' value='" + UnitPrice[k] + "' oninput='a2(this)' style='width:80px;'/><span style='color:red'>*</span><br />";
                    d += "<label id='UnitPrice" + k + "L' style='color:red'></label>";
                    newTd4.innerHTML = d;
                    //价格
                    var newTd5 = newTr.insertCell();
                    newTd5.className = "textright";
                    newTd5.style.width = "80px";
                    var s = "<input type='text' id='TotalPrice" + k + "' value='" + TotalPrice[k] + "' style='width:80px;'/>";
                    newTd5.innerHTML = s;

                    //备注
                    var newTd6 = newTr.insertCell();
                    newTd6.className = "textright";
                    newTd6.style.width = "80px";

                    newTd6.innerHTML = "<input type='text'  value='" + Comments[k] + "' style='width:80px;' />";

                    //单位
                    var newTd7 = newTr.insertCell();
                    newTd7.className = "textright";
                    newTd7.style.display = "none";
                    newTd7.innerHTML = "<input  value='" + Measure[k] + "' type='text'/>";
                }
            }
        }
    });
}
function a(t) {
    if ($(t).val() == "") {
        $("#" + t.id + "L").html("不能为空");
        b = false;
    } else {
        $("#" + t.id + "L").html("");
        b = true;
    }
}

function a2(t) {
    if ($(t).val() == "") {
        $("#" + t.id + "L").html("不能为空");
        var n = t.id.substring(t.id.length - 1, t.id.length);

        $("#TotalPrice" + n).val("");
        b = false;
    } else {

        $("#" + t.id + "L").html("");
        var n = t.id.substring(t.id.length - 1, t.id.length);
        var v = parseInt(t.value) * parseInt($("#Num" + n).text())
        $("#TotalPrice" + n).val(v);
        b = true;
    }
}