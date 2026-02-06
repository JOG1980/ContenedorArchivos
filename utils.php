<?php

//busqueda de carpetas -----------------------------------------------


function buscarCarpetasArchivosRecursivo($ruta) {
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
                "tipo"=>"folder",
                "icon" => "jstree-folder",
                'ruta'   => $rutaCompleta,
                "children" => buscarCarpetasArchivosRecursivo($rutaCompleta)
            ];
        } else {
            $fsize = filesize($rutaCompleta);
            $resultado[] = [
                "text" => $item,
                "tipo"=>"file",
                "size: " . $fsize,
                "fecha_creacion" => filectime($rutaCompleta),
                "fecha_modificacion" => filemtime($rutaCompleta),
                "icon" => "jstree-file",
                'ruta'   => $rutaCompleta,
                "children" => false
            ];
        }
    }

    return $resultado;
}


function buscarCarpetasArchivos($ruta_inicial, $ruta_relativa) {
    $resultado = [];

    $ruta = $ruta_inicial.$ruta_relativa;

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
        $ruta_relativa_completa = $ruta_relativa . DIRECTORY_SEPARATOR . $item;
       
        if (is_dir($ruta_completa)) {
            $resultado[] = [
                "text" => $item,
                "tipo"=>"folder",
                'ruta'   => $ruta_relativa_completa
            ];
        } else {
            $fecha_cre = date('Y-m-d H:i:s', filectime($ruta_completa));
            $fecha_mod = date('Y-m-d H:i:s', filemtime($ruta_completa));
            $fsize = intdiv(filesize($ruta_completa),1024);
            $ext = substr($item, strrpos($item, '.', 0)+1);
            $mime_type = mime_content_type($ruta_completa);
            $resultado[] = [
                "text" => $item,
                "tipo"=>"file",
                "ext"=>$ext,
                "fsize"=>$fsize,
                "fecha_cre" => $fecha_cre,
                "fecha_mod" => $fecha_mod,
                "mime_type" => $mime_type,
                'ruta_completa'   => $ruta_completa,
                'ruta'   => $ruta_relativa_completa,
                
            ];
        }
    }

    return $resultado;
}



function buscarCarpetas($ruta) {
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
                "icon"=> "images/32x32/folder.png",
                'ruta'   => $rutaCompleta,
                "children" => buscarCarpetas($rutaCompleta)
            ];
        }
    }

    return $resultado;
}



?>