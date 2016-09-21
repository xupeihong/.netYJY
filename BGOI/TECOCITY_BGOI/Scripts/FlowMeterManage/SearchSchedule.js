var curPage = 1;
var OnePageCount = 15;
var oldSelID = 0;
var j = 0;
var p = 0

$(document).ready(function () {

    $("#pageContent").height($(window).height());
    LoadList();

})


function CheckDate(id) {
    var type = $("#CardType").val();
    if (type == "CardType2") {
        $.ajax({
            url: "LoadCheckData2Summary",
            type: "post",
            data: { RID: id },
            dataType: "Json",
            success: function (data) {

                
                if (data.success == "false") {
                    alert("无结果");
                    return;
                } else {
                    window.parent.OpenDialog("检测信息", "../FlowMeterManage/CheckData2Summary?rid=" + id, 1000, 500, '');
                }
            }
        });
    } else {
        $.ajax({
            url: "LoadCheckDataSummary",
            type: "post",
            data: { RID: id },
            dataType: "Json",
            success: function (data) {

                
                if (data.success == "false") {
                    alert("无结果");
                    return;
                } else {
                    window.parent.OpenDialog("检测信息", "../FlowMeterManage/CheckDataSummary?rid=" + id, 1000, 500, '');
                }
            }
        });
    }


}

function RepairInfo(id) {
    var CardType = $("#CardType").val();
    $.ajax({
        url: "LoadRepairInfoSummary",
        type: "post",
        data: { RID: id, type: CardType },
        dataType: "Json",
        success: function (data) {

            
            if (data.success == "false") {
                alert("无记录");
                return;
            } else {
                window.parent.OpenDialog("维修记录", "../FlowMeterManage/RepairInfoSummary?rid=" + id + "&type=" + CardType, 1000, 500, '');
            }
        }
    });

}


