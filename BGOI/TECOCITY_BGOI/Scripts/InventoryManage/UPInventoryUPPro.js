
$(document).ready(function () {
    $("#Cancel").click(function () {
        window.parent.ClosePop();
    })
    jq();
    $("#btnSave").click(function () {
        var PID = $("#PID").val();
        var MaterialNum = $("#MaterialNum").val();
        var Spec = $("#Spec").val();
        var ProName = $("#ProName").val();
        var ProTypeID = $('#ProTypeID').val();
        var Remark = $("#Remark").val();
        var UnitPrice = $("#UnitPrice").val();
        var Units = $("#Units").val();
        var Ptype = $("#Ptype").val();
        var Manufacturer = $("#Manufacturer").val();
        var Price2 = $("#Price2").val();
        var Detail = $("#Detail1").val();
        if (PID == "" || PID == null) {
            alert("货品编号不能为空");
            return;
        }
        if (Ptype == "请选择" || Ptype == null) {
            alert("产品类型不能为空");
            return;
        }
        if (Manufacturer == "" || Manufacturer == null) {
            alert("厂家不能为空");
            return;
        }
        $.ajax({
            url: "SaveInventoryAddPro",
            type: "Post",
            data: {
                PID: PID, MaterialNum: MaterialNum, Spec: Spec, ProName: ProName, ProTypeID: ProTypeID, UnitPrice: UnitPrice,
                Units: Units, Ptype: Ptype, Manufacturer: Manufacturer, Price2: Price2, Detail: Detail, Remark: Remark
            },
            async: false,
            success: function (data) {
                if (data.success == true) {
                    alert("修改成功");
                    setTimeout('parent.ClosePop()', 10);
                    window.parent.frames["iframeRight"].reload();
                }
                else {
                    alert(data.Msg);
                }
            }
        });
    });
  
});

function jq() {
    if (location.search != "") {
        PID = location.search.split('&')[0].split('=')[1];
    }
    var UnitIDnew = $("#UnitIDnew").val();
    $.ajax({
        url: "UpInventoryList",
        type: "Post",
        data: {
            PID: PID
        },
        async: false,
        success: function (data) {
            if (data.success == true) {
                var json = eval(data.datas);
                if (json.length > 0) {
                    for (var i = 0; i < json.length; i++) {//循环要修改的数据
                        //$("#Spec option:contains('" + json[i].Spec + "')").prop('selected', true);//规格型号
                        //$("Spec").val(json[i].Spec);
                        $("#ProTypeID option:contains('" + json[i].Text + "')").prop('selected', true);
                        $("#Ptype option:contains('" + json[i].Ptext + "')").prop('selected', true);//为产品类型下拉框选择默认值
                        $("#Manufacturer option:contains('" + json[i].Ptext + "')").prop('selected', true);//厂家
                    }
                }
            }
            else {
                alert(data.Msg);
            }
        }
    });
}
function changcollege(va) {

    $.ajax({
        url: "GetHouseIDone",
        type: "post",
        data: { va: va },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                var IsHouseIDone = document.getElementById("IsHouseIDone");
                for (var i = 0; i < json.length; i++) {
                    IsHouseIDone.options.add(new Option(json[i].HouseName, json[i].HouseID));
                }
            }
        }
    })
    $.ajax({
        url: "GetHouseIDtwo",
        type: "post",
        data: { va: va },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                var IsHouseIDtwo = document.getElementById("IsHouseIDtwo");
                for (var i = 0; i < json.length; i++) {
                    IsHouseIDtwo.options.add(new Option(json[i].HouseName, json[i].HouseID));
                }
            }
        }
    })
}
function GaiBianone() {
    $("#IsHouseIDtwo").hide();
    $("#twodt").hide();
    $("#twodr").hide();
}
function GaiBiantwo() {
    $("#IsHouseIDone").hide();
    $("#onedt").hide();
    $("#onedr").hide();
}
