var curPage = 1;
var OnePageCount = 15;
var isConfirm = false;

var ID = 0;
var RowId = 0;
var SID = "";
var RID = "";
var MeterID = "";
var Accuracy = "";
var Mcount = "";
var OutUnit = "";

$(document).ready(function () {
    $("#pageContent").height($(window).height());

    LoadInspecDetail();// 加载送检单详细信息 
    
    // 添加仪表 完成
    $("#btnAdd").click(function () {
        ShowIframe1("选择仪表信息", "../FlowMeterManage/SelectMeter", 500, 400);
    });

    // 删除仪表 完成
    $("#btnDel").click(function () {
       var isConfirm1 = confirm("确定要撤销该条送检信息吗")
       if (isConfirm1 == false) {
           return false;
       }
       else {
           var tbodyID = "DetailInfo";
           var rowIndex = -1;
           var typeNames = ["RowNumber", "RID", "MeterID", "Accuracy", "Mcount", "OutUnit"];

           if (newRowID != "")
               rowIndex = newRowID.replace(tbodyID, '');
           if (rowIndex != -1) {
               document.getElementById(tbodyID).deleteRow(rowIndex);

               if (rowIndex < $("#" + tbodyID + " tr").length) {
                   for (var i = rowIndex; i < $("#" + tbodyID + " tr").length; i++) {
                       var tr = document.getElementById(tbodyID + (parseInt(i) + 1));
                       var tr1 = $("#" + tbodyID + (parseInt(i) + 1));
                       tr.id = tbodyID + i;
                       for (var j = 0; j < tr.childNodes.length; j++) {
                           var html = tr1.html();
                           for (var k = 0; k < typeNames.length; k++) {

                               var olPID = typeNames[k] + (parseInt(i) + 1);
                               var newid = typeNames[k] + i;

                               var reg = new RegExp(olPID, "g");

                               html = html.replace(reg, newid);
                           }
                           tr1.html(html);
                       }
                       document.getElementById("DetailInfo").getElementsByTagName("tr")[i].cells[0].innerText = parseInt(i) + 1;
                   }
               }
               if (document.getElementById(tbodyID).rows.length > 0) {
                   if (rowIndex == document.getElementById(tbodyID).rows.length)
                       selRow(document.getElementById(tbodyID + (rowIndex - 1)), '');
                   else
                       selRow(document.getElementById(tbodyID + rowIndex), '');;
               }
           }
       }
    });

    // 确定修改 完成
    $("#QD").click(function () {
        isConfirm = confirm("确定要提交修改信息吗")
        if (isConfirm == false) {
            return false;
        }
        else {
            //送检表详细表
            var tbody = document.getElementById("DetailInfo");
            for (var i = 0; i < tbody.rows.length; i++) {
                var sid = $("#strSID").val();
                var rId = document.getElementById("DetailInfo").getElementsByTagName("tr")[i].cells[1].innerText;
                var meterId = document.getElementById("DetailInfo").getElementsByTagName("tr")[i].cells[2].innerText;
                var accuracy = document.getElementById("Accuracy" + i).value;
                var mcount = document.getElementById("Mcount" + i).value;
                var unit = document.getElementById("OutUnit" + i).value;

                ID += parseInt(i + 1);
                SID += sid;
                RID += rId;
                MeterID += meterId;
                Accuracy += accuracy;
                Mcount += mcount;
                OutUnit += unit;

                if (i < tbody.rows.length - 1) {
                    ID += ",";
                    SID += ",";
                    RID += ",";
                    MeterID += ",";
                    Accuracy += ",";
                    Mcount += ",";
                    OutUnit += ",";
                }
                else {
                    ID += "";
                    SID += "";
                    RID += "";
                    MeterID += "";
                    Accuracy += "";
                    Mcount += "";
                    OutUnit += "";
                }
            }
            submitInfo();
        }
    });

});

//
function LoadInspecDetail() {
    var rids = "";
    var SID = $("#strSID").val();
    var rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "GetInspecInfo",
        type: "post",
        data: { SID: SID },
        dataType: "json",
        loadonce: false,
        success: function (data) {
            if (data.success == "true") {
                var json = eval(data.datas);
                if (json.length > 0) {
                    for (var i = 0; i < json.length; i++) {
                        rowCount = i;
                        var html = "";
                        html = '<tr id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                        html += '<td style="width:10px;" align="center"><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + (i + 1) + '</lable></td>';
                        html += '<td style="width:100px;" align="center"><lable class="labRID' + rowCount + ' " id="RID' + rowCount + '">' + json[i].RID + '</lable></td>';
                        html += '<td style="width:100px;" align="center"><lable class="labMeterID' + rowCount + ' " id="MeterID' + rowCount + '">' + json[i].MeterID + '</lable></td>';
                        html += '<td style="width:120px;" align="center"><input type="text" id="Accuracy' + rowCount + '" value="' + json[i].Accuracy + '" style="width:120px;"/></td>';
                        html += '<td style="width:120px;" align="center"><input type="text"  id="Mcount' + rowCount + '" value="' + json[i].Mcount + '" style="width:120px;"/></td>';
                        html += '<td style="width:120px;" align="center"><input type="text"  id="OutUnit' + rowCount + '" value="' + json[i].OutUnit + '" style="width:120px;"/></td>';
                        html += '</tr>'
                        $("#DetailInfo").append(html);

                        rids += "'" + json[i].RID + "',";
                    }
                    rids = rids.substring(0, rids.length - 1);
                    $("#Hidden").val(rids);
                }
            }
        }
    })
}

function selRow(curRow) {
    newRowID = curRow.id;
    $("#DetailInfo tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}

function returnConfirm() {
    return false;
}

//
function submitInfo() {
    var options = {
        url: "UpdateInspec",
        type: "Post",
        data: {
            SID: SID, RID: RID, MeterID: MeterID, Accuracy: Accuracy, Mcount: Mcount, OutUnit: OutUnit
        },
        async: false,
        success: function (data) {
            if (data.success == "true") {
                window.parent.frames["iframeRight"].reload();
                alert(data.Msg);
                setTimeout('parent.ClosePop()', 10);
            }
            else {
                //alert(data.Msg);
            }
        }
    }
    $("#ProjectformInfo").ajaxSubmit(options);
    return false;
}

// 
function addMeterDetail(RID) { //增加仪表信息行
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "GetMeterDetail",
        type: "post",
        data: { RID: RID },
        dataType: "json",
        loadonce: false,
        success: function (data) {
            if (data.success == "true") {
                var json = eval(data.datas);
                if (json.length > 0) {
                    for (var i = 0; i < json.length; i++) {
                        var html = "";
                        html = '<tr id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                        html += '<td style="width:10px; text-align:center;"><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable></td>';
                        html += '<td style="width:100px;" align="center"><lable class="labRID' + rowCount + ' " id="RID' + rowCount + '">' + json[i].RID + '</lable></td>';
                        html += '<td style="width:100px;" align="center"><lable class="labMeterID' + rowCount + ' " id="MeterID' + rowCount + '">' + json[i].MeterID + '</lable></td>';
                        html += '<td style="width:120px;" align="center"><input type="text" id="Accuracy' + rowCount + '"style="width:120px;"></td>';
                        html += '<td style="width:120px;" align="center"><input type="text" id="Mcount' + rowCount + '"style="width:120px;"></td>';
                        html += '<td style="width:120px;" align="center"><input type="text" id="OutUnit' + rowCount + '"style="width:120px;" value="' + json[i].OutUnit + '"/></td>';
                        html += '</tr>'
                        $("#DetailInfo").append(html);
                    }
                }
            }
        }
    })
}