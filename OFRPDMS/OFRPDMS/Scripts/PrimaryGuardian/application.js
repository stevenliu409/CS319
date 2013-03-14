
$(document).ready(function () {

    $('.pickDate').datepicker({
       dateFormat: 'yy-mm-dd',
        showOn: "both",
        buttonText: "Calendar",
        buttonImage: "../../Content/themes/base/images/calendar.png",
        buttonImageOnly: true,
        yearRange: "-20:+0",
        changeMonth: true,
        changeYear: true,
        maxDate: new Date()
    });
});
function removeNestedForm(element, container, deleteElement) {

    $container = $(element).parents(container);

    $container.find(deleteElement).val('True');

    $container.hide();

}

function addNestedForm(container, counter, ticks, content) {

    var nextIndex = $(counter).length;

    var pattern = new RegExp(ticks, "gi");

    content = content.replace(pattern, nextIndex);

    $(container).append(content);
   
    $('.pickDate').datepicker({
        dateFormat: 'yy-mm-dd',
        showOn: "both",
        buttonText: "Calendar",
        buttonImage: "../../Content/themes/base/images/calendar.png",
        buttonImageOnly: true,
        yearRange: "-20:+0",
        changeMonth: true,
        changeYear: true,
        maxDate: new Date()
    });

}

