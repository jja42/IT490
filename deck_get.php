<?php
$user = (int)$_POST["user"];
$deck_name = $_POST["deck_name"];
$output = shell_exec("php rabbitmqphp_example/Deck_Request.php $user $deck_name");
echo $output;	
?>
