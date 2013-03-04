$(function () {
    $(document).ready(function () {
        $.getScript("../Scripts/Custom/jquery.validate.js", function () {
        });
    });

    $("#submit_btn").click(function (e) {
        $("#PrimaryGuardians").validate();

    });



});