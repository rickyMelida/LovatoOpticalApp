const showAlert = (message, title, type) => {
	return Swal.fire({
		icon: type,
		title: title,
		text: message,
		showConfirmButton: true,
	});
};


const showDeleteConfirm = (message, title, type) => {
	return Swal.fire({
		icon: type,
		title: title,
		text: message,
		showCancelButton: true,
		confirmButtonColor: '#3085d6',
		cancelButtonColor: '#d33',
		confirmButtonText: 'Sí, ¡elimínalo!'
	});
}