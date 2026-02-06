<?php

require_once MODEL_PATH . '/Usuario.php'; //incluye la variable de $config_data para la configuracion


class AuthController
{

    private $usuario;

    
    public function __construct($bd_path) {
        
        $this->usuario = new Usuario($bd_path);

    }

    //buscamos la ip -------------------------------------------
    public function buscarIPenBD($ip)
    {      
        $rows = $this->usuario->buscarIP($ip);

        foreach ($rows as $row) {
            //busca la coincidencia de ip en los registros de usuario
            if ($ip == $row['ip']){
                
                return $row;  //retorna todos los campos 
            }
        }
        return null;
    }

    //buscamos el usuario y su contraseña ---------------------
    public function buscarUsuarioenBD($username, $password)
    {      
        $rows = $this->usuario->buscarUsuario($username, $password);

        foreach ($rows as $row) {
            //echo $row['nombre'] . "<br>";
            if ($username == $row['username'] && $password == $row['password']){
                
                return $row;
            }
        }
        return null;
    }

     
} //end class

?>