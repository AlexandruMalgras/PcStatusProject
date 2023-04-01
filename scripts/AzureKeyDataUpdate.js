function UpdateMaxTemperature() {
    fetch(url + "/api/AzureKeyData/max-cpu-temperature")
        .then(response => response.json())
        .then(data => {
            document.getElementById("max-temperature-label").innerText = data.maxCpuTemperature + " \u2103";
        })
        .catch(error => console.error(error));
}

function UpdateMaxLoad() {
    fetch(url + "/api/AzureKeyData/max-cpu-load")
        .then(response => response.json())
        .then(data => {
            document.getElementById("max-load-label").innerText = data.maxCpuLoad + "%";
        })
        .catch(error => console.error(error));
}

function UpdateMaxSpeed() {
    fetch(url + "/api/AzureKeyData/max-cpu-speed")
        .then(response => response.json())
        .then(data => {
            document.getElementById("max-speed-label").innerText = data.maxCpuSpeed + " GHz";
        })
        .catch(error => console.error(error));
}