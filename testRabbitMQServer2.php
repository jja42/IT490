#!/usr/bin/php
<?php
require_once('path.inc');
require_once('get_host_info.inc');
require_once('rabbitMQLib.inc');

$servername="localhost";
$username="root";
$password="Passss1!";
$dbname="gamedb";


function doLogin($username,$password)
{
    // lookup username in databas
    // check password
    return true;
    //return false if not valid
}

function requestProcessor($request)
{
  echo "received request".PHP_EOL;
  var_dump($request);
  if(!isset($request['type']))
  
  {
    echo "null.";
    return "ERROR: unsupported message type";
  }
  switch ($request['type'])
  {
  case "api_insert":
	  $request = $request['data'];
	  return api_insert($request['weather_type'], $request['temp'], $request['humidity'], $request['visibility'], $request['wind_speed'], $request['wind_direction']);
  case "api_output":
	  return api_output();
  case "inventory_output":
	  return inventory_output($request['user']);
  case "deck_output":
	  return deck_output($request['deck_name'], $request['user']); 
  case "deck_list_output":
	  return deck_list_output($request['user']);
  case "historical_api_output":
	  return historical_api_output($request['date']);
  case "LogMsg":
	  return LogMessage($request['errorMessage']);
  case "register":
	  return user_inserts($request['username'], $request['password']);
  case "login":
	  return user_output($request['username'], $request['password']);	 
  case "match_input":
	  return match_inserts($request['winner'], $request['loser']);
  case "match_history":
	  return match_output($request['id']);
  case "deck_input":
	  return deck_input($request['user'], $request['deck_name'], $request['card_id_1'], $request['card_id_2'], $request['card_id_3'], $request['card_id_4'], $request['card_id_5'], $request['card_id_6'], $request['card_id_7'], $request['card_id_8'], $request['card_id_9'], $request['card_id_10'], $request['card_id_11'], $request['card_id_12'], $request['card_id_13'], $request['card_id_14'], $request['card_id_15'], $request['card_id_16'], $request['card_id_17'], $request['card_id_18'], $request['card_id_19'], $request['card_id_20'], $request['card_id_21'], $request['card_id_22'], $request['card_id_23'], $request['card_id_24'], $request['card_id_25'], $request['card_id_26'], $request['card_id_27'], $request['card_id_28'], $request['card_id_29'], $request['card_id_30'], $request['card_id_31'], $request['card_id_32'], $request['card_id_33'], $request['card_id_34'], $request['card_id_35'], $request['card_id_36'], $request['card_id_37'], $request['card_id_38'], $request['card_id_39'], $request['card_id_40'], $request['card_id_41'], $request['card_id_42'], $request['card_id_43'], $request['card_id_44'], $request['card_id_45'], $request['card_id_46'], $request['card_id_47'], $request['card_id_48'], $request['card_id_49'], $request['card_id_50'], $request['card_id_51'], $request['card_id_52'], $request['card_id_53'], $request['card_id_54'], $request['card_id_55'], $request['card_id_56'], $request['card_id_57'], $request['card_id_58'], $request['card_id_59'], $request['card_id_60']);	  
  }	
	  return array("returnCode" => '0', 'message'=>"Server received request and processed");

}

function api_insert($weather_type, $temp, $humidity, $visibility, $wind_speed, $wind_direction)
{

global $servername, $dbname, $username, $password;
//$weather_type = strval($weather_type);

$sql = "INSERT INTO cached_api_data (weather_type, temp, humidity, visibility, wind_speed, wind_direction) VALUES ('$weather_type', $temp, $humidity, $visibility, $wind_speed, $wind_direction)";
  
try {
  $conn = new PDO("mysql:host=".$servername.";dbname=".$dbname, $username, $password);
  // set the PDO error mode to exception
  $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
  // use exec() because no results are returned
  $conn->exec($sql);
  echo "New record created successfully";
} catch(PDOException $e) {
  echo $sql ."<br>" . $e->getMessage();
}


$conn = null;
}

//outputs are select statements 
function api_output()
{
	global $servername, $dbname, $username, $password;
	try {
  $conn = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
  // set the PDO error mode to exception
  $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
  $stmt = $conn->prepare("SELECT weather_type, temp, humidity, visibility, wind_speed, wind_direction FROM cached_api_data");
  $stmt->execute();

  $result = $stmt->fetch(PDO::FETCH_ASSOC);
  return $result;

} catch(PDOException $e) {
  echo "Error: " .$e->getMessage();
}

$conn = null;

}

