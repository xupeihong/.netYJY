$(document).ready(function () {
    $("#wenzhang").height($(window).height());
    $("#wenzhang>dd>dl>dd").hide();
    $("#wenzhang>dd>dl>dd:first").show();
    $("#wenzhang>dd>dl>dt:first").css("background", "url('../images/choose.jpg') repeat-x");
    $("#wenzhang>dd>dl>dt:first").css("color", "white");
    //$("#im").attr("src", "../images/menu1.png");
    $('[id=son]').hide();
    $("#son").hide();
    $.each($("#wenzhang>dd>dl>dt"), function () {
        $(this).click(function () {
            $("#wenzhang>dd>dl>dd").not($(this).next()).slideUp();
            $(this).next().slideToggle(500);
            if ($(this).css("background") == "url('../images/choose.jpg') repeat-x") {
                $("#wenzhang>dd>dl>dt ").css("background", "url('../images/nochoose.jpg') repeat-x");
                $("#wenzhang>dd>dl>dt ").css("color", "#999999");
                $(this).css("background", "url('../images/choose.jpg')  repeat-x;");
                $(this).css("color", "white");
            }
            else {
                $("#wenzhang>dd>dl>dt ").css("background", "url('../images/nochoose.jpg') repeat-x");
                $("#wenzhang>dd>dl>dt ").css("color", "#999999");
                $(this).css("background", "url('../images/choose.jpg') repeat-x");
                $(this).css("color", "white");
            }
        });
    });

    $.each($("#wenzhang>dd>dl>dd>ul>li"), function () {
        $(this).click(function () {
            if ($(this).next().eq(0).attr('id') == "son") {
                var str = $(this).text().indexOf("+");
                var str2;
                if (str == 1) {
                    str2 = $(this).text();
                    str2 = str2.replace("+", "-");
                    $(this).html("<span>"+str2+"</span>");
                }
                else {
                    str2 = $(this).text();
                    str2 = str2.replace("-", "+");
                    $(this).html("<span>" + str2 + "</span>");
                }
                $("#son").not($(this).next()).slideUp();
                $(this).next().slideToggle(500);
            }
        });
    });
});

function TurnTo(url)
{
    parent.document.getElementById('iframeRight').src = url;
}