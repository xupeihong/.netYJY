var curPage = 1;
var OnePageCount = 15;
var oldSelID = 0;

$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#pageContent").width() - 20);
    $("#StaTab").height($("#pageContent").height() - 100);

    var str1 = "<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:5px;line-height:25px;\">";
    str1 += " <tr class=\"left\" style=\"height:25px\"><td style=\"width:8%\" rowspan=\"2\">规格</td><td style=\"width:8%\" rowspan=\"2\">表号</td><td style=\"width:21%\" colspan=\"3\">初检</td>";
    str1 += " <td style=\"width:21%\" colspan=\"3\">清洗后</td><td style=\"width:7%\">重复性</td><td style=\"width:14%\" colspan=\"2\">清洗前后误差降低</td>";
    str1 += " <td style=\"width:14%\" colspan=\"2\">清洗后偏正/偏负</td></tr>";
    str1 += " <tr class=\"left\" style=\"height:25px\"><td style=\"width:7%\">重复性</td><td style=\"width:14%\" colspan=\"2\">示值</td>";
    str1 += " <td style=\"width:7%\">重复性</td><td style=\"width:14%\" colspan=\"2\">示值</td><td></td>";
    str1 += " <td style=\"width:7%\">大段</td><td style=\"width:7%\">小段</td>";
    str1 += " <td style=\"width:7%\">大段</td><td style=\"width:7%\">小段</td></tr>";
    str1 += " </table>";

    $("#StaTab").html(str1);
    $("#Sign").html("");

    // 查询
    $('#CX').click(function () {
        var MeterID = $('#strMeterID').val();
        var Model = $('#Model').val();

        $.ajax({
            url: "GetRepeatValue",
            type: "post",
            data: { MeterID: MeterID, Model: Model },
            dataType: "Json",
            success: function (data) {
                if (data.success == "true") {
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


