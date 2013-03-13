
$(function () {



    var count = 0;

    $(document).ready(function () {

        $('#child_birthdate_' + count).datepicker({
            showOn: "both",
            buttonText: "Date",
            buttonImage: "../Content/themes/base/images/calendar.gif",
            buttonImageOnly: true,
            yearRange: "-20:+0",
            changeMonth: true,
            changeYear: true,
            maxDate: new Date()
        });

        $.getScript("../Scripts/Custom/Utility/DivExtender.js", function () {
        });

    });


    $("#btnDelete").click(function (e) {
        parentDiv = $("#children_container");
        var length = $('#children_container div').children('div').length

        if (length > 6) {
            parentDiv.children(':last').remove();
        }
    });



    $("#btnAdd").click(function (e) {
        count++;
        parentDiv = $("#children_container");



        newChildEntry = parentDiv.children(':first').clone().find("input:text").val("").end();
        newChildEntry.children("div").find("img").remove();
        
        newChildEntry.attr("id", count);



        newChildEntry.children("div").eq(0).find("input").attr("id", "child_first_name[" + count + "].FirstName");
        newChildEntry.children("div").eq(1).find("input").attr("id", "child_last_name[" + count + "].LastName");
        newChildEntry.children("div").eq(2).find("input").attr("id", "child_birthdate_" + count);




        addElementToParent(parentDiv, newChildEntry, true, 1000);

        $('#child_birthdate_' + count).removeAttr('class')
        $('#child_birthdate_' + count).datepicker({
            showOn: "both",
            buttonText: "Date",
            buttonImage: "../Content/themes/base/images/calendar.gif",
            buttonImageOnly: true,
            yearRange: "-20:+0",
            changeMonth: true,
            changeYear: true,
            maxDate: new Date()
        });

    });



});