var isConfirm = false;
$(document).ready(function () {
   // $("#OrderDate").val('');
    var typeVal = $("#Type").val();
    if (typeVal == "0") {
        $("#trZS").css("display", "none");
        $("#trZS0").css("display", "none");
        $("#trZS1").css("display", "none");
        $("#trZS2").css("display", "none");
        $("#spID").html("内购申请单号：");
        $("#spBill").html("内购单");
        $("#spProduct").html("内购产品");

        $("#divInter").css("display", "inline");
        $("#divSend").css("display", "none");
    }
    else if (typeVal == "1") {
        //$("#trZS").css("display", "inline");
        //$("#trZS1").css("display", "inline");
        $("#spID").html("赠送申请单号：");
        $("#spBill").html("赠送单");
        $("#spProduct").html("赠送产品");

        $("#divInter").css("display", "none");
        $("#divSend").css("display", "inline");
    }

    $("#btnSave").click(function () {
        if (typeVal == "0") {
            isConfirm = confirm("确定创建内购申请吗？")
        }
        else if (typeVal == "1") {
            isConfirm = confirm("确定创建赠送申请吗？")
        }
        if (isConfirm == false) {
            return false;
        }
        else
            submitInfo();
    });

    $("#btnCancel").click(function () {
        window.parent.ClosePop();
    })
});

function returnConfirm() {
    return isConfirm;
}

function submitInfo() {
    //

    var OrderDate = $("#OrderDate").val();
    if (OrderDate == "" || OrderDate == undefined || OrderDate == null)
    {
        alert("申请日期不能为空");
        return;
    }
    var typeVal = $("#Type").val();
    if (typeVal == "0") {
        //内购申请
        var ProductID = "";
        var OrderContent = "";
        var SpecsModels = "";
        var GoodsType = "";
        var Amount = "";
        var UnitPrice = "";
        var Discounts = "";
        var Total = "";//小计
        var PayWay = "";
        var Remark = "";

        var tbody = document.getElementById("DetailInfo");
        var rowCount = tbody.rows.length;
        for (var i = 0; i < tbody.rows.length; i++) {
            var Productid = document.getElementById("ProductID" + i).innerHTML;
            var mainContent = document.getElementById("OrderContent" + i).innerHTML;
            var goodsType = document.getElementById("GoodsType" + i).value;
            var specsModels = document.getElementById("Spec" + i).value;
            var salesNum = document.getElementById("Amount" + i).value;
            var uitiprice = document.getElementById("UnitPrice" + i).value;
            var subtotal = document.getElementById("Total" + i).value;
            var discounts = document.getElementById("Discounts" + i).value;
            var payWay = document.getElementById("PayWay" + i).value;
            var remark = document.getElementById("txtRemark" + i).value;
            ProductID += Productid;
            OrderContent += mainContent;
            SpecsModels += specsModels;
            GoodsType += goodsType;
            Amount += salesNum;
            UnitPrice += uitiprice;
            Total += subtotal;
            Discounts += discounts;
            if (discounts < 0 || discounts > 10) {
                alert("折扣输入不正确");
                return;
            }
            PayWay += payWay;
            Remark += remark;
            if (i < tbody.rows.length - 1) {
                //ID += ",";
                ProductID += ",";
                OrderContent += ",";
                GoodsType += ",";
                SpecsModels += ",";
                Amount += ",";
                UnitPrice += ",";
                Total += ",";
                Discounts += ",";
                PayWay += ",";
                Remark += ",";
            }
            else {
                // ID += "";
                ProductID += "";
                OrderContent += "";
                GoodsType += "";
                SpecsModels += "";
                Amount += "";
                UnitPrice += "";
                Total += "";
                Discounts += "";
                PayWay += "";
                Remark += "";
            }
        }
        var options = {
            url: "SaveInternalOrder",
            type: "Post",
            ansyc: false,
            data: {
                ProductID: ProductID, OrderContent: OrderContent, GoodsType: GoodsType, SpecsModels: SpecsModels, Amount: Amount, UnitPrice: UnitPrice, Total: Total, Discounts: Discounts, PayWay: PayWay, txtRemark: Remark
            },
            async: false,
            success: function (data) {
                if (data.success == true) {
                    window.parent.frames["iframeRight"].reload();
                    alert("生成内购单！");
                    window.parent.ClosePop();
                }
                else {
                    alert("生成内购单失败-" + data.Msg);
                }
            }
        }
    }
    else if (typeVal == "1") {
        //内购
        var ProductID = "";
        var OrderContent = "";
        var GoodsType = "";
        var SpecsModels = "";
        var Amount = "";
        var UnitPrice = "";
        var Remark = "";
        var IAmount = 0;//内购单数量
        //var YPrice = "";
        //var TaxRate = "";
        var tbody = document.getElementById("SendDetailInfo");
        var rowCount = tbody.rows.length;
        for (var i = 0; i < tbody.rows.length; i++) {
            var Productid = document.getElementById("ProductID" + i).innerHTML;
            var goodsType = document.getElementById("GoodsType" + i).value;
            var mainContent = document.getElementById("OrderContent" + i).innerHTML;
            var specsModels = document.getElementById("Spec" + i).value;
            var salesNum = document.getElementById("Amount" + i).value;
            var uitiprice = document.getElementById("UnitPrice" + i).value;
            var remark = document.getElementById("txtRemark" + i).value;

            ProductID += Productid;
            OrderContent += mainContent;
            GoodsType += goodsType;
            SpecsModels += specsModels;
            Amount += salesNum;
            IAmount += parseInt(salesNum);
            UnitPrice += uitiprice;
            Remark += remark;
            if (i < tbody.rows.length - 1) {
                //ID += ",";
                ProductID += ",";
                OrderContent += ",";
                SpecsModels += ",";
                GoodsType += ",";
                Amount += ",";
                UnitPrice += ",";
                Remark += ",";
            }
            else {
                // ID += "";
                ProductID += "";
                OrderContent += "";
                SpecsModels += "";
                GoodsType += "";
                Amount += "";
                UnitPrice += "";
                Remark += "";
            }
        }
        var options = {
            url: "SaveInternalOrder",
            type: "Post",
            ansyc: false,
            data: {
                TaskLength: rowCount,IAmount:IAmount,
                ProductID: ProductID, OrderContent: OrderContent, GoodsType: GoodsType, SpecsModels: SpecsModels, Amount: Amount, UnitPrice: UnitPrice, txtRemark: Remark
            },
            async: false,
            success: function (data) {
                if (data.success == true) {
                    window.parent.frames["iframeRight"].reload();
                    alert("提交成功！");
                    window.parent.ClosePop();

                }
                else {
                    alert("生成失败-" + data.Msg);
                }
            }
        }
    }

    //
    //var tbody = document.getElementById("DetailInfo");
    //var rowCount = tbody.rows.length;
    //var options = {
    //    url: "SaveInternalOrder",
    //    data: { TaskLength: rowCount },
    //    type: 'POST',
    //    async: false,
    //    success: function (data) {
    //        if (data.success == true) {
    //            window.parent.frames["iframeRight"].reload();
    //            alert("提交成功！");
    //            window.parent.ClosePop();
    //        }
    //        else {
    //            alert(data.Msg);
    //        }
    //    }
    //};
    $("#form1").ajaxSubmit(options);
    return false;
}

