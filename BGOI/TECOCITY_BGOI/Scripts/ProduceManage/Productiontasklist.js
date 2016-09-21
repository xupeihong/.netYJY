var curPage = 1;
var OnePageCount = 6;
var Customer;
var FollowPerson;
var oldSelID = 0;
var RWID;
var curPage1 = 1;
var OnePageCount1 = 6;
var curPage2 = 1;
var OnePageCount2 = 4;
var curPage4 = 1;


$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    jq();
    jq1("");
    jq2("");
    jq3("");
    jq4("");

    document.getElementById('bor1').style.display = 'block';
    document.getElementById('div2').style.display = 'none';
    document.getElementById('bor2').style.display = 'none';
    document.getElementById('bor4').style.display = 'none';

    $("#DetailXX").click(function () {
        this.className = "btnTw";
        $('#BillXX').attr("class", "btnTh");
        $('#SPXX').attr("class", "btnTh");
        $('#CZRZ').attr("class", "btnTh");
        document.getElementById('bor1').style.display = 'block';
        document.getElementById('div2').style.display = 'none';
        document.getElementById('bor2').style.display = 'none';
        document.getElementById('bor4').style.display = 'none';
    });
    $("#BillXX").click(function () {
        this.className = "btnTw";
        $('#DetailXX').attr("class", "btnTh");
        $('#SPXX').attr("class", "btnTh");
        $('#CZRZ').attr("class", "btnTh");
        document.getElementById('bor1').style.display = 'none';
        document.getElementById('div2').style.display = 'block';
        document.getElementById('bor2').style.display = 'none';
        document.getElementById('bor4').style.display = 'none';
    });
    $("#SPXX").click(function () {
        this.className = "btnTw";
        $('#DetailXX').attr("class", "btnTh");
        $('#BillXX').attr("class", "btnTh");
        $('#CZRZ').attr("class", "btnTh");
        document.getElementById('bor1').style.display = 'none';
        document.getElementById('div2').style.display = 'none';
        document.getElementById('bor2').style.display = 'block';
        document.getElementById('bor4').style.display = 'none';
    });
    $("#CZRZ").click(function () {
        this.className = "btnTw";
        $('#BillXX').attr("class", "btnTh");
        $('#SPXX').attr("class", "btnTh");
        $('#DetailXX').attr("class", "btnTh");
        document.getElementById('bor1').style.display = 'none';
        document.getElementById('div2').style.display = 'none';
        document.getElementById('bor2').style.display = 'none';
        document.getElementById('bor4').style.display = 'block';
    });

    $("#TJShenPi").click(function () {

        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var RWID = jQuery("#list").jqGrid('getRowData', rowid).RWID;
        if (rowid == null) {
            alert("请选择要审批的任务单");
            return;
        }
        else {
            $.ajax({
                async: false,
                url: "getPD",
                type: "post",
                single: true,
                data: { RWID: RWID },
                dataType: "json",
                success: function (data) {
                    var json = eval(data.datas);
                    if (json.length > 0) {
                        for (var i = 0; i < json.length; i++) {
                            if (json[i].name == "新建" || json[i].name == "审批未通过") {
                                var PID = jQuery("#list").jqGrid('getRowData', rowid).RWID;
                                var texts = PID + "@" + "生产任务审批";
                                window.parent.OpenDialog("提交审批", "../COM_Approval/SubmitApproval?id=" + texts, 700, 500, '');
                            }
                            else {
                                alert("该任务单正在审批或审批已通过");
                                return;
                            }
                        }
                    }
                }
            })
        }
    })

    $('#ShenPi').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var RWID = jQuery("#list").jqGrid('getRowData', rowid).RWID;
        if (rowid == null) {
            alert("请选择要进行审批的条目");
            return;
        }
        else {
            $.ajax({
                async: false,
                url: "getPDSP",
                type: "post",
                single: true,
                data: { RWID: RWID },
                dataType: "json",
                success: function (data) {
                    var json = eval(data.datas);
                    if (json.length > 0) {
                        var SPID = jQuery("#list").jqGrid('getRowData', rowid).PID;
                        var PID = jQuery("#list").jqGrid('getRowData', rowid).RWID;
                        var state = jQuery("#list").jqGrid('getRowData', rowid).State;
                        if (state == $("#Nostate").val()) {
                            alert("审批不通过，不能进行审批了");
                            return;
                        }
                        $.ajax({
                            url: "../COM_Approval/JudgeAppDisable",
                            type: "post",
                            data: { data1: $("#webkey").val(), data2: SPID },
                            dataType: "Json",
                            success: function (data) {
                                if (data.success == "true") {
                                    var bol = data.intblo;
                                    if (bol == "-1") {
                                        alert("您没有审批权限，不能进行审批操作");
                                        return;
                                    }
                                    if (bol == "1") {
                                        alert("您已经审批完成，不能进行审批操作");
                                        return;
                                    }
                                    if (bol == "2") {
                                        alert("审批过程还没有进行到您这一步，不能进行审批操作");
                                        return;
                                    }
                                    var texts = $("#webkey").val() + "@" + SPID + "@" + PID;
                                    window.parent.OpenDialog("审批", "../COM_Approval/Approval?id=" + texts, 500, 400, '');
                                }
                                else {
                                    return;
                                }
                            }
                        });
                    }
                    else {
                        alert("该任务还未提交审批不能进行审批");
                        return;
                    }
                }
            })
        }
    })

    $("#CheXiao").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要撤销的任务单");
            return;
        }
        else {
            var msg = "您真的确定要删除吗?";
            if (confirm(msg) == true) {
                var RWID = Model.RWID;
                $.ajax({
                    type: "POST",
                    url: "CheXiaoTask",
                    data: { RWID: RWID },
                    success: function (data) {
                        alert(data.Msg);
                        reload();
                    },
                    dataType: 'json'
                });
                return true;
            } else {
                return false;
            }
        }
    });
    //打印
    $("#DaYin").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).RWID;
        if (rowid == null) {
            alert("请选择要打印的任务单");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).RWID;
            var PID = jQuery("#list").jqGrid('getRowData', rowid).PID;
            var url = "PrintTask?Info=" + escape(texts) + "&PID=" + PID + "";
            window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:500px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });
})

