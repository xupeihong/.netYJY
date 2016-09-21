var PID;
var rowCount;
var newRowID;

$(document).ready(function () {
    tick();
    jq();
    $("#ScrapTime").val("");
    $("#btnSave").click(function () {
        var BXKID = $("#BXKID").val();
        var BXKIDold = $("#Hidden1").val();
        var ContractID = $("#ContractID").val();
        var OrderContent = $("#OrderContent").val();
        var SpecsModels = $("#SpecsModels").val();
        var PID = $("#PID").val();
        var BuyDate = $("#BuyDate").val();
        var BXDate = $("#BXDate").val();
        var EndUnit = $("#EndUnit").val();
        var Contact = $("#Contact").val();
        var Tel = $("#Tel").val();
        var Remark = $("#Remark").val();
        var CreateUser = $("#CreateUser").val();
        var CreateTime = $("#localtime").text();
        var BXKNum = $("#BXKNum").val();
        //if (BXKID == "" || BXKID == null) {
        //    alert("保修卡编号不能为空");
        //    return;
        //}
        $.ajax({
            url: "UPdateWarrantyCard",
            type: "Post",
            data: {
                BXKID: BXKID, ContractID: ContractID, OrderContent: OrderContent, SpecsModels: SpecsModels, PID: PID, BuyDate: BuyDate, BXKIDold: BXKIDold,
                BXDate: BXDate, EndUnit: EndUnit, Contact: Contact, Tel: Tel, Remark: Remark, CreateUser: CreateUser, CreateTime: CreateTime, BXKNum: BXKNum
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
                    // $("#SpecsModels").val(json[i].Spec);
                    $("#SpecsModels option:contains('" + json[i].Spec + "')").prop('selected', true);
                    $("#PID").val(json[i].PID);
                }
            }
        }
    })
}
function jq() {
    if (location.search != "") {
        BXKID = location.search.split('&')[0].split('=')[1];
    }
    $.ajax({
        url: "UpWarrantyCardList",
        type: "Post",
        data: {
            BXKID: BXKID
        },
        async: false,
        success: function (data) {
            if (data.success == true) {
                var json = eval(data.datas);
                if (json.length > 0) {
                    for (var i = 0; i < json.length; i++) {
                        //$("#BXKNum").val(json[i].BXKNum);
                        //$("#ContractID").val(json[i].ContractID);
                        //$("#OrderContent").val(json[i].OrderContent);
                        //$("#SpecsModels").val(json[i].SpecsModels);

                        $("#OrderContent").val(json[i].OrderContent);
                        $("#SpecsModels").val(json[i].SpecsModels);

                        $("#PID").val(json[i].PID);

                        $("#UserAdd").val(json[i].UserAdd);

                        $("#BuyDate").val(json[i].BuyDate);
                        $("#BXDate").val(json[i].BXDate);
                        $("#EndUnit").val(json[i].EndUnit);
                        $("#Contact").val(json[i].Contact);
                        $("#Tel").val(json[i].Tel);
                        $("#Remark").val(json[i].Remark);
                        $("#Hidden1").val(json[i].BXKID);
                    }
                }
            }
            else {
                alert(data.Msg);
            }
        }
    });
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




