var RWID;
var rowCount;
var newRowID;
var FinishCount = "";
var OrderID = "";

function AddNewTask() { //弹出选择物品信息页面
    ShowIframe1("销售订单产品", "../ProduceManage/ChangeTask", 700, 400);
}
function Add() { //弹出选择物品信息页面
    ShowIframe1("产品选择", "../ProduceManage/ChangeTasks", 700, 350);
}

$(document).ready(function () {
    $("#Cancel").click(function () {
        window.parent.ClosePop();
    })

    var date = new Date();
    var yy = date.getYear();
    if (yy < 1900) yy = yy + 1900;
    var MM = date.getMonth() + 1;
    if (MM < 10) MM = '0' + MM;
    var dd = date.getDate();
    if (dd < 10) dd = '0' + dd;
    $("#ContractDate").val(yy + "-" + MM + "-" + dd);


    $("#btnSave").click(function () {


        var RWID = $("#RWID").val();
        var OrderID = $("#OrderID").val();
        var Clientcode = $("#Clientcode").val();
        var OrderUnit = $("#OrderUnit").val();
        var OrderAddress = $("#OrderAddress").val();
        var OrderContactor = $("#OrderContactor").val();
        var OrderTel = $("#OrderTel").val();
        var Remark = $("#Remark").val();
        var CreateUser = $("#CreateUser").val();
        var ContractDate = $("#ContractDate").val();
        var Technology = $("#Technology").val();
        var Note = $("#Note").val();
        var ID = $("#ID").val();
        var m = $("#m").val();
        //var HouseID = $("#HouseID").val();

        //if (HouseID == "" || HouseID == null)
        //{
        //    alert("库房名称不能为空");
        //    return
        //}
        if (CreateUser == "" || CreateUser == null) {
            alert("记录人不能为空");
            return;
        }
        if (ContractDate == "" || ContractDate == null) {
            alert("开单日期不能为空");
            return;
        }

        var MainContent = "";
        var PID = "";
        var OrderContent = "";
        var SpecsModels = "";
        var OrderUnits = "";
        var OrderNum = "";
        //var Technology = "";
        var DeliveryTime = "";
        var Remarks = "";
        var OrderNums = "";


        var tbody = document.getElementById("DetailInfo");
        for (var i = 0; i < tbody.rows.length; i++) {
            var mainContent = document.getElementById("RowNumber" + i).innerHTML;
            var pID = document.getElementById("ProductID" + i).innerHTML;
            var orderNum = document.getElementById("OrderNum" + i).value;
            var orderContent = document.getElementById("OrderContent" + i).innerHTML;
            var specsModels = document.getElementById("SpecsModels" + i).innerHTML;
            var orderUnits = document.getElementById("OrderUnits" + i).innerHTML;
            //var technology = document.getElementById("Technology" + i).value;  //一次修改注释技术要求和备注
            var orderNums = document.getElementById("OrderNums" + i).innerHTML;
            //if (orderNums != "")
            //{
            //    if (Number(orderNum) > Number(orderNums)) {
            //        alert("产品"+pID+"数量不能大于销售单数量");
            //        return;
            //    }
            //}
           
            var deliveryTime = document.getElementById("DeliveryTime" + i).value;
            var n1 = deliveryTime.split("-");
            var date = new Date();
            date.setFullYear(n1[0], n1[1] - 1, n1[2]);

            var mydate = new Date();


            if (date < mydate) {
                alert("完成日期不能小于当前日期！");
                return;
            }
            //var remarks = document.getElementById("Remark" + i).value;


            
            MainContent += mainContent;
            PID += pID;
            OrderContent += orderContent;
            SpecsModels += specsModels;
            OrderUnits += orderUnits;
            OrderNum += orderNum;
            if (orderNum == "" || orderNum == null) {
                alert("数量不能为空");
                return;
            }
            //Technology += technology;
            DeliveryTime += deliveryTime;
            if (deliveryTime == "" || deliveryTime == null) {
                alert("完成日期不能为空");
                return;
            }
            //Remarks += remarks;


            if (i < tbody.rows.length - 1) {
                MainContent += ",";
                PID += ",";
                OrderContent += ",";
                SpecsModels += ",";
                OrderUnits += ",";
                OrderNum += ",";
                //Technology += ",";
                DeliveryTime += ",";
                //Remarks += ",";

            }
            else {
                MainContent += "";
                PID += "";
                OrderContent += "";
                SpecsModels += "";
                OrderUnits += "";
                OrderNum += "";
                //Technology += "";
                DeliveryTime += "";
                //Remarks += "";

            }
        }


        $.ajax({
            url: "SaveTaskIn",
            type: "Post",
            data: {
                RWID: RWID, Clientcode: Clientcode, OrderUnit: OrderUnit, OrderAddress: OrderAddress, ID: ID,
                OrderContactor: OrderContactor, OrderID: OrderID,
                OrderTel: OrderTel, Remark: Remark, CreateUser: CreateUser, ContractDate: ContractDate, Technology: Technology, Note: Note,
                MainContent: MainContent, PID: PID, OrderContent: OrderContent, SpecsModels: SpecsModels,
                OrderUnits: OrderUnits, OrderNum: OrderNum,
                //HouseID: HouseID,
                //Technology: Technology,
                DeliveryTime: DeliveryTime //一次修改注释技术要求和备注
                ,
                //Remarks: Remarks
            },
            async: false,
            success: function (data) {
                if (data.success == true) {
                    alert("添加成功");
                    window.parent.frames["iframeRight"].Search();
                    setTimeout('parent.ClosePop()', 100);
                }
                else {
                    alert(data.Msg);
                }
            }
        });
    });
});

