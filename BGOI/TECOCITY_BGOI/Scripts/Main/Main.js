var Role;
var UnitId;
var UserID;
var EJob;
var wfor;
$(document).ready(function () {
    Role = $("#Role").val();
    UnitId = $("#UnitId").val();
    //领导审批弹框提示
    UserID = $("#UserID").val();
    if (UserID == "1") {
        $("#rightMake").css({ width: "400px;" });
        $("#ReImage").show();
        $("#ReA").show();
        //document.getElementById("rightMake").style.width = "400px;"
        //document.getElementById("ReImage").style.display = ""
        //document.getElementById("ReA").style.display = ""
    }
    else {
        $("#rightMake").css({ width: "300px;" });
        $("#ReImage").hide();
        $("#ReA").hide();
        //document.getElementById("rightMake").style.width = "300px;"
        //document.getElementById("ReImage").style.display = "none"
        //document.getElementById("ReA").style.display = "none"
    }
    wfor = setInterval("selectValidate()", 60000);

    EJob = $("#EJob").val();
    userName = $("#UserName").val();

    //document.cookie = $("#UserID").val();
    //window.setTimeout("checkUser()", 1000);
    Loadbas();

    $("#pageAll").height($(window).height());
    $("#MFun").height($(window).height() - $("#Header").height() - $("#FMenu").height() - $("#footer").height() - 11);
    LoadLR();
    tick();
    //只有在基础管理弹出提示框判断
    if (Role == 6) {
        //document.getElementById("logname").style.display = "";
        wfor = setTimeout("selectNewSign()", 2000);

    }
    //else {
    //    document.getElementById("logname").style.display = "none";
    //}
    //if ($("#UnitID").val() == "100001")// 只有北郊库房可以弹框报警 
    // getLowCount();
    if (Role == 4 && UnitId == "47")//回款提示
    {
        ShowReceivePaymentManage();
    }
    if (Role == 4 && UnitId != "37" && UnitId != "47")
        ShowSalesAlarm();
    if (Role == 4 && UnitId == "37")
        //ShowRetailManage();
        // setTimeout("ShowRetailManage()", 1000);
        window.setInterval("ShowRetailManage()", 1000);
    if ((EJob == "董事长" && EJob == "总经理" && EJob == "副总经理" && EJob == "总工程师") || Role == 6) {
        //debugger
        ShowSPWarm();
    }


    //售后待审批提示
    if (Role == 10) {
       // if (EJob == "经理" || EJob == "副总经理" || EJob == "总经理") {
       
        window.setInterval("ConSP();", 5000);
      //  }
    }
    //库存提示
    if (Role == 7) {
        // if (EJob == "经理" || EJob == "副总经理" || EJob == "总经理") {
        window.setInterval("getruchuNumnew();", 5000);
        //  }
    }
})

