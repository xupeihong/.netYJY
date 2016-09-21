$(document).ready(function () {
    if (location.search != "") {
        JHID = location.search.split('&')[0].split('=')[1];
    //    DID = location.search.split('&')[1].split('=')[1];
    }
    $("#Specifieddate").val($("#date").val());
    LoadplanDatail();
    $("#btnSave").click(function () {
        $("#updateplan").submit();
    });
    $("#Cancel").click(function () {
        window.parent.ClosePop();
    });

    $("#updateplan").submit(function () {
        var tbadyrows = $("#DetailInfo > tr").length;
        var options = {
            url: "SaveUpdatePlan",
            data: { tbadyrows: tbadyrows },
            type: "post",
            async: false,
            success: function (data) {
                if (data.success == true) {
                    alert("保存成功");
                    window.parent.frames["iframeRight"].getSearch();
                    setTimeout('parent.ClosePop()', 100);
                }
                else {
                    alert(data.Msg);
                }
            }
        };
        $("#updateplan").ajaxSubmit(options);
        return false;
    })

});


function LoadplanDatail() {
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    if (location.search != "") {
        JHID = location.search.split('&')[0].split('=')[1];
        //DID  = location.search.split('&')[1].split('=')[1];
    }
    $.ajax({
        url: "LoadPlanDatail",
        type: "post",
        data: { JHID: JHID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td ><lable id="RowNumber' + rowCount + '">' + CountRows + '</lable> <input type="hidden" name ="PID' + rowCount + '" id ="PID' + rowCount + '" value ="' + json[i].PID + '" /></td>';
                    html += '<td ><lable id="Name' + rowCount + '">' + json[i].Name + '</lable><input type="hidden"  id="txtName' + rowCount + '" name ="Name' + rowCount + '" value ="' + json[i].Name + '" /> </td>';
                    html += '<td ><lable id="Specifications' + rowCount + '">' + json[i].Specifications + '</lable> <input type="hidden" id="txtSpecifications' + rowCount + '" name ="Specifications' + rowCount + '" value ="' + json[i].Specifications + '" /> </td>';

                    //Finishedproduct, finishingproduct, finishingproducts, notavailable, Total
                    html += '<td ><input type="text" style="width:50px;"readOnly="true" id="Finishedproduct' + rowCount + '" name ="Finishedproduct' + rowCount + '"  value="' + json[i].Finishedproduct + '" > </td>';

                    html += '<td ><input type="text" style="width:50px;"readOnly="true" id="finishingproduct' + rowCount + '" name ="finishingproduct' + rowCount + '"  value="' + json[i].finishingproduct + '" > </td>';

                    html += '<td ><input type="text" style="width:50px;"readOnly="true" id="OnlineCount' + rowCount + '" name ="OnlineCount' + rowCount + '" value ="' + json[i].OnlineCount + '"/> </td>';

                    html += '<td ><input type="text" style="width:50px;"readOnly="true" id="Spareparts' + rowCount + '" name="Spareparts' + rowCount + '" value="' + json[i].Spareparts + '" > </td>';;
                    html += '<td ><input type="text" style="width:50px;"readOnly="true" id="notavailable' + rowCount + '" name ="notavailable' + rowCount + '" value="' + json[i].notavailable + '" > </td>';
                    html += '<td ><input type="text" style="width:50px;" id="Total' + rowCount + '" name ="Total' + rowCount + '"  value="' + json[i].Total + '"> </td>';
                    html += '<td ><input type="text" style="width:50px;" id="plannumber' + rowCount + '" name ="plannumber' + rowCount + '"  value="' + json[i].plannumber + '"> </td>';
                    html += '<td ><input type="text" style="width:50px;" id="demandnumber' + rowCount + '" name ="demandnumber' + rowCount + '" value="' + json[i].demandnumber + '" > </td>';
                    html += '<td ><input type="text" style="width:150px;" id="Remarks' + rowCount + '"  name ="Remarks' + rowCount + '"  value="' + json[i].Remarks + '" > </td>';
                    html += '<td > <a id="DetailInfos' + rowCount + '" onclick="deleteTr(this);" style="color:bule;cursor:pointer;">删除</a><input type="hidden"  id="txtCreateTime' + rowCount + '" name ="CreateTime' + rowCount + '" value ="' + json[i].CreateTime + '" /><input type="hidden"  id="txtCreateUser' + rowCount + '" name ="CreateUser' + rowCount + '" value ="' + json[i].CreateUser + '" /></td>';
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

function SaveUpdateplanDatail(){
    if (location.search != "") {
        JHID = location.search.split('&')[0].split('=')[1];
        //DID = location.search.split('&')[1].split('=')[1];
    }

        var Specifieddate = $("#Specifieddate").val();
        var Plannedmonth = $("#Plannedmonth").val();
        var Remarks = $("#Remarks").val();
        var Formulation = $("#Formulation").val();
       
        var MainContent = "";
        var Name = "";
        var Specifications = "";
        var Finishedproduct = "";
        var finishingproduct = "";
        var finishingproducts = "";
        var notavailable = "";
        var Total = "";
        var plannumber = "";
        var demandnumber = "";
        var Remark = "";
        var DID="";

        var tbody = document.getElementById("DetailInfo");
        for (var i = 0; i < tbody.rows.length; i++) {
            var mainContent = tbody.getElementsByTagName("tr")[0].cells[0].innerText;// document.getElementById("RowNumber" + i).innerHTML;
            var name = tbody.getElementsByTagName("tr")[0].cells[1].innerText; //document.getElementById("Name" + i).innerHTML;
            var specifications = tbody.getElementsByTagName("tr")[0].cells[2].innerText;// document.getElementById("Specifications" + i).innerHTML;
            var finishedproduct = document.getElementById("Finishedproduct" + i).value;
            var Finishingproduct = document.getElementById("finishingproduct" + i).value;
            var Finishingproducts = document.getElementById("finishingproducts" + i).value;
            var Notavailable = document.getElementById("notavailable" + i).value;
            var total = document.getElementById("Total" + i).value;
            var Plannumber =  document.getElementById("plannumber" + i).value;
            var Demandnumber = document.getElementById("demandnumber" + i).value;
            var remark = document.getElementById("Remark" + i).value;
            var dID= document.getElementById("DID" + i).innerHTML;


            MainContent += mainContent;
            Name += name;
            Specifications += specifications;
            Finishedproduct += finishedproduct;
            finishingproduct += Finishingproduct;
            finishingproducts += Finishingproducts;
            notavailable += Notavailable;
            Total += total;
            plannumber += Plannumber;
            demandnumber += Demandnumber;
            Remark += remark;
            DID+=dID;

            if (i < tbody.rows.length - 1) {
                MainContent += ",";
                Name += ",";
                Specifications += ",";
                Finishedproduct += ",";
                finishingproduct += ",";
                finishingproducts += ",";
                notavailable += ",";
                Total += ",";
                plannumber += ",";
                demandnumber += ",";
                Remark += ",";
                DID+=",";
            }
            else {
                MainContent += "";
                Name += "";
                Specifications += "";
                Finishedproduct += "";
                finishingproduct += "";
                finishingproducts += "";
                notavailable += "";
                Total += "";
                plannumber += "";
                demandnumber += "";
                Remark += "";
                DID+="";
            }
        }

        $.ajax({
            url: "SaveUpdatePlan",
            type: "Post",
            data: {
                JHID: JHID, Specifieddate: Specifieddate, Plannedmonth: Plannedmonth, Remarks: Remarks, Formulation: Formulation, 
                MainContent: MainContent,Name: Name, Specifications: Specifications, Finishedproduct: Finishedproduct, 
                finishingproduct: finishingproduct, finishingproducts: finishingproducts, notavailable: notavailable,
                Total: Total, plannumber: plannumber, demandnumber: demandnumber, Remark: Remark,DID:DID
            },
            async: false,
            success: function (data) {
                if (data.success == true) {
                    alert("修改成功");
                    window.parent.ClosePop();
                }
                else {
                    alert(data.msg);
                }
            }
        });
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
                document.getElementById("RowNumber" + i).innerText=i
                $("#RowNumber" + i).attr("id", "RowNumber" + (i - 1));
                $("#PID" + i).attr("name", "PID" + (i - 1));
                $("#PID" + i).attr("id", "PID" + (i - 1));

                $("#Name" + i).attr("id", "Name" + (i - 1));
                $("#txtName" + i).attr("name", "Name" + (i - 1));
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

                $("#Spareparts" + i).attr("name", "Spareparts" + (i - 1));
                $("#Spareparts" + i).attr("id", "Spareparts" + (i - 1));

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

                $("#txtCreateUser" + i).attr("name", "CreateUser" + (i - 1));
                $("#txtCreateUser" + i).attr("id", "txtCreateUser" + (i - 1));
                $("#txtCreateTime" + i).attr("name", "CreateTime" + (i - 1));
                $("#txtCreateTime" + i).attr("id", "txtCreateTime" + (i - 1));

                $("#DetailInfos" + i).attr("id", "DetailInfos" + (i - 1));
                $("#DetailInfo" + i).attr("id", "DetailInfo" + (i - 1));
                
            }
        }

    }
}
