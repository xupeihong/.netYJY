$(document).ready(function () {
    if (location.search != "") {
        SGID = location.search.split('&')[0].split('=')[1];
    }
    $("#SGID").attr("value", SGID);
    LoadRDatail();
    $("#btnSaveOrder").click(function () {
        SaveUpdateRDatail();
    });
    $("#btnExit").click(function () {
        window.parent.ClosePop();
    });
});


function LoadRDatail() {
    var RWID = $("#RWID").val();
    $.ajax({
        url: "LoadRDatail",
        type: "post",
        data: { SGID: SGID,RWID:RWID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumbers' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labDID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="SpecsModels' + rowCount + '">' + json[i].SpecsModels + '</lable> </td>';
                    html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="OrderUnit' + rowCount + '">' + json[i].OrderUnit + '</lable> </td>';
                    html += '<td ><input type="text" id="OrderNums' + rowCount + '" style="width:60px;" value="' + json[i].OrderNum + '"/></td>';
                    html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="photo' + rowCount + '">' + json[i].photo + '</lable> </td>';
                    html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="Remarks' + rowCount + '">' + json[i].Remark + '</lable> <lable class="labSpecsModels' + rowCount + ' " id="OrderNumss' + rowCount + '" style="display:none">' + json[i].OrderNumss + '</lable> <lable class="labSpecsModels' + rowCount + ' " id="SGDID' + rowCount + '" style="display:none">' + json[i].SGDID + '</lable><lable class="labSpecsModels' + rowCount + ' " id="d' + rowCount + '" style="display:none">' + json[i].d + '</lable></td>';
                    //html += '<td ><a id="DetailInfo' + rowCount + '" onclick="deleteTr(this);" style="color:blue;cursor:pointer;text-decoration:underline;">删除</a> </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                    CountRows = CountRows + 1;
                }
            }
        }
    })

   
    //$.ajax({
    //    url: "LoadRDatails",
    //    type: "post",
    //    data: { SGID: SGID },
    //    dataType: "json",
    //    success: function (data) {
    //        var json = eval(data.datas);
    //        if (json.length > 0) {
    //            for (var i = 0; i < json.length; i++) {
    //                rowCount = document.getElementById("DetailInfos").rows.length;
    //                var CountRows = parseInt(rowCount) + 1;
    //                var html = "";
    //                html = '<tr  id ="DetailInfos' + rowCount + '" onclick="selRows(this)">'
    //                html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
    //                html += '<td ><input type="text" id="Process' + rowCount + '" style="width:60px;" value="' + json[i].Process + '"/></td>';
    //                html += '<td ><input type="text" id="team' + rowCount + '" style="width:60px;" value="' + json[i].team + '"/></td>';
    //                html += '<td ><input type="text" onclick="WdatePicker()" id="Estimatetime' + rowCount + '" style="width:100px;" value="' + json[i].Estimatetime + '"/></td>';
    //                html += '<td ><input type="text" id="person' + rowCount + '" style="width:60px;" value="' + json[i].person + '"/></td>';
    //                html += '<td ><input type="text" id="plannumber' + rowCount + '" style="width:30px;" value="' + json[i].plannumber + '"/></td>';
    //                html += '<td ><input type="text" id="Qualified' + rowCount + '" style="width:30px;" value="' + json[i].Qualified + '"/></td>';
    //                html += '<td ><input type="text" id="number' + rowCount + '" style="width:30px;" value="' + json[i].number + '"/></td>';
    //                html += '<td ><input type="text" id="numbers' + rowCount + '" style="width:30px;" value="' + json[i].numbers + '"/></td>';
    //                html += '<td ><input type="text" id="Fnubers' + rowCount + '" style="width:30px;" value="' + json[i].Fnubers + '"/></td>';
    //                html += '<td ><input type="text" onclick="WdatePicker()" id="finishtime' + rowCount + '" style="width:100px;" value="' + json[i].finishtime + '"/></td>';
    //                html += '<td ><input type="text" id="people' + rowCount + '" style="width:60px;" value="' + json[i].people + '"/></td>';
    //                html += '<td ><input type="text" id="reason' + rowCount + '" style="width:130px;" value="' + json[i].reason + '"/></td>';
    //                html += '<td ><a id="DetailInfos' + rowCount + '" onclick="deleteTrs(this);" style="color:blue;cursor:pointer;text-decoration:underline;">删除</a> </td>';
    //                html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="DID' + rowCount + '" style="display:none">' + json[i].DID + '</lable> </td>';
    //                html += '</tr>'
    //                $("#DetailInfos").append(html);
    //                CountRows = CountRows + 1;
    //            }
    //        }
    //    }
    //})
}