function Loadbas() {
    $.post("../SuppliesManage/GetBas?userid=" + UserID, function (data) {
        if (data != null) {
            var strHtml = "";
            var myHtml = "";
            var comnamec = "";
            var objTask = JSON.parse(data);
            for (var i = 0; i < objTask.length; i++) {
                comnamec = objTask[i]["COMShortName"];
                strHtml = "<span><a style='color:red;'>" + "供应商" + comnamec + "有更新信息" + "</a></span>";
                Showbas();
            }
            $("#BasContent").html(strHtml);
        }
        else {
            $("div[id=BasContent]", "div[id=UpdateBas]").hide();
        }
    });
}
function selectNewSign() {
    var fid = "";
    var sid2 = "";
    var sid = "";
    $.ajax({
        type: "post",
        url: "SignJudge",
        data: {},
        dataType: "Json",
        success: function (data) {
            if (data.success == "false") {
                return;
            }
            else {
                if (document.getElementById("alarm").style.display == "block") {
                    clearInterval(wfor);
                    //$("div[id=content]", "div[id=alarm]").hide();
                }
                else {
                    document.getElementById("alarm").style.display = "block";
                    var warn = "";
                    warn = data.Msg;

                    var crr = warn.split('c');
                    var err = warn.split('e');
                    // cYS0014eYS0013125

                    sid = warn.substring(1, 7);
                    fid = warn.substring(14, 17);
                    sid2 = warn.substring(8, 14);

                    var warnstring = "";
                    if (crr.length > 1)
                        var url = '../SuppliesManage/RemarkFile?sid=' + sid + '&fid=' + fid;
                    warnstring += "<span><a style='cursor:pointer;color:red;' onclick=TurnTo('" + url + "')>供应商" + sid + "有即将到期的持证资质提醒</a></br></br>";

                    if (err.length > 1)
                        var url2 = '../SuppliesManage/RemarkCertifity?sid=' + sid2;

                    warnstring += "<span><a style='cursor:pointer;color:red;' onclick=TurnTo('" + url2 + "')>供应商" + sid2 + "有即将到期的证书提醒</a></br></br>";

                    $("#content").html(warnstring);
                    clearInterval(wfor);
                }
            }
        }
    })
}
//售后待审批提示
function ConSP() {
    $.ajax({
        type: "post",
        url: "../CustomerService/ConSP?userid=" + UserID,
        data: {},
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                var warnstring = "";
                for (var i = 0; i < json.length; i++) {
                    $("#ConHTSP").show();
                    //提示待审批合同
                    if (json[i].ApprovalContent == "售后部门合同审批") {
                        warnstring += "<p><a style='cursor:pointer;color:red;' onclick=\"TurnTo('../CustomerService/ContractProcessing');Back(this) \">售后合同审批-待审批有 :" + json[i].num + "</a></p>";
                    }
                    if (json[i].ApprovalContent == "售后处理记录审批") {
                        warnstring += "<p><a style='cursor:pointer;color:red;' onclick=\"TurnTo('../CustomerService/AppProcessing');Back(this) \">售后处理记录审批-待审批有 :" + json[i].num + "</a></p>";
                    }
                    if (json[i].ApprovalContent == "售后收款记录审批") {
                        warnstring += "<p><a style='cursor:pointer;color:red;' onclick=\"TurnTo('../CustomerService/BillingRecordsProcessing');Back(this) \">售后收款记录审批-待审批有 :" + json[i].num + "</a></p>";
                    }
                    if (json[i].ApprovalContent == "售后开票记录审批") {
                        warnstring += "<p><a style='cursor:pointer;color:red;' onclick=\"TurnTo('../CustomerService/KPProcessing');Back(this) \">售后开票记录审批-待审批有 :" + json[i].num + "</a></p>";
                    }
                }
                $("#ConContent").html(warnstring);
            }
        }
    })
}
//库存提醒
function getruchuNum() {
    $.ajax({
        type: "post",
        //url: "../CustomerService/ConSP?userid=" + UserID,
        url: "../InventoryManage/GetNumTiXinRu",
        data: {},
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    $("#alarm1").show();
                    var warnstring = "";
                    //入库
                    if (json[0].jb != "" && json[0].jb != 0) {
                        warnstring += "<p><a style='cursor:pointer;color:red;' onclick=\"TurnTo('../InventoryManage/BasicStockIn');Back(this)\">基本未入库:'" + json[0].jb + "'</a></p>";
                    }
                    if (json[0].cg != "" && json[0].cg != 0) {
                        warnstring += "<p><a style='cursor:pointer;color:red;' onclick=\"TurnTo('../InventoryManage/ProcureStockIn');Back(this)\">采购未入库:'" + json[0].cg + "'</a></p>";
                    }
                    if (json[0].th != "" && json[0].th != 0) {
                        warnstring += "<p><a style='cursor:pointer;color:red;' onclick=\"TurnTo('../InventoryManage/TestStockIn');Back(this)\">退货检验单未入库:'" + json[0].th + "'</a></p>";
                    }
                    if (json[0].cj != "" && json[0].cj != 0) {
                        warnstring += "<p><a style='cursor:pointer;color:red;' onclick=\"TurnTo('../InventoryManage/ProtoStockIn');Back(this)\">撤样机未调拨:'" + json[0].cj + "'</a></p>";
                    }
                    if (json[0].sc != "" && json[0].sc != 0) {
                        warnstring += "<p><a style='cursor:pointer;color:red;' onclick=\"TurnTo('../InventoryManage/ProductionStockIn');Back(this)\">生产组装未入库:'" + json[0].sc + "'</a></p>";
                    }
                    //出库
                    if (json[0].jbc != "" && json[0].jbc != 0) {
                        warnstring += "<p><a style='cursor:pointer;color:red;' onclick=\"TurnTo('../InventoryManage/BasicStockOut');Back(this)\">基本未出库:'" + json[0].jbc + "'</a></p>";
                    }
                    if (json[0].ls != "" && json[0].ls != 0) {
                        warnstring += "<p><a style='cursor:pointer;color:red;' onclick=\"TurnTo('../InventoryManage/RetailSalesOut');Back(this)\">销售订单未出库:'" + json[0].ls + "'</a></p>";
                    }

                    if (json[0].xm != "" && json[0].xm != 0) {
                        warnstring += "<p><a style='cursor:pointer;color:red;' onclick=\"TurnTo('../InventoryManage/ProjectSalesOut');Back(this)\">项目销售未出库:'" + json[0].xm + "'</a></p>";
                    }

                    if (json[0].rj != "" && json[0].rj != 0) {
                        warnstring += "<p><a style='cursor:pointer;color:red;' onclick=\"TurnTo('../InventoryManage/SecondOut');Back(this)\">二级库未出库:'" + json[0].rj + "'</a></p>";
                    }

                    if (json[0].sx != "" && json[0].sx != 0) {
                        warnstring += "<p><a style='cursor:pointer;color:red;' onclick=\"TurnTo('../InventoryManage/ProtoUpOut')\">上样机未出库:'" + json[0].sx + "'</a></p>";
                    }

                    if (json[0].ng != "" && json[0].ng != 0) {
                        warnstring += "<p><a style='cursor:pointer;color:red;' onclick=\"TurnTo('../InventoryManage/BuyGiveOut')\">内购/赠送出库:'" + json[0].ng + "'</a></p>";
                    }
                    $("#content1").html(warnstring);
                }
            }
        }
    })

}
function getruchuNumnew() {
    $.ajax({
        type: "post",
        url: "../InventoryManage/GetNumTiXinRuNew",
        data: {},
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    $("#alarm1").show();
                    var warnstring = "";
                    //入库
                    if (json[0].cgr != "0") {
                        warnstring += "<p><a style='cursor:pointer;color:red;' onclick=\"TurnTo('../InventoryManage/ProcureStockIn');Back(this)\">有未签收采购单 :'" + json[0].cgr + "'</a></p>";
                    }
                    if (json[1].cgr != "0" && UnitId == "47") {
                        warnstring += "<p><a style='cursor:pointer;color:red;' onclick=\"TurnTo('../InventoryManage/TestStockIn');Back(this)\">有未签收退货检验单:'" + json[1].cgr + "'</a></p>";
                    }
                    if (json[2].cgr != "0" && UnitId == "37") {
                        warnstring += "<p><a style='cursor:pointer;color:red;' onclick=\"TurnTo('../InventoryManage/ProtoStockIn');Back(this)\">有未签收撤样机入库单:'" + json[2].cgr + "'</a></p>";
                    }
                    if (json[3].cgr != "0" && UnitId == "47") {
                        warnstring += "<p><a style='cursor:pointer;color:red;' onclick=\"TurnTo('../InventoryManage/ProductionStockIn');Back(this)\">有未签收生产组装入库单:'" + json[3].cgr + "'</a></p>";
                    }
                    //出库
                    if (json[4].cgr != "0" && UnitId == "37") {
                        warnstring += "<p><a style='cursor:pointer;color:red;' onclick=\"TurnTo('../InventoryManage/RetailSalesOut');Back(this)\">有未签收销售订单出库单:'" + json[4].cgr + "'</a></p>";
                    }
                    if (json[5].cgr != "0" && UnitId == "47") {
                        warnstring += "<p><a style='cursor:pointer;color:red;' onclick=\"TurnTo('../InventoryManage/ProjectSalesOut');Back(this)\">有未签收项目销售出库单:'" + json[5].cgr + "'</a></p>";
                    }
                    if (json[6].cgr != "0" && UnitId == "37") {
                        warnstring += "<p><a style='cursor:pointer;color:red;' onclick=\"TurnTo('../InventoryManage/ProtoUpOut')\">有未签收上样机出库单 :'" + json[6].cgr + "'</a></p>";
                    }
                    if (json[7].cgr != "0" && UnitId == "37") {
                        warnstring += "<p><a style='cursor:pointer;color:red;' onclick=\"TurnTo('../InventoryManage/BuyGiveOut')\">有未签收内购单 :'" + json[7].cgr + "'</a></p>";
                    }
                    if (json[8].cgr != "0" && UnitId == "47") {
                        warnstring += "<p><a style='cursor:pointer;color:red;' onclick=\"TurnTo('../InventoryManage/ProductionMaterials');Back(this)\">有未签收生产领料单:'" + json[8].cgr + "'</a></p>";
                    }
                    if (json[9].cgr != "0" && UnitId == "37") {
                        warnstring += "<p><a style='cursor:pointer;color:red;' onclick=\"TurnTo('../InventoryManage/HomeProductSales');Back(this)\">有未签收家用销售订单:'" + json[9].cgr + "'</a></p>";
                    }
                  
                    $("#content1").html(warnstring);
                }
            }
        }
    })

}
function closeKC() {
    document.getElementById("alarm1").style.display = "none";
}
function closeCon() {
    document.getElementById("ConHTSP").style.display = "none";
    clearInterval(wfor);
}
function ShowSalesAlarm() {
    var unitId = $("#UnitId").val();
    var SalesType = "";
    if (unitId == "47")
        SalesType = "Project"
    else
        SalesType = "Retail";

    var nowInfo = $("#curContent").html();
    $.post("../SalesRetail/GetNowRemind?SalesType=" + SalesType, function (data) {
        if (data != null) {
            var strHtml = "";
            var myHtml = "";
            var objTask = JSON.parse(data);
            for (var i = 0; i < objTask.length; i++) {
                //if (SalesType == "Project") {
                //    strHtml = objTask[i]["SignContent"] + "日有回款";
                //    myHtml += "<span><a id='curContent' style='color:red;' onclick=\"TurnTo('../SalesRetail/SalesRemind?SalesType=Project')\">" + strHtml + "</a></br></br>";
                //}
                //if {
                strHtml = "您有新的家用产品销售抄送";
                var TaskType = objTask[i]["TaskType"];
                if (TaskType == "Prototype")
                    myHtml += "<span><a id='curContent' style='color:red;' onclick=\"TurnTo('../SalesRetail/PropertyManage')\">" + strHtml + "</a></br></br>";
                else if (TaskType == "Internal")
                    myHtml += "<span><a id='curContent' style='color:red;' onclick=\"TurnTo('../SalesRetail/InternalManage?op=NG')\">" + strHtml + "</a></br></br>";
                else if (TaskType == "Send")
                    myHtml += "<span><a id='curContent' style='color:red;' onclick=\"TurnTo('../SalesRetail/InternalManage?op=ZS')\">" + strHtml + "</a></br></br>";
                else if (TaskType == "Shoppe")
                    myHtml += "<span><a id='curContent' style='color:red;' onclick=\"TurnTo('../SalesRetail/ShoppeManage')\">" + strHtml + "</a></br></br>";
                else if (TaskType == "Promotion")
                    myHtml += "<span><a id='curContent' style='color:red;' onclick=\"TurnTo('../SalesRetail/PromotionManage')\">" + strHtml + "</a></br></br>";
                else if (TaskType == "Market")
                    myHtml += "<span><a id='curContent' style='color:red;' onclick=\"TurnTo('../SalesRetail/MarketSalesManage')\">" + strHtml + "</a></br></br>";
                //}
            }


            $("#noticecon").html(myHtml);
            ShowMessage();
        }
        else {
            $("div[id=noticecon]", "div[id=divMessage]").hide();
        }
    });
}
function ShowMessage() {
    $("#divMessage").css({ "right": "0px", "bottom": "1px" });
    $("#divMessage").slideDown("slow");
    $(window).scroll(function () {
        $("#divMessage").css({ "right": "1px" });
        $("#divMessage").css("bottom", "-" + document.documentElement.scrollTop + "px");
    }).resize(function () {
        $("#divMessage").css({ "right": "1px" });
        $("#divMessage").css("bottom", "-" + document.documentElement.scrollTop + "px");
    });

}
//添加消息提示和库存售后关联
function ShowRetailManage() {
    var unitId = $("#UnitId").val();
    var SalesType = "";
    if (unitId == "47")
        SalesType = "Project"
    else
        SalesType = "Retail";
    var nowInfo = $("#curContent").html();
    $("#noticecon2").html("");
    $.ajax({
        url: "../SalesRetail/GetTopRetailLibraryTubeManage",
        type: "post",
        dataType: "json",
        async: false, //是否异步
        success: function (data) {
            if (data == "[]") {
                $("div[id=noticecon2]", "div[id=divRetailMessage]").hide();
                return;
            }
            if (data != null) {
                var strHtml = "";
                var myHtml = "";
                var objTask = JSON.parse(data);
                for (var i = 0; i < objTask.length; i++) {
                    if (SalesType == "Project") {
                    }
                    else {
                        strHtml = "销售记录状态有更新";
                        var OrderID = objTask[i]["OrderID"];
                        var OPerator = objTask[i]["Operator"];
                        var OperationContent = objTask[i]["OperationContent"];
                        var OperTime = objTask[i]["OperationTime"];
                        myHtml += "<span><a id='curContent' style='color:red;' onclick=\"TurnTo('../SalesRetail/RetailLibraryTubeManage')\">" + strHtml + "记录编号为：" + OrderID + "操作人：" + OPerator + "操作内容" + OperationContent + "操作时间" + OperTime + "</a></br></br>";

                    }
                }


                $("#noticecon2").append(myHtml);
                ShowRetailManageDIV();
            }
            else {
                $("div[id=noticecon2]", "div[id=divRetailMessage]").hide();
            }
        }
    });
    $.ajax({
        url: "../SalesRetail/GetTopRetailAfterSaleManage",
        type: "post",
        dataType: "json",
        async: false, //是否异步
        success: function (data) {
            if (data == "[]") {
                $("div[id=noticecon2]", "div[id=divRetailMessage]").hide();
                return;
            }
            if (data != null) {
                var strHtml = "";
                var myHtml = "";
                var objTask = JSON.parse(data);
                for (var i = 0; i < objTask.length; i++) {
                    if (SalesType == "Project") {
                    }
                    else {
                        strHtml = "销售记录状态有更新";
                        var OrderID = objTask[i]["OrderID"];
                        var OPerator = objTask[i]["Operator"];
                        var OperationContent = objTask[i]["OperationContent"];
                        var OperTime = objTask[i]["OperationTime"];
                        myHtml += "<span><a id='curContent' style='color:red;' onclick=\"TurnTo('../SalesRetail/RetailAfterSaleManage')\">" + strHtml + "记录编号为：" + OrderID + "操作人：" + OPerator + "操作内容" + OperationContent + "操作时间" + OperTime + "</a></br></br>";

                    }
                }
                $("#noticecon2").append(myHtml);
                ShowRetailManageDIV();
            }
            else {
                $("div[id=noticecon2]", "div[id=divRetailMessage]").hide();
            }
        }
    });
}
function ShowRetailManageDIV() {
    $("#divRetailMessage").css({ "right": "0px", "bottom": "1px" });
    $("#divRetailMessage").slideDown("slow");
    $(window).scroll(function () {
        $("#divRetailMessage").css({ "right": "1px" });
        $("#divRetailMessage").css("bottom", "-" + document.documentElement.scrollTop + "px");
    }).resize(function () {
        $("#divRetailMessage").css({ "right": "1px" });
        $("#divRetailMessage").css("bottom", "-" + document.documentElement.scrollTop + "px");
    });

}

