var PID;
var rowCount;
var newRowID;
$(document).ready(function () {
    tick();
    $("#ScrapTime").val("");
    $("#btnSave").click(function () {
        var TYID = $("#TYID").val();
        var UserName = $("#UserName").val();
        var UserAdd = $("#UserAdd").val();
        var Users = $("#Users").val();
        var Tel = $("#Tel").val();
        var KeyStorageUnitJia = $("#KeyStorageUnitJia").val();
        var KeyStorageUnitYi = $("#KeyStorageUnitYi").val();
        var OperationTime = $("#OperationTime").val();
        var AfternoonTime = "";// $("#AfternoonTime").val();

        //var Uses = $("#Uses").val();
        //var Boiler = $("#Boiler").val();
        //var KungFu = $("#KungFu").val();
        //var Civil = $("#Civil").val();
        var Uses = $("input[name='Uses']:checked").val();


        var CreateUser = $("#CreateUser").val();
        var CreateTime = $("#localtime").text();
       
      
       

        var UserSignature = $("#UserSignature").val();
        var UsePressureShang = $("#UsePressureShang").val();
        var UsePressureXia = $("#UsePressureXia").val();
        var InspectionPersonnel = $("#InspectionPersonnel").val();
        var Remarks = $("#Remarks").val();
        var type = 1;//添加


       


        var UsePressureShangP1 = "";
        var UsePressureShangP2 = "";
        var UsePressureShangPb = "";
        var UsePressureShangPf = "";
        var UsePressureXiaP1 = "";
        var UsePressureXiaP2 = "";
        var UsePressureXiaPb = "";

        var tbody = document.getElementById("DetailInfo");
       // var len=tbody.rows.length+1;
        for (var i = 0; i < tbody.rows.length ; i++) {
            var usePressureShangP1 = document.getElementById("p1" + i).value;
            var usePressureShangP2 = document.getElementById("p2" + i).value;
            var usePressureShangPb = document.getElementById("pb" + i).value;
            var usePressureShangPf = document.getElementById("pf" + i).value;
            var usePressureXiaP1 = document.getElementById("xp1" + i).value;
            var usePressureXiaP2 = document.getElementById("xp2" + i).value;
            var usePressureXiaPb = document.getElementById("xpb" + i).value;
            UsePressureShangP1 += usePressureShangP1;
            UsePressureShangP2 += usePressureShangP2;
            UsePressureShangPb += usePressureShangPb;
            UsePressureShangPf += usePressureShangPf;
            UsePressureXiaP1 += usePressureXiaP1;
            UsePressureXiaP2 += usePressureXiaP2;
            UsePressureXiaPb += usePressureXiaPb;

            if (i < tbody.rows.length - 2) {
                UsePressureShangP1 += ",";
                UsePressureShangP2 += ",";
                UsePressureShangPb += ",";
                UsePressureShangPf += ",";
                UsePressureXiaP1 += ",";
                UsePressureXiaP2 += ",";
                UsePressureXiaPb += ",";
                i++;
            }
            else {
                UsePressureShangP1 += "";
                UsePressureShangP2 += "";
                UsePressureShangPb += "";
                UsePressureShangPf += "";
                UsePressureXiaP1 += "";
                UsePressureXiaP2 += "";
                UsePressureXiaPb += "";
                i++;
            }
        }

        $.ajax({
            url: "SavePressureAdjustingInspection",
            type: "Post",
            data: {
                TYID: TYID, UserName: UserName, UserAdd: UserAdd, Users: Users, Tel: Tel, KeyStorageUnitJia: KeyStorageUnitJia,
                KeyStorageUnitYi: KeyStorageUnitYi, OperationTime: OperationTime, Uses: Uses, CreateUser: CreateUser, CreateTime: CreateTime,
                UserSignature: UserSignature, UsePressureShang: UsePressureShang,AfternoonTime:AfternoonTime,
                UsePressureXia: UsePressureXia, InspectionPersonnel: InspectionPersonnel, Remarks: Remarks, type: type,
                UsePressureShangP1: UsePressureShangP1, UsePressureShangP2: UsePressureShangP2, UsePressureShangPb: UsePressureShangPb,
                UsePressureShangPf: UsePressureShangPf, UsePressureXiaP1: UsePressureXiaP1, UsePressureXiaP2: UsePressureXiaP2,
                UsePressureXiaPb: UsePressureXiaPb
            },
            async: false,
            success: function (data) {
                if (data.success == true) {
                    alert("保存成功");
                    setTimeout('parent.ClosePop()', 10);
                    window.parent.frames["iframeRight"].reload();
                }
                else {
                    alert(data.Msg);
                }
            }
        });
    });
    $("#Cancel").click(function () {
        window.parent.ClosePop();
    })
});
function DelRow() { //删除货品信息行
    var tbodyID = "DetailInfo";
    var rowIndex = -1;
    var typeNames = ["RowNumber", "PID", "ProName", "Spec", "Units", "Amount", "UnitPrice", "Total", "Manufacturer", "Remark"];

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
                $("#RowNumber" + i).html(parseInt(i) + 1);
            }
        }
        if (document.getElementById(tbodyID).rows.length > 0) {
            if (rowIndex == document.getElementById(tbodyID).rows.length)
                selRow(document.getElementById(tbodyID + (rowIndex - 1)), '');
            else
                selRow(document.getElementById(tbodyID + rowIndex), '');;
        }
    }
    GetAmount();
}
function showLocale(objD) {
    var str, colorhead, colorfoot;
    var yy = objD.getYear();
    if (yy < 1900) yy = yy + 1900;
    var MM = objD.getMonth() + 1;
    if (MM < 10) MM = '0' + MM;
    var dd = objD.getDate();
    if (dd < 10) dd = '0' + dd;
    var hh = objD.getHours();
    if (hh < 10) hh = '0' + hh;
    var mm = objD.getMinutes();
    if (mm < 10) mm = '0' + mm;
    var ss = objD.getSeconds();
    if (ss < 10) ss = '0' + ss;
    var ww = objD.getDay();
    if (ww == 0) colorhead = "<font color=\"#333333\">";
    if (ww > 0 && ww < 6) colorhead = "<font color=\"#333333\">";
    if (ww == 6) colorhead = "<font color=\"#333333\">";
    if (ww == 0) ww = "星期日";
    if (ww == 1) ww = "星期一";
    if (ww == 2) ww = "星期二";
    if (ww == 3) ww = "星期三";
    if (ww == 4) ww = "星期四";
    if (ww == 5) ww = "星期五";
    if (ww == 6) ww = "星期六";
    colorfoot = "</font>"
    str = colorhead + yy + "年" + MM + "月" + dd + "日" + " " + hh + ":" + mm + ":" + ss + " " + ww + colorfoot;
    return (str);
}
function tick() {
    var today;
    today = new Date();
    document.getElementById("localtime").innerHTML = showLocale(today);
    window.setTimeout("tick()", 1000);
}
function GaiBian() {
    var DDL = $("#OrderContent").val();
    $.ajax({
        url: "GetProSpec",
        type: "post",
        data: { DDL: DDL },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    $("#SpecsModels").val(json[i].Spec);
                    $("#PID").val(json[i].PID);
                }
            }
        }
    })
}

