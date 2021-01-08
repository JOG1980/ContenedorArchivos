$(document).ready(function () {

    initTableSorters();

    $("#Dialogo_CrearUsuario").dialog({
        autoOpen: false,
        resizable: false,
        width: 340,
        height: 350,
        modal: true,
        open: function (type, data) {
            $(this).parent().appendTo("form");
        }
    });


    $("#Dialogo_EditarUsuario").dialog({
        autoOpen: false,
        resizable: false,
        width: 340,
        height: 350,
        modal: true,
        open: function (type, data) {
            $(this).parent().appendTo("form");
        }
    });

    $("#Dialogo_BorrarUsuario").dialog({
        autoOpen: false,
        resizable: false,
        width: 400,
        height: 200,
        modal: true,
        open: function (type, data) {
            $(this).parent().appendTo("form");
        }
    });

    $("#Dialogo_ImportarUsuarios").dialog({
        autoOpen: false,
        resizable: false,
        width: 400,
        height: 200,
        modal: true,
        open: function (type, data) {
            $(this).parent().appendTo("form");
        }
    });


});




function initTableSorters() {

    
    $(".tabla_lista_usuarios")
    .tablesorter({
        theme: 'blue',
        widthFixed: true,
        sortList: [[0, 0], [1, 0]],
        widgets: ["zebra", "filter"],
        widgetOptions: {
            // include column filters
            filter_columnFilters: true,
            filter_placeholder: { search: 'Buscar por...' },
            filter_saveFilters: true,
            filter_reset: '.reset'
        },
        headers: {
            6: { sorter: false, filter: false },
            8: { sorter: false, filter: false},
            9: { sorter: false, filter: false }
         }
    });

    //quitar filtros
    $(".tablesorter thead .disabled").css("display","none");

};





function mostrarDialogoCrearUsuario() {

    $("[id$='TextBox_NombreUsuario']").val("");
    $("[id$='TextBox_Password']").val("");
    $("[id$='DropDownList_Roles']").val("User");
    $("[id$='TextBox_Nombre']").val("");
    $("[id$='TextBox_ApellidoPaterno']").val("");
    $("[id$='TextBox_ApellidoMaterno']").val("");
    $("[id$='TextBox_Email']").val("");
    $("[id$='DropDownList_EnviarEmail']").val("No");
    $("#Dialogo_CrearUsuario").dialog("open");
}


//function mostrarDialogoEditarUsuario(usuario, password, email, rol) {
function mostrarDialogoEditarUsuario(usuario, password, rol, nombre, apellido_paterno, apellido_materno, email, enviar_email) {
    
    $("[id$='HiddenField_UsuarioSeleccionado']").val(usuario);
    $("[id$='HiddenField_OldPassword']").val(password);
    $("[id$='HiddenField_OldRol']").val(rol);

    $("#span_etiqueta_nombre_usuario").html(usuario);
    $("[id$='TextBox_Password2']").val(password);
    $("[id$='DropDownList_Roles2']").val(rol);
    $("[id$='TextBox_Nombre2']").val(nombre);

    $("[id$='TextBox_ApellidoPaterno2']").val(apellido_paterno);
    $("[id$='TextBox_ApellidoMaterno2']").val(apellido_materno);
    $("[id$='TextBox_Email2']").val(email);
    $("[id$='DropDownList_EnviarEmail2']").val(enviar_email);
    

    $("#Dialogo_EditarUsuario").dialog("open");

}

function mostrarDialogoBorrarUsuario(usuario) {

    $("[id$='HiddenField_UsuarioSeleccionado']").val(usuario);

    $("#span_etiqueta_borrar_usuario").html(usuario);
    $("#Dialogo_BorrarUsuario").dialog("open");
}


function mostrarDialogoImportarUsuarios() {
    $("#Dialogo_ImportarUsuarios").dialog("open");
}