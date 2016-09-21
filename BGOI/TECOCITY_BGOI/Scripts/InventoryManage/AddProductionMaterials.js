
var DID;
var rowCount;
$(document).ready(function () {
    $("#Cancel").click(function () {
        window.parent.ClosePop();
    })
    $("#ProOutTime").val("");
    $("#btnSave").click(function () {
        var ListOutID = $("#ListOutID").val();
        var SubjectID = $("#SubjectID").val();
        var ProOutTime = $("#ProOutTime").val();
        var HouseID = $("#HouseID").val();
        var ProOutUser = $("#ProOutUser").val();
        var Use = $("#Use").val();
        var Remark1 = $("#Remark").val();
        var Purchase = $("#Purchase").val();
        var Count = $("#Count").val();
        if (SubjectID == "" || SubjectID == null) {
            alert("科目不能为空");
            return;
        }

        if (ProOutTime == "" || ProOutTime == null) {
            alert("出库时间不能为空");
            return;
        }
        if (HouseID == "" || HouseID == null) {
            alert("仓库不能为空");
            return;
        }
        if (ProOutUser == "" || ProOutUser == null) {
            alert("库管不能为空");
            return;
        }
        if (Purchase == "" || Purchase == null) {
            alert("经手人不能为空");
            return;
        }
        var MainContent = "";
        var PID = "";
        var ProName = "";
        var SpecsModels = "";
        var UnitName = "";
        var StockOutCount = "";
        var StockOutCountActual = "";
        var UnitPrice = "";
        var TotalAmount = "";
        // var Manufacturer = "";
        var Remark = "";
        var DID = "";
        var LLID = "";

        var tbody = document.getElementById("DetailInfo");
        for (var i = 0; i < tbody.rows.length; i++) {
            var mainContent = tbody.getElementsByTagName("tr")[i].cells[0].innerText;//document.getElementById("RowNumber" + i).innerHTML;
            var pID = tbody.getElementsByTagName("tr")[i].cells[1].innerText;//document.getElementById("PID" + i).innerHTML;
            var proName = tbody.getElementsByTagName("tr")[i].cells[2].innerText;//document.getElementById("OrderContent" + i).innerHTML;
            var specsModels = tbody.getElementsByTagName("tr")[i].cells[3].innerText;//document.getElementById("Specifications" + i).innerHTML;
            var unitName = tbody.getElementsByTagName("tr")[i].cells[4].innerText;//document.getElementById("OrderUnit" + i).innerHTML;
            var stockOutCount = document.getElementById("OrderNum" + i).value;
            var stockOutCountActual = document.getElementById("StockOutCountActual" + i).value;
            var unitPrice = document.getElementById("Price" + i).value;
            var totalAmount = document.getElementById("Total" + i).value;
            //  var manufacturer = document.getElementById("Manufacturer" + i).innerHTML;
            var remark = tbody.getElementsByTagName("tr")[i].cells[9].innerText;//document.getElementById("Remark" + i).innerHTML;
            var did = tbody.getElementsByTagName("tr")[i].cells[10].innerText;//document.getElementById("DID" + i).innerHTML;
            var siidId = tbody.getElementsByTagName("tr")[i].cells[11].innerText;//document.getElementById("LLID" + i).innerHTML;

            MainContent += mainContent;
            PID += pID;
            ProName += proName;
            SpecsModels += specsModels;
            UnitName += unitName;
            StockOutCount += stockOutCount;
            StockOutCountActual += stockOutCountActual;
            UnitPrice += unitPrice;
            TotalAmount += totalAmount;
            //Manufacturer += manufacturer;
            Remark += remark;
            DID += did;
            LLID += siidId;

            if (i < tbody.rows.length - 1) {
                MainContent += ",";
                PID += ",";
                ProName += ",";
                SpecsModels += ",";
                UnitName += ",";
                StockOutCount += ",";
                StockOutCountActual += ",";
                UnitPrice += ",";
                TotalAmount += ",";
                //Manufacturer += ",";
                Remark += ",";
                DID += ",";
                LLID += ",";

            }
            else {
                MainContent += "";
                PID += "";
                ProName += "";
                SpecsModels += "";
                UnitName += "";
                StockOutCount += "";
                StockOutCountActual += "";
                UnitPrice += "";
                TotalAmount += "";
                // Manufacturer += "";
                Remark += "";
                DID += "";
                LLID += "";

            }
        }

        $.ajax({
            url: "SaveProductionMaterials",
            type: "Post",
            data: {
                ListOutID: ListOutID, SubjectID: SubjectID, ProOutTime: ProOutTime, HouseID: HouseID, DID: DID, Use: Use, Count: Count, Purchase: Purchase,
                ProOutUser: ProOutUser, PID: PID, ProName: ProName, SpecsModels: SpecsModels, MainContent: MainContent, LLID: LLID, Remark1: Remark1,
                StockOutCount: StockOutCount, Remark: Remark, UnitName: UnitName, UnitPrice: UnitPrice, TotalAmount: TotalAmount, StockOutCountActual: StockOutCountActual
            },
            async: false,
            success: function (data) {
                if (data.success == true) {
                    alert("出库成功");
                    setTimeout('parent.ClosePop()', 10);
                    window.parent.frames["iframeRight"].reload();
                }
                else {
                    alert(data.Msg);
                }
            }
        });
    });

});

