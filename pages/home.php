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
	$output = shell_exec("php rabbitmqphp_example/Login.php $username $hash");
	echo $output;
}
?>
<!DOCTYPE html>
<html lang="en">
<head>
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1">
<title>HomePage</title>
<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css">
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
<script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js"></script>
<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js"></script>
</head>
<body>
<div class="login-form">
    <form method="post">
        <h2 class="text-center">Conquest of Tides</h2>
        <h2 class="text-center">Log in</h2>       
        <div class="form-group">
            <input type="username" id="username" name="username" class="form-control" placeholder="Username" required="required">
        </div>
        <div class="form-group">
            <input type="password" id="password" name="password" class="form-control" placeholder="Password" required="required">
        </div>
        <div class="form-group">
            <button type="submit"  name="login" value="login" class="btn btn-primary btn-block">Log In</button>
        </div>
    </form>
    <p class="text-center"><a href="register.php">Register</a></p>
</div>
</body>
</html>