//订单回款
function ShowReceivePaymentManage() {
    var unitId = $("#UnitId").val();
    var nowInfo = $("#curContent").html();
    $("#ReceivePayment").html("");
    $.ajax({
        url: "../SalesManage/GetTopShowReceivePayment",
        type: "post",
        dataType: "json",
        async: false, //是否异步
        success: function (data) {
            if (data == "[]") {
                $("div[id=ReceivePayment]", "div[id=divReceivePaymentMessage]").hide();
                return;
            }

            if (data != null) {
                var strHtml = "";
                var myHtml = "";
                var objTask = JSON.parse(data);
                for (var i = 0; i < objTask.length; i++) {
                    strHtml = "回款状态有更新";
                    var OrderID = objTask[i]["OrderID"];
                    var CreateUser = objTask[i]["CreateUser"];
                    var PayTime = objTask[i]["PayTime"];
                    // var OperTime = objTask[i]["OperationTime"];
                    myHtml += "<span><a id='curContent' style='color:red;' onclick=\"TurnTo('../SalesManage/SHowReceivePayment')\">" + strHtml + "记录编号为：" + OrderID + "操作人：" + CreateUser + "操作时间" + PayTime + "</a></br></br>";
                }
                $("#ReceivePayment").append(myHtml);
                ShowReceivePaymentManageDiv();
            }
            else {
                $("div[id=ReceivePayment]", "div[id=divReceivePaymentMessage]").hide();
            }
        }
    });
}
function ShowReceivePaymentManageDiv() {
    $("#divReceivePaymentMessage").css({ "right": "0px", "bottom": "1px" });
    $("#divReceivePaymentMessage").slideDown("slow");
    $(window).scroll(function () {
        $("#divReceivePaymentMessage").css({ "right": "1px" });
        $("#divReceivePaymentMessage").css("bottom", "-" + document.documentElement.scrollTop + "px");
    }).resize(function () {
        $("#divReceivePaymentMessage").css({ "right": "1px" });
        $("#divReceivePaymentMessage").css("bottom", "-" + document.documentElement.scrollTop + "px");
    });
}


