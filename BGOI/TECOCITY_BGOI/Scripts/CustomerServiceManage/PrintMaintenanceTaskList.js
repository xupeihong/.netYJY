
$(document).ready(function () {
    jq();//加载数据
    //jq1();
    //jq2();
    // 打印 
    $("#btnPrint").click(function () {
        //alert(1);
        document.getElementById("btnPrint").className = "Noprint";
        $("#ReportContent").attr("style", "width: 100%;margin-top:10px")
        pagesetup("0.3", "0", "0.3", "0.3");
        window.print();
        document.getElementById("btnPrint").className = "btn";
        $("#ReportContent").attr("style", "margin: 0 auto; width: 90%;margin-top:10px")
    });
})

function jq() {
    if (location.search != "") {
        Info = location.search.split('&')[0].split('=')[1];
        type = location.search.split('&')[1].split('=')[1];
    }
    if (type == "1") {
        $("#dayin").hide();
        $("#jc").hide();
        $("#rq").hide();
    }
    $.ajax({
        url: "GetUserComplaintsList",
        type: "Post",
        data: {
            Info: Info
        },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    $("#BXID").val(json[i].BXID);
                    $("#Customer").val(json[i].Customer);
                    $("#RepairDate").val(json[i].RepairDate);
                    $("#ContactName").val(json[i].ContactName);
                    $("#Tel").val(json[i].Tel);
                    $("#DeviceType").val(json[i].DeviceType);
                    $("#RepairTheUser").val(json[i].RepairTheUser);
                    $("#Sate").val(json[i].Sate);
                    $("#CreateUser").val(json[i].CreateUser);
                    $("#Remark").val(json[i].Remark);
                }
            }
        }
    });
}
function deleteTr(date) {
    var one = confirm("确认删除");
    if (one == false)
        return;
    else {
        var newCount = date.id;
        var strRow = newCount.charAt(newCount.length - 1);
        // $("#DetailInfo" + strRow).parent().parent().remove();
        $("#DetailInfo" + strRow).closest('tr').remove();
    }
}

