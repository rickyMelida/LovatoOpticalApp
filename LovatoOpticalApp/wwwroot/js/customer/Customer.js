import { handleSaveCustomer } from './Customer.Rule.js'

const deleteCustomerElement = document.querySelectorAll('.deleteCustomer');
const btnSaveButton = document.getElementById("btnSaveCustomer");

btnSaveButton.addEventListener('click', (e) => {
    e.preventDefault();

    handleSaveCustomer();
})


deleteCustomerElement.forEach(element => {

    element.addEventListener('click', () => {
        const swalWithBootstrapButtons = Swal.mixin({
            customClass: {
                confirmButton: "btn btn-success",
                cancelButton: "btn btn-danger"
            },
            buttonsStyling: false
        });
        swalWithBootstrapButtons.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "Yes, delete it!",
            cancelButtonText: "No, cancel!",
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) swalWithBootstrapButtons.fire({
                title: "Deleted!",
                text: "Your file has been deleted.",
                icon: "success"
            });
            else if (result.dismiss === Swal.DismissReason.cancel)
                /* Read more about handling dismissals below */
                swalWithBootstrapButtons.fire({
                    title: "Cancelled",
                    text: "Your imaginary file is safe :)",
                    icon: "error"
                });
        });
    })
})