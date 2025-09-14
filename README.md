#  <div align="center"> APP-CSHARP - WPF - TASK - MVVM - DEPENDENCYINJECTION </div>

[![fondo.png](https://i.postimg.cc/j5DgZYWD/fondo.png)](https://postimg.cc/jwr4S94T)

<h4 align="center">Management application built with C# and .NET using WPF, following the MVVM pattern for a clean and maintainable structure. It uses dependency injection for modularity and testability, and interacts with a RESTful API to manage business data. Asynchronous programming with Task ensures a responsive user interface during background operations.</h4>

<br>

## <div align="center">WPF</div>

[![WPF-CODE.png](https://i.postimg.cc/xTXfMXTQ/WPF-CODE.png)](https://postimg.cc/mtGWG2Hp)

<h4 align="center">For the development of the user interface, I used Windows Presentation Foundation (WPF) with the design tools provided by Visual Studio. The interface includes a wide range of functionalities, such as viewing data, adding new records, updating existing information, deleting entries, and providing convenient options for searching and filtering data.</h4>

<br>

## <div align="center">DEPENDENCY INJECTION</div>

<p align="center">
  <img src="https://i.postimg.cc/BQ2qMdD7/dependencias.png" alt="dependencias" />
</p>

<h4 align="center">The application implements dependency injection to promote a modular and loosely coupled architecture. By injecting dependencies rather than instantiating them directly, the system becomes easier to maintain, extend, and test. This approach facilitates the substitution of components (such as services, repositories, or controllers) without modifying the consuming classes, and aligns with SOLID principles—especially the Dependency Inversion Principle. Dependency injection is configured during application startup, ensuring that all necessary services are properly resolved and managed throughout the application lifecycle.</h4>

<br>

## <div align="center">MVVM</div>

<p align="center">
  <img src="https://i.postimg.cc/G3kNNr7D/mvvm.png" alt="dependencias" />
</p>

<h4 align="center">The application follows the Model-View-ViewModel (MVVM) architecture, which separates the user interface, presentation logic, and data management into distinct components. This structure enhances code organization, improves maintainability, and makes the application more scalable and easier to update or extend in the future.</h4>

<br>

## <div align="center">TASK</div>

<p align="center">
  <img src="https://i.postimg.cc/FFptzNy4/task.png" alt="dependencias" />
</p>

<h4 align="center">The application performs API calls to retrieve and manipulate data, enabling real-time interaction with external services. To maintain a smooth and responsive user experience, these operations are handled asynchronously using Task. This approach prevents the user interface from freezing during data processing, allowing the application to remain efficient and responsive even when dealing with longer-running network operations.</h4>

