
$(document).ready(function () {
    //jq();//加载数据
    for (var i = 0; i < 10; i++) {
        var b = $("#b").val();
        $("#PID" + i).attr("value", b);
    }

    //jq2();
    // 打印 
    $("#btnPrint").click(function () {
        for (var i = 0; i < 10; i++) {
            var a = $("#PID" + i).val();
            //$("#d" + i).attr("value", a);
            document.getElementById("d" + i).innerHTML = a;

        }
        //window.parent.frames["iframeRight"].DY();
        //alert(1);
        document.getElementById("btnPrint").className = "Noprint";
        // $("#ReportContent").attr("style", "width: 100%;margin-top:10px")
        pagesetup("0.3", "0", "0.3", "0.3");
        window.print();
        document.getElementById("btnPrint").className = "btn";
        //$("#ReportContent").attr("style", "margin: 0 auto; width: 90%;margin-top:10px")
    });
})



function jq1() {
    if (location.search != "") {
        Info = location.search.split('&')[0].split('=')[1];
    }
    $.ajax({
        url: "PrintSGss",
        type: "Post",
        data: {
            Info: Info
        },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    // alert(json[i].PID);
                    rowCount = document.getElementById("DetailInfos").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr id ="DetailInfos' + rowCount + '">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].Process + '</lable> </td>';
                    html += '<td  ><lable class="labOrderContent' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].team + '</lable> </td>';
                    html += '<td  ><lable class="labSpecsModels' + rowCount + '  id="SpecsModels' + rowCount + '">' + json[i].Estimatetime + '</lable></td>';
                    html += '<td ><lable class="labOrderNum' + rowCount + '  id="OrderNum' + rowCount + '">' + json[i].person + '</lable></td>';
                    html += '<td><lable class="labOrderUnit' + rowCount + '  id="OrderUnit' + rowCount + '">' + json[i].plannumber + '</lable></td>';
                    html += '<td  ><lable class="labTechnology' + rowCount + '  id="Technology' + rowCount + '">' + json[i].Qualified + '</lable></td>';
                    html += '<td ><lable class="labDeliveryTime' + rowCount + '  id="DeliveryTime' + rowCount + '">' + json[i].number + '</lable></td>';
                    html += '<td><lable class="labDeliveryTime' + rowCount + '  id="DeliveryTime' + rowCount + '">' + json[i].numbers + '</lable></td>';
                    html += '<td ><lable class="labDeliveryTime' + rowCount + '  id="DeliveryTime' + rowCount + '">' + json[i].Fnubers + '</lable></td>';
                    html += '<td  ><lable class="labRemark' + rowCount + '  id="Remark' + rowCount + '">' + json[i].finishtime + '</lable></td>';
                    html += '<td ><lable class="labDeliveryTime' + rowCount + '  id="DeliveryTime' + rowCount + '">' + json[i].people + '</lable></td>';
                    html += '<td  ><lable class="labRemark' + rowCount + '  id="Remark' + rowCount + '">' + json[i].reason + '</lable></td>';
                    html += '</tr>'
                    $("#DetailInfos").append(html);
                    CountRows += 1;
                }
            }
        }
    });
}