function custom(OrderID) { //增加货品信息行
    $.ajax({
        url: "IndexAllcustom",
        type: "post",
        data: { OrderID: OrderID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {

                    if (document.getElementById("Clientcode").value == "") {
                        document.getElementById("Clientcode").value = "";
                    } else
                        document.getElementById("Clientcode").value = json[i].Clientcode;
                    document.getElementById("OrderUnit").value = json[i].OrderUnit;
                    document.getElementById("OrderAddress").value = json[i].OrderAddress;
                    document.getElementById("OrderContactor").value = json[i].OrderContactor;
                    document.getElementById("OrderTel").value = json[i].OrderTel;
                    document.getElementById("Technology").value = json[i].Technology;
                }
            }
        }
    })
}

function addTaskDetail(PID, OrderID, ContractID) { //增加货品信息行
    var PID = PID;
    var OrderID = OrderID;
    $("#ID").val(ContractID);
    $("#OrderID").val(OrderID);
    var tbody = document.getElementById("DetailInfo");
    if (tbody.rows.length == 0) {
        $.ajax({
            url: "GetTaskDetail",
            type: "post",
            data: { OrderID: OrderID },
            dataType: "json",
            success: function (data) {
                var json = eval(data.datas);
                if (json.length > 0) {
                    for (var i = 0; i < json.length; i++) {
                        rowCount = document.getElementById("DetailInfo").rows.length;
                        var CountRows = parseInt(rowCount) + 1;
                        var html = "";
                        html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                        html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
                        html += '<td ><lable class="labProductid' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                        html += '<td ><lable class="labProName' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                        html += '<td ><lable class="labSpec' + rowCount + ' " id="SpecsModels' + rowCount + '">' + json[i].SpecsModels + '</lable> </td>';
                        html += '<td ><lable class="labOrderUnits' + rowCount + ' " id="OrderUnits' + rowCount + '">' + json[i].OrderUnit + '</lable></td>';
                        html += '<td ><input type="text" id="OrderNum' + rowCount + '"  style="width:60px;"readOnly="true" value="' + json[i].OrderNum + '"/></td>';
                        //html += '<td ><input type="text" id="Technology' + rowCount + '" style="width:100px;"/></td>';
                        html += '<td > <input type="text" onclick="WdatePicker()" id="DeliveryTime' + rowCount + '" style="width:60px;"/></td>';
                        //html += '<td ><input type="text" id="Remark' + rowCount + '" style="width:100px;"/></td>';
                        html += '<td ><a id="DetailInfo' + rowCount + '" onclick="deleteTr(this);" style="color:blue;cursor:pointer;text-decoration:underline">删除</a><lable class="labOrderUnits' + rowCount + ' " id="OrderUnits' + rowCount + '"style="display:none">' + json[i].Technology + '</lable> <lable class="labOrderUnits' + rowCount + ' " id="Order' + rowCount + '"style="display:none">' + json[i].OrderID + '</lable><lable class="labOrderUnits' + rowCount + ' " id="OrderNums' + rowCount + '"style="display:none">' + json[i].OrderNums + '</lable><lable class="labOrderUnits' + rowCount + ' " id="OrderNums' + rowCount + '"style="display:none">' + json[i].OrderNums + '</lable><lable class="labOrderUnits' + rowCount + ' " id="DID' + rowCount + '"style="display:none">' + json[i].DID + '</lable></td>';
                        //html += '<td id="FinishCount' + rowCount + '"style="display:none"><lable class="labOrderUnits' + rowCount + ' " id="FinishCount' + rowCount + '"style="display:none"></lable></td>';
                        html += '</tr>'//一次修改注释技术要求和备注
                        $("#DetailInfo").append(html);

                    }
                }
            }
        })
    }
    else {
        for (var i = 0; i < tbody.rows.length; i++) {
            var Order = document.getElementById("Order" + i).innerHTML;
            if (OrderID.replace(/[ ]/g, "") == Order.replace(/[ ]/g, "")) {
                return;
            }
        }
        $.ajax({
            url: "GetTaskDetail",
            type: "post",
            data: { OrderID: OrderID },
            dataType: "json",
            success: function (data) {
                var json = eval(data.datas);
                if (json.length > 0) {
                    for (var i = 0; i < json.length; i++) {
                        rowCount = document.getElementById("DetailInfo").rows.length;
                        var CountRows = parseInt(rowCount) + 1;
                        var html = "";
                        html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                        html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
                        html += '<td ><lable class="labProductid' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                        html += '<td ><lable class="labProName' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                        html += '<td ><lable class="labSpec' + rowCount + ' " id="SpecsModels' + rowCount + '">' + json[i].SpecsModels + '</lable> </td>';
                        html += '<td ><lable class="labOrderUnits' + rowCount + ' " id="OrderUnits' + rowCount + '">' + json[i].OrderUnit + '</lable></td>';
                        html += '<td ><input type="text" id="OrderNum' + rowCount + '"  style="width:60px;"readOnly="true"value="' + json[i].OrderNum + '"/></td>';
                        //html += '<td ><input type="text" id="Technology' + rowCount + '" style="width:100px;"/></td>';
                        html += '<td > <input type="text" onclick="WdatePicker()" id="DeliveryTime' + rowCount + '" style="width:60px;"/></td>';
                        //html += '<td ><input type="text" id="Remark' + rowCount + '" style="width:100px;"/></td>';
                        html += '<td ><a id="DetailInfo' + rowCount + '" onclick="deleteTr(this);" style="color:blue;cursor:pointer;text-decoration:underline">删除</a><lable class="labOrderUnits' + rowCount + ' " id="OrderUnits' + rowCount + '"style="display:none">' + json[i].Technology + '</lable> <lable class="labOrderUnits' + rowCount + ' " id="Order' + rowCount + '"style="display:none">' + json[i].OrderID + '</lable><lable class="labOrderUnits' + rowCount + ' " id="OrderNums' + rowCount + '"style="display:none">' + json[i].OrderNums + '</lable><lable class="labOrderUnits' + rowCount + ' " id="DID' + rowCount + '"style="display:none">' + json[i].DID + '</lable></td>';
                        //html += '<td id="FinishCount' + rowCount + '"style="display:none"><lable class="labOrderUnits' + rowCount + ' " id="FinishCount' + rowCount + '"style="display:none"></lable></td>';
                        html += '</tr>'//一次修改注释技术要求和备注
                        $("#DetailInfo").append(html);
                    }
                }
            }
        })
    }
}

