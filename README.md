# 📦 Magzyn: Desktop Inventory Management

![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white) ![Windows Forms](https://img.shields.io/badge/Windows%20Forms-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)

**Magzyn** is a lightweight, intuitive Windows desktop application built with C# and Windows Forms (WinForms). Designed for straightforward inventory and warehouse management, it provides an easy-to-use graphical interface for tracking products, updating details, and keeping your storage data organized.

## ✨ Features

* **Secure Access:** Built-in login system (`Login.cs`) to ensure only authorized users can manage inventory.
* **Product Management:** Easily add new items to your warehouse using a dedicated interface (`AddProductForm.cs`).
* **Dynamic Updates:** Modify existing stock and change product details on the fly (`ChangeDetail.cs`).
* **Main Dashboard:** A central hub (`Form1.cs`) to view and interact with your current inventory.
* **Lightweight Storage:** Uses local flat-file storage (`data.txt`) for quick setup without the need for complex database configurations.

## 🛠️ Tech Stack

* **Language:** C#
* **Framework:** .NET / Windows Forms (WinForms)
* **IDE:** Visual Studio

## 🚀 Getting Started

Follow these steps to get a local copy up and running on your Windows machine.

### Prerequisites

* [Visual Studio](https://visualstudio.microsoft.com/) (Community edition is fine) with the **.NET desktop development** workload installed.

### Installation & Execution

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/Krychq/Magzyn.git
    ```
2.  **Open the project:**
    Navigate to the cloned directory and double-click the `Magzyn.sln` file to open it in Visual Studio.
3.  **Build and Run:**
    Press `F5` or click the **Start** button at the top of Visual Studio to compile and launch the application.

## 📂 Project Structure Snapshot

A quick look at the core components of the application:

* **`Login`**: Handles user authentication.
* **`Form1`**: The primary dashboard and main user interface.
* **`AddProductForm`**: The data entry window for new inventory.
* **`ChangeDetail`**: The interface for editing existing products.
* **`data.txt`**: The local text file where inventory records are persistently saved.

## 🤝 Contributing

Contributions, issues, and feature requests are welcome! If you have ideas to improve this tool (like migrating to a SQLite database, adding export features, or expanding the UI), feel free to fork the repository and submit a pull request.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License. Feel free to use it for your own educational purposes!
