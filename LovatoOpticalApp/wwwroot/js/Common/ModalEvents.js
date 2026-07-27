export const hideModal = (modalId) => {
    const modalElement = document.getElementById(modalId);
    const modalInstance = bootstrap.Modal.getInstance(modalElement);
    modalInstance.hide();
}

export const showModal = (modalId) => {
	const modalElement = document.getElementById(modalId);
	if (!modalElement) {
		console.warn("Modal element #viewFrameModal not found.");
		return;
	}
	const modalInstance = bootstrap.Modal.getOrCreateInstance(modalElement);

	modalInstance.show();
}