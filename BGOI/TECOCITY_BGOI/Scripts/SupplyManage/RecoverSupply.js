$(document).ready(function () {
    $("#charge").click(function () {
        var Opinions = document.getElementById("RecoverReson").value;
        if (Opinions == "") { alert("恢复原因不能为空"); return; }
        var res = confirm("确定恢复供货？");
        if (res) {
            $("#RecoverSupply").submit();
        } else {
            return;
        }
    })
    $("#RecoverSupply").submit(function () {
        var options = {
            url: "UPRecoverSupply",
            data: {},
            type: 'POST',
            async: false,
            success: function (data) {
                if (data.success == "true") {
                    alert(data.Msg);
                    window.parent.frames["iframeRight"].reload();
                    window.parent.ClosePop();
                }
                else {
                    alert(data.Msg);
                }
            }
        };
        $("#RecoverSupply").ajaxSubmit(options);
        return false;
    })
})