function ShowSPWarm() {
    //var newInfo = $("#detailContent").html();
    $.post("../SuppliesManage/GetApproval?userid=" + UserID, function (data) {
        if (data != null) {
            var strHtml = "";
            var myHtml = "";
            var objTask = JSON.parse(data);//objTask[i]["job"]
            for (var i = 0; i < objTask.length; i++) {
                if (UserID == "124" || UserID == "125" || UserID == "129") {
                    if (objTask[i]["State"] == "3") {
                        strHtml = userName + "有待会签审批信息,请进行操作";
                        myHtml += "<span><a id='detailContent' style='color:red;'\" onclick=TurnTo('../SuppliesManage/Certificate');Back(this)>" + strHtml + "</a></br></br>";
                        Show();
                    }
                    if (objTask[i]["State"] == "22" || objTask[i]["State"] == "22" || objTask[i]["State"] == "28") {
                        strHtml = userName + "有准出审批信息,请进行操作";
                        myHtml += "<span><a id='detailContent' style='color:red;'\" onclick=TurnTo('../SuppliesManage/SupplyOK');Back(this)>" + strHtml + "</a></br></br>";
                        Show();
                    }
                    if (objTask[i]["State"] == "51") {
                        strHtml = userName + "有恢复供应商的信息,请进行操作";
                        myHtml += "<span><a id='detailContent' style='color:red;'\" onclick=TurnTo('../SuppliesManage/SupplyWeigui');Back(this)>" + strHtml + "</a></br></br>";
                        Show();
                    }
                    if (objTask[i]["State"] == "73") {
                        strHtml = userName + "有恢复供货的信息,请进行操作";
                        myHtml += "<span><a id='detailContent' style='color:red;'\" onclick=TurnTo('../SuppliesManage/SupplyWeigui');Back(this)>" + strHtml + "</a></br></br>";
                        Show();
                    }
                    if (objTask[i]["NState"] == "61") {
                        strHtml = userName + "有年度评审信息,请进行操作";
                        myHtml += "<span><a id='detailContent' style='color:red;'\" onclick=TurnTo('../SuppliesManage/SupplyApproval');Back(this)>" + strHtml + "</a></br></br>";
                        Show();
                    }

                }
            }
            $("#NoticeContent").html(myHtml);
        }
        else {
            $("div[id=NoticeContent]", "div[id=SupAlarm]").hide();
        }
    });
}
function Show() {
    $("#SupAlarm").css({ "right": "0px", "bottom": "1px" });
    $("#SupAlarm").slideDown("slow");
    $(window).scroll(function () {
        $("#SupAlarm").css({ "right": "1px" });
        $("#SupAlarm").css("bottom", "-" + document.documentElement.scrollTop + "px");
    }).resize(function () {
        $("#SupAlarm").css({ "right": "1px" });
        $("#SupAlarm").css("bottom", "-" + document.documentElement.scrollTop + "px");
    });
}
function Showbas() {
    $("#UpdateBas").css({ "right": "0px", "bottom": "1px" });
    $("#UpdateBas").slideDown("slow");
    $(window).scroll(function () {
        $("#UpdateBas").css({ "right": "1px" });
        $("#UpdateBas").css("bottom", "-" + document.documentElement.scrollTop + "px");
    }).resize(function () {
        $("#UpdateBas").css({ "right": "1px" });
        $("#UpdateBas").css("bottom", "-" + document.documentElement.scrollTop + "px");
    });
}
function CloseMessage() {
    $("#divMessage").hide();
}
function CloseRetailMessage() {
    $("#divRetailMessage").hide();
}
function CloseReceivePaymentMessage() {
    $("#divReceivePaymentMessage").hide();
}

