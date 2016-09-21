$(document).ready(function () {
    if (location.search != "") {
        RKID = location.search.split('&')[0].split('=')[1];
    }
    $("#RKID").attr("value", RKID);
    LoadposDatail();
    $("#btnSaveOrder").click(function () {
        SaveUpdateposDatail();
    });
    $("#btnExit").click(function () {
        window.parent.ClosePop();
    });
});


function LoadposDatail() {
    var RWID = $("#RWID").val();
    $.ajax({
        url: "LoadRposDatail",
        type: "post",
        data: { RKID: RKID,RWID:RWID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labDID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td ><lable class="labDID' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labDID' + rowCount + ' " id="Specifications' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                    html += '<td ><lable class="labDID' + rowCount + ' " id="Unit' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                    html += '<td ><input type="text" id="Amount' + rowCount + '" style="width:100px;" value="' + json[i].Amount + '"/></td>';
                    html += '<td ><lable class="labDID' + rowCount + ' " id="Remark' + rowCount + '">' + json[i].Remark + '</lable>  <lable class="labDID' + rowCount + ' " id="DID' + rowCount + '"style="display:none">' + json[i].DID + '</lable> <lable class="labOrderNum' + rowCount + ' " id="OrderNum' + rowCount + '"style="display:none">' + json[i].ordernum + '</lable><lable class="labOrderNum' + rowCount + ' " id="a' + rowCount + '"style="display:none">' + json[i].a + '</lable><lable class="labOrderNum' + rowCount + ' " id="m' + rowCount + '"style="display:none">' + json[i].m + '</lable></td>';
                    //html += '<td ><a id="DetailInfo' + rowCount + '" onclick="deleteTr(this);" style="color:blue;cursor:pointer;text-decoration:underline;">删除</a> </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                    CountRows = CountRows + 1;
                }
            }
        }
    })

}

function selRow(curRow) {
    newRowID = curRow.id;
    $("#DetailInfo tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}

function SaveUpdateposDatail() {
    var RKID = $("#RKID").val();
    var RWID = $("#RWID").val();
    var StockInTime = $("#StockInTime").val();
    var FinishTime = $("#FinishTime").val();
    var HouseID = $("#HouseID").val();
    var Batch = $("#Batch").val();
    var StockRemark = $("#StockRemark").val();

   
    var MainContent = "";
    var PID = "";
    var OrderContent = "";
    var Specifications = "";
    var Supplier = "";
    var Unit = "";
    var Amount = "";
    var Remark = "";
    var DID = "";
    var type = 0;
    var M = "";

    var tbody = document.getElementById("DetailInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
        var mainContent = document.getElementById("RowNumber" + i).innerHTML;
        var amount = document.getElementById("Amount" + i).value;
        if (amount == "" || amount == null)
        {
            alert("交货数量不能为空");
            break;
        }
        var dID = document.getElementById("DID" + i).innerHTML;
        var OrderNum = document.getElementById("OrderNum" + i).innerHTML;
        var a = document.getElementById("a" + i).innerHTML;
        var m = document.getElementById("m" + i).innerHTML;
        var n = Number(a) + Number(OrderNum);
        if (amount > 0) {
            if (amount <= Number(n)) {
                MainContent += mainContent;
                Amount += amount;
                DID += dID;
                M += m;

                if (i < tbody.rows.length - 1) {
                    MainContent += ",";
                    Amount += ",";
                    DID += ",";
                    M += ",";
                }
                else {
                    MainContent += "";
                    Amount += "";
                    DID += "";
                    M += "";
                }
            } else {
                alert("'" + amount + "'数量大于任务单数量,剩余数量为'"+n+"'");
                type = 1;
                break;
            }
        } else {
            alert("'" + amount + "'小于或等于0，请重新输入");
            type = 1;
            break;
        }
    }
        if (type != 1)
        {
            getsave(RKID, RWID, StockInTime, FinishTime, HouseID, Batch, StockRemark, MainContent, Amount, DID,M);
        }
    
}

function getsave(RKID, RWID, StockInTime, FinishTime, HouseID, Batch, StockRemark, MainContent, Amount, DID,M)
{
    $.ajax({
        url: "SaveUpdateposDetail",
        type: "Post",
        data: {
            RKID: RKID, RWID: RWID, StockInTime: StockInTime, FinishTime: FinishTime,
            HouseID: HouseID, Batch: Batch, StockRemark: StockRemark,
            MainContent: MainContent,
            Amount: Amount,DID:DID,M:M
        },
        async: false,
        success: function (data) {
            if (data.success == true) {
                alert("修改任务单成功！");
                window.parent.frames["iframeRight"].Search();
                setTimeout('parent.ClosePop()', 100);
            }
            else {
                alert("修改任务单失败-" + data.msg);
            }
        }
    });
}




