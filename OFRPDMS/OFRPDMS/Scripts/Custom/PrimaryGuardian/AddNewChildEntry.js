$(function () {
    $(document).ready(function () {
        $.getScript("../Scripts/Custom/Utility/DivExtender.js", function () {
        });
    });

    $("#btnAdd").click(function (e) {
        parentDiv = $("#children_container");
        newChildId = parseInt(parentDiv.children(':last').attr("id")) + 1

        childNum = "Children[" + newChildId + "]";

        newChildEntry = parentDiv.children(':first').clone(true);
        newChildEntry.attr("id", newChildId);

        newChildEntry.children("div").eq(0).children("input").attr("value", "");
        newChildEntry.children("div").eq(1).children("input").attr("value", "");
        newChildEntry.children("div").eq(2).children("input").attr("value", "");
        newChildEntry.children("div").eq(2).children("input").datepicker('destroy').removeAttr('id');


        newChildEntry.children("div").eq(0).find("input").attr("name", "Children[" + newChildId + "].FirstName")
        newChildEntry.children("div").eq(1).find("input").attr("name", "Children[" + newChildId + "].LastName")

/*        newChildEntry.children("div").eq(2).find("input").attr("name", "Children[" + newChildId + "].Birthdate");
        newChildEntry.children("div").eq(2).find("input").attr("class", "datepicker");
        newChildEntry.children("div").eq(2).find("input").datepicker({
            yearRange: "-20:+0",
            changeMonth: true,
            changeYear: true
        });*/

        newChildEntry.children("div").eq(2).find("input").attr("id", "datepicker_" + newChildId);

        addElementToParent(parentDiv, newChildEntry, true, 1000);
        $('.datepicker').not('.hasDatePicker').datepicker();
    });



});