function AddRow() {
    var typeVal = $("#Type").val();
    if (typeVal == "0") {
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
        listids[9] = "9";

        var listtypes = new Array();
        listtypes[0] = "text";
        listtypes[1] = "text";
        listtypes[2] = "text";
        listtypes[3] = "text";
        listtypes[4] = "text";
        listtypes[5] = "text";
        listtypes[6] = "text";
        listtypes[7] = "text";
        listtypes[8] = "select";
        listtypes[9] = "text";

        var listNewElements = new Array();
        listNewElements[0] = "input";
        listNewElements[1] = "input";
        listNewElements[2] = "input";
        listNewElements[3] = "input";
        listNewElements[4] = "input";
        listNewElements[5] = "input";
        listNewElements[6] = "input";
        listNewElements[7] = "input";
        listNewElements[8] = "select";
        listNewElements[9] = "input";

        var listNewElementsID = new Array();
        listNewElementsID[0] = "ProductID";
        listNewElementsID[1] = "OrderContent";
        listNewElementsID[2] = "GoodsType";
        listNewElementsID[3] = "SpecsModels";
        listNewElementsID[4] = "Amount";
        listNewElementsID[5] = "UnitPrice";
        listNewElementsID[6] = "Discounts";
        listNewElementsID[7] = "Total";
        listNewElementsID[8] = "PayWay";
        listNewElementsID[9] = "txtRemark";

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
        listCheck[9] = "n";

        $.post("GetConfigInfo?TaskType=PayWay", function (data) {
            var objInfo1 = JSON.parse(data);
            var listcontent = new Array();
            listcontent[8] = objInfo1;
            listcontent[9] = null;

            var tableGX = new Table(null, null, null, null, null, null, null, null);
            tableGX.addNewRow('myTable', listtypes, listNewElements, listcontent, 'DetailInfo', listNewElementsID, listCheck, "Internal");
            var tbody = document.getElementById("DetailInfo");
            var rowCount = tbody.rows.length;
            $("#Amount" + (rowCount - 1)).attr("onblur", "GetTotal(this)");
            $("#UnitPrice" + (rowCount - 1)).attr("onblur", "GetTotal(this)");
            $("#Discounts" + (rowCount - 1)).attr("onblur", "GetTotal(this)");
        });
    }
    else if (typeVal == "1") {
        var listids = new Array();
        listids[0] = "0";
        listids[1] = "1";
        listids[2] = "2";
        listids[3] = "3";
        listids[4] = "4";
        listids[5] = "5";
        listids[6] = "6";

        var listtypes = new Array();
        listtypes[0] = "text";
        listtypes[1] = "text";
        listtypes[2] = "text";
        listtypes[3] = "text";
        listtypes[4] = "text";
        listtypes[5] = "text";
        listtypes[6] = "text";

        var listNewElements = new Array();
        listNewElements[0] = "input";
        listNewElements[1] = "input";
        listNewElements[2] = "input";
        listNewElements[3] = "input";
        listNewElements[4] = "input";
        listNewElements[5] = "input";
        listNewElements[6] = "input";

        var listNewElementsID = new Array();
        listNewElementsID[0] = "ProductID";
        listNewElementsID[1] = "OrderContent";
        listNewElementsID[2] = "GoodsType";
        listNewElementsID[3] = "SpecsModels";
        listNewElementsID[4] = "Amount";
        listNewElementsID[5] = "UnitPrice";
        listNewElementsID[6] = "txtRemark";

        var listCheck = new Array();
        listCheck[0] = "y";
        listCheck[1] = "n";
        listCheck[2] = "n";
        listCheck[3] = "n";
        listCheck[4] = "n";
        listCheck[5] = "n";
        listCheck[6] = "n";

        var listcontent = new Array();
        var tableGX = new Table(null, null, null, null, null, null, null, null);
        tableGX.addNewRow('SendTable', listtypes, listNewElements, listcontent, 'SendDetailInfo', listNewElementsID, listCheck, "Internal");
        var tbody = document.getElementById("SendDetailInfo");
        var rowCount = tbody.rows.length;

    }
}

