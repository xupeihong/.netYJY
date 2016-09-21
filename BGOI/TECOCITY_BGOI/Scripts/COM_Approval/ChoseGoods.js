var j = 0;
var strSelect;
var PIDS = "";
$(document).ready(function () {
    $("#hole").height($(window).height());
    loadHave();

    $('#charge').click(function () {
        var tab = document.getElementById("tabight");
        var tds = tab.getElementsByTagName("tr");
        var leng = document.getElementById("tabight").rows.length;
        if (leng <= 1) {
            alert("请选择成品信息");
            return;
        }
        var Name = "";
        var Spc = "";
        var Pid = "";
        var Num = "";
        var Units = "";
        var UnitPrice = "";
        var Price2 = "";
        for (var i = 1; i < tds.length; i++) {
            var td0 = tds[i].cells[0].innerHTML;
            Name += td0 + "$";
            var td1 = tds[i].cells[1].innerHTML;
            Spc += td1 + "$";
            var td2 = tds[i].cells[2].innerHTML;
            Units += td2 + "$";
            var td3 = tds[i].cells[3].innerHTML;
            UnitPrice += td3 + "$";

            var td4 = tds[i].cells[4].innerHTML;
            Price2 += td4 + "$";

            var td5 = tds[i].getElementsByTagName("td")[5].getElementsByTagName("INPUT")[0].value;
            if (td5 == "") {
                alert("请填写数量"); return;
            }
            if (isNaN(td5)) {
                alert("数量填写数字"); return;
            }
            if (parseInt(td5) != td5)
            {
                alert("数量填写整数"); return;
            }
            Num += td5 + "$";


      
            var td6 = tds[i].cells[6].innerHTML;
            Pid += td6 + "$";
        }
        Name = Name.substr(0, Name.length - 1);
        Spc = Spc.substr(0, Spc.length - 1);
        Pid = Pid.substr(0, Pid.length - 1);
        Num = Num.substr(0, Num.length - 1);
        Units = Units.substr(0, Units.length - 1);

        UnitPrice = UnitPrice.substr(0, UnitPrice.length - 1);
        Price2 = Price2.substr(0, Price2.length - 1);

        var one = confirm("确定保存选择的成品信息吗");
        if (one == false)
            return
        else {
            var type = $("#type").val();
            if (type == "") {
                window.parent.loadProduct(Name, Spc, Pid, Num, Units, UnitPrice, Price2);
                alert("选择成功");
                setTimeout('parent.ClosePop()', 100);
            }
            else {
                window.parent.frames["iframeRight"].loadProduct(Name, Spc, Pid, Num, Units, UnitPrice, Price2);
                alert("选择成功");
                setTimeout('parent.ClosePop()', 100);
            }
        }
    })

    $('#Product').click(function () {
        selid1('getProduct', 'GJ', 'divGJ', 'ulGJ', 'LoadGJ', 'BuildUnit');
    })

    $('#SearchProduct').click(function () {
        var ProductName = $("#Product").val();
        if (ProductName == "") {
            alert("请选择物品名称");
            return;
        }
        $.ajax({
            url: "SelectProduct",
            type: "post",
            data: { data1: ProductName },
            dataType: "json",
            async: false,
            success: function (data) {
                if (data.success == "true") {
                    if (data.Strname == "")
                        return;
                    var rname = data.Strname.split('$');
                    var rspc = data.Strspc.split('$');
                    var rpid = data.Stpid.split('$');
                    var runits = data.strunits.split('$');
                    var runitprice = data.Strunitprice.split('$');
                    var rprice2 = data.Strprice2.split('$');
                    var $tr = "";
                    for (var i = 0; i < rname.length; i++) {
                        $tr += "<tr class='tr'><td><input value=" + rname[i] + "$" + rspc[i] + "$" + rpid[i] + "$" + runits[i] + "$" + runitprice[i] + "$" + rprice2[i] + " name='cb' type='checkbox'/></td><td>" + rname[i] + "</td><td>" + rspc[i] + "</td><td>" + runits[i] + "</td><td>" + runitprice[i] + "</td><td>" + rprice2[i] + "</td><td  style='display:none;'>" + rpid[i] + "</td></tr>";
                    }
                    $("#tableft tr:not(:first)").empty();
                    $('#tableft').append($tr);
                }
                else {
                    $("#tableft tr:not(:first)").empty();
                    return;
                }
            }
        });
    })

    $('#ToRight').click(function () {
        var checked = false;
        var id = document.getElementsByName("cb");
        for (var i = 0; i < id.length; i++) {
            if (id[i].checked) {
                checked = true;
            }
        }
        if (!checked) {
            alert("请在左侧列表选择成品信息");
            return;
        }
        var cbval = "";
        $('input[name=cb]:checked').each(function () {
            cbval += $(this).val() + ",";
        });
        cbval = cbval.substr(0, cbval.length - 1);
        arrcb = cbval.split(',');
        var $tr = "";
        j++;
        var tab = document.getElementById("tabight");
        var tds = tab.getElementsByTagName("tr");
        var pid = "";
        for (var j = 1; j < tds.length; j++) {
            pid += tds[j].cells[6].innerHTML + ",";
        }
        pid = pid.substr(0, pid.length - 1);
        for (var i = 0 ; i < arrcb.length; i++) {
            var arrtr = arrcb[i].split('$');
            var newTr = "row" + j + i;
            var newText = "text" + j + i;
            if (pid.indexOf(arrtr[2]) < 0)
                $tr += "<tr class='tr' id=" + newTr + "><td>" + arrtr[0] + "</td><td>" + arrtr[1] + "</td><td>" + arrtr[3] + "</td><td>" + arrtr[4] + "</td><td>" + arrtr[5] + "</td><td><input id=" + newText + " type='text' style='width:50px;'/></td><td  style='display:none;'>" + arrtr[2] + "</td><td><a onclick='deleteTr(" + newTr + ")' style='color:blue;cursor:hand;'>删除</a></td></tr>";
        }
        //$("#tabight tr:not(:first)").empty();
        $('#tabight').append($tr);
    })
})

