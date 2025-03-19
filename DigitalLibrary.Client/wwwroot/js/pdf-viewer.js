window.loadPdfUrl = (pdfUrl, elementId) => {
    document.getElementById(elementId).src = `/readpdfjs/generic/web/viewer_readonly.html?file=${encodeURIComponent(pdfUrl)}`;
};

window.loadPdfBase64 = (base64, elementId) => {
    const byteCharacters = atob(base64.split(',')[1]);
    const byteNumbers = new Array(byteCharacters.length);
    for (let i = 0; i < byteCharacters.length; i++) {
        byteNumbers[i] = byteCharacters.charCodeAt(i);
    }
    const byteArray = new Uint8Array(byteNumbers);
    const blob = new Blob([byteArray], { type: 'application/pdf' });
    const blobUrl = URL.createObjectURL(blob);

    document.getElementById(elementId).src = `/readpdfjs/generic/web/viewer_readonly.html?file=${encodeURIComponent(blobUrl)}`;
};
