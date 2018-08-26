jQuery(document).ready(function ()
{
    var previewOn = false;

    if ($("#page-cover").length)
    {
        var MAX_W=400;
 
        
        $(".img-view").click(ShowPreview);
        function ShowPreview()
        {
            previewOn = true;
            if (!$("#img-preview").length)
                CreatePreview();

            $("#page-cover").css("opacity", "0.8").fadeIn(300);
            $("#img-preview").css("opacity", "1.0").fadeIn(300);
            $("#btn-close-img").css("opacity", "8.0").fadeIn(300);
            onResize();
        }
        function HidePreview() {
            previewOn = false;
            $("#page-cover").fadeOut();
            $("#img-preview").fadeOut();
            $("#btn-close-img").fadeOut();

        }
        function CreatePreview() {
            var imgSrc = $(".img-thumbnail").attr('src');
            
            $('#page-cover').after($('<img>',{id:'img-preview',src:imgSrc}))
            
            
            $('#page-cover').after($('<input>',{type:'button', id:'btn-close-img', value:'Close'}))
            
            $('#btn-close-img').val("Close");
            $('#btn-close-img').click(HidePreview);
            $(window).resize(onResize);
            
            
           
        };
        
        function CenterH(elm)
        {
            var sW = $(window).innerWidth();
            var eW = elm.width();
            var p = (sW/2) - (eW/2);
            elm.css("left", p);
            
            
        };
        
        function CenterV(elm)
        {
            var sH = $(window).innerHeight();
            var eH = elm.height();
            var p = (sH/2) - (eH/2);
            elm.css("top", p);
            
        };
        
        function onResize() {
          if(previewOn)
              {
                  var sW = $(window).innerWidth();
                  var sH =$(window).innerHeight();
                  var imgW = $("#img-preview").width();
                  if(sW-100 < MAX_W)
                      {
                          var newW = sW-100;
                          $("#img-preview").width(newW);
                          $("#img-preview").height(newW);
                      }
                  else if(imgW !== MAX_W)
                      {
                          var newW2 = MAX_W;
                          $("#img-preview").width(newW2);
                          $("#img-preview").height(newW2);
                      }
                  CenterH($("#img-preview"));
                  CenterV($("#img-preview"));
                CenterH($("#btn-close-img"));
                  var bCloseTop = sH-$("#btn-close-img").height() - sH/5;
                  $("#btn-close-img").css('top', bCloseTop);
                  
              }
        };
        
    
    }

    
});