import { hideModal, showModal } from "../Common/ModalEvents.js"
import { guaraniStringANumero } from "../Helper/Helper.js";

const accessoryForm = document.getElementById('newAccessoryForm');
const btnFormSubmit = document.getElementById('btnAccessoryFormSubmit');

const buildAccessoryPayload = () => {
	const formData = new FormData(accessoryForm);

	return {
		Name: formData.get('Name')?.toString().trim() ?? '',
		PurchasePrice: guaraniStringANumero(formData.get("PurchasePrice")),
		SalePrice: guaraniStringANumero(formData.get("SalePrice")),
		Quantity: parseInt(formData.get('Stock')?.toString().trim() ?? '0', 10) || 0,
		MinimumQuantity: parseInt(formData.get('MinStock')?.toString().trim() ?? '0', 10) || 0,
		Description: formData.get('Description')?.toString().trim() ?? '',
		IsOptional: formData.get('IsOptional') === 'on',
		Type: 3
	}
}

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
	accessoryForm.reset();
	enableButton(btnFormSubmit, false);
}

const handleSubmitButton = async () => {
	btnFormSubmit.addEventListener('click', async (event) => {
		event.preventDefault();
		const isEditForm = btnFormSubmit.querySelector('.edit-accessory');
		enableButton(btnFormSubmit, true);

		if (!accessoryForm.checkValidity()) {
			accessoryForm.reportValidity();
			enableButton(btnFormSubmit, false);
			return;
		}

		const formData = buildAccessoryPayload();
		const newAccessoryModalLabel = document.getElementById("newAccessoryModalLabel");
		const id = newAccessoryModalLabel.getAttribute("data-product-id");

		try {
			const data = isEditForm ? await updateAccessoryAsync(formData, id) : await createAccessoryAsync(formData);

			showAlert(data.message, data.status == 200 ? "Éxito" : "Advertencia", data.status == 200 ? "success" : "warning")
				.then(() => {
					hideModal("newAccessoryModal");
					resetForm();
					reloadCurrentPage()
				});

		} catch (error) {
			showAlert("Error al crear el accesorio", "Error", "error");
			enableButton(btnFormSubmit, false);

		}

	});
}

const handleNewAccessoryForm = () => {
	const btnNewModalAccessoryModal = document.getElementById("btnNewAccessoryModal");
	const btnFormSubmit = document.getElementById("btnFormSubmit");

	btnNewModalAccessoryModal.addEventListener("click", () => {
		const newAccessoryModalLabel = document.getElementById("newAccessoryModalLabel");
		newAccessoryModalLabel.innerHTML = `<i class="bi bi-plus-circle me-2"></i>Nuevo Accesorio`;
		btnFormSubmit.innerHTML = `<i class="bi bi-floppy me-1 create-product"></i>Guardar`;

		resetForm()
		showModal("newAccessoryModal");
	})
}


export const initAccessoryForm = async () => {
	await handleSubmitButton();
	handleNewAccessoryForm();
}


const createAccessoryAsync = async (accessoryPayload) => {
	const response = await fetch("/Catalog/CreateAccessory", {
		method: "POST",
		headers: {
			"Content-Type": "application/json"
		},
		body: JSON.stringify(accessoryPayload)
	});

	return await response.json();
}

const updateAccessoryAsync = async (accessory, id) => {
	const request = { ...accessory, id };

	const response = await fetch("/Catalog/UpdateAccessory", {
		method: "POST",
		headers: {
			"Content-Type": "application/json"
		},
		body: JSON.stringify(request)
	});

	return await response.json();
}