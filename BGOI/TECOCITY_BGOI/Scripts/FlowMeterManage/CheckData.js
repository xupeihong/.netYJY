
var curPage = 1;
var OnePageCount = 15;
var oldSelID = 0;

$(document).ready(function () {
    var Request = GetRequest();
    var type = Request["RepairType"];

    $("#typetitle").html(type);
    $("#RepairType").val(type);
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 20);

    $('#WLdiv').click(function () {
        this.className = "btnTw";
        $('#CSdiv').attr("class", "btnTh");


        $("#WL").css("display", "");
        $("#CS").css("display", "none");
        reload();
    })

    $('#CSdiv').click(function () {
        this.className = "btnTw";
        $('#WLdiv').attr("class", "btnTh");


        $("#WL").css("display", "none");
        $("#CS").css("display", "");
        reload2();
    })

    // 查看详情 
    $("#CKXQ").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).RepairID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        } else {
            var type = $("#CardType").val();
            if (type == "CardType2")
                window.parent.OpenDialog("检测详情", "../FlowMeterManage/CkeckData2Detail?RepairID=" + rID, 1000, 450, '');
            else
                window.parent.OpenDialog("检测详情", "../FlowMeterManage/CkeckDataDetail?RepairID=" + rID, 1000, 450, '');
        }
    });
    //修改 完成
    $("#XGJC").click(function () {
        var CardType = $("#CardType").val();
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
        var RepairType = jQuery("#list").jqGrid('getRowData', rowid).RepairType;
        var pid = "4";
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            switch (RepairType) {
                case "进厂检测":
                    pid = "4";
                    break;
                case "维修检测":
                    pid = "7";
                    break;
                case "清洗检测":
                    pid = "6";
                    break;

            }
            $.ajax({
                url: "Operate",
                type: "post",
                data: { P: pid, RID: rID, str: $(this).val(), type: CardType },
                dataType: "Json",
                success: function (data) {
                    if (data.success == "true") {

                        var texts = jQuery("#list").jqGrid('getRowData', rowid).RepairID ;

                        if (CardType == "CardType2")

                            window.parent.OpenDialog("修改检测表", "../FlowMeterManage/UpdateCheckData2?Info=" + texts, 1000, 500, '');
                        else
                            window.parent.OpenDialog("修改检测表", "../FlowMeterManage/UpdateIncomingInspection?Info=" + texts, 1000, 500, '');
                    }
                    else {
                        alert(data.Msg);
                        return;
                    }

                }
            });

        }
    });

    //撤销
    $('#SCJC').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要删除的项");
            return;
        }
        else {
            var CardType = $("#CardType").val();
            var texts = jQuery("#list").jqGrid('getRowData', rowid).RepairID;
            var rid = jQuery("#list").jqGrid('getRowData', rowid).RID;
            var RepairType = jQuery("#list").jqGrid('getRowData', rowid).RepairType;
            var pid = "4";
            switch (RepairType) {
                case "进厂检测":
                    pid = "4";
                    break;
                case "维修检测":
                    pid = "7";
                    break;
                case "清洗检测":
                    pid = "6";
                    break;
            }
            $.ajax({
                url: "Operate",
                type: "post",
                data: { P: pid, RID: rid, str: $(this).val(), type: CardType },
                dataType: "Json",
                success: function (data) {
                    if (data.success == "true") {

                        var one = confirm("是否删除编号为 " + rid + " 的项吗？");
                        if (one == false)
                            return;
                        else {
                            var url = "DeleteIncomingInspection";
                            if (CardType == "CardType2") {
                                 url = "DeleteCheckData2";
                            }
                            $.ajax({
                                url: url,
                                type: "post",
                                data: { id: texts },
                                dataType: "Json",
                                success: function (data) {
                                    if (data.success == "true") {
                                        alert(data.Msg);
                                        reload();
                                    }
                                    else {
                                        alert(data.Msg);
                                        return;
                                    }
                                }
                            });

                        }
                    }
                    else {
                        alert(data.Msg);
                        return;
                    }

                }
            });


        }

    })

    LoadCheckDataList();


});
function GetRequest() {

    var url = location.search; //获取url中"?"符后的字串

    var theRequest = new Object();

    if (url.indexOf("?") != -1) {

        var str = url.substr(1);

        strs = str.split("&");

        for (var i = 0; i < strs.length; i++) {

            theRequest[strs[i].split("=")[0]] = unescape(strs[i].split("=")[1]);

        }

    }

    return theRequest;

}
function LoadCheckDataList() {

    jQuery("#list").jqGrid({
        url: 'LoadCheckDataList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RID: $("#StrRID").val(), RepairType: $("#RepairType").val(), type: $("#CardType").val(),
            RepairMethod: $("#StrRepairMethod").val(), CheckDate: $("#StrCheckDate").val(), CheckUser: $("#StrCheckUser").val()
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
        colNames: ['登记卡号', '检测人', '检测时间', '检测类型', '检测方式', ''],
        colModel: [
        { name: 'RID', index: 'RID', width: 250 },
        { name: 'CheckUser', index: 'CheckUser', width: 100 },
        { name: 'CheckDate', index: 'CheckDate', width: 100 },
        { name: 'RepairType', index: 'RepairType', width: 100 },
        { name: 'RepairMethod', index: 'RepairMethod', width: 100 },
         { name: 'RepairID', index: 'RepairID', hidden: true }


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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 250, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 20, false);
        }
    });
}

function reload() {
    if ($('.field-validation-error').length == 0) {
        $("#list").jqGrid('setGridParam', {
            url: 'LoadCheckDataList',
            datatype: 'json',
            postData: {
                curpage: curPage, rownum: OnePageCount,
                RID: $("#StrRID").val(), RepairType: $("#RepairType").val(), type: $("#CardType").val(),
                RepairMethod: $("#StrRepairMethod").val(), CheckDate: $("#StrCheckDate").val(), CheckUser: $("#StrCheckUser").val()
            },

        }).trigger("reloadGrid");
    }
}

