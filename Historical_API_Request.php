#!/usr/bin/php
<?php
require_once('path.inc');
require_once('get_host_info.inc');
require_once('rabbitMQLib.inc');

$client = new rabbitMQClient("testRabbitMQ.ini","testServer");
$request = array();
$request['type'] = "historical_api_output";
$request['date'] = $argv[1];
$response = $client->send_request($request);
//$response = $client->publish($request);

print_r($response);
