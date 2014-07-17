<?php

// ------ setup section (you can change it)

$server_ip = '192.168.1.80'; //remote host       (marlight wi-fi controller address)
$server_port = 50000; //remote host port  (marlight wi-fi controller port)

$delay = 0.1;         //delay after send a packet before send another (0.1 will be enaougth)

$error_value = 'Error'; //return message if error occurs
$ok_value = 'Ok'; //return message if packet send without error

$debug = true;                  //show debug info in response

$use_password = false; //use password for security
$password = '12345'; //password to compare with

// ------ end setup section











#region marlight udp packets const (do not change it)

// ----------------------------- FIRST PAGE IPHONE APPLICATION  ------------------------------------------
$ALL_ON                 = array(0x01, 0x55);
$ALL_OFF                = array(0x02, 0x55);

$BRIGHT_UP              = array(0x03, 0x55);
$BRIGHT_DOWN            = array(0x04, 0x55);

$TEMP_COLDER            = array(0x05, 0x55);
$TEMP_WARMER            = array(0x06, 0x55);

$BRIGHT_TEMP_DEFAULT    = array(0x07, 0x55);

$ON_1                   = array(0x08, 0x55);
$OFF_1                  = array(0x09, 0x55);
$ON_2                   = array(0x0A, 0x55);
$OFF_2                  = array(0x0B, 0x55);
$ON_3                   = array(0x0C, 0x55);
$OFF_3                  = array(0x0D, 0x55);
$ON_4                   = array(0x0E, 0x55);
$OFF_4                  = array(0x0F, 0x55);

// ----------------------------- SECOND PAGE IPHONE APPLICATION  (RGB) -----------------------------------
$RGB_MODE_ON            = array(0x12, 0x55);
$RGB_MODE_BRIGHT_DOWN   = array(0x10, 0x55);
$RGB_MODE_BRIGHT_UP     = array(0x11, 0x55);
$RGB_MODE_SET_COLOR     = array(0x13, 0x00, 0x00, 0x00, 0x55);

// ----------------------------- THIRD PAGE IPHONE APPLICATION  (PRESETS) --------------------------------
$MODE_NIGHT             = array(0x14, 0x55);
$MODE_MEETING           = array(0x15, 0x55);
$MODE_READING           = array(0x16, 0x55);
$MODE_MODE              = array(0x17, 0x55);

$MODE_TIMER             = array(0x18, 0x00, 0x00, 0x00, 0x00, 0x09, 0x14, 0x55);
$MODE_ALARM             = array(0x19, 0x00, 0x00, 0x09, 0x14, 0x55);
$MODE_SLEEP             = array(0x1A, 0x55); // ??????
$MODE_RECREATION        = array(0x1B, 0x00, 0x00, 0x00, 0x55);
// ----------------------------- END CONSTS -----------------------------------

#endregion


// function to send UDP packet
function sendMessage($udp_message)
{
    $message = vsprintf(str_repeat('%c', count($udp_message)), $udp_message);

    global $server_ip, $server_port, $delay, $debug, $error_value, $ok_value;

    if($debug){
        echo '<b>remote host/IP:</b> ' . $server_ip . '<br/>';
        echo '<b>remote port:</b> ' . $server_port . '<br/>';
        echo '<b>UDP message:</b> ';
        foreach($udp_message as $key => $value){
            echo '0x';
            printf("%02x\n", $value);
        }
        echo'<br/>';
        echo'<b>result:</b> ';
    }

    if ($socket = socket_create(AF_INET, SOCK_DGRAM, SOL_UDP)) {
        socket_sendto($socket, $message, strlen($message), 0, $server_ip, $server_port);
        sleep ($delay);
        socket_close($socket);

        echo $ok_value;
    } else {
        echo $error_value;
    }

    if($debug){
        echo '<br/>';
        echo '---------- debug info ----------<br/><br/>';
    }
}



//define is the POST or GET request
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $request = $_POST;
}   else {
    $request = $_GET;
}


//if value set read them or set it to default
if(isset($request['command'])) {$command = $request['command'];} else {$command='';}

if(isset($request['r'])) {$r = $request['r'];} else {$r=0;}
if(isset($request['g'])) {$g = $request['g'];} else {$g=0;}
if(isset($request['b'])) {$b = $request['b'];} else {$b=0;}

if(isset($request['h1'])) {$h1 = $request['h1'];} else {$h1=0;}
if(isset($request['m1'])) {$m1 = $request['m1'];} else {$m1=0;}

if(isset($request['h2'])) {$h2 = $request['h2'];} else {$h2=0;}
if(isset($request['m2'])) {$m2 = $request['m2'];} else {$m2=0;}

if(isset($request['channel'])) {$channel = $request['channel'];} else {$channel=0;}

