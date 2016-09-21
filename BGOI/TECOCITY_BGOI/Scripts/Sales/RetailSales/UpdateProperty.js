var isConfirm = false;
$(document).ready(function () {
    InitDetailInfo();
    InitRevokeInfo();
    var hdMalls = $("#hdMalls").val();
    var num = $("#Malls option").length;
    for (var i = 0; i < num; i++) {
        if ($("#Malls").get(0).options[i].text == hdMalls) {
            $("#Malls").get(0).options[i].selected = true;
        }
    }

    var applyDate = $("#hdApplyDate").val();
    if (applyDate.indexOf("1900") > -1) {
        $("#ApplyDate").val('');
    }

    $("#SampleNum").focus(function () {
        ChangeNum("SampleNum");
    });

    $("#RevokeNum").focus(function () {
        ChangeNum("RevokeNum");
    });

    $("#btnSave").click(function () {
        isConfirm = confirm("确定修改申请记录吗？")
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
    //var tbody = document.getElementById("DetailInfo");
    //var rowCount = tbody.rows.length;
    //var tbody1 = document.getElementById("RevokeInfo");
    //var rowCount1 = tbody1.rows.length;
    //160604


    var PAID = $("#PAID").val();
    var Applyer = $("#Applyer").val();
    var ApplyDate = $("#ApplyDate").val();
    var Malls = $("#Malls").val();
    var ExPlain = $("#ExPlain").val();
    var SampleNum = $("#SampleNum").val();
    var SampleAmount = $("#SampleAmount").val();
    var RevokeNum = $("#RevokeNum").val();
    var RevokeAmount = $("#RevokeAmount").val();
    //上样产品
    var ProductID = "";
    var OrderContent = "";
    var Ptype = "";
    var SpecsModels = "";
    var Amount = "";
    var UnitPrice = "";
    var Total = "";
    var Remark = "";
    //var BelongCom = "";
    var BusinessType = "";
    var tbody = document.getElementById("DetailInfo");
    var rowCount = tbody.rows.length;
    for (var i = 0; i < tbody.rows.length; i++) {
        var productid = document.getElementById("ProductID" + i).innerHTML;
        var mainContent = document.getElementById("OrderContent" + i).innerHTML;
        var ptype = document.getElementById("Ptype" + i).value;
        var specsModels = document.getElementById("SpecsModels" + i).innerHTML;
        var salesNum = document.getElementById("Amount" + i).value;
        var uitiprice = document.getElementById("UnitPrice" + i).value;
        var subtotal = document.getElementById("Total" + i).value;
        var remark = document.getElementById("Remark" + i).innerHTML;
        var businessType = document.getElementById("BusinessType" + i).value;

        ProductID += productid;
        OrderContent += mainContent;
        Ptype += ptype;
        SpecsModels += specsModels;
        Amount += salesNum;
        UnitPrice += uitiprice;
        Total += subtotal;
        Remark += remark;
        BusinessType += businessType;
        // Channels += channels;

        if (i < tbody.rows.length - 1) {
            //ID += ",";
            ProductID += ",";
            OrderContent += ",";
            Ptype += ",";
            SpecsModels += ",";
            Amount += ",";
            UnitPrice += ",";
            Total += ",";
            Remark += ",";
            BusinessType += ",";
            //  Channels += ",";
        }
        else {
            // ID += "";
            ProductID += "";
            OrderContent += "";
            Ptype += "";
            SpecsModels += "";
            Amount += "";
            UnitPrice += "";
            Total += "";
            Remark += "";
            BusinessType += "";
            //  Channels += "";
        }
    }

    //撤样产品
    var txtProductID = "";
    var txtOrderContent = "";
    var txtPtype = "";
    var txtSpecsModels = "";
    var txtAmount = "";
    var txtTotal = "";
    var txtRemark = "";
    var txtBusinessType = "";
    var txtUnitPrice = "";
    var tbody1 = document.getElementById("RevokeInfo");
    var rowCount1 = tbody1.rows.length;
    for (var i = 0; i < tbody1.rows.length; i++) {
        var txtproductid = document.getElementById("txtProductID" + i).innerHTML;
        var txtmainContent = document.getElementById("txtOrderContent" + i).innerHTML;
        var txtptype = document.getElementById("txtPtype" + i).value;
        var txtspecsModels = document.getElementById("txtSpecsModels" + i).innerHTML;
        var txtsalesNum = document.getElementById("txtAmount" + i).value;
        var txtuitiprice = document.getElementById("txtUnitPrice" + i).value;
        var txtsubtotal = document.getElementById("txtTotal" + i).value;
        var txtremark = document.getElementById("txtRemark" + i).innerHTML;
        var txtbusinessType = document.getElementById("txtBusinessType" + i).value;

        txtProductID += txtproductid;
        txtOrderContent += txtmainContent;
        txtPtype += txtptype;
        txtSpecsModels += txtspecsModels;
        txtAmount += txtsalesNum;
        txtUnitPrice += txtuitiprice;
        txtTotal += txtsubtotal;
        txtRemark += txtremark;
        txtBusinessType += txtbusinessType;
        // Channels += channels;

        if (i < tbody1.rows.length - 1) {
            //ID += ",";
            txtProductID += ",";
            txtOrderContent += ",";
            txtPtype += ",";
            txtSpecsModels += ",";
            txtAmount += ",";
            txtUnitPrice += ",";
            txtTotal += ",";
            txtRemark += ",";
            txtBusinessType += ",";
            //  Channels += ",";
        }
        else {
            // ID += "";
            txtProductID += "";
            txtOrderContent += "";
            txtPtype += "";
            txtSpecsModels += "";
            txtAmount += "";
            txtUnitPrice += "";
            txtTotal += "";
            txtRemark += "";
            txtBusinessType += "";
            //  Channels += "";
        }
    }
    $.ajax({
        url: "UpdatePrototypeInfo",
        type: "Post",
        ansyc: false,
        data: {
            TaskLength: rowCount, RevokeLength: rowCount1, PAID: PAID, Applyer: Applyer,
            ProductID: ProductID, ApplyDate: ApplyDate, Malls: Malls, ExPlain: ExPlain, SampleNum: SampleNum, SampleAmount: SampleAmount, RevokeNum: RevokeNum, RevokeAmount: RevokeAmount,
            OrderContent: OrderContent, Ptype: Ptype, SpecsModels: SpecsModels, Amount: Amount, UnitPrice: UnitPrice, Total: Total, BusinessType: BusinessType, Remark: Remark,
            txtProductID: txtProductID, txtOrderContent: txtOrderContent, txtPtype: txtPtype, txtSpecsModels: txtSpecsModels, txtAmount: txtAmount, txtUnitPrice: txtUnitPrice, txtTotal: txtTotal, txtBusinessType: txtBusinessType, txtRemark: txtRemark
        },
        //async: false,
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
    });

}

function InitDetailInfo2() {
    var PAID = $("#PAID").val();
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
    listtypes[0] = "lable";
    listtypes[1] = "lable";
    listtypes[2] = "text";
    listtypes[3] = "lable";
    listtypes[4] = "text";
    listtypes[5] = "text";
    listtypes[6] = "text";
    listtypes[7] = "text";
    listtypes[8] = "text";

    var listNewElements = new Array();
    listNewElements[0] = "lable";
    listNewElements[1] = "lable";
    listNewElements[2] = "input";
    listNewElements[3] = "lable";
    listNewElements[4] = "input";
    listNewElements[5] = "input";
    listNewElements[6] = "input";
    listNewElements[7] = "input";
    listNewElements[8] = "input";

    var listNewElementsID = new Array();
    listNewElementsID[0] = "ProductID";
    listNewElementsID[1] = "OrderContent";
    listNewElementsID[2] = "Ptype";
    listNewElementsID[3] = "SpecsModels";
    listNewElementsID[4] = "Amount";
    listNewElementsID[5] = "UnitPrice";
    listNewElementsID[6] = "Total";
    listNewElementsID[7] = "BusinessType";
    listNewElementsID[8] = "Remark";

    var listcontent = new Array();

    $.post("GetProtoTypeDetail?PAID=" + PAID + "&OperateType=0", function (data) {
        if (data != null) {
            var jsonTask = JSON.parse(data);
            var tableDetail = new Table(jsonTask, 'oddRow', 'evenRow', 'selRow', listids, listtypes, listNewElements, listcontent, listNewElementsID);
            tableDetail.LoadTableTBody('DetailInfo');
            var tbody = document.getElementById("DetailInfo");
            var rowCount = tbody.rows.length;
            //    $("#BelongCom" + (rowCount - 1)).attr("onchange", "tyoncharge(this)");
            $("#Amount" + (rowCount - 1)).attr("onblur", "XJ(this)");
            $("#UnitPrice" + (rowCount - 1)).attr("onblur", "XJ(this)");
            $("#Total" + (rowCount - 1)).attr("readonly", true);
        }
    });
}

function InitRevokeInfo2() {
    var PAID = $("#PAID").val();
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
    listNewElementsID[0] = "txtProductID";
    listNewElementsID[1] = "txtOrderContent";
    listNewElementsID[2] = "txtPtype";
    listNewElementsID[3] = "txtSpecsModels";
    listNewElementsID[4] = "txtAmount";
    listNewElementsID[5] = "txtUnitPrice";
    listNewElementsID[6] = "txtTotal";
    listNewElementsID[7] = "txtBusinessType";
    listNewElementsID[8] = "txtRemark";

    var listcontent = new Array();

    $.post("GetProtoTypeDetail?PAID=" + PAID + "&OperateType=1", function (data) {
        if (data != null) {
            var jsonTask = JSON.parse(data);
            var tableDetail = new Table(jsonTask, 'oddRow', 'evenRow', 'selRow', listids, listtypes, listNewElements, listcontent, listNewElementsID);
            tableDetail.LoadTableTBody('RevokeInfo');
            var tbody = document.getElementById("DetailInfo");
            var rowCount = tbody.rows.length;
            //    $("#BelongCom" + (rowCount - 1)).attr("onchange", "tyoncharge(this)");
            $("#txtAmount" + (rowCount - 1)).attr("onblur", "CXJ(this)");
            $("#txtUnitPrice" + (rowCount - 1)).attr("onblur", "CXJ(this)");
            $("#txtTotal" + (rowCount - 1)).attr("readonly", true);
        }
    });
}


function InitDetailInfo() { //增加货品信息行
    // var typeVal = $("#Type").val();
    var PAID = $("#PAID").val();
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "GetProtoTypeDetail",
        type: "post",
        data: { PAID:PAID ,OperateType:0 },
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
                    html += '<td ><lable class="labProName' + rowCount + ' "  style="width:60px;" id="OrderContent' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><input type="text"  style="width:60px;" class="labGoodsType' + rowCount + ' " id="Ptype' + rowCount + '" value="' + json[i].Ptype + '" /></td>';
                    html += '<td ><lable  style="width:60px;" class="labSpec' + rowCount + ' " id="SpecsModels' + rowCount + '"  >' + json[i].SpecsModels + '</lable></td>';
                    html += '<td ><input type="text"   style="width:60px;" onblur=XJ(this)  class="labAmount' + rowCount + ' " id="Amount' + rowCount + '" value="'+json[i].Amount+'"  /> </td>';
                    html += '<td ><input type="text" onblur=XJ(this)   id="UnitPrice' + rowCount + '" style="width:60px;" value="'+json[i].UnitPrice+'"> </td>';
                    html += '<td ><input type="text" readonly="readonly"  style="width:60px;"   id="Total' + rowCount + '" value="'+json[i].Total+'" /> </td>';
                    html += '<td ><input type="text"  style="width:60px;"  id="BusinessType' + rowCount + '" value="' + json[i].BusinessType + '" /></td>';//方式
                    html += '<td ><lable  style="width:60px;"  id="Remark' + rowCount + '" >'+json[i].Remark+'</lable></td>';
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

function InitRevokeInfo() {
    var PAID = $("#PAID").val();
    var typeVal = $("#Type").val();
    rowCount1 = document.getElementById("RevokeInfo").rows.length;
    var CountRows1 = parseInt(rowCount1) + 1;
    $.ajax({
        url: "GetProtoTypeDetail",
        type: "post",
        data: { PAID: PAID, OperateType: 1 },
        dataType: "json",
        ansyc: false,
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
              //  $("#myTable DetailInfo").html("");
                for (var i = 0; i < json.length; i++) {
                    var html = "";
                    html = '<tr  id ="RevokeInfo' + rowCount1 + '" onclick="selRow(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount1 + ' "   id="RowNumber' + rowCount1 + '">' + CountRows1 + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount1 + ' "  style="width:60px;" id="txtProductID' + rowCount1 + '" >' + json[i].ProductID + '</lable> </td>';
                    html += '<td ><lable class="labProName' + rowCount1 + ' "  style="width:60px;" id="txtOrderContent' + rowCount1 + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><input type="text"  style="width:60px;" class="labGoodsType' + rowCount1 + ' " id="txtPtype' + rowCount1 + '" value="' + json[i].Ptype + '" /></td>';
                    html += '<td ><lable  style="width:60px;" class="labSpec' + rowCount1 + ' " id="txtSpecsModels' + rowCount1 + '"  >' + json[i].SpecsModels + '</lable></td>';
                    html += '<td ><input type="text"   style="width:60px;" onblur=CXJ(this)  class="labAmount' + rowCount1 + ' " id="txtAmount' + rowCount1 + '" value="'+json[i].Amount+'" /> </td>';
                    html += '<td ><input type="text" onblur=CXJ(this)   id="txtUnitPrice' + rowCount1 + '" style="width:60px;" value="'+json[i].UnitPrice+'" /> </td>';
                    html += '<td ><input type="text" readonly="readonly"  style="width:60px;"   id="txtTotal' + rowCount1 + '" value="'+json[i].Total+'" /> </td>';
                    html += '<td ><input type="text"  style="width:60px;"  id="txtBusinessType' + rowCount1 + '" value="' + json[i].BusinessType + '" /></td>';//方式
                    html += '<td ><lable  style="width:60px;"  id="txtRemark' + rowCount1 + '" >' + json[i].Remark + '</lable></td>';
                    html += '<td style="display:none;"><lable class="labPID' + rowCount1 + ' " id="PID' + rowCount1 + '">' + json[i].PID + '</lable> </td>';
                    html += '</tr>'
                    $("#RevokeInfo").append(html);
                    CountRows1 = CountRows1 + 1;
                    rowCount1 += 1;
                }
            }
        }
    })
}




//上样选择货品信息
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
                    html += '<td ><input type="text"  style="width:60px;" class="labGoodsType' + rowCount + ' " id="Ptype' + rowCount + '" /></td>';
                    html += '<td ><lable  style="width:60px;" class="labSpec' + rowCount + ' " id="SpecsModels' + rowCount + '" >' + json[i].Spec + '</lable></td>';
                    html += '<td ><input type="text"   style="width:60px;" onblur=XJ(this)  class="labAmount' + rowCount + ' " id="Amount' + rowCount + '" /> </td>';
                    html += '<td ><input type="text" onblur=XJ(this)   id="UnitPrice' + rowCount + '" style="width:60px;"> </td>';
                    // html += '<td><input type="text"  style="width:60px;" onblur=GetTotal(this) id="Discounts' + rowCount + '" /></td>';
                    html += '<td ><input type="text" readonly="readonly"  style="width:60px;"   id="Total' + rowCount + '" /> </td>';
                    html += '<td ><input type="text"  style="width:60px;"  id="BusinessType' + rowCount + '" /></td>';//方式
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


//撤样选择货品信息
function CheckDetail1() {
    ShowIframe1("选择货品信息", "../SalesRetail/GetProduct", 850, 550);
}

//撤样添加物品
function addBasicDetail1(PID) { //增加货品信息行
    var typeVal = $("#Type").val();
    rowCount1 = document.getElementById("RevokeInfo").rows.length;
    var CountRows1 = parseInt(rowCount1) + 1;
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
             //   $("#myTable1 RevokeInfo").html("");
                for (var i = 0; i < json.length; i++) {
                    var html = "";
                    html = '<tr  id ="RevokeInfo' + rowCount1 + '" onclick="selRow(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount1 + ' "   id="RowNumber' + rowCount1 + '">' + CountRows1 + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount1 + ' "  style="width:60px;" id="txtProductID' + rowCount1 + '">' + json[i].ProductID + '</lable> </td>';
                    html += '<td ><lable class="labProName' + rowCount1 + ' "  style="width:60px;" id="txtOrderContent' + rowCount1 + '">' + json[i].ProName + '</lable> </td>';
                    html += '<td ><input type="text"  style="width:60px;" class="labGoodsType' + rowCount1 + ' " id="txtPtype' + rowCount1 + '" /></td>';
                    html += '<td ><lable  style="width:60px;" class="labSpec' + rowCount1 + ' " id="txtSpecsModels' + rowCount1 + '" >' + json[i].Spec + '</lable></td>';
                    html += '<td ><input type="text"   style="width:60px;" onblur=CXJ(this)  class="labAmount' + rowCount1 + ' " id="txtAmount' + rowCount1 + '" /> </td>';
                    html += '<td ><input type="text"  onblur=CXJ(this)  id="txtUnitPrice' + rowCount1 + '" style="width:60px;"> </td>';
                    // html += '<td><input type="text"  style="width:60px;" onblur=GetTotal(this) id="Discounts' + rowCount + '" /></td>';
                    html += '<td ><input type="text" readonly="readonly"  style="width:60px;"   id="txtTotal' + rowCount1 + '" /> </td>';
                    html += '<td ><input type="text"  style="width:60px;"  id="txtBusinessType' + rowCount1 + '" /></td>';//方式
                    html += '<td ><input name="select"  style="width:60px;" type="text" id="txtRemark' + rowCount1 + '"/></td>';
                    html += '<td style="display:none;"><lable class="labPID' + rowCount1 + ' " id="txtPID' + rowCount1 + '">' + json[i].PID + '</lable> </td>';
                    html += '</tr>'

                    $("#RevokeInfo").append(html);
                    CountRows1 = CountRows1 + 1;
                    rowCount1 += 1;
                }


            }
        }
    })



}



