$(document).ready(function () {
    if (location.search != "") {
        LLID = location.search.split('&')[0].split('=')[1];
    }
    $("#LLID").attr("value", LLID);
    LoadMaterialForm();
   
    $("#btnExit").click(function () {
        window.parent.ClosePop();
    });
});


function LoadMaterialForm() {
   
    $.ajax({
        url: "GetMaterialFormDetails",
        type: "post",
        data: { LLID: LLID },
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
                    html += '<td ><lable class="labPID' + rowCount + ' " id="PIDs' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td ><lable class="labPID' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labPID' + rowCount + ' " id="SpecsModels' + rowCount + '">' + json[i].SpecsModels + '</lable> </td>';
                    //html += '<td ><lable class="labPID' + rowCount + ' " id="Specifications' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                    html += '<td ><lable class="labPID' + rowCount + ' " id="Manufacturer' + rowCount + '">' + json[i].Manufacturer + '</lable> </td>';
                    html += '<td ><lable class="labPID' + rowCount + ' " id="OrderUnit' + rowCount + '">' + json[i].OrderUnit + '</lable> </td>';
                    html += '<td ><lable class="labPID' + rowCount + ' " id="OrderNum' + rowCount + '">' + json[i].OrderNum + '</lable> </td>';
                    html += '<td ><lable class="labPID' + rowCount + ' " id="Technology' + rowCount + '">' + json[i].Technology + '</lable> </td>';
                    html += '<td ><lable class="labPID' + rowCount + ' " id="Remark' + rowCount + '">' + json[i].Remark + '</lable> </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                    CountRows = CountRows + 1;
                }
            }
        }
    })

    $.ajax({
        url: "GetMaterialFormTaskdetail",
        type: "post",
        data: { LLID: LLID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfos").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr  id ="DetailInfos' + rowCount + '" onclick="selRows(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labDID' + rowCount + ' " id="PIDss' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td ><lable class="labDID' + rowCount + ' " id="PIDss' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labDID' + rowCount + ' " id="PIDss' + rowCount + '">' + json[i].SpecsModels + '</lable> </td>';
                    html += '<td ><lable class="labDID' + rowCount + ' " id="PIDss' + rowCount + '">' + json[i].OrderUnit + '</lable> </td>';
                    html += '<td ><lable class="labDID' + rowCount + ' " id="PIDss' + rowCount + '">' + json[i].Amount + '</lable></td>';
                    html += '<td ><lable class="labDID' + rowCount + ' " id="PIDss' + rowCount + '">' + json[i].Remark + '</lable> </td>';
                    html += '</tr>'
                    $("#DetailInfos").append(html);
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


function selRows(curRow) {
    newRowID = curRow.id;
    $("#DetailInfos tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}