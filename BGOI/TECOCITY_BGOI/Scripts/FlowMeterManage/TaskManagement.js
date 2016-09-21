var curPage = 1;
var OnePageCount = 15;
var oldSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    LoadCardList();// 登记卡
    LoadIncomingInspectionList();//进厂检测
    LoadCleanRepairList();//清洗记录
    LoadRepairInfoList();//维修记录
    LoadFactoryInspectionList();//出厂检测
    LoadMaintenanceInspectionList();//维修检测
    LoadProcedureList();//过程记录
    LoadQuotationList();//报价单
    $('#JCJC').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
        if (rowid == null) {
            alert("请在列表中选择一条数据"); return;
        }
        else {
            var modletype = jQuery("#list").jqGrid('getRowData', rowid).ModelType;
            $.ajax({
                url: "Operate",
                type: "post",
                data: { P: "3", RID: rID, str: $(this).val(), type: modletype },
                dataType: "Json",
                success: function (data) {
                    if (data.success == "true") {

                        if (modletype == "超声波")
                            window.parent.OpenDialog("新增检测记录", "../FlowMeterManage/AddCheckData2?RepairType=进厂检测&RID=" + rID, 1000, 500, '');
                        else
                            window.parent.OpenDialog("新增检测记录", "../FlowMeterManage/AddCheckData?RepairType=进厂检测&RID=" + rID + "&type=" + modletype, 1000, 500, '');

                    }
                    else {
                        alert(data.Msg);
                        return;
                    }
                    reload1();
                }
            });


        }
    });

    $('#KSQX').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
        if (rowid == null) {
            alert("请在列表中选择一条数据"); return;
        }
        else {
            var modletype = jQuery("#list").jqGrid('getRowData', rowid).ModelType;
            $.ajax({
                url: "Operate",
                type: "post",
                data: { P: "4", RID: rID, str: $(this).val(), type: modletype },
                dataType: "Json",
                success: function (data) {
                    if (data.success == "true") {
                        window.parent.OpenDialog("开始清洗", "../FlowMeterManage/StartCleanRepair?RID=" + rID, 400, 250, '');
                    }
                    else {
                        alert(data.Msg);
                        return;
                    }
                }
            });
        }

    });
    $('#QXJL').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
        if (rowid == null) {
            alert("请在列表中选择一条数据"); return;
        }
        else {
            var modletype = jQuery("#list").jqGrid('getRowData', rowid).ModelType;
            $.ajax({
                url: "Operate",
                type: "post",
                data: { P: "5", RID: rID, str: $(this).val(), type: modletype },
                dataType: "Json",
                success: function (data) {
                    if (data.success == "true") {
                        window.parent.OpenDialog("新增清洗记录", "../FlowMeterManage/AddCleanRepair?RID=" + rID, 700, 500, '');
                    }
                    else {
                        alert(data.Msg);
                        return;
                    }
                }
            });

        }
    });
    $('#QXJC').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
        if (rowid == null) {
            alert("请在列表中选择一条数据"); return;
        }
        else {
            var modletype = jQuery("#list").jqGrid('getRowData', rowid).ModelType;
            $.ajax({
                url: "Operate",
                type: "post",
                data: { P: "6", RID: rID, str: $(this).val(), type: modletype },
                dataType: "Json",
                success: function (data) {
                    if (data.success == "true") {
                        var modle = jQuery("#list").jqGrid('getRowData', rowid).ModelType;
                        if (modle == "超声波")
                            window.parent.OpenDialog("新增检测记录", "../FlowMeterManage/AddCheckData2?RepairType=清洗检测&RID=" + rID, 1000, 500, '');
                        else
                            window.parent.OpenDialog("新增检测记录", "../FlowMeterManage/AddCheckData?RepairType=清洗检测&RID=" + rID, 1000, 500, '');

                    }
                    else {
                        alert(data.Msg);
                        return;
                    }
                }
            });

        }
    });
    $('#KSWX').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
        if (rowid == null) {
            alert("请在列表中选择一条数据"); return;
        }
        else {


            var modletype = jQuery("#list").jqGrid('getRowData', rowid).ModelType;
            $.ajax({
                url: "Operate",
                type: "post",
                data: { P: "6", RID: rID, str: $(this).val(), type: modletype },
                dataType: "Json",
                success: function (data) {
                    if (data.success == "true") {
                        $.ajax({
                            url: "CheckDataIsThere",
                            type: "post",
                            data: { RID: rID },
                            dataType: "Json",
                            success: function (s) {

                                if (s.success == true) {
                                    window.parent.OpenDialog("开始维修", "../FlowMeterManage/StartRepair?RID=" + rID, 400, 250, '');
                                } else {
                                    alert("请先添加清洗检测");
                                    return;
                                }
                            }
                        })

                    }
                    else {
                        alert(data.Msg);
                        return;
                    }
                }
            });

        }
    });
    $('#WXJC').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
        if (rowid == null) {
            alert("请在列表中选择一条数据"); return;
        }
        else {

            var modletype = jQuery("#list").jqGrid('getRowData', rowid).ModelType;
            $.ajax({
                url: "Operate",
                type: "post",
                data: { P: "7", RID: rID, str: $(this).val(), type: modletype },
                dataType: "Json",
                success: function (data) {
                    if (data.success == "true") {
                        var modle = jQuery("#list").jqGrid('getRowData', rowid).ModelType;
                        if (modle == "超声波")
                            window.parent.OpenDialog("新增检测记录", "../FlowMeterManage/AddCheckData2?RepairType=维修检测&RID=" + rID, 1000, 500, '');
                        else
                            window.parent.OpenDialog("新增检测记录", "../FlowMeterManage/AddCheckData?RepairType=维修检测&RID=" + rID, 1000, 500, '');

                    }
                    else {
                        alert(data.Msg);
                        return;
                    }
                }
            });

        }
    });


    $('#WXJL').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
        if (rowid == null) {
            alert("请在列表中选择一条数据"); return;
        }
        else {
            var modletype = jQuery("#list").jqGrid('getRowData', rowid).ModelType;
            $.ajax({
                url: "Operate",
                type: "post",
                data: { P: "7", RID: rID, str: $(this).val(), type: modletype },
                dataType: "Json",
                success: function (data) {

                    if (data.success == "true") {
                        var modletype = jQuery("#list").jqGrid('getRowData', rowid).ModelType;
                        $.ajax({
                            url: "RepairInfoIsThere",
                            type: "post",
                            data: { RID: rID },
                            dataType: "Json",
                            success: function (data) {
                                if (data.success == "true") {
                                    window.parent.OpenDialog("修改维修记录", "../FlowMeterManage/UpdateRepairInfo?Info=" + data.id, 700, 650, '');
                                }
                                else {
                                    window.parent.OpenDialog("新增维修记录", "../FlowMeterManage/AddRepairInfo?RID=" + rID, 700, 650, '');
                                }
                            }
                        })

                    }
                    else {
                        alert(data.Msg);
                        return;
                    }
                }
            });

        }
    });









    $('#BJ').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
        if (rowid == null) {
            alert("请在列表中选择一条数据"); return;
        }
        else {


            window.parent.OpenDialog("新增报价单", "../FlowMeterManage/AddQuotation?RID=" + rID, 700, 500, '');
        }
    });
    $('#WXQR').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
        var name = jQuery("#list").jqGrid('getRowData', rowid).CustomerName;
        if (rowid == null) {
            alert("请在列表中选择一条数据"); return;
        }
        else {
            var url = "../FlowMeterManage/Servicing?RID=" + rID

            window.parent.OpenDialog("维修确认单", " ../FlowMeterManage/AddServicing?RID=" + rID, 800, 500, '');
            // window.showModalDialog(url, window, "dialogWidth:900px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");


        }
    });

    $('#DSFJC').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
        if (rowid == null) {
            alert("请在列表中选择一条数据"); return;
        }
        else {

            var modle = jQuery("#list").jqGrid('getRowData', rowid).ModelType;
            if (modle == "超声波")
                window.parent.OpenDialog("新增检测记录", "../FlowMeterManage/AddCheckData2?RepairType=第三方检测&RID=" + rID, 1000, 500, '');
            else

                window.parent.OpenDialog("新增检测记录", "../FlowMeterManage/AddCheckData?RepairType=第三方检测&RID=" + rID, 1000, 500, '');



        }
    });

    $('#FH').click(function () {

        window.parent.OpenDialog("发货", "../FlowMeterManage/SendDelivery", 700, 450, '');
    });
    $('#JCJCdiv').click(function () {
        this.className = "btnTw";
        $('#QXJLdiv').attr("class", "btnTh");
        $('#WXJLdiv').attr("class", "btnTh");
        $('#CCJCdiv').attr("class", "btnTh");
        $('#WXJCdiv').attr("class", "btnTh");
        $('#GCJLdiv').attr("class", "btnTh");
        $('#BJDdiv').attr("class", "btnTh");

        $("#IncomingInspection").css("display", "");
        $("#CleanRepair").css("display", "none");
        $("#RepairInfo").css("display", "none");
        $("#FactoryInspection").css("display", "none");
        $("#MaintenanceInspection").css("display", "none");
        $("#Procedure").css("display", "none");
        $("#Quotation").css("display", "none");
        reload1();
    })

    $('#QXJLdiv').click(function () {
        this.className = "btnTw";
        $('#JCJCdiv').attr("class", "btnTh");
        $('#WXJLdiv').attr("class", "btnTh");
        $('#CCJCdiv').attr("class", "btnTh");
        $('#WXJCdiv').attr("class", "btnTh");
        $('#GCJLdiv').attr("class", "btnTh");
        $('#BJDdiv').attr("class", "btnTh");

        $("#IncomingInspection").css("display", "none");
        $("#CleanRepair").css("display", "");
        $("#RepairInfo").css("display", "none");
        $("#FactoryInspection").css("display", "none");
        $("#MaintenanceInspection").css("display", "none");
        $("#Procedure").css("display", "none");
        $("#Quotation").css("display", "none");
        reload2();
    })

    $('#WXJLdiv').click(function () {
        this.className = "btnTw";
        $('#JCJCdiv').attr("class", "btnTh");
        $('#QXJLdiv').attr("class", "btnTh");
        $('#CCJCdiv').attr("class", "btnTh");
        $('#WXJCdiv').attr("class", "btnTh");
        $('#GCJLdiv').attr("class", "btnTh");
        $('#BJDdiv').attr("class", "btnTh");

        $("#IncomingInspection").css("display", "none");
        $("#CleanRepair").css("display", "none");
        $("#RepairInfo").css("display", "");
        $("#FactoryInspection").css("display", "none");
        $("#MaintenanceInspection").css("display", "none");
        $("#Procedure").css("display", "none");
        $("#Quotation").css("display", "none");
        reload3();
    })
    $('#CCJCdiv').click(function () {
        this.className = "btnTw";
        $('#JCJCdiv').attr("class", "btnTh");
        $('#QXJLdiv').attr("class", "btnTh");
        $('#WXJLdiv').attr("class", "btnTh");
        $('#WXJCdiv').attr("class", "btnTh");
        $('#GCJLdiv').attr("class", "btnTh");
        $('#BJDdiv').attr("class", "btnTh");

        $("#IncomingInspection").css("display", "none");
        $("#CleanRepair").css("display", "none");
        $("#RepairInfo").css("display", "none");
        $("#FactoryInspection").css("display", "");
        $("#MaintenanceInspection").css("display", "none");
        $("#Procedure").css("display", "none");
        $("#Quotation").css("display", "none");
        reload4();
    })

    $('#WXJCdiv').click(function () {
        this.className = "btnTw";
        $('#JCJCdiv').attr("class", "btnTh");
        $('#QXJLdiv').attr("class", "btnTh");
        $('#WXJLdiv').attr("class", "btnTh");
        $('#CCJCdiv').attr("class", "btnTh");
        $('#GCJLdiv').attr("class", "btnTh");
        $('#BJDdiv').attr("class", "btnTh");

        $("#IncomingInspection").css("display", "none");
        $("#CleanRepair").css("display", "none");
        $("#RepairInfo").css("display", "none");
        $("#FactoryInspection").css("display", "none");
        $("#MaintenanceInspection").css("display", "");
        $("#Procedure").css("display", "none");
        $("#Quotation").css("display", "none");
        reload5();
    })

    $('#GCJLdiv').click(function () {
        this.className = "btnTw";

        $('#JCJCdiv').attr("class", "btnTh");
        $('#QXJLdiv').attr("class", "btnTh");
        $('#WXJLdiv').attr("class", "btnTh");
        $('#WXJCdiv').attr("class", "btnTh");
        $('#CCJCdiv').attr("class", "btnTh");
        $('#BJDdiv').attr("class", "btnTh");

        $("#IncomingInspection").css("display", "none");
        $("#CleanRepair").css("display", "none");
        $("#RepairInfo").css("display", "none");
        $("#FactoryInspection").css("display", "none");
        $("#MaintenanceInspection").css("display", "none");
        $("#Procedure").css("display", "");
        $("#Quotation").css("display", "none");
        reload6();
    })


    $('#BJDdiv').click(function () {
        this.className = "btnTw";
        $('#GCJLdiv').attr("class", "btnTh");
        $('#JCJCdiv').attr("class", "btnTh");
        $('#QXJLdiv').attr("class", "btnTh");
        $('#WXJLdiv').attr("class", "btnTh");
        $('#WXJCdiv').attr("class", "btnTh");
        $('#CCJCdiv').attr("class", "btnTh");


        $("#IncomingInspection").css("display", "none");
        $("#CleanRepair").css("display", "none");
        $("#RepairInfo").css("display", "none");
        $("#FactoryInspection").css("display", "none");
        $("#MaintenanceInspection").css("display", "none");
        $("#Procedure").css("display", "none");
        $("#Quotation").css("display", "");
        reload7();
    })


});


