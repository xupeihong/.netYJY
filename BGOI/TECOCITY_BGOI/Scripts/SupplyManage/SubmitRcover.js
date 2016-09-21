var curPage = 1;
var OnePageCount = 20;
var webkey;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());

    $('#TJ').click(function () {

        var state = $("input[name='SState']:checked").val();
        var SContent = document.getElementById("SContent").value;
        //var Sperson = document.getElementById("Sperson").value;
        //var SCreate = document.getElementById("SCreate").value;
        if (state == null) {
            alert("是否同意恢复供应商不能为空"); return;
        }
        if (state == "1" && SContent == "") {
            alert("部门负责人意见不能为空"); return;
        }
        var one = confirm("确认提交部门级恢复供应商审批吗");
        if (one == false)
            return;
        else {
            $.ajax({
                url: "InsertRecover",
                type: "post",
                data: {  data3: $("#RelevanceID").val(), data4: state, data5: SContent },
                dataType: "Json",
                success: function (data) {
                    if (data.success == "true") {
                        alert(data.Msg);
                        window.parent.frames["iframeRight"].reload();
                        setTimeout('parent.ClosePop()', 100);
                    }
                    else {
                        alert(data.Msg);
                        return;
                    }
                }
            });
        }
    })
})