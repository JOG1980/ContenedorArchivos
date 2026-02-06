<?php
//ya es llamado en el index
//session_start();

// 👉 Indica al navegador que:
// no-store: no guarde nada en caché
// no-cache: siempre valide antes de usar una copia
// must-revalidate: si está vencida, debe pedirla otra vez
// max-age=0: la respuesta expira inmediatamente
//header("Cache-Control: no-store, no-cache, must-revalidate, max-age=0");

// 👉 Directiva antigua (principalmente para Internet Explorer):
// * post-check / pre-check controlaban cuándo validar el caché. 
// * El false evita que esta línea reemplace el header anterior y lo concatena
// ⚠️ Hoy en día es más bien legacy, pero no hace daño.
//header("Cache-Control: post-check=0, pre-check=0", false);

//Header antiguo de HTTP/1.0, Se mantiene por compatibilidad con navegadores viejos
//header("Pragma: no-cache");



//require_once CONFIG_PATH . '/config.php'; //incluye la variable de $config_data para la configuracion
//require_once('./utils.php'); //incluye la variable de $config_data para la configuracion


// $ruta_contenedor_ruta_base = $config_data->contenedor_ruta_base; //"__FCONTAINER__" definido en el xml
// $ruta_inicial = $ruta_contenedor_ruta_base . $config_data->nivel_inicial;

// $_SESSION['_RUTA_INICIAL_'] = $ruta_inicial;
//se carga la variable $config_data con los datos de configuracion
$config_data = Config::load();
//$ruta_contenedor_ruta_base = $config->contenedor_ruta_base; //"__FCONTAINER__" definido en el xml
$ruta_contenedor_ruta_base = $config_data->contenedor_ruta_base; //"__FCONTAINER__" definido en el xml
$ruta_inicial = $ruta_contenedor_ruta_base . $_SESSION['nivel_inicial'];

//busqueda de carpetas -----------------------------------------------
//$carpetas = buscarCarpetas($ruta_contenedor );
//$carpetas_json = json_encode($carpetas);
//$carpetas1 = json_encode($carpetas, JSON_PRETTY_PRINT | JSON_UNESCAPED_SLASHES);

?>


<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title><?php echo $config_data->titulo_pagina ?></title>

    <!--CSS -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" integrity="sha384-sRIl4kxILFvY47J16cr9ZwB07vP4J8+LH7qKQnuqkuIAvNWLzeN8tE5YBujZqJLB" crossorigin="anonymous">
    <!--link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css" integrity="sha384-xOolHFLEh07PJGoPkLv1IbcEPTNtaed2xpHsD9ESMhqIYd0nLMwNLD69Npy4HI+N" crossorigin="anonymous"-->
    <link rel="stylesheet" href="./jquery_plugins/jsTree/themes/default/style.min.css" />
    <link href="./jquery_plugins/DataTables/datatables.min.css" rel="stylesheet">

    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.13.1/font/bootstrap-icons.min.css">
    <link rel="stylesheet" href="css/contenido.css" />

    <link rel="icon" type="image/png" sizes="32x32" href="./images/cfe_letras.png" />

</head>

