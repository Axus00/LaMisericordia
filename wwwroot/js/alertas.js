//llamamos los mensajes TempData
setTimeout(function(){
    let mensaje = document.getElementById("alerta");
    if(mensaje)
    {
        mensaje.remove()
    }
}, 2000);