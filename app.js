const API_BASE_URL = 'https://localhost:7283/api';

// Get all stocks
async function getAllStocks() {
    try {
        const response = await fetch(`${API_BASE_URL}/stocks`);
        const data = await response.json();
        displayStocks(data);
    } catch (error) {
        document.getElementById('stocksList').innerHTML = `<p>Error: ${error.message}</p>`;
    }
}

// Display stocks list
function displayStocks(stocks) {
    const container = document.getElementById('stocksList');
    if (stocks.length === 0) {
        container.innerHTML = '<p>No stocks found</p>';
        return;
    }
    
    let html = '<ul>';
    stocks.forEach(stock => {
        html += `
            <li>
                <strong>ID:</strong> ${stock.id || 'N/A'} | 
                <strong>Industry:</strong> ${stock.industry} | 
                <strong>Market Cap:</strong> ${stock.marketCap} | 
                <strong>Last Div:</strong> ${stock.lastDiv}
            </li>
        `;
    });
    html += '</ul>';
    container.innerHTML = html;
}

// Create stock form handler
document.getElementById('createStockForm').addEventListener('submit', async (e) => {
    e.preventDefault();
    
    const stockData = {
        symbol: document.getElementById('symbol').value,
        companyName: document.getElementById('companyName').value,
        purchase: parseFloat(document.getElementById('purchase').value),
        lastDiv: parseFloat(document.getElementById('lastDiv').value),
        industry: document.getElementById('industry').value,
        marketCap: parseInt(document.getElementById('marketCap').value)
    };
    
    try {
        const response = await fetch(`${API_BASE_URL}/stocks`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(stockData)
        });
        
        const result = await response.json();
        if (response.ok) {
            document.getElementById('createStockResult').innerHTML = 
                `<p style="color: green;">Stock created successfully! ID: ${result.id}</p>`;
            // Clear form
            e.target.reset();
            // Refresh stocks list
            getAllStocks();
        } else {
            document.getElementById('createStockResult').innerHTML = 
                `<p style="color: red;">Error: ${JSON.stringify(result)}</p>`;
        }
    } catch (error) {
        document.getElementById('createStockResult').innerHTML = 
            `<p style="color: red;">Error: ${error.message}</p>`;
    }
});

// Get all comments
async function getAllComments() {
    try {
        const response = await fetch(`${API_BASE_URL}/comment`);
        const data = await response.json();
        displayComments(data);
    } catch (error) {
        document.getElementById('commentsList').innerHTML = `<p>Error: ${error.message}</p>`;
    }
}

// Display comments list
function displayComments(comments) {
    const container = document.getElementById('commentsList');
    if (comments.length === 0) {
        container.innerHTML = '<p>No comments found</p>';
        return;
    }
    
    let html = '<ul>';
    comments.forEach(comment => {
        html += `
            <li>
                <strong>ID:</strong> ${comment.id} | 
                <strong>Title:</strong> ${comment.title} | 
                <strong>Content:</strong> ${comment.content} | 
                <strong>Stock ID:</strong> ${comment.stockId} |
                <strong>Created:</strong> ${new Date(comment.createdOn).toLocaleString()}
            </li>
        `;
    });
    html += '</ul>';
    container.innerHTML = html;
}

// Create comment form handler
document.getElementById('createCommentForm').addEventListener('submit', async (e) => {
    e.preventDefault();
    
    const stockId = parseInt(document.getElementById('commentStockId').value);
    const commentData = {
        title: document.getElementById('commentTitle').value,
        content: document.getElementById('commentContent').value
    };
    
    try {
        const response = await fetch(`${API_BASE_URL}/comment/${stockId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(commentData)
        });
        
        const result = await response.json();
        if (response.ok) {
            document.getElementById('createCommentResult').innerHTML = 
                `<p style="color: green;">Comment created successfully! ID: ${result.id}</p>`;
            // Clear form
            e.target.reset();
            // Refresh comments list
            getAllComments();
        } else {
            document.getElementById('createCommentResult').innerHTML = 
                `<p style="color: red;">Error: ${result}</p>`;
        }
    } catch (error) {
        document.getElementById('createCommentResult').innerHTML = 
            `<p style="color: red;">Error: ${error.message}</p>`;
    }
});
