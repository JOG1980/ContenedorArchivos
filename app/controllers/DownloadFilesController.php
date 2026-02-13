<?php


require_once CONFIG_PATH . '/Config.php'; //incluye la variable de $config_data para la configuracion

class DownloadFilesController
{



    //funcion para buscar archivos y agregarlos a l zip
    private static function buscarArchivosRecursivoParaZIP($zip, $ruta_archivo, $ruta_archivo_en_zip)
    {
        if (!is_dir($ruta_archivo)) {
            $zip->addFile($ruta_archivo, $ruta_archivo_en_zip);
            return;
        }

        $items = scandir($ruta_archivo);

        foreach ($items as $item) {
            if ($item === '.' || $item === '..') {
                continue;
            }

            $nueva_ruta_archivo = $ruta_archivo . DIRECTORY_SEPARATOR . $item;
            $nueva_ruta_archivo_en_zip = $ruta_archivo_en_zip . DIRECTORY_SEPARATOR . $item;

            if (is_dir($nueva_ruta_archivo)) {
                //buscarCarpetasArchivosRecursivo($zip, $rutaCompleta);
                self::buscarArchivosRecursivoParaZIP($zip, $nueva_ruta_archivo, $nueva_ruta_archivo_en_zip);
            } else {
                $zip->addFile($nueva_ruta_archivo, $nueva_ruta_archivo_en_zip);
            }
        }
    }





    //funcion para generar un archivo zip con los archivos seleccionados --------------------
    //public static function generateZipFiles($nivel_inicial, $ruta_busqueda, $archivos)
    public static function generateZipFiles($nivel_inicial, $archivos)
    {


        $myObj = (object)[]; //creamos un objeto vacio
        $myObj->file_exist = 'FAIL';

        //si existe una lista de archivos
        if (count($archivos) > 0) {

            $config_data = Config::load();
            $ruta_busqueda_base = $config_data->contenedor_ruta_base . $nivel_inicial;

            //$nombreZip = 'archivos.zip';
            //$nombreZip = sys_get_temp_dir() . '\\archivos.zip';
            //asignamos el nombre del archivo que s eva a guardar desde  ela configuracion
            $nombreZip = '';
            if ($config_data->archivo_zip_agregar_fecha == '1') {
                date_default_timezone_set("America/Mexico_City");
                $fecha_actual = date("Y-m-d \hHis");
                // $fecha_actual = new DateTime("now", new DateTimeZone("America/Mexico_City"));
                // $fecha_actual->format("Y-m-d \hHis");
                $nombreZip =  $config_data->archivo_zip_nombre . ' (' . $fecha_actual . ').' . $config_data->archivo_zip_ext;
            } else
                $nombreZip =  $config_data->archivo_zip_nombre . '.' . $config_data->archivo_zip_ext;






            $zip = new ZipArchive();

            if ($zip->open($nombreZip, ZipArchive::CREATE | ZipArchive::OVERWRITE) === TRUE) {

                foreach ($archivos as $archivo) {
                    $ruta_completa_relativa =  $ruta_busqueda_base . $archivo;
                    if (file_exists($ruta_completa_relativa)) {
                        self::buscarArchivosRecursivoParaZIP($zip, $ruta_completa_relativa, $archivo);
                    }
                }

                $zip->close();
            } else {
                echo 'No se pudo crear el archivo ZIP';
            }

            $myObj->nombre_archivo_zip = $nombreZip;
            $myObj->file_exist = 'OK';
        }
        //codificacion de datos de obj a json
        $myJSON = json_encode($myObj);
        echo $myJSON; //enviamos de regreso el objeto con estructura json
    }


    public static function downloadZipFiles($nombreZip)
    {

        if (file_exists($nombreZip)) {
            $file_size = filesize($nombreZip);

            // Forzar descarga
            header('Content-Type: application/zip');
            header('Content-Disposition: attachment; filename="' . $nombreZip . '"');
            header('Content-Length: ' . $file_size);

            readfile($nombreZip);

            // Eliminar el zip después de descargarlo
            unlink($nombreZip);
        }
        exit;
    }
}
