
var j = 0;
$(document).ready(function () {


    $("#pageContent").height($(window).height());
    $("#ModelType").val("")
    LoadQuotationList();
});

function change() {
    $("#ModelType").val($("#Model").val());
    
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



function LoadQuotationList() {
    
    var t = $("#Model").val();
  
    var Request = GetRequest();
    var strRID = Request["RID"];
    $.ajax({
        url: "LoadScheduleDetail",
        type: "post",
        data: {type:t},
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
                    newTd1.innerHTML = data.rows[i]["口径"];


                    var newTd2 = newTr.insertCell();

                    newTd2.style.width = "80px";
                    newTd2.innerHTML = data.rows[i]["清洗完成"];


                    var newTd3 = newTr.insertCell();

                    newTd3.style.width = "80px";
                    newTd3.innerHTML = data.rows[i]["维修完成"];

                    var newTd4 = newTr.insertCell();

                    newTd4.style.width = "80px";

                    var num = parseInt(data.rows[i]["清洗完成"]) + parseInt(data.rows[i]["维修完成"])
                    newTd4.innerHTML = num;

                    var newTd5 = newTr.insertCell();

                    newTd5.style.width = "80px";

                    newTd5.innerHTML = data.rows[i]["待初测"];


                    var newTd6 = newTr.insertCell();

                    newTd6.style.width = "80px";

                    newTd6.innerHTML = data.rows[i]["待清洗"];

                    var newTd7 = newTr.insertCell();

                    newTd7.style.width = "80px";
                    newTd7.innerHTML = data.rows[i]["清洗中"];


                    var newTd8 = newTr.insertCell();

                    newTd8.style.width = "80px";
                    newTd8.innerHTML = data.rows[i]["待维修"];


                    var newTd9 = newTr.insertCell();

                    newTd9.style.width = "80px";
                    newTd9.innerHTML = data.rows[i]["维修中"];


                    var newTd10 = newTr.insertCell();

                    newTd10.style.width = "80px";


                    newTd10.innerHTML = data.rows[i]["待检测"];

                    var newTd11 = newTr.insertCell();

                    newTd11.style.width = "80px";

                    newTd11.innerHTML = data.rows[i]["待打压"];

                    var num2 = parseInt(data.rows[i]["待初测"]) + parseInt(data.rows[i]["待清洗"]) + parseInt(data.rows[i]["清洗中"])
                        + parseInt(data.rows[i]["待维修"]) + parseInt(data.rows[i]["维修中"]) + parseInt(data.rows[i]["待检测"]) + parseInt(data.rows[i]["待打压"]);
                    var newTd12 = newTr.insertCell();

                    newTd12.style.width = "80px";

                    newTd12.innerHTML = num2;



                    var newTd13 = newTr.insertCell();

                    newTd13.style.width = "80px";

                    newTd13.innerHTML = "0";


                    var newTd14 = newTr.insertCell();

                    newTd14.style.width = "80px";

                    newTd14.innerHTML = "0";

                    var newTd15 = newTr.insertCell();

                    newTd15.style.width = "80px";
                    
                    newTd15.innerHTML = num + num2;



                    var newTd16 = newTr.insertCell();

                    newTd16.style.width = "80px";

                    newTd16.innerHTML = data.rows[i]["待出厂"];

                    var newTd17 = newTr.insertCell();

                    newTd17.style.width = "80px";

                    newTd17.innerHTML = data.rows[i]["已出厂"];;

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