function AddTask() {
    window.parent.parent.OpenDialog("新增生产任务", "../ProduceManage/AddTask", 900, 550);
}
//跳转到领料单页面
function AddLL() {
    var a = 0;
    var b = 0;
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择要领料的任务单");
        return;
    } else {
        var RWID = Model.RWID;

        $.ajax({
            async: false,
            url: "selectPDTJ",
            type: "post",
            single: true,
            data: { RWID: RWID },
            dataType: "json",
            success: function (data) {
                var json = eval(data.datas);
                if (json.length > 0) {
                    for (var i = 0; i < json.length; i++) {
                        if (json[i].IsPass == "0") {
                            alert("此任务单尚未提交审批，请提交");
                            break;
                        }
                        else {
                            selectPDTG(RWID);
                            return RWID;
                        }
                    }
                }
            }

        })

    }
}

function selectPDTG(RWID) {
    $.ajax({
        async: false,
        url: "getSP",
        type: "post",
        single: true,
        data: { RWID: RWID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    if (json[i].IsPass == "0") {
                        getLLSL(RWID);
                        return RWID;
                    }
                    else {
                        alert("审批未通过");
                        a = 1;
                        break;
                    }
                }
            }
        }

    })
}

function intoLL(RWID) {
    window.parent.parent.OpenDialog("新增领料单", "../ProduceManage/AddLL?RWID=" + RWID, 800, 520);
}

function gettll(RWID) {
    $.ajax({
        async: false,
        url: "gettll",
        type: "post",
        single: true,
        data: { RWID: RWID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    //if (json[i].state == "4") {
                    //    alert("该产品正在生产！");
                    //    break;
                    //} else
                    //    if (json[i].state == "5") {
                    //        alert("该产品生产完成！");
                    //        break;
                    //    } else
                            if (json[i].state == "6") {
                                alert("该产品已完成入库！");
                                break;
                            }
                            else {
                                intoLL(RWID);
                                return RWID;
                            }
                }
            }

        }
    })
}

