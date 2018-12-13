function rotateCard() {
    load_ors_uacs();
    var $card = $(".card").closest('.card-container');
    if ($card.hasClass('hover')) {
        $card.removeClass('hover');
    } else {
        $card.addClass('hover');
    }
}


