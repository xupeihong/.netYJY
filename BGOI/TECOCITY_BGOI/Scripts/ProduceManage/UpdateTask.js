var FinishCount = "";
var OrderID = "";

$(document).ready(function () {
    if (location.search != "") {
        RWID = location.search.split('&')[0].split('=')[1];
    }
    //var HouseID = $("#HouseID").val(); 
    //document.getElementById("HouseIDs").value = HouseID;
    $("#RWID").attr("value", RWID);
    LoadTask();
    $("#btnSaveOrder").click(function () {
        SaveUpdateTask();
    });
    $("#btnExit").click(function () {
        window.parent.ClosePop();
    });
});

function Add() { //弹出选择物品信息页面
    ShowIframe1("物品选择", "../ProduceManage/addupdatetask", 500, 350);
}
function AddNewTask() { //弹出选择物品信息页面
    ShowIframe1("物品选择", "../ProduceManage/ChangeT", 700, 400);
}

function SaveUpdateTask() {
    var RWID = $("#RWID").val();
    var Clientcode = $("#Clientcode").val();
    var OrderUnit = $("#OrderUnit").val();
    var OrderAddress = $("#OrderAddress").val();
    var OrderContactor = $("#OrderContactor").val();
    var OrderTel = $("#OrderTel").val();
    var Remark = $("#Remark").val();
    var ContractDate = $("#ContractDate").val();
    var Technology = $("#Technology").val();
    var Note = $("#Note").val();
    //var HouseID = $("#HouseIDs").val();
   
    //详细表
    var MainContent = "";
    var PID = "";
    var OrderContent = "";
    var SpecsModels = "";
    var OrderUnits = "";
    var OrderNum = "";
    //var Technology = "";
    var DeliveryTime = "";
    //var Remarks = "";
    var DID = "";
    
    var tbody = document.getElementById("DetailInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
        var mainContent = document.getElementById("RowNumber" + i).innerHTML;
        var pID = document.getElementById("PID" + i).innerHTML;
        var orderContent = document.getElementById("OrderContent" + i).innerHTML;
        var specsModels = document.getElementById("SpecsModels" + i).innerHTML;
        var orderUnits = document.getElementById("OrderUnits" + i).innerHTML;
        var orderNum = document.getElementById("OrderNum" + i).value;
        if (orderNum == "" || orderNum == null)
        {
            alert("数量不能为空！");
            return false;
        }
        //var technology = document.getElementById("Technology" + i).value;
        var deliveryTime = document.getElementById("DeliveryTime" + i).value;
        if (deliveryTime == "" || deliveryTime == null)
        {
            alert("完成日期不能为空");
            return false;
        }
        //var remarks = document.getElementById("Remarks" + i).value;
       
       


        MainContent += mainContent;
        PID += pID;
        OrderContent += orderContent;
        SpecsModels += specsModels;
        OrderUnits += orderUnits;
        OrderNum += orderNum;
        //Technology += technology;
        DeliveryTime += deliveryTime;
        //Remarks += remarks;
      
       
        if (i < tbody.rows.length - 1) {
            MainContent += ",";
            PID += ",";
            OrderContent += ",";
            SpecsModels += ",";
            OrderUnits += ",";
            OrderNum += ",";
            DeliveryTime += ",";
            //Technology += ",";
            //Remarks += ",";
       
        }
        else {
            MainContent += "";
            PID += "";
            OrderContent += "";
            SpecsModels += "";
            OrderUnits += "";
            OrderNum += "";
            DeliveryTime += "";
            //Technology += "";
            //Remarks += "";
           
        }
    }
    $.ajax({
        url: "SaveUpdateTask",
        type: "Post",
        data: {
            RWID: RWID, Clientcode: Clientcode, OrderUnit: OrderUnit, OrderAddress: OrderAddress,
            OrderContactor: OrderContactor, OrderTel: OrderTel, Remark: Remark, ContractDate: ContractDate,
            MainContent: MainContent, PID: PID, OrderContent: OrderContent, SpecsModels: SpecsModels,
            OrderUnits: OrderUnits, OrderNum: OrderNum, DeliveryTime: DeliveryTime, Technology: Technology, Note: Note
            //,
            //HouseID: HouseID
        },
        async: false,
        success: function (data) {
            if (data.success == true) {
                alert("修改任务单成功！");
                window.parent.frames["iframeRight"].Search();
                setTimeout('parent.ClosePop()', 100);
            }
            else {
                alert(data.Msg);
            }
        }
    });
}


  function LoadTask() {
    $.ajax({
        url: "GetTaskDetailss",
        type: "post",
        data: { RWID: RWID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    var html = "";
                    rowCount = document.getElementById("DetailInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    //PID, OrderContent, SpecsModels, OrderUnit, OrderNum, Technology, DeliveryTime, Remark
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
                    html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' "style="width:140px;" id="OrderContent' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="SpecsModels' + rowCount + '">' + json[i].SpecsModels + '</lable> </td>';
                    html += '<td ><lable class="labOrderUnits' + rowCount + ' " id="OrderUnits' + rowCount + '">' + json[i].OrderUnit + '</lable> </td>';
                    html += '<td ><input type="text" id="OrderNum' + rowCount + '" style="width:60px;" value="' + json[i].OrderNum + '"/></td>';
                    //html += '<td ><input type="text" id="Technology' + rowCount + '" style="width:100px;" value="' + json[i].Technology + '"/></td>';
                    html += '<td > <input type="text" onclick="WdatePicker()" id="DeliveryTime' + rowCount + '" style="width:80px;" value="' + json[i].DeliveryTime + '"/><lable class="labSpecsModels' + rowCount + ' " id="DID' + rowCount + '"style="display:none">' + json[i].DID + '</lable><lable class="labSpecsModels' + rowCount + ' " id="Order' + rowCount + '"style="display:none">' + json[i].OrderID + '</lable> </td>';
                    //html += '<td ><input type="text" id="Remarks' + rowCount + '" style="width:130px;" value="' + json[i].Remark + '"/></td>';
                    html += '<td ><a id="DetailInfo' + rowCount + '" onclick="deleteTr(this);" style="color:blue;cursor:pointer;text-decoration:underline;">删除</a> </td>';
                    //html += '<td id="FinishCount' + rowCount + '"style="display:none"><lable class="labOrderUnits' + rowCount + ' " id="FinishCount' + rowCount + '"style="display:none"></lable></td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                    CountRows = CountRows + 1;
                }

            }
        }
    })
  }


  function customs(OrderID)
  {
      var OrderID = OrderID;
      var tbody = document.getElementById("DetailInfo");
      if (tbody.rows.length == 0) {
          var b = "";
          $.ajax({
              url: "GetTaskDetail",
              type: "post",
              data: { OrderID: OrderID },
              dataType: "json",
              success: function (data) {
                  var json = eval(data.datas);
                  if (json.length > 0) {
                      for (var i = 0; i < json.length; i++) {
                          var html = "";
                          rowCount = document.getElementById("DetailInfo").rows.length;
                          var CountRows = parseInt(rowCount) + 1;
                          //PID, OrderContent, SpecsModels, OrderUnit, OrderNum, Technology, DeliveryTime, Remark
                          html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                          html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
                          html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                          html += '<td ><lable class="labOrderContent' + rowCount + ' "style="width:140px;" id="OrderContent' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                          html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="SpecsModels' + rowCount + '">' + json[i].SpecsModels + '</lable> </td>';
                          html += '<td ><lable class="labOrderUnits' + rowCount + ' " id="OrderUnits' + rowCount + '">' + json[i].OrderUnit + '</lable> </td>';
                          html += '<td ><input type="text" id="OrderNum' + rowCount + '" style="width:60px;" readOnly="true"value="' + json[i].OrderNum + '"/></td>';
                          //html += '<td ><input type="text" id="Technology' + rowCount + '" style="width:100px;" /></td>';
                          html += '<td > <input type="text" onclick="WdatePicker()" id="DeliveryTime' + rowCount + '" style="width:80px;" /></td>';
                          //html += '<td ><input type="text" id="Remarks' + rowCount + '" style="width:130px;"/> </td>';
                          html += '<td ><a id="DetailInfo' + rowCount + '" onclick="deleteTr(this);" style="color:blue;cursor:pointer;text-decoration:underline;">删除</a> <lable class="labPID' + rowCount + ' " id="DID' + rowCount + '">' + b + '</lable><lable style="display:none" class="labPID' + rowCount + ' " id="Order' + rowCount + '">' + OrderID + '</lable></td>';
                          //html += '<td id="FinishCount' + rowCount + '"style="display:none"><lable class="labOrderUnits' + rowCount + ' " id="FinishCount' + rowCount + '"style="display:none"></lable></td>';
                          html += '</tr>'
                          $("#DetailInfo").append(html);
                          CountRows = CountRows + 1;
                      }
                  }
              }
          })
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
                          document.getElementById("OrderUnit").value = +json[i].OrderUnit;
                          document.getElementById("OrderAddress").value = json[i].OrderAddress;
                          document.getElementById("OrderContactor").value = json[i].OrderContactor;
                          document.getElementById("OrderTel").value = json[i].OrderTel;
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
          var b = "";
          $.ajax({
              url: "GetTaskDetail",
              type: "post",
              data: { OrderID: OrderID },
              dataType: "json",
              success: function (data) {
                  var json = eval(data.datas);
                  if (json.length > 0) {
                      for (var i = 0; i < json.length; i++) {
                          var html = "";
                          rowCount = document.getElementById("DetailInfo").rows.length;
                          var CountRows = parseInt(rowCount) + 1;
                          //PID, OrderContent, SpecsModels, OrderUnit, OrderNum, Technology, DeliveryTime, Remark
                          html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                          html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
                          html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                          html += '<td ><lable class="labOrderContent' + rowCount + ' "style="width:140px;" id="OrderContent' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                          html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="SpecsModels' + rowCount + '">' + json[i].SpecsModels + '</lable> </td>';
                          html += '<td ><lable class="labOrderUnits' + rowCount + ' " id="OrderUnits' + rowCount + '">' + json[i].OrderUnit + '</lable> </td>';
                          html += '<td ><input type="text" id="OrderNum' + rowCount + '" style="width:60px;" readOnly="true"value="' + json[i].OrderNum + '"/></td>';
                          //html += '<td ><input type="text" id="Technology' + rowCount + '" style="width:100px;" /></td>';
                          html += '<td > <input type="text" onclick="WdatePicker()" id="DeliveryTime' + rowCount + '" style="width:80px;" /></td>';
                          //html += '<td ><input type="text" id="Remarks' + rowCount + '" style="width:130px;"/> </td>';
                          html += '<td ><a id="DetailInfo' + rowCount + '" onclick="deleteTr(this);" style="color:blue;cursor:pointer;text-decoration:underline;">删除</a> <lable class="labPID' + rowCount + ' " id="DID' + rowCount + '">' + b + '</lable><lable style="display:none" class="labPID' + rowCount + ' " id="Order' + rowCount + '">' + OrderID + '</lable></td>';
                          //html += '<td id="FinishCount' + rowCount + '"style="display:none"><lable class="labOrderUnits' + rowCount + ' " id="FinishCount' + rowCount + '"style="display:none"></lable></td>';
                          html += '</tr>'
                          $("#DetailInfo").append(html);
                          CountRows = CountRows + 1;
                      }
                  }
              }
          })
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
                          document.getElementById("OrderUnit").value = +json[i].OrderUnit;
                          document.getElementById("OrderAddress").value = json[i].OrderAddress;
                          document.getElementById("OrderContactor").value = json[i].OrderContactor;
                          document.getElementById("OrderTel").value = json[i].OrderTel;
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
    var one = confirm("确认删除");
    if (one == false)
        return;
    else {
        newRowID = curRow.id;
        $("#DetailInfo tr").removeAttr("class");
        $("#" + newRowID).attr("class", "RowClass");

        var a = "#" + newRowID
        ss = Number(a.substring(11, 12));
        //alert(ss);
        //字符串截取DetailInfo，要剩下的   int
        //和did的id组装成需要的did的id
        var DID = document.getElementById("DID" + ss).innerHTML;

        if (DID == "") {
            var tbodyID = "DetailInfo";
            var rowIndex = -1;
            var typeNames = ["RowNumber", "PID", "OrderContent", "SpecsModels", "OrderUnits", "OrderNum", "DeliveryTime", "DID", "DetailInfo", "Order"];
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
                        selRow(document.getElementById(tbodyID + rowIndex), '');
                }
            }
            $("#DetailInfo tr").removeAttr("class");
        }
        else {
            var tbodyID = "DetailInfo";
            var rowIndex = -1;
            var typeNames = ["RowNumber", "PID", "OrderContent", "SpecsModels", "OrderUnits", "OrderNum", "DeliveryTime", "DID", "DetailInfo", "Order"];
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
            //$.ajax({
            //    type: "POST",
            //    url: "SCTask",
            //    data: { DID: DID },
            //    dataType: 'json',
            //    success: function (data) {
            //        alert(data.Msg);
            //       // window.parent.frames["iframeRight"].UpdateTask();
            //    },
            //    dataType: 'json',
            //});
        }
        return true;
    }
}

