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
    var str1 = "<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:5px;line-height:25px;\">" +
       "<tr  class=\"left\"><td  rowspan=\"2\" style=\"width:5%\">序号号</td><td  rowspan=\"2\" style=\"width:5%\">姓名</td>" +
       "<td colspan='8'>自XXXX年X月X日起收款情况</td><td rowspan=\"2\">2013年以前合同欠款合计（含2013年合同）</td></tr>";
    str1 += "<tr class=\"staleft\" >" + 
           "<td style=\"width:10%\">07年合同</td>" +
                "<td style=\"width:10%\">08年合同</td>" +
                   "<td style=\"width:10%\">09年合同</td>" +
                      "<td style=\"width:10%\">10年合同</td>" +
                         "<td style=\"width:10%\">11年合同</td>" +
                            "<td style=\"width:10%\">12年合同</td>" +
                               "<td style=\"width:10%\">13年合同</td>" +
                               "<td style=\"width:10%\">小计</td>" +
    "</tr>";

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
    "</tr></table>"
    $("#StaTab").html(str1);
    $("#Sign").html("汇总说明:单位：万元");
    //jq();
    $('#CX').click(function () {
        var Pname = $('#Pname').val();
        var start = $('#StartDate').val();
        var end = $('#EndDate').val();

        $.ajax({
            url: "StatisticsManageTable",
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