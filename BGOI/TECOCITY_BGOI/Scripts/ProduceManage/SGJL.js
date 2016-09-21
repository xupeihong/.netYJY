$(document).ready(function () {
    if (location.search != "") {
        SGID = location.search.split('&')[0].split('=')[1];
    }
    $("#SGID").attr("value", SGID);
    LoadRDetail();
    $("#btnSave").click(function () {
    SaveRDetail();
    });
    $("#Cancel").click(function () {
        window.parent.ClosePop();
    });
});

function LoadRDetail() { //增加产品信息行
    $.ajax({
        url: "GetProductdetail",
        type: "post",
        single:true,
        data: { SGID: SGID },
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
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumbers' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="SpecsModels' + rowCount + '">' + json[i].SpecsModels + '</lable> </td>';
                    html += '<td ><lable class="labOrderUnit' + rowCount + ' " id="OrderUnit' + rowCount + '">' + json[i].OrderUnit + '</lable> </td>';
                    html += '<td ><lable class="labOrderNum' + rowCount + ' " id="OrderNum' + rowCount + '">' + json[i].OrderNum + '</lable> </td>';
                    html += '<td ><lable class="labphoto' + rowCount + ' " id="photo' + rowCount + '">' + json[i].photo + '</lable> </td>';
                    html += '<td ><lable class="labRemark' + rowCount + ' " id="Remarks' + rowCount + '">' + json[i].Remark + '</lable> </td>';
                    html += '<td ><lable class="labRemark' + rowCount + ' "style="display:none" id="DID' + rowCount + '">' + json[i].DID + '</lable> </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                }
            }
                
        }
    })

}

 function SaveRDetail() {
        if (location.search != "") {
            SGID = location.search.split('&')[0].split('=')[1];
        }
        var DID = "";
        var A = "";
    var tbody = document.getElementById("DetailInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
        var dID = document.getElementById("DID" + i).innerHTML;
        var a = document.getElementById("RowNumbers" + i).innerHTML;
        var b=document.getElementById("OrderNum" + i).innerHTML;
        DID += dID;
        A += a;
        if (i < tbody.rows.length - 1) {
            DID += ",";
            A += ",";
        }
        else {
            DID += "";
            A += "";
        }
    }


    var RWID = $("#RWID").val();

    var MainContent = "";
    var Process = "";
    var team = "";
    var Estimatetime = "";
    var person = "";
    var plannumber = "";
    var Qualified = "";
    var number = "";
    var numbers = "";
    var Fnubers = "";
    var finishtime = "";
    var people = "";
    var reason = "";
    var Technical = "";

    var tbody = document.getElementById("DetailInfos");
    var a = $("#m").val();
    if (a == "" || a == null)
    {
        a = 0;
    }
    for (var i = 0; i < tbody.rows.length; i++) {
        var mainContent = document.getElementById("RowNumber" + i).innerHTML;
        var process = document.getElementById("Process" + i).value;
        var Team = document.getElementById("team" + i).value;
        var estimatetime = document.getElementById("Estimatetime" + i).value;
        var Person = document.getElementById("person" + i).value;
        var Plannumber = document.getElementById("plannumber" + i).value;
        if (Plannumber == "" || Plannumber == null)
        {
            Plannumber = 0;
        }
        var qualified = document.getElementById("Qualified" + i).value;
        var a = parseInt(a) + parseInt(qualified);
        if (a > b)
        {
            alert("随工记录数量大于随工单数量");
            return;
        }
        if (qualified == "" || qualified == null) {
            qualified = 0;
        }
        var Number = document.getElementById("number" + i).value;
        if (Number == "" || Number == null) {
            Number = 0;
        }
        var Numbers = document.getElementById("numbers" + i).value;
        if (Numbers == "" || Numbers == null) {
            Numbers = 0;
        }
        var fnubers = document.getElementById("Fnubers" + i).value;
        if (fnubers == "" || fnubers == null) {
            fnubers = 0;
        }
        var Finishtime = document.getElementById("finishtime" + i).value;
        if (Finishtime == "" || Finishtime == null) {
            alert("完成日期不能为空");
            return;
        }
        var People = document.getElementById("people" + i).value;
        var Reason = document.getElementById("reason" + i).value;
        var technical = document.getElementById("Technical" + i).value;

        MainContent += mainContent;
        Process += process;
        team += Team;
        Estimatetime += estimatetime;
        person += Person;
        plannumber += Plannumber;
        Qualified += qualified;
        number += Number;
        numbers += Numbers;
        Fnubers += fnubers;
        finishtime += Finishtime;
        people += People;
        reason += Reason;
        Technical += technical;

        if (i < tbody.rows.length - 1) {
            MainContent += ",";
            Process += ",";
            team += ",";
            Estimatetime += ",";
            person += ",";
            plannumber += ",";
            Qualified += ",";
            number += ",";
            numbers += ",";
            Fnubers += ",";
            finishtime += ",";
            people += ",";
            reason += ",";
            Technical += ",";
        }
        else {
            MainContent += "";
            Process += "";
            team += "";
            Estimatetime += "";
            person += "";
            plannumber += "";
            Qualified += "";
            number += "";
            numbers += "";
            Fnubers += "";
            finishtime += "";
            people += "";
            reason += "";
            Technical += "";
        }
    }

    $.ajax({
        url: "SaveProductRDatail",
        type: "Post",
        data: {
            MainContent: MainContent, Process: Process, team: team, Estimatetime: Estimatetime,SGID:SGID,
            person: person, plannumber: plannumber, Qualified: Qualified, number: number,
            numbers: numbers, Fnubers: Fnubers, finishtime: finishtime, people: people, reason: reason,Technical:Technical,
            DID: DID, RWID: RWID, A: A
        },
        async: false,
        success: function (data) {
            if (data.success == true) {
                alert("添加成功");
                window.parent.ClosePop();
            }
            else {
                alert(data.msg);
            }
        }
    });
}



