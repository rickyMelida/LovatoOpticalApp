import { getColorName } from "../Helper/ColorHelper.js";
import { mapProductTypeToEnum, mapFrameMaterialToString, mapFrameTypeToString } from "../Helper/Mappers.js";
import { formatToGuarani } from "../Helper/Helper.js";

export const handlerGridModal = () => {
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
		button.addEventListener("click", (event) => {
			const productId = event.currentTarget.id;
			showEditModal(productId);
		});
	});

	deleteButtons.forEach(button => {
		button.addEventListener("click", (event) => {
			const productId = event.currentTarget.id;
			showDeleteConfirmation(productId);
		});
	});
}

const showViewDetailsModal = async (productId, productType) => {
	const modalElement = document.getElementById("viewFrameModal");
	if (!modalElement) {
		console.warn("Modal element #viewFrameModal not found.");
		return;
	}
	const modalInstance = bootstrap.Modal.getOrCreateInstance(modalElement);
	const productDetails = await getProductDetails(productId, productType);

	renderProductDetails(productDetails);

	modalInstance.show();
}

const showEditModal = (productId) => {
	const modalElement = document.getElementById("newFrameModal");
	if (!modalElement) {
		console.warn("Modal element #newFrameModal not found.");
		return;
	}
	const modalInstance = bootstrap.Modal.getOrCreateInstance(modalElement);
	modalInstance.show();
}

const showDeleteConfirmation = (productId) => {
	showDeleteConfirm("¿Estás seguro de que deseas eliminar este producto?", "Confirmación de eliminación", "warning")
		.then((result) => {
			if (result.isConfirmed) {
				console.log(`Product with ID ${productId} deleted.`);
			}
		});
}

const getProductDetails = async (productId, productType) => {
	try {
		const response = await fetch(`/Catalog/GetProductDetails?productId=${productId}&productType=${productType}`);
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
}

