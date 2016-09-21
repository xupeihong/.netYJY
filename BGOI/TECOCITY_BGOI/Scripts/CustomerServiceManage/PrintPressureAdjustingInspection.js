
$(document).ready(function () {
    jq();//加载数据
    jq1();
    // 打印 
    $("#btnPrint").click(function () {
        document.getElementById("btnPrint").className = "Noprint";
        $("#ReportContent").attr("style", "width: 100%;margin-top:10px")
        pagesetup("0.3", "0", "0.3", "0.3");
        window.print();
        document.getElementById("btnPrint").className = "btn";
        $("#ReportContent").attr("style", "margin: 0 auto; width: 90%;margin-top:10px")
    });
})

function jq() {
    if (location.search != "") {
        Info = location.search.split('&')[0].split('=')[1];
    }
    $.ajax({
        url: "UpTime",
        type: "Post",
        data: {
            Info: Info
        },
        async: false,
        success: function (data) {
            var json = eval(data.datas);
            for (var i = 0; i < json.length; i++) {
               // $("#OperationTime").val(json[i].OperationTime);
                //  $("#AfternoonTime").val(json[i].AfternoonTime);
                var Uses = json[i].Uses;
                if (Uses == "0") {
                    $(':radio[name=Uses][value=0]').attr('checked', true);
                } else if (Uses == "1") {
                    $(':radio[name=Uses][value=1]').attr('checked', true);
                } else if (Uses == "2") {
                    $(':radio[name=Uses][value=2]').attr('checked', true);
                } else {
                    $(':radio[name=Uses][value=3]').attr('checked', true);
                }


            }
        }
    });
}

function jq1() {
    if (location.search != "") {
        Info = location.search.split('&')[0].split('=')[1];
    }
    $.ajax({
        url: "UpPrintPressureAdjustingInspection",
        type: "Post",
        data: {
            Info: Info
        },
        async: false,
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr id ="DetailInfo' + rowCount + '">';
                    html += '<td>上台</td>';
                    html += '<td  colspan="2">P1= <lable class="labRowNumber' + rowCount + ' " id="P1' + rowCount + '">' + json[i].UsePressureShangP1 + 'MPa</lable> </td>';
                    html += '<td  colspan="2">P2= <lable class="labRowNumber' + rowCount + ' " id="P2' + rowCount + '">' + json[i].UsePressureShangP2 + 'kPa</lable></td>';
                    html += '<td  colspan="2">Pb= <lable class="labRowNumber' + rowCount + ' " id="Pb' + rowCount + '">' + json[i].UsePressureShangPb + 'kPa</lable></td>';
                    html += '<td  colspan="2" rowspan="2">Pf= <lable class="labRowNumber' + rowCount + ' " id="pF' + rowCount + '">' + json[i].UsePressureShangPf + 'kPa</lable></td>';
                    html += '</tr>';
                    $("#DetailInfo").append(html);
                    html = '<tr  id ="DetailInfo' + rowCount + '">';
                    html += '<td>下台</td>';
                    html += '<td  colspan="2">P1= <lable class="labRowNumber' + rowCount + ' " id="P1' + rowCount + '">' + json[i].UsePressureXiaP1 + 'MPa</lable></td>';
                    html += '<td  colspan="2">P2= <lable class="labRowNumber' + rowCount + ' " id="P2' + rowCount + '">' + json[i].UsePressureXiaP2 + 'kPa</lable></td>';
                    html += '<td  colspan="2">Pb= <lable class="labRowNumber' + rowCount + ' " id="Pb' + rowCount + '">' + json[i].UsePressureXiaPb + 'kPa</lable></td>';
                    html += '</tr>';
                    $("#DetailInfo").append(html);
                }
            }
        }
    })
}

