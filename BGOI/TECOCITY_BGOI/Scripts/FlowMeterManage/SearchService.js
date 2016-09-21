
var curPage = 1;
var OnePageCount = 15;
var oldSelID = 0;
var j = 0;
var p = 0
$(document).ready(function () {

    var Request = GetRequest();
    var strRID = Request["RID"];
    $("#pageContent").height($(window).height());
    LoadQuotationList();


});



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



function LoadQuotationList() {

    //var Request = GetRequest();
    //var strRID = Request["RID"];
    $.ajax({
        url: "LoadQuotationList2",
        type: "post",
        data: {
            curpage: curPage, rownum: OnePageCount, RID: "",
            RepairID: $("#RepairID").val(), CustomerName: $("#CustomerName").val(), CustomerAddr: $("#CustomerAddr").val(),
            MeterID: $("#MeterID").val(), MeterName: $("#MeterName").val(), Model: $("#Model").val(),
            SS_Date: $("#SS_Date").val(), ES_Date: $("#ES_Date").val(), State: $("#State").val(),
            OrderDate: $("#Order").val(), CardType: $("#CardType").val(), SubUnit: $("#SubUnit").val()
        },
        dataType: "Json",
        success: function (obj) {

            var data = eval("(" + obj + ")");
            if (data.success == "false") {

                return;

            } else {
                for (var i = 0; i < data.rows.length; i++) {


                    var testTable = document.getElementById("list");
                    j++;
                    var newTr = testTable.insertRow();
                    newTr.id = "row" + j;


                    var newTd1 = newTr.insertCell();

                    newTd1.style.width = "200px";
                    newTd1.innerHTML = "<input type='checkbox' id='c" + data.rows[i].RID + "' name='RIDCheck' value='" + data.rows[i].RID + "' />" + data.rows[i].RID


                    var newTd2 = newTr.insertCell();

                    newTd2.style.width = "80px";
                    newTd2.innerHTML = data.rows[i].Model;


                    var newTd3 = newTr.insertCell();

                    newTd3.style.width = "80px";
                    newTd3.innerHTML = data.rows[i].Caliber;

                    var newTd4 = newTr.insertCell();

                    newTd4.style.width = "80px";

                    if (data.rows[i].PreUnit == null) {
                        newTd4.innerHTML = "无";
                    } else {
                        newTd4.innerHTML = data.rows[i].PreUnit;
                    }
                    var newTd5 = newTr.insertCell();
                    newTd5.style.width = "80px";


                    newTd5.innerHTML = data.rows[i].DeviceName;

                    var newTd6 = newTr.insertCell();
                    newTd6.style.width = "80px";


                    newTd6.innerHTML = data.rows[i].UnitPrice;
                    var newTd7 = newTr.insertCell();
                    newTd7.style.width = "80px";


                    newTd7.innerHTML = data.rows[i].Num;

                    var newTd8 = newTr.insertCell();
                    newTd8.style.width = "80px";


                    newTd8.innerHTML = data.rows[i].Measure;

                    var newTd9 = newTr.insertCell();
                    newTd9.style.width = "80px";


                    newTd9.innerHTML = data.rows[i].TotalPrice2;












                    var newTd10 = newTr.insertCell();

                    newTd10.style.width = "80px";

                    newTd10.innerHTML = data.rows[i].qx;

                    var newTd11 = newTr.insertCell();

                    newTd11.style.width = "80px";
                    newTd11.innerHTML = data.rows[i].ccbd;


                    var newTd12 = newTr.insertCell();

                    newTd12.style.width = "80px";
                    newTd12.innerHTML = data.rows[i].jc;



                    var newTd13 = newTr.insertCell();

                    newTd13.style.width = "80px";

                    newTd13.innerHTML = data.rows[i].TotalPrice;

                }
            }
        }

    });
}
function reload() {

    j = 0;
    var s = document.getElementById("list").rows.length;
    for (var i = 1; i < s; i++) {
        $("tr[id=row" + i + "]").remove();
    }
    LoadQuotationList();
}


function change() {
    $("#ModelType").val($("#CardType").val());

}
function returnConfirm() {

    $("#ModelType").val($("#CardType").val());
    var str = "";
    var r = document.getElementsByName("RIDCheck");
    for (var i = 0; i < r.length; i++) {
        if (r[i].checked) {
            str += r[i].value + ",";
        }
    }
    $("#RID").val(str);
    if (str == "") {

        return false;
    } else {
        return true;
    }
}