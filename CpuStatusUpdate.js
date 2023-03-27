const url = "http://localhost:5000";

function UpdateName() {
    fetch(url + "/api/Status/cpu-name")
        .then(response => response.json())
        .then(data => {
            document.getElementById("cpu-name-label").innerText = data.name;
        })
        .catch(error => console.error(error));
}

function UpdateTemperature() {
    fetch(url + "/api/Status/cpu-temperature")
        .then(response => response.json())
        .then(data => {
            document.getElementById("cpu-temperature-label").innerText = data.temperature;
        })
        .catch(error => console.error(error));
}

function UpdateLoad() {
    fetch(url + "/api/Status/cpu-load")
        .then(response => response.json())
        .then(data => {
            document.getElementById("cpu-load-label").innerText = data.load + "%";
        })
        .catch(error => console.error(error));
}

function UpdateSpeed() {
    fetch(url + "/api/Status/cpu-speed")
        .then(response => response.json())
        .then(data => {
            document.getElementById("cpu-speed-label").innerText = data.speed;
        })
        .catch(error => console.error(error));
}