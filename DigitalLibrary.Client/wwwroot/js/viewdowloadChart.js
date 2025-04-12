window.checkCanvasExists = (canvasId) => {
    return document.getElementById(canvasId) !== null;
};

window.renderViewDownloadChart = (chartData) => {
    const ctx = document.getElementById('viewDownloadChart');
    const labels = chartData.map(d => d.documentName);
    const views = chartData.map(d => d.views);
    const downloads = chartData.map(d => d.downloads);

    new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [
                {
                    label: 'Lượt xem',
                    backgroundColor: '#3498db',
                    data: views
                },
                {
                    label: 'Lượt tải',
                    backgroundColor: '#2ecc71',
                    data: downloads
                }
            ]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                y: {
                    beginAtZero: true,
                    ticks: {
                        stepSize: 1
                    }
                }
            }
        }
    });
}
