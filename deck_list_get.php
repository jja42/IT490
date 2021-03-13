<?php
$user = (int)$_POST["user"];
$output = shell_exec("php rabbitmqphp_example/Deck_List_Request.php $user");
echo $output;	
?>