//上样产品
function XJ(select) {
    RowId = select.id;
    var Total = 0;
    var a = RowId.split('Amount');
    var b = RowId.split('UnitPrice');
    var Total = 0;
    var s = "";
    //s = a[1];
    if (a.length == 2) {
        s = a[1];
    }
    if (b.length == 2) {
        s = b[1];
    }
    var tbody = document.getElementById("DetailInfo");
    //  for (var i = 0; i < tbody.rows.length; i++) {
    var Amount = document.getElementById("Amount" + s).value;
    var UnitPrice = document.getElementById("UnitPrice" + s).value;
    var g = /^[1-9]*[1-9][0-9]*$/;

    if (Amount != "" && g.test(Amount) == false) {
        alert("数量输入有误");
        return;
    }
    if (Amount == "" || Amount == null) {
        Amount = "0";
    }

    var reg = /^[0-9]+.?[0-9]*$/;
    if (UnitPrice != "" && reg.test(UnitPrice) == false) {
        alert("价格输入不正确");
        return;
    }
    if (UnitPrice == "" || UnitPrice == null) {
        UnitPrice = "0.00";
    }
    Total = parseFloat(Amount) * parseFloat(UnitPrice);
    Total = Total.toFixed(2);
    $("#Total" + s).val(Total);
    //}
    HJ();
}
function HJ() {
    var Total = 0;
    var Amount = 0;
    var tbody = document.getElementById("DetailInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
        var Subtotal = document.getElementById("Total" + i).value;
        var SubAmount = document.getElementById("Amount" + i).value;
        if (Subtotal == "" || Subtotal == null) {
            Subtotal = "0";
        }
        if (SubAmount == "" || SubAmount == null) {
            SubAmount = "0";
        }

        Total = Total + parseFloat(Subtotal);
        Amount = Amount + parseInt(SubAmount);

    }
    $("#SampleAmount").val(Total);
    $("#SampleNum").val(Amount);
}

