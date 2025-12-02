# 📋 TodoApi - API REST para Gestión de Tareas

API REST desarrollada en .NET 8 para la gestión de listas de tareas (Todo Lists) y sus ítems correspondientes, con persistencia en PostgreSQL.

## 📑 Tabla de Contenidos

- [Características](#-características)
- [Tecnologías](#-tecnologías)
- [Requisitos Previos](#-requisitos-previos)
- [Configuración](#-configuración)
  - [Base de Datos (PostgreSQL)](#-configuración-de-la-base-de-datos-postgresql)
  - [Cadena de Conexión](#-configurar-cadena-de-conexión)
  - [Migraciones](#-aplicar-migraciones)
- [Ejecución](#-ejecución)
- [Endpoints de la API](#-endpoints-de-la-api)
  - [Lists (Listas)](#lists-listas)
  - [Items (Tareas)](#items-tareas)
- [Testing](#-testing)
- [Estructura del Proyecto](#-estructura-del-proyecto)

## ✨ Características

- ✅ CRUD completo de listas de tareas
- ✅ CRUD completo de ítems dentro de cada lista
- ✅ Relación uno-a-muchos entre listas e ítems
- ✅ Documentación interactiva con Swagger/OpenAPI
- ✅ Entity Framework Core con PostgreSQL
- ✅ Arquitectura basada en DTOs
- ✅ Testing unitario con xUnit
- ✅ Validación de datos y manejo de errores

## 🛠️ Tecnologías

- **.NET 8.0** - Framework principal
- **ASP.NET Core Web API** - Framework para APIs REST
- **Entity Framework Core 8.0.11** - ORM para acceso a datos
- **PostgreSQL** - Base de datos relacional
- **Npgsql.EntityFrameworkCore.PostgreSQL** - Provider de EF Core para PostgreSQL
- **Swagger/OpenAPI** - Documentación de API
- **xUnit** - Framework de testing

## 📋 Requisitos Previos

Antes de comenzar, asegúrate de tener instalado:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/) (versión 12 o superior)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) o [VS Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/) (opcional)

## ⚙️ Configuración

### 🗄️ Configuración de la Base de Datos (PostgreSQL)

#### 1. Crear la base de datos

Abre pgAdmin o psql y ejecuta:

```sql
CREATE DATABASE postgres;
```

> **Nota:** Si decides usar otro nombre, actualiza el `appsettings.json` en consecuencia.

#### 2. Crear tablas necesarias

Las tablas se crearán automáticamente usando Entity Framework Migrations:

```bash
dotnet ef database update --project TodoApi
```

#### Esquema de tablas creadas:

**Tabla `List`:**
| Columna | Tipo | Descripción |
|---------|------|-------------|
| id | bigint | Identificador único (PK) |
| name | varchar | Nombre de la lista |

**Tabla `Item`:**
| Columna | Tipo | Descripción |
|---------|------|-------------|
| id | bigint | Identificador único (PK) |
| Name | varchar | Título del ítem |
| Description | varchar | Descripción del ítem |
| IsComplete | boolean | Estado de completado (default: false) |
| ListId | bigint | Referencia a la lista (FK) |

### 🔗 Configurar Cadena de Conexión

Edita el archivo `TodoApi/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "TodoContext": "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=TU_CONTRASEÑA"
  }
}
```

Reemplaza `TU_CONTRASEÑA` con la contraseña de tu usuario de PostgreSQL.

### 📦 Restaurar paquetes

```bash
dotnet restore
```

### 🔄 Aplicar Migraciones

Si necesitas crear nuevas migraciones después de modificar los modelos:

```bash
# Crear nueva migración
dotnet ef migrations add NombreMigracion --project TodoApi

# Aplicar migraciones
dotnet ef database update --project TodoApi
```

## 🚀 Ejecución

### Ejecutar la aplicación

```bash
cd TodoApi
dotnet run
```

La API estará disponible en:
- **HTTP:** `http://localhost:5000`
- **HTTPS:** `https://localhost:5001`
- **Swagger UI:** `http://localhost:5000` (página principal)

### Verificar que funciona

Abre tu navegador en `http://localhost:5000` para acceder a Swagger UI y probar los endpoints interactivamente.

## 📡 Endpoints de la API

### Lists (Listas)

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| `GET` | `/api/lists` | Obtiene todas las listas |
| `GET` | `/api/lists/{id}` | Obtiene una lista por ID |
| `POST` | `/api/lists` | Crea una nueva lista |
| `PUT` | `/api/lists/{id}` | Actualiza una lista existente |
| `DELETE` | `/api/lists/{id}` | Elimina una lista |

#### Ejemplo de uso:

**Crear lista:**
```bash
curl -X POST http://localhost:5000/api/lists \
  -H "Content-Type: application/json" \
  -d '{"name": "Mi Lista de Tareas"}'
```

**Respuesta:**
```json
{
  "id": 1,
  "name": "Mi Lista de Tareas"
}
```

### Items (Tareas)

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| `GET` | `/api/items` | Obtiene todos los ítems |
| `GET` | `/api/items/{id}` | Obtiene un ítem por ID |
| `POST` | `/api/items` | Crea un nuevo ítem |
| `PUT` | `/api/items/{id}` | Actualiza un ítem existente |
| `DELETE` | `/api/items/{id}` | Elimina un ítem |

#### Ejemplo de uso:

**Crear ítem:**
```bash
curl -X POST http://localhost:5000/api/items \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Comprar leche",
    "description": "Ir al supermercado",
    "isComplete": false,
    "listId": 1
  }'
```

**Respuesta:**
```json
{
  "id": 1,
  "title": "Comprar leche",
  "description": "Ir al supermercado",
  "isComplete": false,
  "listId": 1
}
```

**Actualizar ítem:**
```bash
curl -X PUT http://localhost:5000/api/items/1 \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Comprar leche",
    "description": "Ya comprada",
    "isComplete": true
  }'
```

## 🧪 Testing

El proyecto incluye pruebas unitarias usando xUnit y bases de datos en memoria.

### Ejecutar tests

```bash
dotnet test
```

### Tests incluidos

- ✅ Obtener todas las listas
- ✅ Obtener lista por ID
- ✅ Crear nueva lista
- ✅ Actualizar lista existente
- ✅ Eliminar lista
- ✅ Validación de entidades inexistentes

## 📂 Estructura del Proyecto

```
TecnicaBrunoLarroca/
├── TodoApi/                      # Proyecto principal de la API
│   ├── Controllers/              # Controladores de API
│   │   ├── ListController.cs    # Endpoints de listas
│   │   └── ItemController.cs    # Endpoints de ítems
│   ├── Data/                     # Contexto de base de datos
│   │   └── TodoContext.cs       # DbContext de Entity Framework
│   ├── Dtos/                     # Data Transfer Objects
│   │   ├── List/                # DTOs de listas
│   │   └── Item/                # DTOs de ítems
│   ├── Models/                   # Modelos de dominio
│   │   ├── List.cs              # Modelo de lista
│   │   └── Item.cs              # Modelo de ítem
│   ├── Migrations/               # Migraciones de EF Core
│   ├── Program.cs                # Punto de entrada de la aplicación
│   └── appsettings.json          # Configuración de la aplicación
│
└── TodoApi.Tests/                # Proyecto de pruebas unitarias
    └── Controllers/
        └── TodoListsControllerTests.cs
```
## 📝 Licencia

Este proyecto fue desarrollado como prueba técnica.

## 👤 Autor

**Bruno Larroca**

---
