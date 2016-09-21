var j = 0;
var b = true;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    var Request = GetRequest();
    var rid = Request["RID"];
      LoadChangeBakList();

    $("#StrRID").val(rid);

    $("#addc").click(function () {
        j++;
        var testTable = document.getElementById("taskList");
        var newTr = testTable.insertRow();
        newTr.id = "row" + j;

        //部件名称
        var newTd1 = newTr.insertCell();
        newTd1.style.width = "150px";
        newTd1.innerHTML = "<input  id='ProName" + j + "' onclick='SearchProduct(\"" + j + "\")' type='text' style='width:150px;' readonly='readonly'/>";
        //规格型号 
        var newTd2 = newTr.insertCell();
        newTd2.style.width = "150px";
        newTd2.innerHTML = "<input id='Spec" + j + "' type='text' style='width:150px;' readonly='readonly' />";
        //数量
        var newTd3 = newTr.insertCell();
        newTd3.style.width = "80px";
        newTd3.innerHTML = "<input id='Num" + j + "' style='width:80px;' >";
        //单价
        var newTd4 = newTr.insertCell();
        newTd4.style.width = "80px";
        var d = "<input type='text' id='UnitPrice" + j + "' oninput='a2(this)' style='width:80px;'/>";
        newTd4.innerHTML = d;
        //价格
        var newTd5 = newTr.insertCell();
        newTd5.style.width = "80px";
        newTd5.innerHTML = "<input style='width:80px' />";
        //备注
        var newTd6 = newTr.insertCell();
        newTd6.innerHTML = "<input style='width:80px' />";

        var newTd7 = newTr.insertCell();
        newTd7.style.width = "80px";
        newTd7.innerHTML = "<a onclick='deleteTr(" + newTr.id + ")' style='color:blue;cursor:hand; width:80px;'> 删 除 </a>";

        //单位
        var newTd8 = newTr.insertCell();
        newTd8.className = "textright";
        newTd8.style.display ="none" ;
        newTd8.innerHTML = "<input id='Units" + j + "' type='text'/>";
    });

    $("#QD").click(function () {
        GetChangeBakList();
        GetQuotationList();
       
        if (b) {
            var a = confirm("确定要建新项目吗")
            if (a == false)
                return;
            else {

                submitInfo();
            }
        }
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
        data: { name: $("#keyname").val() },
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
function deleteTr(id) {
    var strid = id.id;
    $("tr[id=" + strid + "]").remove();
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

// 界面提交
function submitInfo() {
  
    $.ajax({
        url: "InsertQuotation",
        type: "Post",

        dataType: "Json",
        data: {
            rid: $("#StrRID").val(), Type: $("#BakType").val(), p: $("#BakUnitPrice").val(),
            strDeviceName: $("#DeviceName").val(), strDeviceType: $("#DeviceType").val(), strNum: $("#Num").val(),
            strUnitPrice: $("#UnitPrice").val(), strTotalPrice: $("#TotalPrice").val(), strComments: $("#Comments").val(),
            strMeasure: $("#Measure").val()
        },

        async: false,
        success: function (data) {
            if (data.success == "true") {
                alert(data.Msg);

                setTimeout('parent.ClosePop()', 100);
            }
            else {
                //alert(data.Msg);
            }
        }
    });

    return false;
}

//获取报价单列表
function GetQuotationList() {
    
    var tab = document.getElementById("taskList");
    var tds = tab.getElementsByTagName("tr");
    var leng = document.getElementById("taskList").rows.length;


    var BakType = "";// 类型
    var BakUnitPrice = "";//单价


    var qx = 0;
    if ($("#qx").val() != "") {
        qx = $("#qx").val()
    } else {
        
        var id = document.getElementById("qx");
        a(id);

    }
    var ccbd = 0;
    if ($("#ccbd").val() != "") {
        ccbd = $("#ccbd").val()
    }
    else {
        var id = document.getElementById("ccbd");
        a(id);

    }
    var wx = 0;
    var r = $("#TotalPrice").val().split(',');
    for (var i = 0; i < r.length-1; i++) {
        wx += parseFloat(r[i]);
    }
   
    //if ($("#wx").val() != "") {
    //    wx = $("#wx").val()
    //}
    //else {
    //    var id = document.getElementById("wx");
    //    a(id);

    //}
    var dlbd = 0;
    if ($("#dlbd").val() != "") {
        dlbd = $("#dlbd").val()
    } else {
        var id = document.getElementById("dlbd");
        a(id);

    }
    var jc = 0;
    if ($("#jc").val() != "") {
        jc = $("#jc").val()
    } else {
        var id = document.getElementById("jc");
        a(id);

    }
    var slbd = 0;
    if ($("#slbd").val() != "") {
        slbd = $("#slbd").val()
    } else {
        var id = document.getElementById("slbd");
        a(id);

    }
    var qt = 0;
    if ($("#qt").val() != "") {
        qt = $("#qt").val()
    } else {
        var id = document.getElementById("qt");
        a(id);

    }

    BakType += "清洗,出厂标定,维修,代理标定,检测,实流标定,其他,";

    // 单价

    BakUnitPrice += qx + "," + ccbd + "," + wx + "," + dlbd + "," + jc + "," + slbd + "," + qt + ",";




    $("#BakType").val(BakType);
    $("#BakUnitPrice").val(BakUnitPrice);



}

// 获取更换部件列表 
function GetChangeBakList() {

    var tab = document.getElementById("taskList");
    var tds = tab.getElementsByTagName("tr");
    var leng = document.getElementById("taskList").rows.length;



    var DeviceName = "";
    var DeviceType = "";
    var Measure = "";
    var Num = "";
    var UnitPrice = "";
    var TotalPrice = "";
    var Comments = "";
    for (var i = 2 ; i < tds.length; i++) {


        // 名称
        var td1 = tds[i].getElementsByTagName("td")[0].getElementsByTagName("INPUT")[0].value;
        DeviceName += td1 + ",";

        // 型号
        var td2 = tds[i].getElementsByTagName("td")[1].getElementsByTagName("INPUT")[0].value;
        DeviceType += td2 + ",";

        // 数量
        var td3 = tds[i].getElementsByTagName("td")[2].getElementsByTagName("INPUT")[0].value;
        Num += td3 + ",";


        // 单价
        var td4 = tds[i].getElementsByTagName("td")[3].getElementsByTagName("INPUT")[0].value;
        //if (td4 == "") {
        //    var id = tds[i].getElementsByTagName("td")[3].getElementsByTagName("INPUT")[0];
        //    a(id);
        //}
        UnitPrice += td4 + ",";

        // 总价
        var td5 = tds[i].getElementsByTagName("td")[4].getElementsByTagName("INPUT")[0].value;

        TotalPrice += td5 + ",";

        // 备注
        var td6 = tds[i].getElementsByTagName("td")[5].getElementsByTagName("INPUT")[0].value;

        Comments += td6 + "@";
        
        // 单位
        var td7 = tds[i].getElementsByTagName("td")[7].getElementsByTagName("INPUT")[0].value;

        Measure += td7 + ",";

    }

    $("#DeviceName").val(DeviceName);
    $("#DeviceType").val(DeviceType);
    $("#Measure").val(Measure);
    $("#Num").val(Num);
    $("#UnitPrice").val(UnitPrice);
    $("#TotalPrice").val(TotalPrice);
    $("#Comments").val(Comments);
}

// 加载部件更换记录
function LoadChangeBakList() {
    var Request = GetRequest();
    var rid = Request["RID"];
    $.ajax({
        url: "ServicingAccessory",
        type: "post",
        data: { RID: rid },
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
                    var testTable = document.getElementById("taskList");
                    var newTr = testTable.insertRow();
                    newTr.id = "row" + j;

                    //部件名称 
                    var newTd1 = newTr.insertCell();
                    newTd1.className = "textright";
                    newTd1.style.width = "200px";
                    newTd1.innerHTML = "<input  id='ProName" + j + "' value='" + BakName[k] + "' onclick='SearchProduct(\"" + j + "\")' type='text' style='width:150px;' readonly='readonly'/>";

                    //规格型号
                    var newTd2 = newTr.insertCell();
                    newTd2.className = "textright";
                    newTd2.style.width = "80px";
                    newTd2.innerHTML = "<input id='Spec" + j + "' type='text' value='" + BakType[k] + "' style='width:150px;' readonly='readonly' />";

                    //数量
                    var newTd3 = newTr.insertCell();
                    newTd3.className = "textright";
                    newTd3.style.width = "80px";
                    newTd3.innerHTML = "<input id='Num" + j + "'value='" + BakNum[k] + "'  style='width:80px;' >"; 
                    
                    //单价
                    var newTd4 = newTr.insertCell();
                    newTd4.className = "textright";
                    newTd4.style.width = "80px";


                    var d = "<input type='text' id='UnitPrice" +j + "'  value='" + UnitPrice[k] + "' oninput='a2(this)' style='width:80px;'/>";
                    newTd4.innerHTML = d;
                    //价格
                    var newTd5 = newTr.insertCell();
                    newTd5.className = "textright";
                    newTd5.style.width = "80px";
                    var s = "<input type='text' id='TotalPrice" + j + "' value='" + TotalPrice[k] + "' style='width:80px;'/>";
                    newTd5.innerHTML = s;

                    //备注
                    var newTd6 = newTr.insertCell();
                    newTd6.className = "textright";
                    newTd6.style.width = "80px";                   
                    newTd6.innerHTML = "<input type='text'  value='" + Comments[k] + "' style='width:80px;' />";
                    var newTd7 = newTr.insertCell();

                    newTd7.style.width = "80px";
                    newTd7.innerHTML = "<a onclick='deleteTr(" + newTr.id + ")' style='color:blue;cursor:hand; width:80px;'> 删 除 </a>";

                    //单位
                    var newTd8 = newTr.insertCell();
                    newTd8.className = "textright";
                    newTd8.style.display = "none";                 
                    newTd8.innerHTML = "<input  id='Units" + j + "' value='" + Measure[k] + "' type='text'/>";
                }
                var r = j + 2;
                $("#firsttd").attr("Rowspan", r);
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
        var v = parseFloat(t.value) * parseFloat($("#Num" + n).val())
        $("#TotalPrice" + n).val(v);
        b = true;
    }
}