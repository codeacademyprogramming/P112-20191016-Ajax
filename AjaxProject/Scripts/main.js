$(document).ready(function () {

    //$("#select-manufacturer").change(function () {
    //    var elem = $(this);

    //    if (elem.val() != 0) {
    //        var xhttp = new XMLHttpRequest();
    //        var url = "https://localhost:44382/Home/ModelsJson/" + elem.val();
    //        xhttp.open("GET", url, true);
    //        xhttp.responseType = "json";

    //        xhttp.onreadystatechange = function () {
    //            if (this.readyState == 4 && this.status == 200) {
    //                console.log(this.response);
    //                var resp = this.response;
    //                console.log(resp.Data);


    //                if (resp.Success == true) {

    //                }
    //                //$("#select-models").empty();
    //                //for (var i = 0; i < resp.length; i++) {
    //                //    console.log(resp[i].Id, resp[i].Name);

    //                //   var opt =  $("<option></option>").attr("value", resp[i].Id).text(resp[i].Name);
    //                //    $("#select-models").append(opt);
    //                //}


    //            }
    //        };

    //        xhttp.send();

    //    }
    //});


    $("#select-manufacturer").change(function () {
        var elem = $(this);

        if (elem.val() != 0) {

            $.ajax({
                url: "https://localhost:44382/Home/Models/" + elem.val(),
                method: "GET",
                success: function (esger) {
                    console.log(esger);

                    $("#select-models").html(esger);
                    $("#ul-models").html(esger);
                    //var models = JSON.parse(anar.Data);

                    //for (var i = 0; i < models.length; i++) {

                    //    var opt = $("<option></option>").attr("value", models[i].Id).text(models[i].Name);
                    //    $("#select-models").append(opt);

                    //    $("#ul-models").append($("<li></li>").text(models[i].Name + " - " + models[i].Id));
                    //}


                }
            });


            //$.ajax({
            //    url : "https://localhost:44382/Home/ModelsJson/" + elem.val(),
            //    dataType : "json",
            //    method : "GET",
            //    success : function (anar) {

            //        if (anar.Success == true) {
            //            $("#select-models").empty();
            //            $("#ul-models").empty();

            //            var models = JSON.parse(anar.Data);

            //            for (var i = 0; i < models.length; i++) {

            //                var opt = $("<option></option>").attr("value", models[i].Id).text(models[i].Name);
            //                $("#select-models").append(opt);

            //                $("#ul-models").append($("<li></li>").text(models[i].Name + " - " + models[i].Id));


            //            }

            //        } else {
            //            alert(anar.ErrorMessage);
            //        }


            //    },
            //    error : function (err) {

            //    }

            //});


        }

    });


    // Register
    $("#register-form").submit(function (ev) {
        ev.preventDefault();
        var form = $(this);

        var file = form.find("input[name=Photo]")[0].files[0];
        console.log(file);

        var formData = new FormData();
        formData.append("Fullname", form.find("input[name=Fullname]").val());
        formData.append("Email", form.find("input[name=Email]").val());
        formData.append("Password", form.find("input[name=Password]").val());
        formData.append("Photo", file);

        console.log(formData.get("Fullname"));

        $.ajax({
            url: form.attr("action"),
            method: form.attr("method"),
            data: formData,
            processData : false,
            cache: false,
            contentType: false,
            enctype: "multipart/form-data",
            beforeSend: function () {
                $("#main-loader").css("display", "flex");
            },
            success: function (resp) {
                if (resp.Success == true) {
                    $("#register-form")[0].reset();
                    toastr.success(resp.Data);
                } else {
                    toastr.error(resp.ErrorMessage);
                }

                $("#main-loader").css("display", "none");
            },
            error: function (err) {
                toastr.error(err.Message);
                $("#main-loader").css("display", "none");

            }
        });
    });

    






    // Form Validation
    $.validate({
        modules: 'security, file'
    });

});