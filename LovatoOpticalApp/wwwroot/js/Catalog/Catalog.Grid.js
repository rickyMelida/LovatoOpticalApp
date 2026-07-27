import { getColorName } from "../Helper/ColorHelper.js";
import { mapProductTypeToEnum, mapFrameMaterialToString, mapFrameTypeToString } from "../Helper/Mappers.js";
import { formatToGuarani } from "../Helper/Helper.js";
import { getProductDetails, showEditProductModal } from "./Catalog.Rules.js"
import { showModal } from "../Common/ModalEvents.js"

export const handlerGridModal = () => {
	const inputCatalogSearch = document.getElementById('inputCatalogSearch');

	inputCatalogSearch.addEventListener('input', async () => {
		const query = inputCatalogSearch.value.trim();
		let debounceTimer;
		clearTimeout(debounceTimer);


		debounceTimer = setTimeout(() => {
			searchCatalogAsync(query);
		}, 500);
	});

	attachGridEvents()
}


const attachGridEvents = () => {
	const viewDetailsButtons = document.querySelectorAll(".view-product-catalog");
	const editButtons = document.querySelectorAll(".edit-product-catalog");
	const deleteButtons = document.querySelectorAll(".deleteProductCatalog");

	viewDetailsButtons.forEach(button => {
		button.addEventListener("click", async (event) => {
			const productId = event.currentTarget.id;
			const productType = mapProductTypeToEnum[event.currentTarget.name];
			await showViewDetailsModal(productId, productType);
		});
	});

	editButtons.forEach(button => {
		button.addEventListener("click", async (event) => {
			const productId = event.currentTarget.id;
			const productType = mapProductTypeToEnum[event.currentTarget.name];
			await showEditProductModal(productId, productType);
		});
	});

	deleteButtons.forEach(button => {
		button.addEventListener("click", (event) => {
			const productId = event.currentTarget.id;
			const productType = mapProductTypeToEnum[event.currentTarget.getAttribute("name")];

			showDeleteConfirmation(productId, productType);
		});
	});
}

const showViewDetailsModal = async (productId, productType) => {
	const productDetails = await getProductDetails(productId, productType);

	renderProductDetails(productDetails);

	showModal("viewFrameModal")
}

const renderProductToEdit = (productDetails) => {
	const newFrameModalLabel = document.getElementById("newFrameModalLabel");
	newFrameModalLabel.innerText = "Editar Armazón"

	console.log({ productDetails })
}

const showDeleteConfirmation = (productId, productType) => {
	showDeleteConfirmAsync(
		"¿Estás seguro de que deseas eliminar este producto?",
		"Confirmación de eliminación",
		"warning",
		() => deleteAction(productId, productType)
	)
}

const deleteAction = async (productId, productType) => {
	try {
		const response = await fetch(`/Catalog/DeleteProduct?productId=${productId}&productType=${productType}`);
		if (!response.ok) {
			throw new Error(`HTTP error! status: ${response.status}`);
		}
		const data = await response.json();
		return data;
	} catch (error) {
		console.error('Error fetching product details:', error);
		return null;
	}
}

const renderProductDetails = (productDetails) => {
	switch (productDetails.type) {
		case 1:
			renderFrameDetails(productDetails);
			break;
		default:
			console.warn("Tipo de producto no reconocido.");
	}
}

const renderFrameDetails = (frameDetails) => {
	const viewFrameName = document.getElementById("viewFrameName");
	const frameCode = document.getElementById("viewFrameCode");
	const frameStock = document.getElementById("viewFrameStockBadge");
	const frameMaterial = document.getElementById("viewFrameMaterial");
	const frameType = document.getElementById("viewFrameType");
	const frameColorLabel = document.getElementById("viewFrameColorLabel");
	const colorSwatch = document.getElementById("viewFrameColorSwatch");
	const purchasePrice = document.getElementById("viewFramePurchasePrice");
	const salePrice = document.getElementById("viewFrameSalePrice");
	const profitMargin = document.getElementById("viewFrameMargin");
	const currentStock = document.getElementById("viewFrameQuantity");
	const minimumStock = document.getElementById("viewFrameMinimumQuantity");
	const description = document.getElementById("viewFrameDescription");
	const profitMarginPercentage = (frameDetails.salePrice - frameDetails.purchasePrice) / frameDetails.purchasePrice * 100;

	viewFrameName.textContent = frameDetails.name;
	frameCode.textContent = frameDetails.code;
	frameStock.textContent = frameDetails.quantity;
	frameMaterial.textContent = mapFrameMaterialToString[frameDetails.material];
	frameType.textContent = mapFrameTypeToString[frameDetails.frameType];
	frameColorLabel.textContent = getColorName(frameDetails.color);
	colorSwatch.style = `width:18px;height:18px;border-radius:50%;display:inline-block;border:1px solid ${frameDetails.color};`
	purchasePrice.textContent = `${formatToGuarani(frameDetails.purchasePrice)}`;
	salePrice.textContent = `${formatToGuarani(frameDetails.salePrice)}`;
	profitMargin.textContent = `${profitMarginPercentage.toFixed(2)}%`;
	currentStock.textContent = frameDetails.quantity;
	minimumStock.textContent = frameDetails.minimumQuantity;
	description.innerHTML = !frameDetails.description ? `<span class="text-muted fst-italic">Sin descripción</span>` : `<span class="text-dark">${frameDetails.description} </span>`
}

const searchCatalogAsync = async (query) => {
	try {
		const response = await fetch(`/Catalog/SearchCatalog?query=${query}`);
		if (!response.ok) throw new Error(`HTTP error! status: ${response.status}`);
		const html = await response.text();
		document.getElementById('catalogGridContainer').innerHTML = html;
		handlerGridModal();
	} catch (error) {
		console.error('Error buscando productos:', error);
	}
}