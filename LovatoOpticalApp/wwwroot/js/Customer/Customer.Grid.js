import { dateDayMonthYear } from '../Common/DateFormats.js';
import { showEditCustomerModal } from './Customer.Rules.js';
import { showModal } from "../Common/ModalEvents.js";

export const handleGridModal = () => {
	const btnNewCustomer = document.getElementById("btnNewCustomer");
	const customerInputSearch = document.getElementById('customerInputSearch');

	btnNewCustomer.addEventListener('click', showCreateCustomerModal);

	customerInputSearch.addEventListener('input', async (e) => {
		const query = customerInputSearch.value.trim();
		let debounceTimer;
		clearTimeout(debounceTimer);


		debounceTimer = setTimeout(() => {
			searchCustomerAsync(query);
		}, 1000);
	})

	attachGridEvents();
}

const attachGridEvents = () => {
	const viewDetailsButtons = document.querySelectorAll(".view-customer-details");
	const editButtons = document.querySelectorAll(".edit-customer");
	const deleteButtons = document.querySelectorAll(".delete-customer");

	viewDetailsButtons.forEach(button => {
		button.addEventListener("click", async (event) => {
			const customerId = event.currentTarget.id;
			await showViewDetailsModal(customerId);
		});
	});

	editButtons.forEach(button => {
		button.addEventListener("click", async (event) => {
			const customerId = event.currentTarget.id;
			await showEditCustomerModal(customerId);
		});
	});

	deleteButtons.forEach(button => {
		button.addEventListener("click", (event) => {
			const customerId = event.currentTarget.id;

			showDeleteConfirmation(customerId);
		});
	});
}

const showCreateCustomerModal = () => {
	document.getElementById("newCustomerForm").reset();
	document.getElementById("newCustomerModalLabel").innerHTML = '<i class="bi bi-plus-circle me-2"></i>Nuevo Cliente';
	document.getElementById("btnSaveCustomer").innerHTML = '<i class="bi bi-floppy me-1"></i>Guardar';

	showModal("newCustomerModal");
}

const showViewDetailsModal = async (customerId) => {
	const modalElement = document.getElementById("viewCustomerModal");
	if (!modalElement) {
		console.warn("Modal element #viewCustomerModal not found.");
		return;
	}
	const modalInstance = bootstrap.Modal.getOrCreateInstance(modalElement);
	const customerDetails = await getCustomerDetails(customerId);

	renderCustomerDetails(customerDetails);

	modalInstance.show();
}

const getCustomerDetails = async (customerId) => {
	try {
		const response = await fetch(`/Customer/GetCustomerDetails?customerId=${customerId}`);

		const data = await response.json();
		return data;
	} catch (error) {
		console.log({ error });
		return null;
	}
}

let _recipes = [];
let _currentRecipeIndex = 0;

const renderCustomerDetails = (customerDetails) => {
	if (!customerDetails) {
		console.warn("No customer details available.");
		return;
	}

	// Render customer details in the modal
	document.getElementById("viewCustomerName").textContent = customerDetails.name;
	document.getElementById("viewCustomerCiRuc").textContent = customerDetails.ciRuc;
	document.getElementById("viewCustomerPhone").textContent = customerDetails.phone;
	document.getElementById("viewCustomerEmail").textContent = customerDetails.email;
	document.getElementById("viewCustomerBirthday").textContent = dateDayMonthYear(customerDetails.birthDay);
	document.getElementById("viewCustomerAddress").textContent = customerDetails.address;

	// Recipes (ordered from newest to oldest)
	_recipes = (customerDetails.recipes ?? []).sort((a, b) => new Date(b.prescriptionIssueDate) - new Date(a.prescriptionIssueDate));
	_currentRecipeIndex = 0;
	renderRecipe(_currentRecipeIndex);
	updateRecipeNav();

	document.getElementById("btnPrevRecipe").onclick = () => {
		if (_currentRecipeIndex < _recipes.length - 1) {
			_currentRecipeIndex++;
			renderRecipe(_currentRecipeIndex);
			updateRecipeNav();
		}
	};
	document.getElementById("btnNextRecipe").onclick = () => {
		if (_currentRecipeIndex > 0) {
			_currentRecipeIndex--;
			renderRecipe(_currentRecipeIndex);
			updateRecipeNav();
		}
	};
}

