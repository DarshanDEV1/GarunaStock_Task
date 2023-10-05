const express = require('express');
const cors = require('cors');
const stocksData = require('./stocks.json'); // Assuming you have your JSON data in a file named stocks.json

const app = express();
const port = process.env.PORT || 3000;

app.use(cors());

// Define a route to serve the entire JSON data
app.get('/api/stocks', (req, res) => {
  res.json(stocksData);
});

// Define a route to search for stocks by symbol
app.get('/api/stocks/search', (req, res) => {
  const { symbol } = req.query;
  if (!symbol) {
    return res.status(400).json({ error: 'Symbol parameter is required' });
  }

  const results = stocksData.stocks.filter((stock) =>
    stock.symbol.toLowerCase().includes(symbol.toLowerCase())
  );

  res.json(results);
});

app.listen(port, () => {
  console.log(`Server is running on port ${port}`);
});