//inserts are insert statements, see example above
function inventory_inserts($player_id, $card_id)
{

global $servername, $dbname, $username, $password;

$sql = "INSERT INTO player_inventory (player_id, card_id) VALUES ($player_id, $card_id)";


try {
  $conn = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
  // set the PDO error mode to exception
  $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

  // use exec() because no results are returned
  $conn->exec($sql);
  echo "New record created successfully";
} catch(PDOException $e) {
  echo $sql . "<br>" . $e->getMessage();
}

$conn = null;


}

function inventory_output($user_id)
{
global $servername, $dbname, $username, $password;

	try {
  $conn = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
  // set the PDO error mode to exception
  $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
  $stmt = $conn->prepare("SELECT card_id FROM player_inventory WHERE player_id = $user_id");
  $stmt->execute();

  $result = $stmt->fetch(PDO::FETCH_ASSOC);
  return $result;
} catch(PDOException $e) {
  echo "Error: " .$e->getMessage();
}

$conn = null;

}

function user_inserts($user, $pass)
{

global $servername, $dbname, $username, $password;

$sql = "INSERT INTO users (password, username) VALUES ('$pass', '$user')";
	try {
  $conn = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
  // set the PDO error mode to exception
  $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

  // use exec() because no results are returned
  $conn->exec($sql);
  echo "New record created successfully";
} catch(PDOException $e) {
  echo $sql . "<br>" . $e->getMessage();
}

$conn = null;
}

function user_output($user, $pass)
{
	global $servername, $dbname, $username, $password;
	try {
  $conn = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
  // set the PDO error mode to exception
  $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
  $stmt = $conn->prepare("SELECT password FROM users WHERE username = '$user'");
  $stmt->execute();

  $result = $stmt->fetch(PDO::FETCH_ASSOC);
  $hashed_pass = $result["password"];

  if(password_verify($pass, $hashed_pass))
  {
	  echo "here";
	  $stmt = $conn->prepare("SELECT id FROM users WHERE username = '$user'");
	$stmt->execute();
	$result = $stmt->fetch(PDO::FETCH_ASSOC);

  }
  return $result;
} catch(PDOException $e) {
  echo "Error: " .$e->getMessage();
}

$conn = null;

}

function deck_input($user_id, $deck_name, $card_id_1, $card_id_2, $card_id_3, $card_id_4, $card_id_5, $card_id_6, $card_id_7, $card_id_8, $card_id_9, $card_id_10, $card_id_11, $card_id_12, $card_id_13, $card_id_14, $card_id_15, $card_id_16, $card_id_17, $card_id_18, $card_id_19, $card_id_20, $card_id_21, $card_id_22, $card_id_23, $card_id_24, $card_id_25, $card_id_26, $card_id_27, $card_id_28, $card_id_29, $card_id_30, $card_id_31, $card_id_32, $card_id_33, $card_id_34, $card_id_35, $card_id_36, $card_id_37, $card_id_38, $card_id_39, $card_id_40, $card_id_41, $card_id_42, $card_id_43, $card_id_44, $card_id_45, $card_id_46, $card_id_47, $card_id_48, $card_id_49, $card_id_50, $card_id_51, $card_id_52, $card_id_53, $card_id_54, $card_id_55, $card_id_56, $card_id_57, $card_id_58, $card_id_59, $card_id_60)
{

global $servername, $dbname, $username, $password;
$sql = "INSERT INTO decks (user_id, deck_name, card_id_1, card_id_2, card_id_3, card_id_4, card_id_5, card_id_6, card_id_7, card_id_8, card_id_9, card_id_10, card_id_11, card_id_12, card_id_13, card_id_14, card_id_15, card_id_16, card_id_17, card_id_18, card_id_19, card_id_20, card_id_21, card_id_22, card_id_23, card_id_24, card_id_25, card_id_26, card_id_27, card_id_28, card_id_29, card_id_30, card_id_31, card_id_32, card_id_33, card_id_34, card_id_35, card_id_36, card_id_37, card_id_38, card_id_39, card_id_40, card_id_41, card_id_42, card_id_43, card_id_44, card_id_45, card_id_46, card_id_47, card_id_48, card_id_49, card_id_50, card_id_51, card_id_52, card_id_53, card_id_54, card_id_55, card_id_56, card_id_57, card_id_58, card_id_59, card_id_60) VALUES ($user_id, '$deck_name', $card_id_1, $card_id_2, $card_id_3, $card_id_4, $card_id_5, $card_id_6, $card_id_7, $card_id_8, $card_id_9, $card_id_10, $card_id_11, $card_id_12, $card_id_13, $card_id_14, $card_id_15, $card_id_16, $card_id_17, $card_id_18, $card_id_19, $card_id_20, $card_id_21, $card_id_22, $card_id_23, $card_id_24, $card_id_25, $card_id_26, $card_id_27, $card_id_28, $card_id_29, $card_id_30, $card_id_31, $card_id_32, $card_id_33, $card_id_34, $card_id_35, $card_id_36, $card_id_37, $card_id_38, $card_id_39, $card_id_40, $card_id_41, $card_id_42, $card_id_43, $card_id_44, $card_id_45, $card_id_46, $card_id_47, $card_id_48, $card_id_49, $card_id_50, $card_id_51, $card_id_52, $card_id_53, $card_id_54, $card_id_55, $card_id_56, $card_id_57, $card_id_58, $card_id_59, $card_id_60)";
	try {
  $conn = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
  // set the PDO error mode to exception
  $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
  
  // use exec() because no results are returned
  $conn->exec($sql);
  echo "New record created successfully";
} catch(PDOException $e) {
  echo $sql . "<br>" . $e->getMessage();
}

$conn = null;
}

