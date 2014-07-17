function send()
{
    var data = $('#mydata').val()
    $.ajax({
        type: "POST",
        url: "marlight.php",
        data: "command="+data,
        success: function(html) {
            $("#result").empty();
            $("#result").append(html);
        }
    });

}

