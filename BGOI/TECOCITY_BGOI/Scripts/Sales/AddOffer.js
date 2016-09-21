var curPage = 1;
var OnePageCount = 15;
var m = 0;
var ID = 0;
var RowId = 0;
var isConfirm = false;
var newRowID = "";
$(document).ready(function () {
    $("#OfferTime").val("");
    if (location.search != "") {
        ID = location.search.split('&')[0].split('=')[1];
    }
    LoadProjectDetail();
    loadtime();
   // ShowPoject();
    //$("#btnSaveOffer").click(function () {
    //    isConfirm = confirm("确定要报价吗")
    //    if (isConfirm == false) {
    //        return false;
    //    }
    //    else
    //        SaveOffer();
    //});

    $("#btnSaveOffer").click(function () {
        isConfirm = confirm("确定要报价吗")
        if (isConfirm == false) {
            return false;
        }
        else {
            //var extend = document.form1.uploadfy.value.substring(form1.file.value.lastindexof(".") + 1);
            //if (extend == "") {
            //    alert("tt");
            //}
            $("#uploadify").uploadify("upload", '*');
            SaveOffer();
            //if (m == 0)
            //{
            //    // SaveOffer();
            //    alert("1223");
            //}

            //var m = document.getElementById("uploadify").value
            //if (m == "")
            //{
            //    alert("m");
            //}
            //var fileFlag = false;
            //$("input[name='uploadfy']").each(function () {
            //    if ($(this).val() != "") {
            //        fileflag = true;
            //        return false;
            //    }
            //});
            //if (fileFlag) {
            //    alert("已有选择好文件的");
            //}
            //var r=jQuery("input[type='file']").val()
            //var ff = document.getElementsByName("uploadfy");
            //var t = ff.length;
            //var s=$("#uploadify").val();
            //if (s == "") {
            //    alert("aa");
            //   // SaveOffer();
            //}
            //else {
            //    alert("bb");
            //   // $("#uploadify").uploadify("upload", '*');
            //}
        }
    })

    $("#btnExit").click(function () {
        window.parent.ClosePop();
    })

    $("#HJ").click(function () {
        HJ();
    });
    $("#upLoad").click(function () {
        var FileName = $("#UploadFile").val();
        if (FileName.replace(/(\s*$)/g, '') == "") {
            alert("请选择要上传的文件");
            return;
        }
        var one = confirm("确定上传文件吗");
        if (one == false)
            return;
        else {
            var filetype = (/\.[^\.]+$/.exec(FileName)).toString();
            if (filetype.toLowerCase().indexOf('jpg') < 0 && filetype.toLowerCase().indexOf('pdf') < 0) {
                alert("不支持该类型文件的上传，请上传jpg,pdf格式文件");
                return;
            }
            document.forms[0].submit();
            // window.parent.ClosePop();
        }
    })
    $("#ProjectformInfo").submit(function () {
       
    });




    $("#uploadify").uploadify({
        uploader: 'InsertBiddingNew',           // 服务器端处理地址
        swf: '../Scripts/uploadify/uploadify.swf',    // 上传使用的 Flash

        width: 90,                          // 按钮的宽度
        height: 23,                         // 按钮的高度
        buttonText: "选择上传文件",                 // 按钮上的文字
        buttonCursor: 'hand',                // 按钮的鼠标图标

        fileObjName: 'Filedata',            // 上传参数名称
        formData: { 'RID': $("#BJID").val() },// 'Types': $("#StrType").val(), 'Comments': $("#StrComments").val()
        // 两个配套使用
        fileTypeExts: "*.xls;*.xlsx;*.jpg;*.png;*.doc;*.docx;*.pdf;*.txt;*.JPG;*.PNG,*.zip;*.rar;", // 扩展名
        fileTypeDesc: "请选择 xls xlsx jpg png doc docx pdf txt JPG PNG zip rar 文件", // 文件说明

        auto: false,                // 选择之后，自动开始上传
        multi: true,               // 是否支持同时上传多个文件

        onUploadStart: function (file) {
          //  debugger;
            if (m == -1) {
               //debugger;
                $("#uploadify").uploadify("stop");
            }
          //  alert(m);
           // m++;
            //var element = {};
            //element.RID = $("#Hidden").val();
            //element.Types = $("#StrType").val();
            //element.Comments = $("#StrComments").val();
            //$("#uploadify").uploadify('settings', 'formData', element);
        },
        onUploadComplete: function (fileObj) {
            //window.parent.frames["iframeRight"].reload();
            //setTimeout('parent.ClosePop()', 10);
        },
        onQueueComplete: function (queueData) {
          //  SaveOffer();
            //上传队列全部完成后执行的回调函数  
            window.parent.frames["iframeRight"].reload();
            setTimeout('parent.ClosePop()', 10);
        }
    });
});


