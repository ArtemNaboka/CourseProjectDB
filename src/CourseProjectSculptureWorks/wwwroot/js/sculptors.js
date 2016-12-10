$(document).ready(function () {
    $(".del").click(function () {
        var sculptorId = parseInt($(this).attr("sculptorId"));
        if (confirm("Вы уверенны, что хотите удалить данного скульптора?")) {
            var model = {
                Integer: sculptorId
            };
            $.ajax({
                type: "POST",
                url: "/Home/DeleteSculptor",
                data: model,
                success: function (data) {
                    if (data)
                        $("#" + sculptorId).remove();
                    else
                        $("#sculptorTable").replaceWith("<h3>В базе данных отсутствует искомая информация о скульпторах...</h3>");
                }
            });
        }
    });

});