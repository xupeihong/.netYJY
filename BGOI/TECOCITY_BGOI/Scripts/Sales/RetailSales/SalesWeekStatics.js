var curPage = 1;
var OnePageCount = 30;
var PID;
var RelenvceID;
var Type = "";
var ProID;
var Pname;
var start;
var end;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#pageContent").width() - 30);

    $("#StaTab").height($("#pageContent").height() - 50);
    //var str1 = "<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:5px;line-height:25px;\">" +
    //   "<tr  class=\"left\"><td style=\"width:10%\">序号</td><td style=\"width:19%\">销售人员</td><td style=\"width:19%\">所属部门</td><td style=\"width:19%\">定价单价（元）</td><td style=\"width:19%\">成交价单价（元）</td><td style=\"width:19%\">成交总价（元）</td></tr></table>";
    //$("#StaTab").html(str1);
    //jq();
    $("#btnPrint").click(function () {
        var StartDate = $("#StartDate").val();
        var EndDate = $("#EndDate").val();
        var SalesMan = $("#SalesMan").val();
        var UnitId = $("#UnitId").val();
        window.showModalDialog("../SalesRetail/PrintWeekStatics?StartDate=" + StartDate + "&EndDate=" + EndDate + "&SalesMan=" + SalesMan + "&UnitId=" + UnitId, window, "dialogWidth:800px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
    });

    $('#CX').click(function () {
        var StartDate = $('#StartDate').val();
        var EndDate = $('#EndDate').val();
        var SalesMan = $('#SalesMan').val();
        var UnitId = $('#UnitId').val();

        $.ajax({
            url: "SearchStatics",
            type: "post",
            data: { StartDate: StartDate, EndDate: EndDate, SalesMan: SalesMan, UnitId: UnitId },
            dataType: "Json",
            success: function (data) {
                if (data.success == "true") {
                    //alert(data.strSb);
                    $("#tbList").append(data.strSb);
                    $("#Sign").html(data.strSign);
                }
                else {
                    return;
                }
            }
        });
    })
})