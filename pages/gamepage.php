<?php
	session_start();
	if(isset($_POST["Logout"]))
	{
		session_unset();
		header("Location: home.php");
	}
?>
<!DOCTYPE html>
<html lang="en">
<head>
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
<title>GamePage</title>
<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css">
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
<script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js"></script>
<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js"></script>
</head>
  <body>
<div class="form">
    <form method="post">
		<h2 class="text-center">Conquest of Tides</h2>
		<div class="form-group">
      <input type="submit" name="Download" value="Download" class="btn btn-primary btn-block"></button>
       </div>
      <div class="form-group">
      <input type="submit" name="Logout" value="Logout" class="btn btn-primary btn-block"></button>
       </div>
    </form>
    </div>
    	</body>
<html>

  	<?php
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