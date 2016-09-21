var curPage = 1;
var OnePageCount = 20;
var webkey;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $('#charge').click(function () {
        var isCompany = $('input:radio[name="ISAgree"]:checked').val();
        var OpinOut = document.getElementById('OpinionsD').value;
        if (isCompany != "0" && isCompany != "1") {
            alert("是否同意通过部门级不能为空"); return;
        }
        if (isCompany == "1" && OpinOut == "") {
            alert("部门负责人意见不能为空"); return;
        }

        var one = confirm("确认提交部门级审批吗？");
        if (one == false)
            return;
        else {
            $.ajax({
                url: "InsertzhunchuApproval",
                type: "post",
                data: { data1: $("#webkey").val(), data2: $("#folderBack").val(), data3: $("#RelevanceID").val(), data4: isCompany, data5: OpinOut },
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