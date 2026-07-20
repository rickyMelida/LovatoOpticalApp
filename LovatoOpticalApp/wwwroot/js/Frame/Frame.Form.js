const form = document.getElementById("newFrameForm");
const btnFormSubmit = document.getElementById("btnFormSubmit");

const buildFramePayload = () => {
	const formData = new FormData(form);

	return {
		Type: 1,
		Name: formData.get("Name")?.toString().trim() ?? "",
		Code: formData.get("Code")?.toString().trim() ?? "",
		Material: formData.get("Material")?.toString() ?? "",
		FrameType: formData.get("FrameType")?.toString() ?? "",
		Color: formData.get("Color")?.toString().trim() ?? "",
		PurchasePrice: parseFloat(formData.get("PurchasePrice") ?? 0) || 0,
		SalePrice: parseFloat(formData.get("SalePrice") ?? 0) || 0,
		Quantity: parseInt(formData.get("Quantity") ?? 0, 10) || 0,
		MinimumQuantity: parseInt(formData.get("MinimumQuantity") ?? 0, 10) || 0,
		Description: formData.get("Description")?.toString().trim() ?? "",
		CreatedBy: "00000000-0000-0000-0000-000000000000"
	};
};


const enableButton = (button, disable = false) => {
	const originalText = `<i class="bi bi-floppy me-1"></i>Guardar`;
	const loadingSpinner = `<div class="spinner-border spinner-border-sm" role="status">
											<span class="visually-hidden">Loading...</span>
										</div>`;

	button.innerHTML = disable ? loadingSpinner : originalText;
	button.disabled = disable;
	button.classList.toggle("loading-btn", disable);
}

const resetForm = () => {
	form.reset();
	enableButton(btnFormSubmit, false);
}

const hideModal = () => {
	const modalElement = document.getElementById("newFrameModal");
	const modalInstance = bootstrap.Modal.getInstance(modalElement);
	modalInstance.hide();
}

export const initFrameForm = async () => {
	btnFormSubmit.addEventListener("click", async (e) => {
		e.preventDefault();
		enableButton(btnFormSubmit, true);

		if (!form.checkValidity()) {
			form.reportValidity();
			enableButton(btnFormSubmit, false);
			return;
		}

		const framePayload = buildFramePayload();

		try {
			const response = await fetch("/Catalog/CreateFrame", {
				method: "POST",
				headers: {
					"Content-Type": "application/json"
				},
				body: JSON.stringify(framePayload)
			});

			const data = await response.json();
			showAlert(data.message, "Éxito", "success")
				.then(() => {
					hideModal();
					resetForm();
					await realoadPage()
				});
			
		} catch (error) {
			showAlert("Error al crear el armazon", "Error", "error");
			enableButton(btnFormSubmit, false);
		}
	});
}

const realoadPage = async () => {
	await fetch("/Catalog/Index");
}
