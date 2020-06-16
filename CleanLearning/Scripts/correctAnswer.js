﻿/// <summary>
/// Checks if correctAnswer option is selected if yes is selected twice give a alert and hide it after a few seconds.
/// </summary>
function val() {
    var count = 0;
    selects = document.getElementsByName("correctAnswer");
    alertbox = $('#alertBox');
    for (i = 0; i < selects.length; i++) {
        if (selects[i].value == 1) {
            count++;
        }
        if (count > 1) {
            alertbox.addClass("alert-danger");
            alertbox.html("<p>The maximum amount of correct anwser is 1!</p>");
            alertbox.show();
            window.scrollTo(0, 0);
            selects[i].value = 0;
        }
        else if (count <= 1) {
            alertbox.hide();
        }
    }

};