//撤样产品合计

function CXJ(select) {
    RowId1 = select.id;
    var Total = 0;
    var a = RowId1.split('txtAmount');
    var b = RowId1.split('txtUnitPrice');
    var c = RowId1.split('txtTotal');
    var Total = 0;
    var s = "";
    //s = a[1];
    if (a.length == 2) {
        s = a[1];
    }
    if (b.length == 2) {
        s = b[1];
    }
    if (c.length == 2) {
        s = c[1];
    }
    var tbody = document.getElementById("RevokeInfo");
    //  for (var i = 0; i < tbody.rows.length; i++) {
    var Amount = document.getElementById("txtAmount" + s).value;
    var UnitPrice = document.getElementById("txtUnitPrice" + s).value;
    var g = /^[1-9]*[1-9][0-9]*$/;

    if (Amount != "" && g.test(Amount) == false) {
        alert("数量输入有误");
        return;
    }
    if (Amount == "" || Amount == null) {
        Amount = "0";
    }

    var reg = /^[0-9]+.?[0-9]*$/;
    if (UnitPrice != "" && reg.test(UnitPrice) == false) {
        alert("价格输入不正确");
        return;
    }
    if (UnitPrice == "" || UnitPrice == null) {
        UnitPrice = "0.00";
    }
    Total = parseFloat(Amount) * parseFloat(UnitPrice);
    Total = Total.toFixed(2);
    $("#txtTotal" + s).val(Total);
    //}
    CHJ();
}
function CHJ() {
    var Total = 0;
    var Amount = 0;
    var tbody1 = document.getElementById("RevokeInfo");
    for (var i = 0; i < tbody1.rows.length; i++) {
        var Subtotal = document.getElementById("txtTotal" + i).value;
        var SubAmount = document.getElementById("txtAmount" + i).value;
        if (Subtotal == "" || Subtotal == null) {
            Subtotal = "0";
        }
        if (SubAmount == "" || SubAmount == null) {
            SubAmount = "0";
        }

        Total = Total + parseFloat(Subtotal);
        Amount = Amount + parseInt(SubAmount);

    }
    $("#RevokeNum").val(Amount);
    $("#RevokeAmount").val(Total);
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

    var listtypes = new Array();
    listtypes[0] = "text";
    listtypes[1] = "text";
    listtypes[2] = "text";
    listtypes[3] = "text";
    listtypes[4] = "text";
    listtypes[5] = "text";
    listtypes[6] = "text";
    listtypes[7] = "text";

    var listNewElements = new Array();
    listNewElements[0] = "input";
    listNewElements[1] = "input";
    listNewElements[2] = "input";
    listNewElements[3] = "input";
    listNewElements[4] = "input";
    listNewElements[5] = "input";
    listNewElements[6] = "input";
    listNewElements[7] = "input";

    var listNewElementsID = new Array();
    listNewElementsID[0] = "ProductID";
    listNewElementsID[1] = "OrderContent";
    listNewElementsID[2] = "Ptype";
    listNewElementsID[3] = "SpecsModels";
    listNewElementsID[4] = "Amount";
    listNewElementsID[5] = "Total";
    listNewElementsID[6] = "BusinessType";
    listNewElementsID[7] = "Remark";

    var listCheck = new Array();
    listCheck[0] = "y";
    listCheck[1] = "n";
    listCheck[2] = "n";
    listCheck[3] = "n";
    listCheck[4] = "n";
    listCheck[5] = "n";
    listCheck[6] = "n";
    listCheck[7] = "n";

    var listcontent = new Array();

    var tableGX = new Table(null, null, null, null, null, null, null, null);
    tableGX.addNewRow('myTable', listtypes, listNewElements, listcontent, 'DetailInfo', listNewElementsID, listCheck, "Sample");
    var tbody = document.getElementById("DetailInfo");
    var rowCount = tbody.rows.length;
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
    listtypeNames[2] = "Ptype";
    listtypeNames[3] = "SpecsModels";
    listtypeNames[4] = "Amount";
    listtypeNames[5] = "UnitPrice";
    listtypeNames[6] = "Total";
    listtypeNames[7] = "BusinessType";
    listtypeNames[8] = "Remark";

    var tableGX = new Table(null, null, null, null, null, null, null, null);
    tableGX.removeRow('myTable', 'DetailInfo', listtypeNames);
    HJ();
}

