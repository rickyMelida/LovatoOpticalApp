const showAlert = (message, title, type) => {
	return Swal.fire({
		icon: type,
		title: title,
		text: message,
		showConfirmButton: true,
	});
};