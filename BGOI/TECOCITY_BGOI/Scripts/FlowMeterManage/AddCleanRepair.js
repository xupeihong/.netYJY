var isConfirm = false;
var ai = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    var Request = GetRequest();
    var RID = Request["RID"];
    $("#StrRID").val(RID);
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
    var options = {
        url: "InsertCleanRepair",
        data: {},
        type: 'POST',
        async: false,
        success: function (data) {
            if (data.success == "true") {
                window.parent.frames["iframeRight"].reload();
                alert(data.Msg);
                setTimeout('parent.ClosePop()', 10);
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