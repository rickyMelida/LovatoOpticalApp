export const crystalShortFormatt = (recipe, eyeSide) => {
    const graduation = recipe[eyeSide];
    return `
            <strong>${eyeSide}:</strong> 
                ${graduation.sphere} 
                ${graduation.cylinder} 
                ${graduation.axis} 
           `;
}