function select() {

    reload1();
    reload2();
    reload3();
    reload4();
    reload5();
    reload6();
    reload7();
}

function LoadCardList() {
    jQuery("#list").jqGrid({
        url: 'LoadCardList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RepairID: $("#RepairID").val(), CustomerName: $("#CustomerName").val(), CustomerAddr: $("#CustomerAddr").val(),
            MeterID: $("#MeterID").val(), MeterName: $("#MeterName").val(), Model: $("#Model").val(),
            SS_Date: $("#SS_Date").val(), ES_Date: $("#ES_Date").val(), State: $("#State").val(),
            OrderDate: $("#Order").val(), CardType: $("#CardType").val()
        },
        loadonce: false,
        mtype: 'POST',
        jsonReader: {
            root: function (obj) {
                var data = eval("(" + obj + ")");
                return data.rows;
            },
            page: function (obj) {
                var data = eval("(" + obj + ")");
                return data.page;
            },
            total: function (obj) {
                var data = eval("(" + obj + ")");
                return data.total;
            },
            records: function (obj) {
                var data = eval("(" + obj + ")");
                return data.records;
            },
            repeatitems: false
        },
        colNames: ['登记卡号', '', '客户名称', '客户地址', '联系电话', '送表日期', '仪表编号', '仪表型号', '仪表名称', '状态', ''],
        colModel: [

        { name: 'RID', index: 'RID', width: 200 },
        { name: 'RepairShow', index: 'RepairShow', width: 90, hidden: true },
        { name: 'CustomerName', index: 'CustomerName', width: 200 },
        { name: 'CustomerAddr', index: 'CustomerAddr', width: 200 },
        { name: 'S_Tel', index: 'S_Tel', width: 120 },
        { name: 'S_Date', index: 'S_Date', width: 120 },
        { name: 'MeterID', index: 'MeterID', width: 100 },
        { name: 'Model', index: 'Model', width: 100 },
        { name: 'MeterName', index: 'MeterName', width: 150 },
        { name: 'State', index: 'State', width: 100 },
        { name: 'ModelType', index: 'ModelType', width: 90, hidden: true }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).RepairID + "' name='cb'/>";
                jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });

            }

        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            select();
        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pager") {
                if (curPage == $("#list").getGridParam("lastpage"))
                    return;
                curPage = $("#list").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager") {
                curPage = $("#list").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#list").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager") {
                curPage = 1;
            }
            else {
                curPage = $("#pager :input").val();
            }
            reload();
        },
        loadComplete: function () {
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 166, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function reload() {
    $("#list").jqGrid('setGridParam', {
        url: 'LoadCardList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RepairID: $("#RepairID").val(), CustomerName: $("#CustomerName").val(), CustomerAddr: $("#CustomerAddr").val(),
            MeterID: $("#MeterID").val(), MeterName: $("#MeterName").val(), Model: $("#Model").val(),
            SS_Date: $("#SS_Date").val(), ES_Date: $("#ES_Date").val(), State: $("#State").val(),
            OrderDate: $("#Order").val(), CardType: $("#CardType").val()
        },

    }).trigger("reloadGrid");
}

