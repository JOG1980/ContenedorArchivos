
document.oncontextmenu = function () { return false; };


//mostramos el cargador
showLoader();

var _primera_vez = "false";
var _tipo_rol = "";


var _tipo_vista = "icons";
var _habilitar_mensaje_bienvenida = "False";

var archivo_id_seleccinado = 0;
var carpeta_id_seleccinado = 0;
var MouseCursorPosition = { x: -1, y: -1 };

var _tamano_canvas = 0;

var _no_cache = "";

var _file_ext_16_array = "";
var _file_ext_32_array = "";

$(document).ready(function () {

    //inicializamos variables y componentes-----

    initVariables();
    initComponents();
    initFirstTime();

    //solucionamos referencias del diagrama de arbol -------------
    solucionarReferenciasCarpetas();
    solucionarReferenciasArchivos();

    //definimos evento del movimiento y pociciones del mouse para 
    $(document).mousemove(function (event) {
        MouseCursorPosition.x = event.pageX;
        MouseCursorPosition.y = event.pageY;
    });


    //eventos del clic y liberacion del del mouse -------------------
    $(".class_sel_op")
        .mouseenter(function () {
            $(this).addClass("sel_op");
        })
        .mouseleave(function () {
            $(this).removeClass("sel_op");
        });


    $(document).click(function () {
        //cerramos todas las ventanas
        $("#dialogo_carpeta_opciones").dialog("close");
        $("#dialogo_interior_carpeta_view").dialog("close");
        $("#dialogo_archivo_opciones").dialog("close");

        $("#dialogo_click_derecho_carpeta_opciones").dialog("close");
        $("#dialogo_click_derecho_archivo_opciones").dialog("close");

    });



    //iconos de carpeta ---------------------------------------------------------------------------------
    /*$(document)
    .on('mousedown', '.icono_carpeta', function (event) {

    var cdiv = $(".sel_icono", this);
    var nom_carpeta = cdiv.data("filepath");


    //boton izquierdo del mouse 8click normal)
    if (event.which == 1) {
    listarArchivosContenidosPorIconos(nom_carpeta);
    }
    //boton derecho del mouse 8menu contextual)
    else if (event.which == 3) {
    mostrarDialogoOpcionesCarpetaClickDerecho(nom_carpeta);
    }

    event.stopPropagation();
    return false;

    })
    .on('mouseover', '.icono_carpeta', function (event) {
    var cdiv = $(".sel_icono", this);
    cdiv.css("border", "1px solid #70C0E7");
    cdiv.css("background-color", "#E5F3FB");
    event.stopPropagation();
    return false;
    })
    .on('mouseleave', '.icono_carpeta', function (event) {
    var cdiv = $(".sel_icono", this);
    cdiv.css("border", "");
    cdiv.css("background-color", "");
    event.stopPropagation();
    return false;
    });*/

    $(document)
        .on('mousedown', '.icono_carpeta .sel_icono', function (event) {

            var cdiv = $(this);
            var nom_carpeta = cdiv.data("filepath");


            //boton izquierdo del mouse 8click normal)
            if (event.which == 1) {
                listarArchivosContenidosPorIconos(nom_carpeta);
            }
            //boton derecho del mouse 8menu contextual)
            else if (event.which == 3) {
                mostrarDialogoOpcionesCarpetaClickDerecho(nom_carpeta);
            }

            event.stopPropagation();
            return false;

        })
        .on('mouseover', '.icono_carpeta .sel_icono', function (event) {
            var cdiv = $(this);
            cdiv.css("border", "1px solid #70C0E7");
            cdiv.css("background-color", "#E5F3FB");
            event.stopPropagation();
            return false;
        })
        .on('mouseleave', '.icono_carpeta .sel_icono', function (event) {
            var cdiv = $(this);
            cdiv.css("border", "");
            cdiv.css("background-color", "");
            event.stopPropagation();
            return false;
        });



    //iconos de archivo ---------------------------------------------------------------------------------
    /*$(document).on('mousedown', '.icono_archivo', function (event) {
    if (event.which == 3) {

    var a_href = $(this).find("a").attr("href");
    var path_ref = a_href.split("?");
    var fullpath = path_ref[0];
    var fd = fullpath.indexOf("/");
    var ld = fullpath.lastIndexOf("/");
    var path = fullpath.substring(fd);
    var file = fullpath.substring(ld + 1);
    mostrarDialogoOpcionesArchivoClickDerecho(path, file);
    }
    event.stopPropagation();
    return false;
    })
    .on('mouseover', '.icono_archivo', function (event) {
    var cdiv = $(".sel_icono", this);
    cdiv.css("border", "1px solid #70C0E7");
    cdiv.css("background-color", "#E5F3FB");
    event.stopPropagation();
    })
    .on('mouseleave', '.icono_archivo', function (event) {
    var cdiv = $(".sel_icono", this);
    cdiv.css("border", "");
    cdiv.css("background-color", "");
    event.stopPropagation();
    });
    */
    $(document).on('mousedown', '.icono_archivo .sel_icono', function (event) {
        if (event.which == 3) {

            var a_href = $(this).find("a").attr("href");
            var path_ref = a_href.split("?");
            var fullpath = path_ref[0];
            var fd = fullpath.indexOf("/");
            var ld = fullpath.lastIndexOf("/");
            var path = fullpath.substring(fd);
            var file = fullpath.substring(ld + 1);
            mostrarDialogoOpcionesArchivoClickDerecho(path, file);
        }
        event.stopPropagation();
        return false;
    })
        .on('mouseover', '.icono_archivo .sel_icono', function (event) {
            var cdiv = $(this);
            cdiv.css("border", "1px solid #70C0E7");
            cdiv.css("background-color", "#E5F3FB");
            event.stopPropagation();
        })
        .on('mouseleave', '.icono_archivo .sel_icono', function (event) {
            var cdiv = $(this);
            cdiv.css("border", "");
            cdiv.css("background-color", "");
            event.stopPropagation();
        });



    //menu sobre las vistas---------------------------------------------------------------------------------
    $(document).on('mousedown', '#div_vista_contenedor', function (event) {

        if (event.which == 3) {
            var carpeta_actual = $("[id$='HiddenField_NombreCarpeta']").val();
            mostrarDialogoOpcionesCarpetaMin(carpeta_actual);

        }
        event.stopPropagation();
        return false;
    })
        .on('mouseover', '.icono_archivo', function (event) {
            event.stopPropagation();
        })
        .on('mouseleave', '.icono_archivo', function (event) {
            event.stopPropagation();
        });


    //diagrama de arbol resaizable
    /*$("#TreeDiagram").resizable({
    resize: function (event, ui) {
    //en cada evento de redimensionamiento justamos el width y height al tamaño requerido por treeview para visualizarse correctamente
    var div_treeview_min_height = $("[id$='TreeView1']").height() + 300; //el 30 es el offset producido por los bnotones
    var div_treeview_min_width = $("[id$='TreeView1']").width();
    ui.size.height = div_treeview_min_height;

    }
    });*/



    $("#TreeDiagram").resize(function () {
        var tree_view_width = $(this).width();
        var div_vista_new_width = (_tamano_canvas - tree_view_width) - 10;
        $("#div_vista_contenedor").width(div_vista_new_width);
        //$("#Text_Vista_CarpetaActual").width(div_vista_new_width - 130);

        var tree_view_height = $(this).height();
        var vista_height = $("#div_vista_contenedor").height();
        //alert(_tipo_vista + " :: tree_view_width: " + tree_view_width + " , tree_view_height" + tree_view_height);
        if (_tipo_vista != "tree") {
            if (tree_view_height > vista_height) {
                $(this).css("border-right", "1px solid #AAAAAA");
                $("#div_vista_contenedor").css("border-left", "");

            }
            else {
                $(this).css("border-right", "");
                $("#div_vista_contenedor").css("border-left", "1px solid #AAAAAA");
            }
        }

        var div_vista_new_height = $("#TreeDiagram").height();
        $("#div_vista_contenedor").css("min-height", div_vista_new_height);


    });




    $("#dialogo_privacidad").dialog({
        autoOpen: false,
        resizable: false,
        width: 550,
        height: 350,
        modal: true,
        closeOnEscape: true,
        draggable: false,
        open: function (type, data) {
            $(".ui-dialog-titlebar").show();
        },

        buttons: {
            "Aceptar": function () {
                $(this).dialog("close");
            }
        }
    });




    $("#dialogo_carpeta_opciones").dialog({
        autoOpen: false,
        resizable: false,
        width: 80,
        height: 100,
        modal: false,
        closeOnEscape: true,
        draggable: false,
        open: function (type, data) {
            $(".ui-dialog-titlebar").hide();
            $(".ui-dialog .ui-dialog-content").css("padding", "0em 0em");
            //$(".ui-dialog .ui-dialog-content​").css("background", "yellow");
            
        }
    });

    $("#dialogo_interior_carpeta_view").dialog({
        autoOpen: false,
        resizable: false,
        width: 80,
        height: 50,
        modal: false,
        closeOnEscape: true,
        draggable: false,
        open: function (type, data) {
            $(".ui-dialog-titlebar").hide();
            $(".ui-dialog .ui-dialog-content").css("padding", "0em 0em");
        }
    });


    $("#dialogo_archivo_opciones").dialog({
        autoOpen: false,
        resizable: false,
        width: 20,
        height: 200,
        modal: false,
        closeOnEscape: true,
        draggable: false,
        open: function (type, data) {
            $(".ui-dialog-titlebar").hide();
            $(".ui-dialog .ui-dialog-content").css("padding", "0em 0em");
        }
    });


    $("#dialogo_click_derecho_carpeta_opciones").dialog({
        autoOpen: false,
        resizable: false,
        width: 80,
        height: 50,
        modal: false,
        closeOnEscape: true,
        draggable: false,
        open: function (type, data) {
            $(".ui-dialog-titlebar").hide();
            $(".ui-dialog .ui-dialog-content").css("padding", "0em 0em");
        }
    });


    $("#dialogo_click_derecho_archivo_opciones").dialog({
        autoOpen: false,
        resizable: false,
        width: 20,
        height: 200,
        modal: false,
        closeOnEscape: true,
        draggable: false,
        open: function (type, data) {
            $(".ui-dialog-titlebar").hide();
            $(".ui-dialog .ui-dialog-content").css("padding", "0em 0em");
        }
    });


    $("#dialogo_file_upload").dialog({
        autoOpen: false,
        resizable: false,
        width: 580,
        height: 280,
        modal: true,
        open: function (type, data) {
            $(this).parent().appendTo("form");
            $(".ui-dialog-titlebar").show();
            $(".ui-dialog .ui-dialog-content").css("padding", "");
        }
    });

    $("#dialogo_crear_carpeta").dialog({
        autoOpen: false,
        resizable: false,
        width: 540,
        height: 280,
        modal: true,
        open: function (type, data) {
            $(this).parent().appendTo("form");
            $(".ui-dialog-titlebar").show();
            $(".ui-dialog .ui-dialog-content").css("padding", "");
        }
    });


    $("#dialogo_borrar_carpeta").dialog({
        autoOpen: false,
        resizable: false,
        width: 310,
        height: 140,
        modal: true,
        open: function (type, data) {
            $(this).parent().appendTo("form");
            $(".ui-dialog-titlebar").show();
            $(".ui-dialog .ui-dialog-content").css("padding", "");
        }
    });

    $("#dialogo_renombrar_carpeta").dialog({
        autoOpen: false,
        resizable: false,
        width: 310,
        height: 160,
        modal: true,
        open: function (type, data) {
            $(this).parent().appendTo("form");
            $(".ui-dialog-titlebar").show();
            $(".ui-dialog .ui-dialog-content").css("padding", "");
        }
    });

    $("#dialogo_borrar_archivo").dialog({
        autoOpen: false,
        resizable: false,
        width: 400,
        height: 250,
        modal: true,
        open: function (type, data) {
            $(this).parent().appendTo("form");
            $(".ui-dialog-titlebar").show();
            $(".ui-dialog .ui-dialog-content").css("padding", "");
        }
    });


    $("#dialogo_renombrar_archivo").dialog({
        autoOpen: false,
        resizable: false,
        width: 400,
        height: 250,
        modal: true,
        open: function (type, data) {
            $(this).parent().appendTo("form");
            $(".ui-dialog-titlebar").show();
            $(".ui-dialog .ui-dialog-content").css("padding", "");
        }
    });

    $("#dialogo_editar_descripcion_carpeta").dialog({
        autoOpen: false,
        resizable: false,
        width: 580,
        height: 280,
        modal: true,
        open: function (type, data) {
            $(this).parent().appendTo("form");
            $(".ui-dialog-titlebar").show();
            $(".ui-dialog .ui-dialog-content").css("padding", "");
        }
    });

    $("#dialogo_editar_descripcion_archivo").dialog({
        autoOpen: false,
        resizable: false,
        width: 550,
        height: 280,
        modal: true,
        open: function (type, data) {
            $(this).parent().appendTo("form");
            $(".ui-dialog-titlebar").show();
            $(".ui-dialog .ui-dialog-content").css("padding", "");
        }
    });


    $("#dialogo_mensaje").dialog({
        autoOpen: false,
        resizable: false,
        width: 400,
        height: 250,
        modal: true,
        buttons: {
            "Aceptar": function () {
                $(this).dialog("close");
                $(".ui-dialog-titlebar").show();
            }
        }
    });


    $("#div_mensaje_bienvenida").dialog({
        autoOpen: false,
        resizable: false,
        modal: true,
        width: 450,
        buttons: {
            "Aceptar": function () {
                $(this).dialog("close");
                $(".ui-dialog-titlebar").show();
            }
        }
    });


    $(".myexpand").click(function () {
        actualizarTreeViewStatusExpand($(this));
    });




    $.datepicker.regional['es'] = {
        closeText: 'Cerrar',
        prevText: '<Ant',
        nextText: 'Sig>',
        currentText: 'Hoy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
        dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
        dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        weekHeader: 'Sm',
        dateFormat: 'dd/mm/yy',
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: ''
    };

    $.datepicker.setDefaults($.datepicker.regional['es']);
    $("[id$='TextBox_FechaArchivo'],[id$='TextBox_FechaCarpeta'],[id$='TextBox_EditarFechaArchivo'],[id$='TextBox_EditarFechaCarpeta']").datepicker({
        dateFormat: "yy-mm-dd",
        showOn: "button",
        buttonImage: "../Images/IconPack1/24x24/calendar_date.png",
        buttonImageOnly: true
    })
        .datepicker("setDate", "0"); //inicializamos el datepicker con la fecha de hoy (del sistema)

    //cambiamos el tamaño de la letra 
    $('#ui-datepicker-div').css('font-size', '10px');



    $(".imgclick").mousedown(function () {
        botonPresionado($(this));
    }).mouseup(function () {
        botonLiberado($(this));
    });


    crearDefinicionTableSorter();

    //cerramos el cargador
    hideLoader();

    finalLoad();

});         //fin del ready()