function Add() {
         rowCount = document.getElementById("DetailInfos").rows.length;
         var CountRows = parseInt(rowCount) + 1;
         $.ajax({
          url: "GetSGJLType",
           type: "post",
           single: true,
           dataType: "json",
           success: function (data) {
            var json = eval(data.datas);
            var json = eval(data.datas);
             if (json.length > 0) {
                    var html = "";
                    html = '<tr  id ="DetailInfos' + rowCount + '" onclick="selRows(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><input type="text" style="width:50px;" id="Process' + rowCount + '"  /> </td>';
                    html += '<td ><select id="team' + rowCount + '" name="team' + rowCount + '"></select> </td>';
                    html += '<td ><input type="text" style="width:80px;" onclick="WdatePicker()" id="Estimatetime' + rowCount + '"  /> </td>';
                    html += '<td ><input type="text" style="width:50px;" id="person' + rowCount + '"  /> </td>';
                    html += '<td ><input type="text" style="width:30px;" id="plannumber' + rowCount + '"  / > </td>';
                    html += '<td ><input type="text" style="width:30px;" id="Qualified' + rowCount + '"  /> </td>';
                    html += '<td ><input type="text" style="width:30px;" id="number' + rowCount + '"   /> </td>';
                    html += '<td ><input type="text" style="width:30px;" id="numbers' + rowCount + '"   /> </td>';
                    html += '<td ><input type="text" style="width:30px;" id="Fnubers' + rowCount + '"  /> </td>';
                    html += '<td ><input type="text" style="width:80px;" onclick="WdatePicker()" id="finishtime' + rowCount + '" / > </td>';
                    html += '<td ><input type="text" style="width:50px;" id="people' + rowCount + '" / > </td>';
                    html += '<td ><input type="text" style="width:100px;" id="reason' + rowCount + '"   /> </td>';
                    html += '<td ><input type="text" style="width:50px;" id="Technical' + rowCount + '"   /> </td>';
                    html += '<td ><a id="DetailInfos' + rowCount + '" onclick="deleteTr(this);" style="color:blue;cursor:pointer;text-decoration:underline;">删除</a> </td>';
                    html += '</tr>'
                    $("#DetailInfos").append(html);
                }
                for (var i = 0; i < json.length; i++) {
                    jQuery("#team" + rowCount).append("<option value=" + json[i].Text + ">" + json[i].Text + "</option>");
                }
            }
      })
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
function deleteTr(curRow) {
    newRowID = curRow.id;
    var one = confirm("确认删除");
    if (one == false)
        return;
    else {
        var tbodyID = "DetailInfos";
        var rowIndex = -1;
        var typeNames = ["RowNumber", "Process", "team", "Estimatetime", "person", "plannumber", "Qualified", "number", "numbers", "Fnubers", "finishtime", "people", "reason", "DetailInfos"];
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
                    var c = parseInt(i) + 1;
                    document.getElementById("RowNumber" + i).innerHTML = parseInt(i) + 1;
                    // $("#RowNumber" + i).html(parseInt(i) + 1);
                }
            }
            if (document.getElementById(tbodyID).rows.length > 0) {
                if (rowIndex == document.getElementById(tbodyID).rows.length)
                    selRows(document.getElementById(tbodyID + (rowIndex - 1)), '');
                else
                    selRows(document.getElementById(tbodyID + rowIndex), '');
            }
        }
        $("#DetailInfos tr").removeAttr("class");
    }
}

