import { showModal } from "../Common/ModalEvents.js";

document.addEventListener("click", (e) => {
    const btn = e.target.closest(".btn-agregar-stock");
    if (!btn) return;

    const productId = btn.dataset.productId;
    const productName = btn.dataset.productName;
    const productType = btn.dataset.productType;

    document.getElementById("modalProductId").value = productId;
    document.getElementById("modalProductName").value = productName;
    document.getElementById("modalProductType").value = productType;
    document.getElementById("modalQuantityToAdd").value = 1;

    showModal("addInventoryModal");
});