//funciones del cargador ----------------------------------------------------------------------------------
function showLoader() {
    $('body').waitMe({
        effect: 'ios',
        text: 'Please wait...',
        //bg: 'rgba(255,255,255,0.7)',
        bg: 'rgba(0,0,0,0.7)',
        color: '#000',
        sizeW: '',
        sizeH: ''
    });
}

function hideLoader() {
    $('body').waitMe('hide');
}



//inicializacion de variables  ----------------------------------------------------------------------------
function initVariables() {

    _primera_vez = $("[id$='HiddenField_FirstTime']").val();

    _tipo_rol = $("[id$='HiddenField_TipoRol']").val();

    _tipo_vista = $("[id$='HiddenField_TipoVista']").val();
    if (_tipo_vista == "") _tipo_vista = "icons";

    _tamano_canvas = $("#DivContenedorPrincipal").width();

    //obtenemos el 
    var d = new Date();
    _no_cache = "?_no_cache=" + d.getTime();

    //lista de extensiones
    var extensions = $("[id$='HiddenField_FileExtensions']").val();
    var extensions_array = extensions.split("|");

    _file_ext_16_array = extensions_array[0].split(",");
    _file_ext_32_array = extensions_array[1].split(",");
}

function initComponents() {

    //pasamos hasta el fente al menu
    $("li", $("#NavigationMenu")).css("z-index", "1000");

    //deseleccionamos tosos los elementos del treeview
    $("a", $("[id$='TreeView1']")).parent().css("border", "");

    if (_tipo_vista == "tree") {

        $("#tbl_vista_details").hide();
        $("#tbl_vista_icons").hide();
        $("#div_vista_contenedor").hide();

        $('#TreeDiagram').width("100%");
        $('#TreeDiagram').css("max-width", "900px");

        //inicializamos el estado de los botones de la vista
        $("#treeview_button_selected").show();
        $("#icons_button_selected").hide();
        $("#details_button_selected").hide();

        //ocualtamos la ruta de la carpeta
        $("#div_ruta_carpeta").hide();

    }
    else if (_tipo_vista == "icons") {
        var carpeta_actual = $("[id$='HiddenField_NombreCarpeta']").val();
        $('#Text_Vista_CarpetaActual').val(carpeta_actual);
        $("#tbl_vista_details").hide();
        $("#tbl_vista_icons").show();
        $("#div_vista_contenedor").show();

        $('#TreeDiagram').css("max-width", "790px");

        $("#TreeDiagram").resizable({});

        //inicializamos el estado de los botones de la vista
        $("#treeview_button_selected").hide();
        $("#icons_button_selected").show();
        $("#details_button_selected").hide();

        //mostramos la ruta de la carpeta
        $("#div_ruta_carpeta").show();

        listarArchivosContenidosPorIconos(carpeta_actual, "");
    }
    else if (_tipo_vista == "details") {
        var carpeta_actual = $("[id$='HiddenField_NombreCarpeta']").val();
        $('#Text_Vista_CarpetaActual').val(carpeta_actual);
        $("#tbl_vista_details").show();
        $("#tbl_vista_icons").hide();
        $("#div_vista_contenedor").show();

        $('#TreeDiagram').css("max-width", "500px");

        $("#TreeDiagram").resizable({});

        //inicializamos el estado de los botones de la vista
        $("#treeview_button_selected").hide();
        $("#icons_button_selected").hide();
        $("#details_button_selected").show();

        //mostramos la ruta de la carpeta
        $("#div_ruta_carpeta").show();

        listarArchivosContenidosPorDetalles(carpeta_actual);
    }

    //tamaño actual del treeview para calcular las vistas de iconos y detalles
    var tree_view_width = $('#TreeDiagram').width();

    var div_vista_new_width = (_tamano_canvas - tree_view_width) - 10;
    $("#div_vista_contenedor").width(div_vista_new_width);

    var div_vista_new_height = $("#TreeDiagram").height();
    $("#div_vista_contenedor").css("min-height", div_vista_new_height);

    //$("#Text_Vista_CarpetaActual").width(div_vista_new_width - 130); //redimensionamos el textbox de la ruta del archivo

    var tree_view_height = $(this).height();
    var vista_height = $("#div_vista_contenedor").height();

    if (_tipo_vista != "tree") {
        if (tree_view_height > vista_height) {
            $("#TreeDiagram").css("border-right", "1px solid #AAAAAA");
            $("#div_vista_contenedor").css("border-left", "");
        }
        else {
            $("#TreeDiagram").css("border-right", "");
            $("#div_vista_contenedor").css("border-left", "1px solid #AAAAAA");
        }
    }
}

