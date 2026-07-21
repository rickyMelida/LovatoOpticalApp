export const enableButton = (button, disable = false) => {
	const originalText = `<i class="bi bi-floppy me-1"></i>Guardar`;
	const loadingSpinner = `<div class="spinner-border spinner-border-sm" role="status">
											<span class="visually-hidden">Loading...</span>
										</div>`;

	button.innerHTML = disable ? loadingSpinner : originalText;
	button.disabled = disable;
	button.classList.toggle("loading-btn", disable);
}