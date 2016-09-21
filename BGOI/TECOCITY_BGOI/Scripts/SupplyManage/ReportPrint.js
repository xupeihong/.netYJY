var CountRows = 0, CountRows2 = 0, rowCount = 0, rowCount2 = 0, rowCount3 = 0, rowCount4 = 0, rowCount5 = 0;

$(document).ready(function () {
    if (location.search != "") {
        SID = location.search.split('&')[0].split('=')[1];
    }
    $("#btnPrint").click(function () {
        document.getElementById("btnPrint").className = "Noprint";
        $("#pageContent").attr("style", "width: 100%;margin-top:10px")
        pagesetup("0.3", "0", "0.3", "0.3");
        window.print();

        document.getElementById("btnPrint").className = "btn";
        $("#pageContent").attr("style", "margin: 0 auto; width: 90%;margin-top:10px")

    });

    LoadProcessInfo();

    //jq();
})
function LoadTitle() {
    var tr2 = $("#line2");
    tr2.html('');
    var td1 = $('<td  rowspan="2" align="center">序号</td>');
    td1.appendTo(tr2);
    var td1 = $('<td rowspan="2" align="center"  >供应商名称</td>');
    td1.appendTo(tr2);
    //var td1 = $('<td rowspan="2" align="center">被处理供应商编号</td>');
    //td1.appendTo(tr2);
    var td1 = $('<td rowspan="2" align="center">申请处理原因</td>');
    td1.appendTo(tr2);
    var td1 = $('<td align="center" rowspan="2">申请处理人员</td>');
    td1.appendTo(tr2);
    var td1 = $('<td align="center" rowspan="2">处理意见</td>');
    td1.appendTo(tr2);
    var td1 = $('<td align="center" rowspan="2">处理意见详细</td>');
    td1.appendTo(tr2);
    var td1 = $('<td align="center" rowspan="2">申请部门</td>');
    td1.appendTo(tr2);
    var td1 = $('<td align="center" rowspan="2">申请日期</td>');
    td1.appendTo(tr2);
    var td1 = $('<td align="center" rowspan="2">状态</td>');
    td1.appendTo(tr2);
    var td1 = $('<td align="center" rowspan="2">申请处理原因时间</td>');
    td1.appendTo(tr2);
    //var td1 = $('<td align="center" rowspan="2">审批状态</td>');
    //td1.appendTo(tr2);
}
function LoadProcessInfo() {
    rowCount = document.getElementById("DetailInfo").rows.length;
    rowCount2 = document.getElementById("Detail").rows.length;
    rowCount3 = document.getElementById("Detail3").rows.length;
    rowCount4 = document.getElementById("Detail4").rows.length;
    rowCount5 = document.getElementById("Detail5").rows.length;
    CountRows = parseInt(rowCount);
    CountRows = parseInt(rowCount5);
    $.ajax({
        url: "GetProcessInfo",
        type: "post",
        data: { sid: SID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var g = 0; g < json.length; g++) {
                    var HasRegulation = json[g].HasRegulation;
                    if (HasRegulation == "0") {
                        HasRegulation = "是";
                    }
                    else {
                        HasRegulation = "否";
                    }
                    var HasAuthorization = json[g].HasAuthorization;
                    if (HasAuthorization == "0") {
                        HasAuthorization = "是";
                    }
                    else {
                        HasAuthorization = "否";
                    }
                    var HasDrawing = json[g].HasDrawing;
                    if (HasDrawing == "0") {
                        HasDrawing = "有";
                    }
                    else {
                        HasDrawing = "无";
                    }
                    var RankingType = json[g].RankingType;
                    if (RankingType == "0") {
                        RankingType = "国内";
                    }
                    else {
                        RankingType = "国外";
                    }
                    CountRows++;
                    CountRows2++;
                    var html = "";
                    var html2 = "";
                    var html3 = "";
                    var html4 = "";
                    var html5 = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)" style="text-align:center;">'
                    //html += '<td style="text-align:center;"><lable class="num' + rowCount + ' " id="rownum' + rowCount + '">' + CountRows + '</lable> </td>';

                    html += '<td ><lable class="labProductID' + rowCount + ' " id="SupplierType' + rowCount + '">' + json[g].SupplierType + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="COMNameC' + rowCount + '">' + json[g].COMNameC + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="COMShortName' + rowCount + '">' + json[g].COMShortName + '</lable></td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="COMArea' + rowCount + '">' + json[g].COMArea + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="COMCountry' + rowCount + '" >' + json[g].COMCountry + '</lable></td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ComAddress' + rowCount + '">' + json[g].ComAddress + '</lable></td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="COMCreateDate' + rowCount + '">' + json[g].COMCreateDate + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="TaxRegistrationNo' + rowCount + '">' + json[g].TaxRegistrationNo + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="BusinessLicenseNo' + rowCount + '">' + json[g].BusinessLicenseNo + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="COMRAddress' + rowCount + '">' + json[g].COMRAddress + '</lable></td>';

                    html += '<td style="display:none;"><lable class="labSID' + rowCount + ' " id="SID' + rowCount + '">' + json[g].SID + '</lable> </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                    html2 = '<tr  id ="Detail' + rowCount2 + '" onclick="selRow2(this)" style="text-align:center;">'
                    html2 += '<td ><lable class="labProductID' + rowCount2 + ' " id="COMLegalPerson' + rowCount2 + '">' + json[g].COMLegalPerson + '</lable></td>';
                    html2 += '<td ><lable class="labProductID' + rowCount2 + ' " id="COMFactoryAddress' + rowCount2 + '">' + json[g].COMFactoryAddress + '</lable></td>';
                    html2 += '<td ><lable class="labProductID' + rowCount2 + ' " id="OrganizationCode' + rowCount2 + '">' + json[g].OrganizationCode + '</lable></td>';
                    html2 += '<td ><lable class="labProductID' + rowCount2 + ' " id="CapitalUnit' + rowCount2 + '">' + json[g].RegisteredCapital + json[g].CapitalUnit + '</lable></td>';
                    html2 += '<td ><lable class="labProductID' + rowCount2 + ' " id="BankName' + rowCount2 + '">' + json[g].BankName + '</lable></td>';
                    html2 += '<td ><lable class="labProductID' + rowCount2 + ' " id="BankAccount' + rowCount2 + '">' + json[g].BankAccount + '</lable></td>';
                    html2 += '<td ><lable class="labProductID' + rowCount2 + ' " id="StaffNum' + rowCount2 + '">' + json[g].StaffNum + "&nbsp 人" + '</lable></td>';
                    html2 += '<td ><lable class="labProductID' + rowCount2 + ' " id="EnterpriseType' + rowCount2 + '">' + json[g].EnterpriseType + '</lable></td>';
                    html2 += '<td ><lable class="labProductID' + rowCount2 + ' " id="BusinessDistribute' + rowCount2 + '">' + json[g].BusinessDistribute + '</lable></td>';
                    html2 += '<td ><lable class="labProductID' + rowCount2 + ' " id="BillingWay' + rowCount2 + '">' + json[g].BillingWay + '</lable></td>';
                    html2 += '<td style="display:none;"><lable class="labSID' + rowCount2 + ' " id="SID' + rowCount2 + '">' + json[g].SID + '</lable> </td>';
                    html2 += '</tr>'
                    $("#Detail").append(html2);
                    html3 = '<tr  id ="Detail3' + rowCount3 + '" onclick="selRow3(this)" style="text-align:center;">'
                    html3 += '<td ><lable class="labProductID' + rowCount3 + ' " id="Turnover' + rowCount3 + '">' + json[g].Turnover + '</lable></td>';
                    html3 += '<td ><lable class="labProductID' + rowCount3 + ' " id="DevelopStaffs' + rowCount3 + '">' + json[g].DevelopStaffs + "人" + '</lable></td>';
                    html3 += '<td ><lable class="labProductID' + rowCount3 + ' " id="QAStaffs' + rowCount3 + '">' + json[g].QAStaffs + "人" + '</lable></td>';
                    html3 += '<td ><lable class="labProductID' + rowCount3 + ' " id="ProduceStaffs' + rowCount3 + '">' + json[g].ProduceStaffs + "人" + '</lable></td>';
                    html3 += '<td ><lable class="labProductID' + rowCount3 + ' " id="Relation' + rowCount3 + '">' + json[g].Relation + '</lable></td>';
                    html3 += '<td ><lable class="labProductID' + rowCount3 + ' " id="WorkTime_Start' + rowCount3 + '">' + json[g].WorkTime_Start + "&nbspAM" + '</lable></td>';
                    html3 += '<td ><lable class="labProductID' + rowCount3 + ' " id="WorkTime_End' + rowCount3 + '">' + json[g].WorkTime_End + "&nbspPM" + '</lable></td>';
                    html3 += '<td ><lable class="labProductID' + rowCount3 + ' " id="HasRegulation' + rowCount3 + '">' + HasRegulation + '</lable></td>';
                    html3 += '<td ><lable class="labProductID' + rowCount3 + ' " id="WorkDay_Start' + rowCount3 + '">' + json[g].WorkDay_Start + '</lable></td>';
                    html3 += '<td ><lable class="labProductID' + rowCount3 + ' " id="WorkDay_End' + rowCount3 + '">' + json[g].WorkDay_End + '</lable></td>';
                    html3 += '<td style="display:none;"><lable class="labSID' + rowCount3 + ' " id="SID' + rowCount3 + '">' + json[g].SID + '</lable> </td>';
                    html3 += '</tr>'
                    $("#Detail3").append(html3);
                    html4 = '<tr  id ="Detail3' + rowCount4 + '" onclick="selRow4(this)" style="text-align:center;">'
                    html4 += '<td ><lable class="labProductID' + rowCount4 + ' " id="BusinessScope' + rowCount4 + '">' + json[g].BusinessScope + '</lable></td>';
                    html4 += '<td ><lable class="labProductID' + rowCount4 + ' " id="IsrankingIn5' + rowCount4 + '">' + json[g].IsrankingIn5 + "&nbsp名" + '</lable></td>';
                    html4 += '<td ><lable class="labProductID' + rowCount4 + ' " id="RankingType' + rowCount4 + '">' + RankingType + '</lable></td>';
                    html4 += '<td ><lable class="labProductID' + rowCount4 + ' " id="Ranking' + rowCount4 + '">' + json[g].Ranking + "&nbsp 名" + '</lable></td>';
                    html4 += '<td ><lable class="labProductID' + rowCount4 + ' " id="ScaleType' + rowCount4 + '">' + json[g].ScaleType + '</lable></td>';
                    html4 += '<td ><lable class="labProductID' + rowCount4 + ' " id="QualityStandard' + rowCount4 + '">' + json[g].QualityStandard + '</lable></td>';
                    html4 += '<td ><lable class="labProductID' + rowCount4 + ' " id="HasAuthorization' + rowCount4 + '">' + HasAuthorization + '</lable></td>';
                    html4 += '<td ><lable class="labProductID' + rowCount4 + ' " id="HasDrawing' + rowCount4 + '">' + HasDrawing + '</lable></td>';
                    html4 += '<td ><lable class="labProductID' + rowCount4 + ' " id="AgentClass' + rowCount4 + '">' + json[g].AgentClass + '</lable></td>';
                    html4 += '<td ><lable class="labProductID' + rowCount4 + ' " id="HasImportMaterial' + rowCount4 + '">' + json[g].HasImportMaterial + '</lable></td>';

                    html4 += '<td style="display:none;"><lable class="labSID' + rowCount4 + ' " id="SID' + rowCount4 + '">' + json[g].SID + '</lable> </td>';
                    html4 += '</tr>'
                    $("#Detail4").append(html4);

                    //html5 = '<tr  id ="Detail5' + rowCount5 + '" onclick="selRow5(this)" style="text-align:center;">'
                    //html5+="<tr ><td  rowspan=" + (CountRows2) + ">日志记录</td><td  >日志标题</td><td  >日志内容</td><td >记录时间</td><td  >记录人</td><td >日志类型</td></tr>";
                    //for (var i = 0; i < CountRows2.Count; i++) {
                    //    if (i == 0)
                    //        html5 += "<tr ><td   >" + json[0]["LogTitle"].ToString() + "</td><td >" + dtLog.Rows[0]["LogContent"].ToString() + "</td><td  >" + dtLog.Rows[0]["LogTime"].ToString() + "</td><td  >" + dtLog.Rows[0]["LogPerson"].ToString() + "</td><td  >" + dtLog.Rows[0]["Type"].ToString() + "</td></tr>";
                    //    else
                    //        html5 += "<tr ><td  >" + json[g]["LogTitle"].ToString() + "</td><td   >" + dtLog.Rows[g]["LogContent"].ToString() + "</td><td >" + dtLog.Rows[g]["LogTime"].ToString() + "</td><td >" + dtLog.Rows[g]["LogPerson"].ToString() + "</td><td >" + dtLog.Rows[g]["Type"].ToString() + "</td></tr>";
                    //}
                    //html5 += '<td style="display:none;"><lable class="labSID' + rowCount5 + ' " id="SID' + rowCount5 + '">' + json[g].SID + '</lable> </td>';
                    //html5 += '</tr>'
                    //$("#Detail5").append(html5);
                }
            }
        }
    })

}
function LoadDate() {
    rowCount = document.getElementById("Detail").rows.length;
    CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "GetSupplyDetail",
        type: "post",
        data: { sid: SID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="SupplierCode' + rowCount + '">' + json[i].SupplierCode + '</lable> </td>';
                    html += '<td ><lable class="labProName' + rowCount + ' " id="COMNameC' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';
                    html += '<td style="display:none;"><lable class="labPID' + rowCount + ' " id="SID' + rowCount + '">' + json[i].SID + '</lable> </td>';
                    html += '</tr>'
                    $("#Detail").append(html);
                    CountRows = CountRows + 1;
                }
            }
        }
    })
}
function jq() {
    $.ajax({
        url: "getRecordList",
        type: "post",
        data: {
            PID: $("#PID").val(), SID: $("#SID").val()
        },
        dataType: "json",
        async: false, //是否异步
        success: function (data) {
            if (data.success == "false") {
                if (data.Msg != "") {
                    alert(data.Msg);
                }
                FileNull();
            }
            else {
                FileData(data);
            }
        }
    })
}
function FileData(data) {
    if (data.RecordListUT != "") {
        var tab = $("#tabList");
        tab.html('');
        var datas = eval("(" + data.RecordListUT + ")");
        $.each(datas.RecordListUT, function (i, n) {
            var tr = $('<tr></tr>');
            var td = '';
            for (var key in n) {
                var keyValue = n[key];
                td = $('<td>' + keyValue + '</td>');
                td.appendTo(tr);
            }
            tr.appendTo(tab);
        })
    }
    FileNull();
}
function FileNull(datas) {
    var count = 10;
    var tab = $("#tabList");
    for (var i = 0; i < count; i++) {
        var tr = $('<tr></tr>');
        var td = '';
        for (var j = 0; j < 12; j++) {
            td = $('<td></td>');
            td.appendTo(tr);
        }
        tr.appendTo(tab);
    }
}

function selRow(rowid) {
    newRowID = rowid.id;
    $("#DetailInfo tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}

function selRow2(rowid) {
    newRowID = rowid.id;
    $("#Detail tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}
function selRow3(rowid) {
    newRowID = rowid.id;
    $("#Detail3 tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}
function selRow4(rowid) {
    newRowID = rowid.id;
    $("#Detail4 tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}
function selRow5(rowid) {
    newRowID = rowid.id;
    $("#Detail5 tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}