//inicializacion por primera vez  ----------------------------------------------------------------------------
function initFirstTime() {
    if (_primera_vez == "True") {
        _habilitar_mensaje_bienvenida = $("[id$='HiddenField_HabilitarMensajeBienvenida']").val();
        if (_habilitar_mensaje_bienvenida == "") _habilitar_mensaje_bienvenida = "False";

    }
    else {

    }
}

//esta funcion se ejecuta al fnal de la carga ------------------------------------------------------------
function finalLoad() {
    if (_habilitar_mensaje_bienvenida == "True") {
        var titulo_mensaje_bienvenida = $("[id$='HiddenField_TituloMensajeBienvenida']").val();
        $("#div_mensaje_bienvenida").dialog("option", "title", titulo_mensaje_bienvenida);
        $("#div_mensaje_bienvenida").dialog('open');
    }
}

//cambiamos las referencias de los archivos ---------------------------------------------------------------
function solucionarReferenciasArchivos() {

    //solo se ejecuta si la vista es de arbol
    if (_tipo_vista == "tree") {
        var res = "";
        $("a[href^='javascript:mostrarDialogoOpcionesArchivo']").each(function () {

            var attributo_todo = $(this).attr("href");
            var atributos = attributo_todo.split("|");
            var nomfunc_js = atributos[0];  //nombre de la funcion
            var elem_id = atributos[1];     //id del elemento definido por un contador
            var arg1_js = atributos[2];     //ruta web relativa y nombre de archivo
            var arg2_js = atributos[3];     //nombre de archivo
            var fecha = atributos[4];       //fecha

            //si con tiene un icono de imagen
            if ($(this).find("img").length > 0) {
                arg1_js = arg1_js.replace(/'/g, "\\'");

                var funcion_js = nomfunc_js + "(" + elem_id + ",'" + arg1_js + "','" + arg2_js + "')";

                //agregamos un atributo con un valor. En este caso un segundo id
                $(this).attr("nid", elem_id);

                $(this).attr("fecha", fecha);

                //agregamos el link de la referencia
                $(this).attr("href", funcion_js);
            }
            else {
                var ref = "../Protegido/Contenedor" + arg1_js;
                $(this).attr("href", ref + _no_cache);
                $(this).attr("target", "_blank");
            }

        });
    } //end if
}


//cambiamos las referencias de los archivos ---------------------------------------------------------------
function solucionarReferenciasCarpetas() {


    var res = "";

    $("a[href^='javascript:mostrarDialogoOpcionesCarpeta']").each(function () {

        var attributo_todo = $(this).attr("href");

        var es_editable = true;
        var atributos = attributo_todo.split("|");
        var nomfunc_js = atributos[0];  //nombre de la funcion
        var elem_id = atributos[1];     //id del elemento definido por un contador
        var arg1_js = atributos[2];     //ruta web relativa y nombre de archivo
        var fecha = atributos[3];       //fecha

        //var id = $("a", this).parent().html();
        var id = $(this).parent().find("a").attr("id");


        if (typeof elem_id === "undefined") {
            es_editable = false;
        }

        if (es_editable) {
            //si con tiene un icono de imagen
            if ($(this).find("img").length > 0) {
                arg1_js = arg1_js.replace(/'/g, "\\'");
                var funcion_js = nomfunc_js + "(" + elem_id + ",'" + arg1_js + "')";


                //agregamos un atributo con un valor. En este caso un segundo id
                $(this).attr("nid", elem_id);

                $(this).attr("nameid", arg1_js);

                $(this).attr("fecha", fecha);

                //agregamos el link de la referencia
                $(this).attr("href", funcion_js);
            }
            else {

                //solo si es vista de arbol se muestra el menu minimo
                if (_tipo_vista == "tree") {
                 //var ref = "javascript:mostrarDialogoOpcionesCarpetaMin('" + arg1_js + "')";
                    var ref = "javascript:mostrarDialogoOpcionesCarpeta('" + arg1_js + "')";
                    $(this).attr("href", ref);
                    $(this).attr("target", "");
                }
                else if (_tipo_vista == "details") //de otra forma mandamos llamar a la funcion de archivos contenidos
                {
                    var ref = "javascript:listarArchivosContenidosPorDetalles('" + arg1_js + "','" + id + "')";
                    $(this).attr("href", ref);
                    $(this).attr("target", "");
                }
                else if (_tipo_vista == "icons") //de otra forma mandamos llamar a la funcion de archivos contenidos
                {
                    var ref = "javascript:listarArchivosContenidosPorIconos('" + arg1_js + "','" + id + "')";
                    $(this).attr("href", ref);
                    $(this).attr("target", "");
                }
            }

            //verificamos si es expandible
            var expand = $(this).parent().parent().find("td a img[alt^='E']");
            expand.each(function () {
                $(this).attr('expname', arg1_js);
                $(this).attr('expand', '0');
                $(this).addClass('myexpand');

            });
            //verificamos si es colapsable
            var collapse = $(this).parent().parent().find("td a img[alt^='C']");
            collapse.each(function () {
                $(this).attr('expname', arg1_js);
                $(this).attr('expand', '1');
                $(this).addClass('myexpand');
            });

        }

    });
}





function actualizarTreeViewStatusExpand(elem) {

    //como el elemento actual que llamo esta funcion esta en tracicion de cambiar de E->C o de C->E, 
    //al momento que se recorren todos los elementos, este se queda con su posicion antes de la transicion, 
    //entonces realmente tiene el valor opuesto al obtemnido en el recorrido de elementos

    //por lo tanto obtenemos el valor solo de este elemento y lo intercambioamos
    var cexpname = elem.attr("expname");
    cexpname = cexpname.replace(/'/g, "\\'");

    var cexpand = elem.attr("alt").substring(0, 1);

    var treeviewstatus_expand = "";

    $("[id$='HiddenField_NombreCarpeta']").val(cexpname);

    $(".myexpand").each(function () {

        var expname = $(this).attr("expname");
        expname = expname.replace(/'/g, "\\'");
        var expand = $(this).attr("alt").substring(0, 1);

        //aqui encontramos elelemento que mando llamar esta funcion, entonces invertimos los valores
        if (cexpname == expname) {
            if (cexpand == 'E') expand = 'C';
            else expand = 'E'
        }

        //se guardan los valores con formato json (de objetos)
        var objeto = '{' +
                         '"name":"' + expname + '",' +
                         '"status":"' + expand + '"' +
                         '}';

        treeviewstatus_expand += objeto + ",";
    });

    //quitamos la coma (,) del final
    treeviewstatus_expand = treeviewstatus_expand.substring(0, treeviewstatus_expand.length - 1);
    treeviewstatus_expand = "[" + treeviewstatus_expand + "]";

    var methodUrl = "Contenido.aspx/actualizarTreeViewStatusExpand";
    parameters = "{'treeviewstatus_expand': '" + treeviewstatus_expand + "'}";
    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: methodUrl,
        data: parameters,
        dataType: "json",
        async: false,
        success: function (response) {
            var data = response.d;

            console.log("ok: " + data);
        },
        error: function (response) {
            var data = response.d;
            //Mostrar msg
            console.log("Error: " + data);
        }
    });
}

function mostrarDialogoOpcionesArchivo(elem_id, ruta_archivo, nom_archivo) {
    
    //archivo seleccionado
    archivo_id_seleccinado = elem_id;

    $("[id$='HiddenField_NombreArchivo']").val(ruta_archivo + "|" + nom_archivo);
    $("#dialogo_archivo_opciones").dialog("open");
    $("#dialogo_archivo_opciones").dialog('option', 'width', 120);
    $("#dialogo_archivo_opciones").dialog('option', 'height', 50);

    $("[aria-describedby*='dialogo_archivo_opciones']").css("width", "120");

    var my_position = "left+" + MouseCursorPosition.x + " top+" + MouseCursorPosition.y;
    $("#dialogo_archivo_opciones").parent().position({
        my: my_position,
        at: "left top",
        of: $(document)
    });

}

function mostrarDialogoOpcionesArchivoClickDerecho(ruta_archivo, nom_archivo) {

    if (_tipo_rol == "Admin" || _tipo_rol == "SuperUser") {

        $("[id$='HiddenField_NombreArchivo']").val(ruta_archivo + "|" + nom_archivo);
        $("#dialogo_archivo_opciones").dialog("open");
        $("#dialogo_archivo_opciones").dialog('option', 'width', 120);
        $("#dialogo_archivo_opciones").dialog('option', 'height', 50);

        $("[aria-describedby*='dialogo_archivo_opciones']").css("width", "150");

        var my_position = "left+" + MouseCursorPosition.x + " top+" + MouseCursorPosition.y;
        $("#dialogo_archivo_opciones").parent().position({
            my: my_position,
            at: "left top",
            of: $(document)
        });
    }

    //$("#dialogo_archivo_opciones").focus();
}

function mostrarDialogoOpcionesCarpeta(elem_id, nombre_carpeta) {

    //archivo seleccionado
    carpeta_id_seleccinado = elem_id;

    $("[id$='HiddenField_NombreCarpeta']").val(nombre_carpeta);
    $("#dialogo_carpeta_opciones").dialog("open");
    $("#dialogo_carpeta_opciones").dialog('option', 'width', 120);
    $("#dialogo_carpeta_opciones").dialog('option', 'height', 110);

    $("[aria-describedby*='dialogo_carpeta_opciones']").css("width", "120");

    var my_position = "left+" + MouseCursorPosition.x + " top+" + MouseCursorPosition.y;
    $("#dialogo_carpeta_opciones").parent().position({
        my: my_position,
        at: "left top",
        of: $(document)
    });


}



function mostrarDialogoOpcionesCarpetaMin(nom_carpeta) {

    $("[id$='HiddenField_NombreCarpeta']").val(nom_carpeta);

    $("#dialogo_interior_carpeta_view").dialog("open");

    $("#dialogo_interior_carpeta_view").dialog('option', 'width', 120);
    $("#dialogo_interior_carpeta_view").dialog('option', 'height', 70);

    $("[aria-describedby*='dialogo_interior_carpeta_view']").css("width", "150");

    var my_position = "left+" + MouseCursorPosition.x + " top+" + MouseCursorPosition.y;
    $("#dialogo_interior_carpeta_view").parent().position({
        my: my_position,
        at: "left top",
        of: $(document)
    });
}


function mostrarDialogoOpcionesCarpetaClickDerecho(nombre_carpeta) {

    if (_tipo_rol == "Admin" || _tipo_rol == "SuperUser") {
        $("[id$='HiddenField_NombreCarpeta']").val(nombre_carpeta);
        $("#dialogo_click_derecho_carpeta_opciones").dialog("open");
        $("#dialogo_click_derecho_carpeta_opciones").dialog('option', 'width', 120);
        $("#dialogo_click_derecho_carpeta_opciones").dialog('option', 'height', 85);

        $("[aria-describedby*='dialogo_click_derecho_carpeta_opciones']").css("width", "120");

        var my_position = "left+" + MouseCursorPosition.x + " top+" + MouseCursorPosition.y;
        $("#dialogo_click_derecho_carpeta_opciones").parent().position({
            my: my_position,
            at: "left top",
            of: $(document)
        });
    }

    //$("#dialogo_click_derecho_carpeta_opciones").focus();

}





//funciones de los botones ------------------------------------------------------
function botonPresionado(cbutton) {
    var mrgtb = parseInt(cbutton.css("margin-top"));
    var mrglf = parseInt(cbutton.css("margin-left"));
    mrgtb = mrgtb + 2;
    mrglf = mrglf + 2;
    cbutton.css("margin-top", mrgtb + "px").css("margin-left", mrglf + "px");
    //$(".button_selected",cbutton.parent().parent()).show();
    //alert(">> " + cbutton.parent().parent().html());

}

function botonLiberado(cbutton) {
    var mrgtb = parseInt(cbutton.css("margin-top"));
    var mrglf = parseInt(cbutton.css("margin-left"));
    mrgtb = mrgtb - 2;
    mrglf = mrglf - 2;
    cbutton.css("margin-top", mrgtb + "px").css("margin-left", mrglf + "px");
    $(".button_selected", cbutton.parent().parent()).show();

}

//con esta funcion checamos que exista la extension de no ser haci da la default new ----------------
function obtenerCoincidenciaExtension16(ext) {
    for (var i = 0; i < _file_ext_16_array.length; i++) {
        if (_file_ext_16_array[i] == ext) {
            return ext;
        }
    }
    return "new";
}

function obtenerCoincidenciaExtension32(ext) {
    for (var i = 0; i < _file_ext_32_array.length; i++) {
        if (_file_ext_32_array[i] == ext) {

            return ext;
        }
    }
    return "new";
}





//lista los archivos por detalle -------------------------------------------------------------------
function listarArchivosContenidosPorDetalles(nombre_carpeta, current_selection_id) {

    //guardamos el nombre de la carpeta seleccionada
    $("[id$='HiddenField_NombreCarpeta']").val(nombre_carpeta);

    $("#Text_Vista_CarpetaActual").val(nombre_carpeta);

    //deseleccionamos tosos los elementos del treeview
    $("a", $("[id$='TreeView1']")).parent().css("border", "");
    //seleccionamos el elemento del treeview
    var current_selection = $("#" + current_selection_id);
    current_selection.parent().css("border", "1px solid #AAAAAA");

    var methodUrl = "Contenido.aspx/listarArchivosContenidos";
    parameters = "{'nombre_carpeta': '" + nombre_carpeta + "'}";
    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: methodUrl,
        data: parameters,
        dataType: "json",
        async: true,
        success: function (response) {


            var data = response.d;
            var dataResult = jQuery.parseJSON(data);
            var carpeta = dataResult.carpeta;

            //borramos el contenido. nos referimos a los archivos
            $("#tbl_vista_details tbody").html("");

            //recorremos todos los archivos para agregarlos a la tabla
            for (i = 0; i < carpeta.length; i++) {
                var renglon = "";

                var elem_carpeta = carpeta[i];
                // var nombre_completo = elem_carpeta.nombre_completo;
                var tipo = elem_carpeta.tipo;

                if (tipo == "carpeta") {
                    var tiene_contenido = elem_carpeta.tiene_contenido;

                    // var nombre_completo = elem_carpeta.nombre_completo;
                    var rutaweb = elem_carpeta.rutaweb;
                    var nombre = elem_carpeta.nombre;

                    //checamos que exista la extension del archivo actual, de no ser asi se asigna la generica new
                    var ext = obtenerCoincidenciaExtension16(elem_carpeta.ext);
                    //var ext = elem_carpeta.ext;

                    var ultima_mod = elem_carpeta.ultima_mod;
                    var ultima_mod_hora = ultima_mod.substring(0, 10);
                    var ultima_mod_min = ultima_mod.substring(11);

                    //descripcion------------------
                    var desc = elem_carpeta.desc;
                    var fecha_desc = elem_carpeta.fecha_desc;

                    var new_ruta_carpeta = nombre_carpeta + "/" + nombre;

                    renglon += "<tr  style='text-align: center;'>";
                    renglon += "<td>";
                    if (tiene_contenido == "True")
                        renglon += "<img class='iconos' style='cursor:pointer;' src='../Images/icons_ext/16/folder_vertical_document.png' onclick='listarArchivosContenidosPorDetalles(\"" + new_ruta_carpeta + "\" )' />";
                    else if (tiene_contenido == "False")
                        renglon += "<img class='iconos' style='cursor:pointer;' src='../Images/icons_ext/16/folder_vertical_open.png' onclick='listarArchivosContenidosPorDetalles(\"" + new_ruta_carpeta + "\" )' />";
                    renglon += "</td>";
                    renglon += "<td style='font-size: 10px;'>" + nombre + "</td>";
                    renglon += "<td style='font-size: 10px;'>" + ultima_mod + "</td>";
                    renglon += "<td style='font-size: 10px;'>&nbsp;</td>";
                    renglon += "<td style='font-size: 10px;'>" + desc + "</td>";
                    renglon += "</tr>";
                }
                else if (tipo == "archivo") {

                    // var nombre_completo = elem_carpeta.nombre_completo;
                    var rutaweb = elem_carpeta.rutaweb;
                    var nombre = elem_carpeta.nombre;

                    //checamos que exista la extension del archivo actual, de no ser asi se asigna la generica new
                    var ext = obtenerCoincidenciaExtension16(elem_carpeta.ext);
                    //var ext = elem_carpeta.ext;

                    var ultima_mod = elem_carpeta.ultima_mod;
                    var ultima_mod_hora = ultima_mod.substring(0, 10);
                    var ultima_mod_min = ultima_mod.substring(11);

                    var size = elem_carpeta.size;
                    var ssize = size.toString();
                    var cad_size = "";

                    //descripcion------------------
                    var desc = elem_carpeta.desc;
                    var fecha_desc = elem_carpeta.fecha_desc;

                    if (ssize.length > 6) {
                        cad_size = Math.floor((size / 1048576) * 10) / 10 + " MB";
                    }
                    else if (ssize.length > 3) {
                        cad_size = Math.floor((size / 1024) * 10) / 10 + " KB";
                    }
                    else {
                        cad_size = size + " Bytes";
                    }

                    renglon += "<tr  style='text-align: center;'>";
                    renglon += "<td><a href='" + rutaweb + _no_cache + "' target='_blank'><img src='../Images/icons_ext/16/file_extension_" + ext + ".png' /><a></td>";
                    renglon += "<td style='font-size: 10px;'>" + nombre + "</td>";
                    renglon += "<td style='font-size: 10px;'>" + ultima_mod + "</td>";
                    renglon += "<td style='font-size: 10px;'>" + cad_size + "</td>";
                    renglon += "<td style='font-size: 10px;'>" + desc + "</td>";
                    renglon += "</tr>";
                }

                $("#tbl_vista_details tbody").append(renglon);

            }


            //console.log("ok: " + dataResult);
        },
        error: function (response) {
            var data = response.d;
            //Mostrar msg
            console.log("Error: " + data);
        }
    });
}