function getLLSL(RWID) {
    $.ajax({
        async: false,
        url: "getLLSL",
        type: "post",
        single: true,
        data: { RWID: RWID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    if (json[i].LLstate != "1") {
                        gettll(RWID);
                        return RWID;
                    }
                    else {
                        alert("该任务单已领料完成，请选择其他任务单");
                        b = 1;
                        break;
                    }
                }
            }
            else {
                intoLL(RWID);
            }
        }
    })
}

//跳转到随工单页面
function AddSG() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择要随工的任务单");
        return;
    } else {

        var RWID = Model.RWID;
        //判断是否审批通过
        $.ajax({
            async: false,
            url: "selectPDTJ",
            type: "post",
            single: true,
            data: { RWID: RWID },
            dataType: "json",
            success: function (data) {
                var json = eval(data.datas);
                if (json.length > 0) {
                    for (var i = 0; i < json.length; i++) {
                        if (json[i].IsPass == "0") {
                            alert("此任务单尚未提交审批，请提交");
                            break;
                        }
                        else {
                            selectSGTJ(RWID);
                            return RWID;
                        }
                    }
                }
            }

        })


    }
}

function selectSGTJ(RWID) {
    $.ajax({
        async: false,
        url: "getSP",
        type: "post",
        single: true,
        data: { RWID: RWID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    if (json[i].IsPass == "0") {
                        getSGSL(RWID);
                        return RWID;
                    }
                    else {
                        alert("审批未通过");
                        a = 1;
                        break;
                    }
                }
            }

        }

    })
}


function intoSG(RWID) {
    window.parent.parent.OpenDialog("新增随工单", "../ProduceManage/AddSG?RWID=" + RWID, 800, 420);
}

//判断随工单是否生成随工单完成数据
function getSGSL(RWID) {
    $.ajax({
        async: false,
        url: "getSGSL",
        type: "post",
        single: true,
        data: { RWID: RWID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    if (json[i].State != "4") {
                        gettsg(RWID);
                        return RWID;
                    }
                    else {
                        alert("此产品正在生产，请选择其他产品");
                        b = 1;
                        break;
                    }
                }
            }
            else {
                gettsg(RWID);
            }
        }
    })
}


function gettsg(RWID) {
    $.ajax({
        async: false,
        url: "gettsg",
        type: "post",
        single: true,
        data: { RWID: RWID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    if (json[i].state == "6") {
                        alert("此产品已完成入库！");
                        return;
                    }
                    else {
                        intoSG(RWID);
                        return RWID;
                    }
                }
            }

        }
    })
}


//跳转到检验报告页面
function AddBG() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择要上传检测报告的任务单");
        return;
    } else {
        var RWID = Model.RWID;
        //判断是否审批通过
        $.ajax({
            async: false,
            url: "selectPDTJ",
            type: "post",
            single: true,
            data: { RWID: RWID },
            dataType: "json",
            success: function (data) {
                var json = eval(data.datas);
                if (json.length > 0) {
                    for (var i = 0; i < json.length; i++) {
                        if (json[i].IsPass == "0") {
                            alert("此任务单尚未提交审批，请提交");
                            break;
                        }
                        else {
                            selectBGTG(RWID);
                            return RWID;
                        }
                    }
                }
            }

        })
    }
}

function selectBGTG(RWID) {
    $.ajax({
        async: false,
        url: "getSP",
        type: "post",
        single: true,
        data: { RWID: RWID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    if (json[i].IsPass == "0") {
                        intoBG(RWID);
                        return RWID;
                    }
                    else {
                        alert("审批未通过");
                        a = 1;
                        break;
                    }
                }
            }
        }

    })
}



