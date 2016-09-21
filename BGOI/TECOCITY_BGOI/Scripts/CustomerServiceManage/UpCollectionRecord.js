var PID;
var rowCount;
var newRowID;
$(document).ready(function () {
    tick();
    jq();
    $("#ScrapTime").val("");
    $("#btnSave").click(function () {
        var CRID = $("#CRID").val();
        var PaymentUnit = $("#PaymentUnit").val();
        var CollectionAmount = $("#CollectionAmount").val();
        var PaymentContent = $("#PaymentContent").val();
        var PaymentMethod = $("input[name='PaymentMethod']:checked").val();
        var PaymentPeo = $("#PaymentPeo").val();
        var CRTime = $("#CRTime").val();
        var CorporateFinance = $("#CorporateFinance").val();
        var Remark = $("#Remark").val();
        var CreateUser = $("#CreateUser").val();
        var CreateTime = $("#localtime").text();
        var type = "2";
        var BRDID = $("#BRDID").val();

        //if (BXKID == "" || BXKID == null) {
        //    alert("保修卡编号不能为空");
        //    return;
        //}
        $.ajax({
            url: "SaveCollectionRecord",
            type: "Post",
            data: {
                CRID: CRID, PaymentUnit: PaymentUnit, CollectionAmount: CollectionAmount, PaymentContent: PaymentContent,
                PaymentMethod: PaymentMethod, PaymentPeo: PaymentPeo, CRTime: CRTime, type: type,BRDID:BRDID,
                CorporateFinance: CorporateFinance, Remark: Remark, CreateUser: CreateUser, CreateTime: CreateTime
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
function jq() {
    if (location.search != "") {
        CRID = location.search.split('&')[0].split('=')[1];
    }
    $.ajax({
        url: "UpUpCollectionRecordList",
        type: "Post",
        data: {
            CRID: CRID
        },
        async: false,
        success: function (data) {
            if (data.success == true) {
                var json = eval(data.datas);
                if (json.length > 0) {
                    for (var i = 0; i < json.length; i++) {
                        $("#CRTime").val(json[i].CRTime);
                        var PaymentMethod = json[i].PaymentMethod;
                        if (PaymentMethod == "0") {
                            $(':radio[name=PaymentMethod][value=0]').attr('checked', true);
                        } else if (PaymentMethod == "1") {
                            $(':radio[name=PaymentMethod][value=1]').attr('checked', true);
                        } else  {
                            $(':radio[name=PaymentMethod][value=2]').attr('checked', true);
                        }

                    }
                }
            }
            else {
                alert(data.Msg);
            }
        }
    });
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
                    // $("#SpecsModels").val(json[i].Spec);
                    $("#SpecsModels option:contains('" + json[i].Spec + "')").prop('selected', true);
                    $("#PID").val(json[i].PID);
                }
            }
        }
    })
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