function addProDetail(DID) {
    $.ajax({
        url: "GetProductionMaterialsSalesDetail",
        type: "post",
        data: { did: DID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                    // html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
                    html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Specifications' + rowCount + '">' + json[i].SpecsModels + '</lable> </td>';
                    html += '<td ><lable class="labOrderUnit' + rowCount + ' " id="OrderUnit' + rowCount + '">' + json[i].OrderUnit + '</lable> </td>';
                    html += '<td ><input type="text" id="OrderNum' + rowCount + '"  value="' + json[i].OrderNum + '" style="width:30px;"/></td>';

                    html += '<td ><input type="text" id="StockOutCountActual' + rowCount + '"style="width:30px;"   onblur="OnBlurAmount(this);"/></td>';
                    //html += '<td ><lable class="labAmount' + rowCount + ' " id="Amount' + rowCount + '">' + json[i].Amount + '</lable></td>';
                    // html += '<td ><lable class="labPrice' + rowCount + ' " id="Price' + rowCount + '">' + json[i].Price + '</lable> </td>';
                    html += '<td ><input type="text" id="Price' + rowCount + '"  value="0" style="width:30px;"  onblur="OnBlurUnitPrice(this);"/></td>';
                    html += '<td ><input type="text" id="Total' + rowCount + '" value="" style="width:60px;" /></td>';
                    //html += '<td ><lable class="labTotal' + rowCount + ' " id="Total' + rowCount + '">' + json[i].Total + '</lable></td>';' + json[i].Total + '' + json[i].Price + '
                    // html += '<td ><lable class="labManufacturer' + rowCount + ' " id="CreateTime' + rowCount + '">' + json[i].CreateTime + '</lable> </td>';
                    html += '<td ><lable class="labRemark' + rowCount + ' " id="Remark' + rowCount + '">' + json[i].Remark + '</lable> </td>';
                    html += '<td style="display:none;"><lable class="labDID' + rowCount + ' " id="DID' + rowCount + '">' + json[i].DID + '</lable> </td>';
                    html += '<td style="display:none;"><lable class="labLLID' + rowCount + ' " id="LLID' + rowCount + '">' + json[i].LLID + '</lable> </td>';
                    //   html += '<td  style="display:none;"><lable class="labCount' + rowCount + ' " id="Count' + rowCount + '">' + json[i].Amount + '</lable></td>';
                    html += '<td ><lable class="labIdentitySharing' + rowCount + ' " id="IdentitySharing' + rowCount + '">' + json[i].IdentitySharing + '</lable> </td>';
                
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                }
                //   GetAmount();
                GetCount();
            }
        }
    })
    //GetAmount();
}

function AddNewOut() {
    ShowIframe1("选择生产领料单", "../InventoryManage/ChangeProductionMaterials", 500, 350);
}

