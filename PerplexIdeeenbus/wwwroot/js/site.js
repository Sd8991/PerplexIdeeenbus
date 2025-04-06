const uri = '/api/ideas';
let ideas = [];

function getItems(filter) {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayItems(data, filter))
        .catch(error => console.error('Unable to get items.', error));
}

function _displayItems(data, filter) {
    const tBody = document.getElementById('ideas');
    tBody.innerHTML = '';

    data.sort((a, b) => _compareId(a, b));

    data.forEach(item => {

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let textNode1 = document.createTextNode(item.onderwerp);
        td1.appendChild(textNode1);

        let td2 = tr.insertCell(1);
        let textNode2 = document.createTextNode(item.beschrijving);
        td2.appendChild(textNode2);

        let td3 = tr.insertCell(2);
        let textNode3 = document.createTextNode(item.userId);
        td3.appendChild(textNode3);

        let td4 = tr.insertCell(3);
        let textNode4 = document.createTextNode(item.userName);
        td4.appendChild(textNode4);

        let td5 = tr.insertCell(4);
        let textNode5 = document.createTextNode(item.type);
        td5.appendChild(textNode5);

        let td6 = tr.insertCell(5);
        let textNode6 = document.createTextNode(item.beginDatum);
        td6.appendChild(textNode6);

        let td7 = tr.insertCell(6);
        let textNode7 = document.createTextNode(item.eindDatum);
        td7.appendChild(textNode7);

        let td8 = tr.insertCell(7);
        let textNode8 = document.createTextNode(item.duur);
        td8.appendChild(textNode8);

        let td9 = tr.insertCell(8);
        let textNode9 = document.createTextNode(item.categories);
        td9.appendChild(textNode9);
    });

    ideas = data;
}


function _compareId(a, b) {
    var aId = new Date(a.id);
    var bId = new Date(b.id);

    return bId - aId;
}


function _compareDate(a, b) {
    var aDate = new Date(a.creationTime);
    var bDate = new Date(b.creationTime);

    return bDate - aDate;
}