//可以隐藏弹出的窗口
function ClosePage() {
    $("#SupAlarm").hide();
}
function ClosePages() {
    $("#UpdateBas").hide();

}
function TurnTo(url) {
    parent.document.getElementById('iframeRight').src = url;
}
function closeAlarm() {
    document.getElementById("alarm").style.display = "none";
    clearInterval(wfor);
}
function CloseDialog() {
    ClosePop();
}

function OpenDialog(titleInfo, address, width, height, event) {
    ShowIframe1(titleInfo, address, width, height, event)
}

function LoadLR() {
    if (Role == "3") {
        document.getElementById("iframeLeftTree").src = "Left_ProjectManage";
        document.getElementById("iframeRight").src = "../ProjectManage/General";
    }
    else if (Role == "2") {
        document.getElementById("iframeLeftTree").src = "Left_FlowMeterManage";
        document.getElementById("iframeRight").src = "../FlowMeterManage/CardManage";
    }
    else if (Role == "4" && UnitId != "37") {
        document.getElementById("iframeLeftTree").src = "Left_SalesManage";
        document.getElementById("iframeRight").src = "../SalesManage/RecordManage";
    }
    else if (Role == "4" && UnitId == "37") {
        document.getElementById("iframeLeftTree").src = "Left_SalesManage";
        document.getElementById("iframeRight").src = "../SalesRetail/SalesRetailManage";
    }
    else if (Role == "9") {
        document.getElementById("iframeLeftTree").src = "Left_PPManage";
        document.getElementById("iframeRight").src = "../PPManage/IndexOrder";
    }
    else if (Role == "6") {
        document.getElementById("iframeLeftTree").src = "Left_SuppliesManage";
        document.getElementById("iframeRight").src = "../SuppliesManage/SupplyMan";
    }
    else if (Role == "7") {
        if (UnitId == "36") {
            document.getElementById("iframeLeftTree").src = "Left_InventoryManage";
            document.getElementById("iframeRight").src = "../InventoryManage/BasicStockIn";
        } else {
            document.getElementById("iframeLeftTree").src = "Left_InventoryManage";
            document.getElementById("iframeRight").src = "../InventoryManage/InventoryFirstPage";
        }

    }
    else if (Role == "8") {
        document.getElementById("iframeLeftTree").src = "Left_ProduceManage";
        document.getElementById("iframeRight").src = "../ProductPlanManage/ProductPlanGrid";
    }
    else if (Role == "10") {
        document.getElementById("iframeLeftTree").src = "Left_CustomerService";
        document.getElementById("iframeRight").src = "../CustomerService/UserVisit";
    }

}

