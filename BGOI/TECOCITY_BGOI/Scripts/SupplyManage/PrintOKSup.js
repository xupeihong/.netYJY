var CountRows = 0, CountRows2 = 0, CountRows3 = 0, CountRows5 = 0, rowCount = 0, rowCount2 = 0, rowCount3 = 0, rowCount4 = 0, rowCount5 = 0;
var CountRows6 = 0;
var rowCount6 = 0;
var CountRows7 = 0;
var rowCount7 = 0;
$(document).ready(function () {
    if (location.search != "") {
        SID = location.search.split('&')[0].split('=')[1];
    }
    $("#btnPrint").click(function () {
        //document.getElementById("btnPrint").className = "Noprint";
        $("#pageContent").attr("style", "width: 100%;margin-top:10px")
        pagesetup("0.3", "0", "0.3", "0.3");
        window.print();

        document.getElementById("btnPrint").className = "btn";
        $("#pageContent").attr("style", "margin: 0 auto; width: 100%;margin-top:10px")
    });
    LoadSUPok();
})
function LoadSUPok() {
    rowCount = document.getElementById("DetailInfo").rows.length;
    rowCount2 = document.getElementById("Detail").rows.length;
    rowCount3 = document.getElementById("Detail3").rows.length;
    rowCount4 = document.getElementById("Detail4").rows.length;
    rowCount5 = document.getElementById("DetailInfo1").rows.length;
    rowCount6 = document.getElementById("Detail1").rows.length;
    rowCount6 = document.getElementById("Detail5").rows.length;
    CountRows = parseInt(rowCount);
    CountRows2 = parseInt(rowCount2);
    CountRows3 = parseInt(rowCount3);
    CountRows5 = parseInt(rowCount5);
    CountRows6 = parseInt(rowCount6);
    CountRows7 = parseInt(rowCount7);
    var ScalllType = "";
    var RelaTion = "";
    var QualityStandard = "";
    var Agenclass = "";
    var BusinessDistributes = "";
    var getbillWay = "";
    //业务分布
    $.ajax({
        url: "getBusinessDistribute",
        type: "post",
        data: { sid: SID },
        dataType: "json",
        async: false,
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var j = 0; j < json.length; j++) {
                    BusinessDistributes += json[j].BusinessDistribute;
                }
            }
        }
    });
    //开票方式
    $.ajax({
        url: "getbillWay",
        type: "post",
        data: { sid: SID },
        dataType: "json",
        async: false,
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var j = 0; j < json.length; j++) {
                    getbillWay += json[j].BillingWay;
                }
            }
        }
    });
    //供需关系
    $.ajax({
        url: "getReation",
        type: "post",
        data: { sid: SID },
        dataType: "json",
        async: false,
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var j = 0; j < json.length; j++) {
                    RelaTion += json[j].Relation;
                }
            }
        }
    });
    //经营品种分类
    $.ajax({
        url: "ScalType",
        type: "post",
        data: { sid: SID },
        dataType: "json",
        async: false,
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var j = 0; j < json.length; j++) {
                    ScalllType += json[j].ScaleType;
                }
            }
        }
    });
    //产品质量执行标准
    $.ajax({
        url: "getQualityStandard",
        type: "post",
        data: { sid: SID },
        dataType: "json",
        async: false,
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var j = 0; j < json.length; j++) {
                    QualityStandard += json[j].QualityStandard;
                }
            }
        }
    });

    //代理产品所属级别
    $.ajax({
        url: "getAgenclass",
        type: "post",
        data: { sid: SID },
        dataType: "json",
        async: false,
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var j = 0; j < json.length; j++) {
                    Agenclass += json[j].AgentClass;
                }
            }
        }
    });
    $.ajax({
        url: "PrintOKSupply",
        type: "post",
        data: { sid: SID },
        dataType: "json",
        async: false,
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
                    var IsrankingIn5 = json[g].IsrankingIn5;
                    if (IsrankingIn5 == "0") {
                        IsrankingIn5 = "是";
                    }
                    else {
                        IsrankingIn5 = "否";
                    }
                    //var createTime = json[g].COMCreateDate.Format('yyyy-MM-dd');

                    //数字转换成汉字
                    var BusinessDistribute = json[g].BusinessDistribute;

                    var arr = new Array();//分割汉字和数字
                    var arr2 = new Array();//汉字分割
                    var arr3 = new Array();//数字分割
                    arr = BusinessDistribute.split(':');
                    arr2 = arr[0].split(',');
                    arr3 = arr[1].split(',');
                    var Business = "";
                    var BusinessDistribute = "";
                    var BusinessNum = "";
                    for (var i = 0; i < arr2.length; i++) {
                        //for (var j = 0; j <json.length; j++) {
                        //    if (one[i] == dt9.Rows[j]["SID"].ToString())
                        //        one[i] = dt9.Rows[j]["Text"].ToString();
                        //}
                        // BusinessNum += arr2[i] + ",";
                        // BusinessDistribute += arr3[i] + "%" + ",";
                        Business += arr3[i] + "%" + ",";
                    }
                    var billway = json[g].BillingWay;
                    var arr4 = new Array();//分割汉字和数字
                    var arr5 = new Array();//汉字分割
                    var arr6 = new Array();//数字分割
                    arr4 = billway.split(':');
                    arr5 = arr[0].split(',');
                    arr6 = arr[1].split(',');
                    var bilway = "";
                    for (var i = 0; i < arr5.length; i++) {
                        bilway += arr6[i] + "%" + ",";
                    }
                    var html = "";
                    var html2 = "";
                    var html3 = "";
                    var html4 = "";
                    var html5 = "";
                    var html6 = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)" style="text-align:center;overflow-y: auto;">'
                    html += '<td style="width: auto;"><lable class="labProductID' + rowCount + ' " id="SupplierType' + rowCount + '">' + json[g].SupplierType + '</lable> </td>';
                    html += '<td style="width: auto;"><lable class="labProductID' + rowCount + ' " id="COMNameC' + rowCount + '">' + json[g].COMNameC + '</lable> </td>';
                    html += '<td style="width: auto;"><lable class="labProductID' + rowCount + ' " id="COMShortName' + rowCount + '">' + json[g].COMShortName + '</lable></td>';
                    html += '<td style="width: auto;"><lable class="labProductID' + rowCount + ' " id="COMArea' + rowCount + '">' + json[g].COMArea + '</lable> </td>';
                    html += '<td style="width: auto;"><lable class="labProductID' + rowCount + ' " id="COMCountry' + rowCount + '" >' + json[g].COMCountry + '</lable></td>';
                    html += '<td style="width: auto;"><lable class="labProductID' + rowCount + ' " id="ComAddress' + rowCount + '">' + json[g].ComAddress + '</lable></td>';

                    html += '<td style="display:none;" style="width: auto;"><lable class="labSID' + rowCount + ' " id="SID' + rowCount + '">' + json[g].SID + '</lable> </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);

                    html5 = '<tr  id ="DetailInfo1' + rowCount5 + '" onclick="selRow5(this)" style="text-align:center;overflow-y: auto;">'
                    html5 += '<td style="width: auto;"><lable class="labProductID' + rowCount5 + ' " id="COMCreateDate' + rowCount5 + '">' + json[g].TaxRegistrationNo + '</lable> </td>';
                    html5 += '<td style="width: auto;"><lable class="labProductID' + rowCount5 + ' " id="TaxRegistrationNo' + rowCount5 + '">' + json[g].BusinessLicenseNo + '</lable> </td>';
                    html5 += '<td style="width: auto;"><lable class="labProductID' + rowCount5 + ' " id="BusinessLicenseNo' + rowCount5 + '">' + json[g].OrganizationCode + '</lable> </td>';
                    html5 += '<td style="width: auto;"><lable class="labProductID' + rowCount5 + ' " id="COMRAddress' + rowCount5 + '">' + json[g].ThreeCertity + '</lable></td>';
                    html5 += '<td style="width: auto;"><lable class="labProductID' + rowCount5 + ' " id="COMLegalPerson' + rowCount2 + '">' + json[g].COMLegalPerson + '</lable></td>';
                    html5 += '<td style="width: auto;"><lable class="labProductID' + rowCount5 + ' " id="COMCreateDate' + rowCount5 + '">' + json[g].COMCreateDate + '</lable></td>';
                    html5 += '<td style="display:none;" style="width: auto;"><lable class="labSID' + rowCount5 + ' " id="SID' + rowCount5 + '">' + json[g].SID + '</lable> </td>';
                    html5 += '</tr>'
                    $("#DetailInfo1").append(html5);

                    html2 = '<tr  id ="Detail' + rowCount2 + '" onclick="selRow2(this)" style="text-align:center;">'

                    html2 += '<td style="width: auto;"><lable class="labProductID' + rowCount2 + ' " id="OrganizationCode' + rowCount2 + '">' + json[g].COMRAddress + '</lable></td>';
                    html2 += '<td style="width: auto;"><lable class="labProductID' + rowCount2 + ' " id="CapitalUnit' + rowCount2 + '">' + json[g].RegisteredCapital + json[g].CapitalUnit + '</lable></td>';
                    html2 += '<td style="width: auto;"><lable class="labProductID' + rowCount2 + ' " id="BankName' + rowCount2 + '">' + json[g].BankName + '</lable></td>';
                    html2 += '<td style="width: auto;"><lable class="labProductID' + rowCount2 + ' " id="BankAccount' + rowCount2 + '">' + json[g].BankAccount + '</lable></td>';
                    html2 += '<td style="width: auto;"><lable class="labProductID' + rowCount2 + ' " id="StaffNum' + rowCount2 + '">' + json[g].StaffNum + "&nbsp 人" + '</lable></td>';
                    html2 += '<td style="width: auto;"><lable class="labProductID' + rowCount2 + ' " id="EnterpriseType' + rowCount2 + '">' + json[g].EnterpriseType + '</lable></td>';

                    html2 += '<td style="display:none;" style="width: auto;"><lable class="labSID' + rowCount2 + ' " id="SID' + rowCount2 + '">' + json[g].SID + '</lable> </td>';
                    html2 += '</tr>'
                    $("#Detail").append(html2);

                    html6 = '<tr  id ="Detail' + rowCount6 + '" onclick="selRow6(this)" style="text-align:center;">'
                    html6 += '<td style="width: auto;"><lable class="labProductID' + rowCount6 + ' " id="BusinessDistribute' + rowCount6 + '">' + BusinessDistributes + ":" + Business + '</lable></td>';
                    html6 += '<td style="width: auto;"><lable class="labProductID' + rowCount6 + ' " id="BillingWay' + rowCount6 + '">' + getbillWay + bilway + '</lable></td>';
                    html6 += '<td style="display:none;" style="width: auto;"><lable class="labSID' + rowCount6 + ' " id="SID' + rowCount6 + '">' + json[g].SID + '</lable> </td>';
                    html6 += '<td style="width: auto;"><lable class="labProductID' + rowCount6 + ' " id="Turnover' + rowCount6 + '">' + json[g].Turnover + "万元" + '</lable></td>';
                    html6 += '<td style="width: auto;"><lable class="labProductID' + rowCount6 + ' " id="DevelopStaffs' + rowCount6 + '">' + json[g].DevelopStaffs + "人" + '</lable></td>';
                    html6 += '<td style="width: auto;"><lable class="labProductID' + rowCount6 + ' " id="QAStaffs' + rowCount6 + '">' + json[g].QAStaffs + "人" + '</lable></td>';
                    html6 += '<td style="width: auto;"><lable class="labProductID' + rowCount6 + ' " id="ProduceStaffs' + rowCount6 + '">' + json[g].ProduceStaffs + "人" + '</lable></td>';
                    html6 += '</tr>'
                    $("#Detail1").append(html6);

                    html3 = '<tr  id ="Detail3' + rowCount3 + '" onclick="selRow3(this)" style="text-align:center;">'
                    html3 += '<td style="width: auto;"><lable class="labProductID' + rowCount3 + ' " id="Relation' + rowCount3 + '">' + RelaTion + '</lable></td>';
                    html3 += '<td style="width: auto;"><lable class="labProductID' + rowCount3 + ' " id="WorkTime_Start' + rowCount3 + '">' + json[g].WorkTime_Start + "&nbspAM" + '</lable></td>';
                    html3 += '<td style="width: auto;"><lable class="labProductID' + rowCount3 + ' " id="WorkTime_End' + rowCount3 + '">' + json[g].WorkTime_End + "&nbspPM" + '</lable></td>';
                    html3 += '<td style="width: auto;"><lable class="labProductID' + rowCount3 + ' " id="COMFactoryAddress' + rowCount3 + '">' + json[g].COMFactoryAddress + '</lable></td>';
                    html3 += '<td style="width: auto;"><lable class="labProductID' + rowCount3 + ' " id="WorkDay_Start' + rowCount3 + '">' + json[g].WorkDay_Start + '</lable></td>';
                    html3 += '<td style="width: auto;"><lable class="labProductID' + rowCount3 + ' " id="WorkDay_End' + rowCount3 + '">' + json[g].WorkDay_End + '</lable></td>';
                    html3 += '<td style="display:none;" style="width: auto;"><lable class="labSID' + rowCount3 + ' " id="SID' + rowCount3 + '">' + json[g].SID + '</lable> </td>';
                    html3 += '</tr>'
                    $("#Detail3").append(html3);

                    html4 = '<tr  id ="Detail3' + rowCount4 + '" onclick="selRow4(this)" style="text-align:center;">'
                    html4 += '<td style="width: auto;"><lable class="labProductID' + rowCount4 + ' " id="BusinessScope' + rowCount4 + '">' + json[g].BusinessScope + '</lable></td>';
                    html4 += '<td style="width: auto;"><lable class="labProductID' + rowCount4 + ' " id="IsrankingIn5' + rowCount4 + '">' + IsrankingIn5 + '</lable></td>';
                    html4 += '<td style="width: auto;"><lable class="labProductID' + rowCount4 + ' " id="RankingType' + rowCount4 + '">' + RankingType + '</lable></td>';
                    html4 += '<td style="width: auto;"><lable class="labProductID' + rowCount4 + ' " id="Ranking' + rowCount4 + '">' + json[g].Ranking + "&nbsp 名" + '</lable></td>';
                    html4 += '<td style="width: auto;"><lable class="labProductID' + rowCount4 + ' " id="ScaleType' + rowCount4 + '">' + ScalllType + '</lable></td>';
                    html4 += '<td style="width: auto;"><lable class="labProductID' + rowCount4 + ' " id="QualityStandard' + rowCount4 + '">' + QualityStandard + '</lable></td>';
                   

                    html4 += '<td style="display:none;" style="width: auto;"><lable class="labSID' + rowCount4 + ' " id="SID' + rowCount4 + '">' + json[g].SID + '</lable> </td>';
                    html4 += '</tr>'
                    $("#Detail4").append(html4);

                    html7 = '<tr  id ="Detail3' + rowCount7 + '" onclick="selRow7(this)" style="text-align:center;">'
                    html7 += '<td style="width: auto;"><lable class="labProductID' + rowCount7 + ' " id="HasAuthorization' + rowCount7 + '">' + HasAuthorization + '</lable></td>';
                    html7 += '<td style="width: auto;"><lable class="labProductID' + rowCount7 + ' " id="HasRegulation' + rowCount7 + '">' + HasRegulation + '</lable></td>';
                    html7 += '<td style="width: auto;"><lable class="labProductID' + rowCount7 + ' " id="AgentClass' + rowCount7 + '">' + Agenclass + '</lable></td>';
                    html7 += '<td style="width: auto;"><lable class="labProductID' + rowCount7 + ' " id="HasImportMaterial' + rowCount7 + '">' + json[g].HasImportMaterial + '</lable></td>';

                    html7 += '<td style="display:none;" style="width: auto;"><lable class="labSID' + rowCount7 + ' " id="SID' + rowCount7 + '">' + json[g].SID + '</lable> </td>';
                    html7 += '</tr>'
                    $("#Detail5").append(html7);
                }
            }
        }
    })

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
    $("#DetailInfo1 tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}
function selRow6(rowid) {
    newRowID = rowid.id;
    $("#Detail1 tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}
function selRow6(rowid) {
    newRowID = rowid.id;
    $("#Detail5 tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}