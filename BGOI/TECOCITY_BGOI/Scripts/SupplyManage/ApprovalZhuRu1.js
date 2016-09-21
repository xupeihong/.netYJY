
var curPage = 1;
var OnePageCount = 20;
var webkey;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());

    $('#TJ').click(function () {
        var state = $("input[name='SState']:checked").val();
        var content = document.getElementById("SContent").value;
        var sid = document.getElementById("sid").value;
        if (state == "1" && content == "") {
            alert("内部负责人意见不能为空"); return;
        }
        var one = confirm("确认提交部门负责人审批信息吗");
        if (one == false)
            return;
        else {
            $.ajax({
                url: "Insertzhunru1",
                type: "post",
                data: { data4: state, data5: content,data1:sid },
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

