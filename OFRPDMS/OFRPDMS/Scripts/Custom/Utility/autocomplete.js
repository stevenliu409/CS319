$(function () {
    $(document).ready(function () {
        $.getScript("http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.11/jquery-ui.min.js", function () {
        });
    });

            $("#LastName").autocomplete({
            source: ["c++", "java", "php", "coldfusion", "javascript", "asp", "ruby"]
    });


});