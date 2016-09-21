

var j = 0;

$(document).ready(function () {

    $("#pageContent").height($(window).height());
  
    var Request = GetRequest();
    var strRID = Request["rid"];
    $("#strRID").val(strRID);

    
    var type = Request["type"];
    $("#strModelType").val(type);

    LoadRepairInfoSummary();
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


function LoadRepairInfoSummary() {
    var Request = GetRequest();
    var strRID = Request["rid"];
    var strtype = Request["type"];
    $.ajax({
        url: "LoadRepairInfoSummary",
        type: "post",
        data: { RID: strRID, type: strtype },
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
                            newTd1.innerHTML = data.rows[i].RepairSDate;


                            var newTd2 = newTr.insertCell();
                           
                            newTd2.style.width = "80px";
                            newTd2.innerHTML = data.rows[i].RepairEDate;


                            var newTd3 = newTr.insertCell();
                           
                            newTd3.style.width = "80px";
                            newTd3.innerHTML = data.rows[i].M;

                            var newTd4 = newTr.insertCell();

                            newTd4.style.width = "80px";


                            newTd4.innerHTML = data.rows[i].ModelType;

                            var newTd5 = newTr.insertCell();

                            newTd5.style.width = "80px";

                            newTd5.innerHTML = data.rows[i].Manufacturer;


                            var newTd6 = newTr.insertCell();

                            newTd6.style.width = "80px";

                            newTd6.innerHTML = data.rows[i].Model;

                            var newTd7 = newTr.insertCell();

                            newTd7.style.width = "80px";
                            newTd7.innerHTML = data.rows[i].Breakdown;


                            var newTd8 = newTr.insertCell();

                            newTd8.style.width = "80px";
                            newTd8.innerHTML = data.rows[i].RepairContent;


                            
                            var newTd9 = newTr.insertCell();

                            newTd9.style.width = "80px";

                            newTd9.innerHTML = data.rows[i].DeviceName;

                            var newTd10 = newTr.insertCell();

                            newTd10.style.width = "80px";

                            newTd10.innerHTML = data.rows[i].DeviceType;



                            var newTd11 = newTr.insertCell();

                            newTd11.style.width = "80px";

                            newTd11.innerHTML = data.rows[i].Measure;

                            var newTd12 = newTr.insertCell();

                            newTd12.style.width = "80px";

                            newTd12.innerHTML = data.rows[i].Num;

                            var newTd13 = newTr.insertCell();

                            newTd13.style.width = "80px";

                            newTd13.innerHTML = data.rows[i].RepairNum;

                            var newTd14 = newTr.insertCell();

                            newTd14.style.width = "80px";

                            newTd14.innerHTML = data.rows[i].AdjustPre;


                            var newTd15 = newTr.insertCell();

                            newTd15.style.width = "80px";

                            newTd15.innerHTML = data.rows[i].AdjustNow;


                            var newTd16 = newTr.insertCell();

                            newTd16.style.width = "80px";

                            newTd16.innerHTML = data.rows[i].RepairUser;


                            var newTd17 = newTr.insertCell();

                            newTd17.style.width = "80px";

                            newTd17.innerHTML = data.rows[i].RepairResult;


                            var newTd18 = newTr.insertCell();

                            newTd18.style.width = "80px";

                            newTd18.innerHTML = data.rows[i].Remark;

                }
            }
        }

    });
}