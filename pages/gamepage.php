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
  		$output = shell_exec("/usr/bin/php rabbitmqphp_example/GetLeaderboard.php $username");
		echo $output;
$table = "<table>";
$table .=  "<tr><th>Game</th><th>Winner</th><th>Loser</th></tr>";
for($i = 0; $i < count($output); $i++){
	$t = $i + 1;
	$table .= "<tr><td>$t</td><td>$output[$i][winner]</td><td>$output[$i][loser]</td></tr>";
}
$table .= "</table>";
echo $table;
  	?>
	</body>
<html>