function intoBG(RWID) {
    window.parent.parent.OpenDialog("检验报告上传", "../ProduceManage/AddBG?RWID=" + RWID, 800, 500);
}
//跳转到修改任务单页面
function UpdateTask() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择要修改的任务单");
        return;
    } else {
        var RWID = Model.RWID;
        $.ajax({
            async: false,
            url: "getupdate",
            type: "post",
            single: true,
            data: { RWID: RWID },
            dataType: "json",
            success: function (data) {
                var json = eval(data.datas);
                if (json.length > 0) {
                    for (var i = 0; i < json.length; i++) {
                        if (json[i].State == "-1" || json[i].State == "0") {
                            intoupdate(RWID);
                            return RWID;
                        }
                        else {
                            alert("此产品已经审批通过或正在审批,不可修改！");
                            b = 1;
                            break;
                        }
                    }
                }

            }
        })

    }
}

function intoupdate(RWID) {
    window.parent.parent.OpenDialog("修改任务单信息", "../ProduceManage/UpdateTask?RWID=" + RWID, 800, 500);
}

//跳转到任务单详情页面
function TaskDetail() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择要查看详情的任务单");
        return;
    } else {
        window.parent.parent.OpenDialog("任务单详细信息", "../ProduceManage/TaskDetail?RWID=" + Model.RWID, 800, 500);
    }
}

//跳转到完成入库页面
function AddRK() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择要入库的任务单号");
        return;
    } else {

        var RWID = Model.RWID;
        //判断是否审批通过
        $.ajax({
            async: false,
            url: "selectPDTJ",
            type: "post",
            single: true,
            data: { RWID: RWID },
            dataType: "json",
            success: function (data) {
                var json = eval(data.datas);
                if (json.length > 0) {
                    for (var i = 0; i < json.length; i++) {
                        if (json[i].IsPass == "0") {
                            alert("此任务单尚未提交审批，请提交");
                            break;
                        }
                        else {
                            selectRKTG(RWID);
                            return RWID;
                        }
                    }
                }
            }

        })
    }
}

function selectRKTG(RWID) {
    $.ajax({
        async: false,
        url: "getSP",
        type: "post",
        single: true,
        data: { RWID: RWID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    if (json[i].IsPass == "0") {
                        selectSG(RWID);
                        return RWID;
                    }
                    else {
                        alert("审批未通过");
                        a = 1;
                        break;
                    }
                }
            }
        }

    })
}


function selectSG(RWID) {
    $.ajax({
        async: false,
        url: "selectSG",
        type: "post",
        single: true,
        data: { RWID: RWID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    if (json[i].a >0) {
                        selectRK(RWID);
                        return RWID;
                    }
                    else {
                        alert("此产品尚未生产,请先生产");
                        break;

                    }
                }
            }

        }

    })
}

