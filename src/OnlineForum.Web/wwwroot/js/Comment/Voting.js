
var commentVoting = (function () {

    function upvoteComment(commentId, url) {
        $.post(url,
            {
                commentId: commentId
            },
            function (data) {

                clearButtonClasses(commentId);

                if (data.didScoreIncrease) {
                    $('.commentUpvoteButton.' + commentId).removeClass('btn-primary');
                    $('.commentUpvoteButton.' + commentId).addClass('btn-success');
                }

                $('.commentScore.' + commentId).html(data.score);
            });
    }

    function downvoteComment(commentId, url) {
        $.post(url,
            {
                commentId: commentId
            },
            function (data) {

                clearButtonClasses(commentId);

                if (!data.didScoreIncrease) {
                    $('.commentDownvoteButton.' + commentId).removeClass('btn-primary');
                    $('.commentDownvoteButton.' + commentId).addClass('btn-danger');
                }

                $('.commentScore.' + commentId).html(data.score);
            });
    }

    function clearButtonClasses(commentId) {
        $('.commentDownvoteButton.' + commentId).removeClass('btn-danger');
        $('.commentDownvoteButton.' + commentId).addClass('btn-primary');
        $('.commentUpvoteButton.' + commentId).removeClass('btn-success');
        $('.commentUpvoteButton.' + commentId).addClass('btn-primary');
    }

    return {
        "upvoteComment": upvoteComment,
        "downvoteComment": downvoteComment
    };
})();
