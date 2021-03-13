<?php
if(isset($username) && isset($password) && isset($confirm)){
	$password = $_POST["password"];
	$confirm = $_POST["confirm"];
	$username = $_POST["username"];
	$isValid = true;
	if($password !== $confirm) echo("Passwords don't match");
	else{
	    //TODO other validation as desired, remember this is the last line of defense
		$hash = password_hash($password, PASSWORD_BCRYPT);
		$output = shell_exec("php rabbitmqphp_example/Register.php $username $password");
		echo $output;
	}
}

//safety measure to prevent php warnings
if(!isset($email)) $email = "";
if(!isset($username)) $username = "";

?>

<form method="POST">
    <label for="user">Username:</label>
    <input type="text" maxlength="60" id="user" name="username" placeholder="Enter Username" value="<?php echo($username); ?>" required/>
    <label for="p1">Password:</label>
    <input type="password" minlength="4" id="p1" name="password" placeholder="Enter Password" required/>
    <label for="p2">Confirm Password:</label>
    <input type="password" minlength="4" id="p2" name="confirm" placeholder="Retype Password" required/>
    <input type="submit" name="register" value="Register"/>
</form>