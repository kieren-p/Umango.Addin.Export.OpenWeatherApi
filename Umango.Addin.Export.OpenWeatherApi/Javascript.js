function InitializeSettings(Settings) {

    $('#city_input').val(Settings.CitySelection);

    // We can attach this function to a button and have it called on click
    // The $.get will hit the Umango server at the /dashboard/json/connector/query/<GUID> path
    // We can catch this request inside our settings manager class and do some things with the data sent in the url
    function GetWeather() {

        var citySelection = $('#city_input').val();

        $.get("/dashboard/json/connector/query/e6808a54-cfba-4a42-aec4-d4fe405955b4/get_weather?city=" + citySelection, function (data) {
            // Take a peek at what the data looks like
            console.log(data);

            if (data.succeeded == false) {
                $('#city_help_text').text(`Something went wrong! Message ---> ${data.message}`);
            } else {
                $('#city_help_text').text(`Current weather in ${data.name}: ${data.weather[0].description}.`);
            }
        });
    }

    $('#btn_city_input').on('click', GetWeather);
}

function ValidateSettings() {

    return true;
}

function UpdateSettings(Settings) {

    // Save our values in the SettingsClass.cs and log them
    Settings.CitySelection = $('#city_input').val();

    console.log(Settings);
    return Settings;
}