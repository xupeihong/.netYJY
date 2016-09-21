var newRowID = "";
$(document).ready(function () {
    $("#RecordDate").val("");
    $("#CreateTime").val("");
    CheckProucductID();
    $("#btnSave").click(function () {
        //备案数据
        var PID = $("#PID").val();
        var ISF = $("#ISF").val();
        var RecordDate = $("#RecordDate").val();
        if (RecordDate == "" || RecordDate == null) {
            alert("备案日期不能为空");
            return;
        }
        var PlanID = $("#PlanID").val();
        if (PlanID == "" || PlanID == null) {
            alert("工程编号不能为空");
            return;
        }
        var PlanName = $("#PlanName").val();
        if (PlanName == "" || PlanName == null) {
            alert("项目名称不能为空");
            return;
        }
        var WorkChief = $("#WorkChief").val();
        if (WorkChief == "" || WorkChief == null) {
            alert("业务负责人不能为空");
            return;
        }
        var Tel = $("#Tel").val();
        //var reg1 = /^((\+?[0-9]{2,4}\-[0-9]{3,4}\-)|([0-9]{3,4}\-))?([0-9]{7,8})(\-[0-9]+)?$|^[1][3,5,8][0-9]{9}$/;// /^(0|86|17951)?(13[0-9]|15[012356789]|17[678]|18[0-9]|14[57])[0-9]{8}$/;
        var reg = /^(\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$/;
        // var reg2 = /^0\d{2,3}-?\d{7,8}?(13[0-9]|15[012356789]|17[678]|18[0-9]|14[57])[0-9]{8}$/;
        //var reg3 = /^((\+?86)|(\(\+86\)))?(13[012356789][0-9]{8}|15[012356789][0-9]{8}|18[02356789][0-9]{8}|147[0-9]{8}|1349[0-9]{7})$/;
        if (Tel == "" || Tel == null)
        { }
        else {
            //if (!!Tel.match(((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$) == false) {
            if (reg.test(Tel) == false) {
                alert("请重新输入电话号码");
                return;
            }
        }
        var Constructor = $("#Constructor").val();
        var BelongArea = $("#BelongArea").val();
        if (BelongArea == "")
        {
            alert("所属区域不能为空");
            return;
        }
        var ExpectedTime = $("#ExpectedTime").val();
        var ChannelsFrom = $("#ChannelsFrom").val();
        var Manager = $("#Manager").val();
        if (Manager == "" || Manager == null) {
            alert("备案人不能为空");
            return;
        }
        var Remark = $("#Remark").val();
        var CreateTime = $("#CreateTime").val();
        //产品信息
        var ID = "";
        var ProductID = "";
        var OrderContent = "";
        var Specifications = "";
        var Unit = "";
        var Amount = "";
        var PRemark = "";
        var tbody = document.getElementById("DetailInfo");
        for (var i = 0; i < tbody.rows.length; i++) {
            var Productid = document.getElementById("ProductID" + i).value;
            var mainContent = document.getElementById("ProName" + i).value;
            var specsModels = document.getElementById("Spec" + i).value;
            var unit = document.getElementById("Units" + i).value;
            var salesNum = document.getElementById("Amount" + i).value;
            var remark = document.getElementById("Remark" + i).value;
            ID += parseInt(i + 1);
            ProductID += Productid;
            OrderContent += mainContent;
            if (mainContent == "")
            {
                alert("产品名称不能为空");
                return;
            }
            Specifications += specsModels;
            if (specsModels == "") {
                alert("规格型号不能为空");
                return;
            }
            Unit += unit;
            Amount += salesNum;
              if (salesNum == "")
            {
                alert("请输入数量数据");
                return;
              }
              var g = /^[0-9]*[0-9][0-9]*$/;
              if (g.test(salesNum) == false) {
                  alert("数量输入有误");
                  return;
              }
            PRemark += remark;
            if (i < tbody.rows.length - 1) {
                ID += ",";
                ProductID += ",";
                OrderContent += ",";
                Specifications += ",";
                Unit += ",";
                Amount += ",";
                PRemark += ",";
            }
            else {
                ID += "";
                ProductID += "";
                OrderContent += "";
                Specifications += "";
                Unit += "";
                Amount += "";
                PRemark += "";

            }
        }

        $.ajax({
            url: "SaveProject",
            type: "Post",
            data: {
                PID: PID, ExpectedTime: ExpectedTime, PlanID: PlanID, PlanName: PlanName, WorkChief: WorkChief, Tel: Tel,ISF:ISF,
                Constructor: Constructor, BelongArea: BelongArea, RecordDate: RecordDate, ChannelsFrom: ChannelsFrom,
                Manager: Manager, Remark: Remark,
                ID: ID, ProductID: ProductID, OrderContent: OrderContent, Specifications: Specifications, Unit: Unit, Amount: Amount, PRemark: PRemark
            },
            async: false,
            success: function (data) {
                if (data.success == true) {
                    window.parent.frames["iframeRight"].reload();
                    alert("备案完成！");
                    window.parent.ClosePop();
                }
                else {
                    alert("备案失败-" + data.msg);
                }
            }
        });

        //var options = {
        //    url: "SaveProject",
        //    data: { ID: ID, ProductID: ProductID, MainContent: MainContent, SpecsModels: SpecsModels, Unit: Unit, Amount: Amount, PRemark: PRemark },
        //    type: 'POST',
        //    async: false,
        //    success: function (data) {
        //        if (data.success == "true") {
        //           // window.parent.frames["iframeRight"].reload();
        //            alert(data.Msg);
        //            //setTimeout('parent.ClosePop()', 100);
        //            window.parent.ClosePop();
        //        }
        //        else {
        //          alert(data.Msg);
        //        }
        //    }
        //};
        // $("#ProjectformInfo").ajaxSubmit(options);
        return false;
    });
});
function returnConfirm() {
    //  return isConfirm;
}


function CheckProucductID() {
    var PlanName = $("#PlanName").val();
    var planID = $("#PlanID").val();
    var BelongArea = $("#BelongArea").val();
    $("#BelongArea").blur(function myfunction() {
        planID = $("#PlanID").val();
        PlanName = $("#PlanName").val();
        BelongArea = $("#BelongArea").val();
        if (planID == "") {
            alert("工程编号不能为空");
            return;
        }
        if (PlanName == "") {
            alert("工程名称不能为空");
            return;
        }
        if (BelongArea == "") {
            alert("所属区域不能为空");
            return;
        }
        $.ajax({
            url: "CheckPlanIDandPlanName",
            type: "post",
            data: { PlanID: planID, PlanName: PlanName, BelongArea: BelongArea },
            dataType: "json",
            ansyc: true,
            success: function (data) {
                if (data.success == true) {
                    alert("工程编号和工程名称所属区域不能重复");
                    $("#PlanID").val("");
                    $("#PlanName").val("");
                    $("#BelongArea").val("");
                    return;
                }


                //var json = eval(data.datas);
                //if (json !=undefined) {
                //    if (json.length > 0) {
                //        for (var i = 0; i < json.length; i++) {
                //            if (json[i].PlanID == planID) {
                //                $("#PlanID").val("");
                //                alert("工程编号重复");

                //                return;
                //            }
                //            if (json[i].BelongArea == BelongArea) {
                //                $("#BelongArea").val("");
                //                alert("工程编号重复");

                //                return;
                //            }
                //        }
                //    }
                //}
            }
        });
        //var changeUrl = "CheckPlanIDandPlanName?PlanID="+palnID+"&Plan=" + gradename;
        //$.get(changeUrl, function (str) {
        //    if (str == '1') {
        //        $("#PlanID").html("<font color=\"red\">您输入的用户名存在！请重新输入！</font>");
        //    } else {
        //        $("#PlanID").html("");
        //    }
        //})

    });

    //$("#PlanName").blur(function myfunction() {

    //    //var changeUrl = "GradeAdmin.php?action=check&gradename=" + gradename;
    //    //$.get(changeUrl, function (str) {
    //    //    if (str == '1') {
    //    //        $("#PlanID").html("<font color=\"red\">您输入的用户名存在！请重新输入！</font>");
    //    //    } else {
    //    //        $("#PlanID").html("");
    //    //    }
    //    //})
    //    planID = $("#PlanID").val();
    //    PlanName = $("#PlanName").val();
    //    $.ajax({
    //        url: "CheckPlanIDandPlanName",
    //        type: "post",
    //        data: {PlanID: planID, PlanName: PlanName },
    //        dataType: "json",
    //        ansyc: true,
    //        success: function (data) {
    //            var json = eval(data.datas);
    //            if (json !=undefined){
    //            if (json.length > 0) {
    //                for (var i = 0; i < json.length; i++) {
    //                    if (json[i].PlanName == PlanName) {
    //                        $("#PlanName").val("");
    //                        alert("工程名称重复");
    //                        return;
    //                    }
    //                }
    //            }
    //            }
    //        }
    //    });

    //});
}
function AddRow() {

    var listids = new Array();
    listids[0] = "0";
    listids[1] = "1";
    listids[2] = "2";
    listids[3] = "3";
    listids[4] = "4";
    listids[5] = "5";
    listids[6] = "6";
    //  listids[7] = "7";
    //listids[8] = "8";
    //listids[9] = "9";

    var listtypes = new Array();
    listtypes[0] = "text";
    listtypes[1] = "text";
    listtypes[2] = "text";
    listtypes[3] = "text";
    listtypes[4] = "text";
    listtypes[5] = "text";
    listtypes[6] = "text";
    //listtypes[7] = "text";
    //listtypes[8] = "text";
    //listtypes[9] = "text";

    var listNewElements = new Array();
    listNewElements[0] = "input";
    listNewElements[1] = "input";
    listNewElements[2] = "input";
    listNewElements[3] = "input";
    listNewElements[4] = "input";
    listNewElements[5] = "input";
    listNewElements[6] = "input";
    //listNewElements[7] = "input";
    //listNewElements[8] = "input";
    ////listNewElements[9] = "input";

    var listNewElementsID = new Array();
    listNewElementsID[0] = "ID";
    listNewElementsID[1] = "WID";
    listNewElementsID[2] = "MainContent";
    listNewElementsID[3] = "SpecsModels";
    listNewElementsID[4] = "Unit";
    listNewElementsID[5] = "Amount";
    listNewElementsID[6] = "OrderTime";
    //listNewElementsID[7] = "ChannelsFrom";
    //listNewElementsID[8] = "OrderTime";
    //listNewElementsID[9] = "ChannelsFrom";

    var listCheck = new Array();
    listCheck[0] = "n";
    listCheck[1] = "n";
    listCheck[2] = "n";
    listCheck[3] = "n";
    listCheck[4] = "n";
    listCheck[5] = "n";
    // listCheck[6] = "n";
    //listCheck[7] = "y";
    // listCheck[8] = "n";
    //listCheck[7] = "n";


    var listcontent = new Array();

    var tableGX = new Table(null, null, null, null, null, null, null, null);
    tableGX.addNewRow('myTable', listtypes, listNewElements, listcontent, 'DetailInfo', listNewElementsID, listCheck);
    var tbody = document.getElementById("DetailInfo");
    var tbody = document.getElementById("DetailInfo");
    var rowCount = tbody.rows.length;
    for (var i = 0; i < rowCount; i++) {
        document.getElementById("myTable").rows[i + 1].cells[0].style.display = "none";
    }
}
function CheckDetail() {
    ShowIframe1("选择货品信息", "../SalesManage/ChangeProduct", 850, 450);
}
function DelRow() {
    var tbody = document.getElementById("DetailInfo");
    var rowCount = tbody.rows.length;

    if (rowCount == 0) {
        alert("当前没有可删除的行！");
        return;
    }

    if (newRowID == "") {
        alert("请选择删除的行");
        return;
    }
    var listtypeNames = new Array();
    listtypeNames[0] = "MainContent";
    listtypeNames[1] = "DeviceName";
    listtypeNames[2] = "SpecsModels";
    listtypeNames[3] = "SalesNum";
    listtypeNames[4] = "WorkChief";
    listtypeNames[5] = "Constructor";
    listtypeNames[6] = "Tel";
    listtypeNames[7] = "BelongArea";
    listtypeNames[8] = "OrderTime";
    listtypeNames[9] = "ChannelsFrom";

    var tableGX = new Table(null, null, null, null, null, null, null, null);
    tableGX.removeRow('myTable', 'DetailInfo', listtypeNames);
}

function CheckSpecial(op) {
    var special = $("#txtSpecial").val();
    var arr = new Array();
    arr = special.split(',');
    var msg = $("#" + op + "").val();
    for (var i = 0; i < arr.length; i++) {
        if (msg.indexOf(arr[i]) > 0) {
            alert("有非法字符" + arr[i] + ",请重新输入");
            $("#" + op + "").val('');

            return false;
        }
    }
}


function InitPage() {
    //TaskID_DX = document.getElementById("TaskID_DX").value;

    var listids = new Array();
    listids[0] = "0";
    listids[1] = "1";
    listids[2] = "2";
    listids[3] = "3";

    var listtypes = new Array();
    listtypes[0] = "text";
    listtypes[1] = "text";
    listtypes[2] = "text";
    listtypes[3] = "text";

    var listNewElements = new Array();
    listNewElements[0] = "input";
    listNewElements[1] = "input";
    listNewElements[2] = "input";
    listNewElements[3] = "input";

    var listNewElementsID = new Array();
    listNewElementsID[0] = "GDDepth";
    listNewElementsID[1] = "SDDepth";
    listNewElementsID[2] = "BDDepth";
    listNewElementsID[3] = "GDIDepth";


    var listcontent = new Array();
    var TaskID = $("#TaskID").val();
    var AccidentPoint = $("#AccidentPoint").val();
    $.post("GetFSInfo?TaskID=" + TaskID + "&AccidentPoint=" + encodeURI(AccidentPoint) + "&tabName=FSInfo", function (data1) {
        var objGXInfo = JSON.parse(data1);
        var tableGX = new Table(objGXInfo, 'oddRow', 'evenRow', 'selRow', listids, listtypes, listNewElements, listcontent, listNewElementsID);
        tableGX.LoadTableTBody('GXInfo');
        var tbody = document.getElementById("GXInfo");
        var rowCount = tbody.rows.length;
        for (var i = 0; i < rowCount; i++) {
            document.getElementById("myTable").rows[i + 1].cells[0].style.display = "none";
        }
    });

    var tbody = document.getElementById("GXInfo");
    var rowCount = tbody.rows.length;
    for (var i = 0; i < rowCount; i++) {
        document.getElementById("myTable").rows[i + 1].cells[0].style.display = "none";
    }
}


function addBasicDetail(PID) { //增加货品信息行

    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    //var strPID = $("#ProductID").val();
    //$("#ProductID").val(strPID + "," + ProductID);
    $.ajax({
        url: "GetBasicDetail",
        type: "post",
        data: { PID: PID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {


                for (var i = 0; i < json.length; i++) {
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                    html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                    html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                    html += '<td ><lable class="labUnits' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Units + '</lable> </td>';

                    html += '<td ><input type="text" id="Amount' + rowCount + '" style="width:30px;"/></td>';
                    //html += '<td ><lable class="labUnitPrice' + rowCount + ' " id="UnitPrice' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';

                    //html += '<td ><input type="text" id="Total' + rowCount + '" style="width:60px;"/></td>';
                    //html += '<td ><lable class="labManufacturer' + rowCount + ' " id="Manufacturer' + rowCount + '">' + json[i].Manufacturer + '</lable> </td>';
                    html += '<td ><lable class="labRemark' + rowCount + ' " id="Remark' + rowCount + '">' + json[i].Remark + '</lable> </td>';
                    html += '<td style="display:none;"><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '</tr>'
                    CountRows = CountRows + 1;
                    rowCount += 1;
                    $("#DetailInfo").append(html);

                }


            }
        }
    })
}

function addRecordF()
{
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    var html = "";
    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
    html += '<td ><input class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '"/> </td>';
    html += '<td ><input  class="labProName' + rowCount + ' " id="ProName' + rowCount + '"/> </td>';
    html += '<td ><input class="labSpec' + rowCount + ' " id="Spec' + rowCount + '"/></td>';
    html += '<td ><input  class="labUnits' + rowCount + ' " id="Units' + rowCount + '" /> </td>';
    html += '<td ><input type="text" id="Amount' + rowCount + '" /></td>';
    html += '<td ><input class="labRemark' + rowCount + ' " id="Remark' + rowCount + '" /></td>';
   // html += '<td style="display:none;"><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
    html += '</tr>'
    CountRows = CountRows + 1;
    rowCount += 1;
    $("#DetailInfo").append(html);
}

function selRow(curRow) {
    newRowID = curRow.id;
    $("#DetailInfo tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}
