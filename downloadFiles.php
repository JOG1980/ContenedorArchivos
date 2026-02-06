<?php


session_start();

header("Cache-Control: no-store, no-cache, must-revalidate, max-age=0");
header("Cache-Control: post-check=0, pre-check=0", false);
header("Pragma: no-cache");


//if (isset($_POST['datos']) && !empty($_POST['datos'])) 
if (empty($_GET['nombre_archivo_zip'])) {
	header("Location: login.php");
	exit;
}


//require_once('./config.php'); //incluye la variable de $config_data para la configuracion



//$nombreZip = "archivos.zip";
$nombreZip =  $_GET['nombre_archivo_zip'];

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

