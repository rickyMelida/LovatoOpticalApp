import { enableButton } from '../Common/ButtonEvents.js';
import { hideModal } from "../Common/ModalEvents.js";

const form = document.getElementById("newCustomerForm");
const btnSaveButton = document.getElementById("btnSaveCustomer");

export const buildCustomerPayload = () => {
    const formData = new FormData(form);

    return {
        Name: formData.get("Name")?.toString().trim() ?? "",
        CiRuc: formData.get("CiRuc")?.toString().trim() ?? "",
        BirthDay: formData.get("Birthday")?.toString() ?? "",
        Address: formData.get("Address")?.toString().trim() ?? "",
        Phone: formData.get("Phone")?.toString().trim() ?? "",
        Email: formData.get("Email")?.toString().trim() ?? ""
    };
};

export const buildRecipePayload = (formElement) => {
    const formData = new FormData(formElement);

    return {
        PrescriptionIssueDate: formData.get("PrescriptionIssueDate")?.toString() ?? "",
        Optometrist: formData.get("Optometrist")?.toString().trim() ?? "",
        VL_OD_ESF: formData.get("VL_OD_ESF")?.toString().trim() ?? "0",
        VL_OD_CIL: formData.get("VL_OD_CIL")?.toString().trim() ?? "0",
        VL_OD_EJE: formData.get("VL_OD_EJE")?.toString().trim() ?? "0",

        // Visión Lejana (VL) - Ojo Izquierdo (OI)
        VL_OI_ESF: formData.get("VL_OI_ESF")?.toString().trim() ?? "0",
        VL_OI_CIL: formData.get("VL_OI_CIL")?.toString().trim() ?? "0",
        VL_OI_EJE: formData.get("VL_OI_EJE")?.toString().trim() ?? "0",

        // Visión Cercana (VC) - Ojo Derecho (OD)
        VC_OD_ESF: formData.get("VC_OD_ESF")?.toString().trim() ?? "0",
        VC_OD_CIL: formData.get("VC_OD_CIL")?.toString().trim() ?? "0",
        VC_OD_EJE: formData.get("VC_OD_EJE")?.toString().trim() ?? "0",

        // Visión Cercana (VC) - Ojo Izquierdo (OI)
        VC_OI_ESF: formData.get("VC_OI_ESF")?.toString().trim() ?? "0",
        VC_OI_CIL: formData.get("VC_OI_CIL")?.toString().trim() ?? "0",
        VC_OI_EJE: formData.get("VC_OI_EJE")?.toString().trim() ?? "0",
        Adicion: formData.get("Adicion")?.toString().trim() ?? "0"
    }
}

export const resetForm = (formElement, btnSubmit) => {
    formElement.reset();
    enableButton(btnSubmit, false);
}

export const initForm = () => {
    btnSaveButton.addEventListener("click", async (e) => {
        e.preventDefault();
        const isNewCustomer = btnSaveButton.innerText != "Editar";

        enableButton(btnSaveButton, true);

        if (!form.checkValidity()) {
            form.reportValidity();
            enableButton(btnSaveButton, false);
            return;
        }

        const customerPayload = buildCustomerPayload();
        const recipePayload = buildRecipePayload(form);

        const result = await saveCustomer(customerPayload, recipePayload, isNewCustomer);
        const alertTitle = isNewCustomer ? "Cliente Creado" : "Cliente Modificado";

        showAlert(result.message, alertTitle, "success")
            .then(() => {
                resetForm(form, btnSaveButton);
                hideModal("newCustomerModal");
				reloadCurrentPage();
            })
    })
}

const saveCustomer = async (customerPayload, recipePayload, isNewCustomer) => {
    let request = { Customer: customerPayload, Recipe: recipePayload };
    let url = `/Customer/create-customer-recipe`;

    if (!isNewCustomer) {
        const customerId = document.getElementById("newCustomerModalLabel").getAttribute("data-customer-id");
        const recipeId = document.getElementById("newCustomerModalLabel").getAttribute("data-recipe-id");
        const customerRequest = { ...customerPayload, id: customerId };
        const recipeRequest = { ...recipePayload, id: recipeId, customerId };

        request = { Customer: customerRequest, Recipe: recipeRequest };
        url = `/Customer/Update`;

    }

    const result = await fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
        },
        body: JSON.stringify(request)
    });

    return result.json();
}

