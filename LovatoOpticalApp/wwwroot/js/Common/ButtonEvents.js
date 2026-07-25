export const enableButton = (button, disable = false) => {
	const originalText = `<i class="bi bi-floppy me-1"></i>Guardar`;
	const loadingSpinner = `<div class="spinner-border spinner-border-sm text-dark" role="status">
											<span class="visually-hidden">Loading...</span>
										</div>`;

	button.innerHTML = disable ? loadingSpinner : originalText;
	button.disabled = disable;
	button.classList.toggle("loading-btn", disable);
}


export const enableLargeButton = (
	button,
	disable = false,
	originalText = `<i class="bi bi-search me-1"></i>Buscar`
) => {
	const loadingSpinner = `<div class="spinner-border spinner-border-sm text-dark" role="status">
											<span class="visually-hidden">Loading...</span>
										</div>`;

	button.innerHTML = disable ? loadingSpinner : originalText;
	button.disabled = disable;
	button.classList.toggle("loading-lg-btn", disable);
}