function updatedatetask(PID)
{
    var PID = PID;
    var tbody = document.getElementById("DetailInfo");
    if (tbody.rows.length == 0) {
        var b = "";
        $.ajax({
            url: "GetTaskDetails",
            type: "post",
            data: { PID: PID },
            dataType: "json",
            success: function (data) {
                var json = eval(data.datas);
                if (json.length > 0) {
                    for (var i = 0; i < json.length; i++) {
                        var html = "";
                        rowCount = document.getElementById("DetailInfo").rows.length;
                        var CountRows = parseInt(rowCount) + 1;
                        //PID, OrderContent, SpecsModels, OrderUnit, OrderNum, Technology, DeliveryTime, Remark
                        html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                        html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
                        html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' "style="width:140px;" id="OrderContent' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                        html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="SpecsModels' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                        html += '<td ><lable class="labOrderUnits' + rowCount + ' " id="OrderUnits' + rowCount + '">' + json[i].Units + '</lable> </td>';
                        html += '<td ><input type="text" id="OrderNum' + rowCount + '" style="width:60px;" /></td>';
                        //html += '<td ><input type="text" id="Technology' + rowCount + '" style="width:100px;" /></td>';
                        html += '<td > <input type="text" onclick="WdatePicker()" id="DeliveryTime' + rowCount + '" style="width:80px;" /></td>';
                        //html += '<td ><input type="text" id="Remarks' + rowCount + '" style="width:130px;"/> </td>';
                        html += '<td ><a id="DetailInfo' + rowCount + '" onclick="deleteTr(this);" style="color:blue;cursor:pointer;text-decoration:underline;">删除</a> <lable class="labPID' + rowCount + ' " id="DID' + rowCount + '">' + b + '</lable><lable class="labPID' + rowCount + ' " id="Order' + rowCount + '">' + OrderID + '</lable></td>';
                        //html += '<td id="FinishCount' + rowCount + '"style="display:none"><lable class="labOrderUnits' + rowCount + ' " id="FinishCount' + rowCount + '"style="display:none"></lable></td>';
                        html += '</tr>'
                        $("#DetailInfo").append(html);
                        CountRows = CountRows + 1;
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
                        var html = "";
                        rowCount = document.getElementById("DetailInfo").rows.length;
                        var CountRows = parseInt(rowCount) + 1;
                        //PID, OrderContent, SpecsModels, OrderUnit, OrderNum, Technology, DeliveryTime, Remark
                        html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                        html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
                        html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' "style="width:140px;" id="OrderContent' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                        html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="SpecsModels' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                        html += '<td ><lable class="labOrderUnits' + rowCount + ' " id="OrderUnits' + rowCount + '">' + json[i].Units + '</lable> </td>';
                        html += '<td ><input type="text" id="OrderNum' + rowCount + '" style="width:60px;" /></td>';
                        //html += '<td ><input type="text" id="Technology' + rowCount + '" style="width:100px;" /></td>';
                        html += '<td > <input type="text" onclick="WdatePicker()" id="DeliveryTime' + rowCount + '" style="width:80px;" /></td>';
                        //html += '<td ><input type="text" id="Remarks' + rowCount + '" style="width:130px;"/> </td>';
                        html += '<td ><a id="DetailInfo' + rowCount + '" onclick="deleteTr(this);" style="color:blue;cursor:pointer;text-decoration:underline;">删除</a> <lable style="display:none" class="labPID' + rowCount + ' " id="DID' + rowCount + '">' + b + '</lable><lable class="labPID' + rowCount + ' " id="Order' + rowCount + '">' + OrderID + '</lable></td>';
                        //html += '<td id="FinishCount' + rowCount + '"style="display:none"><lable class="labOrderUnits' + rowCount + ' " id="FinishCount' + rowCount + '"style="display:none"></lable></td>';
                        html += '</tr>'
                        $("#DetailInfo").append(html);
                        CountRows = CountRows + 1;
                    }
                }
            }
        })
    }
}

function getAllNum() {
    var HouseID = $("#HouseIDs").val();
    $("#KCSL").show();

    var tbody = document.getElementById("DetailInfo");
    for (var m = 0; m < tbody.rows.length; m++) {
        var pID = document.getElementById("PID" + m).innerHTML;
        $("#FinishCount" + m).show();
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