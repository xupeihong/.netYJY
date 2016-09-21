var isConfirm = false;
$(document).ready(function () {
    //InitDetailInfo();
    LoadBasicDetail();
    var provider = $("#hdProvider").val();
    var num = $("#ProvidManager option").length;
    for (var i = 0; i < num; i++) {
        if ($("#ProvidManager").get(0).options[i].text == provider) {
            $("#ProvidManager").get(0).options[i].selected = true;
        }
    }
    //$("#btnSave").click(function () {
    //    isConfirm = confirm("确定修改销售记录吗？")
    //    if (isConfirm == false) {
    //        return false;
    //    }
    //    else
    //        submitInfo();
    //});
    $("#btnSave").click(function () {
        isConfirm = confirm("确定创建销售记录吗？")
        if (isConfirm == false) {
            return false;
        }
        else {

            var OrderID = $("#OrderID").val();
            var ContractDate = $("#ContractDate").val();
            var SupplyTime = $("#SupplyTime").val();
            var OrderUnit = $("#OrderUnit").val();
            var OrderContactor = $("#OrderContactor").val();
            var OrderTel = $("#OrderTel").val();
            var UseAddress = $("#UseAddress").val();
            var Remark = $("#Remark").val();
            var OrderTotal = $("#OrderTotal").val();
            var ProvidManager = $("#ProvidManager").val();
            var IsHK = $("input[name='IsHK']:checked").val();
            //上样产品
            var ISFinish = $("input[name='ISFinish']:checked").val();
            var ISCollection = $("input[name='ISCollection']:checked").val();
            // var ISFinish = $("#ISFinish").val();
            var HKRemark = $("#HKRemark").val();
            if (ISFinish != "0" && HKRemark == "") {
                alert("安装未完成请填写备注");
                return;
            }
          //  var ISFinish = $("input[name='ISFinish']:checked").val();
          ////  var ISHK = $("#ISHK").val();
          //  var ISHK = $("input[name='IsHK']:checked").val();
            //上样产品
            var ProductID = "";
            var OrderContent = "";
            var SpecsModels = "";
            var Amount = "";
            var UnitPrice = "";
            var TaxRate = "";
            var DTotalPrice = "";//小计
            var txtRemark = "";
            var BelongCom = "";
            var Channels = "";
            var tbody = document.getElementById("DetailInfo");
            var rowCount = tbody.rows.length;
            for (var i = 0; i < tbody.rows.length; i++) {
                var productid = document.getElementById("ProductID" + i).innerHTML;
                var mainContent = document.getElementById("OrderContent" + i).innerHTML;
                var specsModels = document.getElementById("Spec" + i).innerHTML;
                var salesNum = document.getElementById("Amount" + i).value;
                var uitiprice = document.getElementById("UnitPrice" + i).value;
                var taxrate = document.getElementById("Discounts" + i).value;
                var subtotal = document.getElementById("DTotalPrice" + i).value;
                var remark = document.getElementById("txtRemark" + i).value;
                var belongCom = document.getElementById("BelongCom" + i).value;
                var channels = document.getElementById("Channels" + i).value;
                ProductID += productid;
                OrderContent += mainContent;
                SpecsModels += specsModels;
                Amount += salesNum;
                TaxRate += taxrate;
                UnitPrice += uitiprice;
                DTotalPrice += subtotal;
                txtRemark += remark;
                BelongCom += belongCom;
                Channels += channels;

                if (i < tbody.rows.length - 1) {
                    //ID += ",";
                    ProductID += ",";
                    OrderContent += ",";
                    SpecsModels += ",";
                    Amount += ",";
                    UnitPrice += ",";
                    TaxRate += ",";
                    DTotalPrice += ",";
                    txtRemark += ",";
                    BelongCom += ",";
                    Channels += ",";
                }
                else {
                    // ID += "";
                    ProductID += "";
                    OrderContent += "";
                    SpecsModels += "";
                    Amount += "";
                    UnitPrice += "";
                    TaxRate += "";
                    DTotalPrice += "";
                    txtRemark += "";
                    BelongCom += "";
                    Channels += "";
                }
            }
            var options = {
                url: "UpdateRecord",
                type: "Post",
                ansyc: false,
                data: {
                    TaskLength: rowCount, ProductID: ProductID,
                    OrderContent: OrderContent, SpecsModels: SpecsModels, Amount: Amount, UnitPrice: UnitPrice,TaxRate:TaxRate, DTotalPrice: DTotalPrice, txtRemark: txtRemark, BelongCom: BelongCom, Channels: Channels
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
        //
    });
});
function returnConfirm() {
    return isConfirm;
}
function submitInfo() {
    var tbody = document.getElementById("DetailInfo");
    var rowCount = tbody.rows.length;
    var options = {
        url: "UpdateRecord",
        data: { TaskLength: rowCount },
        type: 'POST',
        async: false,
        success: function (data) {
            if (data.success == true) {
                window.parent.frames["iframeRight"].reload();
                alert("修改完成！");
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

function InitDetailInfo() {
    var orderID = $("#OrderID").val();
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
    listtypes[6] = "select";
    listtypes[7] = "select";

    var listNewElements = new Array();
    listNewElements[0] = "input";
    listNewElements[1] = "input";
    listNewElements[2] = "input";
    listNewElements[3] = "input";
    listNewElements[4] = "input";
    listNewElements[5] = "input";
    listNewElements[6] = "select";
    listNewElements[7] = "select";

    var listNewElementsID = new Array();
    listNewElementsID[0] = "OrderContent";
    listNewElementsID[1] = "Spec";
    listNewElementsID[2] = "Amount";
    listNewElementsID[3] = "UnitPrice";
    listNewElementsID[4] = "DTotalPrice";
    listNewElementsID[5] = "txtRemark";
    listNewElementsID[6] = "BelongCom";
    listNewElementsID[7] = "Channels";

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
    //$.post("GetConfigRetail?ConfigType=Com00&ChidGrade=0", function (data) {
    //    var objInfo1 = JSON.parse(data);
    //    listcontent[6] = objInfo1;
    //    $.post("GetConfigRetail?ConfigType=Com01&ChidGrade=1", function (data) {
    //        var objInfo2 = JSON.parse(data);
    //        listcontent[7] = objInfo2;
    //        $.post("GetOrderDetail?OrderID=" + orderID, function (data) {
    //            if (data != null) {
    //                var jsonTask = JSON.parse(data);
    //                var tableDetail = new Table(jsonTask, 'oddRow', 'evenRow', 'selRow', listids, listtypes, listNewElements, listcontent, listNewElementsID, listCheck, "Order");
    //                //var tableDetail = new Table(jsonTask, 'oddRow', 'evenRow', 'selRow', listids, listtypes, listNewElements, listcontent, listNewElementsID);
    //                tableDetail.LoadTableTBody('DetailInfo');
    //                var tbody = document.getElementById("DetailInfo");
    //                var rowCount = tbody.rows.length;
    //                $("#BelongCom" + (rowCount - 1)).attr("onchange", "tyoncharge(this)");
    //                $("#Amount" + (rowCount - 1)).attr("onblur", "GetTotal(this)");
    //                $("#UnitPrice" + (rowCount - 1)).attr("onblur", "GetTotal(this)");
                   
    //            }
    //        });
    //    });
    //});

    $.post("GetOrderDetail?OrderID=" + orderID, function (data) {
        if (data != null) {
            var jsonTask = JSON.parse(data);
            var json = eval(data.datas);
            if (jsonTask.length > 0) {
                for (var i = 0; i < jsonTask.length; i++) {
                    $.post("GetConfigRetail?ConfigType=" + jsonTask[i].BelongCom + "&ChidGrade=0", function (data) {
                        var objInfo1 = JSON.parse(data);
                        listcontent[6] = objInfo1;

                    });
                    $.post("GetConfigRetail?ConfigType=" + jsonTask[i].Channels + "&ChidGrade=1", function (data) {
                        var objInfo2 = JSON.parse(data);
                        listcontent[7] = objInfo2;

                    });

                    var tableDetail = new Table(jsonTask, 'oddRow', 'evenRow', 'selRow', listids, listtypes, listNewElements, listcontent, listNewElementsID, listCheck, "Order");
                    //var tableDetail = new Table(jsonTask, 'oddRow', 'evenRow', 'selRow', listids, listtypes, listNewElements, listcontent, listNewElementsID);
                    tableDetail.LoadTableTBody('DetailInfo');
                    var tbody = document.getElementById("DetailInfo");
                    var rowCount = tbody.rows.length;
                    $("#BelongCom" + (rowCount - 1)).attr("onchange", "tyoncharge(this)");
                    $("#Amount" + (rowCount - 1)).attr("onblur", "GetTotal(this)");
                    $("#UnitPrice" + (rowCount - 1)).attr("onblur", "GetTotal(this)");
               
                }

               

            }
           
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

    var listtypes = new Array();
    listtypes[0] = "text";
    listtypes[1] = "text";
    listtypes[2] = "text";
    listtypes[3] = "text";
    listtypes[4] = "text";
    listtypes[5] = "text";
    listtypes[6] = "select";
    listtypes[7] = "select";

    var listNewElements = new Array();
    listNewElements[0] = "input";
    listNewElements[1] = "input";
    listNewElements[2] = "input";
    listNewElements[3] = "input";
    listNewElements[4] = "input";
    listNewElements[5] = "input";
    listNewElements[6] = "select";
    listNewElements[7] = "select";

    var listNewElementsID = new Array();
    listNewElementsID[0] = "OrderContent";
    listNewElementsID[1] = "SpecsModels";
    listNewElementsID[2] = "OrderNum";
    listNewElementsID[3] = "UnitPrice";
    listNewElementsID[4] = "DTotalPrice";
    listNewElementsID[5] = "txtRemark";
    listNewElementsID[6] = "BelongCom";
    listNewElementsID[7] = "Channels";

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
    $.post("GetConfigInfo?TaskType=BelongCom", function (data) {
        var objInfo1 = JSON.parse(data);
        listcontent[6] = objInfo1;
        $.post("GetConfigInfo?TaskType=ChannelsFrom", function (data) {
            var objInfo2 = JSON.parse(data);
            listcontent[7] = objInfo2;
            var tableGX = new Table(null, null, null, null, null, null, null, null);
            tableGX.addNewRow('myTable', listtypes, listNewElements, listcontent, 'DetailInfo', listNewElementsID, listCheck, "Order");
            var tbody = document.getElementById("DetailInfo");
            var rowCount = tbody.rows.length;
        });
    });

}

//加载物品

//添加物品
function LoadBasicDetail() { //增加货品信息行
    var orderID = $("#OrderID").val();
    //var typeVal = $("#Type").val();
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    //var strPID = $("#ProductID").val();
    //$("#ProductID").val(strPID + "," + ProductID);
    $.ajax({
        url: "GetOrderDetail",
        type: "post",
        data: { OrderID: orderID },
        dataType: "json",
        ansyc: false,
        success: function (data) {
           
           
            //
            var jsonTask = JSON.parse(data);
           // var json = eval(data.datas);
         
            //
            //var json = eval(data.datas);
            if (jsonTask.length > 0) {

                $("#myTable DetailInfo").html("");
                for (var i = 0; i < jsonTask.length; i++) {

                    var html = "";

                    var html2 = "";
                    $.ajax({
                        url: "../SalesRetail/GetConfigRetail",//GetConfigRetail?ConfigType=Com00&ChidGrade=0
                        type: "post",
                        data: { ConfigType: "Com00", ChidGrade: "0" },
                        dataType: "json",
                        async: false,
                        success: function (data) {
                            var objInfo2 = JSON.parse(data);
                            if (objInfo2.length > 0) {
                                for (var j = 0; j < objInfo2.length ; j++) {
                                    //   $("#Channels" + number).val(objInfo2[i].Text);
                                    if (jsonTask[i].BelongCom == objInfo2[j].ID)
                                    {
                                        html2 += "<option selected=true  value=" + objInfo2[j].ID + ">" + objInfo2[j].Text + "</option>"
                                    }else{
                                        html2 += "<option  value=" + objInfo2[j].ID + ">" + objInfo2[j].Text + "</option>"
                                    }
                                }
                            }

                        }
                    })
                    var html3 = "";
                    $.ajax({
                        url: "../SalesRetail/GetConfigRetail",//GetConfigRetail?ConfigType=Com00&ChidGrade=0
                        type: "post",
                        data: { ConfigType: "Com01", ChidGrade: "1" },
                        dataType: "json",
                        async: false,
                        success: function (data) {
                            var objInfo3 = JSON.parse(data);
                            if (objInfo3.length > 0) {
                                for (var k = 0; k < objInfo3.length ; k++) {
                                    //   $("#Channels" + number).val(objInfo2[i].Text);
                                    if (jsonTask[i].Channels == objInfo3[k].ID) {
                                        html3 += "<option selected=true  value=" + objInfo3[k].ID + ">" + objInfo3[k].Text + "</option>"
                                    } else {
                                        html3 += "<option  value=" + objInfo3[k].ID + ">" + objInfo3[k].Text + "</option>"
                                    }
                                   
                                }
                            }

                        }
                    })
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' "   id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' "  style="width:60px;" id="ProductID' + rowCount + '">' + jsonTask[i].ProductID + '</lable> </td>';
                    html += '<td ><lable class="labProName' + rowCount + ' "  style="width:60px;" id="OrderContent' + rowCount + '">' + jsonTask[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable  style="width:60px;" class="labSpec' + rowCount + ' " id="Spec' + rowCount + '" >' + jsonTask[i].SpecsModels + '</lable></td>';
                    html += '<td ><input type="text" onblur=GetTotal(this)  style="width:60px;"  class="labAmount' + rowCount + ' " id="Amount' + rowCount + '" value="' + jsonTask[i].OrderNum + '" /> </td>';
                    html += '<td ><input type="text" onblur=GetTotal(this)  id="UnitPrice' + rowCount + '" style="width:60px;" value="' + jsonTask[i].UnitPrice + '"> </td>';//单位成本
                    html += '<td><input type="text"  style="width:60px;" onblur=GetTotal(this) id="Discounts' + rowCount + '" value="' + jsonTask[i].TaxRate+ '" /></td>';
                    html += '<td><input type="text" readonly="readonly"  style="width:60px;" id="DTotalPrice' + rowCount + '" value="' + jsonTask[i].DTotalPrice + '" /></td>';//小计
                    //html += '<td ><input type="text"  style="width:60px;" id="txtRemark' + rowCount + '" value="'+jsonTask[i].txtRemark+'" > </td>';
                    html += '<td ><select name="select"  onchange=ChangeSelect(this) style="width:100px;"  id="BelongCom' + rowCount + '">' + html2 + '</select></td>';
                    html += '<td ><select name="select"  style="width:100px;"  id="Channels' + rowCount + '">' + html3 + '</select></td>';
                    html += '<td ><input type="text"  style="width:60px;" id="txtRemark' + rowCount + '" value="' + jsonTask[i].txtRemark + '" > </td>';
                    html += '<td style="display:none;"><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + jsonTask[i].PID + '</lable> </td>';
                    html += '</tr>'

                    $("#DetailInfo").append(html);
                    CountRows = CountRows + 1;
                    rowCount += 1;
                }


            }
        }
    })



}

//选择货品信息
function CheckDetail() {
    ShowIframe1("选择货品信息", "../SalesManage/ChangeProduct", 850, 550);
}

//添加物品
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
            var html2 = "";
            $.ajax({
                url: "../SalesRetail/GetConfigRetail",//GetConfigRetail?ConfigType=Com00&ChidGrade=0
                type: "post",
                data: { ConfigType: "Com00", ChidGrade: "0" },
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
            var html3 = "";
            $.ajax({
                url: "../SalesRetail/GetConfigRetail",//GetConfigRetail?ConfigType=Com00&ChidGrade=0
                type: "post",
                data: { ConfigType: "Com01", ChidGrade: "1" },
                dataType: "json",
                async: false,
                success: function (data) {
                    var objInfo2 = JSON.parse(data);
                    if (objInfo2.length > 0) {
                        for (var i = 0; i < objInfo2.length ; i++) {
                            //   $("#Channels" + number).val(objInfo2[i].Text);
                            html3 += "<option value=" + objInfo2[i].ID + ">" + objInfo2[i].Text + "</option>"
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
                    html += '<td ><lable class="labProName' + rowCount + ' "  style="width:60px;" id="OrderContent' + rowCount + '" >' + json[i].ProName + ' </lable>  </td>';
                    html += '<td ><lable type="text"  style="width:60px;" class="labSpec' + rowCount + ' " id="Spec' + rowCount + '" >' + json[i].Spec + '</lable></td>';
                    html += '<td ><input type="text" onblur=GetTotal(this)  style="width:60px;"  class="labAmount' + rowCount + ' " id="Amount' + rowCount + '" /> </td>';
                    html += '<td ><input type="text" onblur=GetTotal(this)  id="UnitPrice' + rowCount + '" style="width:60px;" value="' + json[i].UnitPrice + '"> </td>';//单位成本
                    html += '<td><input type="text"  style="width:60px;" onblur=GetTotal(this) id="Discounts' + rowCount + '"  /></td>';
                    html += '<td><input type="text" readonly="readonly"  style="width:60px;" id="DTotalPrice' + rowCount + '" /></td>';//小计
                    html += '<td ><input type="text"  style="width:60px;" id="txtRemark' + rowCount + '" > </td>';
                    html += '<td ><select name="select"  onchange=ChangeSelect(this) style="width:100px;"  id="BelongCom' + rowCount + '">' + html2; +'</select></td>';
                    html += '<td ><select name="select"  style="width:100px;"  id="Channels' + rowCount + '">' + html3; +'</select></td>';
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
    listtypeNames[0] = "OrderContent";
    listtypeNames[1] = "SpecsModels";
    listtypeNames[2] = "OrderNum";
    listtypeNames[3] = "UnitPrice";
    listtypeNames[4] = "DTotalPrice";
    listtypeNames[5] = "txtRemark";
    listtypeNames[6] = "BelongCom";
    listtypeNames[7] = "Channels";

    var tableGX = new Table(null, null, null, null, null, null, null, null);
    tableGX.removeRow('myTable', 'DetailInfo', listtypeNames);
}


function selRow(curRow) {
    newRowID = curRow.id;
}


function tyoncharge(select) {
    var txt = $(select).val();
    var id = select.id;
    // alert(txt + id);
    // var Belong = id.split('BelongCom')[0];
    var Belong = txt;//id.split("Belong")[0];
    var number = id.split("BelongCom")[1];
    //$("#Channels" + number).attr("");
    $.ajax({
        url: "../SalesRetail/GetConfigRetail",
        type: "post",
        data: { ConfigType: Belong, ChidGrade: 1 },
        dataType: "json",
        success: function (data) {
            var objInfo2 = JSON.parse(data);
            var html = "";
            if (objInfo2.length > 0) {

                for (var i = 0; i < objInfo2.length ; i++) {
                    //   $("#Channels" + number).val(objInfo2[i].Text);
                    html += "<option value=" + objInfo2[i].ID + ">" + objInfo2[i].Text + "</option>"
                }
            }
            $("#Channels" + number).html(html);
        }
    })
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
    var reg = new RegExp("^[0-9]*$");
    // var obj = document.getElementById("name");
    if (!reg.test(Num)) {
        alert("请输入数字!");
    }

    var Discounts = $("#Discounts" + number).val();
    var Price = $("#UnitPrice" + number).val();
    var r = new RegExp("^\\d+(\\.\\d+)?$");
    if (!r.test(Price)) {
        alert("请输入正确的金额!");
    }

    if (Num == "" || Num == null) {
        Num = 0;
    }
    if (Num < 0) {
        alert("数量不能为负");

        $("#Amount" + number).val("");
    }
    if (Price < 0) {
        alert("价格不能为负");

        $("#UnitPrice" + number).val("");
    }
    if (Price == "" || Price == null) {
        Price = 0;
    }
    ///
    //var reg = new RegExp("^[0-9]*$");
    //var obj = document.getElementById("name");
    //if (!reg.test(obj.value)) {
    //    alert("请输入数字!");
    //}
    //if (!/^[0-9]+$/.test(obj.value)) {
    //    alert("请输入数字!");
    //}
    //
    //Total = parseFloat(Num) * parseFloat(Price);
    if (Discounts != "" && Discounts < 0 && Discounts > 10) {
        alert("请正确输入折扣价");
        return;
    }
    if (Discounts != "") {
        Discounts = Discounts / 10;
        Total = parseFloat(Num) * parseFloat(Price) * parseFloat(Discounts);
    } else {

        Total = parseFloat(Num) * parseFloat(Price);
    }
    Total = Total.toFixed(2);
    $("#DTotalPrice" + number).val(Total);
    GetShopHJ();
}

function ChangeSelect(select) {
    var id = select.id;
    // var type = $("#BelongCom" + id).val();
    var number = id.split("BelongCom")[1];
    var BelongCom = document.getElementById("BelongCom" + number).value;
    //var grade = $("BelongCom").val();
    var html3 = "";
    $.ajax({
        url: "../SalesRetail/GetConfigRetail",//GetConfigRetail?ConfigType=Com00&ChidGrade=0
        type: "post",
        data: { ConfigType: BelongCom, ChidGrade: "1" },
        dataType: "json",
        async: false,
        success: function (data) {
            var objInfo2 = JSON.parse(data);
            if (objInfo2.length > 0) {
                for (var i = 0; i < objInfo2.length ; i++) {
                    //   $("#Channels" + number).val(objInfo2[i].Text);
                    html3 += "<option value=" + objInfo2[i].ID + ">" + objInfo2[i].Text + "</option>"
                }
            }
            $("#Channels" + number).html(html3);
        }
    })
}

function ChangeSelect1(select) {
    var id = select.id;
    // var type = $("#BelongCom" + id).val();
    var number = id.split("BelongCom")[1];
    $("#BelongCom" + number).html("");
    var BelongCom = document.getElementById("BelongCom" + number).value;
    //var grade = $("BelongCom").val();
    var html3 = "";
    $.ajax({
        url: "../SalesRetail/GetConfigRetail",//GetConfigRetail?ConfigType=Com00&ChidGrade=0
        type: "post",
        data: { ConfigType: "Com00", ChidGrade: "0" },
        dataType: "json",
        async: false,
        success: function (data) {
            var objInfo2 = JSON.parse(data);
            if (objInfo2.length > 0) {
                for (var i = 0; i < objInfo2.length ; i++) {
                    //   $("#Channels" + number).val(objInfo2[i].Text);
                    html3 += "<option value=" + objInfo2[i].ID + ">" + objInfo2[i].Text + "</option>"
                }
            }
            $("#BelongCom" + number).html(html3);
            $("#Channels" + number).html("");
            $("#BelongCom" + (rowCount - 1)).attr("onchange", "tyoncharge(this)");
        }
    })
}

//合计
function GetShopHJ() {
    var SubTotal = 0;
    var tbody = document.getElementById("DetailInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
        var Total = document.getElementById("DTotalPrice" + i).value;
        // var UnitPrice = document.getElementById("UnitPrice" + i).value;
        //var SubTotal = Aount * UnitPrice;
        SubTotal = SubTotal + parseFloat(Total);

    }
    SubTotal = SubTotal.toFixed(2);
    $("#OrderTotal").val(SubTotal);
}