function deck_output($deck_name, $user_id)
{
	global $servername, $dbname, $username, $password;
	try {
  $conn = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
  // set the PDO error mode to exception
  $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
  $stmt = $conn->prepare("SELECT user_id, deck_name, card_id_1, card_id_2, card_id_3, card_id_4, card_id_5, card_id_6, card_id_7, card_id_8, card_id_9, card_id_10, card_id_11, card_id_12, card_id_13, card_id_14, card_id_15, card_id_16, card_id_17, card_id_18, card_id_19, card_id_20, card_id_21, card_id_22, card_id_23, card_id_24, card_id_25, card_id_26, card_id_27, card_id_28, card_id_29, card_id_30, card_id_31, card_id_32, card_id_33, card_id_34, card_id_35, card_id_36, card_id_37, card_id_38, card_id_39, card_id_40, card_id_41, card_id_42, card_id_43, card_id_44, card_id_45, card_id_46, card_id_47, card_id_48, card_id_49, card_id_50, card_id_51, card_id_52, card_id_53, card_id_54, card_id_55, card_id_56, card_id_57, card_id_58, card_id_59, card_id_60 FROM decks WHERE user_id = $user_id and deck_name = '$deck_name'");
  $stmt->execute();

  $result = $stmt->fetch(PDO::FETCH_ASSOC);
  return $result;
} catch(PDOException $e) {
  echo "Error: " .$e->getMessage();
}

$conn = null;

}

function match_inserts($winner, $loser)
{

global $servername, $dbname, $username, $password;
$sql = "INSERT INTO match_history (winner, loser) VALUES ($winner, $loser)";

	try {
  $conn = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
  // set the PDO error mode to exception
  $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
  
  // use exec() because no results are returned
  $conn->exec($sql);
  echo "New record created successfully";
} catch(PDOException $e) {
  echo $sql . "<br>" . $e->getMessage();
}

$conn = null;
}

function match_output($id)
{
	global $servername, $dbname, $username, $password;
 try {
  $conn = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
  // set the PDO error mode to exception
  $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
  $stmt = $conn->prepare("SELECT winner, loser FROM match_history WHERE winner = $id OR loser = $id");
  $stmt->execute();

  $result = $stmt->fetchAll(PDO::FETCH_ASSOC);
  //return $result;
  $op = "";
  for($i = 0; $i < count($result); $i++)
  {
	  $op .= $result[$i]['winner'] . " " . $result[$i]['loser'] . " ";
  }

  return $op;
  
} catch(PDOException $e) {
  echo "Error: " .$e->getMessage();
}

$conn = null;
}

function deck_list_output($user)
{
        global $servername, $dbname, $username, $password;
        try {
  $conn = new PDO("mysql:host=$servername;dbname=$dbname", $username, $password);
  // set the PDO error mode to exception
  $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
  $stmt = $conn->prepare("SELECT deck_name FROM decks WHERE user_id = $user");
  $stmt->execute();

  $result = $stmt->fetchAll(PDO::FETCH_ASSOC);
  return $result;

} catch(PDOException $e) {
  echo "Error: " .$e->getMessage();
}
$conn = null;
}

function historical_api_output($date)
{
	$output = shell_exec("php jsonparser.php $date");
	return $output;
}

function LogMessage($message)
{
	$message.="\n";
	file_put_contents('logfile', $message, FILE_APPEND);
}
$server = new rabbitMQServer("testRabbitMQ.ini","testServer");

echo "testRabbitMQServer BEGIN".PHP_EOL;
$server->process_requests('requestProcessor');
echo "testRabbitMQServer END".PHP_EOL;
exit();
?>

