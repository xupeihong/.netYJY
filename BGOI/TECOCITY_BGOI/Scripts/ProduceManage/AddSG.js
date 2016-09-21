$(document).ready(function () {
    var date = new Date();
    var yy = date.getYear();
    if (yy < 1900) yy = yy + 1900;
    var MM = date.getMonth() + 1;
    if (MM < 10) MM = '0' + MM;
    var dd = date.getDate();
    if (dd < 10) dd = '0' + dd;
    $("#billing").val(yy + "-" + MM + "-" + dd);

    jq();
    $("#Cancel").click(function () {
        window.parent.ClosePop();
    })

    $("#btnSave").click(function () {

        var MainContent = "";
        var OrderNums = "";
        var PID = "";
        var DID = "";
        var type = 0;
        var m = 0;
        var n = 0;
        var a = 0;

        var tbody = document.getElementById("DetailInfo");
        for (var i = 0; i < tbody.rows.length; i++) {
            var ch = document.getElementById("Check" + i);
            if (ch.checked) {
                a += 1;
                var did = document.getElementById("DID" + i).innerHTML;
            }

        }

        var aa = "";
        var cbNum = document.getElementsByName("cb");
        for (var i = 0; i < cbNum.length; i++) {
            var cbid = "";
            if (cbNum[i].checked == true) {
                cbid = cbNum[i].id;
                aa += cbid.substring(5) + ",";
            }
        }

        var arr1 = aa.split(',');
        for (var i = 0; i < arr1.length - 1; i++) {
            var mainContent = document.getElementById("RowNumber" + arr1[i]).innerHTML;
            var orderNums = document.getElementById("OrderNum" + arr1[i]).value;
            if (orderNums == "" || orderNums == null) {
                alert("请输入需领料的数量");
                break;
            }
            var pID = document.getElementById("PID" + arr1[i]).innerHTML;
            var dID = document.getElementById("DID" + arr1[i]).innerHTML;

            var Amount = document.getElementById("Amount" + arr1[i]).innerHTML;

            var orderNums = Number(orderNums);
            var Amount = Number(Amount);
            if (orderNums > 0) {
                if (orderNums <= Amount) {
                    MainContent += mainContent;
                    OrderNums += orderNums;
                    PID += pID;
                    DID += dID;
                    if (i < arr1.length - 1) {
                        MainContent += ",";
                        OrderNums += ",";
                        PID += ",";
                        DID += ",";
                    }
                    else {
                        MainContent += "";
                        OrderNums += "";
                        PID += "";
                        DID += "";
                    }
                }
                else {
                    alert("'" + orderNums + "'数量大于任务单数量");
                    type = 1;
                    break;
                }
            }
            else {
                alert("'" + orderNums + "'小于或等于0，请重新输入");
                m = 1;
                break;
            }
        }
          
            if (DID == "") {
                alert("请选择产品！");
                return;
            }
            if (type != 1 && m != 1 && n != 1) {
                getsave(MainContent, OrderNums, PID, DID);
            }
        
    });

    function getsave(MainContent, OrderNums, PID, DID) {
        var SGID = $("#SGID").val();
        var RWID = $("#RWID").val();
        var ID = $("#ID").val();

        var billing = $("#billing").val();
        var Remark = $("#Remark").val();
        var CreateUser = $("#CreateUser").val();
        var HouseID = $("#HouseID").val();


        if (ID == "" || ID == null) {
            alert("编号不能为空");
            return;
        }
        if (billing == "" || billing == null) {
            alert("发单日期不能为空");
            return;
        }
        if (HouseID == "" || HouseID == null)
        {
            alert("库房不能为空！");
            return;
        }

        $.ajax({
            url: "SaveProductRecordIn",
            type: "Post",
            data: {
                SGID: SGID, RWID: RWID, ID: ID, billing: billing,
                Remark: Remark, CreateUser: CreateUser,
                MainContent: MainContent, OrderNums: OrderNums, PID: PID, DID: DID, HouseID: HouseID
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
    }


    function jq() { //增加产品信息行
        if (location.search != "") {
            RWID = location.search.split('&')[0].split('=')[1];
        }
        $("#RWID").attr("value", RWID);

        $.ajax({
            url: "GetProductRecord",
            type: "post",
            data: { RWID: RWID },
            dataType: "json",
            success: function (data) {
                var json = eval(data.datas);
                if (json.length > 0) {
                    for (var i = 0; i < json.length; i++) {
                        rowCount = document.getElementById("DetailInfo").rows.length;
                        var CountRows = parseInt(rowCount) + 1;
                        var html = "";
                        html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                        html += '<td><input  type="checkbox" id="Check' + rowCount + '" name="cb"/></td>'
                        html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                        html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '"style="width:50px;" >' + json[i].PID + '</lable> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="OrderContent' + rowCount + '"style="width:50px;" >' + json[i].OrderContent + '</lable> </td>';
                        html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="SpecsModels' + rowCount + '"style="width:50px;" >' + json[i].SpecsModels + '</lable> </td>';
                        html += '<td ><lable class="labOrderUnit' + rowCount + ' " id="OrderUnit' + rowCount + '"style="width:30px;" >' + json[i].OrderUnit + '</lable> </td>';
                        html += '<td ><input type="text" id="OrderNum' + rowCount + '" style="width:40px;" value="' + json[i].OrderNum + '"/></td>';
                        html += '<td ><lable class="labphoto' + rowCount + ' " id="photo' + rowCount + '"style="width:60px;" >' + json[i].photo + '</lable> </td>';
                        html += '<td ><lable class="labRemark' + rowCount + ' " id="Remark' + rowCount + '"style="width:80px;" >' + json[i].Remark + '</lable><lable class="labDID' + rowCount + ' " id="DID' + rowCount + '" style="display:none">' + json[i].DID + '</lable><lable class="labDID' + rowCount + ' " id="Amount' + rowCount + '" style="display:none">' + json[i].Amount + '</lable> </td>';

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
