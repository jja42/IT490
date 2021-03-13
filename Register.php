#!/usr/bin/php
<?php
require_once('path.inc');
require_once('get_host_info.inc');
require_once('rabbitMQLib.inc');

$client = new rabbitMQClient("testRabbitMQ.ini","testServer");

$request = array();
$request["type"] = "register";
$request["username"] = $argv[1];
$request["password"] = $argv[2];

$response = $client->send_request($request);

print_r($response);