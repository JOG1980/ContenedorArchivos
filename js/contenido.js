$(function () {


    initFuncionesGenerales();
    initJSTree();
    initButtonsOn();


    // inicial
    //inicializarFileInput();

    cargarContenido('');

    initCanvasToDragAndDrop();
    initMenuContenedor();
    //ocultamos la animacion de carga
    loader(false);

});



//funcion de la animacion del cargador ---------------------------------------------------------------
function loader(show) {
    if (show)
        $("#wait").css("display", "block");
    else
        $("#wait").css("display", "none");
}


//inicializamos el arbol---------------------------------------------------------------------------------
function initJSTree() {


    $('#jstree').jstree({
        core: {
            multiple: false,
            // data: JSON.parse(contenido_tree)
        }
    })
        //inicializa abierto el primer nivel de items
        // on('ready.jstree', function () {
        //     let tree = $(this).jstree(true);
        //     tree.get_container().find('> ul > li').each(function () {
        //         tree.open_node(this);
        //     });
        // })
        //define que el nodo abra cuando es seleccionado el nombre del itemen el arbol
        .on('select_node.jstree', function (e, data) {
            $(this).jstree('open_node', data.node);
        })
        //se ejecuta cuando el nodo tiene un cambio de estado
        //cargamos el contenido de la ruta seleccionada
        .on('changed.jstree', function (e, data) {
            if (data.selected.length) {
                let nodeId = data.selected[0]; // ID del nodo seleccionado
                let nodeData = data.instance.get_node(nodeId); // Obtiene todos los datos del nodo
                //console.log("Nodo seleccionado:", nodeData);

                // Llama a una función para construir la ruta
                //let ruta = 'contenido\\' + construirRutaJSTreePorNodeId(nodeId, data.instance);
                let ruta = '\\' + construirRutaJSTreePorNodeId(nodeId, data.instance);

                $('#hd_ruta_actual').val(ruta);
                //console.log("Ruta generada:", ruta);
                cargarContenido(ruta);
            }
        });

    //cargamos por primera vez el jstree
    let ruta_actual = $("#hd_ruta_actual").val();
    cargarJSTree(ruta_actual);

}



//--Cargar arbol de carpetas --------------------------------------------------------
function cargarJSTree(ruta_actual) {
    loader(true);
    const obj = new Object();
    obj.ruta = ruta_actual;
    let obj_json = JSON.stringify(obj);
    $.ajax({
        type: "POST",
        url: './getCarpetas.php',
        async: false,		// llamada de tipo asincrona
        cache: false,
        //data: $(this).serialize(),
        dataType: 'text',        // el tipo de información que se espera de respuesta
        data: { 'datos': obj_json },
        success: function (data, status, xhr) //(response)
        {
            let obj = JSON.parse(data);
            let items = obj.items;

            //actuaizamos los datos del jstree
            let jstree = $('#jstree').jstree(true);

            jstree.settings.core.data = items;
            jstree.refresh();

            //ocultamos el loader
            loader(false);

        },
        error: function (jqXhr, textStatus, errorMessage) {
            //$("#my_id1").append('Error' + errorMessage);
            alert('Error: ' + errorMessage);
            loader(false);

        },
        // código a ejecutar sin importar si la petición falló o no
        complete: function (xhr, status) {
            //alert('COMPLETO');
            //ocultamos la animacion de carga
            loader(false);
        }
    });
}//end 



//construye la ruta (de reversa) de el nodo seleccionado ---------------------------------------------
function construirRutaJSTreePorNodeId(nodeId, jstreeInstance) {
    let ruta = [];
    let currentNode = jstreeInstance.get_node(nodeId);

    // Mientras no sea el nodo raíz (o el ID padre sea '0' o null)
    while (currentNode && currentNode.parent && currentNode.parent !== '#') {
        ruta.push(currentNode.text); // Añade el nombre del nodo actual
        currentNode = jstreeInstance.get_node(currentNode.parent); // Sube al padre
    }
    if (currentNode) {
        ruta.push(currentNode.text); // Añade la raíz
    }

    // Invierte el array y une con el separador deseado (ej. '/')
    //return ruta.reverse().join('/');
    return ruta.reverse().join('\\');
}


//inicializa el datatable --------------------------------------------------------------------------
function initDataTable() {
    $('#table_items_view').DataTable({
        paging: false,
        info: false,
        lengthChange: false,
        searching: false,
        language: {
            emptyTable: "No hay archivos"
        }

    });//datatable_es-ES
}

