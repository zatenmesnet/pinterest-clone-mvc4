function submitComment(form, id) {
    $.ajax({ 
      type: 'POST',
      url: 'http://localhost:14074/Service/PostComment/',
      data: 'id=' + id + '&comment=' + $(form).find('textarea').val(),
      cache: false, 
      success: function(html){
        location.reload();
      } 
    });
    return false;
}

function submitCommentAjx(id, comment) {
    $.ajax({ 
        type: 'POST',
        url: 'http://localhost:14074/Service/PostCommentAjax/',
        data: 'id=' + id + '&comment=' + comment,
        cache: false, 
        success: function(html){
        if (html != -1) {
            $('.comments').prepend(html);
            $(this).colorbox.resize();
        }
        else
            alert("Comment not posted");
        } 
    });
}

$(window).load(function(){
// masonry initialization
    $('.main_container').masonry({
        // options
        itemSelector : '.pin',
        isAnimated: true,
        isFitWidth: true
    });
});

$(document).ready(function(){
    // onclick event handler (for comments)
    $('.comment_tr').click(function () {
        $(this).toggleClass('disabled');
        $(this).parent().parent().parent().find('form.comment').slideToggle(400, function () {
            $('.main_container').masonry();
        });
    }); 

    $('#addpost').colorbox({
        width: "800px",
        height: "600px",
        onComplete: function(){
             $("#tabs").tabs();
             $('#fileupload').fileupload({
                dataType: 'json',
                done: function (e, data) {
                    window.location.replace("http://localhost:14074/");
                },
                add: function (e, data) {
                    $("#upload-button").empty();
                    data.context = $('<button/>').text('Upload')
                        .appendTo($("#upload-button"))
                        .click(function () {
                            data.context = $('<p/>').text('Uploading...').replaceAll($(this));
                            data.submit();
                        });
                },
                progressall: function (e, data) {
                    var progress = parseInt(data.loaded / data.total * 100, 10);
                    $('#progress .bar').css(
                        'width',
                        progress + '%'
                    );
                },
            });
        },
    });

    $('.ajax').colorbox({
        onOpen:function(){
        },
        onLoad:function(){
        },
        onComplete:function(){
            //resize
            $(this).colorbox.resize();

            //get pin id
            var iPinId = $(this).parent().parent().attr('pin_id');

            //get comments
            $.ajax({ 
              url: 'http://localhost:14074/Service/Comments/' + iPinId,
              cache: false, 
              success: function(html){
                $('.comments').append(html);

                $(this).colorbox.resize();
              } 
            });

        },
        onCleanup:function(){
        },
        onClosed:function(){
        },
    });

    // onclick event handler (for like button)
    $('.pin .actions .likebutton').click(function () {
        $(this).attr('disabled', 'disabled');

        var iPinId = $(this).parent().parent().parent().attr('pin_id');
        $.ajax({ 
          url: 'service.php',
          type: 'POST',
          data: 'add=like&id=' + iPinId,
          cache: false, 
          success: function(res){
            $('.pin[pin_id='+iPinId+'] .info .LikesCount strong').text(res);
          } 
        });
        return false;
    }); 

    // onclick event handler (for repin button)
    $('.pin .actions .repinbutton').click(function () {
        var iPinId = $(this).parent().parent().parent().attr('pin_id');
        $.ajax({ 
          url: 'service.php',
          type: 'POST',
          data: 'add=repin&id=' + iPinId,
          cache: false, 
          success: function(res){
            window.location.href = 'profile.php?id=' + res;
          } 
        });
        return false;
    });
});