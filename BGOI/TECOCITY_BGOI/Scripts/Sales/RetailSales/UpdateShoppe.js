var isConfirm = false;
$(document).ready(function () {
    InitDetailInfo();

    $("#btnSave").click(function () {
        isConfirm = confirm("确定修改专柜申请吗？")
        if (isConfirm == false) {
            return false;
        }
        else
            submitInfo();
    });

    $("#btnCancel").click(function () {
        window.parent.ClosePop();
    });
});

function returnConfirm() {
    return isConfirm;
}

function submitInfo() {
    //var tbody = document.getElementById("DetailInfo");
    //var rowCount = tbody.rows.length;
    var SIID=$("#SIID").val();
    var  ApplyTime=$("#ApplyTime").val();
    var Malls=$("#Malls").val();
    var Customer=$("#Customer").val();
    var MallType=$("#MallType").val();
    var Phone=$("#Phone").val();
    var Address=$("#Address").val();
    var ProductsOneName=$("#ProductsOneName").val();
    var ShoppeSize=$("#ShoppeSize").val();
    var SampleOneNum=$("#SampleOneNum").val();
    var ProductsTwoName=$("#ProductsTwoName").val();
    var ShoppeTwoSize=$("#ShoppeTwoSize").val();
    var SampleNum=$("#SampleNum").val();
    var ShoppePosition=$("#ShoppePosition").val();
    var MonthSalesNum=$("#MonthSalesNum").val();
    var SalesAmount=$("#SalesAmount").val();
    var Cookers=$("#Cookers").val();
    var Turbine=$("#Turbine").val();
    var GasHeater=$("#GasHeater").val();
    var GasBoiler=$("#GasBoiler").val();
    var Amount=$("#Amount").val();
    var ApplyReason=$("#ApplyReason").val();
    var MakeType=$("#MakeType").val();
    var Applyer=$("#Applyer").val();
    var UseYear=$("#UseYear").val();
    var EndYear=$("#EndYear").val();
    var Budget=$("#Budget").val();
    var Explain=$("#Explain").val();

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
        var productid = document.getElementById("ProductID" + i).value;
        var mainContent = document.getElementById("OrderContent" + i).value;
        var type = document.getElementById("GoodsType" + i).value;
        var specsModels = document.getElementById("SpecsModels" + i).value;
        var salesNum = document.getElementById("DAmount" + i).value;
        var unitprice = document.getElementById("UnitPrice" + i).value;
        var discounts = document.getElementById("Discounts" + i).value;
        var subtotal = document.getElementById("Total" + i).value;
        var remark = document.getElementById("txtRemark" + i).value;
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
   $.ajax( {
        url: "SaveApplyShoppe",
        type: "Post",
        ansyc: false,
        data: {SIID:SIID,ApplyTime:ApplyTime,Malls:Malls,Customer:Customer,MallType:MallType,Phone:Phone,Address:Address,ProductsOneName:ProductsOneName,ShoppeSize:ShoppeSize,SampleOneNum:SampleOneNum,ProductsTwoName:ProductsTwoName,ShoppeTwoSize:ShoppeTwoSize,SampleNum:SampleNum,ShoppePosition:ShoppePosition,MonthSalesNum:MonthSalesNum,SalesAmount:SalesAmount,Cookers:Cookers,Turbine:Turbine,GasHeater:GasHeater,GasBoiler:GasBoiler,Amount:Amount,ApplyReason:ApplyReason,MakeType:MakeType,Applyer:Applyer,UseYear:UseYear,EndYear:EndYear,Budget:Budget,Explain:Explain,
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
    })
  //  $("#form1").ajaxSubmit(options);
   // return false;







    //var options = {
    //    url: "UpdateApplyShoppe",
    //    data: { TaskLength: rowCount },
    //    type: 'POST',
    //    async: false,
    //    success: function (data) {
    //        if (data.success == true) {
    //            window.parent.frames["iframeRight"].reload();
    //            alert("提交完成！");
    //            window.parent.ClosePop();
    //        }
    //        else {
    //            alert(data.Msg);
    //        }
    //    }
    //};
    //$("#form1").ajaxSubmit(options);
    //return false;
}

function InitDetailInfo() {
    var SIID = $("#SIID").val();
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
    listNewElementsID[4] = "DAmount";
    listNewElementsID[5] = "UnitPrice";
    listNewElementsID[6] = "Discounts";
    listNewElementsID[7] = "Total";
    listNewElementsID[8] = "txtRemark";

    var listcontent = new Array();

    $.post("GetShoppeDetail?SIID=" + SIID, function (data) {
        if (data != null) {
            var jsonTask = JSON.parse(data);
            var tableDetail = new Table(jsonTask, 'oddRow', 'evenRow', 'selRow', listids, listtypes, listNewElements, listcontent, listNewElementsID);
            tableDetail.LoadTableTBody('DetailInfo');
            var tbody = document.getElementById("DetailInfo");
            var rowCount = tbody.rows.length;
            for (var i = 0; i < length; i++) {
                $("#Amount" + i).attr("onblur", "XJ(this)");
                $("#UnitPrice" + i).attr("onblur", "XJ(this)");
                $("#Total" + i).attr("readonly", true);
            }
            //    $("#BelongCom" + (rowCount - 1)).attr("onchange", "tyoncharge(this)");
            
        }
    });
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
    tableGX.addNewRow('myTable', listtypes, listNewElements, listcontent, 'DetailInfo', listNewElementsID, listCheck,"Shoppe");
    var tbody = document.getElementById("DetailInfo");
    var rowCount = tbody.rows.length;
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
                    html += '<td ><input type="text" class="labProductID' + rowCount + ' "  style="width:60px;" id="ProductID' + rowCount + '" value="' + json[i].ProductID + '"/> </td>';
                    html += '<td ><input type="text" class="labProName' + rowCount + ' "  style="width:60px;" id="OrderContent' + rowCount + '" value="' + json[i].ProName + '"/> </td>';

                    html += '<td ><input type="text"  style="width:60px;" class="labSpec' + rowCount + ' " id="SpecsModels' + rowCount + '" value="' + json[i].Spec + '" /></td>';
                    html += '<td ><input type="text"  style="width:60px;" class="labGoodsType' + rowCount + ' " id="GoodsType' + rowCount + '" /></td>';
                    html += '<td ><input type="text"   style="width:60px;"  class="labAmount' + rowCount + ' " onblur=XJ(this)  id="DAmount' + rowCount + '" /> </td>';
                    html += '<td ><input type="text" onblur=XJ(this)   id="UnitPrice' + rowCount + '" style="width:60px;" value="' + json[i].UnitPrice + '"/> </td>';
                    html += '<td><input type="text"  style="width:60px;" onblur=XJ(this) id="Discounts' + rowCount + '" /></td>';
                    html += '<td ><input type="text"  style="width:60px;"   id="Total' + rowCount + '" /> </td>';
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


//计算金额小计
function XJ(select) {
    var rowid = select.id;
    var Total = 0;
    var s = rowid.substr(rowid.length - 1, rowid.length);
    var tbody = document.getElementById("DetailInfo");
    //  for (var i = 0; i < tbody.rows.length; i++) {
    var Amount = document.getElementById("DAmount" + s).value;
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
        var Subtotal = document.getElementById("UnitPrice" + i).value;
        var SubAmount = document.getElementById("DAmount" + i).value;
        if (Subtotal == "" || Subtotal == null) {
            Subtotal = "0";
        }
        if (SubAmount == "" || SubAmount == null) {
            SubAmount = "0";
        }

        Total = Total + parseFloat(Subtotal * SubAmount);
        // Amount = Amount + parseInt(SubAmount);

    }
    $("#Amount").val(Total);
    // $("#SampleNum").val(Amount);
}
