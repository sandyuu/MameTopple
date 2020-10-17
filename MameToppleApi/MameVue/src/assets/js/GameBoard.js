let cardTransitionTime = 600;

// let $card = document.querySelector('.mame-card');
let $card = $('.mame-card');
let switching = false;

$('.mame-card-wrapper').click(flipCard);

// document.querySelector('.btn').addEventListener('click', flipCard);
// flipCard();
function flipCard() {
    if (switching) {
        return false;
    }
    switching = true;

    $card.toggleClass('is-switched');
    window.setTimeout(function() {
        $card
            .children()
            .children()
            .toggleClass('is-active');
        switching = false;
    }, cardTransitionTime / 2);
}
