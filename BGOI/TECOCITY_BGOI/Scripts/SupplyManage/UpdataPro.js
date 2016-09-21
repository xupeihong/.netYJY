var PName, Standard, SID;
var curPage = 1;
var OnePageCount = 15;
var oldproID = 0;
$(document).ready(function () {
    var msg = $("#msg").val();
    if (msg != "") {
        alert(msg);
        window.parent.frames["iframeRight"].reloadpro();
        window.parent.ClosePop();
    }
    $("#hole").height($(window).height());
    $("#charge").click(function () {
        if ($("#Productid").val() == "") {
            alert("产品编号不能为空"); return;
        }
        if ($("#Productname").val() == "") { alert("产品名称不能为空"); return; }
        if ($("#Standard").val() == "") { alert("产品规格型号不能为空"); return; }
        if ($("#Measureunit").val() == "") { alert("单位不能为空"); return; }

        var a = confirm("确定更新产品信息吗")
        if (a == false)
            return;
        else {
            //$("#Productname").val($("#SelDesc").val());
            //$("#Standard").val($("#Stand").val());
            document.forms[0].submit();
        }
    })
    $('#SelDesc').click(function () {
        selid('GetSelDesc', 'SelDesc', 'divSelDesc', 'ulSelDesc', 'LoadDesc');
    })
    //产品规格
    $('#Stand').click(function () {
        selstand('GetStand', 'Stand', 'divStand', 'ulStand', 'LoadStand');
    })
})
function selid(actionid, selid, divid, ulid, jsfun) {
    $.ajax({
        url: actionid,
        type: "post",
        data: { ThirdType: $("#Ptype").val() }, //产品分类
        dataType: "json",
        async: false,
        success: function (data) {
            if (data != "") {
                var unit = data.split('?');
                $("#" + divid).show();
                $("#" + ulid + " li").remove();
                for (var i = 0; i < unit.length; i++) {
                    $("#" + ulid).append("<li style='cursor:pointer;margin-left:0px; width:120px;height:20px;text-align:center;list-style-type:none; color:black;'><span onclick='" + jsfun + "(this);' style='margin-left:0px; width:120px; height:20px;display:block;'>" + unit[i] + "</span>");
                }
            }
            else {
                $("#" + divid).hide();
            }
        }
    });
}
function selstand(actionstand, selstand, divid, ulid, jsfun) {
    $.ajax({
        url: actionstand,
        type: "post",
        data: { ThirdType: $("#Ptype").val() }, //产品分类
        dataType: "json",
        async: false,
        success: function (data) {
            if (data != "") {
                var unit = data.split('?');
                $("#" + divid).show();
                $("#" + ulid + " li").remove();
                for (var i = 0; i < unit.length; i++) {
                    $("#" + ulid).append("<li style='cursor:pointer;margin-left:0px; width:120px;text-align:center;height:20px;list-style-type:none; color:black;'><span onclick='" + jsfun + "(this);' style='margin-left:0px; width:120px; height:20px;display:block;'>" + unit[i] + "</span>");
                }
            }
            else {
                $("#" + divid).hide();
            }
        }
    });
}
function selprice(actionprice, selprice, divid, ulid, jsfun) {
    $.ajax({
        url: actionprice,
        type: "post",
        data: { ThirdType: $("#SelDesc").val() }, //产品名称
        dataType: "json",
        async: false,
        success: function (data) {
            if (data != "") {
                var unit = data.split('?');
                $("#" + divid).show();
                $("#" + ulid + " li").remove();
                for (var i = 0; i < unit.length; i++) {
                    $("#" + ulid).append("<li style='cursor:pointer;margin-left:0px; width:120px;text-align:center;height:20px;list-style-type:none; color:black;'><span onclick='" + jsfun + "(this);' style='margin-left:0px; width:120px; height:20px;display:block;'>" + unit[i] + "</span>");
                }
            }
            else {
                $("#" + divid).hide();
            }
        }
    });
}
function LoadDesc(liInfo) {
    $('#SelDesc').val($(liInfo).text());
    $('#divSelDesc').hide();
}
function LoadStand(liInfo) {
    $('#Stand').val($(liInfo).text());
    $('#divStand').hide();
}
function getDescLink() {
    // 判断是否全部为数字
    if (/^\d+$/.test($("#SelDesc").val())) {
        $("#warning").show();
        return;
    } else {
        var ulid = "ulSelDesc";
        var jsfun = "LoadDesc";
        $.ajax({
            url: "getDescLink",
            type: "post",
            data: { SelDesc: $("#SelDesc").val(), ThirdType: $("#Ptype").val() },
            dataType: "json",
            async: false, //是否异步
            success: function (data) {
                if (data.success == "true") {
                    if (data != "" && data.strDescLink != "" && data.strDescLink != null) {
                        var unit = data.strDescLink.split('!');
                        $("#divSelDesc").show();
                        $("#" + ulid + " li").remove();
                        for (var i = 0; i < unit.length; i++) {
                            $("#" + ulid).append("<li style='cursor:pointer;margin-left:0px; width:120px;height:20px;list-style-type:none; color:black;'><span onclick='" + jsfun + "(this);' style='margin-left:0px; width:120px; height:20px;display:block;'>" + unit[i] + "</span>");
                        }
                    }
                }
                else {
                    $("#divSelDesc").hide();
                }
            }
        })

    }
}
function getStand() {
    if (/^\d+$/.test($("#Stand").val())) {
        $("#warn").show();
        return;
    } else {
        var ulid = "ulStand";
        var jsfun = "LoadStand";
        $.ajax({
            url: "getProStand",
            type: "post",
            data: { Stand: $("#Stand").val(), ThirdType: $("#Ptype").val() },
            dataType: "json",
            async: false, //是否异步
            success: function (data) {
                if (data.success == "true") {
                    if (data != "" && data.strDescLink != "" && data.strDescLink != null) {
                        var unit = data.strDescLink.split('!');
                        $("#divStand").show();
                        $("#" + ulid + " li").remove();
                        for (var i = 0; i < unit.length; i++) {
                            $("#" + ulid).append("<li style='cursor:pointer;margin-left:0px; width:120px;height:20px;list-style-type:none; color:black;'><span onclick='" + jsfun + "(this);' style='margin-left:0px; width:120px; height:20px;display:block;'>" + unit[i] + "</span>");
                        }
                    }
                }
                else {
                    $("#divStand").hide();
                }
            }
        })
    }
}
