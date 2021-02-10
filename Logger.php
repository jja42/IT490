#!/usr/bin/php
<?php
require_once('path.inc');
require_once('get_host_info.inc');
require_once('rabbitMQLib.inc');

function myErrorHandler($errno, $errstr, $errfile, $errline)
{
    global $msg; 
	if (!(error_reporting() & $errno)) {
        // This error code is not included in error_reporting, so let it fall
        // through to the standard PHP error handler
        return false;
    }

    // $errstr may need to be escaped:
    $errstr = htmlspecialchars($errstr);

    switch ($errno) {
    case E_USER_ERROR:
        $msg = "<b>My ERROR</b> [$errno] $errstr<br />\n";
        $msg .= "  Fatal error on line $errline in file $errfile";
        $msg .= ", PHP " . PHP_VERSION . " (" . PHP_OS . ")<br />\n";
        $msg .= "Aborting...<br />\n";
        break;

    case E_USER_WARNING:
        echo "<b>My WARNING</b> [$errno] $errstr<br />\n";
        break;

    case E_USER_NOTICE:
        echo "<b>My NOTICE</b> [$errno] $errstr<br />\n";
        break;

    default:
        echo "Unknown error type: [$errno] $errstr<br />\n";
        break;
    }

    /* Don't execute PHP internal error handler */
    return true;
}

$old_error_handler = set_error_handler("myErrorHandler");
 

$client = new rabbitMQClient("testRabbitMQ.ini","testServer");
/*
if (isset($argv[1]))
{
  $msg = $argv[1];
}
else
{
  $msg = "test message";
}

 */

trigger_error("Hello World", E_USER_ERROR);

$request = array();
$request['type'] = "LogMsg";
$request['errorMessage'] = $msg;
$response = $client->send_request($request);
//$response = $client->publish($request);

echo "client received response: ".PHP_EOL;
print_r($response);
echo "\n\n";

echo $argv[0]." END".PHP_EOL;