function selectRK(RWID) {
    $.ajax({
        async: false,
        url: "selectRK",
        type: "post",
        single: true,
        data: { RWID: RWID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    if (json[i].State == "6") {
                        alert("此产品已完成入库，请选择其他产品");
                        break;
                    }
                    else {
                        intoRK(RWID);
                        return RWID;
                    }
                }
            }
            else {
                intoRK(RWID);
            }
        }
    })
}
function intoRK(RWID) {
    window.parent.parent.OpenDialog("完成入库", "../ProduceManage/AddRK?RWID=" + RWID, 800, 500);
}
function jq() {
    var UnitID = $('#UnitID').val();
    var OrderContent = $('#OrderContent').val();
    var SpecsModels = $('#SpecsModels').val();
    var Starts = $('#Starts').val();
    var Starte = $('#Starte').val();
    var Ends = $('#Ends').val();
    var Ende = $('#Ende').val();
    var Statea = $("input[name='Statea']:checked").val();
    var Stateb = $("input[name='Stateb']:checked").val();

    jQuery("#list").jqGrid({
        url: 'Productionlist',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, UnitID: UnitID, OrderContent: OrderContent, SpecsModels: SpecsModels, Starts: Starts, Starte: Starte, Ends: Ends, Ende: Ende, Statea: Statea, Stateb: Stateb },
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
        colNames: ['序号', '任务单号', '订货单位', '联系人', '联系方式', '开单日期', '生产说明', '状态', '领料单状态', '', ''],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'RWID', index: 'RWID', width: 140, align: "center" },
        { name: 'OrderUnit', index: 'OrderUnit', width: 120, align: "center" },
        { name: 'OrderContactor', index: 'OrderContactor', width: 150, align: "center" },
        { name: 'OrderTel', index: 'OrderTel', width: 100, align: "center" },
        { name: 'CreateTime', index: 'CreateTime', editable: false, formatter: "date", formatoptions: { srcformat: 'Y-m-d H:i:s', newformat: 'Y-m-d ' }, width: 80, sortable: true, align: "center" },
        { name: 'Remark', index: 'Remark', width: 80, align: "center" },
        { name: 'name', index: 'name', width: 80, align: "center" },
        { name: 'LLState', index: 'LLState', width: 80, align: "center" },
        { name: 'PID', index: 'PID', width: 80, align: "center", hidden: true },
        { name: 'State', index: 'State', width: 80, align: "center", hidden: true }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '',

        gridComplete: function () {

        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;

            var dataSel = jQuery("#list").jqGrid('getGridParam');
            var ids = dataSel.selrow;
            var Model = jQuery("#list").jqGrid('getRowData', ids);
            if (ids == null) {
                return;
            }
            else {
                var RWID = Model.RWID;
                var pid = Model.PID
                reload1(RWID);
                LoadReceiveBill(RWID);
                reload3(pid);
                reload4(RWID);
            }
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 400, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function reload() {
    var UnitID = $('#UnitID').val();
    var OrderContent = $('#OrderContent').val();
    var SpecsModels = $('#SpecsModels').val();
    var Starts = $('#Starts').val();
    var Starte = $('#Starte').val();
    var Ends = $('#Ends').val();
    var Ende = $('#Ende').val();
    var Statea = $("input[name='Statea']:checked").val();
    var Stateb = $("input[name='Stateb']:checked").val();

    $("#list").jqGrid('setGridParam', {
        url: 'Productionlist',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, UnitID: UnitID, OrderContent: OrderContent, SpecsModels: SpecsModels, Starts: Starts, Starte: Starte, Ends: Ends, Ende: Ende, Statea: Statea, Stateb: Stateb },


    }).trigger("reloadGrid");//重新载入
}

//查询
function Search() {
    //判断开始日期
    var strDateStart = $('#Starts').val();
    var strDateEnd = $('#Starte').val();
    if (strDateStart == "" && strDateEnd == "") {

        getSearch();
    }
    else {
        var strSeparator = "-"; //日期分隔符
        var strDateArrayStart;
        var strDateArrayEnd;
        var intDay;
        strDateArrayStart = strDateStart.split(strSeparator);
        strDateArrayEnd = strDateEnd.split(strSeparator);
        var strDateS = new Date(strDateArrayStart[0] + "/" + strDateArrayStart[1] + "/" + strDateArrayStart[2]);
        var strDateE = new Date(strDateArrayEnd[0] + "/" + strDateArrayEnd[1] + "/" + strDateArrayEnd[2]);
        if (strDateS <= strDateE) {
            getSearch();
        }
        else {
            alert("开单截止日期不能小于开始日期！");
            $("#Starte").val("");
            return false;
        }
        //判断结束日期
        var strDateStarts = $('#Ends').val();
        var strDateEnds = $('#Ende').val();
        if (strDateStarts == "" && strDateEnds == "") {

            getSearch();
        }
        else {
            var strSeparatosrs = "-"; //日期分隔符
            var strDateArrayStarts;
            var strDateArrayEnds;
            var intDays;
            strDateArrayStarts = strDateStarts.split(strSeparators);
            strDateArrayEnds = strDateEnds.split(strSeparators);
            var strDateA = new Date(strDateArrayStarts[3] + "/" + strDateArrayStarts[4] + "/" + strDateArrayStarts[5]);
            var strDateB = new Date(strDateArrayEnds[3] + "/" + strDateArrayEnds[4] + "/" + strDateArrayEnds[5]);
            if (strDateA <= strDateB) {
                getSearch();
            }
            else {
                alert("完成截止日期不能小于开始日期！");
                $("#Ende").val("");
                return false;
            }
        }
    }
}

function getSearch() {
    curRow = 0;
    curPage = 1;

    var UnitID = $('#UnitID').val();
    var OrderContent = $('#OrderContent').val();
    var SpecsModels = $('#SpecsModels').val();
    var Starts = $('#Starts').val();
    var Starte = $('#Starte').val();
    var Ends = $('#Ends').val();
    var Ende = $('#Ende').val();
    var Statea = $("input[name='Statea']:checked").val();
    var Stateb = $("input[name='Stateb']:checked").val();
    if (Statea == "1") {
        $('#Fin').hide();
    } else { $('#Fin').show(); }

    $("#list").jqGrid('setGridParam', {
        url: 'Productionlist',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, UnitID: UnitID, OrderContent: OrderContent, SpecsModels: SpecsModels, Starts: Starts, Starte: Starte, Ends: Ends, Ende: Ende, Statea: Statea, Stateb: Stateb },
        loadonce: false

    }).trigger("reloadGrid");//重新载入
}


