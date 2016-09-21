var level;
var curPage = 1;
var OnePageCount = 10;
var strFirsTypeText = "";
var strCount = "";
$(document).ready(function () {
    $("#Cancel").click(function () {
        window.parent.ClosePop();
    })
    $("#btnSave").click(function () {
        var PID = $("#PID").val();
        var Num = $("#Num").val();
        if (PID == "" || PID == null) {
            alert("物料编号为空");
            return;
        }
        if (Num == "" || Num == null) {
            alert("上限数量不能为空");
            return;
        }
        $.ajax({
            url: "SaveLowAlarm",
            type: "Post",
            data: {
                PID: PID, Num: Num
            },
            async: false,
            success: function (data) {
                if (data.success == true) {
                    alert("保存成功");
                    setTimeout('parent.ClosePop()', 10);
                    window.parent.frames["iframeRight"].reload();
                }
                else {
                    alert(data.msg);
                }
            }
        });
    });
})

function GetNum(pid) {
    $.ajax({
        url: "UpLowAlarm",
        type: "Post",
        data: {
            pid: pid
        },
        async: false,
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    $("#Num").val(json[i].Num);
                }
            }
        }
    });

    $.ajax({
        url: "GetPidXiang",
        type: "Post",
        data: {
            pid: pid
        },
        async: false,
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    $("#PIDnew").val(pid);
                    $("#Spec").val(json[i].Spec);
                }
            }
        }
    });
}
