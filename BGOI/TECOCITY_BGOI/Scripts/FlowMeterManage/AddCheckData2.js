var isConfirm = false;
$(document).ready(function () {
    var Request = GetRequest();
    var type = Request["RepairType"];
    document.getElementById('typetitle').innerText = type;

    var RID = Request["RID"];
    $("#StrRID").val(RID);
    $("#RepairType").val(type);
    $("#pageContent").height($(window).height());

    $("#QD").click(function () {
        isConfirm = confirm("确定要建新项目吗")
        if (isConfirm == false) {
            return false;
        }
        else
            submitInfo();
    })
})

function returnConfirm() {
    return false;
}
function submitInfo() {
    $("#Strvalidate").val($('input:radio[name="IsRepair"]:checked').val());
    var options = {
        url: "InsertCheckData2",
        data: { StrRepairType: $("#RepairType").val() },
        type: 'POST',
        async: false,
        success: function (data) {
            if (data.success == "true") {
                window.parent.frames["iframeRight"].reload();
                alert(data.Msg);
                setTimeout('parent.ClosePop()', 100);
            }
            else {
                // alert(data.Msg);
            }
        }
    };
    $("#ProjectformInfo").ajaxSubmit(options);
    return false;
}
function GetRequest() {

    var url = location.search; //获取url中"?"符后的字串

    var theRequest = new Object();

    if (url.indexOf("?") != -1) {

        var str = url.substr(1);

        strs = str.split("&");

        for (var i = 0; i < strs.length; i++) {

            theRequest[strs[i].split("=")[0]] = unescape(strs[i].split("=")[1]);

        }

    }

    return theRequest;

}