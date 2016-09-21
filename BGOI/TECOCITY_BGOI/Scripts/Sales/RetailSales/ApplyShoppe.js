var isConfirm = false;
$(document).ready(function () {
    //$("#ApplyTime").val('');

    $("#btnSave").click(function () {
        isConfirm = confirm("确定创建销售记录吗？")
        if (isConfirm == false) {
            return false;
        }
        else {   //submitInfo();]

            var ProductID = "";
            var OrderContent = "";
            var SpecsModels = "";
            var GoodsType = "";
            var DAmount = "";
            var UnitPrice = "";
            var Discounts = "";
            var Total = "";
            var Remark = "";
            //var BelongCom = "";
            var BusinessType = "";
            var tbody = document.getElementById("DetailInfo");
            var rowCount = tbody.rows.length;
            for (var i = 0; i < tbody.rows.length; i++) {
                var productid = document.getElementById("ProductID" + i).innerHTML;
                var mainContent = document.getElementById("OrderContent" + i).innerHTML;
                var type = document.getElementById("GoodsType" + i).value;
                var specsModels = document.getElementById("Spec" + i).value;
                var salesNum = document.getElementById("Amount" + i).value;
                var unitprice = document.getElementById("UnitPrice" + i).value;
                var discounts = document.getElementById("Discounts" + i).value;
                var subtotal = document.getElementById("Total" + i).value;
                var remark = document.getElementById("Remark" + i).value;
                // var businessType = document.getElementById("BusinessType" + i).value;

                ProductID += productid;
                OrderContent += mainContent;
                GoodsType += type;
                SpecsModels += specsModels;
                DAmount += salesNum;
                // UnitPrice += uitiprice;
                Total += subtotal;
                Remark += remark;
                UnitPrice += unitprice;
                Discounts += discounts;

                if (i < tbody.rows.length - 1) {
                    //ID += ",";
                    ProductID += ",";
                    OrderContent += ",";
                    GoodsType += ",";
                    SpecsModels += ",";
                    DAmount += ",";
                    //  UnitPrice += ",";
                    Total += ",";
                    Remark += ",";
                    UnitPrice += ",";

                    Discounts += ",";
                }
                else {
                    // ID += "";
                    ProductID += "";
                    OrderContent += "";
                    GoodsType += "";
                    SpecsModels += "";
                    DAmount += "";
                    Total += "";
                    Remark += "";
                    UnitPrice += "";
                    Discounts += "";
                }
            }
            var options = {
                url: "SaveApplyShoppe",
                type: "Post",
                ansyc: false,
                data: {
                    TaskLength: rowCount, ProductID: ProductID,
                    OrderContent: OrderContent, GoodsType: GoodsType, SpecsModels: SpecsModels, DAmount: DAmount, Total: Total, Price: UnitPrice, txtRemark: Remark, Discount: Discounts
                },
                async: false,
                success: function (data) {
                    if (data.success == true) {
                        window.parent.frames["iframeRight"].reload();
                        alert("提交完成！");
                        window.parent.ClosePop();
                    }
                    else {
                        alert("提交失败-" + data.Msg);
                    }
                }
            }
            $("#form1").ajaxSubmit(options);
           return false;

        }
    });


    $("#btnCancel").click(function () {
        window.parent.ClosePop();
    });
});

function returnConfirm() {
    return isConfirm;
}

function submitInfo() {
    var tbody = document.getElementById("DetailInfo");
    var rowCount = tbody.rows.length;
    var options = {
        url: "SaveApplyShoppe",
        data: { TaskLength: rowCount },
        type: 'POST',
        async: false,
        success: function (data) {
            if (data.success == true) {
                window.parent.frames["iframeRight"].reload();
                alert("提交完成！");
                window.parent.ClosePop();
            }
            else {
                alert(data.Msg);
            }
        }
    };
    $("#form1").ajaxSubmit(options);
    return false;
}

function AddRow() {

    var listids = new Array();
    listids[0] = "0";
    listids[1] = "1";
    listids[2] = "2";
    listids[3] = "3";
    listids[4] = "4";
    listids[5] = "5";
    listids[6] = "6";
    listids[7] = "7";
    listids[8] = "8";

    var listtypes = new Array();
    listtypes[0] = "text";
    listtypes[1] = "text";
    listtypes[2] = "text";
    listtypes[3] = "text";
    listtypes[4] = "text";
    listtypes[5] = "text";
    listtypes[6] = "text";
    listtypes[7] = "text";
    listtypes[8] = "text";

    var listNewElements = new Array();
    listNewElements[0] = "input";
    listNewElements[1] = "input";
    listNewElements[2] = "input";
    listNewElements[3] = "input";
    listNewElements[4] = "input";
    listNewElements[5] = "input";
    listNewElements[6] = "input";
    listNewElements[7] = "input";
    listNewElements[8] = "input";

    var listNewElementsID = new Array();
    listNewElementsID[0] = "ProductID";
    listNewElementsID[1] = "OrderContent";
    listNewElementsID[2] = "SpecsModels";
    listNewElementsID[3] = "GoodsType";
    listNewElementsID[4] = "Amount";
    listNewElementsID[5] = "Price";
    listNewElementsID[6] = "Discount";
    listNewElementsID[7] = "Total";
    listNewElementsID[8] = "txtRemark";

    var listCheck = new Array();
    listCheck[0] = "y";
    listCheck[1] = "n";
    listCheck[2] = "n";
    listCheck[3] = "n";
    listCheck[4] = "n";
    listCheck[5] = "n";
    listCheck[6] = "n";
    listCheck[7] = "n";
    listCheck[8] = "n";

    var listcontent = new Array();

    var tableGX = new Table(null, null, null, null, null, null, null, null);
    tableGX.addNewRow('myTable', listtypes, listNewElements, listcontent, 'DetailInfo', listNewElementsID, listCheck, "Shoppe");
    var tbody = document.getElementById("DetailInfo");
    var rowCount = tbody.rows.length;
    //for (var i = 0; i < rowCount; i++) {
    //    document.getElementById("myTable").rows[i + 1].cells[0].style.display = "none";
    //}
}
//
function CheckDetail() {
    ShowIframe1("选择货品信息", "../SalesManage/ChangeProduct", 850, 550);
}