function GetAmount() {
    $("#AmountM").val("");
    var Amount = 0;
    var tbody = document.getElementById("DetailInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
        var totalAmount = document.getElementById("Total" + i).value;
        if (totalAmount == "" || totalAmount == null) {
            totalAmount = "0";
        }
        //var totalAmount = document.getElementById("Total0").innerHTML;
        Amount = Amount + parseInt(totalAmount);

        if (Amount == 0) {
            $("#AmountM").val("0");
        }
        else {
            $("#AmountM").val(Amount);
        }
    }
}
var newRowID;
function selRow(curRow) {
    newRowID = curRow.id;
    $("#DetailInfo tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}
function DelRow() {
    var one = confirm("确认删除");
    if (one == false)
        return;
    else {
        var tbodyID = "DetailInfo";
        var rowIndex = -1;
        var typeNames = ["RowNumber", "PID", "OrderContent", "Specifications", "OrderUnit", "OrderNum", "StockOutCountActual", "Price", "Total", "Remark", "DID", 'LLID', 'IdentitySharing'];
        if (newRowID != "")
            rowIndex = newRowID.replace(tbodyID, '');
        if (rowIndex != -1) {
            document.getElementById(tbodyID).deleteRow(rowIndex);

            if (rowIndex < $("#" + tbodyID + " tr").length) {
                for (var i = rowIndex; i < $("#" + tbodyID + " tr").length; i++) {
                    var tr = document.getElementById(tbodyID + (parseInt(i) + 1));
                    var tr1 = $("#" + tbodyID + (parseInt(i) + 1));
                    tr.id = tbodyID + i;
                    for (var j = 0; j < tr.childNodes.length; j++) {
                        var html = tr1.html();
                        for (var k = 0; k < typeNames.length; k++) {

                            var oldid = typeNames[k] + (parseInt(i) + 1);
                            var newid = typeNames[k] + i;

                            var reg = new RegExp(oldid, "g");

                            html = html.replace(reg, newid);

                        }
                        tr1.html(html);
                    }
                    document.getElementById("RowNumber" + i).innerHTML = parseInt(i) + 1;
                    // $("#RowNumber" + i).html(parseInt(i) + 1);
                }
            }
            if (document.getElementById(tbodyID).rows.length > 0) {
                if (rowIndex == document.getElementById(tbodyID).rows.length)
                    selRow(document.getElementById(tbodyID + (rowIndex - 1)), '');
                else
                    selRow(document.getElementById(tbodyID + rowIndex), '');;
            }
        }
        GetAmount();
        GetCount();
        $("#DetailInfo tr").removeAttr("class");
    }
}
function GetCount() {
    // alert(1);
    $("#Count").val("");
    var count = 0;
    var tbody = document.getElementById("DetailInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
        var totalcount = $("#OrderNum" + i).val();
        //alert(totalcount);
        if (totalcount == "" || totalcount == null) {
            totalcount = "0";
        }
        count = count + parseInt(totalcount);
        //alert(5);
        //alert(count);
        $("#Count").val(count);
    }
}

function OnBlurAmount(rowcount) //求金额和
{
    var newCount = rowcount.id;
    var Count = $("#" + newCount).val();
    // var strRow = newCount.charAt(newCount.length - 1);
    var strRow = newCount.substring(19);
    var AmountC = $("#Count" + strRow).text();
    if (parseInt(AmountC) < parseInt(Count)) {
        alert("所填的数量已超过此专柜制作物品数量，请重新填写...");
        $("#" + newCount).val("0");
        $("#Total" + strRow).val("0");
        return;
    }

    //var strU = "#Price" + strRow;
    //var strUnitPrice = $(strU).text();
    var strUnitPrice = document.getElementById("Price" + strRow).value;// document.getElementById("DetailInfo").getElementsByTagName("tr")[strRow].cells[6].innerText;
    var strTotal = parseFloat(Count) * parseFloat(strUnitPrice);

    if (isNaN(strTotal)) {
        $("#Total" + strRow).val("");
    }
    else {
        $("#Total" + strRow).val(strTotal);
    }

    GetAmount();
    GetCount();
}
function OnBlurUnitPrice(rowcount) {
    var newCount = rowcount.id;
    var UnitPrice = $("#" + newCount).val();
    //var strRow = newCount.charAt(newCount.length - 1);
    var strRow = newCount.substring(5);
    //var strU = "#UnitPrice" + strRow;
    //var strUnitPrice = $(strU).text();
    var strAmount = document.getElementById("StockOutCountActual" + strRow).value;// document.getElementById("DetailInfo").getElementsByTagName("tr")[strRow].cells[5].innerText;
    var strTotal = parseFloat(UnitPrice) * parseFloat(strAmount);
    $("#Total" + strRow).val(strTotal);
    GetAmount();
}