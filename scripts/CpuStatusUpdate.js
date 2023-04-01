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
            document.getElementById("cpu-temperature-label").innerText = data.temperature + " \u2103";

            const pbTemperature = document.getElementById('pb-temperature');
            const pbTemperatureValue = pbTemperature.querySelector(".pb-temperature-value");

            pbTemperatureValue.style.transform = 'rotate(' + (180 + (data.temperature * 1.8)) + 'deg)';

            if (data.temperature <= 59) {
                pbTemperatureValue.style.background = "lime";
            }
            else if (data.temperature >= 60 && data.temperature <= 85) {
                pbTemperatureValue.style.background = "orange";
            }
            else {
                pbTemperatureValue.style.background = "red";
            }
        })
        .catch(error => console.error(error));
}

function UpdateLoad() {
    fetch(url + "/api/Status/cpu-load")
        .then(response => response.json())
        .then(data => {
            document.getElementById("cpu-load-label").innerText = data.load + "%";

            const pbLoad = document.getElementById('pb-load');
            const pbLoadValue = pbLoad.querySelector(".pb-load-value");

            pbLoadValue.style.transform = 'rotate(' + (180 + (data.load * 1.8)) + 'deg)';

            if (data.load <= 59) {
                pbLoadValue.style.background = "lime";
            }
            else if (data.load >= 60 && data.load <= 85) {
                pbLoadValue.style.background = "orange";
            }
            else {
                pbLoadValue.style.background = "red";
            }
        })
        .catch(error => console.error(error));
}

function UpdateSpeed() {
    fetch(url + "/api/Status/cpu-speed")
        .then(response => response.json())
        .then(data => {
            document.getElementById("cpu-speed-label").innerText = data.speed + " GHz";

            const pbSpeed = document.getElementById('pb-speed');
            const pbSpeedValue = pbSpeed.querySelector(".pb-speed-value");

            pbSpeedValue.style.transform = 'rotate(' + (180 + (data.speed * 20 * 1.8)) + 'deg)';

            if (data.speed <= 2.8) {
                pbSpeedValue.style.background = "lime";
            }
            else if (data.speed > 2.8 && data.speed <= 3.5) {
                pbSpeedValue.style.background = "orange";
            }
            else {
                pbSpeedValue.style.background = "red";
            }
        })
        .catch(error => console.error(error));
}