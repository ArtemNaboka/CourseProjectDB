var prevCount = -1;

$(document).ready(function () {
    $(".del").click(function () {
        var sculptureId = parseInt($(this).attr("sculptureId"));
        if (confirm("Вы уверенны, что хотите удалить данную скульптуру?")) {
            var model = {
                Integer: sculptureId
            };
            $.ajax({
                type: "POST",
                url: "/Home/DeleteSculpture",
                data: model,
                success: function (data) {
                    if (data)
                        $("#" + sculptureId).remove();
                    else
                        $("#sculptureTable").replaceWith("<h3>В базе данных отсутствует искомая информация о скульптурах...</h3>");
                }
            });
        }
    });

    $("#close").click(function () {
        $("#dash").hide();
    });


    $('#step_forward').on('click', function (e) {
        e.preventDefault();
        if (prevCount != -1)
        {
            $("td[count=" + (prevCount - 1) + "]").css("background", "#98FB98");
        }
        var t = 1000;
        var d = $(this).attr('data-href') ? $(this).attr('data-href') : $(this).attr('href');
        var num = parseInt(d.substr(1));
        $("td[count=" + (num - 1) + "]").css("background", "#ADFF2F");
        $('html,body').stop().animate({ scrollTop: $(d).offset().top - 60 }, t);
        $(this).attr("data-href", "#" + (num + 1));
        prevCount = num;
        $("#step_back").attr("data-href", "#" + num);
    });


    $('#step_back').on('click', function (e) {
        e.preventDefault();
        if (prevCount != -1) {
            $("td[count=" + (prevCount - 1) + "]").css("background", "#98FB98");
        }
        var t = 1000;
        var d = $(this).attr('data-href') ? $(this).attr('data-href') : $(this).attr('href');
        var num = parseInt(d.substr(1)) - 1;
        $("td[count=" + (num - 1) + "]").css("background", "#ADFF2F");
        $('html,body').stop().animate({ scrollTop: $(d).offset().top - 150 }, t);
        $(this).attr("data-href", "#" + (num - 1));
        prevCount = num;
        $("#step_back").attr("data-href", "#" + num);
        $("#step_forward").attr("data-href", "#" + (num + 1));
    });
});
