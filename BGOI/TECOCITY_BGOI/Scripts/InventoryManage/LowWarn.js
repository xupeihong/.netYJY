

$(document).ready(function () {
    LoadTitle();
    jq();

})
// 加载固定行标题
function LoadTitle() {
    var tr1 = $("#line1");
    tr1.html('');

    // 加载第1行
    var td1 = $('<td rowspan="2" align="center">物料编码</td>');
    td1.appendTo(tr1);
    var td1 = $('<td rowspan="2" align="center">物料描述</td>');
    td1.appendTo(tr1);
    var td1 = $('<td rowspan="2" align="center">库房</td>');
    td1.appendTo(tr1);
    var td1 = $('<td rowspan="2" align="center">剩余数量</td>');
    td1.appendTo(tr1);

}
function jq() {
    $.ajax({
        url: "getWarnLow",
        type: "post",
        data: {},
        dataType: "json",
        async: false, //是否异步
        success: function (data) {
            if (data.success == "false") {
                return;
            }
            else {
                FillData(data);
            }
        }
    })
}
// 填充表格
function FillData(data) {
    // 
    if (data.WarnLow != "") {
        var tab = $("#tabList");
        tab.html('');
        var datas = eval("(" + data.WarnLow + ")");
        $.each(datas.WarnLow, function (i, n) {
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

}