function AddRow1() {

    var listids = new Array();
    listids[0] = "0";
    listids[1] = "1";
    listids[2] = "2";
    listids[3] = "3";
    listids[4] = "4";
    listids[5] = "5";
    listids[6] = "6";
    listids[7] = "7";

    var listtypes = new Array();
    listtypes[0] = "text";
    listtypes[1] = "text";
    listtypes[2] = "text";
    listtypes[3] = "text";
    listtypes[4] = "text";
    listtypes[5] = "text";
    listtypes[6] = "text";
    listtypes[7] = "text";

    var listNewElements = new Array();
    listNewElements[0] = "input";
    listNewElements[1] = "input";
    listNewElements[2] = "input";
    listNewElements[3] = "input";
    listNewElements[4] = "input";
    listNewElements[5] = "input";
    listNewElements[6] = "input";
    listNewElements[7] = "input";

    var listNewElementsID = new Array();
    listNewElementsID[0] = "txtProductID";
    listNewElementsID[1] = "txtOrderContent";
    listNewElementsID[2] = "txtPtype";
    listNewElementsID[3] = "txtSpecsModels";
    listNewElementsID[4] = "txtAmount";
    listNewElementsID[5] = "txtTotal";
    listNewElementsID[6] = "txtBusinessType";
    listNewElementsID[7] = "txtRemark";

    var listCheck = new Array();
    listCheck[0] = "y";
    listCheck[1] = "n";
    listCheck[2] = "n";
    listCheck[3] = "n";
    listCheck[4] = "n";
    listCheck[5] = "n";
    listCheck[6] = "n";
    listCheck[7] = "n";

    var listcontent = new Array();

    var tableGX = new Table(null, null, null, null, null, null, null, null);
    tableGX.addNewRow('myTable1', listtypes, listNewElements, listcontent, 'RevokeInfo', listNewElementsID, listCheck, "RevokeInfo");
    var tbody = document.getElementById("RevokeInfo");
    var rowCount = tbody.rows.length;
}

