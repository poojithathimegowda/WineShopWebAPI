# 🍷 Wine Store API

Welcome to the Wine Store API! This API is designed to manage a wine store, including inventory, orders, products, and more. It is built using **ASP.NET Core Web API** and uses **JWT tokens** for authentication and **Microsoft Identity** for authorization.

## 🛠️ Technologies Used
- **Framework**: ASP.NET Core Web API
- **Authentication**: JWT Tokens
- **Authorization**: Microsoft Identity
- **Language**: C#


## 🌐 Base URL

The API is hosted at Microsoft Azure:
```
https://wineshopwebapi20240423191025.azurewebsites.net/
```

## 🔐 Authentication

This API uses JWT tokens for authentication. You need to include a valid JWT token in the Authorization header of your requests.

- **Register Admin**: Register a new admin user. `POST /api/Admin/register`
- **Authenticate**: Authenticate and get a JWT token. `POST /api/Authenticate/login`

## 📚 API Endpoints

### 💸 Expenses
Manage store expenses with the following endpoints:

- **Get All Expenses**: `GET /api/Expenses`
- **Get Expense by ID**: `GET /api/Expenses/{id}`
- **Create Expense**: `POST /api/Expenses`
- **Update Expense**: `PUT /api/Expenses/{id}`
- **Delete Expense**: `DELETE /api/Expenses/{id}`

### 📦 Inventory
Manage inventory items with the following endpoints:

- **Get All Inventory Items**: `GET /api/Inventory`
- **Get Inventory Item by ID**: `GET /api/Inventory/{id}`
- **Create Inventory Item**: `POST /api/Inventory`
- **Update Inventory Item**: `PUT /api/Inventory/{id}`
- **Delete Inventory Item**: `DELETE /api/Inventory/{id}`

### 🛒 Orders
Manage customer orders with the following endpoints:

- **Get All Orders**: `GET /api/Orders`
- **Get Order by ID**: `GET /api/Orders/{id}`
- **Create Order**: `POST /api/Orders`
- **Update Order**: `PUT /api/Orders/{id}`
- **Delete Order**: `DELETE /api/Orders/{id}`

### 🍾 Products
Manage products available in the store with the following endpoints:

- **Get All Products**: `GET /api/Products`
- **Get Product by ID**: `GET /api/Products/{id}`
- **Create Product**: `POST /api/Products`
- **Update Product**: `PUT /api/Products/{id}`
- **Delete Product**: `DELETE /api/Products/{id}`
- **AutoComplete Products**: `GET /api/Products/AutoComplete`

### 🏪 Shop
Manage shop details and profit/loss calculations with the following endpoints:

- **Get Shop Details**: `GET /api/Shop`
- **Get Shop by ID**: `GET /api/Shop/{id}`
- **AutoComplete Shop**: `GET /api/Shop/AutoComplete`
- **Get Profit and Loss**: `GET /api/Shop/GetProfitAndLoss`

### 🤝 Suppliers
Manage suppliers with the following endpoints:

- **Get All Suppliers**: `GET /api/Suppliers`
- **Get Supplier by ID**: `GET /api/Suppliers/{id}`
- **Create Supplier**: `POST /api/Suppliers`
- **Update Supplier**: `PUT /api/Suppliers/{id}`
- **Delete Supplier**: `DELETE /api/Suppliers/{id}`
- **AutoComplete Suppliers**: `GET /api/Suppliers/AutoComplete`


For more detailed information about each endpoint, refer to the OpenAPI/Swagger documentation provided in the API. 

[OpenAPI/Swagger Documentation](https://wineshopwebapi20240423191025.azurewebsites.net/swagger/index.html)
