var id;
$(document).ready(function () {
    if (location.search != "") {
        id = location.search.split('&')[0].split('=')[1];
    }
    alert(id);
    $("#btnSave").click(function () {
        alert("123");
        var PID = $("#PID").val();
        var ApprovalContent = $("#ApprovalContent").val();
        var PIDS = id;
        var ApprovalType = $("#ApprovalType").val();
        var ApprovalLevel = $("#ApprovalLevel").val(); 
        var Job = $("#Job").val();
        var ApprovalTime = $("#ApprovalTime").val();
        var IsPass = $("#IsPass").val();
        var NoPassReason = $("#NoPassReason").val();
        var ApprovalExplain = $("#ApprovalExplain").val();
        $.ajax({
            url: "InsertApproval",
            type: "Post",
            data: {
                PID: PID, ApprovalContent: ApprovalContent, PIDS: PIDS, ApprovalType: ApprovalType, ApprovalLevel: ApprovalLevel,
                Job: Job, ApprovalTime: ApprovalTime, IsPass: IsPass, NoPassReason: NoPassReason, ApprovalExplain: ApprovalExplain
            },
            async: false,
            success: function (data) {
                if (data.success == true) {
                    alert("成功");
                }
                else {
                    alert("失败");
                }
            }
        });
    });
});