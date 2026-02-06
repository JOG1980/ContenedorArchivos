<?php

//definimos la variable inicial del proyecto, todo se direcciona a este archivo
//es importnte que siempre se ejecute primero este archivo para que genere estas rutas


if (!defined('ROOT_PATH')) { //si la ruta raiz no esta definida, define todas las rutas requeridas
    define('ROOT_PATH', __DIR__ );
    define('CONFIG_PATH', ROOT_PATH . '/config');
    define('APP_PATH', ROOT_PATH . '/app');
    define('VIEW_PATH', APP_PATH . '/views');
    define('MODEL_PATH', APP_PATH . '/models');
    define('CONTROLLER_PATH', APP_PATH . '/controllers');
}

$controller = $_GET['controller'] ?? 'loginIP';

if($controller=='loginIP'){
    require_once CONTROLLER_PATH.'/loginController.php';
    $obj = new LoginController();
    $obj->validarIp();
    exit;
}
else if($controller=='loginUser'){

    // Existe y tiene valor
    if (!empty($_POST['username']) && !empty($_POST['password'])) {
	
	    
        $username = $_POST['username'];
        $password = $_POST['password'];
        //$username = $datos->username;
        //$password = $datos->password;
        require_once CONTROLLER_PATH.'/loginController.php';
        $obj = new LoginController();
        $obj->validarUsuario($username, $password);
        exit;
    }
}

 require VIEW_PATH.'/login.php';

exit;

?>