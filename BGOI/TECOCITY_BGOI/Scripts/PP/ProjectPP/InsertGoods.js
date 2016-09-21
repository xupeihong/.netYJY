$(document).ready(function () {

    var id = location.search.split('&')[0].split('=')[1];

    addBasicDetail(id);
    $("#Submit").click(function () {
        ADDLJ();
    });

});
function addBasicDetail(id) { //增加货品信息行
    $.ajax({
        url: "SelectJJDXX",
        type: "post",
        data: { TransferNum: id },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("GXInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr>'
                    html += '<td ><lable class="labRowNumber " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td style="display:none"><lable class="labProductID " id="ID' + rowCount + '">' + json[i].ID + '</lable> </td>';
                    html += '<td ><lable class="labProductID " id="GoodsNum' + rowCount + '">' + json[i].GoodsNum + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="GoodsName' + rowCount + '">' + json[i].GoodsName + '</lable> </td>';
                    html += '<td ><lable class="labProductID " id="GoodsSpe' + rowCount + '">' + json[i].GoodsSpe + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent " id="COMNameC' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="Amount' + rowCount + '">' + json[i].Amount + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="Unit' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                    html += '<td ><input type="text" id="YesAmount' + rowCount + '"  style="width:80px;"/></td>';
                    html += '<td ><input type="text" id="NoAmount' + rowCount + '"   style="width:80px;"/></td>';
                    html += '<td style="display:none"><lable class="labProductID " id="Remark' + rowCount + '">' + json[i].Remark + '</lable> </td>';
                    html += '<td style="display:none"><lable class="labProductID " id="Bak' + rowCount + '">' + json[i].Bak + '</lable> </td>';
                    html += '</tr>'
                    $("#GXInfo").append(html);
                }
            }
        }
    })
}
function ADDLJ()
{
    var Jyr= $("#JYR").val();
    var Summary = $("#Summary").val();
    var testPeople = $("#testPeople").val();
    var TransferNum = location.search.split('&')[0].split('=')[1];

    var ID = "";
    var YesAmount = "";
    var NoAmount = "";
    var Remark = "";
    var Bak = "";
    var tbody = document.getElementById("GXInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
        var Id = document.getElementById("ID" + i).innerHTML;
        var Amount = document.getElementById("Amount" + i).innerHTML;
        var Yesamount = document.getElementById("YesAmount" + i).value;
        if (Yesamount == "")
        {
            alert("请输入合格数量");
            return;;
        }

        var Noamount = document.getElementById("NoAmount" + i).value;
        if (Noamount == "") {
            alert("请输入不合格数量");
            return;;
        }

        if (parseInt(Yesamount) + parseInt(Noamount) > Amount || parseInt(Yesamount) + parseInt(Noamount) < Amount)
        {
            alert("检测数量输入有误！！！");
            return;
        }
        var Remarks = document.getElementById("Remark" + i).innerHTML;
        var BAK = document.getElementById("Bak" + i).innerHTML;
        ID += Id;
        YesAmount += Yesamount;
        NoAmount += Noamount;
        Remark += Remarks;
        Bak += BAK;
        if (i < tbody.rows.length - 1) {
            ID += ',';;
            YesAmount += ',';
            NoAmount += ',';
            Remark += ',';
            Bak += ',';
        }
        else {
            ID += '';;
            YesAmount += '';
            NoAmount += '';
            Remark += '';
            Bak += '';
        }
    }
    isConfirm = confirm("确定要检验吗")
    if (isConfirm == false) {
        return false;
    }
    $.ajax({
        url: "InsertJJDXX",
        type: "Post",
        data: {
            id: ID, yesamount: YesAmount, noamount: NoAmount, remark: Remark,bak:Bak,

            transfernum: TransferNum, bak: Bak, summary: Summary, testpeople: testPeople,jyr:Jyr
        },
        async: false,
        success: function (data) {
            if (data.success == true) {
                window.parent.frames["iframeRight"].reload();
                alert("成功");
                setTimeout('parent.ClosePop()', 100);
            }
            else {
                alert("失败");
            }
        }
    });

}