Conquest of Tides
<?php
if(isset($_POST["login"]))
{
	$password = null;
	$username = null;
	if(isset($_POST["password"]) && isset($_POST["username"]))
	{
		$password = $_POST["password"];
		$username = $_POST["username"];
	}
	// send username and password to server
	$hash = password_hash($password, PASSWORD_BCRYPT);
	$output = shell_exec("php rabbitmqphp_example/Login.php $username $password");
	echo $output;
}
?>
<form method="POST">
	<label for="username">Username:</label>
	<input type="username" id="username" name="username" required/>
	<label for="password">Password:</label>
	<input type="password" id="password" name="password" required/>
	<input type="submit" name="login" value="login"/>
	<a href="register.php">Register Now</a>
</form>
