<?php

class Usuario
{
    private $bd_path;
    
    public function __construct($bd_path) {
        $this->bd_path = $bd_path;
    }

    
    //buscamos la ip --------------------------
    public function buscarIP($ip)
    {
        $db = new PDO('sqlite:'.$this->bd_path);
        $db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
        $sql = "SELECT * FROM usuarios WHERE ip = :ip";
        $stmt = $db->prepare($sql);
        $stmt->bindValue(':ip', $ip, PDO::PARAM_STR);
        $stmt->execute();

        $rows = $stmt->fetchAll(PDO::FETCH_ASSOC);
        
        $db = null; //cerramos conexion
        return $rows;
    }


    //buscamos el usuario --------------------------
    public function buscarUsuario($username,$password)
    {
        $db = new PDO('sqlite:'.$this->bd_path);
        $db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
        $sql = "SELECT * FROM usuarios WHERE username = :username AND password = :password";
        $stmt = $db->prepare($sql);
        $stmt->bindValue(':username', $username, PDO::PARAM_STR);
        $stmt->bindValue(':password', $password, PDO::PARAM_STR);
        $stmt->execute();

        $rows = $stmt->fetchAll(PDO::FETCH_ASSOC);
        
        $db = null; //cerramos conexion
        return $rows;
    }

} //end class

?>