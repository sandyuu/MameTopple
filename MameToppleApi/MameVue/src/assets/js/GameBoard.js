'use strict';
//#region signalR

var connection = new signalR.HubConnectionBuilder()
    .withUrl('http://localhost:5000/mamehub', {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
    })
    .build();
let _dollsTower = null;
connection
    .start()
    .then(() => {
        connection.invoke('GetDollsTower');

        connection.on('GetDollsTower', function(dollsTower) {
            console.log(`${JSON.stringify(dollsTower)}`);
            _dollsTower = dollsTower;
            Binding();
            // dollsTower = JSON.stringify(dollsTower);
        });
    })
    .catch(function(err) {
        console.error(err.toString());
    });
//#endregion signalR
// console.log(`${JSON.stringify(_dollsTower)}`);

var DollsTowerVM;
function Binding() {
    DollsTowerVM = new Vue({
        el: '.mame-line',
        data: {
            items: _dollsTower,
        },
        methods: {
            doDelete: function() {},
        },
    });
}

$(document).ready(function() {});

//#region 點擊翻轉卡片
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
//#endregion 點擊翻轉卡片