//lista los archivos contenidos por icono --------------------------------------------
function listarArchivosContenidosPorIconos(nombre_carpeta, current_selection_id) {

    //guardamos el nombre de la carpeta seleccionada
    $("[id$='HiddenField_NombreCarpeta']").val(nombre_carpeta);

    $("#Text_Vista_CarpetaActual").val(nombre_carpeta);

    //deseleccionamos tosos los elementos del treeview
    $("a", $("[id$='TreeView1']")).parent().css("border", "");
    //seleccionamos el elemento del treeview
    var current_selection = $("#" + current_selection_id);
    current_selection.parent().css("border", "1px solid #AAAAAA");

    var methodUrl = "Contenido.aspx/listarArchivosContenidos";
    parameters = "{'nombre_carpeta': '" + nombre_carpeta + "'}";
    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: methodUrl,
        data: parameters,
        dataType: "json",
        async: true,
        success: function (response) {

            var data = response.d;
            var dataResult = jQuery.parseJSON(data);
            var carpeta = dataResult.carpeta;

            //borramos el contenido. nos referimos a los archivos
            $("#div_vista").html("");

            //recorremos todos los archivos para agregarlos a la tabla
            for (i = 0; i < carpeta.length; i++) {
                var renglon = "";
                var elem_carpeta = carpeta[i];
                // var nombre_completo = elem_carpeta.nombre_completo;
                var tipo = elem_carpeta.tipo;

                if (tipo == "carpeta") {
                    var tiene_contenido = elem_carpeta.tiene_contenido;

                    var rutaweb = elem_carpeta.rutaweb;
                    var nombre = elem_carpeta.nombre;
                    var nombre_recortado = nombre.substring(0, 20) + " " + nombre.substring(20, 40) + " " + nombre.substring(40, 60);

                    var ultima_mod = elem_carpeta.ultima_mod;
                    var ultima_mod_hora = ultima_mod.substring(0, 10);
                    var ultima_mod_min = ultima_mod.substring(11);

                    //descripcion------------------
                    var desc = elem_carpeta.desc;
                    var fecha_desc = elem_carpeta.fecha_desc;

                    var new_ruta_carpeta = nombre_carpeta + "/" + nombre;

                    //renglon += "<div style='float: left; display: block; height:100px; padding: 5px 5px 5px 5px; text-align: center;'>";
                    renglon += "<div class='icono_carpeta' style='float: left; display: block; width:100px; height:130px;  padding: 5px 5px 5px 5px; text-align: center;'>";
                    renglon += "<div class='sel_icono' style='display: inline-block; cursor:pointer;' data-filepath='" + new_ruta_carpeta + "' >";
                    renglon += "<div>";
                    if (tiene_contenido == "True")
                    //renglon += "<img style='cursor:pointer;' src='../Images/icons_ext/32/folder_vertical_document.png' title='" + nombre + "\n\n" + desc + "' onclick='listarArchivosContenidosPorIconos(\"" + new_ruta_carpeta + "\" )' />";
                    //renglon += "<img style='cursor:pointer;' src='../Images/icons_ext/32/folder_vertical_document.png' title='" + nombre + "\n\n" + desc + "' />";
                        renglon += "<img src='../Images/icons_ext/32/folder_vertical_document.png' title='" + nombre + "\n\n" + desc + "' />";
                    else if (tiene_contenido == "False")
                    //renglon += "<img style='cursor:pointer;' src='../Images/icons_ext/32/folder_vertical_open.png' title='" + nombre + "\n\n" + desc + "' onclick='listarArchivosContenidosPorIconos(\"" + new_ruta_carpeta + "\" )' />";
                    //renglon += "<img style='cursor:pointer;' src='../Images/icons_ext/32/folder_vertical_open.png' title='" + nombre + "\n\n" + desc + "' />";
                        renglon += "<img src='../Images/icons_ext/32/folder_vertical_open.png' title='" + nombre + "\n\n" + desc + "' />";

                    renglon += "</div>";
                    renglon += "<div style='font-size: 10px; font-weight: bold;'>" + nombre_recortado + "</div>";
                    renglon += "<div style='font-size: 10px;'>" + ultima_mod_hora + "</div>";
                    renglon += "<div style='font-size: 10px;'>" + ultima_mod_min + "</div>";
                    renglon += "</div>";
                    renglon += "</div>";

                }
                else if (tipo == "archivo") {
                    var rutaweb = elem_carpeta.rutaweb;
                    var nombre = elem_carpeta.nombre;
                    var nombre_recortado = nombre.substring(0, 20) + " " + nombre.substring(20, 40) + " " + nombre.substring(40, 60);

                    //checamos que exista la extension del archivo actual, de no ser asi se asigna la generica new
                    var ext = obtenerCoincidenciaExtension32(elem_carpeta.ext);
                    //var ext = elem_carpeta.ext;

                    var ultima_mod = elem_carpeta.ultima_mod;
                    var ultima_mod_hora = ultima_mod.substring(0, 10);
                    var ultima_mod_min = ultima_mod.substring(11);

                    var size = elem_carpeta.size;
                    var ssize = size.toString();
                    var cad_size = "";

                    //descripcion------------------
                    var desc = elem_carpeta.desc;
                    var fecha_desc = elem_carpeta.fecha_desc;

                    if (ssize.length > 6) {
                        cad_size = Math.floor((size / 1048576) * 10) / 10 + " MB";
                    }
                    else if (ssize.length > 3) {
                        cad_size = Math.floor((size / 1024) * 10) / 10 + " KB";
                    }
                    else {
                        cad_size = size + " Bytes";
                    }

                    //renglon += "<div style='float: left; display: block; height:100px; padding: 5px 5px 5px 5px; text-align: center;'>";
                    renglon += "<div  class='icono_archivo' style='float: left; display: block; width:100px; height:130px;  padding: 5px 5px 5px 5px; text-align: center;'>";
                    renglon += "<div class='sel_icono' style='display: inline-block; cursor:pointer;'>";
                    renglon += "<div><a href='" + rutaweb + _no_cache + "' target='_blank' ><img src='../Images/icons_ext/32/file_extension_" + ext + ".png' title='" + nombre + "\n\n" + desc + "' /><a></div>";
                    renglon += "<div style='font-size: 10px; font-weight: bold;'>" + nombre_recortado + "</div>";
                    renglon += "<div style='font-size: 10px;'>" + ultima_mod_hora + "</div>";
                    renglon += "<div style='font-size: 10px;'>" + ultima_mod_min + "</div>";
                    renglon += "<div style='font-size: 10px;'>" + cad_size + "</div>";
                    renglon += "</div>";
                    renglon += "</div>";
                }
                $("#div_vista").append(renglon);

            } //end for


            console.log("ok: " + data);
        },
        error: function (response) {
            var data = response.d;
            //Mostrar msg
            console.log("Error: " + data);
        }
    });
}