function Accessory(id) {
    $.ajax({
        url: "LoadFileList",
        type: "post",
        data: { RID: id },
        dataType: "Json",
        success: function (obj) {
            var data = eval("(" + obj + ")");

            if (data.rows.length == 0) {
                alert("无记录");
                return;
            } else {
                window.parent.OpenDialog("附件", "../FlowMeterManage/Accessory?rid=" + id, 1000, 300, '');
            }
        }
    });
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

function LoadList() {


    $.ajax({
        url: "LoadScheduleSummary",
        type: "post",
        data: {
            RepairID: $("#RepairID").val(), CustomerName: $("#CustomerName").val(), CustomerAddr: $("#CustomerAddr").val(),
            MeterID: $("#MeterID").val(), MeterName: $("#MeterName").val(), Model: $("#Model").val(),
            SS_Date: $("#SS_Date").val(), ES_Date: $("#ES_Date").val(), State: $("#State").val(),
            OrderDate: $("#Order").val(), CardType: $("#CardType").val()
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
                    newTd1.style.textAlign = "left";
                    newTd1.innerHTML = "<input type='checkbox' id='c" + data.rows[i].RID + "' name='RIDCheck' value='" + data.rows[i].RID + "' />"+data.rows[i].RepairID;


                    var newTd2 = newTr.insertCell();

                    newTd2.style.width = "80px";
                    newTd2.innerHTML = data.rows[i].S_Date;


                    var newTd3 = newTr.insertCell();

                    newTd3.style.width = "80px";
                    newTd3.innerHTML = data.rows[i].ModelType;

                    var newTd4 = newTr.insertCell();

                    newTd4.style.width = "80px";


                    newTd4.innerHTML = data.rows[i].Manufacturer;

                    var newTd5 = newTr.insertCell();
                    // newTd5.setAttribute("colspan", 5);


                    newTd5.innerHTML = data.rows[i].Model;


                    var newTd6 = newTr.insertCell();

                    newTd6.style.width = "80px";

                    newTd6.innerHTML = data.rows[i].CertifID;

                    var newTd7 = newTr.insertCell();

                    newTd7.style.width = "80px";
                    newTd7.innerHTML = data.rows[i].FactoryDate;


                    var newTd8 = newTr.insertCell();

                    newTd8.style.width = "80px";
                    newTd8.innerHTML = data.rows[i].Caliber;



                    var newTd9 = newTr.insertCell();

                    newTd9.style.width = "80px";

                    newTd9.innerHTML = data.rows[i].Pressure;

                    var newTd10 = newTr.insertCell();

                    newTd10.style.width = "80px";


                    newTd10.innerHTML = data.rows[i].FlowRange;


                    var newTd11 = newTr.insertCell();

                    newTd11.style.width = "80px";
                    if (data.rows[i].ModelType == "超声波")
                        newTd11.innerHTML = "无";
                    else
                        newTd11.innerHTML = data.rows[i].Precision;


                    var newTd12 = newTr.insertCell();

                    newTd12.style.width = "80px";
                    if (data.rows[i].ModelType == "超声波")
                        newTd12.innerHTML = "无";
                    else
                        newTd12.innerHTML = data.rows[i].PreUnit;


                    var newTd13 = newTr.insertCell();

                    newTd13.style.width = "80px";
                    if (data.rows[i].ModelType == "超声波")
                        newTd13.innerHTML = "无";
                    else
                        newTd13.innerHTML = data.rows[i].NewUnit;


                    var newTd14 = newTr.insertCell();//归属

                    newTd14.style.width = "80px";

                    newTd14.innerHTML = "";

                    var newTd15 = newTr.insertCell();

                    newTd15.style.width = "80px";

                    newTd15.innerHTML = "<a onclick='Accessory(\"" + data.rows[i].RID + "\")'>附件</a>";

                    var newTd16 = newTr.insertCell();

                    newTd16.style.width = "80px";
                    if (data.rows[i].ModelType == "超声波")
                        newTd16.innerHTML = "无";
                    else
                        newTd16.innerHTML = data.rows[i].X_Manufacturer;
                    var newTd17 = newTr.insertCell();

                    newTd17.style.width = "80px";
                    if (data.rows[i].ModelType == "超声波")
                        newTd17.innerHTML = "无";
                    else
                        newTd17.innerHTML = data.rows[i].X_Model;


                    var newTd18 = newTr.insertCell();

                    newTd18.style.width = "80px";
                    if (data.rows[i].ModelType == "超声波")
                        newTd18.innerHTML = "无";
                    else
                        newTd18.innerHTML = data.rows[i].X_ID;


                    var newTd19 = newTr.insertCell();

                    newTd19.style.width = "80px";
                    if (data.rows[i].ModelType == "超声波")
                        newTd19.innerHTML = "无";
                    else
                        newTd19.innerHTML = data.rows[i].X_FactoryDate;


                    var newTd20 = newTr.insertCell();

                    newTd20.style.width = "80px";
                    if (data.rows[i].ModelType == "超声波")
                        newTd20.innerHTML = "无";
                    else
                        newTd20.innerHTML = data.rows[i].X_PreUnit;


                    var newTd21 = newTr.insertCell();

                    newTd21.style.width = "80px";
                    if (data.rows[i].ModelType == "超声波")
                        newTd21.innerHTML = "无";
                    else
                        newTd21.innerHTML = data.rows[i].X_NewUnit;


                    var newTd22 = newTr.insertCell();

                    newTd22.style.width = "80px";


                    if (data.rows[i].ModelType == "超声波")
                        newTd22.innerHTML = "无";
                    else
                        newTd22.innerHTML = data.rows[i].RecordNum;

                    var newTd23 = newTr.insertCell();

                    newTd23.style.width = "80px";


                    if (data.rows[i].ModelType == "超声波")
                        newTd23.innerHTML = "无";
                    else
                        newTd23.innerHTML = data.rows[i].X_Standard;

                    var newTd24 = newTr.insertCell();

                    newTd24.style.width = "80px";



                    if (data.rows[i].ModelType == "超声波")
                        newTd24.innerHTML = "无";
                    else
                        newTd24.innerHTML = data.rows[i].X_Pressure;
                    var newTd25 = newTr.insertCell();

                    newTd25.style.width = "80px";


                    if (data.rows[i].ModelType == "超声波")
                        newTd25.innerHTML = "无";
                    else
                        newTd25.innerHTML = data.rows[i].X_Temperature;

                    var newTd26 = newTr.insertCell();

                    newTd26.style.width = "80px";

                    newTd26.innerHTML = "<a onclick='CheckDate(\"" + data.rows[i].RID + "\")'>检测结果</a>";

                    var newTd27 = newTr.insertCell();

                    newTd27.style.width = "80px";

                    newTd27.innerHTML = data.rows[i]["初测状态"];


                    var newTd28 = newTr.insertCell();

                    newTd28.style.width = "80px";

                    newTd28.innerHTML = data.rows[i]["次数"];




                    var newTd29 = newTr.insertCell();

                    newTd29.style.width = "80px";

                    newTd29.innerHTML = "<a onclick='RepairInfo(\"" + data.rows[i].RID + "\")'>维修记录</a>";

                    var newTd30 = newTr.insertCell();

                    newTd30.style.width = "80px";

                    newTd30.innerHTML = data.rows[i]["清洗维修状态"];
                    var newTd31 = newTr.insertCell();

                    newTd31.style.width = "80px";

                    newTd31.innerHTML = data.rows[i]["出厂前检测状态"];

                    var newTd32 = newTr.insertCell();

                    newTd32.style.width = "80px";

                    newTd32.innerHTML = data.rows[i]["打压状态"];


                    var newTd33 = newTr.insertCell();

                    newTd33.style.width = "80px";

                    newTd33.innerHTML = "铅封号";

                    var newTd34 = newTr.insertCell();

                    newTd34.style.width = "80px";

                    newTd34.innerHTML = data.rows[i]["完成情况"];


                    var newTd35 = newTr.insertCell();

                    newTd35.style.width = "80px";

                    newTd35.innerHTML = data.rows[i]["零件"];


                    var newTd36 = newTr.insertCell();

                    newTd36.style.width = "80px";

                    newTd36.innerHTML = "说明 ";





                    var newTd37 = newTr.insertCell();

                    newTd37.style.width = "80px";

                    newTd37.innerHTML = data.rows[i].CustomerName;


                    var newTd38 = newTr.insertCell();

                    newTd38.style.width = "80px";

                    newTd38.innerHTML = data.rows[i].S_Name;

                    var newTd39 = newTr.insertCell();

                    newTd39.style.width = "80px";

                    newTd39.innerHTML = data.rows[i].S_Tel;

                    var newTd40 = newTr.insertCell();

                    newTd40.style.width = "80px";

                    newTd40.innerHTML = data.rows[i].ReceiveUser;

                    var newTd41 = newTr.insertCell();

                    newTd41.style.width = "80px";

                    newTd41.innerHTML = data.rows[i].G_Name;

                    var newTd42 = newTr.insertCell();

                    newTd42.style.width = "80px";

                    newTd42.innerHTML = data.rows[i].G_Tel;

                    var newTd43 = newTr.insertCell();

                    newTd43.style.width = "80px";

                    newTd43.innerHTML = data.rows[i].G_Date;

                    var newTd44 = newTr.insertCell();

                    newTd44.style.width = "80px";

                    newTd44.innerHTML = data.rows[i].Text;

                    var newTd45 = newTr.insertCell();

                    newTd45.style.width = "80px";

                    newTd45.innerHTML = "地点";


                    var newTd46 = newTr.insertCell();

                    newTd46.style.width = "80px";

                    newTd46.innerHTML = "情况";
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
    LoadList();
}