//--Cargar archivos de la ruta especificada --------------------------------------------------------
function downloadFiles(rutas) {

    const obj = new Object();
    obj.rutas = rutas;

    if (obj.rutas.length == 0) {
        alert("No hay archivos seleccionados.");
        return;
    }
    let obj_json = JSON.stringify(obj);
    $.ajax({
        url: './generateDownloadFiles.php',
        method: "POST",
        data: { 'datos': obj_json },
        success: function (data, status, xhr) //(response)
        {
            let obj = JSON.parse(data);
            let nombre_archivo_zip = obj.nombre_archivo_zip;
            let file_exist = obj.file_exist;

            if (file_exist == 'OK') {
                console.log('Enviado correctamente');
                window.location.href = 'downloadFiles.php?nombre_archivo_zip=' + nombre_archivo_zip;
            }
            else {
                alert("La generacion del archivo fallo");
            }
        }
    });

}//end 


//--Cargar archivos de la ruta especificada --------------------------------------------------------
function cargarContenido(ruta) {
    loader(true);
    const obj = new Object();
    obj.ruta = ruta;
    let obj_json = JSON.stringify(obj);
    $.ajax({
        type: "POST",
        url: './getContenido.php',
        async: true,		// llamada de tipo asincrona
        cache: false,
        //data: $(this).serialize(),
        dataType: 'text',        // el tipo de información que se espera de respuesta
        data: { 'datos': obj_json },
        success: function (data, status, xhr) //(response)
        {
            let obj = JSON.parse(data);
            let items = obj.items;

            let current_vista = $('#hd_vista_contenido').val();
            let contenido = '';
            let contenedor_items = $("#contenedor_items");
            if (current_vista == 'iconos') {
                armarResultadosContenidoIconos(contenedor_items, items);
            }
            else if (current_vista == 'detalles') {
                armarResultadosContenidoDetalles(contenedor_items, items);
            }

            //ajustamos los margenes del contenedor para mostrar mejor el contenido
            ajustarCSSContenedorItems(current_vista);

            //let ruta_barra = $("#hd_ruta_actual").val().replace("contenido","");
            let ruta_barra = $("#hd_ruta_actual").val();

            $("#barra_ruta").val(ruta_barra);
            //ocultamos el loader
            loader(false);

        },
        error: function (jqXhr, textStatus, errorMessage) {
            //$("#my_id1").append('Error' + errorMessage);
            alert('Error: ' + errorMessage);
            loader(false);

        },
        // código a ejecutar sin importar si la petición falló o no
        complete: function (xhr, status) {
            //alert('COMPLETO');
            //ocultamos la animacion de carga
            loader(false);
        }
    });
}//end 



//arma resultados en html---------------------------------------------------------------
function armarResultadosContenidoIconos(contenedor_items, items) {
    let contenido_items = "";
    let contador = 1;
    for (i = 0; i < items.length; i++) {

        //let id = items[i].id;
        let text = items[i].text;
        let tipo = items[i].tipo;

        let ruta = items[i].ruta;
        let ruta_completa = items[i].ruta_completa;
        //ruta = ruta.replace("\\", "/");
        if (contador == 1) {
            contenido_items += "<div class='row'>";
        }

        if (tipo == 'file') {
            // let size = items[i].size;
            // let fecha_creacion = items[i].fecha_creacion;
            // let fecha_modificacion = items[i].fecha_modificacion;
            let ext = items[i].ext;
            let image_name = "file_extension_" + ext + ".png";
            contenido_items += " <div class='col-3'>"
                + "<div class='item_icon_view' style='display: inline-block;' RUTA='" + ruta + "' tipo='file' >"
                + "<a href='" + ruta_completa + "' target='_blank'>"
                + "<img src='./images/32x32/" + image_name + "' />" + text
                + "</a></div>"
                + "</div>";
        }
        else {
            contenido_items += " <div class='col-3'>"
                + "<div class='item_icon_view mostrarContenidoCarpeta' style='display: inline-block;' RUTA='" + ruta + "' tipo='folder' >"
                + "<a href='#'>"
                + "<img src='./images/32x32/folder.png' />" + text
                + "</a></div>"
                + "</div>";
        }

        contador++;
        //como el contador se incrementa se evalua con uno mas
        if (contador == 5) {
            contenido_items += "</div>";
            contador = 1;
        }

    }
    contenedor_items.html(contenido_items);

}