function jq1(RWID) {

    jQuery("#list1").jqGrid({
        url: 'ProduceInDetialList',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, RWID: RWID },
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
        colNames: ['序号', '物品名称', '规格型号', '单位', '数量', '完成日期', '状态'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'OrderContent', index: 'OrderContent', width: 150, align: "center" },
        { name: 'SpecsModels', index: 'SpecsModels', width: 150, align: "center" },
        { name: 'OrderUnit', index: 'OrderUnit', width: 150, align: "center" },
        { name: 'OrderNum', index: 'OrderNum', width: 150, align: "center" },
        //{ name: 'Technology', index: 'Technology', width: 80, align: "center"},
        { name: 'DeliveryTime', index: 'DeliveryTime', editable: false, formatter: "date", formatoptions: { srcformat: 'Y-m-d H:i:s', newformat: 'Y-m-d ' }, width: 150, align: "center" },
        //{ name: 'Remark', index: 'Remark', width: 120, align: "center" },
        { name: 'State', index: 'State', width: 150, align: "center" }
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount1,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '产品详细',

        gridComplete: function () {
            var ids = jQuery("#list1").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list1").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list1").jqGrid('getRowData', id).PID + "' name='cb'/>";
                jQuery("#list1").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }

        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pager1") {
                if (curPage1 == $("#list1").getGridParam("lastpage1"))
                    return;
                curPage1 = $("#list1").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager1") {
                curPage1 = $("#list1").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager1") {
                if (curPage1 == 1)
                    return;
                curPage1 = $("#list1").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager1") {
                curPage1 = 1;
            }
            else {
                curPage1 = $("#pager1 :input").val();
            }
            reload1();
        },
        loadComplete: function () {
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() - 460, false);
            $("#list1").jqGrid("setGridWidth", $("#bor1").width() - 30, false);
        }
    });
}

function reload1(RWID) {
    //var curPage1 = 1;
    var OnePageCount1 = 4;
    $("#list1").jqGrid('setGridParam', {
        url: 'ProduceInDetialList',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, RWID: RWID },

    }).trigger("reloadGrid");
}

