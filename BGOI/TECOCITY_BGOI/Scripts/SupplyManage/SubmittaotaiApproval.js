
var curPage = 1;
var OnePageCount = 20;
var webkey;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    //var webkey = $("#webkey").val();
    //if (webkey == "年度评审") {
    //    $("#one").show();
    //    $("#two").show();
    //    $("#five").hide();
    //    $("#six").hide();
    //}
    //else {
    //    $("#one").hide();
    //    $("#two").hide();
    //    $("#five").show();
    //    $("#six").show();
    //}
    $('#TJ').click(function () {
        var isCompany = $('input:radio[name="ISAgree"]:checked').val();
        var OpinOut = document.getElementById('OpinionsD').value;
        if (isCompany != "0" && isCompany != "1") {
            alert("是否同意通过部门年审不能为空"); return;
        }
        if (isCompany == "1" && OpinOut == "") {
            alert("部门负责人意见不能为空"); return;
        }

        var one = confirm("确认提交部门级审批吗？");
        if (one == false)
            return;
        else {
            $.ajax({
                url: "InserttaotaiApproval",
                type: "post",
                data: { data3: $("#RelevanceID").val(), data4: isCompany, data5: OpinOut },
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

