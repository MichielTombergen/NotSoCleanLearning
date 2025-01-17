﻿var count = 2;
var answerType = $('input[name=answerType]');

/// <summary>  
/// Checks which answer is selected and show/hide divs depending on which answer is selected.
/// If the likert option is selected a required attribute will be added to the form.
/// </summary>  
answerType.change(function () {
    var selected = $('input[name=answerType]:checked');
    if (selected.val() === "multiplechoice") {
        $("#Option").css("display", "inline");
        $("#AddAnswer").css("display", "inline");
        $("#test").css("display", "none");
        $("#HP").attr("required", false);
    }

}
);

/// <summary>  
/// When the button is clicked add a new Partialview
/// Also check how many partialviews there are. The maximum amout is six
/// If the user wants to add more after six, they will get a error.
/// </summary>  
$('#AddAnswer').click(function (e) {
    e.preventDefault();


    if (count < 6) {
        url = $(this).data('url');

        target = $('#Option');

        $.get(url, function (data) {
            target.append(data);
        });

        count++;
    }
    else {
        alertbox = $('#alertBox');
        alertbox.addClass("alert-danger");
        alertbox.html("<p>The maximum amount of answers is 6!</p>");
        alertbox.show();
        window.scrollTo(0, 0);
    }

}
);
/// <summary>  
/// When the button is clicked delete a  Partialview
/// Also check how many partialviews there are. The minumum amout of partialviews are two.
/// If the user wants to delete another partialview when there are two left, they will get a error.
/// </summary>  
$(document).on('click', '.deleteAnswer', function (e) {
    e.preventDefault();
    if (count > 2) {
        count--;
        deleteAnswer($(this));

    }
    else {
        alertbox = $('#alertBox');
        alertbox.addClass("alert-danger");
        alertbox.empty();
        alertbox.html("<p>Multiple choice questions need at least 2 answers!</p>");
        alertbox.show();
        window.scrollTo(0, 0);
    }

}
);

function deleteAnswer(button) {
    wrapper = button.closest('.answerWrapper');
    wrapper.remove();
    optionDiv = $("#Option");
    optionDiv.load(optionDiv);
}

;