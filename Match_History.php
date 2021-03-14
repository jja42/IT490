#!/usr/bin/php
<?php
require_once('path.inc');
require_once('get_host_info.inc');
require_once('rabbitMQLib.inc');
require_once('Logger.php');

$client = new rabbitMQClient("testRabbitMQ.ini","testServer");
$request = array();
$request['type'] = "match_input";
$request['winner'] = $argv[1];
$request['loser'] = $argv[2];
$response = $client->send_request($request);
//$response = $client->publish($request);

print_r($response);
