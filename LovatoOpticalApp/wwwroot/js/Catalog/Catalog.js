import { initFrame } from "../Frame/Frame.js";
import { getCatalog } from "./Catalog.Grid.js"

initFrame();

getCatalog().then(res => {
	console.log("Catalog:", res);
}).catch(err => {
	console.log("Error fetching catalog:", err);
})