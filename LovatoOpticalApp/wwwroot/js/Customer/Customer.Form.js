import { enableButton } from '../Common/ButtonEvents.js';
import { hideModal } from "../Common/ModalEvents.js";

const form = document.getElementById("newCustomerForm");
const btnSaveButton = document.getElementById("btnSaveCustomer");

const buildCustomerPayload = () => {
    const formData = new FormData(form);
    const name = formData.get("Name")?.toString().trim() ?? "";
    const lastName = formData.get("Lastname")?.toString().trim() ?? "";

    return {
        Name: `${name} ${lastName}` ?? "",
        CiRuc: formData.get("CiRuc")?.toString().trim() ?? "",
        BirthDay: formData.get("Birthday")?.toString() ?? "",
        Address: formData.get("Address")?.toString().trim() ?? "",
        Phone: formData.get("Phone")?.toString().trim() ?? ""
    };
};

const resetForm = () => {
    form.reset();
    enableButton(btnSaveButton, false);
}


export const initForm = () => {
    btnSaveButton.addEventListener("click", async (e) => {
        e.preventDefault();
        enableButton(btnSaveButton, true);

        if (!form.checkValidity()) {
            form.reportValidity();
            enableButton(btnSaveButton, false);
            return;
        }

        const customerPayload = buildCustomerPayload();
        const result = await saveCustomer(customerPayload);

        showAlert(result.message, "Cliente Creaddo", "success")
            .then(() => {
                resetForm();
                hideModal("newCustomerModal");
            })
    })
}

const saveCustomer = async (customerPayload) => {
    const result = await fetch("/Customer/Create", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
        },
        body: JSON.stringify(customerPayload)
    });

    return result.json();
}