function jq2(RWID) {
    rowCount = document.getElementById("ReceiveBill").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "LoadTask",
        type: "post",
        data: { RWID: RWID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {

                $("#ReceiveBill").val("");
                for (var i = 0; i < json.length; i++) {
                    rowCount = i;
                    var html = "";
                    html += '<tr>'
                    var s = json[i].ID.substr(0, 2);
                    if (s == "RW") {
                        html += '<td ><lable class="labMSID' + rowCount + ' " id="MSID' + rowCount + '" style="width:33%;align:center ">任务单信息</lable> </td>';
                    }
                    if (s == "LL") {
                        html += '<td ><lable class="labMSID' + rowCount + ' " id="MSID' + rowCount + '"style="width:33%;align:center ">领料单信息</lable> </td>';
                    }
                    if (s == "SG") {
                        html += '<td ><lable class="labMSID' + rowCount + ' " id="MSID' + rowCount + '"style="width:33%;align:center ">随工单信息</lable> </td>';
                    }
                    if (s == "BG") {
                        html += '<td ><lable class="labMSID' + rowCount + ' " id="MSID' + rowCount + '"style="width:33%;align:center ">检测报告信息</lable> </td>';
                    }
                    if (s == "RK") {
                        html += '<td ><lable class="labMSID' + rowCount + ' " id="MSID' + rowCount + '"style="width:33%;align:center ">入库信息</lable> </td>';
                    }
                    html += '<td ><lable class="labID' + rowCount + ' " id="ID' + rowCount + '" style="width:33%;align:center ">' + json[i].ID + '</lable> </td>';
                    html += '<td ><a href="#" style="color:blue;width:33%;text-align:center " onclick=GetXX("' + json[i].ID + '")>详情</a></td>';
                    html += '</tr>'
                    $("#ReceiveBill").append(html);
                }


            }
        }
    })
}
function LoadReceiveBill(RWID) {
    for (var i = document.getElementById("ReceiveBill").childNodes.length - 1; i >= 0 ; i--) {
        document.getElementById("ReceiveBill").removeChild(document.getElementById("ReceiveBill").childNodes[i]);
    }
    rowCount = document.getElementById("ReceiveBill").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "LoadTask",
        type: "post",
        data: { RWID: RWID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {

                $("#ReceiveBill").val("");
                for (var i = 0; i < json.length; i++) {
                    rowCount = i;
                    var html = "";
                    html += '<tr>'
                    var s = json[i].ID.substr(0, 2);
                    if (s == "RW") {
                        html += '<td ><lable class="labMSID' + rowCount + ' " id="MSID' + rowCount + '" style="width:33%">任务单信息</lable> </td>';
                    }
                    if (s == "LL") {
                        html += '<td ><lable class="labMSID' + rowCount + ' " id="MSID' + rowCount + '"style="width:33%">领料单信息</lable> </td>';
                    }
                    if (s == "SG") {
                        html += '<td ><lable class="labMSID' + rowCount + ' " id="MSID' + rowCount + '"style="width:33%">随工单信息</lable> </td>';
                    }
                    if (s == "BG") {
                        html += '<td ><lable class="labMSID' + rowCount + ' " id="MSID' + rowCount + '"style="width:33%">检测报告信息</lable> </td>';
                    }
                    if (s == "RK") {
                        html += '<td ><lable class="labMSID' + rowCount + ' " id="MSID' + rowCount + '"style="width:33%">入库信息</lable> </td>';
                    }
                    html += '<td ><lable class="labID' + rowCount + ' " id="ID' + rowCount + '" style="width:33%">' + json[i].ID + '</lable> </td>';
                    html += '<td ><a href="#" style="color:blue;width:33%" onclick=GetXX("' + json[i].ID + '")>详情</a></td>';
                    html += '</tr>'
                    $("#ReceiveBill").append(html);
                }


            }
        }
    })
}

function GetXX(SDI) {
    var id = SDI;
    window.parent.parent.OpenDialog("详细", "../ProduceManage/LoadTasks?ID=" + id, 800, 450);
}

//审批信息

function reload3(jhid) {
    //PID = $('#PID').val();
    webkey = $('#webkey').val();
    folderBack = $('#folderBack').val();
    $("#list2").jqGrid('setGridParam', {
        url: '../COM_Approval/ConditionGrid',
        datatype: 'json',
        postData: { curpage: curPage2, rownum: OnePageCount2, PID: jhid, webkey: webkey, folderBack: folderBack },

    }).trigger("reloadGrid");
}

