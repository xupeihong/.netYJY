$(document).ready(function () {
    //$("#Showlist").hidde();
    if (location.search != "") {
        ID = location.search.split('&')[0].split('=')[1];
        //  Type = location.search.split('&')[1].split('=')[1];
    }
    //   Jq();
    LoadProjectDetail();
    $("#BXtxt").val("");
    document.getElementById('div1').style.display = 'none';
    document.getElementById('div2').style.display = 'none';

    if (ID != "" && ID != null) {
        document.getElementById('div1').style.display = 'block';
        document.getElementById('div2').style.display = 'none';
    }
    else {
        document.getElementById('div2').style.display = 'block';
        document.getElementById('div1').style.display = 'none';
    }
    // document.getElementById('div1').style.display = 'none';
    // document.getElementById('div2').style.display = 'none';
    $("#btnSaveOrder").click(function () {
        isConfirm = confirm("确定要添加订单吗")
        if (isConfirm == false) {
            return false;
        }
        else {
            SaveOrderInfo();
        }
    })
    $("#Total").click(function () {
        HJ();
    });

    $("#btnExit").click(function () {
        window.parent.ClosePop();
    });
    $("#ProjectformInfo").submit(function () {
       
    })
});
var ID;
var curPage = 1;
var OnePageCount = 15;
//var PID;
var RowId = 0;
var isConfirm = false;
var Type;
var newRowID = "";
//加载项目的物品数据
function LoadProjectDetail() {
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    var PID = ID;
    $.ajax({
        url: "getProjectDetailGrid",
        type: "post",
        data: { PID: PID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {


                for (var i = 0; i < json.length; i++) {
                    rowCount = i;
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                    html += '<td ><lable class="labUnit' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                    html += '<td ><input type="text" onblur=XJ(this) id="Amount' + rowCount + '" value="' + json[i].Amount + '" style="width:100px;"/></td>';
                    html += '<td ><input type="text" style="width:100px;"  id="Supplier' + rowCount + '"/> </td>';//onclick=CheckSupplier(this)
                    html += '<td ><input type="text" style="width:100px;"  id="UnitPrice' + rowCount + '"  onblur=XJ(this) /> </td>';
                    //html += '<td  style="width:60px;" ><input type="text" onblur=XJ(this)  id="TaxRate' + rowCount + '"  style="width:30px;" />%</td>';
                    html += '<td ><input type="text"  readonly="readonly" id="Subtotal' + rowCount + '" style="width:100px;"/></td>';
                    html += '<td ><input type="text"  id="UnitCost' + rowCount + '"   onblur=TotalCost(this) style="width:100px;"/></td>';//单位成本';
                    html += '<td ><input type="text" readonly="readonly"  id="TotalCost' + rowCount + '" style="width:100px;"/></td>';//累计成本';
                    html += '<td ><input type="text" style="width:100px;" id="Technology' + rowCount + '"/> </td>';
                    html += '<td ><input type="text"  id="SaleNo' + rowCount + '" style="width:100px;"/></td>';//销售单号';
                    html += '<td ><input type="text"  id="ProjectNo' + rowCount + '" style="width:100px;"/></td>';//工程项目编号
                    html += '<td ><input type="text"  id="JNAME' + rowCount + '" style="width:100px;"/></td>';//工程项目名称';

                    //html += '<td ><input type="text" onclick="WdatePicker()" class="Wdate" style="width:170px;" id="DeliveryTime' + rowCount + '"> </td>';
                    html += '<td  td style="display:none;"><lable class="labYPrice' + rowCount + ' " id="YPrice' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';
                    // html += '<td ><lable class="labRemark' + rowCount + ' " id="Remark' + rowCount + '">' + json[i].Remark + '</lable> </td>';
                    html += '<td style="display:none;"><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '</tr>'
                    CountRows = CountRows + 1;
                    rowCount += 1;
                    $("#DetailInfo").append(html);

                }


            }
        }

    })
}
//添加物品

