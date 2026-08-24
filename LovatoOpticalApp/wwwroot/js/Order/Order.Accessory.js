import { state } from './Order.State.js';

let selectedAccessories = {};
let accessoryCounter = 0;
let selectedAccessoryFromSearch = null;
let debounceTimer = null;

const searchInput = document.getElementById('accessorySearch');
const suggestionsList = document.getElementById('accessorySuggestions');
const addAccessoryBtn = document.getElementById('addAccessoryBtn');
const accessoriesTableBody = document.getElementById('accessoriesTableBody');

async function searchAccessories(query) {
	if (!query || query.trim().length < 2)
		return [];

	const searchTerm = query.toLowerCase();
	const result = await fetch(`/Catalog/SearchAccessory?searchTerm=${encodeURIComponent(query)}`);
	const results = await result.json();

	return results;
}

function displaySuggestions(results) {
	if (results.length === 0) {
		suggestionsList.innerHTML = `
				<div class="list-group-item text-muted">
					<small>No se encontraron accesorios</small>
				</div>
			`;
		suggestionsList.style.display = 'block';
		return;
	}

	suggestionsList.innerHTML = '';

	results.forEach(accessory => {
		const suggestionItem = document.createElement('button');
		suggestionItem.type = 'button';
		suggestionItem.className = 'list-group-item list-group-item-action';
		suggestionItem.innerHTML = `
				<div class="d-flex justify-content-between align-items-start">
					<div>
						<strong>${accessory.name}</strong>
						<br>
						<small class="text-muted">Accesorio</small>
					</div>
					<span class="badge bg-primary">Gs.${accessory.salePrice.toLocaleString('es-PY')}</span>
				</div>
			`;

		suggestionItem.addEventListener('click', function (e) {
			e.preventDefault();
			selectAccessory(accessory);
		});

		suggestionsList.appendChild(suggestionItem);
	});

	suggestionsList.style.display = 'block';
}

function selectAccessory(accessory) {
	selectedAccessoryFromSearch = accessory;
	searchInput.value = accessory.name;
	suggestionsList.style.display = 'none';
	addAccessoryBtn.disabled = false;
}

function updateAccessoriesTable() {
	accessoriesTableBody.innerHTML = '';

	if (Object.keys(selectedAccessories).length === 0) {
		accessoriesTableBody.innerHTML = `
				<tr>
					<td colspan="4" class="text-center text-muted py-4">
						No hay accesorios agregados. Busca uno para comenzar.
					</td>
				</tr>
			`;
		updateTotals();
		return;
	}

	for (let key in selectedAccessories) {
		const item = selectedAccessories[key];
		const subtotal = item.salePrice * item.quantity;

		const row = document.createElement('tr');
		row.innerHTML = `
				<td>
					<strong>${item.name}</strong>
					<br>
					<small class="text-muted">Accesorio</small>
				</td>
				<td class="text-end">Gs. ${item.price.toLocaleString('es-PY')}</td>
				<td>
					<div class="input-group input-group-sm">
						<button class="btn btn-outline-secondary" type="button" onclick="window.decreaseQuantity('${key}')">-</button>
						<input type="text" class="form-control text-center" value="${item.quantity}" readonly style="width: 50px;">
						<button class="btn btn-outline-secondary" type="button" onclick="window.increaseQuantity('${key}')">+</button>
					</div>
				</td>
				<td class="text-end">
					<button class="btn btn-sm" onclick="window.removeAccessory('${key}')">
						<i class="bi bi-trash text-danger"></i>
					</button>
				</td>
			`;
		accessoriesTableBody.appendChild(row);
	}

	updateTotals();
}

window.increaseQuantity = function (key) {
	selectedAccessories[key].quantity++;
	const accesoryId = selectedAccessories[key].id;

	const accesoriesUpadated = state.order.accessories.map(a => {
		if (a.id === accesoryId) {
			return { ...a, quantity: a.quantity + 1 };
		}
		return a;
	});

	state.order.accessories = accesoriesUpadated;

	updateAccessoriesTable();
};

window.decreaseQuantity = function (key) {
	const accesoryId = selectedAccessories[key].id;

	if (selectedAccessories[key].quantity > 1) {
		selectedAccessories[key].quantity--;
		const accesoriesUpadated = state.order.accessories.map(a => {
			if (a.id === accesoryId) {
				return { ...a, quantity: a.quantity - 1 };
			}
			return a;
		});

		state.order.accessories = accesoriesUpadated;

	} else {
		removeAccessory(key);
		return;
	}
	updateAccessoriesTable();
};

window.removeAccessory = function (key) {
	const accesoryId = selectedAccessories[key].id;
	const accesoriesUpadated = state.order.accessories.filter(a => a.id !== accesoryId);
	state.order.accessories = accesoriesUpadated;

	delete selectedAccessories[key];
	updateAccessoriesTable();
};


function updateTotals() {
	let total = 0;

	for (let key in selectedAccessories) {
		const item = selectedAccessories[key];
		total += item.price * item.quantity;
	}

	document.getElementById('subtotalAccessories').textContent = `Gs. ${total.toLocaleString('es-PY')}`;
	document.getElementById('totalAccessories').textContent = `Gs. ${total.toLocaleString('es-PY')}`;
}

updateAccessoriesTable();

export const initializeAccessoryModule = () => {
	document.addEventListener('click', function (e) {
		if (!e.target.closest('#accessorySearch') && !e.target.closest('#accessorySuggestions')) {
			suggestionsList.style.display = 'none';
		}
	});

	searchInput.addEventListener('keypress', function (e) {
		if (e.key === 'Enter' && selectedAccessoryFromSearch) {
			addAccessoryBtn.click();
		}
	});


	addAccessoryBtn.addEventListener('click', function () {
		if (!selectedAccessoryFromSearch) {
			alert('Por favor selecciona un accesorio válido');
			return;
		}

		const accessory = selectedAccessoryFromSearch;
		const uniqueId = `acc-${accessory.id}-${Date.now()}`;

		// Verificar si el accesorio ya existe en la lista
		let existingKey = null;
		for (let key in selectedAccessories) {
			if (selectedAccessories[key].id === accessory.id) {
				existingKey = key;
				break;
			}
		}

		if (existingKey) {
			// Incrementar cantidad si ya existe
			selectedAccessories[existingKey].quantity++;
		} else {
			// Agregar nuevo accesorio
			selectedAccessories[uniqueId] = {
				id: accessory.id,
				name: accessory.name,
				price: accessory.salePrice,
				description: accessory.description,
				quantity: 1
			};
		}

		// Limpiar input y desactivar botón
		searchInput.value = '';
		addAccessoryBtn.disabled = true;
		selectedAccessoryFromSearch = null;

		state.order.accessories = [...state.order.accessories, accessory];

		// Actualizar tabla
		updateAccessoriesTable();
	});


	searchInput.addEventListener('input', function (e) {
		const query = e.target.value.trim();

		clearTimeout(debounceTimer);

		if (!query) {
			suggestionsList.style.display = 'none';
			selectedAccessoryFromSearch = null;
			addAccessoryBtn.disabled = true;
			return;
		}

		debounceTimer = setTimeout(async () => {
			try {
				const results = await searchAccessories(query);
				displaySuggestions(results);
			} catch (error) {
				console.error('Error en búsqueda:', error);
				suggestionsList.innerHTML = `
					<div class="list-group-item text-danger">
						<small>Error al buscar accesorios</small>
					</div>
				`;
			}
		}, 300);
	});
}