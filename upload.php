<?php
header('Content-Type: application/json');

if (!isset($_FILES['archivo'])) {
    echo json_encode(['error' => 'No se recibió archivo']);
    exit;
}

$archivo = $_FILES['archivo'];

if ($archivo['error'] !== UPLOAD_ERR_OK) {
    echo json_encode(['error' => 'Error al subir archivo']);
    exit;
}
/*
// Validar tipo
$permitidos = ['image/jpeg', 'image/png', 'image/webp'];
if (!in_array($archivo['type'], $permitidos)) {
    echo json_encode(['error' => 'Tipo no permitido']);
    exit;
}*/

// Carpeta destino
$destino = 'uploads/';
if (!is_dir($destino)) {
    mkdir($destino, 0777, true);
}

$nombre_base = basename($archivo['name']); //nombre completo
$nombre = substr($nombre_base, 0,strrpos($nombre_base, '.'));
$ext = substr($nombre_base, strrpos($nombre_base, '.') + 1);
date_default_timezone_set("America/Mexico_City");
$fecha_actual = date("Y-m-d \hHis");
// Nombre seguro
//$nuevo_nombre_completo = uniqid() . '_' . basename($archivo['name']);
$nuevo_nombre_completo = $nombre.'_('.$fecha_actual.').'.$ext;
$ruta = $destino . $nuevo_nombre_completo;

move_uploaded_file($archivo['tmp_name'], $ruta);

echo json_encode([
    'ok' => true,
    'archivo' => $nombre
]);
?>