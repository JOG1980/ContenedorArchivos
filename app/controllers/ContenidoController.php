<?php

//session_start esta ya esta envocada en el index que requiere este archivo 
//session_start();

require_once CONFIG_PATH . '/Config.php'; //incluye la variable de $config_data para la configuracion

class ContenidoController
{

    private static function buscarCarpetas($ruta)
    {
        $resultado = [];

        if (!is_dir($ruta)) {
            return $resultado;
        }

        $items = scandir($ruta);

        foreach ($items as $item) {
            if ($item === '.' || $item === '..') {
                continue;
            }

            $rutaCompleta = $ruta . DIRECTORY_SEPARATOR . $item;

            if (is_dir($rutaCompleta)) {
                $resultado[] = [
                    "text" => $item,
                    //"icon" => "jstree-folder",
                    //"icon"=> "bi bi-folder-fill text-warning icon-sm",
                    "icon" => "images/32x32/folder.png",
                    'ruta'   => $rutaCompleta,
                    "children" => self::buscarCarpetas($rutaCompleta)
                ];
            }
        }

        return $resultado;
    }



    private static function buscarCarpetasArchivos($ruta_inicial, $ruta_busqueda)
    {
        $resultado = [];

        $ruta = $ruta_inicial . $ruta_busqueda;

        if (!is_dir($ruta)) {
            return $resultado;
        }

        $items = scandir($ruta);

        foreach ($items as $item) {
            if ($item === '.' || $item === '..') {
                continue;
            }

            //solo para los enlaces de los archivos
            //$rutaCompleta = $ruta . DIRECTORY_SEPARATOR . $item;
            $ruta_completa = $ruta . DIRECTORY_SEPARATOR . $item;
            $ruta_busqueda_completa = $ruta_busqueda . DIRECTORY_SEPARATOR . $item;




            if (is_dir($ruta_completa)) {
                $resultado[] = [
                    "text" => $item,
                    "tipo" => "folder",
                    'ruta'   => $ruta_busqueda_completa
                ];
            } else {
                $fecha_cre = date('Y-m-d H:i:s', filectime($ruta_completa));
                $fecha_mod = date('Y-m-d H:i:s', filemtime($ruta_completa));
                $fsize = intdiv(filesize($ruta_completa), 1024);
                $ext = substr($item, strrpos($item, '.', 0) + 1);
                $mime_type = mime_content_type($ruta_completa);
                $resultado[] = [
                    "text" => $item,
                    "tipo" => "file",
                    "ext" => $ext,
                    "fsize" => $fsize,
                    "fecha_cre" => $fecha_cre,
                    "fecha_mod" => $fecha_mod,
                    "mime_type" => $mime_type,
                    'ruta_completa'   => $ruta_completa,
                    'ruta'   => $ruta_busqueda_completa,

                ];
            }
        }

        return $resultado;
    }


    public static function getCarpetas($nivel_inicial)
    {
        $config_data = Config::load();


        //$ruta_busqueda_absoluta = ROOT_PATH . '/' . $config_data->contenedor_ruta_base . $nivel_inicial;
        $ruta_busqueda_base = $config_data->contenedor_ruta_base . $nivel_inicial;

        $items = self::buscarCarpetas($ruta_busqueda_base);


        //oci_close($conn);
        $myObj = (object)[]; //creamosun objeto vacio

        $myObj->items = $items;

        //codificacion de datos de obj a json
        $myJSON = json_encode($myObj);

        echo $myJSON; //enviamos de regreso el objeto con estructura json

    }



    //public static function getContenido($nivel_inicial, $ruta_relativa)
    public static function getContenido($nivel_inicial, $ruta_busqueda_actual)
    {

        $config_data = Config::load();


        // $ruta_inicial = ROOT_PATH . '/' . $config_data->contenedor_ruta_base . $nivel_inicial . $datos->ruta;

        $ruta_busqueda_base = $config_data->contenedor_ruta_base . $nivel_inicial;


        //$items = buscarCarpetasArchivos($ruta_inicial, $ruta_relativa);
        $items = self::buscarCarpetasArchivos($ruta_busqueda_base, $ruta_busqueda_actual);



        $myObj = (object)[]; //creamosun objeto vacio			
        $myObj->items = $items;
        //codificacion de datos de obj a json
        $myJSON = json_encode($myObj);

        echo $myJSON; //enviamos de regreso el objeto con estructura json

    }
}