function showLocale(objD) {
    var str, colorhead, colorfoot;
    var yy = objD.getYear();
    if (yy < 1900) yy = yy + 1900;
    var MM = objD.getMonth() + 1;
    if (MM < 10) MM = '0' + MM;
    var dd = objD.getDate();
    if (dd < 10) dd = '0' + dd;
    var hh = objD.getHours();
    if (hh < 10) hh = '0' + hh;
    var mm = objD.getMinutes();
    if (mm < 10) mm = '0' + mm;
    var ss = objD.getSeconds();
    if (ss < 10) ss = '0' + ss;
    var ww = objD.getDay();
    if (ww == 0) colorhead = "<font color=\"#333333\">";
    if (ww > 0 && ww < 6) colorhead = "<font color=\"#333333\">";
    if (ww == 6) colorhead = "<font color=\"#333333\">";
    if (ww == 0) ww = "星期日";
    if (ww == 1) ww = "星期一";
    if (ww == 2) ww = "星期二";
    if (ww == 3) ww = "星期三";
    if (ww == 4) ww = "星期四";
    if (ww == 5) ww = "星期五";
    if (ww == 6) ww = "星期六";
    colorfoot = "</font>"
    str = colorhead + yy + "年" + MM + "月" + dd + "日" + " " + hh + ":" + mm + ":" + ss + " " + ww + colorfoot;
    return (str);
}
function tick() {
    var today;
    today = new Date();
    document.getElementById("localtime").innerHTML = showLocale(today);
    window.setTimeout("tick()", 1000);
}

