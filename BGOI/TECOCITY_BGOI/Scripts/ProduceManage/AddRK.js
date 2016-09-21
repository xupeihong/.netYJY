$(document).ready(function () {
    jq();

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
    $("#Batch").val(yy + "-" + MM + "-" + dd);

    $("#btnSave").click(function () {
        var RKID = $("#RKID").val();
        var RWID = $("#RWID").val();
        var StockInTime = $("#StockInTime").val();
        var FinishTime = $("#FinishTime").val();
        var HouseID = $("#HouseName").val();
        var Batch = $("#Batch").val();
        var StockRemark = $("#StockRemark").val();
        var StockInUser = $("#StockInUser").val();
        var Storekeeper = $("#Storekeeper").val();
       

        if (StockInTime == "" || StockInTime == null) {
            alert("入库日期不能为空");
            return;
        }
        if (FinishTime == "" || FinishTime == null) {
            alert("完成日期不能为空");
            return;
        }
        if (HouseID == "" || HouseID == null) {
            alert("仓库名不能为空");
            return;
        }
        if (Batch == "" || Batch == null) {
            alert("批注不能为空");
            return;
        }
        if (StockRemark == "" || StockRemark == null) {
            alert("说明不能为空");
            return;
        }
       
        var MainContent = "";
        var PID = "";
        var OrderContent = "";
        var SpecsModels = "";
        var OrderUnit = "";
        var OrderNum = "";
        var Remarks = "";
        var DID = "";
       
        var a = 0;

         var tbody = document.getElementById("DetailInfo");
         for (var i = 0; i < tbody.rows.length; i++) {
             var ch = document.getElementById("a" + i);
             if (ch.checked) {
                 a += 1;
             }
         }

        var tbody = document.getElementById("DetailInfo");
        for (var i = 0; i < tbody.rows.length; i++) {
            var ch = document.getElementById("a" + i);
            if (ch.checked) {
                var mainContent = document.getElementById("RowNumber" + i).innerHTML;
                var pID = document.getElementById("PID" + i).innerHTML;
                var orderContent = document.getElementById("OrderContent" + i).innerHTML;
                var specsModels = document.getElementById("SpecsModels" + i).innerHTML;
                var orderUnit = document.getElementById("OrderUnit" + i).innerHTML;
                var orderNum = document.getElementById("OrderNum" + i).value;
                if (orderNum == "" || OrderNum == null)
                {
                    alert("请输入需要入库的数量！")
                    break;
                }
                var remarks = document.getElementById("Remarks" + i).innerHTML;
                var dID = document.getElementById("DID" + i).innerHTML;
                var Amount = document.getElementById("Amount" + i).innerHTML;
                var orderNum = Number(orderNum);
                var Amount = Number(Amount);


                if (orderNum > 0) {
                    if (orderNum <= Amount) {
                        MainContent += mainContent;
                        PID += pID;
                        OrderContent += orderContent;
                        SpecsModels += specsModels;
                        OrderUnit += orderUnit;
                        OrderNum += orderNum;
                        Remarks += remarks;
                        DID += dID;

                        if (a == 1) {
                            MainContent += "";
                            PID += "";
                            OrderContent += "";
                            SpecsModels += "";
                            OrderUnit += "";
                            OrderNum += "";
                            Remarks += "";
                            DID += "";
                        }
                        else {
                            if (i < tbody.rows.length - 1) {
                                MainContent += ",";
                                PID += ",";
                                OrderContent += ",";
                                SpecsModels += ",";
                                OrderUnit += ",";
                                OrderNum += ",";
                                Remarks += ",";
                                DID += ",";
                            }
                            else {
                                MainContent += "";
                                PID += "";
                                OrderContent += "";
                                SpecsModels += "";
                                OrderUnit += "";
                                OrderNum += "";
                                Remarks += "";
                                DID += "";
                            }
                        }
                    }
                    else {
                        alert("您所选产品的数量大于任务总数量");
                        type = 1;
                        break;
                    }
                } else {
                    alert("您输入的数量小于或等于0，请重新输入");
                    typr = 1;
                    break;
                }
            }
           
        }
        if (DID == "")
        {
            alert("请选择要入库的产品！");
            return;
        }
        $.ajax({
            url: "SavePStockingDetailIn",
            type: "Post",
            data: {
                RKID: RKID, RWID: RWID, StockInTime: StockInTime, FinishTime: FinishTime, HouseID: HouseID, Batch: Batch,
                StockRemark: StockRemark, StockInUser: StockInUser, MainContent: MainContent, PID: PID, OrderContent: OrderContent, SpecsModels: SpecsModels,
                OrderUnit: OrderUnit, OrderNum: OrderNum, Remarks: Remarks, DID: DID, Storekeeper: Storekeeper
            },
            async: false,
            success: function (data) {
                if (data.success == true) {
                    alert("添加成功");
                    window.parent.frames["iframeRight"].Search();
                    setTimeout('parent.ClosePop()', 100);
                }
                else {
                    alert(data.msg);
                }
            }
        });
    });
    
    function jq() { //增加产品信息行
        if (location.search != "") {
            RWID = location.search.split('&')[0].split('=')[1];
        }
        $("#RWID").attr("value", RWID);
        var RKID = $("#RKID").val();
        $.ajax({
            url: "GetReportInfo",
            type: "post",
            single: true,
            data: { RWID: RWID,RKID:RKID },
            dataType: "json",
            success: function (data) {
                var json = eval(data.datas);
                var json = eval(data.datas);
                if (json.length > 0) {
                    for (var i = 0; i < json.length; i++) {
                        rowCount = document.getElementById("DetailInfo").rows.length;
                        var CountRows = parseInt(rowCount) + 1;
                        var html = "";
                        html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                        html += '<td><input  type="checkbox" id="a' + rowCount + '"/></td>'
                        html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                        html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                        html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="SpecsModels' + rowCount + '">' + json[i].SpecsModels + '</lable> </td>';
                        html += '<td ><lable class="labOrderUnit' + rowCount + ' " id="OrderUnit' + rowCount + '">' + json[i].OrderUnit + '</lable> </td>';
                        html += '<td ><input type="text" id="OrderNum' + rowCount + '" style="width:190px;" value="' + json[i].ordernum + '"/></td>';
                        html += '<td ><lable class="labRemark' + rowCount + ' " id="Remarks' + rowCount + '">' + json[i].Remark + '</lable> <lable class="labDID' + rowCount + ' " id="DID' + rowCount + '" style="display:none">' + json[i].DID + '</lable> <lable class="labDID' + rowCount + ' " id="Amount' + rowCount + '" style="display:none">' + json[i].ordernums + '</lable> </td>';
                       
                        html += '</tr>'
                        $("#DetailInfo").append(html);
                    }
                }
            }
        })
    }
})

function selRow(curRow) {
    newRowID = curRow.id;
    $("#DetailInfo tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}