$(document).ready(function () {


    initDialogs();
    initColorPicker();

});


function initColorPicker() {

    var tcolor1 = $("[id$='TextBox_Titulo1Color']").val();
    var tcolor2 = $("[id$='TextBox_Titulo2Color']").val();
    var tcolor3 = $("[id$='TextBox_Titulo3Color']").val();
    var color_fondo = $("[id$='TextBox_FondoColor']").val();
    $(".colorBoton").colpick({
        layout: 'hex',
        submit: 0,
        onChange: function (hsb, hex, rgb, el, bySetColor) {
            hex = hex.toUpperCase();
            $(el).css('border-color', '#' + hex);
            // Fill the text box just if the color was set using the picker, and not the colpickSetColor function.
            if (!bySetColor) $(el).val("#" + hex);
        }
    }).keyup(function () {
        $(this).colpickSetColor(this.value);
    });

    $("[id$='TextBox_Titulo1Color']").colpickSetColor(tcolor1, true);
    $("[id$='TextBox_Titulo2Color']").colpickSetColor(tcolor2, true);
    $("[id$='TextBox_Titulo3Color']").colpickSetColor(tcolor3, true);

    $("[id$='TextBox_FondoColor']").colpickSetColor(color_fondo, true);
};





function initDialogs() {
    $("#dialog_subir_imagen_portada").dialog({
        autoOpen: false,
        resizable: false,
        width: 370,
        height: 220,
        modal: true,
        title: "Seleccionar y subir una imagen para la portada",
        open: function (type, data) {
            $(this).parent().appendTo("form");
            $(".ui-dialog-titlebar").show();
            $(".ui-dialog .ui-dialog-content").css("padding", "");
        }
    });
};


function mostrarVentanaSubirArchivo(tipo_imagen) {
    var titulo = "";
    var mensaje = "";

    $('[id$="Button_SubirImagenLogo1"]').hide();
    $('[id$="Button_SubirImagenLogo2"]').hide();
    $('[id$="Button_SubirImagenPortada"]').hide();
    
    if (tipo_imagen == 'logo1') {
        titulo = "Seleccionar y subir una imagen para el Logo 1";
        mensaje = "Selecciona una imagen (preferentemente de 180x120 pixeles) y despues preciona el boton subir";
        $('[id$="Button_SubirImagenLogo1"]').show();
    }
    else if (tipo_imagen == 'logo2') {
        titulo = "Seleccionar y subir una imagen para el Logo 2";
        mensaje = "Selecciona una imagen (preferentemente de 160x120 pixeles) y despues preciona el boton subir";
        
        $('[id$="Button_SubirImagenLogo2"]').show();
    }
    else if (tipo_imagen == 'portada') {
        titulo = "Seleccionar y subir una imagen para la Portada";
        mensaje = "Selecciona una imagen (preferentemente de 800x600 pixeles) y despues preciona el boton subir";
        $('[id$="Button_SubirImagenPortada"]').show();    
    }

    $("#mensaje_subir_archivo").html(mensaje);
    $("#dialog_subir_imagen_portada").dialog("option", "title", titulo);

    $("#dialog_subir_imagen_portada").dialog("open");
};


