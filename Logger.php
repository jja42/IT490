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
        return logMsg($msg);

    case E_USER_WARNING:
        $msg = "<b>My WARNING</b> [$errno] $errstr<br />\n";
        return logMsg($msg);

    case E_USER_NOTICE:
        $msg = "<b>My NOTICE</b> [$errno] $errstr<br />\n";
        return logMsg($msg);

    default:
        $msg = "Unknown error type: [$errno] $errstr<br />\n";
        return logMsg($msg);
    }

    /* Don't execute PHP internal error handler */
    return true;
}

$old_error_handler = set_error_handler("myErrorHandler");
 
function logMsg($msg){
$client = new rabbitMQClient("testRabbitMQ.ini","testServer");

$request = array();
$request['type'] = "LogMsg";
$request['errorMessage'] = $msg;
$response = $client->send_request($request);
print_r($response);
}
