<?php

//EN ESTA PAGINA DE LOGIN VALIDAMOS SI LA IP ESTA REGISTRADA PARA ACCESO A UN USUARIO, 
//DE SER ASI SE REDIRECCIONA AUTOMATICAMENTE AL CONTENIDO CON EL ESTATUS DE LOGUEADO 
//Y LOS DATOS OBTENIDOS DEL USUARIO QUE TIENE ESTA IP


session_start();

require_once CONFIG_PATH .'/Config.php'; //incluye la variable de $config_data para la configuracion
//require_once('./utils.php'); //incluye la variable funcion de GetIPClient
require_once CONTROLLER_PATH.'/AuthController.php'; //incluye las funciones de autenticacion para las IP registradas en la tabla usuarios

/*
if (empty($_SESSION['intento_login_ip'])) {
	$_SESSION['intento_login_ip'] = '1';
}
else{




}*/




class LoginController{

    
    //esta funcion se obtiene la IP del cliente ---------------------------------
    private function getClientIP() {
        $keys = [
            'HTTP_CLIENT_IP',
            'HTTP_X_FORWARDED_FOR',
            'HTTP_X_REAL_IP',
            'REMOTE_ADDR'
        ];

        foreach ($keys as $key) {
            if (!empty($_SERVER[$key])) {
                foreach (explode(',', $_SERVER[$key]) as $ip) {
                    $ip = trim($ip);
                    if (filter_var($ip, FILTER_VALIDATE_IP)) {
                        return $ip;
                    }
                }
            }
        }
        return null;
    }


    public function validarIp(){

        
        $client_ip = $this->getClientIP();

        //$base_datos = $config_data->base_datos_sqlite;
        $config = Config::load();
        $base_datos = $config->base_datos_sqlite;
        $auth = new AuthController($base_datos);

        //buscamos si la ip del cliente esta regiustrada en las ips autorizadas
        $reg_ip = $auth->buscarIPenBD($client_ip);
        if ($reg_ip!==null) {
            $_SESSION['LOGIN_OK'] = true;
            $_SESSION['nombre_usuario'] = $reg_ip['nombre'];
            //$_SESSION['ip_usuario'] = $reg_ip['ip'];
            $_SESSION['rol_id'] = $reg_ip['rol_id'];
            //$_SESSION['ruta_inicial'] = ROOT_PATH.'/'.$config->contenedor_ruta_base . $reg_ip['nivel_inicial'];
            $_SESSION['nivel_inicial'] = $reg_ip['nivel_inicial'];
            //header("Location: contenido.php");
            require VIEW_PATH.'/contenido.php';
            exit;
        }
        else{
            require VIEW_PATH.'/login.php';
            exit;
        }
    }

    //funcion para validar usuario por username y password -------------------------------
    public function validarUsuario($username, $password){

        $config = Config::load();
        $base_datos = $config->base_datos_sqlite;
        $auth = new AuthController($base_datos);

        $reg_usuario = $auth->buscarUsuarioenBD($username, $password);
        if ($reg_usuario!==null) {
            $_SESSION['LOGIN_OK'] = true;
            $_SESSION['nombre_usuario'] = $reg_usuario['nombre'];
            //$_SESSION['ip_usuario'] = $reg_usuario['ip'];
            $_SESSION['rol_id'] = $reg_usuario['rol_id'];
            //$_SESSION['ruta_inicial'] = ROOT_PATH.'/'.$config->contenedor_ruta_base . $reg_usuario['nivel_inicial'];
            $_SESSION['nivel_inicial'] = $reg_usuario['nivel_inicial'];
            //header("Location: contenido.php");
            require VIEW_PATH.'/contenido.php';
            exit;
        }
        else{
            require VIEW_PATH.'/login.php';
            exit;
        }
    }
}
