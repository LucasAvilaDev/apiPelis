🎬 CineHub – Aplicación Web de Películas

CineHub es una aplicación web full stack para la gestión y visualización de películas, que integra autenticación segura, roles, chat en tiempo real y arquitectura moderna utilizando tecnologías actuales del ecosistema web.
Incluye un frontend en Angular y un backend en ASP.NET Core, con persistencia de datos tanto relacional (MySQL) como NoSQL (MongoDB).

🖼️ DEMO

https://youtu.be/o6_qtV9y984

🚀 Funcionalidades

👤 Usuarios (Espectadores)

Registro e inicio de sesión seguro con JWT
Exploración de películas
Marcar películas como favoritas
Chat grupal por película en tiempo real
Chat de soporte con administrador

🛠️ Administradores

Gestión completa de películas (CRUD)
Respuesta a consultas de usuarios vía chat
Control de acceso por roles

🧱 Stack Tecnológico
Frontend
Angular 19 (componentes standalone)
Bootstrap
Routing con Guards
HttpClient + Interceptor JWT
Backend
ASP.NET Core Web API
Entity Framework Core
ASP.NET Identity
Autenticación con JWT
SignalR (WebSockets)

Bases de Datos

MySQL: Usuarios, películas, categorías, favoritas

🏗️ Arquitectura General

Arquitectura cliente-servidor
API RESTful para operaciones CRUD
SignalR para comunicación en tiempo real
Separación de responsabilidades (Controllers, Services, Repositories)
Persistencia híbrida SQL + NoSQL





