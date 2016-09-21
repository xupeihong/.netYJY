$(document).ready(function () {
    if (location.search != "") {
        SID = location.search.split('&')[0].split('=')[1];
    }
    $("#btnPrint").click(function () {
        //document.getElementById("btnPrint").className = "Noprint";
        $("#pageContent").attr("style", "width: 100%;margin-top:10px")
        pagesetup("0.3", "0", "0.3", "0.3");
        window.print();
        document.getElementById("btnPrint").className = "btn";
        $("#pageContent").attr("style", "margin: 0 auto; width: 100%;margin-top:10px")
    });
    LoadSUPok();
})
function LoadSUPok() {
    rowCount = document.getElementById("DetailInfo").rows.length;
    CountRows = parseInt(rowCount);
    var SupplierType = $("#SupplierType").val();
    var COMNameC = $("#COMNameC").val();
    var COMArea = $("#COMArea").val();
    var SID = $("#Sid").val();
    var State = $("#State").val();


    $.ajax({
        url: "PrintOutSupply",
        type: "post",
        data: { sid: SID, type: SupplierType, name: COMNameC, area: COMArea, state: State },
        dataType: "json",
        async: false,
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var g = 0; g < json.length; g++) {
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)" style="text-align:center;">'
                    html += '<td style="width: auto"><lable class="labProductID' + rowCount + ' " id="SupplierType' + rowCount + '">' + json[g].SID + '</lable> </td>';
                    html += '<td style="width: auto"><lable class="labProductID' + rowCount + ' " id="SupplierType' + rowCount + '">' + json[g].SupplierType + '</lable> </td>';
                    html += '<td style="width: auto"><lable class="labProductID' + rowCount + ' " id="COMNameC' + rowCount + '">' + json[g].COMNameC + '</lable> </td>';
                    html += '<td style="width: auto"><lable class="labProductID' + rowCount + ' " id="COMShortName' + rowCount + '">' + json[g].COMShortName + '</lable></td>';
                    html += '<td style="width: auto"><lable class="labProductID' + rowCount + ' " id="COMCountry' + rowCount + '" >' + json[g].COMCountry + '</lable></td>';
                    html += '<td style="width: auto"><lable class="labProductID' + rowCount + ' " id="COMArea' + rowCount + '">' + json[g].COMArea + '</lable> </td>';
                    html += '<td style="width: auto"><lable class="labProductID' + rowCount + ' " id="ComAddress' + rowCount + '">' + json[g].ComAddress + '</lable></td>';
                    html += '<td style="width: auto"><lable class="labProductID' + rowCount + ' " id="COMCreateDate' + rowCount + '">' + json[g].COMCreateDate + '</lable> </td>';
                    html += '<td style="width: auto"><lable class="labProductID' + rowCount + ' " id="TaxRegistrationNo' + rowCount + '">' + json[g].TaxRegistrationNo + '</lable> </td>';
                    html += '<td style="width: auto"><lable class="labProductID' + rowCount + ' " id="BusinessLicenseNo' + rowCount + '">' + json[g].BusinessLicenseNo + '</lable> </td>';
                    html += '<td style="width: auto"><lable class="labProductID' + rowCount + ' " id="COMRAddress' + rowCount + '">' + json[g].COMRAddress + '</lable></td>';
                    html += '<td style="display:none;" style="width: auto"><lable class="labSID' + rowCount + ' " id="SID' + rowCount + '">' + json[g].SID + '</lable> </td>';
                    html += '</tr>'
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