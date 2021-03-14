<?php

$string = file_get_contents("WeatherAPI.json");
$json_mainstr = json_decode($string, true);

$startdate = "01-01-1979";
$enddate = $argv[1];

$arraynumreturned = datediff($startdate, $enddate);
$arraynumreturned += 12;

echo $json_mainstr[$arraynumreturned]['dt_iso'];
echo "\n";
echo $json_mainstr[$arraynumreturned]['main']['temp'];
echo "\n";
echo $json_mainstr[$arraynumreturned]['weather'][0]['main'];
echo "\n";
echo $json_mainstr[$arraynumreturned]['main']['humidity'];
echo "\n";
echo $json_mainstr[$arraynumreturned]['wind']['speed'];
echo "\n";
echo $json_mainstr[$arraynumreturned]['wind']['deg'];


function datediff($date1, $date2)
{
	$diff = strtotime($date2)-strtotime($date1);
        return abs(round($diff/3600));	
}



?>