//esta funcion arma la estructura html para la vista de detalle ----------------------------------------------------------------------------------------------------------------------------
function armarResultadosContenidoDetalles(contenedor_items, items) {
    let contenido_items = "<table id='table_items_view' class='table' style='width: 100%;'>"
        + "<thead  class='table-light'><tr><th><input type='checkbox' id='all_checkbox'></th><th>Nombre</th><th>Tamaño</th><th>Tipo</th><th>Fecha Creación</th><th>Fecha Modificación</th></tr></thead><tbody>";
    let contador = 1;
    for (i = 0; i < items.length; i++) {

        //let id = items[i].id;
        let text = items[i].text;
        let tipo = items[i].tipo;

        let ruta = items[i].ruta;
        let ruta_completa = items[i].ruta_completa;

        if (tipo == 'file') {
            let fsize = items[i].fsize;
            let mime_type = items[i].mime_type;
            let fecha_cre = items[i].fecha_cre;
            let fecha_mod = items[i].fecha_mod;
            let ext = items[i].ext;
            let image_name = "file_extension_" + ext + ".png";
            contenido_items += "<tr TIPO='file'><td><input type='checkbox' name='checkbox_" + contador + "'></td>"
                + "<td>"
                + "<div class='item_icon_detail' style='display: fit-content;' RUTA='" + ruta + "' tipo='file'>"
                + "<a href='" + ruta_completa + "' target='_blank'>"
                + "<img src='./images/32x32/" + image_name + "' />" + text
                + "</a>"
                + "</div></td>"
                + "<td>" + fsize + " KB</td>"
                + "<td>" + mime_type + "</td>"
                + "<td>" + fecha_cre + "</td>"
                + "<td>" + fecha_mod + "</td>"
                + "</tr>";
        }
        else {
            contenido_items += "<tr  TIPO='folder'><td><input type='checkbox' name='checkbox_" + contador + "'></td>"
                + "<td>"
                + "<div class='item_icon_detail mostrarContenidoCarpeta' style='display: inline-block;' RUTA='" + ruta + "' tipo='folder'>"
                + "<a href='#'>"
                + "<img src='./images/32x32/folder.png' />" + text
                + "</a>"
                + "</div></td>"
                + "<td>&nbsp;</td>"
                + "<td>&nbsp;</td>"
                + "<td>&nbsp;</td>"
                + "<td>&nbsp;</td>"
                + "</tr>";
        }

        contador++;


    }

    contenido_items += "</tbody></table>";
    contenedor_items.html(contenido_items);
    initDataTable();
}



//actualiza el elemento del arbol seleccionado ---------------------------------------------
function actualizarElementoArbolPorSeleccion(texto) {

    //texto = texto.replace("contenido\\", "");
    //texto = texto.replace(/contenido\\/, "");
    let id = obtenerIdPorRutaTexto('jstree', texto);
    // $('#jstree').jstree(true).close_all(); 
    $('#jstree').jstree(true).deselect_all();  //deseleccionamos todos los nodos del arbol
    $('#jstree').jstree(true).select_node(id); //seleccionamos el nodo correspondiente al nivel de archivos

}


//BOTONES ON ------------------------------------------------------------------------
//Etas funciones son ocupadas para elementos que se generan dinamicamente despues de la carga del ready, 
//por ejemplo cuando se generan nuevos elementos de botones y se necesita ligar un evento

