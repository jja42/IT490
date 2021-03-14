<?php
$date = $_POST["date"]; 
$output = shell_exec("php rabbitmqphp_example/Historical_API_Request.php $date");
echo $output;	
?>
