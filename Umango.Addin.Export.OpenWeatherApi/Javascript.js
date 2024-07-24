function InitializeSettings(Settings) {

    $('#city_selection_input').val(Settings.CitySelection);

    // We can attach this function to a button and have it called on click
    // The $.get will hit the Umango server at the /dashboard/json/connector/query/<GUID> path
    // We can catch this request inside our settings manager class and do some things with the data sent in the url
    function GetWeather() {

        var citySelection = $('#city_selection_input').val();

        $.get("/dashboard/json/connector/query/e6808a54-cfba-4a42-aec4-d4fe405955b4/get_weather?city=" + citySelection, function (data) {

            if (data != null && data.exists == false) {
                // Do something with data...
            }
        });
    }
}

function ValidateSettings() {

    return true;

}

function UpdateSettings(Settings) {

    Settings.CitySelection = $('#city_selection_input').val();

    return Settings;

}