function loadHave() {
    var type = $("#type").val();
    var Name = "";
    var Spc = "";
    var Pid = "";
    var Num = "";
    var Units = "";

    var UnitPrice = "";
    var Price2 = "";

    if (type == "") {
        Name = window.parent.document.getElementById("Name").value;
        if (Name == "")
            return;
        Spc = window.parent.document.getElementById("Spc").value;
        Pid = window.parent.document.getElementById("Pid").value;
        Num = window.parent.document.getElementById("Num").value;
        Units = window.parent.document.getElementById("Units").value;
        UnitPrice = window.parent.document.getElementById("UnitPrice").value;
        Price2 = window.parent.document.getElementById("Price2").value;
    }
    else {
        Name = window.parent.frames["iframeRight"].document.getElementById("Name").value;
        if (Name == "")
            return;
        Spc = window.parent.frames["iframeRight"].document.getElementById("Spc").value;
        Pid = window.parent.frames["iframeRight"].document.getElementById("Pid").value;
        Num = window.parent.frames["iframeRight"].document.getElementById("Num").value;
        Units = window.parent.frames["iframeRight"].document.getElementById("Units").value;

        UnitPrice = window.parent.frames["iframeRight"].document.getElementById("UnitPrice").value;
        Price2 = window.parent.frames["iframeRight"].document.getElementById("Price2").value;
    }
    var arrName = Name.split('$');
    var arrSpc = Spc.split('$');
    var arrPid = Pid.split('$');
    var arrNum = Num.split('$');
    var arrUnits = Units.split('$');

    var arrUnitPrice = UnitPrice.split('$');
    var arrPrice2 = Price2.split('$');
    var $tr = "";
    for (var i = 0 ; i < arrName.length; i++) {
        var newTr = "rowH" + i;
        var newText = "textH" + i;
        $tr += "<tr class='tr' id=" + newTr + "><td>" + arrName[i] + "</td><td>" + arrSpc[i] + "</td><td>" + arrUnits[i] + "</td><td>" + arrUnitPrice[i] + "</td><td>" + arrPrice2[i] + "</td><td><input id=" + newText + " value=" + arrNum[i] + " style='width:50px;' type='text'/></td><td  style='display:none;'>" + arrPid[i] + "</td><td><a onclick='deleteTr(" + newTr + ")' style='color:blue;cursor:hand;'>删除</a></td></tr>";
    }
    //$("#tabight tr:not(:first)").empty();
    $('#tabight').append($tr);
}

function deleteTr(obj) {
    var one = confirm("确认删除该行吗？");
    if (one == false)
        return;
    else {
        if (obj) obj.parentNode.removeChild(obj);
    }
}

function selid1(actionid, selid, divid, ulid, jsfun, Type) {
    var TypeID = Type;// 行政区编码
    $.ajax({
        url: actionid,
        type: "post",
        data: { data1: TypeID },
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.success == "true") {
                //var unitid = data.Strid.split(',');
                var unit = data.Strname.split(',');
                $("#" + divid).show();
                $("#" + ulid + " li").remove();
                for (var i = 0; i < unit.length; i++) {
                    $("#" + ulid).append("<li style='cursor:pointer;margin-left:1px; width:190px;height:20px;list-style-type:none; color:black;'><span onclick='" + jsfun + "(\"" + unit[i] + "\");' style='margin-left:1px; width:190px; height:20px;display:block;'>" + unit[i] + "</span>");
                }
            }
            else {
                $("#" + divid).hide();
            }
        }
    });
}

function LoadGJ(liInfo) {
    $('#Product').val(liInfo);
    $('#divGJ').hide();
}

function BuildUnitkey() {
    $('#divGJ').hide();
}

function deleteTr(obj) {
    var one = confirm("确认删除该行吗？");
    if (one == false)
        return;
    else {
        if (obj) obj.parentNode.removeChild(obj);
    }
}