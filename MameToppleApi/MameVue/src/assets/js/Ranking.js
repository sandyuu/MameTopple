import signalR from '../@microsoft/signalr';
// const signalR = require('@microsoft/signalr');
console.log('signalR');

var connection = new signalR.HubConnectionBuilder()
    .withUrl('http://localhost:5000/mamehub', {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
    })
    .build();
let _missionDolls = null;
connection
    .start()
    .then(() => {
        connection.invoke('GetMission');

        connection.on('GetMission', function(missionDolls) {
            console.log(`${JSON.stringify(missionDolls)}`);
            _missionDolls = missionDolls;
            Binding();
            // dollsTower = JSON.stringify(dollsTower);
        });
    })
    .catch(function(err) {
        console.error(err.toString());
    });
//#endregion signalR
// console.log(`${JSON.stringify(_dollsTower)}`);

var MissionDollsVM;
function Binding() {
    MissionDollsVM = new Vue({
        el: '.mission-card',
        data: {
            items: _missionDolls,
        },
    });
}