function DelRow1() {
    var tbody = document.getElementById("RevokeInfo");
    var rowCount = tbody.rows.length;
    if (rowCount == 0) {
        alert("当前没有可删除的行！");
        return;
    }

    var listtypeNames = new Array();
    listtypeNames[0] = "txtProductID";
    listtypeNames[1] = "txtOrderContent";
    listtypeNames[2] = "txtPtype";
    listtypeNames[3] = "txtSpecsModels";
    listtypeNames[4] = "txtAmount";
    listtypeNames[5] = "txtUnitPrice";
    listtypeNames[6] = "txtTotal";
    listtypeNames[7] = "txtBusinessType";
    listtypeNames[8] = "txtRemark";

    var tableGX = new Table(null, null, null, null, null, null, null, null);
    tableGX.removeRow('myTable1', 'RevokeInfo', listtypeNames);
    CHJ();
}


function selRow(curRow) {
    newRowID = curRow.id;
}


function ChangeNum(filed) {
    if (filed == "SampleNum") {
        var totalSampleNum = 0;
        var sampleAmount = 0.00;
        var totalSampleAmount = 0.00;
        var num = document.getElementsByName("Amount").length;
        for (var i = 0; i < num; i++) {
            if (document.getElementsByName("Amount")[i].value != "")
                totalSampleNum += parseInt(document.getElementsByName("Amount")[i].value);

            if (document.getElementsByName("Total")[i].value != "" && document.getElementsByName("Amount")[i].value != "") {
                sampleAmount = parseFloat(document.getElementsByName("Total")[i].value) * parseInt(document.getElementsByName("Amount")[i].value);
                totalSampleAmount += sampleAmount;
            }
        }
        document.getElementById("SampleNum").value = totalSampleNum;
        document.getElementById("SampleAmount").value = totalSampleAmount;
    }
    else if (filed == "RevokeNum") {
        var totalRevokeNum = 0;
        var revokeAmount = 0.00;
        var totalRevokeAmount = 0.00;
        var num = document.getElementsByName("txtAmount").length;
        for (var i = 0; i < num; i++) {
            if (document.getElementsByName("txtAmount")[i].value != "")
                totalRevokeNum += parseInt(document.getElementsByName("txtAmount")[i].value);

            if (document.getElementsByName("txtTotal")[i].value != "" && document.getElementsByName("txtAmount")[i].value != "") {
                revokeAmount = parseFloat(document.getElementsByName("txtTotal")[i].value) * parseInt(document.getElementsByName("txtAmount")[i].value);
                totalRevokeAmount += revokeAmount;
            }
        }
        document.getElementById("RevokeNum").value = totalRevokeNum;
        document.getElementById("RevokeAmount").value = totalRevokeAmount;
    }

}
