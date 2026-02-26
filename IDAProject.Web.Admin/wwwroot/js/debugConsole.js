// Create a debug console div if it doesn't already exist
(function () {
    if (!document.getElementById('debugConsole')) {
        const debugConsoleDiv = document.createElement('div');
        debugConsoleDiv.id = 'debugConsole';
        debugConsoleDiv.style.cssText = `
            font-family: monospace;
            white-space: pre-wrap;
            background-color: #f3f3f3;
            padding: 10px;
            max-height: 300px;
            overflow-y: scroll;
            border: 1px solid #ddd;
        `;
        document.body.appendChild(debugConsoleDiv);
    }
})();

// Reference to the HTML debug console
const debugConsole = document.getElementById('debugConsole');

// Function to append messages to the HTML debug console
function appendToConsole(message) {
    const time = new Date().toLocaleTimeString();
    debugConsole.innerText += `[${time}] ${message}\n`;
    debugConsole.scrollTop = debugConsole.scrollHeight; // Auto-scroll to bottom
}

// Capture and display all console messages
['log', 'warn', 'error', 'info', 'debug'].forEach(method => {
    const originalMethod = console[method];

    console[method] = (...args) => {
        // Format the message exactly as it would appear in the console
        const formattedMessage = args.map(arg => {
            if (typeof arg === 'object') {
                try {
                    return JSON.stringify(arg, null, 2); // Pretty-print objects
                } catch (error) {
                    return '[Circular]';
                }
            }
            return String(arg);
        }).join(' ');

        // Append to the HTML debug console
        appendToConsole(`${method.toUpperCase()}: ${formattedMessage}`);

        // Also output to the actual console
        originalMethod.apply(console, args);
    };
});

// Capture unhandled promise rejections
window.addEventListener("unhandledrejection", event => {
    appendToConsole(`Unhandled Promise Rejection: ${event.reason}`);
});

// Capture global errors
window.onerror = function (message, source, lineno, colno, error) {
    appendToConsole(`Global Error: ${message} at ${source}:${lineno}:${colno}`);
    return true; // Prevents the default browser error handling
};

window.addEventListener("error", event => {
    if (event.error) {
        appendToConsole(`Error: ${event.error.message} at ${event.filename}:${event.lineno}:${event.colno}`);
    } else if (event.message) {
        appendToConsole(`Error: ${event.message}`);
    }
});

// Override fetch to log errors
const originalFetch = window.fetch;
window.fetch = function (...args) {
    return originalFetch(...args)
        .then(response => {
            if (!response.ok) {
                appendToConsole(`Fetch error: ${response.status} ${response.statusText} for ${args[0]}`);
            }
            return response;
        })
        .catch(error => {
            appendToConsole(`Fetch exception: ${error} for ${args[0]}`);
            throw error;  // Rethrow error to ensure normal behavior
        });
};

// Override XMLHttpRequest to log errors
const originalXhrOpen = XMLHttpRequest.prototype.open;
XMLHttpRequest.prototype.open = function (method, url, ...args) {
    this.addEventListener("error", () => appendToConsole(`XHR error: ${method} ${url}`));
    this.addEventListener("load", () => {
        if (this.status >= 400) {
            appendToConsole(`XHR load error: ${method} ${url} returned ${this.status}`);
        }
    });
    originalXhrOpen.apply(this, [method, url, ...args]);
};
