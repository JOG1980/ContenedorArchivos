<?php

//CONFIG_PATH define la ruta ABSOLUTA DEL ARCHIVO DE CONFIGURACION



class Config
{
    static function load()
    {
        $xml = simplexml_load_file(CONFIG_PATH . '/config.xml');

        if ($xml === false) {
            echo "Error al cargar el archivo XML.";
        }

        $config_data = (object) [];

        $config_data->titulo_pagina = $xml->titulos->titulo_pagina;
        $config_data->titulo1 = $xml->titulos->titulo1;
        $config_data->titulo2 = $xml->titulos->titulo2;
        $config_data->titulo3 = $xml->titulos->titulo3;

        $config_data->contenedor_ruta_base = $xml->contenedor->ruta_base;
        //$config_data->nivel_inicial = "";

        $config_data->base_datos_sqlite  = $xml->bd_sqlite->ruta_bd;

        $config_data->archivo_zip_nombre    = $xml->archivo_zip->nombre;
        $config_data->archivo_zip_ext     = $xml->archivo_zip->ext;
        $config_data->archivo_zip_agregar_fecha = $xml->archivo_zip->agregar_fecha;

        return $config_data;
    }
}
