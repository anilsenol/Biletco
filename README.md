# 🚀 Biletco
Biletco is an ASP.NET MVC project I developed for my web programming course, inspired by Biletix.com. It serves as an event management and registration system, designed to make planning and attending events effortless. As my first project using ASP.NET, Biletco allowed me to deepen my understanding of MVC architecture and tackle complex CRUD operations, enhancing both my technical skills and problem-solving abilities.

## 🔥 Features
- ✨ **CRUD Functionality**– Supports Create, Read, Update, and Delete operations for events .
- 🛠️ **MVC Architecture & Clean Code** – Implements the Model-View-Controller (MVC) design pattern with a focus on clean, maintainable code.
- 🌐 **MSSQL Database Integration** – Utilizes MSSQL for efficient and reliable data storage.
- 🔒 **ASP.NET Core Identity** - ASP.NET Core Identity handles user authentication, authorization, and management.
- 🛡️ **User Authentication** – Features Login, Registration, and Main Pages to ensure secure access and smooth navigation.
- 👻 **Role-based login** - Redirects to different pages based on credentials.
- 📢 **Name-based filtering** - Filters the events by their name and categories on the main page.
- 🔍 **Save Your Favorites** - Saves your favourite events to a list.

## 🚧 **Notes**
- Password complexity requirements are intentionally disabled to improve testability. This can be modified in `Program.cs`.
- The admin credentials are:  
  - Email: `admin@biletco.com`  
  - Password: `biletco`
- The project uses **MSSQL Local Database** by default, but you can change the database settings in `appsettings.json`.
- The project does not include payment functionality.

## 🛠️ Prerequisites  
Ensure you have the following installed:  
- 🔹 [.NET SDK](https://dotnet.microsoft.com/en-us/download) (Version **X.X** or later)  
- 🔹 [Visual Studio](https://visualstudio.microsoft.com/) (Recommended)  
- 🔹 SQL Server (LocalDB or Microsoft SQL Server)  
- 🔹 Entity Framework

## 📦 **Installation**  
Follow these steps to set up the project on your local environment:  

1. **Installation media**  
   - LocalDB is a feature you select during SQL Server Express installation, and is available when you download the media. If you download the media, either choose Express Advanced or the LocalDB package.
   - [Download Link for the latest SQL Server 2022 Express edition](https://go.microsoft.com/fwlink/?linkid=2215160)
     
2. **Clone the Repository**  
   - Download the project files using the following command:  
     ```bash
     git clone https://github.com/emirsapmaz/Biletco.git
     cd repository
     ```
     
3. **Import the Project**  
   - Open your **IDE (Visual Studio, etc.)** and import the project.  

4. **Change database in needed**
   - Open the `appsettings.json` file and update the database connection string:  
  ```json
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=YourDatabase;Trusted_Connection=True;"
  }
  ```

5. **Restore Dependencies**
  - Open the **Package Manager Console** and run the following command:  
    ```sh
    dotnet restore
    ```
    
6. **Run the Application**  
   - Launch the application using your IDE's **Run** option or with:  
     ```bash
     dotnet run
     ```
     
🎉 **That's it! The application is up and running. Enjoy!**  

## 📸 Screenshots




  
