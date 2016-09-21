$(document).ready(function () {
    var msg = $("#msg").val();
    if (msg != "") {
        alert(msg);
        window.parent.frames["iframeRight"].reloadpro();//刷新子窗体
        //新增页面和内部评审页面都要添加产品，服务等
        //window.parent.frames["iframeRight"].reload();//刷新父窗体
        window.parent.ClosePop();
    }
    $("#hole").height($(window).height());
    $("#sure1").click(function () {
        if ($("#Ptype").val() == "") {
            alert("产品分类不能为空"); return;
        }
        if ($("#SelDesc").val() == "") {
            alert("产品名称不能为空"); return;
        }
        if ($("#Stand").val() == "") {
            alert("规格类型不能为空"); return;
        }
        //if ($("#Proid").val() == "") {
        //    alert("产品编号不能为空");
        //    return;
        //}
        if ($("#Measureunit").val() == "") {
            alert("单位不能为空"); return;
        }
        if ($("#Price").val() == "") {
            alert("价格不能为空"); return;
        }
        var FileName = $("#UploadFile").val();
        if (FileName.replace(/(\s*$)/g, '') != "") {
            var filetype = (/\.[^\.]+$/.exec(FileName)).toString();
            if (filetype.toLowerCase().indexOf('xlsx') < 0 && filetype.toLowerCase().indexOf('docx') < 0 && filetype.toLowerCase().indexOf('pic') < 0 && filetype.toLowerCase().indexOf('xls') < 0 && filetype.toLowerCase().indexOf('doc') < 0 && filetype.toLowerCase().indexOf('jpg') < 0 && filetype.toLowerCase().indexOf('png') < 0 && filetype.toLowerCase().indexOf('gif') < 0) {
                alert("不支持该类型文件的上传，请上传word,pic,excel,png,jpg,gif格式文件");
                return;
            }
        }
        var res = confirm("确定要保存产品信息吗？");
        if (res) {
            //$("#Productname").val($("#SelDesc").val());
            //$("#Standard").val($("#Stand").val());
            //$("#Productid").val($("#Proid").val());
            document.forms[0].submit();
        }
        else {
            return;
        }

    })
    //产品名称
    $('#SelDesc').click(function () {
        selid('GetSelDesc', 'SelDesc', 'divSelDesc', 'ulSelDesc', 'LoadDesc');
    })
    //产品规格类型
    $('#Stand').click(function () {
        selstand('GetStand', 'Stand', 'divStand', 'ulStand', 'LoadStand');
    })
    //产品编号
    $('#Proid').click(function () {
        selProid('GetProIDS', 'Proid', 'divProid', 'ulProid', 'LoadProID');
    })

});
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
function selProid(actionProid, selProid, divproid, ulproid, jsfun) {
    $.ajax({
        url: actionProid,
        type: "post",
        data: { Stand: $("#Stand").val() }, //产品类型
        dataType: "json",
        async: false,
        success: function (data) {
            if (data != "") {
                var unit = data.split('?');
                $("#" + divproid).show();
                $("#" + ulproid + " li").remove();
                for (var i = 0; i < unit.length; i++) {
                    $("#" + ulproid).append("<li style='cursor:pointer;margin-left:0px; width:120px;text-align:center;height:20px;list-style-type:none; color:black;'><span onclick='" + jsfun + "(this);' style='margin-left:0px; width:120px; height:20px;display:block;'>" + unit[i] + "</span>");
                }
            }
            else {
                $("#" + divproid).hide();
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
function LoadProID(liInfo) {
    $('#Proid').val($(liInfo).text());
    $('#divProid').hide();
}

//获得产品名称
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
//获得规格类型
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
//获得产品编号
function getProid() {
    if (/^\d+$/.test($("#Proid").val())) {
        $("#warn").show();
        return;
    } else {
        var ulproid = "ulProid";
        var jsfun = "LoadProID";
        $.ajax({
            url: "getProID",
            type: "post",
            data: { Proid: $("#Proid").val(), Stand: $("#Stand").val() },
            dataType: "json",
            async: false, //是否异步
            success: function (data) {
                if (data.success == "true") {
                    if (data != "" && data.strDescLink != "" && data.strDescLink != null) {
                        var unit = data.strDescLink.split('!');
                        $("#divProid").show();
                        $("#" + ulproid + " li").remove();
                        for (var i = 0; i < unit.length; i++) {
                            $("#" + ulproid).append("<li style='cursor:pointer;margin-left:0px; width:120px;height:20px;list-style-type:none; color:black;'><span onclick='" + jsfun + "(this);' style='margin-left:0px; width:120px; height:20px;display:block;'>" + unit[i] + "</span>");
                        }
                    }
                }
                else {
                    $("#divProid").hide();
                }
            }
        })
    }
}


