var isConfirm = false;
$(document).ready(function () {

    $("#charge").click(function () {
        var isConfirm = confirm("确定要修改数据吗")
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
        url: "UpEarlyContact",
        data: {},
        type: 'POST',
        async: false,
        success: function (data) {
            if (data.success == "true") {
                window.parent.frames["iframeRight"].reload();
                alert(data.Msg);
                setTimeout('parent.ClosePop()', 100);
            }
            else {
                //alert(data.Msg);
            }
        }
    };
    $("#ProjectformInfo").ajaxSubmit(options);
    return false;
}

function changeType() {
    var PID = $("#StrPID").val();
    var JQ = $("#StrJQType").val();
    $.ajax({
        url: "ChangeJQ",
        type: "post",
        data: { data1: PID, data2: JQ },
        dataType: "Json",
        success: function (data) {
            if (data.success == "true") {
                var content = data.Content;
                var man = data.Man;
                $("#StrContent").val(content);
                $("#StrCreatePerson").val(man);
            }
            else {
                return;
            }
        }
    })
}