function initButtonsOn() {

    $(document).on("click", ".mostrarContenidoCarpeta", function () {
        let ruta_folder = $(this).attr('RUTA');
        //let tree_id = $(this).attr('TREE_ID');
        $('#hd_ruta_actual').val(ruta_folder); //guardamos la ruta de archivos seleccionada
        actualizarElementoArbolPorSeleccion(ruta_folder);
        cargarContenido(ruta_folder);

    });



    $(document).on("click", "#btn_cargar_carpetas", function () {
        let rura_carpeta_contenedor = $("#hd_ruta_contenedor").val();

        cargarArbolCarpetas(rura_carpeta_contenedor);

    });


    //boton inicio --------
    $(document).on("click", "#btn_home_file", function (event) {
        $('#hd_ruta_actual').val('');
        cargarContenido('');
        $('#jstree').jstree(true).close_all();
        $('#jstree').jstree(true).deselect_all();  //deseleccionamos todos los nodos del arbol
    });


    //boton Expandir y contaer folders --------
    $(document).on("click", "#btn_folders_show", function (event) {
        var tree = $('#jstree').jstree(true);
        //buscamos si hay nodos abiertos
        let hay_nodos_abiertos = false;
        tree.get_json('#', { flat: true }).forEach(function (node) {
            if (node.state && node.state.opened) {
                hay_nodos_abiertos = true;
            }
        });
        //si hay algun nodo abierto, los cerramos
        if (hay_nodos_abiertos) {
            tree.close_all();
            tree.deselect_all();
            //cargarContenido('contenido');
        }
        else {//si no hay nada abierto, los abrimos
            tree.open_all();
        }
    });




    //boton vista icono-----
    $(document).on("click", "#btn_view_icons", function (event) {

        $('#hd_vista_contenido').val('iconos');
        let ruta_folder = $('#hd_ruta_actual').val();
        cargarContenido(ruta_folder);
    });

    //boton vista detalles-----
    $(document).on("click", "#btn_view_details", function (event) {
        $('#hd_vista_contenido').val('detalles');
        let ruta_folder = $('#hd_ruta_actual').val();
        cargarContenido(ruta_folder);

    });

    //subir de nivel-----
    $(document).on("click", "#btn_up_level", function (event) {
        let ruta_actual = $('#hd_ruta_actual').val();
        let pos = ruta_actual.lastIndexOf('\\');

        if (pos >= 0) {
            let ruta_up = ruta_actual.substring(0, pos);

            if (ruta_up == '') {
                $('#hd_ruta_actual').val('');
            }

            actualizarElementoArbolPorSeleccion(ruta_up);
            cargarContenido(ruta_up);
        }

    });




    //boton vista detalles---------------------------------
    $(document).on("click", "#btn_download_file", function (event) {

        let rutas = [];
        $('input[name^="checkbox_"]:checked').each(function () {
            let ruta = $(this).parent().parent().find('div').attr('RUTA');

            rutas.push(ruta); //agregamos en el arreglo la ruta cada checkbox seleccionado
        });

        downloadFiles(rutas);

    });


    //chbox de todos los checkbox ---------------------------
    $(document).on('change', '#all_checkbox', function () {
        if ($(this).is(':checked')) {
            $("input[name^='checkbox_']").prop('checked', this.checked);

        } else {

            $("input[name^='checkbox_']").prop('checked', '');
        }
    });


    $(document).on("click", "#btn_folder_add , #btn_folder_delete , #btn_folder_renombrar , #btn_file_delete , #btn_file_renombrar", function (e) {

    
        let ruta_actual = $("#hd_ruta_actual").val(); 
        alert($(this).attr('id') +" --- "+ruta_actual);
    });




}

//obtenemos  la ruta de carpetas de un nodo del arbol ----------------------------------
function obtenerIdPorRutaTexto(treeId, ruta) {

    //remplazamos la primera ocurrencia de \\ ya que al hacer el split si existe este caracter 
    // lo identifica como un elemento mas del arreglo al inicio
    ruta = ruta.replace("\\", "");

    const tree = $('#' + treeId).jstree(true);
    const partes = ruta.split('\\').map(p => p.trim());
    let nodoActual = '#'; // raíz
    for (let texto of partes) {
        let hijos = tree.get_children_dom(nodoActual);
        let encontrado = null;

        hijos.each(function () {
            const node = tree.get_node(this);
            if (node.text === texto) {
                encontrado = node.id;
                return false;
            }
        });

        if (!encontrado) return null;
        nodoActual = encontrado;
    }

    return nodoActual;
}



//inicializamos la zona de recepcion del drag & drop de archivos ----------------
function initCanvasToDragAndDrop() {
    //const canvas = document.getElementById("myCanvas");
    const canvas = document.getElementById("contenedor_items"); //al parecer no funciona con jquery

    //const canvas = $("#contenedor_items");
    let archivo = null;

    // Permitir drop
    canvas.addEventListener("dragover", e => e.preventDefault());

    // Capturar archivo
    // canvas.addEventListener("drop", e => {
    //     e.preventDefault();
    //     archivo = e.dataTransfer.files[0];

    //     if (!archivo) return;

    //     if (!archivo.type.startsWith("image/")) {
    //         alert("Solo imágenes");
    //         archivo = null;
    //         return;
    //     }

    //     // Mostrar imagen en canvas
    //     const reader = new FileReader();
    //     reader.onload = e => {
    //         const img = new Image();
    //         img.onload = () => {
    //             const ctx = canvas.getContext("2d");
    //             ctx.clearRect(0, 0, canvas.width, canvas.height);
    //             ctx.drawImage(img, 0, 0, canvas.width, canvas.height);
    //         };
    //         img.src = e.target.result;
    //     };
    //     reader.readAsDataURL(archivo);
    // });

    canvas.addEventListener("drop", e => {
        e.preventDefault();
        let archivos = e.dataTransfer.files;


        for (let archivo of archivos) {
            if (!archivo) return;
            enviarArchivoServidor(archivo);
        }
    });


}

