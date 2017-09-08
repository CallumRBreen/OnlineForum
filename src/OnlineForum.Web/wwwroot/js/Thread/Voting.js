
function Upvote(threadId, url) {
    $.post(url,
        {
            threadId: threadId
        },
        function (data) {

            ClearButtonClasses(threadId);

            if (data.didScoreIncrease) {
                $('.upvoteButton.' + threadId).removeClass('btn-primary');
                $('.upvoteButton.' + threadId).addClass('btn-success');
            }

            $('.score.' + threadId).html(data.score);
        });
};

function Downvote(threadId, url)
{
    $.post(url,
        {
            threadId: threadId
        },
        function (data) {

            ClearButtonClasses(threadId);

            if (!data.didScoreIncrease) {
                $('.downvoteButton.' + threadId).removeClass('btn-primary');
                $('.downvoteButton.' + threadId).addClass('btn-danger');
            }

            $('.score.' + threadId).html(data.score);
        });
};

function ClearButtonClasses(threadId)
{
    $('.downvoteButton.' + threadId).removeClass('btn-danger');
    $('.downvoteButton.' + threadId).addClass('btn-primary');
    $('.upvoteButton.' + threadId).removeClass('btn-success');
    $('.upvoteButton.' + threadId).addClass('btn-primary');
}
