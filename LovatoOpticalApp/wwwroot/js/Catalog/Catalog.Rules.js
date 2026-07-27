import { showModal } from "../Common/ModalEvents.js";
import { mapProductModal } from "../Helper/Mappers.js"


const renderEditProductFrom = (productDetails, productId, productType) => {
	switch (productType) {
		case 1:
			renderEditFrameFrom(productDetails, productId);
			break;
		case 2:
			renderEditCrystalModal(productDetails, productId);
			break;
		case 3:
			renderEditAccessoryModal(productDetails, productId);
			break;
		default:
			break;
	}
}

const renderEditFrameFrom = (productDetails, productId) => {
	const newFrameModalLabel = document.getElementById("newFrameModalLabel");
	const btnFormSubmit = document.getElementById("btnFormSubmit");
	const frameName = document.getElementById("frameName");
	const frameCode = document.getElementById("frameCode");
	const frameMaterial = document.getElementById("frameMaterial");
	const frameType = document.getElementById("frameType");
	const frameColor = document.getElementById("frameColor");
	const framePurchasePrice = document.getElementById("framePurchasePrice");
	const frameSalePrice = document.getElementById("frameSalePrice");
	const frameQuantity = document.getElementById("frameQuantity");
	const frameMinimumQuantity = document.getElementById("frameMinimumQuantity");
	const frameDescription = document.getElementById("frameDescription");

    newFrameModalLabel.setAttribute("data-product-id", productId);
	newFrameModalLabel.innerHTML = `<i class="bi bi-pencil me-2"></i>Editar Armazón`;
	btnFormSubmit.innerHTML = `<i class="bi bi-pencil me-1 edit-product"></i>Editar`;
	frameName.value = productDetails.name;
	frameCode.value = productDetails.code;
	frameMaterial.value = productDetails.material;
	frameType.value = productDetails.frameType;
	frameColor.value = productDetails.color;
	framePurchasePrice.value = productDetails.purchasePrice;
	frameSalePrice.value = productDetails.salePrice;
	frameQuantity.value = productDetails.quantity;
	frameMinimumQuantity.value = productDetails.minimumQuantity
	frameDescription.value = productDetails.description;
}

const renderEditCrystalModal = (productDetails, productId) => {

}

const renderEditAccessoryModal = (productDetails, productId) => {

}

export const showEditProductModal = async (productId, productType) => {
	const productDetails = await getProductDetails(productId, productType);

	renderEditProductFrom(productDetails, productId, productType);

	showModal(mapProductModal[productType]);
}

export const getProductDetails = async (productId, productType) => {
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