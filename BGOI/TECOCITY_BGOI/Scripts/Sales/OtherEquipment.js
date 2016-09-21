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
    //序号	常规/技改	规格型号	单位
    //单位成本	累计直接成本	毛利润	直接成本占销售价比例	已发货数量	待发货数量	生产中数量


    $("#StaTab").height($("#pageContent").height() - 50);
    var str1 = "<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:5px;line-height:25px;\">" +
       "<tr  class=\"left\"><td rowspan=\"2\" style=\"width:10%\">序号号</td><td rowspan=\"2\" style=\"width:20%\">产品名称</td>" +
       "<td rowspan=\"2\" style=\"width:10%\">规格型号</td><td rowspan=\"2\" style=\"width:10%\">单位</td>" +
       "<td colspan=\"2\" style=\"width:20%\">X月</td>" +
       "<td colspan=\"7\" style=\"width:20%\">年度累计</td>" +
       "<td colspan=\"3\" style=\"width:20%\">产品状态</td>" +
      " </tr>";
    str1 += "<tr><td style=\"width:10%\">销售数量</td><td style=\"width:10%\">合同额</td><td style=\"width:10%\">数量</td><td style=\"width:10%\">销售均价</td><td style=\"width:10%\">合同额</td><td style=\"width:10%\">单位成本</td><td style=\"width:10%\">累计成本</td><td style=\"width:10%\">毛利润</td><td style=\"width:20%\">直接成本占销售价比例</td><td style=\"width:20%\">已发货数量</td><td style=\"width:20%\">待发货数量</td><td style=\"width:20%\">生产中数量</td></tr>"
    str1 += "<tr class=\"staleft\"  style=\"color:red\">" + "<td style=\"width:4%\">合计</td>" +
        "<td style=\"width:10%\"></td>" +
         "<td style=\"width:10%\"></td>" +
          "<td style=\"width:10%\"></td>" +
           "<td style=\"width:10%\"></td>" +
                "<td style=\"width:10%\"></td>" +
                  "<td style=\"width:10%\"></td>" +
           "<td style=\"width:10%\"></td>" +
                "<td style=\"width:10%\"></td>" +
                 "<td style=\"width:10%\"></td>" +
         "<td style=\"width:10%\"></td>" +
          "<td style=\"width:10%\"></td>" +
           "<td style=\"width:10%\"></td>" +
                "<td style=\"width:10%\"></td>" +
                  "<td style=\"width:10%\"></td>" +
           "<td style=\"width:10%\"></td>" +
               
    "</tr></table>"
    $("#StaTab").html(str1);
    $("#Sign").html("汇总说明:");
    //jq();
    $('#CX').click(function () {
        var Pname = $('#Pname').val();
        var start = $('#StartDate').val();
        var end = $('#EndDate').val();

        $.ajax({
            url: "OtherEquipmentTable",
            type: "post",
            data: { data1: start, data2: end, data3: Pname },
            dataType: "Json",
            success: function (data) {
                if (data.success == "true") {
                    //alert(data.strSb);
                    $("#StaTab").html(data.strSb);
                    $("#Sign").html(data.strSign);
                }
                else {
                    return;
                }
            }
        });
    })
})