function addTaskDetails(PID) { //增加货品信息行
    var DID = "";
    var PID = PID;
    var OrderNums = "";
    var tbody = document.getElementById("DetailInfo");
    if (tbody.rows.length == 0) {
        $.ajax({
            url: "GetTaskDetails",
            type: "post",
            data: { PID: PID },
            dataType: "json",
            success: function (data) {
                var json = eval(data.datas);
                if (json.length > 0) {
                    for (var i = 0; i < json.length; i++) {
                        rowCount = document.getElementById("DetailInfo").rows.length;
                        var CountRows = parseInt(rowCount) + 1;
                        var html = "";
                        html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                        html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
                        html += '<td ><lable class="labPID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                        html += '<td ><lable class="labProName' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                        html += '<td ><lable class="labSpec' + rowCount + ' " id="SpecsModels' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                        html += '<td ><lable class="labOrderUnits' + rowCount + ' " id="OrderUnits' + rowCount + '">' + json[i].Units + '</lable></td>';
                        html += '<td ><input type="text" id="OrderNum' + rowCount + '"  style="width:60px;"/></td>';
                        //html += '<td ><input type="text" id="Technology' + rowCount + '" style="width:100px;"/></td>';
                        html += '<td > <input type="text" onclick="WdatePicker()" id="DeliveryTime' + rowCount + '" style="width:60px;"/></td>';
                        //html += '<td ><input type="text" id="Remark' + rowCount + '" style="width:100px;"/></td>';  //一次修改注释技术要求和备注
                        html += '<td ><a id="DetailInfo' + rowCount + '" onclick="deleteTr(this);" style="color:blue;cursor:pointer;text-decoration:underline;">删除</a> <lable class="labOrderUnits' + rowCount + ' " id="DID' + rowCount + '"style="display:none">' + DID + '</lable><lable class="labOrderUnits' + rowCount + ' " id="Order' + rowCount + '"style="display:none">' + OrderID + '</lable><lable class="labOrderUnits' + rowCount + ' " id="OrderNums' + rowCount + '"style="display:none">' + OrderNums + '</lable></td>';
                        //html += '<td id="FinishCount' + rowCount + '"style="display:none"><lable class="labOrderUnits' + rowCount + ' " id="FinishCount' + rowCount + '"style="display:none"></lable></td>';
                        html += '</tr>'
                        $("#DetailInfo").append(html);
                    }
                }
            }
        })
    }
    else {
        for (var i = 0; i < tbody.rows.length; i++) {
            var pID = tbody.getElementsByTagName("tr")[i].cells[1].innerText;
            if (PID.replace(/[ ]/g, "") == pID.replace(/[ ]/g, "")) {
                return;
            }
        }
        $.ajax({
            url: "GetTaskDetails",
            type: "post",
            data: { PID: PID },
            dataType: "json",
            success: function (data) {
                var json = eval(data.datas);
                if (json.length > 0) {
                    for (var i = 0; i < json.length; i++) {
                        rowCount = document.getElementById("DetailInfo").rows.length;
                        var CountRows = parseInt(rowCount) + 1;
                        var html = "";
                        html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                        html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
                        html += '<td ><lable class="labPID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                        html += '<td ><lable class="labProName' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                        html += '<td ><lable class="labSpec' + rowCount + ' " id="SpecsModels' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                        html += '<td ><lable class="labOrderUnits' + rowCount + ' " id="OrderUnits' + rowCount + '">' + json[i].Units + '</lable></td>';
                        html += '<td ><input type="text" id="OrderNum' + rowCount + '"  style="width:60px;"/></td>';
                        //html += '<td ><input type="text" id="Technology' + rowCount + '" style="width:100px;"/></td>';
                        html += '<td > <input type="text" onclick="WdatePicker()" id="DeliveryTime' + rowCount + '" style="width:60px;"/></td>';
                        //html += '<td ><input type="text" id="Remark' + rowCount + '" style="width:100px;"/></td>';  //一次修改注释技术要求和备注
                        html += '<td ><a id="DetailInfo' + rowCount + '" onclick="deleteTr(this);" style="color:blue;cursor:pointer;text-decoration:underline;">删除</a> <lable class="labOrderUnits' + rowCount + ' " id="DID' + rowCount + '"style="display:none">' + DID + '</lable><lable class="labOrderUnits' + rowCount + ' " id="Order' + rowCount + '"style="display:none">' + OrderID + '</lable><lable class="labOrderUnits' + rowCount + ' " id="OrderNums' + rowCount + '"style="display:none">' + OrderNums + '</lable></td>';
                        //html += '<td id="FinishCount' + rowCount + '"style="display:none"><lable class="labOrderUnits' + rowCount + ' " id="FinishCount' + rowCount + '"style="display:none"></lable></td>';
                        html += '</tr>'
                        $("#DetailInfo").append(html);
                    }
                }
            }
        })
    }
}


