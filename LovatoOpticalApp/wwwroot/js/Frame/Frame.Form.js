import { hideModal, showModal } from "../Common/ModalEvents.js"

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

const handleSubmitButton = async () => {
	btnFormSubmit.addEventListener("click", async (e) => {
		e.preventDefault();
		const isEditForm = btnFormSubmit.querySelector('.edit-product');
		enableButton(btnFormSubmit, true);

		if (!form.checkValidity()) {
			form.reportValidity();
			enableButton(btnFormSubmit, false);
			return;
		}

		const framePayload = buildFramePayload();
		const newFrameModalLabel = document.getElementById("newFrameModalLabel");
		const id = newFrameModalLabel.getAttribute("data-product-id");

		try {
			const data = isEditForm ? await updateFrameAsync(framePayload, id) : await createFrameAsync(framePayload);

			showAlert(data.message, data.status == 200 ? "Éxito" : "Advertencia", data.status == 200 ? "success" : "warning")
				.then(() => {
					hideModal("newFrameModal");
					resetForm();
					reloadCurrentPage()
				});

		} catch (error) {
			showAlert("Error al crear el armazon", "Error", "error");
			enableButton(btnFormSubmit, false);

		}

	});
}

const handleNewFrameForm = () => {
	const btnNewModalFrameModal = document.getElementById("btnNewFrameModal");
	const btnFormSubmit = document.getElementById("btnFormSubmit");

	btnNewModalFrameModal.addEventListener("click", () => {
		const newFrameModalLabel = document.getElementById("newFrameModalLabel");
		newFrameModalLabel.innerHTML = `<i class="bi bi-plus-circle me-2"></i>Nuevo Armazón`;
		btnFormSubmit.innerHTML = `<i class="bi bi-floppy me-1 create-product"></i>Guardar`;

		resetForm()
		showModal("newFrameModal");
	})
}

export const initFrameForm = async () => {
	await handleSubmitButton();
	handleNewFrameForm();
}

const createFrameAsync = async (framePayload) => {
	const response = await fetch("/Catalog/CreateFrame", {
		method: "POST",
		headers: {
			"Content-Type": "application/json"
		},
		body: JSON.stringify(framePayload)
	});

	return await response.json();
}

const updateFrameAsync = async (frame, id) => {
	const request = { ...frame, id };

	const response = await fetch("/Catalog/UpdateFrame", {
		method: "POST",
		headers: {
			"Content-Type": "application/json"
		},
		body: JSON.stringify(request)
	});

	return await response.json();
}