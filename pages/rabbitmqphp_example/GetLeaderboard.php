<?php
  require_once('path.inc');
  require_once('get_host_info.inc');
  require_once('rabbitMQLib.inc');
  require_once('Logger.php');

  $client = new rabbitMQClient("testRabbitMQ.ini", "testServer");
  
  $username = $argv[1];

  //echo "Here" . $id;

  
  $request = array();
  $request["type"] = "match_history";
  $request["id"] = $username;

  $response = $client->send_request($request);

  print_r($response);
  
?>
