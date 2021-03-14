Download
<?php
	session_start();
	if(isset($_POST["Logout"]))
	{
		session_unset();
		header("Location: home.php");
	}
?>
<html>
  <body>
    <form method="POST">
      <input type="submit" name="Download" value="download"/>
      <input type="submit" name="Logout" value="logout"/>
    </form>
  	<?php
  		session_start();
			$username = $_SESSION["username"];
  		$output = shell_exec("php rabbitmqphp_example/GetLeaderboard.php $username");
  		//parse_str($output, $op);
  		//print_r($op);
  		$op = explode(" ", $output);
		//echo $output;
$table = "<table>";
$table .=  "<tr><th>Game</th><th>Winner</th><th>Loser</th></tr>";
$a = 0;
for($i = 0; $i < count($op) - 1; $i+=2){
	$t = $i + 1;
	$a++;
	$table .= "<tr><td>$a</td><td>$op[$i]</td><td>$op[$t]</td></tr>";
}
$table .= "</table>";
echo $table;
  	?>
	</body>
<html>
