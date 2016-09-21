$(document).ready(function () {
    $("#hole").height($(window).height());
    $("#ProjectformInfo").submit(function () {
        var options = {
            url: "InsertDeviceBas",
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
                    alert(data.Msg);
                }
            }
        };
        $("#ProjectformInfo").ajaxSubmit(options);
        return false;
    })

    $("#charge").click(function () {
        if ($("#StrControlCode").val() == "") {
            alert("控制编号不能为空");
            return;
        }
        if ($("#StrEname").val() == "") {
            alert("设备名称不能为空");
            return;
        }
        if ($("#StrSpecification").val() == "") {
            alert("规格型号不能为空");
            return;
        }
        if ($("#StrTracingType").val() == "") {
            alert("请选择溯源类型");
            return;
        }
        if ($("#StrCycleType").val() == "") {
            alert("请选择检定/校准周期类型");
            return;
        }
        if ($("#StrCycle").val() == "") {
            alert("检定/校准周期不能空");
            return;
        }
        if (isNaN($("#StrCycle").val())) {
            alert("检定/校准周期请填写数字");
            return;
        }
        var a = confirm("确定要添加新设备吗")
        if (a == false)
            return;
        else {
            $("#ProjectformInfo").submit();
        }
    })
})

function parseISO8601(dateStringInRange) {
    var isoExp = /^\s*(\d{4})-(\d\d)-(\d\d)\s*$/,
        date = new Date(NaN), month,
        parts = isoExp.exec(dateStringInRange);

    if (parts) {
        month = +parts[2];
        date.setFullYear(parts[1], month - 1, parts[3]);
        if (month != date.getMonth() + 1) {
            date.setTime(NaN);
        }
    }
    return date;
}


function loadPlanCalibrationDate() {
    var CycleType = $("#StrCycleType").val();
    var Cycle = $("#StrCycle").val();
    var date = $("#StrLastDate").val();
    var newDate;
    if (CycleType == "Cy1") {
        if (Cycle == "")
            return;
        //newDate = new Date(date);
        newDate = parseISO8601(date);
        newDate.setFullYear(newDate.getFullYear() + parseInt(Cycle));
        newDate = jsTimeToString(newDate)
        $("#StrPlanDate").val(newDate);
    }
    if (CycleType == "Cy2") {
        if (Cycle == "")
            return;
        //newDate = new Date(date);
        newDate = parseISO8601(date);
        newDate.setMonth(newDate.getMonth() + parseInt(Cycle));
        newDate = jsTimeToString(newDate)
        $("#StrPlanDate").val(newDate);

    }
    if (CycleType == "Cy3") {
        if (Cycle == "")
            return;
        //newDate = new Date(date);
        newDate = parseISO8601(date);
        newDate.setDate(newDate.getDate() + parseInt(Cycle));
        newDate = jsTimeToString(newDate)
        $("#StrPlanDate").val(newDate);
    }
}

function loadPlanCheckDate() {
    var CycleType = $("#StrCheckCycleType").val();
    var Cycle = $("#StrCheckCycle").val();
    var date = $("#StrLastCheckDate").val();
    var newDate;
    if (CycleType == "Cy1") {
        if (Cycle == "")
            return;
        newDate = new Date(date);
        newDate.setFullYear(newDate.getFullYear() + parseInt(Cycle));
        newDate = jsTimeToString(newDate)
        $("#StrPlanCheckDate").val(newDate);
    }
    if (CycleType == "Cy2") {
        if (Cycle == "")
            return;
        newDate = new Date(date);
        newDate.setMonth(newDate.getMonth() + parseInt(Cycle));
        newDate = jsTimeToString(newDate)
        $("#StrPlanCheckDate").val(newDate);

    }
    if (CycleType == "Cy3") {
        if (Cycle == "")
            return;
        newDate = new Date(date);
        newDate.setDate(newDate.getDate() + parseInt(Cycle));
        newDate = jsTimeToString(newDate)
        $("#StrPlanCheckDate").val(newDate);
    }
}

function loadPlanTestDate() {
    var CycleType = $("#StrTestCycleType").val();
    var Cycle = $("#StrTestCycle").val();
    var date = $("#StrLastTestDate").val();
    var newDate;
    if (CycleType == "Cy1") {
        if (Cycle == "")
            return;
        newDate = new Date(date);
        newDate.setFullYear(newDate.getFullYear() + parseInt(Cycle));
        newDate = jsTimeToString(newDate)
        $("#StrPlanTestDate").val(newDate);
    }
    if (CycleType == "Cy2") {
        if (Cycle == "")
            return;
        newDate = new Date(date);
        newDate.setMonth(newDate.getMonth() + parseInt(Cycle));
        newDate = jsTimeToString(newDate)
        $("#StrPlanTestDate").val(newDate);

    }
    if (CycleType == "Cy3") {
        if (Cycle == "")
            return;
        newDate = new Date(date);
        newDate.setDate(newDate.getDate() + parseInt(Cycle));
        newDate = jsTimeToString(newDate)
        $("#StrPlanTestDate").val(newDate);
    }
}
function jsTimeToString(time) {
    var year = time.getFullYear();

    var month = time.getMonth() + 1;
    var day = time.getDate();
    var hour = time.getHours();
    var minute = time.getMinutes();
    var second = time.getSeconds();
    if (month < 10) {
        month = "0" + month;
    }
    if (day < 10) {
        day = "0" + day;
    }
    if (hour < 10) {
        hour = "0" + hour;
    }
    if (minute < 10) {
        minute = "0" + minute;
    }
    if (second < 10) {
        second = "0" + second;
    }
    var strTime = year + "-" + month + "-" + day;
    return strTime;
}