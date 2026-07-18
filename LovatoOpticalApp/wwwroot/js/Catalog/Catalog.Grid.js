export const  getCatalog = async () => {
	const response = await fetch("/Catalog/GetProductCatalog?pageNumber=1&pageSize=10", {
		method: "GET",
		headers: {
			"Content-Type": "application/json"
		}
	});
	if (!response.ok) {
		throw new Error("Error al obtener los armazones");
	}

	const data = await response.json();

	return data;
}

