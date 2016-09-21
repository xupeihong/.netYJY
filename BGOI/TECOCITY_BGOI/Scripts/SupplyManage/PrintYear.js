var CountRows = 0, rowCount = 0
$(document).ready(function () {
    if (location.search != "") {
        SID = location.search.split('&')[0].split('=')[1];
    }
    $("#btnPrint").click(function () {

        $("#pageContent").attr("style", "width: 100%;margin-top:10px")
        pagesetup("0.3", "0", "0.3", "0.3");
        window.print();
        document.getElementById("btnPrint").className = "btn";
        $("#pageContent").attr("style", "margin: 0 auto; width: 100%;margin-top:10px")
    });
    LoadSUPok();
})
function LoadSUPok() {
    rowCount = document.getElementById("DetailInfo").rows.length;//供应商基本信息
    CountRows = parseInt(rowCount);
    $.ajax({
        url: "PrintYearSupply",
        type: "post",
        data: { sid: SID },
        dataType: "json",
        async: false,
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var g = 0; g < json.length; g++) {
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)" style="text-align:center;">'
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="COMNameC' + rowCount + '">' + json[g].COMNameC + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="DeptName' + rowCount + '">' + json[g].DeptName + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="Score1' + rowCount + '">' + json[g].Score1 + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="Score2' + rowCount + '">' + json[g].Score2 + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="Score3' + rowCount + '">' + json[g].Score3 + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="Score4' + rowCount + '">' + json[g].Score4 + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="Score5' + rowCount + '">' + json[g].Score5 + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="Result' + rowCount + '">' + json[g].Result + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="DeclareUser' + rowCount + '">' + json[g].DeclareUser + '</lable> </td>';
                    html += '</tr>';
                    $("#DetailInfo").append(html);
                }
            }
        }
    })

}
function selRow(rowid) {
    newRowID = rowid.id;
    $("#DetailInfo tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}

