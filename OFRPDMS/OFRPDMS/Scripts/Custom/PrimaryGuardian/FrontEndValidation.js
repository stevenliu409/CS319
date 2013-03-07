$(function () {
    $(document).ready(function () {
        $.getScript("../Scripts/Custom/jquery.validate.js", function () {
        });
        $.getScript("../Scripts/Custom/jquery.validate.min.js", function () {
        });
       
    });

    $("#submit_btn").click(function (e) {
        $("#PrimaryGuardians").validate();

    });



});