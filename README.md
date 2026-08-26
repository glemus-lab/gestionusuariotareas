# Prueba Técnica - API REST de Gestión de Usuarios y Tareas

Esta solución consiste en una API REST desarrollada con **.NET 10 (C#)** para la gestión integrada de usuarios y sus tareas asociadas. El proyecto ha sido diseñado bajo los lineamientos más estrictos de **Arquitectura Limpia (Clean Architecture)**, garantizando una total separación de conceptos, alta mantenibilidad y facilidad para realizar pruebas de software.

## 🛠️ Stack Tecnológico Utilizado

*   **Backend:** .NET 10 (C#) Web API.

*   **Acceso a Datos:** Entity Framework Core (ORM) utilizando el Patrón Repositorio.

*   **Base de Datos:** Microsoft SQL Server 2022.

*   **Validaciones:** FluentValidation centralizado mediante un despachador genérico de dependencias en la capa de Aplicación.

*   **Arquitectura:** Clean Architecture combinada con el patrón *Result* (para el manejo consistente de respuestas y listas de errores sin lanzar excepciones de control de flujo).

*   **Contenedores:** Docker y Docker Compose para despliegue automatizado.

---

## 📂 Estructura de la Solución (Arquitectura Limpia)

El proyecto se encuentra organizado directamente en la raíz de la solución, dividido en cuatro capas desacopladas:

*   **Domain:** Contiene las entidades puras del negocio (`Usuario`, `Tarea`), interfaces de contratos de datos y la estructura base del objeto `Result`. No tiene dependencias externas.

*   **Application:** Gobierna la lógica del negocio. Contiene las interfaces de los servicios, implementaciones, DTOs (`record`), validadores y el motor de inyección de dependencias modular.

*   **Infrastructure:** Capa encargada de la persistencia física. Incluye el `AppDbContext` de Entity Framework, migraciones y la implementación del patrón repositorio utilizando inicializaciones genéricas (`Set<T>`).

*   **WebApi:** Proyecto de inicio que expone los controladores REST, middlewares globales y la configuración nativa de documentación OpenAPI.

---

## 🚀 Instrucciones de Ejecución

El proyecto está diseñado para inicializarse por completo con un solo comando, configurando la API, levantando la base de datos en SQL Server y aplicando las migraciones automáticamente con resiliencia de arranque.

### Prerrequisitos

*   Tener instalado y ejecutándose [Docker Desktop](https://docker.com).

### Ejecución con Docker Compose (Recomendado)

1. Abra una terminal en la raíz del proyecto

2. Ejecute el siguiente comando para compilar las imágenes e iniciar los servicios:

```
bash

docker compose up --build
```

3. El sistema encenderá el motor de SQL Server, compilará las capas y aplicará de forma automática el esquema de la base de datos.

---

## 🌐 Endpoints de la API y Documentación

De acuerdo a las especificaciones técnicas requeridas, la documentación interactiva de Swagger se encuentra disponible exclusivamente en la siguiente ruta:

*   **Documentación Swagger UI:** [http://localhost:8080/api-docs](http://localhost:8080/api-docs)

*   **JSON de OpenAPI Nativo:** [http://localhost:8080/openapi/v1.json](http://localhost:8080/openapi/v1.json)

### Mapa de Rutas RESTful Implementadas

#### 👥 Gestión de Usuarios (`api/usuarios`)

*   `POST /api/usuarios` - Crear un usuario

*   `GET /api/usuarios/{id}` - Obtener el detalle de un usuario.

*   `PUT /api/usuarios/{id}` - Actualizar los datos de un usuario.

*   `DELETE /api/usuarios/{id}` - Eliminar un usuario (Aplica borrado en cascada de sus tareas).



#### 📝 Gestión de Tareas (`api/tareas`)

*   `POST /api/tareas` - Crear una tarea asociada a un usuario.

*   `GET /api/tareas/usuario/{id}` - Listar todas las tareas asignadas a un usuario específico.

*   `PUT /api/tareas/{id}` - Actualizar el estado de una tarea (completada/no completada mediante payload booleano).

*   `DELETE /api/tareas/{id}` - Eliminar una tarea de forma independiente.

### Postman

Se incluye una colección de Postman en la raíz del proyecto (`GestionUsuarioTarea.postman_collection.json`) con todos los endpoints configurados para facilitar las pruebas de la API.