//function CheckDetail() {
//    ShowIframe1("选择货品信息", "../SalesManage/ChangeProduct", 500, 500);
//}

//选择物品页
function CheckDetail() {
    //this.className = "btnTw";
    //$('#btnAddF').attr("class", "btnTh");
    ShowIframe1("选择货品信息", "../SalesManage/ChangeProduct", 850, 550);
}
//添加物品
function addBasicDetail(PID) { //增加货品信息行
    var typeVal = $("#Type").val();
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    rowCount1 = document.getElementById("SendDetailInfo").rows.length;
    var CountRows1 = parseInt(rowCount1) + 1;
    //var strPID = $("#ProductID").val();
    //$("#ProductID").val(strPID + "," + ProductID);
    if (typeVal == "0") {

        $.ajax({
            url: "../SalesManage/GetBasicDetail",
            type: "post",
            data: { PID: PID },
            dataType: "json",
            ansyc: false,
            success: function (data) {
                var html2 = "";
                $.ajax({
                    url: "../SalesRetail/GetConfigInfo",
                    type: "post",
                    data: { TaskType: "PayWay" },
                    dataType: "json",
                    async: false,
                    success: function (data) {
                        var objInfo2 = JSON.parse(data);
                        if (objInfo2.length > 0) {
                            for (var i = 0; i < objInfo2.length ; i++) {
                                //   $("#Channels" + number).val(objInfo2[i].Text);
                                html2 += "<option value=" + objInfo2[i].ID + ">" + objInfo2[i].Text + "</option>"
                            }
                        }

                    }
                })
                var json = eval(data.datas);
                if (json.length > 0) {

                    $("#myTable DetailInfo").html("");
                    for (var i = 0; i < json.length; i++) {

                        var html = "";

                        html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                        html += '<td ><lable class="labRowNumber' + rowCount + ' "   id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' "  style="width:60px;" id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                        html += '<td ><lable class="labProName' + rowCount + ' "  style="width:60px;" id="OrderContent' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                        html += '<td ><input type="text"  style="width:60px;" class="labGoodsType' + rowCount + ' " id="GoodsType' + rowCount + '" /></td>';
                        html += '<td ><input type="text"  style="width:60px;" class="labSpec' + rowCount + ' " id="Spec' + rowCount + '" value="' + json[i].Spec + '" /></td>';
                        html += '<td ><input type="text" onblur=GetTotal(this)  style="width:60px;"  class="labAmount' + rowCount + ' " id="Amount' + rowCount + '" /> </td>';
                        html += '<td ><input type="text" onblur=GetTotal(this)  id="UnitPrice' + rowCount + '" style="width:60px;"> </td>';
                        html += '<td><input type="text"  style="width:60px;" onblur=GetTotal(this) id="Discounts' + rowCount + '" /></td>';
                        html += '<td ><input type="text"  style="width:60px;"  readonly="readonly" id="Total' + rowCount + '" > </td>';
                        html += '<td ><select name="select"  style="width:100px;"  id="PayWay' + rowCount + '">' + html2; +'</select></td>';//方式
                        html += '<td ><input name="select"  style="width:60px;" type="text" id="txtRemark' + rowCount + '"/></td>';
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
    else if (typeVal == "1") {
        $.ajax({
            url: "../SalesManage/GetBasicDetail",
            type: "post",
            data: { PID: PID },
            dataType: "json",
            ansyc: false,
            success: function (data) {
                var json = eval(data.datas);
                if (json.length > 0) {

                    $("#SendTable SendDetailInfo").html("");
                    for (var i = 0; i < json.length; i++) {
                        var html2 = "";
                        html2 = '<tr  id ="SendDetailInfo' + rowCount1 + '" onclick="selRow(this)">'
                        html2 += '<td ><lable class="labRowNumber' + rowCount1 + ' "   id="RowNumber' + rowCount1 + '">' + CountRows1 + '</lable> </td>';
                        html2 += '<td ><lable class="labProductID' + rowCount1 + ' "  style="width:60px;" id="ProductID' + rowCount1 + '">' + json[i].ProductID + '</lable> </td>';
                        html2 += '<td ><lable class="OrderContent' + rowCount1 + ' "  style="width:60px;" id="OrderContent' + rowCount1 + '">' + json[i].ProName + '</lable> </td>';
                        html2 += '<td ><input type="text"  style="width:60px;" class="labGoodsType' + rowCount1 + ' " id="GoodsType' + rowCount1 + '" /></td>';
                        html2 += '<td ><input type="text"  style="width:60px;" class="labSpec' + rowCount + ' " id="Spec' + rowCount1 + '" value="' + json[i].Spec + '" /></td>';
                        html2 += '<td ><input type="text" style="width:60px;" onblur=GetHJ()  class="labAmount' + rowCount1 + ' " id="Amount' + rowCount1 + '" /> </td>';
                        html2 += '<td ><input type="text" onblur=GetHJ() id="UnitPrice' + rowCount1 + '" style="width:60px;" /> </td>';
                        // html += '<td><input type="text"  style="width:60px;" onblur=GetTotal(this) id="Discounts' + rowCount + '" /></td>';
                        //    html += '<td ><input type="text"  style="width:60px;"  readonly="readonly" id="Total' + rowCount + '" > </td>';
                        //  html += '<td ><select name="select"  style="width:60px;"  id="PayWay' + rowCount + '">' + html2; +'</select></td>';//方式
                        // html2 += '<td ><input name="select"  style="width:60px;" type="text" id="txtRemark' + rowCount + '"/></td>';
                        html2 += '<td ><input name="select"  style="width:60px;" type="text" id="txtRemark' + rowCount1 + '"/></td>';
                        html2 += '<td style="display:none;"><lable class="labPID' + rowCount1 + ' " id="PID' + rowCount1 + '">' + json[i].PID + '</lable> </td>';
                        html2 += '</tr>'

                        $("#SendDetailInfo").append(html2);
                        CountRows1 = CountRows1 + 1;
                        rowCount1 += 1;
                    }


                }
            }
        })
    }

}

//方式下拉框值
function tyoncharge(select) {
    var txt = $(select).val();
    var id = select.id;
    // alert(txt + id);
    // var Belong = id.split('BelongCom')[0];
    var Belong = txt;//id.split("Belong")[0];
    var number = id.split("PayWay")[1];
    //$("#Channels" + number).attr("");
    $.ajax({
        url: "../SalesRetail/GetConfigInfo",
        type: "post",
        data: { TaskType: "PayWay" },
        dataType: "json",
        async: false,
        success: function (data) {
            var objInfo2 = JSON.parse(data);
            var html = "";
            if (objInfo2.length > 0) {

                for (var i = 0; i < objInfo2.length ; i++) {
                    //   $("#Channels" + number).val(objInfo2[i].Text);
                    html += "<option value=" + objInfo2[i].ID + ">" + objInfo2[i].Text + "</option>"
                }
            }
            $("#PayWay" + number).html(html);
        }
    })
}

function DelRow() {
    var typeVal = $("#Type").val();
    if (typeVal == "0") {
        var tbody = document.getElementById("DetailInfo");
        var rowCount = tbody.rows.length;
        if (rowCount == 0) {
            alert("当前没有可删除的行！");
            return;
        }

        var listtypeNames = new Array();
        listtypeNames[0] = "ProductID";
        listtypeNames[1] = "OrderContent";
        listtypeNames[2] = "GoodsType";
        listtypeNames[3] = "SpecsModels";
        listtypeNames[4] = "Amount";
        listtypeNames[5] = "UnitPrice";
        listtypeNames[6] = "Discounts";
        listtypeNames[7] = "Total";
        listtypeNames[8] = "PayWay";
        listtypeNames[9] = "txtRemark";

        var tableGX = new Table(null, null, null, null, null, null, null, null);
        tableGX.removeRow('myTable', 'DetailInfo', listtypeNames);
    }
    else if (typeVal == "1") {
        var tbody = document.getElementById("SendDetailInfo");
        var rowCount = tbody.rows.length;
        if (rowCount == 0) {
            alert("当前没有可删除的行！");
            return;
        }

        var listtypeNames = new Array();
        listtypeNames[0] = "ProductID";
        listtypeNames[1] = "OrderContent";
        listtypeNames[2] = "GoodsType";
        listtypeNames[3] = "SpecsModels";
        listtypeNames[4] = "Amount";
        listtypeNames[5] = "UnitPrice";
        listtypeNames[6] = "txtRemark";

        var tableGX = new Table(null, null, null, null, null, null, null, null);
        tableGX.removeRow('SendTable', 'SendDetailInfo', listtypeNames);
    }
}


function selRow(curRow) {
    newRowID = curRow.id;
}
//计算金额
function GetTotal(select) {
    var id = select.id;
    var number = id.split("Amount")[1];
    if (number == undefined) {
        number = id.split("UnitPrice")[1];
    }
    if (number == undefined) {
        number = id.split("Discounts")[1];
    }
    var Num = $("#Amount" + number).val();
    var Price = $("#UnitPrice" + number).val();
    var Discounts = $("#Discounts" + number).val();

    TaxRate = parseFloat(Discounts / 10);

    if (Num == "" || Num == null) {
        Num = 0;
    }
    if (Price == "" || Price == null) {
        Price = 0;
    }
    // Total = parseFloat(Num) * parseFloat(Price);
    var Total = 0;
    if (TaxRate == "") {
        Total = parseFloat(Price) * parseFloat(Num);
    } else { Total = parseFloat(Price) * parseFloat(Num) * parseFloat(TaxRate); }
    Total = Total.toFixed(2);
    $("#Total" + number).val(Total);
    GetNGHJ();
}
//赠送单合计
function GetHJ() {
    var Total = 0;
    var tbody = document.getElementById("SendDetailInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
        var Aount = document.getElementById("Amount" + i).value;
        var UnitPrice = document.getElementById("UnitPrice" + i).value;
        var SubTotal = Aount * UnitPrice;
        Total = Total + parseFloat(SubTotal);

    }
    Total = Total.toFixed(2);
    $("#GoodsTotal").val(Total);
}

//内购合计
function GetNGHJ() {
    var SubTotal = 0;
    var tbody = document.getElementById("DetailInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
        var Total = document.getElementById("Total" + i).value;
        // var UnitPrice = document.getElementById("UnitPrice" + i).value;
        //var SubTotal = Aount * UnitPrice;
        SubTotal = SubTotal + parseFloat(Total);

    }
    SubTotal = SubTotal.toFixed(2);
    $("#GoodsTotal").val(SubTotal);
}