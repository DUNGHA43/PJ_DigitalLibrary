window.checkCanvasExists = (canvasId) => {
    return document.getElementById(canvasId) !== null;
};

window.renderDailyTrafficChart = function (data) {
    console.log("Rendering chart with labels:", data.labels);
    console.log("Rendering chart with data:", data.data);

    const ctx = document.getElementById('dailyTrafficChart');
    if (ctx) {
        new Chart(ctx, {
            type: 'bar',
            data: {
                labels: data.labels,
                datasets: [{
                    label: 'Lượt truy cập',
                    data: data.data,
                    backgroundColor: 'rgba(52, 152, 219, 0.7)',
                    borderColor: 'rgba(52, 152, 219, 1)',
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
                            precision: 0
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
};

