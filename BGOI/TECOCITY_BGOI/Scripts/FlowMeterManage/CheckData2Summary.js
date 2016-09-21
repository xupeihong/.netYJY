

var j = 0;

$(document).ready(function () {

    $("#pageContent").height($(window).height());
    LoadCheckData2List();
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


function LoadCheckData2List() {
    var Request = GetRequest();
    var strRID = Request["rid"];

    $.ajax({
        url: "LoadCheckData2Summary",
        type: "post",
        data: { RID: strRID },
        dataType: "Json",
        success: function (obj) {

            var data = eval("(" + obj + ")");
            if (data.success == "false") {

                return;

            } else {
                for (var i = 0; i < data.rows.length; i++) {

                    for (var k = 0; k < 2; k++) {

                        if (k == 0) {
                            var testTable = document.getElementById("list");
                            j++;
                            var newTr = testTable.insertRow();
                            newTr.id = "row" + j;

                           
                                var newTd1 = newTr.insertCell();
                                newTd1.setAttribute("rowspan", 2);
                                newTd1.style.width = "200px";
                                newTd1.innerHTML = data.rows[i].M;
                          

                            var newTd2 = newTr.insertCell();
                            newTd2.setAttribute("rowspan", 2);
                            newTd2.style.width = "80px";
                            newTd2.innerHTML = data.rows[i].CertifID;


                            var newTd3 = newTr.insertCell();
                            newTd3.setAttribute("rowspan", 2);
                            newTd3.style.width = "80px";
                            newTd3.innerHTML = data.rows[i].Remark;

                            var newTd4 = newTr.insertCell();

                            newTd4.style.width = "80px";


                            newTd4.innerHTML = "温度（" + data.rows[i].ATemperature + "） ℃<br/>压力（" + data.rows[i].APressure + "）kpa";

                            var newTd5 = newTr.insertCell();



                            newTd5.innerHTML = data.rows[i].A1path;


                            var newTd6 = newTr.insertCell();

                            newTd6.style.width = "80px";

                            newTd6.innerHTML = data.rows[i].A2path;

                            var newTd7 = newTr.insertCell();

                            newTd7.style.width = "80px";
                            newTd7.innerHTML = data.rows[i].A3path;


                            var newTd8 = newTr.insertCell();

                            newTd8.style.width = "80px";
                            newTd8.innerHTML = data.rows[i].A4path;



                            var newTd9 = newTr.insertCell();

                            newTd9.style.width = "80px";

                            newTd9.innerHTML = data.rows[i].A5path;

                            var newTd10 = newTr.insertCell();

                            newTd10.style.width = "80px";

                            newTd10.innerHTML = data.rows[i].A6path;



                            var newTd11 = newTr.insertCell();

                            newTd11.style.width = "80px";

                            newTd11.innerHTML = data.rows[i].AAverage;

                            var newTd12 = newTr.insertCell();

                            newTd12.style.width = "80px";

                            newTd12.innerHTML = data.rows[i].AAheory;

                            var newTd13 = newTr.insertCell();

                            newTd13.style.width = "80px";

                            newTd13.innerHTML = "误差<br/>error%";

                            var newTd14 = newTr.insertCell();

                            newTd14.style.width = "80px";

                            newTd14.innerHTML = "";


                            var newTd15 = newTr.insertCell();

                            newTd15.style.width = "80px";

                            newTd15.innerHTML = "";


                            var newTd16 = newTr.insertCell();

                            newTd16.style.width = "80px";

                            newTd16.innerHTML = "";


                            var newTd17 = newTr.insertCell();

                            newTd17.style.width = "80px";

                            newTd17.innerHTML = "";


                            var newTd18 = newTr.insertCell();

                            newTd18.style.width = "80px";

                            newTd18.innerHTML = "";


                            var newTd19 = newTr.insertCell();

                            newTd19.style.width = "80px";

                            newTd19.innerHTML = "";


                            var newTd20 = newTr.insertCell();

                            newTd20.style.width = "80px";

                            newTd20.innerHTML = "";


                            var newTd21 = newTr.insertCell();

                            newTd21.style.width = "80px";

                            newTd21.innerHTML = "";
                        } else {

                            var testTable = document.getElementById("list");
                            j++;
                            var newTr = testTable.insertRow();
                            newTr.id = "row" + j;


                            //var newTd1 = newTr.insertCell();
                            //newTd1.setAttribute("rowspan", 2);
                            //newTd1.style.width = "200px";
                            //newTd1.innerHTML = data.rows[i].MeterID;


                            //var newTd2 = newTr.insertCell();

                            //newTd2.style.width = "80px";
                            //newTd2.innerHTML = data.rows[i].CertifID;


                            //var newTd3 = newTr.insertCell();

                            //newTd3.style.width = "80px";
                            //newTd3.innerHTML = data.rows[i].Remark;

                            var newTd4 = newTr.insertCell();

                            newTd4.style.width = "80px";


                            newTd4.innerHTML = "温度（" + data.rows[i].BTemperature + "） ℃<br/>压力（" + data.rows[i].BPressure + "）kpa";

                            var newTd5 = newTr.insertCell();



                            newTd5.innerHTML = data.rows[i].B1path;


                            var newTd6 = newTr.insertCell();

                            newTd6.style.width = "80px";

                            newTd6.innerHTML = data.rows[i].B2path;

                            var newTd7 = newTr.insertCell();

                            newTd7.style.width = "80px";
                            newTd7.innerHTML = data.rows[i].B3path;


                            var newTd8 = newTr.insertCell();

                            newTd8.style.width = "80px";
                            newTd8.innerHTML = data.rows[i].B4path;



                            var newTd9 = newTr.insertCell();

                            newTd9.style.width = "80px";

                            newTd9.innerHTML = data.rows[i].B5path;

                            var newTd10 = newTr.insertCell();

                            newTd10.style.width = "80px";

                            newTd10.innerHTML = data.rows[i].B6path;



                            var newTd11 = newTr.insertCell();

                            newTd11.style.width = "80px";

                            newTd11.innerHTML = data.rows[i].BAverage;

                            var newTd12 = newTr.insertCell();

                            newTd12.style.width = "80px";

                            newTd12.innerHTML = data.rows[i].BAheory;

                            var newTd13 = newTr.insertCell();

                            newTd13.style.width = "80px";

                            newTd13.innerHTML = "误差<br/>error%";

                            var newTd14 = newTr.insertCell();

                            newTd14.style.width = "80px";

                            newTd14.innerHTML = "";


                            var newTd15 = newTr.insertCell();

                            newTd15.style.width = "80px";

                            newTd15.innerHTML = "";


                            var newTd16 = newTr.insertCell();

                            newTd16.style.width = "80px";

                            newTd16.innerHTML = "";


                            var newTd17 = newTr.insertCell();

                            newTd17.style.width = "80px";

                            newTd17.innerHTML = "";


                            var newTd18 = newTr.insertCell();

                            newTd18.style.width = "80px";

                            newTd18.innerHTML = "";


                            var newTd19 = newTr.insertCell();

                            newTd19.style.width = "80px";

                            newTd19.innerHTML = "";


                            var newTd20 = newTr.insertCell();

                            newTd20.style.width = "80px";

                            newTd20.innerHTML = "";


                            var newTd21 = newTr.insertCell();

                            newTd21.style.width = "80px";

                            newTd21.innerHTML = "";
                        }
                      
                    }
                    

                }
            }
        }

    });
}