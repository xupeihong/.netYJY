jQuery.validator.addMethod("injection",
function (value, element, params) {

    var RexValue = params.rexvalue;
    //alert(RexValue);
    //alert(value);
    return value.match(RexValue);
});

jQuery.validator.unobtrusive.adapters.add("injection", ["rexvalue"], function (options) {
    options.rules["injection"] = {
        rexvalue: options.params.rexvalue
    };
    options.messages["injection"] = options.message;
});