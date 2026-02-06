<?php

//EN ESTA PAGINA DE LOGIN VALIDAMOS SI LA IP ESTA REGISTRADA PARA ACCESO A UN USUARIO, 
//DE SER ASI SE REDIRECCIONA AUTOMATICAMENTE AL CONTENIDO CON EL ESTATUS DE LOGUEADO 
//Y LOS DATOS OBTENIDOS DEL USUARIO QUE TIENE ESTA IP


//session_start();

?>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Login</title>
</head>
<body>
     <form action="?controller=loginUser" method="post">
        <label>Usuario:</label>
        <input type="text" name="username" required>

        <br><br>

        <label>Contraseña:</label>
        <input type="password" name="password" required>

        <br><br>

        <button type="submit">Entrar</button>
    </form>
</body>
</html>