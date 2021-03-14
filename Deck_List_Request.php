#!/usr/bin/php
<?php
require_once('path.inc');
require_once('get_host_info.inc');
require_once('rabbitMQLib.inc');
require_once('Logger.php');

$client = new rabbitMQClient("testRabbitMQ.ini","testServer");
$request = array();
$request['type'] = "deck_list_output";
$request['user'] = (int)$argv[1];
$response = $client->send_request($request);
//$response = $client->publish($request);

print_r($response);
