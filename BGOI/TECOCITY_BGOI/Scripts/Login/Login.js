$(document).ready(function () {
    window.moveTo(0, 0);
    window.resizeTo(window.screen.width, window.screen.height);
    $("#LoginAll").height($(window).height());
    $("#middle").height($(window).height() - $("#top").height() - $("#foot").height() - 2);
    $("#fieldset").css("position", "relative");
    $("#fieldset").css("top", $("#middle").height() / 3 - 40);
})