function loadtime()
{
    var date = new Date();
    var seperator1 = "-";
    var seperator2 = ":";
    var month = date.getMonth() + 1;
    var strDate = date.getDate();
    if (month >= 1 && month <= 9) {
        month = "0" + month;
    }
    if (strDate >= 0 && strDate <= 9) {
        strDate = "0" + strDate;
    }
    var currentdate = date.getFullYear() + seperator1 + month + seperator1 + strDate
            + " " + date.getHours() + seperator2 + date.getMinutes()
            + seperator2 + date.getSeconds();
    $("#OfferTime").val(currentdate);
  //  return currentdate;
}

//加载备案项目详细
function ShowPoject()
{
    ShowIframe1("备案项目信息", "../SalesManage/ShowProject?PID="+ID, 850, 450);
}

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
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick=selRow(this)>'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + (i + 1) + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                    html += '<td ><lable class="labUnit' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                    html += '<td ><input type="text" onblur=XJ(this) id="Amount' + rowCount + '" value="' + json[i].Amount + '" style="width:30px;"/></td>';
                    html += '<td ><input type="text" id="Supplier' + rowCount + '" /> </td>';//onclick=CheckSupplier(this) 
                    html += '<td ><input type="text" onblur=XJ(this)  id="UnitPrice' + rowCount + '" /> </td>';
                    html += '<td ><input type="text" readonly="readonly"   id="txtTotal' + rowCount + '" style="width:60px;"/></td>';
                    html += '<td ><lable class="labRemark' + rowCount + ' " id="Remark' + rowCount + '">' + json[i].Remark + '</lable> </td>';
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
//添加物品数据
function CheckDetail() {
    ShowIframe1("选择货品信息", "../SalesManage/ChangeProduct", 850, 450);
}
function addBasicDetail(PID) { //增加货品信息行
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    //var strPID = $("#ProductID").val();
    //$("#ProductID").val(strPID + "," + ProductID);
    var tbody = document.getElementById("DetailInfo");
    var s = PID.split(',');
    for (var j = 0; j < s.length; j++) {
        for (var i = 0; i < tbody.rows.length; i++) {
            var Productid = document.getElementById("ProductID" + i).innerHTML;
            Productid = "'" + Productid + "'";
            if (s[j] == Productid) {
                if (s.length >= 2 && j == 0) {
                    Productid = Productid + ",";
                    PID = PID.replace(Productid, "");
                }
                else if (s.length > 2) {
                    Productid = "," + Productid;
                    PID = PID.replace(Productid, "");
                }
                else
                    PID = PID.replace(Productid, "");
            }
            
        }
    }
    if (PID == "")
    {
        return;
    }
    $.ajax({
        url: "GetBasicDetail",
        type: "post",
        data: { PID: PID },
        dataType: "json",
        ansyc: false,
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick=selRow(this)>'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                    html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                    html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                    html += '<td ><lable class="labUnits' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Units + '</lable> </td>';
                    html += '<td ><input type="text"  onblur=XJ(this) id="Amount' + rowCount + '" style="width:30px;"/></td>';
                    //html += '<td ><lable class="labUnitPrice' + rowCount + ' " id="UnitPrice' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';
                    //html += '<td ><lable class="labManufacturer' + rowCount + ' " readonly="readonly" id="Manufacturer' + rowCount + '">' + json[i].Manufacturer + '</lable> </td>';
                    html += '<td ><input type="text"  id="Supplier' + rowCount + '" /> </td>';//onclick=CheckSupplier(this)
                    html += '<td ><input type="text" onblur=XJ(this) id="UnitPrice' + rowCount + '" value="' + json[i].UnitPrice + '"  /> </td>';
                    html += '<td ><input type="text" id="txtTotal' + rowCount + '" style="width:60px;" readonly="readonly" /></td>';
                    html += '<td ><lable class="labRemark' + rowCount + ' " id="Remark' + rowCount + '">' + json[i].Remark + '</lable> </td>';
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

function selRow(curRow) {
    newRowID = curRow.id;
    RowId = curRow.id;
    $("#DetailInfo tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}
function DeleteRow() {
    if (newRowID == "") {
        alert("请选择删除的数据信息");
        return;
    }
    var tbodyID = "DetailInfo";
    var rowIndex = -1;
    var typeNames = ["RowNumber", "ProductID", "ProName", "Spec", "Units", "Amount", "Supplier", "UnitPrice", "txtTotal", "Remark"];
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
}
//选择供应商
function CheckSupplier() {
    // RowId = rowid.id;
    ShowIframe1("选择供应商", "../SalesManage/Supplier", 800, 350);
    //获取单价
    //RowId = rowid.id;


}

function addSupplier(SID) {
    var rownumber = RowId.substr(RowId.length - 1, 1);
    var ProID = document.getElementById("ProductID" + rownumber).innerHTML;
    $.ajax({
        url: "GetSupplier",
        type: "post",
        data: { SID: SID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {
                $("#Supplier" + rownumber).val(json[0].COMNameC);
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
                            $("#UnitPrice" + rownumber).val(json[0].price);
                            XJ();
                            HJ();
                        }
                    }
                });
                //   $("#UnitPrice" + rownumber).val(json[0].price);
                //  XJ();
            }
        }
    });
    //var s = RowId.substr(RowId.length - 1, RowId.length);
    //var ProID = document.getElementById("ProductID" + s).innerHTML;
    //var SupID = document.getElementById("Supplier" + s).value;
    //$.ajax({
    //    url: "GetProductPrice",
    //    type: "post",
    //    data: { ProID: ProID, SupID: SupID },
    //    dataType: "json",
    //    ansyc: false,
    //    success: function (data) {

    //        var json = eval(data.datas);
    //        if (json.length > 0) {
    //            for (var i = 0; i < json.length; i++) {
    //                $("#UnitPrice" + s).val(json[i].price);
    //            }
    //        }
    //    }
    //});
    //XJ();
}

function SaveOffer() {
    //
   
    //var one = confirm("确定上传文件吗");
    //if (one == false)
    //    return;
    //else {
    //    var filetype = (/\.[^\.]+$/.exec(FileName)).toString();
    //    if (filetype.toLowerCase().indexOf('jpg') < 0 && filetype.toLowerCase().indexOf('pdf') < 0 && filetype.toLowerCase().indexOf('doc') < 0 && filetype.toLowerCase().indexOf('jpg') < 0 && filetype.toLowerCase().indexOf('docx') < 0) {
    //        alert("不支持该类型文件的上传，请上传jpg,doc,docx,pdf格式文件");
    //        return;
    //    }
    //}



    //报价主表
    var PID = $("#PID").val();
    var BJID = $("#BJID").val();
    var ISF = $("#ISF").val();
    var OfferTitle = $("#OfferTitle").val();
    if (OfferTitle == "" || OfferTitle == null) {
        m = -1;
        alert("报价标题不能为空");
        return;
    }
    var OfferTime = $("#OfferTime").val();
    if (OfferTime == "" || OfferTime == null) {
        alert("报价时间不能为空");
        m = -1;
        return;
    }
    var Customer = $("#Customer").val();
    var CustomerTel = $("#CustomerTel").val();
    var Total = $("#Total").val();
    if (Total == "0" || Total == null) {
        alert("报价单价不正确");
        m = -1;
        return;
    }
    //var Description = $("#Description").val();
    var OfferContacts = $("#OfferContacts").val();
    if (OfferContacts == "" || OfferContacts == null) {
        alert("报价人不能为空");
        m = -1;
        return;
    }
    var FKYD = $("#FKYD").val();
    var Description = $("#Description").val();

    //报价详细表
    var ProductID = "";
    var OrderContent = "";
    var SpecsModels = "";
    var Supplier = "";
    var Unit = "";
    var Amount = "";
    var UnitPrice = "";
    var Remark = "";
    var txtTotal = "";
    var tbody = document.getElementById("DetailInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
        var Productid = document.getElementById("ProductID" + i).innerHTML;
        var mainContent = document.getElementById("ProName" + i).innerHTML;
        var specsModels = document.getElementById("Spec" + i).innerHTML;
        var unit = document.getElementById("Units" + i).innerHTML;
        var salesNum = document.getElementById("Amount" + i).value;
        var supplier = document.getElementById("Supplier" + i).value;
        var uitiprice = document.getElementById("UnitPrice" + i).value;
        var txttotal = document.getElementById("txtTotal" + i).value;
        var remark = document.getElementById("Remark" + i).innerHTML;

        ID += parseInt(i + 1);
        ProductID += Productid;
        OrderContent += mainContent;
        SpecsModels += specsModels;
        Unit += unit;
        Amount += salesNum;
        Supplier += supplier;
        UnitPrice += uitiprice;
        if (uitiprice == "") {
            alert("单价不能为空");
            m = -1;
            return;
        }

        Remark += remark;
        txtTotal += txttotal;
        if (i < tbody.rows.length - 1) {
            ID += ",";
            ProductID += ",";
            OrderContent += ",";
            SpecsModels += ",";
            Unit += ",";
            Amount += ",";
            Supplier += ",";
            UnitPrice += ",";
            Remark += ",";
            txtTotal += ",";
        }
        else {
            ID += "";
            ProductID += "";
            OrderContent += "";
            SpecsModels += "";
            Unit += "";
            Amount += "";
            Supplier += "";
            UnitPrice += "";
            Remark += "";
            txtTotal += "";
        }
    }
    if (Amount.length <= 0) {
        alert("");
        return;
    }

    //$.ajax({
    //    url: "InsertForm",
    //    type: "post",
    //    contentType: false,
    //    processData: false,
    //    data: filesm,
    //    ansyc:false,
    //    success: function (data) {
    //        if (data.success == true) {
    //            window.parent.frames["iframeRight"].reload();
    //          //  alert("报价完成！");
    //            window.parent.ClosePop();
    //        }
    //        else {
    //            alert("报价失败");
    //        }
    //    }
    //});
    $.ajax({
        url: "SaveOffer",
        type: "Post",
        data: {
            PID: PID, BJID: BJID, ISF: ISF, OfferTitle: OfferTitle, OfferTime: OfferTime,FKYD:FKYD,Total:Total,Description:Description,OfferContacts:OfferContacts,Customer:Customer,CustomerTel:CustomerTel,
            ID: ID, ProductID: ProductID, OrderContent: OrderContent, SpecsModels: SpecsModels, Unit: Unit, Amount: Amount, Supplier: Supplier, UnitPrice: UnitPrice, Remark: Remark, txtTotal: txtTotal
        },
       // async: false,
        success: function (data) {
            if (data.success == true) {
               
                window.parent.frames["iframeRight"].reload();
                alert("报价完成！");
                window.parent.ClosePop();
            }
            else {
                alert("报价失败");
            }
        }
    })
    document.forms[0].submit();
   // $("#ProjectformInfo").ajaxSubmit(options);
   // return false;
       
}
function returnConfirm() {
    return isConfirm;
}


function XJ(rowid) {
    RowId = rowid.id;
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
    // var s = RowId.substr(RowId.length - 1, RowId.length);
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
    $("#txtTotal" + s).val(Total);
    //}
    HJ();
}
function HJ() {
    var Total = 0;
    var tbody = document.getElementById("DetailInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
        var Subtotal = document.getElementById("txtTotal" + i).value;
        if (Subtotal == "" || Subtotal == null) {
            Subtotal = "0";
        }
        Total = Total + parseFloat(Subtotal);

        $("#Total").val(Total);
    }
}

function GetPrice(rowid) {
    RowId = rowid.id;
    var s = RowId.substr(RowId.length - 1, RowId.length);
    var ProID = document.getElementById("ProductID" + s).innerHTML;
    var SupID = document.getElementById("Supplier" + s).value;
    $.ajax({
        url: "GetProductPrice",
        type: "post",
        data: { ProID: ProID, SupID: SupID },
        dataType: "json",
        ansyc: false,
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    $("#UnitPrice" + RowId).val(json[i].Price);
                }
            }
        }
    });
}