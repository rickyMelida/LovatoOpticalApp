const form = document.getElementById("newFrameForm");
const btnFormSubmit = document.getElementById("btnFormSubmit");

const buildFramePayload = () => {
    const formData = new FormData(form);

    return {
        Type: 0,
        Name: formData.get("Name")?.toString().trim() ?? "",
        Code: formData.get("Code")?.toString().trim() ?? "",
        Material: formData.get("Material")?.toString() ?? "",
        Shape: formData.get("Shape")?.toString() ?? "",
        Color: formData.get("Color")?.toString().trim() ?? "",
        PurchasePrice: parseFloat(formData.get("PurchasePrice") ?? 0) || 0,
        SalePrice: parseFloat(formData.get("SalePrice") ?? 0) || 0,
        Quantity: parseInt(formData.get("Quantity") ?? 0, 10) || 0,
        MinimumQuantity: parseInt(formData.get("MinimumQuantity") ?? 0, 10) || 0,
        Description: formData.get("Description")?.toString().trim() ?? "",
        CreatedBy: "00000000-0000-0000-0000-000000000000"
    };
};

btnFormSubmit.addEventListener("click", async (e) => {
    e.preventDefault();

    if (!form.checkValidity()) {
        form.reportValidity();
        return;
    }

    const framePayload = buildFramePayload();
	console.log("Frame Payload:", framePayload);

    try {
        const response = await fetch("/Catalog/CreateFrame", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(framePayload)
        });

        const data = await response.json();
        console.log("Frame created successfully:", data);
    } catch (error) {
        console.error("Error creating frame:", error);
    }
});