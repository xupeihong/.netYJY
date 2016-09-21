$(document).ready(function () {


    $("#Submit").click(function () {
        AddLJ();
    });

});
function AddLJ() {
    var Warehouse = $("#Warehouse").val();
    var id = location.search.split('&')[0].split('=')[1];
    $.ajax({
        url: "UpdateWarehouse",
        type: "Post",
        data: {
            warehouse: Warehouse, transfernum: id
        },
        async: false,
        success: function (data) {
            if (data.success == true) {
                window.parent.frames["iframeRight"].reload();
                alert("成功");
                setTimeout('parent.ClosePop()', 100);
            }
            else {
                alert("失败");
            }
        }
    });

}