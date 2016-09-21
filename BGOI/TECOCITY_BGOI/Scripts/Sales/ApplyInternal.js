var isConfirm = false;
$(document).ready(function () {
    $("#OrderDate").val('');
    var typeVal = $("#Type").val();
    if (typeVal == "0") {
        $("#trZS").css("display", "none");
        $("#spID").html("内购申请单号：");
        $("#spBill").html("员工内购单");
        $("#spProduct").html("内购产品");
    }
    else if (typeVal == "1") {
        $("#trZS").css("display", "block");
        $("#spID").html("赠送申请单号：");
        $("#spBill").html("员工赠送单");
        $("#spProduct").html("赠送产品");
    }

    $("#btnSave").click(function () {
        if (typeVal == "0") {
            isConfirm = confirm("确定创建内购申请吗？")
        }
        else if (typeVal == "1") {
            isConfirm = confirm("确定创建赠送申请吗？")
        }
        if (isConfirm == false) {
            return false;
        }
        else
            submitInfo();
    });
});

function returnConfirm() {
    return isConfirm;
}

function submitInfo() {
    var tbody = document.getElementById("DetailInfo");
    var rowCount = tbody.rows.length;
    var options = {
        url: "SaveInternalOrder",
        data: { TaskLength: rowCount },
        type: 'POST',
        async: false,
        success: function (data) {
            if (data.success == true) {
                alert("提交成功！");
            }
            else {
                alert(data.Msg);
            }
        }
    };
    $("#form1").ajaxSubmit(options);
    return false;
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
    listids[7] = "7";
    listids[8] = "8";

    var listtypes = new Array();
    listtypes[0] = "text";
    listtypes[1] = "text";
    listtypes[2] = "text";
    listtypes[3] = "text";
    listtypes[4] = "text";
    listtypes[5] = "text";
    listtypes[6] = "text";
    listtypes[7] = "text";
    listtypes[8] = "text";

    var listNewElements = new Array();
    listNewElements[0] = "input";
    listNewElements[1] = "input";
    listNewElements[2] = "input";
    listNewElements[3] = "input";
    listNewElements[4] = "input";
    listNewElements[5] = "input";
    listNewElements[6] = "input";
    listNewElements[7] = "input";
    listNewElements[8] = "input";

    var listNewElementsID = new Array();
    listNewElementsID[0] = "ProductID";
    listNewElementsID[1] = "OrderContent";
    listNewElementsID[2] = "GoodsType";
    listNewElementsID[3] = "SpecsModels";
    listNewElementsID[4] = "Amount";
    listNewElementsID[5] = "UnitPrice";
    listNewElementsID[6] = "Discounts";
    listNewElementsID[7] = "Total";
    listNewElementsID[8] = "txtRemark";

    var listCheck = new Array();
    listCheck[0] = "y";
    listCheck[1] = "n";
    listCheck[2] = "n";
    listCheck[3] = "n";
    listCheck[4] = "n";
    listCheck[5] = "n";
    listCheck[6] = "n";
    listCheck[7] = "n";
    listCheck[8] = "n";


    var listcontent = new Array();

    var tableGX = new Table(null, null, null, null, null, null, null, null);
    tableGX.addNewRow('myTable', listtypes, listNewElements, listcontent, 'DetailInfo', listNewElementsID, listCheck);
    var tbody = document.getElementById("DetailInfo");
    var rowCount = tbody.rows.length;
}

//function CheckDetail() {
//    ShowIframe1("选择货品信息", "../SalesManage/ChangeProduct", 500, 500);
//}

function DelRow() {
    var tbody = document.getElementById("DetailInfo");
    var rowCount = tbody.rows.length;
    if (rowCount == 0) {
        alert("当前没有可删除的行！");
        return;
    }

    var listtypeNames = new Array();
    listtypeNames[0] = "ProductID";
    listtypeNames[1] = "OrderContent";
    listtypeNames[2] = "GoodsType";
    listtypeNames[3] = "SpecsModels";
    listtypeNames[4] = "Amount";
    listtypeNames[5] = "UnitPrice";
    listtypeNames[6] = "Discounts";
    listtypeNames[7] = "Total";
    listtypeNames[8] = "txtRemark";

    var tableGX = new Table(null, null, null, null, null, null, null, null);
    tableGX.removeRow('myTable', 'DetailInfo', listtypeNames);
}


function selRow(curRow) {
    newRowID = curRow.id;
}