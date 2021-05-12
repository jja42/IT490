<?php
$username = $_POST["username"];
$password = $_POST["password"];
$hashed_password = password_hash($password, PASSWORD_BCRYPT)
$output = shell_exec("php rabbitmqphp_example/ID_Request.php $username $hashed_password");
echo $output;	
?>
