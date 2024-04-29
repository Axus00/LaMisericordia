
/* setTimeout(function(){
    window.location.reload();
},2000); */

//Función para ingresar al perfil
const button = document.getElementById("button")

function Mensaje()
{
    Swal.fire({
        title: "Sesión iniciada",
        text: "Click en el botón para continuar",
        icon: "success"
    });

};

button.addEventListener("click", Mensaje);