function LoadIncomingInspectionList() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');

    var rID = "1";
    if (rowid != null) {
        rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
    }
    var u = "LoadCheckDataList";
    if ($("#CardType").val() == "CardType2") {
        u = "LoadCheckDataList2"
    }
    jQuery("#list1").jqGrid({
        url: u,
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RID: rID, RepairType: "进厂检测",
            RepairMethod: "", CheckDate: "", CheckUser: "", type: $("#CardType").val()
        },
        loadonce: false,
        mtype: 'POST',
        jsonReader: {
            root: function (obj) {
                var data = eval("(" + obj + ")");
                return data.rows;
            },
            page: function (obj) {
                var data = eval("(" + obj + ")");
                return data.page;
            },
            total: function (obj) {
                var data = eval("(" + obj + ")");
                return data.total;
            },
            records: function (obj) {
                var data = eval("(" + obj + ")");
                return data.records;
            },
            repeatitems: false
        },
        colNames: ['登记卡号', '检测人', '检测时间', '检测类型', '检测方式'],
        colModel: [
        { name: 'RID', index: 'RID', width: 250 },
        { name: 'CheckUser', index: 'CheckUser', width: 100 },
        { name: 'CheckDate', index: 'CheckDate', width: 100 },
        { name: 'RepairType', index: 'RepairType', width: 100 },
        { name: 'RepairMethod', index: 'RepairMethod', width: 100 },



        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',

        //gridComplete: function () {
        //    var ids = jQuery("#list").jqGrid('getDataIDs');
        //    for (var i = 0; i < ids.length; i++) {
        //        var id = ids[i];
        //        var curRowData = jQuery("#list").jqGrid('getRowData', id);
        //        var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).RepairID + "' name='cb'/>";
        //        jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });

        //    }

        //},
        //onSelectRow: function (rowid, status) {
        //    if (oldSelID != 0) {
        //        $('input[id=c' + oldSelID + ']').prop("checked", false);
        //    }
        //    $('input[id=c' + rowid + ']').prop("checked", true);
        //    oldSelID = rowid;
        //},

        onPaging: function (pgButton) {
            if (pgButton == "next_pager1") {
                if (curPage == $("#list1").getGridParam("lastpage"))
                    return;
                curPage = $("#list1").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager1") {
                curPage = $("#list1").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager1") {
                if (curPage == 1)
                    return;
                curPage = $("#list1").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager1") {
                curPage = 1;
            }
            else {
                curPage = $("#pager1 :input").val();
            }
            reload1();
        },
        loadComplete: function () {
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 166, false);
            $("#list1").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function reload1() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = "1";
    if (rowid != null) {
        rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
    }
    var u = "LoadCheckDataList";
    if ($("#CardType").val() == "CardType2") {
        u = "LoadCheckDataList2"
    }
    $("#list1").jqGrid('setGridParam', {
        url: u,
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RID: rID, RepairType: "进厂检测",
            RepairMethod: "", CheckDate: "", CheckUser: "", type: $("#CardType").val()
        },

    }).trigger("reloadGrid");

}