if(isset($request['password'])) {$pass = $request['password'];} else {$pass='';}


if($use_password && $pass!=$password) {
    echo 'You are dont autorized to this operation!';
    return;
}

// if degug mode
if($debug){
    echo '---------- debug info ----------<br/>';
    echo '<b>request method</b> '.$_SERVER['REQUEST_METHOD'].'<br/>';
    echo '<b>request value command:</b> '.$command.'<br/>';
    echo '<b>request value r:</b>  '.$r.'<br/>';
    echo '<b>request value g:</b>  '.$g.'<br/>';
    echo '<b>request value b:</b>  '.$b.'<br/>';

    echo '<b>request value h1:</b> '.$h1.'<br/>';
    echo '<b>request value m1:</b> '.$m1.'<br/>';
    echo '<b>request value h2:</b> '.$h2.'<br/>';
    echo '<b>request value m2:</b> '.$m2.'<br/>';
    if($channel!=0) {echo '<b>request command for channel:</b> '.$channel.'<br/>';}

}


// if request for specifyed channel then send ON command for this channel
if($channel!=0){
    $debug = false;
    switch($channel)        {
        case 1:
            sendMessage($ON_1); 
            break;
        case 2:
            sendMessage($ON_2); 
            break;
        case 3:
            sendMessage($ON_3); 
            break;
        case 4:
            sendMessage($ON_4); 
            break;
    }
    $debug = true;
} 


//send command depending POST/GET request
switch($command){
    case 'ALL_ON': 
        sendMessage($ALL_ON); 
        break;  
    case 'ALL_OFF': 
        sendMessage($ALL_OFF); 
        break;  
    case 'BRIGHT_UP': 
        sendMessage($BRIGHT_UP); 
        break;  
    case 'BRIGHT_DOWN': 
        sendMessage($BRIGHT_DOWN); 
        break;  
    case 'TEMP_COLDER': 
        sendMessage($TEMP_COLDER); 
        break;
    case 'TEMP_WARMER': 
        sendMessage($TEMP_WARMER);
        break;
    case 'BRIGHT_TEMP_DEFAULT': 
        sendMessage($BRIGHT_TEMP_DEFAULT);
        break;
    case 'ON_1': 
        sendMessage($ON_1); 
        break;
    case 'OFF_1': 
        sendMessage($OFF_1); 
        break;
    case 'ON_2':
        sendMessage($ON_2); 
        break;
    case 'OFF_2': 
        sendMessage($OFF_2); 
        break;
    case 'ON_3': 
        sendMessage($ON_3);
        break;
    case 'OFF_3':
        sendMessage($OFF_3);
        break;
    case 'ON_4': 
        sendMessage($ON_4);
        break;
    case 'OFF_4': 
        sendMessage($OFF_4); 
        break;
    case 'RGB_MODE_ON': 
        sendMessage($RGB_MODE_ON); 
        break;
    case 'RGB_MODE_BRIGHT_DOWN': 
        sendMessage($RGB_MODE_BRIGHT_DOWN);
        break;
    case 'RGB_MODE_BRIGHT_UP': 
        sendMessage($RGB_MODE_BRIGHT_UP); 
        break;
    case 'RGB_MODE_SET_COLOR':
        $rgb_msg =$RGB_MODE_SET_COLOR;
        $rgb_msg[1] = $r;
        $rgb_msg[2] = $g;
        $rgb_msg[3] = $b;
        sendMessage($rgb_msg);
        break;
    case 'MODE_NIGHT': 
        sendMessage($MODE_NIGHT); 
        break;
    case 'MODE_MEETING': 
        sendMessage($MODE_MEETING); 
        break;
    case 'MODE_READING': 
        sendMessage($MODE_READING); 
        break;
    case 'MODE_MODE': 
        sendMessage($MODE_MODE); 
        break;
    case 'MODE_TIMER': 
        $timer_msg =$MODE_TIMER;
        $timer_msg[1] = $h1;
        $timer_msg[2] = $m1;
        $timer_msg[3] = $h2;
        $timer_msg[4] = $m2;
        sendMessage($timer_msg);
        break;
    case 'MODE_ALARM': 
        $alarm_msg =$MODE_ALARM;
        $alarm_msg[1] = $h1;
        $alarm_msg[2] = $m1;
        sendMessage($alarm_msg);
        break;
    case 'MODE_SLEEP': 
        sendMessage($MODE_SLEEP); 
        break;
    case 'MODE_RECREATION': 
        $recreation_msg =$MODE_RECREATION;
        $recreation_msg[1] = $R;
        $recreation_msg[2] = $G;
        $recreation_msg[3] = $B;
        sendMessage($recreation_msg);
        break;
    default:
        break;
}