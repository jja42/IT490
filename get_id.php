<?php
$username = $_POST["username"];
$password = $_POST["password"];
$output = shell_exec("php rabbitmqphp_example/ID_Request.php $username $password");
echo $output;	
?>