// Enviar al backend (servidor) -------------------------------------------------------
function enviarArchivoServidor(archivo) {
    if (!archivo) {
        alert("No hay archivo");
        return;
    }

    const formData = new FormData();
    formData.append("archivo", archivo);

    fetch("upload.php", {
        method: "POST",
        body: formData
    })
        .then(r => r.json())
        .then(resp => console.log(resp))
        .catch(err => console.error(err));
}




//OTRAS FUNCIONES ---------------------------------------------------------------------------------------------------------------------------------

//ajustamos los margenes del contenedor para mostrar mejor el contenido
function ajustarCSSContenedorItems(current_vista) {
    if (current_vista == 'iconos') {

        $("#contenedor_items").addClass("contenedor_items_icons");
        $("#contenedor_items").removeClass("contenedor_items_details");
    }
    else {
        $("#contenedor_items").addClass("contenedor_items_details");
        $("#contenedor_items").removeClass("contenedor_items_icons");
    }

}

//funcion click en toda la pagina ----------------------------------
function initFuncionesGenerales() {
    $(document).on('click', function (e) {
        // console.log('Click en la página');
        console.log('Elemento clickeado:', e.target);
        ocultarMenuContenedor();
    });

}


function initMenuContenedor() {

    const menu_contenedor = document.getElementById('menu_contenedor');

    //ocultarMenuContenedor();
    // document.addEventListener('mousemove', (e) => {
    // tooltip.style.left = (e.clientX + 10) + 'px';
    // tooltip.style.top  = (e.clientY + 10) + 'px';
    // });


    /*  $("#contenedor_items").on("contextmenu", function (e) {
         e.preventDefault(); // evita el menú contextual del navegador
         alert("Clic derecho detectado");
         menu_contenedor.style.left = (e.clientX) + 'px';
         menu_contenedor.style.top  = (e.clientY) + 'px';
 mostrarMenuContenedor();
 
     });*/

    //se capturan los eventos del boton derecho delmouse para los items y para el contenedor
    $(document).on("contextmenu", "#contenedor_items, .item_icon_view, .item_icon_detail", function (e) {


        e.preventDefault(); // evita el menú contextual del navegador
        //alert("Clic derecho detectado item"+ $(this).attr('RUTA'));
        let tipo = $(this).attr('tipo');
        menu_contenedor.style.left = (e.clientX) + 'px';
        menu_contenedor.style.top = (e.clientY) + 'px';

        //si ya se hizo un click con el boton derecho
        $is_item_prev_click_derecho = $("#hd_item_prev_click_derecho").val();

        if ($is_item_prev_click_derecho == 'false') {

            //obtenemos la ruta para saber a quien se hizo clic
            let ruta = $(this).attr('ruta');
            $("#hd_ruta_actual").val(ruta);
            
            //mostramos el menu del boton derecho del mouse
            mostrarMenuContenedor(tipo);

            if (tipo == 'file' || tipo == 'folder')
                $("#hd_item_prev_click_derecho").val('true');
        }
        else {
            $("#hd_item_prev_click_derecho").val('false');
        }


    });

}

function mostrarMenuContenedor(tipo) {


   

    if (tipo == 'file') {
        $("#btn_folder_add").parent().css('display', 'none');
        $("#btn_folder_delete").parent().css('display', 'none');
        $("#btn_folder_renombrar").parent().css('display', 'none');
        $("#btn_file_delete").parent().css('display', 'block');
        $("#btn_file_renombrar").parent().css('display', 'block');

    }
    else if (tipo == 'folder') {
        $("#btn_folder_add").parent().css('display', 'none');
        $("#btn_folder_delete").parent().css('display', 'block');
        $("#btn_folder_renombrar").parent().css('display', 'block');
        $("#btn_file_delete").parent().css('display', 'none');
        $("#btn_file_renombrar").parent().css('display', 'none');
    }
    else {
        $("#btn_folder_add").parent().css('display', 'block');
        $("#btn_folder_delete").parent().css('display', 'none');
        $("#btn_folder_renombrar").parent().css('display', 'none');
        $("#btn_file_delete").parent().css('display', 'none');
        $("#btn_file_renombrar").parent().css('display', 'none');

    }

     $('#menu_contenedor').css('display', 'block');

}

function ocultarMenuContenedor() {
    $('#menu_contenedor').css('display', 'none');
}