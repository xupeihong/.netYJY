var PID;
var rowCount;
var newRowID;

$(document).ready(function () {
    $("#Cancel").click(function () {
        window.parent.ClosePop();
    })
    $("#ScrapTime").val("");
    $("#btnSave").click(function () {
       // alert(1);
        var ShipGoodsID = $("#ShipGoodsID").val();
        var SubjectID = $("#SubjectID").val();
        var Remark = $("#Remark").val();
      
        var CreateTime = $("#CreateTime").val();
        var HouseID = $("#HouseID").val();
        var CreateUser = $("#CreateUser").val();
        var AmountM = $("#AmountM").val();
        var Count = $("#Count").val();


        if (SubjectID == "" || SubjectID == null) {
            alert("科目不能为空");
            return;
        }

        if (CreateTime == "" || CreateTime == null) {
            alert("创建时间不能为空");
            return;
        }
        if (HouseID == "" || HouseID == null) {
            alert("仓库不能为空");
            return;
        }
        if (CreateUser == "" || CreateUser == null) {
            alert("创建人不能为空");
            return;
        }

        var MainContent = "";
        var PID = "";
        var ProName = "";
        var SpecsModels = "";
        var UnitName = "";
        var ScrapCount = "";
        var UnitPrice = "";
        var TotalAmount = "";
        var Manufacturer = "";
        var Remark = "";
        var DID = "";
        var OrderID = "";

        var tbody = document.getElementById("DetailInfo");
        for (var i = 0; i < tbody.rows.length; i++) {
            var mainContent = document.getElementById("RowNumber" + i).innerHTML;
            var pID = document.getElementById("ProductID" + i).innerHTML;
            var proName = document.getElementById("OrderContent" + i).innerHTML;
            var specsModels = document.getElementById("SpecsModels" + i).innerHTML;
            var unitName = document.getElementById("OrderUnit" + i).innerHTML;
            var scrapCount = document.getElementById("OrderNum" + i).value;
            var unitPrice = document.getElementById("Price" + i).innerHTML;
            var totalAmount = document.getElementById("Subtotal" + i).value;
            var manufacturer = document.getElementById("Manufacturer" + i).innerHTML;
           // var remark = document.getElementById("Remark" + i).innerHTML;
            var did = document.getElementById("DID" + i).innerHTML;
            var orderId = document.getElementById("OrderID" + i).innerHTML;
            alert(orderId);

            MainContent += mainContent;
            PID += pID;
            ProName += proName;
            SpecsModels += specsModels;
            UnitName += unitName;
            ScrapCount += scrapCount;
            UnitPrice += unitPrice;
            TotalAmount += totalAmount;
            Manufacturer += manufacturer;
            //Remark += remark;
            DID += did;
            OrderID += orderId;

            if (i < tbody.rows.length - 1) {
                MainContent += ",";
                PID += ",";
                ProName += ",";
                SpecsModels += ",";
                UnitName += ",";
                ScrapCount += ",";
                UnitPrice += ",";
                TotalAmount += ",";
                Manufacturer += ",";
                //Remark += ",";
                DID += ",";
                OrderID += ",";

            }
            else {
                MainContent += "";
                PID += "";
                ProName += "";
                SpecsModels += "";
                UnitName += "";
                ScrapCount += "";
                UnitPrice += "";
                TotalAmount += "";
                Manufacturer += "";
               // Remark += "";
                DID += "";
                OrderID += "";
            }
        }
        //alert(2);
        $.ajax({
            url: "SaveSalesInvoiceManagement",
            type: "Post",
            data: {
                ShipGoodsID: ShipGoodsID, SubjectID: SubjectID, CreateTime: CreateTime, HouseID: HouseID, DID: DID, Count: Count,
                CreateUser: CreateUser, PID: PID, ProName: ProName, SpecsModels: SpecsModels, MainContent: MainContent, OrderID: OrderID,
                UnitName: UnitName, ScrapCount: ScrapCount, UnitPrice: UnitPrice, TotalAmount: TotalAmount, Manufacturer: Manufacturer, Remark: Remark, AmountM: AmountM
            },
            async: false,
            success: function (data) {
                if (data.success == true) {
                    alert("添加成功");
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
      

function addBasicDetail(PID) { //增加货品信息行
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    var strPID = $("#PID").val();
    $("#PID").val(strPID + "," + PID);
    $.ajax({
        url: "GetOrderSalesInvoDetail",
        type: "post",
        data: { PID: PID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="SpecsModels' + rowCount + '">' + json[i].SpecsModels + '</lable> </td>';
                    html += '<td ><lable class="labOrderUnit' + rowCount + ' " id="OrderUnit' + rowCount + '">' + json[i].OrderUnit + '</lable> </td>';

                    html += '<td ><input type="text" id="OrderNum' + rowCount + '" value="' + json[i].OrderNum + '" style="width:30px;" onblur="OnBlurAmount(this);"/></td>';
                    //html += '<td ><lable class="labOrderNum' + rowCount + ' " id="OrderNum' + rowCount + '">' + json[i].OrderNum + '</lable></td>';
                    html += '<td ><lable class="labPrice' + rowCount + ' " id="Price' + rowCount + '">' + json[i].Price + '</lable> </td>';

                    html += '<td ><input type="text" id="Subtotal' + rowCount + '" value="' + json[i].Subtotal + '" style="width:60px;"/></td>';
                    //html += '<td ><lable class="labSubtotal' + rowCount + ' " id="Subtotal' + rowCount + '">' + json[i].Subtotal + '</lable></td>';
                    html += '<td ><lable class="labManufacturer' + rowCount + ' " id="Manufacturer' + rowCount + '">' + json[i].Manufacturer + '</lable> </td>';
                    html += '<td ><lable class="labRemark' + rowCount + ' " id="Remark' + rowCount + '">' + json[i].Remark + '</lable> </td>';
                    html += '<td style="display:none;"><lable class="labDID' + rowCount + ' " id="DID' + rowCount + '">' + json[i].DID + '</lable> </td>';
                    html += '<td style="display:none;"><lable class="labOrderID' + rowCount + ' " id="OrderID' + rowCount + '">' + json[i].OrderID + '</lable> </td>';
                    html += '<td  style="display:none;"><lable class="labCount' + rowCount + ' " id="Count' + rowCount + '">' + json[i].OrderNum + '</lable></td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                    
                }

                GetAmount();
                GetCount();
            }
        }
    })
}

function AddNewBasic() { //弹出选择货品信息页面
    ShowIframe1("选择货品信息", "../InventoryManage/SalasInvoiSales", 500, 350);
}

function GetAmount() {
    $("#AmountM").val("");
    var Amount = 0;
    var tbody = document.getElementById("DetailInfo");

    for (var i = 0; i < tbody.rows.length; i++) {
        var totalAmount = document.getElementById("Subtotal" + i).value;
        if (totalAmount == "" || totalAmount == null) {
            totalAmount = "0";
        }
        Amount = Amount + parseInt(totalAmount);

        if (Amount == 0) {
            $("#AmountM").val("0");
        }
        else {
            $("#AmountM").val(Amount);
        }
    }
}

function GetCount() {
    $("#Count").val("");
    var count = 0;
    var tbody = document.getElementById("DetailInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
        var totalcount = document.getElementById("OrderNum" + i).value;
        if (totalcount == "" || totalcount == null) {
            totalcount = "0";
        }
        count = count + parseInt(totalcount);

        $("#Count").val(count);
    }
}

function selRow(curRow) {
    newRowID = curRow.id;
}

function DelRow() {
    var tbodyID = "DetailInfo";
    var rowIndex = -1;
    var typeNames = ["RowNumber", "ProductID", "OrderContent", "SpecsModels", "OrderUnit", "OrderNum", "Price", "Subtotal", "Manufacturer", "Remark", "DID", 'OrderID', 'Count'];

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


function OnBlurAmount(rowcount) //求金额和
{
    var newCount = rowcount.id;
    var Count = $("#" + newCount).val();
    var strRow = newCount.charAt(newCount.length - 1);

    var AmountC = $("#Count" + strRow).text();
    if (parseInt(AmountC) < parseInt(Count)) {
        alert("所填的数量已超过此零售销售的物品数量，请重新填写...");
        $("#" + newCount).val("0");
        $("#Subtotal" + strRow).val("0");
        return;
    }
    var strU = "#Price" + strRow;
    var strUnitPrice = $(strU).text();
    var strTotal = parseFloat(Count) * parseFloat(strUnitPrice);

    if (isNaN(strTotal)) {
        $("#Subtotal" + strRow).val("");
    }
    else {
        $("#Subtotal" + strRow).val(strTotal);
    }

    GetAmount();
    GetCount();
}