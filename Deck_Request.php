#!/usr/bin/php
<?php
require_once('path.inc');
require_once('get_host_info.inc');
require_once('rabbitMQLib.inc');
require_once('Logger.php');

$client = new rabbitMQClient("testRabbitMQ.ini","testServer");
if (isset($argv[1]))
{
  $msg = $argv[1];
}
else
{
  $msg = "test message";
}

$request = array();
$request['type'] = "deck_output";
$request['user'] = (int)$argv[1];
$request['deck_name'] = $argv[2];
$response = $client->send_request($request);
//$response = $client->publish($request);

print_r($response);
