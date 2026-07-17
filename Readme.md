# Lovato Optical App

Lovato Optical App es una aplicación web desarrollada en ASP.NET Core MVC para apoyar la gestión operativa de una óptica. El proyecto está orientado a centralizar funciones como la administración de clientes, catálogo de productos, pedidos, inventario, facturas y reportes en una solución modular y escalable.

## Descripción general

Esta aplicación funciona como una base para digitalizar los procesos de negocio de una óptica. Su enfoque actual está en la estructura de la solución y en la organización de módulos clave, con una arquitectura pensada para crecer hacia un sistema más completo.

## Funcionalidades principales

Actualmente la solución incluye módulos y pantallas para:

- Inicio y navegación principal
- Gestión de clientes
- Catálogo de productos
- Gestión de pedidos
- Inventario
- Facturación
- Reportes

## Arquitectura del proyecto

El proyecto está organizado en varias capas para separar responsabilidades y facilitar el mantenimiento:

- LovatoOpticalApp: aplicación web principal con controladores, vistas Razor y assets estáticos
- LovatoOpticalApp.Core: entidades de dominio, enums, interfaces y lógica central del negocio
- LovatoOpticalApp.Infrastructure: servicios de aplicación, DTOs, mapeos y contratos de servicios
- LovatoOpticalApp.Persistence: capa de persistencia para futuras integraciones con base de datos

## Tecnologías utilizadas

- ASP.NET Core MVC
- C#
- Razor Views
- Dependency Injection
- AutoMapper
- Docker

## Estructura del repositorio

```text
LovatoOpticalApp/           # Aplicación web principal
  Controllers/              # Controladores MVC
  Views/                    # Vistas Razor
  wwwroot/                  # Archivos estáticos (CSS, JS, imágenes)

LovatoOpticalApp.Core/      # Dominio y entidades
LovatoOpticalApp.Infrastructure/  # Servicios y DTOs
LovatoOpticalApp.Persistence/     # Persistencia
```

## Requisitos previos

Para ejecutar el proyecto, necesitas:

- .NET SDK 10.0 o superior
- Un terminal con acceso a dotnet
- Docker (opcional, si deseas ejecutar el proyecto en contenedor)

## Cómo ejecutar la aplicación

Desde la raíz del repositorio, ejecuta los siguientes comandos:

```bash
dotnet restore LovatoOpticalApp/LovatoOpticalApp.slnx
dotnet build LovatoOpticalApp/LovatoOpticalApp.slnx
dotnet run --project LovatoOpticalApp/LovatoOpticalApp.csproj
```

Luego abre la URL que indique la consola del proyecto en tu navegador.

## Estado actual del proyecto

El proyecto se encuentra en desarrollo inicial. La estructura base está bien definida y los módulos principales ya están esbozados, pero algunas funcionalidades de negocio aún requieren implementación adicional, especialmente en la capa de persistencia y en la lógica completa de operaciones como clientes y pedidos.

## Próximos pasos recomendados

- Completar el CRUD de clientes
- Implementar la persistencia real con base de datos
- Finalizar la lógica de pedidos, inventario y facturación
- Agregar autenticación y autorización
- Incorporar pruebas automatizadas

## Conclusión

Lovato Optical representa una base sólida para construir una solución completa de gestión para una óptica, con una arquitectura limpia y modular que facilita su evolución.
