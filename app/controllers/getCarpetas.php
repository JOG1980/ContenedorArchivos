<?php
session_start();

// 👉 Indica al navegador que:
// no-store: no guarde nada en caché
// no-cache: siempre valide antes de usar una copia
// must-revalidate: si está vencida, debe pedirla otra vez
// max-age=0: la respuesta expira inmediatamente
header("Cache-Control: no-store, no-cache, must-revalidate, max-age=0");

// 👉 Directiva antigua (principalmente para Internet Explorer):
// * post-check / pre-check controlaban cuándo validar el caché. 
// * El false evita que esta línea reemplace el header anterior y lo concatena
// ⚠️ Hoy en día es más bien legacy, pero no hace daño.
header("Cache-Control: post-check=0, pre-check=0", false);

//Header antiguo de HTTP/1.0, Se mantiene por compatibilidad con navegadores viejos
header("Pragma: no-cache");


//if (isset($_POST['datos']) && !empty($_POST['datos'])) 
if (empty($_POST['datos'])) {
	header("Location: login.php");
	exit;
}

/*if (empty($_SESSION['_RUTA_INICIAL_'])) {
	header("Location: login.php");
	exit;
}*/

require_once CONFIG_PATH .'/Config.php'; //incluye la variable de $config_data para la configuracion
$config_data = Config::load();


//require_once('./utils.php'); //incluye la variable de $config_data para la configuracion



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




//obtenemos la ruta a partiur de donde va a buscar los archivos y carpetas buscados
$nivel_inicial = $_SESSION['nivel_inicial'];

//Takes a JSON encoded string and converts it into a PHP variable.
$datos = json_decode($_POST['datos']);

//obtenemos datos de llegada
//$ruta = 'container/'.$datos->ruta;
//$ruta = $_SESSION['_RUTA_INICIAL_'] . $datos->ruta;

$ruta = ROOT_PATH .'/'. $config_data->contenedor_ruta_base . $nivel_inicial . $datos->ruta;

$items = buscarCarpetas($ruta);


//oci_close($conn);
$myObj = (object)[]; //creamosun objeto vacio

$myObj->items = $items;

//codificacion de datos de obj a json
$myJSON = json_encode($myObj);

echo $myJSON; //enviamos de regreso el objeto con estructura json
