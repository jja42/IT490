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
			$id = $_SESSION["id"];
  		$output = shell_exec("php rabbitmqphp_example/GetLeaderboard.php $id");
			echo $output;
  	?>
	</body>
<html>
