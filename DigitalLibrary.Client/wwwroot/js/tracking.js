window.getBrowserInfo = () => {
    return {
        useragent: navigator.userAgent,
        url: window.location.href
    };
};