function selRow(curRow) {
    newRowID = curRow.id;
    $("#DetailInfo tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}

function selRows(curRow) {
    newRowID = curRow.id;
    $("#DetailInfos tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}

//function deleteTr(curRow) {
//    var one = confirm("确认删除");
//    if (one == false)
//        return;
//    else {
//        newRowID = curRow.id;
//        $("#DetailInfo tr").removeAttr("class");
//        $("#" + newRowID).attr("class", "RowClass");
//        var tbodyID = "DetailInfo";
//        var rowIndex = -1;
//        var typeNames = ["RowNumber", "PID", "OrderContent", "SpecsModels", "OrderUnit", "OrderNums", "photo", "Remarks", "DetailInfo"];
//        if (newRowID != "")
//            rowIndex = newRowID.replace(tbodyID, '');
//        if (rowIndex != -1) {
//            document.getElementById(tbodyID).deleteRow(rowIndex);
//            //var a = $("#" + tbodyID + " tr").length;
//            if (rowIndex < $("#" + tbodyID + " tr").length) {
//                for (var i = rowIndex; i < $("#" + tbodyID + " tr").length; i++) {
//                    // var b = parseInt(i);
//                    var tr = document.getElementById(tbodyID + (parseInt(i) + 1));
//                    var tr1 = $("#" + tbodyID + (parseInt(i) + 1));
//                    tr.id = tbodyID + i;
//                    for (var j = 0; j < tr.childNodes.length; j++) {
//                        var html = tr1.html();
//                        for (var k = 0; k < typeNames.length; k++) {
//                            var olPID = typeNames[k] + (parseInt(i) + 1);
//                            var newid = typeNames[k] + i;
//                            var reg = new RegExp(olPID, "g");
//                            html = html.replace(reg, newid);
//                        }
//                        tr1.html(html);//.toLocaleLowerCase());//replace('TD','td'));
//                    }
//                    document.getElementById("RowNumber" + i).innerHTML = parseInt(i) + 1;
//                    // $("#RowNumber" + i).html(parseInt(i) + 1);
//                }
//            }
//            if (document.getElementById(tbodyID).rows.length > 0) {
//                if (rowIndex == document.getElementById(tbodyID).rows.length)
//                    selRow(document.getElementById(tbodyID + (rowIndex - 1)), '');
//                else
//                    selRow(document.getElementById(tbodyID + rowIndex), '');;
//            }
//        }
//        $("#DetailInfo tr").removeAttr("class");
//    }
//}

function deleteTrs(curRow) {
    var one = confirm("确认删除");
    if (one == false)
        return;
    else {
        newRowID = curRow.id;
        $("#DetailInfo tr").removeAttr("class");
        $("#" + newRowID).attr("class", "RowClass");
        var tbodyID = "DetailInfos";
        var rowIndex = -1;
        var typeNames = ["RowNumber", "Process", "team", "Estimatetime", "person", "plannumber", "Qualified", "number", "numbers", "Fnubers", "finishtime", "people", "reason", "DetailInfos","DID"];
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
                    selRows(document.getElementById(tbodyID + (rowIndex - 1)), '');
                else
                    selRows(document.getElementById(tbodyID + rowIndex), '');;
            }
        }
        $("#DetailInfos tr").removeAttr("class");
    }
}


function SaveUpdateRDatail()
{
        
        var MainContents = "";
        var DIDs = "";
        var OrderNums = "";
        var type = 0;
        var m = 0;

        var tbody = document.getElementById("DetailInfo");
        for (var i = 0; i < tbody.rows.length; i++) {
            var mainContents = document.getElementById("RowNumbers" + i).innerHTML;
            var orderNums = document.getElementById("OrderNums" + i).value;
            var orderNumss = document.getElementById("OrderNumss" + i).innerHTML;
            var d = document.getElementById("d" + i).innerHTML;
            var dIDs = document.getElementById("SGDID" + i).innerHTML;
            if (orderNums == "" || orderNums == null)
            {
                alert("需生成随工单的数量不能为空！");
                return;
            }
            var t = Number(Number(d) +Number(orderNumss));
            if (orderNums > 0) {
                if (orderNums <= t) {
                    MainContents += mainContents;
                    OrderNums += orderNums;
                    DIDs += dIDs;
                        if (i < tbody.rows.length - 1) {
                            MainContents += ",";
                            OrderNums += ",";
                            DIDs += ",";
                        } else {
                            MainContents += "";
                            OrderNums += "";
                            DIDs += "";
                        }
                  
                } else {
                    alert("产品数量大于任务单数量！");
                    type = 1;
                    break;
                }
            } else {
                alert("'" + orderNums + "'小于或等于0，请重新输入");
                type = 1;
                break;
            }
        }
        if (type!=1) {
            getsave(OrderNums,MainContents,DIDs);
        }
}

function getsave(OrderNums,MainContents,DIDs)
{
    var SGID = $("#SGID").val();
    var RWID = $("#RWID").val();
    var ID = $("#ID").val();
    var SpecsModels = $("#SpecsModels").val();
    var billing = $("#billing").val();
    var OrderContent = $("#OrderContent").val();
    var Remark = $("#Remark").val();
    var CreateUser = $("#CreateUser").val();

   
    //var MainContent = "";
    //var Process = "";
    //var Team = "";
    //var Estimatetime = "";
    //var Person = "";
    //var Plannumber = "";
    //var Qualified = "";
    //var Number = "";
    //var Numbers = "";
    //var Fnubers = "";
    //var Finishtime = "";
    //var People = "";
    //var Reason = "";
    //var DID = "";


    //var a = 0;
    //var tbody = document.getElementById("DetailInfos");
    //for (var i = 0; i < tbody.rows.length; i++) {
    //    var mainContent = document.getElementById("RowNumber" + i).innerHTML;
    //    var process = document.getElementById("Process" + i).value;
    //    var team = document.getElementById("team" + i).value;
    //    var estimatetime = document.getElementById("Estimatetime" + i).value;
    //    var person = document.getElementById("person" + i).value;
    //    var plannumber = document.getElementById("plannumber" + i).value;
    //    if (plannumber == "" || plannumber == null)
    //    {
    //        plannumber = 0;
    //    }
    //    var qualified = document.getElementById("Qualified" + i).value;
    //    a = parseInt(a) + parseInt(qualified);
    //    if (a > OrderNums)
    //    {
    //        alert("随工记录数量大于随工数量");
    //        return;
    //    }
    //    if (qualified == "" || qualified == null) {
    //        qualified = 0;
    //    }
    //    var number = document.getElementById("number" + i).value;
    //    if (number == "" || number == null) {
    //        number = 0;
    //    }
    //    var numbers = document.getElementById("numbers" + i).value;
    //    if (numbers == "" || numbers == null) {
    //        numbers = 0;
    //    }
    //    var fnubers = document.getElementById("Fnubers" + i).value;
    //    if (fnubers == "" || fnubers == null) {
    //        fnubers = 0;
    //    }
    //    var finishtime = document.getElementById("finishtime" + i).value;
    //    if (finishtime =="" || finishtime == null) {
    //        alert("完成时间不允许为空！");
    //        return;
    //    }
    //    var people = document.getElementById("people" + i).value;
    //    var reason = document.getElementById("reason" + i).value;
    //    var did = document.getElementById("DID" + i).innerHTML;

    //    MainContent += mainContent;
    //    Process += process;
    //    Team += team;
    //    Estimatetime += estimatetime;
    //    Person += person;
    //    Plannumber += plannumber;
    //    Qualified += qualified;
    //    Number += number;
    //    Numbers += numbers;
    //    Fnubers += fnubers;
    //    Finishtime += finishtime;
    //    People += people;
    //    Reason += reason;
    //    DID += did;

    //    if (i < tbody.rows.length - 1) {

    //        MainContent += ",";
    //        Process += ",";
    //        Team += ",";
    //        Estimatetime += ",";
    //        Person += ",";
    //        Plannumber += ",";
    //        Qualified += ",";
    //        Number += ",";
    //        Numbers += ",";
    //        Fnubers += ",";
    //        Finishtime += ",";
    //        People += ",";
    //        Reason += ",";
    //        DID += ",";
    //    }
    //    else {

    //        MainContent += "";
    //        Process += "";
    //        Team += "";
    //        Estimatetime += "";
    //        Person += "";
    //        Plannumber += "";
    //        Qualified += "";
    //        Number += "";
    //        Numbers += "";
    //        Fnubers += "";
    //        Finishtime += "";
    //        People += "";
    //        Reason += "";
    //        DID += "";
    //    }
    //}
    $.ajax({
        url: "SaveUpdateRDatail",
        type: "Post",
        data: {
            SGID: SGID, RWID: RWID, ID: ID, SpecsModels: SpecsModels,
            billing: billing, OrderContent: OrderContent, Remark: Remark, CreateUser: CreateUser,
            MainContents: MainContents, DIDs: DIDs, OrderNums: OrderNums,
            //MainContent: MainContent,
            //Process: Process, Team: Team, Estimatetime: Estimatetime, Person: Person,
            //Plannumber: Plannumber, Qualified: Qualified, Number: Number, Numbers: Numbers,
            //Fnubers: Fnubers, Finishtime: Finishtime, People: People, Reason: Reason,DID:DIDhhh
        },
        async: false,
        success: function (data) {
            if (data.success == true) {
                alert("修改随工单成功！");
                window.parent.frames["iframeRight"].Search();
                setTimeout('parent.ClosePop()', 100);
            }
            else {
                alert("修改随工单失败-" + data.msg);
            }
        }
    });
}