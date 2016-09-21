

var j = 0;

$(document).ready(function () {

    $("#pageContent").height($(window).height());
    LoadCheckDataList();
    var Request = GetRequest();
    var strRID = Request["rid"];
    $("#StrRID").val(strRID);
})







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


function LoadCheckDataList() {
    var Request = GetRequest();
    var strRID = Request["rid"];

    $.ajax({
        url: "LoadCheckDataSummary",
        type: "post",
        data: { RID: strRID },
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
                    newTd1.innerHTML = data.rows[i].M;


                    var newTd2 = newTr.insertCell();

                    newTd2.style.width = "80px";
                    newTd2.innerHTML = data.rows[i].CertifID;


                    var newTd3 = newTr.insertCell();

                    newTd3.style.width = "80px";
                    newTd3.innerHTML = data.rows[i].Remark;

                    var newTd4 = newTr.insertCell();

                    newTd4.style.width = "80px";


                    newTd4.innerHTML = data.rows[i].Qmin;

                    var newTd5 = newTr.insertCell();
                   


                    newTd5.innerHTML = data.rows[i]["0.1Qmax"];


                    var newTd6 = newTr.insertCell();

                    newTd6.style.width = "80px";

                    newTd6.innerHTML = data.rows[i]["0.2Qmax"];

                    var newTd7 = newTr.insertCell();

                    newTd7.style.width = "80px";
                    newTd7.innerHTML = data.rows[i]["0.25Qmax"];


                    var newTd8 = newTr.insertCell();

                    newTd8.style.width = "80px";
                    newTd8.innerHTML = data.rows[i]["0.4Qmax"];



                    var newTd9 = newTr.insertCell();

                    newTd9.style.width = "80px";

                    newTd9.innerHTML = data.rows[i]["0.7Qmax"];

                    var newTd10 = newTr.insertCell();

                    newTd10.style.width = "80px";

                    newTd10.innerHTML = data.rows[i]["Qmax"];



                    var newTd11 = newTr.insertCell();

                    newTd11.style.width = "80px";

                    newTd11.innerHTML = data.rows[i]["Avg_Qmin"];

                    var newTd12 = newTr.insertCell();

                    newTd12.style.width = "80px";

                    newTd12.innerHTML = data.rows[i]["Avg_0.1Qmax"];

                    var newTd13 = newTr.insertCell();

                    newTd13.style.width = "80px";

                    newTd13.innerHTML = data.rows[i]["Avg_0.2Qmax"];

                    var newTd14 = newTr.insertCell();

                    newTd14.style.width = "80px";

                    newTd14.innerHTML = data.rows[i]["Avg_0.25Qmax"];

                    var newTd15 = newTr.insertCell();

                    newTd15.style.width = "80px";

                    newTd15.innerHTML = data.rows[i]["Avg_0.4Qmax"];

                    var newTd16 = newTr.insertCell();

                    newTd16.style.width = "80px";

                    newTd16.innerHTML = data.rows[i]["Avg_0.7Qmax"];

                    var newTd17 = newTr.insertCell();

                    newTd17.style.width = "80px";

                    newTd17.innerHTML = data.rows[i]["Avg_Qmax"];

                    var newTd18 = newTr.insertCell();

                    newTd18.style.width = "80px";

                    newTd18.innerHTML = data.rows[i]["Repeat_Qmin"];

                    var newTd19 = newTr.insertCell();

                    newTd19.style.width = "80px";

                    newTd19.innerHTML = data.rows[i]["Repeat_0.1Qmax"];


                    var newTd20 = newTr.insertCell();

                    newTd20.style.width = "80px";

                    newTd20.innerHTML = data.rows[i]["Repeat_0.2Qmax"];


                    var newTd21 = newTr.insertCell();

                    newTd21.style.width = "80px";

                    newTd21.innerHTML = data.rows[i]["Repeat_0.25Qmax"];

                    var newTd22 = newTr.insertCell();

                    newTd22.style.width = "80px";

                    newTd22.innerHTML = data.rows[i]["Repeat_0.4Qmax"];


                    var newTd23 = newTr.insertCell();

                    newTd23.style.width = "80px";

                    newTd23.innerHTML = data.rows[i]["Repeat_0.7Qmax"];

                    var newTd24 = newTr.insertCell();

                    newTd24.style.width = "80px";

                    newTd24.innerHTML = data.rows[i]["Repeat_Qmax"];


                    var newTd25 = newTr.insertCell();

                    newTd25.style.width = "80px";

                    newTd25.innerHTML = data.rows[i].Ratio;


                    var newTd26 = newTr.insertCell();

                    newTd26.style.width = "80px";

                    newTd26.innerHTML = data.rows[i].q1;

                    var newTd27 = newTr.insertCell();

                    newTd27.style.width = "80px";

                    newTd27.innerHTML = data.rows[i].q2;


                    var newTd28 = newTr.insertCell();

                    newTd28.style.width = "80px";
                    
                    newTd28.innerHTML = data.rows[i].PDeviation;




                    var newTd29 = newTr.insertCell();

                    newTd29.style.width = "80px";

                    newTd29.innerHTML = data.rows[i].Oratio;

                }
            }
        }

    });
}


