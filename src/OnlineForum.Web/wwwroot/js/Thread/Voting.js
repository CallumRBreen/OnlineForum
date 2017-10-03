


var threadVoting = (function() {

    function upvoteThread(threadId, url) {
        $.post(url,
            {
                threadId: threadId
            },
            function (data) {

                clearButtonClasses(threadId);

                if (data.didScoreIncrease) {
                    $('.threadUpvoteButton.' + threadId).removeClass('btn-primary');
                    $('.threadUpvoteButton.' + threadId).addClass('btn-success');
                }

                $('.threadScore.' + threadId).html(data.score);
            });
    }

    function downvoteThread(threadId, url) {
        $.post(url,
            {
                threadId: threadId
            },
            function (data) {

                clearButtonClasses(threadId);

                if (!data.didScoreIncrease) {
                    $('.threadDownvoteButton.' + threadId).removeClass('btn-primary');
                    $('.threadDownvoteButton.' + threadId).addClass('btn-danger');
                }

                $('.threadScore.' + threadId).html(data.score);
            });
    }

    function clearButtonClasses(threadId) {
        $('.threadDownvoteButton.' + threadId).removeClass('btn-danger');
        $('.threadDownvoteButton.' + threadId).addClass('btn-primary');
        $('.threadUpvoteButton.' + threadId).removeClass('btn-success');
        $('.threadUpvoteButton.' + threadId).addClass('btn-primary');
    }

    return {
        "upvoteThread": upvoteThread,
        "downvoteThread": downvoteThread
    };

})();