function LoadCleanRepairList() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = "1";
    if (rowid != null) {
        rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
    }

    jQuery("#list2").jqGrid({
        url: 'LoadCleanRepairList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RID: rID, CleanUser: "", CleanSDate: "", CleanEDate: "", type: $("#CardType").val()
        },
        loadonce: false,
        mtype: 'POST',
        jsonReader: {
            root: function (obj) {
                var data = eval("(" + obj + ")");
                return data.rows;
            },
            page: function (obj) {
                var data = eval("(" + obj + ")");
                return data.page;
            },
            total: function (obj) {
                var data = eval("(" + obj + ")");
                return data.total;
            },
            records: function (obj) {
                var data = eval("(" + obj + ")");
                return data.records;
            },
            repeatitems: false
        },
        colNames: ['登记卡号', '清洗人', '清洗开始时间', '清洗结束时间'],
        colModel: [
        { name: 'RID', index: 'RID', width: 250 },
        { name: 'CleanUser', index: 'CleanUser', width: 100 },
        { name: 'CleanSDate', index: 'CleanSDate', width: 100 },
        { name: 'CleanEDate', index: 'CleanEDate', width: 100 },


        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',

        //gridComplete: function () {
        //    var ids = jQuery("#list").jqGrid('getDataIDs');
        //    for (var i = 0; i < ids.length; i++) {
        //        var id = ids[i];
        //        var curRowData = jQuery("#list").jqGrid('getRowData', id);
        //        var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).RepairID + "' name='cb'/>";
        //        jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });

        //    }

        //},
        //onSelectRow: function (rowid, status) {
        //    if (oldSelID != 0) {
        //        $('input[id=c' + oldSelID + ']').prop("checked", false);
        //    }
        //    $('input[id=c' + rowid + ']').prop("checked", true);
        //    oldSelID = rowid;
        //},

        onPaging: function (pgButton) {
            if (pgButton == "next_pager2") {
                if (curPage == $("#list2").getGridParam("lastpage"))
                    return;
                curPage = $("#list2").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager2") {
                curPage = $("#list2").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager2") {
                if (curPage == 1)
                    return;
                curPage = $("#list2").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager2") {
                curPage = 1;
            }
            else {
                curPage = $("#pager2 :input").val();
            }
            reload2();
        },
        loadComplete: function () {
            $("#list2").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 166, false);
            $("#list2").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function reload2() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = "1";
    if (rowid != null) {
        rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
    }
    $("#list2").jqGrid('setGridParam', {
        url: 'LoadCleanRepairList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RID: rID, CleanUser: "", CleanSDate: "", CleanEDate: "", type: $("#CardType").val()
        },

    }).trigger("reloadGrid");
}