function jq3(jhid) {
    //PID = $('#PID').val();
    webkey = $('#webkey').val();
    folderBack = $('#folderBack').val();
    jQuery("#list2").jqGrid({
        url: '../COM_Approval/ConditionGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, PID: jhid, webkey: webkey, folderBack: folderBack },
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
        colNames: ['', '职务', '姓名', '审批方式', '人数', '审批情况', '审批意见', '审批时间', '备注'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'Job', index: 'Job', width: 100, align: "center" },
        { name: 'UserName', index: 'UserName', width: 100, align: "center" },
        { name: 'AppTypeDesc', index: 'AppTypeDesc', width: 100, align: "center" },
        { name: 'Num', index: 'Num', width: 100, align: "center" },
        { name: 'stateDesc', index: 'stateDesc', width: 100, align: "center" },
        { name: 'Opinion', index: 'Opinion', width: $("#bor").width() - 920, align: "center" },
        { name: 'ApprovalTime', index: 'ApprovalTime', width: 150, align: "center", editable: false, formatter: "date", formatoptions: { srcformat: 'Y-m-d H:i:s', newformat: 'Y-m-d ' } },
        { name: 'Remark', index: 'Remark', width: 200, align: "center" },
        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '审批情况表',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                //var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).PID + "' name='cb'/>";
                //jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }

        },
        //onSelectRow: function (rowid, status) {
        //    if (oldSelID != 0) {
        //        $('input[id=c' + oldSelID + ']').prop("checked", false);
        //    }
        //    $('input[id=c' + rowid + ']').prop("checked", true);
        //    oldSelID = rowid;
        //},

        onPaging: function (pgButton) {
            if (pgButton == "next_pager2") {
                if (curPage2 == $("#list2").getGridParam("lastpage2"))
                    return;
                curPage2 = $("#list2").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager2") {
                curPage2 = $("#list2").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager2") {
                if (curPage2 == 1)
                    return;
                curPage2 = $("#list2").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager2") {
                curPage2 = 1;
            }
            else {
                curPage2 = $("#pager2 :input").val();
            }
            reload3();
        },
        loadComplete: function () {
            $("#list2").jqGrid("setGridHeight", $("#pageContent").height() - 460, false);
            $("#list2").jqGrid("setGridWidth", $("#bor2").width() - 30, false);
        }
    });
}

function reload4(RWID) {
    $("#list4").jqGrid('setGridParam', {
        url: 'getRZ',
        datatype: 'json',
        postData: { curpage: curPage4, rownum: OnePageCount2, RWID: RWID },

    }).trigger("reloadGrid");
}

function jq4(RWID) {
    jQuery("#list4").jqGrid({
        url: 'getRZ',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount2, RWID: RWID },
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
        colNames: ['', '编号', '操作内容', '操作结果', '操作时间', '操作人'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'YYCode', index: 'YYCode', width: 200, align: "center" },
        { name: 'Content', index: 'Content', width: 200, align: "center" },
        { name: 'YYType', index: 'YYType', width: 200, align: "center" },
        { name: 'LogTime', index: 'LogTime', width: 200, align: "center", editable: false, formatter: "date", formatoptions: { srcformat: 'Y-m-d H:i:s', newformat: 'Y-m-d ' } },
        { name: 'Actor', index: 'Actor', width: 200, align: "center" }
        ],
        pager: jQuery('#pager4'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '审批情况表',

        gridComplete: function () {
            var ids = jQuery("#list4").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list4").jqGrid('getRowData', id);
            }
        },


        onPaging: function (pgButton) {
            if (pgButton == "next_pager4") {
                if (curPage4 == $("#list4").getGridParam("lastpage4"))
                    return;
                curPage4 = $("#list4").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager4") {
                curPage4 = $("#list4").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager4") {
                if (curPage4 == 1)
                    return;
                curPage4 = $("#list4").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager4") {
                curPage4 = 1;
            }
            else {
                curPage4 = $("#pager4 :input").val();
            }
            reload4();
        },
        loadComplete: function () {
            $("#list4").jqGrid("setGridHeight", $("#pageContent").height() - 460, false);
            $("#list4").jqGrid("setGridWidth", $("#bor4").width() - 30, false);
        }
    });
}

function getspec() {
    var OrderContent = $("#OrderContent").find("option:selected").text();
    var SpecsModels = document.getElementById('SpecsModels');
    SpecsModels.options.length = 0;
    var op = new Option("请选择", "");
    SpecsModels.options.add(op);
    $.ajax({
        url: "getspec",
        type: "post",
        data: { OrderContent: OrderContent },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    //  $("#Specnew").val(json[i].Spec);
                    var op = new Option(json[i].SpecsModels, "");
                    SpecsModels.options.add(op);
                }
            }
        }
    })
}