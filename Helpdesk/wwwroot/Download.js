
console.log("Download.js loaded!!");

window.downloadFileFromBase64 = (base64Data, contentType, fileName) => {
    const link = document.createElement('a');
    link.href = `data:${contentType};base64,${base64Data}`;
    link.download = fileName;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
};