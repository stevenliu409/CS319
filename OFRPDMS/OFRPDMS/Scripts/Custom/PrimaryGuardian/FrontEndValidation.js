$(function () {
    $(document).ready(function () {
        $.getScript("../Scripts/jquery.validate.js", function () {
        });
        $.getScript("../Scripts/jquery.validate.min.js", function () {
        });
       
    });

    $("#submit_btn").click(function (e) {
        $("#PrimaryGuardians").validate();

    });



});