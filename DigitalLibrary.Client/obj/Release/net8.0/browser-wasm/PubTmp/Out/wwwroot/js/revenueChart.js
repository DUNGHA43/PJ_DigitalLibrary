window.checkCanvasExists = (canvasId) => {
    return document.getElementById(canvasId) !== null;
};

window.renderRevenueChart = (labels, data) => {
    const ctx = document.getElementById('revenueChart').getContext('2d');

    if (window.revenueChartInstance) {
        window.revenueChartInstance.destroy();
    }

    window.revenueChartInstance = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Doanh thu (VNĐ)',
                data: data,
                backgroundColor: [
                    'rgba(52, 152, 219, 0.7)', 
                    'rgba(155, 89, 182, 0.7)'  
                ],
                borderColor: [
                    'rgba(41, 128, 185, 1)',   
                    'rgba(142, 68, 173, 1)'     
                ],
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                y: {
                    beginAtZero: true,
                    ticks: {
                        autoSkip: true,
                        maxRotation: 45,
                        minRotation: 45,
                        callback: function (value) {
                            return value.toLocaleString('vi-VN');
                        }
                    }
                }
            },
            plugins: {
                legend: {
                    display: false
                }
            }
        }
    });
}

window.renderMonthlyRevenueChart = (labels, datasets) => {
    const ctx = document.getElementById('monthlyRevenueChart').getContext('2d');

    if (window.monthlyRevenueChartInstance) {
        window.monthlyRevenueChartInstance.destroy();
    }


    const chartDatasets = datasets.map(dataset => ({
        label: dataset.label,
        backgroundColor: dataset.backgroundColor,
        data: dataset.data.map(item => item.revenue),
        customData: dataset.data
    }));

    window.monthlyRevenueChartInstance = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: chartDatasets
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                y: {
                    beginAtZero: true,
                    ticks: {
                        callback: function (value) {
                            return value.toLocaleString('vi-VN');
                        }
                    }
                }
            },
            plugins: {
                tooltip: {
                    callbacks: {
                        label: function (context) {
                            const dataset = context.dataset;
                            const dataIndex = context.dataIndex;
                            const transaction = dataset.customData[dataIndex].transaction;
                            const revenue = context.raw;
                            return [
                                `Doanh thu: ${revenue.toLocaleString('vi-VN')}đ`,
                                `Số giao dịch: ${transaction}`
                            ];
                        }
                    }
                }
            }
        }
    });
};

