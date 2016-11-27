var date = false;
$(document).ready(function () {
    $(".del").click(function () {
        var excursionId = parseInt($(this).attr("excursionId"));
        var multi = $(this).attr("multi");
        var s = "";
        if (multi)
            s = "s";
        if (confirm("Вы уверенны, что хотите удалить данную экскурсию?")) {
            var model = {
                Integer: excursionId
            };
            $.ajax({
                type: "POST",
                url: "/Home/DeleteExcursion" + s,
                data: model,
                success: function (data) {
                    if (data)
                        $("#" + excursionId).remove();
                    else
                        $("#locationTable").replaceWith("<h3>В базе данных отсутствует искомая информация о местоположениях...</h3>");
                }
            });
        }
    });
    
    $("select[name=searchCriteria]").change(function () {
        if ($(this).val() == 10 && date == false) {
            $("input[name=searchString]").replaceWith("<input type='date' name='dateOfExcursion' class='form-control' />");
            date = true;
        }
        else if($(this).val() != 10 && date == true)
        {
            $("input[name=dateOfExcursion]").replaceWith("<input placeholder='Название, стиль...' type='text' name='searchString' class='form-control' />");
            date = false;
        }
    });
});