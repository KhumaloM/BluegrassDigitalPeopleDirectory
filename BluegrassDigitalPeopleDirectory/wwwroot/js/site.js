$(document).ready(function () {
    $(function () {
        var apiurl = "/api/People/GetPeople"
        var peopleDirectoryNames = [];
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: apiurl,
            success: function (result) {
                for (var i = 0; i < result.length; i++) {
                    peopleDirectoryNames.push(result[i].name + " " + result[i].surname);
                }
            }
        });

        $("#txtsearch").autocomplete({
            source: peopleDirectoryNames
        });
    });
    //search button logic in home page
    $("#btnSearch").click(function () {
        var searchBoxValue = $("#txtsearch").val().toLowerCase();
        var selectedCountry = $("#countryfilter option:selected").text().toLowerCase();
        var selectedCity = $("#cityfilter option:selected").text().toLowerCase();

        var selectedCityValue = $("#cityfilter option:selected").val().toLowerCase();
        var selectedCountryValue = $("#countryfilter option:selected").val().toLowerCase();

        $("#searchresults tr").filter(function () {
            var gridValue = $(this).find("td:eq(0)").text() + " " + $(this).find("td:eq(1)").text();
            if (selectedCountryValue != -1) {
                gridValue = gridValue + " " + $(this).find("td:eq(3)").text();
            }
            if (selectedCityValue != -1) {
                gridValue = gridValue + " " + $(this).find("td:eq(4)").text();
            }
            $(this).toggle(gridValue.toLowerCase().indexOf(searchBoxValue) > -1);
        });
    });
    //country filter in home page
    $("#countryfilter").change(function () {
        var selectedCountry = $("#countryfilter option:selected").text().toLowerCase();

        var selectedCityValue = $("#cityfilter option:selected").val().toLowerCase();
        var selectedCity = $("#cityfilter option:selected").text().toLowerCase();
        if (selectedCityValue != -1) {
            selectedCountry = selectedCountry + " " + selectedCity;
        }
        $("#searchresults tr").filter(function () {
            var gridValue = $(this).find("td:eq(3)").text();
            if (selectedCityValue != -1) {
                gridValue = gridValue + " " + $(this).find("td:eq(4)").text().toLowerCase();
            }
            $(this).toggle(gridValue.toLowerCase().indexOf(selectedCountry) > -1);
        });
    });
    //city filter in homepage
    $("#cityfilter").change(function () {
        var selectedCity = $("#cityfilter option:selected").text().toLowerCase();

        var selectedCountryValue = $("#countryfilter option:selected").val().toLowerCase();
        var selectedCountry = $("#countryfilter option:selected").text().toLowerCase();
        if (selectedCountryValue != -1) {
            selectedCity = selectedCountry + " " + selectedCity;
        }
        $("#searchresults tr").filter(function () {
            var gridValue = $(this).find("td:eq(4)").text();
            if (selectedCountryValue != -1) {
                gridValue = $(this).find("td:eq(3)").text().toLowerCase() + " " + gridValue;
            }
            $(this).toggle(gridValue.toLowerCase().indexOf(selectedCity) > -1);
        });
    });

    //populate the city dropdown when country is selected
    $("#CountryId").change(function () {
        var CountryId = parseInt($(this).val());
        if (!isNaN(CountryId)) {
            $.ajax({
                url: '/api/People/GetCitiesByCountryId',
                type: 'GET',
                dataType: 'json',
                data: { CountryId: CountryId },
                success: function (d) {
                    var CityId = $('#CityId');
                    CityId.empty();
                    $.each(d, function (i, city) {
                        CityId.append($("<option></option>").val(city.id).html(city.name));
                    });
                },
                error: function () {
                    alert('Error!');
                }
            });
        }
        else {
            var CityId = $('#CityId');
            CityId.empty();
        }
    });
});