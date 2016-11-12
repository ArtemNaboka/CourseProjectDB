$(document).ready(function () {
    $(".del").click(function () {
        var styleId = parseInt($(this).attr("styleId"));
        if (confirm("Вы уверенны, что хотите удалить данный стиль?")) {
            var model = {
                Integer: styleId
            };
            $.ajax({
                type: "POST",
                url: "/Home/DeleteStyle",
                data: model,
                success: function (data) {
                    if (data)
                        $("#" + styleId).remove();
                    else
                        $("#styleTable").replaceWith("<h3>В базе данных отсутствует информация о стилях скульптуры...</h3>");
                }
            });
        }
    });

    //$("input[name=radioSort]").change(function () {
    //    var model = {
    //        Integer: $('input:radio[name=radioSort]:checked').val()
    //    };
    //    var request = getXmlHttpRequest(); // создание объекта XmlHttpRequest
    //    request.open('GET', location + model, false); // готовим сам запрос
    //    request.send(null);
    //});
});