function mostrarDialogoFileUpload() {


    $("[id$='TextBox_DescripcionArchivo']").val('');

    //cerramos carpeta de opciones
    $("#dialogo_carpeta_opciones").dialog("close");
    $("#dialogo_interior_carpeta_view").dialog("close");

    $("#dialogo_file_upload").dialog("open");
}



function mostrarDialogoCrearCarpeta() {

    //limpiamos el textbox
    $("[id$='TextBox_CrearCarpeta_Nombre']").val("");
    $("[id$='TextBox_DescripcionCarpeta']").val("");


    //cerramos carpeta de opciones
    $("#dialogo_carpeta_opciones").dialog("close");

    $("#dialogo_crear_carpeta").dialog("open");
}


function mostrarDialogoBorrarCarpeta() {

    //cerramos carpeta de opciones
    $("#dialogo_carpeta_opciones").dialog("close");

    var nom_carpeta_borrar = $("[id$='HiddenField_NombreCarpeta']").val();
    
    $("#etiqueta_nombre_borrar_carpeta").html(nom_carpeta_borrar);

    $("#dialogo_borrar_carpeta").dialog("open");
}


function mostrarDialogoRenombrarCarpeta() {

    //cerramos carpeta de opciones
    $("#dialogo_carpeta_opciones").dialog("close");

    var nom_carpeta = $("[id$='HiddenField_NombreCarpeta']").val();

    var pos = nom_carpeta.lastIndexOf("/");
    nom_carpeta = nom_carpeta.substring(pos+1);

 
    $("#etiqueta_nombre_renombrar_carpeta").html(nom_carpeta);

    $("[id$='TextBox_NuevoNombreCarpeta']").val(nom_carpeta);
    $("#dialogo_renombrar_carpeta").dialog("open");
}


