
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
        var Amount = $("#AmountM").val();
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
            alert("经手人不能为空");
            return;
        }

        var MainContent = "";
        var PID = "";
        var ProName = "";
        var SpecsModels = "";
        var UnitName = "";
        var StockOutCount = "";
        var UnitPrice = "";
        var TotalAmount = "";
        // var Manufacturer = "";
        var Remark = "";
        var DID = "";
        var SIID = "";

        var tbody = document.getElementById("DetailInfo");
        for (var i = 0; i < tbody.rows.length; i++) {
            var mainContent = document.getElementById("RowNumber" + i).innerHTML;
            var pID = document.getElementById("MaterialNum" + i).innerHTML;
            var proName = document.getElementById("OrderContent" + i).innerHTML;
            var specsModels = document.getElementById("Specifications" + i).innerHTML;
            var unitName = document.getElementById("Company" + i).innerHTML;
            var stockOutCount = document.getElementById("Amount" + i).value;
            //alert(stockOutCount);
            var unitPrice = document.getElementById("Price" + i).innerHTML;
            var totalAmount = document.getElementById("Total" + i).value;
            // var manufacturer = document.getElementById("Manufacturer" + i).innerHTML;
            var remark = document.getElementById("Remark" + i).innerHTML;
            var did = document.getElementById("DID" + i).innerHTML;
            var siidId = document.getElementById("SIID" + i).innerHTML;

            MainContent += mainContent;
            PID += pID;
            ProName += proName;
            SpecsModels += specsModels;
            UnitName += unitName;
            StockOutCount += stockOutCount;
            UnitPrice += unitPrice;
            TotalAmount += totalAmount;
            //Manufacturer += manufacturer;
            Remark += remark;
            DID += did;
            SIID += siidId;

            if (i < tbody.rows.length - 1) {
                MainContent += ",";
                PID += ",";
                ProName += ",";
                SpecsModels += ",";
                UnitName += ",";
                StockOutCount += ",";
                UnitPrice += ",";
                TotalAmount += ",";
                //Manufacturer += ",";
                Remark += ",";
                DID += ",";
                SIID += ",";

            }
            else {
                MainContent += "";
                PID += "";
                ProName += "";
                SpecsModels += "";
                UnitName += "";
                StockOutCount += "";
                UnitPrice += "";
                TotalAmount += "";
                // Manufacturer += "";
                Remark += "";
                DID += "";
                SIID += "";

            }
        }

        $.ajax({
            url: "SaveCounterSalesOut",
            type: "Post",
            data: {
                ListOutID: ListOutID, SubjectID: SubjectID, ProOutTime: ProOutTime, HouseID: HouseID, DID: DID, Use: Use, Count: Count,
                ProOutUser: ProOutUser, PID: PID, ProName: ProName, SpecsModels: SpecsModels, MainContent: MainContent, SIID: SIID,
                UnitName: UnitName, StockOutCount: StockOutCount, UnitPrice: UnitPrice, TotalAmount: TotalAmount, Remark: Remark, Amount: Amount
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
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "GetCounterSalesDetail",
        type: "post",
        data: { did: DID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labMaterialNum' + rowCount + ' " id="MaterialNum' + rowCount + '">' + json[i].MaterialNum + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Specifications' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                    html += '<td ><lable class="labCompany' + rowCount + ' " id="Company' + rowCount + '">' + json[i].Company + '</lable> </td>';

                    html += '<td ><input type="text" id="Amount' + rowCount + '"  value="' + json[i].Amount + '" style="width:30px;" onblur="OnBlurAmount(this);"/></td>';
                    //html += '<td ><lable class="labAmount' + rowCount + ' " id="Amount' + rowCount + '">' + json[i].Amount + '</lable></td>';
                    html += '<td ><lable class="labPrice' + rowCount + ' " id="Price' + rowCount + '">' + json[i].Price + '</lable> </td>';

                    html += '<td ><input type="text" id="Total' + rowCount + '" value="' + json[i].Total + '" style="width:60px;" /></td>';
                    //html += '<td ><lable class="labTotal' + rowCount + ' " id="Total' + rowCount + '">' + json[i].Total + '</lable></td>';
                    // html += '<td ><lable class="labManufacturer' + rowCount + ' " id="CreateTime' + rowCount + '">' + json[i].CreateTime + '</lable> </td>';
                    html += '<td ><lable class="labRemark' + rowCount + ' " id="Remark' + rowCount + '">' + json[i].Remark + '</lable> </td>';
                    html += '<td style="display:none;"><lable class="labDID' + rowCount + ' " id="DID' + rowCount + '">' + json[i].DID + '</lable> </td>';
                    html += '<td style="display:none;"><lable class="labSIID' + rowCount + ' " id="SIID' + rowCount + '">' + json[i].SIID + '</lable> </td>';
                    html += '<td  style="display:none;"><lable class="labCount' + rowCount + ' " id="Count' + rowCount + '">' + json[i].Amount + '</lable></td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                }

                GetAmount();
                GetCount();
            }
        }
    })
    //GetAmount();
}

function AddNewOut() {
    ShowIframe1("选择专柜货品", "../InventoryManage/ChangeCountersSales", 500, 350);
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
    var tbodyID = "DetailInfo";
    var rowIndex = -1;
    var typeNames = ["RowNumber", "MaterialNum", "OrderContent", "Specifications", "Company", "Amount", "Price", "Total", "Manufacturer", "Remark", "DID", 'SIID', 'Count'];

    if (newRowID != "")
        rowIndex = newRowID.replace(tbodyID, '');
    if (rowIndex != -1) {
        document.getElementById(tbodyID).deleteRow(rowIaaandex);

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
                $("#RowNumber" + i).html(parseInt(i) + 1);
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
function GetCount() {
    $("#Count").val("");
    var count = 0;
    var tbody = document.getElementById("DetailInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
        var totalcount = document.getElementById("Amount" + i).value;
        //alert(totalcount);
        if (totalcount == "" || totalcount == null) {
            totalcount = "0";
        }
        count = count + parseInt(totalcount);

        $("#Count").val(count);
    }
}

function OnBlurAmount(rowcount) //求金额和
{
    var newCount = rowcount.id;
    var Count = $("#" + newCount).val();
    var strRow = newCount.charAt(newCount.length - 1);

    var AmountC = $("#Count" + strRow).text();
    if (parseInt(AmountC) < parseInt(Count)) {
        alert("所填的数量已超过此专柜制作物品数量，请重新填写...");
        $("#" + newCount).val("0");
        $("#Total" + strRow).val("0");
        return;
    }

    //var strU = "#Price" + strRow;
    //var strUnitPrice = $(strU).text();
    var strUnitPrice = document.getElementById("DetailInfo").getElementsByTagName("tr")[strRow].cells[6].innerText;
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