export const handleSaveCustomer = () => {
    const name = document.getElementById("customerName").value;
    const lastName = document.getElementById("customerLastname").value;
    const ciRuc = document.getElementById("customerCiRuc").value;
    const birthDate = document.getElementById("customerBirthDay").value;
    const address = document.getElementById("customerAddress").value;
    const phoneNumber = document.getElementById("customerPhone").value;

    const customer = {
        Name: `${name} ${lastName}`,
        CiRuc: ciRuc,
        BirthDay: birthDate,
        Address: address,
        Phone: phoneNumber
    };

    fetch("/Customer/Create", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(customer)
    })
    .then(response => response.json())
    .then(data => {
        console.log("Customer created successfully:", data);
    })
    .catch(error => {
        console.error("Error creating customer:", error);
    });
}