function LoadRepairInfoList() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = "1";
    if (rowid != null) {
        rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
    }
    jQuery("#list3").jqGrid({
        url: 'LoadRepairInfoList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RID: rID, RepairUser: "", RepairSDate: "", RepairEDate: "", type: $("#CardType").val()
        },
        loadonce: false,
        mtype: 'POST',
        jsonReader: {
            root: function (obj) {
                var data = eval("(" + obj + ")");
                return data.rows;
            },
            page: function (obj) {
                var data = eval("(" + obj + ")");
                return data.page;
            },
            total: function (obj) {
                var data = eval("(" + obj + ")");
                return data.total;
            },
            records: function (obj) {
                var data = eval("(" + obj + ")");
                return data.records;
            },
            repeatitems: false
        },
        colNames: ['登记卡号', '维修人员', '维修开始时间', '维修结束时间'],
        colModel: [
        { name: 'RID', index: 'RID', width: 250 },
        { name: 'RepairUser', index: 'RepairUser', width: 100 },
        { name: 'RepairSDate', index: 'RepairSDate', width: 100 },
        { name: 'RepairEDate', index: 'RepairEDate', width: 100 }


        ],
        pager: jQuery('#pager3'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',

        //gridComplete: function () {
        //    var ids = jQuery("#list").jqGrid('getDataIDs');
        //    for (var i = 0; i < ids.length; i++) {
        //        var id = ids[i];
        //        var curRowData = jQuery("#list").jqGrid('getRowData', id);
        //        var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).RepairID + "' name='cb'/>";
        //        jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });

        //    }

        //},
        //onSelectRow: function (rowid, status) {
        //    if (oldSelID != 0) {
        //        $('input[id=c' + oldSelID + ']').prop("checked", false);
        //    }
        //    $('input[id=c' + rowid + ']').prop("checked", true);
        //    oldSelID = rowid;
        //},

        onPaging: function (pgButton) {
            if (pgButton == "next_pager3") {
                if (curPage == $("#list3").getGridParam("lastpage"))
                    return;
                curPage = $("#list3").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager3") {
                curPage = $("#list3").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager3") {
                if (curPage == 1)
                    return;
                curPage = $("#list3").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager3") {
                curPage = 1;
            }
            else {
                curPage = $("#pager3 :input").val();
            }
            reload3();
        },
        loadComplete: function () {
            $("#list3").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 166, false);
            $("#list3").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function reload3() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = "1";
    if (rowid != null) {
        rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
    }
    $("#list3").jqGrid('setGridParam', {
        url: 'LoadRepairInfoList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RID: rID, RepairUser: "", RepairSDate: "", RepairEDate: "", type: $("#CardType").val()

        },

    }).trigger("reloadGrid");
}


