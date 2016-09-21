$(document).ready(function () {
    if (location.search != "") {
        RWID = location.search.split('&')[0].split('=')[1];
    }
    $("#RWID").attr("value", RWID);
    LoadTask();
    $("#btnSaveOrder").click(function () {
       
    });
    $("#btnExit").click(function () {
        window.parent.ClosePop();
    });
});

function LoadTask() {
    $.ajax({
        url: "ProTaskDetail",
        type: "post",
        data: { RWID: RWID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="SpecsModels' + rowCount + '">' + json[i].SpecsModels + '</lable> </td>';
                    html += '<td ><lable class="labOrderUnit' + rowCount + ' " id="OrderUnit' + rowCount + '">' + json[i].OrderUnit + '</lable> </td>';
                    html += '<td ><lable class="labOrderNum' + rowCount + ' " id="OrderNum' + rowCount + '">' + json[i].OrderNum + '</lable> </td>';
                    //html += '<td ><lable class="labTechnology' + rowCount + ' " id="Technology' + rowCount + '">' + json[i].Technology + '</lable> </td>';
                    html += '<td ><lable class="labDeliveryTime' + rowCount + ' " id="DeliveryTime' + rowCount + '">' + json[i].DeliveryTime + '</lable> </td>';
                    //html += '<td ><lable class="labRemark' + rowCount + ' " id="Remark' + rowCount + '">' + json[i].Remark + '</lable> </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                    CountRows = CountRows + 1;
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