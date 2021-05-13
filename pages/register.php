<?php
if(isset($_POST["register"]))
{
	$password = $_POST["password"];
        $confirm = $_POST["confirm"];
        $username = $_POST["username"];
	if(isset($username) && isset($password) && isset($confirm)){
	if($password !== $confirm) echo("Passwords don't match");
	else
	{
			// $hashed_password = crypt($password, "IT490WebsiteSalt");
			$hashed_password = password_hash($_POST["password"], PASSWORD_BCRYPT);
			$output = shell_exec("php rabbitmqphp_example/Register.php $username '$hashed_password'");
		}
	}

	//safety measure to prevent php warnings
	if(!isset($email)) $email = "";
	if(!isset($username)) $username = "";
}
?>
<!DOCTYPE html>
<html lang="en">
<head>
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
<title>Register</title>
<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css">
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
<script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js"></script>
<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js"></script>
</head>
<body>
<div class="register-form">
    <form method="post">
        <h2 class="text-center">Conquest of Tides</h2>
        <h2 class="text-center">Register</h2>       
        <div class="form-group">
            <input type="username" id="username" name="username" class="form-control" placeholder="Username" required="required">
        </div>
        <div class="form-group">
            <input type="password" minlength="4" id="p1" name="password" class="form-control" placeholder="Enter Password" required="required">
        </div>
        <div class="form-group">
            <input type="password" minlength="4" id="p2" name="confirm" class="form-control" placeholder="Retype Password" required="required">
        </div>
        <div class="form-group">
            <button type="submit"  name="register" value="register" class="btn btn-primary btn-block">Register</button>
        </div>
    </form>
</div>
</body>
</html>
