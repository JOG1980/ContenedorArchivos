<?php


require_once CONFIG_PATH . '/Config.php'; //incluye la variable de $config_data para la configuracion

class UploadFilesController
{


    public static function uploadFiles($nivel_inicial, $ruta_destino, $archivo)
    {
        $config_data = Config::load();
        $ruta_busqueda_base = $config_data->contenedor_ruta_base . $nivel_inicial . $ruta_destino;


        $nombre_base = basename($archivo['name']); //nombre completo
        $nombre = substr($nombre_base, 0, strrpos($nombre_base, '.'));
        $ext = substr($nombre_base, strrpos($nombre_base, '.') + 1);
        date_default_timezone_set("America/Mexico_City");
        $fecha_actual = date("Y-m-d \hHis");
        // Nombre seguro
        //$nuevo_nombre_completo = uniqid() . '_' . basename($archivo['name']);
        $nuevo_nombre_completo = $nombre . '_(' . $fecha_actual . ').' . $ext;
        $ruta = $ruta_busqueda_base . '\\' . $nuevo_nombre_completo;

        move_uploaded_file($archivo['tmp_name'], $ruta);

        echo json_encode([
            'ok' => true,
            'archivo' => $nombre
        ]);
        exit;
    }
}
