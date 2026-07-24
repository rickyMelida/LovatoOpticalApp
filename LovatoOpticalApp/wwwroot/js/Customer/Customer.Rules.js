import { showModal } from "../Common/ModalEvents.js";

const renderEditCustomerForm = (customerDetails) => {
	console.log({ customerDetails });
	const customerId = customerDetails.id;
	const recipeId = customerDetails.recipes[0].id;
	const { name, ciRuc, phone, email, birthDay, address } = customerDetails;
	const { adicion, prescriptionIssueDate, optometrist } = customerDetails.recipes[0];
	const {
		vL_OD_ESF,
		vL_OD_CIL,
		vL_OD_EJE,
		vL_OI_ESF,
		vL_OI_CIL,
		vL_OI_EJE,
		vC_OD_ESF,
		vC_OD_CIL,
		vC_OD_EJE,
		vC_OI_ESF,
		vC_OI_CIL,
		vC_OI_EJE,
	} = customerDetails.recipes[0];

	document.getElementById("newCustomerModalLabel").innerHTML = '<i class="bi bi-pencil me-2"></i>Editar Cliente';
	document.getElementById("newCustomerModalLabel").setAttribute("data-customer-id", customerId);
	document.getElementById("newCustomerModalLabel").setAttribute("data-recipe-id", recipeId);


	document.getElementById("btnSaveCustomer").innerHTML = '<i class="bi bi-pencil me-1"></i>Editar';

	document.getElementById("customerName").value = name;
	document.getElementById("customerCiRuc").value = ciRuc
	document.getElementById("customerPhone").value = phone;
	document.getElementById("customerEmail").value = email;
	document.getElementById("customerBirthDay").value = birthDay.split("T")[0];
	document.getElementById("customerAddress").value = address;

	document.getElementById("prescriptionIssueDate").value = prescriptionIssueDate.split("T")[0];
	document.getElementById("optometrist").value = optometrist;

	document.getElementById("vl-od-esf").value = vL_OD_ESF;
	document.getElementById("vl-od-cil").value = vL_OD_CIL;
	document.getElementById("vl-od-eje").value = vL_OD_EJE;

	document.getElementById("vl-oi-esf").value = vL_OI_ESF;
	document.getElementById("vl-oi-cil").value = vL_OI_CIL;
	document.getElementById("vl-oi-eje").value = vL_OI_EJE;

	document.getElementById("f-adicion").value = adicion;

	document.getElementById("vc-od-esf").value = vC_OD_ESF;
	document.getElementById("vc-od-cil").value = vC_OD_CIL;
	document.getElementById("vc-od-eje").value = vC_OD_EJE;

	document.getElementById("vc-oi-esf").value = vC_OI_ESF;
	document.getElementById("vc-oi-cil").value = vC_OI_CIL;
	document.getElementById("vc-oi-eje").value = vC_OI_EJE;
}

export const showEditCustomerModal = async (customerId) => {
	const customerDetails = await getCustomerDetails(customerId);

	renderEditCustomerForm(customerDetails);

	showModal("newCustomerModal");
}

export const getCustomerDetails = async (customerId) => {
	try {
		const response = await fetch(`/Customer/GetCustomerDetails?customerId=${customerId}`);
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
