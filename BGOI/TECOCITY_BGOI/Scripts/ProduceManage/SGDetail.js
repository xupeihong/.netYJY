$(document).ready(function () {
    if (location.search != "") {
        SGID = location.search.split('&')[0].split('=')[1];
    }
    $("#SGID").attr("value", SGID);
    LoadRDatail();
    
    $("#btnExit").click(function () {
        window.parent.ClosePop();
    });
});


function LoadRDatail() {
   
    $.ajax({
        url: "LoadSGDetail",
        type: "post",
        data: { SGID: SGID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumbers' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labDID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="SpecsModels' + rowCount + '">' + json[i].SpecsModels + '</lable> </td>';
                    html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="OrderUnit' + rowCount + '">' + json[i].OrderUnit + '</lable> </td>';
                    html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="OrderNums' + rowCount + '">' + json[i].OrderNum + '</lable> </td>';
                    //html += '<td ><input type="text" id="OrderNums' + rowCount + '" style="width:60px;" value="' + json[i].OrderNum + '"/></td>';
                    html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="photo' + rowCount + '">' + json[i].photo + '</lable> </td>';
                    html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="Remarks' + rowCount + '">' + json[i].Remark + '</lable> <lable class="labSpecsModels' + rowCount + ' " id="OrderNumss' + rowCount + '" style="display:none">' + json[i].OrderNumss + '</lable> <lable class="labSpecsModels' + rowCount + ' " id="SGDID' + rowCount + '" style="display:none">' + json[i].SGDID + '</lable></td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                    CountRows = CountRows + 1;
                }
            }
        }
    })

   
    $.ajax({
        url: "LoadSGDetails",
        type: "post",
        data: { SGID: SGID },
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
                    html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="Process' + rowCount + '">' + json[i].Process + '</lable> </td>';
                    html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="team' + rowCount + '">' + json[i].team + '</lable> </td>';
                    html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="Estimatetime' + rowCount + '">' + json[i].Estimatetime + '</lable> </td>';
                    html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="person' + rowCount + '">' + json[i].person + '</lable> </td>';
                    html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="plannumber' + rowCount + '">' + json[i].plannumber + '</lable> </td>';
                    html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="Qualified' + rowCount + '">' + json[i].Qualified + '</lable> </td>';
                    html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="number' + rowCount + '">' + json[i].number + '</lable> </td>';
                    html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="numbers' + rowCount + '">' + json[i].numbers + '</lable> </td>';
                    html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="Fnubers' + rowCount + '">' + json[i].Fnubers + '</lable> </td>';
                    html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="finishtime' + rowCount + '">' + json[i].finishtime + '</lable> </td>';
                    html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="people' + rowCount + '">' + json[i].people + '</lable> </td>';
                    html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="reason' + rowCount + '">' + json[i].reason + '</lable> </td>';
                    //html += '<td ><input type="text" id="Process' + rowCount + '" style="width:60px;" value="' + json[i].Process + '"/></td>';
                    //html += '<td ><input type="text" id="team' + rowCount + '" style="width:60px;" value="' + json[i].team + '"/></td>';
                    //html += '<td ><input type="text" onclick="WdatePicker()" id="Estimatetime' + rowCount + '" style="width:100px;" value="' + json[i].Estimatetime + '"/></td>';
                    //html += '<td ><input type="text" id="person' + rowCount + '" style="width:60px;" value="' + json[i].person + '"/></td>';
                    //html += '<td ><input type="text" id="plannumber' + rowCount + '" style="width:30px;" value="' + json[i].plannumber + '"/></td>';
                    //html += '<td ><input type="text" id="Qualified' + rowCount + '" style="width:30px;" value="' + json[i].Qualified + '"/></td>';
                    //html += '<td ><input type="text" id="number' + rowCount + '" style="width:30px;" value="' + json[i].number + '"/></td>';
                    //html += '<td ><input type="text" id="numbers' + rowCount + '" style="width:30px;" value="' + json[i].numbers + '"/></td>';
                    //html += '<td ><input type="text" id="Fnubers' + rowCount + '" style="width:30px;" value="' + json[i].Fnubers + '"/></td>';
                    //html += '<td ><input type="text" onclick="WdatePicker()" id="finishtime' + rowCount + '" style="width:100px;" value="' + json[i].finishtime + '"/></td>';
                    //html += '<td ><input type="text" id="people' + rowCount + '" style="width:60px;" value="' + json[i].people + '"/></td>';
                    //html += '<td ><input type="text" id="reason' + rowCount + '" style="width:130px;" value="' + json[i].reason + '"/></td>';
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