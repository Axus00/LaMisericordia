// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function ActualizarPagina(){

   location.href = "~/Usuario/TicketScreen.cshtml";

}


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
        location.href = "http://localhost:5081/Admin/DeleteTurnos/"+id;
        Swal.fire({
        title: "Eliminado!",
        text: "El turno ha sido eliminado.",
        icon: "success"
        });
    }
    });
}
