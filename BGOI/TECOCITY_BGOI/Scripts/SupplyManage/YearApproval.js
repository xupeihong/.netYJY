
$(document).ready(function () {
    var msg = $("#msg").val();
    if (msg != "") {
        alert(msg);
        window.parent.frames["iframeRight"].reload();
        window.parent.ClosePop();
    }

    $("#hole").height($(window).height());
    $("#YApproval").click(function () {
        var Score1 = $("#Score1").val();
        var Score2 = $("#Score2").val();
        var Score3 = $("#Score3").val();
        var Score4 = $("#Score4").val();
        var Score5 = $("#Score5").val();
        var ReviewDate = $("#ReviewDate").val();
        var userName = $("#DeclareUser").val();
        var DeptName = $("#DeclareUnit").val();
        if (Score1 == "") { alert("质量管理体系不能为空，没分填0"); return; }
        if (Score2 == "") { alert("价格得分不能为空，没分填0"); return; }
        if (Score3 == "") { alert("供货及时率得分不能为空，没分填0"); return; }
        if (Score4 == "") { alert("服务得分不能为空，没分填0"); return; }
        if (ReviewDate == "") { alert("评价日期不能为空"); return; }
        if (userName == "") { alert("评分人不能为空"); return; }
        if (DeptName == "") { alert("评分部门不能为空"); return; }
        var res = confirm("确定要提交年度评价？");
        if (res) {
            document.forms[0].submit();
        }
        else {
            return;
        }
    })

})
function LoadSum(result) {
    result = parseInt($("#Score1").val());
    result += parseInt($("#Score2").val());
    result += parseInt($("#Score3").val());
    result += parseInt($("#Score4").val());
    $("#Score5").val(result);


}
function ChangeCK() {
    LoadSum(result);
    //判断根据分数
    //var Score5 = $("#Score5").val();//document.getElementById("Score5").value;
    if (result >= 80) {
        $("input[name='Result']:checked").val() = "0";
    } else if (result >= 60 && result < 80) {
        $("input[name='Result']:checked").val() = "1";
    }
    else {
        $("input[name='Result']:checked").val() = "2";
    }
}