function LoadFactoryInspectionList() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = "1";
    if (rowid != null) {
        rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
    }
    var u = "LoadCheckDataList";
    if ($("#CardType").val() == "CardType2") {
        u = "LoadCheckDataList2"
    }
    jQuery("#list4").jqGrid({
        url: u,
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RID: rID, RepairType: "出厂检测",
            RepairMethod: "", CheckDate: "", CheckUser: "", type: $("#CardType").val()
        },
        loadonce: false,
        mtype: 'POST',
        jsonReader: {
            root: function (obj) {
                var data = eval("(" + obj + ")");
                return data.rows;
            },
            page: function (obj) {
                var data = eval("(" + obj + ")");
                return data.page;
            },
            total: function (obj) {
                var data = eval("(" + obj + ")");
                return data.total;
            },
            records: function (obj) {
                var data = eval("(" + obj + ")");
                return data.records;
            },
            repeatitems: false
        },
        colNames: ['登记卡号', '检测人', '检测时间', '检测类型', '检测方式'],
        colModel: [
        { name: 'RID', index: 'RID', width: 250 },
        { name: 'CheckUser', index: 'CheckUser', width: 100 },
        { name: 'CheckDate', index: 'CheckDate', width: 100 },
        { name: 'RepairType', index: 'RepairType', width: 100 },
        { name: 'RepairMethod', index: 'RepairMethod', width: 100 },



        ],
        pager: jQuery('#pager4'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',

        //gridComplete: function () {
        //    var ids = jQuery("#list4").jqGrid('getDataIDs');
        //    for (var i = 0; i < ids.length; i++) {
        //        var id = ids[i];
        //        var curRowData = jQuery("#list4").jqGrid('getRowData', id);
        //        var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list4").jqGrid('getRowData', id).RepairID + "' name='cb'/>";
        //        jQuery("#list4").jqGrid('setRowData', ids[i], { IDCheck: curChk });

        //    }

        //},
        //onSelectRow: function (rowid, status) {
        //    if (oldSelID != 0) {
        //        $('input[id=c' + oldSelID + ']').prop("checked", false);
        //    }
        //    $('input[id=c' + rowid + ']').prop("checked", true);
        //    oldSelID = rowid;
        //},

        onPaging: function (pgButton) {
            if (pgButton == "next_pager4") {
                if (curPage == $("#list4").getGridParam("lastpage"))
                    return;
                curPage = $("#list4").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager4") {
                curPage = $("#list4").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager4") {
                if (curPage == 1)
                    return;
                curPage = $("#list4").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager4") {
                curPage = 1;
            }
            else {
                curPage = $("#pager4 :input").val();
            }
            reload4();
        },
        loadComplete: function () {
            $("#list4").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 166, false);
            $("#list4").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function reload4() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = "1";
    if (rowid != null) {
        rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
    }
    var u = "LoadCheckDataList";
    if ($("#CardType").val() == "CardType2") {
        u = "LoadCheckDataList2"
    }
    $("#list4").jqGrid('setGridParam', {
        url: u,
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RID: rID, RepairType: "出厂检测",
            RepairMethod: "", CheckDate: "", CheckUser: "", type: $("#CardType").val()
        },

    }).trigger("reloadGrid");

}