function mostrarDialogoBorrarArchivo() {

    //cerramos carpeta de opciones
    $("#dialogo_archivo_opciones").dialog("close");

    var nom_archivo_borrar = $("[id$='HiddenField_NombreArchivo']").val();
    nom_archivo_borrar = nom_archivo_borrar.split('|');
    $("#etiqueta_nombre_borrar_archivo").html(nom_archivo_borrar[1]);

    $("#dialogo_borrar_archivo").dialog("open");
}

function mostrarDialogoRenombrarArchivo() {

    //cerramos carpeta de opciones
    $("#dialogo_archivo_opciones").dialog("close");

    var nom_archivo = $("[id$='HiddenField_NombreArchivo']").val();
    nom_archivo = nom_archivo.split('|');
    $("#etiqueta_nombre_renombrar_archivo").html(nom_archivo[1]);

    $("[id$='TextBox_NuevoNombreArchivo']").val(nom_archivo[1]);
    $("#dialogo_renombrar_archivo").dialog("open");
}



function mostrarDialogoEditarResumenCarpeta() {

    var elem_seleccionado = $("[nid='" + carpeta_id_seleccinado + "']");

    var cfecha = elem_seleccionado.attr("fecha");
    var cdesc = elem_seleccionado.attr("title");

    $("[id$='TextBox_EditarFechaCarpeta']").val(cfecha);
    $("[id$='TextBox_EditarDescripcionCarpeta']").val(cdesc);

    //cerramos carpeta de opciones
    $("#dialogo_carpeta_opciones").dialog("close");

    var nom_carpeta_editar_desc = $("[id$='HiddenField_NombreCarpeta']").val();
    $("#etiqueta_nombre_editar_documentacion_carpeta").html(nom_carpeta_editar_desc);

    $("#dialogo_editar_descripcion_carpeta").dialog("open");
}



