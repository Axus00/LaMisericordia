
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

//Función para eliminar turno
/* const button2 = document.getElementById("delete");

function Confirm(id)
{
    
    Swal.fire({
    title: "¿Seguro deseas eliminar el turno?",
    text: "Esto es irrevertible",
    icon: "warning",
    showCancelButton: true,
    confirmButtonColor: "#3085d6",
    cancelButtonColor: "#d33",
    confirmButtonText: "Sí, eliminar!"
    }).then((result) => {
    if (result.isConfirmed) {
        location.href = `http://localhost:5081/Admin/Turnos/${id}`;
        Swal.fire({
        title: "Eliminado!",
        text: "El turno ha sido eliminado.",
        icon: "success"
        });
    }
    });
}

button2.addEventListener("click", Confirm); */