// 获取
function getLowCount() {
    var curPage = 1;
    var OnePageCount = 10;
    $.ajax({
        url: "../InventoryManage/MaterialBasicData",
        type: "post",
        data: { curpage: curPage, rownum: OnePageCount, level: "" },
        dataType: "json",
        async: false, //是否异步
        success: function (data) {
            if (data != "") {
                // window.parent.OpenDialog1('库存报警', '../InventoryManage/LowWarn', 400, 170);
                window.parent.parent.OpenDialog('库存报警', '../InventoryManage/LowWarn', 400, 170);
            }
        }
    })
}

function checkUser() {
    //var UserID = $("#UserID").val();
    if (document.cookie != UserID) {
        location.href = "../Account/Login";
    }
    window.setTimeout("checkUser()", 1000);
}
window.onload = function () {
    document.getElementsByTagName("body")[0].onkeydown = function () {

        //获取事件对象  
        var elem = event.relatedTarget || event.srcElement || event.target || event.currentTarget;

        if (event.keyCode == 8) {//判断按键为backSpace键  

            //获取按键按下时光标做指向的element  
            var elem = event.srcElement || event.currentTarget;

            //判断是否需要阻止按下键盘的事件默认传递  
            var name = elem.nodeName;

            if (name != 'INPUT' && name != 'TEXTAREA') {
                return _stopIt(event);
            }
            var type_e = elem.type.toUpperCase();
            if (name == 'INPUT' && (type_e != 'TEXT' && type_e != 'TEXTAREA' && type_e != 'PASSWORD' && type_e != 'FILE')) {
                return _stopIt(event);
            }
            if (name == 'INPUT' && (elem.readOnly == true || elem.disabled == true)) {
                return _stopIt(event);
            }
        }
    }

    function _stopIt(e) {
        if (e.returnValue) {
            e.returnValue = false;
        }
        if (e.preventDefault) {
            e.preventDefault();
        }

        return false;
    }
}

function UpdatePwd() {
    ShowIframe1("修改密码", "UpdatePwd", 400, 150, '');
}

function RestPwd() {
    ShowIframe1("重置密码", "ResetPwd", 400, 150, '');
}

function selectValidate() {
    $.ajax({
        type: "post",
        url: "ValidateJudge",
        data: {},
        dataType: "Json",
        success: function (data) {
            if (data.success == "false") {
                clearInterval(wfor);
                return;
            }
            else {
                alert("您的密码经过重置后,只能使用一天,到期时间为 " + data.Msg + " ,请立刻修改密码");
            }
        }
    })
}