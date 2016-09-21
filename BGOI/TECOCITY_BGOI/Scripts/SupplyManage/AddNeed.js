$(document).ready(function () {
    $("#NeedInfo").submit(function () {
        var options = {
            url: "InsertNeed",
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
        $("#NeedInfo").ajaxSubmit(options);
        return false;
    });
    $("#Save").click(function () {
        var needName = document.getElementById("Relation").value;
        var res = confirm("确定保存供应商信息吗？");
        if (res) {
            $("#NeedInfo").submit();
        } else {
            return;
        }
    });
    check();

});
function check() {
    $('.n1').click(function () {
        var ckbname = this.name;
        if (this.checked) {
            var getId = document.getElementById(this.name);
            var res = ckbname;
            document.getElementById("Relation").value = res;
            //$('.n1').each(function () {
            //    //if (this.name != ckbname) {
            //    //    $(this).attr("disabled", true);
            //    //}
            //});
        } //else {
        //    $('.n1').attr("disabled", false);
        //}
    })
}