<body>

    <input type="hidden" id="hd_ruta_contenedor_nivel_inicial" value='<?php echo $ruta_contenedor_nivel_inicial; ?>' /> <!-- Tiene la ruta base-->
    <input type="hidden" id="hd_contenido_carpetas" value='<?php echo $carpetas_json; ?>' />
    <input type="hidden" id="hd_ruta_actual" value='' /> <!--contiene la ruta actual de la session, no la de todos los archivos del proyecto-->
    <input type="hidden" id="hd_vista_contenido" value='iconos' /> <!--almacena la vista seleccionada actualmente-->
    <input type="hidden" id="hd_item_prev_click_derecho" value='false' /> <!--guarda si el objeto de click derecho es un item, esto para no ejecutar el evento en el canvas -->

    <?php require_once VIEW_PATH .'/cargador.php'; ?>
    <?php require_once VIEW_PATH .'/header.php'; ?>


    <div class="container-fluid">
        <div class="my_barra_menu">
            <div class="row">
                <div class="col-2">
                    <div class="d-flex  gap-2">
                        <button class="btn my_btn" id="btn_home_file" title="Inicio"><img src="./images/32x32/application_home.png" /></button>
                        <button class="btn my_btn" id="btn_folders_show" title="Expandir/Contraer Folders"><img src="./images/32x32/folders_explorer.png" /></button>

                    </div>
                </div>

                <div class="col">
                    <div class="d-flex gap-2">
                        <button class="btn my_btn" id="btn_view_icons" title="Ver Iconos"><img src="./images/32x32/application_view_icons.png" /></button>
                        <button class="btn my_btn" id="btn_view_details" title="Ver Detalles"><img src="./images/32x32/application_view_detail.png" /></button>

                        <button class="btn my_btn" id="btn_up_level" title="Subir Nivel"><img src="./images/32x32/arrow_up.png" /></button>
                        <input type="text" class="form-control" id="barra_ruta" style="width:100%;" readonly>

                        <!--button class="btn my_btn" id="btn_folder_add" title="Agregar Carpeta"><img src="./images/32x32/folder_add.png" /></button-->
                        <!--button class="btn my_btn" id="btn_folder_delete" title="Borrar Carpetra"><img src="./images/32x32/folder_delete.png" /></button-->
                    </div>
                </div>

                <div class="col-2">
                    <div class="d-flex justify-content-end gap-1">
                        <button class="btn my_btn" id="btn_download_file" title="Bajar Archivos"><img src="./images/32x32/download_cloud.png" /></button>
                        <button class="btn my_btn" id="btn_config" title="Configurar"><img src="./images/32x32/cog.png" /></button>

                    </div>
                </div>
            </div>
        </div>
    </div>





    <div class="container-fluid">
        <div class="row">
            <div class="col-sm-2">
                <div class='jstree_contenedor'>
                    <div id="jstree"></div>
                </div>
            </div>
            <div class="col-sm">
                <div id="contenedor_items" class="container-fluid contenedor_items">
                </div>
                <!--canvas id="myCanvas" width="500" height="30" style="border:1px solid #000;"></canvas-->
                <!--canvas id="myCanvas" style="border:1px solid #000; width:100%; height:50%;"></canvas-->


            </div>
        </div>


        <div id="menu_contenedor" style='display:none;'>
       
            <div> <button class="btn" id="btn_folder_add" title="Agregar carpeta"><img src="./images/32x32/folder_add.png" />&nbsp;Agregar carpeta</button></div>
            <div> <button class="btn" id="btn_folder_delete" title="Borrar carpeta"><img src="./images/32x32/folder_delete.png" />&nbsp;Borrar carpeta</button></div>
            <div> <button class="btn" id="btn_folder_renombrar" title="Renombrar carpeta"><img src="./images/32x32/folder_edit.png" />&nbsp;Renombrar carpeta</button></div>
            <div> <button class="btn" id="btn_file_delete" title="Borrar archivo"><img src="./images/32x32/page_delete.png" />&nbsp;Borrar archivo</button></div>
            <div> <button class="btn" id="btn_file_renombrar" title="Renombrar archivo"><img src="./images/32x32/page_edit.png" />&nbsp;Renombrar archivo</button></div>
        </div>


        <script src="https://code.jquery.com/jquery-3.7.1.min.js" integrity="sha256-/JqT3SQfawRcv/BIHPThkBvs0OEvtFFmqPF/lYI/Cxo=" crossorigin="anonymous"></script>
        <!--script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/1.12.1/jquery.min.js"></script-->
        <!-- Bootstrap CSS -->
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js" integrity="sha384-FKyoEForCGlyvwx9Hj09JcYn3nv7wiPVlz7YYwJrWVcXK/BmnVDxM+D2scQbITxI" crossorigin="anonymous"></script>
        <!--script src="https://cdn.jsdelivr.net/npm/jquery@3.5.1/dist/jquery.slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-Fy6S3B9q64WdZWQUiU+q4/2Lc9npb8tCaSX9FK7E8HnRr0Jz8D6OP9dO5Vg3Q9ct" crossorigin="anonymous"></script-->
        <script src="./jquery_plugins/jsTree/jstree.min.js"></script>


        <script src="./jquery_plugins/DataTables/datatables.min.js"></script>






        <script src="./js/contenido.js"></script>

</body>