function CheckDetail() {
    //this.className = "btnTw";
    //$('#btnAddF').attr("class", "btnTh");
    ShowIframe1("选择货品信息", "../SalesManage/ChangeProduct", 850, 450);
}
function addBasicDetail(PID) { //增加货品信息行
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    //判断是否重复添加物品
    var tbody = document.getElementById("DetailInfo");
    var s = PID.split(',');
    for (var j = 0; j < s.length; j++) {
        for (var i = 0; i < tbody.rows.length; i++) {
            var Productid = document.getElementById("ProductID" + i).innerHTML;
            Productid = "'" + Productid + "'";
            if (s[j] == Productid) {
                if (s.length >= 2 && j==0) {
                    Productid =Productid+ ",";
                    PID = PID.replace(Productid, "");
                }
                else if (s.length >2)
                {
                    Productid = "," + Productid;
                    PID = PID.replace(Productid, "");
                }
                else
                    PID = PID.replace(Productid, "");
            }
        }
    }
    if (PID == "") {
        return;
    }
    //
    $.ajax({
        url: "GetBasicDetail",
        type: "post",
        data: { PID: PID },
        dataType: "json",
        ansyc: false,
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {

                //  $("#myTable DetailInfo").html("");
                for (var i = 0; i < json.length; i++) {
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable style="width:90px;" class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable></td>';
                    html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '"  style="width:100px;">' + json[i].ProName + '</lable></td>';
                    html += '<td ><lable  class="labSpec' + rowCount + ' " id="Spec' + rowCount + '" style="width:90px;"  >' + json[i].Spec + '</lable></td>';
                    html += '<td ><lable  class="labUnits' + rowCount + ' " id="Units' + rowCount + '"  style="width:90px;"  >' + json[i].Units + '</lable></td>';
                    html += '<td ><input type="text"  onblur=XJ(this)  id="Amount' + rowCount + '"  style="width:90px;" /></td>';
                    html += '<td ><input type="text"   id="Supplier' + rowCount + '" style="width:90px;" /> </td>';//readonly="readonly"onclick=CheckSupplier(this) 
                    html += '<td ><input type="text" onblur=XJ(this)  id="UnitPrice' + rowCount + '" style="width:90px;" /> </td>';
                    html += '<td ><input type="text"  readonly="readonly" id="Subtotal' + rowCount + '"  style="width:90px;" /> </td>';
                    html += '<td ><input type="text"  id="UnitCost' + rowCount + '" onblur=TotalCost(this) style="width:90px;" /></td>';//单位成本';
                    html += '<td ><input type="text" readonly="readonly"  id="TotalCost' + rowCount + '" style="width:90px;" /></td>';//累计成本';
                    html += '<td ><input type="text"  id="Technology' + rowCount + '" style="width:90px;" /> </td>';
                    html += '<td ><input type="text"  id="SaleNo' + rowCount + '" style="width:90px;" /></td>';//销售单号';
                    html += '<td ><input type="text"  id="ProjectNo' + rowCount + '" style="width:90px;" /></td>';//工程项目编号
                    html += '<td ><input type="text"  id="JNAME' + rowCount + '" style="width:90px;"/></td>';//工程项目名称';
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
//选择供应商
function CheckSupplier(rowid) {
    RowId = rowid.id;
    ShowIframe1("选择货品信息", "../SalesManage/Supplier", 850, 350);
}

function addSupplier(SID) {
    var rownumber = RowId.substr(RowId.length - 1, 1);
    var ProID = document.getElementById("ProductID" + rownumber).innerHTML;
    $.ajax({
        url: "GetSupplier",
        type: "post",
        data: { SID: SID },
        dataType: "json",
        ansyc: false,
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                // for (var i = 0; i < json.length; i++) {
                $("#Supplier" + rownumber).val(json[0].COMNameC);
                //  $("#UnitPrice" + rownumber).val(json[0].price);
                var ProID = document.getElementById("ProductID" + rownumber).innerHTML;
                var SupID = document.getElementById("Supplier" + rownumber).value;
                $.ajax({
                    url: "GetProductPrice",
                    type: "post",
                    data: { ProID: ProID, SupID: SupID },
                    dataType: "json",
                    ansyc: false,
                    success: function (data) {

                        var json = eval(data.datas);
                        if (json.length > 0) {
                            //$("#Supplier" + rownumber).val(json[0].COMNameC);
                            $("#YPrice" + rownumber).val(json[0].price);
                            XJ();
                            HJ();
                        }
                    }
                });

            }
        }
    });
}
//
function selRow(curRow) {
    newRowID = curRow.id;
    $("#DetailInfo tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}
function DeleteRow() {
    var tbodyID = "DetailInfo";
    var rowIndex = -1;
    var typeNames = ["RowNumber", "ProductID", "ProName", "Spec", "Units", "Amount", "Supplier", "UnitPrice", "Subtotal", "UnitCost", "TotalCost", "Technology", "SaleNo", "ProjectNo", "JNAME"];

    //if (newRowID != "")
    //    rowIndex = newRowID.replace(tbodyID, '');
    //if (rowIndex != -1) {

    //    document.getElementById(tbodyID).deleteRow(rowIndex);

    //    if (rowIndex < $("#" + tbodyID + " tr").length) {
    //        for (var i = rowIndex; i < $("#" + tbodyID + " tr").length; i++) {
    //            var tr = document.getElementById(tbodyID + (parseInt(i) + 1));
    //            var tr1 = $("#" + tbodyID + (parseInt(i) + 1));
    //            tr.id = tbodyID + i;
    //            for (var j = 0; j < tr.childNodes.length; j++) {
    //                var html = tr1.html();
    //                for (var k = 0; k < typeNames.length; k++) {

    //                    var olPID = typeNames[k] + (parseInt(i) + 1);
    //                    var newid = typeNames[k] + i;

    //                    var reg = new RegExp(olPID, "g");

    //                    html = html.replace(reg, newid);

    //                }
    //                tr1.html(html);
    //            }
    //            // $("#RowNumber" + i).html(parseInt(i) + 1);
    //            document.getElementById("RowNumber" + i).innerHTML = parseInt(i) + 1;
    //        }
    //    }
    //    if (document.getElementById(tbodyID).rows.length > 0) {
    //        if (rowIndex == document.getElementById(tbodyID).rows.length)
    //            selRow(document.getElementById(tbodyID + (rowIndex - 1)), '');
    //        else
    //            selRow(document.getElementById(tbodyID + rowIndex), '');;
    //    }
    //}



    var rowIndex = -1;
    if (newRowID != "")
        rowIndex = newRowID.replace(tbodyID, '');
    if (rowIndex != -1) {
        document.getElementById(tbodyID).deleteRow(rowIndex);

        if (rowIndex < document.getElementById(tbodyID).rows.length) {
            for (var i = rowIndex; i < document.getElementById(tbodyID).rows.length; i++) {
                var tr = document.getElementById(tbodyID + (parseInt(i) + 1));
                tr.id = tbodyID + i;
                tr.childNodes[0].innerHTML = parseInt(i) + 1;
                for (var j = 1; j < tr.childNodes.length; j++) {
                    var td = tr.childNodes[j];
                    td.childNodes[0].id = typeNames[j] + i;
                    td.childNodes[0].name = typeNames[j] + i;

                }
            }
        }
        if (document.getElementById(tbodyID).rows.length > 0) {

            if (rowIndex == document.getElementById(tbodyID).rows.length)
                selRow(document.getElementById(tbodyID + (rowIndex - 1)), '');
            else
                selRow(document.getElementById(tbodyID + rowIndex), '');
        }
    }
    HJ();
}
//保存订单
function SaveOrderInfo() {
    var PID = ID;
    var OrderUnit = $("#OrderUnit").val();
    var ContractID = $("#ContractID").val();
    var ISF = $("#ISF").val();
    var OrderID = $("#OrderID").val();
    var OrderContactor = $("#OrderContactor").val();
    var OrderTel = $("#OrderTel").val();
    var OrderAddress = $("#OrderAddress").val();
    var UseUnit = $("#UseUnit").val();
    var UseContactor = $("#UseContactor").val();
    var UseTel = $("#UseTel").val();
    var UseAddress = $("#UseAddress").val();
    var Total = $("#Total").val();
    var PayWay = $("#PayWay").val();
    var Guarantee = $("#Guarantee").val();
    var ExpectedReturnDate = $("#ExpectedReturnDate").val();
    var Provider = $("#Provider").val();
    var Demand = $("#Demand").val();
    var ProvidManager = $("#ProvidManager").val();
    var DemandManager = $("#DemandManager").val();
    var Remark = $("#Remark").val();
    var ChannelsFrom = $("#ChannelsFrom").val();
    var DeliveryTime = $("#DeliveryTime").val();
 //   PayWay
    //Total:Total,PayWay:PayWay,Guarantee:Guarantee,ExpectedReturnDate:ExpectedReturnDate,Provider:Provider,Demand:Demand,ProvidManager:ProvidManager,DemandManager:DemandManager,Remark:Remark,ChannelsFrom:ChannelsFrom,


    if (OrderUnit == "" || OrderUnit == null) {
        alert("订货单位不能为空");
        return false;
    }
    var Guarantee = $("#Guarantee").val();
    if (Guarantee == "" || Guarantee == null) {
        alert("保修日期不能为空");
        return false;
    }
    var ExpectedReturnDate = $("#ExpectedReturnDate").val();
    if (ExpectedReturnDate == "" || ExpectedReturnDate == null) {
        alert("预计回款日期不能为空");
        return false;
    }
    var ProvidManager = $("#ProvidManager").val();
    if (ProvidManager == "" || ProvidManager == null || ProvidManager == undefined) {
        alert("供方负责人不能为空");
        return false;
    }


    //订单详细表
    var ProductID = "";
    var OrderContent = "";
    var SpecsModels = "";
    var Supplier = "";
    var Unit = "";
    var Amount = "";
    var OrderAmount = 0;
    var UnitPrice = "";
    var Subtotal = "";//小计
    var OrderTotal = "";
    var UnitCost = "";
    var TotalCost = "";
    var SaleNo = "";
    var ProjectNo = "";
    var JNAME = "";

    var Technology = "";
    //var DeliveryTime = "";
    //var YPrice = "";
    //var TaxRate = "";

    var str = "";
    var tbody = document.getElementById("DetailInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
        var Productid = document.getElementById("ProductID" + i).innerHTML;
        Productid = tbody.rows[i].cells[1].outerText;
        var mainContent = document.getElementById("ProName" + i).innerHTML;
        mainContent = tbody.rows[i].cells[2].outerText;
        var specsModels = document.getElementById("Spec" + i).innerHTML;
        specsModels = tbody.rows[i].cells[3].outerText;
        var unit = document.getElementById("Units" + i).innerHTML;
        unit = tbody.rows[i].cells[4].outerText;
        var salesNum = document.getElementById("Amount" + i).value;
        var supplier = document.getElementById("Supplier" + i).value;
        var uitiprice = document.getElementById("UnitPrice" + i).value;
        var subtotal = document.getElementById("Subtotal" + i).value;
        var unitcost = document.getElementById("UnitCost" + i).value;
        var totalcost = document.getElementById("TotalCost" + i).value;
        var saleno = document.getElementById("SaleNo" + i).value;
        var projetno = document.getElementById("ProjectNo" + i).value;
        var jname = document.getElementById("JNAME" + i).value;
        var technology = document.getElementById("Technology" + i).value;
        //var deliverytime = document.getElementById("DeliveryTime" + i).value;
        //var yprice = document.getElementById("YPrice" + i).innerHTML;
        //var taxrate = document.getElementById("TaxRate" + i).value;

        //ID += parseInt(i + 1);


        if (technology == "") {
            alert("技术参数必须填写！");
            return;
        }

        ProductID += Productid;
        OrderContent += mainContent;
        SpecsModels += specsModels;
        str += "规格型号：" + specsModels;
        Unit += unit;
        Amount += salesNum;
        str += "数量：" + salesNum;
        OrderAmount += parseInt(salesNum);
        Supplier += supplier;
        UnitPrice += uitiprice;
        Subtotal += subtotal;
       // OrderTotal += parseFloat(subtotal);
        UnitCost += unitcost;
        TotalCost += totalcost;
        SaleNo += saleno;
        ProjectNo += projetno;
        JNAME += jname;
        Technology += technology;
        str += "技术参数：" + technology + "\u000d";
        //DeliveryTime += deliverytime;
        //YPrice += yprice;
        //TaxRate += taxrate;
        if (i < tbody.rows.length - 1) {
            //ID += ",";
            ProductID += ",";
            OrderContent += ",";
            SpecsModels += ",";
            Unit += ",";
            Amount += ",";
            Supplier += ",";
            UnitPrice += ",";
            Subtotal += ",";
            Technology += ",";
            UnitCost += ",";
            TotalCost += ",";
            SaleNo += ",";
            ProjectNo += ",";
            JNAME += ",";
            //DeliveryTime += ",";
            //YPrice += ",";
            //TaxRate += ",";
        }
        else {
            // ID += "";
            ProductID += "";
            OrderContent += "";
            SpecsModels += "";
            Unit += "";
            Amount += "";
            Supplier += "";
            UnitPrice += "";
            Subtotal += "";
            Technology += "";
            UnitCost += "";
            TotalCost += "";
            SaleNo += "";
            ProjectNo += "";
            JNAME += "";
            //DeliveryTime += "";
            //YPrice += "";
            //TaxRate += "";
        }
    }
    //"规则型号：" + SpecsModels + "数量：" + Amount + "技术参数：" + Technology
    isConfirm = confirm(str)
    if (isConfirm == false) {
        return false;
    }
    //  alert("规则型号：" + SpecsModels + "数量：" + Amount + "技术参数：" + Technology);
   $.ajax( {
        url: "SaveOrderInfo",
        type: "Post",
        ansyc: false,
        data: {
            ID: ID,OrderID:OrderID, ContractID: ContractID, ISF: ISF,OrderUnit:OrderUnit,OrderContactor:OrderContactor,OrderTel:OrderTel,OrderAddress:OrderAddress,UseUnit:UseUnit,UseContactor:UseContactor,UseTel:UseTel,UseAddress:UseAddress,DeliveryTime:DeliveryTime,Total:Total,PayWay:PayWay,Guarantee:Guarantee,ExpectedReturnDate:ExpectedReturnDate,Provider:Provider,Demand:Demand,ProvidManager:ProvidManager,DemandManager:DemandManager,Remark:Remark,ChannelsFrom:ChannelsFrom,
            ProductID: ProductID, OrderContent: OrderContent, SpecsModels: SpecsModels, Unit: Unit, Amount: Amount, Supplier: Supplier, UnitPrice: UnitPrice, Subtotal: Subtotal, UnitCost: UnitCost, TotalCost: TotalCost, SaleNo: SaleNo, ProjectNo: ProjectNo, JNAME: JNAME, Technology: Technology//, OrderAmount: OrderAmount //OrderTotal: OrderTotal// DeliveryTime: DeliveryTime,YPrice:YPrice,TaxRate:TaxRate,OrderAmount:OrderAmount,OrderTotal:OrderToal
        },
        async: false,
        success: function (data) {
            if (data.success == true) {
                window.parent.frames["iframeRight"].reload();
                alert("生成订单！");

                window.parent.ClosePop();

            }
            else {
                alert("生成订单失败-" + data.Msg);
            }
        }
    })
   // $("#ProjectformInfo").ajaxSubmit(options);
   // return false;
}
function returnConfirm() {
    return isConfirm;
}
//从备案流转
function Jq() {
    var ID = ID;
    $.ajax({
        url: "GetProject",
        type: "post",
        data: { ID: ID },
        dataType: "json",
        ansyc: false,
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    $("#PID").val(json[i].PID);
                    $("#PlanID").val(json[i].PlanID);
                    $("#PlanName").val(json[i].PlanName);
                }
            }
        }
    });
}
//从合同流转

//小计
function XJ(rowrid) {
    RowId = rowrid.id;
    var a = RowId.split('Amount');
    var b = RowId.split('UnitPrice');
    var Total = 0;
    var s = "";
    //s = a[1];
    if (a.length == 2) {
        s = a[1];
    }
    if (b.length == 2)
    {
        s = b[1];
    }
    var tbody = document.getElementById("DetailInfo");
    var Amount = document.getElementById("Amount" + s).value;
    var UnitPrice = document.getElementById("UnitPrice" + s).value;
    var g = /^[1-9]*[1-9][0-9]*$/;
    if (Amount != "" && g.test(Amount) == false) {
        alert("数量输入有误");
        return;
    }
    var reg = /^[0-9]+.?[0-9]*$/;
    if (UnitPrice != "" && reg.test(UnitPrice) == false) {
        alert("价格输入不正确");
        return;
    }
    if (Amount == "" || Amount == null || Amount == "0") {
        alert("请输入数量");
        return;
        //Amount = "0";
    }
    if (UnitPrice == "" || UnitPrice == null || UnitPrice == "0" || parseFloat(UnitPrice) < 0) {
        alert("请输入价格");
        return;
    }
    //  TaxRate = parseFloat(TaxRate / 100);
    //  UnitPrice = parseFloat(UnitPrice) * parseFloat(1 + parseFloat(TaxRate));
    Total = parseFloat(Amount) * parseFloat(UnitPrice);
    Total = Total.toFixed(2);
    $("#Subtotal" + s).val(Total);
    HJ();
     TotalCost(rowrid);
    // }
}
//合计
function HJ() {
    var Total = 0;
    var tbody = document.getElementById("DetailInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
        var Subtotal = document.getElementById("Subtotal" + i).value;
        if (Subtotal == "" || Subtotal == null) {
            Subtotal = "0";
        }
        Total = Total + parseFloat(Subtotal);

        $("#Total").val(Total);
        //  $("#Total").val(convertCurrency(Total));
    }
}

//累计成本
function TotalCost(rowrid) {
    RowId = rowrid.id;
    var a = RowId.split('Amount');
    var b = RowId.split('UnitCost');
    var c = RowId.split('UnitPrice');
    var Total = 0;
    var s = "";
    if (a.length == 2) {
        s = a[1];
    }
    if (b.length ==2)
    {
        s = b[1];
    }
    if (c.length == 2)
    {
        s=c[1];
    }
    var tbody = document.getElementById("DetailInfo");
    //for (var i = 0; i < tbody.rows.length; i++) {
    var Amount = document.getElementById("Amount" + s).value;
    var UnitCost = document.getElementById("UnitCost" + s).value;
    //var TaxRate=document.getElementById("TaxRate" + s).value+"";
    if (Amount == "" || Amount == null) {
        Amount = "0";
    }
    if (UnitCost == "" || UnitCost == null) {
        UnitCost = "0.00";
    }
    //  TaxRate = parseFloat(TaxRate / 100);
    //  UnitPrice = parseFloat(UnitPrice) * parseFloat(1 + parseFloat(TaxRate));
    Total = parseFloat(Amount) * parseFloat(UnitCost);
    Total = Total.toFixed(2);
    $("#TotalCost" + s).val(Total);
    HJ();
}

//金额转换大写
function convertCurrency(currencyDigits) {
    currencyDigits = currencyDigits.toString();
    var MAXIMUM_NUMBER = 99999999999.99;  //最大值
    // 定义转移字符
    var CN_ZERO = "零";
    var CN_ONE = "壹";
    var CN_TWO = "贰";
    var CN_THREE = "叁";
    var CN_FOUR = "肆";
    var CN_FIVE = "伍";
    var CN_SIX = "陆";
    var CN_SEVEN = "柒";
    var CN_EIGHT = "捌";
    var CN_NINE = "玖";
    var CN_TEN = "拾";
    var CN_HUNDRED = "佰";
    var CN_THOUSAND = "仟";
    var CN_TEN_THOUSAND = "万";
    var CN_HUNDRED_MILLION = "亿";
    var CN_DOLLAR = "元";
    var CN_TEN_CENT = "角";
    var CN_CENT = "分";
    var CN_INTEGER = "整";

    // 初始化验证:
    var integral, decimal, outputCharacters, parts;
    var digits, radices, bigRadices, decimals;
    var zeroCount;
    var i, p, d;
    var quotient, modulus;

    // 验证输入字符串是否合法
    //if (currencyDigits.toString() == "") {
    //    alert("还没有输入数字");
    //    $("#Digits").focus();
    //    return;
    //}
    //if (!currencyDigits.match(/[^,.\d]/)) {
    //    alert("请输入有效数字");
    //    $("#Digits").focus();
    //    return;
    //}

    //判断是否输入有效的数字格式
    var reg = /^((\d{1,3}(,\d{3})*(.((\d{3},)*\d{1,3}))?)|(\d+(.\d+)?))$/;
    if (!reg.test(currencyDigits)) {
        alert("请输入有效格式数字");
        $("#Digits").focus();
        return;

    }

    currencyDigits = currencyDigits.replace(/,/g, "");
    currencyDigits = currencyDigits.replace(/^0+/, "");
    //判断输入的数字是否大于定义的数值
    if (Number(currencyDigits) > MAXIMUM_NUMBER) {
        alert("您输入的数字太大了");
        //$("#Digits").focus();
        return;
    }

    parts = currencyDigits.split(".");
    if (parts.length > 1) {
        integral = parts[0];
        decimal = parts[1];
        decimal = decimal.substr(0, 2);
    }
    else {
        integral = parts[0];
        decimal = "";
    }
    // 实例化字符大写人民币汉字对应的数字
    digits = new Array(CN_ZERO, CN_ONE, CN_TWO, CN_THREE, CN_FOUR, CN_FIVE, CN_SIX, CN_SEVEN, CN_EIGHT, CN_NINE);
    radices = new Array("", CN_TEN, CN_HUNDRED, CN_THOUSAND);
    bigRadices = new Array("", CN_TEN_THOUSAND, CN_HUNDRED_MILLION);
    decimals = new Array(CN_TEN_CENT, CN_CENT);

    outputCharacters = "";
    //大于零处理逻辑
    if (Number(integral) > 0) {
        zeroCount = 0;
        for (i = 0; i < integral.length; i++) {
            p = integral.length - i - 1;
            d = integral.substr(i, 1);
            quotient = p / 4;
            modulus = p % 4;
            if (d == "0") {
                zeroCount++;
            }
            else {
                if (zeroCount > 0) {
                    outputCharacters += digits[0];
                }
                zeroCount = 0;
                outputCharacters += digits[Number(d)] + radices[modulus];
            }
            if (modulus == 0 && zeroCount < 4) {
                outputCharacters += bigRadices[quotient];
            }
        }
        outputCharacters += CN_DOLLAR;
    }
    // 包含小数部分处理逻辑
    if (decimal != "") {
        for (i = 0; i < decimal.length; i++) {
            d = decimal.substr(i, 1);
            if (d != "0") {
                outputCharacters += digits[Number(d)] + decimals[i];
            }
        }
    }
    //确认并返回最终的输出字符串
    if (outputCharacters == "") {
        outputCharacters = CN_ZERO + CN_DOLLAR;
    }
    if (decimal == "") {
        outputCharacters += CN_INTEGER;
    }
    return outputCharacters;
    //获取人民币大写
    //$("#getCapital").val(outputCharacters);
}



