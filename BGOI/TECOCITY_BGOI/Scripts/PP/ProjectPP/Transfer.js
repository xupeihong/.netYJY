
$(document).ready(function () {

    var SHID = location.search.split('&')[0].split('=')[1];
    var XXID = location.search.split('&')[1].split('=')[1];
    if (XXID == "L") {
        addBasicDetail(SHID);
    }
    else {
        addBasicDetails(SHID);
    }

    $("#Submit").click(function () {
        AddLJ();
    });

});


function addBasicDetail(SHID) { //增加货品信息行
    $.ajax({
        url: "SelectSHXX",
        type: "post",
        data: { SHID: SHID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("GXInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr>'
                    html += '<td ><lable class="labRowNumber " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td style="display:none"><lable class="labProductID " id="SHID' + rowCount + '">' + json[i].SHID + '</lable> </td>';
                    html += '<td style="display:none"><lable class="labProductID " id="DID' + rowCount + '">' + json[i].DID + '</lable> </td>';
                    html += '<td style="display:none" ><lable class="labProductID " id="DDID' + rowCount + '">' + json[i].Bak + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="MaterialNO' + rowCount + '">' + json[i].INID + '</lable> </td>';
                    html += '<td style="display:none"><lable class="labOrderContent' + rowCount + ' " id="Supplier' + rowCount + '">' + json[i].Supplier + '</lable> </td>';
                    html += '<td ><lable class="labProductID " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="Amount' + rowCount + '">' + json[i].ActualAmount + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                    html += '</tr>'
                    if (json[i].ActualAmount != "0") {
                        $("#GXInfo").append(html);
                    }

                }
            }
        }
    })
}
function addBasicDetails(SHID) { //增加货品信息行
    $.ajax({
        url: "SelectSHXX",
        type: "post",
        data: { SHID: SHID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("GXInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr>'
                    html += '<td ><lable class="labRowNumber " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td style="display:none"><lable class="labProductID " id="SHID' + rowCount + '">' + json[i].SHID + '</lable> </td>';
                    html += '<td style="display:none"><lable class="labProductID " id="DID' + rowCount + '">' + json[i].DID + '</lable> </td>';
                    html += '<td style="display:none" ><lable class="labProductID " id="DDID' + rowCount + '">' + json[i].Bak + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="MaterialNO' + rowCount + '">' + json[i].INID + '</lable> </td>';
                    html += '<td style="display:none"><lable class="labOrderContent' + rowCount + ' " id="Supplier' + rowCount + '">' + json[i].Supplier + '</lable> </td>';
                    html += '<td ><lable class="labProductID " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="Amount' + rowCount + '">' + json[i].ActualAmount + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                    html += '</tr>'
                    if (json[i].ActualAmount != "0") {
                       $("#GXInfo").append(html);
                    }
                }
            }
        }
    })
}
function AddLJ() {

    var TransferNum = $("#TransferNum").val();
    var SJPeople = $("#SJPeople").val();
    if (SJPeople == "") {
        alert("送检人不可为空！！！");
        return;
    }
    var Inspectiondate = $("#Inspectiondate").val();
    if (Inspectiondate == "") {
        alert("送检日期不可为空！！！");
        return;
    }
    var GoodsDate = $("#GoodsDate").val();
    if (GoodsDate == "") {
        alert("送检日期不可为空！！！");
        return;
    }
    //var LJReturnDate = $("#LJReturnDate").val();
    //if (LJReturnDate == "") {
    //    alert("送检日期不可为空！！！");
    //    return;
    //}
    var SHID = "";
    var DDid = "";
    var Did = "";
    var MaterialNO = "";
    var Supplier = "";
    var ProName = "";
    var Spec = "";
    var Amount = "";
    var Units = "";
    var tbody = document.getElementById("GXInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
        var Shid = document.getElementById("SHID" + i).innerHTML;
        var DId = document.getElementById("DID" + i).innerHTML;
        var DDID = document.getElementById("DDID" + i).innerHTML;
        var Materialno = document.getElementById("MaterialNO" + i).innerHTML;
        var Suppliers = document.getElementById("Supplier" + i).innerHTML;
        var Proname = document.getElementById("ProName" + i).innerHTML;
        var Specs = document.getElementById("Spec" + i).innerHTML;
        var Amounts = document.getElementById("Amount" + i).innerHTML;
        var Unitss = document.getElementById("Units" + i).innerHTML;
        SHID += Shid;
        DDid += DDID;
        Did += DId;
        MaterialNO += Materialno;
        Supplier += Suppliers;
        ProName += Proname;
        Spec += Specs;
        Amount += Amounts;
        Units += Unitss;
        if (i < tbody.rows.length - 1) {
            SHID += ',';
            DDid += ',';
            Did += ',';
            MaterialNO += ',';
            Supplier += ',';
            ProName += ',';
            Spec += ',';
            Amount += ',';
            Units += ',';
        }
        else {
            SHID += '';
            DDid += '';
            Did += '';
            MaterialNO += '';
            Supplier += '';
            ProName += '';
            Spec += '';
            Amount += '';
            Units += '';
        }
    }
    isConfirm = confirm("确定要生成吗")
    if (isConfirm == false) {
        return false;
    }
    $.ajax({
        url: "InsertJJD",
        type: "Post",
        data: {
            shid: SHID, ddid: DDid, materialno: MaterialNO, supplier: Supplier, proname: ProName, spec: Spec, amount: Amount, units: Units, did: Did,

            transfernum: TransferNum, sjpeople: SJPeople, inspectiondate: Inspectiondate, goodsdate: GoodsDate
        },
        async: false,
        success: function (data) {
            if (data.success == true) {
                window.parent.frames["iframeRight"].reload();
                alert("成功");
                setTimeout('parent.ClosePop()', 100);
            }
            else {
                alert("失败");
            }
        }
    });

}