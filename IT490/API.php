#!/usr/bin/php
<?php

require_once('path.inc');
require_once('get_host_info.inc');
require_once('rabbitMQLib.inc');

$client = new rabbitMQClient("testRabbitMQ.ini","testServer");

$env_path = "/home/it490/git/IT490/.env";

// API Section:
$myfile = fopen($env_path, "r") or die("Unable to open file!");
$key = fread($myfile, filesize($env_path));
fclose($myfile);
$key = trim(preg_replace('/\s\s+/', '', $key));

$location = "Newark,NewJersey";
$units = "imperial";
$base_url = "https://api.openweathermap.org/data/2.5/weather";
$url = $base_url . '?' . "q=" . $location . '&' . "units=" . $units . '&' . "appid=" . $key;
$c = curl_init($url);
curl_setopt($c, CURLOPT_RETURNTRANSFER, 1);

// Main variable that stores the page content:
$page = curl_exec($c);
curl_close($c);

// Sending the page to the Server:

$obj = json_decode($page, true);

$weather = array(
    "temp" => $obj["main"]["temp"],
    "humidity" => $obj["main"]["humidity"],
    "weather_type" => $obj["weather"][0]["main"],
    "visibility" => $obj["visibility"],
    "wind_speed" => $obj["wind"]["speed"],
    "wind_direction" => $obj["wind"]["deg"]
);

$send_data = array("type" => "api_insert", "data" => $weather);

print_r($weather);

$response = $client->send_request($send_data);

echo "client received response: ".PHP_EOL;
print_r($response);
echo "\n\n";

echo $argv[0]." END".PHP_EOL;

?>
