$(function () {
    $(document).ready(function () {
        $.getScript("http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.11/jquery-ui.min.js", function () {
        });
    });


               $("#LastName").autocomplete({
    source: function (request, response) {
        // define a function to call your Action (assuming UserController)
        $.ajax({
            url: '/home/GetParticipants', type: 'GET', dataType: "json",

            // query will be the param used by your action method
            data: { query: request.term },
            success: function (data) {
                response($.map(data, function (item) {
                    return { label: item, value: item };
                }))
            }
        })
    },
    minLength: 1, // require at least one character from the user
}); 
         
    });