function LoadMaintenanceInspectionList() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = "1";
    if (rowid != null) {
        rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
    }
    var u = "LoadCheckDataList";
    if ($("#CardType").val() == "CardType2") {
        u = "LoadCheckDataList2"
    }
    jQuery("#list5").jqGrid({
        url: u,
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RID: rID, RepairType: "维修检测",
            RepairMethod: "", CheckDate: "", CheckUser: "", type: $("#CardType").val()
        },
        loadonce: false,
        mtype: 'POST',
        jsonReader: {
            root: function (obj) {
                var data = eval("(" + obj + ")");
                return data.rows;
            },
            page: function (obj) {
                var data = eval("(" + obj + ")");
                return data.page;
            },
            total: function (obj) {
                var data = eval("(" + obj + ")");
                return data.total;
            },
            records: function (obj) {
                var data = eval("(" + obj + ")");
                return data.records;
            },
            repeatitems: false
        },
        colNames: ['登记卡号', '检测人', '检测时间', '检测类型', '检测方式'],
        colModel: [
        { name: 'RID', index: 'RID', width: 250 },
        { name: 'CheckUser', index: 'CheckUser', width: 100 },
        { name: 'CheckDate', index: 'CheckDate', width: 100 },
        { name: 'RepairType', index: 'RepairType', width: 100 },
        { name: 'RepairMethod', index: 'RepairMethod', width: 100 },



        ],
        pager: jQuery('#pager5'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',

        //gridComplete: function () {
        //    var ids = jQuery("#list4").jqGrid('getDataIDs');
        //    for (var i = 0; i < ids.length; i++) {
        //        var id = ids[i];
        //        var curRowData = jQuery("#list4").jqGrid('getRowData', id);
        //        var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list4").jqGrid('getRowData', id).RepairID + "' name='cb'/>";
        //        jQuery("#list4").jqGrid('setRowData', ids[i], { IDCheck: curChk });

        //    }

        //},
        //onSelectRow: function (rowid, status) {
        //    if (oldSelID != 0) {
        //        $('input[id=c' + oldSelID + ']').prop("checked", false);
        //    }
        //    $('input[id=c' + rowid + ']').prop("checked", true);
        //    oldSelID = rowid;
        //},

        onPaging: function (pgButton) {
            if (pgButton == "next_pager5") {
                if (curPage == $("#list5").getGridParam("lastpage"))
                    return;
                curPage = $("#list5").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager5") {
                curPage = $("#list5").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager5") {
                if (curPage == 1)
                    return;
                curPage = $("#list5").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager5") {
                curPage = 1;
            }
            else {
                curPage = $("#pager5 :input").val();
            }
            reload5();
        },
        loadComplete: function () {
            $("#list5").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 166, false);
            $("#list5").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function reload5() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = "1";
    if (rowid != null) {
        rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
    }
    var u = "LoadCheckDataList";
    if ($("#CardType").val() == "CardType2") {
        u = "LoadCheckDataList2"
    }
    $("#list5").jqGrid('setGridParam', {
        url: u,
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RID: rID, RepairType: "维修检测",
            RepairMethod: "", CheckDate: "", CheckUser: "", type: $("#CardType").val()
        },

    }).trigger("reloadGrid");

}