//上样添加物品
function addBasicDetail(PID) { //增加货品信息行
    var typeVal = $("#Type").val();
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    //var strPID = $("#ProductID").val();
    //$("#ProductID").val(strPID + "," + ProductID);
    $.ajax({
        url: "../SalesManage/GetBasicDetail",
        type: "post",
        data: { PID: PID },
        dataType: "json",
        ansyc: false,
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {

                $("#myTable DetailInfo").html("");
                for (var i = 0; i < json.length; i++) {
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' "   id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' "  style="width:60px;" id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                    html += '<td ><lable class="labProName' + rowCount + ' "  style="width:60px;" id="OrderContent' + rowCount + '">' + json[i].ProName + '</lable> </td>';

                    html += '<td ><input type="text"  style="width:60px;" class="labSpec' + rowCount + ' " id="Spec' + rowCount + '" value="' + json[i].Spec + '" /></td>';
                    html += '<td ><input type="text"  style="width:60px;" class="labGoodsType' + rowCount + ' " id="GoodsType' + rowCount + '" /></td>';
                    html += '<td ><input type="text"   style="width:60px;"  class="labAmount' + rowCount + ' " onblur=GetTotal(this)  id="Amount' + rowCount + '" /> </td>';
                    html += '<td ><input type="text" onblur=GetTotal(this)   id="UnitPrice' + rowCount + '" style="width:60px;" value="' + json[i].UnitPrice + '"/> </td>';
                    html += '<td><input type="text"  style="width:60px;" onblur=GetTotal(this) id="Discounts' + rowCount + '" /></td>';
                    html += '<td ><input type="text"  style="width:60px;"   id="Total' + rowCount + '" /> </td>';
                    html += '<td ><input name="select"  style="width:60px;" type="text" id="Remark' + rowCount + '"/></td>';
                    html += '<td style="display:none;"><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '</tr>'

                    $("#DetailInfo").append(html);
                    CountRows = CountRows + 1;
                    rowCount += 1;
                }


            }
        }
    })



}

function DelRow() {
    var tbody = document.getElementById("DetailInfo");
    var rowCount = tbody.rows.length;
    if (rowCount == 0) {
        alert("当前没有可删除的行！");
        return;
    }

    var listtypeNames = new Array();
    listtypeNames[0] = "ProductID";
    listtypeNames[1] = "OrderContent";
    listtypeNames[2] = "SpecsModels";
    listtypeNames[3] = "GoodsType";
    listtypeNames[4] = "Amount";
    listtypeNames[5] = "Price";
    listtypeNames[6] = "Discount";
    listtypeNames[7] = "Total";
    listtypeNames[8] = "txtRemark";

    var tableGX = new Table(null, null, null, null, null, null, null, null);
    tableGX.removeRow('myTable', 'DetailInfo', listtypeNames);
}


function selRow(curRow) {
    newRowID = curRow.id;
}

function GetTotal(select) {
    var id = select.id;
    var number = id.split("Amount")[1];
    if (number == undefined) {
        number = id.split("UnitPrice")[1];
    }
    if (number == undefined) {
        number = id.split("Discounts")[1]
    }
    var Num = $("#Amount" + number).val();
    var Price = $("#UnitPrice" + number).val();
    var Discounts = $("#Discounts" + number).val();
    if (Num == "" || Num == null) {
        Num = 0;
    }
    if (Price == "" || Price == null) {
        Price = 0;
    }
    if (Discounts != "") {
        Discounts = Discounts / 10;
        Total = parseFloat(Num) * parseFloat(Price) * parseFloat(Discounts);
    } else {

        Total = parseFloat(Num)*parseFloat(Price) ;
    }

    Total = Total.toFixed(2);
    $("#Total" + number).val(Total);
    GetShopHJ();
}

//专柜合计
function GetShopHJ() {
    var SubTotal = 0;
    var tbody = document.getElementById("DetailInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
        var Total = document.getElementById("Total" + i).value;
        // var UnitPrice = document.getElementById("UnitPrice" + i).value;
        //var SubTotal = Aount * UnitPrice;
        SubTotal = SubTotal + parseFloat(Total);

    }
    SubTotal = SubTotal.toFixed(2);
    $("#Amount").val(SubTotal);
}