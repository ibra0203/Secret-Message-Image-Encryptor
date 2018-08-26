jQuery(document).ready(function ()
{
    
    if ($(".upload1").length)
    {
        $(".upload-btn").attr("disabled", "disabled");
        
        $(".upload1").change(function () {
            var valid = true;
            var size = parseFloat($(".upload1")[0].files[0].size).toFixed(2);
            if (size > 2000000)
                valid = false;

            var ext = $(".upload1").val().split(".").pop();

            if (ext !== "bmp")
                valid = false;

            if (valid)
                $(".upload-btn").removeAttr("disabled");
            else 
                $(".upload-btn").attr("disabled", "disabled");

                

        });
    }
});