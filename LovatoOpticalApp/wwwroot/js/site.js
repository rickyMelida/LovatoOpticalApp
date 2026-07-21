const showAlert = (message, title, type) => {
	return Swal.fire({
		icon: type,
		title: title,
		text: message,
		showConfirmButton: true,
	});
};

const reloadCurrentPage = () => {
    location.reload();
}


const showDeleteConfirmAsync = (message, title, type, action) => {
	return Swal.fire({
		icon: type,
		title: title,
		text: message,
		showCancelButton: true,
		confirmButtonColor: '#3085d6',
		cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, ¡elimínalo!',
        showLoaderOnConfirm: true,
        preConfirm: async () => {
            try {
               return await action()
            } catch (error) {
                Swal.showValidationMessage(`Request failed: ${error}`);
            }
        },
        allowOutsideClick: () => !Swal.isLoading()
    }).then((result) => {
        if (result.isConfirmed) {
            const { message, status } = result.value;
            showAlert(
                message,
                status == 200 ? "Producto Eliminado" : "Advertencia",
                status == 200 ? "success" : "warning"
            ).then(() => reloadCurrentPage())
        }
    });
}

