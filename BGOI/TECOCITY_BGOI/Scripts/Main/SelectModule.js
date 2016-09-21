var Role;
$(document).ready(function () {
    Role = $("#Role").val();
    $("#pageSelectAll").height($(window).height());
})

function selectM(name,obj)
{
    var userRole = "";
    userRole = "," + name + ",";
    if (Role.indexOf(userRole) >= 0) {
        window.location.href = "../Main/Main?id="+ name;
    }
    else {
        //alert("您没有访问该模块的权限");
        obj.className = "btnSelectR";
        $("#hint").html("您没有访问该模块的权限");
    }
}

function Mover(obj)
{
    obj.className = "btnSelectW";
}

function Mout(obj) {
    obj.className = "btnSelect";
    $("#hint").html("");
}