function selRow(curRow) {
    newRowID = curRow.id;
    $("#DetailInfo tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}


function deleteTr(curRow) {
    newRowID = curRow.id;
    var one = confirm("确认删除");
    if (one == false)
        return;
    else {
        var tbodyID = "DetailInfo";
        var rowIndex = -1;
        var typeNames = ["RowNumber", "ProductID", "OrderContent", "SpecsModels", "OrderUnits", "OrderNum", "DeliveryTime", "DetailInfo", "DID", "OrderNums"];//一次修改删除技术要求和备注
        if (newRowID != "")

            rowIndex = newRowID.replace(tbodyID, '');
        if (rowIndex != -1) {
            document.getElementById(tbodyID).deleteRow(rowIndex);
            //var a = $("#" + tbodyID + " tr").length;
            if (rowIndex < $("#" + tbodyID + " tr").length) {
                for (var i = rowIndex; i < $("#" + tbodyID + " tr").length; i++) {
                    // var b = parseInt(i);
                    var tr = document.getElementById(tbodyID + (parseInt(i) + 1));
                    var tr1 = $("#" + tbodyID + (parseInt(i) + 1));
                    tr.id = tbodyID + i;
                    for (var j = 0; j < tr.childNodes.length; j++) {
                        var html = tr1.html();
                        for (var k = 0; k < typeNames.length; k++) {
                            var olPID = typeNames[k] + (parseInt(i) + 1);
                            var newid = typeNames[k] + i;
                            var reg = new RegExp(olPID, "g");
                            html = html.replace(reg, newid);
                        }
                        tr1.html(html);//.toLocaleLowerCase());//replace('TD','td'));
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
        $("#DetailInfo tr").removeAttr("class");
    }
}

function getAllNum()
{
    var HouseID = $("#HouseID").val();
    $("#KCSL").show();
    
    var tbody = document.getElementById("DetailInfo");
    for (var m = 0; m < tbody.rows.length; m++) {
        var pID = document.getElementById("ProductID" + m).innerHTML;
        $("#FinishCount"+m).show();
        $.ajax({
            async: false,
            single: true,
            url: "getKCnum",
            type: "post",
            data: { PID: pID, HouseID: HouseID },
            dataType: "json",
            success: function (data) {
                var json = eval(data.datas);
                if (json.length > 0) {
                    for (var i = 0; i < json.length; i++) {
                        if (json[i].FinishCount == "" || json[i].FinishCount == null) {
                            json[i].FinishCount = 0;
                        }
                        document.getElementById("FinishCount" + m).innerHTML = json[i].FinishCount;
                    }
                }
                else {
                    document.getElementById("FinishCount" + m).innerHTML = 0;
                }
            }
        })
    }
}