function TianJia() {
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    var html = "";
    html = '<tr  id ="DetailInfo' + rowCount + '">';
    html += '<td  colspan="2">上台</td>';
    html += '<td  colspan="2">P1= <input type="text" id="p1' + rowCount + '" style="width:30px;"/>MPa</td>';
    html += '<td  colspan="2">P2= <input type="text" id="p2' + rowCount + '" style="width:30px;"/>kPa</td>';
    html += '<td  colspan="2">Pb= <input type="text" id="pb' + rowCount + '" style="width:30px;"/>kPa</td>';
    html += '<td  colspan="2" rowspan="2">Pf= <input type="text" id="pf' + rowCount + '" style="width:30px;"/>kPa</td>';
    html += '</tr>';
    $("#DetailInfo").append(html);
    html = '<tr  id ="DetailInfo' + rowCount + '">';
    html += '<td  colspan="2">下台</td>';
    html += '<td  colspan="2">P1= <input type="text" id="xp1' + rowCount + '" style="width:30px;"/>MPa </td>';
    html += '<td  colspan="2">P2= <input type="text" id="xp2' + rowCount + '" style="width:30px;"/>kPa </td>';
    html += '<td  colspan="2">Pb= <input type="text" id="xpb' + rowCount + '" style="width:30px;"/>kPa </td>';
    //html += '<td  colspan="2" rowspan="2">Pf= <input type="text" id="pf' + rowCount + '" style="width:30px;" readonly = "true" />kPa</td>';
    html += '</tr>';
    $("#DetailInfo").append(html);
}




