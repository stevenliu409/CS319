/**
* JS file for implementing popup calendar.
* To implement popup calendar simplay set your
* HTML text input element to have id='datepicker'
* <input id='datepicker' type='text'>.
*
**/
document.write("<script src='http://code.jquery.com/jquery-1.8.2.js' type='text/javascript'></script>");
document.write("<script src='http://code.jquery.com/ui/1.8.24/jquery-ui.js' type='text/javascript'></script>");
document.write("<link rel='stylesheet' href='http://code.jquery.com/ui/1.8.24/themes/base/jquery-ui.css' />");

$(function () {
    $(".datepicker").datepicker({
        yearRange: "-20:+0",
        changeMonth: true,
        changeYear: true
    });
});