function LoadProcedureList() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = "1";
    if (rowid != null) {
        rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
    }
    jQuery("#list6").jqGrid({
        url: 'LoadProcedureList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RID: rID
        },
        loadonce: false,
        mtype: 'POST',
        jsonReader: {
            root: function (obj) {
                var data = eval("(" + obj + ")");
                return data.rows;
            },
            page: function (obj) {
                var data = eval("(" + obj + ")");
                return data.page;
            },
            total: function (obj) {
                var data = eval("(" + obj + ")");
                return data.total;
            },
            records: function (obj) {
                var data = eval("(" + obj + ")");
                return data.records;
            },
            repeatitems: false
        },
        colNames: ['登记卡号', '进厂检测时间', '开始清洗时间', '清洗完成时间', '开始维修时间', '维修完成时间'],
        colModel: [
        { name: 'RID', index: 'RID', width: 200 },
        { name: '进厂检测', index: '进厂检测', width: 100 },
        { name: '开始清洗', index: '开始清洗', width: 100 },
        { name: '清洗完成', index: '清洗完成', width: 100 },
        { name: '开始维修', index: '开始维修', width: 100 },
        { name: '维修完成', index: '维修完成', width: 100 },


        ],
        pager: jQuery('#pager6'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',


        onPaging: function (pgButton) {
            if (pgButton == "next_pager6") {
                if (curPage == $("#list6").getGridParam("lastpage"))
                    return;
                curPage = $("#list6").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager6") {
                curPage = $("#list6").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager6") {
                if (curPage == 1)
                    return;
                curPage = $("#list6").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager6") {
                curPage = 1;
            }
            else {
                curPage = $("#pager6 :input").val();
            }
            reload6();
        },
        loadComplete: function () {
            $("#list6").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 166, false);
            $("#list6").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function reload6() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = "1";
    if (rowid != null) {
        rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
    }
    $("#list6").jqGrid('setGridParam', {
        url: 'LoadProcedureList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RID: rID
        },

    }).trigger("reloadGrid");

}

function LoadQuotationList() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = "1";
    if (rowid != null) {
        rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
    }
    jQuery("#list7").jqGrid({
        url: 'GetGenQtnList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RID: rID, Type: "", CardType: $("#CardType").val()
        },
        loadonce: false,
        mtype: 'POST',
        jsonReader: {
            root: function (obj) {
                var data = eval("(" + obj + ")");
                return data.rows;
            },
            page: function (obj) {
                var data = eval("(" + obj + ")");
                return data.page;
            },
            total: function (obj) {
                var data = eval("(" + obj + ")");
                return data.total;
            },
            records: function (obj) {
                var data = eval("(" + obj + ")");
                return data.records;
            },
            repeatitems: false
        },
        colNames: ['登记卡号', '总价'],
        colModel: [
        { name: 'RID', index: 'RID', width: 250 },
        { name: 'TotalPriceU', index: 'TotalPriceU', width: 100 },



        ],
        pager: jQuery('#pager7'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',

        onPaging: function (pgButton) {
            if (pgButton == "next_pager7") {
                if (curPage == $("#list7").getGridParam("lastpage"))
                    return;
                curPage = $("#list7").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager7") {
                curPage = $("#list7").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager7") {
                if (curPage == 1)
                    return;
                curPage = $("#list7").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager7") {
                curPage = 1;
            }
            else {
                curPage = $("#pager7 :input").val();
            }
            reload7();
        },
        loadComplete: function () {

            $("#list7").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 166, false);
            $("#list7").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function reload7() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = "1";
    if (rowid != null) {
        rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
    }
    $("#list7").jqGrid('setGridParam', {
        url: 'GetGenQtnList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RID: rID, Type: "", CardType: $("#CardType").val()
        },

    }).trigger("reloadGrid");

}


