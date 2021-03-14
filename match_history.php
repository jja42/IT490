<?php
$winner = (int)$_POST["winner"];
$loser = $_POST["loser"];
$output = shell_exec("php rabbitmqphp_example/Match_History.php $winner $loser");
echo $output;	
?>
