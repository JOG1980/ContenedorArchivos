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

if (empty($_SESSION['_RUTA_INICIAL_'])) {
	header("Location: login.php");
	exit;
}



require_once('./config.php'); //incluye la variable de $config_data para la configuracion


$ruta_inicial = $_SESSION['_RUTA_INICIAL_'];


//funcion para buscar archivos y agregarlos a l zip
function buscarArchivosRecursivoParaZIP($zip, $ruta_archivo, $ruta_archivo_en_zip)
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
			buscarArchivosRecursivoParaZIP($zip, $nueva_ruta_archivo, $nueva_ruta_archivo_en_zip);
		} else {
			$zip->addFile($nueva_ruta_archivo, $nueva_ruta_archivo_en_zip);
		}
	}
}


	$myObj = (object)[]; //creamosun objeto vacio	
	$myObj->nombre_archivo_zip = "";
	$myObj->file_exist = 'FAIL';


//if (isset($_GET['datos']) && !empty($_GET['datos'])) {

//Takes a JSON encoded string and converts it into a PHP variable.
$datos = json_decode($_POST['datos']);

$archivos = $datos->rutas;
if (count($archivos) >0) {

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
			$ruta_completa_relativa =  $ruta_inicial . $archivo;
			if (file_exists($ruta_completa_relativa)) {
				buscarArchivosRecursivoParaZIP($zip, $ruta_completa_relativa, $archivo);
			}
		}

		$zip->close();
	} else {
		echo 'No se pudo crear el archivo ZIP';
	}


	//si el archivo existe
	if (file_exists($ruta_completa_relativa)) {
		$myObj->nombre_archivo_zip = $nombreZip;
		$myObj->file_exist = 'OK';
	}


	
}

	$myJSON = json_encode($myObj);
echo $myJSON; //enviamos de regreso el objeto con estructura json
