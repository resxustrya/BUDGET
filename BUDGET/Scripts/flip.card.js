function rotateCard() {
    var $card = $(".card").closest('.card-container');
    if ($card.hasClass('hover')) {
        $card.removeClass('hover');
    } else {
        $card.addClass('hover');
    }
}


