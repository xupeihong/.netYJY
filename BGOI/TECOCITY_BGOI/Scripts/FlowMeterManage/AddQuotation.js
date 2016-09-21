var b = true;
var j = 0;

$(document).ready(function () {
    $("#pageContent").height($(window).height());
    var Request = GetRequest();
    var RID = Request["RID"];
    $("#StrRID").val(RID);
    LoadChangeBakList();
    // 确定
    $("#QD").click(function () {
        GetChangeBakList();
        GetQuotationList();
       
    


        if (b) {
            var a = confirm("确定要建新项目吗")
            if (a == false)
                return;
            else {

                submit();
            }
        } else {
            return
        }

    });



})

function returnConfirm() { return false; }

// 界面提交
function submit() {

   

    var options = {
        url: "InsertQuotation",
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

// 获取报价单
function GetQuotationList() {

    var tab = document.getElementById("taskList");
    var tds = tab.getElementsByTagName("tr");
    var leng = document.getElementById("taskList").rows.length;
    if (leng == 2) {
        $("#BakType").val("");
        $("#BakUnitPrice").val("");
        //$("#BakConcesioPrice").val("");
        return;
    }
    else {


        var BakType;// 类型
        var BakUnitPrice;//单价
        //var BakConcesioPrice;// 优惠价


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
        BakType = BakType.substr(9);

        BakUnitPrice = BakUnitPrice.substr(9);

        //BakConcesioPrice = BakConcesioPrice.substr(9);
        $("#BakType").val(BakType);
        $("#BakUnitPrice").val(BakUnitPrice);
        //$("#BakConcesioPrice").val(BakConcesioPrice);

    }
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
        var td7 = tds[i].getElementsByTagName("td")[6].innerText;

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
                var strMeasure = data.BakMeasure;

                var BakName = strBakName.split(',');
                var BakType = strBakType.split(',');
                var BakNum = strBakNum.split(',');
                var BakMeasure = strMeasure.split(',');
              
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
                    newTd3.innerHTML = "<lable id='Num"+k+"' >"+BakNum[k]+"</lable>";

                    //单价
                    var newTd4 = newTr.insertCell();
                    newTd4.className = "textright";
                    newTd4.style.width = "80px";
                   
                    var d = "<input type='text' id='UnitPrice" + k + "' oninput='a2(this)' style='width:80px;'/><span style='color:red'>*</span><br />";
                    d += "<label id='UnitPrice" + k + "L' style='color:red'></label>";
                    newTd4.innerHTML = d;
                    //价格
                    var newTd5 = newTr.insertCell();
                    newTd5.className = "textright";
                    newTd5.style.width = "80px";
                    var s = "<input type='text' id='TotalPrice" + k + "' style='width:80px;readonly:'readonly''/>";
                    newTd5.innerHTML = s;
                 
                    //备注
                    var newTd6 = newTr.insertCell();
                    newTd6.className = "textright";
                    newTd6.style.width = "80px";
                 
                    newTd6.innerHTML = "<input type='text' style='width:80px;' />";

                    //单位
                    var newTd7 = newTr.insertCell();
                    newTd7.className = "textright";
                    newTd7.style.display = "none";

                    newTd7.innerHTML = BakMeasure[k];
                }
            }
        }
    });
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
        var v = parseFloat(t.value) * parseFloat($("#Num" + n).text())
        $("#TotalPrice" + n).val(v);
        b = true;
    }
}