
var curPage = 1;
var OnePageCount = 15;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    // 打印
    $("#btnPrint").click(function () {
        start = $('#start').val();
        end = $('#end').val();
        HouseID = $('#HouseID').val();
        var spec = $("#Spec").val();
        var url = "PrintMaterialSummaryTable?start='" + start + "'&end='" + end + "'&HouseID='" + HouseID + "'&spec='" + spec + "'";
        window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
    });
    $('#Spec').click(function () {
        selid1('getSpecOptionalAdd', 'GJ', 'divGJ', 'ulGJ', 'LoadGJ');//, 'BuildUnit'
    })
})

function reload() {
    ProType = $('#ProType option:selected').text();
    PID = $('#PID').val();
    ProName = $('#ProName').val();
    //Spec = $('#Spec option:selected').text();
    var spec = $("#Spec").val();
    HouseID = $('#HouseID option:selected').text();

    if (ProType == "请选择") {
        ProType = "";
    }
    //if (Spec == "请选择") {
    //    Spec = "";
    //}
    if (HouseID == "请选择") {
        HouseID = "";
    }
    $("#list").jqGrid('setGridParam', {
        url: 'MaterialSummaryTableList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, ProType: ProType, PID: PID, ProName: ProName, Spec: Spec, HouseID: HouseID },

    }).trigger("reloadGrid");
}

function Search() {
    curRow = 0;
    curPage = 1;
    start = $('#start').val();
    end = $('#end').val();
    var spec = $("#Spec").val();
    //HouseID = $('#HouseID option:selected').text();
    HouseID = $('#HouseID').val();
    if (HouseID == "") {
        HouseID = "H00001";
    }
    if (start == "") {
        alert("请选择基准日期");
        return;
    }
    if (end == "") {
        alert("请选择盘点日期");
        return;
    }
    for (var i = document.getElementById("DetailInfo").childNodes.length - 1; i >= 0 ; i--) {
        document.getElementById("DetailInfo").removeChild(document.getElementById("DetailInfo").childNodes[i]);
    }
    $.ajax({
        url: "MaterialSummaryTableList",
        type: "post",
        data: { curpage: curPage, rownum: OnePageCount, end: end, start: start, HouseID: HouseID, spec: spec },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '">'

                    html += '<td ><lable class="labText' + rowCount + ' " id="Text' + rowCount + '">' + json[i].Text + '</lable> </td>';//物料类型
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].PID + '</lable> </td>';//物料编号
                    html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';//物料名称
                    html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Spec + '</lable> </td>';//物品规则
                    html += '<td ><lable class="labHouseNAME' + rowCount + ' " id="HouseNAME' + rowCount + '">' + json[i].HouseNAME + '</lable> </td>';//所属仓库
                    html += '<td ><lable class="labUnits' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Units + '</lable> </td>';//单位
                    html += '<td ><lable class="labUnitPrice' + rowCount + ' " id="UnitPrice' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';//基准日单价

                    html += '<td ><lable class="labFinishCount' + rowCount + ' " id="FinishCount' + rowCount + '">' + json[i].totalCount + '</lable> </td>';//基准日账面数量
                    html += '<td ><lable class="labtotal' + rowCount + ' " id="total' + rowCount + '">' + json[i].totalPrice + '</lable> </td>';//基准日账面金额

                    html += '<td ><lable class="labFinishCount' + rowCount + ' " id="FinishCount' + rowCount + '">' + json[i].totalCount + '</lable> </td>';//基准日记录数量

                    html += '<td ><lable class="labInCount' + rowCount + ' " id="InCount' + rowCount + '">' + json[i].InCount + '</lable> </td>';//基准日至汇总日入帐数入库数量
                    html += '<td ><lable class="labOutCount' + rowCount + ' " id="FinishCount' + rowCount + '">' + json[i].OutCount + '</lable> </td>';//基准日至汇总日入帐数出库数量

                    html += '<td ><lable class="labtotalCount' + rowCount + ' " id="totalCount' + rowCount + '">' + json[i].FinishCount + '</lable> </td>';//汇总日应结存数量
                    html += '<td ><lable class="labtotalPrice' + rowCount + ' " id="totalPrice' + rowCount + '">' + json[i].total + '</lable> </td>';//汇总日应结存金额

                    html += '<td ><lable class="labFinishCount1' + rowCount + ' " id="FinishCount1' + rowCount + '">' + json[i].Cynum + '</lable> </td>';//差异数量
                    html += '<td ><lable class="labtotal1' + rowCount + ' " id="total1' + rowCount + '">' + json[i].CynumPrice + '</lable> </td>';//差异金额
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                }
                //
            }
        }
    })
}



function LoadGJ(liInfo) {
    $('#Spec').val(liInfo);
    $('#divGJ').hide();
}
function BuildUnitkey() {
    $('#divGJ').hide();
}
function selid1(actionid, selid, divid, ulid, jsfun) {
    // var TypeID = Type;// 行政区编码
    var spec = $("#Spec").val();
    $.ajax({
        url: actionid,
        type: "post",
        data: { spec: spec },//data1: TypeID,
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.success == "true") {
                //var unitid = data.Strid.split(',');
                var unit = data.Strname.split(',');
                $("#" + divid).show();
                $("#" + ulid + " li").remove();
                for (var i = 0; i < unit.length; i++) {
                    $("#" + ulid).append("<li style='cursor:pointer;margin-left:1px; width:190px;height:20px;list-style-type:none; color:black;'><span onclick='" + jsfun + "(\"" + unit[i] + "\");' style='margin-left:1px; width:190px; height:20px;display:block;'>" + unit[i] + "</span>");
                }
            }
            else {
                $("#" + divid).hide();
            }
        }
    });
}