function mostrarDialogoEditarResumenArchivo() {

    var elem_seleccionado = $("[nid='" + archivo_id_seleccinado + "']");

    var cfecha = elem_seleccionado.attr("fecha");
    var cdesc = elem_seleccionado.attr("title");


    $("[id$='TextBox_EditarFechaArchivo']").val(cfecha);
    $("[id$='TextBox_EditarDescripcionArchivo']").val(cdesc);


    //cerramos carpeta de opciones
    $("#dialogo_archivo_opciones").dialog("close");

    var nom_archivo_editar_desc = $("[id$='HiddenField_NombreArchivo']").val();
    var res = nom_archivo_editar_desc.split('|');
    $("#etiqueta_nombre_editar_documentacion_archivo").html(res[0]);



    $("#dialogo_editar_descripcion_archivo").dialog("open");
}





//esta funcion se debe llamar cada que ya es creada una tabla
function crearDefinicionTableSorter() {



    /*$("table").tablesorter({
    widthFixed: true,
    showProcessing: true,
    headerTemplate: '{content} {icon}',
    widgets: ['uitheme', 'zebra', 'filter', 'scroller'],
    widgetOptions: {
    scroller_height: 300,
    scroller_barWidth: 18,
    scroller_upAfterSort: true,
    scroller_jumpToHeader: true,
    scroller_idPrefix: 's_'
    }
    });*/

    $("#tbl_vista_details").tablesorter({
        theme: 'blue',
        //sortList: [[0, 0], [1, 0]],  // sort on the first column and third column in ascending order
        widthFixed: true,

        // initialize zebra striping and filter widgets
        widgets: ["zebra", "filter"],
        //sortList: [[2, 0], [1, 0]],
        headers: {
            0: { sorter: false },    //no queremos que se active el ordenamiento por este campo
            1: { sorter: 'text' },   //definimos que la columna 2 la ordene considerando los datos como texto (alfanumerico)
            2: { sorter: 'text' },
            3: { sorter: 'text' }
        },

        ignoreCase: false,

        widgetOptions: {

            // filter_anyMatch options was removed in v2.15; it has been replaced by the filter_external option

            // If there are child rows in the table (rows with class name from "cssChildRow" option)
            // and this option is true and a match is found anywhere in the child row, then it will make that row
            // visible; default is false
            filter_childRows: false,

            // if true, a filter will be added to the top of each table column;
            // disabled by using -> headers: { 1: { filter: false } } OR add class="filter-false"
            // if you set this to false, make sure you perform a search using the second method below
            filter_columnFilters: true,

            // extra css class name(s) applied to the table row containing the filters & the inputs within that row
            // this option can either be a string (class applied to all filters) or an array (class applied to indexed filter)
            filter_cssFilter: '', // or []

            // jQuery selector (or object) pointing to an input to be used to match the contents of any column
            // please refer to the filter-any-match demo for limitations - new in v2.15
            filter_external: '',

            // class added to filtered rows (rows that are not showing); needed by pager plugin
            filter_filteredRow: 'filtered',

            // add custom filter elements to the filter row
            // see the filter formatter demos for more specifics
            filter_formatter: null,

            // add custom filter functions using this option
            // see the filter widget custom demo for more specifics on how to use this option
            filter_functions: null,

            // if true, filters are collapsed initially, but can be revealed by hovering over the grey bar immediately
            // below the header row. Additionally, tabbing through the document will open the filter row when an input gets focus
            filter_hideFilters: true,

            // Set this option to false to make the searches case sensitive
            filter_ignoreCase: true,

            // if true, search column content while the user types (with a delay)
            filter_liveSearch: true,

            // a header with a select dropdown & this class name will only show available (visible) options within that drop down.
            filter_onlyAvail: 'filter-onlyAvail',

            // jQuery selector string of an element used to reset the filters
            filter_reset: 'button.reset',

            // Use the $.tablesorter.storage utility to save the most recent filters (default setting is false)
            filter_saveFilters: true,

            // Delay in milliseconds before the filter widget starts searching; This option prevents searching for
            // every character while typing and should make searching large tables faster.
            filter_searchDelay: 300,

            // if true, server-side filtering should be performed because client-side filtering will be disabled, but
            // the ui and events will still be used.
            filter_serversideFiltering: false,

            // Set this option to true to use the filter to find text from the start of the column
            // So typing in "a" will find "albert" but not "frank", both have a's; default is false
            filter_startsWith: false,

            // Filter using parsed content for ALL columns
            // be careful on using this on date columns as the date is parsed and stored as time in seconds
            filter_useParsedData: false,

            // data attribute in the header cell that contains the default filter value
            filter_defaultAttrib: 'data-value'



        }

    });
}


function mostrarMensaje(msg) {
    $("#etiqueta_mensaje").html(msg);
    $("#dialogo_mensaje").dialog("open");
};


//subimos un nivel de archivos. solo aplica en iconos y detalles
function subirNivelCarpeta() {

    //guardamos el nombre de la carpeta seleccionada
    var nombre_carpeta = $("[id$='HiddenField_NombreCarpeta']").val();
    var last_position = nombre_carpeta.lastIndexOf("/");

    var nueva_ruta_carpeta = nombre_carpeta.substring(0, last_position);
    //alert("nombre_carpeta: " + nombre_carpeta + " > " + nueva_ruta_carpeta);
    listarArchivosContenidosPorIconos(nueva_ruta_carpeta);

}

    