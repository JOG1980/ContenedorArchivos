<?php

//definimos la variable inicial del proyecto, todo se direcciona a este archivo
//es importnte que siempre se ejecute primero este archivo para que genere estas rutas
session_start();

if (!defined('ROOT_PATH')) { //si la ruta raiz no esta definida, define todas las rutas requeridas
    define('ROOT_PATH', __DIR__ );
    define('CONFIG_PATH', ROOT_PATH . '/config');
    define('APP_PATH', ROOT_PATH . '/app');
    define('VIEW_PATH', APP_PATH . '/views');
    define('MODEL_PATH', APP_PATH . '/models');
    define('CONTROLLER_PATH', APP_PATH . '/controllers');
}

$controller = $_GET['controller'] ?? 'loginIP';
$action = $_GET['action'] ?? '';

if($controller=='loginIP'){
    require_once CONTROLLER_PATH.'/LoginController.php';
    $obj = new LoginController();
    $obj->validarIp();
    exit;
}
else if($controller=='loginUser'){

    // Existe y tiene valor
    if (!empty($_POST['username']) && !empty($_POST['password'])) {
	
	    
        $username = $_POST['username'];
        $password = $_POST['password'];
        require_once CONTROLLER_PATH.'/LoginController.php';
        $obj = new LoginController();
        $obj->validarUsuario($username, $password);
        exit;
    }
}
else if($controller=='contenido'){

    //obtenemos la ruta a partiur de donde va a buscar los archivos y carpetas buscados
    $nivel_inicial = $_SESSION['nivel_inicial'];

    //Takes a JSON encoded string and converts it into a PHP variable.
    $datos = json_decode($_POST['datos']);
    $ruta_busqueda = $datos->ruta;
    require_once CONTROLLER_PATH.'/ContenidoController.php';

    
    
    if($action=='getCarpetas'){
        //$ruta = ROOT_PATH .'/'. $config_data->contenedor_ruta_base . $nivel_inicial;

        //require_once CONTROLLER_PATH.'/getCarpetas.php';
         ContenidoController::getCarpetas($nivel_inicial);
        exit;
    }
    else if($action=='getContent'){

    
        //ContenidoController::getContedido($nivel_inicial,$ruta_relativa);
        //require_once CONTROLLER_PATH.'/getCarpetas.php';
        //exit;
    } 

}

 require VIEW_PATH.'/login.php';

exit;

?>