var strFFFS;
$(document).ready(function () {
    var DDID = location.search.split('&')[0].split('=')[1];
    $.ajax({
        url: "GetSupplierID",
        type: "post",
        data: { where: "a.DDID ='" + DDID + "'" },
        dataType: "Json",
        success: function (data) {
            if (data.success == "true") {
                var strSel = data.Strid;
                var strVal = data.Strname;
                var sel = strSel.split(',');
                var val = strVal.split(',');
                strFFFS = "<select id='Supplier' style='width:200px;'>";
                for (var i = 0; i < sel.length; i++) {
                    strFFFS += "<option value=" + sel[i] + ">" + val[i] + "</option>";
                }
                strFFFS += "</select>";
                fun();
            }
            else {
                document.getElementById("Supplier").options.length = 0;
                return;
            }
        }

    });

    $("#DY").click(function () {
 
    
        var Supplier = $("#Supplier").val();
        if (Supplier == "") {
            alert("请选择供货商");
            return;
        }
        var DDID = location.search.split('&')[0].split('=')[1];
        var url = "PrintDD?id=" + escape(DDID) + "&Supplier=" + escape(Supplier) + "";
        window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");


    });

});
function fun() {
    var htmls = "";
    htmls += "<tr>"
    htmls += '<td > 供货商 </td>';
    htmls += '<td > ' + strFFFS + ' </td>';
    htmls += "</tr>";
    $("#GXInfo").append(htmls);
}

