$(document).ready(function () {
    //$("#Formulation").attr("value", Formulation);
    var mydate = new Date();
    var a = mydate.getMonth();
    var b = mydate.getFullYear();
    if (a == 11) {
        $("#Plannedmonth").val(1);
        $("#Plannedyear").val(b + 1);
    }
        //}else if (a = 12) {
        //        $("#Plannedmonth").val(2);
        //    }
    else {
        $("#Plannedmonth").val(a + 2);
        $("#Plannedyear").val(b);
    }
    var date = b;
    var day = mydate.getDate();
    if (a < 9)
        date += "-0" + (a + 1);
    else
        date += "-" + (a + 1);
    if (day < 10)
        date += "-0" + day;
    else
        date += "-" + day;

    $("#Specifieddate").val(date);
    //mydate.getDay
    jq();

    $("#Cancel").click(function () {
        window.parent.ClosePop();
    })

    //$("#btnSave").click(function () {
    //    var JHID = $("#JHID").val();
    //    var Specifieddate = $("#Specifieddate").val();
    //    var Plannedmonth = $("#Plannedmonth").val();
    //    var Remarks = $("#Remarks").val();
    //    var Formulation = $("#Formulation").val();
    //    var Plannedyear = $("#Plannedyear").val();

    //    var MainContent = "";
    //    var Name = "";
    //    var Specifications = "";
    //    var Finishedproduct = "";
    //    var finishingproduct = "";
    //    var finishingproducts = "";
    //    var notavailable = "";
    //    var Total = "";
    //    var plannumber = "";
    //    var demandnumber = "";
    //    var Remark = "";
    //    var PID = "";

    //    var tbody = document.getElementById("DetailInfo");
    //    for (var i = 0; i < tbody.rows.length; i++) {
    //        var mainContent = document.getElementById("RowNumber" + i).innerHTML;
    //        var name = document.getElementById("Name" + i).innerHTML;
    //        var specifications = document.getElementById("Specifications" + i).innerHTML;
    //        var finishedproduct = document.getElementById("Finishedproduct" + i).value;
    //        var Finishingproduct = document.getElementById("finishingproduct" + i).value;
    //        var Finishingproducts = document.getElementById("finishingproducts" + i).value;
    //        var Notavailable = document.getElementById("notavailable" + i).value;
    //        var total = document.getElementById("Total" + i).value;
    //        var Plannumber = document.getElementById("plannumber" + i).value;
    //        var Demandnumber = document.getElementById("demandnumber" + i).value;
    //        var remark = document.getElementById("Remarks" + i).value;
    //        var pID = document.getElementById("PID" + i).innerHTML;


    //        MainContent += mainContent;
    //        Name += name;
    //        Specifications += specifications;
    //        Finishedproduct += finishedproduct;
    //        finishingproduct += Finishingproduct;
    //        finishingproducts += Finishingproducts;
    //        notavailable += Notavailable;
    //        Total += total;
    //        plannumber += Plannumber;
    //        demandnumber += Demandnumber;
    //        Remark += remark;
    //        PID += pID;

    //        if (i < tbody.rows.length - 1) {
    //            MainContent += ",";
    //            Name += ",";
    //            Specifications += ",";
    //            Finishedproduct += ",";
    //            finishingproduct += ",";
    //            finishingproducts += ",";
    //            notavailable += ",";
    //            Total += ",";
    //            plannumber += ",";
    //            demandnumber += ",";
    //            Remark += ",";
    //            PID += ",";
    //        }
    //        else {
    //            MainContent += "";
    //            Name += "";
    //            Specifications += "";
    //            Finishedproduct += "";
    //            finishingproduct += "";
    //            finishingproducts += "";
    //            notavailable += "";
    //            Total += "";
    //            plannumber += "";
    //            demandnumber += "";
    //            Remark += "";
    //            PID += "";
    //        }
    //    }
    //    $.ajax({
    //        url: "SaveZD",
    //        type: "Post",
    //        data: {
    //            JHID: JHID, Specifieddate: Specifieddate, Plannedmonth: Plannedmonth, Remarks: Remarks, Formulation: Formulation,
    //            MainContent: MainContent, Name: Name, Specifications: Specifications, Finishedproduct: Finishedproduct,
    //            finishingproduct: finishingproduct, finishingproducts: finishingproducts, notavailable: notavailable,
    //            Total: Total, plannumber: plannumber, demandnumber: demandnumber, Remark: Remark, PID: PID, Plannedyear: Plannedyear,
    //        },
    //        async: false,
    //        success: function (data) {
    //            if (data.success == true) {
    //                alert("添加成功");
    //                window.parent.ClosePop();
    //            }
    //            else {
    //                alert(data.msg);
    //            }
    //        }
    //    });
    //});
    $("#btnSave").click(function () {

        var tbody = document.getElementById("DetailInfo");
        for (var i = 0; i < tbody.rows.length; i++) {
            var demandnumber = document.getElementById("demandnumber" + i).value;
            if (demandnumber <= 0 || demandnumber == null) {
                alert("计划生产数量不能小于或等于0！");
                return;
            }
            var plannumber = document.getElementById("plannumber" + i).value
            if (plannumber <= 0 || plannumber == null) {
                alert("需求零件数量不能小于或者等于0！");
                return;
            }
        }
        $("#ProductPlanAdd").submit();
    });

    $("#ProductPlanAdd").submit(function () {
        var tbadyrows = $("#DetailInfo > tr").length;
        var options = {
            url: "SaveProductPlan",
            data: { tbadyrows: tbadyrows },
            type: "post",
            async: false,
            success: function (data) {
                if (data.success == true) {
                    alert("添加成功");
                    window.parent.frames["iframeRight"].getSearch();
                    setTimeout('parent.ClosePop()', 100);
                }
                else {
                    alert(data.msg);
                }
            }
        };
        $("#ProductPlanAdd").ajaxSubmit(options);
        return false;
    })





})
function jq() { //增加产品信息行
    //var rowCount = "";
    $.ajax({
        url: "GetZD",
        type: "post",
        single: true,
        //data: { RWID: RWID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            // var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td ><lable id="RowNumber' + rowCount + '">' + CountRows + '</lable> <input type="hidden" name ="PID' + rowCount + '" id ="PID' + rowCount + '" value ="' + json[i].ProductID + '" /></td>';
                    html += '<td ><lable id="Name' + rowCount + '">' + json[i].ProName + '</lable> <input type="hidden"  id="txtName' + rowCount + '" name ="ProName' + rowCount + '" value ="' + json[i].ProName + '" /></td>';
                    html += '<td ><lable  id="Specifications' + rowCount + '">' + json[i].Spec + '</lable><input type="hidden" id="txtSpecifications' + rowCount + '" name ="Specifications' + rowCount + '" value ="' + json[i].Spec + '" /> </td>';
                    //Finishedproduct, finishingproduct, finishingproducts, notavailable, Total
                    html += '<td ><input type="text" style="width:50px;" readOnly="true" id="Finishedproduct' + rowCount + '"  name ="Finishedproduct' + rowCount + '" value ="' + json[i].finishCount + '"/></td>';
                    html += '<td ><input type="text" style="width:50px;" readOnly="true" id="finishingproduct' + rowCount + '" name ="finishingproduct' + rowCount + '" value ="' + json[i].HalfCount + '" > </td>';
                    html += '<td ><input type="text" style="width:50px;" readOnly="true" id="OnlineCount' + rowCount + '" name ="OnlineCount' + rowCount + '" value ="' + json[i].OnlineCount + '"/> </td>';
                    html += '<td ><input type="text" style="width:50px;" readOnly="true" id="lj' + rowCount + '" name ="lj' + rowCount + '" value ="' + json[i].lj + '"/> </td>';

                    html += '<td ><input type="text" style="width:50px;" readOnly="true" id="notavailable' + rowCount + '" name ="notavailable' + rowCount + '" value ="' + json[i].xdnum + '"/> </td>';
                    html += '<td ><input type="text" style="width:50px;" id="Total' + rowCount + '"  name ="Total' + rowCount + '"/> </td>';
                    html += '<td ><input type="text" style="width:50px;" id="plannumber' + rowCount + '" name ="plannumber' + rowCount + '" /> </td>';
                    html += '<td ><input type="text" style="width:50px;" id="demandnumber' + rowCount + '" name ="demandnumber' + rowCount + '"/ > </td>';
                    html += '<td ><input type="text" style="width:150px;" id="Remarks' + rowCount + '" name ="Remarks' + rowCount + '"  /> </td>';
                    html += '<td ><a id="DetailInfos' + rowCount + '" onclick="deleteTr(this);" style="color:bule;cursor:pointer;">删除</a> </td>';
                    //html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '" style="display:none">' + json[i].PID + '</lable> </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);
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

function deleteTr(date) {
    var one = confirm("确认删除");
    if (one == false)
        return;
    else {
        var newCount = date.id;
        var rowNumer = newCount.split("DetailInfos")[1];
        var strRow = newCount.charAt(newCount.length - 1);
        var rowCount = document.getElementById("DetailInfo").rows.length;
        // $("#DetailInfo" + strRow).parent().parent().remove();
        $("#DetailInfo" + rowNumer).closest('tr').remove();
        if (rowCount > rowNumer) {
            var i = parseInt(rowNumer) + 1;
            for (i; i < rowCount; i++) {
                //$("#RowNumber" + i).text(i);
                document.getElementById("RowNumber" + i).innerText = i
                $("#RowNumber" + i).attr("id", "RowNumber" + (i - 1));
                $("#PID" + i).attr("name", "PID" + (i - 1));
                $("#PID" + i).attr("id", "PID" + (i - 1));

                $("#Name" + i).attr("id", "Name" + (i - 1));
                $("#txtName" + i).attr("name", "ProName" + (i - 1));
                $("#txtName" + i).attr("id", "txtName" + (i - 1));

                $("#Specifications" + i).attr("id", "Specifications" + (i - 1));
                $("#txtSpecifications" + i).attr("name", "Specifications" + (i - 1));
                $("#txtSpecifications" + i).attr("id", "txtSpecifications" + (i - 1));

                $("#Finishedproduct" + i).attr("name", "Finishedproduct" + (i - 1));
                $("#Finishedproduct" + i).attr("id", "Finishedproduct" + (i - 1));

                $("#finishingproduct" + i).attr("name", "finishingproduct" + (i - 1));
                $("#finishingproduct" + i).attr("id", "finishingproduct" + (i - 1));

                $("#OnlineCount" + i).attr("name", "OnlineCount" + (i - 1));
                $("#OnlineCount" + i).attr("id", "OnlineCount" + (i - 1));

                $("#lj" + i).attr("name", "lj" + (i - 1));
                $("#lj" + i).attr("id", "lj" + (i - 1));

                $("#notavailable" + i).attr("name", "notavailable" + (i - 1));
                $("#notavailable" + i).attr("id", "notavailable" + (i - 1));

                //$("#finishingproducts" + i).attr("id", "finishingproducts" + (i - 1));

                $("#Total" + i).attr("name", "Total" + (i - 1));
                $("#Total" + i).attr("id", "Total" + (i - 1));

                $("#plannumber" + i).attr("name", "plannumber" + (i - 1));
                $("#plannumber" + i).attr("id", "plannumber" + (i - 1));

                $("#demandnumber" + i).attr("name", "demandnumber" + (i - 1));
                $("#demandnumber" + i).attr("id", "demandnumber" + (i - 1));

                $("#Remarks" + i).attr("name", "Remarks" + (i - 1));
                $("#Remarks" + i).attr("id", "Remarks" + (i - 1));

                $("#DetailInfos" + i).attr("id", "DetailInfos" + (i - 1));
                $("#DetailInfo" + i).attr("id", "DetailInfo" + (i - 1));

            }
        }

    }
}