const renderRecipe = (index) => {
	const empty = "—";
	const recipe = _recipes.length > 0 ? _recipes[index] : null;
	const val = (v) => v || empty;

	document.getElementById("viewVlOdEsf").textContent = recipe ? val(recipe.vL_OD_ESF) : empty;
	document.getElementById("viewVlOdCil").textContent = recipe ? val(recipe.vL_OD_CIL) : empty;
	document.getElementById("viewVlOdEje").textContent = recipe ? val(recipe.vL_OD_EJE) : empty;
	document.getElementById("viewVlOiEsf").textContent = recipe ? val(recipe.vL_OI_ESF) : empty;
	document.getElementById("viewVlOiCil").textContent = recipe ? val(recipe.vL_OI_CIL) : empty;
	document.getElementById("viewVlOiEje").textContent = recipe ? val(recipe.vL_OI_EJE) : empty;
	document.getElementById("viewVcOdEsf").textContent = recipe ? val(recipe.vC_OD_ESF) : empty;
	document.getElementById("viewVcOdCil").textContent = recipe ? val(recipe.vC_OD_CIL) : empty;
	document.getElementById("viewVcOdEje").textContent = recipe ? val(recipe.vC_OD_EJE) : empty;
	document.getElementById("viewVcOiEsf").textContent = recipe ? val(recipe.vC_OI_ESF) : empty;
	document.getElementById("viewVcOiCil").textContent = recipe ? val(recipe.vC_OI_CIL) : empty;
	document.getElementById("viewVcOiEje").textContent = recipe ? val(recipe.vC_OI_EJE) : empty;
	document.getElementById("viewCustomerAdicion").textContent = recipe ? val(recipe.adicion) : empty;
	document.getElementById("viewCustomerOptometrist").textContent = recipe ? val(recipe.optometrist) : empty;

	const fechaEl = document.getElementById("viewRecipeDate");
	if (recipe?.prescriptionIssueDate) {
		const date = new Date(recipe.prescriptionIssueDate);
		fechaEl.textContent = date.toLocaleDateString("es-EC", { year: "numeric", month: "long", day: "numeric" });
	} else {
		fechaEl.textContent = empty;
	}
}

const updateRecipeNav = () => {
	const total = _recipes.length;
	const navContainer = document.getElementById("recipeNavContainer");
	const label = document.getElementById("recipeNavLabel");
	const btnPrev = document.getElementById("btnPrevRecipe");
	const btnNext = document.getElementById("btnNextRecipe");

	if (total === 0) {
		navContainer.classList.add("d-none");
		return;
	}
	navContainer.classList.remove("d-none");

	// index 0 = receta más reciente = "receta N de N" visualmente
	label.textContent = `${total - _currentRecipeIndex} de ${total}`;
	btnPrev.disabled = _currentRecipeIndex >= total - 1;  // hacia más antigua
	btnNext.disabled = _currentRecipeIndex <= 0;           // hacia más reciente
}

const showDeleteConfirmation = (customerId) => {
	showDeleteConfirmAsync(
		"¿Estás seguro de que deseas eliminar este cliente?",
		"Confirmación de eliminación",
		"warning",
		() => deleteAction(customerId)

	)
}

const deleteAction = async (customerId) => {
	try {
		const response = await fetch(`/Customer/DeleteCustomer?customerId=${customerId}`);
		if (!response.ok) {
			throw new Error(`HTTP error! status: ${response.status}`);
		}
		const data = await response.json();
		return data;
	} catch (error) {
		console.error('Error fetching customer details:', error);
		return null;
	}
}

const searchCustomerAsync = async (query) => {
	try {
		const response = await fetch(`/Customer/SearchCustomer?query=${query}`);
		if (!response.ok) throw new Error(`HTTP error! status: ${response.status}`);
		const html = await response.text();
		document.getElementById('customerGridContainer').innerHTML = html;
		attachGridEvents();
	} catch (error) {
		console.error('Error buscando clientes:', error);
	}
}